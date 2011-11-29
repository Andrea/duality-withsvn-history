using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Duality;

namespace DualityEditor
{
	public static class EditorHelper
	{
		public const string DataDirectory			= @"Data";
		public const string SourceDirectory			= @"Source";
		public const string SourceMediaDirectory	= @"Source\Media";
		public const string SourceCodeDirectory		= @"Source\Code";
		public const string SourceCodeSolutionFile	= @"Source\Code\ProjectPlugins.sln";
		public const string SourceCodeGameResFile	= @"Source\Code\CorePlugin\Properties\GameRes.cs";

		public static bool CopyDirectory(string sourcePath, string targetPath)
		{
			if (!Directory.Exists(sourcePath)) return false;
			if (Directory.Exists(targetPath)) return false;

			Directory.CreateDirectory(targetPath);
			foreach (string file in Directory.GetFiles(sourcePath))
				File.Copy(file, Path.Combine(targetPath, Path.GetFileName(file)));
			foreach (string subDir in Directory.GetDirectories(sourcePath))
				CopyDirectory(subDir, Path.Combine(targetPath, Path.GetFileName(subDir)));

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

		public static void OpenResourceSrcFile(Resource r, string srcFileExt, string srcFilePath, Action<string> saveSrcToAction)
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
				if (String.IsNullOrEmpty(srcFilePath))
					srcFilePath = EditorHelper.GenerateResourceSrcFilePath(r, srcFileExt);

				if (srcFilePath != null)
				{
					saveSrcToAction(srcFilePath);
					System.Diagnostics.Process.Start(srcFilePath);
				}
			}
		}
		public static string GenerateResourceSrcFilePath(Resource r, string srcFileExt)
		{
			string filePath = PathHelper.MakePathRelative(r.Path, EditorHelper.DataDirectory);
			if (filePath.Contains("..")) filePath = Path.GetFileName(filePath);

			string fileName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(r.Path));
			return PathHelper.GetFreePath(Path.Combine(EditorHelper.SourceMediaDirectory, fileName), srcFileExt);
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

			string ext = Path.GetExtension(filePath);
			if (ext.ToLower() != Resource.FileExt) { propName = null; return ""; }

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
			if (path.EndsWith(Resource.FileExt, StringComparison.InvariantCultureIgnoreCase))
				path = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
			else
				path = Path.GetFileName(path);

			// Replace chars that aren't allowed as class name
			char[] pathChars = path.ToCharArray();
			for (int i = 0; i < pathChars.Length; i++)
			{
				if (!char.IsLetterOrDigit(pathChars[i]))
					pathChars[i] = '_';
			}
			path = new string(pathChars);

			// Avoid certain ambiguity
			if (path == "System")		path = "System_";
			else if (path == "Duality")	path = "Duality_";
			else if (path == "OpenTK")	path = "OpenTK_";

			return path;
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
}
