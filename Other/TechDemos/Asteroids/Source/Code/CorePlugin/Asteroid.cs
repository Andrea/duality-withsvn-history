using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components.Renderers;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	public enum AsteroidType
	{
		Small,
		Medium,
		Big
	}

	/// <summary>
	/// This is an Asteroid. Imagine some docs here.
	/// </summary>
	[Serializable]
    public class Asteroid : Component, ICmpUpdatable, ICmpInitializable
    {
		private	static	List<Asteroid>	instances		= new List<Asteroid>();
		public static IEnumerable<Asteroid> Instances
		{
			get { return instances; }
		}


		private	float			hp;
		private	AsteroidType	type;
		private	PowerupType		powerup;

		public float Hitpoints
		{
			get { return this.hp; }
			set { this.hp = value; }
		}
		public AsteroidType Type
		{
			get { return this.type; }
			set { this.type = value; }
		}
		public PowerupType ContainedPowerup
		{
			get { return this.powerup; }
			set { this.powerup = value; }
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (this.hp <= 0.0f) this.Die();			
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				this.GameObj.Transform.RelativeAngle = MathF.Rnd.NextFloat(0.0f, MathF.TwoPi);
				this.GameObj.Transform.RelativeAngleVel = MathF.Rnd.NextFloat(-0.01f, 0.01f);
				this.GameObj.Transform.RelativeVel = MathF.Rnd.NextVector3(-1.0f, -1.0f, 0.0f, 2.0f, 2.0f, 0.0f);
				instances.Add(this);
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
			{
				instances.Remove(this);
			}
		}

		public void NotifyHitBy(Projectile p)
		{
			this.hp -= p.Damage;
			SoundInstance sound = DualityApp.Sound.PlaySound3D(GameRes.Data.Sound.HitAsteroid_Sound, this.GameObj.Transform.Pos);
			sound.Pitch = MathF.Rnd.NextFloat(0.8f, 1.25f);
		}
		public void Die()
		{
			if (this.type == AsteroidType.Big)
			{
				ExploEffect.Create(this.GameObj.Transform.Pos, 3.0f);
				
				if (Player.Instance != null)
				{
					for (int i = MathF.Rnd.Next(1, 3); i > 0; i--)
						this.CreateDebris(AsteroidType.Medium, Player.Instance.PickValidPowerup(PowerupType.Random));
					for (int i = MathF.Rnd.Next(1, 3); i > 0; i--)
						this.CreateDebris(AsteroidType.Small, Player.Instance.PickValidPowerup(PowerupType.Random));
				}
				else
				{
					for (int i = MathF.Rnd.Next(1, 3); i > 0; i--)
						this.CreateDebris(AsteroidType.Medium, PowerupType.None);
					for (int i = MathF.Rnd.Next(1, 3); i > 0; i--)
						this.CreateDebris(AsteroidType.Small, PowerupType.None);
				}

				Player.Score += 100;
			}
			else if (this.type == AsteroidType.Medium)
			{
				ExploEffect.Create(this.GameObj.Transform.Pos, 1.5f);
				
				if (Player.Instance != null)
				{
					for (int i = MathF.Rnd.Next(1, 4); i > 0; i--)
						this.CreateDebris(AsteroidType.Small, Player.Instance.PickValidPowerup(PowerupType.Random));
				}
				else
				{
					for (int i = MathF.Rnd.Next(1, 4); i > 0; i--)
						this.CreateDebris(AsteroidType.Small, PowerupType.None);
				}

				Player.Score += 50;
			}
			else if (this.type == AsteroidType.Small)
			{
				ExploEffect.Create(this.GameObj.Transform.Pos, 0.5f);

				Player.Score += 25;
			}

			if (this.powerup != PowerupType.None)
				Powerup.Create(this.GameObj.Transform.Pos, this.powerup);

			this.GameObj.DisposeLater();
		}
		public void CreateDebris(AsteroidType t, PowerupType p)
		{
			GameObject debris;
			if (t == AsteroidType.Medium)
			{
				if (p == PowerupType.Blue)
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidMediumBlue_Prefab);
				else if (p == PowerupType.Green)
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidMediumGreen_Prefab);
				else
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidMedium_Prefab);
			}
			else
			{
				if (p == PowerupType.Blue)
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidSmallBlue_Prefab);
				else if (p == PowerupType.Green)
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidSmallGreen_Prefab);
				else
					debris = new GameObject(GameRes.Data.Prefabs.AsteroidSmall_Prefab);
			}
			debris.Transform.Pos = this.GameObj.Transform.Pos;
			debris.Transform.Vel += this.GameObj.Transform.Vel;

			Scene.Current.RegisterObj(debris);
		}
	}
}
