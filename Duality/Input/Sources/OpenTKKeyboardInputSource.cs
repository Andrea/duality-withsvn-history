using System;
using OpenTK.Input;

namespace Duality
{
	public class OpenTKKeyboardInputSource : IKeyboardInputSource
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
		public event EventHandler LostFocus
		{
			add { this.device.LostFocus += value; }
			remove { this.device.LostFocus -= value; }
		}
		public event EventHandler GotFocus
		{
			add { this.device.GotFocus += value; }
			remove { this.device.GotFocus -= value; }
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

		public OpenTKKeyboardInputSource(KeyboardDevice device)
		{
			this.device = device;
		}
	}
}
