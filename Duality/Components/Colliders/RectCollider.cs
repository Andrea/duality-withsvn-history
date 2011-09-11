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
	/// Represents a <see cref="Collider"/> based on a single rectangular shape.
	/// </summary>
	[Serializable]
	public class RectCollider : Collider
	{
		private	Vector2	size	= new Vector2(128.0f, 128.0f);
		private	float	density	= 1.0f;

		/// <summary>
		/// [GET / SET] The collision / body size
		/// </summary>
		public Vector2 Size
		{
			get { return this.size; }
			set { this.size = value; this.UpdateBodyShape(); }
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
			return BodyFactory.CreateRectangle(Scene.CurrentPhysics, 1.0f, 1.0f, 1.0f);
		}
		protected override void UpdateBodyShape()
		{
			PolygonShape rect = ((PolygonShape)this.body.FixtureList[0].Shape);
			rect.Vertices = FarseerPhysics.Common.PolygonTools.CreateRectangle(
				size.X * this.GameObj.Transform.Scale.X * 0.01f * 0.5f, 
				size.Y * this.GameObj.Transform.Scale.Y * 0.01f * 0.5f);
			rect.Density = this.density;
		}
	}
}
