using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		private static Random rnd = new Random();

		public DualityLauncher(int w, int h, GraphicsMode mode, string title = "Duality Launcher", GameWindowFlags flags = GameWindowFlags.Default)
			: base(w, h, mode, title, flags)
		{
		}

		// ------- DEBUG --------
		private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (DualityApp.Keyboard[Key.F5])
			{
				Log.Core.Write("------ DEBUG: Reloading Plugins ------");
				Log.Core.PushIndent();

				//Type testCmpType = DualityApp.PluginTypeBinder.BindToType("TestPlugin2.core", "TestPlugin2.SomeInterfaceComponent");
				//GameObject testObj = new GameObject();
				//testObj.Name = "TestObj";
				//testObj.AddComponent(testCmpType);
				//Component cmp = testObj.GetComponent(testCmpType);
				//Scene.Current.RegisterObj(testObj);

				//ContentRef<Prefab> prefab1 = ContentProvider.RequestContent<Prefab>("obj.prefab.res");
				//GameObject prefab1Inst = prefab1.Res.Instantiate();
				//prefab1Inst.Name = "Some TestObject";
				//prefab1Inst.GetComponent<RectRenderer>().Color = ColorRGBA.Green;
				//prefab1Inst.PrefabLink.PushChange(prefab1Inst, typeof(GameObject).GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance), prefab1Inst.Name);
				//prefab1Inst.PrefabLink.PushChange(prefab1Inst.GetComponent<RectRenderer>(), typeof(RectRenderer).GetField("color", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance), prefab1Inst.GetComponent<RectRenderer>().Color);
				//Scene.Current.RegisterObjDeep(prefab1Inst);

				System.IO.MemoryStream str = new System.IO.MemoryStream(1024 * 1024 * 10);
				Scene.Current.Save(str);
				
				DualityApp.LoadPlugins();

				str.Seek(0, System.IO.SeekOrigin.Begin);
				Scene.Current = Resource.LoadResource<Scene>(str); // Note: Won't work anymore. Type Resolving only done in Editor mode
				str.Close();

				//testObj = Scene.Current.AllObjects.First();
				//testCmpType = DualityApp.PluginTypeBinder.BindToType("TestPlugin2.core", "TestPlugin2.SomeInterfaceComponent");
				//cmp = testObj.GetComponent(testCmpType);
				//Type[] interf = cmp.GetType().GetInterfaces();
				//System.Reflection.AssemblyName[] refAssemblies = testCmpType.Assembly.GetReferencedAssemblies();

				//for (int i = 0; i < AppDomain.CurrentDomain.GetAssemblies().Length; i++)
				//{
				//    if (AppDomain.CurrentDomain.GetAssemblies()[i] == cmp.GetType().Assembly)
				//        Log.Core.Write("cmp.GetType() from {0}", i);
				//    if (AppDomain.CurrentDomain.GetAssemblies()[i] == interf[1].Assembly)
				//        Log.Core.Write("interface.GetType() from {0}", i);
				//}
				//Scene.Current.UnregisterObj(testObj);
				//testObj.Dispose();

				Log.Core.PopIndent();
				Log.Core.Write("--------------- /DEBUG ---------------");
			}
			else if (DualityApp.Keyboard[Key.Escape])
			{
				this.Close();
			}
		}
		// -----------------------

		protected override void OnResize(EventArgs e)
		{
			DualityApp.TargetResolution = new Vector2(Width, Height);
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			DualityApp.Update();
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			DualityApp.Draw();
			this.SwapBuffers();
		}

		[STAThread]
		public static void Main()
		{
			DualityApp.Init(DualityApp.ExecutionContext.Launcher);
			using (DualityLauncher launcherWindow = new DualityLauncher(800, 600, DualityApp.DefaultMode))
			{
				// Retrieve icon fromexecutable file and set it as window icon
				string executablePath = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
				launcherWindow.Icon = System.Drawing.Icon.ExtractAssociatedIcon(executablePath);

				// Initialize default content
				launcherWindow.MakeCurrent();
				DualityApp.TargetMode = launcherWindow.Context.GraphicsMode;
				ContentProvider.InitDefaultContent();

				// Input setup
				DualityApp.Mouse = launcherWindow.Mouse;
				DualityApp.Keyboard = launcherWindow.Keyboard;
				DualityApp.Joysticks = launcherWindow.Joysticks;

				// Debug: Debug Hotkeys
				DualityApp.Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(launcherWindow.Keyboard_KeyDown);

				// Debug: Load test scene
				Scene.Current = Resource.LoadResource<Scene>("scene.tmp");
				Scene.Current.AbandonPrefabLinks();

				// Run the DualityApp
				launcherWindow.Run(60.0d, 60.0d);

				// Debug: Save test scene
				//Scene.Current.Save("scene.tmp");
			}
			DualityApp.Terminate();
		}
	}
}
