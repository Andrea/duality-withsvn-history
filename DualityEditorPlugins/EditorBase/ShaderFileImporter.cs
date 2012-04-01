﻿using System.IO;

using Duality;
using Duality.Resources;

using DualityEditor;

namespace EditorBase
{
	public class ShaderFileImporter : IFileImporter
	{
		public bool CanImportFile(string srcFile)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			return ext == ".vert" || ext == ".frag";
		}
		public void ImportFile(string srcFile, string targetName, string targetDir)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			string[] output = this.GetOutputFiles(srcFile, targetName, targetDir);
			if (ext == ".vert")
			{
				VertexShader res = new VertexShader();
				res.LoadSource(srcFile);
				res.Compile();
				res.Save(output[0]);
			}
			else
			{
				FragmentShader res = new FragmentShader();
				res.LoadSource(srcFile);
				res.Compile();
				res.Save(output[0]);
			}
		}
		public string[] GetOutputFiles(string srcFile, string targetName, string targetDir)
		{
			string ext = Path.GetExtension(srcFile).ToLower();
			string targetResPath;
			if (ext == ".vert")
				targetResPath = PathHelper.GetFreePath(Path.Combine(targetDir, targetName), VertexShader.FileExt);
			else
				targetResPath = PathHelper.GetFreePath(Path.Combine(targetDir, targetName), FragmentShader.FileExt);
			return new string[] { targetResPath };
		}


		public bool IsUsingSrcFile(Resource r, string srcFile)
		{
			AbstractShader s = r as AbstractShader;
			return s != null && s.SourcePath == srcFile;
		}
		public void ReimportFile(Resource r, string srcFile)
		{
			AbstractShader s = r as AbstractShader;
			s.LoadSource(srcFile);
			s.Compile();
			s.Save();

			// Recompile ShaderPrograms depending on this
			foreach (ShaderProgram p in ContentProvider.GetAvailContent<ShaderProgram>())
			{
				if (p.Vertex.Res != s && p.Fragment.Res != s) continue;
				p.AttachShaders();
				p.Compile();
			}
		}
	}
}
