﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor.EditorRes;

using WeifenLuo.WinFormsUI.Docking;

namespace DualityEditor.Forms
{
	public partial class MainForm : Form, IHelpProvider
	{
		private	bool				nonUserClosing	= false;
		private	ToolStripMenuItem	activeMenu		= null;
		private	Dictionary<string,ToolStripItem>	menuRegistry	= new Dictionary<string,ToolStripItem>();

		// Hardcoded main menu items
		private ToolStripMenuItem	menuRunSandboxPlay		= null;
		private ToolStripMenuItem	menuRunSandboxPause		= null;
		private ToolStripMenuItem	menuRunSandboxStop		= null;
		private ToolStripMenuItem	menuRunSandboxPlayPause	= null;
		private ToolStripMenuItem	menuRunSandboxStep		= null;
		private ToolStripMenuItem	menuRunSandboxFaster	= null;
		private ToolStripMenuItem	menuRunSandboxSlower	= null;


		public DockPanel MainDockPanel
		{
			get { return this.dockPanel; }
		}



		internal MainForm()
		{
			this.InitializeComponent();
			this.ApplyDockPanelSkin();
			this.mainMenuStrip.Renderer = new Controls.ToolStrip.DualitorToolStripProfessionalRenderer();
			this.mainToolStrip.Renderer = new Controls.ToolStrip.DualitorToolStripProfessionalRenderer();

			this.actionDebugApp.Enabled = EditorHelper.IsJITDebuggerAvailable();

			this.splitButtonBackupSettings.DropDown.Closing += this.splitButtonBackupSettings_Closing;
			this.menuAutosave.DropDown.Closing += this.menuAutosave_Closing;

			this.InitMenus();
		}
		private void ApplyDockPanelSkin()
		{
			Color bgColor = Color.FromArgb(255, 162, 162, 162);
			Color fgColor = Color.FromArgb(255, 196, 196, 196);
			Color inactiveTab = Color.FromArgb(255, 192, 192, 192);
			Color inactiveTab2 = Color.FromArgb(255, 224, 224, 224);
			Color activeTab = Color.FromArgb(255, 224, 224, 224);
			Color activeTab2 = Color.FromArgb(255, 242, 242, 242);

			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = bgColor;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = bgColor;

			this.dockPanel.Skin.AutoHideStripSkin.DockStripGradient.StartColor = bgColor;
			this.dockPanel.Skin.AutoHideStripSkin.DockStripGradient.EndColor = bgColor;
			this.dockPanel.Skin.AutoHideStripSkin.TabGradient.StartColor = fgColor;
			this.dockPanel.Skin.AutoHideStripSkin.TabGradient.EndColor = fgColor;

			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = activeTab2;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = activeTab;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = inactiveTab2;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = inactiveTab;

			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.StartColor = fgColor;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.EndColor = fgColor;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = bgColor;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.EndColor = bgColor;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;

			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.StartColor = bgColor;
			this.dockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient.EndColor = bgColor;

			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = fgColor;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = fgColor;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Color.Black;

			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = activeTab2;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = activeTab;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.Black;
		}
		
