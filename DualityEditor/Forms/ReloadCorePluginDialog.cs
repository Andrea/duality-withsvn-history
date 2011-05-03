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
	public partial class ReloadCorePluginDialog : Form
	{
		private class WorkerInterface
		{
			private	bool			finished	= false;
			private	float			progress	= 0.0f;
			private	Exception		error		= null;
			private	List<string>	reloadSched	= null;
			private	bool			recoverMode	= false;

			public bool Finished
			{
				get { return this.finished; }
				set { this.finished = value; }
			}
			public float Progress
			{
				get { return this.progress; }
				set { this.progress = value; }
			}
			public Exception Error
			{
				get { return this.error; }
				set { this.error = value; }
			}
			public List<string> ReloadSched
			{
				get { return this.reloadSched; }
				set { this.reloadSched = value; }
			}
			public bool RecoverMode
			{
				get { return this.recoverMode; }
				set { this.recoverMode = value; }
			}
		}

		public enum ReloaderState
		{
			Idle,
			WaitForPlugins,
			ReloadPlugins,

			RecoverFromRestart
		}


		Thread			worker			= null;
		WorkerInterface	workerInterface	= null;
		IWin32Window	owner			= null;
		List<string>	reloadSchedule	= new List<string>();
		ReloaderState	state			= ReloaderState.Idle;
		int				waitTime		= 0;


		public	event	EventHandler	BeforeBeginReload	= null;
		public	event	EventHandler	AfterEndReload		= null;


		public List<string> ReloadSchedule
		{
			get { return this.reloadSchedule; }
		}
		public ReloaderState State
		{
			get { return this.state; }
			set
			{
				if (this.state == ReloaderState.ReloadPlugins) return;

				this.state = value;
				if (this.state == ReloaderState.Idle)
				{
					this.progressTimer.Stop();
				}
				else if (this.state == ReloaderState.WaitForPlugins)
				{
					this.waitTime = 0;
					this.progressTimer.Start();
				}
				else if (this.state == ReloaderState.ReloadPlugins)
				{
					this.ShowDialog(this.owner);
				}
				else if (this.state == ReloaderState.RecoverFromRestart)
				{
					this.ShowDialog(this.owner);
				}
			}
		}


		public ReloadCorePluginDialog(IWin32Window owner)
		{
			this.InitializeComponent();
			this.owner = owner;
		}

		protected void OnBeforeBeginReload()
		{
			if (this.BeforeBeginReload != null)
				this.BeforeBeginReload(this, null);
		}
		protected void OnAfterEndReload()
		{
			if (this.AfterEndReload != null)
				this.AfterEndReload(this, null);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (this.state != ReloaderState.RecoverFromRestart)
			{
				this.OnBeforeBeginReload();
				this.state = ReloaderState.ReloadPlugins;
			}

			this.progressTimer.Start();
			this.Owner.SetTaskbarOverlayIcon(GeneralRes.Icon_Cog, GeneralRes.TaskBarOverlay_ReloadCorePlugin_Desc);

			this.workerInterface = new WorkerInterface();
			if (this.state != ReloaderState.RecoverFromRestart)
				this.workerInterface.ReloadSched = new List<string>(this.reloadSchedule);
			else
				this.workerInterface.RecoverMode = true;

			this.worker = new Thread(WorkerThreadProc);
			this.worker.Start(this.workerInterface);

			this.state = ReloaderState.ReloadPlugins;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			this.Owner.SetTaskbarProgress(0.0f);
			this.Owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.NoProgress);
			this.Owner.SetTaskbarOverlayIcon(null, null);
			this.reloadSchedule.Clear();

			this.state = ReloaderState.Idle;
			this.OnAfterEndReload();
		}

		private void progressTimer_Tick(object sender, EventArgs e)
		{
			if (this.state == ReloaderState.WaitForPlugins)
			{
				this.waitTime += this.progressTimer.Interval;
				if (this.waitTime > 1000)
				{
					this.State = ReloaderState.ReloadPlugins;
				}
			}
			else if (this.state == ReloaderState.ReloadPlugins)
			{
				this.progressBar.Value = (int)Math.Round(this.workerInterface.Progress * 100.0f);
				this.Owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.Normal);
				this.Owner.SetTaskbarProgress(this.progressBar.Value);

				if (this.workerInterface.Error != null)
				{
					this.progressTimer.Stop();

					this.Owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.Error);
					MessageBox.Show(this, 
						String.Format(GeneralRes.Msg_ErrorReloadCorePlugin_Desc, "\n", this.workerInterface.Error.ToString()), 
						GeneralRes.Msg_ErrorReloadCorePlugin_Caption, 
						MessageBoxButtons.OK, MessageBoxIcon.Error);

					this.Close();
				}
				else if (this.workerInterface.Finished)
				{
					this.progressTimer.Stop();
					this.Close();
				}
			}
		}
		
		private static void WorkerThreadProc(object args)
		{
			WorkerInterface workInterface = args as WorkerInterface;

			try
			{
				Stream str;
				bool fullRestart = false;

				if (!workInterface.RecoverMode)
				{
					foreach (string asmFile in workInterface.ReloadSched)
					{
						if (asmFile.EndsWith(".editor.dll") || !DualityApp.IsLeafPlugin(asmFile))
						{
							fullRestart = true;
							break;
						}
					}

					if (fullRestart)
					{
						if (!Directory.Exists("Temp")) Directory.CreateDirectory("Temp");
						str = File.Create(@"Temp\_reloadPluginData.tmp");
					}
					else
						str = new MemoryStream(1024 * 1024 * 10);

					// Save current data
					Log.Editor.Write("Saving data...");
					Scene.Current.Save(str);
					workInterface.Progress += 0.4f;
			
					if (!fullRestart)
					{
						// Abandon all data
						Scene.Current = null;

						// Reload core plugins
						Log.Editor.Write("Reloading core plugins...");
						Log.Editor.PushIndent();
						int count = workInterface.ReloadSched.Count;
						while (workInterface.ReloadSched.Count > 0)
						{
							DualityApp.ReloadPlugin(workInterface.ReloadSched[0]);
							workInterface.ReloadSched.RemoveAt(0);
							workInterface.Progress += 0.2f / (float)count;
						}
						Log.Editor.PopIndent();

						str.Seek(0, SeekOrigin.Begin);
					}
					else
					{
						str.Close();
						bool debug = System.Diagnostics.Debugger.IsAttached;
						Application.Exit();
						Process newEditor = Process.Start(Application.ExecutablePath, "recover" + (debug ? " debug" : ""));
						return;
					}
				}
				else
				{
					str = File.OpenRead(@"Temp\_reloadPluginData.tmp");
					workInterface.Progress = 0.6f;
				}

				// Reload data
				Log.Editor.Write("Restoring data...");
				Scene.Current = Resource.LoadResource<Scene>(str);
				str.Close();

				workInterface.Progress = 1.0f;
				workInterface.Finished = true;
			}
			catch (Exception e)
			{
				Log.Editor.WriteError(e.ToString());
				workInterface.Error = e;
			}
		}
	}
}
