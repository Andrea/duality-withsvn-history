using System;
using OpenTK.Input;

namespace Duality
{
	public class OpenTKMouseInputSource : IMouseInputSource
	{
		public delegate void CursorPosSetter(int v);

		private	MouseDevice	device;
		private CursorPosSetter cursorPosSetterX;
		private CursorPosSetter cursorPosSetterY;
		
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
		public event EventHandler Leave
		{
			add { this.device.Leave += value; }
			remove { this.device.Leave -= value; }
		}
		public event EventHandler Enter
		{
			add { this.device.Enter += value; }
			remove { this.device.Enter -= value; }
		}
		public event EventHandler<MouseWheelEventArgs> WheelChanged
		{
			add { this.device.WheelChanged += value; }
			remove { this.device.WheelChanged -= value; }
		}

		public int X
		{
			get { return this.device.X; }
			set { if (this.cursorPosSetterX != null) this.cursorPosSetterX(value); }
		}
		public int Y
		{
			get { return this.device.Y; }
			set { if (this.cursorPosSetterY != null) this.cursorPosSetterY(value); }
		}
		public int Wheel
		{
			get { return this.device.Wheel; }
		}
		public bool this[MouseButton key]
		{
			get { return this.device[key]; }
		}

		public OpenTKMouseInputSource(MouseDevice device, CursorPosSetter cursorPosSetterX, CursorPosSetter cursorPosSetterY)
		{
			this.device = device;
			this.cursorPosSetterX = cursorPosSetterX;
			this.cursorPosSetterY = cursorPosSetterY;
		}
	}
}
