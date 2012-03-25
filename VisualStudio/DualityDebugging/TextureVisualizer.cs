using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.DebuggerVisualizers;

using Duality;
using Duality.Resources;

[assembly: DebuggerVisualizer(
	typeof(DualityDebugging.BitmapDebuggerVisualizer), 
	typeof(DualityDebugging.TextureDebuggerVisualizerObjectSource), 
	Target = typeof(Texture), 
	Description = "Texture Visualizer")]

namespace DualityDebugging
{
	public class TextureDebuggerVisualizerObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			Texture texture = target as Texture;
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(outgoingData, texture.ToString());
			formatter.Serialize(outgoingData, texture.RetrievePixelData());
			outgoingData.Flush();
		}
	}
}
