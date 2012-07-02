using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

using Duality.EditorHints;

namespace Duality.Components.Physics
{
	/// <summary>
	/// Describes a <see cref="RigidBody">Colliders</see> circle shape.
	/// </summary>
	[Serializable]
	public sealed class CircleShapeInfo : ShapeInfo
	{
		private	float	radius;
		private	Vector2	position;

		/// <summary>
		/// [GET / SET] The circles radius.
		/// </summary>
		[EditorHintIncrement(1)]
		[EditorHintDecimalPlaces(1)]
		public float Radius
		{
			get { return this.radius; }
			set { this.radius = value; this.UpdateFixture(); }
		}
		/// <summary>
		/// [GET / SET] The circles position.
		/// </summary>
		[EditorHintIncrement(1)]
		[EditorHintDecimalPlaces(1)]
		public Vector2 Position
		{
			get { return this.position; }
			set { this.position = value; this.UpdateFixture(); }
		}
		[EditorHintFlags(MemberFlags.Invisible)]
		public override Rect AABB
		{
			get { return Rect.AlignCenter(position.X, position.Y, radius * 2, radius * 2); }
		}

		public CircleShapeInfo() {}
		public CircleShapeInfo(float radius, Vector2 position, float density) : base(density)
		{
			this.radius = radius;
			this.position = position;
		}

		protected override Fixture CreateFixture(Body body)
		{
			return body.CreateFixture(new CircleShape(1.0f, 1.0f), this);
		}
		internal override void UpdateFixture()
		{
			base.UpdateFixture();
			if (this.fixture == null) return;
			if (this.Parent == null) return;

			Vector2 scale = Vector2.One;
			if (this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
				scale = this.Parent.GameObj.Transform.Scale.Xy;
			float uniformScale = scale.Length / MathF.Sqrt(2.0f);

			CircleShape circle = this.fixture.Shape as CircleShape;
			circle.Radius = PhysicsConvert.ToPhysicalUnit(this.radius * uniformScale);
			circle.Position = PhysicsConvert.ToPhysicalUnit(new Vector2(this.position.X * scale.X, this.position.Y * scale.Y));
		}

		protected override void CopyTo(ShapeInfo target)
		{
			base.CopyTo(target);
			CircleShapeInfo c = target as CircleShapeInfo;
			c.radius = this.radius;
			c.position = this.position;
		}
	}
}
