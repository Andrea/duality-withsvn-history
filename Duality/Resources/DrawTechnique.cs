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
	public class DrawTechnique : Resource
	{
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "DrawTechnique:";
		public const string ContentPath_Solid		= VirtualContentPath + "Solid";
		public const string ContentPath_Mask		= VirtualContentPath + "Mask";
		public const string ContentPath_Add			= VirtualContentPath + "Add";
		public const string ContentPath_Alpha		= VirtualContentPath + "Alpha";
		public const string ContentPath_Multiply	= VirtualContentPath + "Multiply";
		public const string ContentPath_Light		= VirtualContentPath + "Light";
		public const string ContentPath_Invert		= VirtualContentPath + "Invert";
		public const string ContentPath_Picking		= VirtualContentPath + "Picking";

		public static ContentRef<DrawTechnique> Solid		{ get; private set; }
		public static ContentRef<DrawTechnique> Mask		{ get; private set; }
		public static ContentRef<DrawTechnique> Add			{ get; private set; }
		public static ContentRef<DrawTechnique> Alpha		{ get; private set; }
		public static ContentRef<DrawTechnique> Multiply	{ get; private set; }
		public static ContentRef<DrawTechnique> Light		{ get; private set; }
		public static ContentRef<DrawTechnique> Invert		{ get; private set; }
		public static ContentRef<DrawTechnique> Picking		{ get; private set; }
		public static bool MaskUseAlphaToCoverage 
		{ 
			get 
			{
				if (RenderTarget.BoundRT.IsExplicitNull)
					return DualityApp.TargetMode.Samples > 0; 
				else
					return RenderTarget.BoundRT.Res.Samples > 0;
			}
		}

		internal static void InitDefaultContent()
		{
			DrawTechnique tmp;

			tmp = new DrawTechnique(BlendMode.Solid);
			tmp.path = ContentPath_Solid;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Mask);
			tmp.path = ContentPath_Mask;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Add);
			tmp.path = ContentPath_Add;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Alpha);
			tmp.path = ContentPath_Alpha;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Multiply);
			tmp.path = ContentPath_Multiply;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Light);
			tmp.path = ContentPath_Light;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Invert);
			tmp.path = ContentPath_Invert;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new DrawTechnique(BlendMode.Mask, ShaderProgram.Picking);
			tmp.path = ContentPath_Picking;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Solid		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Solid);
			Mask		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Mask);
			Add			= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Add);
			Alpha		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Alpha);
			Multiply	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Multiply);
			Light		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Light);
			Invert		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Invert);
			Picking		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Picking);
		}


		private	BlendMode					blendType	= BlendMode.Solid;
		private	ContentRef<ShaderProgram>	shader		= ContentRef<ShaderProgram>.Null;

		public BlendMode Blending
		{
			get { return this.blendType; }
			set { this.blendType = value; }
		}
		public ContentRef<ShaderProgram> Shader
		{
			get { return this.shader; }
			set { this.shader = value; }
		}
		public bool NeedsZSort
		{
			get 
			{ 
				return 
					this.blendType == BlendMode.Alpha ||
					this.blendType == BlendMode.Add ||
					this.blendType == BlendMode.Invert ||
					this.blendType == BlendMode.Multiply ||
					this.blendType == BlendMode.Light; 
			}
		}
		public virtual bool NeedsVertexPreprocess
		{
			get { return false; }
		}

		public DrawTechnique() {}
		public DrawTechnique(BlendMode blendType) 
		{
			this.blendType = blendType;
		}
		public DrawTechnique(BlendMode blendType, ContentRef<ShaderProgram> shader) 
		{
			this.blendType = blendType;
			this.shader = shader;
		}
		
		public virtual void PreprocessVertices<T>(ref BatchInfo material, ref BeginMode vertexMode, ref T[] vertices)
		{

		}
		public void SetupForRendering(DrawTechnique lastTechnique, Dictionary<string,ContentRef<Texture>> textures, Dictionary<string,float[]> uniforms)
		{
			// Setup BlendType
			if (lastTechnique == null || this.blendType != lastTechnique.blendType)
				this.SetupBlendType(this.blendType);

			// Bind Shader
			ContentRef<ShaderProgram> selShader = this.SelectShader();
			if (lastTechnique == null || selShader.Res != lastTechnique.shader.Res)
				ShaderProgram.Bind(selShader);

			// Setup shader data
			if (selShader.IsAvailable)
			{
				ShaderVarInfo[] varInfo = selShader.Res.VarInfo;

				// Setup sampler bindings automatically
				int curSamplerIndex = 0;
				if (textures != null && textures.Count > 0)
				{
					ContentRef<Texture> tex;
					for (int i = 0; i < varInfo.Length; i++)
					{
						if (varInfo[i].glVarLoc == -1) continue;
						if (varInfo[i].type != ShaderVarType.Sampler2D) continue;
						if (!textures.TryGetValue(varInfo[i].name, out tex)) continue;
						Texture.Bind(tex, curSamplerIndex);
						GL.Uniform1(varInfo[i].glVarLoc, curSamplerIndex);
						curSamplerIndex++;
					}
				}
				Texture.ResetBinding(curSamplerIndex);

				// Transfer uniform data from material to actual shader
				if (uniforms != null && uniforms.Count > 0)
				{
					float[] data = null;
					for (int i = 0; i < varInfo.Length; i++)
					{
						if (varInfo[i].glVarLoc == -1) continue;
						if (!uniforms.TryGetValue(varInfo[i].name, out data)) continue;
						varInfo[i].SetupUniform(data);
					}
				}
			}
			// Setup fixed function data
			else
			{
				// Fixed function texture binding
				if (textures != null && textures.Count > 0)
				{
					int samplerIndex = 0;
					foreach (var tex in textures.Values)
					{
						Texture.Bind(tex, samplerIndex);
						samplerIndex++;
					}
					Texture.ResetBinding(samplerIndex);
				}
				else
					Texture.ResetBinding();
			}
		}
		public void FinishRendering()
		{
			SetupBlendType(BlendMode.Reset);
			ShaderProgram.Bind(ContentRef<ShaderProgram>.Null);
		}

		protected void SetupBlendType(BlendMode mode)
		{
			switch (mode)
			{
				default:
				case BlendMode.Reset:
				case BlendMode.Solid:
					GL.DepthMask(true);
					GL.Disable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					break;
				case BlendMode.Mask:
					GL.DepthMask(true);
					GL.Disable(EnableCap.Blend);
					if (MaskUseAlphaToCoverage)
						GL.Enable(EnableCap.SampleAlphaToCoverage);
					else
					{
						GL.Enable(EnableCap.AlphaTest);
						GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
					}
					break;
				case BlendMode.Alpha:
					GL.DepthMask(false);
					GL.Enable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);
					break;
				case BlendMode.Add:
					GL.DepthMask(false);
					GL.Enable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One, BlendingFactorSrc.One, BlendingFactorDest.One);
					break;
				case BlendMode.Light:
					GL.DepthMask(false);
					GL.Enable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					GL.BlendFuncSeparate(BlendingFactorSrc.DstColor, BlendingFactorDest.One, BlendingFactorSrc.Zero, BlendingFactorDest.One);
					break;
				case BlendMode.Multiply:
					GL.DepthMask(false);
					GL.Enable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					GL.BlendFunc(BlendingFactorSrc.DstColor, BlendingFactorDest.Zero);
					break;
				case BlendMode.Invert:
					GL.DepthMask(false);
					GL.Enable(EnableCap.Blend);
					GL.Disable(EnableCap.AlphaTest);
					GL.Disable(EnableCap.SampleAlphaToCoverage);
					GL.BlendFunc(BlendingFactorSrc.OneMinusDstColor, BlendingFactorDest.OneMinusSrcColor);
					break;
			}
		}
		protected virtual ContentRef<ShaderProgram> SelectShader()
		{
			return this.shader;
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			DrawTechnique c = r as DrawTechnique;
			c.blendType = this.blendType;
		}
	}
}
