using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	[Serializable]
	public class VertexShader : AbstractShader
	{
		public new const string FileExt = ".VertexShader" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "VertexShader:";
		public const string ContentPath_Minimal	= VirtualContentPath + "Minimal";

		public static ContentRef<VertexShader> Minimal	{ get; private set; }

		internal static void InitDefaultContent()
		{
			VertexShader tmp;

			tmp = new VertexShader(); tmp.path = ContentPath_Minimal;
			tmp.SetSource(
				"void main()" + Environment.NewLine +
				"{	" + Environment.NewLine +
				"	gl_Position = ftransform();" + Environment.NewLine +
				"	gl_TexCoord[0] = gl_MultiTexCoord0;" + Environment.NewLine +
				"	gl_FrontColor = gl_Color;" + Environment.NewLine +
				"}");
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Minimal	= ContentProvider.RequestContent<VertexShader>(ContentPath_Minimal);
		}


		protected override ShaderType OglShaderType
		{
			get { return ShaderType.VertexShader; }
		}

		public VertexShader() {}
	}
}
