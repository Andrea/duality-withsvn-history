using System;
using OpenTK.Input;

namespace Duality
{
	public class OpenTKJoystickInputSource : IJoystickInputSource
	{
		private	JoystickDevice	device;
		
		public string Description
		{
			get { return this.device.Description; }
		}
		public bool IsAvailable
		{
			get { return true; }
		}
		public bool this[JoystickButton button]
		{
			get { return this.device.Button[button]; }
		}
		public float this[JoystickAxis axis]
		{
			get { return this.device.Axis[axis]; }
		}

		public OpenTKJoystickInputSource(JoystickDevice device)
		{
			this.device = device;
		}
	}
}
