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
	typeof(DualityDebugging.TextureDebuggerVisualizer), 
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

	public class TextureDebuggerVisualizer : DialogDebuggerVisualizer
	{
		protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			Stream incomingData = objectProvider.GetData();
			BinaryFormatter formatter = new BinaryFormatter();
			string name = (string)formatter.Deserialize(incomingData);
			Bitmap pixelData = (Bitmap)formatter.Deserialize(incomingData);
			using (BitmapForm form = new BitmapForm()) {
				form.Text = name;
				form.Bitmap = pixelData;
				windowService.ShowDialog(form);
			}
		}

		public static void TestShow(object objToVisualize)
		{
			var visualizerHost = new VisualizerDevelopmentHost(
				objToVisualize,
				typeof(TextureDebuggerVisualizer),
				typeof(TextureDebuggerVisualizerObjectSource));
			visualizerHost.ShowVisualizer();
		}
	}
}
