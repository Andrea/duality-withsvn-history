using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Duality;
using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class CameraRenderPassPropertyEditor : MemberwisePropertyEditor
	{
		protected override void BeforeAutoCreateEditors()
		{
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Input);
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Output);
			base.BeforeAutoCreateEditors();
		}
		protected override bool IsAutoCreateMember(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Input)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Output)) return false;
			return base.IsAutoCreateMember(info);
		}
	}
}
