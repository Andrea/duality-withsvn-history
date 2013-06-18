using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to user keyboard input.
	/// </summary>
	public sealed class KeyboardInput : IUserInput
	{
		private	IKeyboardInputSource	realInput	= null;
		private bool					keyRepeat	= false;
		private	bool					gotFocus	= false;


		internal IKeyboardInputSource Source
		{
			get { return this.realInput; }
			set
			{
				if (this.realInput != value)
				{
					if (this.realInput != null)
					{
						this.realInput.KeyUp		-= this.realInput_KeyUp;
						this.realInput.KeyDown		-= this.realInput_KeyDown;
						this.realInput.LostFocus	-= this.realInput_LostFocus;
						this.realInput.GotFocus		-= this.realInput_GotFocus;
					}

					this.realInput = value;

					if (this.realInput != null)
					{
						this.realInput.KeyUp		+= this.realInput_KeyUp;
						this.realInput.KeyDown		+= this.realInput_KeyDown;
						this.realInput.LostFocus	+= this.realInput_LostFocus;
						this.realInput.GotFocus		+= this.realInput_GotFocus;
						this.realInput.KeyRepeat = this.keyRepeat;
					}
				}
			}
		}
		/// <summary>
		/// [GET] A text description of this input.
		/// </summary>
		public string Description
		{
			get { return "Keyboard"; }
		}
		/// <summary>
		/// [GET] Returns whether this input is currently available.
		/// </summary>
		public bool IsAvailable
		{
			get { return this.realInput != null && this.gotFocus; }
		}
		/// <summary>
		/// [GET / SET] Whether a key that is pressed and hold down should fire the <see cref="KeyDown"/> event repeatedly.
		/// </summary>
		public bool KeyRepeat
		{
			get { return this.keyRepeat; }
			set 
			{
				this.keyRepeat = value;
				if (this.realInput != null) this.realInput.KeyRepeat = this.keyRepeat;
			}
		}
		/// <summary>
		/// [GET] Returns whether a specific key is currently pressed.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool this[Key key]
		{
			get { return this.realInput != null ? this.realInput[key] : false; }
		}

		/// <summary>
		/// Fired when a key is no longer pressed.
		/// </summary>
		public event EventHandler<KeyboardKeyEventArgs> KeyUp;
		/// <summary>
		/// Fired once when a key is pressed. May be fired repeatedly, if <see cref="KeyRepeat"/> is true.
		/// </summary>
		public event EventHandler<KeyboardKeyEventArgs> KeyDown;
		/// <summary>
		/// Fired when keyboard input is no longer available to Duality.
		/// </summary>
		public event EventHandler LostFocus;
		/// <summary>
		/// Fired when keyboard input becomes available to Duality.
		/// </summary>
		public event EventHandler GotFocus;
		

		internal KeyboardInput() {}

		private void realInput_KeyUp(object sender, KeyboardKeyEventArgs e)
		{
			if (this.KeyUp != null)
				this.KeyUp(this, e);
		}
		private void realInput_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (this.KeyDown != null)
				this.KeyDown(this, e);
		}
		private void realInput_LostFocus(object sender, EventArgs e)
		{
			this.gotFocus = false;
			if (this.LostFocus != null)
				this.LostFocus(this, e);
		}
		private void realInput_GotFocus(object sender, EventArgs e)
		{
			this.gotFocus = true;
			if (this.GotFocus != null)
				this.GotFocus(this, e);
		}
	}
}
