using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

using Duality;
using Duality.Resources;

using OpenTK;
using OpenTK.Graphics;

namespace DualityLauncher
{
	public class DualityLauncher : GameWindow
	{
		private Stopwatch	frameLimiterWatch	= new Stopwatch();

		public DualityLauncher(int w, int h, GraphicsMode mode, string title, GameWindowFlags flags)
			: base(w, h, mode, title, flags)
		{
		}

		protected override void OnResize(EventArgs e)
		{
			DualityApp.TargetResolution = new Vector2(Width, Height);
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Terminated)
			{
				this.Close();
				return;
			}
			
			if (!System.Diagnostics.Debugger.IsAttached) // Don't limit frame rate when debugging.
			{
				//// Assure we'll at least wait 16 ms until updating again.
				//if (this.frameLimiterWatch.IsRunning)
				//{
				//    while (this.frameLimiterWatch.Elapsed.TotalSeconds < 0.016d) 
				//    {
				//        // Go to sleep if we'd have to wait too long
				//        if (this.frameLimiterWatch.Elapsed.TotalSeconds < 0.01d)
				//            System.Threading.Thread.Sleep(1);
				//    }
				//}

				// Give the processor a rest if we have the time, don't use 100% CPU
				if (this.frameLimiterWatch.IsRunning)
				{
					while (this.frameLimiterWatch.Elapsed.TotalSeconds < 0.01d) 
					{
						// Sleep a little
						System.Threading.Thread.Sleep(1);
					}
				}
				this.frameLimiterWatch.Restart();
			}
			DualityApp.Update();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Terminated) return;

			DualityApp.Render();
			Performance.TimeRender.BeginMeasure();
			Performance.TimeSwapBuffers.BeginMeasure();
			this.SwapBuffers();
			Performance.TimeSwapBuffers.EndMeasure();
			Performance.TimeRender.EndMeasure();
		}

		private void SetMouseDeviceX(int x)
		{
			if (!this.Focused) return;
			Point targetPos = this.PointToScreen(new Point(x, this.PointToClient(Cursor.Position).Y));
			Cursor.Position = targetPos;
			return;
		}
		private void SetMouseDeviceY(int y)
		{
			if (!this.Focused) return;
			Point targetPos = this.PointToScreen(new Point(this.PointToClient(Cursor.Position).X, y));
			Cursor.Position = targetPos;
			return;
		}

		[STAThread]
		public static void Main(string[] args)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

			bool debugging = System.Diagnostics.Debugger.IsAttached || args.Contains(DualityApp.CmdArgDebug);
			bool editor = args.Contains(DualityApp.CmdArgEditor);
			if (debugging || editor) ShowConsole();

			DualityApp.Init(DualityApp.ExecutionEnvironment.Launcher, DualityApp.ExecutionContext.Game, args);

			using (DualityLauncher launcherWindow = new DualityLauncher(
				DualityApp.UserData.GfxWidth, 
				DualityApp.UserData.GfxHeight, 
				DualityApp.DefaultMode, 
				DualityApp.AppData.AppName,
				(DualityApp.UserData.GfxMode == ScreenMode.Fullscreen && !debugging) ? GameWindowFlags.Fullscreen : GameWindowFlags.Default))
			{
				// Retrieve icon from executable file and set it as window icon
				string executablePath = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
				launcherWindow.Icon = System.Drawing.Icon.ExtractAssociatedIcon(executablePath);

				// Go into native fullscreen mode
				if (DualityApp.UserData.GfxMode == ScreenMode.Native && !debugging)
					launcherWindow.WindowState = WindowState.Fullscreen;

				if (DualityApp.UserData.GfxMode == ScreenMode.FixedWindow)
					launcherWindow.WindowBorder = WindowBorder.Fixed;
				else if (DualityApp.UserData.GfxMode == ScreenMode.Window)
					launcherWindow.WindowBorder = WindowBorder.Resizable;

				// Initialize default content
				launcherWindow.MakeCurrent();
				DualityApp.TargetResolution = new Vector2(launcherWindow.Width, launcherWindow.Height);
				DualityApp.TargetMode = launcherWindow.Context.GraphicsMode;
				ContentProvider.InitDefaultContent();

				// Input setup
				DualityApp.Mouse = new WindowMouseInput(launcherWindow.Mouse, launcherWindow.SetMouseDeviceX, launcherWindow.SetMouseDeviceY);
				DualityApp.Keyboard = new WindowKeyboardInput(launcherWindow.Keyboard);

				// Load the starting Scene
				Scene.Current = DualityApp.AppData.StartScene.Res;

				// Run the DualityApp
				launcherWindow.CursorVisible = System.Diagnostics.Debugger.IsAttached || DualityApp.UserData.SystemCursorVisible;
				launcherWindow.VSync = debugging ? VSyncMode.Off : VSyncMode.On; // Don't limit frame rate when debugging.
				launcherWindow.Run();
			}
			DualityApp.Terminate();
		}

		private static bool hasConsole = false;
		public static void ShowConsole()
		{
			if (hasConsole) return;
			AllocConsole();
			hasConsole = true;
		}

		[DllImport("kernel32.dll")]
		private static extern Int32 AllocConsole();
	}
}
