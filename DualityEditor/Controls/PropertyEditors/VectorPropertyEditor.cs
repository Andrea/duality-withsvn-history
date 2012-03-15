using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.EditorTemplates;

using Vector2 = OpenTK.Vector2;

using Duality;
using Duality.EditorHints;

namespace DualityEditor.Controls.PropertyEditors
{
	public abstract class VectorPropertyEditor : PropertyEditor
	{
		protected	NumericEditorTemplate[]	editor		= null;
		protected	bool[]					multiple	= null;
		protected	int						focusEditor	= 0;


		public VectorPropertyEditor(int size)
		{
			this.editor = new NumericEditorTemplate[size];
			this.multiple = new bool[size];

			for (int i = 0; i < this.editor.Length; i++)
			{
				this.editor[i] = new NumericEditorTemplate(this);
				this.editor[i].Invalidate += this.child_Invalidate;
			}
		}


		public void SetFocusEditorIndex(int index)
		{
			index = MathF.Clamp(index, 0, this.editor.Length - 1);
			if (this.focusEditor == index) return;

			if (this.Focused) this.editor[this.focusEditor].OnLostFocus(EventArgs.Empty);
			this.focusEditor = index;
			if (this.Focused)
			{
				this.editor[this.focusEditor].OnGotFocus(EventArgs.Empty);
				this.editor[this.focusEditor].Select();
			}
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].OnPaint(e, this.Enabled, this.multiple[i]);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.focusEditor = 0;
			this.editor[this.focusEditor].OnGotFocus(e);
			this.editor[this.focusEditor].Select();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.editor[this.focusEditor].OnLostFocus(e);
			this.focusEditor = 0;
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.editor[this.focusEditor].OnKeyPress(e);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.Return)
			{
				this.SetFocusEditorIndex((this.focusEditor + 1) % 2);
				e.Handled = true;
			}
			else
			{
				this.editor[this.focusEditor].OnKeyDown(e);
			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			this.editor[this.focusEditor].OnKeyUp(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].OnMouseMove(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].OnMouseLeave(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			for (int i = 0; i < this.editor.Length; i++)
			{
				if (this.editor[i].Rect.Contains(e.Location)) this.SetFocusEditorIndex(i);
				this.editor[i].OnMouseDown(e);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].OnMouseUp(e);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			int subEditSpace = 1;
			int subEditWidth = (this.ClientRectangle.Width - 2 - (subEditSpace * (this.editor.Length - 1))) / this.editor.Length;
			
			int curX = this.ClientRectangle.X + 1;
			for (int i = 0; i < this.editor.Length; i++)
			{
				this.editor[i].Rect = new Rectangle(
					curX,
					this.ClientRectangle.Y + 1,
					subEditWidth,
					this.ClientRectangle.Height - 1);
				curX += subEditWidth + subEditSpace;
			}
		}
		protected override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].ReadOnly = this.ReadOnly;
		}
		protected override void OnEditedMemberChanged()
		{
			base.OnEditedMemberChanged();
			
			for (int i = 0; i < this.editor.Length; i++)
				this.editor[i].ResetProperties();

			if (this.EditedMember != null)
			{
				var places = this.EditedMember.GetEditorHint<EditorHintDecimalPlacesAttribute>();
				var increment = this.EditedMember.GetEditorHint<EditorHintIncrementAttribute>();
				var range = this.EditedMember.GetEditorHint<EditorHintRangeAttribute>();
				
				for (int i = 0; i < this.editor.Length; i++)
				{
					if (places != null) this.editor[i].DecimalPlaces = places.Places;
					if (increment != null) this.editor[i].Increment = increment.Increment;
					if (range != null)
					{
						this.editor[i].Minimum = range.Min;
						this.editor[i].Maximum = range.Max;
					}
				}
			}
		}

		private void child_Invalidate(object sender, EventArgs e)
		{
			this.Invalidate();
		}
	}
}

