using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;
using ButtonState = CustomPropertyGrid.Renderer.ButtonState;

namespace CustomPropertyGrid.EditorTemplates
{
	public class ComboBoxEditorTemplate : EditorTemplate
	{
		private const string ClipboardDataFormat = "ComboBoxEditorTemplateData";

		private	object				selectedObject	= null;
		private	bool				pressed			= false;
		private	int					dropdownHeight	= 100;
		private	ComboBoxDropDown	dropdown		= null;
		private	List<object>		dropdownItems	= new List<object>();

		public object SelectedObject
		{
			get { return this.selectedObject; }
			set
			{
				this.selectedObject = value;
				if (this.dropdown != null) this.dropdown.SelectedItem = this.selectedObject;
			}
		}
		public bool IsDropDownOpened
		{
			get { return this.dropdown != null; }
		}
		public int DropDownHeight
		{
			get { return this.dropdownHeight; }
			set { this.dropdownHeight = value; }
		}
		public IEnumerable<object> DropDownItems
		{
			get { return this.dropdownItems; }
			set
			{
				this.dropdownItems = value.ToList();
				if (this.dropdown != null) this.dropdown.Items = this.dropdownItems;
			}
		}
		
		public ComboBoxEditorTemplate(PropertyEditor parent) : base(parent) {}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			ButtonState comboState = ButtonState.Normal;
			if (!enabled || this.ReadOnly)
				comboState = ButtonState.Disabled;
			else if (this.pressed || this.IsDropDownOpened || (this.focused && (Control.ModifierKeys & Keys.Control) == Keys.Control))
				comboState = ButtonState.Pressed;
			else if (this.hovered || this.focused)
				comboState = ButtonState.Hot;

			ControlRenderer.DrawComboButton(e.Graphics, this.rect, comboState, this.selectedObject.ToString());
		}
		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.ReadOnly) this.hovered = false;
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (!this.rect.Contains(e.Location)) return;
			if (this.hovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.pressed = true;
				this.EmitInvalidate();
			}
		}
		public void OnMouseUp(MouseEventArgs e)
		{
			if (this.pressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.hovered) this.OpenDropDown();
				this.pressed = false;
				this.EmitInvalidate();
			}
		}
		public void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
				this.EmitInvalidate();
		}
		public void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
				this.EmitInvalidate();

			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right || (e.KeyCode == Keys.Down && e.Control))
			{
				this.OpenDropDown();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down && e.Control)
			{
				int index = this.dropdownItems.IndexOf(this.selectedObject);
				this.selectedObject = this.dropdownItems[(index + 1) % this.dropdownItems.Count];
				this.EmitEdited();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Up && e.Control)
			{
				int index = this.dropdownItems.IndexOf(this.selectedObject);
				this.selectedObject = this.dropdownItems[(index + this.dropdownItems.Count - 1) % this.dropdownItems.Count];
				this.EmitEdited();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.C)
			{
				DataObject data = new DataObject();
				data.SetText(this.selectedObject.ToString());
				data.SetData(ClipboardDataFormat, this.selectedObject);
				Clipboard.SetDataObject(data);
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.V)
			{
				bool success = false;
				if (Clipboard.ContainsData(ClipboardDataFormat) || Clipboard.ContainsText())
				{
					object pasteObjProxy = null;
					if (Clipboard.ContainsData(ClipboardDataFormat))
					{
						object pasteObj = Clipboard.GetData(ClipboardDataFormat);
						pasteObjProxy = this.dropdownItems.FirstOrDefault(obj => object.Equals(obj, pasteObj));
					}
					else if (Clipboard.ContainsText())
					{
						string pasteObj = Clipboard.GetText();
						pasteObjProxy = this.dropdownItems.FirstOrDefault(obj => obj != null && obj.ToString() == pasteObj);
					}
					if (pasteObjProxy != null)
					{
						if (this.selectedObject != pasteObjProxy) this.EmitEdited();
						this.selectedObject = pasteObjProxy;
						success = true;
					}
				}
				if (!success) System.Media.SystemSounds.Beep.Play();
				e.Handled = true;
			}
		}

		public void OpenDropDown()
		{
			if (this.dropdown != null) return;
			if (this.ReadOnly) return;
			PropertyGrid parentGrid = this.Parent.ParentGrid;

			this.dropdown = new ComboBoxDropDown();
			this.dropdown.Items = this.dropdownItems;
			this.dropdown.SelectedItem = this.selectedObject;

			Size dropDownSize = new Size(
				this.rect.Width, 
				Math.Min(this.dropdownHeight, this.dropdown.PreferredHeight));

			Point dropDownLoc = parentGrid.GetEditorLocation(this.Parent, true);
			dropDownLoc = parentGrid.PointToScreen(dropDownLoc);
			dropDownLoc.Y += this.rect.Height + 1;
			dropDownLoc.X += this.Parent.Width - this.rect.Width;
			
			this.dropdown.Location = dropDownLoc;
			this.dropdown.Size = dropDownSize;
			this.dropdown.FormClosed += this.dropdown_FormClosed;
			this.dropdown.AcceptSelection += this.dropdown_AcceptSelection;
			this.dropdown.Show(this.Parent.ParentGrid);

			this.EmitInvalidate();
		}
		public void CloseDropDown()
		{
			if (this.dropdown == null) return;
			if (!this.dropdown.Disposing && !this.dropdown.IsDisposed)
				this.dropdown.Dispose();
			this.dropdown = null;

			this.EmitInvalidate();
		}
		private void dropdown_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.CloseDropDown();
		}
		private void dropdown_AcceptSelection(object sender, EventArgs e)
		{
			if (this.selectedObject != this.dropdown.SelectedItem)
			{
				this.selectedObject = this.dropdown.SelectedItem;
				this.EmitEdited();
			}
		}
	}
}
