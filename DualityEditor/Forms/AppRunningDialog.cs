using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Windows7.DesktopIntegration;
using Windows7.DesktopIntegration.WindowsForms;

using Duality;
using DualityEditor;
using DualityEditor.EditorRes;
using Duality.Resources;

namespace DualityEditor.Forms
{
	public partial class AppRunningDialog : Form
	{
		Process		app		= null;

		public AppRunningDialog(Process app)
		{
			this.InitializeComponent();
			this.app = app;
			this.timerProcessState.Enabled = true;
		}

		private void timerProcessState_Tick(object sender, EventArgs e)
		{
			if (this.app.HasExited) this.Close();
		}
	}
}
