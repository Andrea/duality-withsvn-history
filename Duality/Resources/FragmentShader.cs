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
	public class FragmentShader : AbstractShader
	{
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "FragmentShader:";
		public const string ContentPath_Minimal	= VirtualContentPath + "Minimal";
		public const string ContentPath_Picking	= VirtualContentPath + "Picking";

		public static ContentRef<FragmentShader> Minimal	{ get; private set; }
		public static ContentRef<FragmentShader> Picking	{ get; private set; }

		internal static void InitDefaultContent()
		{
			FragmentShader tmp;

			tmp = new FragmentShader(); tmp.path = ContentPath_Minimal;
			tmp.SetSource(
				"uniform sampler2D mainTex;" + Environment.NewLine +
				"void main()" + Environment.NewLine +
				"{" + Environment.NewLine +
				"	gl_FragColor = gl_Color * texture2D(mainTex, gl_TexCoord[0].st);" + Environment.NewLine +
				"}");
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new FragmentShader(); tmp.path = ContentPath_Picking;
			tmp.SetSource(
				"uniform sampler2D mainTex;" + Environment.NewLine +
				"void main()" + Environment.NewLine +
				"{" + Environment.NewLine +
				"	gl_FragColor = vec4(gl_Color.rgb, step(0.5, texture2D(mainTex, gl_TexCoord[0].st).a));" + Environment.NewLine +
				"}");
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Minimal	= ContentProvider.RequestContent<FragmentShader>(ContentPath_Minimal);
			Picking	= ContentProvider.RequestContent<FragmentShader>(ContentPath_Picking);
		}


		protected override ShaderType OglShaderType
		{
			get { return ShaderType.FragmentShader; }
		}

		public FragmentShader() {}
	}
}
