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
	public class CameraRenderPassPropertyEditor : MemberwisePropertyEditor
	{
		protected override void OnAddingEditors()
		{
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Input);
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Output);
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_VisibilityMask);
			base.OnAddingEditors();
		}
		protected override bool MemberPredicate(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Input)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Output)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_VisibilityMask)) return false;
			return base.MemberPredicate(info);
		}
		protected override PropertyEditor MemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_VisibilityMask))
			{
				FlagPropertyEditor e = new FlagPropertyEditor();
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				// ToDo: Use actual user-definable visibility groups
				e.AddFlag("None", 0);
				for (int i = 0; i < 32; ++i) e.AddFlag("Group " + i, 1UL << i);
				e.AddFlag("All", (1UL << 32) - 1);
				return e;
			}
			return base.MemberEditor(info);
		}
	}
}
