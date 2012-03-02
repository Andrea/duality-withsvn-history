using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace CustomPropertyGrid
{
	public class MemberwisePropertyEditor : GroupedPropertyEditor
	{
		private	object[]	curObjects	= null;

		public override object DisplayedValue
		{
			get { return this.curObjects; }
		}

		public override void InitContent()
		{
			base.InitContent();

			this.ClearContent();
			if (this.EditedType != null)
			{
				// Generate and add property editors for the current type
				this.BeginUpdate();
				this.OnAddingEditors();
				// Properties
				{
					PropertyInfo[] propArr = this.EditedType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
					var propQuery = 
						from p in propArr
						where p.CanRead && p.GetIndexParameters().Length == 0 && this.MemberPredicate(p)
						orderby GetTypeHierarchyLevel(p.DeclaringType) ascending, p.Name
						select p;
					foreach (PropertyInfo prop in propQuery)
					{
						this.AddEditorForProperty(prop);
					}
				}
				// Fields
				{
					FieldInfo[] fieldArr = this.EditedType.GetFields(BindingFlags.Instance | BindingFlags.Public);
					var fieldQuery =
						from f in fieldArr
						where this.MemberPredicate(f)
						orderby GetTypeHierarchyLevel(f.DeclaringType) ascending, f.Name
						select f;
					foreach (FieldInfo field in fieldQuery)
					{
						this.AddEditorForField(field);
					}
				}
				this.EndUpdate();
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

		public PropertyEditor AddEditorForProperty(PropertyInfo prop)
		{
			bool flaggedReadOnly = false;

			PropertyEditor e = this.MemberEditor(prop);
			if (e == null) e = this.ParentGrid.CreateEditor(prop.PropertyType);
			if (e == null) return null;
			e.Getter = this.CreatePropertyValueGetter(prop);
			e.Setter = (prop.CanWrite && !flaggedReadOnly) ? this.CreatePropertyValueSetter(prop) : null;
			e.PropertyName = prop.Name;
			e.EditedMember = prop;
			this.AddPropertyEditor(e);
			return e;
		}
		public PropertyEditor AddEditorForField(FieldInfo field)
		{
			bool flaggedReadOnly = false;

			PropertyEditor e = this.MemberEditor(field);
			if (e == null) e = this.ParentGrid.CreateEditor(field.FieldType);
			if (e == null) return null;
			e.Getter = this.CreateFieldValueGetter(field);
			e.Setter = !flaggedReadOnly ? this.CreateFieldValueSetter(field) : null;
			e.PropertyName = field.Name;
			e.EditedMember = field;
			this.AddPropertyEditor(e);
			return e;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.curObjects = this.GetValue().ToArray();

			if (this.curObjects == null)
			{
				return;
			}

			this.OnUpdateFromObjects(this.curObjects);

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

				this.Expanded = false;
					
				valString = "null";
			}
			else
			{
				valString = values.Count() == 1 ? 
					values.First().ToString() :
					string.Format(CustomPropertyGrid.Properties.Resources.PropertyGrid_N_Objects, values.Count());
			}
		}

		protected Func<IEnumerable<object>> CreatePropertyValueGetter(PropertyInfo property)
		{
			return () => this.curObjects.Select(o => o != null ? property.GetValue(o, null) : null);
		}
		protected Func<IEnumerable<object>> CreateFieldValueGetter(FieldInfo field)
		{
			return () => this.curObjects.Select(o => o != null ? field.GetValue(o) : null);
		}
		protected Action<IEnumerable<object>> CreatePropertyValueSetter(PropertyInfo property)
		{
			bool affectsOthers = false;
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.curObjects;

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) property.SetValue(target, curValue, null);
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(property, targetArray);
				if (affectsOthers) this.PerformGetValue();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType || this.ForceWriteBack) this.SetValue((IEnumerable<object>)targetArray);
			};
		}
		protected Action<IEnumerable<object>> CreateFieldValueSetter(FieldInfo field)
		{
			bool affectsOthers = false;
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<object> valuesEnum = values.GetEnumerator();
				object[] targetArray = this.curObjects;

				object curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (object target in targetArray)
				{
					if (target != null) field.SetValue(target, curValue);
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnFieldSet(field, targetArray);
				if (affectsOthers) this.PerformGetValue();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType || this.ForceWriteBack) this.SetValue((IEnumerable<object>)targetArray);
			};
		}

		protected virtual void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{

		}
		protected virtual void OnFieldSet(FieldInfo property, IEnumerable<object> targets)
		{

		}

		private static int GetTypeHierarchyLevel(Type t)
		{
			int level = 0;
			while (t.BaseType != null) { t = t.BaseType; level++; }
			return level;
		}
	}
}
