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
	/// Describes a <see cref="RigidBody">Colliders</see> polygon shape.
	/// </summary>
	[Serializable]
	public sealed class PolyShapeInfo : ShapeInfo
	{
		private	Vector2[]	vertices;

		/// <summary>
		/// [GET / SET] The polygons vertices.
		/// </summary>
		[EditorHintFlags(MemberFlags.ForceWriteback)]
		[EditorHintIncrement(1)]
		[EditorHintDecimalPlaces(1)]
		public Vector2[] Vertices
		{
			get { return this.vertices; }
			set { this.vertices = value; this.UpdateFixture(true); }
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
				for (int i = 0; i < this.vertices.Length; i++)
				{
					minX = MathF.Min(minX, this.vertices[i].X);
					minY = MathF.Min(minY, this.vertices[i].Y);
					maxX = MathF.Max(maxX, this.vertices[i].X);
					maxY = MathF.Max(maxY, this.vertices[i].Y);
				}
				return new Rect(minX, minY, maxX - minX, maxY - minY);
			}
		}
			
		public PolyShapeInfo() {}
		public PolyShapeInfo(IEnumerable<Vector2> vertices, float density) : base(density)
		{
			this.vertices = vertices.ToArray();
		}

		protected override Fixture CreateFixture(Body body)
		{
			var farseerVert = this.CreateVertices(Vector2.One);
			if (farseerVert == null) return null;

			return body.CreateFixture(new PolygonShape(farseerVert, 1.0f), this);
		}
		internal override void UpdateFixture(bool updateShape = false)
		{
			base.UpdateFixture(updateShape);
			if (this.fixture == null) return;
			if (this.Parent == null) return;
				
			Vector2 scale = Vector2.One;
			if (this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
				scale = this.Parent.GameObj.Transform.Scale.Xy;

			PolygonShape poly = this.fixture.Shape as PolygonShape;
			poly.Set(this.CreateVertices(scale));
		}
		private FarseerPhysics.Common.Vertices CreateVertices(Vector2 scale)
		{
			if (!MathF.IsPolygonConvex(this.vertices)) return null;

			// Sort vertices clockwise before submitting them to Farseer
			Vector2[] sortedVertices = this.vertices.ToArray();
			Vector2 centroid = Vector2.Zero;
			for (int i = 0; i < sortedVertices.Length; i++)
				centroid += sortedVertices[i];
			centroid /= sortedVertices.Length;
			sortedVertices.StableSort(delegate(Vector2 first, Vector2 second)
			{
				return MathF.RoundToInt(
					1000000.0f * MathF.Angle(centroid.X, centroid.Y, first.X, first.Y) - 
					1000000.0f * MathF.Angle(centroid.X, centroid.Y, second.X, second.Y));
			});

			// Shrink a little bit
			//for (int i = 0; i < sortedVertices.Length; i++)
			//{
			//    Vector2 rel = (sortedVertices[i] - centroid);
			//    float len = rel.Length;
			//    sortedVertices[i] = centroid + rel.Normalized * MathF.Max(0.0f, len - 1.5f);
			//}

			// Submit vertices
			FarseerPhysics.Common.Vertices v = new FarseerPhysics.Common.Vertices(sortedVertices.Length);
			for (int i = 0; i < sortedVertices.Length; i++)
			{
				v.Add(new Vector2(
					PhysicsConvert.ToPhysicalUnit(sortedVertices[i].X * scale.X), 
					PhysicsConvert.ToPhysicalUnit(sortedVertices[i].Y * scale.Y)));
			}
			return v;
		}

		protected override void CopyTo(ShapeInfo target)
		{
			base.CopyTo(target);
			PolyShapeInfo c = target as PolyShapeInfo;
			c.vertices = this.vertices != null ? (Vector2[])this.vertices.Clone() : null;
		}
	}
}
