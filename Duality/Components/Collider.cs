using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

using Duality.EditorHints;
using Duality.Resources;

namespace Duality.Components
{
	/// <summary>
	/// Represents a body instance for physical simulation, collision detection and response.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public partial class Collider : Component, ICmpInitializable, ICmpUpdatable, ITransformUpdater
	{
		/// <summary>
		/// The type of a <see cref="Collider">Colliders</see> physical body.
		/// </summary>
		public enum BodyType
		{
			/// <summary>
			/// A static body. It will never move due to physical forces.
			/// </summary>
			Static,
			/// <summary>
			/// A dynamic body. Its movement is determined by physical effects.
			/// </summary>
			Dynamic
		}
		private struct ColEvent
		{
			public enum EventType
			{
				Collision,
				Separation,
				PostSolve
			}

			public	EventType		Type;
			public	Fixture			FixtureA;
			public	Fixture			FixtureB;
			public	CollisionData	Data;

			public ColEvent(EventType type, Fixture fxA, Fixture fxB, CollisionData data)
			{
				this.Type = type;
				this.FixtureA = fxA;
				this.FixtureB = fxB;
				this.Data = data;
			}
		}

		private	BodyType	bodyType		= BodyType.Dynamic;
		private	float		linearDamp		= 0.0f;
		private	float		angularDamp		= 0.0f;
		private	bool		fixedAngle		= false;
		private	bool		ignoreGravity	= false;
		private	bool		continous		= false;
		private	Category	colCat			= Category.Cat1;
		private	Category	colWith			= Category.All;
		private	List<ShapeInfo>	shapes		= new List<ShapeInfo>();
		private	List<JointInfo>	joints		= new List<JointInfo>();
		[NonSerialized]	private	bool			initialized	= false;
		[NonSerialized]	private	Body			body		= null;
		[NonSerialized]	private	List<ColEvent>	eventBuffer	= new List<ColEvent>();

		/// <summary>
		/// [GET / SET] The type of the physical body.
		/// </summary>
		public BodyType PhysicsBodyType
		{
			get { return this.bodyType; }
			set 
			{
				if (this.body != null) this.body.BodyType = (value == BodyType.Static ? FarseerPhysics.Dynamics.BodyType.Static : FarseerPhysics.Dynamics.BodyType.Dynamic);
				this.bodyType = value;
			}
		}
		/// <summary>
		/// [GET / SET] The damping that is applied to the bodies velocity.
		/// </summary>
		[EditorHintRange(0.0f, 100.0f)]
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
		[EditorHintRange(0.0f, 100.0f)]
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
		/// [GET / SET] Whether the body is included in continous collision detection or not.
		/// It prevents the body from moving through others at high speeds at the cost of performance.
		/// </summary>
		public bool ContinousCollision
		{
			get { return this.continous; }
			set 
			{
				if (this.body != null) this.body.IsBullet = value;
				this.continous = value;
			}
		}
		/// <summary>
		/// [GET / SET] A bitmask that specifies the collision categories to which this Collider belongs.
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
		/// [GET / SET] A bitmask that specifies which collision categories this Collider interacts with.
		/// </summary>
		public Category CollidesWith
		{
			get { return this.colWith; }
			set
			{
				this.colWith = value;
				if (this.body != null) this.body.CollidesWith = value;
			}
		}
		/// <summary>
		/// [GET / SET] Enumerates all <see cref="ShapeInfo">primitive shapes</see> which this body consists of.
		/// If you modify any of the returned ShapeInfos, be sure to call <see cref="UpdateBody"/> afterwards.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<ShapeInfo> Shapes
		{
			get { return this.shapes; }
			set
			{
				this.SetShapes(value);
			}
		}
		/// <summary>
		/// [GET] Enumerates all <see cref="JointInfo">joints</see> that are connected to this Collider.
		/// If you modify any of the returned ShapeInfos, be sure to call <see cref="UpdateBody"/> afterwards.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<JointInfo> Joints
		{
			get { return this.joints; }
		}
		/// <summary>
		/// [GET] The physical bodys bounding radius.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public float BoundRadius
		{
			get
			{
				if (this.shapes == null || this.shapes.Count == 0) return 0.0f;

				Rect boundRect = this.shapes[0].AABB;
				foreach (ShapeInfo info in this.shapes.Skip(1))
					boundRect = boundRect.ExpandToContain(info.AABB);

				Vector2 scale = this.GameObj.Transform.Scale.Xy;
				return boundRect.Transform(scale).BoundingRadius;
			}
		}

		public Collider()
		{
			// Default shape
			this.AddShape(new CircleShapeInfo(128.0f, Vector2.Zero, 1.0f));
		}

		/// <summary>
		/// Adds a new shape to the Collider
		/// </summary>
		/// <param name="shape"></param>
		public void AddShape(ShapeInfo shape)
		{
			if (shape == null) throw new ArgumentNullException("shape");
			if (this.shapes != null && this.shapes.Contains(shape)) return;

			if (this.shapes == null) this.shapes = new List<ShapeInfo>();
			this.shapes.Add(shape);
			shape.Parent = this;

			if (this.body != null)
			{
				bool wasEnabled = this.body.Enabled;
				if (wasEnabled) this.body.Enabled = false;

				shape.InitFixture(this.body);
				this.UpdateBodyShape();

				if (wasEnabled) this.body.Enabled = true;
			}
		}
		/// <summary>
		/// Removes an existing shape from the Collider.
		/// </summary>
		/// <param name="shape"></param>
		public void RemoveShape(ShapeInfo shape)
		{
			if (shape == null) throw new ArgumentNullException("shape");
			if (this.shapes == null || !this.shapes.Contains(shape)) return;

			this.shapes.Remove(shape);
			shape.Parent = null;

			if (this.body != null)
			{
				shape.DestroyFixture(this.body);
				this.UpdateBodyShape();
			}
		}
		/// <summary>
		/// Removes all existing shapes from the Collider.
		/// </summary>
		public void ClearShapes()
		{
			if (this.shapes == null) return;

			var oldShapes = this.shapes.ToArray();
			this.shapes.Clear();
			foreach (ShapeInfo shape in oldShapes)
			{
				if (this.body != null) shape.DestroyFixture(this.body);
				shape.Parent = null;
			}
			this.UpdateBodyShape();
		}
		/// <summary>
		/// Sets the Colliders shape.
		/// </summary>
		/// <param name="shapes"></param>
		public void SetShapes(IEnumerable<ShapeInfo> shapes)
		{
			if (shapes == null) throw new ArgumentNullException("shapes");

			// Clone shape collection
			ShapeInfo[] cloned = shapes.ToArray();
			for (int i = 0; i < cloned.Length; i++)
				cloned[i] = cloned[i].Clone();
			shapes = cloned;

			// Disable body during shape update
			bool wasEnabled = this.body != null && this.body.Enabled;
			if (wasEnabled) this.body.Enabled = false;
			
			// Destroy old shapes
			if (this.shapes != null)
			{
				var oldShapes = this.shapes.ToArray();
				this.shapes.Clear();
				foreach (ShapeInfo shape in oldShapes)
				{
					if (this.body != null) shape.DestroyFixture(this.body);
					shape.Parent = null;
				}
			}

			// Generate new shapes
			if (this.shapes == null) this.shapes = new List<ShapeInfo>();
			foreach (ShapeInfo shape in shapes)
			{
				if (shape == null) continue;

				this.shapes.Add(shape);
				shape.Parent = this;

				if (this.body != null) shape.InitFixture(this.body);
			}
			this.UpdateBodyShape();

			// Reactivate body after shape update
			if (wasEnabled) this.body.Enabled = true;
		}
		
		/// <summary>
		/// Removes an existing joint from the Collider.
		/// </summary>
		/// <param name="joint"></param>
		public void RemoveJoint(JointInfo joint)
		{
			if (joint == null) throw new ArgumentNullException("joint");
			if (this.joints == null || !this.joints.Contains(joint)) return;

			this.joints.Remove(joint);
			if (joint.ColliderA == this)
				joint.ColliderA = null;
			else
				joint.ColliderB = null;

			joint.DestroyJoint();
			if (this.body != null) this.UpdateBodyJoints();
		}
		/// <summary>
		/// Adds a new joint to the Collider
		/// </summary>
		/// <param name="joint"></param>
		public void AddJoint(JointInfo joint, Collider other = null)
		{
			if (joint == null) throw new ArgumentNullException("joint");
			if (this.joints != null && this.joints.Contains(joint)) return;

			if (this.joints == null) this.joints = new List<JointInfo>();
			this.joints.Add(joint);

			if (joint.ColliderA == null || joint.ColliderA == this)
			{
				joint.ColliderA = this;
				joint.ColliderB = other;

				if (this.body != null && (other == null || other.body != null))
				{
					joint.InitJoint(this.body, other != null ? other.body : null);
					this.UpdateBodyJoints();
				}
			}
			else
			{
				joint.ColliderB = this;
				joint.ColliderA = other;

				if (this.body != null && (other == null || other.body != null))
				{
					joint.InitJoint(other != null ? other.body : null, this.body);
					this.UpdateBodyJoints();
				}
			}
		}
		/// <summary>
		/// Removes all existing joints from the Collider.
		/// </summary>
		public void ClearJoints()
		{
			if (this.joints == null) return;

			var oldJoints = this.joints.ToArray();
			this.joints.Clear();
			foreach (JointInfo joint in oldJoints)
			{
				joint.DestroyJoint();
				joint.ColliderA = null;
				joint.ColliderB = null;
			}
			this.UpdateBodyJoints();
		}

		/// <summary>
		/// Applies a Transform-local angular impulse to the object.
		/// </summary>
		/// <param name="angularImpulse"></param>
		public void ApplyLocalImpulse(float angularImpulse)
		{
			if (this.body == null) return;
			this.body.ApplyAngularImpulse(angularImpulse / Time.SPFMult);
		}
		/// <summary>
		/// Applies a Transform-local impulse to the objects mass center.
		/// </summary>
		/// <param name="impulse"></param>
		public void ApplyLocalImpulse(Vector2 impulse)
		{
			if (this.body == null) return;
			this.ApplyWorldImpulse(this.gameobj.Transform.GetWorldVector(new Vector3(impulse)).Xy, this.body.LocalCenter);
		}
		/// <summary>
		/// Applies a Transform-local impulse to the specified point.
		/// </summary>
		/// <param name="impulse"></param>
		/// <param name="applyAt"></param>
		public void ApplyLocalImpulse(Vector2 impulse, Vector2 applyAt)
		{
			this.ApplyWorldImpulse(
				this.gameobj.Transform.GetWorldVector(new Vector3(impulse)).Xy,
				this.gameobj.Transform.GetWorldPoint(new Vector3(applyAt)).Xy);
		}
		/// <summary>
		/// Applies a world impulse to the objects mass center.
		/// </summary>
		/// <param name="impulse"></param>
		public void ApplyWorldImpulse(Vector2 impulse)
		{
			if (this.body == null) return;
			this.body.ApplyLinearImpulse(
				PhysicsConvert.ToPhysicalUnit(impulse) / Time.SPFMult, 
				this.body.GetWorldPoint(this.body.LocalCenter));
		}
		/// <summary>
		/// Applies a world impulse to the specified point.
		/// </summary>
		/// <param name="impulse"></param>
		/// <param name="applyAt"></param>
		public void ApplyWorldImpulse(Vector2 impulse, Vector2 applyAt)
		{
			if (this.body == null) return;
			this.body.ApplyLinearImpulse(PhysicsConvert.ToPhysicalUnit(impulse) / Time.SPFMult, PhysicsConvert.ToPhysicalUnit(applyAt));
		}
		
		/// <summary>
		/// Applies a Transform-local angular force to the object.
		/// </summary>
		/// <param name="angularForce"></param>
		public void ApplyLocalForce(float angularForce)
		{
			if (this.body == null) return;
			this.body.ApplyTorque(angularForce / Time.SPFMult);
		}
		/// <summary>
		/// Applies a Transform-local force to the objects mass center.
		/// </summary>
		/// <param name="force"></param>
		public void ApplyLocalForce(Vector2 force)
		{
			if (this.body == null) return;
			this.ApplyWorldForce(this.gameobj.Transform.GetWorldVector(new Vector3(force)).Xy, this.body.LocalCenter);
		}
		/// <summary>
		/// Applies a Transform-local force to the specified point.
		/// </summary>
		/// <param name="force"></param>
		/// <param name="applyAt"></param>
		public void ApplyLocalForce(Vector2 force, Vector2 applyAt)
		{
			this.ApplyWorldForce(
				this.gameobj.Transform.GetWorldVector(new Vector3(force)).Xy,
				this.gameobj.Transform.GetWorldPoint(new Vector3(applyAt)).Xy);
		}
		/// <summary>
		/// Applies a world force to the objects mass center.
		/// </summary>
		/// <param name="force"></param>
		public void ApplyWorldForce(Vector2 force)
		{
			if (this.body == null) return;
			this.body.ApplyForce(
				PhysicsConvert.ToPhysicalUnit(force) / Time.SPFMult, 
				this.body.GetWorldPoint(this.body.LocalCenter));
		}
		/// <summary>
		/// Applies a world force to the specified point.
		/// </summary>
		/// <param name="force"></param>
		/// <param name="applyAt"></param>
		public void ApplyWorldForce(Vector2 force, Vector2 applyAt)
		{
			if (this.body == null) return;
			this.body.ApplyForce(PhysicsConvert.ToPhysicalUnit(force) / Time.SPFMult, PhysicsConvert.ToPhysicalUnit(applyAt));
		}

		private void UpdateBodyShape()
		{
			if (this.body == null) return;

			foreach (ShapeInfo info in this.shapes) info.UpdateFixture();
			this.body.CollisionCategories = this.colCat;
			this.body.CollidesWith = this.colWith;
			this.body.ResetMassData();

			this.AwakeBody();
		}
		private void CleanupBody()
		{
			if (this.body == null) return;

			this.body.Collision -= this.body_OnCollision;
			this.body.Separation -= this.body_OnSeparation;
			this.body.PostSolve -= this.body_PostSolve;

			this.body.Dispose();
			this.body = null;
		}
		private Body CreateBody()
		{
			Body b = new Body(Scene.CurrentPhysics, this);
			if (this.shapes != null)
			{
				foreach (ShapeInfo s in this.shapes) s.InitFixture(b);
			}
			return b;
		}
		private void InitBody()
		{
			if (this.body != null) this.CleanupBody();
			Transform t = this.GameObj != null ? this.GameObj.Transform : null;

			this.body = this.CreateBody();
			this.UpdateBodyShape();

			this.body.BodyType = (this.bodyType == BodyType.Static ? FarseerPhysics.Dynamics.BodyType.Static : FarseerPhysics.Dynamics.BodyType.Dynamic);
			this.body.LinearDamping = this.linearDamp;
			this.body.AngularDamping = this.angularDamp;
			this.body.FixedRotation = this.fixedAngle;
			this.body.IgnoreGravity = this.ignoreGravity;
			this.body.IsBullet = this.continous;
			this.body.CollisionCategories = this.colCat;
			this.body.CollidesWith = this.colWith;

			if (t != null)
			{
				this.body.SetTransform(PhysicsConvert.ToPhysicalUnit(t.Pos.Xy), t.Angle);
				this.body.LinearVelocity = PhysicsConvert.ToPhysicalUnit(t.Vel.Xy) / Time.SPFMult;
				this.body.AngularVelocity = t.AngleVel / Time.SPFMult;
			}

			this.body.Collision += this.body_OnCollision;
			this.body.Separation += this.body_OnSeparation;
			this.body.PostSolve += this.body_PostSolve;

			//var testJoint = JointFactory.CreateFixedAngleJoint(Scene.CurrentPhysics, this.body);
			//var testJoint = JointFactory.CreateFixedDistanceJoint(Scene.CurrentPhysics, this.body, Vector2.Zero, Vector2.Zero);
			//var testJoint = JointFactory.CreateFixedFrictionJoint(Scene.CurrentPhysics, this.body, Vector2.Zero);
			//var testJoint = JointFactory.CreateFixedPrismaticJoint(Scene.CurrentPhysics, this.body, Vector2.Zero, Vector2.UnitX);
			//var testJoint = JointFactory.CreateFixedRevoluteJoint(Scene.CurrentPhysics, this.body, Vector2.Zero, Vector2.Zero);
			
			// etc.
			//var testJoint = JointFactory.CreateAngleJoint(
		}

		private void UpdateBodyJoints()
		{
			foreach (JointInfo info in this.joints)
			{
				if (!info.IsInitialized) continue;
				info.UpdateJoint();
			}
		}
		private void CleanupJoints()
		{
			foreach (JointInfo j in this.joints)
				j.DestroyJoint();
		}
		private void InitJoints()
		{
			if (this.joints == null) return;
			foreach (JointInfo j in this.joints)
			{
				if (j.ColliderA != null && j.ColliderA.body == null) j.ColliderA.InitBody();
				if (j.ColliderB != null && j.ColliderB.body == null) j.ColliderB.InitBody();
				j.InitJoint(j.ColliderA.body, j.ColliderB != null ? j.ColliderB.body : null);
			}
			this.UpdateBodyJoints();
		}

		private void Initialize()
		{
			if (this.initialized) return;

			this.InitBody();
			this.GameObj.Transform.RegisterExternalUpdater(this);

			this.initialized = true;

			this.InitJoints();
		}
		private void Shutdown()
		{
			if (!this.initialized) return;
			
			this.CleanupJoints();
			this.CleanupBody();
			this.GameObj.Transform.UnregisterExternalUpdater(this);

			this.initialized = false;
		}
		private void CleanupInvalidJoints()
		{
			for (int i = this.joints.Count - 1; i >= 0; i--)
			{
				JointInfo joint = this.joints[i];
				if (joint.ColliderA != null && joint.ColliderA.Disposed ||
					joint.ColliderB != null && joint.ColliderB.Disposed)
				{
					joint.DestroyJoint();
					this.RemoveJoint(joint);
				}
			}
		}

		/// <summary>
		/// Awakes the body if it has been in a resting state that is now being left, such as
		/// when changing physical properties at runtime. You usually don't need to call this.
		/// </summary>
		public void AwakeBody()
		{
			if (this.body != null) this.body.Awake = true;
		}
		/// <summary>
		/// Updates the Colliders internal body shape and joints.
		/// </summary>
		public void UpdateBody()
		{
			this.UpdateBodyShape();
			this.UpdateBodyJoints();
		}

		/// <summary>
		/// Performs a physical picking operation and returns the <see cref="ShapeInfo">shape</see> in which
		/// the specified world coordinate is located in.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <returns></returns>
		public ShapeInfo PickShape(Vector2 worldCoord)
		{
			if (this.body == null) return null;
			Vector2 fsWorldCoord = PhysicsConvert.ToPhysicalUnit(worldCoord);

			for (int i = 0; i < this.shapes.Count; i++)
			{
				Fixture f = this.body.FixtureList[i];
				if (f.TestPoint(ref fsWorldCoord)) return this.shapes[i];
			}
			return null;
		}
		/// <summary>
		/// Performs a physical picking operation and returns the <see cref="ShapeInfo">shapes</see> that
		/// intersect the specified world coordinate.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <returns></returns>
		public List<ShapeInfo> PickShapes(Vector2 worldCoord)
		{
			if (this.body == null) return new List<ShapeInfo>();

			List<ShapeInfo> picked = new List<ShapeInfo>();
			Vector2 fsWorldCoord = PhysicsConvert.ToPhysicalUnit(worldCoord);

			for (int i = 0; i < this.shapes.Count; i++)
			{
				Fixture f = this.body.FixtureList[i];
				if (f.TestPoint(ref fsWorldCoord)) picked.Add(this.shapes[i]);
			}

			return picked;
		}
		/// <summary>
		/// Performs a physical picking operation and returns the <see cref="ShapeInfo">shapes</see> that
		/// intersect the specified world coordinate area.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public List<ShapeInfo> PickShapes(Vector2 worldCoord, Vector2 size)
		{
			if (this.body == null) return new List<ShapeInfo>();
			Vector2 fsWorldCoord = PhysicsConvert.ToPhysicalUnit(worldCoord);
			FarseerPhysics.Collision.AABB fsWorldAABB = new FarseerPhysics.Collision.AABB(fsWorldCoord, PhysicsConvert.ToPhysicalUnit(worldCoord + size));

			List<ShapeInfo> picked = new List<ShapeInfo>();
			for (int i = 0; i < this.shapes.Count; i++)
			{
				Fixture f = this.body.FixtureList[i];

				FarseerPhysics.Collision.AABB fAABB;
				FarseerPhysics.Common.Transform transform;
				this.body.GetTransform(out transform);
				f.Shape.ComputeAABB(out fAABB, ref transform, 0);
				
				if (fsWorldAABB.Contains(ref fAABB))
				{
					picked.Add(this.shapes[i]);
					continue;
				}
				else if (!FarseerPhysics.Collision.AABB.TestOverlap(ref fsWorldAABB, ref fAABB))
					continue;

				FarseerPhysics.Collision.AABB fAABBIntersect;
				fAABBIntersect.LowerBound = Vector2.ComponentMax(fAABB.LowerBound, fsWorldAABB.LowerBound);
				fAABBIntersect.UpperBound = Vector2.ComponentMin(fAABB.UpperBound, fsWorldAABB.UpperBound);

				Vector2 fsWorldCoordStep = PhysicsConvert.ToPhysicalUnit(new Vector2(MathF.Max(this.shapes[i].AABB.w, 1.0f), MathF.Max(this.shapes[i].AABB.h, 1.0f)) * 0.05f);
				Vector2 fsTemp = fAABBIntersect.LowerBound;
				do
				{
					if (f.TestPoint(ref fsTemp))
					{
						picked.Add(this.shapes[i]);
						break;
					}

					fsTemp.X += fsWorldCoordStep.X;
					if (fsTemp.X > fAABBIntersect.UpperBound.X)
					{
						fsTemp.X = fAABBIntersect.LowerBound.X;
						fsTemp.Y += fsWorldCoordStep.Y;
					}
					if (fsTemp.Y > fAABBIntersect.UpperBound.Y) break;
				} while (true);
			}

			return picked;
		}
		
		private bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			this.eventBuffer.Add(new ColEvent(ColEvent.EventType.Collision, fixtureA, fixtureB, null));
			return true;
		}
		private void body_OnSeparation(Fixture fixtureA, Fixture fixtureB)
		{
			this.eventBuffer.Add(new ColEvent(ColEvent.EventType.Separation, fixtureA, fixtureB, null));
		}
        private void body_PostSolve(Contact contact, ContactConstraint impulse)
        {
            int count = contact.Manifold.PointCount;
            for (int i = 0; i < count; ++i)
            {
				if (impulse.Points[i].NormalImpulse > 0.0f || impulse.Points[i].TangentImpulse > 0.0f)
				{
					CollisionData colData = new CollisionData(this.body, impulse, i);
					if (contact.FixtureA.Body == this.body)
						this.eventBuffer.Add(new ColEvent(ColEvent.EventType.PostSolve, contact.FixtureA, contact.FixtureB, colData));
					else
						this.eventBuffer.Add(new ColEvent(ColEvent.EventType.PostSolve, contact.FixtureB, contact.FixtureA, colData));
				}
            }
        }
		
