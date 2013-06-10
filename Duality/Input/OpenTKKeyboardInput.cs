using System;
using OpenTK.Input;

namespace Duality
{
	public class OpenTKKeyboardInput : IKeyboardInput
	{
		private	KeyboardDevice	device;
		
		public event EventHandler<KeyboardKeyEventArgs> KeyUp
		{
			add { this.device.KeyUp += value; }
			remove { this.device.KeyUp -= value; }
		}
		public event EventHandler<KeyboardKeyEventArgs> KeyDown
		{
			add { this.device.KeyDown += value; }
			remove { this.device.KeyDown -= value; }
		}

		public bool KeyRepeat
		{
			get { return this.device.KeyRepeat; }
			set { this.device.KeyRepeat = value; }
		}
		public bool this[Key key]
		{
			get { return this.device[key]; }
		}

		public OpenTKKeyboardInput(KeyboardDevice device)
		{
			this.device = device;
		}
	}
}
