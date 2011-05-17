namespace EditorBase
{
	partial class ProjectFolderView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectFolderView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonWorkDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabelProjectName = new System.Windows.Forms.ToolStripLabel();
			this.folderView = new Aga.Controls.Tree.TreeViewAdv();
			this.contextMenuNode = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparatorNew = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorShowInExplorer = new System.Windows.Forms.ToolStripSeparator();
			this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nodeStateIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
			this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.timerFlashItem = new System.Windows.Forms.Timer(this.components);
			this.contextMenuDragMoveCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.labelFilter = new System.Windows.Forms.Label();
			this.toolStrip.SuspendLayout();
			this.contextMenuNode.SuspendLayout();
			this.contextMenuDragMoveCopy.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonWorkDir,
            this.toolStripLabelProjectName});
			resources.ApplyResources(this.toolStrip, "toolStrip");
			this.toolStrip.Name = "toolStrip";
			// 
			// toolStripButtonWorkDir
			// 
			this.toolStripButtonWorkDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonWorkDir.Image = global::EditorBase.Properties.Resources.WorkingFolderIcon16;
			resources.ApplyResources(this.toolStripButtonWorkDir, "toolStripButtonWorkDir");
			this.toolStripButtonWorkDir.Name = "toolStripButtonWorkDir";
			this.toolStripButtonWorkDir.Click += new System.EventHandler(this.toolStripButtonWorkDir_Click);
			// 
			// toolStripLabelProjectName
			// 
			this.toolStripLabelProjectName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripLabelProjectName.Name = "toolStripLabelProjectName";
			resources.ApplyResources(this.toolStripLabelProjectName, "toolStripLabelProjectName");
			// 
			// folderView
			// 
			this.folderView.AllowDrop = true;
			this.folderView.BackColor = System.Drawing.SystemColors.Window;
			this.folderView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.folderView.ContextMenuStrip = this.contextMenuNode;
			this.folderView.DefaultToolTipProvider = null;
			this.folderView.DisplayDraggingNodes = true;
			resources.ApplyResources(this.folderView, "folderView");
			this.folderView.DragDropMarkColor = System.Drawing.Color.Black;
			this.folderView.LineColor = System.Drawing.SystemColors.ControlDark;
			this.folderView.Model = null;
			this.folderView.Name = "folderView";
			this.folderView.NodeControls.Add(this.nodeStateIcon);
			this.folderView.NodeControls.Add(this.nodeTextBoxName);
			this.folderView.SelectedNode = null;
			this.folderView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
			this.folderView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.folderView_ItemDrag);
			this.folderView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.folderView_NodeMouseDoubleClick);
			this.folderView.SelectionChanged += new System.EventHandler(this.folderView_SelectionChanged);
			this.folderView.Expanding += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.folderView_Expanding);
			this.folderView.DragDrop += new System.Windows.Forms.DragEventHandler(this.folderView_DragDrop);
			this.folderView.DragOver += new System.Windows.Forms.DragEventHandler(this.folderView_DragOver);
			this.folderView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folderView_KeyDown);
			// 
			// contextMenuNode
			// 
			this.contextMenuNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparatorNew,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripSeparatorShowInExplorer,
            this.showInExplorerToolStripMenuItem});
			this.contextMenuNode.Name = "contextMenuNode";
			resources.ApplyResources(this.contextMenuNode, "contextMenuNode");
			this.contextMenuNode.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuNode_Opening);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderToolStripMenuItem,
            this.toolStripSeparator4});
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
			this.newToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.newToolStripMenuItem_DropDownItemClicked);
			// 
			// folderToolStripMenuItem
			// 
			this.folderToolStripMenuItem.Image = global::EditorBase.Properties.Resources.folder;
			this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
			resources.ApplyResources(this.folderToolStripMenuItem, "folderToolStripMenuItem");
			this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// toolStripSeparatorNew
			// 
			this.toolStripSeparatorNew.Name = "toolStripSeparatorNew";
			resources.ApplyResources(this.toolStripSeparatorNew, "toolStripSeparatorNew");
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = global::EditorBase.Properties.Resources.cut;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = global::EditorBase.Properties.Resources.page_copy;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = global::EditorBase.Properties.Resources.page_paste;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Image = global::EditorBase.Properties.Resources.cross;
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// toolStripSeparatorShowInExplorer
			// 
			this.toolStripSeparatorShowInExplorer.Name = "toolStripSeparatorShowInExplorer";
			resources.ApplyResources(this.toolStripSeparatorShowInExplorer, "toolStripSeparatorShowInExplorer");
			// 
			// showInExplorerToolStripMenuItem
			// 
			this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
			resources.ApplyResources(this.showInExplorerToolStripMenuItem, "showInExplorerToolStripMenuItem");
			this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInExplorerToolStripMenuItem_Click);
			// 
			// nodeStateIcon
			// 
			this.nodeStateIcon.DataPropertyName = "Image";
			this.nodeStateIcon.LeftMargin = 1;
			this.nodeStateIcon.ParentColumn = null;
			this.nodeStateIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
			// 
			// nodeTextBoxName
			// 
			this.nodeTextBoxName.DataPropertyName = "Text";
			this.nodeTextBoxName.EditEnabled = true;
			this.nodeTextBoxName.IncrementalSearchEnabled = true;
			this.nodeTextBoxName.LeftMargin = 3;
			this.nodeTextBoxName.ParentColumn = null;
			// 
			// timerFlashItem
			// 
			this.timerFlashItem.Interval = 30;
			this.timerFlashItem.Tick += new System.EventHandler(this.timerFlashItem_Tick);
			// 
			// contextMenuDragMoveCopy
			// 
			this.contextMenuDragMoveCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyHereToolStripMenuItem,
            this.moveHereToolStripMenuItem,
            this.toolStripSeparator1,
            this.cancelToolStripMenuItem});
			this.contextMenuDragMoveCopy.Name = "contextMenuDragMoveCopy";
			resources.ApplyResources(this.contextMenuDragMoveCopy, "contextMenuDragMoveCopy");
			// 
			// copyHereToolStripMenuItem
			// 
			this.copyHereToolStripMenuItem.Name = "copyHereToolStripMenuItem";
			resources.ApplyResources(this.copyHereToolStripMenuItem, "copyHereToolStripMenuItem");
			this.copyHereToolStripMenuItem.Click += new System.EventHandler(this.copyHereToolStripMenuItem_Click);
			// 
			// moveHereToolStripMenuItem
			// 
			this.moveHereToolStripMenuItem.Name = "moveHereToolStripMenuItem";
			resources.ApplyResources(this.moveHereToolStripMenuItem, "moveHereToolStripMenuItem");
			this.moveHereToolStripMenuItem.Click += new System.EventHandler(this.moveHereToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// cancelToolStripMenuItem
			// 
			this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
			resources.ApplyResources(this.cancelToolStripMenuItem, "cancelToolStripMenuItem");
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.textBoxFilter);
			this.panelBottom.Controls.Add(this.labelFilter);
			resources.ApplyResources(this.panelBottom, "panelBottom");
			this.panelBottom.Name = "panelBottom";
			// 
			// textBoxFilter
			// 
			resources.ApplyResources(this.textBoxFilter, "textBoxFilter");
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
			// 
			// labelFilter
			// 
			resources.ApplyResources(this.labelFilter, "labelFilter");
			this.labelFilter.Name = "labelFilter";
			// 
			// ProjectFolderView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.folderView);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.toolStrip);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ProjectFolderView";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.contextMenuNode.ResumeLayout(false);
			this.contextMenuDragMoveCopy.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.panelBottom.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private Aga.Controls.Tree.TreeViewAdv folderView;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
		private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon;
		private System.Windows.Forms.Timer timerFlashItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuDragMoveCopy;
		private System.Windows.Forms.ToolStripMenuItem copyHereToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveHereToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuNode;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorNew;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorShowInExplorer;
		private System.Windows.Forms.ToolStripMenuItem showInExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonWorkDir;
		private System.Windows.Forms.ToolStripLabel toolStripLabelProjectName;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.Label labelFilter;
	}
}