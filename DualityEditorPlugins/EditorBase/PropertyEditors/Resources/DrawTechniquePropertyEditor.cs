using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class DrawTechniquePropertyEditor : ResourcePropertyEditor
	{
		public DrawTechniquePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{

		}

		protected override void OnPropertySet(System.Reflection.PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_DrawTechnique_Blending))
			{
				this.PerformGetValue();
			}
		}
		protected override PropertyEditor MemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_DrawTechnique_Blending))
			{
				EnumPropertyEditor e = new EnumPropertyEditor(this, this.ParentGrid);
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				e.Editor.Items.Remove("Count");
				e.Editor.Items.Remove("Reset");
				return e;
			}
			return base.MemberEditor(info);
		}
	}
}
