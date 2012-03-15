using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DualityEditor.Forms
{
	public partial class SplashScreen : Form
	{
		private	bool	recover	= false;

		public SplashScreen(bool recover)
		{
			this.InitializeComponent();
			this.recover = recover;
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.mainFormLoader.RunWorkerAsync(this);
		}
		protected MainForm CreateMainForm()
		{
			MainForm main = new MainForm(this.recover);
			return main;
		}

		private void mainFormLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			SplashScreen screen = e.Argument as SplashScreen;

			System.Threading.Thread.Sleep(50); // Assures the screen had time to display correctly
			object result = screen.Invoke(new Func<MainForm>(screen.CreateMainForm));

			e.Result = result;
		}
		private void mainFormLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.Hide();
			try
			{
				(e.Result as MainForm).ShowDialog();
			}
			finally
			{
				this.Close();
			}
		}
	}
}
