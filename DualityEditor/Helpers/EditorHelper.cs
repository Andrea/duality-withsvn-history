using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Win32;
using Microsoft.Build.Execution;

using Ionic.Zip;

using Duality;
using Duality.Serialization.MetaFormat;

namespace DualityEditor
{
	public static class EditorHelper
	{
		public const string DataDirectory				= @"Data";
		public const string PluginDirectory				= @"Plugins";
		public const string SourceDirectory				= @"Source";
		public const string SourceMediaDirectory		= SourceDirectory + @"\Media";
		public const string SourceCodeDirectory			= SourceDirectory + @"\Code";
		public const string SourceCodeProjectCorePluginDir		= SourceCodeDirectory + @"\CorePlugin";
		public const string SourceCodeProjectEditorPluginDir	= SourceCodeDirectory + @"\EditorPlugin";
		public const string SourceCodeSolutionFile				= SourceCodeDirectory + @"\ProjectPlugins.sln";
		public const string SourceCodeProjectCorePluginFile		= SourceCodeProjectCorePluginDir + @"\CorePlugin.csproj";
		public const string SourceCodeProjectEditorPluginFile	= SourceCodeProjectEditorPluginDir + @"\EditorPlugin.csproj";
		public const string SourceCodeGameResFile			= SourceCodeProjectCorePluginDir + @"\Properties\GameRes.cs";
		public const string SourceCodeCorePluginFile		= SourceCodeProjectCorePluginDir + @"\CorePlugin.cs";
		public const string SourceCodeComponentExampleFile	= SourceCodeProjectCorePluginDir + @"\YourCustomComponentType.cs";
		public const string SourceCodeEditorPluginFile		= SourceCodeProjectEditorPluginDir + @"\EditorPlugin.cs";

		public static readonly string GlobalUserDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Duality"));
		public static readonly string GlobalProjectTemplateDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Duality"), "ProjectTemplates");

