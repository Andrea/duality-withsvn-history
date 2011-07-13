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
	public class SpriteRendererPropertyEditor : ComponentPropertyEditor
	{
		public SpriteRendererPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{

		}

		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			this.AddEditorForProperty(ReflectionInfo.Property_SpriteRenderer_CustomMaterial);
		}
		protected override bool MemberPredicate(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SpriteRenderer_BoundRadius)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SpriteRenderer_CustomMaterial)) return false;
			return base.MemberPredicate(info);
		}
		protected override PropertyEditor MemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Renderer_VisibilityGroup))
			{
				FlagPropertyEditor e = new FlagPropertyEditor(this, this.ParentGrid);
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				// ToDo: Use actual user-definable visibility groups
				e.AddFlag("None", 0);
				for (int i = 0; i < 32; ++i) e.AddFlag("Group " + i, 1UL << i);
				e.AddFlag("All", (1UL << 32) - 1);
				return e;
			}
			else if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SpriteRenderer_Rect))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor(
					ReflectionInfo.Property_SpriteRenderer_Rect.PropertyType, this, this.ParentGrid);
				RectPropertyEditor rectEdit = e as RectPropertyEditor;
				if (rectEdit != null)
				{
					rectEdit.EditorX.DecimalPlaces = 1;
					rectEdit.EditorY.DecimalPlaces = 1;
					rectEdit.EditorW.DecimalPlaces = 1;
					rectEdit.EditorH.DecimalPlaces = 1;
				}
				return e;
			}
			return base.MemberEditor(info);
		}
	}
}
