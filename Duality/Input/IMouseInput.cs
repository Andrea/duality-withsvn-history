using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to user mouse input
	/// </summary>
	public interface IMouseInput
	{
		/// <summary>
		/// [GET] The current viewport-local cursor X position.
		/// </summary>
		int X { get; }
		/// <summary>
		/// [GET] The current viewport-local cursor Y position.
		/// </summary>
		int Y { get; }
		/// <summary>
		/// [GET] The current mouse wheel value
		/// </summary>
		int Wheel { get; }
		/// <summary>
		/// [GET] Returns whether a specific <see cref="MouseButton"/> is currently pressed.
		/// </summary>
		bool this[MouseButton btn] { get; }

		/// <summary>
		/// Fired when a <see cref="MouseButton"/> is no longer pressed.
		/// </summary>
		event EventHandler<MouseButtonEventArgs> ButtonUp;
		/// <summary>
		/// Fired once when a <see cref="MouseButton"/> is pressed.
		/// </summary>
		event EventHandler<MouseButtonEventArgs> ButtonDown;
		/// <summary>
		/// Fired when the cursor moves.
		/// </summary>
		event EventHandler<MouseMoveEventArgs> Move;
		/// <summary>
		/// Fired when the mouse wheel value changes.
		/// </summary>
		event EventHandler<MouseWheelEventArgs> WheelChanged;
	}
}
