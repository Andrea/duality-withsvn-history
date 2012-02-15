using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

using DW.RtfWriter;

using Duality.ObjectManagers;
using Duality.Resources;
using Duality.Serialization;

namespace Duality
{
	/// <summary>
	/// This class controls Duality's main program flow control and general maintenance functionality.
	/// It initializes the engine, loads plugins, provides access to user input, houses global data structures
	/// and handles logfiles internally.
	/// </summary>
	public static class DualityApp
	{
		/// <summary>
		/// Describes the context in which the current DualityApp runs.
		/// </summary>
		public enum ExecutionContext : int
		{
			/// <summary>
			/// Duality has been terminated. There is no guarantee that any object is still valid or usable.
			/// </summary>
			Terminated,
			/// <summary>
			/// The context in which Duality is executed is unknown.
			/// </summary>
			Unknown,
			/// <summary>
			/// Duality runs in a game environment.
			/// </summary>
			Game,
			/// <summary>
			/// Duality runs in an editing environment.
			/// </summary>
			Editor
		}
		/// <summary>
		/// Describes the environment in which the current DualityApp runs.
		/// </summary>
		public enum ExecutionEnvironment : int
		{
			/// <summary>
			/// The environment in which Duality is executed is unknown.
			/// </summary>
			Unknown,
			/// <summary>
			/// Duality runs in the DualityLauncher
			/// </summary>
			Launcher,
			/// <summary>
			/// Duality runs in the DualityEditor
			/// </summary>
			Editor
		}

		private	static	bool					initialized			= false;
		private	static	bool					isUpdating			= false;
		private	static	bool					runFromEditor		= false;
		private	static	bool					terminateScheduled	= false;
		private	static	string					logfilePath			= "logfile";
		private	static	StreamWriter			logfile				= null;
		private	static	RtfDocument				logfileRtf			= null;
		private	static	Vector2					targetResolution	= Vector2.Zero;
		private	static	GraphicsMode			targetMode			= null;
		private	static	HashSet<GraphicsMode>	availModes			= new HashSet<GraphicsMode>(new GraphicsModeComparer());
		private	static	GraphicsMode			defaultMode			= null;
		private	static	IMouseInput				mouse				= null;
		private	static	IKeyboardInput			keyboard			= null;
		private	static	SoundDevice				sound				= null;
		private	static	ExecutionEnvironment	environment			= ExecutionEnvironment.Unknown;
		private	static	ExecutionContext		execContext			= ExecutionContext.Terminated;
		private	static	DualityAppData			appData				= null;
		private	static	DualityUserData			userData			= null;
		private	static	DualityMetaData			metaData			= null;
		private	static	List<object>			disposeSchedule		= new List<object>();

		private	static	Dictionary<string,CorePlugin>	plugins			= new Dictionary<string,CorePlugin>();
		private	static	List<Assembly>					disposedPlugins	= new List<Assembly>();
		private static	Dictionary<Type,List<Type>>		availTypeDict	= new Dictionary<Type,List<Type>>();

		internal static event EventHandler UserDataChanged	= null;
		internal static event EventHandler AppDataChanged	= null;


