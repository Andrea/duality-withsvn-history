using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to extended user input such as joysticks or gamepads.
	/// </summary>
	public sealed class JoystickInput : IUserInput
	{
		private	IJoystickInputSource	realInput	= null;
		private	string					description	= null;


		internal IJoystickInputSource Source
		{
			get { return this.realInput; }
			set
			{
				if (this.realInput != value)
				{
					if (this.realInput != null)
					{
						this.realInput.ButtonUp		-= this.realInput_ButtonUp;
						this.realInput.ButtonDown	-= this.realInput_ButtonDown;
						this.realInput.Move			-= this.realInput_Move;
					}

					this.realInput = value;

					if (this.realInput != null)
					{
						this.description = this.realInput.Description;
						this.realInput.ButtonUp		+= this.realInput_ButtonUp;
						this.realInput.ButtonDown	+= this.realInput_ButtonDown;
						this.realInput.Move			+= this.realInput_Move;
					}
				}
			}
		}
		/// <summary>
		/// [GET] A text description of this input.
		/// </summary>
		public string Description
		{
			get { return this.description; }
		}
		/// <summary>
		/// [GET] Returns whether this input is currently available.
		/// </summary>
		public bool IsAvailable
		{
			get { return this.realInput != null && this.realInput.IsAvailable; }
		}
		/// <summary>
		/// [GET] Returns whether the specified device button is currently pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool this[JoystickButton button]
		{
			get { return this.realInput[button]; }
		}
		/// <summary>
		/// [GET] Returns the specified device axis current value.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		public float this[JoystickAxis axis]
		{
			get { return this.realInput[axis]; }
		}

		/// <summary>
		/// Fired once when a device button is no longer pressed.
		/// </summary>
		public event EventHandler<JoystickButtonEventArgs> ButtonUp;
		/// <summary>
		/// Fired once when a device button is pressed.
		/// </summary>
		public event EventHandler<JoystickButtonEventArgs> ButtonDown;
		/// <summary>
		/// Fired whenever a device axis changes its value.
		/// </summary>
		public event EventHandler<JoystickMoveEventArgs> Move;
		

		internal JoystickInput() {}

		private void realInput_ButtonUp(object sender, JoystickButtonEventArgs e)
		{
			if (this.ButtonUp != null)
				this.ButtonUp(this, e);
		}
		private void realInput_ButtonDown(object sender, JoystickButtonEventArgs e)
		{
			if (this.ButtonDown != null)
				this.ButtonDown(this, e);
		}
		private void realInput_Move(object sender, JoystickMoveEventArgs e)
		{
			if (this.Move != null)
				this.Move(this, e);
		}
	}
}
