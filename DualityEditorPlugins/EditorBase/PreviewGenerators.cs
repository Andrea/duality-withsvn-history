using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using OpenTK;

using Duality;
using Duality.Resources;
using Font = Duality.Resources.Font;

using DualityEditor;
using DualityEditor.CorePluginInterface;


namespace EditorBase.PreviewGenerators
{
	public class PixmapPreviewGenerator : PreviewGenerator<Pixmap>
	{
		public override Bitmap Perform(Pixmap pixmap, PreviewSettings settings)
		{
			int desiredWidth = settings.DesiredWidth;
			int desiredHeight = settings.DesiredHeight;

			Pixmap.Layer layer = pixmap.MainLayer;
			float widthRatio = (float)layer.Width / (float)layer.Height;

			if (pixmap.Width * pixmap.Height > 4096 * 4096)
			{
				layer = layer.CloneSubImage(
					pixmap.Width / 2 - Math.Min(desiredWidth, pixmap.Width) / 2,
					pixmap.Height / 2 - Math.Min(desiredHeight, pixmap.Height) / 2,
					Math.Min(desiredWidth, pixmap.Width),
					Math.Min(desiredHeight, pixmap.Height));
				if (layer.Width != desiredWidth || layer.Height != desiredHeight)
					layer = layer.CloneRescale(desiredWidth, desiredHeight, Pixmap.FilterMethod.Linear);
			}
			else if (settings.SizeMode == PreviewSizeMode.FixedBoth)
				layer = layer.CloneRescale(desiredWidth, desiredHeight, Pixmap.FilterMethod.Linear);
			else if (settings.SizeMode == PreviewSizeMode.FixedWidth)
				layer = layer.CloneRescale(desiredWidth, MathF.RoundToInt(desiredWidth / widthRatio), Pixmap.FilterMethod.Linear);
			else if (settings.SizeMode == PreviewSizeMode.FixedHeight)
				layer = layer.CloneRescale(MathF.RoundToInt(widthRatio * desiredHeight), desiredHeight, Pixmap.FilterMethod.Linear);
			else
				layer = layer.Clone();

			return layer.ToBitmap();
		}
		public override bool CanPerformOn(Pixmap obj, PreviewSettings settings)
		{
			return true;
		}
	}
	public class AudioDataPreviewGenerator : PreviewGenerator<AudioData>
	{
		public override Bitmap Perform(AudioData audio, PreviewSettings settings)
		{
			int desiredWidth = settings.DesiredWidth;
			int desiredHeight = settings.DesiredHeight;
			int oggHash = audio.OggVorbisData.GetCombinedHashCode();
			int oggLen = audio.OggVorbisData.Length;
			Duality.OggVorbis.PcmData pcm = Duality.OggVorbis.OV.LoadFromMemory(audio.OggVorbisData, 1000000); //41236992
			short[] sdata = new short[pcm.data.Length / 2];
			Buffer.BlockCopy(pcm.data, 0, sdata, 0, pcm.data.Length);

			Bitmap result = new Bitmap(desiredWidth, desiredHeight);
			int channelLength = sdata.Length / pcm.channelCount;
			int yMid = result.Height / 2;
			int stepWidth = (channelLength / (2 * result.Width)) - 1;
			const int samples = 10;
			using (Graphics g = Graphics.FromImage(result))
			{
				Color baseColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
					(float)(oggHash % 90) * (float)(oggLen % 4) / 360.0f, 
					0.5f, 
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
						left = (float)Math.Abs((int)channel1) / (float)short.MaxValue;
						right = (float)Math.Abs((int)channel2) / (float)short.MaxValue;
						g.DrawLine(linePen, x, yMid, x, yMid + MathF.RoundToInt(left * yMid));
						g.DrawLine(linePen, x, yMid, x, yMid - MathF.RoundToInt(right * yMid));
					}
				}
			}

			return result;
		}
		public override bool CanPerformOn(AudioData obj, PreviewSettings settings)
		{
			return true;
		}
	}
	public class FontPreviewGenerator : PreviewGenerator<Font>
	{
		public override Bitmap Perform(Font font, PreviewSettings settings)
		{
			int desiredWidth = settings.DesiredWidth;
			int desiredHeight = settings.DesiredHeight;
			const string text = "/acThe quick brown fox jumps over the lazy dog.";
			FormattedText formatText = new FormattedText();
			formatText.MaxWidth = Math.Max(1, desiredWidth - 10);
			formatText.MaxHeight = Math.Max(1, desiredHeight - 10);
			formatText.WordWrap = FormattedText.WrapMode.Word;
			formatText.Fonts = new[] { new ContentRef<Font>(font) };
			formatText.ApplySource(text);
			FormattedText.Metrics metrics = formatText.Measure();
			Vector2 textSize = metrics.Size;
			Pixmap.Layer textLayer = new Pixmap.Layer(desiredWidth, MathF.RoundToInt(textSize.Y));
			formatText.RenderToBitmap(text, textLayer, 5, 0);

			textLayer.Resize(desiredWidth, desiredHeight, Alignment.Left);
			return textLayer.ToBitmap();
		}
		public override bool CanPerformOn(Font obj, PreviewSettings settings)
		{
			return true;
		}
	}
}
