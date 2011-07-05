using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
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
			tmp.Family = FontFamily.GenericMonospace.Name;
			tmp.Size = 10;
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.MonoSpace = true;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new Font();
			tmp.path = ContentPath_GenericSerif12;
			tmp.Family = FontFamily.GenericSerif.Name;
			tmp.Size = 12;
			tmp.Kerning = true;
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new Font();
			tmp.path = ContentPath_GenericSansSerif12;
			tmp.Family = FontFamily.GenericSansSerif.Name;
			tmp.Size = 12;
			tmp.Kerning = true;
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			GenericMonospace10	= ContentProvider.RequestContent<Font>(ContentPath_GenericMonospace10);
			GenericSerif12		= ContentProvider.RequestContent<Font>(ContentPath_GenericSerif12);
			GenericSansSerif12	= ContentProvider.RequestContent<Font>(ContentPath_GenericSansSerif12);
		}


		public static readonly ContentRef<Font> None			= ContentRef<Font>.Null;
		public static readonly string			SupportedChars	= " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,;.:-_<>|#'+*~@^°!\"§$%&/()=?`²³{[]}\\´öäüÖÄÜ";
		public static readonly string			BodyAscentRef	= "acehmnorsuvwxz";
		public static readonly int[]			CharLookup;

		private	static	PrivateFontCollection			fontManager			= new PrivateFontCollection();
		private	static	Dictionary<string,FontFamily>	loadedFontRegistry	= new Dictionary<string,FontFamily>();
		public static FontFamily[] LoadedFontFamilies
		{
			get { return fontManager.Families; }
		}

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
			Monochrome	= TextRenderingHint.SingleBitPerPixelGridFit,
			AntiAlias	= TextRenderingHint.AntiAliasGridFit
		}

		public struct GlyphData
		{
			public	int		width;
			public	int		height;
			public	int		offsetX;
			public	int[]	kerningSamplesLeft;
			public	int[]	kerningSamplesRight;
		}

		
		private	string		familyName	= FontFamily.GenericMonospace.Name;
		private	float		size		= 10.0f;
		private	FontStyle	style		= FontStyle.Regular;
		private	ColorRGBA	color		= ColorRGBA.White;
		private	ColorRGBA	bgColor		= ColorRGBA.TransparentWhite;
		private	RenderHint	hint		= RenderHint.AntiAlias;
		private	float		spacing		= 0.0f;
		private	bool		monospace	= false;
		private	bool		kerning		= false;
		// Embedded custom font family
		private	byte[]		customFamilyData		= null;
		private	string		customFamilyBasePath	= null;
		// Data that is automatically acquired while loading the font
		[NonSerialized]	private SysDrawFont	internalFont	= new SysDrawFont(FontFamily.GenericMonospace, 10);
		[NonSerialized]	private	GlyphData[]	glyphs			= null;
		[NonSerialized]	private	int			bodyAscent		= 0;
		[NonSerialized]	private	Material	mat				= null;
		[NonSerialized]	private	Pixmap		pixelData		= null;
		[NonSerialized]	private	Texture		texture			= null;
		[NonSerialized] private	bool		needsReload		= false;
		[NonSerialized] private	int			maxGlyphWidth	= 0;


		public string Family
		{
			get { return this.familyName; }
			set 
			{
				// Do not allow changing the family if a custom family is used
				if (this.customFamilyData != null) return;

				this.familyName = value;
				this.needsReload = true;
			}
		}
		public float Size
		{
			get { return this.size; }
			set 
			{ 
				this.size = Math.Max(1.0f, value);
				this.UpdateCharSpacing();
				this.needsReload = true;
			}
		}
		public FontStyle Style
		{
			get { return this.style; }
			set
			{
				this.style = value;
				this.needsReload = true;
			}
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
				this.UpdateCharSpacing();
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
		public bool Kerning
		{
			get { return this.kerning; }
			set { this.kerning = value; this.needsReload = true; }
		}
		public bool NeedsReload
		{
			get { return this.needsReload; }
		}
		
		public byte[] CustomFamilyData
		{
			get { return this.customFamilyData; }
		}
		public string CustomFamilyBasePath
		{
			get { return this.customFamilyBasePath; }
		}

		public int Height
		{
			get { return this.internalFont.Height; }
		}
		public int Ascent
		{
			get { return (int)Math.Round(this.internalFont.FontFamily.GetCellAscent(this.internalFont.Style) * this.internalFont.Size / this.internalFont.FontFamily.GetEmHeight(this.internalFont.Style)); }
		}
		public int BodyAscent
		{
			get { return this.bodyAscent; }
		}
		public int Descent
		{
			get { return (int)Math.Round(this.internalFont.FontFamily.GetCellDescent(this.internalFont.Style) * this.internalFont.GetHeight() / this.internalFont.FontFamily.GetLineSpacing(this.internalFont.Style)); }
		}
		public int BaseLine
		{
			get { return (int)Math.Round(this.internalFont.FontFamily.GetCellAscent(this.internalFont.Style) * this.internalFont.GetHeight() / this.internalFont.FontFamily.GetLineSpacing(this.internalFont.Style)); }
		}


		public Font(string familyName, float emSize, FontStyle style) 
		{
			this.familyName = familyName;
			this.size = emSize;
			this.style = style;
			this.ReloadData();
		}
		public Font() {}
		
		public void LoadCustomFamilyData(string path = null)
		{
			if (path == null) path = this.customFamilyBasePath;

			this.customFamilyBasePath = path;

			if (String.IsNullOrEmpty(this.customFamilyBasePath) || !File.Exists(this.customFamilyBasePath))
				this.customFamilyBasePath = null;
			else
			{
				this.customFamilyData = File.ReadAllBytes(this.customFamilyBasePath);
				this.familyName = LoadFontFamilyFromMemory(this.customFamilyData).Name;
			}
		}
		public void SaveCustomFamilyData(string path = null)
		{
			if (path == null) path = this.customFamilyBasePath;

			// We're saving this Pixmaps pixel data for the first time
			if (!this.path.Contains(':') && this.customFamilyBasePath == null) this.customFamilyBasePath = path;

			File.WriteAllBytes(path, this.customFamilyData);
		}

		private void UpdateCharSpacing()
		{
			this.UpdateInternalFont();
			this.spacing = this.hint == RenderHint.Monochrome ? MathF.Round(this.internalFont.Size / 10.0f) : this.internalFont.Size / 10.0f;
		}
		private void UpdateInternalFont()
		{
			FontFamily family = GetFontFamily(this.familyName);
			if (family != null)
				this.internalFont = new SysDrawFont(family, this.size, this.style);
			else
				this.internalFont = new SysDrawFont(FontFamily.GenericMonospace, this.size, this.style);
		}
		public void ReloadData()
		{
			this.UpdateInternalFont();
			this.needsReload = false;
			this.maxGlyphWidth = 0;
			this.bodyAscent = 0;
			this.glyphs = new GlyphData[SupportedChars.Length];

			if (this.pixelData != null) this.pixelData.Dispose();
			if (this.texture != null) this.texture.Dispose();
			if (this.mat != null) this.mat.Dispose();

			int cols;
			int rows;
			cols = rows = (int)Math.Ceiling(Math.Sqrt(SupportedChars.Length));

			Bitmap pxTemp = new Bitmap(MathF.RoundToInt(cols * this.internalFont.Size), MathF.RoundToInt(rows * this.internalFont.Height));
			Bitmap glyphTemp;
			Bitmap glyphTempTypo;
			Rect[] atlas = new Rect[SupportedChars.Length];
			using (Graphics pxGraphics = Graphics.FromImage((Image)pxTemp))
			{
				Brush fntBrush = new SolidBrush(Color.FromArgb(this.color.a, this.color.r, this.color.g, this.color.b));

				pxGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));

				StringFormat formatDef = StringFormat.GenericDefault;
				formatDef.LineAlignment = StringAlignment.Near;
				StringFormat formatTypo = StringFormat.GenericTypographic;
				formatTypo.LineAlignment = StringAlignment.Near;

				int x = 0;
				int y = 0;
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					string str = SupportedChars[i].ToString();
					SizeF charSize = pxGraphics.MeasureString(str, this.internalFont, pxTemp.Width, formatDef);
					SizeF charSizeTypo = pxGraphics.MeasureString(str, this.internalFont, pxTemp.Width, formatTypo);

					// Render a single glyph
					glyphTemp = new Bitmap((int)Math.Ceiling(Math.Max(1, charSize.Width)), this.internalFont.Height);
					using (Graphics glyphGraphics = Graphics.FromImage((Image)glyphTemp))
					{
						glyphGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));
						glyphGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.hint;
						glyphGraphics.DrawString(str, this.internalFont, fntBrush, new RectangleF(0, 0, glyphTemp.Width, glyphTemp.Height), formatDef);
					}
					if (str != " ")
					{
						Rectangle glyphTempBounds = glyphTemp.OpaqueBounds();
						glyphTemp = glyphTemp.SubImage(glyphTempBounds.X, 0, glyphTempBounds.Width, glyphTemp.Height);
						if (BodyAscentRef.Contains(SupportedChars[i]))
							this.bodyAscent += glyphTempBounds.Height;

						// Render a single glyph in typographic mode
						glyphTempTypo = new Bitmap((int)Math.Ceiling(Math.Max(1, charSize.Width)), this.internalFont.Height);
						using (Graphics glyphGraphics = Graphics.FromImage((Image)glyphTempTypo))
						{
							glyphGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));
							glyphGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.hint;
							glyphGraphics.DrawString(str, this.internalFont, fntBrush, new RectangleF(0, 0, glyphTempTypo.Width, glyphTempTypo.Height), formatTypo);
						}
						glyphTempTypo = glyphTempTypo.Crop(true, false);
					}
					else
						glyphTempTypo = glyphTemp;

					// Update xy values if it doesn't fit anymore
					if (x + glyphTemp.Width + 2 > pxTemp.Width)
					{
						x = 0;
						y += this.internalFont.Height + 2;
					}
					
					// Memorize atlas coordinates & glyph data
					this.maxGlyphWidth = Math.Max(this.maxGlyphWidth, glyphTemp.Width);
					this.glyphs[i].width = glyphTemp.Width;
					this.glyphs[i].height = glyphTemp.Height;
					this.glyphs[i].offsetX = glyphTemp.Width - glyphTempTypo.Width;
					atlas[i].x = (float)x / (float)pxTemp.Width;
					atlas[i].y = (float)y / (float)pxTemp.Height;
					atlas[i].w = (float)glyphTemp.Width / (float)pxTemp.Width;
					atlas[i].h = (float)this.internalFont.Height / (float)pxTemp.Height;

					// Draw it onto the font surface
					pxGraphics.DrawImageUnscaled(glyphTemp, x, y);

					x += glyphTemp.Width + 2;
				}
			}

			this.bodyAscent /= BodyAscentRef.Length;
			this.pixelData = new Pixmap(pxTemp);
			this.texture = new Texture(this.pixelData, 
				Texture.SizeMode.Enlarge, 
				this.hint == RenderHint.Monochrome ? OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest : OpenTK.Graphics.OpenGL.TextureMagFilter.Linear,
				this.hint == RenderHint.Monochrome ? OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest : OpenTK.Graphics.OpenGL.TextureMinFilter.LinearMipmapLinear);
			this.texture.Atlas = new List<Rect>(atlas.Select(r => r.Transform(this.texture.UVRatio)));

			this.mat = new Material(this.hint == RenderHint.Monochrome ? DrawTechnique.Mask : DrawTechnique.Alpha, ColorRGBA.White, this.texture);

			// Monospace offset adjustments
			if (this.monospace)
			{
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					this.glyphs[i].offsetX -= (int)Math.Round((this.maxGlyphWidth - this.glyphs[i].width) / 2.0f);
				}
			}

			// Kerning data
			this.UpdateKerningData();
		}
		public void UpdateKerningData()
		{
			if (this.kerning)
			{
				int kerningSamples = (this.Ascent + this.Descent) / 4;
				int[] kerningY;
				if (kerningSamples <= 6)
				{
					kerningSamples = 6;
					kerningY = new int[] {
						this.BaseLine - this.Ascent,
						this.BaseLine - this.BodyAscent,
						this.BaseLine - this.BodyAscent * 2 / 3,
						this.BaseLine - this.BodyAscent / 3,
						this.BaseLine,
						this.BaseLine + this.Descent};
				}
				else
				{
					kerningY = new int[kerningSamples];
					int bodySamples = kerningSamples * 2 / 3;
					int descentSamples = (kerningSamples - bodySamples) / 2;
					int ascentSamples = kerningSamples - bodySamples - descentSamples;

					for (int k = 0; k < ascentSamples; k++) 
						kerningY[k] = this.BaseLine - this.Ascent + k * (this.Ascent - this.BodyAscent) / ascentSamples;
					for (int k = 0; k < bodySamples; k++) 
						kerningY[ascentSamples + k] = this.BaseLine - this.BodyAscent + k * this.BodyAscent / (bodySamples - 1);
					for (int k = 0; k < descentSamples; k++) 
						kerningY[ascentSamples + bodySamples + k] = this.BaseLine + (k + 1) * this.Descent / descentSamples;
				}

				int[] c = new int[3];
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					Bitmap glyphTemp = this.GetGlyphBitmap(SupportedChars[i]);

					this.glyphs[i].kerningSamplesLeft	= new int[kerningY.Length];
					this.glyphs[i].kerningSamplesRight	= new int[kerningY.Length];

					if (SupportedChars[i] != ' ')
					{
						ColorRGBA[] glyphTempPx = glyphTemp.GetPixelDataRGBA();
						int pxIndex;
						// Left side samples
						for (int sampleIndex = 0; sampleIndex < this.glyphs[i].kerningSamplesLeft.Length; sampleIndex++)
						{
							this.glyphs[i].kerningSamplesLeft[sampleIndex] = glyphTemp.Width / 2;
							for (int off = 0; off <= 2; off++)
							{
								pxIndex = MathF.Clamp(kerningY[sampleIndex] + off - 1, 0, glyphTemp.Height - 1) * glyphTemp.Width;
								c[off] = 0;
								while (glyphTempPx[pxIndex + c[off]].a == 0)
								{
									c[off]++;
									if (c[off] >= glyphTemp.Width / 2) break;
								}
								this.glyphs[i].kerningSamplesLeft[sampleIndex] = Math.Min(this.glyphs[i].kerningSamplesLeft[sampleIndex], c[off]);
							}
						}
						// Right side samples
						for (int sampleIndex = 0; sampleIndex < this.glyphs[i].kerningSamplesRight.Length; sampleIndex++)
						{
							this.glyphs[i].kerningSamplesRight[sampleIndex] = glyphTemp.Width / 2;
							for (int off = 0; off <= 2; off++)
							{
								pxIndex = MathF.Clamp(kerningY[sampleIndex] + off - 1, 0, glyphTemp.Height - 1) * glyphTemp.Width + glyphTemp.Width - 1;
								c[off] = 0;
								while (glyphTempPx[pxIndex - c[off]].a == 0)
								{
									c[off]++;
									if (c[off] >= glyphTemp.Width / 2) break;
								}
								this.glyphs[i].kerningSamplesRight[sampleIndex] = Math.Min(this.glyphs[i].kerningSamplesRight[sampleIndex], c[off]);
							}
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < SupportedChars.Length; ++i)
				{
					this.glyphs[i].kerningSamplesLeft	= null;
					this.glyphs[i].kerningSamplesRight	= null;
				}
			}
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
		public Bitmap GetGlyphBitmap(char glyph)
		{
			Rect targetRect = this.texture.Atlas[CharLookup[(int)glyph]];
			targetRect = targetRect.Transform(this.pixelData.Width / this.texture.UVRatio.X, this.pixelData.Height / this.texture.UVRatio.Y);
			return this.pixelData.PixelData.SubImage(
				MathF.RoundToInt(targetRect.x), 
				MathF.RoundToInt(targetRect.y), 
				MathF.RoundToInt(targetRect.w), 
				MathF.RoundToInt(targetRect.h));
		}

		public void EmitTextVertices(string text, ref VertexC4P3T2[] vertices, float x, float y, float z = 0.0f)
		{
			this.EmitTextVertices(text, ref vertices, x, y, z, ColorRGBA.White);
		}
		public void EmitTextVertices(string text, ref VertexC4P3T2[] vertices, float x, float y, float z, ColorRGBA clr, float angle = 0.0f, float scale = 1.0f)
		{
			this.EmitTextVertices(text, ref vertices);
			
			Vector3 offset = new Vector3(x, y, z);
			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(angle, scale, out xDot, out yDot);

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i].pos;

				MathF.TransdormDotVec(ref vertex, ref xDot, ref yDot);
				vertex += offset;

				vertices[i].pos = vertex;
				vertices[i].clr = clr;
			}
		}
		public void EmitTextVertices(string text, ref VertexC4P3T2[] vertices, float x, float y, ColorRGBA clr)
		{
			this.EmitTextVertices(text, ref vertices);
			
			Vector3 offset = new Vector3(x, y, 0);

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i].pos;
				vertex += offset;
				vertices[i].pos = vertex;
				vertices[i].clr = clr;
			}
		}
		public void EmitTextVertices(string text, ref VertexC4P3T2[] vertices)
		{
			if (vertices == null || vertices.Length != text.Length * 4) vertices = new VertexC4P3T2[text.Length * 4];
			
			float curOffset = 0.0f;
			GlyphData glyphData;
			Rect uvRect;
			float glyphXOff;
			float glyphXAdv;
			for (int i = 0; i < text.Length; i++)
			{
				this.ProcessTextAdv(text, i, out glyphData, out uvRect, out glyphXAdv, out glyphXOff);

				vertices[i * 4 + 0].pos.X = curOffset + glyphXOff;
				vertices[i * 4 + 0].pos.Y = 0.0f;
				vertices[i * 4 + 0].pos.Z = 0.0f;
				vertices[i * 4 + 0].texCoord = uvRect.TopLeft;
				vertices[i * 4 + 0].clr = ColorRGBA.White;

				vertices[i * 4 + 1].pos.X = curOffset + glyphXOff + glyphData.width;
				vertices[i * 4 + 1].pos.Y = 0.0f;
				vertices[i * 4 + 1].pos.Z = 0.0f;
				vertices[i * 4 + 1].texCoord = uvRect.TopRight;
				vertices[i * 4 + 1].clr = ColorRGBA.White;

				vertices[i * 4 + 2].pos.X = curOffset + glyphXOff + glyphData.width;
				vertices[i * 4 + 2].pos.Y = glyphData.height;
				vertices[i * 4 + 2].pos.Z = 0.0f;
				vertices[i * 4 + 2].texCoord = uvRect.BottomRight;
				vertices[i * 4 + 2].clr = ColorRGBA.White;

				vertices[i * 4 + 3].pos.X = curOffset + glyphXOff;
				vertices[i * 4 + 3].pos.Y = glyphData.height;
				vertices[i * 4 + 3].pos.Z = 0.0f;
				vertices[i * 4 + 3].texCoord = uvRect.BottomLeft;
				vertices[i * 4 + 3].clr = ColorRGBA.White;

				curOffset += glyphXAdv;
			}
		}

		public Vector2 MeasureText(string text)
		{
			Vector2 textSize = Vector2.Zero;

			float curOffset = 0.0f;
			GlyphData glyphData;
			Rect uvRect;
			float glyphXOff;
			float glyphXAdv;
			for (int i = 0; i < text.Length; i++)
			{
				this.ProcessTextAdv(text, i, out glyphData, out uvRect, out glyphXAdv, out glyphXOff);

				textSize.X = Math.Max(textSize.X, curOffset + glyphXAdv);
				textSize.Y = Math.Max(textSize.Y, glyphData.height);

				curOffset += glyphXAdv;
			}

			return textSize;
		}
		public string FitText(string text, float maxWidth, bool byWord = false)
		{
			Vector2 textSize = Vector2.Zero;

			float curOffset = 0.0f;
			GlyphData glyphData;
			Rect uvRect;
			float glyphXOff;
			float glyphXAdv;
			int lastValidLength = 0;
			for (int i = 0; i < text.Length; i++)
			{
				this.ProcessTextAdv(text, i, out glyphData, out uvRect, out glyphXAdv, out glyphXOff);

				textSize.X = Math.Max(textSize.X, curOffset + glyphXAdv);
				textSize.Y = Math.Max(textSize.Y, glyphData.height);

				if (textSize.X > maxWidth) return lastValidLength > 0 ? text.Substring(0, lastValidLength) : "";

				if (!byWord || text[i] == ' ')
					lastValidLength = i + 1;

				curOffset += glyphXAdv;
			}

			return text;
		}
		public Rect MeasureTextGlyph(string text, int index)
		{
			float curOffset = 0.0f;
			GlyphData glyphData;
			Rect uvRect;
			float glyphXOff;
			float glyphXAdv;
			for (int i = 0; i < text.Length; i++)
			{
				this.ProcessTextAdv(text, i, out glyphData, out uvRect, out glyphXAdv, out glyphXOff);

				if (i == index) return new Rect(curOffset + glyphXOff, 0, glyphData.width, glyphData.height);

				curOffset += glyphXAdv;
			}

			return new Rect();
		}
		public int PickTextGlyph(string text, float x, float y)
		{
			float curOffset = 0.0f;
			GlyphData glyphData;
			Rect uvRect;
			Rect glyphRect;
			float glyphXOff;
			float glyphXAdv;
			for (int i = 0; i < text.Length; i++)
			{
				this.ProcessTextAdv(text, i, out glyphData, out uvRect, out glyphXAdv, out glyphXOff);

				glyphRect = new Rect(curOffset + glyphXOff, 0, glyphData.width, glyphData.height);
				if (glyphRect.Contains(x, y)) return i;

				curOffset += glyphXAdv;
			}

			return -1;
		}

		private void ProcessTextAdv(string text, int index, out GlyphData glyphData, out Rect uvRect, out float glyphXAdv, out float glyphXOff)
		{
			char glyph = text[index];
			uvRect = this.texture.Atlas[CharLookup[(int)glyph]];

			this.GetGlyphData(glyph, out glyphData);
			glyphXOff = -glyphData.offsetX;

			if (this.kerning && !this.monospace)
			{
				char glyphNext = index + 1 < text.Length ? text[index + 1] : ' ';
				GlyphData glyphDataNext;
				this.GetGlyphData(glyphNext, out glyphDataNext);

				int minSum = int.MaxValue;
				for (int k = 0; k < glyphData.kerningSamplesRight.Length; k++)
					minSum = Math.Min(minSum, glyphData.kerningSamplesRight[k] + glyphDataNext.kerningSamplesLeft[k]);

				glyphXAdv = (this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing - minSum;
			}
			else
				glyphXAdv = (this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			// Load custom font, if not available yet
			if (GetFontFamily(this.familyName) == null && this.customFamilyData != null)
				LoadFontFamilyFromMemory(this.customFamilyData);

			this.ReloadData();
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
			c.customFamilyBasePath = this.customFamilyBasePath;
			c.customFamilyData = this.customFamilyData != null ? (byte[])this.customFamilyData.Clone() : null;
			c.familyName = this.familyName;
			c.size = this.size;
			c.style = this.style;
			c.hint = this.hint;
			c.bgColor = this.bgColor;
			c.color = this.color;
			c.monospace = this.monospace;
			c.kerning = this.kerning;
			c.spacing = this.spacing;
			c.ReloadData();
		}

		public static FontFamily GetFontFamily(string name)
		{
			if (string.IsNullOrEmpty(name)) return null;

			FontFamily result;
			if (!loadedFontRegistry.TryGetValue(name, out result))
			{
				foreach (FontFamily installedFamily in FontFamily.Families)
				{
					if (installedFamily.Name == name) return installedFamily;
				}
			}
			return result;
		}
		public static FontFamily LoadFontFamilyFromFile(string file)
		{
			fontManager.AddFontFile(file);
			FontFamily result = fontManager.Families[fontManager.Families.Length - 1];
			loadedFontRegistry[result.Name] = result;
			return result;
		}
		public static FontFamily LoadFontFamilyFromMemory(byte[] memory)
		{
			FontFamily result = null;

			GCHandle handle = GCHandle.Alloc(memory, GCHandleType.Pinned);
			try
			{
				IntPtr fontMemPtr = handle.AddrOfPinnedObject();
				fontManager.AddMemoryFont(fontMemPtr, memory.Length);
				result = fontManager.Families[fontManager.Families.Length - 1];
			}
			finally
			{
				handle.Free();
			}
			
			loadedFontRegistry[result.Name] = result;
			return result;
		}
		public static FontFamily LoadFontFamilyFromStream(Stream stream)
		{
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, buffer.Length);
			return LoadFontFamilyFromMemory(buffer);
		}
	}
}
