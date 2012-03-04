using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;
using CustomPropertyGrid.EditorTemplates;

namespace CustomPropertyGrid.PropertyEditors
{
	public class NumericPropertyEditor : PropertyEditor
	{
		private	NumericEditorTemplate	numEditor	= new NumericEditorTemplate();
		private	decimal	val			= 0m;
		private	bool	valMultiple	= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, this.EditedType); }
		}
		

		public NumericPropertyEditor()
		{
			this.numEditor.Invalidate += this.numEditor_Invalidate;
			this.numEditor.ValueEdited += this.numEditor_ValueEdited;
			this.numEditor.EditingFinished += this.numEditor_EditingFinished;

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

			this.numEditor.Value = this.val;
			this.EndUpdate();
		}
		
		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.numEditor.OnPaint(e, this.Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.numEditor.OnGotFocus(e);
			this.numEditor.Select();
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.numEditor.OnLostFocus(e);
		}
		protected internal override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.numEditor.OnKeyPress(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			this.numEditor.OnKeyDown(e);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.numEditor.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.numEditor.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.numEditor.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.numEditor.OnMouseUp(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.numEditor.Rect = new Rectangle(
				this.ClientRectangle.X + 1,
				this.ClientRectangle.Y + 1,
				this.ClientRectangle.Width - 2,
				this.ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.numEditor.ReadOnly = this.ReadOnly;
		}

		private void numEditor_ValueEdited(object sender, EventArgs e)
		{
			this.val = this.numEditor.Value;
			this.Invalidate();
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
		}
		private void numEditor_Invalidate(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		private void numEditor_EditingFinished(object sender, EventArgs e)
		{
			this.OnEditingFinished();
		}
	}
}
