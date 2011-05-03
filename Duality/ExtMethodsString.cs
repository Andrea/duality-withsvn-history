using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	public static class ExtMethodsString
	{
		public static string Multiply(this string source, int times)
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < times; i++)
			{
				builder.Append(source);
			}
			return builder.ToString();
		}
	}
}
