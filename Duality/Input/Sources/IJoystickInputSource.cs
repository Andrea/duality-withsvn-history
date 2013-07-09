﻿using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Describes a source of extended user input such as joysticks or gamepads. This is usually an input device.
	/// </summary>
	public interface IJoystickInputSource
	{
		/// <summary>
		/// [GET] A string containing a unique description for this instance.
		/// </summary>
		string Description { get; }
		/// <summary>
		/// [GET] Returns whether this input is currently available.
		/// </summary>
		bool IsAvailable { get; }
		/// <summary>
		/// [GET] Returns whether the specified device button is currently pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		bool this[JoystickButton button] { get; }
		/// <summary>
		/// [GET] Returns the specified device axis current value.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		float this[JoystickAxis axis] { get; }
	}
}
