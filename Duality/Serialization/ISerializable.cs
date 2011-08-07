using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality.Serialization
{
	public interface ISerializable
	{
		void WriteData(IDataWriter writer);
		void ReadData(IDataReader reader);
	}
}
