﻿using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Describes a source of user mouse input. This is usually an input device.
	/// </summary>
	public interface IMouseInputSource
	{
		/// <summary>
		/// [GET / SET] The current viewport-local cursor X position.
		/// </summary>
		int X { get; set; }
		/// <summary>
		/// [GET / SET] The current viewport-local cursor Y position.
		/// </summary>
		int Y { get; set; }
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
		/// Fired when the cursor leaves the viewport area.
		/// </summary>
		event EventHandler Leave;
		/// <summary>
		/// Fired when the cursor enters the viewport area.
		/// </summary>
		event EventHandler Enter;
		/// <summary>
		/// Fired when the mouse wheel value changes.
		/// </summary>
		event EventHandler<MouseWheelEventArgs> WheelChanged;
	}
}
