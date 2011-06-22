using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

using Duality;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	public enum ShaderVarType
	{
		Unknown = -1,

		Int,
		Float,

		Vec2,
		Vec3,
		Vec4,

		Mat2,
		Mat3,
		Mat4,

		Sampler2D
	}

	public enum ShaderVarScope
	{
		Unknown = -1,

		Uniform,
		Attribute
	}
	
	[System.Diagnostics.DebuggerDisplay("ShaderVarInfo: {scope} {type} {name}[{arraySize}]")]
	public struct ShaderVarInfo
	{
		public	ShaderVarScope	scope;
		public	ShaderVarType	type;
		public	int				arraySize;
		public	string			name;
		public	int				glVarLoc;

		public void SetupUniform(float[] data)
		{
			if (this.glVarLoc == -1) return;
			switch (this.type)
			{
				case ShaderVarType.Int:
					int[] arrI = new int[this.arraySize];
					for (int j = 0; j < arrI.Length; j++) arrI[j] = (int)data[j];
					GL.Uniform1(this.glVarLoc, arrI.Length, arrI);
					break;
				case ShaderVarType.Float:
					GL.Uniform1(this.glVarLoc, data.Length, data);
					break;
				case ShaderVarType.Vec2:
					GL.Uniform2(this.glVarLoc, 1, data);
					break;
				case ShaderVarType.Vec3:
					GL.Uniform3(this.glVarLoc, 1, data);
					break;
				case ShaderVarType.Vec4:
					GL.Uniform4(this.glVarLoc, 1, data);
					break;
				case ShaderVarType.Mat2:
					GL.UniformMatrix2(this.glVarLoc, 1, false, data);
					break;
				case ShaderVarType.Mat3:
					GL.UniformMatrix3(this.glVarLoc, 1, false, data);
					break;
				case ShaderVarType.Mat4:
					GL.UniformMatrix4(this.glVarLoc, 1, false, data);
					break;
			}
		}
		public float[] InitDataByType()
		{
			switch (this.type)
			{
				case ShaderVarType.Int:
					return new float[this.arraySize];
				case ShaderVarType.Float:
					return new float[this.arraySize];
				case ShaderVarType.Vec2:
					return new float[2];
				case ShaderVarType.Vec3:
					return new float[3];
				case ShaderVarType.Vec4:
					return new float[4];
				case ShaderVarType.Mat2:
					return new float[4];
				case ShaderVarType.Mat3:
					return new float[9];
				case ShaderVarType.Mat4:
					return new float[16];
			}
			return null;
		}
	}

	[Serializable]
	public abstract class AbstractShader : Resource
	{
		protected	string	source		= null;
		protected	string	sourcePath	= null;
		[NonSerialized] protected	int				glShaderId	= 0;
		[NonSerialized] protected	bool			compiled	= false;
		[NonSerialized] protected	ShaderVarInfo[]	varInfo		= null;

		protected abstract ShaderType OglShaderType { get; }
		public bool Compiled
		{
			get { return this.compiled; }
		}
		public ShaderVarInfo[] VarInfo
		{
			get { return this.varInfo; }
		}
		public string SourcePath
		{
			get { return this.sourcePath; }
		}

		public void SetSource(string source)
		{
			this.compiled = false;
			this.sourcePath = null;
			this.source = source;
		}
		public void LoadSource(Stream stream)
		{
			StreamReader reader = new StreamReader(stream);

			this.compiled = false;
			this.sourcePath = null;
			this.source = reader.ReadToEnd();
		}
		public void LoadSource(string filePath = null)
		{
			if (filePath == null) filePath = this.sourcePath;

			this.compiled = false;
			this.sourcePath = filePath;
			this.source = "";
			if (!File.Exists(this.sourcePath)) return;

			this.source = File.ReadAllText(this.sourcePath);
		}
		public void SaveSource(string filePath = null)
		{
			if (filePath == null) filePath = this.sourcePath;

			// We're saving this data for the first time
			if (!this.path.Contains(':') && this.sourcePath == null) this.sourcePath = filePath;

			File.WriteAllText(filePath, this.source);
		}

		public void Compile()
		{
			if (this.compiled) return;
			if (String.IsNullOrEmpty(this.source)) return;
			if (this.glShaderId == 0) this.glShaderId = GL.CreateShader(this.OglShaderType);
			GL.ShaderSource(this.glShaderId, this.source);
			GL.CompileShader(this.glShaderId);

			int result;
			GL.GetShader(this.glShaderId, ShaderParameter.CompileStatus, out result);
			if (result == 0)
			{
				string infoLog = GL.GetShaderInfoLog(this.glShaderId);
				Log.Core.WriteError("Error compiling {0}. InfoLog:\n{1}", this.OglShaderType, infoLog);
				return;
			}
			this.compiled = true;

			// Remove comments from source code before extracting variables
			string sourceWithoutComments;
			{
				string blockComments = @"/\*(.*?)\*/";
				string lineComments = @"//(.*?)\r?\n";
				string strings = @"""((\\[^\n]|[^""\n])*)""";
				string verbatimStrings = @"@(""[^""]*"")+";
				sourceWithoutComments = Regex.Replace(this.source,
					blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
					me => {
						if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
							return me.Value.StartsWith("//") ? Environment.NewLine : "";
						// Keep the literal strings
						return me.Value;
					},
					RegexOptions.Singleline);
			}

			// Scan remaining code chunk for variable declarations
			List<ShaderVarInfo> varInfoList = new List<ShaderVarInfo>();
			string[] lines = sourceWithoutComments.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
			ShaderVarInfo varInfo = new ShaderVarInfo();;
			for (int i = 0; i < lines.Length; i++)
			{
				string curLine = lines[i].TrimStart();

				if (curLine.StartsWith("uniform"))
					varInfo.scope = ShaderVarScope.Uniform;
				else if (curLine.StartsWith("attribute"))
					varInfo.scope = ShaderVarScope.Attribute;
				else continue;

				string[] curLineSplit = curLine.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
				switch (curLineSplit[1].ToUpper())
				{
					case "FLOAT":		varInfo.type = ShaderVarType.Float; break;
					case "VEC2":		varInfo.type = ShaderVarType.Vec2; break;
					case "VEC3":		varInfo.type = ShaderVarType.Vec3; break;
					case "VEC4":		varInfo.type = ShaderVarType.Vec4; break;
					case "MAT2":		varInfo.type = ShaderVarType.Mat2; break;
					case "MAT3":		varInfo.type = ShaderVarType.Mat3; break;
					case "MAT4":		varInfo.type = ShaderVarType.Mat4; break;
					case "INT":			varInfo.type = ShaderVarType.Int; break;
					case "SAMPLER2D":	varInfo.type = ShaderVarType.Sampler2D; break;
				}

				curLineSplit = curLineSplit[2].Split(new char[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries);
				varInfo.name = curLineSplit[0];
				varInfo.arraySize = (curLineSplit.Length > 1) ? int.Parse(curLineSplit[1]) : 1;
				varInfo.glVarLoc = -1;

				varInfoList.Add(varInfo);
			}

			this.varInfo = varInfoList.ToArray();
		}
		
		internal void AttachTo(int glProgId)
		{
			if (!this.compiled) this.Compile();
			GL.AttachShader(glProgId, this.glShaderId);
		}
		internal void DetachFrom(int glProgId)
		{
			GL.DetachShader(glProgId, this.glShaderId);
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.Compile();
		}
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
				this.glShaderId != 0)
			{
				GL.DeleteShader(this.glShaderId);
				this.glShaderId = 0;
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			AbstractShader c = r as AbstractShader;
			c.source = this.source;
			c.sourcePath = this.sourcePath;
			if (this.compiled) c.Compile();
		}
	}
}
