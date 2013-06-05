using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components.Renderers;
using Duality.Components;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
	[RequiredComponent(typeof(TextRenderer))]
    public class PowerupEffect : Component, ICmpUpdatable, ICmpInitializable
    {
		private float		fade;
		private	ColorRgba	baseColor;

		public string Text
		{
			get { return this.GameObj.GetComponent<TextRenderer>().Text.SourceText; }
			set 
			{ 
				this.GameObj.GetComponent<TextRenderer>().Text.SourceText = value;
			}
		}

		void ICmpUpdatable.OnUpdate()
		{
			TextRenderer r = this.GameObj.Renderer as TextRenderer;

			this.fade *= MathF.Pow(0.975f, Time.TimeMult);
			this.GameObj.Transform.RelativeScale += Vector3.One * (0.0025f * Time.TimeMult);

			r.ColorTint = this.baseColor * new ColorRgba(1.0f, this.fade);

			if (this.fade <= 0.005f) this.GameObj.DisposeLater();
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				this.fade = 1.0f;

				TextRenderer r = this.GameObj.Renderer as TextRenderer;
				r.ColorTint = r.ColorTint.WithAlpha(MathF.Rnd.NextFloat(0.125f, 1.0f));
				this.baseColor = r.ColorTint;

				float scale = MathF.Rnd.NextFloat(1.0f, 3.0f) * MathF.Rnd.NextFloat(0.5f, 1.0f);
				this.GameObj.Transform.RelativeScale = Vector3.One * scale;
				this.GameObj.Transform.RelativeVel += MathF.Rnd.NextVector3(1.0f / scale);
				this.GameObj.Transform.RelativeAngleVel += MathF.Rnd.NextFloat(-0.005f, 0.005f) / scale;
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context) {}


		public static void Create(Vector3 pos, string text, ColorRgba color)
		{
			GameObject pe = new GameObject(GameRes.Data.Prefabs.PowerupEffect_Prefab);
			pe.GetComponent<PowerupEffect>().Text = text;
			pe.GetComponent<TextRenderer>().ColorTint = color;
			pe.Transform.Pos = pos;
			Scene.Current.RegisterObj(pe);
		}
	}
}
