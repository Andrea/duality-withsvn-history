using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using Duality;
using Duality.Resources;
using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class TextureContentPropertyEditor : ResourcePropertyEditor
	{
		public TextureContentPropertyEditor()
		{
			this.Hints = HintFlags.None;
			this.HeaderHeight = 0;
			this.HeaderValueText = null;
			this.Expanded = true;
		}

		protected override PropertyEditor AutoCreateMemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Texture_Atlas))
			{
				PropertyEditor e = this.ParentGrid.CreateEditor(ReflectionInfo.Property_Texture_Atlas.PropertyType, this);
				IListPropertyEditor listEdit = e as IListPropertyEditor;
				if (listEdit != null)
				{
					listEdit.EditorAdded += this.AtlasList_EditorAdded;
				}
				return e;
			}
			return base.AutoCreateMemberEditor(info);
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			Texture[] texArr = targets.Cast<Texture>().ToArray();
			bool anyReload = false;
			foreach (Texture tex in texArr)
			{
				if (tex.NeedsReload) 
				{
					tex.ReloadData();
					anyReload = true;
				}
			}

			if (anyReload ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Texture_AnimCols) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Texture_AnimRows) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Texture_Atlas))
			{
				this.PerformGetValue();
				(this.ParentEditor as TexturePropertyEditor).UpdatePreview();
			}
		}
		
		private void AtlasList_EditorAdded(object sender, PropertyEditorEventArgs e)
		{
			RectPropertyEditor rectEdit = e.Editor as RectPropertyEditor;
			if (rectEdit != null)
			{
				rectEdit.ConverterGet = this.AtlasConverterGet;
				rectEdit.ConverterSet = this.AtlasConverterSet;
			}
		}
		private object AtlasConverterGet(object val)
		{
			Rect rect = (Rect)val;
			Texture tex = this.GetValue().NotNull().FirstOrDefault() as Texture;
			if (tex == null) return rect;
			return new Rect(
				rect.X * tex.OglWidth,
				rect.Y * tex.OglHeight,
				rect.W * tex.OglWidth,
				rect.H * tex.OglHeight);
		}
		private object AtlasConverterSet(object val)
		{
			Rect rect = (Rect)val;
			Texture tex = this.GetValue().NotNull().FirstOrDefault() as Texture;
			if (tex == null) return rect;
			return new Rect(
				rect.X / tex.OglWidth,
				rect.Y / tex.OglHeight,
				rect.W / tex.OglWidth,
				rect.H / tex.OglHeight);
		}
	}
}
