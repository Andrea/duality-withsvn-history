﻿using System;
using System.Collections.Generic;
using System.Linq;

using Duality.ColorFormat;

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
		/// (Virtual) path of the <see cref="Checkerboard256"/> Material.
		/// </summary>
		public const string ContentPath_Checkerboard256	= VirtualContentPath + "Checkerboard256";

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
		/// <summary>
		/// A Material showing a black and white checkerboard.
		/// </summary>
		public static ContentRef<Material> Checkerboard256	{ get; private set; }

		internal static void InitDefaultContent()
		{
			ContentProvider.RegisterContent(ContentPath_SolidWhite, new Material(DrawTechnique.Solid, ColorRgba.White));
			ContentProvider.RegisterContent(ContentPath_InvertWhite, new Material(DrawTechnique.Invert, ColorRgba.White));
			ContentProvider.RegisterContent(ContentPath_DualityLogo256, new Material(DrawTechnique.Mask, ColorRgba.White, Texture.DualityLogo256));
			ContentProvider.RegisterContent(ContentPath_DualityLogoB256, new Material(DrawTechnique.Mask, ColorRgba.White, Texture.DualityLogoB256));
			ContentProvider.RegisterContent(ContentPath_Checkerboard256, new Material(DrawTechnique.Solid, ColorRgba.White, Texture.Checkerboard256));

			SolidWhite		= ContentProvider.RequestContent<Material>(ContentPath_SolidWhite);
			InvertWhite		= ContentProvider.RequestContent<Material>(ContentPath_InvertWhite);
			DualityLogo256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Material>(ContentPath_DualityLogoB256);
			Checkerboard256	= ContentProvider.RequestContent<Material>(ContentPath_Checkerboard256);
		}

		/// <summary>
		/// Creates a new Material Resource based on the specified Texture, saves it and returns a reference to it.
		/// </summary>
		/// <param name="baseRes"></param>
		/// <returns></returns>
		public static ContentRef<Material> CreateFromTexture(ContentRef<Texture> baseRes)
		{
			string resPath = PathHelper.GetFreePath(baseRes.FullName, FileExt);
			Material res = new Material(DrawTechnique.Mask, ColorRgba.White, baseRes);
			res.Save(resPath);
			return res;
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
		}
		/// <summary>
		/// [GET / SET] The main color, typically used for coloring displayed vertices.
		/// </summary>
		public ColorRgba MainColor
		{
			get { return this.info.MainColor; }
		}
		/// <summary>
		/// [GET] The set of <see cref="Duality.Resources.Texture">Textures</see> to use.
		/// </summary>
		public IEnumerable<KeyValuePair<string,ContentRef<Texture>>> Textures
		{
			get { return this.info.Textures; }
		}
		/// <summary>
		/// [GET] Returns the main texture.
		/// </summary>
		public ContentRef<Texture> MainTexture
		{
			get { return this.info.MainTexture; }
		}
		/// <summary>
		/// [GET] The set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.
		/// </summary>
		public IEnumerable<KeyValuePair<string,float[]>> Uniforms
		{
			get { return this.info.Uniforms; }
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
		public Material(ContentRef<DrawTechnique> technique, ColorRgba mainColor, ContentRef<Texture> mainTex)
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
		public Material(ContentRef<DrawTechnique> technique, ColorRgba mainColor, Dictionary<string,ContentRef<Texture>> textures = null, Dictionary<string,float[]> uniforms = null)
		{
			this.info = new BatchInfo(technique, mainColor, textures, uniforms);
		}
		/// <summary>
		/// Creates a new Material based on the specified BatchInfo
		/// </summary>
		/// <param name="info"></param>
		public Material(BatchInfo info)
		{
			this.info = new BatchInfo(info);
		}
		
		/// <summary>
		/// Gets a texture by name. Returns a null reference if the name doesn't exist.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ContentRef<Texture> GetTexture(string name)
		{
			return this.info.GetTexture(name);
		}
		/// <summary>
		/// Gets a uniform by name. Returns a null reference if the name doesn't exist.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public float[] GetUniform(string name)
		{
			return this.info.GetUniform(name);
		}

		protected override void OnCopyTo(Resource r, Duality.Cloning.CloneProvider provider)
		{
			base.OnCopyTo(r, provider);
			Material c = r as Material;
			c.info = new BatchInfo(this.info);
			c.info.CleanDirty();
		}
		protected override void OnLoaded()
		{
			base.OnLoaded();
			if (this.info != null)
			{
				// Make references available
				this.info.MakeAvailable();
			}
		}
	}

	/// <summary>
	/// BatchInfos describe how an object, represented by a set of vertices, looks like.
	/// </summary>
	/// <seealso cref="Material"/>
	[Serializable]
	public class BatchInfo : IEquatable<BatchInfo>
	{
		[Flags]
		private enum DirtyFlag
		{
			None		= 0x0,

			Textures	= 0x1,
			Uniforms	= 0x2,

			All			= Textures | Uniforms
		}

		private	ContentRef<DrawTechnique>	technique	= DrawTechnique.Mask;
		private	ColorRgba					mainColor	= ColorRgba.White;
		private	Dictionary<string,ContentRef<Texture>>	textures	= null;
		private	Dictionary<string,float[]>				uniforms	= null;
		private	DirtyFlag	dirtyFlag	= DirtyFlag.None;
		private	int	hashCode = 0;
		
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.DrawTechnique"/> that is used.
		/// </summary>
		public ContentRef<DrawTechnique> Technique
		{
			get { return this.technique; }
			set { this.technique = value; this.InvalidateHashCode(); }
		}
		/// <summary>
		/// [GET / SET] The main color, typically used for coloring displayed vertices.
		/// </summary>
		public ColorRgba MainColor
		{
			get { return this.mainColor; }
			set { this.mainColor = value; this.InvalidateHashCode(); }
		}
		/// <summary>
		/// [GET / SET] The set of <see cref="Duality.Resources.Texture">Textures</see> to use.
		/// </summary>
		public IEnumerable<KeyValuePair<string,ContentRef<Texture>>> Textures
		{
			get { return this.textures; }
			set
			{
				if (value == null)
					this.textures = null;
				else
				{
					this.textures = new Dictionary<string,ContentRef<Texture>>();
					foreach (var pair in value)
					{
						if (pair.Key == null) continue;
						if (pair.Value == null) continue;
						this.textures.Add(pair.Key, pair.Value);
					}
				}
				this.InvalidateHashCode();
				this.dirtyFlag &= ~DirtyFlag.Textures;
			}
		}
		/// <summary>
		/// [GET / SET] The main texture.
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
			set
			{
				if (this.textures == null)
					this.textures = new Dictionary<string,ContentRef<Texture>>();
				else
					this.CleanDirty(DirtyFlag.Textures);
				this.textures[ShaderVarInfo.VarName_MainTex] = value;
				this.InvalidateHashCode();
			}
		}
		/// <summary>
		/// [GET / SET] The set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.
		/// </summary>
		public IEnumerable<KeyValuePair<string,float[]>> Uniforms
		{
			get { return this.uniforms; }
			set
			{
				if (value == null)
					this.uniforms = null;
				else
				{
					this.uniforms = new Dictionary<string,float[]>();
					foreach (var pair in value)
					{
						if (pair.Key == null) continue;
						if (pair.Value == null) continue;
						this.uniforms.Add(pair.Key, pair.Value);
					}
				}
				this.InvalidateHashCode();
				this.dirtyFlag &= ~DirtyFlag.Uniforms;
			}
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
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorRgba mainColor, ContentRef<Texture> mainTex) : this(technique, mainColor, null, null) 
		{
			this.textures = new Dictionary<string,ContentRef<Texture>>();
			this.textures.Add(ShaderVarInfo.VarName_MainTex, mainTex);
			this.InvalidateHashCode();
		}
		/// <summary>
		/// Creates a new complex BatchInfo.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="textures">A set of <see cref="Duality.Resources.Texture">Textures</see> to use.</param>
		/// <param name="uniforms">A set of <see cref="Duality.Resources.ShaderVarInfo">uniform values</see> to use.</param>
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorRgba mainColor, IEnumerable<KeyValuePair<string,ContentRef<Texture>>> textures = null, IEnumerable<KeyValuePair<string,float[]>> uniforms = null)
		{
			this.technique = technique;
			this.mainColor = mainColor;
			this.Textures = textures;
			this.Uniforms = uniforms;
			this.InvalidateHashCode();
		}
		
		/// <summary>
		/// Copies this BatchInfo's data to a different one.
		/// </summary>
		/// <param name="info">The target BatchInfo to copy data to.</param>
		public void CopyTo(BatchInfo info)
		{
			info.technique = this.technique;
			info.mainColor = this.mainColor;
			info.textures = this.textures;
			info.uniforms = this.uniforms;
			info.hashCode = this.hashCode;
			info.dirtyFlag |= DirtyFlag.All;
		}
		/// <summary>
		/// Assures that the current BatchInfo is not a temporarily shallow copy of an existing one.
		/// </summary>
		public void CleanDirty()
		{
			this.CleanDirty(DirtyFlag.All);
		}
		private void CleanDirty(DirtyFlag clean)
		{
			if ((this.dirtyFlag & clean) == DirtyFlag.None) return;

			if ((clean & DirtyFlag.Textures) != DirtyFlag.None && this.textures != null)
			{
				this.textures = new Dictionary<string,ContentRef<Texture>>(this.textures);
			}
			if ((clean & DirtyFlag.Uniforms) != DirtyFlag.None && this.uniforms != null)
			{
				var oldUniforms = this.uniforms;
				this.uniforms = new Dictionary<string,float[]>(oldUniforms);
				foreach (var pair in oldUniforms)
				{
					this.uniforms[pair.Key] = (float[])pair.Value.Clone();
				}
			}

			this.dirtyFlag &= ~clean;
		}
		/// <summary>
		/// Triggers content retrieval in all references Resources.
		/// </summary>
		public void MakeAvailable()
		{
			this.technique.MakeAvailable();
			if (this.textures != null)
			{
				foreach (var pair in this.textures.ToArray())
				{
					this.textures[pair.Key] = pair.Value.IsAvailable ? pair.Value.Res : pair.Value;
				}
			}
			this.InvalidateHashCode();
		}

		/// <summary>
		/// Sets up the appropriate OpenGL rendering state to render vertices using this BatchInfo.
		/// </summary>
		/// <param name="lastInfo">
		/// The BatchInfo that has been used to set up the current OpenGL state. This parameter is
		/// optional but supplying it will improve rendering performance by reducing redundant
		/// state changes.
		/// </param>
		public void SetupForRendering(IDrawDevice device, BatchInfo lastInfo)
		{
			if (object.ReferenceEquals(this, lastInfo)) return;
			this.technique.Res.SetupForRendering(device, this, (lastInfo == null) ? null : lastInfo.technique.Res);
		}
		/// <summary>
		/// Resets the OpenGL rendering state. This should only be called if there are no more
		/// BatchInfos to be set up directy after this one, i.e. if this is the last BatchInfo
		/// that has been rendered so far.
		/// </summary>
		public void FinishRendering()
		{
			this.technique.Res.FinishRendering();
		}

		/// <summary>
		/// Gets a texture by name. Returns a null reference if the name doesn't exist.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ContentRef<Texture> GetTexture(string name)
		{
			if (this.textures == null) return ContentRef<Texture>.Null;
			ContentRef<Texture> result;
			if (!this.textures.TryGetValue(name, out result))
				return ContentRef<Texture>.Null;
			else
				return result;
		}
		/// <summary>
		/// Sets a texture.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="tex"></param>
		public void SetTexture(string name, ContentRef<Texture> tex)
		{
			if (this.textures == null)
				this.textures = new Dictionary<string,ContentRef<Texture>>();
			else
				this.CleanDirty(DirtyFlag.Textures);

			if (tex.IsExplicitNull)
				this.textures.Remove(name);
			else
				this.textures[name] = tex;

			this.InvalidateHashCode();
		}
		/// <summary>
		/// Gets a uniform by name. Returns a null reference if the name doesn't exist.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public float[] GetUniform(string name)
		{
			if (this.uniforms == null) return null;
			float[] result;
			if (!this.uniforms.TryGetValue(name, out result)) return null;
			return result;
		}
		/// <summary>
		/// Sets a uniform value
		/// </summary>
		/// <param name="name"></param>
		/// <param name="uniform"></param>
		public void SetUniform(string name, params float[] uniform)
		{
			if (this.uniforms == null)
				this.uniforms = new Dictionary<string,float[]>();
			else
				this.CleanDirty(DirtyFlag.Uniforms);

			if (uniform == null)
				this.uniforms.Remove(name);
			else
				this.uniforms[name] = uniform;

			this.InvalidateHashCode();
		}
		/// <summary>
		/// Sets a uniform value
		/// </summary>
		/// <param name="name"></param>
		/// <param name="index"></param>
		/// <param name="uniformVal"></param>
		public void SetUniform(string name, int index, float uniformVal)
		{
			if (this.uniforms != null)
				this.CleanDirty(DirtyFlag.Uniforms);

			float[] uniformArr = this.GetUniform(name);
			if (uniformArr == null)
			{
				uniformArr = new float[index + 1];
				this.SetUniform(name, uniformArr);
			}
			if (uniformArr.Length <= index)
			{
				Array.Resize(ref uniformArr, index + 1);
				this.SetUniform(name, uniformArr);
			}
			uniformArr[index] = uniformVal;

			this.InvalidateHashCode();
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
			if (first.GetHashCode() != second.GetHashCode()) return false;

			if (first.mainColor != second.mainColor) return false;
			if (first.technique.Res != second.technique.Res) return false;

			if (first.textures != second.textures)
			{
				if (first.textures == null || second.textures == null) return false;
				if (first.textures.Count != second.textures.Count) return false;
				if (first.textures.Any(pair => second.textures[pair.Key].Res != pair.Value.Res)) return false;
			}

			if (first.uniforms != second.uniforms)
			{
				if (first.uniforms == null || second.uniforms == null) return false;
				if (first.uniforms.Count != second.uniforms.Count) return false;
				foreach (var pair in first.uniforms)
				{
					float[] firstArr = pair.Value;
					float[] secondArr = second.uniforms[pair.Key];
					if (firstArr.Length != secondArr.Length) return false;
					for (int i = 0; i < firstArr.Length; i++)
					{
						if (firstArr[i] != secondArr[i]) return false;
					}
				}
			}

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
		
		private void InvalidateHashCode()
		{
			this.hashCode = 0;
		}
		private void UpdateHashCode()
		{
			this.hashCode = MathF.CombineHashCode(17,
				this.mainColor.GetHashCode(),
				this.technique.GetHashCode(),
				this.GetTextureHashCode(),
				this.GetUniformHashCode());
		}
		private int GetTextureHashCode()
		{
			if (this.textures == null) return 0;
			int texHash = 17;
			foreach (var pair in this.textures)
			{
				MathF.CombineHashCode(ref texHash, pair.Key.GetHashCode());
				MathF.CombineHashCode(ref texHash, pair.Value.GetHashCode());
			}
			return texHash;
		}
		private int GetUniformHashCode()
		{
			if (this.uniforms == null) return 0;
			int uniformHash = 17;
			foreach (var pair in this.uniforms)
			{
				MathF.CombineHashCode(ref uniformHash, pair.Key.GetHashCode());
				for (int i = 0; i < pair.Value.Length; i++)
					MathF.CombineHashCode(ref uniformHash, pair.Value[i].GetHashCode());
			}
			return uniformHash;
		}

		public override string ToString()
		{
			ContentRef<Texture> inputTex = this.MainTexture;
			return string.Format("{0}, {1}", 
				inputTex.FullName,
				this.technique.FullName);
		}
		public override int GetHashCode()
		{
			if (this.hashCode == 0) this.UpdateHashCode();
			return this.hashCode;
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
