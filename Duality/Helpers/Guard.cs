using System;
using System.Collections.Generic;
using System.Linq;

namespace Duality.Helpers
{
	public static class Guard
	{
		public static void NotNull(object value, string message = null)
		{
			NullException(value, message, new ArgumentNullException());
		}

		private static void NullException(object value, string message, Exception exception)
		{
			if (value != null)
				return;
			Log.Core.WriteError(message ?? "Argument was null {0}", Environment.StackTrace);
			throw exception;
		}

		public static void NotNullComponent(object value, string message = null)
		{
			NullException(value, message, new InvalidOperationException(message ?? "Missing component on script"));
		}

		public static void StringNotNullEmpty(string value, string argument = null)
		{
			var condition = string.IsNullOrWhiteSpace(value);
			if (!condition)
				return;
			Log.Core.WriteError("Argument {1} was null {0}", Environment.StackTrace, argument);
			throw new ArgumentException("String was null or empty");
			//Debug.Assert(!condition);
		}

		public static void NotEmpty(object value, string argument = null)
		{
			var condition = value == null || !((IEnumerable<object>)value).Any();
			if (!condition)
				return;
			Log.Core.WriteError("The collection was empty {0}", Environment.StackTrace);
			throw new ArgumentException("collection was null or empty");

			//Debug.Assert(!condition);
		}
	}
}