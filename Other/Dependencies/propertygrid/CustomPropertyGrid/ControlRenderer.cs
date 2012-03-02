using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;

namespace CustomPropertyGrid.Renderer
{
	public enum CheckBoxState
	{
		CheckedDisabled		= System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled,
		CheckedPressed		= System.Windows.Forms.VisualStyles.CheckBoxState.CheckedPressed,
		CheckedHot			= System.Windows.Forms.VisualStyles.CheckBoxState.CheckedHot,
		CheckedNormal		= System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal,

		UncheckedDisabled	= System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled,
		UncheckedPressed	= System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedPressed,
		UncheckedHot		= System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedHot,
		UncheckedNormal		= System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal,

		MixedDisabled		= System.Windows.Forms.VisualStyles.CheckBoxState.MixedDisabled,
		MixedPressed		= System.Windows.Forms.VisualStyles.CheckBoxState.MixedPressed,
		MixedHot			= System.Windows.Forms.VisualStyles.CheckBoxState.MixedHot,
		MixedNormal			= System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal,

		PlusDisabled,
		PlusPressed,
		PlusHot,
		PlusNormal,

		MinusDisabled,
		MinusPressed,
		MinusHot,
		MinusNormal
	}
	public enum GroupHeaderStyle
	{
		Flat,
		Emboss,
		SmoothSunken
	}

	public static class ControlRenderer
	{
		private	static	Dictionary<CheckBoxState,Bitmap>	checkBoxImages	= null;

		public static Size CheckBoxSize
		{
			get
			{
				InitCheckBox();
				return checkBoxImages.Values.First().Size;
			}
		}
		public static void DrawCheckBox(Graphics g, Point loc, CheckBoxState state)
		{
			InitCheckBox();
			g.DrawImageUnscaled(checkBoxImages[state], loc);
		}

