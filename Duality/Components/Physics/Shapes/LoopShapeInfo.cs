﻿using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

using Duality.EditorHints;

namespace Duality.Components.Physics
{
	/// <summary>
	/// Describes a double-sided edge loop (outline) in a <see cref="RigidBody">RigidBodies</see> shape.
	/// </summary>
	[Serializable]
	public sealed class LoopShapeInfo : ShapeInfo
	{
		private	Vector2[]	vertices;

		/// <summary>
		/// [GET / SET] The edge loops vertices.
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
			
		public LoopShapeInfo() {}
		public LoopShapeInfo(IEnumerable<Vector2> vertices) : base(1.0f)
		{
			this.vertices = vertices.ToArray();
		}

		protected override Fixture CreateFixture(Body body)
		{
			return body.CreateFixture(new LoopShape(this.CreateVertices(Vector2.One)), this);
		}
		internal override void UpdateFixture()
		{
			base.UpdateFixture();
			if (this.fixture == null) return;
			if (this.Parent == null) return;
				
			Vector2 scale = Vector2.One;
			if (this.Parent.GameObj != null && this.Parent.GameObj.Transform != null)
				scale = this.Parent.GameObj.Transform.Scale.Xy;

			LoopShape poly = this.fixture.Shape as LoopShape;
			poly.Vertices = this.CreateVertices(scale);
		}
		private FarseerPhysics.Common.Vertices CreateVertices(Vector2 scale)
		{
			Vector2[] vertices = this.vertices.ToArray();
			FarseerPhysics.Common.Vertices farseerVert = new FarseerPhysics.Common.Vertices(vertices.Length);
			for (int i = 0; i < vertices.Length; i++)
			{
				farseerVert.Add(new Vector2(
					PhysicsConvert.ToPhysicalUnit(vertices[i].X * scale.X), 
					PhysicsConvert.ToPhysicalUnit(vertices[i].Y * scale.Y)));
			}
			return farseerVert;
		}

		protected override void CopyTo(ShapeInfo target)
		{
			base.CopyTo(target);
			LoopShapeInfo c = target as LoopShapeInfo;
			c.vertices = this.vertices != null ? (Vector2[])this.vertices.Clone() : null;
		}
	}
}
