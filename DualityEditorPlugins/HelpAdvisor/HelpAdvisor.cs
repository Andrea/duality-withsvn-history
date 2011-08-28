using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DualityEditor;

using WeifenLuo.WinFormsUI.Docking;

namespace HelpAdvisor
{
	public partial class HelpAdvisor : DockContent
	{
		private HelpInfo advisorHelp = new HelpInfo("Advisor Window", "The Advisor will support you with context-based information about Duality.");

		public HelpAdvisor()
		{
			this.InitializeComponent();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelpChanged += this.Help_ActiveHelpChanged;
			this.UpdateHelp();
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelpChanged -= this.Help_ActiveHelpChanged;
		}

		public void UpdateHelp()
		{
			HelpInfo help = HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelp ?? advisorHelp;
			this.labelTopic.Text = help.Topic;
			this.labelDescription.Text = help.Description;
		}

		private void Help_ActiveHelpChanged(object sender, HelpStackChangedEventArgs e)
		{
			this.UpdateHelp();
		}
	}
}
