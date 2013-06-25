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
	public enum PowerupType
	{
		None,

		Blue,
		Green,
		Random,

		FrontCannon,
		DiagonalCannon,
		SideCannon,

		KillAll
	}

	[Serializable]
    public class Powerup : Component, ICmpInitializable, ICmpUpdatable
    {
		private	static	List<Powerup>	instances		= new List<Powerup>();
		public static IEnumerable<Powerup> Instances
		{
			get { return instances; }
		}


		private	PowerupType	type;
		public PowerupType Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
				instances.Add(this);
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
				instances.Remove(this);
		}
		void ICmpUpdatable.OnUpdate()
		{
			if (Player.Instance != null && (this.GameObj.Transform.Pos - Player.Instance.GameObj.Transform.Pos).Length > 1000)
				this.GameObj.DisposeLater();
		}

		public void NotifyCollect()
		{
			switch (this.type)
			{
				case PowerupType.FrontCannon:
				case PowerupType.DiagonalCannon:
				case PowerupType.SideCannon:
					for (int i = MathF.Rnd.Next(4, 8); i >= 0; i--)
						PowerupEffect.Create(this.GameObj.Transform.Pos + MathF.Rnd.NextVector3(50.0f), this.type.ToString(), ColorRgba.Blue + ColorRgba.VeryLightGrey);
					break;
				case PowerupType.KillAll:
					for (int i = MathF.Rnd.Next(4, 8); i >= 0; i--)
						PowerupEffect.Create(this.GameObj.Transform.Pos + MathF.Rnd.NextVector3(50.0f), this.type.ToString(), ColorRgba.Green + ColorRgba.LightGrey);
					break;
			}
			DualityApp.Sound.PlaySound(GameRes.Data.Sound.CollectPowerup_Sound);
			this.GameObj.DisposeLater();
		}


		public static void Create(Vector3 pos, PowerupType type)
		{
			GameObject p;

			if (Player.Instance == null) return;
			type = Player.Instance.PickValidPowerup(type);

			if (type == PowerupType.FrontCannon)
				p = new GameObject(GameRes.Data.Prefabs.Powerups.FrontCannon_Prefab);
			else if (type == PowerupType.DiagonalCannon)
				p = new GameObject(GameRes.Data.Prefabs.Powerups.DiagonalCannon_Prefab);
			else if (type == PowerupType.SideCannon)
				p = new GameObject(GameRes.Data.Prefabs.Powerups.SideCannon_Prefab);
			else if (type == PowerupType.KillAll)
				p = new GameObject(GameRes.Data.Prefabs.Powerups.KillAll_Prefab);
			else
				return;

			p.Transform.Pos = pos;
			p.Transform.Vel = new Vector3(MathF.Rnd.NextVector2(1.0f));
			Scene.Current.RegisterObj(p);
		}
	}
}
