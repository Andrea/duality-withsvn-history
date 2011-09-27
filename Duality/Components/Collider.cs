using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

using Duality;
using Duality.Resources;

namespace Duality.Components
{
	/// <summary>
	/// Represents a body instance for physical simulation, collision detection and response.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public class Collider : Component, ICmpInitializable, ITransformUpdater
	{
		/// <summary>
		/// Describes a <see cref="Collider">Colliders</see> primitive shape. A Colliders overall shape may be combined of any number of primitive shapes.
		/// </summary>
		[Serializable]
		public abstract class ShapeInfo
		{
			private	float	density	= 1.0f;
			
			/// <summary>
			/// [GEt / SET] The shapes density.
			/// </summary>
			public float Density
			{
				get { return this.density; }
				set { this.density = value; }
			}

			protected ShapeInfo() {}
			protected ShapeInfo(float density)
			{
				this.density = density;
			}

			/// <summary>
			/// Creates an actual physics shape.
			/// </summary>
			/// <returns></returns>
			public abstract Shape CreateShape();
			/// <summary>
			/// Updates the specified physics shape to fit this ShapeInfo
			/// </summary>
			/// <param name="shape"></param>
			/// <param name="scale"></param>
			public abstract void UpdateShape(Shape shape, Vector2 scale);

			/// <summary>
			/// Copies this ShapeInfos data to another one. It is assumed that both are of the same type.
			/// </summary>
			/// <param name="target"></param>
			protected virtual void CopyTo(ShapeInfo target)
			{
				target.density = this.density;
			}
			/// <summary>
			/// Clones the ShapeInfo.
			/// </summary>
			/// <returns></returns>
			public ShapeInfo Clone()
			{
				ShapeInfo newObj = ReflectionHelper.CreateInstanceOf(this.GetType()) as ShapeInfo;
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
			public float Radius
			{
				get { return this.radius; }
				set { this.radius = value; }
			}
			/// <summary>
			/// [GET / SET] The circles position.
			/// </summary>
			public Vector2 Position
			{
				get { return this.position; }
				set { this.position = value; }
			}

			public CircleShapeInfo() {}
			public CircleShapeInfo(float radius, Vector2 position, float density) : base(density)
			{
				this.radius = radius;
				this.position = position;
			}

			public override Shape CreateShape()
			{
				return new CircleShape(1.0f, 1.0f);
			}
			public override void UpdateShape(Shape shape, Vector2 scale)
			{
				float uniformScale = scale.Length / MathF.Sqrt(2.0f);
				CircleShape circle = shape as CircleShape;
				circle.Radius = this.radius * uniformScale * 0.01f;
				circle.Position = this.position;
				circle.Density = this.Density * 100.0f;
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
			public Vector2[] Vertices
			{
				get { return this.vertices; }
				set { this.vertices = value; }
			}
			
			public PolyShapeInfo() {}
			public PolyShapeInfo(IEnumerable<Vector2> vertices, float density) : base(density)
			{
				this.vertices = vertices.ToArray();
			}

			public override Shape CreateShape()
			{
				return new PolygonShape(1.0f);
			}
			public override void UpdateShape(Shape shape, Vector2 scale)
			{
				PolygonShape poly = shape as PolygonShape;
				poly.Density = this.Density * 100.0f;
				poly.Vertices = new FarseerPhysics.Common.Vertices(this.vertices.Length);
				for (int i = 0; i < poly.Vertices.Count; i++)
				{
					poly.Vertices[i] = new Vector2(
						this.vertices[i].X * scale.X * 0.01f, 
						this.vertices[i].Y * scale.Y * 0.01f);
				}
			}

			protected override void CopyTo(ShapeInfo target)
			{
				base.CopyTo(target);
				PolyShapeInfo c = target as PolyShapeInfo;
				c.vertices = this.vertices != null ? (Vector2[])this.vertices.Clone() : null;
			}
		}

		[NonSerialized]	private	Body	body	= null;
		private	BodyType	bodyType		= BodyType.Dynamic;
		private	float		linearDamp		= 0.0f;
		private	float		angularDamp		= 0.0f;
		private	bool		fixedAngle		= false;
		private	bool		ignoreGravity	= false;
		private	float		friction		= 0.0f;
		private	float		restitution		= 0.0f;
		private	Category	colCat			= Category.All;
		private	List<ShapeInfo>	shapes		= new List<ShapeInfo>{ new CircleShapeInfo(64.0f, Vector2.Zero, 1.0f) };

		/// <summary>
		/// [GET / SET] The type of the physical body.
		/// </summary>
		public BodyType PhysicsBodyType
		{
			get { return this.bodyType; }
			set 
			{
				if (this.body != null) this.body.BodyType = value;
				this.bodyType = value;
			}
		}
		/// <summary>
		/// [GET / SET] The damping that is applied to the bodies velocity.
		/// </summary>
		public float LinearDamping
		{
			get { return this.linearDamp; }
			set 
			{
				if (this.body != null) this.body.LinearDamping = value;
				this.linearDamp = value;
			}
		}
		/// <summary>
		/// [GET / SET] The damping that is applied to the bodies angular velocity.
		/// </summary>
		public float AngularDamping
		{
			get { return this.angularDamp; }
			set 
			{
				if (this.body != null) this.body.AngularDamping = value;
				this.angularDamp = value;
			}
		}
		/// <summary>
		/// [GET / SET] Whether the bodies rotation is fixed.
		/// </summary>
		public bool FixedAngle
		{
			get { return this.fixedAngle; }
			set 
			{
				if (this.body != null) this.body.FixedRotation = value;
				this.fixedAngle = value;
			}
		}
		/// <summary>
		/// [GET / SET] Whether the body ignores gravity.
		/// </summary>
		public bool IgnoreGravity
		{
			get { return this.ignoreGravity; }
			set 
			{
				if (this.body != null) this.body.IgnoreGravity = value;
				this.ignoreGravity = value;
			}
		}
		/// <summary>
		/// [GET / SET] The bodies (average) friction value.
		/// </summary>
		public float Friction
		{
			get { return this.friction; }
			set 
			{
				if (this.body != null) this.body.Friction = value;
				this.friction = value;
			}
		}
		/// <summary>
		/// [GET / SET] The bodies (average) restitution value.
		/// </summary>
		public float Restitution
		{
			get { return this.restitution; }
			set 
			{
				if (this.body != null) this.body.Restitution = value;
				this.restitution = value;
			}
		}
		/// <summary>
		/// [GET / SET] A bitmask that specifies the collision categories to which this Collider belongs.
		/// It will interact with all Colliders with which it shares at least one common category.
		/// </summary>
		public Category CollisionCategory
		{
			get { return this.colCat; }
			set
			{
				this.colCat = value;
				if (this.body != null) this.body.CollisionCategories = value;
			}
		}
		/// <summary>
		/// [GET] Enumerates the <see cref="ShapeInfo">primitive shapes</see> this Body consists of.
		/// </summary>
		public IEnumerable<ShapeInfo> Shapes
		{
			get { return this.shapes; }
		}

		private Body CreateBody()
		{
			Body b = new Body(Scene.CurrentPhysics);
			foreach (ShapeInfo s in this.shapes) b.CreateFixture(s.CreateShape(), s);
			return b;
		}
		private void UpdateBodyShape()
		{
			Vector2 scale = this.GameObj.Transform.Scale.Xy;
			foreach (Fixture f in this.body.FixtureList)
			{
				ShapeInfo info = f.UserData as ShapeInfo;
				info.UpdateShape(f.Shape, scale);
			}
		}
		private void InitBody()
		{
			if (this.body != null) this.body.Dispose();
			Transform t = this.GameObj.Transform;

			this.body = this.CreateBody();
			this.UpdateBodyShape();

			this.body.BodyType = this.bodyType;
			this.body.LinearDamping = this.linearDamp;
			this.body.AngularDamping = this.angularDamp;
			this.body.FixedRotation = this.fixedAngle;
			this.body.IgnoreGravity = this.ignoreGravity;
			this.body.Friction = this.friction;
			this.body.Restitution = this.restitution;
			this.body.CollisionCategories = this.colCat;
			this.body.UserData = this;

			this.body.SetTransform(t.Pos.Xy * 0.01f, t.Angle);
			this.body.LinearVelocity = t.Vel.Xy * 0.01f / Time.SPFMult;
			this.body.AngularVelocity = t.AngleVel / Time.SPFMult;

			this.body.OnCollision += this.body_OnCollision;
			this.body.OnSeparation += this.body_OnSeparation;
			this.body.AfterCollision += this.body_AfterCollision;
		}

		/// <summary>
		/// Awakes the body if it has been in a resting state that is now being left, such as
		/// when changing physical properties at runtime. You usually don't need to call this.
		/// </summary>
		public void AwakeBody()
		{
			if (this.body != null) this.body.Awake = true;
		}
		
		private bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			return true;
			Log.Core.Write("OnCollision: {0},\t{1}",
				fixtureA.Body.UserData as Collider,
				fixtureB.Body.UserData as Collider);
			int count = contact.Manifold.PointCount;
			for (int i = 0; i < count; i++)
			{
				Log.Core.Write("\t{0:F}\t{1:F}",
					contact.Manifold.Points[i].NormalImpulse * 100.0f,
					contact.Manifold.Points[i].TangentImpulse * 100.0f);
			}
			return true;
		}
		private void body_OnSeparation(Fixture fixtureA, Fixture fixtureB)
		{
			return;
			Log.Core.Write("OnSeparation: {0},\t{1}",
				fixtureA.Body.UserData as Collider,
				fixtureB.Body.UserData as Collider);
		}
		private void body_AfterCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			return;
			Log.Core.Write("AfterCollision: {0},\t{1}",
				fixtureA.Body.UserData as Collider,
				fixtureB.Body.UserData as Collider);
			int count = contact.Manifold.PointCount;
			for (int i = 0; i < count; i++)
			{
				Log.Core.Write("\t{0:F}\t{1:F}",
					contact.Manifold.Points[i].NormalImpulse * 100.0f,
					contact.Manifold.Points[i].TangentImpulse * 100.0f);
			}
		}

		void ITransformUpdater.UpdateTransform(Transform t)
		{
			t.SetTransform(
				new Vector3(this.body.Position.X * 100.0f, this.body.Position.Y * 100.0f, t.Pos.Z),
				new Vector3(this.body.LinearVelocity.X * 100.0f * Time.SPFMult, this.body.LinearVelocity.Y * 100.0f * Time.SPFMult, t.Vel.Z),
				t.Scale,
				this.body.Rotation,
				this.body.AngularVelocity * Time.SPFMult);
		}
		void ITransformUpdater.OnTransformChanged(Transform t, Transform.DirtyFlags changes)
		{
			if ((changes & Transform.DirtyFlags.Pos) != Transform.DirtyFlags.None)
				this.body.Position = t.Pos.Xy * 0.01f;
			if ((changes & Transform.DirtyFlags.Vel) != Transform.DirtyFlags.None)
				this.body.LinearVelocity = t.Vel.Xy * 0.01f / Time.SPFMult;
			if ((changes & Transform.DirtyFlags.Angle) != Transform.DirtyFlags.None)
				this.body.Rotation = t.Angle;
			if ((changes & Transform.DirtyFlags.AngleVel) != Transform.DirtyFlags.None)
				this.body.AngularVelocity = t.AngleVel / Time.SPFMult;
			if ((changes & Transform.DirtyFlags.Scale) != Transform.DirtyFlags.None)
				this.UpdateBodyShape();

			if (changes != Transform.DirtyFlags.None) this.body.Awake = true;
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				this.InitBody();
				this.GameObj.Transform.RegisterExternalUpdater(this);
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
			{
				if (this.body != null)
				{
					this.body.Dispose();
					this.body = null;
				}
				this.GameObj.Transform.UnregisterExternalUpdater(this);
			}
		}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Collider c = target as Collider;
			c.bodyType = this.bodyType;
			c.linearDamp = this.linearDamp;
			c.angularDamp = this.angularDamp;
			c.fixedAngle = this.fixedAngle;
			c.ignoreGravity = this.ignoreGravity;
			c.friction = this.friction;
			c.restitution = this.restitution;
			c.colCat = this.colCat;
			c.shapes = this.shapes == null ? null : new List<ShapeInfo>(this.shapes.Select(s => s.Clone()));
			c.InitBody();
		}
	}
}
