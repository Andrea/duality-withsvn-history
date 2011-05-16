using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Drawing.Imaging;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using BitmapPixelFormat = System.Drawing.Imaging.PixelFormat;

using Duality;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	/// <summary>
	/// A Texture refers to pixel data stored in video memory
	/// </summary>
	[Serializable]
	public class Texture : Resource
	{
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Texture:";
		public const string ContentPath_DualityLogo256	= VirtualContentPath + "DualityLogo256";
		public const string ContentPath_DualityLogoB256	= VirtualContentPath + "DualityLogoB256";
		public const string ContentPath_White			= VirtualContentPath + "White";

		public static ContentRef<Texture> DualityLogo256	{ get; private set; }
		public static ContentRef<Texture> DualityLogoB256	{ get; private set; }
		public static ContentRef<Texture> White				{ get; private set; }

		internal static void InitDefaultContent()
		{
			Texture tmp;

			tmp = new Texture(Pixmap.DualityLogo256); tmp.path = ContentPath_DualityLogo256;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Texture(Pixmap.DualityLogoB256); tmp.path = ContentPath_DualityLogoB256;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Texture(Pixmap.White); tmp.path = ContentPath_White;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			DualityLogo256	= ContentProvider.RequestContent<Texture>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Texture>(ContentPath_DualityLogoB256);
			White			= ContentProvider.RequestContent<Texture>(ContentPath_White);
		}


		public enum SizeMode
		{
			Enlarge,
			Stretch,
			NonPowerOfTwo,

			Default = Enlarge
		}


		public static readonly ContentRef<Texture> None	= ContentRef<Texture>.Null;

		private	static	bool			initialized		= false;
		private	static	int				activeTexUnit	= 0;
		private	static	Texture[]		curBound		= null;
		private	static	TextureUnit[]	texUnits		= null;

		public static ContentRef<Texture> BoundTexPrimary
		{
			get { return new ContentRef<Texture>(curBound[0]); }
		}
		public static ContentRef<Texture> BoundTexSecondary
		{
			get { return new ContentRef<Texture>(curBound[1]); }
		}
		public static ContentRef<Texture> BoundTexTertiary
		{
			get { return new ContentRef<Texture>(curBound[2]); }
		}
		public static ContentRef<Texture> BoundTexQuartary
		{
			get { return new ContentRef<Texture>(curBound[3]); }
		}
		public static ContentRef<Texture>[] BoundTex
		{
			get 
			{ 
				ContentRef<Texture>[] result = new ContentRef<Texture>[curBound.Length];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = new ContentRef<Texture>(curBound[i]);
				}
				return result;
			}
		}

		private static void Init()
		{
			if (initialized) return;
			
			int numTexUnits;
			GL.GetInteger(GetPName.MaxTextureImageUnits, out numTexUnits);
			texUnits = new TextureUnit[numTexUnits];
			curBound = new Texture[numTexUnits];

			for (int i = 0; i < numTexUnits; i++)
			{
				texUnits[i] = (TextureUnit)((int)TextureUnit.Texture0 + i);
			}

			initialized = true;
		}
		public static void Bind(ContentRef<Texture> tex, int texUnit = 0)
		{
			if (!initialized) Init();

			Texture texRes = tex.IsExplicitNull ? null : tex.Res;
			if (curBound[texUnit] == texRes) return;
			if (activeTexUnit != texUnit) GL.ActiveTexture(texUnits[texUnit]);
			activeTexUnit = texUnit;

			if (texRes == null)
			{
				GL.BindTexture(TextureTarget.Texture2D, 0);
				GL.Disable(EnableCap.Texture2D);
				curBound[texUnit] = null;
			}
			else
			{
				if (texRes.glTexId == 0)	throw new ArgumentException("Specified texture has no valid OpenGL texture Id! Maybe it hasn't been loaded / initialized properly?", "tex");
				if (texRes.Disposed)		throw new ArgumentException("Specified texture has already been deleted!", "tex");
					
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, texRes.glTexId);
				curBound[texUnit] = texRes;
			}
		}
		public static void ResetBinding(int beginAtIndex = 0)
		{
			if (!initialized) Init();
			for (int i = beginAtIndex; i < texUnits.Length; i++)
			{
				Bind(None, i);
			}
		}

		
		protected	ContentRef<Pixmap>		basePixmap	= ContentRef<Pixmap>.Null;
		protected	int						width		= 0;
		protected	int						height		= 0;
		protected	SizeMode				oglSizeMode	= SizeMode.Default;
		protected	TextureMagFilter		filterMag	= TextureMagFilter.Linear;
		protected	TextureMinFilter		filterMin	= TextureMinFilter.LinearMipmapLinear;
		protected	TextureWrapMode			wrapX		= TextureWrapMode.ClampToEdge;
		protected	TextureWrapMode			wrapY		= TextureWrapMode.ClampToEdge;
		protected	PixelInternalFormat		pixelformat	= PixelInternalFormat.Rgba;
		[NonSerialized]	protected	int		glTexId		= 0;
		[NonSerialized]	protected	float	diameter	= 0.0f;
		[NonSerialized]	protected	int		oglWidth	= 0;
		[NonSerialized]	protected	int		oglHeight	= 0;
		[NonSerialized]	protected	Vector2	curUVRatio	= new Vector2(1.0f, 1.0f);
		[NonSerialized] protected	bool	needsReload	= false;


		/// <summary>
		/// [GET] The Textures (original, unadjusted) width
		/// </summary>
		public int Width
		{
			get { return this.width; }
		}			//	G
		/// <summary>
		/// [GET] The Textures (original, unadjusted) height
		/// </summary>
		public int Height
		{
			get { return this.height; }
		}		//	G
		/// <summary>
		/// [GET] The Textures diameter
		/// </summary>
		public float Diameter
		{
			get { return this.diameter; }
		}	//	G
		/// <summary>
		/// [GET] The Textures internal width as uploaded to video memory
		/// </summary>
		public int OglWidth
		{
			get { return this.oglWidth; }
		}		//	G
		/// <summary>
		/// [GET] The Textures internal height as uploaded to video memory
		/// </summary>
		public int OglHeight
		{
			get { return this.oglHeight; }
		}		//	G
		/// <summary>
		/// [GET] The Textures internal id value
		/// </summary>
		internal int OglTexId
		{
			get { return this.glTexId; }
		}	//	G
		/// <summary>
		/// [GET] UV (Texture) coordinates for the Textures lower right
		/// </summary>
		public Vector2 UVRatio
		{
			get { return this.curUVRatio; }
		}	//	G
		/// <summary>
		/// Returns whether or not the texture uses mipmaps.
		/// </summary>
		public bool Mipmaps
		{
			get { return 
				this.filterMin == TextureMinFilter.LinearMipmapLinear ||
				this.filterMin == TextureMinFilter.LinearMipmapNearest ||
				this.filterMin == TextureMinFilter.NearestMipmapLinear ||
				this.filterMin == TextureMinFilter.NearestMipmapNearest; }
		}		//	G
		/// <summary>
		/// Indicates that the textures parameters have been changed in a way that might make it
		/// necessary to reload its data before using it next time.
		/// </summary>
		public bool NeedsReload
		{
			get { return this.needsReload; }
		}  //  G
		/// <summary>
		/// [GET / SET] The Textures magnifying filter
		/// </summary>
		public TextureMagFilter FilterMag
		{
			get { return this.filterMag; }
			set { if (this.filterMag != value) { this.filterMag = value; this.needsReload = true; } }
		}		//	GS
		/// <summary>
		/// [GET / SET] The Textures minifying filter
		/// </summary>
		public TextureMinFilter FilterMin
		{
			get { return this.filterMin; }
			set { if (this.filterMin != value) { this.filterMin = value; this.needsReload = true; } }
		}		//	GS
		/// <summary>
		/// [GET / SET] The Textures horizontal wrap mode
		/// </summary>
		public TextureWrapMode WrapX
		{
			get { return this.wrapX; }
			set { if (this.wrapX != value) { this.wrapX = value; this.needsReload = true; } }
		}				//	GS
		/// <summary>
		/// [GET / SET] The Textures vertical wrap mode
		/// </summary>
		public TextureWrapMode WrapY
		{
			get { return this.wrapY; }
			set { if (this.wrapY != value) { this.wrapY = value; this.needsReload = true; } }
		}				//	GS
		/// <summary>
		/// [GET / SET] The Textures pixel format
		/// </summary>
		public PixelInternalFormat PixelFormat
		{
			get { return this.pixelformat; }
			set { if (this.pixelformat != value) { this.pixelformat = value; this.needsReload = true; } }
		}	//	GS
		/// <summary>
		/// [GET / SET] Handles how the Textures base Pixmap is adjusted in order to fit GPU texture size requirements (Power of Two dimensions)
		/// </summary>
		public SizeMode OglSizeMode
		{
			get { return this.oglSizeMode; }
			set { if (this.oglSizeMode != value) { this.oglSizeMode = value; this.needsReload = true; } }
		}				//	GS
		/// <summary>
		/// [GET / SET] Reference to a Pixmap that contains the pixel data that is or has been uploaded to the Texture
		/// </summary>
		public ContentRef<Pixmap> BasePixmap
		{
			get { return this.basePixmap; }
			set { if (this.basePixmap.Res != value.Res) { this.basePixmap = value; this.needsReload = true; } }
		}		//	GS


		public Texture() {}
		public Texture(ContentRef<Pixmap> basePixmap, 
			SizeMode sizeMode			= SizeMode.Default, 
			TextureMagFilter filterMag	= TextureMagFilter.Linear, 
			TextureMinFilter filterMin	= TextureMinFilter.LinearMipmapLinear,
			TextureWrapMode wrapX		= TextureWrapMode.ClampToEdge,
			TextureWrapMode wrapY		= TextureWrapMode.ClampToEdge,
			PixelInternalFormat format	= PixelInternalFormat.Rgba)
		{
			this.filterMag = filterMag;
			this.filterMin = filterMin;
			this.wrapX = wrapX;
			this.wrapY = wrapY;
			this.pixelformat = format;
			this.LoadData(basePixmap, sizeMode);
		}
		public Texture(int width, int height, 
			SizeMode sizeMode			= SizeMode.Default, 
			TextureMagFilter filterMag	= TextureMagFilter.Linear, 
			TextureMinFilter filterMin	= TextureMinFilter.LinearMipmapLinear,
			TextureWrapMode wrapX		= TextureWrapMode.ClampToEdge,
			TextureWrapMode wrapY		= TextureWrapMode.ClampToEdge,
			PixelInternalFormat format	= PixelInternalFormat.Rgba)
		{
			this.filterMag = filterMag;
			this.filterMin = filterMin;
			this.wrapX = wrapX;
			this.wrapY = wrapY;
			this.pixelformat = format;
			this.oglSizeMode = sizeMode;
			this.AdjustSize(width, height);
			this.SetupInfo();
		}

		public void ReloadData()
		{
			this.LoadData(this.basePixmap, this.oglSizeMode);
		}
		public void LoadData(ContentRef<Pixmap> basePixmap)
		{
			this.LoadData(basePixmap, this.oglSizeMode);
		}
		public void LoadData(ContentRef<Pixmap> basePixmap, SizeMode sizeMode)
		{
			if (this.glTexId == 0) this.glTexId = GL.GenTexture();
			this.needsReload = false;
			this.basePixmap = basePixmap;
			this.oglSizeMode = sizeMode;

			int lastTexId;
			GL.GetInteger(GetPName.TextureBinding2D, out lastTexId);
			GL.BindTexture(TextureTarget.Texture2D, this.glTexId);

			this.SetupInfo();

			Bitmap bm = this.basePixmap.IsExplicitNull ? null : this.basePixmap.Res.PixelData;
			if (bm != null)
			{
				this.AdjustSize(bm.Width, bm.Height);
				if (this.oglSizeMode != SizeMode.NonPowerOfTwo &&
					(this.width != this.oglWidth || this.height != this.oglHeight))
				{
					if (this.oglSizeMode == SizeMode.Enlarge)
						bm = bm.Resize(this.oglWidth, this.oglHeight);
					else
						bm = bm.Rescale(this.oglWidth, this.oglHeight, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
				}

				BitmapData data = bm.LockBits(
					new Rectangle(0, 0, bm.Width, bm.Height),
					ImageLockMode.ReadOnly,
					BitmapPixelFormat.Format32bppArgb);

				// Load pixel data to video memory
				GL.TexImage2D(TextureTarget.Texture2D, 0,
					this.pixelformat, data.Width, data.Height, 0,
					GLPixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

				bm.UnlockBits(data);
			}

			GL.BindTexture(TextureTarget.Texture2D, lastTexId);
		}

		protected void AdjustSize(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.diameter = MathF.Distance(this.width, this.height);

			if (this.oglSizeMode == SizeMode.NonPowerOfTwo)
			{
				this.oglWidth = this.width;
				this.oglHeight = this.height;
				this.curUVRatio = Vector2.One;
			}
			else
			{
				this.oglWidth = OpenTK.MathHelper.NextPowerOfTwo(this.width);
				this.oglHeight = OpenTK.MathHelper.NextPowerOfTwo(this.height);
				if (this.width != this.oglWidth || this.height != this.oglHeight)
				{
					if (this.oglSizeMode == SizeMode.Enlarge)
					{
						this.curUVRatio.X = (float)this.width / (float)this.oglWidth;
						this.curUVRatio.Y = (float)this.height / (float)this.oglHeight;
					}
					else
					{
						this.curUVRatio = Vector2.One;
					}
				}
			}
		}
		public void SetupInfo()
		{
			if (this.glTexId == 0) this.glTexId = GL.GenTexture();

			int lastTexId;
			GL.GetInteger(GetPName.TextureBinding2D, out lastTexId);
			if (lastTexId != this.glTexId) GL.BindTexture(TextureTarget.Texture2D, this.glTexId);

			// Set texture parameters
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)this.filterMin);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)this.filterMag);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)this.wrapX);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)this.wrapY);

			// If needed, care for Mipmaps
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, this.Mipmaps ? 1 : 0);

			// Setup pixel format
			GL.TexImage2D(TextureTarget.Texture2D, 0,
				this.pixelformat, this.oglWidth, this.oglHeight, 0,
				GLPixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

			if (lastTexId != this.glTexId) GL.BindTexture(TextureTarget.Texture2D, lastTexId);
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.LoadData(this.basePixmap, this.oglSizeMode);
		}
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
				this.glTexId != 0)
			{
				GL.DeleteTexture(this.glTexId);
				this.glTexId = 0;
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Texture c = r as Texture;
			c.width = this.width;
			c.height = this.height;
			c.filterMag = this.filterMag;
			c.filterMin = this.filterMin;
			c.wrapX = this.wrapX;
			c.wrapY = this.wrapY;
			c.pixelformat = this.pixelformat;
			c.LoadData(this.basePixmap, this.oglSizeMode);
		}
	}
}
