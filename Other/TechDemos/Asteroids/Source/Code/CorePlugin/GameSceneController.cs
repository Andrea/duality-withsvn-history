using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components.Renderers;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
    public class GameSceneController : Component, ICmpUpdatable, ICmpInitializable
    {
		private static	GameSceneController	instance = null;
		public static GameSceneController Instance
		{
			get { return instance; }
		}

		private int		currentLevel;
		private	float	currentLevelBeginTime;
		private	float	asteroidTimer;

		public int CurrentLevel
		{
			get { return this.currentLevel; }
			set { this.currentLevel = value; }
		}
		public float AsteroidCountdown
		{
			get { return MathF.Max(0.0f, this.asteroidTimer); }
		}

		public GameObject GenerateAsteroid(AsteroidType type)
		{
			GameObject result = null;

			if (type == AsteroidType.Small)
			{
				PowerupType p = Player.Instance != null ? Player.Instance.PickValidPowerup(PowerupType.Random) : PowerupType.None;
				result = new GameObject(p == PowerupType.Blue ? GameRes.Data.Prefabs.AsteroidSmallBlue_Prefab : GameRes.Data.Prefabs.AsteroidSmall_Prefab);
			}
			else if (type == AsteroidType.Medium)
			{
				PowerupType p = Player.Instance != null ? Player.Instance.PickValidPowerup(PowerupType.Random) : PowerupType.None;
				result = new GameObject(p == PowerupType.Blue ? GameRes.Data.Prefabs.AsteroidMediumBlue_Prefab : GameRes.Data.Prefabs.AsteroidMedium_Prefab);
			}
			else if (type == AsteroidType.Big)
			{
				int rnd = MathF.Rnd.Next(3);
				result = new GameObject(rnd == 0 ? GameRes.Data.Prefabs.AsteroidBig1_Prefab : (rnd == 1 ? GameRes.Data.Prefabs.AsteroidBig2_Prefab : GameRes.Data.Prefabs.AsteroidBig3_Prefab));
			}

			result.Transform.Pos = this.FindValidAsteroidPos(250);
			Scene.Current.RegisterObj(result);
			return result;
		}
		public Vector3 FindValidAsteroidPos(float boundRad)
		{
			Vector3 result;
			do
			{
				result = MathF.Rnd.NextVector3(-400.0f, -400.0f, 0.0f, 800.0f, 800.0f, 0.0f);
			} while (Player.Instance != null && (Player.Instance.GameObj.Transform.Pos - result).Length < boundRad);
			return result;
		}

		public void GoToMenu()
		{
			Scene.Current.Dispose();
			Scene.Current = GameRes.Data.Scenes.MenuScene_Scene.Res;
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (Player.Instance != null)
			{
				if (this.currentLevel == 0 || !Asteroid.Instances.Any() || Time.GameTimer - currentLevelBeginTime > 20000.0f)
				{
					Player.Score += this.currentLevel * 100;

					SoundInstance levelSnd = DualityApp.Sound.PlaySound(GameRes.Data.Sound.Level_Sound);
					levelSnd.Pitch = MathF.Sqrt(1.0f + this.currentLevel * 0.125f);
					levelSnd.Volume = MathF.Sqrt(1.0f + this.currentLevel * 0.125f);

					this.currentLevel++;
					this.currentLevelBeginTime = Time.GameTimer;
				}

				this.asteroidTimer -= Time.MsPFMult * Time.TimeMult;
				if (!Asteroid.Instances.Any() || this.asteroidTimer <= 0.0f)
				{
					this.asteroidTimer = 30000.0f / MathF.Pow(this.currentLevel, 0.85f);
					if (this.currentLevel < 3)
						this.GenerateAsteroid(AsteroidType.Small);
					else if (this.currentLevel < 5)
						this.GenerateAsteroid(AsteroidType.Medium);
					else
						this.GenerateAsteroid(AsteroidType.Big);
				}
			}
		}

		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				instance = this;
				Player.Score = 0;
				DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				if (instance == this) instance = null;
				DualityApp.Keyboard.KeyDown -= this.Keyboard_KeyDown;
			}
		}

		private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (Player.Instance == null && (e.Key == Key.Enter || e.Key == Key.KeypadEnter)) this.GoToMenu();
			else if (e.Key == Key.Escape) this.GoToMenu();
		}
	}
}
