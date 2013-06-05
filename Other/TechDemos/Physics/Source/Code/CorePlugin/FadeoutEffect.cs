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
	/// A simple fadeout effect
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(SpriteRenderer))]
	public class FadeoutEffect : Component, ICmpUpdatable
	{
		private	float	fade		= 1.0f;
		private	float	fadeSpeed	= 0.01f;

		public float FadeValue
		{
			get { return this.fade; }
			set { this.fade = value; }
		}
		public float FadeSpeed
		{
			get { return this.fadeSpeed; }
			set { this.fadeSpeed = value; }
		}

		void ICmpUpdatable.OnUpdate()
		{
			SpriteRenderer sprite = this.GameObj.GetComponent<SpriteRenderer>();

			sprite.ColorTint = sprite.ColorTint.WithAlpha(MathF.Clamp(this.fade, 0.0f, 1.0f));

			this.fade -= Time.TimeMult * this.fadeSpeed;
			if (this.fade <= 0.0f) this.GameObj.DisposeLater();
		}
	}
}