		public void CloseNonUser()
		{
			// Because FormClosingEventArgs.CloseReason is UserClosing on this.Close()
			this.nonUserClosing = true;
			this.Close();
			this.nonUserClosing = false;
		}
		public void InitMenus()
		{
			ToolStripMenuItem fileItem =	this.RequestMenu(GeneralRes.MenuName_File);
			ToolStripMenuItem viewItem =	this.RequestMenu(GeneralRes.MenuName_View);
			ToolStripMenuItem newProjectItem =	this.RequestMenu(GeneralRes.MenuName_File, GeneralRes.MenuItemName_NewProject);
												this.RequestSeparator(GeneralRes.MenuName_File, "SaveSeparator");
			ToolStripMenuItem saveAllItem =		this.RequestMenu(GeneralRes.MenuName_File, this.actionSaveAll.Text);
												this.RequestSeparator(GeneralRes.MenuName_File, "CodeSeparator");
			ToolStripMenuItem openCodeItem =	this.RequestMenu(GeneralRes.MenuName_File, this.actionOpenCode.Text);
												this.RequestSeparator(GeneralRes.MenuName_File, "EndSeparator");
			ToolStripMenuItem quitItem =		this.RequestMenu(GeneralRes.MenuName_File, GeneralRes.MenuItemName_Quit);
			ToolStripMenuItem runItem =		this.RequestMenu(GeneralRes.MenuName_Run);
			ToolStripMenuItem runGameItem =		this.RequestMenu(GeneralRes.MenuName_Run, this.actionRunApp.Text);
			ToolStripMenuItem debugGameItem	=	this.RequestMenu(GeneralRes.MenuName_Run, this.actionDebugApp.Text);
												this.RequestSeparator(GeneralRes.MenuName_Run, "SandboxSeparator");
			this.menuRunSandboxPlay	=			this.RequestMenu(GeneralRes.MenuName_Run, this.actionRunSandbox.Text);
			this.menuRunSandboxStep =			this.RequestMenu(GeneralRes.MenuName_Run, this.actionStepSandbox.Text);
			this.menuRunSandboxPause =			this.RequestMenu(GeneralRes.MenuName_Run, this.actionPauseSandbox.Text);
			this.menuRunSandboxStop =			this.RequestMenu(GeneralRes.MenuName_Run, this.actionStopSandbox.Text);
												this.RequestSeparator(GeneralRes.MenuName_Run, "SandboxDebugSeparator");
			this.menuRunSandboxSlower =			this.RequestMenu(GeneralRes.MenuName_Run, GeneralRes.MenuItemName_SandboxSlower);
			this.menuRunSandboxFaster =			this.RequestMenu(GeneralRes.MenuName_Run, GeneralRes.MenuItemName_SandboxFaster);
			ToolStripMenuItem helpItem =	this.RequestMenu(GeneralRes.MenuName_Help);
			ToolStripMenuItem aboutItem =		this.RequestMenu(GeneralRes.MenuName_Help, GeneralRes.MenuItemName_About);

			// ---------- Help ----------
			helpItem.Alignment = ToolStripItemAlignment.Right;
			aboutItem.Click += this.aboutItem_Click;

			// ---------- File ----------
			newProjectItem.Image = EditorRes.GeneralRes.ImageAppCreate;
			newProjectItem.Click += this.newProjectItem_Click;
			newProjectItem.Tag = HelpInfo.FromText(newProjectItem.Text, GeneralRes.MenuItemInfo_NewProject);

			saveAllItem.Image = this.actionSaveAll.Image;
			saveAllItem.ShortcutKeys = Keys.Control | Keys.S;
			saveAllItem.Click += this.actionSaveAll_Click;
			saveAllItem.Tag = HelpInfo.FromText(saveAllItem.Text, GeneralRes.MenuItemInfo_SaveAll);

			openCodeItem.Image = this.actionOpenCode.Image;
			openCodeItem.Click += this.actionOpenCode_Click;
			openCodeItem.Tag = HelpInfo.FromText(openCodeItem.Text, GeneralRes.MenuItemInfo_OpenProjectSource);

			runGameItem.Image = this.actionRunApp.Image;
			runGameItem.Click += this.actionRunApp_Click;
			runGameItem.ShortcutKeys = Keys.Alt | Keys.F5;
			runGameItem.Tag = HelpInfo.FromText(runGameItem.Text, GeneralRes.MenuItemInfo_RunGame);

			debugGameItem.Image = this.actionDebugApp.Image;
			debugGameItem.Click += this.actionDebugApp_Click;
			debugGameItem.Enabled = this.actionDebugApp.Enabled;
			debugGameItem.ShortcutKeys = Keys.Alt | Keys.F6;
			debugGameItem.Tag = HelpInfo.FromText(debugGameItem.Text, GeneralRes.MenuItemInfo_DebugGame);

			quitItem.Click += this.quitItem_Click;
			quitItem.ShortcutKeys = Keys.Alt | Keys.F4;

			// ---------- Run ----------
			this.menuRunSandboxPlay.Image = this.actionRunSandbox.Image;
			this.menuRunSandboxPlay.Click += this.actionRunSandbox_Click;
			this.menuRunSandboxPlay.ShortcutKeys = Keys.F5;
			this.menuRunSandboxPlay.Tag = HelpInfo.FromText(this.menuRunSandboxPlay.Text, GeneralRes.MenuItemInfo_SandboxPlay);

			this.menuRunSandboxStep.Image = this.actionStepSandbox.Image;
			this.menuRunSandboxStep.Click += this.actionStepSandbox_Click;
			this.menuRunSandboxStep.ShortcutKeys = Keys.F6;
			this.menuRunSandboxStep.Tag = HelpInfo.FromText(this.menuRunSandboxStep.Text, GeneralRes.MenuItemInfo_SandboxStep);

			this.menuRunSandboxPause.Image = this.actionPauseSandbox.Image;
			this.menuRunSandboxPause.Click += this.actionPauseSandbox_Click;
			this.menuRunSandboxPause.ShortcutKeys = Keys.F7;
			this.menuRunSandboxPause.Tag = HelpInfo.FromText(this.menuRunSandboxPause.Text, GeneralRes.MenuItemInfo_SandboxPause);

			this.menuRunSandboxStop.Image = this.actionStopSandbox.Image;
			this.menuRunSandboxStop.Click += this.actionStopSandbox_Click;
			this.menuRunSandboxStop.ShortcutKeys = Keys.F8;
			this.menuRunSandboxStop.Tag = HelpInfo.FromText(this.menuRunSandboxStop.Text, GeneralRes.MenuItemInfo_SandboxStop);

			this.menuRunSandboxSlower.Click += this.menuRunSandboxSlower_Click;
			this.menuRunSandboxSlower.ShortcutKeys = Keys.F9;
			this.menuRunSandboxSlower.Tag = HelpInfo.FromText(this.menuRunSandboxSlower.Text, GeneralRes.MenuItemInfo_SandboxSlower);

			this.menuRunSandboxFaster.Click += this.menuRunSandboxFaster_Click;
			this.menuRunSandboxFaster.ShortcutKeys = Keys.F10;
			this.menuRunSandboxFaster.Tag = HelpInfo.FromText(this.menuRunSandboxFaster.Text, GeneralRes.MenuItemInfo_SandboxFaster);

			// Attach help data to toolstrip actions
			this.actionOpenCode.Tag = HelpInfo.FromText(this.actionOpenCode.Text, GeneralRes.MenuItemInfo_OpenProjectSource);
			this.actionSaveAll.Tag = HelpInfo.FromText(this.actionSaveAll.Text, GeneralRes.MenuItemInfo_SaveAll);
			this.actionRunApp.Tag = HelpInfo.FromText(this.actionRunApp.Text, GeneralRes.MenuItemInfo_RunGame);
			this.actionDebugApp.Tag = HelpInfo.FromText(this.actionDebugApp.Text, GeneralRes.MenuItemInfo_DebugGame);
			this.actionRunSandbox.Tag = HelpInfo.FromText(this.actionRunSandbox.Text, GeneralRes.MenuItemInfo_SandboxPlay);
			this.actionStepSandbox.Tag = HelpInfo.FromText(this.actionStepSandbox.Text, GeneralRes.MenuItemInfo_SandboxStep);
			this.actionPauseSandbox.Tag = HelpInfo.FromText(this.actionPauseSandbox.Text, GeneralRes.MenuItemInfo_SandboxPause);
			this.actionStopSandbox.Tag = HelpInfo.FromText(this.actionStopSandbox.Text, GeneralRes.MenuItemInfo_SandboxStop);
		}
		public void RequestSeparator(params string[] menuNames)
		{
			this.RequestMenu<ToolStripSeparator>(menuNames);
		}
		public ToolStripMenuItem RequestMenu(params string[] menuNames)
		{
			return this.RequestMenu<ToolStripMenuItem>(menuNames);
		}
		public T RequestMenu<T>(params string[] menuNames) where T : ToolStripItem, new()
		{
			if (menuNames == null || menuNames.Length < 1) throw new ArgumentException("You need to specify at least one menu name");

			string menuPath = PathHelper.Combine(menuNames).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);;
			string menuId = menuPath.ToUpper();

