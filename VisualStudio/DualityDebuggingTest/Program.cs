using System;
using OpenTK;
using OpenTK.Graphics;
using Duality;
using Duality.Resources;
using DualityDebugging;

namespace DualityDebuggingTest
{
	public class DualityDebuggingTester : GameWindow
	{
		public DualityDebuggingTester(int w, int h, GraphicsMode mode, string title, GameWindowFlags flags) : base(w, h, mode, title, flags) {}

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			DualityApp.Init(DualityApp.ExecutionEnvironment.Launcher, DualityApp.ExecutionContext.Game);
			using (DualityDebuggingTester launcherWindow = new DualityDebuggingTester(
				DualityApp.UserData.GfxWidth, 
				DualityApp.UserData.GfxHeight, 
				DualityApp.DefaultMode, 
				DualityApp.AppData.AppName,
				(DualityApp.UserData.GfxFullScreen && !System.Diagnostics.Debugger.IsAttached) ? GameWindowFlags.Fullscreen : GameWindowFlags.Default))
			{
				// Retrieve icon from executable file and set it as window icon
				string executablePath = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
				launcherWindow.Icon = System.Drawing.Icon.ExtractAssociatedIcon(executablePath);

				// Initialize default content
				launcherWindow.MakeCurrent();
				DualityApp.TargetResolution = new Vector2(launcherWindow.Width, launcherWindow.Height);
				DualityApp.TargetMode = launcherWindow.Context.GraphicsMode;
				ContentProvider.InitDefaultContent();

				// Run the DualityApp
				launcherWindow.VSync = VSyncMode.On;
				//launcherWindow.Run();
				BitmapDebuggerVisualizer.TestShow(Pixmap.DualityLogo256.Res);
				BitmapDebuggerVisualizer.TestShow(Texture.DualityLogo256.Res);
			}
			DualityApp.Terminate();
		}
	}
}
