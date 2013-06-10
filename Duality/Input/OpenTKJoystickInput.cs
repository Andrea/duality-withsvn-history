using System;
using OpenTK.Input;

namespace Duality
{
	public class OpenTKJoystickInput : IJoystickInput
	{
		private	JoystickDevice	device;
		
		public event EventHandler<JoystickButtonEventArgs> ButtonUp
		{
			add { this.device.ButtonUp += value; }
			remove { this.device.ButtonUp -= value; }
		}
		public event EventHandler<JoystickButtonEventArgs> ButtonDown
		{
			add { this.device.ButtonDown += value; }
			remove { this.device.ButtonDown -= value; }
		}
		public event EventHandler<JoystickMoveEventArgs> Move
		{
			add { this.device.Move += value; }
			remove { this.device.Move -= value; }
		}

		public string Description
		{
			get { return this.device.Description; }
		}
		public bool this[JoystickButton button]
		{
			get { return this.device.Button[button]; }
		}
		public float this[JoystickAxis axis]
		{
			get { return this.device.Axis[axis]; }
		}

		public OpenTKJoystickInput(JoystickDevice device)
		{
			this.device = device;
		}
	}
}
