using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Duality.Serialization.Surrogates
{
	/// <summary>
	/// De/Serializes a <see cref="System.Drawing.Bitmap"/>.
	/// </summary>
	public class BitmapSurrogate : Surrogate<Bitmap>
	{
		public override void WriteConstructorData(IDataWriter writer)
		{
			writer.WriteValue("width", this.RealObject.Width);
			writer.WriteValue("height", this.RealObject.Height);
		}
		public override void WriteData(IDataWriter writer)
		{
			uint[] data;
			this.RealObject.GetPixelDataIntArgb(out data);

			writer.WriteValue("data", data);
		}

		public override object ConstructObject(IDataReader reader, Type objType)
		{
			int width = reader.ReadValue<int>("width");
			int height = reader.ReadValue<int>("height");

			return new Bitmap(width, height);
		}
		public override void ReadData(IDataReader reader)
		{
			uint[] data = reader.ReadValue<uint[]>("data");

			this.RealObject.SetPixelDataIntArgb(data);
		}
	}
}
