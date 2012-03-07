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
	public partial class MultiComboBoxDropDown : Form
	{
		private bool isOpen = false;
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
		public IEnumerable<object> Items
		{
			get { return this.checkedListBox.Items.Cast<object>(); }
			set
			{ 
				this.checkedListBox.Items.Clear();
				this.checkedListBox.Items.AddRange(value.ToArray());
			}
		}
		public IEnumerable<object> CheckedItems
		{
			get { return this.checkedListBox.CheckedItems.Cast<object>(); }
			set
			{
				for (int i = 0; i < this.checkedListBox.Items.Count; i++)
					this.checkedListBox.SetItemChecked(i, value.Contains(this.checkedListBox.Items[i]));
			}
		}

		public MultiComboBoxDropDown()
		{
			this.InitializeComponent();
		}
		public MultiComboBoxDropDown(IEnumerable<object> items) : this()
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
			this.isOpen = true;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.isOpen = false;
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
					Clipboard.SetText(this.checkedListBox.SelectedItem.ToString());
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
			if (this.IsHandleCreated && this.isOpen)
			{
				// Delay exexution until item actually checked
				this.BeginInvoke((MethodInvoker)(() => this.Accept()));
			}
		}
	}
}
