using System;
using OpenTK.Input;

namespace Duality
{
	internal class StaticJoystickInput : IJoystickInput
	{
		private	IJoystickInput	realInput;
		private	EventHandler<JoystickButtonEventArgs>	pipeButtonUp;
		private	EventHandler<JoystickButtonEventArgs>	pipeButtonDown;
		private	EventHandler<JoystickMoveEventArgs>		pipeMove;


		public IJoystickInput RealInput
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
						this.realInput.ButtonUp		+= this.realInput_ButtonUp;
						this.realInput.ButtonDown	+= this.realInput_ButtonDown;
						this.realInput.Move			+= this.realInput_Move;
					}
				}
			}
		}
		public string Description
		{
			get { return this.realInput.Description; }
		}
		public bool this[JoystickButton button]
		{
			get { return this.realInput[button]; }
		}
		public float this[JoystickAxis axis]
		{
			get { return this.realInput[axis]; }
		}

		public event EventHandler<JoystickButtonEventArgs> ButtonUp
		{
			add { this.pipeButtonUp += value; }
			remove { this.pipeButtonUp -= value; }
		}
		public event EventHandler<JoystickButtonEventArgs> ButtonDown
		{
			add { this.pipeButtonDown += value; }
			remove { this.pipeButtonDown -= value; }
		}
		public event EventHandler<JoystickMoveEventArgs> Move
		{
			add { this.pipeMove += value; }
			remove { this.pipeMove -= value; }
		}
		

		private void realInput_ButtonUp(object sender, JoystickButtonEventArgs e)
		{
			if (this.pipeButtonUp != null)
				this.pipeButtonUp(sender, e);
		}
		private void realInput_ButtonDown(object sender, JoystickButtonEventArgs e)
		{
			if (this.pipeButtonDown != null)
				this.pipeButtonDown(sender, e);
		}
		private void realInput_Move(object sender, JoystickMoveEventArgs e)
		{
			if (this.pipeMove != null)
				this.pipeMove(sender, e);
		}
	}
}
