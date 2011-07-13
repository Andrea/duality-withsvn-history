using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	//[Flags]
	public enum Alignment
	{
		Center			= 0x0,

		Left			= 0x1,
		Right			= 0x2,
		Top				= 0x4,
		Bottom			= 0x8,

		TopLeft			= Top | Left,
		TopRight		= Top | Right,
		BottomLeft		= Bottom | Left,
		BottomRight		= Bottom | Right
	}
}
