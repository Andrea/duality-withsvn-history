using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	public class PixmapPropertyEditor : ResourcePropertyEditor
	{
		public PixmapPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			PixmapPreviewPropertyEditor preview = new PixmapPreviewPropertyEditor(this, this.ParentGrid);
			preview.EditedType = this.EditedType;
			preview.Getter = this.Getter;
			this.AddPropertyEditor(preview);
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Pixmap_PixelData)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Pixmap_PixelDataBasePath)) return false;
			return base.MemberPredicate(info);
		}
	}
}
