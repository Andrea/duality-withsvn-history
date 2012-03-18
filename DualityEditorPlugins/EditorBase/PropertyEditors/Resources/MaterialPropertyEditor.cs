using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using Duality;
using Duality.Resources;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class MaterialPropertyEditor : ResourcePropertyEditor
	{
		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			BatchInfoPropertyEditor e = this.AddEditorForField(ReflectionInfo.Field_Material_Info) as BatchInfoPropertyEditor;
			e.PropertyName = null;
			e.Hints = HintFlags.None;
			e.HeaderIcon = null;
			e.HeaderValueText = null;
			e.HeaderHeight = 0;
			e.Indent = 0;
			e.Expanded = true;
		}
		protected override bool IsAutoCreateMember(MemberInfo info)
		{
			return false;
		}
		protected override void OnFieldSet(FieldInfo property, IEnumerable<object> targets)
		{
			base.OnFieldSet(property, targets);
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(targets), ReflectionInfo.Property_Material_Info);
		}
	}
}
