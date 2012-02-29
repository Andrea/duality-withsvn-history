using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
    public class Projectile : Component, ICmpUpdatable, ICmpCollisionListener
    {
		private	float	dmg;

		public float Damage
		{
			get { return this.dmg; }
			set { this.dmg = value; }
		}
		
		void ICmpUpdatable.OnUpdate()
		{
			if (Player.Instance != null && (this.GameObj.Transform.Pos - Player.Instance.GameObj.Transform.Pos).Length > 1000)
				this.GameObj.DisposeLater();
		}

		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args)
		{
			Asteroid asteroid = args.CollideWith.GetComponent<Asteroid>();
			if (asteroid != null)
			{
				asteroid.NotifyHitBy(this);
				args.CollideWith.Collider.ApplyWorldImpulse(this.GameObj.Transform.Vel.Xy.Normalized * 0.01f, this.GameObj.Transform.Pos.Xy);
				SoundInstance sound = DualityApp.Sound.PlaySound3D(GameRes.Data.Sound.HitWeapon_Sound, this.GameObj.Transform.Pos);
				sound.Pitch = MathF.Rnd.NextFloat(0.6f, 1.5f);
				this.GameObj.DisposeLater();
			}
		}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args) {}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args) {}
	}
}