			int lastDirSeparator = menuPath.LastIndexOf(Path.DirectorySeparatorChar);
			string menuLastName;
			if (lastDirSeparator != -1)
				menuLastName = menuPath.Substring(lastDirSeparator + 1, menuPath.Length - (lastDirSeparator + 1));
			else
				menuLastName = menuPath;

			ToolStripItem item;
			if (!this.menuRegistry.TryGetValue(menuId, out item))
			{
				var parent = this.RequestMenuParent(menuPath);

				if (typeof(ToolStripSeparator).IsAssignableFrom(typeof(T)) &&
					parent.Count > 1 && parent[parent.Count - 1] is ToolStripSeparator)
				{
					// Reuse existing separator items
					item = parent[parent.Count - 1] as T;
				}
				else
				{
					// Create new item
					item = new T();
					item.Name = menuId;
					item.Text = menuLastName;

					// If its a separator, hide it by default because its not surrounded yet
					if (item is ToolStripSeparator)
						item.Visible = false;
					// Is there a separator that we could show?
					else if (parent.Count > 1 && parent[parent.Count - 1] is ToolStripSeparator)
						parent[parent.Count - 1].Visible = true;
					
					// Register new item
					parent.Add(item);
					this.menuRegistry[menuId] = item;

					// If its a main menu, register dropdown events
					if (item is ToolStripMenuItem && parent == this.mainMenuStrip.Items)
					{
						ToolStripMenuItem mainMenuItem = item as ToolStripMenuItem;
						mainMenuItem.DropDownOpened += mainMenuItem_DropDownOpened;
						mainMenuItem.DropDownClosed += mainMenuItem_DropDownClosed;
					}
				}
			}

