using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Resources;

using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class PixmapPropertyEditor : MemberwisePropertyEditor
	{
		public PixmapPropertyEditor()
		{
			this.Indent = 0;
		}

		protected override bool IsAutoCreateMember(MemberInfo info)
		{
			return false;
		}
		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			PixmapPreviewPropertyEditor preview = new PixmapPreviewPropertyEditor();
			preview.EditedType = this.EditedType;
			preview.Getter = this.GetValue;
			this.ParentGrid.ConfigureEditor(preview);
			this.AddPropertyEditor(preview);
			PixmapContentPropertyEditor content = new PixmapContentPropertyEditor();
			content.EditedType = this.EditedType;
			content.Getter = this.GetValue;
			content.Setter = this.SetValues;
			this.ParentGrid.ConfigureEditor(content);
			this.AddPropertyEditor(content);
		}
	}
}
