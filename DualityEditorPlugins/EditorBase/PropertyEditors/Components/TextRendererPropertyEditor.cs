using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Components.Renderers;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class TextRendererPropertyEditor : ComponentPropertyEditor
	{
		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			this.AddEditorForProperty(ReflectionInfo.Property_TextRenderer_CustomMaterial);
		}
		protected override bool MemberPredicate(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_TextRenderer_CustomMaterial)) return false;
			return base.MemberPredicate(info);
		}
		protected override PropertyEditor MemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Renderer_VisibilityGroup))
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
		protected override void OnPropertySet(System.Reflection.PropertyInfo property, IEnumerable<object> targets)
		{
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_TextRenderer_Text))
			{
				TextRenderer[] renderers = targets.Cast<TextRenderer>().NotNull().ToArray();
				foreach (TextRenderer r in renderers)
					r.UpdateMetrics();
			}
			base.OnPropertySet(property, targets);
		}
	}
}
