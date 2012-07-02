using System;

using OpenTK;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

using Duality.EditorHints;
using Duality.Resources;

namespace Duality.Components.Physics
{
	/// <summary>
	/// Constrains the Collider to a fixed angle
	/// </summary>
	[Serializable]
	public sealed class FixedAngleJointInfo : JointInfo
	{
		private	float	angle	= 0.0f;


		public override bool DualJoint
		{
			get { return false; }
		}
		/// <summary>
		/// [GET / SET] The Colliders target angle.
		/// </summary>
		public float TargetAngle
		{
			get { return this.angle; }
			set { this.angle = value; this.UpdateJoint(); }
		}


		public FixedAngleJointInfo() : this(0.0f) {}
		public FixedAngleJointInfo(float angle)
		{
			this.angle = angle;
		}

		protected override Joint CreateJoint(Body bodyA, Body bodyB)
		{
			return bodyA != null ? JointFactory.CreateFixedAngleJoint(Scene.PhysicsWorld, bodyA) : null;
		}
		internal override void UpdateJoint()
		{
			base.UpdateJoint();
			if (this.joint == null) return;

			FixedAngleJoint j = this.joint as FixedAngleJoint;
			j.TargetAngle = this.angle;
		}

		protected override void CopyTo(JointInfo target)
		{
			base.CopyTo(target);
			FixedAngleJointInfo c = target as FixedAngleJointInfo;
			c.angle = this.angle;
		}
	}
}
