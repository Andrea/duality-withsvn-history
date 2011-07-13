using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class MaterialPropertyEditor : ResourcePropertyEditor
	{
		public MaterialPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			BatchInfoPropertyEditor e = this.AddEditorForField(ReflectionInfo.Field_Material_Info) as BatchInfoPropertyEditor;
			e.Header.Visible = false;
			e.Expanded = true;
			e.Indent = 0;
		}
		protected override bool MemberPredicate(MemberInfo info)
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
