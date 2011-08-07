using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public interface IDataWriter
	{
		void WriteValue(string name, object value);
	}
}
