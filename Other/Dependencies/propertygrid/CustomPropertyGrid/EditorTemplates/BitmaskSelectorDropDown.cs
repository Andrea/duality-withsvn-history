using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomPropertyGrid.EditorTemplates
{
	public partial class BitmaskSelectorDropDown : Form
	{
		private bool isUpdatingCheckStates = false;
		private bool openedWithCtrl	= false;
		public event EventHandler AcceptSelection = null;

		public int ItemHeight
		{
			get { return this.checkedListBox.ItemHeight; }
		}
		public int PreferredHeight
		{
			get { return this.checkedListBox.PreferredHeight; }
		}
		public CheckedListBox ListControl
		{
			get { return this.checkedListBox; }
		}
		public IEnumerable<BitmaskItem> Items
		{
			get { return this.checkedListBox.Items.Cast<BitmaskItem>(); }
			set
			{ 
				this.checkedListBox.Items.Clear();
				this.checkedListBox.Items.AddRange(value.ToArray());
			}
		}
		public ulong BitmaskValue
		{
			get
			{
				ulong v = 0;
				foreach (BitmaskItem item in this.checkedListBox.CheckedItems.OfType<BitmaskItem>())
					v |= item.Value;
				return v;
			}
			set
			{
				this.isUpdatingCheckStates = true;
				for (int i = 0; i < this.checkedListBox.Items.Count; i++)
				{
					BitmaskItem item = this.checkedListBox.Items[i] as BitmaskItem;
					if (item.Value == 0)
						this.checkedListBox.SetItemChecked(i, value == 0);
					else
						this.checkedListBox.SetItemChecked(i, (item.Value & value) == item.Value);
				}
				this.isUpdatingCheckStates = false;
			}
		}

		public BitmaskSelectorDropDown()
		{
			this.InitializeComponent();
		}
		public BitmaskSelectorDropDown(IEnumerable<BitmaskItem> items) : this()
		{
			this.checkedListBox.Items.AddRange(items.ToArray());
		}

		protected void Accept()
		{
			if (this.AcceptSelection != null)
				this.AcceptSelection(this, EventArgs.Empty);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.openedWithCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
		}
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			this.Accept();
			this.BeginInvoke((MethodInvoker)(() => this.Close()));
		}
		private void checkedListBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				int index = this.checkedListBox.SelectedIndex;
				if (index != -1) this.checkedListBox.SetItemChecked(index, !this.checkedListBox.GetItemChecked(index));
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				if (this.checkedListBox.SelectedItem != null)
					Clipboard.SetText((this.checkedListBox.SelectedItem as BitmaskItem).Caption);
			}
			else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Left || e.KeyCode == Keys.Escape)
			{
				this.Accept();
				this.Close();
			}
		}
		private void checkedListBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey && this.openedWithCtrl)
			{
				this.Accept();
				this.Close();
			}
		}
		private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
            if (this.isUpdatingCheckStates) return;

			BitmaskItem item = this.checkedListBox.Items[e.Index] as BitmaskItem;
			
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
