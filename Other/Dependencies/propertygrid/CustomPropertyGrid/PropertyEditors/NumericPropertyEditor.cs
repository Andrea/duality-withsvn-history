using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;

namespace CustomPropertyGrid.PropertyEditors
{
	public class NumericPropertyEditor : PropertyEditor
	{
		private	decimal	val			= 0m;
		private	bool	valMultiple	= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, this.EditedType); }
		}
		

		public NumericPropertyEditor()
		{
			this.Height = 18;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.BeginUpdate();
			object[] values = this.GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.val = 0m;
			else
			{
				this.val = values.Where(o => o != null).Average(o => Convert.ToDecimal(o));
				this.valMultiple = values.Any(o => o == null) || !values.All(o => Convert.ToDecimal(o) == this.val);
			}

			this.EndUpdate();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			TextBoxState textBoxState;
			Rectangle textBoxRect = new Rectangle(
				this.ClientRectangle.X + 1,
				this.ClientRectangle.Y + 1,
				this.ClientRectangle.Width - 2,
				this.ClientRectangle.Height - 1);

			if (!this.Enabled)
				textBoxState = TextBoxState.Disabled;
			else if (this.Focused)
				textBoxState = TextBoxState.Focus;
			else
				textBoxState = TextBoxState.Normal;

			if (this.ReadOnly)
				textBoxState |= TextBoxState.ReadOnlyFlag;

			ControlRenderer.DrawTextField(
				e.Graphics, 
				textBoxRect, 
				this.val.ToString(), 
				SystemFonts.DefaultFont, 
				SystemColors.ControlText,
 				this.valMultiple ? Color.Bisque : SystemColors.Window,
				textBoxState, 
				TextBoxStyle.Sunken);
		}
	}
}
