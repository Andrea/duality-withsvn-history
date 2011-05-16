using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

using Duality;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	[Serializable]
	public class ShaderProgram : Resource
	{
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "ShaderProgram:";
		public const string ContentPath_Minimal	= VirtualContentPath + "Minimal";
		public const string ContentPath_Picking	= VirtualContentPath + "Picking";

		public static ContentRef<ShaderProgram> Minimal	{ get; private set; }
		public static ContentRef<ShaderProgram> Picking	{ get; private set; }

		internal static void InitDefaultContent()
		{
			ShaderProgram tmp;

			tmp = new ShaderProgram(VertexShader.Minimal, FragmentShader.Minimal); tmp.path = ContentPath_Minimal;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new ShaderProgram(VertexShader.Minimal, FragmentShader.Picking); tmp.path = ContentPath_Picking;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Minimal	= ContentProvider.RequestContent<ShaderProgram>(ContentPath_Minimal);
			Picking	= ContentProvider.RequestContent<ShaderProgram>(ContentPath_Picking);
		}

		public static readonly ContentRef<ShaderProgram> None	= ContentRef<ShaderProgram>.Null;
		private	static	ShaderProgram	curBound	= null;
		public static ContentRef<ShaderProgram> BoundProgram
		{
			get { return new ContentRef<ShaderProgram>(curBound); }
		}

		public static void Bind(ContentRef<ShaderProgram> prog)
		{
			ShaderProgram progRes = prog.IsExplicitNull ? null : prog.Res;
			if (curBound == progRes) return;

			if (progRes == null)
			{
				GL.UseProgram(0);
				curBound = null;
			}
			else
			{
				if (!progRes.compiled) progRes.Compile();

				if (progRes.glProgramId == 0)	throw new ArgumentException("Specified shader program has no valid OpenGL program Id! Maybe it hasn't been loaded / initialized properly?", "prog");
				if (progRes.Disposed)			throw new ArgumentException("Specified shader program has already been deleted!", "prog");
					
				GL.UseProgram(progRes.glProgramId);
				curBound = progRes;
			}
		}


		private	ContentRef<VertexShader>	vert	= ContentRef<VertexShader>.Null;
		private	ContentRef<FragmentShader>	frag	= ContentRef<FragmentShader>.Null;
		[NonSerialized] private	int				glProgramId	= 0;
		[NonSerialized] private bool			compiled	= false;
		[NonSerialized] private	ShaderVarInfo[]	varInfo		= null;

		public bool Compiled
		{
			get { return this.compiled; }
		}
		public ShaderVarInfo[] VarInfo
		{
			get { return this.varInfo; }
		}

		public ShaderProgram() {}
		public ShaderProgram(ContentRef<VertexShader> v, ContentRef<FragmentShader> f)
		{
			this.AttachShaders(v, f);
		}

		public void AttachShaders()
		{
			this.AttachShaders(this.vert, this.frag);
		}
		public void AttachShaders(ContentRef<VertexShader> v, ContentRef<FragmentShader> f)
		{
			if (this.glProgramId == 0)	this.glProgramId = GL.CreateProgram();
			else						this.DetachShaders();

			this.compiled = false;
			this.vert = v;
			this.frag = f;

			if (this.vert.IsAvailable) this.vert.Res.AttachTo(this.glProgramId);
			if (this.frag.IsAvailable) this.frag.Res.AttachTo(this.glProgramId);
		}
		public void DetachShaders()
		{
			if (this.glProgramId == 0) return;
			this.compiled = false;
			if (this.frag.IsAvailable) this.frag.Res.DetachFrom(this.glProgramId);
			if (this.vert.IsAvailable) this.vert.Res.DetachFrom(this.glProgramId);
		}

		public void Compile()
		{
			if (this.glProgramId == 0) return;
			if (this.compiled) return;

			GL.LinkProgram(this.glProgramId);

			int result;
			GL.GetProgram(this.glProgramId, ProgramParameter.LinkStatus, out result);
			if (result == 0)
			{
				string infoLog = GL.GetProgramInfoLog(this.glProgramId);
				throw new ApplicationException(string.Format(
					"Error compiling shader program. InfoLog: {0}",
					Environment.NewLine + infoLog));
			}
			this.compiled = true;

			// Collect variable infos from sub programs
			this.varInfo = new List<ShaderVarInfo>(this.vert.Res.VarInfo.Union(this.frag.Res.VarInfo)).ToArray();
			// Determine actual variable locations
			for (int i = 0; i < this.varInfo.Length; i++)
			{
				if (this.varInfo[i].scope == ShaderVarScope.Uniform)
					this.varInfo[i].glVarLoc = GL.GetUniformLocation(this.glProgramId, this.varInfo[i].name);
				else
					this.varInfo[i].glVarLoc = GL.GetAttribLocation(this.glProgramId, this.varInfo[i].name);
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.AttachShaders();
			this.Compile();
		}
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
				this.glProgramId != 0)
			{
				this.DetachShaders();
				GL.DeleteProgram(this.glProgramId);
				this.glProgramId = 0;
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			ShaderProgram c = r as ShaderProgram;
			c.AttachShaders(this.vert, this.frag);
			if (this.compiled) c.Compile();
		}
	}
}
