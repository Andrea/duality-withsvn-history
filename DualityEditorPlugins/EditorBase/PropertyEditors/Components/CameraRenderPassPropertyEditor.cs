using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Duality;
using Duality.Components.Renderers;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class CameraRenderPassPropertyEditor : MemberwisePropertyEditor
	{
		protected override void BeforeAutoCreateEditors()
		{
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Input);
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_Output);
			this.AddEditorForProperty(ReflectionInfo.Property_Camera_RenderPass_VisibilityMask);
			base.BeforeAutoCreateEditors();
		}
		protected override bool IsAutoCreateMember(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Input)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_Output)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_VisibilityMask)) return false;
			return base.IsAutoCreateMember(info);
		}
		protected override PropertyEditor AutoCreateMemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_RenderPass_VisibilityMask))
			{
				BitmaskPropertyEditor e = new BitmaskPropertyEditor();
				e.EditedType = (info as PropertyInfo).PropertyType;
				// ToDo: Use actual user-definable visibility groups
				List<BitmaskItem> items = Enumerable.Range(0, 31).Select(i => new BitmaskItem(1UL << i, "Group " + i)).ToList();
				items.Insert(0, new BitmaskItem(0, "None"));
				items.Add(new BitmaskItem((1UL << 32) - 1, "All"));
				e.Items = items;
				this.ParentGrid.ConfigureEditor(e);
				return e;
			}
			return base.AutoCreateMemberEditor(info);
		}
	}
}
