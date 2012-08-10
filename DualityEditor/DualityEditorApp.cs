using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Text.RegularExpressions;

using Duality;
using Duality.Resources;
using Duality.ObjectManagers;

using DualityEditor.Forms;
using DualityEditor.CorePluginInterface;

using OpenTK;

using Ionic.Zip;
using WeifenLuo.WinFormsUI.Docking;

namespace DualityEditor
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

	public static class DualityEditorApp
	{
		private	const	string	UserDataFile			= "editoruserdata.xml";
		private	const	string	UserDataDockSeparator	= "<!-- DockPanel Data -->";
		
		private	static MainForm						mainForm			= null;
		private	static GLControl					mainContextControl	= null;
		private	static List<EditorPlugin>			plugins				= new List<EditorPlugin>();
		private	static Dictionary<Type,List<Type>>	availTypeDict	= new Dictionary<Type,List<Type>>();
		private	static ReloadCorePluginDialog		corePluginReloader	= null;
		private	static bool							needsRecovery		= false;
		private	static Control						hoveredControl		= null;
		private	static IHelpProvider				hoveredHelpProvider	= null;
		private	static bool							hoveredHelpCaptured	= false;
		private	static GameObjectManager			editorObjects		= new GameObjectManager();
		private	static bool							dualityAppSuspended	= true;
		private	static bool							sandboxStateChange	= false;
		private	static bool							sandboxSceneFreeze	= false;
		private	static List<Resource>				unsavedResources	= new List<Resource>();
		
		private static FileSystemWatcher			pluginWatcher			= null;
		private static FileSystemWatcher			dataDirWatcher			= null;
		private static FileSystemWatcher			sourceDirWatcher		= null;
		private	static HashSet<string>				reimportSchedule		= new HashSet<string>();
		private	static List<string>					editorJustSavedRes		= new List<string>();
		private	static List<FileSystemEventArgs>	dataDirEventBuffer		= new List<FileSystemEventArgs>();
		private	static List<FileSystemEventArgs>	sourceDirEventBuffer	= new List<FileSystemEventArgs>();

		private	static HelpStack		helpStack			= new HelpStack();
		private	static bool				needHelpStackUpdate	= false;
		private	static SandboxState		sandboxState		= SandboxState.Inactive;
		private	static ObjectSelection	selectionCurrent	= ObjectSelection.Null;
		private	static ObjectSelection	selectionPrevious	= ObjectSelection.Null;
		private	static bool				selectionChanging	= false;


		public	static	event	EventHandler	Terminating						= null;
		public	static	event	EventHandler	BeforeReloadCorePlugins			= null;
		public	static	event	EventHandler	AfterReloadCorePlugins			= null;
		public	static	event	EventHandler	BeforeUpdateDualityApp			= null;
		public	static	event	EventHandler	AfterUpdateDualityApp			= null;
		public	static	event	EventHandler	SaveAllProjectDataTriggered		= null;
		public	static	event	EventHandler	EnteringSandbox					= null;
		public	static	event	EventHandler	LeaveSandbox					= null;
		public	static	event	EventHandler	PausedSandbox					= null;
		public	static	event	EventHandler	UnpausingSandbox				= null;
		public	static	event	EventHandler	SandboxStateChanged				= null;
		public	static	event	EventHandler<SelectionChangedEventArgs>			SelectionChanged		= null;
		public	static	event	EventHandler<ObjectPropertyChangedEventArgs>	ObjectPropertyChanged	= null;
		public	static	event	EventHandler<ResourceEventArgs>					ResourceCreated			= null;
		public	static	event	EventHandler<ResourceEventArgs>					ResourceDeleted			= null;
		public	static	event	EventHandler<ResourceEventArgs>					ResourceModified		= null;
		public	static	event	EventHandler<ResourceRenamedEventArgs>			ResourceRenamed			= null;
		public	static	event	EventHandler<FileSystemEventArgs>				SrcFileDeleted			= null;
		public	static	event	EventHandler<FileSystemEventArgs>				SrcFileModified			= null;
		public	static	event	EventHandler<FileSystemEventArgs>				SrcFileRenamed			= null;
		

		public static MainForm MainForm
		{
			get { return mainForm; }
		}
		public static GameObjectManager EditorObjects
		{
			get { return editorObjects; }
		}
		public static ObjectSelection Selection
		{
			get { return selectionCurrent; }
		}
		public static bool IsSelectionChanging
		{
			get { return selectionChanging; }
		}
		public static HelpStack Help
		{
			get { return helpStack; }
		}
		public static GLControl MainContextControl
		{
			get { return mainContextControl; }
		}
		public static SandboxState SandboxState
		{
			get { return sandboxState; }
		}
		public static IEnumerable<EditorPlugin> Plugins
		{
			get { return plugins; }
		}
		public static IEnumerable<Resource> UnsavedResources
		{
			get { return unsavedResources.Where(r => !r.Disposed && !r.IsDefaultContent && !string.IsNullOrEmpty(r.Path)); }
		}
		private static bool AppStillIdle
		{
			 get
			{
				NativeMethods.Message msg;
				return !NativeMethods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
			 }
		}
		
		
		public static void Init(MainForm mainForm, bool recover)
		{
			DualityEditorApp.needsRecovery = recover;
			DualityEditorApp.mainForm = mainForm;

			if (!Directory.Exists(EditorHelper.DataDirectory))
			{
				Directory.CreateDirectory(EditorHelper.DataDirectory);
				using (FileStream s = File.OpenWrite(Path.Combine(EditorHelper.DataDirectory, "WorkingFolderIcon.ico")))
				{
					EditorRes.GeneralRes.IconWorkingFolder.Save(s);
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

			DualityApp.Init(DualityApp.ExecutionEnvironment.Editor, DualityApp.ExecutionContext.Editor, new[] {"logfile", "logfile_editor"});
			InitMainGLContext();
			ContentProvider.InitDefaultContent();
			LoadXmlCodeDoc();
			LoadPlugins();
			LoadUserData();
			InitPlugins();

			corePluginReloader = new ReloadCorePluginDialog(mainForm);
			corePluginReloader.BeforeBeginReload	+= corePluginReloader_BeforeBeginReload;
			corePluginReloader.AfterEndReload		+= corePluginReloader_AfterEndReload;

			Scene.Leaving += Scene_Leaving;
			Scene.Entered += Scene_Entered;
			Scene.Current = new Scene();
			
			pluginWatcher = new FileSystemWatcher();
			pluginWatcher.SynchronizingObject = mainForm;
			pluginWatcher.EnableRaisingEvents = false;
			pluginWatcher.Filter = "*.dll";
			pluginWatcher.IncludeSubdirectories = true;
			pluginWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			pluginWatcher.Path = EditorHelper.PluginDirectory;
			pluginWatcher.Changed += new FileSystemEventHandler(corePluginWatcher_Changed);
			pluginWatcher.Created += new FileSystemEventHandler(corePluginWatcher_Changed);
			pluginWatcher.EnableRaisingEvents = true;
			
			dataDirWatcher = new FileSystemWatcher();
			dataDirWatcher.SynchronizingObject = mainForm;
			dataDirWatcher.EnableRaisingEvents = false;
			dataDirWatcher.IncludeSubdirectories = true;
			dataDirWatcher.Path = EditorHelper.DataDirectory;
			dataDirWatcher.Created += delegate(object sender, FileSystemEventArgs e) { PushDataDirEvent(e); };
			dataDirWatcher.Changed += delegate(object sender, FileSystemEventArgs e) { PushDataDirEvent(e); };
			dataDirWatcher.Deleted += delegate(object sender, FileSystemEventArgs e) { PushDataDirEvent(e); };
			dataDirWatcher.Renamed += delegate(object sender, RenamedEventArgs e) { PushDataDirEvent(e); };
			dataDirWatcher.EnableRaisingEvents = true;
			
			sourceDirWatcher = new FileSystemWatcher();
			sourceDirWatcher.SynchronizingObject = mainForm;
			sourceDirWatcher.EnableRaisingEvents = false;
			sourceDirWatcher.IncludeSubdirectories = true;
			sourceDirWatcher.Path = EditorHelper.SourceDirectory;
			sourceDirWatcher.Created += delegate(object sender, FileSystemEventArgs e) { PushSourceDirEvent(e); };
			sourceDirWatcher.Changed += delegate(object sender, FileSystemEventArgs e) { PushSourceDirEvent(e); };
			sourceDirWatcher.Deleted += delegate(object sender, FileSystemEventArgs e) { PushSourceDirEvent(e); };
			sourceDirWatcher.Renamed += delegate(object sender, RenamedEventArgs e) { PushSourceDirEvent(e); };
			sourceDirWatcher.EnableRaisingEvents = true;

			dualityAppSuspended = false;
			Application.Idle += Application_Idle;
			Resource.ResourceSaved += Resource_ResourceSaved;

			// Hook message filter
			InputEventMessageFilter inputFilter = new InputEventMessageFilter();
			inputFilter.MouseMove += inputFilter_MouseMove;
			inputFilter.MouseLeave += inputFilter_MouseLeave;
			inputFilter.KeyDown += inputFilter_KeyDown;
			inputFilter.MouseUp += inputFilter_MouseUp;
			Application.AddMessageFilter(inputFilter);

			mainForm.Activated += mainForm_Activated;
			mainForm.Deactivate += mainForm_Deactivate;
		}
		public static bool Terminate(bool byUser)
		{
			bool cancel = false;

			// Safety messageboxes are only displayed when the close operation is triggered by the user.
			if (byUser)
			{
				var unsavedResTemp = DualityEditorApp.UnsavedResources.ToArray();
				if (unsavedResTemp.Any())
				{
					string unsavedResText = unsavedResTemp.Take(5).ToString(r => r.GetType().GetTypeCSCodeName(true) + ":\t" + r.FullName, "\n");
					if (unsavedResTemp.Count() > 5) 
						unsavedResText += "\n" + string.Format(EditorRes.GeneralRes.Msg_ConfirmQuitUnsaved_Desc_More, unsavedResTemp.Count() - 5);
					DialogResult result = MessageBox.Show(
						string.Format(EditorRes.GeneralRes.Msg_ConfirmQuitUnsaved_Desc, "\n\n" + unsavedResText + "\n\n"), 
						EditorRes.GeneralRes.Msg_ConfirmQuitUnsaved_Caption, 
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
					if (result == DialogResult.Yes)
					{
						DualityEditorApp.SandboxStop();
						DualityEditorApp.SaveAllProjectData();
					}
					else if (result == DialogResult.Cancel)
						cancel = true;
				}
				else
				{
					DialogResult result = MessageBox.Show(
						EditorRes.GeneralRes.Msg_ConfirmQuit_Desc, 
						EditorRes.GeneralRes.Msg_ConfirmQuit_Caption, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.No)
						cancel = true;
				}
			}

			if (!cancel)
			{
				if (Terminating != null) Terminating(null, EventArgs.Empty);
				DualityEditorApp.SandboxStop();
				DualityEditorApp.SaveUserData();
				DualityApp.Terminate();
			}

			return !cancel;
		}

		private static void LoadPlugins()
		{
			CorePluginRegistry.RegisterPropertyEditorProvider(new Controls.PropertyEditors.DualityPropertyEditorProvider());

			Log.Editor.Write("Scanning for editor plugins...");
			Log.Editor.PushIndent();

			if (Directory.Exists("Plugins"))
			{
				string[] pluginDllPaths = Directory.GetFiles("Plugins", "*.editor.dll", SearchOption.AllDirectories);
				foreach (string dllPath in pluginDllPaths)
				{
					Log.Editor.Write("Loading '{0}'...", dllPath);
					Log.Editor.PushIndent();
					Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(dllPath));
					Type[] exportedTypes = pluginAssembly.GetExportedTypes();
					try
					{
						// Initialize plugin objects
						for (int j = 0; j < exportedTypes.Length; j++)
						{
							if (typeof(EditorPlugin).IsAssignableFrom(exportedTypes[j]))
							{
								Log.Editor.Write("Instantiating class '{0}'...", exportedTypes[j].Name);
								EditorPlugin plugin = (EditorPlugin)exportedTypes[j].CreateInstanceOf();
								plugin.LoadPlugin();
								plugins.Add(plugin);
							}
						}
					}
					catch (Exception e)
					{
						Log.Editor.WriteError("Error loading plugin '{0}'. Exception: {1}", dllPath, Log.Exception(e));
					}
					Log.Editor.PopIndent();
				}
			}

			Log.Editor.PopIndent();
		}
		private static void InitPlugins()
		{
			Log.Editor.Write("Initializing editor plugins...");
			Log.Editor.PushIndent();
			foreach (EditorPlugin plugin in plugins)
			{
				Log.Editor.Write("'{0}'...", plugin.Id);
				plugin.InitPlugin(mainForm);
			}
			Log.Editor.PopIndent();
		}
		
		public static IEnumerable<Assembly> GetDualityEditorAssemblies()
		{
			yield return typeof(MainForm).Assembly;
			foreach (Assembly a in plugins.Select(ep => ep.GetType().Assembly)) yield return a;
		}
		public static IEnumerable<Type> GetAvailDualityEditorTypes(Type baseType)
		{
			List<Type> availTypes;
			if (availTypeDict.TryGetValue(baseType, out availTypes)) return availTypes;

			availTypes = new List<Type>();
			IEnumerable<Assembly> asmQuery = GetDualityEditorAssemblies();
			foreach (Assembly asm in asmQuery)
			{
				availTypes.AddRange(
					from t in asm.GetExportedTypes()
					where baseType.IsAssignableFrom(t)
					orderby t.Name
					select t);
			}
			availTypeDict[baseType] = availTypes;

			return availTypes;
		}

		private static void SaveUserData()
		{
			Log.Editor.Write("Saving user data...");
			Log.Editor.PushIndent();

			using (FileStream str = File.Create(UserDataFile))
			{
				StreamWriter writer = new StreamWriter(str);
				// --- Save custom user data here ---
				XmlDocument xmlDoc = new XmlDocument();
				XmlElement rootElement = xmlDoc.CreateElement("PluginUserData");
				xmlDoc.AppendChild(rootElement);
				foreach (EditorPlugin plugin in plugins)
				{
					XmlElement pluginXmlElement = xmlDoc.CreateElement("Plugin_" + plugin.Id);
					rootElement.AppendChild(pluginXmlElement);
					plugin.SaveUserData(pluginXmlElement);
				}
				xmlDoc.Save(writer.BaseStream);
				// ----------------------------------
				writer.WriteLine();
				writer.WriteLine(UserDataDockSeparator);
				writer.Flush();
				mainForm.MainDockPanel.SaveAsXml(str, writer.Encoding);
			}

			Log.Editor.PopIndent();
		}
		private static void LoadUserData()
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
					mainForm.MainDockPanel.LoadFromXml(dockPanelDataStream, DeserializeDockContent);
				}
				catch (XmlException e)
				{
					Log.Editor.WriteError("Cannot load DockPanel data due to malformed or non-existent Xml: {0}", Log.Exception(e));
				}
				Log.Editor.PopIndent();

				// --- Read custom user data from StringBuilder here ---
				Log.Editor.Write("Loading plugin user data...");
				Log.Editor.PushIndent();
				XmlDocument xmlDoc = new XmlDocument();
				try
				{
					xmlDoc.LoadXml(editorData.ToString());
					foreach (XmlElement child in xmlDoc.DocumentElement)
					{
						if (child.Name.StartsWith("Plugin_"))
						{
							string pluginName = child.Name.Substring(7, child.Name.Length - 7);
							foreach (EditorPlugin plugin in plugins)
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
				catch (XmlException e)
				{
					Log.Editor.WriteError("Cannot load plugin user data due to malformed or non-existent Xml: {0}", Log.Exception(e));
				}
				Log.Editor.PopIndent();
				// -----------------------------------------------------
			}

			Log.Editor.PopIndent();
		}
		private static IDockContent DeserializeDockContent(string persistName)
		{
			Log.Editor.Write("Deserializing layout: '" + persistName + "'");

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
				foreach (EditorPlugin plugin in plugins)
				{
					if (plugin.GetType().Assembly == dockContentAssembly)
					{
						deserializeDockContent = plugin.DeserializeDockContent(dockContentType);
						if (deserializeDockContent != null) break;
					}
				}

				// If none exists, create one
				return deserializeDockContent ?? (dockContentType.CreateInstanceOf() as IDockContent);
			}
		}

		public static void InitMainGLContext()
		{
			if (mainContextControl != null) return;
			mainContextControl = new GLControl(DualityApp.DefaultMode);
			mainContextControl.MakeCurrent();
			DualityApp.TargetMode = mainContextControl.Context.GraphicsMode;
		}

		public static void SandboxPlay()
		{
			if (sandboxState == SandboxState.Playing) return;
			sandboxStateChange = true;
			if (sandboxState == SandboxState.Paused)
			{
				OnUnpausingSandbox();
				sandboxState = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;
			}
			else
			{
				OnEnteringSandbox();

				// Save the current scene
				SaveCurrentScene();
				
				// Force later Scene reload by disposing it
				string curPath = null;
				if (!String.IsNullOrEmpty(Scene.Current.Path))
				{
					curPath = Scene.CurrentPath;
					Scene.Current.Dispose();
				}

				sandboxState = SandboxState.Playing;
				DualityApp.ExecContext = DualityApp.ExecutionContext.Game;

				// (Re)Load Scene.
				if (curPath != null)
					Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;
			}

			OnSandboxStateChanged();
			sandboxStateChange = false;
		}
		public static void SandboxPause()
		{
			if (sandboxState == SandboxState.Paused) return;
			sandboxStateChange = true;

			sandboxState = SandboxState.Paused;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;

			OnPausedSandbox();
			OnSandboxStateChanged();
			sandboxStateChange = false;
		}
		public static void SandboxStop()
		{
			if (sandboxState == SandboxState.Inactive) return;
			sandboxStateChange = true;

			// Force later Scene reload by disposing it
			string curPath = null;
			if (!String.IsNullOrEmpty(Scene.Current.Path))
			{
				curPath = Scene.CurrentPath;
				Scene.Current.Dispose();
			}

			sandboxState = SandboxState.Inactive;
			DualityApp.ExecContext = DualityApp.ExecutionContext.Editor;
			
			// (Re)Load Scene
			if (curPath != null)
				Scene.Current = ContentProvider.RequestContent<Scene>(curPath).Res;

			OnLeaveSandbox();
			OnSandboxStateChanged();
			sandboxStateChange = false;
		}
		private static void SandboxLeaveScene()
		{
			if (sandboxStateChange) return;
			//if (corePluginReloader.IsReloadingPlugins) return;

			// Force later Scene reload by disposing it
			string curPath = null;
			if (!String.IsNullOrEmpty(Scene.Current.Path))
			{
				curPath = Scene.CurrentPath;
				Scene.Current.Dispose();
			}
		}
		public static void SandboxSceneStartFreeze()
		{
			sandboxSceneFreeze = true;
		}
		public static void SandboxSceneStopFreeze()
		{
			sandboxSceneFreeze = false;
		}
		public static void SetCurrentDualityAppInput(IMouseInput mouse, IKeyboardInput keyboard)
		{
			DualityApp.Mouse = mouse;
			DualityApp.Keyboard = keyboard;
		}

		public static void Select(object sender, ObjectSelection sel, SelectMode mode = SelectMode.Set)
		{
			selectionPrevious = selectionCurrent;
			if (mode == SelectMode.Set)
				selectionCurrent = selectionCurrent.Transform(sel);
			else if (mode == SelectMode.Append)
				selectionCurrent = selectionCurrent.Append(sel);
			else if (mode == SelectMode.Toggle)
				selectionCurrent = selectionCurrent.Toggle(sel);
			OnSelectionChanged(sender, sel.Categories);
		}
		public static void Deselect(object sender, ObjectSelection sel)
		{
			selectionPrevious = selectionCurrent;
			selectionCurrent = selectionCurrent.Remove(sel);
			OnSelectionChanged(sender, ObjectSelection.Category.None);
		}
		public static void Deselect(object sender, ObjectSelection.Category category)
		{
			selectionPrevious = selectionCurrent;
			selectionCurrent = selectionCurrent.Clear(category);
			OnSelectionChanged(sender, ObjectSelection.Category.None);
		}
		public static void Deselect(object sender, Predicate<object> predicate)
		{
			selectionPrevious = selectionCurrent;
			selectionCurrent = selectionCurrent.Clear(predicate);
			OnSelectionChanged(sender, ObjectSelection.Category.None);
		}

		public static void SaveCurrentScene(bool skipYetUnsaved = true)
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
		public static void SaveResources()
		{
			foreach (Resource res in UnsavedResources.ToArray()) // The Property does some safety checks
			{
				if (res == Scene.Current && sandboxState != SandboxState.Inactive) continue;
				res.Save();
			}
			unsavedResources.Clear();
		}
		public static void FlagResourceUnsaved(Resource res)
		{
			if (unsavedResources.Contains(res)) return;
			unsavedResources.Add(res);
		}
		public static void FlagResourceSaved(Resource res)
		{
			unsavedResources.Remove(res);
		}
		public static bool IsResourceUnsaved(Resource res)
		{
			return UnsavedResources.Contains(res);
		}
		public static bool IsResourceUnsaved(IContentRef res)
		{
			return res.ResWeak != null ? IsResourceUnsaved(res.ResWeak) : IsResourceUnsaved(res.Path);
		}
		public static bool IsResourceUnsaved(string resPath)
		{
			return UnsavedResources.Any(r => Path.GetFullPath(r.Path) == Path.GetFullPath(resPath));
		}
		public static void SaveAllProjectData()
		{
			if (!IsResourceUnsaved(Scene.Current) && sandboxState == SandboxState.Inactive) SaveCurrentScene();
			SaveResources();

			if (SaveAllProjectDataTriggered != null)
				SaveAllProjectDataTriggered(null, EventArgs.Empty);
		}
		
		private static void UpdateHelpStack()
		{
			foreach (Form f in EditorHelper.GetZSortedAppWindows())
			{
				if (!f.Visible) continue;
				if (!new Rectangle(f.Location, f.Size).Contains(Cursor.Position)) continue;

				Point localPos = f.PointToClient(Cursor.Position);
				hoveredControl = f.GetChildAtPointDeep(localPos, GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Transparent);
				break;
			}

			Control c;
			HelpInfo help;

			// An IHelpProvider has captured the mouse: Ask what to do with it.
			if (hoveredHelpCaptured)
			{
				c = hoveredHelpProvider as Control;
				help = hoveredHelpProvider.ProvideHoverHelp(c.PointToClient(Cursor.Position), ref hoveredHelpCaptured);

				// Update provider's help info
				Help.UpdateFromProvider(hoveredHelpProvider, help);

				// If still in charge: Return early.
				if (hoveredHelpCaptured) return;
			}

			// No IHelpProvider in charge: Find one that provides help
			help = null;
			IHelpProvider lastHelpProvider = hoveredHelpProvider;
			foreach (IHelpProvider hp in hoveredControl.GetControlAncestors<IHelpProvider>())
			{
				c = hp as Control;
				help = hp.ProvideHoverHelp(c.PointToClient(Cursor.Position), ref hoveredHelpCaptured);
				hoveredHelpProvider = hp;
				if (help != null || hoveredHelpCaptured) break;
			}

			// Update help system based on the result.
			if (lastHelpProvider != hoveredHelpProvider)
				Help.UpdateFromProvider(lastHelpProvider, hoveredHelpProvider, help);
			else if (hoveredHelpProvider != null)
				Help.UpdateFromProvider(hoveredHelpProvider, help);
		}
		private static bool PerformHelpAction()
		{
			bool success = false;

			// Ask Help Provider for help
			if (Help.ActiveHelpProvider != null)
			{
				success = success | Help.ActiveHelpProvider.PerformHelpAction(Help.ActiveHelp);
			}

			// No reaction? Just open the reference then.
			if (!success && File.Exists("DDoc.chm") && !System.Diagnostics.Process.GetProcessesByName("hh").Any())
			{
				System.Diagnostics.Process.Start("HH.exe", Path.GetFullPath("DDoc.chm"));
				success = true;
			}

			return success;
		}

		public static void UpdatePluginSourceCode()
		{
			// Initially generate source code, if not existing yet
			if (!File.Exists(EditorHelper.SourceCodeSolutionFile)) InitPluginSourceCode();
			
			// Replace exec path in user files, since VS doesn't support relative paths there..
			{
				XmlDocument userDoc;
				const string userFileCore = EditorHelper.SourceCodeProjectCorePluginFile + ".user";
				const string userFileEditor = EditorHelper.SourceCodeProjectEditorPluginFile + ".user";

				if (File.Exists(userFileCore))
				{
					userDoc = new XmlDocument();
					userDoc.Load(userFileCore);
					foreach (XmlElement element in userDoc.GetElementsByTagName("StartProgram").OfType<XmlElement>())
						element.InnerText = Path.GetFullPath("DualityLauncher.exe");
					foreach (XmlElement element in userDoc.GetElementsByTagName("StartWorkingDirectory").OfType<XmlElement>())
						element.InnerText = Path.GetFullPath(".");
					userDoc.Save(userFileCore);
				}
				
				if (File.Exists(userFileEditor))
				{
					userDoc = new XmlDocument();
					userDoc.Load(userFileEditor);
					foreach (XmlElement element in userDoc.GetElementsByTagName("StartProgram").OfType<XmlElement>())
						element.InnerText = Path.GetFullPath("DualityEditor.exe");
					foreach (XmlElement element in userDoc.GetElementsByTagName("StartWorkingDirectory").OfType<XmlElement>())
						element.InnerText = Path.GetFullPath(".");
					userDoc.Save(userFileEditor);
				}
			}

			// Keep auto-generated files up-to-date
			File.WriteAllText(EditorHelper.SourceCodeGameResFile, EditorHelper.GenerateGameResSrcFile());
		}
		public static void ReadPluginSourceCodeContentData(out string rootNamespace, out string desiredRootNamespace)
		{
			rootNamespace = null;
			desiredRootNamespace = EditorHelper.GenerateClassNameFromPath(EditorHelper.CurrentProjectName);

			// Read root namespaces
			if (File.Exists(EditorHelper.SourceCodeProjectCorePluginFile))
			{
				XmlDocument projXml = new XmlDocument();
				projXml.Load(EditorHelper.SourceCodeProjectCorePluginFile);
				foreach (XmlElement element in projXml.GetElementsByTagName("RootNamespace").OfType<XmlElement>())
				{
					if (rootNamespace == null) rootNamespace = element.InnerText;
				}
			}
		}
		public static void InitPluginSourceCode()
		{
			// Create solution file if not existing yet
			if (!File.Exists(EditorHelper.SourceCodeSolutionFile))
			{
				using (ZipFile gamePluginZip = ZipFile.Read(ReflectionHelper.GetEmbeddedResourceStream(typeof(MainForm).Assembly,  @"Resources\GamePluginTemplate.zip")))
				{
					gamePluginZip.ExtractAll(EditorHelper.SourceCodeDirectory, ExtractExistingFileAction.DoNotOverwrite);
				}
			}

			// If Visual Studio is available, don't use the express version
			if (File.Exists(EditorHelper.SourceCodeSolutionFile) && EditorHelper.IsJITDebuggerAvailable())
			{
				string solution = File.ReadAllText(EditorHelper.SourceCodeSolutionFile);
				File.WriteAllText(EditorHelper.SourceCodeSolutionFile, solution.Replace("# Visual C# Express 2010", "# Visual Studio 2010"), Encoding.UTF8);
			}
			
			string projectClassName = EditorHelper.GenerateClassNameFromPath(EditorHelper.CurrentProjectName);
			string newRootNamespaceCore = projectClassName;
			string newRootNamespaceEditor = newRootNamespaceCore + ".Editor";
			string pluginNameCore = projectClassName + "CorePlugin";
			string pluginNameEditor = projectClassName + "EditorPlugin";
			string oldRootNamespaceCore = null;
			string oldRootNamespaceEditor = null;

			// Update root namespaces
			if (File.Exists(EditorHelper.SourceCodeProjectCorePluginFile))
			{
				XmlDocument projXml = new XmlDocument();
				projXml.Load(EditorHelper.SourceCodeProjectCorePluginFile);
				foreach (XmlElement element in projXml.GetElementsByTagName("RootNamespace").OfType<XmlElement>())
				{
					if (oldRootNamespaceCore == null) oldRootNamespaceCore = element.InnerText;
					element.InnerText = newRootNamespaceCore;
				}
				projXml.Save(EditorHelper.SourceCodeProjectCorePluginFile);
			}

			if (File.Exists(EditorHelper.SourceCodeProjectEditorPluginFile))
			{
				XmlDocument projXml = new XmlDocument();
				projXml.Load(EditorHelper.SourceCodeProjectEditorPluginFile);
				foreach (XmlElement element in projXml.GetElementsByTagName("RootNamespace").OfType<XmlElement>())
				{
					if (oldRootNamespaceEditor == null) oldRootNamespaceEditor = element.InnerText;
					element.InnerText = newRootNamespaceEditor;
				}
				projXml.Save(EditorHelper.SourceCodeProjectEditorPluginFile);
			}

			// Guess old plugin class names
			string oldPluginNameCore = oldRootNamespaceCore + "CorePlugin";
			string oldPluginNameEditor = oldRootNamespaceCore + "EditorPlugin";
			string regExpr;
			string regExprReplace;

			// Replace namespace names: Core
			if (Directory.Exists(EditorHelper.SourceCodeProjectCorePluginDir))
			{
				regExpr = @"^(\s*namespace\s*)(.*)(" + oldRootNamespaceCore + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + newRootNamespaceCore + @"$4$5";
				foreach (string filePath in Directory.GetFiles(EditorHelper.SourceCodeProjectCorePluginDir, "*.cs", SearchOption.AllDirectories))
				{
					string fileContent = File.ReadAllText(filePath);
					fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);
					File.WriteAllText(filePath, fileContent, Encoding.UTF8);
				}
			}

			// Replace namespace names: Editor
			if (Directory.Exists(EditorHelper.SourceCodeProjectEditorPluginDir))
			{
				regExpr = @"^(\s*namespace\s*)(.*)(" + oldRootNamespaceEditor + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + newRootNamespaceEditor + @"$4$5";
				foreach (string filePath in Directory.GetFiles(EditorHelper.SourceCodeProjectEditorPluginDir, "*.cs", SearchOption.AllDirectories))
				{
					string fileContent = File.ReadAllText(filePath);
					fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);
					File.WriteAllText(filePath, fileContent, Encoding.UTF8);
				}
			}

			// Replace class names: Core
			if (File.Exists(EditorHelper.SourceCodeCorePluginFile))
			{
				string fileContent = File.ReadAllText(EditorHelper.SourceCodeCorePluginFile);

				// Replace class name
				regExpr = @"(\bclass\b)(.*)(" + oldPluginNameCore + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + pluginNameCore + @"$4$5";
				fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);

				regExpr = @"(\bclass\b)(.*)(" + @"__CorePluginClassName__" + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + pluginNameCore + @"$4$5";
				fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);

				File.WriteAllText(EditorHelper.SourceCodeCorePluginFile, fileContent, Encoding.UTF8);
			}

			// Replace class names: Editor
			if (File.Exists(EditorHelper.SourceCodeEditorPluginFile))
			{
				string fileContent = File.ReadAllText(EditorHelper.SourceCodeEditorPluginFile);

				// Replace class name
				regExpr = @"(\bclass\b)(.*)(" + oldPluginNameEditor + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + pluginNameEditor + @"$4$5";
				fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);

				regExpr = @"(\bclass\b)(.*)(" + @"__EditorPluginClassName__" + @")(.*)(\s*{)";
				regExprReplace = @"$1$2" + pluginNameEditor + @"$4$5";
				fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);
				
				// Repalce Id property
				regExpr = @"(\boverride\s*string\s*Id\s*{\s*get\s*{\s*return\s*" + '"' + @")(.*)(" + '"' + @"\s*;\s*}\s*})";
				regExprReplace = @"$1" + pluginNameEditor + @"$3";
				fileContent = Regex.Replace(fileContent, regExpr, regExprReplace, RegexOptions.Multiline);

				File.WriteAllText(EditorHelper.SourceCodeEditorPluginFile, fileContent, Encoding.UTF8);
			}
		}

		public static void NotifyObjPrefabApplied(object sender, ObjectSelection obj)
		{
			OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, new PropertyInfo[0], true));
		}
		public static void NotifyObjPropChanged(object sender, ObjectSelection obj, params PropertyInfo[] info)
		{
			OnObjectPropertyChanged(sender, new ObjectPropertyChangedEventArgs(obj, info, false));
		}

		public static T GetPlugin<T>() where T : EditorPlugin
		{
			return plugins.OfType<T>().FirstOrDefault();
		}

		public static void LoadXmlCodeDoc()
		{
			LoadXmlCodeDoc("Duality.xml");
			foreach (string xmlDocFile in Directory.EnumerateFiles("Plugins", "*.core.xml", SearchOption.AllDirectories))
				LoadXmlCodeDoc(xmlDocFile);
		}
		public static void LoadXmlCodeDoc(string file)
		{
			XmlCodeDoc xmlDoc = new XmlCodeDoc(file);
			CorePluginRegistry.RegisterXmlCodeDoc(xmlDoc);
		}

		public static bool DisplayConfirmBreakPrefabLink(ObjectSelection obj = null)
		{
			if (obj == null) obj = DualityEditorApp.Selection;

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
			DualityEditorApp.NotifyObjPropChanged(null, 
				new ObjectSelection(linkList.Select(l => l.Obj)), 
				ReflectionInfo.Property_GameObject_PrefabLink);

			return true;
		}

		private static bool IsResPathIgnored(string filePath)
		{
			return IsPathIgnored(filePath);
		}
		private static bool IsSourcePathIgnored(string filePath)
		{
			return IsPathIgnored(filePath);
		}
		private static bool IsPathIgnored(string filePath)
		{
			if (!File.Exists(filePath) && !Directory.Exists(filePath)) return false;
			if (!PathHelper.IsPathVisible(filePath)) return true;
			if (filePath.Contains(@"/.svn/") || filePath.Contains(@"\.svn\")) return true;
			return false;
		}
		
		private static FileSystemEventArgs FetchFileSystemEvent(List<FileSystemEventArgs> dirEventList, string basePath)
		{
			if (dirEventList.Count == 0) return null;

			FileSystemEventArgs	current	= dirEventList[0];
			dirEventList.RemoveAt(0);

			// Discard or pack rename-rename
			if (current.ChangeType == WatcherChangeTypes.Renamed)
			{
				RenamedEventArgs rename = current as RenamedEventArgs;

				while (rename != null)
				{
					RenamedEventArgs renameB = dirEventList.OfType<RenamedEventArgs>().FirstOrDefault(e => 
						Path.GetFileName(e.OldFullPath) == Path.GetFileName(rename.FullPath));
					if (renameB != null)
					{
						dirEventList.Remove(renameB);
						rename = new RenamedEventArgs(WatcherChangeTypes.Renamed, basePath, renameB.Name, rename.OldName);
						current = rename;
					}
					else break;
				}

				// Discard useless renames
				if (rename.OldFullPath == rename.FullPath) return null;
			}

			// Pack del-create to rename
			if (current.ChangeType == WatcherChangeTypes.Created || current.ChangeType == WatcherChangeTypes.Deleted)
			{
				FileSystemEventArgs del		= current.ChangeType == WatcherChangeTypes.Deleted ? current : null;
				FileSystemEventArgs create	= current.ChangeType == WatcherChangeTypes.Created ? current : null;

				if (del != null)
				{
					create = dirEventList.FirstOrDefault(e => 
						e.ChangeType == WatcherChangeTypes.Created && 
						Path.GetFileName(e.FullPath) == Path.GetFileName(del.FullPath));
					dirEventList.Remove(create);
				}
				else if (create != null)
				{
					del = dirEventList.FirstOrDefault(e => 
						e.ChangeType == WatcherChangeTypes.Deleted && 
						Path.GetFileName(e.FullPath) == Path.GetFileName(create.FullPath));
					dirEventList.Remove(del);
				}

				if (del != null && create != null) return new RenamedEventArgs(WatcherChangeTypes.Renamed, basePath, create.Name, del.Name);
			}
			
			return current;
		}
		private static void PushDataDirEvent(FileSystemEventArgs e)
		{
			if (IsResPathIgnored(e.FullPath)) return;
			dataDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && f.ChangeType == e.ChangeType);
			dataDirEventBuffer.Add(e);
		}
		private static void ProcessDataDirEvents()
		{
			List<ResourceRenamedEventArgs> renameEventBuffer = null;

			// Process events
			while (dataDirEventBuffer.Count > 0)
			{
				FileSystemEventArgs e = FetchFileSystemEvent(dataDirEventBuffer, dataDirWatcher.Path);
				if (e == null) continue;

				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					// Is it a Resource file or just something else?
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);
					if (Resource.IsResourceFile(e.FullPath) || args.IsDirectory)
					{
						// Unregister outdated resources, if modified outside the editor
						if (!args.IsDirectory &&
							!editorJustSavedRes.Contains(Path.GetFullPath(e.FullPath)) && 
							ContentProvider.IsContentRegistered(args.Path))
						{
							bool isCurrentScene = args.Content.Is<Scene>() && Scene.Current == args.Content.Res;
							if (isCurrentScene || IsResourceUnsaved(e.FullPath))
							{
								DialogResult result = MessageBox.Show(
									String.Format(EditorRes.GeneralRes.Msg_ConfirmReloadResource_Text, e.FullPath), 
									EditorRes.GeneralRes.Msg_ConfirmReloadResource_Caption, 
									MessageBoxButtons.YesNo,
									MessageBoxIcon.Exclamation);
								if (result == DialogResult.Yes)
								{
									string curScenePath = Scene.CurrentPath;
									ContentProvider.UnregisterContent(args.Path);
									if (isCurrentScene) Scene.Current = ContentProvider.RequestContent<Scene>(curScenePath).Res;
								}
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
							NotifyObjPrefabApplied(null, new ObjectSelection(changedObjects));
						}

						if (ResourceModified != null) ResourceModified(null, args);
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
					if (File.Exists(e.FullPath))
					{
						// Register newly detected ressource file
						if (Resource.IsResourceFile(e.FullPath))
						{
							if (ResourceCreated != null)
								ResourceCreated(null, new ResourceEventArgs(e.FullPath));
						}
						// Import non-ressource file
						else
						{
							bool abort = false;

							if (FileImportProvider.IsImportFileExisting(e.FullPath))
							{
								DialogResult result = MessageBox.Show(
									String.Format(EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Text, e.FullPath), 
									EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Caption, 
									MessageBoxButtons.YesNo, 
									MessageBoxIcon.Warning);
								abort = result == DialogResult.No;
							}

							if (!abort)
							{
								bool importedSuccessfully = FileImportProvider.ImportFile(e.FullPath);
								if (!importedSuccessfully)
								{
									MessageBox.Show(
										String.Format(EditorRes.GeneralRes.Msg_CantImport_Text, e.FullPath), 
										EditorRes.GeneralRes.Msg_CantImport_Caption, 
										MessageBoxButtons.OK, 
										MessageBoxIcon.Error);
								}
								abort = !importedSuccessfully;
							}
						}
					}
					else if (Directory.Exists(e.FullPath))
					{
						// Register newly detected ressource directory
						if (ResourceCreated != null)
							ResourceCreated(null, new ResourceEventArgs(e.FullPath));
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					// Is it a Resource file or just something else?
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);
					if (Resource.IsResourceFile(e.FullPath) || args.IsDirectory)
					{

						// Unregister no-more existing resources
						if (args.IsDirectory)	ContentProvider.UnregisterContentTree(args.Path);
						else					ContentProvider.UnregisterContent(args.Path);

						if (ResourceDeleted != null)
							ResourceDeleted(null, args);
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					// Is it a Resource file or just something else?
					RenamedEventArgs re = e as RenamedEventArgs;
					ResourceRenamedEventArgs args = new ResourceRenamedEventArgs(re.FullPath, re.OldFullPath);
					if (Resource.IsResourceFile(e.FullPath) || args.IsDirectory)
					{
						// Rename content registerations
						if (args.IsDirectory)	ContentProvider.RenameContentTree(args.OldPath, args.Path);
						else					ContentProvider.RenameContent(args.OldPath, args.Path);

						// Buffer rename event to perform the global rename for all at once.
						if (renameEventBuffer == null) renameEventBuffer = new List<ResourceRenamedEventArgs>();
						renameEventBuffer.Add(args);

						if (ResourceRenamed != null) ResourceRenamed(null, args);
					}
				}
			}

			// If required, perform a global rename operation in all existing content
			if (renameEventBuffer != null)
			{
				// Don't do it now - schedule it for the main form event loop so we don't block here.
				mainForm.BeginInvoke((Action)delegate() {
					ProcessingBigTaskDialog taskDialog = new ProcessingBigTaskDialog( 
						EditorRes.GeneralRes.TaskRenameContentRefs_Caption, 
						EditorRes.GeneralRes.TaskRenameContentRefs_Desc, 
						async_RenameContentRefs, renameEventBuffer);
					taskDialog.ShowDialog(mainForm);
				});
			}
		}
		private static void PushSourceDirEvent(FileSystemEventArgs e)
		{
			if (IsSourcePathIgnored(e.FullPath)) return;
			sourceDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && f.ChangeType == e.ChangeType);
			sourceDirEventBuffer.Add(e);
		}
		private static void ProcessSourceDirEvents()
		{
			// Process events
			while (sourceDirEventBuffer.Count > 0)
			{
				FileSystemEventArgs e = FetchFileSystemEvent(sourceDirEventBuffer, sourceDirWatcher.Path);
				if (e == null) continue;

				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					if (File.Exists(e.FullPath)) reimportSchedule.Add(e.FullPath);
					if (SrcFileModified != null) SrcFileModified(null, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					if (SrcFileDeleted != null) SrcFileDeleted(null, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					RenamedEventArgs rename = e as RenamedEventArgs;
					FileImportProvider.NotifyFileRenamed(rename.OldFullPath, rename.FullPath);
					if (SrcFileRenamed != null) SrcFileRenamed(null, e);
				}
			}
		}

		private static void OnBeforeReloadCorePlugins()
		{
			dualityAppSuspended = true;

			Log.Editor.Write("Core plugin reloader initialized");
			Log.Editor.PushIndent();
			if (BeforeReloadCorePlugins != null)
				BeforeReloadCorePlugins(null, EventArgs.Empty);
		}
		private static void OnAfterReloadCorePlugins()
		{
			dualityAppSuspended = false;

			if (AfterReloadCorePlugins != null)
				AfterReloadCorePlugins(null, EventArgs.Empty);
			Log.Editor.PopIndent();
			Log.Editor.Write("Core plugin reloader finished");
		}
		private static void OnBeforeUpdateDualityApp()
		{
			if (BeforeUpdateDualityApp != null)
				BeforeUpdateDualityApp(null, EventArgs.Empty);
		}
		private static void OnAfterUpdateDualityApp()
		{
			if (AfterUpdateDualityApp != null)
				AfterUpdateDualityApp(null, EventArgs.Empty);
		}
		private static void OnEnteringSandbox()
		{
			if (EnteringSandbox != null)
				EnteringSandbox(null, EventArgs.Empty);
		}
		private static void OnLeaveSandbox()
		{
			if (LeaveSandbox != null)
				LeaveSandbox(null, EventArgs.Empty);
		}
		private static void OnPausedSandbox()
		{
			if (PausedSandbox != null)
				PausedSandbox(null, EventArgs.Empty);
		}
		private static void OnUnpausingSandbox()
		{
			if (UnpausingSandbox != null)
				UnpausingSandbox(null, EventArgs.Empty);
		}
		private static void OnSandboxStateChanged()
		{
			if (SandboxStateChanged != null)
				SandboxStateChanged(null, EventArgs.Empty);
		}
		private static void OnSelectionChanged(object sender, ObjectSelection.Category changedCategoryFallback)
		{
			//if (selectionCurrent == selectionPrevious) return;
			selectionChanging = true;

			if (SelectionChanged != null)
				SelectionChanged(sender, new SelectionChangedEventArgs(selectionCurrent, selectionPrevious, changedCategoryFallback));

			selectionChanging = false;
		}
		private static void OnObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs args)
		{
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
						if (PushPrefabLinkPropertyChange(link, o, info))
							changedLinks.Add(link);
					}
				}

				foreach (PrefabLink link in changedLinks)
				{
					NotifyObjPropChanged(null, new ObjectSelection(link.Obj), ReflectionInfo.Property_GameObject_PrefabLink);
				}
			}

			// If a Resource's Properties are modified, mark Resource for saving
			if (args.Objects.ResourceCount > 0)
			{
				foreach (Resource res in args.Objects.Resources)
				{
					if (sandboxState != SandboxState.Inactive && res is Scene && (res as Scene).IsCurrent) continue;
					FlagResourceUnsaved(res);
				}
			}

			// If a GameObjects's Property is modified, mark current Scene for saving
			if (args.Objects.GameObjects.Any(g => Scene.Current.AllObjects.Contains(g)) ||
				args.Objects.Components.Any(c => Scene.Current.AllObjects.Contains(c.GameObj)))
			{
				FlagResourceUnsaved(Scene.Current);
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
			if (ObjectPropertyChanged != null)
				ObjectPropertyChanged(sender, args);
		}
		private static bool PushPrefabLinkPropertyChange(PrefabLink link, object target, PropertyInfo info)
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
					NotifyObjPropChanged(null, new ObjectSelection(parentLink.Obj), info);
				}
				return false;
			}
			else
			{
				link.PushChange(target, info);
				return true;
			}
		}
		
		private static void Application_Idle(object sender, EventArgs e)
		{
			// Process file / source events, if no modal dialog is open.
			if (mainForm.Visible && mainForm.CanFocus)
			{
				ProcessSourceDirEvents();
				ProcessDataDirEvents();
				editorJustSavedRes.Clear();
			}

			// Perform scheduled help stack updates
			if (needHelpStackUpdate)
			{
				UpdateHelpStack();
			}

			// Update Duality engine
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			while (AppStillIdle)
			{
				watch.Restart();
				if (!dualityAppSuspended)
				{
					OnBeforeUpdateDualityApp();
					try
					{
						DualityApp.EditorUpdate(editorObjects, sandboxSceneFreeze);
					}
					catch (Exception exception)
					{
						Log.Editor.WriteError("An error occured during a core update: {0}", Log.Exception(exception));
					}
					OnAfterUpdateDualityApp();
				}

				// Assure we'll at least wait 16 ms until updating again.
				while (watch.Elapsed.TotalSeconds < 0.016666d) 
				{
					// Go to sleep if we'd have to wait too long
					if (watch.Elapsed.TotalSeconds < 0.012d)
						System.Threading.Thread.Sleep(1);
					// App wants to do something? Stop waiting.
					else if (!AppStillIdle)
						break;
				}
			}
		}
		private static void Scene_Leaving(object sender, EventArgs e)
		{
			if (selectionCurrent.GameObjects.Any() || selectionCurrent.Components.Any())
				Deselect(null, ObjectSelection.Category.GameObjCmp);
			SandboxLeaveScene();
		}
		private static void Scene_Entered(object sender, EventArgs e)
		{
			// Try to restore last GameObject / Component selection
			var objQuery = selectionPrevious.GameObjects.Select(g => Scene.Current.AllObjects.FirstOrDefault(sg => sg.FullName == g.FullName)).NotNull();
			var cmpQuery = selectionPrevious.Components.Select(delegate (Component c)
			{
				GameObject cmpObj = Scene.Current.AllObjects.FirstOrDefault(sg => sg.FullName == c.GameObj.FullName);
				if (cmpObj == null) return null;
				return cmpObj.GetComponent(c.GetType());
			}).NotNull();
			// Append restored selection to current one.
			ObjectSelection objSel = new ObjectSelection(((IEnumerable<object>)objQuery).Concat(cmpQuery));
			if (objSel.ObjectCount > 0) Select(null, objSel, SelectMode.Append);
		}
		private static void Resource_ResourceSaved(object sender, Duality.ResourceEventArgs e)
		{
			if (e.Path == null) return; // Ignore Resources without a path.
			if (e.IsDefaultContent) return; // Ignore default content
			editorJustSavedRes.Add(Path.GetFullPath(e.Path));
			FlagResourceSaved(e.Content.Res);
		}
		
		private static void mainForm_Activated(object sender, EventArgs e)
		{
			// Core plugin reload
			if (needsRecovery)
			{
				needsRecovery = false;
				Log.Editor.Write("Recovering from full plugin reload restart...");
				Log.Editor.PushIndent();
				corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.RecoverFromRestart;
			}
			else if (corePluginReloader.State == ReloadCorePluginDialog.ReloaderState.WaitForPlugins)
			{
				corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.ReloadPlugins;
			}

			// Perform scheduled source file reimports
			if (reimportSchedule.Count > 0)
			{
				// Hacky: Wait a little for the files to be accessable again (Might be used by another process)
				System.Threading.Thread.Sleep(50);

				foreach (string file in reimportSchedule)
				{
					if (!File.Exists(file)) continue;
					FileImportProvider.ReimportFile(file);
				}
				reimportSchedule.Clear();
			}
		}
		private static void mainForm_Deactivate(object sender, EventArgs e)
		{
			// Update source code, in case the user is switching to his IDE without hitting the "open source code" button again
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated)
				DualityEditorApp.UpdatePluginSourceCode();
		}

		private static void corePluginWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			string pluginStr = Path.Combine("Plugins", e.Name);
			if (!corePluginReloader.ReloadSchedule.Contains(pluginStr))
				corePluginReloader.ReloadSchedule.Add(pluginStr);
			corePluginReloader.State = ReloadCorePluginDialog.ReloaderState.WaitForPlugins;
		}
		private static void corePluginReloader_AfterEndReload(object sender, EventArgs e)
		{
			OnAfterReloadCorePlugins();
		}
		private static void corePluginReloader_BeforeBeginReload(object sender, EventArgs e)
		{
			OnBeforeReloadCorePlugins();
		}

		private static void inputFilter_MouseLeave(object sender, EventArgs e)
		{
			needHelpStackUpdate = true;
		}
		private static void inputFilter_MouseMove(object sender, EventArgs e)
		{
			if (Control.MouseButtons != MouseButtons.None) return;
			needHelpStackUpdate = true;
		}
		private static void inputFilter_MouseUp(object sender, EventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.None)
				needHelpStackUpdate = true;
		}
		private static void inputFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
				e.Handled = e.Handled || PerformHelpAction();
		}

		private static System.Collections.IEnumerable async_RenameContentRefs(ProcessingBigTaskDialog.WorkerInterface state)
		{
			var renameData = state.Data as List<ResourceRenamedEventArgs>;
			int totalCounter = 0;
			int fileCounter = 0;
			
			// Rename in static application data
			state.StateDesc = "DualityApp Data"; yield return null;
			DualityApp.LoadAppData();
			DualityApp.LoadUserData();
			DualityApp.LoadMetaData();
			state.Progress += 0.04f; yield return null;

			totalCounter += async_RenameContentRefs_Perform(DualityApp.AppData, renameData);
			totalCounter += async_RenameContentRefs_Perform(DualityApp.UserData, renameData);
			totalCounter += async_RenameContentRefs_Perform(DualityApp.MetaData, renameData);
			state.Progress += 0.02f; yield return null;

			DualityApp.SaveAppData();
			DualityApp.SaveUserData();
			DualityApp.SaveMetaData();
			state.Progress += 0.04f; yield return null;

			// Special case: Current Scene in sandbox mode
			if (sandboxState != SandboxState.Inactive)
			{
				// Because changes we'll do will be discarded when leaving the sandbox we'll need to
				// do it the hard way - manually load an save the file.
				state.StateDesc = "Current Scene"; yield return null;
				Scene curScene = Resource.LoadResource<Scene>(Scene.CurrentPath);
				fileCounter = async_RenameContentRefs_Perform(curScene, renameData);
				totalCounter += fileCounter;
				if (fileCounter > 0) curScene.Save(Scene.CurrentPath);
			}

			// Rename in actual content
			var availContent = ContentProvider.GetAvailContent<Resource>();
			var reloadContent = new List<IContentRef>();
			List<string> resFiles = Resource.GetResourceFiles();
			foreach (string file in resFiles)
			{
				state.StateDesc = file; yield return null;

				// Loaded for the first time? Schedule for later reload.
				bool reload = !availContent.Any(r => r.Path == file);
				// Keep in mind that this operation is performed while Duality content was
				// in an inconsistent state. Loading Resources now may lead to wrong data.
				// Because the ContentRefs might be wrong right now.

				// Load content
				var cr = ContentProvider.RequestContent(file);
				state.Progress += 0.35f / resFiles.Count; yield return null;

				// Perform rename and flag unsaved
				fileCounter = async_RenameContentRefs_Perform(cr.Res, renameData);
				totalCounter += fileCounter;
				if (fileCounter > 0)
				{
					if (!reload)
						FlagResourceUnsaved(cr.Res);
					else
						reloadContent.Add(cr);
				}
				state.Progress += 0.35f / resFiles.Count; yield return null;
			}

			// Perform Resource unload where scheduled
			state.StateDesc = "Saving Resources.."; yield return null;
			foreach (IContentRef cr in reloadContent)
			{
				cr.Res.Save();
				ContentProvider.UnregisterContent(cr.Path);
				state.Progress += 0.2f / reloadContent.Count; yield return null;
			}
		}
		private static int async_RenameContentRefs_Perform(object obj, List<ResourceRenamedEventArgs> args)
		{
			int counter = 0;
			ReflectionHelper.VisitObjectsDeep<IContentRef>(obj, r => 
			{
				if (r.IsDefaultContent) return r;
				if (r.IsExplicitNull) return r;
				if (string.IsNullOrEmpty(r.Path)) return r;

				foreach (ResourceRenamedEventArgs e in args)
				{
					if (e.IsResource && r.Path == e.OldPath)
					{
						r.Path = e.Path;
						counter++;
						break;
					}
					else if (e.IsDirectory && PathHelper.IsPathLocatedIn(r.Path, e.OldPath))
					{
						r.Path = r.Path.Replace(e.OldPath, e.Path);
						counter++;
						break;
					}
				}
				return r; 
			});
			return counter;
		}
	}
}
