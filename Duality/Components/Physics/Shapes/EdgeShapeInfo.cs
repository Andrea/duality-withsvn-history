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
	/// Describes a single line-segment (edge) of a a <see cref="RigidBody">Rigidbodies</see> shape. 
	/// The edge is double-sided, so it doesn't matter in which order the vertices are defined.
	/// </summary>
	[Serializable]
	public sealed class EdgeShapeInfo : ShapeInfo
	{
		private	Vector2 vertex1;
		private	Vector2 vertex2;

		/// <summary>
		/// [GET / SET] The first edge vertex.
		/// </summary>
		[EditorHintIncrement(1)]
		[EditorHintDecimalPlaces(1)]
		public Vector2 VertexStart
		{
			get { return this.vertex1; }
			set { this.vertex1 = value; this.UpdateFixture(); }
		}
		/// <summary>
		/// [GET / SET] The last edge vertex.
		/// </summary>
		[EditorHintIncrement(1)]
		[EditorHintDecimalPlaces(1)]
		public Vector2 VertexEnd
		{
			get { return this.vertex2; }
			set { this.vertex2 = value; this.UpdateFixture(); }
		}
		[EditorHintFlags(MemberFlags.Invisible)]
		public override Rect AABB
		{
			get 
			{
				float minX = float.MaxValue;
				float minY = float.MaxValue;
				float maxX = float.MinValue;
				float maxY = float.MinValue;

				minX = MathF.Min(minX, this.vertex1.X);
				minY = MathF.Min(minY, this.vertex1.Y);
				maxX = MathF.Max(maxX, this.vertex1.X);
				maxY = MathF.Max(maxY, this.vertex1.Y);

				minX = MathF.Min(minX, this.vertex2.X);
				minY = MathF.Min(minY, this.vertex2.Y);
				maxX = MathF.Max(maxX, this.vertex2.X);
				maxY = MathF.Max(maxY, this.vertex2.Y);

				return new Rect(minX, minY, maxX - minX, maxY - minY);
			}
		}
			
		public EdgeShapeInfo() {}
		public EdgeShapeInfo(Vector2 start, Vector2 end) : base(1.0f)
		{
			this.vertex1 = start;
			this.vertex2 = end;
		}

		protected override Fixture CreateFixture(Body body)
		{
			return body.CreateFixture(new EdgeShape(this.vertex1, this.vertex2), this);
		}
		internal override void UpdateFixture()
		{
			base.UpdateFixture();
			if (this.fixture == null) return;
			if (this.Parent == null) return;
				
			Vector2 scale = Vector2.One;
			if (this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
				scale = this.Parent.GameObj.Transform.Scale.Xy;

			EdgeShape edge = this.fixture.Shape as EdgeShape;
			edge.Set(
				PhysicsConvert.ToPhysicalUnit(this.vertex1 * scale), 
				PhysicsConvert.ToPhysicalUnit(this.vertex2 * scale));
		}

		protected override void CopyTo(ShapeInfo target)
		{
			base.CopyTo(target);
			EdgeShapeInfo c = target as EdgeShapeInfo;
			c.vertex1 = this.vertex1;
			c.vertex2 = this.vertex2;
		}
	}
}
