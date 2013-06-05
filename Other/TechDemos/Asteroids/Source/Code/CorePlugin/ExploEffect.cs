using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components.Renderers;
using Duality.Components;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
	[RequiredComponent(typeof(AnimSpriteRenderer))]
	[RequiredComponent(typeof(SoundEmitter))]
    public class ExploEffect : Component, ICmpUpdatable, ICmpInitializable
    {
		private	float	intensity	= 1.0f;
		public float Intensity
		{
			get { return this.intensity; }
			set { this.intensity = value; }
		}

		void ICmpUpdatable.OnUpdate()
		{
			AnimSpriteRenderer r = this.GameObj.Renderer as AnimSpriteRenderer;
			SoundEmitter s = this.GameObj.GetComponent<SoundEmitter>();

			if (!r.IsAnimationRunning && s.Sources.All(src => src.Instance == null || src.Instance.Disposed)) this.GameObj.DisposeLater();
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				AnimSpriteRenderer r = this.GameObj.Renderer as AnimSpriteRenderer;
				SoundEmitter s = this.GameObj.GetComponent<SoundEmitter>();
				Transform t = this.GameObj.Transform;

				r.AnimDuration = MathF.RoundToInt(0.4f * r.AnimDuration * MathF.Rnd.NextFloat(0.8f, 1.25f) * MathF.Sqrt(intensity));
				t.RelativeScale *= MathF.Sqrt(intensity);
				t.RelativeAngle = MathF.Rnd.NextFloat(MathF.RadAngle360);
				t.RelativeVel = new Vector3(MathF.Rnd.NextVector2(1.0f));
				
				ContentRef<Sound> soundRes = ContentRef<Sound>.Null;
				switch (MathF.Rnd.Next(5))
				{
					case 0:	soundRes = GameRes.Data.Sound.Explo1_Sound; break;
					case 1:	soundRes = GameRes.Data.Sound.Explo2_Sound; break;
					case 2:	soundRes = GameRes.Data.Sound.Explo3_Sound; break;
					case 3:	soundRes = GameRes.Data.Sound.Explo4_Sound; break;
					case 4:	soundRes = GameRes.Data.Sound.Explo5_Sound; break;
				}
				SoundEmitter.Source source = new SoundEmitter.Source(soundRes, false);
				source.Volume = MathF.Rnd.NextFloat(0.9f, 1.15f) * MathF.Sqrt(intensity);
				source.Pitch = MathF.Rnd.NextFloat(0.8f, 1.25f) / MathF.Sqrt(intensity);
				source.Paused = false;
				s.Sources.Add(source);
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context) {}


		public static void Create(Vector3 pos, float intensity)
		{
			GameObject explo = new GameObject(GameRes.Data.Prefabs.ExploEffect_Prefab);
			explo.GetComponent<ExploEffect>().Intensity = intensity;
			explo.Transform.Pos = pos;
			Scene.Current.RegisterObj(explo);
		}
	}
}
