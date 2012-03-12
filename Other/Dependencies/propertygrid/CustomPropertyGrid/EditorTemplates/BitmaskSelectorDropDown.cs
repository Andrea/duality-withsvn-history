using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdamsLair.PropertyGrid.EditorTemplates
{
	public partial class BitmaskSelectorDropDown : CheckedListBox
	{
		private bool isUpdatingCheckStates = false;
		private bool openedWithCtrl	= false;

		public event EventHandler AcceptSelection = null;
		public event EventHandler RequestClose = null;


		public new IEnumerable<BitmaskItem> Items
		{
			get { return base.Items.Cast<BitmaskItem>(); }
			set
			{ 
				base.Items.Clear();
				base.Items.AddRange(value.ToArray());
			}
		}
		public ulong BitmaskValue
		{
			get
			{
				ulong v = 0;
				foreach (BitmaskItem item in this.CheckedItems.OfType<BitmaskItem>())
					v |= item.Value;
				return v;
			}
			set
			{
				this.isUpdatingCheckStates = true;
				for (int i = 0; i < base.Items.Count; i++)
				{
					BitmaskItem item = base.Items[i] as BitmaskItem;
					if (item.Value == 0)
						this.SetItemChecked(i, value == 0);
					else
						this.SetItemChecked(i, (item.Value & value) == item.Value);
				}
				this.isUpdatingCheckStates = false;
			}
		}


		public BitmaskSelectorDropDown()
		{
			this.IntegralHeight = false;
			this.CheckOnClick = true;
			this.Font = this.Font; // Prevents parent PopupControl from interfering on resize.
		}
		public BitmaskSelectorDropDown(IEnumerable<BitmaskItem> items) : this()
		{
			this.Items = items;
		}

		protected void Accept()
		{
			if (this.AcceptSelection != null)
				this.AcceptSelection(this, EventArgs.Empty);
		}
		protected void Close()
		{
			if (this.RequestClose != null) this.RequestClose(this, EventArgs.Empty);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.openedWithCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return)
			{
				int index = this.SelectedIndex;
				if (index != -1) this.SetItemChecked(index, !this.GetItemChecked(index));
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				if (this.SelectedItem != null)
					Clipboard.SetText((this.SelectedItem as BitmaskItem).Caption);
			}
			else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Left || e.KeyCode == Keys.Escape)
			{
				this.Accept();
				this.Close();
			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyCode == Keys.ControlKey && this.openedWithCtrl)
			{
				this.Accept();
				this.Close();
			}
		}
		protected override void OnItemCheck(ItemCheckEventArgs e)
		{
			base.OnItemCheck(e);
            if (this.isUpdatingCheckStates) return;

			BitmaskItem item = base.Items[e.Index] as BitmaskItem;
			
			if (item.Value == 0)
				this.BitmaskValue = 0;
			else if (e.NewValue == CheckState.Checked)
				this.BitmaskValue |= item.Value;
			else if (e.NewValue == CheckState.Unchecked)
				this.BitmaskValue &= ~item.Value;

			this.Accept();
		}
	}
}
