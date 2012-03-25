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
	typeof(DualityDebugging.PixmapDebuggerVisualizerObjectSource), 
	Target = typeof(Pixmap), 
	Description = "Pixmap Visualizer")]

namespace DualityDebugging
{
	public class PixmapDebuggerVisualizerObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			Pixmap pixmap = target as Pixmap;
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(outgoingData, pixmap.ToString());
			formatter.Serialize(outgoingData, pixmap.PixelData);
			outgoingData.Flush();
		}
	}
}
