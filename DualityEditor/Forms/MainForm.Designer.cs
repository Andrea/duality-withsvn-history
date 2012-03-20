﻿namespace DualityEditor.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin3 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
			WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin3 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient7 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient15 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin3 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient16 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient8 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient17 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient18 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient19 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient9 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient20 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient21 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.pluginWatcher = new System.IO.FileSystemWatcher();
			this.dataDirWatcher = new System.IO.FileSystemWatcher();
			this.sourceDirWatcher = new System.IO.FileSystemWatcher();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.actionSaveAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.actionOpenCode = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.actionRunApp = new System.Windows.Forms.ToolStripButton();
			this.actionDebugApp = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.actionRunSandbox = new System.Windows.Forms.ToolStripButton();
			this.actionPauseSandbox = new System.Windows.Forms.ToolStripButton();
			this.actionStopSandbox = new System.Windows.Forms.ToolStripButton();
			this.selectFormattingMethod = new System.Windows.Forms.ToolStripSplitButton();
			this.formatBinary = new System.Windows.Forms.ToolStripMenuItem();
			this.formatXml = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pluginWatcher)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataDirWatcher)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceDirWatcher)).BeginInit();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(162)))), ((int)(((byte)(162)))));
			this.dockPanel.Location = new System.Drawing.Point(0, 49);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.ShowDocumentIcon = true;
			this.dockPanel.Size = new System.Drawing.Size(916, 639);
			dockPanelGradient7.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient7.StartColor = System.Drawing.SystemColors.ControlLight;
			autoHideStripSkin3.DockStripGradient = dockPanelGradient7;
			tabGradient15.EndColor = System.Drawing.SystemColors.Control;
			tabGradient15.StartColor = System.Drawing.SystemColors.Control;
			tabGradient15.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			autoHideStripSkin3.TabGradient = tabGradient15;
			autoHideStripSkin3.TextFont = new System.Drawing.Font("Segoe UI", 9F);
			dockPanelSkin3.AutoHideStripSkin = autoHideStripSkin3;
			tabGradient16.EndColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient16.StartColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient16.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient3.ActiveTabGradient = tabGradient16;
			dockPanelGradient8.EndColor = System.Drawing.SystemColors.Control;
			dockPanelGradient8.StartColor = System.Drawing.SystemColors.Control;
			dockPaneStripGradient3.DockStripGradient = dockPanelGradient8;
			tabGradient17.EndColor = System.Drawing.SystemColors.ControlLight;
			tabGradient17.StartColor = System.Drawing.SystemColors.ControlLight;
			tabGradient17.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient3.InactiveTabGradient = tabGradient17;
			dockPaneStripSkin3.DocumentGradient = dockPaneStripGradient3;
			dockPaneStripSkin3.TextFont = new System.Drawing.Font("Segoe UI", 9F);
			tabGradient18.EndColor = System.Drawing.SystemColors.ActiveCaption;
			tabGradient18.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient18.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
			tabGradient18.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient3.ActiveCaptionGradient = tabGradient18;
			tabGradient19.EndColor = System.Drawing.SystemColors.Control;
			tabGradient19.StartColor = System.Drawing.SystemColors.Control;
			tabGradient19.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient3.ActiveTabGradient = tabGradient19;
			dockPanelGradient9.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient9.StartColor = System.Drawing.SystemColors.ControlLight;
			dockPaneStripToolWindowGradient3.DockStripGradient = dockPanelGradient9;
			tabGradient20.EndColor = System.Drawing.SystemColors.InactiveCaption;
			tabGradient20.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient20.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient20.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
			dockPaneStripToolWindowGradient3.InactiveCaptionGradient = tabGradient20;
			tabGradient21.EndColor = System.Drawing.Color.Transparent;
			tabGradient21.StartColor = System.Drawing.Color.Transparent;
			tabGradient21.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient3.InactiveTabGradient = tabGradient21;
			dockPaneStripSkin3.ToolWindowGradient = dockPaneStripToolWindowGradient3;
			dockPanelSkin3.DockPaneStripSkin = dockPaneStripSkin3;
			this.dockPanel.Skin = dockPanelSkin3;
			this.dockPanel.TabIndex = 0;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(916, 24);
			this.mainMenuStrip.TabIndex = 2;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// TopToolStripPanel
			// 
			this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// RightToolStripPanel
			// 
			this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// LeftToolStripPanel
			// 
			this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.Size = new System.Drawing.Size(916, 639);
			// 
			// pluginWatcher
			// 
			this.pluginWatcher.EnableRaisingEvents = true;
			this.pluginWatcher.Filter = "*.dll";
			this.pluginWatcher.IncludeSubdirectories = true;
			this.pluginWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.CreationTime)));
			this.pluginWatcher.Path = "Plugins";
			this.pluginWatcher.SynchronizingObject = this;
			this.pluginWatcher.Changed += new System.IO.FileSystemEventHandler(this.corePluginWatcher_Changed);
			this.pluginWatcher.Created += new System.IO.FileSystemEventHandler(this.corePluginWatcher_Changed);
			// 
			// dataDirWatcher
			// 
			this.dataDirWatcher.EnableRaisingEvents = true;
			this.dataDirWatcher.IncludeSubdirectories = true;
			this.dataDirWatcher.SynchronizingObject = this;
			// 
			// sourceDirWatcher
			// 
			this.sourceDirWatcher.EnableRaisingEvents = true;
			this.sourceDirWatcher.IncludeSubdirectories = true;
			this.sourceDirWatcher.SynchronizingObject = this;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionSaveAll,
            this.toolStripSeparator1,
            this.actionOpenCode,
            this.toolStripSeparator2,
            this.actionRunApp,
            this.actionDebugApp,
            this.toolStripSeparator3,
            this.actionRunSandbox,
            this.actionPauseSandbox,
            this.actionStopSandbox,
            this.selectFormattingMethod});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(916, 25);
			this.mainToolStrip.TabIndex = 4;
			// 
			// actionSaveAll
			// 
			this.actionSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionSaveAll.Image = global::DualityEditor.Properties.Resources.disk_multiple;
			this.actionSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionSaveAll.Name = "actionSaveAll";
			this.actionSaveAll.Size = new System.Drawing.Size(23, 22);
			this.actionSaveAll.Text = "Save All Project Data";
			this.actionSaveAll.Click += new System.EventHandler(this.actionSaveAll_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// actionOpenCode
			// 
			this.actionOpenCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionOpenCode.Image = global::DualityEditor.Properties.Resources.page_white_csharp;
			this.actionOpenCode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionOpenCode.Name = "actionOpenCode";
			this.actionOpenCode.Size = new System.Drawing.Size(23, 22);
			this.actionOpenCode.Text = "Open Project Sourcecode";
			this.actionOpenCode.Click += new System.EventHandler(this.actionOpenCode_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// actionRunApp
			// 
			this.actionRunApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionRunApp.Image = global::DualityEditor.Properties.Resources.application_go;
			this.actionRunApp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionRunApp.Name = "actionRunApp";
			this.actionRunApp.Size = new System.Drawing.Size(23, 22);
			this.actionRunApp.Text = "Run Application";
			this.actionRunApp.Click += new System.EventHandler(this.actionRunApp_Click);
			// 
			// actionDebugApp
			// 
			this.actionDebugApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionDebugApp.Image = global::DualityEditor.Properties.Resources.application_bug;
			this.actionDebugApp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionDebugApp.Name = "actionDebugApp";
			this.actionDebugApp.Size = new System.Drawing.Size(23, 22);
			this.actionDebugApp.Text = "Debug Application";
			this.actionDebugApp.Click += new System.EventHandler(this.actionDebugApp_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// actionRunSandbox
			// 
			this.actionRunSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionRunSandbox.Image = global::DualityEditor.Properties.Resources.control_play_blue;
			this.actionRunSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionRunSandbox.Name = "actionRunSandbox";
			this.actionRunSandbox.Size = new System.Drawing.Size(23, 22);
			this.actionRunSandbox.Text = "Enter Sandbox";
			this.actionRunSandbox.Click += new System.EventHandler(this.actionRunSandbox_Click);
			// 
			// actionPauseSandbox
			// 
			this.actionPauseSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionPauseSandbox.Image = global::DualityEditor.Properties.Resources.control_pause_blue;
			this.actionPauseSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionPauseSandbox.Name = "actionPauseSandbox";
			this.actionPauseSandbox.Size = new System.Drawing.Size(23, 22);
			this.actionPauseSandbox.Text = "Pause Sandbox";
			this.actionPauseSandbox.Click += new System.EventHandler(this.actionPauseSandbox_Click);
			// 
			// actionStopSandbox
			// 
			this.actionStopSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.actionStopSandbox.Image = global::DualityEditor.Properties.Resources.control_stop_blue;
			this.actionStopSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionStopSandbox.Name = "actionStopSandbox";
			this.actionStopSandbox.Size = new System.Drawing.Size(23, 22);
			this.actionStopSandbox.Text = "Leave Sandbox";
			this.actionStopSandbox.Click += new System.EventHandler(this.actionStopSandbox_Click);
			// 
			// selectFormattingMethod
			// 
			this.selectFormattingMethod.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.selectFormattingMethod.AutoToolTip = false;
			this.selectFormattingMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatBinary,
            this.formatXml});
			this.selectFormattingMethod.Image = ((System.Drawing.Image)(resources.GetObject("selectFormattingMethod.Image")));
			this.selectFormattingMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectFormattingMethod.Name = "selectFormattingMethod";
			this.selectFormattingMethod.Size = new System.Drawing.Size(144, 22);
			this.selectFormattingMethod.Text = "Project Data Format";
			this.selectFormattingMethod.Click += new System.EventHandler(this.selectFormattingMethod_Click);
			// 
			// formatBinary
			// 
			this.formatBinary.Image = global::DualityEditor.Properties.Resources.page_gear;
			this.formatBinary.Name = "formatBinary";
			this.formatBinary.Size = new System.Drawing.Size(107, 22);
			this.formatBinary.Text = "Binary";
			this.formatBinary.Click += new System.EventHandler(this.formatBinary_Click);
			// 
			// formatXml
			// 
			this.formatXml.Image = global::DualityEditor.Properties.Resources.page_code;
			this.formatXml.Name = "formatXml";
			this.formatXml.Size = new System.Drawing.Size(107, 22);
			this.formatXml.Text = "Xml";
			this.formatXml.Click += new System.EventHandler(this.formatXml_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(162)))), ((int)(((byte)(162)))));
			this.ClientSize = new System.Drawing.Size(916, 688);
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.Text = "Dualitor";
			((System.ComponentModel.ISupportInitialize)(this.pluginWatcher)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataDirWatcher)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceDirWatcher)).EndInit();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.IO.FileSystemWatcher pluginWatcher;
		private System.IO.FileSystemWatcher dataDirWatcher;
		private System.IO.FileSystemWatcher sourceDirWatcher;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripButton actionRunApp;
		private System.Windows.Forms.ToolStripButton actionDebugApp;
		private System.Windows.Forms.ToolStripButton actionSaveAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton actionOpenCode;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton actionRunSandbox;
		private System.Windows.Forms.ToolStripButton actionPauseSandbox;
		private System.Windows.Forms.ToolStripButton actionStopSandbox;
		private System.Windows.Forms.ToolStripSplitButton selectFormattingMethod;
		private System.Windows.Forms.ToolStripMenuItem formatBinary;
		private System.Windows.Forms.ToolStripMenuItem formatXml;
	}
}

