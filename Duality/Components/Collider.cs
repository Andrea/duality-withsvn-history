using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

using Duality;
using Duality.Resources;

namespace Duality.Components
{
	/// <summary>
	/// Represents a body instance for physical simulation, collision detection and response.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public abstract class Collider : Component, ICmpInitializable, ITransformUpdater
	{
		[NonSerialized]	protected	Body	body	= null;
		private	BodyType	bodyType		= BodyType.Dynamic;
		private	float		linearDamp		= 0.0f;
		private	float		angularDamp		= 0.0f;
		private	float		mass			= 0.0f;
		private	bool		fixedAngle		= false;
		private	bool		ignoreGravity	= false;
		private	float		friction		= 0.0f;
		private	float		restitution		= 0.0f;
		private	Category	colCat			= Category.All;

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
		/// [GET / SET] The bodies mass. Can't be zero. To achieve zer-mass behaviour, use
		/// the kinematic body type.
		/// </summary>
		public float Mass
		{
			get { return this.mass; }
			set 
			{
				if (this.body != null) this.body.Mass = value;
				this.mass = value;
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
				if (this.body != null)
				{
					this.body.FixedRotation = value;
					this.body.Mass = this.mass;
				}
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
		/// Creates the Colliders actual body as part of the specified World.
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		protected abstract Body CreateBody(World world);
		/// <summary>
		/// Updates the Colliders body shape according to the GameObjects characteristics. It is called once initially
		/// after creating the body and may be called whenever the GameObjects state changes in a way that alters the
		/// Colliders body shape, for example when modifying its Transforms scale value.
		/// </summary>
		protected abstract void UpdateBodyShape();
		/// <summary>
		/// Initializes the Colliders physical body object.
		/// </summary>
		protected void InitBody()
		{
			if (this.body != null) this.body.Dispose();
			Transform t = this.GameObj.Transform;

			this.body = this.CreateBody(Scene.CurrentPhysics);
			this.UpdateBodyShape();

			this.body.BodyType = this.bodyType;
			this.body.LinearDamping = this.linearDamp;
			this.body.AngularDamping = this.angularDamp;
			this.body.FixedRotation = this.fixedAngle;
			this.body.IgnoreGravity = this.ignoreGravity;
			this.body.Friction = this.friction;
			this.body.Restitution = this.restitution;
			this.body.CollisionCategories = this.colCat;
			this.body.Mass = this.mass;
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
			c.mass = this.mass;
			c.fixedAngle = this.fixedAngle;
			c.ignoreGravity = this.ignoreGravity;
			c.friction = this.friction;
			c.restitution = this.restitution;
			c.colCat = this.colCat;
			c.InitBody();
		}
	}
}
