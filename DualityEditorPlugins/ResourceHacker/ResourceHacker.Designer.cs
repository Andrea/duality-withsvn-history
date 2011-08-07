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
			this.treeViewColumnName = new Aga.Controls.Tree.TreeColumn();
			this.treeViewColumnObjId = new Aga.Controls.Tree.TreeColumn();
			this.treeViewColumnValue = new Aga.Controls.Tree.TreeColumn();
			this.treeViewColumnType = new Aga.Controls.Tree.TreeColumn();
			this.nodeStateIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
			this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.nodeTextBoxObjId = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.nodeTextBoxType = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.nodeTextBoxValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.actionOpen = new System.Windows.Forms.ToolStripButton();
			this.actionSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.actionRenameType = new System.Windows.Forms.ToolStripButton();
			this.actionRenameField = new System.Windows.Forms.ToolStripButton();
			this.propertyGrid = new DualityEditor.Controls.PropertyGrid();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
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
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.SystemColors.Window;
			this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeView.Columns.Add(this.treeViewColumnName);
			this.treeView.Columns.Add(this.treeViewColumnObjId);
			this.treeView.Columns.Add(this.treeViewColumnValue);
			this.treeView.Columns.Add(this.treeViewColumnType);
			this.treeView.DefaultToolTipProvider = null;
			resources.ApplyResources(this.treeView, "treeView");
			this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
			this.treeView.FullRowSelect = true;
			this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
			this.treeView.Model = null;
			this.treeView.Name = "treeView";
			this.treeView.NodeControls.Add(this.nodeStateIcon);
			this.treeView.NodeControls.Add(this.nodeTextBoxName);
			this.treeView.NodeControls.Add(this.nodeTextBoxObjId);
			this.treeView.NodeControls.Add(this.nodeTextBoxType);
			this.treeView.NodeControls.Add(this.nodeTextBoxValue);
			this.treeView.SelectedNode = null;
			this.treeView.UseColumns = true;
			this.treeView.SelectionChanged += new System.EventHandler(this.treeView_SelectionChanged);
			// 
			// treeViewColumnName
			// 
			resources.ApplyResources(this.treeViewColumnName, "treeViewColumnName");
			this.treeViewColumnName.SortOrder = System.Windows.Forms.SortOrder.None;
			// 
			// treeViewColumnObjId
			// 
			resources.ApplyResources(this.treeViewColumnObjId, "treeViewColumnObjId");
			this.treeViewColumnObjId.SortOrder = System.Windows.Forms.SortOrder.None;
			// 
			// treeViewColumnValue
			// 
			resources.ApplyResources(this.treeViewColumnValue, "treeViewColumnValue");
			this.treeViewColumnValue.SortOrder = System.Windows.Forms.SortOrder.None;
			// 
			// treeViewColumnType
			// 
			resources.ApplyResources(this.treeViewColumnType, "treeViewColumnType");
			this.treeViewColumnType.SortOrder = System.Windows.Forms.SortOrder.None;
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
			// nodeTextBoxType
			// 
			this.nodeTextBoxType.DataPropertyName = "ResolvedTypeName";
			this.nodeTextBoxType.IncrementalSearchEnabled = true;
			this.nodeTextBoxType.LeftMargin = 3;
			this.nodeTextBoxType.ParentColumn = this.treeViewColumnType;
			// 
			// nodeTextBoxValue
			// 
			this.nodeTextBoxValue.DataPropertyName = "DataValue";
			this.nodeTextBoxValue.IncrementalSearchEnabled = true;
			this.nodeTextBoxValue.LeftMargin = 3;
			this.nodeTextBoxValue.ParentColumn = this.treeViewColumnValue;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionOpen,
            this.actionSave,
            this.toolStripSeparator1,
            this.actionRenameType,
            this.actionRenameField});
			resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
			this.mainToolStrip.Name = "mainToolStrip";
			// 
			// actionOpen
			// 
			this.actionOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.actionOpen, "actionOpen");
			this.actionOpen.Name = "actionOpen";
			this.actionOpen.Click += new System.EventHandler(this.actionOpen_Click);
			// 
			// actionSave
			// 
			this.actionSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionSave.Image = global::ResourceHacker.Properties.Resources.iconSaveFile;
			resources.ApplyResources(this.actionSave, "actionSave");
			this.actionSave.Name = "actionSave";
			this.actionSave.Click += new System.EventHandler(this.actionSave_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// actionRenameType
			// 
			this.actionRenameType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionRenameType.Image = global::ResourceHacker.Properties.Resources.iconRenameClass;
			resources.ApplyResources(this.actionRenameType, "actionRenameType");
			this.actionRenameType.Name = "actionRenameType";
			this.actionRenameType.Click += new System.EventHandler(this.actionRenameType_Click);
			// 
			// actionRenameField
			// 
			this.actionRenameField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionRenameField.Image = global::ResourceHacker.Properties.Resources.iconRenameField;
			resources.ApplyResources(this.actionRenameField, "actionRenameField");
			this.actionRenameField.Name = "actionRenameField";
			this.actionRenameField.Click += new System.EventHandler(this.actionRenameField_Click);
			// 
			// propertyGrid
			// 
			resources.ApplyResources(this.propertyGrid, "propertyGrid");
			this.propertyGrid.Name = "propertyGrid";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
			// 
			// ResourceHacker
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ResourceHacker";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
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
		private Aga.Controls.Tree.TreeColumn treeViewColumnType;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxType;
		private Aga.Controls.Tree.TreeColumn treeViewColumnValue;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxValue;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton actionRenameType;
		private System.Windows.Forms.ToolStripButton actionRenameField;


	}
}