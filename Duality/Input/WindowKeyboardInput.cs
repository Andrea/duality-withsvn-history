using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Input;

namespace Duality
{
	internal class WindowKeyboardInput : IKeyboardInput
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

		public WindowKeyboardInput(KeyboardDevice device)
		{
			this.device = device;
		}
	}
}
