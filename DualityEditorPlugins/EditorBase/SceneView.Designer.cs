namespace EditorBase
{
	partial class SceneView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonSelectSceneRes = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabelSceneName = new System.Windows.Forms.ToolStripLabel();
			this.toolStripButtonSaveScene = new System.Windows.Forms.ToolStripButton();
			this.objectView = new Aga.Controls.Tree.TreeViewAdv();
			this.contextMenuNode = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newGameObjectSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparatorNew = new System.Windows.Forms.ToolStripSeparator();
			this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripButtonSelectSceneRes,
            this.toolStripLabelSceneName,
            this.toolStripButtonSaveScene});
			resources.ApplyResources(this.toolStrip, "toolStrip");
			this.toolStrip.Name = "toolStrip";
			// 
			// toolStripButtonSelectSceneRes
			// 
			this.toolStripButtonSelectSceneRes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSelectSceneRes.Image = global::EditorBase.Properties.Resources.GotoScene;
			resources.ApplyResources(this.toolStripButtonSelectSceneRes, "toolStripButtonSelectSceneRes");
			this.toolStripButtonSelectSceneRes.Name = "toolStripButtonSelectSceneRes";
			this.toolStripButtonSelectSceneRes.Click += new System.EventHandler(this.toolStripButtonSelectSceneRes_Click);
			// 
			// toolStripLabelSceneName
			// 
			this.toolStripLabelSceneName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripLabelSceneName.Name = "toolStripLabelSceneName";
			resources.ApplyResources(this.toolStripLabelSceneName, "toolStripLabelSceneName");
			// 
			// toolStripButtonSaveScene
			// 
			this.toolStripButtonSaveScene.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSaveScene.Image = global::EditorBase.Properties.Resources.disk;
			resources.ApplyResources(this.toolStripButtonSaveScene, "toolStripButtonSaveScene");
			this.toolStripButtonSaveScene.Name = "toolStripButtonSaveScene";
			this.toolStripButtonSaveScene.Click += new System.EventHandler(this.toolStripButtonSaveScene_Click);
			// 
			// objectView
			// 
			this.objectView.AllowDrop = true;
			this.objectView.BackColor = System.Drawing.SystemColors.Window;
			this.objectView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objectView.ContextMenuStrip = this.contextMenuNode;
			this.objectView.DefaultToolTipProvider = null;
			this.objectView.DisplayDraggingNodes = true;
			resources.ApplyResources(this.objectView, "objectView");
			this.objectView.DragDropMarkColor = System.Drawing.Color.Black;
			this.objectView.LineColor = System.Drawing.SystemColors.ControlDark;
			this.objectView.Model = null;
			this.objectView.Name = "objectView";
			this.objectView.NodeControls.Add(this.nodeStateIcon);
			this.objectView.NodeControls.Add(this.nodeTextBoxName);
			this.objectView.SelectedNode = null;
			this.objectView.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
			this.objectView.ShowNodeToolTips = true;
			this.objectView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.objectView_ItemDrag);
			this.objectView.SelectionChanged += new System.EventHandler(this.objectView_SelectionChanged);
			this.objectView.DragDrop += new System.Windows.Forms.DragEventHandler(this.objectView_DragDrop);
			this.objectView.DragOver += new System.Windows.Forms.DragEventHandler(this.objectView_DragOver);
			this.objectView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectView_KeyDown);
			// 
			// contextMenuNode
			// 
			this.contextMenuNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparatorNew,
            this.cloneToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
			this.contextMenuNode.Name = "contextMenuNode";
			resources.ApplyResources(this.contextMenuNode, "contextMenuNode");
			this.contextMenuNode.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuNode_Opening);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameObjectToolStripMenuItem,
            this.newGameObjectSeparator});
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
			this.newToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.newToolStripMenuItem_DropDownItemClicked);
			// 
			// gameObjectToolStripMenuItem
			// 
			this.gameObjectToolStripMenuItem.Name = "gameObjectToolStripMenuItem";
			resources.ApplyResources(this.gameObjectToolStripMenuItem, "gameObjectToolStripMenuItem");
			this.gameObjectToolStripMenuItem.Click += new System.EventHandler(this.gameObjectToolStripMenuItem_Click);
			// 
			// newGameObjectSeparator
			// 
			this.newGameObjectSeparator.Name = "newGameObjectSeparator";
			resources.ApplyResources(this.newGameObjectSeparator, "newGameObjectSeparator");
			// 
			// toolStripSeparatorNew
			// 
			this.toolStripSeparatorNew.Name = "toolStripSeparatorNew";
			resources.ApplyResources(this.toolStripSeparatorNew, "toolStripSeparatorNew");
			// 
			// cloneToolStripMenuItem
			// 
			this.cloneToolStripMenuItem.Image = global::EditorBase.Properties.Resources.page_copy;
			this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
			resources.ApplyResources(this.cloneToolStripMenuItem, "cloneToolStripMenuItem");
			this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
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
			// SceneView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.objectView);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.toolStrip);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SceneView";
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
		private Aga.Controls.Tree.TreeViewAdv objectView;
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorNew;
		private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonSelectSceneRes;
		private System.Windows.Forms.ToolStripLabel toolStripLabelSceneName;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.Label labelFilter;
		private System.Windows.Forms.ToolStripMenuItem gameObjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator newGameObjectSeparator;
		private System.Windows.Forms.ToolStripButton toolStripButtonSaveScene;
	}
}