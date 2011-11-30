using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Duality;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace DualityLauncher
{
	public class DualityLauncher : GameWindow
	{
		private Stopwatch	frameLimiterWatch	= new Stopwatch();
		private	double		frameLimiterLast	= 0.0d;

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
			
			// Assure we'll at least wait 16 ms until updating again.
			if (this.frameLimiterWatch.IsRunning)
			{
				while (this.frameLimiterWatch.Elapsed.TotalSeconds - this.frameLimiterLast < 0.016666d) 
				{
					// Go to sleep if we'd have to wait too long
					if (this.frameLimiterWatch.Elapsed.TotalSeconds - this.frameLimiterLast < 0.012d)
						System.Threading.Thread.Sleep(1);
				}
			}
			this.frameLimiterWatch.Restart();
			this.frameLimiterLast = this.frameLimiterWatch.Elapsed.TotalSeconds;
			DualityApp.Update();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Terminated) return;

			DualityApp.Render();
			this.SwapBuffers();
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (!System.Diagnostics.Debugger.IsAttached) System.Windows.Forms.Cursor.Hide();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!System.Diagnostics.Debugger.IsAttached) System.Windows.Forms.Cursor.Show();
		}

		[STAThread]
		public static void Main(string[] args)
		{
			DualityApp.Init(DualityApp.ExecutionContext.Launcher, args);
			using (DualityLauncher launcherWindow = new DualityLauncher(
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

				// Input setup
				DualityApp.Mouse = new WindowMouseInput(launcherWindow.Mouse);
				DualityApp.Keyboard = new WindowKeyboardInput(launcherWindow.Keyboard);

				// Load the starting Scene
				Scene.Current = DualityApp.AppData.StartScene.Res;

				// Run the DualityApp
				launcherWindow.VSync = VSyncMode.On;
				launcherWindow.Run();
			}
			DualityApp.Terminate();
		}
	}
}
