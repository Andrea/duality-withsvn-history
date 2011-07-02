using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
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
				else if (!File.Exists(srcFilePath))
					srcFilePath = null;

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
			return PathHelper.GetFreePathName(Path.Combine(EditorHelper.SourceMediaDirectory, fileName), srcFileExt);
		}
	}
}
