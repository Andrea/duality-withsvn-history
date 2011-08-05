namespace ResourceHacker
{
	partial class ResourceHacker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceHacker));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new Aga.Controls.Tree.TreeViewAdv();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.actionOpen = new System.Windows.Forms.ToolStripButton();
			this.actionSave = new System.Windows.Forms.ToolStripButton();
			this.propertyGrid = new DualityEditor.Controls.PropertyGrid();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.treeViewColumnName = new Aga.Controls.Tree.TreeColumn();
			this.treeViewColumnObjId = new Aga.Controls.Tree.TreeColumn();
			this.nodeStateIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
			this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.nodeTextBoxObjId = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.treeView);
			this.splitContainer.Panel1.Controls.Add(this.mainToolStrip);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer.Size = new System.Drawing.Size(537, 447);
			this.splitContainer.SplitterDistance = 315;
			this.splitContainer.TabIndex = 0;
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.SystemColors.Window;
			this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeView.Columns.Add(this.treeViewColumnName);
			this.treeView.Columns.Add(this.treeViewColumnObjId);
			this.treeView.DefaultToolTipProvider = null;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
			this.treeView.FullRowSelect = true;
			this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
			this.treeView.Location = new System.Drawing.Point(0, 25);
			this.treeView.Model = null;
			this.treeView.Name = "treeView";
			this.treeView.NodeControls.Add(this.nodeStateIcon);
			this.treeView.NodeControls.Add(this.nodeTextBoxName);
			this.treeView.NodeControls.Add(this.nodeTextBoxObjId);
			this.treeView.SelectedNode = null;
			this.treeView.Size = new System.Drawing.Size(315, 422);
			this.treeView.TabIndex = 0;
			this.treeView.Text = "DataNodes";
			this.treeView.UseColumns = true;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionOpen,
            this.actionSave});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(315, 25);
			this.mainToolStrip.TabIndex = 1;
			// 
			// actionOpen
			// 
			this.actionOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionOpen.Image = global::ResourceHacker.Properties.Resources.iconOpenFile;
			this.actionOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionOpen.Name = "actionOpen";
			this.actionOpen.Size = new System.Drawing.Size(23, 22);
			this.actionOpen.Text = "Open Resource File...";
			this.actionOpen.Click += new System.EventHandler(this.actionOpen_Click);
			// 
			// actionSave
			// 
			this.actionSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionSave.Image = global::ResourceHacker.Properties.Resources.iconSaveFile;
			this.actionSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionSave.Name = "actionSave";
			this.actionSave.Size = new System.Drawing.Size(23, 22);
			this.actionSave.Text = "Save Resource File...";
			this.actionSave.Click += new System.EventHandler(this.actionSave_Click);
			// 
			// propertyGrid
			// 
			this.propertyGrid.AutoScroll = true;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(218, 447);
			this.propertyGrid.TabIndex = 0;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
			// 
			// treeViewColumnName
			// 
			this.treeViewColumnName.Header = "Name";
			this.treeViewColumnName.SortOrder = System.Windows.Forms.SortOrder.None;
			this.treeViewColumnName.TooltipText = null;
			this.treeViewColumnName.Width = 200;
			// 
			// treeViewColumnObjId
			// 
			this.treeViewColumnObjId.Header = "ObjId";
			this.treeViewColumnObjId.SortOrder = System.Windows.Forms.SortOrder.None;
			this.treeViewColumnObjId.TooltipText = null;
			// 
			// nodeStateIcon
			// 
			this.nodeStateIcon.DataPropertyName = "Image";
			this.nodeStateIcon.LeftMargin = 1;
			this.nodeStateIcon.ParentColumn = this.treeViewColumnName;
			this.nodeStateIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
			// 
			// nodeTextBoxName
			// 
			this.nodeTextBoxName.DataPropertyName = "Text";
			this.nodeTextBoxName.IncrementalSearchEnabled = true;
			this.nodeTextBoxName.LeftMargin = 3;
			this.nodeTextBoxName.ParentColumn = this.treeViewColumnName;
			// 
			// nodeTextBoxObjId
			// 
			this.nodeTextBoxObjId.DataPropertyName = "ObjId";
			this.nodeTextBoxObjId.IncrementalSearchEnabled = true;
			this.nodeTextBoxObjId.LeftMargin = 3;
			this.nodeTextBoxObjId.ParentColumn = this.treeViewColumnObjId;
			// 
			// ResourceHacker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 447);
			this.Controls.Add(this.splitContainer);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResourceHacker";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.Text = "Resource Hacker";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private Aga.Controls.Tree.TreeViewAdv treeView;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private DualityEditor.Controls.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripButton actionOpen;
		private System.Windows.Forms.ToolStripButton actionSave;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private Aga.Controls.Tree.TreeColumn treeViewColumnName;
		private Aga.Controls.Tree.TreeColumn treeViewColumnObjId;
		private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxObjId;


	}
}