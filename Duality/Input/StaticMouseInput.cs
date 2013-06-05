using System;
using OpenTK.Input;

namespace Duality
{
	internal class StaticMouseInput : IMouseInput
	{
		private	IMouseInput	realInput;
		private	EventHandler<MouseButtonEventArgs>	pipeKeyUp;
		private	EventHandler<MouseButtonEventArgs>	pipeKeyDown;
		private	EventHandler<MouseMoveEventArgs>	pipeMove;
		private	EventHandler<MouseWheelEventArgs>	pipeWheel;


		public IMouseInput RealInput
		{
			get { return this.realInput; }
			set
			{
				if (this.realInput != value)
				{
					if (this.realInput != null)
					{
						this.realInput.ButtonUp -= this.realInput_ButtonUp;
						this.realInput.ButtonDown -= this.realInput_ButtonDown;
						this.realInput.Move -= this.realInput_Move;
						this.realInput.WheelChanged -= this.realInput_WheelChanged;
					}

					this.realInput = value;

					if (this.realInput != null)
					{
						this.realInput.ButtonUp += this.realInput_ButtonUp;
						this.realInput.ButtonDown += this.realInput_ButtonDown;
						this.realInput.Move += this.realInput_Move;
						this.realInput.WheelChanged += this.realInput_WheelChanged;
					}
				}
			}
		}
		public int X
		{
			get { return this.realInput != null ? this.realInput.X : 0; }
			set { if (this.realInput != null) this.realInput.X = value; }
		}
		public int Y
		{
			get { return this.realInput != null ? this.realInput.Y : 0; }
			set { if (this.realInput != null) this.realInput.Y = value; }
		}
		public int Wheel
		{
			get { return this.realInput != null ? this.realInput.Wheel : 0; }
		}
		public bool this[MouseButton btn]
		{
			get { return this.realInput != null ? this.realInput[btn] : false; }
		}

		public event EventHandler<MouseButtonEventArgs> ButtonUp
		{
			add { this.pipeKeyUp += value; }
			remove { this.pipeKeyUp -= value; }
		}
		public event EventHandler<MouseButtonEventArgs> ButtonDown
		{
			add { this.pipeKeyDown += value; }
			remove { this.pipeKeyDown -= value; }
		}
		public event EventHandler<MouseMoveEventArgs> Move
		{
			add { this.pipeMove += value; }
			remove { this.pipeMove -= value; }
		}
		public event EventHandler<MouseWheelEventArgs> WheelChanged
		{
			add { this.pipeWheel += value; }
			remove { this.pipeWheel -= value; }
		}

		
		private void realInput_ButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (this.pipeKeyUp != null)
				this.pipeKeyUp(sender, e);
		}
		private void realInput_ButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.pipeKeyDown != null)
				this.pipeKeyDown(sender, e);
		}
		private void realInput_Move(object sender, MouseMoveEventArgs e)
		{
			if (this.pipeMove != null)
				this.pipeMove(sender, e);
		}
		private void realInput_WheelChanged(object sender, MouseWheelEventArgs e)
		{
			if (this.pipeWheel != null)
				this.pipeWheel(sender, e);
		}
	}
}