			return item as T;
		}
		private ToolStripItemCollection RequestMenuParent(string menuPath)
		{
			int lastDirSeparator = menuPath.LastIndexOf(Path.DirectorySeparatorChar);
			if (lastDirSeparator != -1)
			{
				// Create parent menus
				string parentPath = menuPath.Substring(0, lastDirSeparator);
				ToolStripMenuItem parentItem = this.RequestMenu(parentPath);
				return parentItem.DropDownItems;
			}
			else
			{
				// No parents? Return base collection
				return this.mainMenuStrip.Items;
			}
		}
		private void UpdateToolbar()
		{
			this.actionRunSandbox.Enabled	= Sandbox.State != SandboxState.Playing;
			this.actionStepSandbox.Enabled	= Sandbox.State != SandboxState.Playing;
			this.actionStopSandbox.Enabled	= Sandbox.State != SandboxState.Inactive;
			this.actionPauseSandbox.Enabled	= Sandbox.State == SandboxState.Playing;

			this.menuRunSandboxPlay.Enabled = this.actionRunSandbox.Enabled;
			this.menuRunSandboxStep.Enabled = this.actionStepSandbox.Enabled;
			this.menuRunSandboxStop.Enabled = this.actionStopSandbox.Enabled;
			this.menuRunSandboxPause.Enabled = this.actionPauseSandbox.Enabled;
			this.menuRunSandboxSlower.Enabled = Sandbox.State != SandboxState.Inactive;
			this.menuRunSandboxFaster.Enabled = Sandbox.State != SandboxState.Inactive;

			if (Duality.Serialization.Formatter.DefaultMethod == Duality.Serialization.FormattingMethod.Xml)
			{
				this.selectFormattingMethod.Image = this.formatXml.Image;
				this.formatXml.Checked = true;
				this.formatBinary.Checked = false;
			}
			else
			{
				this.selectFormattingMethod.Image = this.formatBinary.Image;
				this.formatXml.Checked = false;
				this.formatBinary.Checked = true;
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.WindowState = FormWindowState.Maximized;
			this.UpdateToolbar();

			Sandbox.StateChanged += this.Sandbox_StateChanged;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Sandbox.StateChanged -= this.Sandbox_StateChanged;
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			e.Cancel = !DualityEditorApp.Terminate(!this.nonUserClosing && !DualityEditorApp.IsReloadingPlugins);
		}

		private void actionRunApp_Click(object sender, EventArgs e)
		{
			DualityEditorApp.SaveAllProjectData();
			System.Diagnostics.Process appProc = System.Diagnostics.Process.Start("DualityLauncher.exe", "editor");
			AppRunningDialog runningDialog = new AppRunningDialog(appProc);
			runningDialog.ShowDialog(this);
		}
		private void actionDebugApp_Click(object sender, EventArgs e)
		{
			DualityEditorApp.SaveAllProjectData();
			System.Diagnostics.Process appProc = System.Diagnostics.Process.Start("DualityLauncher.exe", "editor debug");
			AppRunningDialog runningDialog = new AppRunningDialog(appProc);
			runningDialog.ShowDialog(this);
		}
		private void actionSaveAll_Click(object sender, EventArgs e)
		{
			DualityEditorApp.SaveAllProjectData();
			System.Media.SystemSounds.Asterisk.Play();
		}
		private void actionOpenCode_Click(object sender, EventArgs e)
		{
			DualityEditorApp.UpdatePluginSourceCode();
			System.Diagnostics.Process.Start(EditorHelper.SourceCodeSolutionFile);
		}
		private void actionRunSandbox_Click(object sender, EventArgs e)
		{
			Sandbox.Play();
		}
		private void actionStepSandbox_Click(object sender, EventArgs e)
		{
			if (Sandbox.State == SandboxState.Paused)
				Sandbox.Step();
			else if (Sandbox.State == SandboxState.Inactive)
			{
				if (Sandbox.Play())
					Sandbox.Pause();
			}
		}
		private void actionPauseSandbox_Click(object sender, EventArgs e)
		{
			Sandbox.Pause();
		}
		private void actionStopSandbox_Click(object sender, EventArgs e)
		{
			Sandbox.Stop();
		}
		private void menuRunSandboxSlower_Click(object sender, EventArgs e)
		{
			Sandbox.Slower();
		}
		private void menuRunSandboxFaster_Click(object sender, EventArgs e)
		{
			Sandbox.Faster();
		}

		private void aboutItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			about.ShowDialog(this);
		}
		private void newProjectItem_Click(object sender, EventArgs e)
		{
			NewProjectDialog newProject = new NewProjectDialog();
			DialogResult result = newProject.ShowDialog(this);

			// Project successfully created?
			if (result == DialogResult.OK)
			{
				// Open new project
				var startInfo = new System.Diagnostics.ProcessStartInfo(newProject.ResultEditorBinary);
				startInfo.WorkingDirectory = Path.GetDirectoryName(startInfo.FileName);
				startInfo.UseShellExecute = false;
				System.Diagnostics.Process.Start(startInfo);

				// Don't need this DualityEditor anymore - close it!
				this.CloseNonUser();
			}
		}

