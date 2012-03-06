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
	public partial class ComboBoxDropDown : Form
	{
		public event EventHandler AcceptSelection = null;

		public int ItemHeight
		{
			get { return this.listBox.ItemHeight; }
		}
		public int PreferredHeight
		{
			get { return this.listBox.PreferredHeight; }
		}
		public ListBox ListControl
		{
			get { return this.listBox; }
		}
		public IEnumerable<object> Items
		{
			get { return this.listBox.Items.Cast<object>(); }
			set
			{ 
				this.listBox.Items.Clear();
				this.listBox.Items.AddRange(value.ToArray());
			}
		}
		public IEnumerable<object> SelectedItems
		{
			get { return this.listBox.SelectedItems.Cast<object>(); }
		}
		public object SelectedItem
		{
			get { return this.listBox.SelectedItem; }
			set { this.listBox.SelectedItem = value; }
		}

		public ComboBoxDropDown()
		{
			this.InitializeComponent();
		}
		public ComboBoxDropDown(IEnumerable<object> items)
		{
			this.listBox.Items.AddRange(items.ToArray());
		}

		protected void Accept()
		{
			if (this.AcceptSelection != null)
				this.AcceptSelection(this, EventArgs.Empty);
			this.Close();
		}

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			this.Close();
		}
		private void listBox_Click(object sender, EventArgs e)
		{
			if (this.listBox.SelectionMode == SelectionMode.One)
				this.Accept();
		}
		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (this.listBox.SelectionMode == SelectionMode.One)
					this.Accept();
			}
			else if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
	}
}
