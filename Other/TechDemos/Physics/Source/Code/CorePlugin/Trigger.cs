using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Input;

using Duality;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Physics;

namespace PhysicsTestbed
{
	/// <summary>
	/// A simple Component that reacts to collisions.
	/// </summary>
	[Serializable]
	public class Trigger : Component, ICmpCollisionListener
	{
		private	int		collisionCount	= 0;
		private	bool	sensorOnly		= true;

		/// <summary>
		/// [GET] Returns whether or not the Trigger is currently triggered.
		/// </summary>
		public bool Triggered
		{
			get { return this.collisionCount > 0; }
		}
		/// <summary>
		/// [GET / SET] Specifies whether the Trigger reacty only to its own sensor shapes.
		/// </summary>
		public bool SensorsOnly
		{
			get { return this.sensorOnly; }
			set { this.sensorOnly = value; }
		}

		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args)
		{
			// We'll watch for RigidBody collisions
			RigidBodyCollisionEventArgs bodyArgs = args as RigidBodyCollisionEventArgs;
			if (bodyArgs == null) return;

			if (this.sensorOnly && !bodyArgs.MyShape.IsSensor) return;
			this.collisionCount++;
		}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args)
		{
			// We'll watch for RigidBody collisions
			RigidBodyCollisionEventArgs bodyArgs = args as RigidBodyCollisionEventArgs;
			if (bodyArgs == null) return;

			if (this.sensorOnly && !bodyArgs.MyShape.IsSensor) return;
			this.collisionCount--;
		}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args) {}
	}
}