		public static string CurrentProjectName
		{
			get
			{
				string dataFullPath = Path.GetFullPath(EditorHelper.DataDirectory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
				string dataDir = Path.GetDirectoryName(dataFullPath);
				return Path.GetFileName(dataDir);
			}
		}

		public static bool CopyDirectory(string sourcePath, string targetPath, bool overwrite = false, Predicate<string> filter = null)
		{
			if (!Directory.Exists(sourcePath)) return false;
			if (!overwrite && Directory.Exists(targetPath)) return false;

			if (!Directory.Exists(targetPath)) 
				Directory.CreateDirectory(targetPath);

			foreach (string file in Directory.GetFiles(sourcePath))
			{
				if (filter != null && !filter(file)) continue;
				File.Copy(file, Path.Combine(targetPath, Path.GetFileName(file)), overwrite);
			}
			foreach (string subDir in Directory.GetDirectories(sourcePath))
			{
				if (filter != null && !filter(subDir)) continue;
				CopyDirectory(subDir, Path.Combine(targetPath, Path.GetFileName(subDir)), overwrite);
			}

			return true;
		}
		public static bool IsJITDebuggerAvailable()
		{
			return Registry.LocalMachine
				.OpenSubKey("SOFTWARE")
				.OpenSubKey("Microsoft")
				.OpenSubKey(".NetFramework")
				.GetValueNames().Contains("DbgManagedDebugger");
		}

		public static void OpenResourceSrcFile(Resource r, string srcFileExt, Action<string> saveSrcToAction)
		{
			// Default content: Use temporary location
			if (r.Path.Contains(':'))
			{
				string tmpLoc = Path.Combine(Path.GetTempPath(), r.Path.Replace(':', '_')) + srcFileExt;
				saveSrcToAction(tmpLoc);
				System.Diagnostics.Process.Start(tmpLoc);
			}
			// Other content: Use permanent src file location
			else
			{
				string srcFilePath = r.SourcePath;
				if (String.IsNullOrEmpty(srcFilePath) || !File.Exists(srcFilePath))
				{
					srcFilePath = EditorHelper.GenerateResourceSrcFilePath(r, srcFileExt);
					Directory.CreateDirectory(Path.GetDirectoryName(srcFilePath));
					r.SourcePath = srcFilePath;
				}

				if (srcFilePath != null)
				{
					saveSrcToAction(srcFilePath);
					System.Diagnostics.Process.Start(srcFilePath);
				}
			}
		}
		public static string GenerateResourceSrcFilePath(Resource r, string srcFileExt)
		{
			string filePath = PathHelper.MakeFilePathRelative(r.Path, EditorHelper.DataDirectory);
			string fileDir = Path.GetDirectoryName(filePath);
			if (filePath.Contains(".."))
			{
				filePath = Path.GetFileName(filePath);
				fileDir = ".";
			}
			return PathHelper.GetFreePath(Path.Combine(Path.Combine(EditorHelper.SourceMediaDirectory, fileDir), r.Name), srcFileExt);
		}

		public static string GenerateGameResSrcFile()
		{
			string gameRes;
			using (StreamReader sr = new StreamReader(ReflectionHelper.GetEmbeddedResourceStream(typeof(EditorHelper).Assembly,  @"Resources\GameResTemplate.txt")))
			{
				gameRes = sr.ReadToEnd();
			}
			string mainClassName;
			return gameRes.Replace("CONTENT", GenerateGameResSrcFile_ScanDir(DataDirectory, 1, out mainClassName));
		}
		private static string GenerateGameResSrcFile_ScanFile(string filePath, int indent, out string propName)
		{
			if (!PathHelper.IsPathVisible(filePath)) { propName = null; return ""; }
			if (!Resource.IsResourceFile(filePath)) { propName = null; return ""; }

			StringBuilder fileContent = new StringBuilder();
			Type resType = Resource.GetTypeByFileName(filePath);
			string typeStr = resType.GetTypeCSCodeName();
			string indentStr = new string('\t', indent);
			propName = GenerateGameResSrcFile_ClassName(filePath);

			fileContent.Append(indentStr); 
			fileContent.Append("private static Duality.ContentRef<");
			fileContent.Append(typeStr);
			fileContent.Append("> _");
			fileContent.Append(propName);
			fileContent.AppendLine(";");

			fileContent.Append(indentStr); 
			fileContent.Append("public static Duality.ContentRef<");
			fileContent.Append(typeStr);
			fileContent.Append("> ");
			fileContent.Append(propName);
			fileContent.Append(" { get { ");
			fileContent.Append("if (_");
			fileContent.Append(propName);
			fileContent.Append(".IsExplicitNull) _");
			fileContent.Append(propName);
			fileContent.Append(" = Duality.ContentProvider.RequestContent<");
			fileContent.Append(typeStr);
			fileContent.Append(">(@\"");
			fileContent.Append(filePath);
			fileContent.Append("\"); ");
			fileContent.Append("return _");
			fileContent.Append(propName);
			fileContent.AppendLine("; }}");

			return fileContent.ToString();
		}
		private static string GenerateGameResSrcFile_ScanDir(string dirPath, int indent, out string className)
		{
			if (!PathHelper.IsPathVisible(dirPath)) { className = null; return ""; }
			dirPath = dirPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			StringBuilder dirContent = new StringBuilder();
			string indentStr = new string('\t', indent);
			className = GenerateGameResSrcFile_ClassName(dirPath);

			// ---------- Begin class ----------
			dirContent.Append(indentStr); 
			dirContent.Append("public static class ");
			dirContent.Append(className);
			dirContent.AppendLine(" {");

			// ---------- Sub directories ----------
			string[] subDirs = Directory.GetDirectories(dirPath);
			List<string> dirClassNames = new List<string>();
			foreach (string dir in subDirs)
			{
				string dirClassName;
				string dirCode = GenerateGameResSrcFile_ScanDir(dir, indent + 1, out dirClassName);
				if (!String.IsNullOrEmpty(dirCode))
				{
					dirContent.Append(dirCode);
					dirClassNames.Add(dirClassName);
				}
			}

			// ---------- Files ----------
			string[] files = Directory.GetFiles(dirPath);
			List<string> filePropNames = new List<string>();
			foreach (string file in files)
			{
				string propName;
				string fileCode = GenerateGameResSrcFile_ScanFile(file, indent + 1, out propName);
				if (!String.IsNullOrEmpty(fileCode))
				{
					dirContent.Append(fileCode);
					filePropNames.Add(propName);
				}
			}

			// ---------- LoadAll() method ----------
			dirContent.Append(indentStr); 
			dirContent.Append('\t'); 
			dirContent.AppendLine("public static void LoadAll() {");
			foreach (string dirClassName in dirClassNames)
			{
				dirContent.Append(indentStr); 
				dirContent.Append('\t'); 
				dirContent.Append('\t'); 
				dirContent.Append(dirClassName);
				dirContent.AppendLine(".LoadAll();");
			}
			foreach (string propName in filePropNames)
			{
				dirContent.Append(indentStr); 
				dirContent.Append('\t'); 
				dirContent.Append('\t'); 
				dirContent.Append(propName);
				dirContent.AppendLine(".MakeAvailable();");
			}
			dirContent.Append(indentStr); 
			dirContent.Append('\t'); 
			dirContent.AppendLine("}");

			// ---------- End class ----------
			dirContent.Append(indentStr); 
			dirContent.AppendLine("}");
			return dirContent.ToString();
		}
		private static string GenerateGameResSrcFile_ClassName(string path)
		{
			// Strip path and resource extension
			if (Resource.IsResourceFile(path))
				path = Path.GetFileNameWithoutExtension(path);
			else
				path = Path.GetFileName(path);

			return GenerateClassNameFromPath(path);
		}
		
		public static string GenerateClassNameFromPath(string path)
		{
			// Replace chars that aren't allowed as class name
			char[] pathChars = path.ToCharArray();
			for (int i = 0; i < pathChars.Length; i++)
			{
				if (!char.IsLetterOrDigit(pathChars[i]))
					pathChars[i] = '_';
			}
			// Do not allow beginning digit
			if (char.IsDigit(pathChars[0]))
				path = "_" + new string(pathChars);
			else
				path = new string(pathChars);

			// Avoid certain ambiguity
			if (path == "System")		path = "System_";
			else if (path == "Duality")	path = "Duality_";
			else if (path == "OpenTK")	path = "OpenTK_";

			return path;
		}


		public static string CreateNewProject(string projName, string projFolder, ProjectTemplateInfo template)
		{
			// Create project folder
			projFolder = Path.Combine(projFolder, projName);
			if (!Directory.Exists(projFolder)) Directory.CreateDirectory(projFolder);

			// Extract template
			if (template.SpecialTag == ProjectTemplateInfo.SpecialInfo.None)
			{
				template.ExtractTo(projFolder);

				// Update main directory
				foreach (string srcFile in Directory.GetFiles(Environment.CurrentDirectory, "*", SearchOption.TopDirectoryOnly))
				{
					if (Path.GetFileName(srcFile) == "appdata.dat") continue;
					if (Path.GetFileName(srcFile) == "defaultuserdata.dat") continue;
					string dstFile = Path.Combine(projFolder, Path.GetFileName(srcFile));
					File.Copy(srcFile, dstFile, true);
				}

				// Update plugin directory
				foreach (string dstFile in Directory.GetFiles(Path.Combine(projFolder, EditorHelper.PluginDirectory), "*", SearchOption.AllDirectories))
				{
					string srcFile = Path.Combine(EditorHelper.PluginDirectory, Path.GetFileName(dstFile));
					if (File.Exists(srcFile)) File.Copy(srcFile, dstFile, true);
				}
			}
			else if (template.SpecialTag == ProjectTemplateInfo.SpecialInfo.Current)
			{
				MainForm.Instance.SaveAllProjectData();
				CopyDirectory(Environment.CurrentDirectory, projFolder, true);
			}
			else
			{
				CopyDirectory(Environment.CurrentDirectory, projFolder, true, delegate(string path)
				{
					bool isDir = Directory.Exists(path);
					string fullPath = Path.GetFullPath(path);
					if (isDir)
					{
						return 
							fullPath != Path.GetFullPath(EditorHelper.DataDirectory) &&
							fullPath != Path.GetFullPath(EditorHelper.SourceDirectory);
					}
					else
					{
						string fileName = Path.GetFileName(fullPath);
						return fileName != "appdata.dat" && fileName != "defaultuserdata.dat";
					}
				});
			}

			// Adjust current directory for further operations
			string oldPath = Environment.CurrentDirectory;
			Environment.CurrentDirectory = projFolder;

			// Initialize content
			if (Directory.Exists(EditorHelper.DataDirectory))
			{
				// Read content source code data (needed to rename classes / namespaces)
				string oldRootNamespaceNameCore;
				string newRootNamespaceNameCore;
				MainForm.Instance.ReadPluginSourceCodeContentData(out oldRootNamespaceNameCore, out newRootNamespaceNameCore);

				// Rename classes / namespaces
				List<string> resFiles = Resource.GetResourceFiles(EditorHelper.DataDirectory);
				foreach (string resFile in resFiles)
				{
					MetaFormatHelper.FilePerformAction(resFile, d => d.ReplaceTypeStrings(oldRootNamespaceNameCore, newRootNamespaceNameCore), false);
				}
			}

			// Initialize source code
			MainForm.Instance.InitPluginSourceCode(); // Force re-init to update namespaces, etc.
			MainForm.Instance.UpdatePluginSourceCode();

			// Compile plugins
			var buildProperties = new Dictionary<string, string>();
			buildProperties["Configuration"] = "Release";
			var buildRequest = new BuildRequestData(EditorHelper.SourceCodeSolutionFile, buildProperties, null, new string[] { "Build" }, null);
			var buildParameters = new BuildParameters();
			var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);

			Environment.CurrentDirectory = oldPath;
			return Path.Combine(projFolder, "DualityEditor.exe");
		}



