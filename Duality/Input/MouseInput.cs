using System;
using OpenTK.Input;

namespace Duality
{
	/// <summary>
	/// Provides access to user mouse input.
	/// </summary>
	public sealed class MouseInput : IUserInput
	{
		private	IMouseInputSource	realInput			= null;
		private	bool				cursorInViewport	= false;


		internal IMouseInputSource Source
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
						this.realInput.Leave		-= this.realInput_Leave;
						this.realInput.Enter		-= this.realInput_Enter;
						this.realInput.WheelChanged -= this.realInput_WheelChanged;
					}

					this.realInput = value;

					if (this.realInput != null)
					{
						this.realInput.ButtonUp		+= this.realInput_ButtonUp;
						this.realInput.ButtonDown	+= this.realInput_ButtonDown;
						this.realInput.Move			+= this.realInput_Move;
						this.realInput.Leave		+= this.realInput_Leave;
						this.realInput.Enter		+= this.realInput_Enter;
						this.realInput.WheelChanged += this.realInput_WheelChanged;
					}
				}
			}
		}
		/// <summary>
		/// [GET] A text description of this input.
		/// </summary>
		public string Description
		{
			get { return "Mouse"; }
		}
		/// <summary>
		/// [GET] Returns whether this input is currently available.
		/// </summary>
		public bool IsAvailable
		{
			get { return this.realInput != null && this.cursorInViewport; }
		}
		/// <summary>
		/// [GET / SET] The current viewport-local cursor X position.
		/// </summary>
		public int X
		{
			get { return this.realInput != null ? this.realInput.X : 0; }
			set { if (this.realInput != null) this.realInput.X = value; }
		}
		/// <summary>
		/// [GET / SET] The current viewport-local cursor Y position.
		/// </summary>
		public int Y
		{
			get { return this.realInput != null ? this.realInput.Y : 0; }
			set { if (this.realInput != null) this.realInput.Y = value; }
		}
		/// <summary>
		/// [GET] The current mouse wheel value
		/// </summary>
		public int Wheel
		{
			get { return this.realInput != null ? this.realInput.Wheel : 0; }
		}
		/// <summary>
		/// [GET] Returns whether a specific <see cref="MouseButton"/> is currently pressed.
		/// </summary>
		/// <param name="btn"></param>
		/// <returns></returns>
		public bool this[MouseButton btn]
		{
			get { return this.realInput != null ? this.realInput[btn] : false; }
		}

		/// <summary>
		/// Fired when a <see cref="MouseButton"/> is no longer pressed.
		/// </summary>
		public event EventHandler<MouseButtonEventArgs> ButtonUp;
		/// <summary>
		/// Fired once when a <see cref="MouseButton"/> is pressed.
		/// </summary>
		public event EventHandler<MouseButtonEventArgs> ButtonDown;
		/// <summary>
		/// Fired when the cursor moves.
		/// </summary>
		public event EventHandler<MouseMoveEventArgs> Move;
		/// <summary>
		/// Fired when the cursor leaves the viewport area.
		/// </summary>
		public event EventHandler Leave;
		/// <summary>
		/// Fired when the cursor enters the viewport area.
		/// </summary>
		public event EventHandler Enter;
		/// <summary>
		/// Fired when the mouse wheel value changes.
		/// </summary>
		public event EventHandler<MouseWheelEventArgs> WheelChanged;

		
		internal MouseInput() {}

		private void realInput_ButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this.ButtonUp != null)
				this.ButtonUp(this, e);
		}
		private void realInput_ButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.ButtonDown != null)
				this.ButtonDown(this, e);
		}
		private void realInput_Move(object sender, MouseMoveEventArgs e)
		{
			if (!this.cursorInViewport) return;
			if (this.Move != null)
				this.Move(this, e);
		}
		private void realInput_Leave(object sender, EventArgs e)
		{
			this.cursorInViewport = false;
			if (this.Leave != null)
				this.Leave(this, e);
		}
		private void realInput_Enter(object sender, EventArgs e)
		{
			this.cursorInViewport = true;
			if (this.Enter != null)
				this.Enter(this, e);
		}
		private void realInput_WheelChanged(object sender, MouseWheelEventArgs e)
		{
			if (this.WheelChanged != null)
				this.WheelChanged(this, e);
		}
	}
}
