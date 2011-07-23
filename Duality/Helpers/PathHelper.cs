using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality
{
	public static class PathHelper
	{
		public static string GetFreePathName(string pathBase, string pathExt)
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

			// ... and then go to the desired path from there
			for (int i = sameDirIndex + 1; i < dirToken.Length; i++)
			{
				resultDir = Path.Combine(resultDir, dirToken[i]);
			}

			return Path.Combine(resultDir, fileName);
		}
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
	}
}
