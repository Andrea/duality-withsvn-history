namespace DualityEditor.Forms
{
	partial class NewProjectDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectDialog));
			this.folderView = new Aga.Controls.Tree.TreeViewAdv();
			this.templateView = new System.Windows.Forms.ListView();
			this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitFolderTemplate = new System.Windows.Forms.SplitContainer();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.labelProjectName = new System.Windows.Forms.Label();
			this.labelProjectFolder = new System.Windows.Forms.Label();
			this.buttonBrowseFolder = new System.Windows.Forms.Button();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.labelLowerArea = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelDialogDesc = new System.Windows.Forms.Label();
			this.labelProjectTemplate = new System.Windows.Forms.Label();
			this.textBoxTemplate = new System.Windows.Forms.TextBox();
			this.buttonBrowseTemplate = new System.Windows.Forms.Button();
			this.folderViewControlIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
			this.folderViewControlName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitFolderTemplate)).BeginInit();
			this.splitFolderTemplate.Panel1.SuspendLayout();
			this.splitFolderTemplate.Panel2.SuspendLayout();
			this.splitFolderTemplate.SuspendLayout();
			this.SuspendLayout();
			// 
			// folderView
			// 
			this.folderView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.folderView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.folderView.DefaultToolTipProvider = null;
			this.folderView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.folderView.DragDropMarkColor = System.Drawing.Color.Black;
			this.folderView.FullRowSelect = true;
			this.folderView.FullRowSelectActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.folderView.FullRowSelectInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.folderView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
			this.folderView.LoadOnDemand = true;
			this.folderView.Location = new System.Drawing.Point(0, 0);
			this.folderView.Model = null;
			this.folderView.Name = "folderView";
			this.folderView.NodeControls.Add(this.folderViewControlIcon);
			this.folderView.NodeControls.Add(this.folderViewControlName);
			this.folderView.NodeFilter = null;
			this.folderView.SelectedNode = null;
			this.folderView.Size = new System.Drawing.Size(158, 201);
			this.folderView.TabIndex = 0;
			this.folderView.Text = "Installed Templates";
			this.folderView.SelectionChanged += new System.EventHandler(this.folderView_SelectionChanged);
			// 
			// templateView
			// 
			this.templateView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.templateView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.templateView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDesc});
			this.templateView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.templateView.FullRowSelect = true;
			this.templateView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.templateView.Location = new System.Drawing.Point(0, 0);
			this.templateView.MultiSelect = false;
			this.templateView.Name = "templateView";
			this.templateView.ShowItemToolTips = true;
			this.templateView.Size = new System.Drawing.Size(324, 201);
			this.templateView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.templateView.TabIndex = 1;
			this.templateView.TileSize = new System.Drawing.Size(100, 34);
			this.templateView.UseCompatibleStateImageBehavior = false;
			this.templateView.View = System.Windows.Forms.View.Tile;
			this.templateView.Resize += new System.EventHandler(this.templateView_Resize);
			// 
			// columnName
			// 
			this.columnName.Text = "Name";
			// 
			// columnDesc
			// 
			this.columnDesc.Text = "Description";
			// 
			// splitFolderTemplate
			// 
			this.splitFolderTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitFolderTemplate.Location = new System.Drawing.Point(-1, -1);
			this.splitFolderTemplate.Name = "splitFolderTemplate";
			// 
			// splitFolderTemplate.Panel1
			// 
			this.splitFolderTemplate.Panel1.Controls.Add(this.folderView);
			// 
			// splitFolderTemplate.Panel2
			// 
			this.splitFolderTemplate.Panel2.Controls.Add(this.templateView);
			this.splitFolderTemplate.Size = new System.Drawing.Size(486, 201);
			this.splitFolderTemplate.SplitterDistance = 158;
			this.splitFolderTemplate.TabIndex = 2;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(398, 332);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.Enabled = false;
			this.buttonOk.Location = new System.Drawing.Point(317, 332);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// labelProjectName
			// 
			this.labelProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelProjectName.AutoSize = true;
			this.labelProjectName.Location = new System.Drawing.Point(12, 273);
			this.labelProjectName.Name = "labelProjectName";
			this.labelProjectName.Size = new System.Drawing.Size(71, 13);
			this.labelProjectName.TabIndex = 5;
			this.labelProjectName.Text = "Project Name";
			// 
			// labelProjectFolder
			// 
			this.labelProjectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelProjectFolder.AutoSize = true;
			this.labelProjectFolder.Location = new System.Drawing.Point(12, 299);
			this.labelProjectFolder.Name = "labelProjectFolder";
			this.labelProjectFolder.Size = new System.Drawing.Size(72, 13);
			this.labelProjectFolder.TabIndex = 6;
			this.labelProjectFolder.Text = "Project Folder";
			// 
			// buttonBrowseFolder
			// 
			this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseFolder.Location = new System.Drawing.Point(402, 293);
			this.buttonBrowseFolder.Name = "buttonBrowseFolder";
			this.buttonBrowseFolder.Size = new System.Drawing.Size(71, 23);
			this.buttonBrowseFolder.TabIndex = 7;
			this.buttonBrowseFolder.Text = "Browse...";
			this.buttonBrowseFolder.UseVisualStyleBackColor = true;
			this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFolder.Location = new System.Drawing.Point(105, 296);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(291, 20);
			this.textBoxFolder.TabIndex = 8;
			this.textBoxFolder.Text = "<Select Folder>";
			this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxFolder_TextChanged);
			// 
			// labelLowerArea
			// 
			this.labelLowerArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelLowerArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.labelLowerArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelLowerArea.Location = new System.Drawing.Point(-1, 324);
			this.labelLowerArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.labelLowerArea.Name = "labelLowerArea";
			this.labelLowerArea.Size = new System.Drawing.Size(486, 39);
			this.labelLowerArea.TabIndex = 9;
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(105, 270);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(291, 20);
			this.textBoxName.TabIndex = 10;
			this.textBoxName.Text = "<Enter Name here>";
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// labelDialogDesc
			// 
			this.labelDialogDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDialogDesc.Location = new System.Drawing.Point(12, 208);
			this.labelDialogDesc.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.labelDialogDesc.Name = "labelDialogDesc";
			this.labelDialogDesc.Size = new System.Drawing.Size(460, 30);
			this.labelDialogDesc.TabIndex = 11;
			this.labelDialogDesc.Text = "This dialog will create a new Duality project. Select a suitable project template" +
    " and specify a destination where the project will be created.";
			// 
			// labelProjectTemplate
			// 
			this.labelProjectTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelProjectTemplate.AutoSize = true;
			this.labelProjectTemplate.Location = new System.Drawing.Point(12, 247);
			this.labelProjectTemplate.Name = "labelProjectTemplate";
			this.labelProjectTemplate.Size = new System.Drawing.Size(87, 13);
			this.labelProjectTemplate.TabIndex = 12;
			this.labelProjectTemplate.Text = "Project Template";
			// 
			// textBoxTemplate
			// 
			this.textBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTemplate.Location = new System.Drawing.Point(105, 244);
			this.textBoxTemplate.Name = "textBoxTemplate";
			this.textBoxTemplate.Size = new System.Drawing.Size(291, 20);
			this.textBoxTemplate.TabIndex = 13;
			this.textBoxTemplate.Text = "<Select Template>";
			this.textBoxTemplate.TextChanged += new System.EventHandler(this.textBoxTemplate_TextChanged);
			// 
			// buttonBrowseTemplate
			// 
			this.buttonBrowseTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseTemplate.Location = new System.Drawing.Point(402, 242);
			this.buttonBrowseTemplate.Name = "buttonBrowseTemplate";
			this.buttonBrowseTemplate.Size = new System.Drawing.Size(71, 23);
			this.buttonBrowseTemplate.TabIndex = 14;
			this.buttonBrowseTemplate.Text = "Browse...";
			this.buttonBrowseTemplate.UseVisualStyleBackColor = true;
			this.buttonBrowseTemplate.Click += new System.EventHandler(this.buttonBrowseTemplate_Click);
			// 
			// folderViewControlIcon
			// 
			this.folderViewControlIcon.DataPropertyName = "Icon";
			this.folderViewControlIcon.LeftMargin = 1;
			this.folderViewControlIcon.ParentColumn = null;
			this.folderViewControlIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
			// 
			// folderViewControlName
			// 
			this.folderViewControlName.DataPropertyName = "Name";
			this.folderViewControlName.IncrementalSearchEnabled = true;
			this.folderViewControlName.LeftMargin = 3;
			this.folderViewControlName.ParentColumn = null;
			// 
			// NewProjectDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(484, 362);
			this.Controls.Add(this.buttonBrowseTemplate);
			this.Controls.Add(this.textBoxTemplate);
			this.Controls.Add(this.labelProjectTemplate);
			this.Controls.Add(this.labelDialogDesc);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.buttonBrowseFolder);
			this.Controls.Add(this.labelProjectFolder);
			this.Controls.Add(this.labelProjectName);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.splitFolderTemplate);
			this.Controls.Add(this.labelLowerArea);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 400);
			this.Name = "NewProjectDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Project...";
			this.splitFolderTemplate.Panel1.ResumeLayout(false);
			this.splitFolderTemplate.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitFolderTemplate)).EndInit();
			this.splitFolderTemplate.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Aga.Controls.Tree.TreeViewAdv folderView;
		private System.Windows.Forms.ListView templateView;
		private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.ColumnHeader columnDesc;
		private System.Windows.Forms.SplitContainer splitFolderTemplate;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label labelProjectName;
		private System.Windows.Forms.Label labelProjectFolder;
		private System.Windows.Forms.Button buttonBrowseFolder;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Label labelLowerArea;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelDialogDesc;
		private System.Windows.Forms.Label labelProjectTemplate;
		private System.Windows.Forms.TextBox textBoxTemplate;
		private System.Windows.Forms.Button buttonBrowseTemplate;
		private Aga.Controls.Tree.NodeControls.NodeStateIcon folderViewControlIcon;
		private Aga.Controls.Tree.NodeControls.NodeTextBox folderViewControlName;
	}
}