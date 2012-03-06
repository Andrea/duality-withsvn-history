using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;

namespace CustomPropertyGrid.EditorTemplates
{
	public class ComboBoxEditorTemplate : EditorTemplate
	{
		private	string		text		= null;

		public string SelectedText
		{
			get { return this.text; }
			set { this.text = value; }
		}
		
		public ComboBoxEditorTemplate(PropertyEditor parent) : base(parent) {}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			ControlRenderer.DrawComboButton(e.Graphics, this.rect, Renderer.ButtonState.Normal, this.text);
		}
	}
}
