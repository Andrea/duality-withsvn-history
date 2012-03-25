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
	typeof(DualityDebugging.PixmapDebuggerVisualizer), 
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
			formatter.Serialize(outgoingData, pixmap);
			outgoingData.Flush();
		}
	}

	public class PixmapDebuggerVisualizer : DialogDebuggerVisualizer
	{
		protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			Stream incomingData = objectProvider.GetData();
			BinaryFormatter formatter = new BinaryFormatter();
			string name = (string)formatter.Deserialize(incomingData);
			Pixmap pixmap = (Pixmap)formatter.Deserialize(incomingData);
			using (BitmapForm form = new BitmapForm()) {
				form.Text = name;
				form.Bitmap = pixmap.PixelData;
				windowService.ShowDialog(form);
			}
		}

		public static void TestShow(object objToVisualize)
		{
			var visualizerHost = new VisualizerDevelopmentHost(
				objToVisualize,
				typeof(PixmapDebuggerVisualizer),
				typeof(PixmapDebuggerVisualizerObjectSource));
			visualizerHost.ShowVisualizer();
		}
	}
}
