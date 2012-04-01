using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Duality.ColorFormat;

namespace Duality
{
	/// <summary>
	/// Provides extension methods for <see cref="System.Drawing.Bitmap">Bitmaps</see>.
	/// </summary>
	public static class ExtMethodsBitmap
	{
		/// <summary>
		/// Extracts a rectangular portion of the original image.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="x">The rectangular portion to extract.</param>
		/// <param name="y">The rectangular portion to extract.</param>
		/// <param name="w">The rectangular portion to extract.</param>
		/// <param name="h">The rectangular portion to extract.</param>
		/// <returns>A new Bitmap containing the selected area.</returns>
		public static Bitmap SubImage(this Bitmap bm, int x, int y, int w, int h)
		{
			if (w == 0 || h == 0) return null;
			Bitmap result = new Bitmap(w, h);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.DrawImageUnscaledAndClipped(bm, new Rectangle(-x, -y, bm.Width, bm.Height));
			}
			return result;
		}
		/// <summary>
		/// Extracts a rectangular portion of the original image.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="rect">The rectangular portion to extract.</param>
		/// <returns>A new Bitmap containing the selected area.</returns>
		public static Bitmap SubImage(this Bitmap bm, Rect rect)
		{
			return SubImage(bm,
				MathF.RoundToInt(rect.x),
				MathF.RoundToInt(rect.y),
				MathF.RoundToInt(rect.w),
				MathF.RoundToInt(rect.h));
		}
		/// <summary>
		/// Extracts a rectangular portion of the original image.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="rect">The rectangular portion to extract.</param>
		/// <returns>A new Bitmap containing the selected area.</returns>
		public static Bitmap SubImage(this Bitmap bm, Rectangle rect)
		{
			return SubImage(bm, rect.X, rect.Y, rect.Width, rect.Height);
		}
		/// <summary>
		/// Creates a resized version of a Bitmap. Gained space will be empty, lost space will crop the image.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="w">The desired width.</param>
		/// <param name="h">The desired height.</param>
		/// <param name="origin">The desired resize origin in the original image.</param>
		/// <returns>A new Bitmap that has the specified size.</returns>
		public static Bitmap Resize(this Bitmap bm, int w, int h, Alignment origin = Alignment.TopLeft)
		{
			int x = 0;
			int y = 0;

			if (origin == Alignment.Right || 
				origin == Alignment.TopRight || 
				origin == Alignment.BottomRight)
				x = w - bm.Width;
			else if (
				origin == Alignment.Center || 
				origin == Alignment.Top || 
				origin == Alignment.Bottom)
				x = (w - bm.Width) / 2;

			if (origin == Alignment.Bottom || 
				origin == Alignment.BottomLeft || 
				origin == Alignment.BottomRight)
				y = h - bm.Height;
			else if (
				origin == Alignment.Center || 
				origin == Alignment.Left || 
				origin == Alignment.Right)
				y = (h - bm.Height) / 2;

			return bm.SubImage(-x, -y, w, h);
		}
		/// <summary>
		/// Creates a rescaled version of a Bitmap.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="w">The desired width.</param>
		/// <param name="h">The desired height.</param>
		/// <param name="mode">Specified how to interpolate the original image in order to calculate the result image.</param>
		/// <returns>A new Bitmap that has been scaled to the specified size.</returns>
		public static Bitmap Rescale(this Bitmap bm, int w, int h, InterpolationMode mode = InterpolationMode.Bilinear)
		{
			Bitmap result = new Bitmap(w, h);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.InterpolationMode = mode;

				ImageAttributes imageAttr = new ImageAttributes();
				imageAttr.SetWrapMode(WrapMode.TileFlipXY);
				g.DrawImage(bm, 
					new Rectangle(0, 0, w, h),
					0, 0, bm.Width, bm.Height,
					GraphicsUnit.Pixel,
					imageAttr);
			}
			return result;
		}
		/// <summary>
		/// Creates a cropped version of the specified Bitmap, removing transparent / empty border areas.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="cropX">Whether the image should be cropped in X-direction</param>
		/// <param name="cropY">Whether the image should be cropped in Y-direction</param>
		/// <returns>A cropped version of the original Bitmap.</returns>
		public static Bitmap Crop(this Bitmap bm, bool cropX = true, bool cropY = true)
		{
			if (!cropX && !cropY) return bm.Clone() as Bitmap;
			Rectangle bounds = bm.OpaqueBounds();
			return bm.SubImage(cropX ? bounds.X : 0, cropY ? bounds.Y : 0, cropX ? bounds.Width : bm.Width, cropY ? bounds.Height : bm.Height);
		}
		/// <summary>
		/// Measures the bounding rectangle of the opaque pixels in a Bitmap.
		/// </summary>
		/// <param name="bm"></param>
		/// <returns></returns>
		public static Rectangle OpaqueBounds(this Bitmap bm)
		{
			ColorRgba[] pixels = bm.GetPixelDataRgba();
			Rectangle bounds = new Rectangle(bm.Width, bm.Height, 0, 0);
			for (int i = 0; i < pixels.Length; i++)
			{
				if (pixels[i].a == 0) continue;
				int x = i % bm.Width;
				int y = i / bm.Width;
				bounds.X = Math.Min(bounds.X, x);
				bounds.Y = Math.Min(bounds.Y, y);
				bounds.Width = Math.Max(bounds.Width, x);
				bounds.Height = Math.Max(bounds.Height, y);
			}
			bounds.Width = 1 + Math.Max(0, bounds.Width - bounds.X);
			bounds.Height = 1 + Math.Max(0, bounds.Height - bounds.Y);

			return bounds;
		}
		/// <summary>
		/// Measures the bounding rectangle of the opaque pixels in a Bitmap. Returns a float value result
		/// </summary>
		/// <param name="bm"></param>
		/// <returns></returns>
		public static Rect OpaqueBoundsF(this Bitmap bm)
		{
			Rectangle bounds = OpaqueBounds(bm);
			return new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
		}
		/// <summary>
		/// Creates a new version of a Bitmap, where transparent pixels have been colored based on the non-transparent color values next to them.
		/// This does not affect any alpha values but prepares the Bitmap for correct filtering of edges.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <returns></returns>
		public static Bitmap ColorTransparentPixels(this Bitmap bm)
		{
			Bitmap result = bm.Clone() as Bitmap;
			ColorRgba[] pixelData = result.GetPixelDataRgba();

			Point	pos		= new Point();
			int[]	nPos	= new int[8];
			bool[]	nOk		= new bool[8];
			int[]	nMult	= new[]{2, 2, 2, 2, 1, 1, 1, 1};
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

			result.SetPixelDataRgba(pixelData);
			return result;
		}
		/// <summary>
		/// Creates a new version of a Bitmap, where transparent pixels have been colored based on the specified transparent color.
		/// This does not affect any alpha values but prepares the Bitmap for correct filtering of edges.
		/// </summary>
		/// <param name="bm">The original Bitmap.</param>
		/// <param name="transparentColor"></param>
		/// <returns></returns>
		public static Bitmap ColorTransparentPixels(this Bitmap bm, ColorRgba transparentColor)
		{
			Bitmap result = bm.Clone() as Bitmap;
			ColorRgba[] pixelData = result.GetPixelDataRgba();

			for (int i = 0; i < pixelData.Length; i++)
			{
				if (pixelData[i].a != 0) continue;
				pixelData[i] = transparentColor;
			}

			result.SetPixelDataRgba(pixelData);
			return result;
		}
		/// <summary>
		/// Determines the average color of a Bitmap.
		/// </summary>
		/// <param name="bm"></param>
		/// <param name="weightTransparent">If true, the alpha value weights a pixels color value. </param>
		/// <returns></returns>
		public static ColorRgba GetAverageColor(this Bitmap bm, bool weightTransparent = true)
		{
			float[] sum = new float[4];
			int count = 0;
			ColorRgba[] pixelData = bm.GetPixelDataRgba();

			if (weightTransparent)
			{
				for (int i = 0; i < pixelData.Length; i++)
				{
					sum[0] += pixelData[i].r * ((float)pixelData[i].a / 255.0f);
					sum[1] += pixelData[i].g * ((float)pixelData[i].a / 255.0f);
					sum[2] += pixelData[i].b * ((float)pixelData[i].a / 255.0f);
					sum[3] += (float)pixelData[i].a / 255.0f;
					++count;
				}
				if (sum[3] <= 0.001f) return ColorRgba.TransparentBlack;

				return new ColorRgba(
					(byte)MathF.Clamp((int)(sum[0] / sum[3]), 0, 255),
					(byte)MathF.Clamp((int)(sum[1] / sum[3]), 0, 255),
					(byte)MathF.Clamp((int)(sum[2] / sum[3]), 0, 255),
					(byte)MathF.Clamp((int)(sum[3] / (float)count), 0, 255));
			}
			else
			{
				for (int i = 0; i < pixelData.Length; i++)
				{
					sum[0] += pixelData[i].r;
					sum[1] += pixelData[i].g;
					sum[2] += pixelData[i].b;
					sum[3] += pixelData[i].a;
					++count;
				}
				if (count == 0) return ColorRgba.TransparentBlack;

				return new ColorRgba(
					(byte)MathF.Clamp((int)(sum[0] / (float)count), 0, 255),
					(byte)MathF.Clamp((int)(sum[1] / (float)count), 0, 255),
					(byte)MathF.Clamp((int)(sum[2] / (float)count), 0, 255),
					(byte)MathF.Clamp((int)(sum[3] / (float)count), 0, 255));
			}
		}

		/// <summary>
		/// Extracts a Bitmaps pixel data.
		/// </summary>
		/// <param name="bm"></param>
		/// <returns></returns>
		public static ColorRgba[] GetPixelDataRgba(this Bitmap bm)
		{
			int[] argbValues = GetPixelDataIntArgb(bm);

			// Convert to ColorRGBA
			ColorRgba[] result = new ColorRgba[argbValues.Length];
			unchecked
			{
				for (int i = 0; i < argbValues.Length; i++)
					result[i].SetIntArgb(argbValues[i]);
			}
			return result;
		}
		/// <summary>
		/// Extracts a Bitmaps pixel data as (signed) IntArgb values.
		/// </summary>
		/// <param name="bm"></param>
		public static int[] GetPixelDataIntArgb(this Bitmap bm)
		{
			BitmapData data = bm.LockBits(
				new Rectangle(0, 0, bm.Width, bm.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			
			int pixels = data.Width * data.Height;
			int[] argbValues = new int[pixels];
			System.Runtime.InteropServices.Marshal.Copy(data.Scan0, argbValues, 0, pixels);
			bm.UnlockBits(data);

			return argbValues;
		}

		/// <summary>
		/// Replaces a Bitmaps pixel data.
		/// </summary>
		/// <param name="bm"></param>
		/// <param name="pixelData"></param>
		public static void SetPixelDataRgba(this Bitmap bm, ColorRgba[] pixelData)
		{
			int[] argbValues = new int[pixelData.Length];
			unchecked
			{
				for (int i = 0; i < pixelData.Length; i++)
					argbValues[i] = pixelData[i].ToIntArgb();
			}
			SetPixelDataIntArgb(bm, argbValues);
		}
		/// <summary>
		/// Replaces a Bitmaps pixel data.
		/// </summary>
		/// <param name="bm"></param>
		/// <param name="pixelData"></param>
		public static void SetPixelDataRgba(this Bitmap bm, byte[] pixelData)
		{
			int pixels = (int)MathF.Ceiling(pixelData.Length / 4.0f);
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
			SetPixelDataIntArgb(bm, argbValues);
		}
		/// <summary>
		/// Replaces a Bitmaps pixel data.
		/// </summary>
		/// <param name="bm"></param>
		/// <param name="pixelData"></param>
		public static void SetPixelDataIntArgb(this Bitmap bm, int[] pixelData)
		{
			BitmapData data = bm.LockBits(
				new Rectangle(0, 0, bm.Width, bm.Height),
				ImageLockMode.WriteOnly,
				PixelFormat.Format32bppArgb);
			
			int pixels = data.Width * data.Height;
			System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, data.Scan0, pixels);

			bm.UnlockBits(data);
		}
	}
}
