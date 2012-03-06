using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using CustomPropertyGrid.Renderer;
using ButtonState = CustomPropertyGrid.Renderer.ButtonState;

namespace CustomPropertyGrid.EditorTemplates
{
	public class NumericEditorTemplate
	{
		public const int GripSize = 11;

		private static IconImage gripIcon = new IconImage(Properties.Resources.NumberGripIcon);

		private	bool					isTextValid		= false;
		private	bool					isValueClamped	= false;
		private	decimal					value			= decimal.MinValue;
		private	decimal					min				= decimal.MinValue;
		private	decimal					max				= decimal.MaxValue;
		private	decimal					increment		= 1;
		private	int						decimalPlaces	= 0;
		private	Rectangle				rect			= Rectangle.Empty;
		private	StringEditorTemplate	stringEditor	= new StringEditorTemplate();
		private	Rectangle				gripRect		= Rectangle.Empty;
		private	bool					gripHovered		= false;
		private	bool					gripPressed		= false;

		public event EventHandler Invalidate = null;
		public event EventHandler ValueEdited = null;
		public event EventHandler EditingFinished = null;

		public Rectangle Rect
		{
			get { return this.rect; }
			set
			{
				if (this.rect != value)
				{
					this.rect = value;
					this.gripRect = new Rectangle(
						this.rect.Right - GripSize + 2,
						this.rect.Y,
						GripSize,
						this.rect.Height);
					this.stringEditor.Rect = new Rectangle(
						this.rect.X, 
						this.rect.Y, 
						this.rect.Width - GripSize + 2, 
						this.rect.Height);
				}
			}
		}
		public bool ReadOnly
		{
			get { return this.stringEditor.ReadOnly; }
			set { this.stringEditor.ReadOnly = value; }
		}
		public int DecimalPlaces
		{
			get { return this.decimalPlaces; }
			set
			{
				value = Math.Max(Math.Min(this.decimalPlaces, 10), 0);
				if (this.decimalPlaces != value)
				{
					this.decimalPlaces = value;
					this.SetTextFromValue();
				}
			}
		}
		public decimal Maximum
		{
			get { return this.max; }
			set
			{
				if (this.max != value)
				{
					this.max = value;
					if (this.value > this.max) this.Value = this.max;
				}
			}
		}
		public decimal Minimum
		{
			get { return this.min; }
			set
			{
				if (this.min != value)
				{
					this.min = value;
					if (this.value > this.min) this.Value = this.min;
				}
			}
		}
		public decimal Increment
		{
			get { return this.increment; }
			set { this.increment = value; }
		}
		public decimal Value
		{
			get { return this.value; }
			set
			{
				value = Math.Max(Math.Min(value, this.max), this.min);
				if (this.value != value)
				{
					this.value = value;
					this.SetTextFromValue();
				}
			}
		}


		public NumericEditorTemplate()
		{
			this.stringEditor.Invalidate += this.ForwardInvalidate;
			this.stringEditor.TextEdited += this.stringEditor_TextEdited;
			this.stringEditor.EditingFinished += this.stringEditor_EditingFinished;
		}


		public void Select()
		{
			this.stringEditor.Select();
		}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			this.stringEditor.OnPaint(e, enabled, multiple);

			ButtonState gripState = ButtonState.Normal;
			if (!enabled)
				gripState = ButtonState.Disabled;
			if (this.gripPressed || Control.ModifierKeys.HasFlag(Keys.Control))
				gripState = ButtonState.Pressed;
			else if (this.gripHovered || this.stringEditor.Focused)
				gripState = ButtonState.Hot;
			Rectangle gfxGripRect = new Rectangle(this.gripRect.X - 1, this.gripRect.Y, this.gripRect.Width, this.gripRect.Height);
			ControlRenderer.DrawButton(e.Graphics, gfxGripRect, gripState, null, enabled ? gripIcon.Normal : gripIcon.Disabled);
		}

