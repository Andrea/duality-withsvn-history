using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TestPlugin;
using Duality;

namespace TestPlugin2
{
	[Serializable]
	public class SomeInterfaceComponent : Component, ICmpSomeInterface
	{
		public void Foo()
		{
			Log.Core.Write("Foo");
		}
	}
}
