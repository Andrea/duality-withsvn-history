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
	public class AudioDataFileImporter : IFileImporter
	{
		public bool CanImportFile(string srcFile)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			return ext == ".ogg";
		}
		public void ImportFile(string srcFile, string targetName, string targetDir)
		{
			string[] output = this.GetOutputFiles(srcFile, targetName, targetDir);
			AudioData res = new AudioData(srcFile);
			res.Save(output[0]);
		}
		public string[] GetOutputFiles(string srcFile, string targetName, string targetDir)
		{
			string targetResPath = PathHelper.GetFreePath(Path.Combine(targetDir, targetName), AudioData.FileExt);
			return new string[] { targetResPath };
		}


		public bool IsUsingSrcFile(Resource r, string srcFile)
		{
			AudioData a = r as AudioData;
			return a != null && a.OggVorbisDataBasePath == srcFile;
		}
		public void ReimportFile(Resource r, string srcFile)
		{
			AudioData a = r as AudioData;
			a.LoadOggVorbisData(srcFile);
			a.Save();
		}
	}
}
