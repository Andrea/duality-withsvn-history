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
	public class AudioDataPropertyEditor : ResourcePropertyEditor
	{
		public AudioDataPropertyEditor()
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
			AudioDataPreviewPropertyEditor preview = new AudioDataPreviewPropertyEditor();
			preview.EditedType = this.EditedType;
			preview.Getter = this.GetValue;
			this.ParentGrid.ConfigureEditor(preview);
			this.AddPropertyEditor(preview);
			ResourcePropertyEditor content = new ResourcePropertyEditor();
			content.EditedType = this.EditedType;
			content.Getter = this.GetValue;
			content.Setter = this.SetValues;
			content.Hints = HintFlags.None;
			content.HeaderHeight = 0;
			content.HeaderValueText = null;
			content.PreventFocus = true;
			this.ParentGrid.ConfigureEditor(content);
			this.AddPropertyEditor(content);
			content.Expanded = true;
		}
	}
}