		void ICmpUpdatable.OnUpdate()
		{
			this.CleanupInvalidJoints();

			foreach (ColEvent e in this.eventBuffer)
			{
				// Ignore disposed fixtures / bodies
				if (e.FixtureA.Body == null) continue;
				if (e.FixtureB.Body == null) continue;

				ColliderCollisionEventArgs args = new ColliderCollisionEventArgs(
					(e.FixtureB.Body.UserData as Collider).GameObj,
					e.Data,
					e.FixtureA.UserData as ShapeInfo,
					e.FixtureB.UserData as ShapeInfo);

				if (e.Type == ColEvent.EventType.Collision)
					this.gameobj.NotifyCollisionBegin(this, args);
				else if (e.Type == ColEvent.EventType.Separation)
					this.gameobj.NotifyCollisionEnd(this, args);
				else if (e.Type == ColEvent.EventType.PostSolve)
					this.gameobj.NotifyCollisionSolve(this, args);
			}
			this.eventBuffer.Clear();
		}

		bool ITransformUpdater.IgnoreParent
		{
			get { return this.bodyType == BodyType.Dynamic; }
		}
		void ITransformUpdater.UpdateTransform(Transform t)
		{
			if (this.bodyType == BodyType.Dynamic)
			{
				t.SetTransform(
					new Vector3(
						PhysicsConvert.ToDualityUnit(this.body.Position.X), 
						PhysicsConvert.ToDualityUnit(this.body.Position.Y), 
						t.Pos.Z + t.Vel.Z * Time.TimeMult),
					new Vector3(
						PhysicsConvert.ToDualityUnit(this.body.LinearVelocity.X * Time.SPFMult), 
						PhysicsConvert.ToDualityUnit(this.body.LinearVelocity.Y * Time.SPFMult), 
						t.Vel.Z),
					t.Scale,
					this.body.Rotation,
					this.body.AngularVelocity * Time.SPFMult);
			}
			else
			{
				if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game && (t.RelativeVel != Vector3.Zero || t.RelativeAngleVel != 0.0f))
				{
				    t.SetRelativeTransform(
				        t.RelativePos + t.RelativeVel * Time.TimeMult,
				        t.RelativeVel,
				        t.RelativeScale,
				        MathF.NormalizeAngle(t.RelativeAngle + t.RelativeAngleVel * Time.TimeMult),
				        t.RelativeAngleVel);
				    (this as ITransformUpdater).OnTransformChanged(t, Transform.DirtyFlags.Pos | Transform.DirtyFlags.Angle);
				}
			}
		}
		void ITransformUpdater.OnTransformChanged(Transform t, Transform.DirtyFlags changes)
		{
			if ((changes & Transform.DirtyFlags.Pos) != Transform.DirtyFlags.None)
				this.body.Position = PhysicsConvert.ToPhysicalUnit(t.Pos.Xy);
			if ((changes & Transform.DirtyFlags.Vel) != Transform.DirtyFlags.None)
				this.body.LinearVelocity = PhysicsConvert.ToPhysicalUnit(t.Vel.Xy) / Time.SPFMult;
			if ((changes & Transform.DirtyFlags.Angle) != Transform.DirtyFlags.None)
				this.body.Rotation = t.Angle;
			if ((changes & Transform.DirtyFlags.AngleVel) != Transform.DirtyFlags.None)
				this.body.AngularVelocity = t.AngleVel / Time.SPFMult;
			if ((changes & Transform.DirtyFlags.Scale) != Transform.DirtyFlags.None)
				this.UpdateBodyShape();

			if (changes != Transform.DirtyFlags.None)
			{
				// Update joints
				this.CleanupInvalidJoints();
				foreach (JointInfo joint in this.joints)
				{
					joint.UpdateFromWorld();
					joint.UpdateJoint();
				}

				this.body.Awake = true;
			}
		}
		void ICmpInitializable.OnInit(InitContext context)
		{
			if (context == InitContext.Activate)
				this.Initialize();
			else if (context == InitContext.Loaded)
				this.CleanupInvalidJoints();
		}
		void ICmpInitializable.OnShutdown(ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
				this.Shutdown();
			else if (context == ShutdownContext.Saving)
				this.CleanupInvalidJoints();
		}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Collider c = target as Collider;

			bool wasInitialized = c.initialized;
			if (wasInitialized) c.Shutdown();

			c.bodyType = this.bodyType;
			c.linearDamp = this.linearDamp;
			c.angularDamp = this.angularDamp;
			c.fixedAngle = this.fixedAngle;
			c.ignoreGravity = this.ignoreGravity;
			c.continous = this.continous;
			c.colCat = this.colCat;

			// Discard old shape list and set new.
			c.ClearShapes();
			if (this.shapes != null) c.SetShapes(this.shapes);

			// Do not copy any joints.
			//c.ClearJoints();

			if (wasInitialized) c.Initialize();
		}
		
		/// <summary>
		/// Performs a global physical picking operation and returns the <see cref="ShapeInfo">shape</see> in which
		/// the specified world coordinate is located in.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <returns></returns>
		public static ShapeInfo PickShapeGlobal(Vector2 worldCoord)
		{
			Vector2 fsWorldCoord = PhysicsConvert.ToPhysicalUnit(worldCoord);
			Fixture f = Scene.CurrentPhysics.TestPoint(fsWorldCoord);

			return f != null && f.UserData is ShapeInfo ? (f.UserData as ShapeInfo) : null;
		}
		/// <summary>
		/// Performs a global physical picking operation and returns the <see cref="ShapeInfo">shapes</see> that
		/// intersect the specified world coordinate.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <returns></returns>
		public static List<ShapeInfo> PickShapesGlobal(Vector2 worldCoord)
		{
			Vector2 fsWorldCoord = PhysicsConvert.ToPhysicalUnit(worldCoord);
			List<Fixture> fixtureList = Scene.CurrentPhysics.TestPointAll(fsWorldCoord);
			return new List<ShapeInfo>(fixtureList.Where(f => f != null && f.UserData is ShapeInfo).Select(f => f.UserData as ShapeInfo));
		}
		/// <summary>
		/// Performs a global physical picking operation and returns the <see cref="ShapeInfo">shapes</see> that
		/// intersect the specified world coordinate area.
		/// </summary>
		/// <param name="worldCoord"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static List<ShapeInfo> PickShapesGlobal(Vector2 worldCoord, Vector2 size)
		{
			List<ShapeInfo> picked = new List<ShapeInfo>();

			Collider[] colliderArray = Scene.Current.ActiveObjects.GetComponents<Collider>().ToArray();
			foreach (Collider c in colliderArray)
				picked.AddRange(c.PickShapes(worldCoord, size));

			return picked;
		}
	}

	public class ColliderCollisionEventArgs : CollisionEventArgs
	{
		private	Collider.ShapeInfo	colShapeA;
		private	Collider.ShapeInfo	colShapeB;

		public Collider.ShapeInfo MyCollideShape
		{
			get { return this.colShapeA; }
		}
		public Collider.ShapeInfo OtherCollideShape
		{
			get { return this.colShapeB; }
		}

		public ColliderCollisionEventArgs(GameObject obj, CollisionData data, Collider.ShapeInfo shapeA, Collider.ShapeInfo shapeB) : base(obj, data)
		{
			this.colShapeA = shapeA;
			this.colShapeB = shapeB;
		}
	}
}
