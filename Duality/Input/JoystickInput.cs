﻿using System;
using OpenTK.Input;


namespace Duality
{

	/// <summary>
	/// Provides access to extended user input such as joysticks or gamepads.
	/// </summary>
	public sealed class JoystickInput : IUserInput
	{
		private class State
		{
			public float[] AxisValue = new float[(int)JoystickAxis.Axis9 + 1];
			public bool[] ButtonPressed = new bool[(int)JoystickButton.Button15 + 1];

			public State() { }

			public State(State baseState)
			{
				baseState.CopyTo(this);
			}
			public void CopyTo(State other)
			{
				this.AxisValue.CopyTo(other.AxisValue, 0);
				this.ButtonPressed.CopyTo(other.ButtonPressed, 0);
			}

			public void UpdateFromSource(IJoystickInputSource source)
			{
				if (source == null)
					return;
				for (int i = 0; i < ButtonPressed.Length; i++)
				{
					if (i < source.ButtonCount)
						ButtonPressed[i] = source[(JoystickButton)i];
				}
				for (int i = 0; i < AxisValue.Length; i++)
				{
					if (i < source.AxisCount)
						AxisValue[i] = source[(JoystickAxis)i];
				}
			}
		}

		private IJoystickInputSource source = null;
		private State currentState = new State();
		private State lastState = new State();
		private string description = null;
		private bool isDummy = false;


		/// <summary>
		/// [GET / SET] The extended user inputs data source.
		/// </summary>
		public IJoystickInputSource Source
		{
			get { return this.source; }
			set
			{
				if (this.source != value && !this.isDummy)
				{
					this.source = value;
					if (this.source != null)
					{
						this.description = this.source.Description;
					}
				}
			}
		}
		/// <summary>
		/// [GET] A string containing a unique description for this instance.
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
			get { return this.source != null && this.source.IsAvailable; }
		}
		/// <summary>
		/// [GET] Returns whether the specified device button is currently pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool this[JoystickButton button]
		{
			get { return this.currentState.ButtonPressed[(int)button]; }
		}
		/// <summary>
		/// [GET] Returns the specified device axis current value.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		public float this[JoystickAxis axis]
		{
			get { return this.currentState.AxisValue[(int)axis]; }
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


		internal JoystickInput(bool dummy = false)
		{
			this.isDummy = dummy;
		}
		internal void Update()
		{
			// Memorize last state
			this.currentState.CopyTo(this.lastState);

			// Obtain new state
			this.currentState.UpdateFromSource(this.source);

			// Fire events
			for (int i = 0; i < this.currentState.ButtonPressed.Length; i++)
			{
				if (this.currentState.ButtonPressed[i] && !this.lastState.ButtonPressed[i])
				{
					if (this.ButtonDown != null)
					{
						this.ButtonDown(this, new JoystickButtonEventArgs(
							(JoystickButton)i,
							this.currentState.ButtonPressed[i]));
					}
				}
				if (!this.currentState.ButtonPressed[i] && this.lastState.ButtonPressed[i])
				{
					if (this.ButtonUp != null)
					{
						this.ButtonUp(this, new JoystickButtonEventArgs(
							(JoystickButton)i,
							this.currentState.ButtonPressed[i]));
					}
				}
			}
			for (int i = 0; i < this.currentState.AxisValue.Length; i++)
			{
				if (this.currentState.AxisValue[i] != this.lastState.AxisValue[i])
				{
					if (this.Move != null)
					{
						this.Move(this, new JoystickMoveEventArgs(
							(JoystickAxis)i,
							this.currentState.AxisValue[i],
							this.currentState.AxisValue[i] - this.lastState.AxisValue[i]));
					}
				}
			}
		}

		/// <summary>
		/// Returns whether the specified button is currently pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool ButtonPressed(JoystickButton button)
		{
			return this.currentState.ButtonPressed[(int)button];
		}
		/// <summary>
		/// Returns whether the specified button was hit this frame.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool ButtonHit(JoystickButton button)
		{
			return this.currentState.ButtonPressed[(int)button] && !this.lastState.ButtonPressed[(int)button];
		}
		/// <summary>
		/// Returns whether the specified button was released this frame.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool ButtonReleased(JoystickButton button)
		{
			return !this.currentState.ButtonPressed[(int)button] && this.lastState.ButtonPressed[(int)button];
		}

		/// <summary>
		/// Returns the specified axis value.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		public float AxisValue(JoystickAxis axis)
		{
			return this.currentState.AxisValue[(int)axis];
		}
		/// <summary>
		/// Returns the specified axis value change since last frame.
		/// </summary>
		/// <param name="axis"></param>
		/// <returns></returns>
		public float AxisSpeed(JoystickAxis axis)
		{
			return this.currentState.AxisValue[(int)axis] - this.lastState.AxisValue[(int)axis];
		}
	}
}
