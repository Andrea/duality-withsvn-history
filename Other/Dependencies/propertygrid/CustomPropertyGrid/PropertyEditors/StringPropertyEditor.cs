using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;

namespace CustomPropertyGrid.PropertyEditors
{
	public class StringPropertyEditor : PropertyEditor
	{
		private	StringEditorTemplate	stringEditor	= null;
		private string	val				= null;
		private	bool	valMultiple		= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, this.EditedType); }
		}
		

		public StringPropertyEditor()
		{
			this.stringEditor = new StringEditorTemplate();
			this.stringEditor.Invalidate += this.stringEditor_Invalidate;
			this.stringEditor.TextEdited += this.stringEditor_TextEdited;

			this.Height = 18;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.BeginUpdate();
			object[] values = this.GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.val = null;
			else
			{
				this.val = (string)values.First();
				this.valMultiple = values.Any(o => o == null || (string)o != this.val);
			}

			this.stringEditor.Text = this.val;
			this.EndUpdate();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.stringEditor.OnPaint(e, this.Enabled, this.valMultiple);
		}
		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.stringEditor.OnGotFocus(e);
			this.stringEditor.Select();
		}
		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.stringEditor.OnLostFocus(e);
		}
		protected internal override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.stringEditor.OnKeyPress(e);
		}
		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return)
			{
				this.OnEditingFinished();
				e.Handled = true;
			}
			else
			{
				this.stringEditor.OnKeyDown(e);
			}
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.stringEditor.OnMouseMove(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.stringEditor.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.stringEditor.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.stringEditor.OnMouseUp(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.stringEditor.Rect = new Rectangle(
				this.ClientRectangle.X + 1,
				this.ClientRectangle.Y + 1,
				this.ClientRectangle.Width - 2,
				this.ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.stringEditor.ReadOnly = this.ReadOnly;
		}

		private void stringEditor_Invalidate(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		private void stringEditor_TextEdited(object sender, EventArgs e)
		{
			this.val = this.stringEditor.Text;
			this.Invalidate();
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
		}
	}

	public class StringEditorTemplate
	{
		private	Rectangle	rect			= Rectangle.Empty;
		private	string		text			= null;
		private	bool		readOnly		= false;
		private	bool		hovered			= false;
		private	bool		focused			= false;
		private	Timer		cursorTimer		= null;
		private	bool		cursorVisible	= false;
		private	int			cursorIndex		= 0;
		private	int			selectionLength	= 0;
		private int			scroll			= 0;
		private	bool		mouseSelect		= false;

		public event EventHandler Invalidate = null;
		public event EventHandler TextEdited = null;

		public Rectangle Rect
		{
			get { return this.rect; }
			set { this.rect = value; }
		}
		public bool ReadOnly
		{
			get { return this.readOnly; }
			set { this.readOnly = value; }
		}
		public string Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
		public string SelectedText
		{
			get
			{
				if (this.text == null) return null;
				int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
				return this.text.Substring(begin, Math.Abs(this.selectionLength));
			}
		}

		public void Select(int pos = 0, int length = -1)
		{
			if (pos == 0 && length == -1) length = this.text != null ? this.text.Length : 0;
			this.cursorIndex = pos + length;
			this.selectionLength = -length;

			this.UpdateScroll();
			this.EmitInvalidate();
		}
		public void Deselect()
		{
			this.selectionLength = 0;

			this.EmitInvalidate();
		}
		public void DeleteSelection()
		{
			if (this.selectionLength == 0) return;
			if (this.text == null) return;
			if (this.readOnly) return;

			int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
			this.text = this.text.Remove(begin, Math.Abs(this.selectionLength));
			this.selectionLength = 0;
			this.cursorIndex = begin;

			this.UpdateScroll();
			this.EmitTextEdited();
		}
		public void InsertText(string insertText)
		{
			if (insertText == null) return;
			if (this.readOnly) return;

			if (this.text == null)
			{
				this.text = insertText;
				this.cursorIndex = 0;
			}
			else
			{
				int begin = Math.Min(this.cursorIndex, this.cursorIndex + this.selectionLength);
				this.text = 
					this.text.Substring(0, begin) + 
					insertText + 
					this.text.Substring(begin + Math.Abs(this.selectionLength), this.text.Length - begin - Math.Abs(this.selectionLength));
				this.cursorIndex = begin;
			}

			this.cursorIndex += insertText.Length;
			this.selectionLength = 0;

			this.UpdateScroll();
			this.EmitTextEdited();
		}
		public void ShowCursor()
		{
			this.cursorTimer.Stop();
			this.cursorTimer.Start();
			this.cursorVisible = true;
			this.EmitInvalidate();
		}
		public void UpdateScroll()
		{
			int cursorPixelPos = ControlRenderer.GetCharPosTextField(
				this.rect,
				this.text,
				SystemFonts.DefaultFont,
				TextBoxStyle.Sunken,
				this.cursorIndex,
				this.scroll);
			if (cursorPixelPos - this.rect.X < 15 && this.scroll > 0)
			{
				this.scroll = Math.Max(this.scroll - (15 - cursorPixelPos + this.rect.X), 0);
				this.EmitInvalidate();
			}
			else if (cursorPixelPos - this.rect.X > this.rect.Width - 15)
			{
				this.scroll += (cursorPixelPos - this.rect.X) - (this.rect.Width - 15);
				this.EmitInvalidate();
			}
		}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			TextBoxState textBoxState;

			if (!enabled)
				textBoxState = TextBoxState.Disabled;
			else if (this.focused)
				textBoxState = TextBoxState.Focus;
			else if (this.hovered)
				textBoxState = TextBoxState.Hot;
			else
				textBoxState = TextBoxState.Normal;

			if (this.readOnly)
				textBoxState |= TextBoxState.ReadOnlyFlag;

			ControlRenderer.DrawTextField(
				e.Graphics, 
				rect, 
				text, 
				SystemFonts.DefaultFont, 
				SystemColors.ControlText, 
				multiple ? Color.Bisque : SystemColors.Window,
				textBoxState, 
				TextBoxStyle.Sunken,
				this.scroll,
				(this.selectionLength != 0 || this.cursorVisible) ? this.cursorIndex : -1,
				this.selectionLength);
		}

		public void OnGotFocus(EventArgs e)
		{
			this.focused = true;
			if (this.cursorTimer == null)
			{
				this.cursorTimer = new Timer();
				this.cursorTimer.Interval = 500;
				this.cursorTimer.Tick += this.cursorTimer_Tick;
				this.cursorTimer.Enabled = true;
				this.cursorVisible = true;
			}
		}
		public void OnLostFocus(EventArgs e)
		{
			this.focused = false;
			if (this.cursorTimer != null)
			{
				this.cursorTimer.Tick -= this.cursorTimer_Tick;
				this.cursorTimer.Dispose();
				this.cursorTimer = null;
				this.cursorVisible = false;
			}
			this.scroll = 0;
		}
		public void OnKeyPress(KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar)) return;
			this.InsertText(e.KeyChar.ToString());
			e.Handled = true;
		}
		public void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (!this.readOnly && this.selectionLength == 0 && this.text != null && this.cursorIndex < this.text.Length)
				{
					this.text = this.text.Remove(this.cursorIndex, 1);
					this.UpdateScroll();
					this.EmitTextEdited();
				}
				else
					this.DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Back)
			{
				if (!this.readOnly && this.selectionLength == 0 && this.text != null && this.cursorIndex > 0)
				{
					this.text = this.text.Remove(this.cursorIndex - 1, 1);
					this.cursorIndex--;
					this.UpdateScroll();
					this.EmitTextEdited();
				}
				else
					this.DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Left)
			{
				if (e.Shift)
				{
					if (this.cursorIndex > 0)
					{
						this.cursorIndex--;
						this.selectionLength = Math.Min(this.selectionLength + 1, this.text != null ? this.text.Length : 0);
						this.UpdateScroll();
						this.ShowCursor();
					}
				}
				else
				{
					if (this.selectionLength < 0) this.cursorIndex += this.selectionLength;
					this.cursorIndex = Math.Max(this.cursorIndex - 1, 0);
					this.selectionLength = 0;
					this.UpdateScroll();
					this.ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Right)
			{
				if (e.Shift)
				{
					if (this.cursorIndex < (this.text != null ? this.text.Length : 0))
					{
						this.cursorIndex++;
						this.selectionLength = Math.Max(this.selectionLength - 1, -this.cursorIndex);
						this.UpdateScroll();
						this.ShowCursor();
					}
				}
				else
				{
					if (this.selectionLength > 0) this.cursorIndex += this.selectionLength;
					this.cursorIndex = Math.Min(this.cursorIndex + 1, this.text != null ? this.text.Length : 0);
					this.selectionLength = 0;
					this.UpdateScroll();
					this.ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.End)
			{
				if (e.Shift)
				{
					int oldSelEnd = this.cursorIndex + this.selectionLength;
					this.cursorIndex = (this.text != null ? this.text.Length : 0);
					this.selectionLength = oldSelEnd - this.cursorIndex;
					this.UpdateScroll();
					this.ShowCursor();
				}
				else
				{
					this.cursorIndex = (this.text != null ? this.text.Length : 0);
					this.selectionLength = 0;
					this.UpdateScroll();
					this.ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Home)
			{
				if (e.Shift)
				{
					int oldSelEnd = this.cursorIndex + this.selectionLength;
					this.cursorIndex = 0;
					this.selectionLength = oldSelEnd - this.cursorIndex;
					this.UpdateScroll();
					this.ShowCursor();
				}
				else
				{
					this.cursorIndex = 0;
					this.selectionLength = 0;
					this.UpdateScroll();
					this.ShowCursor();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.A && e.Control)
			{
				this.Select();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				Clipboard.SetText(this.SelectedText ?? "");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.X && e.Control)
			{
				Clipboard.SetText(this.SelectedText ?? "");
				this.DeleteSelection();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.V && e.Control)
			{
				string newText = Clipboard.GetText();
				this.InsertText(newText);
				e.Handled = true;
			}
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (!this.rect.Contains(e.Location)) return;
			Cursor.Current = Cursors.IBeam;

			// Pick char
			int pickedCharIndex;
			Point pickLoc = new Point(Math.Min(Math.Max(e.X, rect.X + 2), rect.Right - 2), rect.Y + rect.Height / 2);
			pickedCharIndex = ControlRenderer.PickCharTextField( 
				this.rect, 
				this.text,
				SystemFonts.DefaultFont,
				TextBoxStyle.Sunken,
				pickLoc,
				this.scroll);
			if (pickedCharIndex == -1) pickedCharIndex = this.text != null ? this.text.Length : 0;

			this.cursorIndex = pickedCharIndex;
			this.selectionLength = 0;
			this.UpdateScroll();
			this.EmitInvalidate();

			this.mouseSelect = true;
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			Cursor.Current = Cursors.IBeam;
			this.mouseSelect = false;
		}
		public void OnMouseMove(MouseEventArgs e)
		{
			bool lastHovered = this.hovered;
			this.hovered = this.rect.Contains(e.Location);
			if (lastHovered != this.hovered) this.EmitInvalidate();

			Cursor.Current = (this.hovered || this.mouseSelect) ? Cursors.IBeam : Cursors.Default;
			if (this.mouseSelect)
			{
				// Pick char
				int pickedCharIndex;
				Point pickLoc = new Point(Math.Min(Math.Max(e.X, rect.X + 2), rect.Right - 2), rect.Y + rect.Height / 2);
				pickedCharIndex = ControlRenderer.PickCharTextField(
					this.rect, 
					this.text,
					SystemFonts.DefaultFont,
					TextBoxStyle.Sunken,
					pickLoc,
					this.scroll);
				if (pickedCharIndex == -1) pickedCharIndex = this.text != null ? this.text.Length : 0;

				this.selectionLength = (this.cursorIndex + this.selectionLength) - pickedCharIndex;
				this.cursorIndex = pickedCharIndex;
				this.UpdateScroll();
				this.EmitInvalidate();
			}
		}
		public void OnMouseLeave(EventArgs e)
		{
			if (this.hovered) this.EmitInvalidate();
			this.hovered = false;

			Cursor.Current = (this.hovered || this.mouseSelect) ? Cursors.IBeam : Cursors.Default;
		}

		private void cursorTimer_Tick(object sender, EventArgs e)
		{
			if (this.selectionLength > 0) return;
			this.EmitInvalidate();
			this.cursorVisible = !this.cursorVisible;
		}
		protected void EmitInvalidate()
		{
			if (this.Invalidate != null)
				this.Invalidate(this, EventArgs.Empty);
		}
		protected void EmitTextEdited()
		{
			if (this.TextEdited != null)
				this.TextEdited(this, EventArgs.Empty);
		}
	}
}
