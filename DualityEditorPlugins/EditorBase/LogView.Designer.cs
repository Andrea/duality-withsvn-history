namespace EditorBase
{
	partial class LogView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
			this.nodeStateIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
			this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.textLog = new System.Windows.Forms.RichTextBox();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.labelVisibility = new System.Windows.Forms.ToolStripLabel();
			this.buttonErrors = new System.Windows.Forms.ToolStripButton();
			this.buttonWarnings = new System.Windows.Forms.ToolStripButton();
			this.buttonMessages = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonCore = new System.Windows.Forms.ToolStripButton();
			this.buttonEditor = new System.Windows.Forms.ToolStripButton();
			this.buttonGame = new System.Windows.Forms.ToolStripButton();
			this.actionClear = new System.Windows.Forms.ToolStripSplitButton();
			this.checkAutoClear = new System.Windows.Forms.ToolStripMenuItem();
			this.updateTimer = new System.Windows.Forms.Timer(this.components);
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
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
			// textLog
			// 
			this.textLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.textLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textLog.ForeColor = System.Drawing.Color.Gray;
			this.textLog.Location = new System.Drawing.Point(0, 24);
			this.textLog.Margin = new System.Windows.Forms.Padding(0);
			this.textLog.Name = "textLog";
			this.textLog.ReadOnly = true;
			this.textLog.Size = new System.Drawing.Size(515, 328);
			this.textLog.TabIndex = 0;
			this.textLog.Text = "";
			this.textLog.WordWrap = false;
			// 
			// toolStrip
			// 
			this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(162)))), ((int)(((byte)(162)))));
			this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelVisibility,
            this.buttonErrors,
            this.buttonWarnings,
            this.buttonMessages,
            this.toolStripSeparator1,
            this.buttonCore,
            this.buttonEditor,
            this.buttonGame,
            this.actionClear});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(515, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip";
			// 
			// labelVisibility
			// 
			this.labelVisibility.Name = "labelVisibility";
			this.labelVisibility.Size = new System.Drawing.Size(39, 22);
			this.labelVisibility.Text = "Show:";
			// 
			// buttonErrors
			// 
			this.buttonErrors.Checked = true;
			this.buttonErrors.CheckOnClick = true;
			this.buttonErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonErrors.Image = global::EditorBase.Properties.Resources.log_error;
			this.buttonErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonErrors.Name = "buttonErrors";
			this.buttonErrors.Size = new System.Drawing.Size(23, 22);
			this.buttonErrors.Text = "Errors";
			this.buttonErrors.CheckedChanged += new System.EventHandler(this.buttonErrors_CheckedChanged);
			// 
			// buttonWarnings
			// 
			this.buttonWarnings.Checked = true;
			this.buttonWarnings.CheckOnClick = true;
			this.buttonWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonWarnings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonWarnings.Image = global::EditorBase.Properties.Resources.log_warning;
			this.buttonWarnings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonWarnings.Name = "buttonWarnings";
			this.buttonWarnings.Size = new System.Drawing.Size(23, 22);
			this.buttonWarnings.Text = "Warnings";
			this.buttonWarnings.CheckedChanged += new System.EventHandler(this.buttonWarnings_CheckedChanged);
			// 
			// buttonMessages
			// 
			this.buttonMessages.Checked = true;
			this.buttonMessages.CheckOnClick = true;
			this.buttonMessages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonMessages.Image = global::EditorBase.Properties.Resources.log_message;
			this.buttonMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonMessages.Name = "buttonMessages";
			this.buttonMessages.Size = new System.Drawing.Size(23, 22);
			this.buttonMessages.Text = "Messages";
			this.buttonMessages.CheckedChanged += new System.EventHandler(this.buttonMessages_CheckedChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// buttonCore
			// 
			this.buttonCore.Checked = true;
			this.buttonCore.CheckOnClick = true;
			this.buttonCore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonCore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonCore.Image = global::EditorBase.Properties.Resources.log_core;
			this.buttonCore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonCore.Name = "buttonCore";
			this.buttonCore.Size = new System.Drawing.Size(23, 22);
			this.buttonCore.Text = "Core";
			this.buttonCore.CheckedChanged += new System.EventHandler(this.buttonCore_CheckedChanged);
			// 
			// buttonEditor
			// 
			this.buttonEditor.CheckOnClick = true;
			this.buttonEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonEditor.Image = global::EditorBase.Properties.Resources.log_editor;
			this.buttonEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonEditor.Name = "buttonEditor";
			this.buttonEditor.Size = new System.Drawing.Size(23, 22);
			this.buttonEditor.Text = "Editor";
			this.buttonEditor.CheckedChanged += new System.EventHandler(this.buttonEditor_CheckedChanged);
			// 
			// buttonGame
			// 
			this.buttonGame.Checked = true;
			this.buttonGame.CheckOnClick = true;
			this.buttonGame.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonGame.Image = global::EditorBase.Properties.Resources.log_game;
			this.buttonGame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonGame.Name = "buttonGame";
			this.buttonGame.Size = new System.Drawing.Size(23, 22);
			this.buttonGame.Text = "Game";
			this.buttonGame.CheckedChanged += new System.EventHandler(this.buttonGame_CheckedChanged);
			// 
			// actionClear
			// 
			this.actionClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.actionClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionClear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAutoClear});
			this.actionClear.Image = global::EditorBase.Properties.Resources.cross;
			this.actionClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionClear.Name = "actionClear";
			this.actionClear.Size = new System.Drawing.Size(32, 22);
			this.actionClear.Text = "Clear Logs";
			this.actionClear.ButtonClick += new System.EventHandler(this.actionClear_ButtonClick);
			// 
			// checkAutoClear
			// 
			this.checkAutoClear.Checked = true;
			this.checkAutoClear.CheckOnClick = true;
			this.checkAutoClear.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAutoClear.Name = "checkAutoClear";
			this.checkAutoClear.Size = new System.Drawing.Size(174, 22);
			this.checkAutoClear.Text = "Auto-Clear on Play";
			// 
			// updateTimer
			// 
			this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
			// 
			// LogView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(515, 352);
			this.Controls.Add(this.textLog);
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LogView";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
			this.Text = "Log View";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
		private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon;
		private System.Windows.Forms.RichTextBox textLog;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton buttonCore;
		private System.Windows.Forms.ToolStripButton buttonEditor;
		private System.Windows.Forms.ToolStripButton buttonGame;
		private System.Windows.Forms.ToolStripButton buttonMessages;
		private System.Windows.Forms.ToolStripButton buttonErrors;
		private System.Windows.Forms.ToolStripButton buttonWarnings;
		private System.Windows.Forms.ToolStripLabel labelVisibility;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSplitButton actionClear;
		private System.Windows.Forms.ToolStripMenuItem checkAutoClear;
		private System.Windows.Forms.Timer updateTimer;
	}
}