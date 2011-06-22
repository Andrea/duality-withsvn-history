using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality;
using Duality.Resources;

using DualityEditor;

namespace EditorBase
{
	public class PixmapFileImporter : IFileImporter
	{
		public bool CanImportFile(string srcFile)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			return ext == ".png" || ext == ".bmp" || ext == ".jpg";
		}
		public void ImportFile(string srcFile, string targetName, string targetDir)
		{
			string[] output = this.GetOutputFiles(srcFile, targetName, targetDir);
			Pixmap res = new Pixmap(srcFile);
			res.Save(output[0]);
		}
		public string[] GetOutputFiles(string srcFile, string targetName, string targetDir)
		{
			string targetResPath = PathHelper.GetFreePathName(Path.Combine(targetDir, targetName), Pixmap.FileExt);
			return new string[] { targetResPath };
		}


		public bool IsUsingSrcFile(Resource r, string srcFile)
		{
			Pixmap p = r as Pixmap;
			return p != null && p.PixelDataBasePath == srcFile;
		}
		public void ReimportFile(Resource r, string srcFile)
		{
			Pixmap p = r as Pixmap;
			p.LoadPixelData(srcFile);
			p.Save();
		}
	}
}
