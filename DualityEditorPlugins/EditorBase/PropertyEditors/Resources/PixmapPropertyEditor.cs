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
		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			PixmapPreviewPropertyEditor preview = new PixmapPreviewPropertyEditor();
			preview.EditedType = this.EditedType;
			preview.Getter = this.Getter;
			this.AddPropertyEditor(preview);
		}
	}
}
