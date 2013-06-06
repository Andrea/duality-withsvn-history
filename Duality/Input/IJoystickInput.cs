using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to extended user input such as joysticks or gamepads.
	/// </summary>
	public interface IJoystickInput
	{
		/// <summary>
		/// [GET] A textual description of this input device.
		/// </summary>
		string Description { get; }
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

		/// <summary>
		/// Fired once when a device button is no longer pressed.
		/// </summary>
		event EventHandler<JoystickButtonEventArgs> ButtonUp;
		/// <summary>
		/// Fired once when a device button is pressed.
		/// </summary>
		event EventHandler<JoystickButtonEventArgs> ButtonDown;
		/// <summary>
		/// Fired whenever a device axis changes its value.
		/// </summary>
		event EventHandler<JoystickMoveEventArgs> Move;
	}
}