		private void formatBinary_Click(object sender, EventArgs e)
		{
			if (Duality.Serialization.Formatter.DefaultMethod == Duality.Serialization.FormattingMethod.Binary) return;
			Duality.Serialization.Formatter.DefaultMethod = Duality.Serialization.FormattingMethod.Binary;
			this.UpdateToolbar();

			ProcessingBigTaskDialog taskDialog = new ProcessingBigTaskDialog(this, 
				EditorRes.GeneralRes.TaskChangeDataFormat_Caption, 
				string.Format(EditorRes.GeneralRes.TaskChangeDataFormat_Desc, Duality.Serialization.Formatter.DefaultMethod.ToString()), 
				this.async_ChangeDataFormat, null);
			taskDialog.ShowDialog();
		}
		private void formatXml_Click(object sender, EventArgs e)
		{
			if (Duality.Serialization.Formatter.DefaultMethod == Duality.Serialization.FormattingMethod.Xml) return;
			Duality.Serialization.Formatter.DefaultMethod = Duality.Serialization.FormattingMethod.Xml;
			this.UpdateToolbar();

			ProcessingBigTaskDialog taskDialog = new ProcessingBigTaskDialog(this, 
				EditorRes.GeneralRes.TaskChangeDataFormat_Caption, 
				string.Format(EditorRes.GeneralRes.TaskChangeDataFormat_Desc, Duality.Serialization.Formatter.DefaultMethod.ToString()), 
				this.async_ChangeDataFormat, null);
			taskDialog.ShowDialog();
		}
		private void selectFormattingMethod_Click(object sender, EventArgs e)
		{
			this.selectFormattingMethod.ShowDropDown();
		}
		
		private void Sandbox_StateChanged(object sender, EventArgs e)
		{
			this.UpdateToolbar();
		}

		private System.Collections.IEnumerable async_ChangeDataFormat(ProcessingBigTaskDialog.WorkerInterface state)
		{
			state.StateDesc = "DualityApp Data"; yield return null;
			DualityApp.LoadAppData();
			DualityApp.LoadUserData();
			DualityApp.LoadMetaData();
			state.Progress += 0.05f; yield return null;
					
			DualityApp.SaveAppData();
			DualityApp.SaveUserData();
			DualityApp.SaveMetaData();
			state.Progress += 0.05f; yield return null;

			// Special case: Current Scene in sandbox mode
			if (Sandbox.IsActive)
			{
				// Because changes we'll do will be discarded when leaving the sandbox we'll need to
				// do it the hard way - manually load an save the file.
				state.StateDesc = "Current Scene"; yield return null;
				Scene curScene = Resource.LoadResource<Scene>(Scene.CurrentPath);
				curScene.Save(Scene.CurrentPath);
			}

			List<string> resFiles = Resource.GetResourceFiles();
			foreach (string file in resFiles)
			{
				if (Sandbox.IsActive && file == Scene.CurrentPath) continue; // Skip current Scene in Sandbox
				state.StateDesc = file; yield return null;

				//var data = MetaFormatHelper.FileReadAll(file);	// Removed again so we don't need to handle "unsaved resources".
				var cr = ContentProvider.RequestContent(file);
				state.Progress += 0.45f / resFiles.Count; yield return null;

				//MetaFormatHelper.FileSaveAll(file, data);
				cr.Res.Save(file);
				state.Progress += 0.45f / resFiles.Count; yield return null;
			}
		}

