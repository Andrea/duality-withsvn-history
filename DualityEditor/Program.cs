using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

using DualityEditor.Forms;

namespace DualityEditor
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			bool recover = false;
			foreach (string a in args)
			{
				if (a == "debug")
					System.Diagnostics.Debugger.Launch();
				else if (a == "recover")
					recover = true;
			}

			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // de-DE
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

			Application.CurrentCulture = Thread.CurrentThread.CurrentCulture;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(recover));
		}
	}
}
