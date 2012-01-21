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
			this.toolbarCamera = new System.Windows.Forms.ToolStrip();
			this.stateSelector = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleAccMove = new System.Windows.Forms.ToolStripButton();
			this.toggleParallaxity = new System.Windows.Forms.ToolStripButton();
			this.parallaxRefDist = new DualityEditor.Controls.ToolStrip.ToolStripNumericUpDown();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.showBgColorDialog = new System.Windows.Forms.ToolStripButton();
			this.camSelector = new System.Windows.Forms.ToolStripComboBox();
			this.statusbarCamera = new System.Windows.Forms.StatusStrip();
			this.posXStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.posYStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.posZStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.angleStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.springLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockXLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockYLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.axisLockZLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolbarCamera.SuspendLayout();
			this.statusbarCamera.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolbarCamera
			// 
			this.toolbarCamera.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolbarCamera.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stateSelector,
            this.toolStripSeparator1,
            this.toggleAccMove,
            this.toggleParallaxity,
            this.parallaxRefDist,
            this.toolStripSeparator2,
            this.showBgColorDialog,
            this.camSelector});
			this.toolbarCamera.Location = new System.Drawing.Point(0, 0);
			this.toolbarCamera.Name = "toolbarCamera";
			this.toolbarCamera.Size = new System.Drawing.Size(539, 25);
			this.toolbarCamera.TabIndex = 1;
			this.toolbarCamera.Text = "toolStrip";
			// 
			// stateSelector
			// 
			this.stateSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateSelector.Name = "stateSelector";
			this.stateSelector.Size = new System.Drawing.Size(121, 25);
			this.stateSelector.DropDown += new System.EventHandler(this.stateSelector_DropDown);
			this.stateSelector.DropDownClosed += new System.EventHandler(this.stateSelector_DropDownClosed);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toggleAccMove
			// 
			this.toggleAccMove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toggleAccMove.Checked = true;
			this.toggleAccMove.CheckOnClick = true;
			this.toggleAccMove.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toggleAccMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleAccMove.Image = global::EditorBase.Properties.Resources.arrow_right_accelerate;
			this.toggleAccMove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toggleAccMove.Name = "toggleAccMove";
			this.toggleAccMove.Size = new System.Drawing.Size(23, 22);
			this.toggleAccMove.Text = "Accelerated Movement (A)";
			this.toggleAccMove.CheckedChanged += new System.EventHandler(this.toggleAccMove_CheckedChanged);
			// 
			// toggleParallaxity
			// 
			this.toggleParallaxity.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toggleParallaxity.Checked = true;
			this.toggleParallaxity.CheckOnClick = true;
			this.toggleParallaxity.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toggleParallaxity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleParallaxity.Image = ((System.Drawing.Image)(resources.GetObject("toggleParallaxity.Image")));
			this.toggleParallaxity.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toggleParallaxity.Name = "toggleParallaxity";
			this.toggleParallaxity.Size = new System.Drawing.Size(23, 22);
			this.toggleParallaxity.Text = "Toggle Perspective";
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
			this.parallaxRefDist.Size = new System.Drawing.Size(128, 22);
			this.parallaxRefDist.Text = "RefDist";
			this.parallaxRefDist.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.parallaxRefDist.ValueChanged += new System.EventHandler(this.parallaxRefDist_ValueChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// showBgColorDialog
			// 
			this.showBgColorDialog.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.showBgColorDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.showBgColorDialog.Image = ((System.Drawing.Image)(resources.GetObject("showBgColorDialog.Image")));
			this.showBgColorDialog.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.showBgColorDialog.Name = "showBgColorDialog";
			this.showBgColorDialog.Size = new System.Drawing.Size(23, 22);
			this.showBgColorDialog.Text = "Change Background Color";
			this.showBgColorDialog.Click += new System.EventHandler(this.showBgColorDialog_Click);
			// 
			// camSelector
			// 
			this.camSelector.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.camSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.camSelector.Name = "camSelector";
			this.camSelector.Size = new System.Drawing.Size(121, 25);
			this.camSelector.DropDown += new System.EventHandler(this.camSelector_DropDown);
			this.camSelector.DropDownClosed += new System.EventHandler(this.camSelector_DropDownClosed);
			// 
			// statusbarCamera
			// 
			this.statusbarCamera.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.posXStatusLabel,
            this.posYStatusLabel,
            this.posZStatusLabel,
            this.angleStatusLabel,
            this.springLabel,
            this.axisLockXLabel,
            this.axisLockYLabel,
            this.axisLockZLabel});
			this.statusbarCamera.Location = new System.Drawing.Point(0, 375);
			this.statusbarCamera.Name = "statusbarCamera";
			this.statusbarCamera.Size = new System.Drawing.Size(539, 22);
			this.statusbarCamera.SizingGrip = false;
			this.statusbarCamera.TabIndex = 2;
			this.statusbarCamera.Text = "statusStrip1";
			// 
			// posXStatusLabel
			// 
			this.posXStatusLabel.AutoSize = false;
			this.posXStatusLabel.Name = "posXStatusLabel";
			this.posXStatusLabel.Size = new System.Drawing.Size(100, 17);
			this.posXStatusLabel.Text = "X: x";
			this.posXStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// posYStatusLabel
			// 
			this.posYStatusLabel.AutoSize = false;
			this.posYStatusLabel.Name = "posYStatusLabel";
			this.posYStatusLabel.Size = new System.Drawing.Size(100, 17);
			this.posYStatusLabel.Text = "Y: y";
			this.posYStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// posZStatusLabel
			// 
			this.posZStatusLabel.AutoSize = false;
			this.posZStatusLabel.Name = "posZStatusLabel";
			this.posZStatusLabel.Size = new System.Drawing.Size(100, 17);
			this.posZStatusLabel.Text = "Z: z";
			this.posZStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// angleStatusLabel
			// 
			this.angleStatusLabel.AutoSize = false;
			this.angleStatusLabel.Name = "angleStatusLabel";
			this.angleStatusLabel.Size = new System.Drawing.Size(75, 17);
			this.angleStatusLabel.Text = "Angle: a";
			this.angleStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// springLabel
			// 
			this.springLabel.Name = "springLabel";
			this.springLabel.Size = new System.Drawing.Size(75, 17);
			this.springLabel.Spring = true;
			// 
			// axisLockXLabel
			// 
			this.axisLockXLabel.Enabled = false;
			this.axisLockXLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.axisLockXLabel.ForeColor = System.Drawing.Color.Red;
			this.axisLockXLabel.Name = "axisLockXLabel";
			this.axisLockXLabel.Size = new System.Drawing.Size(15, 17);
			this.axisLockXLabel.Text = "X";
			this.axisLockXLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// axisLockYLabel
			// 
			this.axisLockYLabel.Enabled = false;
			this.axisLockYLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.axisLockYLabel.ForeColor = System.Drawing.Color.Green;
			this.axisLockYLabel.Name = "axisLockYLabel";
			this.axisLockYLabel.Size = new System.Drawing.Size(14, 17);
			this.axisLockYLabel.Text = "Y";
			// 
			// axisLockZLabel
			// 
			this.axisLockZLabel.Enabled = false;
			this.axisLockZLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.axisLockZLabel.ForeColor = System.Drawing.Color.Blue;
			this.axisLockZLabel.Name = "axisLockZLabel";
			this.axisLockZLabel.Size = new System.Drawing.Size(14, 17);
			this.axisLockZLabel.Text = "Z";
			this.axisLockZLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CamView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 397);
			this.Controls.Add(this.statusbarCamera);
			this.Controls.Add(this.toolbarCamera);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CamView";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.Text = "CamView";
			this.toolbarCamera.ResumeLayout(false);
			this.toolbarCamera.PerformLayout();
			this.statusbarCamera.ResumeLayout(false);
			this.statusbarCamera.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolbarCamera;
		private System.Windows.Forms.StatusStrip statusbarCamera;
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
		private System.Windows.Forms.ToolStripComboBox camSelector;
		private System.Windows.Forms.ToolStripComboBox stateSelector;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}