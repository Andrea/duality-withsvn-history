﻿using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to user keyboard input.
	/// </summary>
	public sealed class KeyboardInput : IUserInput
	{
		private class State
		{
			public bool		KeyRepeat		= true;
			public int		KeyRepeatCount	= 0;
			public bool		HasFocus		= false;
			public bool[]	KeyPressed		= new bool[(int)Key.LastKey + 1];

			public State() {}
			public State(State baseState)
			{
				baseState.CopyTo(this);
			}
			public void CopyTo(State other)
			{
				other.KeyRepeat			= this.KeyRepeat;
				other.KeyRepeatCount	= this.KeyRepeatCount;
				other.HasFocus			= this.HasFocus;
				this.KeyPressed.CopyTo(other.KeyPressed, 0);
			}
			public void UpdateFromSource(IKeyboardInputSource source)
			{
				if (source == null) return;
				this.HasFocus = source.HasFocus;
				this.KeyRepeat = source.KeyRepeat;
				this.KeyRepeatCount = source.KeyRepeatCounter;
				for (int i = 0; i < this.KeyPressed.Length; i++)
				{
					this.KeyPressed[i] = source[(Key)i];
				}
			}
		}

		private	IKeyboardInputSource	source			= null;
		private	State					currentState	= new State();
		private	State					lastState		= new State();


		/// <summary>
		/// [GET / SET] The keyboard inputs data source.
		/// </summary>
		public IKeyboardInputSource Source
		{
			get { return this.source; }
			set
			{
				if (this.source != value)
				{
					this.source = value;

					if (this.source != null)
					{
						this.source.KeyRepeat = this.currentState.KeyRepeat;
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
			get { return this.source != null && this.currentState.HasFocus; }
		}
		/// <summary>
		/// [GET / SET] Whether a key that is pressed and hold down should fire the <see cref="KeyDown"/> event repeatedly.
		/// </summary>
		public bool KeyRepeat
		{
			get { return this.currentState.KeyRepeat; }
			set 
			{
				this.currentState.KeyRepeat = value;
				if (this.source != null) this.source.KeyRepeat = this.currentState.KeyRepeat;
			}
		}
		/// <summary>
		/// [GET] Returns whether a specific key is currently pressed.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool this[Key key]
		{
			get { return this.currentState.KeyPressed[(int)key]; }
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
		internal void Update()
		{
			// Memorize last state
			this.currentState.CopyTo(this.lastState);

			// Obtain new state
			this.currentState.UpdateFromSource(this.source);

			// Fire events
			if (this.currentState.HasFocus && !this.lastState.HasFocus)
			{
				if (this.GotFocus != null)
					this.GotFocus(this, EventArgs.Empty);
			}
			if (!this.currentState.HasFocus && this.lastState.HasFocus)
			{
				if (this.LostFocus != null)
					this.LostFocus(this, EventArgs.Empty);
			}
			bool anyKeyDown = false;
			for (int i = 0; i < this.currentState.KeyPressed.Length; i++)
			{
				if (this.currentState.KeyPressed[i] && !this.lastState.KeyPressed[i])
				{
					anyKeyDown = true;
					if (this.KeyDown != null)
						this.KeyDown(this, new KeyboardKeyEventArgs((Key)i));
				}
				if (!this.currentState.KeyPressed[i] && this.lastState.KeyPressed[i])
				{
					if (this.KeyUp != null)
						this.KeyUp(this, new KeyboardKeyEventArgs((Key)i));
				}
			}
			if (!anyKeyDown && this.currentState.KeyRepeatCount != this.lastState.KeyRepeatCount && this.currentState.KeyRepeat)
			{
				for (int i = 0; i < this.currentState.KeyPressed.Length; i++)
				{
					if (this.currentState.KeyPressed[i])
					{
						if (this.KeyDown != null)
							this.KeyDown(this, new KeyboardKeyEventArgs((Key)i));
					}
				}
			}
		}

		/// <summary>
		/// Returns whether the specified key is currently pressed.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool KeyPressed(Key key)
		{
			return this.currentState.KeyPressed[(int)key];
		}
		/// <summary>
		/// Returns whether the specified key was hit this frame.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool KeyHit(Key key)
		{
			return this.currentState.KeyPressed[(int)key] && !this.lastState.KeyPressed[(int)key];
		}
		/// <summary>
		/// Returns whether the specified key was released this frame.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool KeyReleased(Key key)
		{
			return !this.currentState.KeyPressed[(int)key] && this.lastState.KeyPressed[(int)key];
		}
	}
}
