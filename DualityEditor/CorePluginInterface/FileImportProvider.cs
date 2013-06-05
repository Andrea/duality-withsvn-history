using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Duality;

namespace DualityEditor.CorePluginInterface
{
	public interface IFileImporter
	{
		bool CanImportFile(string srcFile);
		void ImportFile(string srcFile, string targetName, string targetDir);
		string[] GetOutputFiles(string srcFile, string targetName, string targetDir);

		bool IsUsingSrcFile(Resource r, string srcFile);
		void ReimportFile(Resource r, string srcFile);
	}

	public static class FileImportProvider
	{
		public static bool IsImportFileExisting(string filePath)
		{
			string srcFilePath, targetName, targetDir;
			PrepareImportFilePaths(filePath, out srcFilePath, out targetName, out targetDir);

			// Does the source file already exist?
			if (File.Exists(srcFilePath)) return true;

			// Find an importer and check if one of its output files already exist
			IFileImporter importer = CorePluginRegistry.RequestFileImporter(i => i.CanImportFile(srcFilePath));
			return importer != null && importer.GetOutputFiles(srcFilePath, targetName, targetDir).Any(File.Exists);
		}
		public static bool ImportFile(string filePath)
		{
			// Determine & check paths
			string srcFilePath, targetName, targetDir;
			PrepareImportFilePaths(filePath, out srcFilePath, out targetName, out targetDir);

			// Find an importer to handle the file import
			IFileImporter importer = CorePluginRegistry.RequestFileImporter(i => i.CanImportFile(srcFilePath));
			if (importer != null)
			{
				try
				{
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
				} catch (Exception) { return false; }

				// Import it
				importer.ImportFile(srcFilePath, targetName, targetDir);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				return true;
			}
			else
				return false;
		}
		public static void ReimportFile(string filePath)
		{
			// Find an importer to handle the file import
			IFileImporter importer = CorePluginRegistry.RequestFileImporter(i => i.CanImportFile(filePath));
			if (importer == null) return;

			List<Resource> reimported = null;
			foreach (Resource r in ContentProvider.GetAvailContent<Resource>())
			{
				if (!importer.IsUsingSrcFile(r, filePath)) continue;
				try
				{
					importer.ReimportFile(r, filePath);
					DualityEditorApp.FlagResourceUnsaved(r);

					if (reimported == null) reimported = new List<Resource>();
					reimported.Add(r);
				}
				catch (Exception) 
				{
					Log.Editor.WriteError("Can't re-import file '{0}'", filePath);
				}
			}

			if (reimported != null)
				DualityEditorApp.NotifyObjPropChanged(null, new ObjectSelection((IEnumerable<object>)reimported));
		}
		public static void NotifyFileRenamed(string filePathOld, string filePathNew)
		{
			if (string.IsNullOrEmpty(filePathOld)) return;

			// Find an importer to handle the file rename
			IFileImporter importer = CorePluginRegistry.RequestFileImporter(i => i.CanImportFile(filePathOld));
			if (importer == null) return;

			foreach (Resource r in ContentProvider.GetAvailContent<Resource>())
			{
				if (!importer.IsUsingSrcFile(r, filePathOld)) continue;
				try
				{
					if (r.SourcePath == filePathOld) r.SourcePath = filePathNew;
					DualityEditorApp.FlagResourceUnsaved(r);
				}
				catch (Exception) 
				{
					Log.Editor.WriteError("There was an error internally renaming a source file '{0}' to '{1}'", filePathOld, filePathNew);
				}
			}
		}
		
		public static void OpenSourceFile(Resource r, string srcFileExt, Action<string> saveSrcToAction)
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
					srcFilePath = GenerateSourceFilePath(r, srcFileExt);
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
		public static string GenerateSourceFilePath(Resource r, string srcFileExt)
		{
			string filePath = PathHelper.MakeFilePathRelative(r.Path, DualityApp.DataDirectory);
			string fileDir = Path.GetDirectoryName(filePath);
			if (filePath.Contains(".."))
			{
				filePath = Path.GetFileName(filePath);
				fileDir = ".";
			}
			return PathHelper.GetFreePath(Path.Combine(Path.Combine(EditorHelper.SourceMediaDirectory, fileDir), r.Name), srcFileExt);
		}

		private static void PrepareImportFilePaths(string filePath, out string srcFilePath, out string targetName, out string targetDir)
		{
			srcFilePath = PathHelper.MakeFilePathRelative(filePath, DualityApp.DataDirectory);
			if (srcFilePath.Contains("..")) srcFilePath = Path.GetFileName(srcFilePath);

			targetDir = Path.GetDirectoryName(Path.Combine(DualityApp.DataDirectory, srcFilePath));
			targetName = Path.GetFileNameWithoutExtension(filePath);

			srcFilePath = PathHelper.GetFreePath(
				Path.Combine(EditorHelper.SourceMediaDirectory, Path.GetFileNameWithoutExtension(srcFilePath)), 
				Path.GetExtension(srcFilePath));
		}
	}
}
