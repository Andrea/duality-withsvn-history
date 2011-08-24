using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality.ColorFormat
{
	/// <summary>
	/// A general interface for different types of color data.
	/// </summary>
	public interface IColorData
	{
		/// <summary>
		/// Converts the color to a <see cref="System.UInt32"/>-Rgba value.
		/// </summary>
		/// <returns></returns>
		int ToIntRgba();
		/// <summary>
		/// Sets the color base ond a <see cref="System.UInt32"/>-Rgba value.
		/// </summary>
		/// <param name="rgba"></param>
		void SetIntRgba(int rgba);
		
		/// <summary>
		/// Converts the color to a <see cref="System.UInt32"/>-Argb value.
		/// </summary>
		/// <returns></returns>
		int ToIntArgb();
		/// <summary>
		/// Sets the color base ond a <see cref="System.UInt32"/>-Argb value.
		/// </summary>
		/// <param name="rgba"></param>
		void SetIntArgb(int argb);

		/// <summary>
		/// Converts the color to the specified format.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T ConvertTo<T>() where T : IColorData;
	}
}
