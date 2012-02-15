using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using OpenTK;

using Duality;
using Duality.ObjectManagers;
using Duality.Resources;
using Duality.Components;
using Duality.ColorFormat;

using DualityEditor;
using DualityEditor.Controls;

using WeifenLuo.WinFormsUI.Docking;
using Ionic.Zip;

namespace DualityEditor.Forms
{
	public partial class MainForm : Form
	{
		public enum SelectMode
		{
			Set,
			Append,
			Toggle
		}
		public enum SandboxState
		{
			Inactive,
			Playing,
			Paused
		}


		private static MainForm instance;
		internal static MainForm Instance
		{
			get { return instance; }
		}

		private	const	string	UserDataFile			= "editoruserdata.xml";
		private	const	string	UserDataDockSeparator	= "<!-- DockPanel Data -->";

		private	GLControl				mainContextControl	= null;
		private	HashSet<string>			reimportSchedule	= new HashSet<string>();
		private	List<IFileImporter>		fileImporters		= new List<IFileImporter>();
		private	List<EditorPlugin>		plugins				= new List<EditorPlugin>();
		private	Dictionary<Type,List<Type>>	availTypeDict	= new Dictionary<Type,List<Type>>();
		private	ReloadCorePluginDialog	corePluginReloader	= null;
		private	bool					needsRecovery		= false;
		private	Control					hoveredControl		= null;
		private	IHelpProvider			hoveredHelpProvider	= null;
		private	bool					hoveredHelpCaptured	= false;
		private	GameObjectManager		editorObjects		= new GameObjectManager();
		private	bool					dualityAppSuspended	= true;
		private	bool					sandboxStateChange	= false;
		private	bool					sandboxSceneFreeze	= false;

		private	List<string>				editorJustSavedRes		= new List<string>();
		private	List<FileSystemEventArgs>	dataDirEventBuffer		= new List<FileSystemEventArgs>();
		private	List<FileSystemEventArgs>	sourceDirEventBuffer	= new List<FileSystemEventArgs>();

		private	HelpStack		helpStack			= new HelpStack();
		private	SandboxState	sandboxState		= SandboxState.Inactive;
		private	ObjectSelection	selectionCurrent	= ObjectSelection.Null;
		private	ObjectSelection	selectionPrevious	= ObjectSelection.Null;
		private	bool			selectionChanging	= false;

		private	Dictionary<string,ToolStripMenuItem>	menuRegistry	= new Dictionary<string,ToolStripMenuItem>();


		public	event	EventHandler	BeforeReloadCorePlugins	= null;
		public	event	EventHandler	AfterReloadCorePlugins	= null;
		public	event	EventHandler	BeforeUpdateDualityApp	= null;
		public	event	EventHandler	AfterUpdateDualityApp	= null;
		public	event	EventHandler	SaveAllProjectData		= null;
		public	event	EventHandler	EnteringSandbox			= null;
		public	event	EventHandler	LeaveSandbox			= null;
		public	event	EventHandler<SelectionChangedEventArgs>			SelectionChanged		= null;
		public	event	EventHandler<ObjectPropertyChangedEventArgs>	ObjectPropertyChanged	= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceCreated			= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceDeleted			= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceModified		= null;
		public	event	EventHandler<ResourceRenamedEventArgs>			ResourceRenamed			= null;
		public	event	EventHandler<FileSystemEventArgs>				SrcFileDeleted			= null;
		public	event	EventHandler<FileSystemEventArgs>				SrcFileModified			= null;
		public	event	EventHandler<FileSystemEventArgs>				SrcFileRenamed			= null;

		public DockPanel MainDockPanel
		{
			get { return this.dockPanel; }
		}
		public GameObjectManager EditorObjects
		{
			get { return this.editorObjects; }
		}
		public ObjectSelection Selection
		{
			get { return this.selectionCurrent; }
		}
		public bool IsSelectionChanging
		{
			get { return this.selectionChanging; }
		}
		public HelpStack Help
		{
			get { return this.helpStack; }
		}
		public GLControl MainContextControl
		{
			get { return this.mainContextControl; }
		}
		public SandboxState CurrentSandboxState
		{
			get { return this.sandboxState; }
		}
		private bool AppStillIdle
		{
			 get
			{
				NativeMethods.Message msg;
				return !NativeMethods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
			 }
		}


