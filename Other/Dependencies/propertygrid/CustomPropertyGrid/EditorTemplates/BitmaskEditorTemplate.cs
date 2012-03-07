using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;
using ButtonState = CustomPropertyGrid.Renderer.ButtonState;

namespace CustomPropertyGrid.EditorTemplates
{
	public class BitmaskEditorTemplate : EditorTemplate
	{
		private const string ClipboardDataFormat = "BitmaskEditorTemplateData";

		private	ulong					bitmask			= 0;
		private	bool					pressed			= false;
		private	int						dropdownHeight	= 100;
		private	BitmaskSelectorDropDown	dropdown		= null;
		private	List<BitmaskItem>		dropdownItems	= new List<BitmaskItem>();

		public ulong BitmaskValue
		{
			get { return this.bitmask; }
			set
			{
				this.bitmask = value;
				if (this.dropdown != null) this.dropdown.BitmaskValue = this.bitmask;;
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
		public IEnumerable<BitmaskItem> DropDownItems
		{
			get { return this.dropdownItems; }
			set
			{
				this.dropdownItems = value.OrderBy(i => i.Value).ToList();
				if (this.dropdown != null) this.dropdown.Items = this.dropdownItems;
			}
		}
		
		public BitmaskEditorTemplate(PropertyEditor parent) : base(parent) {}

		public void OnPaint(PaintEventArgs e, bool enabled, bool multiple)
		{
			ButtonState comboState = ButtonState.Normal;
			if (!enabled || this.ReadOnly)
				comboState = ButtonState.Disabled;
			else if (this.pressed || this.IsDropDownOpened || (this.focused && (Control.ModifierKeys & Keys.Control) == Keys.Control))
				comboState = ButtonState.Pressed;
			else if (this.hovered || this.focused)
				comboState = ButtonState.Hot;

			ControlRenderer.DrawComboButton(e.Graphics, this.rect, comboState, DefaultValueStringGenerator(this.bitmask));
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
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right || e.KeyCode == Keys.ControlKey)
			{
				this.OpenDropDown();
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.C)
			{
				DataObject data = new DataObject();
				data.SetText(DefaultValueStringGenerator(this.bitmask));
				data.SetData(ClipboardDataFormat, this.bitmask);
				Clipboard.SetDataObject(data);
				e.Handled = true;
			}
			else if (e.Control && e.KeyCode == Keys.V)
			{
				bool success = false;
				ulong pasteValue = 0; 
				if (Clipboard.ContainsData(ClipboardDataFormat))
				{
					pasteValue = (ulong)Clipboard.GetData(ClipboardDataFormat);
					success = true;
				}
				else if (Clipboard.ContainsText())
				{
					string[] pasteObj = Clipboard.GetText().Split(new [] { ", " }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string p in pasteObj)
					{
						if (p == null) continue;
						BitmaskItem item = this.dropdownItems.FirstOrDefault(obj => obj != null && p == obj.ToString());
						if (item != null)
						{
							pasteValue |= item.Value;
							success = true;
						}
					}
				}
				if (success)
				{
					this.bitmask = pasteValue;
					this.EmitEdited();
				}
				else
					System.Media.SystemSounds.Beep.Play();
				e.Handled = true;
			}
		}

		public void OpenDropDown()
		{
			if (this.dropdown != null) return;
			if (this.ReadOnly) return;
			PropertyGrid parentGrid = this.Parent.ParentGrid;

			this.dropdown = new BitmaskSelectorDropDown();
			this.dropdown.Items = this.dropdownItems;
			this.dropdown.BitmaskValue = this.bitmask;

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
			if (this.bitmask != this.dropdown.BitmaskValue)
			{
				this.bitmask = this.dropdown.BitmaskValue;
				this.EmitEdited();
			}
		}

		protected string DefaultValueStringGenerator(ulong bitmask)
		{
			ulong num = bitmask;
			int index = this.dropdownItems.Count - 1;
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (index >= 0)
			{
				if ((index == 0) && (this.dropdownItems[index].Value == 0L)) break;
				if ((num & this.dropdownItems[index].Value) == this.dropdownItems[index].Value)
				{
					num -= this.dropdownItems[index].Value;
					if (!flag) builder.Insert(0, ", ");
					builder.Insert(0, this.dropdownItems[index].Caption);
					flag = false;
				}
				index--;
			}
			if (num != 0L) return bitmask.ToString();
			if (num3 != 0L) return builder.ToString();
			if (this.dropdownItems.Count > 0 && this.dropdownItems[0].Value == 0L) return this.dropdownItems[0].Caption;
			return "0";
		}
	}
}
