using System;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

using Duality.EditorHints;
using Duality.Resources;

namespace Duality.Components
{
#if FALSE // Removed for now. Joints are an experimental feature.
	public partial class Collider
	{
		/// <summary>
		/// Describes a <see cref="Collider"/> joint. Joints limit a Colliders degree of freedom 
		/// by connecting it to fixed world coordinates or other Colliders.
		/// </summary>
		[Serializable]
		public abstract class JointInfo
		{
			[NonSerialized]	
			protected	Joint		joint		= null;
			private		Collider	colA		= null;
			private		Collider	colB		= null;
			private		bool		collide		= false;
			private		bool		enabled		= true;
			private		float		breakPoint	= -1.0f;
			
			[EditorHintFlags(MemberFlags.Invisible)]
			public bool IsInitialized
			{
				get { return this.joint != null; }
			}
			[EditorHintFlags(MemberFlags.Invisible)]
			public Collider ColliderA
			{
				get { return this.colA; }
				set 
				{ 
					if (this.colA != value)
					{
						if (this.colA != null) this.colA.RemoveJoint(this);
						this.colA = value;
						if (this.colA != null) this.colA.AddJoint(this, this.colB);
					}
				}
			}
			[EditorHintFlags(MemberFlags.Invisible)]
			public Collider ColliderB
			{
				get { return this.colB; }
				set 
				{ 
					if (this.colB != value)
					{
						if (this.colB != null) this.colB.RemoveJoint(this);
						this.colB = value;
						if (this.colB != null) this.colB.AddJoint(this, this.colA);
					}
				}
			}
			public bool CollideConnected
			{
				get { return this.collide; }
				set { this.collide = value; this.UpdateJoint(); }
			}
			public bool Enabled
			{
				get { return this.enabled; }
				set { this.enabled = value; this.UpdateJoint(); }
			}
			[EditorHintRange(-1.0f, float.MaxValue)]
			public float BreakPoint
			{
				get { return this.breakPoint; }
				set { this.breakPoint = value; this.UpdateJoint(); }
			}


			internal void DestroyJoint()
			{
				if (this.joint == null) return;
				Scene.CurrentPhysics.RemoveJoint(this.joint);
				this.joint = null;
			}
			internal void InitJoint(Body bodyA, Body bodyB)
			{
				this.joint = this.CreateJoint(bodyA, bodyB);
				this.joint.UserData = this;
			}
			protected abstract Joint CreateJoint(Body bodyA, Body bodyB);
			internal virtual void UpdateJoint()
			{
				this.joint.CollideConnected = this.collide;
				this.joint.Enabled = this.enabled;
				this.joint.Breakpoint = this.breakPoint < 0.0f ? float.MaxValue : this.breakPoint;
			}

			public virtual void UpdateFromWorld()
			{

			}

			protected Vector2 GetFarseerPoint(bool secondCollider, Vector2 dualityPoint)
			{
				Collider c = secondCollider ? this.ColliderB : this.ColliderA;
				if (c == null) return dualityPoint;

				Vector2 scale = Vector2.One;
				if (c.GameObj != null && c.GameObj.Transform != null)
					scale = c.GameObj.Transform.Scale.Xy;
				float uniformScale = scale.Length / MathF.Sqrt(2.0f);

				return PhysicsConvert.ToPhysicalUnit(dualityPoint * scale);
			}
			protected Vector2 GetDualityPoint(bool secondCollider, Vector2 farseerPoint)
			{
				Collider c = secondCollider ? this.ColliderB : this.ColliderA;
				if (c == null) return farseerPoint;

				Vector2 scale = Vector2.One;
				if (c.GameObj != null && c.GameObj.Transform != null)
					scale = c.GameObj.Transform.Scale.Xy;
				float uniformScale = scale.Length / MathF.Sqrt(2.0f);

				return PhysicsConvert.ToDualityUnit(farseerPoint / scale);
			}

			/// <summary>
			/// Copies this JointInfos data to another one. It is assumed that both are of the same type.
			/// </summary>
			/// <param name="target"></param>
			protected virtual void CopyTo(JointInfo target)
			{
				// Don't copy the parents!
				target.collide = this.collide;
				target.enabled = this.enabled;
				target.breakPoint = this.breakPoint;
			}
			/// <summary>
			/// Clones the JointInfo.
			/// </summary>
			/// <returns></returns>
			public JointInfo Clone()
			{
				JointInfo newObj = this.GetType().CreateInstanceOf() as JointInfo;
				this.CopyTo(newObj);
				return newObj;
			}
		}

		[Serializable]
		public sealed class WeldJointInfo : JointInfo
		{
			private Vector2 localPointA	= Vector2.Zero;
			private	Vector2	localPointB	= Vector2.Zero;
			private	float	refAngle	= 0.0f;


			protected override Joint CreateJoint(Body bodyA, Body bodyB)
			{
				return JointFactory.CreateWeldJoint(Scene.CurrentPhysics, bodyA, bodyB, Vector2.Zero);
			}
			internal override void UpdateJoint()
			{
				base.UpdateJoint();
				WeldJoint j = this.joint as WeldJoint;
				j.LocalAnchorA = this.GetFarseerPoint(false, this.localPointA);
				j.LocalAnchorB = this.GetFarseerPoint(true, this.localPointB);
				j.ReferenceAngle = this.refAngle;
			}

			public override void UpdateFromWorld()
			{
				base.UpdateFromWorld();

				this.localPointA = this.ColliderA.GameObj.Transform.GetLocalPoint(Vector3.Zero).Xy;
				this.localPointB = this.ColliderB.GameObj.Transform.GetLocalPoint(Vector3.Zero).Xy;
				this.refAngle = this.ColliderB.GameObj.Transform.Angle - this.ColliderA.GameObj.Transform.Angle;
			}

			protected override void CopyTo(JointInfo target)
			{
				base.CopyTo(target);
				WeldJointInfo c = target as WeldJointInfo;
				c.localPointA = this.localPointA;
				c.localPointB = this.localPointB;
				c.refAngle = this.refAngle;
			}
		}
	}
#endif
}
