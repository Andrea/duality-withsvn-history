namespace DualityEditor.Forms
{
	partial class ReloadCorePluginDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReloadCorePluginDialog));
			this.descLabel = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.progressTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// descLabel
			// 
			resources.ApplyResources(this.descLabel, "descLabel");
			this.descLabel.Name = "descLabel";
			this.descLabel.UseWaitCursor = true;
			// 
			// progressBar
			// 
			resources.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			this.progressBar.UseWaitCursor = true;
			// 
			// progressTimer
			// 
			this.progressTimer.Interval = 50;
			this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
			// 
			// ReloadCorePluginDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.descLabel);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReloadCorePluginDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.UseWaitCursor = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label descLabel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Timer progressTimer;
	}
}