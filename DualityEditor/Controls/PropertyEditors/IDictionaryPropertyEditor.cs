using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;

namespace DualityEditor.Controls.PropertyEditors
{
	public class IDictionaryPropertyEditor : GroupedPropertyEditor
	{
		private	PropertyEditor			addKeyEditor	= null;
		private	object					addKey			= null;
		private	NumericPropertyEditor	offsetEditor	= null;
		private	int						offset			= 0;

		public IDictionaryPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.offsetEditor = new NumericPropertyEditor(this, parentGrid);
			this.offsetEditor.EditedType = typeof(uint);
			this.offsetEditor.PropertyName = "Offset";
			this.offsetEditor.Getter = this.OffsetValueGetter;
			this.offsetEditor.Setter = this.OffsetValueSetter;

			this.Header.ResetClicked += new EventHandler(Header_ResetClicked);
		}

		public override void InitContent()
		{
			base.InitContent();

			if (this.EditedType != null)
			{
				this.Header.ExpandEnabled = true;

				Type keyType = this.GetKeyType();
				this.addKeyEditor = ParentGrid.PropertyEditorProvider.CreateEditor(keyType, this, this.ParentGrid);
				this.addKeyEditor.EditedType = keyType;
				this.addKeyEditor.PropertyName = "Add Entry";
				this.addKeyEditor.Getter = this.AddKeyValueGetter;
				this.addKeyEditor.Setter = this.AddKeyValueSetter;
				this.addKeyEditor.EditingFinished += this.AddKeyEditingFinished;

				this.PerformGetValue();
			}
			else
				this.ClearContent();
		}

		public override void ClearContent()
		{
			base.ClearContent();
			this.offset = 0;
			this.offsetEditor.Visible = false;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			IDictionary[] values = this.Getter().Cast<IDictionary>().ToArray();
			Type valueType = this.GetValueType();

			this.Header.ResetEnabled = !this.ReadOnly;
			if (values == null)
			{
				this.Header.ValueText = null;
				return;
			}

			string valString = null;
			if (!values.Any() || values.All(o => o == null))
			{
				this.ClearContent();

				this.Header.ExpandEnabled = false;
				this.Expanded = false;
				this.Header.ResetVisible = true;
				this.Header.ResetIsInit = true;
					
				valString = "null";
			}
			else
			{
				if (this.ContentInitialized)
				{
					this.UpdateElementEditors(values, valueType);
					foreach (PropertyEditor e in this.PropertyEditors)
						e.PerformGetValue();
				}

				this.Header.ExpandEnabled = !this.ContentInitialized || this.PropertyEditors.Any();
				if (!this.Header.ExpandEnabled) this.Expanded = false;
				this.Header.ResetVisible = true;
				this.Header.ResetIsInit = false;

				valString = values.Count() == 1 ? 
					string.Format("{0}, Count = {1}", ReflectionHelper.GetTypeName(this.EditedType, TypeNameFormat.CSCodeIdentShort), values.First().Count) :
					string.Format(DualityEditor.EditorRes.GeneralRes.PropertyGrid_N_Objects, values.Count());
			}

			this.Header.ValueText = valString;
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (!this.PropertyEditors.Any()) return;

			foreach (PropertyEditor e in this.PropertyEditors)
			    e.PerformSetValue();
		}

