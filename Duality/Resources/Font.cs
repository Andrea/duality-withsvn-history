using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using SysDrawFont = System.Drawing.Font;

using Duality;
using Duality.ColorFormat;
using Duality.VertexFormat;

using OpenTK;

namespace Duality.Resources
{
	[Serializable]
	public class Font : Resource
	{
		public new const string FileExt = ".Font" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Font:";
		public const string ContentPath_GenericMonospace10	= VirtualContentPath + "GenericMonospace10";
		public const string ContentPath_GenericSerif12		= VirtualContentPath + "GenericSerif12";
		public const string ContentPath_GenericSansSerif12	= VirtualContentPath + "GenericSansSerif12";

		public static ContentRef<Font> GenericMonospace10	{ get; private set; }
		public static ContentRef<Font> GenericSerif12		{ get; private set; }
		public static ContentRef<Font> GenericSansSerif12	{ get; private set; }

		internal static void InitDefaultContent()
		{
			Font tmp;

			tmp = new Font();
			tmp.path = ContentPath_GenericMonospace10;
			tmp.SystemFont = new SysDrawFont(FontFamily.GenericMonospace, 10, FontStyle.Regular);
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.MonoSpace = true;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new Font();
			tmp.path = ContentPath_GenericSerif12;
			tmp.SystemFont = new SysDrawFont(FontFamily.GenericSerif, 12, FontStyle.Regular);
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new Font();
			tmp.path = ContentPath_GenericSansSerif12;
			tmp.SystemFont = new SysDrawFont(FontFamily.GenericSansSerif, 12, FontStyle.Regular);
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			GenericMonospace10	= ContentProvider.RequestContent<Font>(ContentPath_GenericMonospace10);
			GenericSerif12		= ContentProvider.RequestContent<Font>(ContentPath_GenericSerif12);
			GenericSansSerif12	= ContentProvider.RequestContent<Font>(ContentPath_GenericSansSerif12);
		}


		public static readonly ContentRef<Font> None			= ContentRef<Font>.Null;
		public static readonly string			SupportedChars	= " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,;.:-_<>|#'+*~@^°!\"§$%&/()=?`²³{[]}\\´öäüÖÄÜ";
		public static readonly int[]			CharLookup;

		static Font()
		{
			int maxCharVal = 0;
			for (int i = 0; i < SupportedChars.Length; i++) maxCharVal = Math.Max(maxCharVal, (int)SupportedChars[i]);

			int[] cl = new int[maxCharVal + 1];
			for (int i = 0; i < SupportedChars.Length; i++) cl[SupportedChars[i]] = i;

			CharLookup = cl;
		}


		public enum RenderHint
		{
			Monochrome	= System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit,
			AntiAlias	= System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
		}

		public struct GlyphData
		{
			public	int	width;
			public	int	height;
			public	int	offsetX;
		}


		private SysDrawFont	font		= null;
		private	ColorRGBA	color		= ColorRGBA.White;
		private	ColorRGBA	bgColor		= ColorRGBA.TransparentWhite;
		private	RenderHint	hint		= RenderHint.AntiAlias;
		private	GlyphData[]	glyphs		= null;
		private	float		spacing		= 0.0f;
		private	bool		monospace	= false;
		[NonSerialized]	private	Material	mat				= null;
		[NonSerialized]	private	Pixmap		pixelData		= null;
		[NonSerialized]	private	Texture		texture			= null;
		[NonSerialized] private	bool		needsReload		= false;
		[NonSerialized] private	int			maxGlyphWidth	= 0;


		public SysDrawFont SystemFont
		{
			get { return this.font; }
			set { this.font = value; this.needsReload = true; }
		}
		public ColorRGBA GlyphColor
		{
			get { return this.color; }
			set
			{
				this.color = value;
				this.needsReload = true;
			}
		}
		public ColorRGBA GlyphBgColor
		{
			get { return this.bgColor; }
			set
			{
				this.bgColor = value;
				this.needsReload = true;
			}
		}
		public RenderHint GlyphRenderHint
		{
			get { return this.hint; }
			set
			{
				this.hint = value;
				this.needsReload = true;
			}
		}
		public Material Material
		{
			get { return this.mat; }
		}
		public float CharSpacing
		{
			get { return this.spacing; }
			set { this.spacing = value; }
		}
		public bool MonoSpace
		{
			get { return this.monospace; }
			set { this.monospace = value; this.needsReload = true; }
		}
		public bool NeedsReload
		{
			get { return this.needsReload; }
		}