		/// <summary>
		/// [GET / SET] The size of the current rendering surface (full screen, a single window, etc.) in pixels. Setting this will not actually change
		/// Duality's state - this is a pure "for your information" property.
		/// </summary>
		public static Vector2 TargetResolution
		{
			get { return targetResolution; }
			set { targetResolution = value; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="GraphicsMode"/> in which rendering takes place. Setting this will not actually change
		/// Duality's state - this is a pure "for your information" property.
		/// </summary>
		public static GraphicsMode TargetMode
		{
			get { return targetMode; }
			set { targetMode = value; }
		}
		/// <summary>
		/// [GET] Provides access to mouse user input.
		/// </summary>
		public static IMouseInput Mouse
		{
			get { return mouse; }
			internal set { mouse = value; }
		}
		/// <summary>
		/// [GET] Provides access to keyboard user input
		/// </summary>
		public static IKeyboardInput Keyboard
		{
			get { return keyboard; }
			internal set { keyboard = value; }
		}
		/// <summary>
		/// [GET] Provides access to the main <see cref="SoundDevice"/>.
		/// </summary>
		public static SoundDevice Sound
		{
			get { return sound; }
		}
		/// <summary>
		/// [GET / SET] Provides access to Duality's current <see cref="DualityAppData">application data</see>. This is never null.
		/// Any kind of data change event is fired as soon as you re-assign this property. Be sure to do that after changing its data.
		/// </summary>
		public static DualityAppData AppData
		{
			get { return appData; }
			set 
			{ 
				appData = value; 
				if (appData == null) appData = new DualityAppData();
				// We're currently missing direct changes without invoking this setter
				OnAppDataChanged();
			}
		}
		/// <summary>
		/// [GET / SET] Provides access to Duality's current <see cref="DualityUserData">user data</see>. This is never null.
		/// Any kind of data change event is fired as soon as you re-assign this property. Be sure to do that after changing its data.
		/// </summary>
		public static DualityUserData UserData
		{
			get { return userData; }
			set 
			{ 
				userData = value; 
				if (userData == null) userData = new DualityUserData();
				// We're currently missing direct changes without invoking this setter
				OnUserDataChanged();
			}
		}
		/// <summary>
		/// [GET] Provides access to Duality's current <see cref="DualityMetaData">meta data</see>. This is never null.
		/// </summary>
		public static DualityMetaData MetaData
		{
			get { return metaData; }
		}
		/// <summary>
		/// [GET] Returns the path where this DualityApp's <see cref="DualityAppData">application data</see> is located at.
		/// </summary>
		public static string AppDataPath
		{
			get { return "appdata.dat"; }
		}
		/// <summary>
		/// [GET] Returns the path where this DualityApp's <see cref="DualityUserData">user data</see> is located at.
		/// </summary>
		public static string UserDataPath
		{
			get
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				path = Path.Combine(path, "Duality");
				path = Path.Combine(path, PathHelper.GetValidFileName(appData.AppName));
				path = Path.Combine(path, "userdata.dat");
				return path;
			}
		}
		/// <summary>
		/// [GET] Returns the path where this DualityApp's <see cref="DualityMetaData">meta data</see> is located at.
		/// </summary>
		public static string MetaDataPath
		{
			get
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				path = Path.Combine(path, "Duality");
				path = Path.Combine(path, "metadata.dat");
				return path;
			}
		}
		/// <summary>
		/// [GET] Returns the <see cref="GraphicsMode"/> that Duality intends to use by default.
		/// </summary>
		public static GraphicsMode DefaultMode
		{
			get { return defaultMode; }
		}
		/// <summary>
		/// [GET] Enumerates all available <see cref="GraphicsMode">GraphicsModes</see>.
		/// </summary>
		public static IEnumerable<GraphicsMode> AvailableModes
		{
			get { return availModes; }
		}
		/// <summary>
		/// [GET] Returns the <see cref="ExecutionContext"/> in which this DualityApp is currently running.
		/// </summary>
		public static ExecutionContext ExecContext
		{
			get { return execContext; }
			internal set 
			{
				if (execContext != value)
				{
					ExecutionContext previous = execContext;
					execContext = value;
					OnExecContextChanged(previous);
				}
			}
		}
		/// <summary>
		/// [GET] Returns the <see cref="ExecutionEnvironment"/> in which this DualityApp is currently running.
		/// </summary>
		public static ExecutionEnvironment ExecEnvironment
		{
			get { return environment; }
		}
		/// <summary>
		/// [GET] Enumerates all currently loaded plugins.
		/// </summary>
		public static IEnumerable<CorePlugin> LoadedPlugins
		{
			get { return plugins.Values; }
		}
		/// <summary>
		/// [GET] Enumerates all plugin assemblies that have been loaded before, but have been discarded due to a runtime plugin reload operation.
		/// This is usually only the case when being executed from withing the editor or manually triggering a plugin reload. However,
		/// this is normally unnecessary.
		/// </summary>
		public static IEnumerable<Assembly> DisposedPlugins
		{
			get { return disposedPlugins; }
		}


