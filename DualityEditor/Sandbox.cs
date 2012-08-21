using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duality;
using Duality.Resources;

using OpenTK;

namespace DualityEditor
{
	public enum SandboxState
	{
		Inactive,
		Playing,
		Paused
	}

	public static class Sandbox
	{
		private	static bool			stateChange	= false;
		private	static int			sceneFreeze	= 0;
		private	static SandboxState	state		= SandboxState.Inactive;

		public	static	event	EventHandler	Entering		= null;
		public	static	event	EventHandler	Leave			= null;
		public	static	event	EventHandler	Paused			= null;
		public	static	event	EventHandler	Unpausing		= null;
		public	static	event	EventHandler	StateChanged	= null;
		
		public static SandboxState State
		{
			get { return state; }
		}
		public static bool IsActive
		{
			get { return state != SandboxState.Inactive; }
		}
		public static bool IsFreezed
		{
			get { return sceneFreeze > 0; }
		}
		
		
		internal static void Init()
		{
			Scene.Leaving += Scene_Leaving;
		}
		internal static void Terminate()
		{
			Stop();
			Scene.Leaving -= Scene_Leaving;
		}

		public static bool Play()
		{
			if (state == SandboxState.Playing) return true;

			// If the current Scene is unsaved when entering sandbox mode, ask whether this should be done before
			if (Scene.Current.IsRuntimeResource)
			{
				DialogResult result = MessageBox.Show(DualityEditorApp.MainForm,
					EditorRes.GeneralRes.Msg_EnterSandboxUnsavedScene_Desc,
					EditorRes.GeneralRes.Msg_EnterSandboxUnsavedScene_Caption,
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				if (result == DialogResult.Cancel) return false;
				if (result == DialogResult.Yes) DualityEditorApp.SaveCurrentScene(false);
			}

			stateChange = true;
			if (state == SandboxState.Paused)
			{
				OnUnpausingSandbox();
				state = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;
			}
			else
			{
				OnEnteringSandbox();

				// Save the current scene
				DualityEditorApp.SaveCurrentScene();
				
				// Force later Scene reload by disposing it
				string curPath = null;
				if (!String.IsNullOrEmpty(Scene.Current.Path))
				{
					curPath = Scene.CurrentPath;
					Scene.Current.Dispose();
				}

				state = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;

				// (Re)Load Scene.
				if (curPath != null)
					Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;
			}

			OnSandboxStateChanged();
			stateChange = false;
			return true;
		}
		public static void Pause()
		{
			if (state == SandboxState.Paused) return;
			stateChange = true;

			state = SandboxState.Paused;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;

			OnPausedSandbox();
			OnSandboxStateChanged();
			stateChange = false;
		}
		public static void Stop()
		{
			if (state == SandboxState.Inactive) return;
			stateChange = true;

			// Force later Scene reload by disposing it
			string curPath = null;
			if (!String.IsNullOrEmpty(Scene.Current.Path))
			{
				curPath = Scene.CurrentPath;
				Scene.Current.Dispose();
			}

			state = SandboxState.Inactive;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;
			
			// (Re)Load Scene
			if (curPath != null)
				Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;

			OnLeaveSandbox();
			OnSandboxStateChanged();
			stateChange = false;
		}
		public static void Freeze()
		{
			sceneFreeze++;
		}
		public static void UnFreeze()
		{
			sceneFreeze--;
		}
		public static void SetEngineInput(IMouseInput mouse, IKeyboardInput keyboard)
		{
			DualityApp.Mouse = mouse;
			DualityApp.Keyboard = keyboard;
		}

		private static void OnEnteringSandbox()
		{
			if (Entering != null)
				Entering(null, EventArgs.Empty);
		}
		private static void OnLeaveSandbox()
		{
			if (Leave != null)
				Leave(null, EventArgs.Empty);
		}
		private static void OnPausedSandbox()
		{
			if (Paused != null)
				Paused(null, EventArgs.Empty);
		}
		private static void OnUnpausingSandbox()
		{
			if (Unpausing != null)
				Unpausing(null, EventArgs.Empty);
		}
		private static void OnSandboxStateChanged()
		{
			if (StateChanged != null)
				StateChanged(null, EventArgs.Empty);
		}

		private static void Scene_Leaving(object sender, EventArgs e)
		{
			if (stateChange) return;

			// Force later Scene reload by disposing it
			string curPath = null;
			if (!String.IsNullOrEmpty(Scene.Current.Path))
			{
				curPath = Scene.CurrentPath;
				Scene.Current.Dispose();
			}
		}
	}
}