		public float Size
		{
			get { return this.font.Size; }
		}
		public int Height
		{
			get { return this.font.Height; }
		}
		public int Ascent
		{
			get { return (int)(this.font.FontFamily.GetCellAscent(this.font.Style) * this.font.Size / this.font.FontFamily.GetEmHeight(this.font.Style)); }
		}
		public int Descent
		{
			get { return (int)(this.font.FontFamily.GetCellDescent(this.font.Style) * this.font.Size / this.font.FontFamily.GetEmHeight(this.font.Style)); }
		}


		public Font(FontFamily family, float emSize, FontStyle style) : this(new SysDrawFont(family, emSize, style)) {}
		public Font(string family, float emSize, FontStyle style) : this(new SysDrawFont(family, emSize, style)) {}
		public Font(SysDrawFont font)
		{
			this.InitFromFont(font);
		}
		public Font() {}

		public void ReloadData()
		{
			this.InitFromFont(this.font);
		}
		public void InitFromFont(SysDrawFont font)
		{
			this.font = font;
			this.spacing = this.hint == RenderHint.Monochrome ? MathF.Round(this.font.Size / 10.0f) : this.font.Size / 10.0f;
			this.maxGlyphWidth = 0;

			int cols;
			int rows;
			cols = rows = (int)Math.Ceiling(Math.Sqrt(SupportedChars.Length));

			Bitmap pxTemp = new Bitmap(MathF.RoundToInt(cols * this.font.Size), MathF.RoundToInt(rows * this.font.Height));
			Rect[] atlas = new Rect[SupportedChars.Length];
			this.glyphs = new GlyphData[SupportedChars.Length];
			using (Graphics pxGraphics = Graphics.FromImage((Image)pxTemp))
			{
				Brush fntBrush = new SolidBrush(Color.FromArgb(this.color.a, this.color.r, this.color.g, this.color.b));
				Bitmap glyphTemp;
				Bitmap glyphTempTypo;

				pxGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));

				int x = 0;
				int y = 0;
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					string str = SupportedChars[i].ToString();
					SizeF charSize = pxGraphics.MeasureString(str, this.font, pxTemp.Width, StringFormat.GenericDefault);
					SizeF charSizeTypo = pxGraphics.MeasureString(str, this.font, pxTemp.Width, StringFormat.GenericTypographic);

					// Render a single glyph
					glyphTemp = new Bitmap((int)Math.Ceiling(Math.Max(1, charSize.Width)), this.font.Height);
					using (Graphics glyphGraphics = Graphics.FromImage((Image)glyphTemp))
					{
						glyphGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.hint;
						glyphGraphics.DrawString(str, this.font, fntBrush, 0, 0, StringFormat.GenericDefault);
					}
					if (str != " ")
					{
						glyphTemp = glyphTemp.Crop(true, false);

						// Render a single glyph in typographic mode
						glyphTempTypo = new Bitmap((int)Math.Ceiling(Math.Max(1, charSize.Width)), this.font.Height);
						using (Graphics glyphGraphics = Graphics.FromImage((Image)glyphTempTypo))
						{
							glyphGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.hint;
							glyphGraphics.DrawString(str, this.font, fntBrush, 0, 0, StringFormat.GenericTypographic);
						}
						glyphTempTypo = glyphTempTypo.Crop(true, false);
					}
					else
						glyphTempTypo = glyphTemp;

					// Update xy values if it doesn't fit anymore
					if (x + glyphTemp.Width + 2 > pxTemp.Width)
					{
						x = 0;
						y += this.font.Height + 2;
					}
					
					// Memorize atlas coordinates & glyph data
					this.maxGlyphWidth = Math.Max(this.maxGlyphWidth, glyphTemp.Width);
					this.glyphs[i].width = glyphTemp.Width;
					this.glyphs[i].height = glyphTemp.Height;
					this.glyphs[i].offsetX = glyphTemp.Width - glyphTempTypo.Width;
					atlas[i].x = (float)x / (float)pxTemp.Width;
					atlas[i].y = (float)y / (float)pxTemp.Height;
					atlas[i].w = (float)glyphTemp.Width / (float)pxTemp.Width;
					atlas[i].h = (float)this.font.Height / (float)pxTemp.Height;

					// Draw it onto the font surface
					pxGraphics.DrawImageUnscaled(glyphTemp, x, y);