		public static void DrawGroupHeaderBackground(Graphics g, Rectangle rect, Color baseColor, GroupHeaderStyle style)
		{
			Color lightColor = baseColor.ScaleBrightness(style == GroupHeaderStyle.SmoothSunken ? 0.85f : 1.1f);
			Color darkColor = baseColor.ScaleBrightness(style == GroupHeaderStyle.SmoothSunken ? 0.95f : 0.85f);
			LinearGradientBrush gradientBrush = new LinearGradientBrush(rect, lightColor, darkColor, 90.0f);

			if (style != GroupHeaderStyle.Flat)
				g.FillRectangle(gradientBrush, rect);
			g.DrawLine(new Pen(Color.FromArgb(128, Color.White)), rect.Left, rect.Top, rect.Right, rect.Top);
			g.DrawLine(new Pen(Color.FromArgb(64, Color.Black)), rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
		}

		public static void DrawStringLine(Graphics g, string text, Font font, Rectangle textRect, Color color, StringAlignment align = StringAlignment.Near, StringAlignment lineAlign = StringAlignment.Center)
		{
			textRect.Width -= 5;
			StringFormat nameLabelFormat = StringFormat.GenericDefault;
			nameLabelFormat.Alignment = align;
			nameLabelFormat.LineAlignment = lineAlign;
			nameLabelFormat.Trimming = StringTrimming.Character;
			nameLabelFormat.FormatFlags |= StringFormatFlags.NoWrap;

			int charsFit, lines;
			SizeF nameLabelSize = g.MeasureString(text, font, textRect.Size, nameLabelFormat, out charsFit, out lines);
			g.DrawString(text, font, new SolidBrush(color), textRect, nameLabelFormat);

			if (charsFit < text.Length)
			{
				Pen ellipsisPen = new Pen(color);
				ellipsisPen.DashStyle = DashStyle.Dot;
				g.DrawLine(ellipsisPen, 
					textRect.Right, 
					(textRect.Y + textRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f), 
					textRect.Right + 3, 
					(textRect.Y + textRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f));
			}
		}

		private static void InitCheckBox()
		{
			if (checkBoxImages != null) return;
			Size checkBoxSize = CheckBoxRenderer.GetGlyphSize(Graphics.FromImage(new Bitmap(1, 1)), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
			checkBoxImages = new Dictionary<CheckBoxState,Bitmap>();
			foreach (CheckBoxState checkState in Enum.GetValues(typeof(CheckBoxState)))
			{
				Bitmap image = new Bitmap(checkBoxSize.Width, checkBoxSize.Height);
				using (Graphics checkBoxGraphics = Graphics.FromImage(image))
				{
					if (checkState == CheckBoxState.PlusDisabled || 
						checkState == CheckBoxState.PlusHot ||
						checkState == CheckBoxState.PlusNormal ||
						checkState == CheckBoxState.PlusPressed ||
						checkState == CheckBoxState.MinusDisabled || 
						checkState == CheckBoxState.MinusHot ||
						checkState == CheckBoxState.MinusNormal ||
						checkState == CheckBoxState.MinusPressed)
					{
						Color plusSignColor;
						Pen expandLineShadowPen;
						Pen expandLinePen;

						if (checkState == CheckBoxState.PlusNormal || checkState == CheckBoxState.MinusNormal)
						{
							plusSignColor = Color.FromArgb(24, 32, 82);
							expandLinePen = new Pen(Color.FromArgb(255, plusSignColor));
							expandLineShadowPen = new Pen(Color.FromArgb(64, plusSignColor));
							CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
						}
						else if (checkState == CheckBoxState.PlusHot || checkState == CheckBoxState.MinusHot)
						{
							plusSignColor = Color.FromArgb(32, 48, 123);
							expandLinePen = new Pen(Color.FromArgb(255, plusSignColor));
							expandLineShadowPen = new Pen(Color.FromArgb(64, plusSignColor));
							CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedHot);
						}
						else if (checkState == CheckBoxState.PlusPressed || checkState == CheckBoxState.MinusPressed)
						{
							plusSignColor = Color.FromArgb(48, 64, 164);
							expandLinePen = new Pen(Color.FromArgb(255, plusSignColor));
							expandLineShadowPen = new Pen(Color.FromArgb(96, plusSignColor));
							CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedPressed);
						}
						else //if (checkState == CheckBoxState.PlusDisabled)
						{
							plusSignColor = Color.FromArgb(24, 28, 41);
							expandLinePen = new Pen(Color.FromArgb(128, plusSignColor));
							expandLineShadowPen = new Pen(Color.FromArgb(32, plusSignColor));
							CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled);
						}

						// Plus Shadow
						checkBoxGraphics.DrawLine(expandLineShadowPen, 
							3,
							image.Height / 2 + 1,
							image.Width - 4,
							image.Height / 2 + 1);
						checkBoxGraphics.DrawLine(expandLineShadowPen, 
							3,
							image.Height / 2 - 1,
							image.Width - 4,
							image.Height / 2 - 1);
						if (checkState == CheckBoxState.PlusDisabled ||
							checkState == CheckBoxState.PlusHot ||
							checkState == CheckBoxState.PlusNormal ||
							checkState == CheckBoxState.PlusPressed)
						{
							checkBoxGraphics.DrawLine(expandLineShadowPen, 
								image.Width / 2 + 1,
								3,
								image.Width / 2 + 1,
								image.Height - 4);
							checkBoxGraphics.DrawLine(expandLineShadowPen, 
								image.Width / 2 - 1,
								3,
								image.Width / 2 - 1,
								image.Height - 4);
						}
						// Plus
						checkBoxGraphics.DrawLine(expandLinePen, 
							3,
							image.Height / 2,
							image.Width - 4,
							image.Height / 2);
						if (checkState == CheckBoxState.PlusDisabled ||
							checkState == CheckBoxState.PlusHot ||
							checkState == CheckBoxState.PlusNormal ||
							checkState == CheckBoxState.PlusPressed)
						{
							checkBoxGraphics.DrawLine(expandLinePen, 
								image.Width / 2,
								3,
								image.Width / 2,
								image.Height - 4);
						}
					}
					else
					{
						CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, (System.Windows.Forms.VisualStyles.CheckBoxState)checkState);
					}
				}
				checkBoxImages[checkState] = image;
			}
		}
	}

	public class IconImage
	{
		private	Image		sourceImage	= null;
		private	Bitmap[]	images		= new Bitmap[4];
		
		public Image SourceImage
		{
			get { return this.sourceImage; }
		}
		public Image Passive
		{
			get { return this.images[0]; }
		}
		public Image Normal
		{
			get { return this.images[1]; }
		}
		public Image Active
		{
			get { return this.images[2]; }
		}
		public Image Disabled
		{
			get { return this.images[3]; }
		}

		public int Width
		{
			get { return this.sourceImage.Width; }
		}
		public int Height
		{
			get { return this.sourceImage.Height; }
		}
		public Size Size
		{
			get { return this.sourceImage.Size; }
		}

		public IconImage(Image source)
		{
			this.sourceImage = source;

			// Generate specific images
			var imgAttribs = new System.Drawing.Imaging.ImageAttributes();
			System.Drawing.Imaging.ColorMatrix colorMatrix = null;
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
				imgAttribs.SetColorMatrix(colorMatrix);
				this.images[0] = new Bitmap(source.Width, source.Height);
				using (Graphics g = Graphics.FromImage(this.images[0]))
				{
					g.DrawImage(source, 
						new Rectangle(Point.Empty, source.Size), 
						0, 0, source.Width, source.Height, GraphicsUnit.Pixel, 
						imgAttribs);
				}
			}
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
				imgAttribs.SetColorMatrix(colorMatrix);
				this.images[1] = new Bitmap(source.Width, source.Height);
				using (Graphics g = Graphics.FromImage(this.images[1]))
				{
					g.DrawImage(source, 
						new Rectangle(Point.Empty, source.Size), 
						0, 0, source.Width, source.Height, GraphicsUnit.Pixel, 
						imgAttribs);
				}
			}
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {1.3f, 0.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 1.3f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 1.3f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
				imgAttribs.SetColorMatrix(colorMatrix);
				this.images[2] = new Bitmap(source.Width, source.Height);
				using (Graphics g = Graphics.FromImage(this.images[2]))
				{
					g.DrawImage(source, 
						new Rectangle(Point.Empty, source.Size), 
						0, 0, source.Width, source.Height, GraphicsUnit.Pixel, 
						imgAttribs);
				}
			}
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
				imgAttribs.SetColorMatrix(colorMatrix);
				this.images[3] = new Bitmap(source.Width, source.Height);
				using (Graphics g = Graphics.FromImage(this.images[3]))
				{
					g.DrawImage(source, 
						new Rectangle(Point.Empty, source.Size), 
						0, 0, source.Width, source.Height, GraphicsUnit.Pixel, 
						imgAttribs);
				}
			}
		}
	}

	public static class ExtMethodsSystemColor
	{
		public static Color ScaleBrightness(this Color c, float ratio)
		{
			return Color.FromArgb(c.A,
				(byte)Math.Min(Math.Max((float)c.R * ratio, 0.0f), 255.0f),
				(byte)Math.Min(Math.Max((float)c.G * ratio, 0.0f), 255.0f),
				(byte)Math.Min(Math.Max((float)c.B * ratio, 0.0f), 255.0f));
		}
	}
}
