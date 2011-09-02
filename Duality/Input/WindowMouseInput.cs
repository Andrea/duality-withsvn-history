using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Input;

namespace Duality
{
	internal class WindowMouseInput : IMouseInput
	{
		private	MouseDevice	device;
		
		public event EventHandler<MouseButtonEventArgs> ButtonUp
		{
			add { this.device.ButtonUp += value; }
			remove { this.device.ButtonUp -= value; }
		}
		public event EventHandler<MouseButtonEventArgs> ButtonDown
		{
			add { this.device.ButtonDown += value; }
			remove { this.device.ButtonDown -= value; }
		}
		public event EventHandler<MouseMoveEventArgs> Move
		{
			add { this.device.Move += value; }
			remove { this.device.Move -= value; }
		}
		public event EventHandler<MouseWheelEventArgs> WheelChanged
		{
			add { this.device.WheelChanged += value; }
			remove { this.device.WheelChanged -= value; }
		}

		public int X
		{
			get { return this.device.X; }
		}
		public int Y
		{
			get { return this.device.Y; }
		}
		public int Wheel
		{
			get { return this.device.Wheel; }
		}
		public bool this[MouseButton key]
		{
			get { return this.device[key]; }
		}

		public WindowMouseInput(MouseDevice device)
		{
			this.device = device;
		}
	}
}
