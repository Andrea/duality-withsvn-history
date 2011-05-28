using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

using DW.RtfWriter;

using Duality.ObjectManagers;
using Duality.Resources;

namespace Duality
{
	public static class DualityApp
	{
		public const string DefaultAppDataPath	= "appdata.dat";
		public const string DefaultUserDataPath	= "userdata.dat";

		public enum ExecutionContext
		{
			Terminated,
			Unknown,
			Launcher,
			Editor
		}

		public class PluginSerializationBinder : System.Runtime.Serialization.SerializationBinder
		{
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type result = null;

				// Is it a plugin assembly? Use a Type from there
				int colonIndex = assemblyName.IndexOfAny(new char[] {','});
				string assemblyNameStub = (colonIndex >= 0 ? assemblyName.Substring(0, colonIndex) : assemblyName);
				Assembly plugin;
				if (DualityApp.plugins.TryGetValue(assemblyNameStub, out plugin))
				{
					result = plugin.GetType(typeName);
				}

				// No result yet? Use global search
				if (result == null)
				{
					result = Type.GetType(typeName + ", " + assemblyName);					
				}

				// Still no type? Log as error
				if (result == null)
				{
					Log.Core.WriteError("Can't bind type name '{0}' to Type. Assembly: '{1}'", typeName, assemblyName);
				}

				return result;
			}
		}

		private	static	bool					initialized			= false;
		private	static	string					logfilePath			= "logfile";
		private	static	StreamWriter			logfile				= null;
		private	static	RtfDocument				logfileRtf			= null;
		private	static	Vector2					targetResolution	= Vector2.Zero;
		private	static	GraphicsMode			targetMode			= null;
		private	static	HashSet<GraphicsMode>	availModes			= new HashSet<GraphicsMode>(new GraphicsModeComparer());
		private	static	GraphicsMode			defaultMode			= null;
		private	static	MouseDevice				mouse				= null;
		private	static	KeyboardDevice			keyboard			= null;
		private	static	SoundDevice				sound				= null;
		private	static	IList<JoystickDevice>	joysticks			= null;
		private	static	ExecutionContext		execContext			= ExecutionContext.Terminated;
		private	static	DualityAppData			appData				= null;
		private	static	DualityUserData			userData			= null;

		private	static	PluginSerializationBinder	pluginTypeBinder;
		private	static	Dictionary<string,Assembly>	plugins;
		private static	Dictionary<Type,List<Type>>	availTypeDict;

		public static event EventHandler Initialized	= null;
		public static event EventHandler Terminating	= null;
		public static event EventHandler Updating		= null;


		public static Vector2 TargetResolution
		{
			get { return targetResolution; }
			set { targetResolution = value; }
		}
		public static GraphicsMode TargetMode
		{
			get { return targetMode; }
			set { targetMode = value; }
		}
		public static MouseDevice Mouse
		{
			get { return mouse; }
			set { mouse = value; }
		}
		public static KeyboardDevice Keyboard
		{
			get { return keyboard; }
			set { keyboard = value; }
		}
		public static SoundDevice Sound
		{
			get { return sound; }
		}
		public static IList<JoystickDevice> Joysticks
		{
			get { return joysticks; }
			set { joysticks = value; }
		}
		public static DualityAppData AppData
		{
			get { return appData; }
			set { appData = value; if (appData == null) appData = new DualityAppData(); }
		}
		public static DualityUserData UserData
		{
			get { return userData; }
			set { userData = value; if (userData == null) userData = new DualityUserData(); }
		}
		public static GraphicsMode DefaultMode
		{
			get { return defaultMode; }
		}
		public static IEnumerable<GraphicsMode> AvailableModes
		{
			get { return availModes; }
		}
		public static ExecutionContext ExecContext
		{
			get { return execContext; }
		}
		public static PluginSerializationBinder PluginTypeBinder
		{
			get { return pluginTypeBinder; }
		}
		public static IEnumerable<Assembly> LoadedPlugins
		{
			get { return plugins.Values; }
		}


		public static void Init(ExecutionContext context = ExecutionContext.Unknown, string[] args = null)
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

			execContext = context;
			plugins = new Dictionary<string,Assembly>();
			availTypeDict = new Dictionary<Type,List<Type>>();
			pluginTypeBinder = new PluginSerializationBinder();

