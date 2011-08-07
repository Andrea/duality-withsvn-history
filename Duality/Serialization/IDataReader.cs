using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public interface IDataReader
	{
		object ReadValue(string name);
	}
}
