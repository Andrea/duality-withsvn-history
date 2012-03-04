using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

using CustomPropertyGrid.Renderer;

namespace CustomPropertyGrid.EditorTemplates
{
	public class NumericEditorTemplate
	{
		private	bool					isTextValid		= false;
		private	decimal					value			= decimal.MinValue;
		private	int						decimalPlaces	= 0;
		private	Rectangle				rect			= Rectangle.Empty;
		private	StringEditorTemplate	stringEditor	= new StringEditorTemplate();

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
					this.stringEditor.Rect = new Rectangle(
						this.rect.X, 
						this.rect.Y, 
						this.rect.Width, 
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
		public decimal Value
		{
			get { return this.value; }
			set
			{
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
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (!this.rect.Contains(e.Location)) return;
			this.stringEditor.OnMouseDown(e);
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			this.stringEditor.OnMouseUp(e);
		}
		public void OnMouseMove(MouseEventArgs e)
		{
			this.stringEditor.OnMouseMove(e);
		}
		public void OnMouseLeave(EventArgs e)
		{
			this.stringEditor.OnMouseLeave(e);
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
		}
		protected void SetValueFromText()
		{
			decimal valResult;
			this.isTextValid = decimal.TryParse(this.stringEditor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valResult);
			if (this.isTextValid) this.value = valResult;
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
			if (this.isTextValid)
				this.EmitEditingFinished();
			else
				System.Media.SystemSounds.Beep.Play();
			this.SetTextFromValue();
			this.Select();
			this.EmitInvalidate();
		}
	}
}
