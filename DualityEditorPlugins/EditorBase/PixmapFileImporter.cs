using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DualityEditor;
using Duality.Resources;

namespace EditorBase
{
	public class PixmapFileImporter : IFileImporter
	{
		public bool CanImportFile(string srcFile)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			return ext == ".png" || ext == ".bmp" || ext == ".jpg";
		}
		public void ImportFile(string srcFile, string targetDir)
		{
			string[] output = this.GetOutputFiles(srcFile, targetDir);
			Pixmap res = new Pixmap(srcFile);
			res.Save(output[0]);
		}
		public string[] GetOutputFiles(string srcFile, string targetDir)
		{
			string targetResPath = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(srcFile));
			targetResPath += ".Pixmap.res";
			return new string[] { targetResPath };
		}
	}
}