		private const int GW_HWNDNEXT = 2; // The next window is below the specified window
		private const int GW_HWNDPREV = 3; // The previous window is above

		[DllImport("user32.dll")]
		private static extern IntPtr GetTopWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindowVisible(IntPtr hWnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindow", SetLastError = true)]
		private static extern IntPtr GetNextWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int wFlag);

		public static List<Form> GetZSortedAppWindows()
		{
			List<Form> result = new List<Form>();

			IntPtr hwnd = GetTopWindow((IntPtr)null);
			while (hwnd != IntPtr.Zero)
			{
				// Get next window under the current handler
				hwnd = GetNextWindow(hwnd, GW_HWNDNEXT);

				try
				{
					Form frm = Form.FromHandle(hwnd) as Form;
					if (frm != null && Application.OpenForms.OfType<Form>().Contains(frm))
						result.Add(frm);
				}
				catch
				{
					// Weird behaviour: In some cases, trying to cast to a Form a handle of an object 
					// that isn't a form will just return null. In other cases, will throw an exception.
				}
			}

			return result;
		}
	}

	public class ProjectTemplateInfo
	{
		public enum SpecialInfo
		{
			None,
			Empty,
			Current
		}

		private string	file;
		private	Bitmap	icon;
		private	string	name;
		private	string	desc;
		private	SpecialInfo	specialTag;

