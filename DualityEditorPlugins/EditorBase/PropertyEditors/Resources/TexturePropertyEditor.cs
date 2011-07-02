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
		private TexturePreviewPropertyEditor preview = null;

		public TexturePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		public override void ClearContent()
		{
			base.ClearContent();
			this.preview = null;
		}
		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			if (this.preview == null) this.preview = new TexturePreviewPropertyEditor(this, this.ParentGrid);
			this.preview.EditedType = this.EditedType;
			this.preview.Getter = this.Getter;
			this.AddPropertyEditor(this.preview);
		}
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Atlas))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor(
					ReflectionHelper.Property_Texture_Atlas.PropertyType, this, this.ParentGrid);
				IListPropertyEditor listEdit = e as IListPropertyEditor;
				if (listEdit != null)
				{
					listEdit.EditorAdded += this.AtlasList_EditorAdded;
					listEdit.ValueEdited += this.AtlasList_ValueEdited;
				}
				return e;
			}
			return base.MemberEditor(info);
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_PxWidth)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_PxHeight)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_PxDiameter)) return false;
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
				ReflectionHelper.MemberInfoEquals(property, ReflectionHelper.Property_Texture_AnimRows) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionHelper.Property_Texture_Atlas))
			{
				this.PerformGetValue();
			}
		}
		
		private void AtlasList_ValueEdited(object sender, PropertyGridValueEditedEventArgs e)
		{
			this.preview.PerformGetValue();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
				this, new ObjectSelection(this.Getter()),
				ReflectionHelper.Property_Texture_Atlas);
		}
		private void AtlasList_EditorAdded(object sender, PropertyEditorEventArgs e)
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