		public void OnGotFocus(EventArgs e)
		{
			this.stringEditor.OnGotFocus(e);
		}
		public void OnLostFocus(EventArgs e)
		{
			this.stringEditor.OnLostFocus(e);
		}
		public void OnKeyPress(KeyPressEventArgs e)
		{
			this.stringEditor.OnKeyPress(e);
		}
		public void OnKeyDown(KeyEventArgs e)
		{
			this.stringEditor.OnKeyDown(e);
			if (e.KeyCode == Keys.ControlKey)
			{
				this.EmitInvalidate();
			}
			else if (e.Control && e.KeyCode == Keys.Up)
			{
				this.Value += this.increment;
				this.EmitValueEdited();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.Down)
			{
				this.Value -= this.increment;
				this.EmitValueEdited();
				e.Handled = true;
			}
		}
		public void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				this.EmitInvalidate();
			}
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (!this.rect.Contains(e.Location)) return;
			this.stringEditor.OnMouseDown(e);
			if (this.gripHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.gripPressed = true;
				this.EmitInvalidate();
				// Begin grip action here
			}
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			this.stringEditor.OnMouseUp(e);
			if (this.gripPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.gripPressed = false;
				this.EmitInvalidate();
				// End grip action here
			}
		}
		public void OnMouseMove(MouseEventArgs e)
		{
			this.stringEditor.OnMouseMove(e);

			bool lastGripHovered = this.gripHovered;
			this.gripHovered = this.gripRect.Contains(e.Location);
			if (lastGripHovered != this.gripHovered) this.EmitInvalidate();

			if (this.gripPressed)
			{
				// Update grip action here
			}
		}
		public void OnMouseLeave(EventArgs e)
		{
			this.stringEditor.OnMouseLeave(e);

			if (this.gripHovered) this.EmitInvalidate();
			this.gripHovered = false;
		}

		protected void SetTextFromValue()
		{
			if (this.decimalPlaces > 0)
			{
				decimal beforeSep = Math.Floor(this.value);
				decimal afterSep = this.value - beforeSep;
				afterSep = Math.Round(afterSep * (decimal)Math.Pow(10.0d, this.decimalPlaces));
				this.stringEditor.Text = beforeSep.ToString() + "." + afterSep.ToString().PadLeft(this.decimalPlaces, '0');
			}
			else
			{
				this.stringEditor.Text = Math.Round(this.value).ToString();
			}
			this.isTextValid = true;
			this.isValueClamped = false;
		}
		protected void SetValueFromText()
		{
			decimal valResult;
			this.isTextValid = decimal.TryParse(this.stringEditor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valResult);
			if (this.isTextValid)
			{
				this.value = Math.Max(Math.Min(valResult, this.max), this.min);
				this.isValueClamped = this.value != valResult;
			}
			else
			{
				this.isValueClamped = false;
			}
		}

		protected void EmitInvalidate()
		{
			if (this.Invalidate != null)
				this.Invalidate(this, EventArgs.Empty);
		}
		protected void EmitValueEdited()
		{
			if (this.ValueEdited != null)
				this.ValueEdited(this, EventArgs.Empty);
		}
		protected void EmitEditingFinished()
		{
			if (this.EditingFinished != null)
				this.EditingFinished(this, EventArgs.Empty);
		}
		
		private void ForwardInvalidate(object sender, EventArgs e)
		{
			this.EmitInvalidate();
		}
		private void stringEditor_TextEdited(object sender, EventArgs e)
		{
			this.SetValueFromText();
			if (this.isTextValid)
				this.EmitValueEdited();
			else
				this.EmitInvalidate();
		}
		void stringEditor_EditingFinished(object sender, EventArgs e)
		{
			if (!this.isTextValid || this.isValueClamped) System.Media.SystemSounds.Beep.Play();
			if (this.isTextValid) this.EmitEditingFinished();
			this.SetTextFromValue();
			this.Select();
			this.EmitInvalidate();
		}
	}
}
