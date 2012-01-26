using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	/// <summary>
	/// Materials are standardized <see cref="BatchInfo">BatchInfos</see>, stored as a Resource. 
	/// Just like BatchInfo objects, they describe how an object, represented by a set of vertices, 
	/// looks like. Using Materials is generally more performant than using BatchInfos but not always
	/// reasonable, for example when there is a single, unique GameObject with a special appearance:
	/// This is a typical <see cref="BatchInfo"/> case.
	/// </summary>
	/// <seealso cref="BatchInfo"/>
	[Serializable]
	public class Material : Resource
	{
		/// <summary>
		/// A Material resources file extension.
		/// </summary>
		public new const string FileExt = ".Material" + Resource.FileExt;
		
		/// <summary>
		/// (Virtual) base path for Duality's embedded default Materials.
		/// </summary>
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Material:";
		/// <summary>
		/// (Virtual) path of the <see cref="SolidWhite"/> Material.
		/// </summary>
		public const string ContentPath_SolidWhite		= VirtualContentPath + "SolidWhite";
		/// <summary>
		/// (Virtual) path of the <see cref="InvertWhite"/> Material.
		/// </summary>
		public const string ContentPath_InvertWhite		= VirtualContentPath + "InvertWhite";
		/// <summary>
		/// (Virtual) path of the <see cref="DualityLogo256"/> Material.
		/// </summary>
		public const string ContentPath_DualityLogo256	= VirtualContentPath + "DualityLogo256";
		/// <summary>
		/// (Virtual) path of the <see cref="DualityLogoB256"/> Material.
		/// </summary>
		public const string ContentPath_DualityLogoB256	= VirtualContentPath + "DualityLogoB256";

		/// <summary>
		/// A solid, white Material.
		/// </summary>
		public static ContentRef<Material> SolidWhite		{ get; private set; }
		/// <summary>
		/// A Material that inverts its background.
		/// </summary>
		public static ContentRef<Material> InvertWhite		{ get; private set; }
		/// <summary>
		/// A Material showing the Duality logo.
		/// </summary>
		public static ContentRef<Material> DualityLogo256	{ get; private set; }
		/// <summary>
		/// A Material showing the Duality logo, but without the text on it.
		/// </summary>
		public static ContentRef<Material> DualityLogoB256	{ get; private set; }

		internal static void InitDefaultContent()
		{
			ContentProvider.RegisterContent(ContentPath_SolidWhite, new Material(DrawTechnique.Solid, ColorRgba.White));
			ContentProvider.RegisterContent(ContentPath_InvertWhite, new Material(DrawTechnique.Invert, ColorRgba.White));
			ContentProvider.RegisterContent(ContentPath_DualityLogo256, new Material(DrawTechnique.Mask, ColorRgba.White, Texture.DualityLogo256));
			ContentProvider.RegisterContent(ContentPath_DualityLogoB256, new Material(DrawTechnique.Mask, ColorRgba.White, Texture.DualityLogoB256));

			SolidWhite		= ContentProvider.RequestContent<Material>(ContentPath_SolidWhite);
			InvertWhite		= ContentProvider.RequestContent<Material>(ContentPath_InvertWhite);
			DualityLogo256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogoB256);
		}


		private	BatchInfo	info	= new BatchInfo();

		/// <summary>
		/// [GET] Returns a newly created <see cref="BatchInfo"/> that visually equals this Material.
		/// </summary>
		public BatchInfo Info
		{
			get { return new BatchInfo(this.info); }
		}
		internal BatchInfo InfoDirect
		{
			get { return this.info; }
		}

		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.DrawTechnique"/> that is used.
		/// </summary>
		public ContentRef<DrawTechnique> Technique
		{
			get { return this.info.Technique; }
			set { this.info.Technique = value; }
		}
		/// <summary>
		/// [GET / SET] The main color, typically used for coloring displayed vertices.
		/// </summary>
		public ColorFormat.ColorRgba MainColor
		{
			get { return this.info.MainColor; }
			set { this.info.MainColor = value; }
		}
		/// <summary>
		/// [GET / SET] A set of <see cref="Duality.Resources.Texture">Textures</see> to use.
		/// </summary>
		public Dictionary<string,ContentRef<Texture>> Textures
		{
			get { return this.info.Textures; }
			set { this.info.Textures = value; }
		}
		/// <summary>
		/// [GET] Returns the main texture.
		/// </summary>
		public ContentRef<Texture> MainTexture
		{
			get { return this.info.MainTexture; }
		}
		/// <summary>
		/// [GET / SET] A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.
		/// </summary>
		public Dictionary<string,float[]> Uniforms
		{
			get { return this.info.Uniforms; }
			set { this.info.Uniforms = value; }
		}

		/// <summary>
		/// Creates a new Material
		/// </summary>
		public Material()
		{
			this.info = new BatchInfo();
		}
		/// <summary>
		/// Creates a new single-texture Material.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="mainTex">The main <see cref="Duality.Resources.Texture"/> to use.</param>
		public Material(ContentRef<DrawTechnique> technique, ColorFormat.ColorRgba mainColor, ContentRef<Texture> mainTex)
		{
			this.info = new BatchInfo(technique, mainColor, mainTex);
		}
		/// <summary>
		/// Creates a new complex Material.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="textures">A set of <see cref="Duality.Resources.Texture">Textures</see> to use.</param>
		/// <param name="uniforms">A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.</param>
		public Material(ContentRef<DrawTechnique> technique, ColorFormat.ColorRgba mainColor, Dictionary<string,ContentRef<Texture>> textures = null, Dictionary<string,float[]> uniforms = null)
		{
			this.info = new BatchInfo(technique, mainColor, textures, uniforms);
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Material c = r as Material;
			c.info = new BatchInfo(this.info);
		}
	}

	/// <summary>
	/// BatchInfos describe how an object, represented by a set of vertices, looks like.
	/// </summary>
	/// <seealso cref="Material"/>
	[Serializable]
	public class BatchInfo : IEquatable<BatchInfo>
	{
		private	ContentRef<DrawTechnique>	technique	= DrawTechnique.Mask;
		private	ColorFormat.ColorRgba		mainColor	= ColorFormat.ColorRgba.White;
		private	Dictionary<string,ContentRef<Texture>>	textures	= null;
		private	Dictionary<string,float[]>				uniforms	= null;
		
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.DrawTechnique"/> that is used.
		/// </summary>
		public ContentRef<DrawTechnique> Technique
		{
			get { return this.technique; }
			set { this.technique = value; }
		}
		/// <summary>
		/// [GET / SET] The main color, typically used for coloring displayed vertices.
		/// </summary>
		public ColorFormat.ColorRgba MainColor
		{
			get { return this.mainColor; }
			set { this.mainColor = value; }
		}
		/// <summary>
		/// [GET / SET] A set of <see cref="Duality.Resources.Texture">Textures</see> to use.
		/// </summary>
		public Dictionary<string,ContentRef<Texture>> Textures
		{
			get { return this.textures; }
			set { this.textures = value; }
		}
		/// <summary>
		/// [GET] Returns the main texture.
		/// </summary>
		public ContentRef<Texture> MainTexture
		{
			get
			{
				if (this.textures == null || this.textures.Count == 0) return ContentRef<Texture>.Null;
				ContentRef<Texture> mainTexRef;
				if (!this.textures.TryGetValue(ShaderVarInfo.VarName_MainTex, out mainTexRef)) return ContentRef<Texture>.Null;
				return mainTexRef;
			}
		}
		/// <summary>
		/// [GET / SET] A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.
		/// </summary>
		public Dictionary<string,float[]> Uniforms
		{
			get { return this.uniforms; }
			set { this.uniforms = value; }
		}

		/// <summary>
		/// Creates a new, empty BatchInfo.
		/// </summary>
		public BatchInfo() {}
		/// <summary>
		/// Creates a new BatchInfo based on an existing <see cref="Material"/>.
		/// </summary>
		/// <param name="source"></param>
		public BatchInfo(Material source) : this(source.InfoDirect) {}
		/// <summary>
		/// Creates a new BatchInfo based on an existing BatchInfo. This is essentially a copy constructor.
		/// </summary>
		/// <param name="source"></param>
		public BatchInfo(BatchInfo source)
		{
			source.CopyTo(this);
		}
		/// <summary>
		/// Creates a new single-texture BatchInfo.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="mainTex">The main <see cref="Duality.Resources.Texture"/> to use.</param>
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorFormat.ColorRgba mainColor, ContentRef<Texture> mainTex) : this(technique, mainColor, null, null) 
		{
			this.textures = new Dictionary<string,ContentRef<Texture>>();
			this.textures.Add(ShaderVarInfo.VarName_MainTex, mainTex);
		}
		/// <summary>
		/// Creates a new complex BatchInfo.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="textures">A set of <see cref="Duality.Resources.Texture">Textures</see> to use.</param>
		/// <param name="uniforms">A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.</param>
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorFormat.ColorRgba mainColor, Dictionary<string,ContentRef<Texture>> textures = null, Dictionary<string,float[]> uniforms = null)
		{
			this.technique = technique;
			this.mainColor = mainColor;
			this.textures = textures;
			this.uniforms = uniforms;
		}
		
		/// <summary>
		/// Copies this BatchInfo's data to a different one.
		/// </summary>
		/// <param name="info">The target BatchInfo to copy data to.</param>
		public void CopyTo(BatchInfo info)
		{
			info.technique = this.technique;
			info.mainColor = this.mainColor;
			info.textures = this.textures == null ? null : new Dictionary<string,ContentRef<Texture>>(this.textures);
			info.uniforms = this.uniforms == null ? null : new Dictionary<string,float[]>(this.uniforms);
		}

		/// <summary>
		/// Sets up the appropriate OpenGL rendering state to render vertices using this BatchInfo.
		/// </summary>
		/// <param name="lastInfo">
		/// The BatchInfo that has been used to set up the current OpenGL state. This parameter is
		/// optional but supplying it will improve rendering performance by reducing redundant
		/// state changes.
		/// </param>
		public void SetupForRendering(BatchInfo lastInfo)
		{
			if (object.ReferenceEquals(this, lastInfo)) return;

			// Setup main color
			GL.Color4(this.mainColor.r, this.mainColor.g, this.mainColor.b, this.mainColor.a);

			// Setup technique
			this.technique.Res.SetupForRendering(
				(lastInfo == null) ? null : lastInfo.technique.Res,
				this.textures,
				this.uniforms);
		}
		/// <summary>
		/// Resets the OpenGL rendering state. This should only be called if there are no more
		/// BatchInfos to be set up directy after this one, i.e. if this is the last BatchInfo
		/// that has been rendered so far.
		/// </summary>
		public void FinishRendering()
		{
			Texture.ResetBinding();
			this.technique.Res.FinishRendering();
		}

		/// <summary>
		/// Compares two BatchInfos for equality. If a <see cref="System.Object.ReferenceEquals"/> test
		/// fails, their actual data is compared.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>True, if both BatchInfos can be considered equal, false if not.</returns>
		public static bool operator ==(BatchInfo first, BatchInfo second)
		{
			if (object.ReferenceEquals(first, second)) return true;
			if (object.ReferenceEquals(first, null)) return false;
			if (object.ReferenceEquals(second, null)) return false;

			if (first.mainColor != second.mainColor) return false;
			if (first.technique.Res != second.technique.Res) return false;

			if (first.textures != null)
			{
				if (second.textures == null) return false;
				if (first.textures.Count != second.textures.Count) return false;
				foreach (var pair in first.textures)
				{
					if (second.textures[pair.Key].Res != pair.Value.Res) return false;
				}
			}
			else if (second.textures != null) return false;

			if (first.uniforms != null)
			{
				if (second.uniforms == null) return false;
				if (first.uniforms.Count != second.uniforms.Count) return false;
				foreach (var pair in first.uniforms)
				{
					if (second.uniforms[pair.Key] != pair.Value) return false;
				}
			}
			else if (second.uniforms != null) return false;

			return true;
		}
		/// <summary>
		/// Compares two BatchInfos for inequality. If a <see cref="System.Object.ReferenceEquals"/> test
		/// fails, their actual data is compared.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns>True, if both BatchInfos can be considered unequal, false if not.</returns>
		public static bool operator !=(BatchInfo first, BatchInfo second)
		{
			return !(first == second);
		}
		
		public override string ToString()
		{
			ContentRef<Texture> inputTex = this.MainTexture;
			return string.Format("{0}, {1}", 
				inputTex.IsExplicitNull ? "[/]" : inputTex.Name,
				this.technique.IsExplicitNull ? "?" : this.technique.Name);
		}
		public override int GetHashCode()
		{
			// This method is used by the DrawBatch Optimizer for generating
			// priorized BatchInfo sort indices. The lower 23 bits of this hash
			// code are used.

			int techHash = this.technique.IsAvailable ? this.technique.Res.GetHashCode() : 0;
			int texHash = 0;
			if (this.textures != null) foreach (var tex in this.textures.Values) texHash ^= tex.IsAvailable ? tex.Res.GetHashCode() : 0;

			return							// -- Priority ascending --
				(techHash & 2047) << 0 |	// 11 Bit Technique
				(texHash & 4095) << 11;		// 12 Bit Used Textures
		}
		public override bool Equals(object obj)
		{
			if (obj is BatchInfo) return this == (obj as BatchInfo);

			return base.Equals(obj);
		}
		public bool Equals(BatchInfo other)
		{
			return this == other;
		}
	}
}
