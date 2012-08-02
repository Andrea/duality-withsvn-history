using System;
using OpenTK.Input;

namespace Duality
{
	internal class StaticKeyboardInput : IKeyboardInput
	{
		private bool keyRepeat;
		private	IKeyboardInput	realInput;
		private	EventHandler<KeyboardKeyEventArgs>	pipeKeyUp;
		private	EventHandler<KeyboardKeyEventArgs>	pipeKeyDown;

		public IKeyboardInput RealInput
		{
			get { return this.realInput; }
			set
			{
				if (this.realInput != value)
				{
					if (this.realInput != null)
					{
						this.realInput.KeyUp -= this.realInput_KeyUp;
						this.realInput.KeyDown -= this.realInput_KeyDown;
					}

					this.realInput = value;

					if (this.realInput != null)
					{
						this.realInput.KeyUp += this.realInput_KeyUp;
						this.realInput.KeyDown += this.realInput_KeyDown;
						this.realInput.KeyRepeat = this.keyRepeat;
					}
				}
			}
		}
		
		private void realInput_KeyUp(object sender, KeyboardKeyEventArgs e)
		{
			if (this.pipeKeyUp != null)
				this.pipeKeyUp(sender, e);
		}
		private void realInput_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (this.pipeKeyDown != null)
				this.pipeKeyDown(sender, e);
		}

		bool IKeyboardInput.KeyRepeat
		{
			get { return this.keyRepeat; }
			set 
			{
				this.keyRepeat = value;
				if (this.realInput != null) this.realInput.KeyRepeat = this.keyRepeat;
			}
		}
		bool IKeyboardInput.this[Key key]
		{
			get { return this.realInput != null ? this.realInput[key] : false; }
		}
		event EventHandler<KeyboardKeyEventArgs> IKeyboardInput.KeyUp
		{
			add { this.pipeKeyUp += value; }
			remove { this.pipeKeyUp -= value; }
		}
		event EventHandler<KeyboardKeyEventArgs> IKeyboardInput.KeyDown
		{
			add { this.pipeKeyDown += value; }
			remove { this.pipeKeyDown -= value; }
		}
	}
}