		internal MainForm(bool recover)
		{
			if (instance != null && !instance.IsDisposed && !instance.Disposing) throw new InvalidOperationException("There can be only one MainForm at a time");
			instance = this;
			this.InitializeComponent();
			this.ApplyDockPanelSkin();

			this.needsRecovery = recover;

			if (!Directory.Exists(EditorHelper.DataDirectory))
			{
				Directory.CreateDirectory(EditorHelper.DataDirectory);
				using (FileStream s = File.OpenWrite(Path.Combine(EditorHelper.DataDirectory, "WorkingFolderIcon.ico")))
				{
					EditorRes.GeneralRes.Icon_WorkingFolder.Save(s);
				}
				using (StreamWriter w = new StreamWriter(Path.Combine(EditorHelper.DataDirectory, "desktop.ini")))
				{
					w.WriteLine("[.ShellClassInfo]");
					w.WriteLine("ConfirmFileOp=0");
					w.WriteLine("NoSharing=0");
					w.WriteLine("IconFile=WorkingFolderIcon.ico");
					w.WriteLine("IconIndex=0");
					w.WriteLine("InfoTip=This is DualityEditors working folder");
				}

				DirectoryInfo dirInfo = new DirectoryInfo(EditorHelper.DataDirectory);
				dirInfo.Attributes |= FileAttributes.System;

				FileInfo fileInfoDesktop = new FileInfo(Path.Combine(EditorHelper.DataDirectory, "desktop.ini"));
				fileInfoDesktop.Attributes |= FileAttributes.Hidden;

				FileInfo fileInfoIcon = new FileInfo(Path.Combine(EditorHelper.DataDirectory, "WorkingFolderIcon.ico"));
				fileInfoIcon.Attributes |= FileAttributes.Hidden;
			}
			if (!Directory.Exists(EditorHelper.SourceDirectory)) Directory.CreateDirectory(EditorHelper.SourceDirectory);
			if (!Directory.Exists(EditorHelper.SourceMediaDirectory)) Directory.CreateDirectory(EditorHelper.SourceMediaDirectory);
			if (!Directory.Exists(EditorHelper.SourceCodeDirectory)) Directory.CreateDirectory(EditorHelper.SourceCodeDirectory);

			this.actionDebugApp.Enabled = EditorHelper.IsJITDebuggerAvailable();

			DualityApp.Init(DualityApp.ExecutionEnvironment.Editor, DualityApp.ExecutionContext.Editor, new string[] {"logfile", "logfile_editor"});
			this.InitMenus();
			this.InitMainGLContext();
			ContentProvider.InitDefaultContent();
			this.LoadXmlCodeDoc();
			this.LoadPlugins();
			this.LoadUserData();
			this.InitPlugins();
			this.UpdateToolbar();

			this.corePluginReloader = new ReloadCorePluginDialog(this);
			this.corePluginReloader.BeforeBeginReload	+= this.corePluginReloader_BeforeBeginReload;
			this.corePluginReloader.AfterEndReload		+= this.corePluginReloader_AfterEndReload;
			this.pluginWatcher.EnableRaisingEvents = true;

			Scene.Leaving += new EventHandler(this.Scene_Leaving);
			Scene.Current = new Scene();

			this.dataDirWatcher.Path = EditorHelper.DataDirectory;
			this.dataDirWatcher.Created += delegate(object sender, FileSystemEventArgs e) { this.PushDataDirEvent(e); };
			this.dataDirWatcher.Changed += delegate(object sender, FileSystemEventArgs e) { this.PushDataDirEvent(e); };
			this.dataDirWatcher.Deleted += delegate(object sender, FileSystemEventArgs e) { this.PushDataDirEvent(e); };
			this.dataDirWatcher.Renamed += delegate(object sender, RenamedEventArgs e) { this.PushDataDirEvent(e); };
			this.dataDirWatcher.EnableRaisingEvents = true;

			this.sourceDirWatcher.Path = EditorHelper.SourceDirectory;
			this.sourceDirWatcher.Created += delegate(object sender, FileSystemEventArgs e) { this.PushSourceDirEvent(e); };
			this.sourceDirWatcher.Changed += delegate(object sender, FileSystemEventArgs e) { this.PushSourceDirEvent(e); };
			this.sourceDirWatcher.Deleted += delegate(object sender, FileSystemEventArgs e) { this.PushSourceDirEvent(e); };
			this.sourceDirWatcher.Renamed += delegate(object sender, RenamedEventArgs e) { this.PushSourceDirEvent(e); };
			this.sourceDirWatcher.EnableRaisingEvents = true;

			this.dualityAppSuspended = false;
			Application.Idle += this.Application_Idle;
			Resource.ResourceSaved += this.Resource_ResourceSaved;

			// Hook message filter
			InputEventMessageFilter inputFilter = new InputEventMessageFilter();
			inputFilter.MouseMove += this.inputFilter_MouseMove;
			inputFilter.KeyDown += this.inputFilter_KeyDown;
			Application.AddMessageFilter(inputFilter);
		}
		private void ApplyDockPanelSkin()
		{
			Color bgColor = Color.FromArgb(255, 128, 128, 128);
			Color inactiveTab = Color.FromArgb(255, 230, 235, 240);
			Color inactiveTab2 = Color.FromArgb(255, 255, 255, 255);
			Color activeTab = Color.FromArgb(255, 255, 225, 155);
			Color activeTab2 = Color.FromArgb(255, 255, 235, 205);

			//this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = bgColor;
			//this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = bgColor;
			//this.dockPanel.Skin.AutoHideStripSkin.DockStripGradient.StartColor = bgColor;
			//this.dockPanel.Skin.AutoHideStripSkin.DockStripGradient.EndColor = bgColor;

			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = inactiveTab2;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = inactiveTab;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Color.Black;

			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = activeTab2;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = activeTab;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.Black;
		}
		public void InitMainGLContext()
		{
			if (this.mainContextControl != null) return;
			this.mainContextControl = new GLControl(DualityApp.DefaultMode);
			this.mainContextControl.MakeCurrent();
			DualityApp.TargetMode = this.mainContextControl.Context.GraphicsMode;
		}

