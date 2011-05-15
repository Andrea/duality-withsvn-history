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
	public class Material : Resource
	{
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Material:";
		public const string ContentPath_SolidWhite		= VirtualContentPath + "SolidWhite";
		public const string ContentPath_InvertWhite		= VirtualContentPath + "InvertWhite";
		public const string ContentPath_DualityLogo256	= VirtualContentPath + "DualityLogo256";
		public const string ContentPath_DualityLogoB256	= VirtualContentPath + "DualityLogoB256";

		public static ContentRef<Material> SolidWhite		{ get; private set; }
		public static ContentRef<Material> InvertWhite		{ get; private set; }
		public static ContentRef<Material> DualityLogo256	{ get; private set; }
		public static ContentRef<Material> DualityLogoB256	{ get; private set; }

		internal static void InitDefaultContent()
		{
			Material tmp;

			tmp = new Material(DrawTechnique.Solid, ColorFormat.ColorRGBA.White);
			tmp.path = ContentPath_SolidWhite;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Material(DrawTechnique.Invert, ColorFormat.ColorRGBA.White);
			tmp.path = ContentPath_InvertWhite;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Material(DrawTechnique.Mask, ColorFormat.ColorRGBA.White, Texture.DualityLogo256);
			tmp.path = ContentPath_DualityLogo256;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Material(DrawTechnique.Mask, ColorFormat.ColorRGBA.White, Texture.DualityLogoB256);
			tmp.path = ContentPath_DualityLogoB256;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			SolidWhite		= ContentProvider.RequestContent<Material>(ContentPath_SolidWhite);
			InvertWhite		= ContentProvider.RequestContent<Material>(ContentPath_InvertWhite);
			DualityLogo256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogoB256);
		}


		private	BatchInfo	info	= new BatchInfo();

		public BatchInfo Info
		{
			get { return new BatchInfo(this.info); }
		}
		internal BatchInfo InfoDirect
		{
			get { return this.info; }
		}

		public ContentRef<DrawTechnique> Technique
		{
			get { return this.info.Technique; }
			set { this.info.Technique = value; }
		}
		public ColorFormat.ColorRGBA MainColor
		{
			get { return this.info.MainColor; }
			set { this.info.MainColor = value; }
		}
		public Dictionary<string,ContentRef<Texture>> Textures
		{
			get { return this.info.Textures; }
			set { this.info.Textures = value; }
		}
		public Dictionary<string,float[]> Uniforms
		{
			get { return this.info.Uniforms; }
			set { this.info.Uniforms = value; }
		}

		public Material()
		{
			this.info = new BatchInfo();
		}
		public Material(ContentRef<DrawTechnique> technique, ColorFormat.ColorRGBA mainColor, ContentRef<Texture> mainTex)
		{
			this.info = new BatchInfo(technique, mainColor, mainTex);
		}
		public Material(ContentRef<DrawTechnique> technique, ColorFormat.ColorRGBA mainColor, Dictionary<string,ContentRef<Texture>> textures = null, Dictionary<string,float[]> uniforms = null)
		{
			this.info = new BatchInfo(technique, mainColor, textures, uniforms);
		}

		protected override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Material c = r as Material;
			c.info = new BatchInfo(this.info);
		}
	}

	[Serializable]
	public class BatchInfo : IEquatable<BatchInfo>
	{
		private	ContentRef<DrawTechnique>	technique	= ContentRef<DrawTechnique>.Null;
		private	ColorFormat.ColorRGBA		mainColor	= ColorFormat.ColorRGBA.White;
		private	Dictionary<string,ContentRef<Texture>>	textures	= null;
		private	Dictionary<string,float[]>				uniforms	= null;

		public ContentRef<DrawTechnique> Technique
		{
			get { return this.technique; }
			set { this.technique = value; }
		}
		public ColorFormat.ColorRGBA MainColor
		{
			get { return this.mainColor; }
			set { this.mainColor = value; }
		}
		public Dictionary<string,ContentRef<Texture>> Textures
		{
			get { return this.textures; }
			set { this.textures = value; }
		}
		public Dictionary<string,float[]> Uniforms
		{
			get { return this.uniforms; }
			set { this.uniforms = value; }
		}

		public BatchInfo() {}
		public BatchInfo(Material source) : this(source.InfoDirect) {}
		public BatchInfo(BatchInfo source)
		{
			this.technique = source.technique;
			this.mainColor = source.mainColor;
			this.textures = source.textures == null ? null : new Dictionary<string,ContentRef<Texture>>(source.textures);
			this.uniforms = source.uniforms == null ? null : new Dictionary<string,float[]>(source.uniforms);
		}
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorFormat.ColorRGBA mainColor, ContentRef<Texture> mainTex) : this(technique, mainColor, null, null) 
		{
			this.textures = new Dictionary<string,ContentRef<Texture>>();
			this.textures.Add("mainTex", mainTex);
		}
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorFormat.ColorRGBA mainColor, Dictionary<string,ContentRef<Texture>> textures = null, Dictionary<string,float[]> uniforms = null)
		{
			this.technique = technique;
			this.mainColor = mainColor;
			this.textures = textures;
			this.uniforms = uniforms;
		}
		
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
		public void FinishRendering()
		{
			Texture.ResetBinding();
			this.technique.Res.FinishRendering();
		}

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
		public static bool operator !=(BatchInfo first, BatchInfo second)
		{
			return !(first == second);
		}

		public override int GetHashCode()
		{
			// This method is used by the DrawBatch Optimizer for generating
			// priorized BatchInfo sort indices. The lower 23 bits of this hash
			// code are used.

			int techHash = this.technique.Res.GetHashCode();
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
