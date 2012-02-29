using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
    public class Player : Component, ICmpUpdatable, ICmpInitializable, ICmpCollisionListener
    {
		private static	Player	instance = null;
		private	static	int		score;
		private	static	float	lastPowerupTime	= -10000.0f;

		public static Player Instance
		{
			get { return instance; }
		}
		public static int Score
		{
			get { return score; }
			set { score = value; }
		}


		private	float	weaponTimer;
		private	int		lastCtrlDir;
		private	int		powerFront;
		private	int		powerDiagonal;
		private	int		powerSide;

		void ICmpUpdatable.OnUpdate()
		{
			// Turn the ship
			if (DualityApp.Keyboard[Key.Left])
			{
				//this.GameObj.Collider.AngularDamping = 0.0f;
				this.GameObj.Collider.ApplyLocalImpulse((-0.085f - this.GameObj.Transform.RelativeAngleVel) * 0.05f * Time.TimeMult);
				this.lastCtrlDir = -1;
			}
			else if (DualityApp.Keyboard[Key.Right])
			{
				//this.GameObj.Collider.AngularDamping = 0.0f;
				this.GameObj.Collider.ApplyLocalImpulse((0.085f - this.GameObj.Transform.RelativeAngleVel) * 0.05f * Time.TimeMult);
				this.lastCtrlDir = 1;
			}
			else
			{
				this.GameObj.Transform.RelativeAngleVel *= MathF.Pow(0.75f, Time.TimeMult);
				//this.GameObj.Collider.AngularDamping = 25.0f;
			}

			// Accelerate the ship
			if (DualityApp.Keyboard[Key.Up])
			{
				this.GameObj.Collider.ApplyLocalForce(-Vector2.UnitY);
				this.GameObj.GetComponent<SoundEmitter>().Sources[0].Paused = false;
			}
			else if (DualityApp.Keyboard[Key.Down])
			{
				this.GameObj.Collider.ApplyLocalForce(Vector2.UnitY);
				this.GameObj.GetComponent<SoundEmitter>().Sources[0].Paused = false;
			}
			else
			{
				this.GameObj.GetComponent<SoundEmitter>().Sources[0].Paused = true;
			}

			// Fire weapons
			this.weaponTimer += Time.MsPFMult * Time.TimeMult;
			if (DualityApp.Keyboard[Key.Space] && this.weaponTimer > 250.0f)
			{
				// Front Cannons
				for (int i = this.powerFront; i >= 0; i--)
					this.FireFrontCannon(i);

				// Diagonal Cannons
				for (int i = this.powerDiagonal - 1; i >= 0; i--)
					this.FireSideCannon(MathF.RadAngle45, i);

				// Side Cannons
				for (int i = this.powerSide - 1; i >= 0; i--)
					this.FireSideCannon(MathF.RadAngle90, i);

				// Weapon sound
				SoundInstance snd = DualityApp.Sound.PlaySound(GameRes.Data.Sound.FireWeapon_Sound);
				snd.Volume = 0.6f * MathF.Pow(1 + this.powerFront + this.powerDiagonal + this.powerSide, 0.5f);

				this.weaponTimer = 0.0f;
			}
		}

		private void FireFrontCannon(int index)
		{
			int		wTemp = MathF.RoundToInt(this.GameObj.Renderer.BoundRadius * 0.6f * (1.0f - MathF.Pow(1.5f, -this.powerFront)));
			Vector2	posTemp;
			float	speedTemp;

			posTemp = this.GameObj.Transform.Right.Xy * (-(wTemp / 2) + (wTemp * index / MathF.Max(1, this.powerFront)));
			speedTemp = 0.5f * (0.5f - MathF.Abs(((float)index / (float)MathF.Max(1, this.powerFront)) - 0.5f));
			speedTemp = MathF.Sqrt(speedTemp) * 0.5f;
			posTemp += speedTemp * this.GameObj.Transform.Forward.Xy;

			GameObject proj = GameRes.Data.Prefabs.Projectile_Prefab.Res.Instantiate();
			proj.Transform.Pos = this.GameObj.Transform.Pos + Vector3.UnitZ + new Vector3(posTemp);
			proj.Transform.Angle = this.GameObj.Transform.Angle;
			proj.Transform.Vel = this.GameObj.Transform.Vel + this.GameObj.Transform.Forward * (6.5f + speedTemp);
			Scene.Current.RegisterObj(proj);
		}
		private void FireSideCannon(float baseAngle, int index)
		{
			Vector2	posTemp;
			float	speedTemp;
			int		dirTemp;

			float angleTemp = -MathF.RadAngle90 + (float)index * MathF.RadAngle180 / MathF.Max(1, this.powerSide - 1);
			posTemp = new Vector2(0.0f, 10.0f) + 5.0f * new Vector2(MathF.Sin(angleTemp), -MathF.Cos(angleTemp));
			speedTemp = 0.0f;
			MathF.TransformCoord(ref posTemp.X, ref posTemp.Y, this.GameObj.Transform.Angle);
					
			if (index * 2 < (this.powerSide - 1))
				dirTemp = -1;
			else if (index * 2 > (this.powerSide - 1))
				dirTemp = 1;
			else
				dirTemp = -this.lastCtrlDir;

			angleTemp = baseAngle * dirTemp * 0.5f;
			Vector2 velTemp = this.GameObj.Transform.Forward.Xy * (6.5f + speedTemp);
			MathF.TransformCoord(ref velTemp.X, ref velTemp.Y, angleTemp);

			GameObject proj = GameRes.Data.Prefabs.Projectile_Prefab.Res.Instantiate();
			proj.Transform.Pos = this.GameObj.Transform.Pos + Vector3.UnitZ + new Vector3(posTemp);
			proj.Transform.Angle = this.GameObj.Transform.Angle + angleTemp;
			proj.Transform.Vel = this.GameObj.Transform.Vel + new Vector3(velTemp);
			Scene.Current.RegisterObj(proj);
		}
		public PowerupType PickValidPowerup(PowerupType type)
		{
			int powerupCount = 1 + 
				instance.powerDiagonal + instance.powerFront + instance.powerSide + 
				Powerup.Instances.Count() +
				Asteroid.Instances.Count(a => a.ContainedPowerup != PowerupType.None);
			float timeSinceLast = Time.GameTimer - lastPowerupTime;

			if (type == PowerupType.Blue)
			{
				if (this.powerFront + this.powerDiagonal + this.powerSide < 13)
				{
					type = MathF.Rnd.WeightedNext(
						new[] { PowerupType.FrontCannon, PowerupType.DiagonalCannon, PowerupType.SideCannon },
						new[] { 
							this.powerFront < 5 ? 1.5f : 0.0f,
							this.powerDiagonal < 4 ? 1.25f : 0.0f,
							this.powerSide < 4 ? 1.0f : 0.0f});
				}
				else
					type = PowerupType.None;
			}
			else if (type == PowerupType.Green)
			{
				type = PowerupType.KillAll;
			}
			else if (type == PowerupType.Random)
			{
				if (timeSinceLast >= 10000.0f)
				{
					float noneWeight = 2.0f * MathF.Pow(powerupCount, 1.5f);
					if (this.powerFront + this.powerDiagonal + this.powerSide < 13)
					{
						type = MathF.Rnd.WeightedNext(
							new[] { PowerupType.None,	PowerupType.Blue,	PowerupType.Green },
							new[] { 1.0f + noneWeight,	1.0f,				2.0f });
					}
					else
					{
						type = MathF.Rnd.WeightedNext(
							new[] { PowerupType.None,	PowerupType.Green },
							new[] { 1.0f + noneWeight,	3.0f });
					}
				}
				else
					type = PowerupType.None;

				if (type != PowerupType.None) lastPowerupTime = Time.GameTimer;
			}

			return type;
		}
		public void Die()
		{
			// Detach main camera from player object
			Camera mainCam = this.GameObj.GetComponentsInChildren<Camera>().FirstOrDefault();
			mainCam.GameObj.Parent = null;
			mainCam.GameObj.Transform.Vel = Vector3.Zero;
			mainCam.GameObj.Transform.AngleVel = 0.0f;

			// Create explosion
			ExploEffect.Create(this.GameObj.Transform.Pos + MathF.Rnd.NextVector3(50.0f), MathF.Rnd.NextFloat(0.8f, 1.75f));
			ExploEffect.Create(this.GameObj.Transform.Pos + MathF.Rnd.NextVector3(50.0f), MathF.Rnd.NextFloat(0.8f, 1.75f));
			ExploEffect.Create(this.GameObj.Transform.Pos + MathF.Rnd.NextVector3(50.0f), MathF.Rnd.NextFloat(0.8f, 1.75f));

			// Schedule player object for disposal
			this.GameObj.DisposeLater();
		}

		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
				instance = this;
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
				if (instance == this) instance = null;
		}

		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args)
		{
			if (args.CollideWith.GetComponent<Asteroid>() != null)
			{
				// GAME OVER MAN. GAME OVER.
				this.Die();
			}
			else
			{
				Powerup powerup = args.CollideWith.GetComponent<Powerup>();
				if (powerup != null)
				{
					powerup.NotifyCollect();
					switch (powerup.Type)
					{
						case PowerupType.FrontCannon:
							this.powerFront++;
							break;
						case PowerupType.DiagonalCannon:
							this.powerDiagonal++;
							break;
						case PowerupType.SideCannon:
							this.powerSide++;
							break;
						case PowerupType.KillAll:
							Asteroid[] asteroids = Asteroid.Instances.ToArray();
							foreach (Asteroid a in asteroids)
								a.Die();
							break;
					}
				}
			}
		}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args) {}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args) {}
	}
}
