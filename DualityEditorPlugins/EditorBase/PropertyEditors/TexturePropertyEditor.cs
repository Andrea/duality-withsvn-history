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
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class TexturePropertyEditor : ResourcePropertyEditor
	{
		public TexturePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Atlas))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor(
					ReflectionHelper.Property_Texture_Atlas.PropertyType, this, this.ParentGrid);
				IListPropertyEditor listEdit = e as IListPropertyEditor;
				if (listEdit != null) listEdit.EditorAdded += this.listEdit_EditorAdded;
				return e;
			}
			return base.MemberEditor(info);
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Width)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Height)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Diameter)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_OglWidth)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_OglHeight)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_UVRatio)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Mipmaps)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_NeedsReload)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_AnimFrames)) return false;
			return base.MemberPredicate(info);
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
				ReflectionHelper.MemberInfoEquals(property, ReflectionHelper.Property_Texture_AnimCols) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionHelper.Property_Texture_AnimRows))
			{
				this.PerformGetValue();
			}
		}

		private void listEdit_EditorAdded(object sender, PropertyEditorEventArgs e)
		{
			RectPropertyEditor rectEdit = e.Editor as RectPropertyEditor;
			if (rectEdit != null)
			{
				rectEdit.ConverterGet = this.ConverterGet;
				rectEdit.ConverterSet = this.ConverterSet;
				rectEdit.EditorX.DecimalPlaces = 0;
				rectEdit.EditorY.DecimalPlaces = 0;
				rectEdit.EditorW.DecimalPlaces = 0;
				rectEdit.EditorH.DecimalPlaces = 0;
			}
		}
		private Rect ConverterGet(Rect rect)
		{
			Texture tex = this.Getter().NotNull().FirstOrDefault() as Texture;
			if (tex == null) return rect;
			return new Rect(
				rect.x * tex.OglWidth,
				rect.y * tex.OglHeight,
				rect.w * tex.OglWidth,
				rect.h * tex.OglHeight);
		}
		private Rect ConverterSet(Rect rect)
		{
			Texture tex = this.Getter().NotNull().FirstOrDefault() as Texture;
			if (tex == null) return rect;
			return new Rect(
				rect.x / tex.OglWidth,
				rect.y / tex.OglHeight,
				rect.w / tex.OglWidth,
				rect.h / tex.OglHeight);
		}
	}
}
