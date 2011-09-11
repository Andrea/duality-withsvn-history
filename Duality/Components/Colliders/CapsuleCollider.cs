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
	/// Represents a <see cref="Collider"/> based on a compound capsule shape.
	/// </summary>
	[Serializable]
	public class CapsuleCollider : Collider
	{
		private	Vector2	size	= new Vector2(128.0f, 256.0f);

		/// <summary>
		/// [GET / SET] The collision / body size
		/// </summary>
		public Vector2 Size
		{
			get { return this.size; }
			set { this.size = value; this.UpdateBodyShape(); }
		}

		protected override Body CreateBody(World world)
		{
			return BodyFactory.CreateCapsule(Scene.CurrentPhysics, 1.0f, 1.0f, 1.0f);
		}
		protected override void UpdateBodyShape()
		{
			float width = size.X * this.GameObj.Transform.Scale.X * 0.01f;
			float height = Math.Max(width + 0.000001f, size.Y * this.GameObj.Transform.Scale.Y * 0.01f);

			PolygonShape rect = ((PolygonShape)this.body.FixtureList[0].Shape);
			rect.Vertices = FarseerPhysics.Common.PolygonTools.CreateRectangle(
				width * 0.5f, 
				(height - width) * 0.5f);

			CircleShape topCircle = ((CircleShape)this.body.FixtureList[1].Shape);
			topCircle.Radius = width * 0.5f;
			topCircle.Position = new Vector2(0, (height - width) * 0.5f);

			CircleShape bottomCircle = ((CircleShape)this.body.FixtureList[2].Shape);
			bottomCircle.Radius = width * 0.5f;
			bottomCircle.Position = new Vector2(0, (height - width) * -0.5f);
		}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			CapsuleCollider c = target as CapsuleCollider;
			c.size = this.size;
			c.UpdateBodyShape();
		}
	}
}