		protected Type GetValueType()
		{
			if (this.EditedType.IsGenericType && this.EditedType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
				return this.EditedType.GetGenericArguments()[1];
			else
				return typeof(object);
		}
		protected Type GetKeyType()
		{
			if (this.EditedType.IsGenericType && this.EditedType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
				return this.EditedType.GetGenericArguments()[0];
			else
				return typeof(object);
		}
		protected void UpdateElementEditors(IDictionary[] values, Type elementType)
		{
			PropertyInfo indexer = typeof(IDictionary).GetProperty("Item");
			int visibleElementCount = values.Where(o => o != null).Min(o => (int)o.Count);
			if (visibleElementCount > 10)
			{
				this.offsetEditor.Visible = true;
				this.offset = Math.Min(this.offset, visibleElementCount - 10);
				visibleElementCount = 10;
			}
			else
			{
				this.offsetEditor.Visible = false;
				this.offset = 0;
			}

			this.BeginUpdate();
			if (!this.PropertyEditors.Any())
			{
				this.AddPropertyEditor(this.addKeyEditor);
				this.AddPropertyEditor(this.offsetEditor);
			}

			// Determine which keys are currently displayed
			int elemIndex = 0;
			object[] keys = new object[visibleElementCount];
			foreach (object key in values.NotNull().First().Keys)
			{
				if (elemIndex >= this.offset)
				{
					keys[elemIndex - this.offset] = key;
				}
				elemIndex++;
				if (elemIndex >= this.offset + visibleElementCount) break;
			}

			// Add missing editors
			for (int i = 2; i < visibleElementCount + 2; i++)
			{
				PropertyEditor elementEditor;
				if (i < this.PropertyEditors.Count())
				{
					elementEditor = this.PropertyEditors.ElementAt(i);
				}
				else
				{
					elementEditor = this.ParentGrid.PropertyEditorProvider.CreateEditor(elementType, this, this.ParentGrid);
					this.AddPropertyEditor(elementEditor);
				}
				elementEditor.Getter = this.CreateElementValueGetter(indexer, keys[i - 2]);
				elementEditor.Setter = this.CreateElementValueSetter(indexer, keys[i - 2]);
				elementEditor.PropertyName = keys[i - 2].ToString();
			}
			// Remove overflowing editors
			for (int i = this.PropertyEditors.Count() - 3; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.PropertyEditors.Last());
			}
			this.EndUpdate();
		}
		
		protected IEnumerable<object> AddKeyValueGetter()
		{
			//if (this.addKey == null) this.addKey = ReflectionHelper.CreateInstanceOf(this.GetKeyType());
			return new object[] { this.addKey };
		}
		protected void AddKeyValueSetter(IEnumerable<object> keys)
		{
			this.addKey = keys.FirstOrDefault();
		}
		protected void AddKeyEditingFinished(object sender, EventArgs e)
		{
			if (this.addKey == null) return;

			IDictionary[] targetArray = this.Getter().Cast<IDictionary>().ToArray();
			Type valueType = this.GetValueType();

			for (int t = 0; t < targetArray.Length; t++)
			{
				IDictionary target = targetArray[t];
				if (target != null)
				{
					if (!target.IsFixedSize && !target.IsReadOnly)
					{
						if (!target.Contains(this.addKey))
						{
							// Add a new key value pair
							target.Add(this.addKey, valueType.IsValueType ? ReflectionHelper.CreateInstanceOf(valueType) : null);
						}
					}
					else
					{
						// Just some read-only container? Well, can't do anything here.
					}
				}
			}
			this.addKey = null;
			this.PerformGetValue();
		}
		protected IEnumerable<object> OffsetValueGetter()
		{
			yield return (uint)this.offset;
		}
		protected void OffsetValueSetter(IEnumerable<object> values)
		{
			this.offset = (int)Convert.ChangeType(values.First(), typeof(int));
			this.PerformGetValue();
		}
		protected Func<IEnumerable<object>> CreateElementValueGetter(PropertyInfo indexer, object key)
		{
			return () => this.Getter().Select(o => o != null ? indexer.GetValue(o, new object[] {key}) : null);
		}
		protected Action<IEnumerable<object>> CreateElementValueSetter(PropertyInfo indexer, object key)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.Getter().ToArray();

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) indexer.SetValue(target, curValue, new object[] {key});
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.UpdateModifiedState();
			};
		}

		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			this.Expanded = false;
			this.ClearContent();
		}

		private void Header_ResetClicked(object sender, EventArgs e)
		{
			if (this.Header.ResetIsInit)
			{
				IDictionary newIList = null;
				newIList = (IDictionary)ReflectionHelper.CreateInstanceOf(this.EditedType);

				this.SetterSingle(newIList);
				this.Expanded = true;
			}
			else
			{
				this.SetterSingle(null);
			}

			this.PerformGetValue();
		}
	}
}
