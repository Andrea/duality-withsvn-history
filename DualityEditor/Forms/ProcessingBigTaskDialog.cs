using System;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using Windows7.DesktopIntegration;
using Windows7.DesktopIntegration.WindowsForms;

using Duality;
using DualityEditor.EditorRes;

namespace DualityEditor.Forms
{
	public partial class ProcessingBigTaskDialog : Form
	{
		public delegate IEnumerable TaskAction(WorkerInterface worker);
		public class WorkerInterface
		{
			internal	ProcessingBigTaskDialog	owner	= null;
			private	Exception	error		= null;
			private	MainForm	mainForm	= null;
			private	TaskAction	task		= null;
			private float		progress	= 0.0f;
			private string		stateDesc	= null;
			private	object		data		= null;

			public float Progress
			{
				get { return this.progress; }
				set { this.progress = value; }
			}
			public string StateDesc
			{
				get { return this.stateDesc; }
				set { this.stateDesc = value; }
			}
			public Exception Error
			{
				get { return this.error; }
				set { this.error = value; }
			}
			public MainForm MainForm
			{
				get { return this.mainForm; }
				set { this.mainForm = value; }
			}
			public TaskAction Task
			{
				get { return this.task; }
				set { this.task = value; }
			}
			public object Data
			{
				get { return this.data; }
				set { this.data = value; }
			}
		}


		Thread					worker			= null;
		WorkerInterface			workerInterface	= null;
		MainForm				owner			= null;
		TaskAction				task			= null;
		object					data			= null;
		string					taskCaption		= "Performing Task...";
		string					taskDesc		= "A task is being performed. Please wait.";


		public ProcessingBigTaskDialog(string caption, string desc, TaskAction task, object data) : this(MainForm.Instance, caption, desc, task, data) {}
		public ProcessingBigTaskDialog(MainForm owner, string caption, string desc, TaskAction task, object data)
		{
			this.InitializeComponent();

			this.owner = owner;
			this.taskCaption = caption;
			this.taskDesc = desc;
			this.task = task;
			this.data = data;

			this.Text = this.taskCaption;
			this.descLabel.Text = this.taskDesc;
			this.stateDescLabel.Text = "";
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.owner.SetTaskbarOverlayIcon(GeneralRes.Icon_Cog, this.taskCaption);

			this.workerInterface = new WorkerInterface();
			this.workerInterface.owner = this;
			this.workerInterface.MainForm = this.owner;
			this.workerInterface.Task = this.task;
			this.workerInterface.Data = this.data;

			this.progressTimer.Start();

			this.worker = new Thread(WorkerThreadProc);
			this.worker.Start(this.workerInterface);
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			this.owner.SetTaskbarProgress(0.0f);
			this.owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.NoProgress);
			this.owner.SetTaskbarOverlayIcon(null, null);
		}
		private void UpdateStateDesc()
		{
			this.stateDescLabel.Text = this.workerInterface.StateDesc;
		}

		private void progressTimer_Tick(object sender, EventArgs e)
		{
			this.progressBar.Value = (int)Math.Round(this.workerInterface.Progress * 100.0f);
			this.owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.Normal);
			this.owner.SetTaskbarProgress(this.progressBar.Value);

			if (this.workerInterface.Error != null)
			{
				this.progressTimer.Stop();

				this.owner.SetTaskbarProgressState(Windows7Taskbar.ThumbnailProgressState.Error);
				MessageBox.Show(this, 
					String.Format(GeneralRes.Msg_ErrorPerformBigTask_Desc, this.taskCaption, "\n", Log.Exception(this.workerInterface.Error)), 
					GeneralRes.Msg_ErrorPerformBigTask_Caption, 
					MessageBoxButtons.OK, MessageBoxIcon.Error);

				this.Close();
			}
			else if (!this.worker.IsAlive)
			{
				this.progressTimer.Stop();
				this.Close();
			}
		}
		
		private static void WorkerThreadProc(object args)
		{
			WorkerInterface workInterface = args as WorkerInterface;
			
			workInterface.Error = null;
			workInterface.Progress = 0.0f;
			try 
			{
				var taskEnumerator = workInterface.Task(workInterface).GetEnumerator();
				DateTime lastCheck = DateTime.Now;
				while ((bool)workInterface.MainForm.Invoke((Func<bool>)taskEnumerator.MoveNext))
				{
					TimeSpan lastTime = DateTime.Now - lastCheck;
					workInterface.MainForm.Invoke((Action)workInterface.owner.UpdateStateDesc);
					Thread.Sleep((int)Math.Min(20, lastTime.TotalMilliseconds));
					lastCheck = DateTime.Now;
				}

				workInterface.Progress = 1.0f;
			}
			catch (Exception e)
			{
				Log.Editor.WriteError("Failed to perform task: {0}", Log.Exception(e));
				workInterface.Error = e;
			}
		}
	}
}
