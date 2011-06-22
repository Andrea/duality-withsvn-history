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
		public const string ContentPath_Minimal		= VirtualContentPath + "Minimal";
		public const string ContentPath_SmoothAnim	= VirtualContentPath + "SmoothAnim";

		public static ContentRef<VertexShader> Minimal		{ get; private set; }
		public static ContentRef<VertexShader> SmoothAnim	{ get; private set; }

		internal static void InitDefaultContent()
		{
			VertexShader tmp;

			tmp = new VertexShader(); tmp.path = ContentPath_Minimal;
			tmp.LoadSource(ReflectionHelper.GetEmbeddedResourceStream(typeof(FragmentShader).Assembly, @"Resources\Default\Minimal.vert"));
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new VertexShader(); tmp.path = ContentPath_SmoothAnim;
			tmp.LoadSource(ReflectionHelper.GetEmbeddedResourceStream(typeof(FragmentShader).Assembly, @"Resources\Default\SmoothAnim.vert"));
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Minimal		= ContentProvider.RequestContent<VertexShader>(ContentPath_Minimal);
			SmoothAnim	= ContentProvider.RequestContent<VertexShader>(ContentPath_SmoothAnim);
		}


		protected override ShaderType OglShaderType
		{
			get { return ShaderType.VertexShader; }
		}

		public VertexShader() {}
	}
}
