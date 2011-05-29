namespace EditorBase
{
	partial class CamView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.showBgColorDialog = new System.Windows.Forms.ToolStripButton();
			this.toggleAccMove = new System.Windows.Forms.ToolStripButton();
			this.toggleParallaxity = new System.Windows.Forms.ToolStripButton();
			this.parallaxRefDist = new DualityEditor.Controls.ToolStrip.ToolStripNumericUpDown();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.posXStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.posYStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.posZStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.angleStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.springLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockXLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockYLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockZLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showBgColorDialog,
            this.toggleAccMove,
            this.toggleParallaxity,
            this.parallaxRefDist});
			resources.ApplyResources(this.toolStrip, "toolStrip");
			this.toolStrip.Name = "toolStrip";
			// 
			// showBgColorDialog
			// 
			this.showBgColorDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.showBgColorDialog, "showBgColorDialog");
			this.showBgColorDialog.Name = "showBgColorDialog";
			this.showBgColorDialog.Click += new System.EventHandler(this.showBgColorDialog_Click);
			// 
			// toggleAccMove
			// 
			this.toggleAccMove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toggleAccMove.Checked = true;
			this.toggleAccMove.CheckOnClick = true;
			this.toggleAccMove.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toggleAccMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleAccMove.Image = global::EditorBase.Properties.Resources.arrow_right_accelerate;
			resources.ApplyResources(this.toggleAccMove, "toggleAccMove");
			this.toggleAccMove.Name = "toggleAccMove";
			// 
			// toggleParallaxity
			// 
			this.toggleParallaxity.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toggleParallaxity.Checked = true;
			this.toggleParallaxity.CheckOnClick = true;
			this.toggleParallaxity.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toggleParallaxity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.toggleParallaxity, "toggleParallaxity");
			this.toggleParallaxity.Name = "toggleParallaxity";
			this.toggleParallaxity.CheckStateChanged += new System.EventHandler(this.toggleParallaxity_CheckStateChanged);
			// 
			// parallaxRefDist
			// 
			this.parallaxRefDist.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.parallaxRefDist.BackColor = System.Drawing.Color.Transparent;
			this.parallaxRefDist.DecimalPlaces = 2;
			this.parallaxRefDist.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.parallaxRefDist.Name = "parallaxRefDist";
			this.parallaxRefDist.NumericWidth = 75;
			resources.ApplyResources(this.parallaxRefDist, "parallaxRefDist");
			this.parallaxRefDist.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.parallaxRefDist.ValueChanged += new System.EventHandler(this.parallaxRefDist_ValueChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.posXStatusLabel,
            this.posYStatusLabel,
            this.posZStatusLabel,
            this.angleStatusLabel,
            this.springLabel,
            this.axisLockXLabel,
            this.axisLockYLabel,
            this.axisLockZLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.SizingGrip = false;
			// 
			// posXStatusLabel
			// 
			resources.ApplyResources(this.posXStatusLabel, "posXStatusLabel");
			this.posXStatusLabel.Name = "posXStatusLabel";
			// 
			// posYStatusLabel
			// 
			resources.ApplyResources(this.posYStatusLabel, "posYStatusLabel");
			this.posYStatusLabel.Name = "posYStatusLabel";
			// 
			// posZStatusLabel
			// 
			resources.ApplyResources(this.posZStatusLabel, "posZStatusLabel");
			this.posZStatusLabel.Name = "posZStatusLabel";
			// 
			// angleStatusLabel
			// 
			resources.ApplyResources(this.angleStatusLabel, "angleStatusLabel");
			this.angleStatusLabel.Name = "angleStatusLabel";
			// 
			// springLabel
			// 
			this.springLabel.Name = "springLabel";
			resources.ApplyResources(this.springLabel, "springLabel");
			this.springLabel.Spring = true;
			// 
			// axisLockXLabel
			// 
			resources.ApplyResources(this.axisLockXLabel, "axisLockXLabel");
			this.axisLockXLabel.ForeColor = System.Drawing.Color.Red;
			this.axisLockXLabel.Name = "axisLockXLabel";
			// 
			// axisLockYLabel
			// 
			resources.ApplyResources(this.axisLockYLabel, "axisLockYLabel");
			this.axisLockYLabel.ForeColor = System.Drawing.Color.Green;
			this.axisLockYLabel.Name = "axisLockYLabel";
			// 
			// axisLockZLabel
			// 
			resources.ApplyResources(this.axisLockZLabel, "axisLockZLabel");
			this.axisLockZLabel.ForeColor = System.Drawing.Color.Blue;
			this.axisLockZLabel.Name = "axisLockZLabel";
			// 
			// CamView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.toolStrip);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CamView";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel posXStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel posYStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel posZStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel angleStatusLabel;
		private System.Windows.Forms.ToolStripButton toggleParallaxity;
		private DualityEditor.Controls.ToolStrip.ToolStripNumericUpDown parallaxRefDist;
		private System.Windows.Forms.ToolStripButton showBgColorDialog;
		private System.Windows.Forms.ToolStripStatusLabel springLabel;
		private System.Windows.Forms.ToolStripStatusLabel axisLockXLabel;
		private System.Windows.Forms.ToolStripStatusLabel axisLockYLabel;
		private System.Windows.Forms.ToolStripStatusLabel axisLockZLabel;
		private System.Windows.Forms.ToolStripButton toggleAccMove;
	}
}