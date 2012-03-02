using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CustomPropertyGrid.ControlRenderer
{
	public enum GroupHeaderStyle
	{
		Flat,
		Emboss,
		SmoothSunken
	}

	public static class Renderer
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
			Size checkBoxSize = CheckBoxRenderer.GetGlyphSize(Graphics.FromImage(new Bitmap(1, 1)), CheckBoxState.CheckedNormal);
			checkBoxImages = new Dictionary<CheckBoxState,Bitmap>();
			foreach (CheckBoxState checkState in Enum.GetValues(typeof(CheckBoxState)))
			{
				Bitmap image = new Bitmap(checkBoxSize.Width, checkBoxSize.Height);
				using (Graphics checkBoxGraphics = Graphics.FromImage(image))
				{
					CheckBoxRenderer.DrawCheckBox(checkBoxGraphics, Point.Empty, checkState);
				}
				checkBoxImages[checkState] = image;
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
