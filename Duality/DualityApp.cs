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
		private	static	StreamWriter			logfile				= null;
		private	static	RtfDocument				logfileRtf			= null;
		private	static	Vector2					targetResolution	= Vector2.Zero;
		private	static	GraphicsMode			targetMode			= null;
		private	static	HashSet<GraphicsMode>	availModes			= new HashSet<GraphicsMode>(new GraphicsModeComparer());
		private	static	GraphicsMode			defaultMode			= null;
		private	static	MouseDevice				mouse				= null;
		private	static	KeyboardDevice			keyboard			= null;
		private	static	IList<JoystickDevice>	joysticks			= null;
		private	static	Random					rnd					= null;
		private	static	ExecutionContext		execContext			= ExecutionContext.Terminated;

		private	static	PluginSerializationBinder	pluginTypeBinder;
		private	static	Dictionary<string,Assembly>	plugins;
		private static	Dictionary<Type,List<Type>>	availTypeDict;


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
		public static IList<JoystickDevice> Joysticks
		{
			get { return joysticks; }
			set { joysticks = value; }
		}
		public static Random Rnd
		{
			get { return rnd; }
			set { rnd = value; if (rnd == null) rnd = new Random(); }
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


		public static void Init(ExecutionContext context = ExecutionContext.Unknown)
		{
			if (initialized) return;

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
			rnd = new Random((int)(DateTime.Now.Ticks % (long)int.MaxValue));
			plugins = new Dictionary<string,Assembly>();
			availTypeDict = new Dictionary<Type,List<Type>>();
			pluginTypeBinder = new PluginSerializationBinder();


			logfile = new StreamWriter("logfile.txt");
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

			LoadPlugins();

			initialized = true;
		}
		public static void Terminate(bool unexpected = false)
		{
			if (!initialized) return;

			if (unexpected)	Log.Core.WriteError("DualityApp terminated unexpectedly");
			else			Log.Core.Write("DualityApp terminated");

			logfile.Close();
			logfileRtf.save("logfile.rtf");

			initialized = false;
			execContext = ExecutionContext.Terminated;
		}

		public static void Update()
		{
			Time.FrameTick();
			Scene.Current.Update();
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
}
