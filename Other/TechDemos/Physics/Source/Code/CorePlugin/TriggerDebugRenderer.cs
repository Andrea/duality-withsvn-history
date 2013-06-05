using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Input;

using Duality;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Components.Physics;

namespace PhysicsTestbed
{
	/// <summary>
	/// This component uses a SpriteRenderers ColorTint to display the state of a Trigger.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Trigger))]
	[RequiredComponent(typeof(SpriteRenderer))]
	public class TriggerDebugRenderer : Component, ICmpUpdatable
	{
		private	ColorRgba	oldColorTint	= ColorRgba.White;
		private	bool		wasTriggered	= false;

		void ICmpUpdatable.OnUpdate()
		{
			Trigger trig = this.GameObj.GetComponent<Trigger>();
			SpriteRenderer sprite = this.GameObj.GetComponent<SpriteRenderer>();

			if (trig.Triggered && !this.wasTriggered)
			{
				this.oldColorTint = sprite.ColorTint;
				sprite.ColorTint = ColorRgba.Mix(this.oldColorTint, ColorRgba.Red, 0.5f);
			}
			else if (!trig.Triggered && this.wasTriggered)
			{
				sprite.ColorTint = this.oldColorTint;
			}
			this.wasTriggered = trig.Triggered;
		}
	}
}
