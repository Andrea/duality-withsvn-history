using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Duality.Components
{
	static class GraphicsHelpers
	{
		public static Bitmap GrabScreenshot(IDrawDevice device)
		{
			if (GraphicsContext.CurrentContext == null)
				throw new GraphicsContextMissingException();

			var bmp = new Bitmap((int) device.TargetSize.X, (int)device.TargetSize.Y);
			var data = bmp.LockBits(new Rectangle(0, 0, (int) device.TargetSize.X, (int) device.TargetSize.Y), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			GL.ReadPixels(0, 0, (int) device.TargetSize.X, (int) device.TargetSize.Y, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
			bmp.UnlockBits(data);

//			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return bmp;
		}
	}
}