		public void InitMenus()
		{
			ToolStripMenuItem helpItem = this.RequestMenu(EditorRes.GeneralRes.MenuName_Help);
			ToolStripMenuItem aboutItem = this.RequestMenu(Path.Combine(EditorRes.GeneralRes.MenuName_Help, EditorRes.GeneralRes.MenuItemName_About));

			helpItem.Alignment = ToolStripItemAlignment.Right;
			aboutItem.Click += this.aboutItem_Click;
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

		public void SandboxPlay()
		{
			if (this.sandboxState == SandboxState.Playing) return;
			this.sandboxStateChange = true;
			if (this.sandboxState == SandboxState.Paused)
			{
				this.sandboxState = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;
				this.UpdateToolbar();
			}
			else
			{
				this.OnEnteringSandbox();

				// Save the current scene
				this.SaveCurrentScene();
				
				// Force later Scene reload by disposing it
				string curPath = null;
				if (!String.IsNullOrEmpty(Scene.Current.Path))
				{
					curPath = Scene.CurrentPath;
					Scene.Current.Dispose();
				}

				this.sandboxState = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;
				this.UpdateToolbar();

				// (Re)Load Scene
				if (curPath != null)
					Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;
			}

			this.sandboxStateChange = false;
		}
		public void SandboxPause()
		{
			if (this.sandboxState == SandboxState.Paused) return;
			this.sandboxStateChange = true;

			this.sandboxState = SandboxState.Paused;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;
			this.UpdateToolbar();

			this.sandboxStateChange = false;
		}
		public void SandboxStop()
		{
			if (this.sandboxState == SandboxState.Inactive) return;
			this.sandboxStateChange = true;

			// Force later Scene reload by disposing it
			string curPath = null;
			if (!String.IsNullOrEmpty(Scene.Current.Path))
			{
				curPath = Scene.CurrentPath;
				Scene.Current.Dispose();
			}

			this.sandboxState = SandboxState.Inactive;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;
			this.UpdateToolbar();
			
			// (Re)Load Scene
			if (curPath != null)
				Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;

			this.OnLeaveSandbox();
			this.sandboxStateChange = false;
		}
		public void SandboxSceneStartFreeze()
		{
			this.sandboxSceneFreeze = true;
		}
		public void SandboxSceneStopFreeze()
		{
			this.sandboxSceneFreeze = false;
		}
		public void SetCurrentDualityAppInput(IMouseInput mouse, IKeyboardInput keyboard)
		{
			DualityApp.Mouse = mouse;
			DualityApp.Keyboard = keyboard;
		}

		public void Select(object sender, ObjectSelection sel, SelectMode mode = SelectMode.Set)
		{
			this.selectionPrevious = this.selectionCurrent;
			if (mode == SelectMode.Set)
				this.selectionCurrent = this.selectionCurrent.Transform(sel);
			else if (mode == SelectMode.Append)
				this.selectionCurrent = this.selectionCurrent.Append(sel);
			else if (mode == SelectMode.Toggle)
				this.selectionCurrent = this.selectionCurrent.Toggle(sel);
			this.OnSelectionChanged(sender);
		}
		public void Deselect(object sender, ObjectSelection sel)
		{
			this.selectionPrevious = this.selectionCurrent;
			this.selectionCurrent = this.selectionCurrent.Remove(sel);
			this.OnSelectionChanged(sender);
		}
		public void Deselect(object sender, ObjectSelection.Category category)
		{
			this.selectionPrevious = this.selectionCurrent;
			this.selectionCurrent = this.selectionCurrent.Clear(category);
			this.OnSelectionChanged(sender);
		}
		public void Deselect(object sender, Predicate<object> predicate)
		{
			this.selectionPrevious = this.selectionCurrent;
			this.selectionCurrent = this.selectionCurrent.Clear(predicate);
			this.OnSelectionChanged(sender);
		}

		public void SaveCurrentScene(bool skipYetUnsaved = true)
		{
			if (!String.IsNullOrEmpty(Scene.Current.Path))
				Scene.Current.Save();
			else if (!skipYetUnsaved)
			{
				string basePath = Path.Combine(EditorHelper.DataDirectory, "Scene");
				string path = PathHelper.GetFreePath(basePath, Scene.FileExt);
				Scene.Current.Save(path);
			}
		}
		public void RequestSaveAllProjectData()
		{
			this.SaveCurrentScene();

			if (this.SaveAllProjectData != null)
				this.SaveAllProjectData(this, EventArgs.Empty);
		}

		public void UpdateSourceCode()
		{
			// Initially generate source code
			if (!File.Exists(EditorHelper.SourceCodeSolutionFile))
			{
				using (ZipFile gamePluginZip = ZipFile.Read(ReflectionHelper.GetEmbeddedResourceStream(typeof(MainForm).Assembly,  @"Resources\GamePluginTemplate.zip")))
				{
					gamePluginZip.ExtractAll(EditorHelper.SourceCodeDirectory, ExtractExistingFileAction.DoNotOverwrite);
				}
				// If Visual Studio is available, don't use the express version
				if (EditorHelper.IsJITDebuggerAvailable())
				{
					string solution = File.ReadAllText(EditorHelper.SourceCodeSolutionFile);
					File.WriteAllText(EditorHelper.SourceCodeSolutionFile, solution.Replace("# Visual C# Express 2010", "# Visual Studio 2010"), Encoding.UTF8);
				}

				// Replace class names
				string namespaceName = EditorHelper.GenerateClassNameFromPath(EditorHelper.CurrentProjectName);
				string pluginNameCore = namespaceName + "CorePlugin";
				string pluginNameEditor = namespaceName + "EditorPlugin";

				string file_CorePlugin_cs = File.ReadAllText(EditorHelper.SourceCodeCorePluginFile);
				string file_ComponentExample_cs = File.ReadAllText(EditorHelper.SourceCodeComponentExampleFile);
				string file_EditorPlugin_cs = File.ReadAllText(EditorHelper.SourceCodeEditorPluginFile);

				file_ComponentExample_cs = file_ComponentExample_cs.Replace("<Namespace>", namespaceName);
				file_CorePlugin_cs = file_CorePlugin_cs.Replace("<Namespace>", namespaceName);
				file_EditorPlugin_cs = file_EditorPlugin_cs.Replace("<Namespace>", namespaceName);

				file_CorePlugin_cs = file_CorePlugin_cs.Replace("<PluginClassName>", pluginNameCore);
				file_EditorPlugin_cs = file_EditorPlugin_cs.Replace("<PluginClassName>", pluginNameEditor);

				File.WriteAllText(EditorHelper.SourceCodeComponentExampleFile, file_ComponentExample_cs, Encoding.UTF8);
				File.WriteAllText(EditorHelper.SourceCodeCorePluginFile, file_CorePlugin_cs, Encoding.UTF8);
				File.WriteAllText(EditorHelper.SourceCodeEditorPluginFile, file_EditorPlugin_cs, Encoding.UTF8);
			}

			// Keep auto-generated files up-to-date
			File.WriteAllText(EditorHelper.SourceCodeGameResFile, EditorHelper.GenerateGameResSrcFile());
		}

		public void NotifyObjPrefabApplied(object sender, ObjectSelection obj)
		{
			this.OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, new PropertyInfo[0], true));
		}
		public void NotifyObjPropChanged(object sender, ObjectSelection obj, params PropertyInfo[] info)
		{
			this.OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, info, false));
		}

		public T GetPlugin<T>() where T : EditorPlugin
		{
			foreach (EditorPlugin p in this.plugins)
			{
				if (p is T) return p as T;
			}
			return null;
		}

		public void LoadXmlCodeDoc()
		{
			this.LoadXmlCodeDoc("Duality.xml");
			foreach (string xmlDocFile in Directory.EnumerateFiles("Plugins", "*.core.xml", SearchOption.AllDirectories))
				this.LoadXmlCodeDoc(xmlDocFile);
		}
		public void LoadXmlCodeDoc(string file)
		{
			XmlCodeDoc xmlDoc = new XmlCodeDoc(file);
			CorePluginHelper.RegisterXmlCodeDoc(xmlDoc);
		}

		public void RegisterFileImporter(IFileImporter importer)
		{
			this.fileImporters.Add(importer);
		}
		public void UnregisterFileImporter(IFileImporter importer)
		{
			this.fileImporters.Remove(importer);
		}
		public bool IsImportFileExisting(string filePath)
		{
			string srcFilePath, targetName, targetDir;
			this.PrepareImportFilePaths(filePath, out srcFilePath, out targetName, out targetDir);

			// Does the source file already exist?
			if (File.Exists(srcFilePath)) return true;

			// Find an importer and check if one of its output files already exist
			foreach (IFileImporter i in this.fileImporters)
			{
				if (i.CanImportFile(srcFilePath))
				{
					foreach (string file in i.GetOutputFiles(srcFilePath, targetName, targetDir))
					{
						if (File.Exists(file)) return true;
					}
					// If we've got a hit, don't search further - ImportFile won't either.
					break;
				}
			}

			return false;
		}
		public bool ImportFile(string filePath)
		{
			// Determine & check paths
			string srcFilePath, targetName, targetDir;
			this.PrepareImportFilePaths(filePath, out srcFilePath, out targetName, out targetDir);

			// Find an importer to handle the file import
			IFileImporter importer = this.fileImporters.FirstOrDefault(i => i.CanImportFile(filePath));
			if (importer != null)
			{
				try
				{
					// Assure the directory exists
					Directory.CreateDirectory(Path.GetDirectoryName(srcFilePath));

					// Move file from data directory to source directory
					if (File.Exists(srcFilePath))
					{
						File.Copy(filePath, srcFilePath, true);
						File.Delete(filePath);
					}
					else
						File.Move(filePath, srcFilePath);
				} catch (Exception) { return false; }

				// Import it
				importer.ImportFile(srcFilePath, targetName, targetDir);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				return true;
			}
			else
				return false;
		}

		private bool IsResPathIgnored(string filePath)
		{
			return this.IsPathIgnored(filePath);
		}
		private bool IsSourcePathIgnored(string filePath)
		{
			return this.IsPathIgnored(filePath);
		}
		private bool IsPathIgnored(string filePath)
		{
			if (!File.Exists(filePath) || !Directory.Exists(filePath)) return false;
			if (!PathHelper.IsPathVisible(filePath)) return true;
			if (filePath.Contains(@"/.svn/") || filePath.Contains(@"\.svn\")) return true;
			return false;
		}

		private void ReimportFile(string filePath)
		{
			// Find an importer to handle the file import
			IFileImporter importer = this.fileImporters.FirstOrDefault(i => i.CanImportFile(filePath));
			if (importer == null) return;

			foreach (Resource r in ContentProvider.GetAvailContent<Resource>())
			{
				if (!importer.IsUsingSrcFile(r, filePath)) continue;
				try
				{
					importer.ReimportFile(r, filePath);
				}
				catch (Exception) 
				{
					Log.Editor.WriteError("Can't re-import file '{0}'", filePath);
				}
			}
		}
		private void PrepareImportFilePaths(string filePath, out string srcFilePath, out string targetName, out string targetDir)
		{
			srcFilePath = PathHelper.MakeFilePathRelative(filePath, EditorHelper.DataDirectory);
			if (srcFilePath.Contains("..")) srcFilePath = Path.GetFileName(srcFilePath);

			targetDir = Path.GetDirectoryName(Path.Combine(EditorHelper.DataDirectory, srcFilePath));
			targetName = Path.GetFileNameWithoutExtension(filePath);

			srcFilePath = PathHelper.GetFreePath(
				Path.Combine(EditorHelper.SourceMediaDirectory, Path.GetFileNameWithoutExtension(srcFilePath)), 
				Path.GetExtension(srcFilePath));
		}
		private void PerformScheduledReimport()
		{
			if (this.reimportSchedule.Count == 0) return;

			// Hacky: Wait a little for the files to be accessable again (Might be used by another process)
			System.Threading.Thread.Sleep(50);

			foreach (string file in this.reimportSchedule)
			{
				if (!File.Exists(file)) continue;
				this.ReimportFile(file);
			}
			this.reimportSchedule.Clear();
		}

		private bool DisplayConfirmImportOverwrite(string importFilePath)
		{
			DialogResult result = MessageBox.Show(
				String.Format(EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Text, importFilePath), 
				EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Warning);
			return result == DialogResult.Yes;
		}
		private void DisplayErrorImportFile(string importFilePath)
		{
			MessageBox.Show(
				String.Format(EditorRes.GeneralRes.Msg_CantImport_Text, importFilePath), 
				EditorRes.GeneralRes.Msg_CantImport_Caption, 
				MessageBoxButtons.OK, 
				MessageBoxIcon.Error);
		}
		private bool DisplayConfirmReloadResource(string resFilePath)
		{
			DialogResult result = MessageBox.Show(
				String.Format(EditorRes.GeneralRes.Msg_ConfirmReloadResource_Text, resFilePath), 
				EditorRes.GeneralRes.Msg_ConfirmReloadResource_Caption, 
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}

		private void UpdateToolbar()
		{
			this.actionRunSandbox.Enabled	= this.sandboxState != SandboxState.Playing;
			this.actionStopSandbox.Enabled	= this.sandboxState != SandboxState.Inactive;
			this.actionPauseSandbox.Enabled	= this.sandboxState == SandboxState.Playing;

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
		
		private void LoadPlugins()
		{
			Log.Editor.Write("Scanning for editor plugins...");
			Log.Editor.PushIndent();

			if (Directory.Exists("Plugins"))
			{
				string[] pluginDllPaths = Directory.GetFiles("Plugins", "*.editor.dll", SearchOption.AllDirectories);
				Assembly pluginAssembly;
				Type[] exportedTypes;
				for (int i = 0; i < pluginDllPaths.Length; i++)
				{
					Log.Editor.Write("Loading '{0}'...", pluginDllPaths[i]);
					Log.Editor.PushIndent();
					pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginDllPaths[i])); //Assembly.LoadFile(Path.GetFullPath(pluginDllPaths[i]));
					exportedTypes = pluginAssembly.GetExportedTypes();
					try
					{
						// Initialize plugin objects
						for (int j = 0; j < exportedTypes.Length; j++)
						{
							if (typeof(EditorPlugin).IsAssignableFrom(exportedTypes[j]))
							{
								Log.Editor.Write("Instantiating class '{0}'...", exportedTypes[j].Name);
								EditorPlugin plugin = (EditorPlugin)ReflectionHelper.CreateInstanceOf(exportedTypes[j]);
								plugin.LoadPlugin();
								this.plugins.Add(plugin);
							}
						}
					}
					catch (Exception e)
					{
						Log.Editor.WriteError("Error loading plugin '{0}'. Exception: {1}", pluginDllPaths[i], Log.Exception(e));
					}
					Log.Editor.PopIndent();
				}
			}

			Log.Editor.PopIndent();
		}
		private void InitPlugins()
		{
			Log.Editor.Write("Initializing editor plugins...");
			Log.Editor.PushIndent();
			foreach (EditorPlugin plugin in this.plugins)
			{
				Log.Editor.Write("'{0}'...", plugin.Id);
				plugin.InitPlugin(this);
			}
			Log.Editor.PopIndent();
		}
		
		public IEnumerable<Assembly> GetDualityEditorAssemblies()
		{
			yield return typeof(MainForm).Assembly;
			foreach (Assembly a in this.plugins.Select(ep => ep.GetType().Assembly)) yield return a;
		}
		public IEnumerable<Type> GetAvailDualityEditorTypes(Type baseType)
		{
			List<Type> availTypes;
			if (this.availTypeDict.TryGetValue(baseType, out availTypes)) return availTypes;

			availTypes = new List<Type>();
			IEnumerable<Assembly> asmQuery = this.GetDualityEditorAssemblies();
			foreach (Assembly asm in asmQuery)
			{
				availTypes.AddRange(
					from t in asm.GetExportedTypes()
					where baseType.IsAssignableFrom(t)
					select t);
			}
			this.availTypeDict[baseType] = availTypes;

			return availTypes;
		}

		private void SaveUserData()
		{
			Log.Editor.Write("Saving user data...");
			Log.Editor.PushIndent();

			using (FileStream str = File.Create(UserDataFile))
			{
				StreamWriter writer = new StreamWriter(str);
				// --- Save custom user data here ---
				System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
				System.Xml.XmlElement rootElement = xmlDoc.CreateElement("PluginUserData");
				xmlDoc.AppendChild(rootElement);
				foreach (EditorPlugin plugin in this.plugins)
				{
					System.Xml.XmlElement pluginXmlElement = xmlDoc.CreateElement("Plugin_" + plugin.Id);
					rootElement.AppendChild(pluginXmlElement);
					plugin.SaveUserData(xmlDoc, pluginXmlElement);
				}
				xmlDoc.Save(writer.BaseStream);
				// ----------------------------------
				writer.WriteLine();
				writer.WriteLine(UserDataDockSeparator);
				writer.Flush();
				this.dockPanel.SaveAsXml(str, writer.Encoding);
			}

			Log.Editor.PopIndent();
		}
		private void LoadUserData()
		{
			if (!File.Exists(UserDataFile)) return;

			Log.Editor.Write("Loading user data...");
			Log.Editor.PushIndent();

			using (StreamReader reader = new StreamReader(UserDataFile))
			{
				string line;
				// Retrieve pre-DockPanel section
				StringBuilder editorData = new StringBuilder();
				while ((line = reader.ReadLine()) != null && line.Trim() != UserDataDockSeparator) 
					editorData.AppendLine(line);
				// Retrieve DockPanel section
				StringBuilder dockPanelData = new StringBuilder();
				while ((line = reader.ReadLine()) != null) 
					dockPanelData.AppendLine(line);

				// Load DockPanel Data
				Log.Editor.Write("Loading DockPanel data...");
				Log.Editor.PushIndent();
				MemoryStream dockPanelDataStream = new MemoryStream(reader.CurrentEncoding.GetBytes(dockPanelData.ToString()));
				try
				{
					this.dockPanel.LoadFromXml(dockPanelDataStream, this.DeserializeDockContent);
				}
				catch (System.Xml.XmlException e)
				{
					Log.Editor.WriteError("Cannot load DockPanel data due to malformed or non-existent Xml: {0}", Log.Exception(e));
				}
				Log.Editor.PopIndent();

				// --- Read custom user data from StringBuilder here ---
				Log.Editor.Write("Loading plugin user data...");
				Log.Editor.PushIndent();
				System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
				try
				{
					xmlDoc.LoadXml(editorData.ToString());
					foreach (System.Xml.XmlElement child in xmlDoc.DocumentElement)
					{
						if (child.Name.StartsWith("Plugin_"))
						{
							string pluginName = child.Name.Substring(7, child.Name.Length - 7);
							foreach (EditorPlugin plugin in this.plugins)
							{
								if (plugin.Id == pluginName)
								{
									plugin.LoadUserData(child);
									break;
								}
							}
						}
					}
				}
				catch (System.Xml.XmlException e)
				{
					Log.Editor.WriteError("Cannot load plugin user data due to malformed or non-existent Xml: {0}", Log.Exception(e));
				}
				Log.Editor.PopIndent();
				// -----------------------------------------------------
			}

			Log.Editor.PopIndent();
		}
		private IDockContent DeserializeDockContent(string persistName)
		{
			Log.Editor.Write("Deserializing layout: '" + persistName + "'");

			string[] persistNameToken = persistName.Split('.');

			Type dockContentType = null;
			Assembly dockContentAssembly = null;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly a in assemblies)
			{
				if ((dockContentType = a.GetType(persistName)) != null)
				{
					dockContentAssembly = a;
					break;
				}
			}
			
			if (dockContentType == null) 
				return null;
			else
			{
				// First ask plugins from the dock contents assembly for existing instances
				IDockContent deserializeDockContent = null;
				foreach (EditorPlugin plugin in this.plugins)
				{
					if (plugin.GetType().Assembly == dockContentAssembly)
					{
						deserializeDockContent = plugin.DeserializeDockContent(dockContentType);
						if (deserializeDockContent != null) break;
					}
				}

				// If none exists, create one
				if (deserializeDockContent == null) 
					deserializeDockContent = ReflectionHelper.CreateInstanceOf(dockContentType) as IDockContent;

				return deserializeDockContent;
			}
		}

		private void OnBeforeReloadCorePlugins()
		{
			this.dualityAppSuspended = true;

			Log.Editor.Write("Core plugin reloader initialized");
			Log.Editor.PushIndent();
			if (this.BeforeReloadCorePlugins != null)
				this.BeforeReloadCorePlugins(this, null);
		}
		private void OnAfterReloadCorePlugins()
		{
			this.dualityAppSuspended = false;

			if (this.AfterReloadCorePlugins != null)
				this.AfterReloadCorePlugins(this, null);
			Log.Editor.PopIndent();
			Log.Editor.Write("Core plugin reloader finished");
		}
		private void OnBeforeUpdateDualityApp()
		{
			if (this.BeforeUpdateDualityApp != null)
				this.BeforeUpdateDualityApp(this, null);
		}
		private void OnAfterUpdateDualityApp()
		{
			if (this.AfterUpdateDualityApp != null)
				this.AfterUpdateDualityApp(this, null);
		}
		private void OnEnteringSandbox()
		{
			if (this.EnteringSandbox != null)
				this.EnteringSandbox(this, null);
		}
		private void OnLeaveSandbox()
		{
			if (this.LeaveSandbox != null)
				this.LeaveSandbox(this, null);
		}
		private void OnSelectionChanged(object sender)
		{
			if (this.selectionCurrent == this.selectionPrevious) return;
			if (sender == null) sender = this;

			this.selectionChanging = true;

			if (this.SelectionChanged != null)
				this.SelectionChanged(sender, new SelectionChangedEventArgs(this.selectionCurrent, this.selectionPrevious));

			this.selectionChanging = false;
		}
		private void OnObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs args)
		{
			if (sender == null) sender = this;

			// If a linked GameObject was modified, update its prefab link changelist
			if (!args.PrefabApplied && (args.Objects.GameObjects.Any() || args.Objects.Components.Any()))
			{
				HashSet<PrefabLink> changedLinks = new HashSet<PrefabLink>();
				foreach (object o in args.Objects.Objects)
				{
					Component cmp = o as Component;
					GameObject obj = o as GameObject;
					if (cmp == null && obj == null) continue;

					PrefabLink link = obj != null ? obj.AffectedByPrefabLink : cmp.GameObj.AffectedByPrefabLink;
					if (link == null) continue;
					if (cmp != null && !link.AffectsObject(cmp)) continue;
					if (obj != null && !link.AffectsObject(obj)) continue;

					// Handle property changes regarding affected prefab links change lists
					foreach (PropertyInfo info in args.PropInfos)
					{
						if (this.PushPrefabLinkPropertyChange(link, o, info))
							changedLinks.Add(link);
					}
				}

				foreach (PrefabLink link in changedLinks)
				{
					this.NotifyObjPropChanged(this, new ObjectSelection(link.Obj), ReflectionInfo.Property_GameObject_PrefabLink);
				}
			}

			// If a Resource's Properties are modified, save the Resource
			if (args.Objects.ResourceCount > 0)
			{
				// This is probably not the best idea for generalized behaviour, but sufficient for now
				foreach (Resource res in args.Objects.Resources)
				{
					if (this.sandboxState != SandboxState.Inactive && res is Scene && (res as Scene).IsCurrent) continue;
					res.Save();
				}
			}

			// If DualityAppData or DualityUserData is modified, save it
			if (args.Objects.OtherObjectCount > 0)
			{
				// This is probably not the best idea for generalized behaviour, but sufficient for now
				if (args.Objects.OtherObjects.Any(o => o is DualityAppData))
					DualityApp.SaveAppData();
				else if (args.Objects.OtherObjects.Any(o => o is DualityUserData))
					DualityApp.SaveUserData();
			}

			// Fire the actual event
			if (this.ObjectPropertyChanged != null)
				this.ObjectPropertyChanged(sender, args);
		}

		public bool DisplayConfirmBreakPrefabLink(ObjectSelection obj = null)
		{
			if (obj == null) obj = this.Selection;

			var linkQueryObj =
				from o in obj.GameObjects
				where (o.PrefabLink == null && o.AffectedByPrefabLink != null && o.AffectedByPrefabLink.AffectsObject(o)) || (o.PrefabLink != null && o.PrefabLink.ParentLink != null && o.PrefabLink.ParentLink.AffectsObject(o))
				select o.PrefabLink == null ? o.AffectedByPrefabLink : o.PrefabLink.ParentLink;
			var linkQueryCmp =
				from c in obj.Components
				where c.GameObj.AffectedByPrefabLink != null && c.GameObj.AffectedByPrefabLink.AffectsObject(c)
				select c.GameObj.AffectedByPrefabLink;
			var linkList = new List<PrefabLink>(linkQueryObj.Concat(linkQueryCmp).Distinct());
			if (linkList.Count == 0) return true;

			DialogResult result = MessageBox.Show(
				EditorRes.GeneralRes.Msg_ConfirmBreakPrefabLink_Desc, 
				EditorRes.GeneralRes.Msg_ConfirmBreakPrefabLink_Caption, 
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result != DialogResult.Yes) return false;

			foreach (PrefabLink link in linkList) link.Obj.BreakPrefabLink();
			this.NotifyObjPropChanged(
				this, 
				new ObjectSelection(linkList.Select(l => l.Obj)), 
				ReflectionInfo.Property_GameObject_PrefabLink);

			return true;
		}
		public bool PushPrefabLinkPropertyChange(PrefabLink link, object target, PropertyInfo info)
		{
			if (link == null) return false;

			if (info == ReflectionInfo.Property_GameObject_PrefabLink)
			{
				GameObject obj = target as GameObject;
				if (obj == null) return false;

				PrefabLink parentLink;
				if (obj.PrefabLink == link && (parentLink = link.ParentLink) != null)
				{
					parentLink.PushChange(obj, info, obj.PrefabLink.Clone());
					this.NotifyObjPropChanged(this, new ObjectSelection(parentLink.Obj), info);
				}
				return false;
			}
			else
			{
				link.PushChange(target, info);
				return true;
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.WindowState = FormWindowState.Maximized;
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (!e.Cancel) this.SaveUserData();
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			DualityApp.Terminate();
			if (instance == this) instance = null;
		}
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			// Core plugin reload
			if (this.needsRecovery)
			{
				this.needsRecovery = false;
				Log.Editor.Write("Recovering from full plugin reload restart...");
				Log.Editor.PushIndent();
				this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.RecoverFromRestart;
			}
			else if (this.corePluginReloader.State == ReloadCorePluginDialog.ReloaderState.WaitForPlugins)
			{
				this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.ReloadPlugins;
			}

			// Reimport data
			this.PerformScheduledReimport();
		}
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			// Update source code, in case the user is switching to his IDE without hitting the "open source code" button again
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated)
				this.UpdateSourceCode();
		}

		private void PushDataDirEvent(FileSystemEventArgs e)
		{
			if (this.IsResPathIgnored(e.FullPath)) return;
			this.dataDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && f.ChangeType == e.ChangeType);
			this.dataDirEventBuffer.Add(e);
		}
		private void ProcessDataDirEvents()
		{
			foreach (FileSystemEventArgs e in this.dataDirEventBuffer)
			{
				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);

					// Unregister outdated resources, if modified outside the editor
					if (!this.editorJustSavedRes.Contains(Path.GetFullPath(e.FullPath)) && ContentProvider.IsContentRegistered(args.Path))
					{
						if (args.Content.Is<Scene>() && Scene.Current == args.Content.Res)
						{
							if (this.DisplayConfirmReloadResource(e.FullPath))
								ContentProvider.UnregisterContent(args.Path);
						}
						else
							ContentProvider.UnregisterContent(args.Path);
					}

					// When modifying prefabs, apply changes to all linked objects
					if (args.IsResource && args.Content.Is<Prefab>())
					{
						ContentRef<Prefab> prefabRef = args.Content.As<Prefab>();
						List<PrefabLink> appliedLinks = PrefabLink.ApplyAllLinks(Scene.Current.AllObjects, p => p.Prefab == prefabRef);
						List<GameObject> changedObjects = new List<GameObject>(appliedLinks.Select(p => p.Obj));
						this.NotifyObjPrefabApplied(this, new ObjectSelection(changedObjects));
					}

					if (this.ResourceModified != null) this.ResourceModified(this, args);
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
					if (File.Exists(e.FullPath))
					{
						// Register newly detected ressource file
						if (Resource.IsResourceFile(e.FullPath))
						{
							if (this.ResourceCreated != null)
								this.ResourceCreated(this, new ResourceEventArgs(e.FullPath));
						}
						// Import non-ressource file
						else
						{
							bool abort = false;

							if (this.IsImportFileExisting(e.FullPath)) 
								abort = !this.DisplayConfirmImportOverwrite(e.FullPath);

							if (!abort)
							{
								bool importedSuccessfully = this.ImportFile(e.FullPath);
								if (!importedSuccessfully) this.DisplayErrorImportFile(e.FullPath);
								abort = !importedSuccessfully;
							}
						}
					}
					else if (Directory.Exists(e.FullPath))
					{
						// Register newly detected ressource directory
						if (this.ResourceCreated != null)
							this.ResourceCreated(this, new ResourceEventArgs(e.FullPath));
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);

					// Unregister no-more existing resources
					if (args.IsDirectory)	ContentProvider.UnregisterContentTree(args.Path);
					else					ContentProvider.UnregisterContent(args.Path);

					if (this.ResourceDeleted != null)
						this.ResourceDeleted(this, args);
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					RenamedEventArgs re = e as RenamedEventArgs;
					ResourceRenamedEventArgs args = new ResourceRenamedEventArgs(re.FullPath, re.OldFullPath);

					// Rename content registerations
					if (args.IsDirectory)	ContentProvider.RenameContentTree(args.OldPath, args.Path);
					else					ContentProvider.RenameContent(args.OldPath, args.Path);

					// If we just renamed the currently loaded scene, relocate it.
					// Doesn't trigger if done properly from inside the editor.
					if (Scene.CurrentPath == re.OldFullPath) Scene.Current = Resource.LoadResource<Scene>(re.FullPath);

					if (this.ResourceRenamed != null) this.ResourceRenamed(this, args);
				}
			}
			this.dataDirEventBuffer.Clear();
		}
		private void PushSourceDirEvent(FileSystemEventArgs e)
		{
			if (this.IsSourcePathIgnored(e.FullPath)) return;
			this.sourceDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && e.ChangeType == e.ChangeType);
			this.sourceDirEventBuffer.Add(e);
		}
		private void ProcessSourceDirEvents()
		{
			foreach (FileSystemEventArgs e in this.sourceDirEventBuffer)
			{
				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					if (File.Exists(e.FullPath)) this.reimportSchedule.Add(e.FullPath);
					if (this.SrcFileModified != null) this.SrcFileModified(this, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					if (this.SrcFileDeleted != null) this.SrcFileDeleted(this, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					if (File.Exists(e.FullPath)) this.reimportSchedule.Add(e.FullPath);
					if (this.SrcFileRenamed != null) this.SrcFileRenamed(this, e);
				}
			}
			this.sourceDirEventBuffer.Clear();
		}

		private void corePluginWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			string pluginStr = Path.Combine("Plugins", e.Name);
			if (!this.corePluginReloader.ReloadSchedule.Contains(pluginStr))
				this.corePluginReloader.ReloadSchedule.Add(pluginStr);
			this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.WaitForPlugins;
		}
		private void corePluginReloader_AfterEndReload(object sender, EventArgs e)
		{
			this.OnAfterReloadCorePlugins();
		}
		private void corePluginReloader_BeforeBeginReload(object sender, EventArgs e)
		{
			this.OnBeforeReloadCorePlugins();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ProcessSourceDirEvents();
			this.ProcessDataDirEvents();
			this.editorJustSavedRes.Clear();

			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			while (this.AppStillIdle)
			{
				watch.Restart();
				if (!this.dualityAppSuspended)
				{
					this.OnBeforeUpdateDualityApp();
					try
					{
						DualityApp.EditorUpdate(this.editorObjects, this.sandboxSceneFreeze);
					}
					catch (Exception exception)
					{
						Log.Editor.Write("An error occured during a core update: {0}", Log.Exception(exception));
					}
					this.OnAfterUpdateDualityApp();
				}
				System.Threading.Thread.Sleep(Math.Max(1, 17 - (int)watch.ElapsedMilliseconds));
			}
		}
		private void Scene_Leaving(object sender, EventArgs e)
		{
			this.Deselect(this, ObjectSelection.Category.All);
			if (!this.sandboxStateChange) this.SandboxStop();
		}

		private void actionRunApp_Click(object sender, EventArgs e)
		{
			this.RequestSaveAllProjectData();
			System.Diagnostics.Process appProc = System.Diagnostics.Process.Start("DualityLauncher.exe", "editor");
			AppRunningDialog runningDialog = new AppRunningDialog(appProc);
			runningDialog.ShowDialog(this);
		}
		private void actionDebugApp_Click(object sender, EventArgs e)
		{
			this.RequestSaveAllProjectData();
			System.Diagnostics.Process appProc = System.Diagnostics.Process.Start("DualityLauncher.exe", "editor debug");
			AppRunningDialog runningDialog = new AppRunningDialog(appProc);
			runningDialog.ShowDialog(this);
		}
		private void actionSaveAll_Click(object sender, EventArgs e)
		{
			this.RequestSaveAllProjectData();
			System.Media.SystemSounds.Asterisk.Play();
		}
		private void actionOpenCode_Click(object sender, EventArgs e)
		{
			this.UpdateSourceCode();
			System.Diagnostics.Process.Start(EditorHelper.SourceCodeSolutionFile);
		}
		private void actionRunSandbox_Click(object sender, EventArgs e)
		{
			this.SandboxPlay();
		}
		private void actionPauseSandbox_Click(object sender, EventArgs e)
		{
			this.SandboxPause();
		}
		private void actionStopSandbox_Click(object sender, EventArgs e)
		{
			this.SandboxStop();
		}

		private void aboutItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			about.ShowDialog(this);
		}

		private void inputFilter_MouseMove(object sender, EventArgs e)
		{
			Control lastHoveredControl = this.hoveredControl;
			foreach (Form f in EditorHelper.GetZSortedAppWindows())
			{
				if (!f.Visible) continue;
				if (!new Rectangle(f.Location, f.Size).Contains(Cursor.Position)) continue;

				Point localPos = f.PointToClient(Cursor.Position);
				this.hoveredControl = f.GetChildAtPointDeep(localPos, GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Transparent);
				break;
			}

			Control c;
			HelpInfo help;

			// An IHelpProvider has captured the mouse: Ask what to do with it.
			if (this.hoveredHelpCaptured)
			{
				c = this.hoveredHelpProvider as Control;
				help = this.hoveredHelpProvider.ProvideHoverHelp(c.PointToClient(Cursor.Position), ref this.hoveredHelpCaptured);
				lastHoveredControl = c;

				// Update provider's help info
				this.Help.UpdateFromProvider(this.hoveredHelpProvider, help);

				// If still in charge: Return early.
				if (this.hoveredHelpCaptured) return;
			}

			// No IHelpProvider in charge: Find one that provides help
			help = null;
			IHelpProvider lastHelpProvider = this.hoveredHelpProvider;
			foreach (IHelpProvider hp in this.hoveredControl.GetControlAncestors<IHelpProvider>())
			{
				c = hp as Control;
				help = hp.ProvideHoverHelp(c.PointToClient(Cursor.Position), ref this.hoveredHelpCaptured);
				this.hoveredHelpProvider = hp;
				if (help != null || this.hoveredHelpCaptured) break;
			}

			// Update help system based on the result.
			if (lastHelpProvider != this.hoveredHelpProvider)
				this.Help.UpdateFromProvider(lastHelpProvider, this.hoveredHelpProvider, help);
			else if (this.hoveredHelpProvider != null)
				this.Help.UpdateFromProvider(this.hoveredHelpProvider, help);
		}
		private void inputFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				// Ask Help Provider for help
				if (this.Help.ActiveHelpProvider != null)
				{
					e.Handled = e.Handled | this.Help.ActiveHelpProvider.PerformHelpAction(this.Help.ActiveHelp);
				}

				// No reaction? Just open the reference then.
				if (!e.Handled && File.Exists("DDoc.chm") && !System.Diagnostics.Process.GetProcessesByName("hh").Any())
				{
					System.Diagnostics.Process.Start("HH.exe", Path.GetFullPath("DDoc.chm"));
					e.Handled = true;
				}
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

		private void Resource_ResourceSaved(object sender, Duality.ResourceEventArgs e)
		{
			if (e.Path == null) return; // Ignore Resources without a path.
			this.editorJustSavedRes.Add(Path.GetFullPath(e.Path));
		}

		private System.Collections.IEnumerable async_ChangeDataFormat(ProcessingBigTaskDialog.WorkerInterface state)
		{
			DualityApp.LoadAppData();
			DualityApp.LoadUserData();
			DualityApp.LoadMetaData();
			state.Progress += 0.05f; yield return null;

			ContentProvider.ClearContent();
			string[] resFiles = Directory.GetFiles("Data", "*" + Resource.FileExt, SearchOption.AllDirectories);
			foreach (string file in resFiles)
			{
				state.StateDesc = file; yield return null;

				var cr = ContentProvider.RequestContent(file);
				state.Progress += 0.45f / resFiles.Length; yield return null;

				cr.Res.Save(file);
				state.Progress += 0.45f / resFiles.Length; yield return null;
			}
			ContentProvider.ClearContent();
					
			DualityApp.SaveAppData();
			DualityApp.SaveUserData();
			DualityApp.SaveMetaData();
			state.Progress += 0.05f; yield return null;
		}
	}
}
