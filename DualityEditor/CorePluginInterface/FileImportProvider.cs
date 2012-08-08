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
		void NotifySrcRenamed(Resource r, string srcFileOld, string srcFileNew);
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

			foreach (Resource r in ContentProvider.GetAvailContent<Resource>())
			{
				if (!importer.IsUsingSrcFile(r, filePath)) continue;
				try
				{
					importer.ReimportFile(r, filePath);
					MainForm.Instance.FlagResourceUnsaved(r);
				}
				catch (Exception) 
				{
					Log.Editor.WriteError("Can't re-import file '{0}'", filePath);
				}
			}
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
					importer.NotifySrcRenamed(r, filePathOld, filePathNew);
					MainForm.Instance.FlagResourceUnsaved(r);
				}
				catch (Exception) 
				{
					Log.Editor.WriteError("There was an error internally renaming a source file '{0}' to '{1}'", filePathOld, filePathNew);
				}
			}
		}

		private static void PrepareImportFilePaths(string filePath, out string srcFilePath, out string targetName, out string targetDir)
		{
			srcFilePath = PathHelper.MakeFilePathRelative(filePath, EditorHelper.DataDirectory);
			if (srcFilePath.Contains("..")) srcFilePath = Path.GetFileName(srcFilePath);

			targetDir = Path.GetDirectoryName(Path.Combine(EditorHelper.DataDirectory, srcFilePath));
			targetName = Path.GetFileNameWithoutExtension(filePath);

			srcFilePath = PathHelper.GetFreePath(
				Path.Combine(EditorHelper.SourceMediaDirectory, Path.GetFileNameWithoutExtension(srcFilePath)), 
				Path.GetExtension(srcFilePath));
		}
	}
}
