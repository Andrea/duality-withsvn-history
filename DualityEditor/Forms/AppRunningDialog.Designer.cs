namespace DualityEditor.Forms
{
	partial class AppRunningDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppRunningDialog));
			this.descLabel = new System.Windows.Forms.Label();
			this.timerProcessState = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// descLabel
			// 
			resources.ApplyResources(this.descLabel, "descLabel");
			this.descLabel.Name = "descLabel";
			this.descLabel.UseWaitCursor = true;
			// 
			// timerProcessState
			// 
			this.timerProcessState.Interval = 200;
			this.timerProcessState.Tick += new System.EventHandler(this.timerProcessState_Tick);
			// 
			// AppRunningDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.descLabel);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AppRunningDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.UseWaitCursor = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label descLabel;
		private System.Windows.Forms.Timer timerProcessState;
	}
}