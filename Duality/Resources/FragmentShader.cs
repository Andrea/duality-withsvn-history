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
		public new const string FileExt = ".FragmentShader" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "FragmentShader:";
		public const string ContentPath_Minimal		= VirtualContentPath + "Minimal";
		public const string ContentPath_Picking		= VirtualContentPath + "Picking";
		public const string ContentPath_SmoothAnim	= VirtualContentPath + "SmoothAnim";

		public static ContentRef<FragmentShader> Minimal	{ get; private set; }
		public static ContentRef<FragmentShader> Picking	{ get; private set; }
		public static ContentRef<FragmentShader> SmoothAnim	{ get; private set; }

		internal static void InitDefaultContent()
		{
			FragmentShader tmp;

			tmp = new FragmentShader(); tmp.path = ContentPath_Minimal;
			tmp.LoadSource(ReflectionHelper.GetEmbeddedResourceStream(typeof(FragmentShader).Assembly, @"Resources\Default\Minimal.frag"));
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new FragmentShader(); tmp.path = ContentPath_Picking;
			tmp.LoadSource(ReflectionHelper.GetEmbeddedResourceStream(typeof(FragmentShader).Assembly, @"Resources\Default\Picking.frag"));
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new FragmentShader(); tmp.path = ContentPath_SmoothAnim;
			tmp.LoadSource(ReflectionHelper.GetEmbeddedResourceStream(typeof(FragmentShader).Assembly, @"Resources\Default\SmoothAnim.frag"));
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Minimal		= ContentProvider.RequestContent<FragmentShader>(ContentPath_Minimal);
			Picking		= ContentProvider.RequestContent<FragmentShader>(ContentPath_Picking);
			SmoothAnim	= ContentProvider.RequestContent<FragmentShader>(ContentPath_SmoothAnim);
		}


		protected override ShaderType OglShaderType
		{
			get { return ShaderType.FragmentShader; }
		}

		public FragmentShader() {}
	}
}
