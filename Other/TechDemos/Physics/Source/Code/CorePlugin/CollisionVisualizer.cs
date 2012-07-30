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
using Duality.Components.Renderers;
using Duality.Components.Physics;

namespace PhysicsTestbed
{
	/// <summary>
	/// This Component visualizes collisions.
	/// </summary>
	[Serializable]
	public class CollisionVisualizer : Component, ICmpCollisionListener
	{
		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args)
		{
			// We'll watch for RigidBody collisions
			RigidBodyCollisionEventArgs bodyArgs = args as RigidBodyCollisionEventArgs;
			if (bodyArgs == null) return;
		}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args)
		{
			// We'll watch for RigidBody collisions
			RigidBodyCollisionEventArgs bodyArgs = args as RigidBodyCollisionEventArgs;
			if (bodyArgs == null) return;
		}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args)
		{
			// We'll watch for RigidBody collisions
			RigidBodyCollisionEventArgs bodyArgs = args as RigidBodyCollisionEventArgs;
			if (bodyArgs == null) return;

			if (bodyArgs.CollisionData.NormalImpulse > 0.5f)
			{
				// Create a hit particle
				GameObject hitParticle = GameRes.Data.Misc.HitParticle_Prefab.Res.Instantiate();
				hitParticle.Transform.Pos = this.GameObj.Transform.Pos + new Vector3(bodyArgs.CollisionData.Pos);
				hitParticle.Transform.Scale = Vector3.One * MathF.Sqrt(bodyArgs.CollisionData.NormalImpulse);
				hitParticle.GetComponent<SpriteRenderer>().ColorTint = new ColorRgba(255, 128, 128);
				Scene.Current.RegisterObj(hitParticle);
			}
		}
	}
}
