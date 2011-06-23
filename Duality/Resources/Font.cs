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

using OpenTK;

namespace Duality.Resources
{
	[Serializable]
	public class Font : Resource
	{
		public new const string FileExt = ".Font" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Font:";
		public const string ContentPath_GenericMonospace10	= VirtualContentPath + "GenericMonospace10";

		public static ContentRef<Font> GenericMonospace10	{ get; private set; }

		internal static void InitDefaultContent()
		{
			Font tmp;

			tmp = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular); tmp.path = ContentPath_GenericMonospace10;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			GenericMonospace10	= ContentProvider.RequestContent<Font>(ContentPath_GenericMonospace10);
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
			AntiAlias	= System.Drawing.Text.TextRenderingHint.AntiAliasGridFit,
			ClearType	= System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
		}

		public struct GlyphData
		{
			public	int	width;
			public	int	height;
			public	int	offsetX;
		}


		private SysDrawFont	font	= null;
		private	ColorRGBA	color	= ColorRGBA.White;
		private	ColorRGBA	bgColor	= ColorRGBA.TransparentWhite;
		private	RenderHint	hint	= RenderHint.AntiAlias;
		[NonSerialized]	private	Pixmap	pixelData	= null;
		[NonSerialized]	private	Texture	texture		= null;

		public Font(FontFamily family, float emSize, FontStyle style) : this(new SysDrawFont(family, emSize, style)) {}
		public Font(string family, float emSize, FontStyle style) : this(new SysDrawFont(family, emSize, style)) {}
		public Font(SysDrawFont font)
		{
			this.InitFromFont(font);
		}

		public void InitFromFont(SysDrawFont font)
		{
			this.font = font;

			int cols;
			int rows;
			cols = rows = (int)Math.Ceiling(Math.Sqrt(SupportedChars.Length));

			Bitmap pxTemp = new Bitmap(MathF.RoundToInt(cols * this.font.Size), MathF.RoundToInt(rows * this.font.Height));
			Rect[] atlas = new Rect[SupportedChars.Length];
			GlyphData[] glyphData = new GlyphData[SupportedChars.Length];
			using (Graphics pxGraphics = Graphics.FromImage((Image)pxTemp))
			{
				Brush fntBrush = new SolidBrush(Color.FromArgb(this.color.a, this.color.r, this.color.g, this.color.b));
				Bitmap glyphTemp;
				Bitmap glyphTempTypo;

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
						glyphGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));
						glyphGraphics.DrawString(str, this.font, fntBrush, 0, 0, StringFormat.GenericDefault);
					}
					glyphTemp = glyphTemp.Crop(true, false);

					// Render a single glyph in typographic mode
					glyphTempTypo = new Bitmap((int)Math.Ceiling(Math.Max(1, charSize.Width)), this.font.Height);
					using (Graphics glyphGraphics = Graphics.FromImage((Image)glyphTempTypo))
					{
						glyphGraphics.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.hint;
						glyphGraphics.Clear(Color.FromArgb(this.bgColor.a, this.bgColor.r, this.bgColor.g, this.bgColor.b));
						glyphGraphics.DrawString(str, this.font, fntBrush, 0, 0, StringFormat.GenericTypographic);
					}
					glyphTempTypo = glyphTempTypo.Crop(true, false);

					// Determine begin and width of the glyph "body"
					int glyphBodyMinX = 0;
					int glyphBodyMaxX = 0;
					{
						ColorRGBA[] glyphTempPixels = glyphTemp.GetPixelDataRGBA();
						float colWeight = 0.0f;
						float colWeightLast;
						// Determine "body" boundaries
						for (int gtp_x = 0; gtp_x < glyphTemp.Width; gtp_x++)
						{
							int gtp_count = 0;
							for (int gtp_y = 0; gtp_y < glyphTemp.Height; gtp_y++)
							{
								int gtp_i = gtp_x + gtp_y * glyphTemp.Width;
								gtp_count += glyphTempPixels[gtp_i].a;
							}
							colWeightLast = colWeight;
							colWeight = (float)gtp_count / (255.0f * (float)glyphTemp.Height);
							if (colWeight <= colWeightLast || colWeight > colWeightLast + 0.05f || colWeight > 0.1f)
							{
								glyphBodyMinX = gtp_x;
								break;
							}
						}
						colWeight = 0.0f;
						for (int gtp_x = glyphTemp.Width - 1; gtp_x >= 0; gtp_x--)
						{
							int gtp_count = 0;
							for (int gtp_y = 0; gtp_y < glyphTemp.Height; gtp_y++)
							{
								int gtp_i = gtp_x + gtp_y * glyphTemp.Width;
								gtp_count += glyphTempPixels[gtp_i].a;
							}
							colWeightLast = colWeight;
							colWeight = (float)gtp_count / (255.0f * (float)glyphTemp.Height);
							if (colWeight <= colWeightLast || colWeight > colWeightLast + 0.05f || colWeight > 0.1f)
							{
								glyphBodyMaxX = gtp_x;
								break;
							}
						}
					}

					// ToDo: Figure out if that experimental "glyph body" stuff is needed i.e. can be used or not.

					// Memorize atlas coordinates & glyph data
					glyphData[i].width = glyphTemp.Width;
					glyphData[i].height = glyphTemp.Height;
					glyphData[i].offsetX = glyphTemp.Width - glyphTempTypo.Width;
					atlas[i].x = (float)x / (float)pxTemp.Width;
					atlas[i].y = (float)y / (float)pxTemp.Height;
					atlas[i].w = (float)glyphTemp.Width / (float)pxTemp.Width;
					atlas[i].h = (float)this.font.Height / (float)pxTemp.Height;

					// Debug
					Log.Core.Write("{0}\t{1}\t{2}\t{3}",
						str,
						glyphData[i].width,
						glyphData[i].height,
						glyphData[i].offsetX);

					// Draw it onto the font surface
					if (x + glyphTemp.Width + 1 > pxTemp.Width)
					{
						x = 0;
						y += this.font.Height;
					}

					pxGraphics.DrawImageUnscaled(glyphTemp, x, y);
					pxGraphics.DrawImageUnscaled(glyphTempTypo, x + glyphData[i].offsetX, y);
					pxGraphics.DrawRectangle(Pens.Blue, x, y, glyphData[i].width, glyphData[i].height);
					pxGraphics.DrawRectangle(Pens.Green, x + glyphData[i].offsetX, y + 1, glyphData[i].width - glyphData[i].offsetX, glyphData[i].height - 2);
					pxGraphics.DrawRectangle(Pens.Red, x + glyphBodyMinX, y + 2, glyphBodyMaxX - glyphBodyMinX, glyphData[i].height - 4);

					x += glyphTemp.Width + 1;
				}
			}

			// ToDo: Generate pixmap and texture

			// Debug
			string tmp = System.IO.Path.GetTempFileName() + ".png";
			pxTemp.Save(tmp);
			System.Diagnostics.Process.Start(tmp);
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
