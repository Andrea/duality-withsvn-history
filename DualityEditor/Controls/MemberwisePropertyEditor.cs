using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.EditorHints;

namespace DualityEditor.Controls
{
	public class MemberwisePropertyEditor : GroupedPropertyEditor
	{
		protected	Dictionary<PropertyEditor,MemberInfo>	memberMap	= new Dictionary<PropertyEditor,MemberInfo>();

		public MemberwisePropertyEditor()
		{
			this.Header.ResetClicked += new EventHandler(Header_ResetClicked);
		}

		public override void InitContent()
		{
			base.InitContent();

			this.ClearContent();
			this.memberMap.Clear();
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
						orderby ReflectionHelper.GetTypeHierarchyLevel(p.DeclaringType) ascending, p.Name
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
			EditorHintFlagsAttribute flagsAttrib = info.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			return flagsAttrib == null || (flagsAttrib.Flags & MemberFlags.Invisible) == MemberFlags.None;
		}

		public PropertyEditor AddEditorForProperty(PropertyInfo prop)
		{
			EditorHintFlagsAttribute flagsAttrib = prop.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			bool flaggedReadOnly = flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.ReadOnly) != MemberFlags.None;

			PropertyEditor e = this.MemberEditor(prop);
			if (e == null) e = this.ParentGrid.PropertyEditorProvider.CreateEditor(prop.PropertyType);
			e.Getter = this.CreatePropertyValueGetter(prop);
			e.Setter = (prop.CanWrite && !flaggedReadOnly) ? this.CreatePropertyValueSetter(prop) : null;
			e.PropertyName = prop.Name;
			e.EditedMember = prop;
			if (e is GroupedPropertyEditor) (e as GroupedPropertyEditor).Indent = DefaultIndent;
			this.memberMap[e] = prop;
			this.AddPropertyEditor(e);
			return e;
		}
		public PropertyEditor AddEditorForField(FieldInfo field)
		{
			EditorHintFlagsAttribute flagsAttrib = field.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			bool flaggedReadOnly = flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.ReadOnly) != MemberFlags.None;

			PropertyEditor e = this.MemberEditor(field);
			if (e == null) e = this.ParentGrid.PropertyEditorProvider.CreateEditor(field.FieldType);
			e.Getter = this.CreateFieldValueGetter(field);
			e.Setter = !flaggedReadOnly ? this.CreateFieldValueSetter(field) : null;
			e.PropertyName = field.Name;
			e.EditedMember = field;
			if (e is GroupedPropertyEditor) (e as GroupedPropertyEditor).Indent = DefaultIndent;
			this.memberMap[e] = field;
			this.AddPropertyEditor(e);
			return e;
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
			EditorHintFlagsAttribute flagsAttrib = property.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			bool affectsOthers = flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.AffectsOthers) != MemberFlags.None;
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
				if (affectsOthers) this.PerformGetValue();
				this.UpdateModifiedState();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType || this.ForceWriteBack) this.Setter(targetArray);
			};
		}
		protected Action<IEnumerable<object>> CreateFieldValueSetter(FieldInfo field)
		{
			EditorHintFlagsAttribute flagsAttrib = field.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			bool affectsOthers = flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.AffectsOthers) != MemberFlags.None;
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
				if (affectsOthers) this.PerformGetValue();
				this.UpdateModifiedState();

				// Fixup struct values by assigning the modified struct copy to its original member
				if (this.EditedType.IsValueType || this.ForceWriteBack) this.Setter(targetArray);
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
