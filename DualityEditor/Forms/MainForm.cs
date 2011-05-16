using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Duality;
using Duality.ObjectManagers;
using Duality.Resources;
using Duality.Components;

using DualityEditor;

using WeifenLuo.WinFormsUI.Docking;

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


		private	const	string	UserDataFile			= "userdata.xml";
		private	const	string	UserDataDockSeparator	= "<!-- DockPanel Data -->";

		private	List<IFileImporter>		fileImporters		= new List<IFileImporter>();
		private	List<EditorPlugin>		plugins				= new List<EditorPlugin>();
		private	ReloadCorePluginDialog	corePluginReloader	= null;
		private	bool					needsRecovery		= false;

		private	GameObjectManager		editorObjects		= new GameObjectManager();
		private	bool					dualityAppSuspended	= true;

		private	ObjectSelection	selectionCurrent	= ObjectSelection.Null;
		private	ObjectSelection	selectionPrevious	= ObjectSelection.Null;
		private	bool			selectionChanging	= false;

		private	Dictionary<string,ToolStripMenuItem>	menuRegistry	= new Dictionary<string,ToolStripMenuItem>();


		public	event	EventHandler	BeforeReloadCorePlugins	= null;
		public	event	EventHandler	AfterReloadCorePlugins	= null;
		public	event	EventHandler	BeforeUpdateDualityApp	= null;
		public	event	EventHandler	AfterUpdateDualityApp	= null;
		public	event	EventHandler<SelectionChangedEventArgs>			SelectionChanged		= null;
		public	event	EventHandler<ObjectPropertyChangedEventArgs>	ObjectPropertyChanged	= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceCreated			= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceDeleted			= null;
		public	event	EventHandler<ResourceEventArgs>					ResourceModified		= null;
		public	event	EventHandler<ResourceRenamedEventArgs>			ResourceRenamed			= null;


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
		private bool AppStillIdle
		{
			 get
			{
				NativeMethods.Message msg;
				return !NativeMethods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
			 }
		}


		public MainForm(bool recover)
		{
			this.InitializeComponent();

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

			this.RequestMenu(EditorRes.GeneralRes.MenuName_File);
			this.RequestMenu(EditorRes.GeneralRes.MenuName_View);

			// Register file importers
			this.RegisterFileImporter(new PixmapFileImporter());

			DualityApp.Init(DualityApp.ExecutionContext.Editor);
			this.LoadPlugins();
			this.LoadUserData();
			this.InitPlugins();

			this.corePluginReloader = new ReloadCorePluginDialog(this);
			this.corePluginReloader.BeforeBeginReload	+= this.corePluginReloader_BeforeBeginReload;
			this.corePluginReloader.AfterEndReload		+= this.corePluginReloader_AfterEndReload;
			this.pluginWatcher.EnableRaisingEvents = true;

			Duality.Resources.Scene.Leaving += new EventHandler(this.Scene_Leaving);

			// Load initial scene
			Duality.Resources.Scene.Current = new Duality.Resources.Scene();

			this.dataDirWatcher.Path = EditorHelper.DataDirectory;
			this.sourceDirWatcher.Path = EditorHelper.SourceDirectory;
			this.sourceDirWatcher.EnableRaisingEvents = true;
			this.dataDirWatcher.EnableRaisingEvents = true;

			this.dualityAppSuspended = false;
			Application.Idle += this.Application_Idle;
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

		public void Select(object sender, ObjectSelection sel, SelectMode mode = SelectMode.Set)
		{
			if (this.selectionChanging) return;

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
			if (this.selectionChanging) return;

			this.selectionPrevious = this.selectionCurrent;
			this.selectionCurrent = this.selectionCurrent.Remove(sel);
			this.OnSelectionChanged(sender);
		}
		public void Deselect(object sender, ObjectSelection.Category category)
		{
			if (this.selectionChanging) return;

			this.selectionPrevious = this.selectionCurrent;
			this.selectionCurrent = this.selectionCurrent.Clear(category);
			this.OnSelectionChanged(sender);
		}

		public void NotifyObjPrefabApplied(object sender, ObjectSelection obj)
		{
			this.OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, new PropertyInfo[0], true));
		}
		public void NotifyObjPropChanged(object sender, ObjectSelection obj, params PropertyInfo[] info)
		{
			this.OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, info, false));
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
					pluginAssembly = Assembly.LoadFile(Path.GetFullPath(pluginDllPaths[i]));
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
						Log.Editor.WriteError("Error loading plugin '{0}'. Exception: {1}", pluginDllPaths[i], e);
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
		public T GetPlugin<T>() where T : EditorPlugin
		{
			foreach (EditorPlugin p in this.plugins)
			{
				if (p is T) return p as T;
			}
			return null;
		}

		public void RegisterFileImporter(IFileImporter importer)
		{
			this.fileImporters.Add(importer);
		}
		public void UnregisterFileImporter(IFileImporter importer)
		{
			this.fileImporters.Remove(importer);
		}
		/// <summary>
		/// Checks whether or not importing the specified file would overwrite an existing, already imported file
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public bool IsImportFileExisting(string filePath)
		{
			string srcFilePath, targetDir;
			this.PrepareImportFilePaths(filePath, out srcFilePath, out targetDir);

			// Does the source file already exist?
			if (File.Exists(srcFilePath)) return true;

			// Find an importer and check if one of its output files already exist
			foreach (IFileImporter i in this.fileImporters)
			{
				if (i.CanImportFile(srcFilePath))
				{
					foreach (string file in i.GetOutputFiles(srcFilePath, targetDir))
					{
						if (File.Exists(file)) return true;
					}
					// If we've got a hit, don't search further - ImportFile won't either.
					break;
				}
			}

			return false;
		}
		/// <summary>
		/// Imports the specified file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public bool ImportFile(string filePath)
		{
			// Determine & check paths
			string srcFilePath, targetDir;
			this.PrepareImportFilePaths(filePath, out srcFilePath, out targetDir);

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

			// Find an importer to handle the file import
			foreach (IFileImporter i in this.fileImporters)
			{
				if (i.CanImportFile(srcFilePath))
				{
					// Import it
					i.ImportFile(srcFilePath, targetDir);
					GC.Collect();
					GC.WaitForPendingFinalizers();
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Prepares file import by determining import directories from the specified file path
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="srcFilePath"></param>
		/// <param name="targetDir"></param>
		private void PrepareImportFilePaths(string filePath, out string srcFilePath, out string targetDir)
		{
			srcFilePath = PathHelper.MakePathRelative(filePath, EditorHelper.DataDirectory);
			if (srcFilePath.Contains("..")) srcFilePath = Path.GetFileName(srcFilePath);
			targetDir = Path.GetDirectoryName(Path.Combine(EditorHelper.DataDirectory, srcFilePath));
			srcFilePath = Path.Combine(EditorHelper.SourceMediaDirectory, srcFilePath);
		}

		protected bool DisplayConfirmImportOverwrite(string importFilePath)
		{
			DialogResult result = MessageBox.Show(
				String.Format(EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Text, importFilePath), 
				EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Warning);
			return result == DialogResult.Yes;
		}
		protected void DisplayErrorImportFile(string importFilePath)
		{
			MessageBox.Show(
				String.Format(EditorRes.GeneralRes.Msg_CantImport_Text, importFilePath), 
				EditorRes.GeneralRes.Msg_CantImport_Caption, 
				MessageBoxButtons.OK, 
				MessageBoxIcon.Error);
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
					Log.Editor.WriteError("Cannot load DockPanel data due to malformed or non-existent Xml: {0}", e);
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
					Log.Editor.WriteError("Cannot load plugin user data due to malformed or non-existent Xml: {0}", e);
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
					this.NotifyObjPropChanged(this, new ObjectSelection(link.Obj), ReflectionHelper.Property_GameObject_PrefabLink);
				}
			}

			// If a Resource's Properties are modified, save the Resource
			if (args.Objects.ResourceCount > 0)
			{
				// This is probably not the best idea for generalized behaviour, but sufficient for now
				foreach (Resource res in args.Objects.Resources) res.Save();
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
		private void OnResourceCreated(string path)
		{
			if (this.ResourceCreated != null)
				this.ResourceCreated(this, new ResourceEventArgs(path));
		}
		private void OnResourceDeleted(string path)
		{
			ResourceEventArgs args = new ResourceEventArgs(path);

			// Unregister no-more existing resources
			if (args.IsDirectory)	ContentProvider.UnregisterContentTree(args.Path);
			else					ContentProvider.UnregisterContent(args.Path);

			if (this.ResourceDeleted != null)
				this.ResourceDeleted(this, args);
		}
		private void OnResourceModified(string path)
		{
			ResourceEventArgs args = new ResourceEventArgs(path);

			// Unregister Content in order to force a reload from file
			//if (args.IsDirectory)
			//    ContentProvider.UnregisterContentTree(args.Path);
			//else if (Resource.SaveInProgressRes == null || Path.GetFullPath(Resource.SaveInProgressRes.Path) != Path.GetFullPath(args.Path))
			//    ContentProvider.UnregisterContent(args.Path);

			// When modifying prefabs, apply changes to all linked objects
			if (args.IsResource && args.Content.Is<Prefab>())
			{
				ContentRef<Prefab> prefabRef = args.Content.As<Prefab>();
				List<PrefabLink> appliedLinks = PrefabLink.ApplyAllLinks(Scene.Current.Graph.AllObjects, p => p.Prefab == prefabRef);
				List<GameObject> changedObjects = new List<GameObject>(appliedLinks.Select(p => p.Obj));
				this.NotifyObjPrefabApplied(this, new ObjectSelection(changedObjects));
			}

			if (this.ResourceModified != null) this.ResourceModified(this, args);
		}
		private void OnResourceRenamed(string path, string oldPath)
		{
			ResourceRenamedEventArgs args = new ResourceRenamedEventArgs(path, oldPath);

			// Rename content registerations
			if (args.IsDirectory)	ContentProvider.RenameContentTree(args.OldPath, args.Path);
			else					ContentProvider.RenameContent(args.OldPath, args.Path);

			// If we just renamed the currently loaded scene, relocate it
			if (Scene.CurrentPath == oldPath) Scene.Current = Resource.LoadResource<Scene>(path);

			if (this.ResourceRenamed != null) this.ResourceRenamed(this, args);
		}

		public bool ConfirmBreakPrefabLink(ObjectSelection obj = null)
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
			this.NotifyObjPropChanged(this, obj, ReflectionHelper.Property_GameObject_PrefabLink);

			return true;
		}
		public bool PushPrefabLinkPropertyChange(PrefabLink link, object target, PropertyInfo info)
		{
			if (link == null) return false;

			if (info == ReflectionHelper.Property_GameObject_PrefabLink)
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

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (!e.Cancel)
			{
				this.SaveUserData();
			}
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			DualityApp.Terminate();
		}
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this.needsRecovery)
			{
				this.needsRecovery = false;
				Log.Editor.Write("Recovering from full plugin reload restart...");
				Log.Editor.PushIndent();
				this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.RecoverFromRestart;
			}
			else
			{
				if (this.corePluginReloader.State == ReloadCorePluginDialog.ReloaderState.WaitForPlugins)
					this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.ReloadPlugins;
			}
		}

		private void corePluginWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			this.OnBeforeReloadCorePlugins();

			string pluginStr = Path.Combine("Plugins", e.Name);
			if (!this.corePluginReloader.ReloadSchedule.Contains(pluginStr))
				this.corePluginReloader.ReloadSchedule.Add(pluginStr);
			this.corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.WaitForPlugins;

			this.OnAfterReloadCorePlugins();
		}
		private void corePluginReloader_AfterEndReload(object sender, EventArgs e)
		{
			this.OnAfterReloadCorePlugins();
		}
		private void corePluginReloader_BeforeBeginReload(object sender, EventArgs e)
		{
			this.OnBeforeReloadCorePlugins();
		}
		
		private void dataDirWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			this.OnResourceModified(e.FullPath);
		}
		private void dataDirWatcher_Created(object sender, FileSystemEventArgs e)
		{
			if (File.Exists(e.FullPath))
			{
				// Register newly detected ressource file
				if (Path.GetExtension(e.FullPath).ToLower() == ".res")
				{
					this.OnResourceCreated(e.FullPath);
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

					if (abort && File.Exists(e.FullPath)) File.Delete(e.FullPath);
				}
			}
			else if (Directory.Exists(e.FullPath))
			{
				// Register newly detected ressource directory
				this.OnResourceCreated(e.FullPath);
			}
		}
		private void dataDirWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			this.OnResourceDeleted(e.FullPath);
		}
		private void dataDirWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			this.OnResourceRenamed(e.FullPath, e.OldFullPath);
		}

		private void sourceDirWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			// Might auto-check for ressource files being based on the changed file and ask whether or not to reload it
		}
		private void sourceDirWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			// Might auto-check for ressource files being based on the changed file and ask whether or not to lose connection
		}
		private void sourceDirWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			// Might auto-check for ressource files being based on the changed file and ask whether or not to update the connection
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			while (this.AppStillIdle)
			{
				watch.Restart();
				if (!this.dualityAppSuspended)
				{
					this.OnBeforeUpdateDualityApp();
					DualityApp.EditorUpdate(this.editorObjects);
					this.OnAfterUpdateDualityApp();
				}
				System.Threading.Thread.Sleep(Math.Max(1, 17 - (int)watch.ElapsedMilliseconds));
			}
		}
		private void Scene_Leaving(object sender, EventArgs e)
		{
			this.Deselect(this, ObjectSelection.Category.All);
		}
	}
}
