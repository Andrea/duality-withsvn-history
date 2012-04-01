using System;
using System.Windows.Forms;

namespace DualityEditor.Forms
{
	partial class AboutBox : Form
	{
		public AboutBox()
		{
			this.InitializeComponent();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(this.linkLabelWebsite.Text);
		}
		private void linkLabelDevWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(this.linkLabelDevWebsite.Text);
		}
	}
}