		/// <summary>
		/// Initializes this DualityApp. Should be called before performing any operations withing Duality.
		/// </summary>
		/// <param name="context">The <see cref="ExecutionContext"/> in which Duality runs.</param>
		/// <param name="args">
		/// Command line arguments to run this DualityApp with. 
		/// Usually these are just the ones from the host application, passed on.
		/// </param>
		public static void Init(ExecutionEnvironment env = ExecutionEnvironment.Unknown, ExecutionContext context = ExecutionContext.Unknown, string[] args = null)
		{
			if (initialized) return;

			// Process command line options
			if (args != null)
			{
				int logArgIndex = args.IndexOfFirst("logfile");
				if (logArgIndex != -1 && logArgIndex + 1 < args.Length) logArgIndex++;
				else logArgIndex = -1;

				// Enter debug mode
				if (args.Contains("debug")) System.Diagnostics.Debugger.Launch();
				// Run from editor
				if (args.Contains("editor")) runFromEditor = true;
				// Set logfile path
				if (logArgIndex != -1) logfilePath = args[logArgIndex];
			}

			// Determine available and default graphics modes
			foreach (int samplecount in new int[] { 0, 2, 4, 6, 8, 16, 32, 48, 64 })
			{
				GraphicsMode mode = new GraphicsMode(32, 24, 0, samplecount);
				if (!availModes.Contains(mode))
				{
					availModes.Add(mode);
					defaultMode = mode;
				}
			}

			environment = env;
			execContext = context;

			// Initialize Logfile
			logfile = new StreamWriter(logfilePath + ".txt");
			TextWriterLogOutput logfileOutput = new TextWriterLogOutput(logfile);
			Log.Game.RegisterOutput(logfileOutput);
			Log.Core.RegisterOutput(logfileOutput);
			Log.Editor.RegisterOutput(logfileOutput);

			logfileRtf = new RtfDocument(PaperSize.A4, PaperOrientation.Portrait, Lcid.English);
			Log.Game.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(230, 255, 220)));
			Log.Core.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(220, 220, 255)));
			Log.Editor.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(245, 220, 255)));

			// Assure Duality is properly terminated in any case and register additional AppDomain events
			AppDomain.CurrentDomain.ProcessExit			+= CurrentDomain_ProcessExit;
			AppDomain.CurrentDomain.UnhandledException	+= CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.AssemblyResolve		+= CurrentDomain_AssemblyResolve;
			AppDomain.CurrentDomain.AssemblyLoad		+= CurrentDomain_AssemblyLoad;

			Performance.InitDualityCounters();
			sound = new SoundDevice();
			LoadPlugins();
			LoadAppData();
			LoadUserData();
			LoadMetaData();

			// Initial changed event
			OnAppDataChanged();
			OnUserDataChanged();

			Formatter.InitDefaultMethod();
			
			Log.Core.Write("DualityApp initialized");
			Log.Core.Write("Debug Mode: {0}", System.Diagnostics.Debugger.IsAttached);
			Log.Core.Write("Command line arguments: {0}", args != null ? args.ToString(", ") : "null");

			initialized = true;
			InitPlugins();
		}
		/// <summary>
		/// Terminates this DualityApp. This does not end the current Process, but it isn't recommended to
		/// attemp performing any Duality operations after it has been terminated.
		/// </summary>
		/// <param name="unexpected">
		/// If true, this is handled as an unexpected termination, such as because of an exception that
		/// from which the application can't recover.
		/// </param>
		public static void Terminate(bool unexpected = false)
		{
			if (!initialized) return;

			if (unexpected)
			{
				Log.Core.WriteError("DualityApp terminated unexpectedly");
			}
			else
			{
				if (isUpdating)
				{
					terminateScheduled = true;
					return;
				}
				if (execContext != ExecutionContext.Editor)
				{
					SaveUserData();
					SaveMetaData();
				}
				sound.Dispose();
				UnloadPlugins();
				Log.Core.Write("DualityApp terminated");
			}

			logfile.Close();
			logfileRtf.save(logfilePath + ".rtf");

			initialized = false;
			execContext = ExecutionContext.Terminated;
		}

		/// <summary>
		/// Performs a single update cycle.
		/// </summary>
		public static void Update()
		{
			isUpdating = true;
			Performance.timeUpdate.BeginMeasure();

			Time.FrameTick();
			Performance.ResetCounters();
			OnBeforeUpdate();
			Scene.Current.Update();
			sound.Update();
			OnAfterUpdate();
			RunCleanup();

			Performance.timeUpdate.EndMeasure();
			isUpdating = false;

			if (terminateScheduled) Terminate();
		}
		/// <summary>
		/// Performs a single render cycle.
		/// </summary>
		public static void Render()
		{
			Scene.Current.Render();
		}

		/// <summary>
		/// Schedules the specified object for disposal. It is guaranteed to be disposed by the end of the current update cycle.
		/// </summary>
		/// <param name="o">The object to schedule for disposal.</param>
		public static void DisposeLater(object o)
		{
			disposeSchedule.Add(o);
		}
		private static void RunCleanup()
		{
			foreach (object o in disposeSchedule)
			{
				GameObject g = o as GameObject;
				if (g != null) { g.Dispose(); continue; }
				Component c = o as Component;
				if (c != null) { c.Dispose(); continue; }
				Resource r = o as Resource;
				if (r != null) { r.Dispose(); continue; }
				IDisposable d = o as IDisposable;
				if (d != null) { d.Dispose(); continue; }
			}
			disposeSchedule.Clear();
			Resource.RunCleanup();
		}

		internal static void EditorUpdate(GameObjectManager updateObjects, bool freezeScene)
		{
			isUpdating = true;
			Performance.timeUpdate.BeginMeasure();
			
			Time.FrameTick();
			Performance.ResetCounters();
			OnBeforeUpdate();
			if (execContext == ExecutionContext.Editor)
			{
				Scene.Current.EditorUpdate();
				foreach (GameObject obj in updateObjects.ActiveObjects) obj.Update();
			}
			else if (execContext == ExecutionContext.Game)
			{
				if (!freezeScene)	Scene.Current.Update();
				else				Scene.Current.EditorUpdate();
				foreach (GameObject obj in updateObjects.ActiveObjects) obj.Update();
			}
			sound.Update();
			OnAfterUpdate();
			RunCleanup();

			Performance.timeUpdate.EndMeasure();
			isUpdating = false;
		}
		
		/// <summary>
		/// Loads all <see cref="Resource">Resources</see> that are located in this DualityApp's data directory and
		/// saves them again. All loaded content is discarded both before and after this operation. You usually don't
		/// need this.
		/// </summary>
		public static void LoadSaveAll()
		{
			DualityApp.LoadAppData();
			DualityApp.LoadUserData();
			DualityApp.LoadMetaData();

			ContentProvider.ClearContent();
			string[] resFiles = Directory.GetFiles("Data", "*" + Resource.FileExt, SearchOption.AllDirectories);
			foreach (string file in resFiles)
			{
				var cr = ContentProvider.RequestContent(file);
				cr.Res.Save(file);
			}
			ContentProvider.ClearContent();

			DualityApp.SaveAppData();
			DualityApp.SaveUserData();
			DualityApp.SaveMetaData();
		}

		/// <summary>
		/// Triggers Duality to (re)load its <see cref="DualityAppData"/>.
		/// </summary>
		public static void LoadAppData()
		{
			string path = AppDataPath;
			if (File.Exists(path))
			{
				try
				{
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							appData = formatter.ReadObject() as DualityAppData ?? new DualityAppData();
						}
					}
				}
				catch (Exception)
				{
					appData = new DualityAppData();
				}
			}
			else
				appData = new DualityAppData();
		}
		/// <summary>
		/// Triggers Duality to (re)load its <see cref="DualityUserData"/>.
		/// </summary>
		public static void LoadUserData()
		{
			string path = UserDataPath;
			if (!File.Exists(path) || execContext == ExecutionContext.Editor || runFromEditor) path = "defaultuserdata.dat";
			if (File.Exists(path))
			{
				try
				{
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							UserData = formatter.ReadObject() as DualityUserData ?? new DualityUserData();
						}
					}
				}
				catch (Exception)
				{
					UserData = new DualityUserData();
				}
			}
			else
				UserData = new DualityUserData();
		}
		/// <summary>
		/// Triggers Duality to (re)load its <see cref="DualityMetaData"/>.
		/// </summary>
		public static void LoadMetaData()
		{
			string path = MetaDataPath;
			if (File.Exists(path))
			{
				try
				{
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							metaData = formatter.ReadObject() as DualityMetaData ?? new DualityMetaData();
						}
					}
				}
				catch (Exception)
				{
					metaData = new DualityMetaData();
				}
			}
			else
				metaData = new DualityMetaData();
		}
		/// <summary>
		/// Triggers Duality to save its <see cref="DualityAppData"/>.
		/// </summary>
		public static void SaveAppData()
		{
			string path = AppDataPath;
			using (FileStream str = File.Open(path, FileMode.Create))
			{
				using (var formatter = Formatter.Create(str, FormattingMethod.Binary))
				{
					formatter.WriteObject(appData);
				}
			}
		}
		/// <summary>
		/// Triggers Duality to save its <see cref="DualityUserData"/>.
		/// </summary>
		public static void SaveUserData()
		{
			string path = UserDataPath;
			if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));
			if (execContext == ExecutionContext.Editor) path = "defaultuserdata.dat";

			using (FileStream str = File.Open(path, FileMode.Create))
			{
				using (var formatter = Formatter.Create(str, FormattingMethod.Binary))
				{
					formatter.WriteObject(userData);
				}
			}
		}
		/// <summary>
		/// Triggers Duality to save its <see cref="DualityMetaData"/>.
		/// </summary>
		public static void SaveMetaData()
		{
			string path = MetaDataPath;
			if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));

			using (FileStream str = File.Open(path, FileMode.Create))
			{
				using (var formatter = Formatter.Create(str, FormattingMethod.Binary))
				{
					formatter.WriteObject(metaData);
				}
			}
		}

		private static void LoadPlugins()
		{
			UnloadPlugins();

			Log.Core.Write("Scanning for core plugins...");
			Log.Core.PushIndent();

			if (Directory.Exists("Plugins"))
			{
				string[] pluginDllPaths = Directory.GetFiles("Plugins", "*.core.dll", SearchOption.AllDirectories);
				Assembly pluginAssembly;
				Type pluginType;
				CorePlugin plugin;
				for (int i = 0; i < pluginDllPaths.Length; i++)
				{
					Log.Core.Write("Loading '{0}'...", pluginDllPaths[i]);
					Log.Core.PushIndent();
					if (environment == ExecutionEnvironment.Launcher)
						pluginAssembly = Assembly.LoadFrom(pluginDllPaths[i]);
					else
						pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginDllPaths[i]));
					pluginType = pluginAssembly.GetExportedTypes().FirstOrDefault(t => typeof(CorePlugin).IsAssignableFrom(t));
					if (pluginType == null)
					{
						Log.Core.WriteError("Can't find CorePlugin class. Discarding plugin...");
						disposedPlugins.Add(pluginAssembly);
						continue;
					}
					plugin = (CorePlugin)ReflectionHelper.CreateInstanceOf(pluginType);
					plugins.Add(plugin.AssemblyName, plugin);
					Log.Core.PopIndent();
				}
			}

			Log.Core.PopIndent();
		}
		private static void InitPlugins()
		{
			foreach (CorePlugin plugin in plugins.Values) plugin.InitPlugin();
		}
		private static void UnloadPlugins()
		{
			ContentProvider.ClearContent();
			ReflectionHelper.ClearTypeCache();
			availTypeDict.Clear();
			foreach (CorePlugin plugin in plugins.Values)
			{
				disposedPlugins.Add(plugin.PluginAssembly);
				plugin.Dispose();
			}
			plugins.Clear();
		}
		internal static void ReloadPlugin(string pluginFileName)
		{
			Log.Core.Write("Reloading core plugin '{0}'...", pluginFileName);
			Log.Core.PushIndent();

			// Load new plugin
			Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginFileName));
			Type pluginType = pluginAssembly.GetExportedTypes().FirstOrDefault(t => typeof(CorePlugin).IsAssignableFrom(t));
			CorePlugin plugin = (CorePlugin)ReflectionHelper.CreateInstanceOf(pluginType);

			// If we're overwritin an old plugin here, add the old version to the "disposed" blacklist
			CorePlugin oldPlugin = null;
			Type[] oldResTypes = new Type[0];
			if (plugins.TryGetValue(plugin.AssemblyName, out oldPlugin))
			{
				disposedPlugins.Add(oldPlugin.PluginAssembly);
				oldPlugin.Dispose();
			}

			// Register newly loaded plugin
			plugins[plugin.AssemblyName] = plugin;
			
			Log.Core.PopIndent();
			availTypeDict.Clear();
			ReflectionHelper.ClearTypeCache();
			Scene.Current.Dispose();

			// Init plugin
			if (initialized) plugin.InitPlugin();
		}
		internal static bool IsLeafPlugin(string pluginFileName)
		{
			string asmName = Path.GetFileNameWithoutExtension(pluginFileName);
			foreach (CorePlugin plugin in plugins.Values)
			{
				AssemblyName[] refNames = plugin.PluginAssembly.GetReferencedAssemblies();
				foreach (AssemblyName rn in refNames)
				{
					if (rn.Name == asmName) return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Enumerates all currently loaded assemblies that are part of Duality, i.e. Duality itsself and all loaded plugins.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<Assembly> GetDualityAssemblies()
		{
			yield return typeof(Duality.DualityApp).Assembly;
			foreach (CorePlugin p in LoadedPlugins) yield return p.PluginAssembly;
		}
		/// <summary>
		/// Enumerates all available Duality <see cref="System.Type">Types</see> that are assignable
		/// to the specified Type. 
		/// </summary>
		/// <param name="baseType">The base type to use for matching the result types.</param>
		/// <returns>An enumeration of all Duality types deriving from the specified type.</returns>
		/// <example>
		/// The following code logs all available kinds of <see cref="Duality.Components.Renderer">Renderers</see>:
		/// <code>
		/// var rendererTypes = DualityApp.GetAvailDualityTypes(typeof(Duality.Components.Renderer));
		/// foreach (Type rt in rendererTypes)
		/// {
		/// 	Log.Core.Write("Renderer Type '{0}' from Assembly '{1}'", Log.Type(rt), rt.Assembly.FullName);
		/// }
		/// </code>
		/// </example>
		public static IEnumerable<Type> GetAvailDualityTypes(Type baseType)
		{
			List<Type> availTypes;
			if (availTypeDict.TryGetValue(baseType, out availTypes)) return availTypes;

			availTypes = new List<Type>();
			IEnumerable<Assembly> asmQuery = GetDualityAssemblies();
			foreach (Assembly asm in asmQuery)
			{
				availTypes.AddRange(
					from t in asm.GetExportedTypes()
					where baseType.IsAssignableFrom(t)
					select t);
			}
			availTypeDict[baseType] = availTypes;

			return availTypes;
		}

		private static void OnBeforeUpdate()
		{
			foreach (CorePlugin plugin in plugins.Values) plugin.OnBeforeUpdate();
		}
		private static void OnAfterUpdate()
		{
			foreach (CorePlugin plugin in plugins.Values) plugin.OnAfterUpdate();
		}
		private static void OnExecContextChanged(ExecutionContext previousContext)
		{
			foreach (CorePlugin plugin in plugins.Values) plugin.OnExecContextChanged(previousContext);
		}
		private static void OnUserDataChanged()
		{
			if (UserDataChanged != null)
				UserDataChanged(null, EventArgs.Empty);
		}
		private static void OnAppDataChanged()
		{
			if (AppDataChanged != null)
				AppDataChanged(null, EventArgs.Empty);

			FarseerPhysics.Settings.VelocityThreshold = appData.PhysicsVelocityThreshold * 0.01f / Time.SPFMult;
		}

		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			Terminate(true);
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Log.Core.WriteError(e.ExceptionObject.ToString());
			if (e.IsTerminating) Terminate(true);
		}
		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			// If this method gets called, assume we are searching for a dynamically loaded plugin assembly
			string assemblyNameStub = args.Name.Split(',')[0];
			CorePlugin plugin;
			if (DualityApp.plugins.TryGetValue(assemblyNameStub, out plugin))
				return plugin.PluginAssembly;
			else
				return null;
		}
		private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			Log.Core.Write("Assembly loaded: {0}", args.LoadedAssembly.FullName.Split(',')[0]);
		}
	}

	/// <summary>
	/// Provides general information about this Duality application / game.
	/// </summary>
	[Serializable]
	public class DualityAppData
	{
		private	string				appName				= "Duality Application";
		private	string				authorName			= "Unknown";
		private	string				websiteUrl			= "http://www.fetzenet.de";
		private	uint				version				= 0;
		private	ContentRef<Scene>	startScene			= ContentRef<Scene>.Null;
		private	float				speedOfSound		= 360.0f;
		private	float				soundDopplerFactor	= 1.0f;
		private	float				physicsVelThreshold	= 0.5f * Time.SPFMult / 0.01f;

		/// <summary>
		/// [GET / SET] The name of your application / game. It will also be used as a window title by the launcher app.
		/// </summary>
		public string AppName
		{
			get { return this.appName; }
			set { this.appName = value; }
		}
		/// <summary>
		/// [GET / SET] The author name of your application. Might be your or your team's name or -nickname.
		/// </summary>
		public string AuthorName
		{
			get { return this.authorName; }
			set { this.authorName = value; }
		}
		/// <summary>
		/// [GET / SET] The address of this game's official website or similar.
		/// </summary>
		public string WebsiteUrl
		{
			get { return this.websiteUrl; }
			set { this.websiteUrl = value; }
		}
		/// <summary>
		/// [GET / SET] The current application / game version.
		/// </summary>
		public uint Version
		{
			get { return this.version; }
			set { this.version = value; }
		}
		/// <summary>
		/// [GET / SET] A reference to the start <see cref="Duality.Resources.Scene"/>. It is used by the launcher app to
		/// determine which Scene to load initially.
		/// </summary>
		public ContentRef<Scene> StartScene
		{
			get { return this.startScene; }
			set { this.startScene = value; }
		}
		/// <summary>
		/// [GET / SET] The speed of sound. While this is technically a unitless value, you might assume something like "meters per second".
		/// It is used to calculate the doppler effect of <see cref="SoundInstance">SoundInstances</see> that are moving relative to the
		/// <see cref="Duality.Components.SoundListener"/>.
		/// </summary>
		public float SpeedOfSound
		{
			get { return this.speedOfSound; }
			set { this.speedOfSound = value; }
		}
		/// <summary>
		/// [GET / SET] A factor by which the strength of the doppler effect is multiplied.
		/// </summary>
		public float SoundDopplerFactor
		{
			get { return this.soundDopplerFactor; }
			set { this.soundDopplerFactor = value; }
		}
		/// <summary>
		/// [GET / SET] Any velocity below this value will be resolved using inelastic equations i.e. won't lead to "bouncing".
		/// </summary>
		public float PhysicsVelocityThreshold
		{
			get { return this.physicsVelThreshold; }
			set { this.physicsVelThreshold = value; }
		}
	}

	/// <summary>
	/// Provides information about user settings for this Duality application / game.
	/// It is persistent beyond installing or deleting this Duality game.
	/// </summary>
	[Serializable]
	public class DualityUserData
	{
		private	string	userName		= "Unknown";
		private	int		gfxWidth		= 800;
		private	int		gfxHeight		= 600;
		private	bool	gfxFullScreen	= true;
		private	float	sfxEffectVol	= 1.0f;
		private	float	sfxSpeechVol	= 1.0f;
		private	float	sfxMusicVol		= 1.0f;
		private	float	sfxMasterVol	= 1.0f;

		/// <summary>
		/// [GET / SET] The player's name. This may be his main character's name or simply remain unused.
		/// </summary>
		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}
		/// <summary>
		/// [GET / SET] Width of the game's display area.
		/// </summary>
		public int GfxWidth
		{
			get { return this.gfxWidth; }
			set { this.gfxWidth = value; }
		}
		/// <summary>
		/// [GET / SET] Height of the game's display area.
		/// </summary>
		public int GfxHeight
		{
			get { return this.gfxHeight; }
			set { this.gfxHeight = value; }
		}
		/// <summary>
		/// [GET / SET] Whether or not the game is launched in fullscreen mode. Not all display area sizes are available in fullscreen
		/// and some of them might look distorted when applied to a display they do not fit on. To be sure, you should let the user decide
		/// which screen resolution to use when in fullscreen.
		/// </summary>
		public bool GfxFullScreen
		{
			get { return this.gfxFullScreen; }
			set { this.gfxFullScreen = value; }
		}
		/// <summary>
		/// [GET / SET] Volume factor of sound effects. This is applied automatically by the <see cref="SoundDevice"/> based on the <see cref="SoundType"/>.
		/// </summary>
		public float SfxEffectVol
		{
			get { return this.sfxEffectVol; }
			set { this.sfxEffectVol = value; }
		}
		/// <summary>
		/// [GET / SET] Volume factor of speech / vocals. This is applied automatically by the <see cref="SoundDevice"/> based on the <see cref="SoundType"/>.
		/// </summary>
		public float SfxSpeechVol
		{
			get { return this.sfxSpeechVol; }
			set { this.sfxSpeechVol = value; }
		}
		/// <summary>
		/// [GET / SET] Volume factor of music. This is applied automatically by the <see cref="SoundDevice"/> based on the <see cref="SoundType"/>.
		/// </summary>
		public float SfxMusicVol
		{
			get { return this.sfxMusicVol; }
			set { this.sfxMusicVol = value; }
		}
		/// <summary>
		/// [GET / SET] Volume master factor for sound in general. This is applied automatically by the <see cref="SoundDevice"/>.
		/// </summary>
		public float SfxMasterVol
		{
			get { return this.sfxMasterVol; }
			set { this.sfxMasterVol = value; }
		}
	}

	/// <summary>
	/// Provides custom information about the Duality environment in which this application / game runs.
	/// It is persistent beyond installing or deleting a specific Duality game and is shared among all Duality
	/// games. Developers can use the DualityMetaData API to share player-related game information, such as
	/// stats, player descisions, tasks, progress, etc.
	/// </summary>
	[Serializable]
	public class DualityMetaData
	{
		/// <summary>
		/// An array of valid path separators for meta data.
		/// </summary>
		public static readonly char[] Separator = "/\\".ToCharArray();

		[Serializable]
		private class Entry
		{
			public Dictionary<string,Entry> children;
			public string value;

			public Entry()
			{
				this.children = null;
				this.value = null;
			}
			public Entry(Entry cc)
			{
				this.value = cc.value;
				this.children = new Dictionary<string,Entry>();

				foreach (var pair in cc.children)
					this.children[pair.Key] = new Entry(pair.Value);
			}
			public Entry(string value)
			{
				this.value = value;
				this.children = null;
			}

			public Entry ReadValueEntry(string key)
			{
				if (String.IsNullOrEmpty(key)) return this;
				if (this.children == null || this.children.Count == 0) return null;

				int sepIndex = key.IndexOfAny(Separator);
				string singleKey;
				if (sepIndex != -1)
				{
					singleKey = key.Substring(0, sepIndex);
					key = key.Substring(sepIndex + 1, key.Length - sepIndex - 1);
				}
				else
				{
					singleKey = key;
					key = null;
				}

				Entry valEntry;
				if (this.children.TryGetValue(singleKey, out valEntry))
					return valEntry.ReadValueEntry(key);
				else
					return null;
			}
			public string ReadValue(string key)
			{
				Entry valEntry = this.ReadValueEntry(key);
				return valEntry != null ? valEntry.value : null;
			}
			public void WriteValue(string key, string value)
			{
				if (String.IsNullOrEmpty(key))
				{
					this.value = value;
					return;
				}

				int sepIndex = key.IndexOfAny(Separator);
				string singleKey;
				if (sepIndex != -1)
				{
					singleKey = key.Substring(0, sepIndex);
					key = key.Substring(sepIndex + 1, key.Length - sepIndex - 1);
				}
				else
				{
					singleKey = key;
					key = null;
				}

				Entry valEntry;
				if (this.children == null || !this.children.TryGetValue(singleKey, out valEntry))
				{
					if (this.children == null) this.children = new Dictionary<string,Entry>();
					valEntry = new Entry();
					this.children[singleKey] = valEntry;
				}
				valEntry.WriteValue(key, value);
			}
		}

		private Entry	rootEntry	= new Entry();

		/// <summary>
		/// [GET / SET] The string value that is located at the specified key (path). Keys are organized hierarchially and behave
		/// like file paths. Use the normal path separator chars to address keys in keys.
		/// </summary>
		/// <param name="key">The key that defines where to look for the value.</param>
		/// <returns>The string value associated with the specified key.</returns>
		/// <example>
		/// The following code reads and writes the value of <c>MainNode / SubNode / SomeKey</c>:
		/// <code>
		/// string value = DualityApp.MetaData["MainNode/SubNode/SomeKey"];
		/// DualityApp.MetaData["MainNode/SubNode/SomeKey"] = "Some other value";
		/// </code>
		/// </example>
		/// <seealso cref="ReadValue(string)"/>
		/// <seealso cref="ReadValueAs{T}(string, out T)"/>
		public string this[string key]
		{
			get { return this.ReadValue(key); }
			set { this.WriteValue(key, value); }
		}

		/// <summary>
		/// Reads the specified key's string value. Keys are organized hierarchially and behave
		/// like file paths. Use the normal path separator chars to address keys in keys.
		/// </summary>
		/// <param name="key">The key that defines where to look for the value.</param>
		/// <returns>The string value associated with the specified key.</returns>
		/// <example>
		/// The following code reads the value of <c>MainNode / SubNode / SomeKey</c>:
		/// <code>
		/// string value = DualityApp.MetaData.ReadValue("MainNode/SubNode/SomeKey");
		/// </code>
		/// </example>
		/// <seealso cref="ReadValueAs{T}(string, out T)"/>
		public string ReadValue(string key)
		{
			return this.rootEntry.ReadValue(key);
		}
		/// <summary>
		/// Reads the specified key's string value and tries to parse it.
		/// </summary>
		/// <typeparam name="T">The desired value type</typeparam>
		/// <param name="key">The key that defines where to look for the value.</param>
		/// <param name="value">The parsed value based on the string that is associated with the specified key.</param>
		/// <returns>True, if successful, false if not.</returns>
		/// <seealso cref="ReadValue(string)"/>
		/// <example>
		/// The following code writes and reads an int value:
		/// <code>
		/// DualityApp.MetaData.WriteValue("SomeKey", 42);
		/// int value =  DualityApp.MetaData.ReadValueAs{int}("SomeKey");
		/// </code>
		/// </example>
		public bool ReadValueAs<T>(string key, out T value)
		{
			string valStr = this.ReadValue(key);
			try
			{
				value = (T)Convert.ChangeType(valStr, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
				return true;
			}
			catch (Exception)
			{
				value = default(T);
				return false;
			}
		}
		/// <summary>
		/// Reads all the <see cref="KeyValuePair{T,U}"/>s that are children of the specified key.
		/// </summary>
		/// <param name="key">The key of which to return child values.</param>
		/// <returns>An enumeration of <see cref="KeyValuePair{T,U}"/>s.</returns>
		/// <example>
		/// The following code creates a small hierarchy and reads a part of it out again:
		/// <code>
		/// DualityApp.MetaData["MainNode/SubNode/SomeKey"] = "42";
		/// DualityApp.MetaData["MainNode/SubNode/SomeOtherKey"] = "43";
		/// DualityApp.MetaData["MainNode/SubNode/SomeOtherKey2"] = "44";
		/// DualityApp.MetaData["MainNode/SubNode2"] = "Something";
		/// 
		/// var pairs = DualityApp.MetaData.ReadSubValues("MainNode/SubNode");
		/// foreach (var pair in pairs)
		/// {
		/// 	Log.Core.Write("{0}: {1}", pair.Key, pair.Value);
		/// }
		/// </code>
		/// The expected output is:
		/// <code>
		/// SomeKey: 42
		/// SomeOtherKey: 43
		/// SomeOtherKey2: 44
		/// </code>
		/// </example>
		public IEnumerable<KeyValuePair<string,string>> ReadSubValues(string key)
		{
			Entry parentEntry = this.rootEntry.ReadValueEntry(key);
			if (parentEntry == null) yield break;

			foreach (var pair in parentEntry.children)
				yield return new KeyValuePair<string,string>(pair.Key, pair.Value.value);
		}
		/// <summary>
		/// Writes the specified string value to the specified key. Keys are organized hierarchially and behave
		/// like file paths. Use the normal path separator chars to address keys in keys.
		/// </summary>
		/// <param name="key">The key that defines to write the value to.</param>
		/// <param name="value">The value to write</param>
		/// <seealso cref="WriteValue{T}(string, T)"/>
		public void WriteValue(string key, string value)
		{
			this.rootEntry.WriteValue(key, value);
		}
		/// <summary>
		/// Writes the specified value to the specified key. Keys are organized hierarchially and behave
		/// like file paths. Use the normal path separator chars to address keys in keys.
		/// </summary>
		/// <typeparam name="T">The value's Type.</typeparam>
		/// <param name="key">The key that defines to write the value to.</param>
		/// <param name="value">The value to write</param>
		/// <seealso cref="WriteValue(string, string)"/>
		public void WriteValue<T>(string key, T value)
		{
			string valStr = value as string;
			if (valStr != null)
			{
				this.WriteValue(key, valStr);
				return;
			}

			IFormattable valFormattable = value as IFormattable;
			if (valFormattable != null)
			{
				this.WriteValue(key, valFormattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture));
				return;
			}

			this.WriteValue(key, value.ToString());
			return;
		}
	}
}
