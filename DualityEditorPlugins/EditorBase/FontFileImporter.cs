﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality;
using Duality.Resources;

using DualityEditor;

namespace EditorBase
{
	public class FontFileImporter : IFileImporter
	{
		public bool CanImportFile(string srcFile)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			return ext == ".ttf";
		}
		public void ImportFile(string srcFile, string targetName, string targetDir)
		{
			string[] output = this.GetOutputFiles(srcFile, targetName, targetDir);
			Font res = new Font();
			res.LoadCustomFamilyData(srcFile);
			res.ReloadData();
			res.Save(output[0]);
		}
		public string[] GetOutputFiles(string srcFile, string targetName, string targetDir)
		{
			string targetResPath = PathHelper.GetFreePathName(Path.Combine(targetDir, targetName), Font.FileExt);
			return new string[] { targetResPath };
		}


		public bool IsUsingSrcFile(Resource r, string srcFile)
		{
			Font f = r as Font;
			return f != null && f.CustomFamilyData != null && f.CustomFamilyBasePath == srcFile;
		}
		public void ReimportFile(Resource r, string srcFile)
		{
			Font f = r as Font;
			f.LoadCustomFamilyData(srcFile);
			f.ReloadData();
			f.Save();
		}
	}
}