		private void checkBackups_Clicked(object sender, EventArgs e)
		{
			DualityEditorApp.BackupsEnabled = !DualityEditorApp.BackupsEnabled;
			this.UpdateSplitButtonBackupSettings();
		}
		private void optionAutosaveDisabled_Clicked(object sender, EventArgs e)
		{
			DualityEditorApp.Autosaves = DualityEditorApp.Autosaves != AutosaveFrequency.Disabled ? AutosaveFrequency.Disabled : AutosaveFrequency.ThirtyMinutes;
			this.UpdateSplitButtonBackupSettings();
		}
		private void optionAutosaveTenMinutes_Clicked(object sender, EventArgs e)
		{
			DualityEditorApp.Autosaves = DualityEditorApp.Autosaves != AutosaveFrequency.TenMinutes ? AutosaveFrequency.TenMinutes : AutosaveFrequency.Disabled;
			this.UpdateSplitButtonBackupSettings();
		}
		private void optionAutosaveThirtyMinutes_Clicked(object sender, EventArgs e)
		{
			DualityEditorApp.Autosaves = DualityEditorApp.Autosaves != AutosaveFrequency.ThirtyMinutes ? AutosaveFrequency.ThirtyMinutes : AutosaveFrequency.Disabled;
			this.UpdateSplitButtonBackupSettings();
		}
		private void optionAutoSaveOneHour_Clicked(object sender, EventArgs e)
		{
			DualityEditorApp.Autosaves = DualityEditorApp.Autosaves != AutosaveFrequency.OneHour ? AutosaveFrequency.OneHour : AutosaveFrequency.Disabled;
			this.UpdateSplitButtonBackupSettings();
		}
		private void splitButtonBackupSettings_DropDownOpening(object sender, EventArgs e)
		{
			this.UpdateSplitButtonBackupSettings();
		}
		private void splitButtonBackupSettings_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) e.Cancel = true;
		}
		private void splitButtonBackupSettings_Click(object sender, EventArgs e)
		{
			this.splitButtonBackupSettings.ShowDropDown();
		}
		private void menuAutosave_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) e.Cancel = true;
		}
		private void quitItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void mainMenuItem_DropDownClosed(object sender, EventArgs e)
		{
			this.activeMenu = null;
		}
		private void mainMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			this.activeMenu = sender as ToolStripMenuItem;
		}
		private void UpdateSplitButtonBackupSettings()
		{
			this.checkBackups.Checked = DualityEditorApp.BackupsEnabled;
			this.optionAutosaveDisabled.Checked = DualityEditorApp.Autosaves == AutosaveFrequency.Disabled;
			this.optionAutosaveTenMinutes.Checked = DualityEditorApp.Autosaves == AutosaveFrequency.TenMinutes;
			this.optionAutosaveThirtyMinutes.Checked = DualityEditorApp.Autosaves == AutosaveFrequency.ThirtyMinutes;
			this.optionAutoSaveOneHour.Checked = DualityEditorApp.Autosaves == AutosaveFrequency.OneHour;
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			Point globalPos = this.PointToScreen(localPos);

			// Hovering an opened menu
			if (this.activeMenu != null)
			{
				ToolStripItem	item		= this.mainMenuStrip.GetItemAtDeep(globalPos);
				object			itemTag		= item != null ? item.Tag : null;

				result = itemTag as HelpInfo;
				captured = true;
			}
			else
			{
				ToolStripItem	item		= this.mainToolStrip.GetItemAtDeep(globalPos);
				object			itemTag		= item != null ? item.Tag : null;
				
				result = itemTag as HelpInfo;
				captured = false;
			}

			return result;
		}
	}
}
