using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Duality;
using Duality.Resources;

using WeifenLuo.WinFormsUI.Docking;

namespace DualityEditor.Forms
{
	public partial class MainForm : Form
	{
		private	bool	nonUserClosing	= false;
		private	Dictionary<string,ToolStripMenuItem>	menuRegistry	= new Dictionary<string,ToolStripMenuItem>();

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
			ToolStripMenuItem fileItem = this.RequestMenu(EditorRes.GeneralRes.MenuName_File);
			ToolStripMenuItem newProjectItem = this.RequestMenu(Path.Combine(EditorRes.GeneralRes.MenuName_File, EditorRes.GeneralRes.MenuItemName_NewProject));

			ToolStripMenuItem helpItem = this.RequestMenu(EditorRes.GeneralRes.MenuName_Help);
			ToolStripMenuItem aboutItem = this.RequestMenu(Path.Combine(EditorRes.GeneralRes.MenuName_Help, EditorRes.GeneralRes.MenuItemName_About));

			helpItem.Alignment = ToolStripItemAlignment.Right;
			aboutItem.Click += this.aboutItem_Click;

			newProjectItem.Image = EditorRes.GeneralRes.ImageAppCreate;
			newProjectItem.Click += this.newProjectItem_Click;
		}
		public ToolStripMenuItem RequestMenu(string menuPath)
		{
			menuPath = menuPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			string menuId = menuPath.ToUpper();
			ToolStripMenuItem item;
			if (!this.menuRegistry.TryGetValue(menuId, out item))
			{
				int lastDirSeparator = menuPath.LastIndexOf(Path.DirectorySeparatorChar);
				if (lastDirSeparator != -1)
				{
					string parentPath = menuPath.Substring(0, lastDirSeparator);
					string menuName = menuPath.Substring(lastDirSeparator + 1, menuPath.Length - (lastDirSeparator + 1));
					ToolStripMenuItem parentItem = this.RequestMenu(parentPath);
					item = new ToolStripMenuItem(menuName);
					parentItem.DropDownItems.Add(item);
				}
				else
				{
					item = new ToolStripMenuItem(menuPath);
					this.mainMenuStrip.Items.Add(item);
				}

				this.menuRegistry[menuId] = item;
			}
			return item;
		}
		private void UpdateToolbar()
		{
			this.actionRunSandbox.Enabled	= Sandbox.State != SandboxState.Playing;
			this.actionStopSandbox.Enabled	= Sandbox.State != SandboxState.Inactive;
			this.actionPauseSandbox.Enabled	= Sandbox.State == SandboxState.Playing;

			if (Duality.Serialization.Formatter.DefaultMethod == Duality.Serialization.FormattingMethod.Xml)
			{
				this.selectFormattingMethod.Image = this.formatXml.Image;
				this.selectFormattingMethod.ToolTipText = this.formatXml.Text;
				this.formatXml.Checked = true;
				this.formatBinary.Checked = false;
			}
			else
			{
				this.selectFormattingMethod.Image = this.formatBinary.Image;
				this.selectFormattingMethod.ToolTipText = this.formatBinary.Text;
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
			e.Cancel = !DualityEditorApp.Terminate(!this.nonUserClosing);
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
		private void actionPauseSandbox_Click(object sender, EventArgs e)
		{
			Sandbox.Pause();
		}
		private void actionStopSandbox_Click(object sender, EventArgs e)
		{
			Sandbox.Stop();
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
	}
}
