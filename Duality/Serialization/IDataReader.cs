﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	/// <summary>
	/// Provides reading capabilities for serialization purposes
	/// </summary>
	public interface IDataReader
	{
		/// <summary>
		/// Reads the value that is associated with the specified name.
		/// </summary>
		/// <param name="name">The name that is used for retrieving the value.</param>
		/// <returns>The value that has been read using the given name.</returns>
		/// <seealso cref="ReadValue{T}(string)"/>
		/// <seealso cref="ReadValue{T}(string, out T)"/>
		object ReadValue(string name);
		/// <summary>
		/// Reads the value that is associated with the specified name.
		/// </summary>
		/// <typeparam name="T">The expected value type.</typeparam>
		/// <param name="name">The name that is used for retrieving the value.</param>
		/// <returns>The value that has been read and cast using the given name and type.</returns>
		/// <seealso cref="ReadValue(string)"/>
		/// <seealso cref="ReadValue{T}(string, out T)"/>
		T ReadValue<T>(string name);
		/// <summary>
		/// Reads the value that is associated with the specified name.
		/// </summary>
		/// <typeparam name="T">The expected value type.</typeparam>
		/// <param name="name">The name that is used for retrieving the value.</param>
		/// <param name="value">The value that has been read and cast using the given name and type.</param>
		/// <seealso cref="ReadValue(string)"/>
		/// <seealso cref="ReadValue{T}(string)"/>
		void ReadValue<T>(string name, out T value);
	}
}