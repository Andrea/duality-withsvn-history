using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CustomPropertyGrid
{
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
}
