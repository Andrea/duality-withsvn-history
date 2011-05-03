//#define GEN_PREFABS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.ColorFormat;
using Duality.Resources;

namespace TestApp
{
	public class SimpleWindow : GameWindow
	{
		private	static	Random	rnd		= new Random();

		public SimpleWindow()
			: base(800, 600, DualityApp.DefaultMode, "Test")
		{
			this.Keyboard.KeyDown += this.Keyboard_KeyDown;

			MakeCurrent();
			DualityApp.TargetMode = this.Context.GraphicsMode;
			ContentProvider.InitDefaultContent();

			DualityApp.Mouse = this.Mouse;
			DualityApp.Keyboard = this.Keyboard;
			DualityApp.Joysticks = this.Joysticks;

			//Scene.Current = Scene.LoadFrom("scene.tmp");
			//return;

#if GEN_PREFABS
			Renderer r;
			GameObject obj = new GameObject();
			obj.Name = "obj";
			obj.AddComponent<Transform>();
			r = obj.AddComponent(new RectRenderer(Rect.AlignCenter(0, 0, 75, 75), ColorRGBA.White));
			r.RenderFlags |= RendererFlags.Parallax;
			obj.Transform.RelativeScale = new Vector3(1.0f, 1.0f, 1.0f);
			obj.Transform.RelativeAngleVel = 0.01f;
			obj.Transform.RelativePos = new Vector3(0, 0, 0);
			Scene.Current.RegisterObj(obj);

			GameObject obj2 = new GameObject();
			obj2.Name = "obj2";
			obj2.AddComponent<Transform>();
			r = obj2.AddComponent(new RectRenderer(Rect.AlignCenter(0, 0, 75, 75), ColorRGBA.LightGrey & ColorRGBA.Red));
			r.RenderFlags |= RendererFlags.Parallax;
			obj2.Parent = obj;
			obj2.Transform.RelativeAngleVel = 0.01f;
			obj2.Transform.RelativePos = new Vector3(100, 0, 100);
			Scene.Current.RegisterObj(obj2);

			GameObject obj3 = new GameObject();
			obj3.Name = "obj3";
			obj3.AddComponent<Transform>();
			r = obj3.AddComponent(new RectRenderer(Rect.AlignCenter(0, 0, 75, 75), ColorRGBA.Grey & ColorRGBA.Red));
			r.RenderFlags |= RendererFlags.Parallax;
			obj3.Parent = obj2;
			obj3.Transform.RelativeAngleVel = 0.01f;
			obj3.Transform.RelativePos = new Vector3(50, 0, 100);
			Scene.Current.RegisterObj(obj3);

			Prefab prefab1 = new Prefab(obj);
			prefab1.Save("obj.prefab.res");
#else
			ContentRef<Prefab> prefab1 = ContentProvider.RequestContent<Prefab>("obj.prefab.res");
			Scene.Current.Graph.RegisterObjDeep(prefab1.Res.Instantiate());
#endif
			
#if GEN_PREFABS
			GameObject cam = new GameObject();
			cam.Name = "cam";
			cam.AddComponent<Transform>().Pos = new Vector3(0, 0, -500);
			Camera camCmp = cam.AddComponent<Camera>();
			camCmp.NearZ = 0;
			camCmp.FarZ = 10000;
			camCmp.Viewport = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
			camCmp.ClearColor = ColorRGBA.Grey + ColorRGBA.Blue;
			cam.AddComponent<TestPlugin.DebugCamControl>();
			Scene.Current.RegisterObj(cam);

			Prefab prefab2 = new Prefab(cam);
			prefab2.Save("cam.prefab.res");
#else
			ContentRef<Prefab> prefab2 = ContentProvider.RequestContent<Prefab>("cam.prefab.res");
			Scene.Current.Graph.RegisterObjDeep(prefab2.Res.Instantiate());
#endif

#if GEN_PREFABS
			GameObject cam2 = new GameObject();
			cam2.Name = "cam2";
			cam2.AddComponent<Transform>().Pos = new Vector3(0, 0, -500);
			Camera camCmp2 = cam2.AddComponent<Camera>();
			camCmp2.NearZ = 0;
			camCmp2.FarZ = 10000;
			camCmp2.Viewport = new Rect(0.1f, 0.1f, 0.25f, 0.25f);
			camCmp2.ClearColor = ColorRGBA.Grey + ColorRGBA.Green;
			cam2.AddComponent<TestPlugin.DebugCamControl>();
			Scene.Current.RegisterObj(cam2);

			Prefab prefab3 = new Prefab(cam2);
			prefab3.Save("cam2.prefab.res");
#else
			ContentRef<Prefab> prefab3 = ContentProvider.RequestContent<Prefab>("cam2.prefab.res");
			Scene.Current.Graph.RegisterObjDeep(prefab3.Res.Instantiate());
#endif

			for (int i = 0; i < 10000; i++)
			{
			    CreateRandomRect();
			}

			Scene.Current.Save("scene.tmp");
		}

		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		/// <param name="sender">The KeyboardDevice which generated this event.</param>
		/// <param name="e">The key that was pressed.</param>
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				this.Exit();

			if (e.Key == Key.F11)
				if (this.WindowState == WindowState.Fullscreen)
					this.WindowState = WindowState.Normal;
				else
					this.WindowState = WindowState.Fullscreen;
		}

		/// <summary>
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(Color.MidnightBlue);
		}

		/// <summary>
		/// Respond to resize events here.
		/// </summary>
		/// <param name="e">Contains information on the new GameWindow size.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnResize(EventArgs e)
		{
			DualityApp.TargetResolution = new Vector2(Width, Height);
		}

		/// <summary>
		/// Add your game logic here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			DualityApp.Update();
		}

		/// <summary>
		/// Add your game rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			DualityApp.Draw();
			this.SwapBuffers();
		}

		/// <summary>
		/// Entry point of this example.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			DualityApp.Init();
			using (SimpleWindow example = new SimpleWindow())
			{
				example.Run(60.0d, 60.0d);
			}
			DualityApp.Terminate();
		}

		public static GameObject CreateRandomRect()
		{
			GameObject obj = new GameObject();
			obj.Name = "randomRect" + rnd.Next().ToString();
			obj.AddComponent<Transform>();
			SpriteRenderer r = obj.AddComponent(new SpriteRenderer(Rect.AlignCenter(0, 0, 10, 10), Material.DualityLogo256));
			r.RenderFlags |= RendererFlags.Parallax;
			obj.Transform.RelativePos = new Vector3(rnd.NextFloat(-1000.0f, 1000.0f), rnd.NextFloat(-1000.0f, 1000.0f), rnd.NextFloat(-500.0f, 1000.0f));
			obj.Transform.RelativeAngleVel = rnd.NextFloat(-0.1f, 0.1f);
			obj.Transform.RelativeVel = new Vector3(rnd.NextFloat(-1.0f, 1.0f), rnd.NextFloat(-1.0f, 1.0f), rnd.NextFloat(-0.5f, 0.5f));
			Scene.Current.Graph.RegisterObj(obj);
			return obj;
		}
	}
}