					x += glyphTemp.Width + 2;
				}
			}

			// Monospace offset adjustments
			if (this.monospace)
			{
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					this.glyphs[i].offsetX -= (int)Math.Round((this.maxGlyphWidth - this.glyphs[i].width) / 2.0f);
				}
			}

			if (this.pixelData != null) this.pixelData.Dispose();
			this.pixelData = new Pixmap(pxTemp);

			if (this.texture != null) this.texture.Dispose();
			this.texture = new Texture(this.pixelData, 
				Texture.SizeMode.Enlarge, 
				this.hint == RenderHint.Monochrome ? OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest : OpenTK.Graphics.OpenGL.TextureMagFilter.Linear,
				this.hint == RenderHint.Monochrome ? OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest : OpenTK.Graphics.OpenGL.TextureMinFilter.LinearMipmapLinear);
			this.texture.Atlas = new List<Rect>(atlas.Select(r => r.Transform(this.texture.UVRatio)));

			if (this.mat != null) this.mat.Dispose();
			this.mat = new Material(this.hint == RenderHint.Monochrome ? DrawTechnique.Mask : DrawTechnique.Alpha, ColorRGBA.White, this.texture);
		}
		public bool GetGlyphData(char glyph, out GlyphData data)
		{
			int glyphId = (int)glyph;
			if (glyphId >= CharLookup.Length)
			{
				data = new GlyphData();
				return false;
			}
			else
			{
				data = this.glyphs[CharLookup[glyphId]];
				return true;
			}
		}

		public void DrawText(string text, float x, float y, float z, float angle, float scale, ref VertexP3T2[] vertices)
		{
			if (vertices == null || vertices.Length != text.Length * 4) vertices = new VertexP3T2[text.Length * 4];
			
			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(angle, scale, out xDot, out yDot);

			Vector2 curOffset = Vector2.Zero;
			char glyph;
			GlyphData glyphData;
			Rect uvRect;
			Vector2 glyphXSize = Vector2.Zero;
			Vector2 glyphXOff = Vector2.Zero;
			Vector2 glyphXAdv = Vector2.Zero;
			Vector2 glyphYSize = Vector2.Zero;
			for (int i = 0; i < text.Length; i++)
			{
				glyph = text[i];
				uvRect = this.texture.Atlas[CharLookup[(int)glyph]];
				this.GetGlyphData(glyph, out glyphData);

				glyphXSize = new Vector2(glyphData.width, 0.0f);
				glyphXOff = new Vector2(-glyphData.offsetX, 0.0f);
				glyphXAdv = new Vector2((this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing, 0.0f);
				glyphYSize = new Vector2(0.0f, glyphData.height);
				MathF.TransdormDotVec(ref glyphXSize, ref xDot, ref yDot);
				MathF.TransdormDotVec(ref glyphXOff, ref xDot, ref yDot);
				MathF.TransdormDotVec(ref glyphXAdv, ref xDot, ref yDot);
				MathF.TransdormDotVec(ref glyphYSize, ref xDot, ref yDot);

				vertices[i * 4 + 0].pos.X = x + curOffset.X + glyphXOff.X;
				vertices[i * 4 + 0].pos.Y = y + curOffset.Y + glyphXOff.Y;
				vertices[i * 4 + 0].pos.Z = z;
				vertices[i * 4 + 0].texCoord = uvRect.TopLeft;

				vertices[i * 4 + 1].pos.X = x + curOffset.X + glyphXOff.X + glyphXSize.X;
				vertices[i * 4 + 1].pos.Y = y + curOffset.Y + glyphXOff.Y + glyphXSize.Y;
				vertices[i * 4 + 1].pos.Z = z;
				vertices[i * 4 + 1].texCoord = uvRect.TopRight;

				vertices[i * 4 + 2].pos.X = x + curOffset.X + glyphXOff.X + glyphXSize.X + glyphYSize.X;
				vertices[i * 4 + 2].pos.Y = y + curOffset.Y + glyphXOff.Y + glyphXSize.Y + glyphYSize.Y;
				vertices[i * 4 + 2].pos.Z = z;
				vertices[i * 4 + 2].texCoord = uvRect.BottomRight;

				vertices[i * 4 + 3].pos.X = x + curOffset.X + glyphXOff.X + glyphYSize.X;
				vertices[i * 4 + 3].pos.Y = y + curOffset.Y + glyphXOff.Y + glyphYSize.Y;
				vertices[i * 4 + 3].pos.Z = z;
				vertices[i * 4 + 3].texCoord = uvRect.BottomLeft;

				curOffset += glyphXAdv;
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitFromFont(this.font);
		}
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			this.texture.Dispose();
			this.pixelData.Dispose();
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Font c = r as Font;
			c.hint = this.hint;
			c.bgColor = this.bgColor;
			c.color = this.color;
			c.InitFromFont(this.font);
		}
	}
}
