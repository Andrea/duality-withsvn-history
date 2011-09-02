using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to user keyboard input
	/// </summary>
	public interface IKeyboardInput
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
	}
}
