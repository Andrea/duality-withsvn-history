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

	public static class IColorDataCreator
	{
		public static IColorData FromIntRgba(uint rgba, Type colorDataType)
		{
			if (colorDataType == typeof(ColorRGBA))
				return ColorRGBA.FromIntRgba(rgba);
			else if (colorDataType == typeof(ColorHSVA))
				return ColorHSVA.FromIntRgba(rgba);
			else
			{
				System.Reflection.MethodInfo createMethod = colorDataType.GetMethod("FromIntRgba", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
				if (createMethod != null)
					return createMethod.Invoke(null, new object[] { rgba }) as IColorData;
				else
					return null;
			}
		}
		public static T FromIntRgba<T>(uint rgba) where T : IColorData
		{
			return (T)IColorDataCreator.FromIntRgba(rgba, typeof(T));
		}

		public static IColorData FromIntArgb(uint argb, Type colorDataType)
		{
			if (colorDataType == typeof(ColorRGBA))
				return ColorRGBA.FromIntArgb(argb);
			else if (colorDataType == typeof(ColorHSVA))
				return ColorHSVA.FromIntArgb(argb);
			else
			{
				System.Reflection.MethodInfo createMethod = colorDataType.GetMethod("FromIntArgb", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
				if (createMethod != null)
					return createMethod.Invoke(null, new object[] { argb }) as IColorData;
				else
					return null;
			}
		}
		public static T FromIntArgb<T>(uint argb) where T : IColorData
		{
			return (T)IColorDataCreator.FromIntArgb(argb, typeof(T));
		}
	}
}
