using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using Microsoft.Win32;

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
	}
}
