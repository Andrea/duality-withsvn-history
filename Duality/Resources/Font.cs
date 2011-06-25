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
			tmp.Kerning = true;
			tmp.GlyphRenderHint = RenderHint.Monochrome;
			tmp.ReloadData();
			ContentProvider.RegisterContent(tmp.Path, tmp);

			tmp = new Font();
			tmp.path = ContentPath_GenericSansSerif12;
			tmp.SystemFont = new SysDrawFont(FontFamily.GenericSansSerif, 12, FontStyle.Regular);
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
			public	int		width;
			public	int		height;
			public	int		offsetX;
			public	int[]	kerningSamplesLeft;
			public	int[]	kerningSamplesRight;
		}


		private SysDrawFont	font		= null;
		private	ColorRGBA	color		= ColorRGBA.White;
		private	ColorRGBA	bgColor		= ColorRGBA.TransparentWhite;
		private	RenderHint	hint		= RenderHint.AntiAlias;
		private	GlyphData[]	glyphs		= null;
		private	float		spacing		= 0.0f;
		private	bool		monospace	= false;
		private	bool		kerning		= false;
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
		public bool Kerning
		{
			get { return this.kerning; }
			set { this.kerning = value; this.needsReload = true; }
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
			get { return (int)Math.Round(this.font.FontFamily.GetCellAscent(this.font.Style) * this.font.Size / this.font.FontFamily.GetEmHeight(this.font.Style)); }
		}
		public int Descent
		{
			get { return (int)Math.Round(this.font.FontFamily.GetCellDescent(this.font.Style) * this.font.GetHeight() / this.font.FontFamily.GetLineSpacing(this.font.Style)); }
		}
		public int BaseLine
		{
			get { return (int)Math.Round(this.font.FontFamily.GetCellAscent(this.font.Style) * this.font.GetHeight() / this.font.FontFamily.GetLineSpacing(this.font.Style)); }
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

					// Retrieve kerning information
					if (this.kerning)
					{
						int sampleMultiOffset = glyphTemp.Height / 10;
						int[] kerningY = new int[] {
							this.BaseLine - this.Ascent + sampleMultiOffset,
							this.BaseLine - this.Ascent * 2 / 3,
							this.BaseLine - this.Ascent / 3,
							this.BaseLine,
							this.BaseLine + this.Descent - sampleMultiOffset};
						this.glyphs[i].kerningSamplesLeft	= new int[5];
						this.glyphs[i].kerningSamplesRight	= new int[5];
						if (str != " ")
						{
							ColorRGBA[] glyphTempPx = glyphTemp.GetPixelDataRGBA();
							int[] pxIndex = new int[3];
							// Left side samples
							for (int sampleIndex = 0; sampleIndex < this.glyphs[i].kerningSamplesLeft.Length; sampleIndex++)
							{
								pxIndex[0] = Math.Max(kerningY[sampleIndex] - sampleMultiOffset, 0) * glyphTemp.Width;
								pxIndex[1] = kerningY[sampleIndex] * glyphTemp.Width;
								pxIndex[2] = Math.Min(kerningY[sampleIndex] + sampleMultiOffset, glyphTemp.Height - 1) * glyphTemp.Width;
								for (int m = 0; m < pxIndex.Length; m++)
								{
									while (glyphTempPx[pxIndex[m]].a == 0)
									{
										this.glyphs[i].kerningSamplesLeft[sampleIndex]++;
										pxIndex[m]++;
										if (this.glyphs[i].kerningSamplesLeft[sampleIndex] >= glyphTemp.Width / 2) break;
									}
								}
								this.glyphs[i].kerningSamplesLeft[sampleIndex] = this.glyphs[i].kerningSamplesLeft[sampleIndex] / 3;
							}
							// Right side samples
							for (int sampleIndex = 0; sampleIndex < this.glyphs[i].kerningSamplesRight.Length; sampleIndex++)
							{
								pxIndex[0] = Math.Max(kerningY[sampleIndex] - sampleMultiOffset, 0) * glyphTemp.Width + glyphTemp.Width - 1;
								pxIndex[1] = kerningY[sampleIndex] * glyphTemp.Width + glyphTemp.Width - 1;
								pxIndex[2] = Math.Min(kerningY[sampleIndex] + sampleMultiOffset, glyphTemp.Height - 1) * glyphTemp.Width + glyphTemp.Width - 1;
								for (int m = 0; m < pxIndex.Length; m++)
								{
									while (glyphTempPx[pxIndex[m]].a == 0)
									{
										this.glyphs[i].kerningSamplesRight[sampleIndex]++;
										pxIndex[m]--;
										if (this.glyphs[i].kerningSamplesRight[sampleIndex] >= glyphTemp.Width / 2) break;
									}
								}
								this.glyphs[i].kerningSamplesRight[sampleIndex] = this.glyphs[i].kerningSamplesRight[sampleIndex] / 3;
							}
						}
					}
					else
					{
						this.glyphs[i].kerningSamplesLeft	= null;
						this.glyphs[i].kerningSamplesRight	= null;
					}

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

		public void DrawText(string text, ref VertexP3T2[] vertices, float x, float y, float z = 0.0f, float angle = 0.0f, float scale = 1.0f)
		{
			this.DrawText(text, ref vertices);
			
			Vector3 offset = new Vector3(x, y, z);
			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(angle, scale, out xDot, out yDot);

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i].pos;

				MathF.TransdormDotVec(ref vertex, ref xDot, ref yDot);
				vertex += offset;

				vertices[i].pos = vertex;
			}
		}
		public void DrawText(string text, ref VertexP3T2[] vertices)
		{
			if (vertices == null || vertices.Length != text.Length * 4) vertices = new VertexP3T2[text.Length * 4];
			
			float curOffset = 0.0f;
			char glyph;
			GlyphData glyphData;
			Rect uvRect;
			Vector2 glyphSize = Vector2.Zero;
			float glyphXOff = 0.0f;
			float glyphXAdv = 0.0f;
			for (int i = 0; i < text.Length; i++)
			{
				glyph = text[i];
				uvRect = this.texture.Atlas[CharLookup[(int)glyph]];

				this.GetGlyphData(glyph, out glyphData);
				glyphSize = new Vector2(glyphData.width, glyphData.height);
				glyphXOff = -glyphData.offsetX;

				if (this.kerning && !this.monospace)
				{
					char glyphNext = i + 1 < text.Length ? text[i + 1] : ' ';
					GlyphData glyphDataNext;
					this.GetGlyphData(glyphNext, out glyphDataNext);

					int minSum = int.MaxValue;
					for (int k = 0; k < glyphData.kerningSamplesRight.Length; k++)
						minSum = Math.Min(minSum, glyphData.kerningSamplesRight[k] + glyphDataNext.kerningSamplesLeft[k]);

					minSum = Math.Min(minSum, (int)Math.Round((glyphData.width + glyphDataNext.width) * 0.1f));
					glyphXAdv = (this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing - minSum;
				}
				else
					glyphXAdv = (this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing;

				vertices[i * 4 + 0].pos.X = curOffset + glyphXOff;
				vertices[i * 4 + 0].pos.Y = 0.0f;
				vertices[i * 4 + 0].pos.Z = 0.0f;
				vertices[i * 4 + 0].texCoord = uvRect.TopLeft;

				vertices[i * 4 + 1].pos.X = curOffset + glyphXOff + glyphSize.X;
				vertices[i * 4 + 1].pos.Y = 0.0f;
				vertices[i * 4 + 1].pos.Z = 0.0f;
				vertices[i * 4 + 1].texCoord = uvRect.TopRight;

				vertices[i * 4 + 2].pos.X = curOffset + glyphXOff + glyphSize.X;
				vertices[i * 4 + 2].pos.Y = glyphSize.Y;
				vertices[i * 4 + 2].pos.Z = 0.0f;
				vertices[i * 4 + 2].texCoord = uvRect.BottomRight;

				vertices[i * 4 + 3].pos.X = curOffset + glyphXOff;
				vertices[i * 4 + 3].pos.Y = glyphSize.Y;
				vertices[i * 4 + 3].pos.Z = 0.0f;
				vertices[i * 4 + 3].texCoord = uvRect.BottomLeft;

				curOffset += glyphXAdv;
			}
		}
		public Vector2 MeasureText(string text)
		{
			Vector2 textSize = Vector2.Zero;

			float curOffset = 0.0f;
			char glyph;
			GlyphData glyphData;
			Rect uvRect;
			Vector2 glyphSize = Vector2.Zero;
			float glyphXOff = 0.0f;
			float glyphXAdv = 0.0f;
			for (int i = 0; i < text.Length; i++)
			{
				glyph = text[i];
				uvRect = this.texture.Atlas[CharLookup[(int)glyph]];
				this.GetGlyphData(glyph, out glyphData);

				glyphSize = new Vector2(glyphData.width, glyphData.height);
				glyphXOff = -glyphData.offsetX;
				glyphXAdv = (this.monospace ? this.maxGlyphWidth : -glyphData.offsetX + glyphData.width) + this.spacing;

				textSize.X = Math.Max(textSize.X, curOffset + glyphXOff + glyphSize.X);
				textSize.Y = Math.Max(textSize.Y, glyphSize.Y);

				curOffset += glyphXAdv;
			}

			return textSize;
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
