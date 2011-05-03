using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using Duality.ColorFormat;

namespace Duality
{
	public static class ExtMethodsBitmap
	{
		public enum ResizeOrigin
		{
			Center,

			Left,
			Right,
			Top,
			Bottom,

			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight
		}

		public static Bitmap Resize(this Bitmap bm, int w, int h, ResizeOrigin origin = ResizeOrigin.TopLeft)
		{
			int x = 0;
			int y = 0;

			if (origin == ResizeOrigin.Right || 
				origin == ResizeOrigin.TopRight || 
				origin == ResizeOrigin.BottomRight)
				x = w - bm.Width;
			else if (
				origin == ResizeOrigin.Center || 
				origin == ResizeOrigin.Top || 
				origin == ResizeOrigin.Bottom)
				x = (w - bm.Width) / 2;

			if (origin == ResizeOrigin.Bottom || 
				origin == ResizeOrigin.BottomLeft || 
				origin == ResizeOrigin.BottomRight)
				y = h - bm.Height;
			else if (
				origin == ResizeOrigin.Center || 
				origin == ResizeOrigin.Left || 
				origin == ResizeOrigin.Right)
				y = (h - bm.Height) / 2;

			Bitmap result = new Bitmap(w, h);
			using (Graphics g = Graphics.FromImage((Image)result))
			{
				g.DrawImageUnscaledAndClipped(bm, new Rectangle(x, y, Math.Min(bm.Width, w), Math.Min(bm.Height, h)));
			}

			return result;
		}
		public static Bitmap Rescale(this Bitmap bm, int w, int h, InterpolationMode mode = InterpolationMode.Bilinear)
		{
			Bitmap result = new Bitmap(w, h);
			using (Graphics g = Graphics.FromImage((Image)result))
			{
				g.InterpolationMode = mode;

				System.Drawing.Imaging.ImageAttributes imageAttr = new System.Drawing.Imaging.ImageAttributes();
				imageAttr.SetWrapMode(WrapMode.TileFlipXY);
				g.DrawImage(bm, 
					new Rectangle(0, 0, w, h),
					0, 0, bm.Width, bm.Height,
					GraphicsUnit.Pixel,
					imageAttr);
			}
			return result;
		}
		public static Bitmap ColorTransparentPixels(this Bitmap bm)
		{
			Bitmap result = bm.Clone() as Bitmap;
			ColorRGBA[] pixelData = result.GetPixelDataRGBA();

			Point	pos		= new Point();
			int[]	nPos	= new int[8];
			bool[]	nOk		= new bool[8];
			int[]	nMult	= new int[]{2, 2, 2, 2, 1, 1, 1, 1};
			int[]	mixClr	= null;

			for (int i = 0; i < pixelData.Length; i++)
			{
				if (pixelData[i].a != 0) continue;

				pos.Y	= i / bm.Width;
				pos.X	= i - (pos.Y * bm.Width);

				nPos[0] = (pos.X + ((pos.Y - 1) * bm.Width));
				nPos[1] = (pos.X + ((pos.Y + 1) * bm.Width));
				nPos[2] = ((pos.X - 1) + (pos.Y * bm.Width));
				nPos[3] = ((pos.X + 1) + (pos.Y * bm.Width));
				nPos[4] = ((pos.X - 1) + ((pos.Y - 1) * bm.Width));
				nPos[5] = ((pos.X - 1) + ((pos.Y + 1) * bm.Width));
				nPos[6] = ((pos.X + 1) + ((pos.Y - 1) * bm.Width));
				nPos[7] = ((pos.X + 1) + ((pos.Y + 1) * bm.Width));

				nOk[0]	= pos.Y > 0;
				nOk[1]	= pos.Y < bm.Height - 1;
				nOk[2]	= pos.X > 0;
				nOk[3]	= pos.X < bm.Width - 1;
				nOk[4]	= pos.X > 0 && pos.Y > 0;
				nOk[5]	= pos.X > 0 && pos.Y < bm.Height - 1;
				nOk[6]	= pos.X < bm.Width - 1 && pos.Y > 0;
				nOk[7]	= pos.X < bm.Width - 1 && pos.Y < bm.Height - 1;

				for (int j = 0; j < nPos.Length; j++)
				{
					if (!nOk[j]) continue;
					if (pixelData[nPos[j]].a == 0) continue;

					if (mixClr == null)
						mixClr = new int[4];

					mixClr[0] += pixelData[nPos[j]].r * nMult[j];
					mixClr[1] += pixelData[nPos[j]].g * nMult[j];
					mixClr[2] += pixelData[nPos[j]].b * nMult[j];
					mixClr[3] += nMult[j];
				}

				if (mixClr != null)
				{
					pixelData[i].r = (byte)Math.Round((float)mixClr[0] / (float)mixClr[3]);
					pixelData[i].g = (byte)Math.Round((float)mixClr[1] / (float)mixClr[3]);
					pixelData[i].b = (byte)Math.Round((float)mixClr[2] / (float)mixClr[3]);
					mixClr = null;
				}
			}

			result.SetPixelDataRGBA(pixelData);
			return result;
		}
		public static Bitmap ColorTransparentPixels(this Bitmap bm, ColorRGBA transparentColor)
		{
			Bitmap result = bm.Clone() as Bitmap;
			ColorRGBA[] pixelData = result.GetPixelDataRGBA();

			for (int i = 0; i < pixelData.Length; i++)
			{
				if (pixelData[i].a != 0) continue;
				pixelData[i] = transparentColor;
			}

			result.SetPixelDataRGBA(pixelData);
			return result;
		}

		public static ColorRGBA[] GetPixelDataRGBA(this Bitmap bm)
		{
			BitmapData data = bm.LockBits(
				new Rectangle(0, 0, bm.Width, bm.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			
			int pixels = data.Width * data.Height;
			int[] argbValues = new int[pixels];
			System.Runtime.InteropServices.Marshal.Copy(data.Scan0, argbValues, 0, pixels);

			bm.UnlockBits(data);

			// Convert to ColorRGBA
			ColorRGBA[] result = new ColorRGBA[pixels];
			unchecked
			{
				for (int i = 0; i < pixels; i++)
					result[i].SetIntArgb((uint)argbValues[i]);
			}
			return result;
		}
		public static void SetPixelDataRGBA(this Bitmap bm, ColorRGBA[] pixelData)
		{
			BitmapData data = bm.LockBits(
				new Rectangle(0, 0, bm.Width, bm.Height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format32bppArgb);
			
			int pixels = data.Width * data.Height;
			int[] argbValues = new int[pixels];
			unchecked
			{
				for (int i = 0; i < pixels; i++)
					argbValues[i] = (int)pixelData[i].ToIntArgb();
			}
			System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, data.Scan0, pixels);

			bm.UnlockBits(data);
		}
		public static void SetPixelDataRGBA(this Bitmap bm, byte[] pixelData)
		{
			BitmapData data = bm.LockBits(
				new Rectangle(0, 0, bm.Width, bm.Height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format32bppArgb);
			
			int pixels = data.Width * data.Height;
			int[] argbValues = new int[pixels];
			unchecked
			{
				for (int i = 0; i < pixels; i++)
					argbValues[i] = 
						((int)pixelData[i * 4 + 3] << 24) |
						((int)pixelData[i * 4 + 0] << 16) |
						((int)pixelData[i * 4 + 1] << 8) |
						((int)pixelData[i * 4 + 2] << 0);
			}
			System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, data.Scan0, pixels);

			bm.UnlockBits(data);
		}
	}
}