		public string FilePath
		{
			get { return this.file; }
			set { this.file = value; }
		}
		public Bitmap Icon
		{
			get { return this.icon; }
			set { this.icon = value; }
		}
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public string Description
		{
			get { return this.desc; }
			set { this.desc = value; }
		}
		public SpecialInfo SpecialTag
		{
			get { return this.specialTag; }
			set { this.specialTag = value; }
		}

		public ProjectTemplateInfo() {}
		public ProjectTemplateInfo(string templatePath)
		{
			if (string.IsNullOrEmpty(templatePath)) throw new ArgumentNullException("templatePath");
			if (Path.GetExtension(templatePath) != ".zip") throw new ArgumentException("The specified template path is expected to be a .zip file.", "templatePath");
			if (!File.Exists(templatePath)) throw new FileNotFoundException("Template file does not exist", templatePath);

			using (FileStream str = File.OpenRead(templatePath)) { this.InitFrom(str); }
			this.file = templatePath;
		}
		public ProjectTemplateInfo(Stream templateStream)
		{
			this.InitFrom(templateStream);
		}

		public void ExtractTo(string dir)
		{
			if (string.IsNullOrWhiteSpace(this.file) || !File.Exists(this.file)) 
				throw new InvalidOperationException("Can't extract Project Template, because the template file is missing");

			using (ZipFile templateZip = ZipFile.Read(this.file))
			{
				templateZip.ExtractAll(dir, ExtractExistingFileAction.OverwriteSilently);
			}
			if (File.Exists(Path.Combine(dir, "TemplateIcon.png"))) File.Delete(Path.Combine(dir, "TemplateIcon.png"));
			if (File.Exists(Path.Combine(dir, "TemplateInfo.xml"))) File.Delete(Path.Combine(dir, "TemplateInfo.xml"));
		}
		public void InitFrom(Stream templateStream)
		{
			if (templateStream == null) throw new ArgumentNullException("templateStream");

			this.file = null;
			this.name = "Unknown";
			this.specialTag = SpecialInfo.None;

			using (ZipFile templateZip = ZipFile.Read(templateStream))
			{
				ZipEntry entryInfo = templateZip.FirstOrDefault(z => !z.IsDirectory && z.FileName == "TemplateInfo.xml");
				ZipEntry entryIcon = templateZip.FirstOrDefault(z => !z.IsDirectory && z.FileName == "TemplateIcon.png");

				if (entryIcon != null)
				{
					using (MemoryStream str = new MemoryStream())
					{
						entryIcon.Extract(str);
						str.Seek(0, SeekOrigin.Begin);
						this.icon = new Bitmap(str);
					}
				}

				if (entryInfo != null)
				{
					string xmlSource = null;
					using (MemoryStream str = new MemoryStream())
					{
						entryInfo.Extract(str);
						str.Seek(0, SeekOrigin.Begin);
							
						using (StreamReader reader = new StreamReader(str))
						{
							xmlSource = reader.ReadToEnd();
						}
					}

					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(xmlSource);

					XmlElement elemName = xmlDoc.DocumentElement["name"];
					if (elemName != null) this.name = elemName.InnerText;

					XmlElement elemDesc = xmlDoc.DocumentElement["description"];
					if (elemDesc != null) this.desc = elemDesc.InnerText;
				}
			}

			return;
		}
	}
}
