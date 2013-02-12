using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Threading;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio.OpenAL;

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
		public enum ExecutionContext
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
		public enum ExecutionEnvironment
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

		public const string CmdArgDebug = "debug";
		public const string CmdArgEditor = "editor";

		private	static	Thread					mainThread			= null;
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
		private	static	StaticMouseInput		mouse				= new StaticMouseInput();
		private	static	StaticKeyboardInput		keyboard			= new StaticKeyboardInput();
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

		/// <summary>
		/// Called when the games UserData changes
		/// </summary>
		public static event EventHandler UserDataChanged	= null;
		/// <summary>
		/// Called when the games AppData changes
		/// </summary>
		public static event EventHandler AppDataChanged		= null;
		/// <summary>
		/// Called when Duality is being terminated by choice (e.g. not because of crashes or similar).
		/// It is also called in an editor environment.
		/// </summary>
		public static event EventHandler Terminating		= null;
		/// <summary>
		/// Fired whenever a core plugin has been initialized. This is the case after loading or reloading one.
		/// </summary>
		public static event EventHandler<CorePluginEventArgs> PluginReady	= null;


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
			internal set { mouse.RealInput = value; }
		}
		/// <summary>
		/// [GET] Provides access to keyboard user input
		/// </summary>
		public static IKeyboardInput Keyboard
		{
			get { return keyboard; }
			internal set { keyboard.RealInput = value; }
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
				appData = value ?? new DualityAppData();
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
				userData = value ?? new DualityUserData();
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
				string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				path = Path.Combine(path, "Duality");
				path = Path.Combine(path, "AppData");
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
				string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				path = Path.Combine(path, "Duality");
				path = Path.Combine(path, "AppData");
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

			// Set main thread
			mainThread = Thread.CurrentThread;

			// Process command line options
			if (args != null)
			{
				int logArgIndex = args.IndexOfFirst("logfile");
				if (logArgIndex != -1 && logArgIndex + 1 < args.Length) logArgIndex++;
				else logArgIndex = -1;

				// Enter debug mode
				if (args.Contains(CmdArgDebug)) System.Diagnostics.Debugger.Launch();
				// Run from editor
				if (args.Contains(CmdArgEditor)) runFromEditor = true;
				// Set logfile path
				if (logArgIndex != -1) logfilePath = args[logArgIndex];
			}

			environment = env;
			execContext = context;

			// Initialize Logfile
			try
			{
				logfile = new StreamWriter(logfilePath + ".txt");
				TextWriterLogOutput logfileOutput = new TextWriterLogOutput(logfile);
				Log.Game.RegisterOutput(logfileOutput);
				Log.Core.RegisterOutput(logfileOutput);
				Log.Editor.RegisterOutput(logfileOutput);
			}
			catch (Exception e)
			{
				Log.Core.WriteWarning("Text Logfile unavailable: {0}", Log.Exception(e));
			}

			try
			{
				logfileRtf = new RtfDocument(PaperSize.A4, PaperOrientation.Portrait, Lcid.English);
				Log.Game.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(230, 255, 220)));
				Log.Core.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(220, 220, 255)));
				Log.Editor.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, new ColorFormat.ColorRgba(245, 220, 255)));
			}
			catch (Exception e)
			{
				Log.Core.WriteWarning("Rtf Logfile unavailable: {0}", Log.Exception(e));
			}

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
			
			// Determine available and default graphics modes
			int[] aaLevels = new int[] { 0, 2, 4, 6, 8, 16, 32, 48, 64 };
			foreach (int samplecount in aaLevels)
			{
				GraphicsMode mode = new GraphicsMode(32, 24, 0, samplecount);
				if (!availModes.Contains(mode)) availModes.Add(mode);
			}
			int highestAALevel = MathF.RoundToInt(MathF.Log(MathF.Max(availModes.Max(m => m.Samples), 1.0f), 2.0f));
			int targetAALevel = highestAALevel;
			switch (userData.AntialiasingQuality)
			{
				case AAQuality.High:	targetAALevel = highestAALevel;		break;
				case AAQuality.Medium:	targetAALevel = highestAALevel / 2; break;
				case AAQuality.Low:		targetAALevel = highestAALevel / 4; break;
				case AAQuality.Off:		targetAALevel = 0;					break;
			}
			int targetSampleCount = MathF.RoundToInt(MathF.Pow(2.0f, targetAALevel));
			defaultMode = availModes.LastOrDefault(m => m.Samples <= targetSampleCount) ?? availModes.Last();

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
		public static void Terminate()
		{
			Terminate(false);
		}
		internal static void Terminate(bool unexpected)
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

				if (environment == ExecutionEnvironment.Editor && execContext == ExecutionContext.Game)
				{
					Scene.Current.Dispose();
					Log.Core.Write("DualityApp Sandbox terminated");
					terminateScheduled = false;
					return;
				}

				if (execContext != ExecutionContext.Editor)
				{
					OnTerminating();
					SaveUserData();
					SaveMetaData();
				}
				sound.Dispose();
				UnloadPlugins();
				Performance.SaveTextReport(environment == ExecutionEnvironment.Editor ? "perflog_editor.txt" : "perflog.txt");
				Log.Core.Write("DualityApp terminated");
			}

			if (logfile != null) logfile.Close();
			if (logfileRtf != null) logfileRtf.save(logfilePath + ".rtf");

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
			Performance.FrameTick();
			OnBeforeUpdate();
			Scene.Current.Update();
			sound.Update();
			OnAfterUpdate();
			CheckOpenALErrors();
			//CheckOpenGLErrors();
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

		internal static void EditorUpdate(IEnumerable<GameObject> updateObjects, bool freezeScene, bool forceFixedStep)
		{
			isUpdating = true;
			Performance.timeUpdate.BeginMeasure();

			Time.FrameTick(forceFixedStep);
			Performance.FrameTick();
			OnBeforeUpdate();
			if (execContext == ExecutionContext.Game)
			{
				if (!freezeScene)	Scene.Current.Update();
				else				Scene.Current.EditorUpdate();

				foreach (GameObject obj in updateObjects)
				{
					if (!freezeScene && Scene.Current.AllObjects.Contains(obj)) continue;
					obj.Update();
				}
			}
			else if (execContext == ExecutionContext.Editor)
			{
				Scene.Current.EditorUpdate();
				foreach (GameObject obj in updateObjects) obj.Update();
			}
			sound.Update();
			OnAfterUpdate();
			CheckOpenALErrors();
			//CheckOpenGLErrors();
			RunCleanup();

			Performance.timeUpdate.EndMeasure();
			isUpdating = false;

			if (terminateScheduled) Terminate();
		}
		
		/// <summary>
		/// Loads all <see cref="Resource">Resources</see> that are located in this DualityApp's data directory and
		/// saves them again. All loaded content is discarded both before and after this operation. You usually don't
		/// need this.
		/// </summary>
		public static void LoadSaveAll()
		{
			LoadAppData();
			LoadUserData();
			LoadMetaData();

			ContentProvider.ClearContent();
			string[] resFiles = Directory.GetFiles("Data", "*" + Resource.FileExt, SearchOption.AllDirectories);
			foreach (string file in resFiles)
			{
				var cr = ContentProvider.RequestContent(file);
				cr.Res.Save(file);
			}
			ContentProvider.ClearContent();

			SaveAppData();
			SaveUserData();
			SaveMetaData();
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
					Log.Core.Write("Loading AppData..");
					Log.Core.PushIndent();
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							appData = formatter.ReadObject() as DualityAppData ?? new DualityAppData();
						}
					}
					Log.Core.PopIndent();
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
					Log.Core.Write("Loading UserData..");
					Log.Core.PushIndent();
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							UserData = formatter.ReadObject() as DualityUserData ?? new DualityUserData();
						}
					}
					Log.Core.PopIndent();
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
					Log.Core.Write("Loading MetaData..");
					Log.Core.PushIndent();
					using (FileStream str = File.OpenRead(path))
					{
						using (var formatter = Formatter.Create(str))
						{
							metaData = formatter.ReadObject() as DualityMetaData ?? new DualityMetaData();
						}
					}
					Log.Core.PopIndent();
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
			Log.Core.Write("Saving AppData..");
			Log.Core.PushIndent();

			try
			{
				string path = AppDataPath;
				using (FileStream str = File.Open(path, FileMode.Create))
				{
					using (var formatter = Formatter.Create(str, FormattingMethod.Xml))
					{
						formatter.WriteObject(appData);
					}
				}
			}
			catch (Exception e) { Log.Core.WriteError(Log.Exception(e)); }

			Log.Core.PopIndent();
		}
		/// <summary>
		/// Triggers Duality to save its <see cref="DualityUserData"/>.
		/// </summary>
		public static void SaveUserData()
		{
			Log.Core.Write("Saving UserData..");
			Log.Core.PushIndent();

			try
			{
				string path = UserDataPath;
				if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));
				if (execContext == ExecutionContext.Editor) path = "defaultuserdata.dat";

				using (FileStream str = File.Open(path, FileMode.Create))
				{
					using (var formatter = Formatter.Create(str, FormattingMethod.Xml))
					{
						formatter.WriteObject(userData);
					}
				}
			}
			catch (Exception e) { Log.Core.WriteError(Log.Exception(e)); }

			Log.Core.PopIndent();
		}
		/// <summary>
		/// Triggers Duality to save its <see cref="DualityMetaData"/>.
		/// </summary>
		public static void SaveMetaData()
		{
			Log.Core.Write("Saving MetaData..");
			Log.Core.PushIndent();

			try
			{
				string path = MetaDataPath;
				if (!Directory.Exists(Path.GetDirectoryName(path))) Directory.CreateDirectory(Path.GetDirectoryName(path));

				using (FileStream str = File.Open(path, FileMode.Create))
				{
					using (var formatter = Formatter.Create(str, FormattingMethod.Xml))
					{
						formatter.AddFieldBlocker(Resource.NonSerializedResourceBlocker);
						formatter.WriteObject(metaData);
					}
				}
			}
			catch (Exception e) { Log.Core.WriteError(Log.Exception(e)); }

			Log.Core.PopIndent();
		}

		private static void LoadPlugins()
		{
			UnloadPlugins();

			Log.Core.Write("Scanning for core plugins...");
			Log.Core.PushIndent();

			if (Directory.Exists("Plugins"))
			{
				string[] pluginDllPaths = Directory.GetFiles("Plugins", "*.core.dll", SearchOption.AllDirectories);
				foreach (string dllPath in pluginDllPaths)
				{
					Log.Core.Write("Loading '{0}'...", dllPath);
					Log.Core.PushIndent();
					Assembly pluginAssembly;
					if (environment == ExecutionEnvironment.Launcher)
						pluginAssembly = Assembly.LoadFrom(dllPath);
					else
						pluginAssembly = Assembly.Load(File.ReadAllBytes(dllPath));
					Type pluginType = pluginAssembly.GetExportedTypes().FirstOrDefault(t => typeof(CorePlugin).IsAssignableFrom(t));
					if (pluginType == null)
					{
						Log.Core.WriteError("Can't find CorePlugin class. Discarding plugin...");
						disposedPlugins.Add(pluginAssembly);
						continue;
					}
					CorePlugin plugin = (CorePlugin)pluginType.CreateInstanceOf();
					plugins.Add(plugin.AssemblyName, plugin);
					Log.Core.PopIndent();
				}
			}

			Log.Core.PopIndent();
		}
		private static void InitPlugins()
		{
			foreach (CorePlugin plugin in plugins.Values)
			{
				plugin.InitPlugin();
				OnPluginReady(plugin);
			}
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
			CorePlugin plugin = (CorePlugin)pluginType.CreateInstanceOf();

			// If we're overwritin an old plugin here, add the old version to the "disposed" blacklist
			CorePlugin oldPlugin;
			if (plugins.TryGetValue(plugin.AssemblyName, out oldPlugin))
			{
				disposedPlugins.Add(oldPlugin.PluginAssembly);
				oldPlugin.Dispose();
			}

			// Register newly loaded plugin
			plugins[plugin.AssemblyName] = plugin;
			
			// Discard plugin-related data
			availTypeDict.Clear();
			ReflectionHelper.ClearTypeCache();
			if (!Scene.Current.IsEmpty)
				Scene.Current.Dispose();
			foreach (Resource r in ContentProvider.EnumeratePluginContent().ToArray())
				ContentProvider.UnregisterContent(r.Path);

			Log.Core.PopIndent();

			// Init plugin
			if (initialized)
			{
				plugin.InitPlugin();
				OnPluginReady(plugin);
			}
		}
		internal static bool IsLeafPlugin(string pluginFileName)
		{
			string asmName = Path.GetFileNameWithoutExtension(pluginFileName);
			foreach (CorePlugin plugin in plugins.Values)
			{
				AssemblyName[] refNames = plugin.PluginAssembly.GetReferencedAssemblies();
				if (refNames.Any(rn => rn.Name == asmName)) return false;
			}
			return true;
		}

		/// <summary>
		/// Enumerates all currently loaded assemblies that are part of Duality, i.e. Duality itsself and all loaded plugins.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<Assembly> GetDualityAssemblies()
		{
			yield return typeof(DualityApp).Assembly;
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
					orderby t.Name
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
		private static void OnPluginReady(CorePlugin plugin)
		{
			if (PluginReady != null)
				PluginReady(null, new CorePluginEventArgs(plugin));
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
		}
		private static void OnTerminating()
		{
			if (Terminating != null)
				Terminating(null, EventArgs.Empty);
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
			if (plugins.TryGetValue(assemblyNameStub, out plugin))
				return plugin.PluginAssembly;
			else
				return null;
		}
		private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			Log.Core.Write("Assembly loaded: {0}", args.LoadedAssembly.FullName.Split(',')[0]);
		}
		

		/// <summary>
		/// Checks for errors that might have occured during audio processing.
		/// </summary>
		/// <param name="silent">If true, errors aren't logged.</param>
		/// <returns>True, if an error occured, false if not.</returns>
		public static bool CheckOpenALErrors(bool silent = false)
		{
			ALError error;
			bool found = false;
			while ((error = AL.GetError()) != ALError.NoError)
			{
				if (!silent)
				{
					Log.Core.WriteError(
						"Internal OpenAL error, code {0} at {1}", 
						error,
						Log.CurrentMethod(1));
				}
				found = true;
			}
			if (found && !silent && System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
			return found;
		}
		/// <summary>
		/// Checks for errors that might have occured during video processing. You should avoid calling this method due to performance reasons.
		/// Only use it on suspect.
		/// </summary>
		/// <param name="silent">If true, errors aren't logged.</param>
		/// <returns>True, if an error occured, false if not.</returns>
		public static bool CheckOpenGLErrors(bool silent = false)
		{
			ErrorCode error;
			bool found = false;
			while ((error = GL.GetError()) != ErrorCode.NoError)
			{
				if (!silent)
				{
					Log.Core.WriteError(
						"Internal OpenGL error, code {0} at {1}", 
						error,
						Log.CurrentMethod(1));
				}
				found = true;
			}
			if (found && !silent && System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
			return found;
		}

		/// <summary>
		/// Guards the calling method agains being called from a thread that is not the main thread.
		/// Use this only at critical code segments that are likely to be called from somewhere else than the main thread
		/// but aren't allowed to.
		/// </summary>
		/// <param name="silent"></param>
		/// <returns>True if everyhing is allright. False if the guarded state has been violated.</returns>
		[System.Diagnostics.DebuggerStepThrough]
		public static bool GuardSingleThreadState(bool silent = false)
		{
			if (Thread.CurrentThread != mainThread)
			{
				if (!silent)
				{
					Log.Core.WriteError(
						"Method {0} isn't allowed to be called from a Thread that is not the main Thread.", 
						Log.CurrentMethod(1));
				}
				return false;
			}
			return true;
		}
	}
}
