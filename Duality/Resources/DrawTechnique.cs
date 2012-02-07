﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.VertexFormat;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	/// <summary>
	/// DrawTechniques represent the method by which a set of colors, <see cref="Duality.Resources.Texture">Textures</see> and
	/// vertex data is applied to screen. 
	/// </summary>
	/// <seealso cref="Duality.Resources.Material"/>
	/// <seealso cref="Duality.Resources.ShaderProgram"/>
	/// <seealso cref="Duality.BlendMode"/>
	[Serializable]
	public class DrawTechnique : Resource
	{
		/// <summary>
		/// A DrawTechnique resources file extension.
		/// </summary>
		public new static string FileExt
		{ 
			get { return ".DrawTechnique" + Resource.FileExt; }
		}
		
		/// <summary>
		/// (Virtual) base path for Duality's embedded default DrawTechniques.
		/// </summary>
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "DrawTechnique:";
		/// <summary>
		/// (Virtual) base path for Duality's embedded SmoothAnim DrawTechniques.
		/// </summary>
		public const string ContentDir_SmoothAnim	= VirtualContentPath + "SmoothAnim:";
		
		/// <summary>
		/// (Virtual) path of the <see cref="Solid"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Solid		= VirtualContentPath + "Solid";
		/// <summary>
		/// (Virtual) path of the <see cref="Mask"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Mask		= VirtualContentPath + "Mask";
		/// <summary>
		/// (Virtual) path of the <see cref="Add"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Add			= VirtualContentPath + "Add";
		/// <summary>
		/// (Virtual) path of the <see cref="Alpha"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Alpha		= VirtualContentPath + "Alpha";
		/// <summary>
		/// (Virtual) path of the <see cref="Multiply"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Multiply	= VirtualContentPath + "Multiply";
		/// <summary>
		/// (Virtual) path of the <see cref="Light"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Light		= VirtualContentPath + "Light";
		/// <summary>
		/// (Virtual) path of the <see cref="Invert"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Invert		= VirtualContentPath + "Invert";
		/// <summary>
		/// (Virtual) path of the <see cref="Picking"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_Picking		= VirtualContentPath + "Picking";
		
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Solid"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Solid	= ContentDir_SmoothAnim + "Solid";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Mask"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Mask		= ContentDir_SmoothAnim + "Mask";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Add"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Add		= ContentDir_SmoothAnim + "Add";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Alpha"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Alpha	= ContentDir_SmoothAnim + "Alpha";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Multiply"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Multiply	= ContentDir_SmoothAnim + "Multiply";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Light"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Light	= ContentDir_SmoothAnim + "Light";
		/// <summary>
		/// (Virtual) path of the <see cref="SmoothAnim_Invert"/> DrawTechnique.
		/// </summary>
		public const string ContentPath_SmoothAnim_Invert	= ContentDir_SmoothAnim + "Invert";
		
		/// <summary>
		/// Renders solid geometry without utilizing the alpha channel. This is the fastest default DrawTechnique.
		/// </summary>
		public static ContentRef<DrawTechnique> Solid		{ get; private set; }
		/// <summary>
		/// Renders alpha-masked solid geometry. This is the recommended DrawTechnique for regular sprite rendering.
		/// If multisampling is available, it is utilized to smooth masked edges.
		/// </summary>
		public static ContentRef<DrawTechnique> Mask		{ get; private set; }
		/// <summary>
		/// Renders additive geometry. Ideal for glow effects.
		/// </summary>
		public static ContentRef<DrawTechnique> Add			{ get; private set; }
		/// <summary>
		/// Renders geometry and using the alpha channel. However, for stencil-sharp alpha edges, <see cref="Mask"/> might
		/// be sufficient and is a lot faster. Consider using it.
		/// </summary>
		public static ContentRef<DrawTechnique> Alpha		{ get; private set; }
		/// <summary>
		/// Renders geometry multiplying the existing background with incoming color values. Can be used for shadowing effects.
		/// </summary>
		public static ContentRef<DrawTechnique> Multiply	{ get; private set; }
		/// <summary>
		/// Renders geometry adding incoming color values weighted based on the existing background. Can be used for lighting effects.
		/// </summary>
		public static ContentRef<DrawTechnique> Light		{ get; private set; }
		/// <summary>
		/// Renders geometry inverting the background color.
		/// </summary>
		public static ContentRef<DrawTechnique> Invert		{ get; private set; }
		/// <summary>
		/// Renders geometry for a picking operation. This isn't used for regular rendering.
		/// </summary>
		public static ContentRef<DrawTechnique> Picking		{ get; private set; }
		
		/// <summary>
		/// Renders SmoothAnim solid geometry without utilizing the alpha channel. This is the fastest default DrawTechnique.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Solid	{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim alpha-masked solid geometry. This is the recommended DrawTechnique for regular sprite rendering.
		/// If multisampling is available, it is utilized to smooth masked edges.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Mask		{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim additive geometry. Ideal for glow effects.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Add		{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim geometry and using the alpha channel. However, for stencil-sharp alpha edges, <see cref="Mask"/> might
		/// be sufficient and is a lot faster. Consider using it.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Alpha	{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim geometry multiplying the existing background with incoming color values. Can be used for shadowing effects.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Multiply	{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim geometry adding incoming color values weighted based on the existing background. Can be used for lighting effects.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Light	{ get; private set; }
		/// <summary>
		/// Renders SmoothAnim geometry inverting the background color.
		/// </summary>
		public static ContentRef<DrawTechnique> SmoothAnim_Invert	{ get; private set; }

		/// <summary>
		/// [GET] Returns whether the <see cref="BlendMode.Mask">masked</see> DrawTechniques utilize OpenGL Alpha-to-Coverage to
		/// smooth alpha-edges in masked rendering.
		/// </summary>
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
			ContentProvider.RegisterContent(ContentPath_Solid,		new DrawTechnique(BlendMode.Solid));
			ContentProvider.RegisterContent(ContentPath_Mask,		new DrawTechnique(BlendMode.Mask));
			ContentProvider.RegisterContent(ContentPath_Add,		new DrawTechnique(BlendMode.Add));
			ContentProvider.RegisterContent(ContentPath_Alpha,		new DrawTechnique(BlendMode.Alpha));
			ContentProvider.RegisterContent(ContentPath_Multiply,	new DrawTechnique(BlendMode.Multiply));
			ContentProvider.RegisterContent(ContentPath_Light,		new DrawTechnique(BlendMode.Light));
			ContentProvider.RegisterContent(ContentPath_Invert,		new DrawTechnique(BlendMode.Invert));

			ContentProvider.RegisterContent(ContentPath_Picking,	new DrawTechnique(BlendMode.Mask, ShaderProgram.Picking));
			
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Solid,		new DrawTechnique(BlendMode.Solid,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Mask,		new DrawTechnique(BlendMode.Mask,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Add,			new DrawTechnique(BlendMode.Add,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Alpha,		new DrawTechnique(BlendMode.Alpha,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Multiply,	new DrawTechnique(BlendMode.Multiply,	ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Light,		new DrawTechnique(BlendMode.Light,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));
			ContentProvider.RegisterContent(ContentPath_SmoothAnim_Invert,		new DrawTechnique(BlendMode.Invert,		ShaderProgram.SmoothAnim, VertexDataFormat.VertexC1P3T4A1));

			Solid		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Solid);
			Mask		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Mask);
			Add			= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Add);
			Alpha		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Alpha);
			Multiply	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Multiply);
			Light		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Light);
			Invert		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Invert);
			Picking		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_Picking);

			SmoothAnim_Solid	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Solid);
			SmoothAnim_Mask		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Mask);
			SmoothAnim_Add		= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Add);
			SmoothAnim_Alpha	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Alpha);
			SmoothAnim_Multiply	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Multiply);
			SmoothAnim_Light	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Light);
			SmoothAnim_Invert	= ContentProvider.RequestContent<DrawTechnique>(ContentPath_SmoothAnim_Invert);
		}


		private	BlendMode					blendType	= BlendMode.Solid;
		private	ContentRef<ShaderProgram>	shader		= ContentRef<ShaderProgram>.Null;
		private	VertexDataFormat			formatPref	= VertexDataFormat.Unknown;

		/// <summary>
		/// [GET / SET] Specifies how incoming color values interact with the existing background color.
		/// </summary>
		public BlendMode Blending
		{
			get { return this.blendType; }
			set { this.blendType = value; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.ShaderProgram"/> that is used for rendering.
		/// </summary>
		public ContentRef<ShaderProgram> Shader
		{
			get { return this.shader; }
			set { this.shader = value; }
		}
		/// <summary>
		/// [GET / SET] The vertex format that is preferred by this DrawTechnique. If there is no specific preference,
		/// <see cref="VertexDataFormat.Unknown"/> is returned.
		/// </summary>
		public VertexDataFormat PreferredVertexFormat
		{
			get { return this.formatPref; }
			set { this.formatPref = value; }
		}
		/// <summary>
		/// [GET] Returns whether this DrawTechnique requires z sorting. It is derived from its <see cref="Blending"/>.
		/// </summary>
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
		/// <summary>
		/// [GET] Returns whether this DrawTechnique requires <see cref="PreprocessVertices{T}">vertex preprocessing</see>.
		/// This is false for all standard DrawTechniques, but may return true when deriving custom DrawTechniques.
		/// </summary>
		public virtual bool NeedsVertexPreprocess
		{
			get { return false; }
		}

		/// <summary>
		/// Creates a new, default DrawTechnique
		/// </summary>
		public DrawTechnique() {}
		/// <summary>
		/// Creates a new DrawTechnique that uses the specified <see cref="BlendMode"/>.
		/// </summary>
		/// <param name="blendType"></param>
		public DrawTechnique(BlendMode blendType) 
		{
			this.blendType = blendType;
		}
		/// <summary>
		/// Creates a new DrawTechnique using the specified <see cref="BlendMode"/> and <see cref="Duality.Resources.ShaderProgram"/>.
		/// </summary>
		/// <param name="blendType"></param>
		/// <param name="shader"></param>
		/// <param name="formatPref"></param>
		public DrawTechnique(BlendMode blendType, ContentRef<ShaderProgram> shader, VertexDataFormat formatPref = VertexDataFormat.Unknown) 
		{
			this.blendType = blendType;
			this.shader = shader;
			this.formatPref = formatPref;
		}
		
		/// <summary>
		/// Performs a preprocessing operation for incoming vertices. Does nothing by default but may be overloaded, if needed.
		/// </summary>
		/// <typeparam name="T">The incoming vertex type</typeparam>
		/// <param name="material"><see cref="Duality.Resources.Material"/> information for the current batch.</param>
		/// <param name="vertexMode">The mode of incoming vertex data.</param>
		/// <param name="vertices">Incoming vertex data.</param>
		public virtual void PreprocessVertices<T>(ref BatchInfo material, ref BeginMode vertexMode, ref T[] vertices)
		{

		}
		/// <summary>
		/// Sets up the appropriate OpenGL rendering state for this DrawTechnique.
		/// </summary>
		/// <param name="lastTechnique">The last DrawTechnique that has been set up. This parameter is optional, but
		/// specifying it will increase performance by reducing redundant state changes.</param>
		/// <param name="textures">A set of <see cref="Duality.Resources.Texture">Textures</see> to use.</param>
		/// <param name="uniforms">A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to apply.</param>
		public void SetupForRendering(DrawTechnique lastTechnique, Dictionary<string,ContentRef<Texture>> textures, Dictionary<string,float[]> uniforms)
		{
			// Setup BlendType
			if (lastTechnique == null || this.blendType != lastTechnique.blendType)
				this.SetupBlendType(this.blendType);

			// Bind Shader
			ContentRef<ShaderProgram> selShader = this.SelectShader(textures, uniforms);
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
		/// <summary>
		/// Resets the OpenGL rendering state after finishing DrawTechnique-Setups. Only call this when there are no more
		/// DrawTechniques to follow directly.
		/// </summary>
		public void FinishRendering()
		{
			SetupBlendType(BlendMode.Reset);
			ShaderProgram.Bind(ContentRef<ShaderProgram>.Null);
		}

		/// <summary>
		/// Sets up OpenGL rendering state according to a certain <see cref="BlendMode"/>.
		/// </summary>
		/// <param name="mode">The BlendMode to set up.</param>
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
		/// <summary>
		/// Dynamically selects the <see cref="Duality.Resources.ShaderProgram"/> to use. Just returns <see cref="Shader"/> by default.
		/// </summary>
		/// <param name="textures">The current set of textures.</param>
		/// <param name="uniforms">The current set of uniforms.</param>
		/// <returns>The selected <see cref="Duality.Resources.ShaderProgram"/>.</returns>
		protected virtual ContentRef<ShaderProgram> SelectShader(Dictionary<string,ContentRef<Texture>> textures, Dictionary<string,float[]> uniforms)
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
