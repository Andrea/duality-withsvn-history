using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality.ColorFormat
{
	public interface IColorData
	{
		uint ToIntRgba();
		void SetIntRgba(uint rgba);

		uint ToIntArgb();
		void SetIntArgb(uint argb);
	}
}
