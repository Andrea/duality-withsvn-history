﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality;
using Duality.Resources;

namespace DualityEditor
{
	public interface IFileImporter
	{
		bool CanImportFile(string srcFile);
		void ImportFile(string srcFile, string targetDir);
		string[] GetOutputFiles(string srcFile, string targetDir);
	}
}
