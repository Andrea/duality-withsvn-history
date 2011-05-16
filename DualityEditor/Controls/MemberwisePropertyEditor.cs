using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;

namespace DualityEditor.Controls
{
	public class MemberwisePropertyEditor : GroupedPropertyEditor
	{
		[Flags]
		public enum MemberFlags
		{
			None		= 0x0,

			Properties	= 0x1,
			Fields		= 0x2,

			All			= Properties | Fields,
			Default		= All
		}

		protected	MemberFlags	flags	= MemberFlags.Default;
		protected	Dictionary<PropertyEditor,MemberInfo>	memberMap	= new Dictionary<PropertyEditor,MemberInfo>();

		public MemberwisePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid, MemberFlags flags) : base(parentEditor, parentGrid)
		{
			this.flags = flags;
			this.Header.ResetClicked += new EventHandler(Header_ResetClicked);
		}

		public override void InitContent()
		{
			base.InitContent();

			this.ClearPropertyEditors();
			this.memberMap.Clear();
			if (this.EditedType != null)
			{
				// Generate and add property editors for the current type
				this.BeginUpdate();
				this.OnAddingEditors();
				if ((this.flags & MemberFlags.Properties) != MemberFlags.None)
				{
					PropertyInfo[] propArr = this.EditedType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
					var propQuery = 
						from p in propArr
						where p.CanRead && p.GetIndexParameters().Length == 0 && this.MemberPredicate(p)
						orderby ReflectionHelper.GetTypeHierarchyLevel(p.DeclaringType) ascending, p.Name
						select p;
					foreach (PropertyInfo prop in propQuery)
					{
						this.AddEditorForProperty(prop);
					}
				}
				if ((this.flags & MemberFlags.Fields) != MemberFlags.None)
				{
					FieldInfo[] fieldArr = this.EditedType.GetFields(BindingFlags.Instance | BindingFlags.Public);
					var fieldQuery =
						from f in fieldArr
						where this.MemberPredicate(f)
						orderby ReflectionHelper.GetTypeHierarchyLevel(f.DeclaringType) ascending, f.Name
						select f;
					foreach (FieldInfo field in fieldQuery)
					{
						this.AddEditorForField(field);
					}
				}
				this.EndUpdate();

				this.Header.ExpandEnabled = this.PropertyEditors.Any();
				this.PerformGetValue();
			}
		}
		protected virtual void OnAddingEditors()
		{

		}
		protected virtual PropertyEditor MemberEditor(MemberInfo info)
		{
			return null;
		}
		protected virtual bool MemberPredicate(MemberInfo info)
		{
			return true;
		}

		public void AddEditorForProperty(PropertyInfo prop)
		{
			PropertyEditor e = this.MemberEditor(prop);
			if (e == null) e = this.ParentGrid.PropertyEditorProvider.CreateEditor(prop.PropertyType, this, this.ParentGrid);
			e.Getter = this.CreatePropertyValueGetter(prop);
			e.Setter = prop.CanWrite ? this.CreatePropertyValueSetter(prop) : null;
			e.PropertyName = prop.Name;
			if (e is GroupedPropertyEditor) (e as GroupedPropertyEditor).Indent = 20;
			this.memberMap[e] = prop;
			this.AddPropertyEditor(e);
		}
		public void AddEditorForField(FieldInfo field)
		{
			PropertyEditor e = this.MemberEditor(field);
			if (e == null) e = this.ParentGrid.PropertyEditorProvider.CreateEditor(field.FieldType, this, this.ParentGrid);
			e.Getter = this.CreateFieldValueGetter(field);
			e.Setter = this.CreateFieldValueSetter(field);
			e.PropertyName = field.Name;
			if (e is GroupedPropertyEditor) (e as GroupedPropertyEditor).Indent = 20;
			this.memberMap[e] = field;
			this.AddPropertyEditor(e);
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.Getter().ToArray();

			this.Header.ResetEnabled = !this.ReadOnly;

			if (values == null)
			{
				this.Header.ValueText = null;
				return;
			}

			this.OnUpdateFromObjects(values);

			foreach (PropertyEditor e in this.PropertyEditors)
				e.PerformGetValue();
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (!this.PropertyEditors.Any()) return;

			foreach (PropertyEditor e in this.PropertyEditors)
				e.PerformSetValue();
		}
		protected virtual void OnUpdateFromObjects(object[] values)
		{
			string valString = null;

			if (!values.Any() || values.All(o => o == null))
			{
				this.ClearContent();

				this.Header.ExpandEnabled = false;
				this.Expanded = false;
				this.Header.ResetIsInit = true;
					
				valString = "null";
				this.ActiveState = true;
			}
			else
			{
				this.Header.ExpandEnabled = !this.ContentInitialized || this.PropertyEditors.Any();
				if (!this.Header.ExpandEnabled) this.Expanded = false;
				this.Header.ResetIsInit = false;

				valString = values.Count() == 1 ? 
					values.First().ToString() :
					string.Format(DualityEditor.EditorRes.GeneralRes.PropertyGrid_N_Objects, values.Count());
			}

			this.Header.ValueText = valString;
		}

		protected Func<IEnumerable<object>> CreatePropertyValueGetter(PropertyInfo property)
		{
			return () => this.Getter().Select(o => o != null ? property.GetValue(o, null) : null);
		}
		protected Func<IEnumerable<object>> CreateFieldValueGetter(FieldInfo field)
		{
			return () => this.Getter().Select(o => o != null ? field.GetValue(o) : null);
		}
		protected Action<IEnumerable<object>> CreatePropertyValueSetter(PropertyInfo property)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.Getter().ToArray();

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) property.SetValue(target, curValue, null);
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(property, targetArray);
				this.UpdateModifiedState();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType) this.Setter(targetArray);
			};
		}
		protected Action<IEnumerable<object>> CreateFieldValueSetter(FieldInfo field)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.Getter().ToArray();

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) field.SetValue(target, curValue);
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnFieldSet(field, targetArray);
				this.UpdateModifiedState();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType) this.Setter(targetArray);
			};
		}

		protected virtual void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{

		}
		protected virtual void OnFieldSet(FieldInfo property, IEnumerable<object> targets)
		{

		}

		private void Header_ResetClicked(object sender, EventArgs e)
		{
			if (this.EditedType.IsValueType)
			{
				this.SetterSingle(ReflectionHelper.CreateInstanceOf(this.EditedType));
			}
			else
			{
				if (this.Header.ResetIsInit)
				{
					this.SetterSingle(ReflectionHelper.CreateInstanceOf(this.EditedType));
					this.Expanded = true;
				}
				else
				{
					this.SetterSingle(null);
				}
			}

			this.PerformGetValue();
		}
	}
}