			// Initialize Logfile
			logfile = new StreamWriter(logfilePath + ".txt");
			LogOutputFormat logfileSharedFormat = new LogOutputFormat();
			Log.Game.RegisterOutput(new TextWriterLogOutput(logfile, "[Game]   ", logfileSharedFormat));
			Log.Core.RegisterOutput(new TextWriterLogOutput(logfile, "[Core]   ", logfileSharedFormat));
			Log.Editor.RegisterOutput(new TextWriterLogOutput(logfile, "[Editor] ", logfileSharedFormat));

			logfileRtf = new RtfDocument(PaperSize.A4, PaperOrientation.Portrait, Lcid.English);
			LogOutputFormat logfileRtfSharedFormat = new LogOutputFormat();
			Log.Game.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, "[Game]   ", new ColorFormat.ColorRGBA(230, 255, 220), logfileRtfSharedFormat));
			Log.Core.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, "[Core]   ", new ColorFormat.ColorRGBA(220, 220, 255), logfileRtfSharedFormat));
			Log.Editor.RegisterOutput(new RtfDocWriterLogOutput(logfileRtf, "[Editor] ", new ColorFormat.ColorRGBA(245, 220, 255), logfileRtfSharedFormat));

			// Assure Duality is propery terminated in any case
			AppDomain.CurrentDomain.ProcessExit			+= CurrentDomain_ProcessExit;
			AppDomain.CurrentDomain.UnhandledException	+= CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.AssemblyResolve		+= CurrentDomain_AssemblyResolve;

			Log.Core.Write("DualityApp initialized");
			Log.Core.Write("Debug Mode: {0}", System.Diagnostics.Debugger.IsAttached);
			Log.Core.Write("Command line arguments: {0}", args != null ? args.ToString(", ") : "null");

			sound = new SoundDevice();
			LoadPlugins();

			initialized = true;
			OnInitialized();
		}
		public static void Terminate(bool unexpected = false)
		{
			if (!initialized) return;

			if (!unexpected) OnTerminating();

			if (unexpected)	Log.Core.WriteError("DualityApp terminated unexpectedly");
			else			Log.Core.Write("DualityApp terminated");

			logfile.Close();
			logfileRtf.save(logfilePath + ".rtf");

			initialized = false;
			execContext = ExecutionContext.Terminated;
		}

		public static void Update()
		{
			Time.FrameTick();
			Scene.Current.Update();
			OnUpdating();
			Resource.RunCleanup();
		}
		public static void Draw()
		{
			Scene.Current.Render();
		}

		public static void EditorUpdate(GameObjectManager updateObjects)
		{
			if (execContext != ExecutionContext.Editor)
				throw new ApplicationException("This method may only be used in Editor execution context.");

			Time.FrameTick();
			Scene.Current.EditorUpdate();
			foreach (GameObject obj in updateObjects.ActiveObjects)
			{
				obj.Update();
			}
			Resource.RunCleanup();
		}
		
		public static void LoadAppData(string path = DefaultAppDataPath)
		{
			if (File.Exists(path))
			{
				try
				{
					using (FileStream str = File.OpenRead(path))
					{
						BinaryFormatter formatter = RequestSerializer(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
						appData = formatter.Deserialize(str) as DualityAppData;
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
		public static void LoadUserData(string path = DefaultUserDataPath)
		{
			if (File.Exists(path))
			{
				try
				{
					using (FileStream str = File.OpenRead(path))
					{
						BinaryFormatter formatter = RequestSerializer(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
						userData = formatter.Deserialize(str) as DualityUserData;
					}
				}
				catch (Exception)
				{
					userData = new DualityUserData();
				}
			}
			else
				userData = new DualityUserData();
		}
		public static void SaveAppData(string path = DefaultAppDataPath)
		{
			using (FileStream str = File.Open(path, FileMode.Create))
			{
				BinaryFormatter formatter = RequestSerializer(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
				formatter.Serialize(str, appData);
			}
		}
		public static void SaveUserData(string path = DefaultUserDataPath)
		{
			using (FileStream str = File.Open(path, FileMode.Create))
			{
				BinaryFormatter formatter = RequestSerializer(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
				formatter.Serialize(str, userData);
			}
		}

		public static void LoadPlugins()
		{
			plugins.Clear();
			availTypeDict.Clear();

			Log.Core.Write("Scanning for core plugins...");
			Log.Core.PushIndent();

			if (Directory.Exists("Plugins"))
			{
				string[] pluginDllPaths = Directory.GetFiles("Plugins", "*.core.dll", SearchOption.AllDirectories);
				Assembly pluginAssembly;
				for (int i = 0; i < pluginDllPaths.Length; i++)
				{
					Log.Core.Write("Loading '{0}'...", pluginDllPaths[i]);
					Log.Core.PushIndent();
					pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginDllPaths[i]));
					plugins.Add(pluginAssembly.FullName.Split(',')[0], pluginAssembly);
					Log.Core.PopIndent();
				}
			}

			Log.Core.PopIndent();
		}
		public static void ReloadPlugin(string pluginFileName)
		{
			Log.Core.Write("Reloading core plugin '{0}'...", pluginFileName);
			Log.Core.PushIndent();

			Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginFileName));
			plugins[pluginAssembly.FullName.Split(',')[0]] = pluginAssembly;
			availTypeDict.Clear();
			
			Log.Core.PopIndent();
		}
		public static bool IsLeafPlugin(string pluginFileName)
		{
			string asmName = Path.GetFileNameWithoutExtension(pluginFileName);
			foreach (Assembly asm in plugins.Values)
			{
				AssemblyName[] refNames = asm.GetReferencedAssemblies();
				foreach (AssemblyName rn in refNames)
				{
					if (rn.Name == asmName) return false;
				}
			}
			return true;
		}
		public static BinaryFormatter RequestSerializer(ISurrogateSelector surrogateSelector = null, StreamingContext context = new StreamingContext())
		{
			BinaryFormatter serializer = new BinaryFormatter(surrogateSelector, context);
			serializer.Binder = pluginTypeBinder;
			return serializer;
		}

		public static IEnumerable<Assembly> GetDualityAssemblies()
		{
			yield return typeof(Duality.DualityApp).Assembly;
			foreach (Assembly a in LoadedPlugins) yield return a;
		}
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
		public static Type FindDualityRessourceType(string typeName)
		{
			return GetAvailDualityTypes(typeof(Duality.Resource)).FirstOrDefault(t => t.Name == typeName);
		}

		private static void OnInitialized()
		{
			LoadAppData();
			LoadUserData();

			if (Initialized != null)
				Initialized(null, EventArgs.Empty);
		}
		private static void OnTerminating()
		{
			if (execContext != ExecutionContext.Editor) SaveUserData();

			if (Terminating != null)
				Terminating(null, EventArgs.Empty);
		}
		private static void OnUpdating()
		{
			if (Updating != null)
				Updating(null, EventArgs.Empty);
		}

		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			Terminate();
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
			Assembly plugin;
			if (DualityApp.plugins.TryGetValue(assemblyNameStub, out plugin))
				return plugin;
			else
				return null;
		}
	}

	[Serializable]
	public class DualityAppData
	{
		private	string				appName		= "Duality Application";
		private	string				authorName	= "Unknown";
		private	string				websiteUrl	= "http://www.fetzenet.de";
		private	uint				version		= 0;
		private	ContentRef<Scene>	startScene	= ContentRef<Scene>.Null;

		public string AppName
		{
			get { return this.appName; }
			set { this.appName = value; }
		}
		public string AuthorName
		{
			get { return this.authorName; }
			set { this.authorName = value; }
		}
		public string WebsiteUrl
		{
			get { return this.websiteUrl; }
			set { this.websiteUrl = value; }
		}
		public uint Version
		{
			get { return this.version; }
			set { this.version = value; }
		}
		public ContentRef<Scene> StartScene
		{
			get { return this.startScene; }
			set { this.startScene = value; }
		}
	}

	[Serializable]
	public class DualityUserData
	{
		private	string			userName		= "Unknown";
		private	int				gfxWidth		= 800;
		private	int				gfxHeight		= 600;
		private	bool			gfxFullScreen	= true;

		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}
		public int GfxWidth
		{
			get { return this.gfxWidth; }
			set { this.gfxWidth = value; }
		}
		public int GfxHeight
		{
			get { return this.gfxHeight; }
			set { this.gfxHeight = value; }
		}
		public bool GfxFullScreen
		{
			get { return this.gfxFullScreen; }
			set { this.gfxFullScreen = value; }
		}
	}
}
