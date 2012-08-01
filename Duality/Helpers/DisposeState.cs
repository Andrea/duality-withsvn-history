using System;
using OpenTK;

namespace Duality
{
	/// <summary>
	/// Describes the state of object disposal.
	/// </summary>
	public enum DisposeState
	{
		Active,
		Disposing,
		Disposed
	}
}
