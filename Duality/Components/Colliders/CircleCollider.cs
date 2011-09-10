using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

using Duality;
using Duality.Resources;

namespace Duality.Components.Colliders
{
	/// <summary>
	/// Represents a <see cref="Collider"/> based on a single circle shape.
	/// </summary>
	[Serializable]
	public class CircleCollider : Collider
	{
		private	float	radius	= 64.0f;
		private	float	density	= 1.0f;

		/// <summary>
		/// [GET / SET] The collision / body radius.
		/// </summary>
		public float Radius
		{
			get { return this.radius; }
			set { this.radius = value; this.UpdateBodyShape(); }
		}
		/// <summary>
		/// [GET / SET] The bodies density.
		/// </summary>
		public float Density
		{
			get { return this.density; }
			set { this.density = value; this.UpdateBodyShape(); }
		}

		protected override Body CreateBody(World world)
		{
			return BodyFactory.CreateCircle(Scene.CurrentPhysics, 1.0f, 1.0f);
		}
		protected override void UpdateBodyShape()
		{
			float scale = this.GameObj.Transform.Scale.Xy.Length / MathF.Sqrt(2.0f);
			CircleShape circle = ((CircleShape)this.body.FixtureList[0].Shape);
			circle.Radius = this.radius * scale * 0.01f;
			circle.Density = this.density;
		}
	}
}
