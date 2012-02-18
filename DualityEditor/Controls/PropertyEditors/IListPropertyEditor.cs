using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.EditorHints;

namespace DualityEditor.Controls.PropertyEditors
{
	public class IListPropertyEditor : GroupedPropertyEditor
	{
		private	NumericPropertyEditor	sizeEditor		= null;
		private	NumericPropertyEditor	offsetEditor	= null;
		private	int						offset			= 0;

		public IListPropertyEditor()
		{
			this.sizeEditor = new NumericPropertyEditor();
			this.sizeEditor.EditedType = typeof(uint);
			this.sizeEditor.PropertyName = "Size";
			this.sizeEditor.Getter = this.SizeValueGetter;
			this.sizeEditor.Setter = this.SizeValueSetter;
			this.offsetEditor = new NumericPropertyEditor();
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
			IList[] values = this.Getter().Cast<IList>().ToArray();
			Type elementType = this.GetElementType();

			this.Header.ResetEnabled = !this.ReadOnly;
			if (values == null)
			{
				this.Header.ValueText = null;
				return;
			}

			string valString = null;
			this.UpdateModifiedState();
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
					this.UpdateElementEditors(values, elementType);
					foreach (PropertyEditor e in this.PropertyEditors)
						e.PerformGetValue();
				}

				this.Header.ExpandEnabled = !this.ContentInitialized || this.PropertyEditors.Any();
				if (!this.Header.ExpandEnabled) this.Expanded = false;
				this.Header.ResetVisible = true;
				this.Header.ResetIsInit = false;

				valString = values.Count() == 1 ? 
					string.Format("{0}, Count = {1}", this.EditedType.GetTypeCSCodeName(true), values.First().Count) :
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

		protected Type GetElementType()
		{
			if (typeof(Array).IsAssignableFrom(this.EditedType))
				return this.EditedType.GetElementType() ?? typeof(object);
			else if (this.EditedType != null && this.EditedType.IsGenericType)
				return this.EditedType.GetGenericArguments()[0];
			else
				return typeof(object);
		}
		protected void UpdateElementEditors(IList[] values, Type elementType)
		{
			PropertyInfo indexer = typeof(IList).GetProperty("Item");
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
				this.AddPropertyEditor(this.sizeEditor);
				this.AddPropertyEditor(this.offsetEditor);
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
					elementEditor = this.ParentGrid.PropertyEditorProvider.CreateEditor(elementType);
					this.AddPropertyEditor(elementEditor);
				}
				elementEditor.Getter = this.CreateElementValueGetter(indexer, i - 2 + this.offset);
				elementEditor.Setter = this.CreateElementValueSetter(indexer, i - 2 + this.offset);
				elementEditor.PropertyName = "Element " + (i - 2 + this.offset);
			}
			// Remove overflowing editors
			for (int i = this.PropertyEditors.Count() - 3; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.PropertyEditors.Last());
			}
			this.EndUpdate();
		}

		protected IEnumerable<object> SizeValueGetter()
		{
			return this.Getter().Select(o => o != null ? (object)((IList)o).Count : null);
		}
		protected void SizeValueSetter(IEnumerable<object> values)
		{
			IEnumerator<object> valuesEnum = values.GetEnumerator();
			IList[] targetArray = this.Getter().Cast<IList>().ToArray();
			Type elementType = this.GetElementType();

			bool writeBack = false;
			uint curValue = 0;
			if (valuesEnum.MoveNext()) curValue = (uint)valuesEnum.Current;
			for (int t = 0; t < targetArray.Length; t++)
			{
				IList target = targetArray[t];
				if (target != null)
				{
					if (!target.IsFixedSize && !target.IsReadOnly)
					{
						// Dynamically adjust IList length
						while (target.Count < curValue)
							target.Add(elementType.IsValueType ? ReflectionHelper.CreateInstanceOf(elementType) : null);
						while (target.Count > curValue)
							target.RemoveAt(target.Count - 1);
					}
					else if (target is Array)
					{
						// Create new array that replaces the old one
						Array newTarget = Array.CreateInstance(elementType, curValue);
						for (int i = 0; i < Math.Min(curValue, target.Count); i++) newTarget.SetValue(target[i], i);
						targetArray[t] = newTarget;
						writeBack = true;
					}
					else
					{
						// Just some read-only container? Well, can't do anything here.
					}
				}
				if (valuesEnum.MoveNext()) curValue = (uint)valuesEnum.Current;
			}
			if (writeBack || this.ForceWriteBack) this.Setter(targetArray);
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
		protected Func<IEnumerable<object>> CreateElementValueGetter(PropertyInfo indexer, int index)
		{
			return () => this.Getter().Select(o => o != null ? indexer.GetValue(o, new object[] {index}) : null);
		}
		protected Action<IEnumerable<object>> CreateElementValueSetter(PropertyInfo indexer, int index)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.Getter().ToArray();

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) indexer.SetValue(target, curValue, new object[] {index});
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.UpdateModifiedState();
				if (this.ForceWriteBack) this.Setter(targetArray);
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
				IList newIList = null;
				if (typeof(Array).IsAssignableFrom(this.EditedType))
					newIList = Array.CreateInstance(this.GetElementType(), 0);
				else
					newIList = (IList)ReflectionHelper.CreateInstanceOf(this.EditedType);

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
