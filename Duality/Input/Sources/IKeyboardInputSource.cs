using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Describes a source of user keyboard input. This is usually an input device.
	/// </summary>
	public interface IKeyboardInputSource
	{
		/// <summary>
		/// [GET / SET] Whether a key that is pressed and hold down should fire the <see cref="KeyDown"/> event repeatedly.
		/// </summary>
		bool KeyRepeat { get; set; }
		/// <summary>
		/// [GET] Returns whether a specific key is currently pressed.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool this[Key key] { get; }

		/// <summary>
		/// Fired when a key is no longer pressed.
		/// </summary>
		event EventHandler<KeyboardKeyEventArgs> KeyUp;
		/// <summary>
		/// Fired once when a key is pressed. May be fired repeatedly, if <see cref="KeyRepeat"/> is true.
		/// </summary>
		event EventHandler<KeyboardKeyEventArgs> KeyDown;
		/// <summary>
		/// Fired when the keyboard input is no longer available to Duality.
		/// </summary>
		event EventHandler LostFocus;
		/// <summary>
		/// Fired when the keyboard input becomes available to Duality.
		/// </summary>
		event EventHandler GotFocus;
	}
}
