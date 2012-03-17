using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

using Duality;
using Duality.Resources;

namespace DualityEditor.CorePluginInterface
{
	public enum PreviewSizeMode
	{
		FixedNone,
		FixedWidth,
		FixedHeight,
		FixedBoth
	}

	public static class PreviewProvider
	{
		public static Bitmap GetPreviewImage(this Resource res, int desiredWidth, int desiredHeight, PreviewSizeMode mode = PreviewSizeMode.FixedNone)
		{
			Bitmap result = null;
			
			System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
			w.Restart();

			ConvertOperation convert = new ConvertOperation(new[] { res }, ConvertOperation.Operation.Convert);
			if (convert.CanPerform<Pixmap>())
			{
			    Pixmap pixmap = convert.Perform<Pixmap>().FirstOrDefault();
				if (pixmap != null) result = pixmap.PixelData;
				if (result != null)
				{
					float widthRatio = (float)result.Width / (float)result.Height;
					if (pixmap.Width * pixmap.Height > 4096 * 4096)
					{
						result = result.Clone(new Rectangle(
							pixmap.Width / 2 - Math.Min(desiredWidth, pixmap.Width) / 2,
							pixmap.Height / 2 - Math.Min(desiredHeight, pixmap.Height) / 2,
							Math.Min(desiredWidth, pixmap.Width),
							Math.Min(desiredHeight, pixmap.Height)), 
							System.Drawing.Imaging.PixelFormat.DontCare);
						if (result.Width != desiredWidth || result.Height != desiredHeight)
							result = result.Rescale(desiredWidth, desiredHeight, InterpolationMode.HighQualityBicubic);
					}
					else if (mode == PreviewSizeMode.FixedBoth)
						result = result.Rescale(desiredWidth, desiredHeight, InterpolationMode.HighQualityBicubic);
					else if (mode == PreviewSizeMode.FixedWidth)
						result = result.Rescale(desiredWidth, MathF.RoundToInt(desiredWidth / widthRatio), InterpolationMode.HighQualityBicubic);
					else if (mode == PreviewSizeMode.FixedHeight)
						result = result.Rescale(MathF.RoundToInt(widthRatio * desiredHeight), desiredHeight, InterpolationMode.HighQualityBicubic);
					else
						result = result.Clone() as Bitmap;
				}
			}
			else if (convert.CanPerform<AudioData>())
			{
			    AudioData audio = convert.Perform<AudioData>().FirstOrDefault();
				int oggHash = audio.OggVorbisData.GetCombinedHashCode();
				int oggLen = audio.OggVorbisData.Length;
				Duality.OggVorbis.PcmData pcm = Duality.OggVorbis.OV.LoadFromMemory(audio.OggVorbisData, 1000000); //41236992
				short[] sdata = new short[pcm.data.Length / 2];
				Buffer.BlockCopy(pcm.data, 0, sdata, 0, pcm.data.Length);

				result = new Bitmap(desiredWidth, desiredHeight);
				int channelLength = sdata.Length / pcm.channelCount;
				int yMid = result.Height / 2;
				int stepWidth = (channelLength / (2 * result.Width)) - 1;
				int samples = 10;
				using (Graphics g = Graphics.FromImage(result))
				{
					Color baseColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
						(float)(oggHash % 90) * (float)(oggLen % 4) / 360.0f, 
						0.65f, 
						1f);
					Pen linePen = new Pen(Color.FromArgb(MathF.RoundToInt(255.0f / MathF.Pow((float)samples, 0.65f)), baseColor));
					g.Clear(Color.Transparent);
					for (int x = 0; x < result.Width; x++)
					{
						float timePercentage = (float)x / (float)result.Width;
						int i = MathF.RoundToInt(timePercentage * channelLength);
						float left;
						float right;
						short channel1;
						short channel2;

						for (int s = 0; s <= samples; s++)
						{
							int offset = MathF.RoundToInt((float)stepWidth * (float)s / (float)samples);
							channel1 = sdata[(i + offset) * pcm.channelCount + 0];
							channel2 = sdata[(i + offset) * pcm.channelCount + 1];
							left = (float)Math.Abs(channel1) / (float)short.MaxValue;
							right = (float)Math.Abs(channel2) / (float)short.MaxValue;
							g.DrawLine(linePen, x, yMid, x, yMid + MathF.RoundToInt(left * yMid));
							g.DrawLine(linePen, x, yMid, x, yMid - MathF.RoundToInt(right * yMid));
						}
					}
				}
			}

			//Log.Editor.Write("Generating preview for {0} took {1} ms", res, w.ElapsedMilliseconds);

			return result;
		}
	}
}
