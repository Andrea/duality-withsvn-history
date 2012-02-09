using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality
{
	/// <summary>
	/// Provides helper methods for handling <see cref="System.IO.Path">Paths</see>.
	/// </summary>
	public static class PathHelper
	{
		/// <summary>
		/// Returns a path that isn't taken yet.
		/// </summary>
		/// <param name="pathBase">The path to use as base for finding a available path.</param>
		/// <param name="pathExt">The (file) extension to add to the new path.</param>
		/// <returns>A path that doesn't relate to any existing file or directory.</returns>
		/// <example>
		/// Assuming the directory <c>C:\SomeDir\</c> contains the file <c>File.txt</c>,
		/// the code <c>PathHelper.GetFreePath(@"C:\SomeDir\File", ".txt");</c>
		/// will return <c>C:\SomeDir\File (2).txt</c>.
		/// </example>
		public static string GetFreePath(string pathBase, string pathExt)
		{
			int nameNum = 1;
			string path = pathBase + pathExt;
			while (Directory.Exists(path) || File.Exists(path))
			{
				nameNum++;
				path = pathBase + " (" + nameNum + ")" + pathExt;
			}
			return path;
		}

		/// <summary>
		/// Returns whether one path is a sub-path of another.
		/// </summary>
		/// <param name="path">The supposed sub-path.</param>
		/// <param name="baseDir">The (directory) path in which the supposed sub-path might be located in.</param>
		/// <returns>True, if <c>path</c> is a sub-path of <c>baseDir</c>.</returns>
		/// <example>
		/// <c>PathHelper.IsPathLocatedIn(@"C:\SomeDir\SubDir", @"C:\SomeDir")</c> will return true.
		/// </example>
		public static bool IsPathLocatedIn(string path, string baseDir)
		{
			bool baseDirIsFile = File.Exists(baseDir);
			if (baseDirIsFile) return false;

			path = Path.GetFullPath(path);
			baseDir = Path.GetFullPath(baseDir);
			do
			{
				path = Path.GetDirectoryName(path);
				if (path == baseDir) return true;
				if (path.Length < baseDir.Length) return false;
			} while (!String.IsNullOrEmpty(path));
			return false;
		}
		/// <summary>
		/// Returns the relative path from one path to another.
		/// </summary>
		/// <param name="path">The path to make relative.</param>
		/// <param name="relativeTo">The path to make it relative to.</param>
		/// <returns>A path that, if <see cref="System.IO.Path.Combine(string,string)">combined</see> with <c>relativeTo</c>, equals the original path.</returns>
		/// <example>
		/// <c>PathHelper.MakePathRelative(@"C:\SomeDir\SubDir\File.txt", @"C:\SomeDir")</c> will return <c>SubDir\File.txt</c>.
		/// </example>
		public static string MakePathRelative(string path, string relativeTo)
		{
			string dir		= Path.GetFullPath(path);
			string dirRel	= Path.GetFullPath(relativeTo);
			string fileName	= "";

			if (File.Exists(dir))			fileName	= Path.GetFileName(dir);
			if (!Directory.Exists(dir))		dir			= Path.GetDirectoryName(dir);
			if (!Directory.Exists(dirRel))	dirRel		= Path.GetDirectoryName(dirRel);

			// Different disk drive: Cannot generate relative path.
			if (Directory.GetDirectoryRoot(dir) != Directory.GetDirectoryRoot(dirRel))	return null;

			string		resultDir	= "";
			string[]	dirToken	= dir.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			string[]	dirRelToken	= dirRel.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			int numBackDir = dirRelToken.Length - dirToken.Length;
			int sameDirIndex = int.MaxValue;
			for (int i = 0; i < Math.Min(dirToken.Length, dirRelToken.Length); i++)
			{
				if (dirToken[i] != dirRelToken[i])
				{
					numBackDir = dirRelToken.Length - i;
					break;
				}
				else
				{
					sameDirIndex = i;
				}
			}

			// Go back until we've reached the smallest mutual directory
			if (numBackDir > 0)
			{
				resultDir = 
					Path.DirectorySeparatorChar + 
					(".." + Path.DirectorySeparatorChar).Multiply(numBackDir) + 
					resultDir;
			}

			// ... and then go to the desired path from there
			for (int i = sameDirIndex + 1; i < dirToken.Length; i++)
			{
				resultDir = Path.Combine(resultDir, dirToken[i]);
			}

			return Path.Combine(resultDir, fileName);
		}
		/// <summary>
		/// Returns a mutual base path of two different paths.
		/// </summary>
		/// <param name="path">The first path.</param>
		/// <param name="path2">The second path.</param>
		/// <returns>The mutual base path of both.</returns>
		/// <example>
		/// <c>PathHelper.GetMutualDirectory(@"C:\SomeDir\SubDir\File.txt", @"C:\SomeDir\SubDir2\File.txt")</c> will return <c>C:\SomeDir</c>.
		/// </example>
		public static string GetMutualDirectory(string path, string path2)
		{
			string dir		= Path.GetFullPath(path);
			string dirRel	= Path.GetFullPath(path2);
			string fileName	= "";

			if (File.Exists(dir))			fileName	= Path.GetFileName(dir);
			if (!Directory.Exists(dir))		dir			= Path.GetDirectoryName(dir);
			if (!Directory.Exists(dirRel))	dirRel		= Path.GetDirectoryName(dirRel);

			// Different disk drive: No mutual directory
			if (Directory.GetDirectoryRoot(dir) != Directory.GetDirectoryRoot(dirRel))	return null;

			string		resultDir	= "";
			string[]	dirToken	= dir.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			string[]	dirRelToken	= dirRel.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

			int numBackDir = dirRelToken.Length - dirToken.Length;
			int sameDirIndex = int.MaxValue;
			for (int i = 1; i < Math.Min(dirToken.Length, dirRelToken.Length); i++)
			{
				if (dirToken[i] != dirRelToken[i])
				{
					numBackDir = dirRelToken.Length - i;
					break;
				}
				else
				{
					sameDirIndex = i;
				}
			}

			// Go back until we've reached the smallest mutual directory
			if (numBackDir > 0)
			{
				resultDir = 
					Path.DirectorySeparatorChar + 
					(".." + Path.DirectorySeparatorChar).Multiply(numBackDir) + 
					resultDir;
			}

			return resultDir;
		}

		/// <summary>
		/// Takes a string that is supposed to be a file name and converts it into an
		/// actually valid file name, replacing special characters and so on.
		/// </summary>
		/// <param name="fileNameWithoutExt">A string that is supposed to be a file name.</param>
		/// <returns>A valid file name.</returns>
		public static string GetValidFileName(string fileNameWithoutExt)
		{
			char[] pathChars = fileNameWithoutExt.ToCharArray();
			for (int i = 0; i < pathChars.Length; i++)
			{
				if (!char.IsLetterOrDigit(pathChars[i]))
					pathChars[i] = '_';
			}
			fileNameWithoutExt = new string(pathChars);
			return fileNameWithoutExt;
		}

		/// <summary>
		/// Returns whether the specified file or directory is visible, i.e. not hidden.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool IsPathVisible(string path)
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo dirInfo = new DirectoryInfo(path);
				return (dirInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
			}
			else if (File.Exists(path))
			{
				FileInfo fileInfo = new FileInfo(path);
				return (fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
			}
			else return false;
		}
	}
}
