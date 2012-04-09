using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

using Duality.EditorHints;

namespace Duality.Components
{
	public partial class Collider
	{
		/// <summary>
		/// Describes a <see cref="Collider">Colliders</see> primitive shape. A Colliders overall shape may be combined of any number of primitive shapes.
		/// </summary>
		[Serializable]
		public abstract class ShapeInfo
		{
			[NonSerialized]	
			protected	Fixture		fixture		= null;
			private		Collider	parent		= null;
			private		float		density		= 1.0f;
			private		float		friction	= 0.3f;
			private		float		restitution	= 0.3f;
			private		bool		sensor		= false;
			
			/// <summary>
			/// [GET] The shape's parent <see cref="Collider"/>.
			/// </summary>
			[EditorHintFlags(MemberFlags.Invisible)]
			public Collider Parent
			{
				get { return this.parent; }
				set 
				{ 
					if (this.parent != value)
					{
						if (this.parent != null) this.parent.RemoveShape(this);
						this.parent = value;
						if (this.parent != null) this.parent.AddShape(this);
					}
				}
			}
			/// <summary>
			/// [GET / SET] The shapes density.
			/// </summary>
			[EditorHintIncrement(0.05f)]
			[EditorHintRange(0.0f, 100.0f)]
			public float Density
			{
				get { return this.density; }
				set 
				{
					this.density = value;
					if (this.parent != null) // Full update to recalculate mass
						this.parent.UpdateBodyShape();
					else
						this.UpdateFixture();
				}
			}
			/// <summary>
			/// [GET / SET] Whether or not the shape acts as sensor i.e. is not part of a rigid body.
			/// </summary>
			public bool IsSensor
			{
				get { return this.sensor; }
				set { this.sensor = value; this.UpdateFixture(); }
			}
			/// <summary>
			/// [GET / SET] The shapes friction value.
			/// </summary>
			[EditorHintIncrement(0.05f)]
			[EditorHintRange(0.0f, 1.0f)]
			public float Friction
			{
				get { return this.friction; }
				set { this.friction = value; this.UpdateFixture(); }
			}
			/// <summary>
			/// [GET / SET] The shapes restitution value.
			/// </summary>
			[EditorHintIncrement(0.05f)]
			[EditorHintRange(0.0f, 1.0f)]
			public float Restitution
			{
				get { return this.restitution; }
				set { this.restitution = value; this.UpdateFixture(); }
			}
			/// <summary>
			/// [GET] Returns the Shapes axis-aligned bounding box
			/// </summary>
			[EditorHintFlags(MemberFlags.Invisible)]
			public abstract Rect AABB { get; }

			protected ShapeInfo()
			{
			}
			protected ShapeInfo(float density)
			{
				this.density = density;
			}

			internal void DestroyFixture(Body body)
			{
				if (this.fixture == null) return;
				body.DestroyFixture(this.fixture);
				this.fixture = null;
			}
			internal void InitFixture(Body body)
			{
				this.fixture = this.CreateFixture(body);
				this.fixture.UserData = this;
			}
			protected abstract Fixture CreateFixture(Body body);
			internal virtual void UpdateFixture()
			{
				this.fixture.Shape.Density = this.density;
				this.fixture.IsSensor = this.sensor;
				this.fixture.Restitution = this.restitution;
				this.fixture.Friction = this.friction;
			}

			/// <summary>
			/// Copies this ShapeInfos data to another one. It is assumed that both are of the same type.
			/// </summary>
			/// <param name="target"></param>
			protected virtual void CopyTo(ShapeInfo target)
			{
				// Don't copy the parent!
				target.density = this.density;
				target.sensor = this.sensor;
				target.friction = this.friction;
				target.restitution = this.restitution;
			}
			/// <summary>
			/// Clones the ShapeInfo.
			/// </summary>
			/// <returns></returns>
			public ShapeInfo Clone()
			{
				ShapeInfo newObj = this.GetType().CreateInstanceOf() as ShapeInfo;
				this.CopyTo(newObj);
				return newObj;
			}
		}
		/// <summary>
		/// Describes a <see cref="Collider">Colliders</see> circle shape.
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

				if (this.Parent == null) return;
				Vector2 scale = Vector2.One;
				if (this.Parent != null && this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
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
		/// <summary>
		/// Describes a <see cref="Collider">Colliders</see> polygon shape.
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
				set { this.vertices = value; this.UpdateFixture(); }
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
				return body.CreateFixture(new PolygonShape(this.CreateVertices(Vector2.One), 1.0f), this);
			}
			internal override void UpdateFixture()
			{
				base.UpdateFixture();
				
				Vector2 scale = Vector2.One;
				if (this.Parent != null && this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
					scale = this.Parent.GameObj.Transform.Scale.Xy;

				PolygonShape poly = this.fixture.Shape as PolygonShape;
				poly.Set(this.CreateVertices(scale));
			}
			private FarseerPhysics.Common.Vertices CreateVertices(Vector2 scale)
			{
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
				for (int i = 0; i < sortedVertices.Length; i++)
				{
					Vector2 rel = (sortedVertices[i] - centroid);
					float len = rel.Length;
					sortedVertices[i] = centroid + rel.Normalized * MathF.Max(0.0f, len - 1.5f);
				}

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
}
