using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.VisualStudio.DebuggerVisualizers;

using Duality;
using Duality.ColorFormat;
using Duality.Resources;

[assembly: DebuggerVisualizer(
	typeof(DualityDebugging.BitmapDebuggerVisualizer), 
	typeof(DualityDebugging.TextureAtlasDebuggerVisualizerObjectSource), 
	Target = typeof(Texture), 
	Description = "Texture Atlas Visualizer")]

namespace DualityDebugging
{
	public class TextureAtlasDebuggerVisualizerObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			Texture texture = target as Texture;
			Pixmap.Layer layer = texture.RetrievePixelData();
			Bitmap bitmap = layer.ToBitmap();
			ColorRgba avgColor = bitmap.GetAverageColor();
			ColorRgba atlasColor = avgColor.GetLuminance() < 0.5f ? new ColorRgba(128, 0, 0, 164) : new ColorRgba(255, 128, 128, 164);

			// Draw atlas rects
			if (texture.Atlas != null)
			{
				Pen atlasPen = new Pen(Color.FromArgb(atlasColor.A, atlasColor.R, atlasColor.G, atlasColor.B));
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					foreach (Rect r in texture.Atlas)
					{
						g.DrawRectangle(atlasPen,
							r.X * texture.OglWidth,
							r.Y * texture.OglHeight,
							r.W * texture.OglWidth,
							r.H * texture.OglHeight);
					}
				}
			}

			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(outgoingData, texture.ToString());
			formatter.Serialize(outgoingData, bitmap);
			outgoingData.Flush();
		}
	}
}
