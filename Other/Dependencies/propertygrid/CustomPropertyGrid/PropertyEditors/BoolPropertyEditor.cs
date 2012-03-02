using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace CustomPropertyGrid.PropertyEditors
{
	public class BoolPropertyEditor : PropertyEditor
	{
		private	CheckState	state	= CheckState.Unchecked;
		private	bool		hovered	= false;
		private	bool		pressed	= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.state == CheckState.Checked, this.EditedType); }
		}
		
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.BeginUpdate();
			object[] values = this.GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.state = CheckState.Unchecked;
			else
			{
				int trueCount = values.Count(o => o != null && (bool)Convert.ToBoolean(o));
				if (!this.ReadOnly && (values.Any(o => o == null) || (trueCount > 0 && trueCount < values.Count())))
					this.state = CheckState.Indeterminate;
				else
					this.state = trueCount > 0 ? CheckState.Checked : CheckState.Unchecked;
			}

			this.EndUpdate();
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;
			if (this.state == CheckState.Indeterminate) return;

			this.SetValue(this.DisplayedValue);
		}

		protected void ToggleState()
		{
			if (this.state == CheckState.Checked)
				this.state = CheckState.Unchecked;
			else
				this.state = CheckState.Checked;

			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			CheckBoxState boxState = CheckBoxState.UncheckedNormal;
			bool boxEnabled = this.Enabled && !this.ReadOnly;
			if (this.state == CheckState.Checked)
			{
				if (!boxEnabled)		boxState = CheckBoxState.CheckedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.CheckedPressed;
				else if (this.hovered)	boxState = CheckBoxState.CheckedHot;
				else					boxState = CheckBoxState.CheckedNormal;				
			}
			else if (this.state == CheckState.Unchecked)
			{
				if (!boxEnabled)		boxState = CheckBoxState.UncheckedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.UncheckedPressed;
				else if (this.hovered)	boxState = CheckBoxState.UncheckedHot;
				else					boxState = CheckBoxState.UncheckedNormal;	
			}
			else if (this.state == CheckState.Indeterminate)
			{
				if (!boxEnabled)		boxState = CheckBoxState.MixedDisabled;
				else if (this.pressed)	boxState = CheckBoxState.MixedPressed;
				else if (this.hovered)	boxState = CheckBoxState.MixedHot;
				else					boxState = CheckBoxState.MixedNormal;	
			}

			Size boxSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, boxState);
			Point boxLoc = new Point(
				this.ClientRectangle.X + 2,
				this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - boxSize.Height / 2 - 1);
			CheckBoxRenderer.DrawCheckBox(e.Graphics, boxLoc, Rectangle.Empty, "", null, this.Focused, boxState);
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			bool lastHovered = this.hovered;
			this.hovered = this.ClientRectangle.Contains(e.Location);
			if (lastHovered != this.hovered) this.Invalidate();
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.hovered) this.Invalidate();
			this.hovered = false;
			this.pressed = false;
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.hovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (!this.pressed) this.Invalidate();
				this.pressed = true;
			}
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if ((e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.pressed) this.Invalidate();
				this.pressed = false;
			}
			if (this.hovered) this.ToggleState();
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return)
			{
				this.ToggleState();
				e.Handled = true;
			}
		}
	}
}
