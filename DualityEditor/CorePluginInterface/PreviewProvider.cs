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
					if (mode == PreviewSizeMode.FixedBoth)
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
				Duality.OggVorbis.PcmData pcm = Duality.OggVorbis.OV.LoadFromMemory(audio.OggVorbisData);
				short[] sdata = new short[pcm.data.Length / 2];
				Buffer.BlockCopy(pcm.data, 0, sdata, 0, pcm.data.Length);
				
				short[] channel1 = null;
				short[] channel2 = null;
				if (pcm.channelCount == 1)
				{
					channel1 = sdata;
					channel2 = sdata;
				}
				else
				{
					channel1 = new short[sdata.Length / 2];
					channel2 = new short[sdata.Length / 2];
					for (int i = 0, j = 0; i < sdata.Length; i+=2, ++j)
					{
						channel1[j] = sdata[i];
						channel2[j] = sdata[i+1];
					}
				}

				result = new Bitmap(desiredWidth, desiredHeight);
				int yMid = result.Height / 2;
				int stepWidth = (channel1.Length / (2 * result.Width)) - 1;
				int samples = 10;
				using (Graphics g = Graphics.FromImage(result))
				{
					Pen linePen = new Pen(Color.FromArgb(MathF.RoundToInt(255.0f / MathF.Pow((float)samples, 0.75f)), Color.GreenYellow));
					g.Clear(Color.Transparent);
					for (int x = 0; x < result.Width; x++)
					{
						float timePercentage = (float)x / (float)result.Width;
						int i = MathF.RoundToInt(timePercentage * channel1.Length);
						float left;
						float right;

						for (int s = 0; s <= samples; s++)
						{
							int offset = MathF.RoundToInt((float)stepWidth * (float)s / (float)samples);
							left = (float)Math.Abs(channel1[i + offset]) / (float)short.MaxValue;
							right = (float)Math.Abs(channel2[i + offset]) / (float)short.MaxValue;
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
