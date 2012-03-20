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
			this.stateSelector = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleParallaxity = new System.Windows.Forms.ToolStripButton();
			this.parallaxRefDist = new DualityEditor.Controls.ToolStrip.ToolStripNumericUpDown();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.showBgColorDialog = new System.Windows.Forms.ToolStripButton();
			this.camSelector = new System.Windows.Forms.ToolStripComboBox();
			this.toolbarCamera = new System.Windows.Forms.ToolStrip();
			this.toolbarCamera.SuspendLayout();
			this.SuspendLayout();
			// 
			// stateSelector
			// 
			this.stateSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateSelector.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.stateSelector.Name = "stateSelector";
			this.stateSelector.Size = new System.Drawing.Size(121, 25);
			this.stateSelector.DropDown += new System.EventHandler(this.stateSelector_DropDown);
			this.stateSelector.DropDownClosed += new System.EventHandler(this.stateSelector_DropDownClosed);
			this.stateSelector.SelectedIndexChanged += new System.EventHandler(this.stateSelector_SelectedIndexChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
			this.parallaxRefDist.NumBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
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
			this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
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
			this.camSelector.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.camSelector.Name = "camSelector";
			this.camSelector.Size = new System.Drawing.Size(121, 25);
			this.camSelector.DropDown += new System.EventHandler(this.camSelector_DropDown);
			this.camSelector.DropDownClosed += new System.EventHandler(this.camSelector_DropDownClosed);
			this.camSelector.SelectedIndexChanged += new System.EventHandler(this.camSelector_SelectedIndexChanged);
			// 
			// toolbarCamera
			// 
			this.toolbarCamera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.toolbarCamera.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolbarCamera.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stateSelector,
            this.toolStripSeparator1,
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
			// CamView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(162)))), ((int)(((byte)(162)))));
			this.ClientSize = new System.Drawing.Size(539, 397);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripComboBox stateSelector;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toggleParallaxity;
		private DualityEditor.Controls.ToolStrip.ToolStripNumericUpDown parallaxRefDist;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton showBgColorDialog;
		private System.Windows.Forms.ToolStripComboBox camSelector;
		private System.Windows.Forms.ToolStrip toolbarCamera;

	}
}