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
    public class MenuSceneController : Component, ICmpInitializable, ICmpUpdatable
    {
		private	GameObject	mainCamObj;

		void ICmpUpdatable.OnUpdate()
		{
			// Make camera move
			if (this.mainCamObj != null)
			{
				this.mainCamObj.Transform.Vel = new Vector3(0.0f, 0.0f, 0.25f * MathF.Sin(0.25f * Time.GameTimer / 1000.0f));
				this.mainCamObj.Transform.AngleVel = 0.0005f * MathF.Cos(0.125f * Time.GameTimer / 1000.0f);
			}
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				GameRes.Data.LoadAll(); // Preload all data - loading it on-demand might result in freeze-frames for big files
				DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;
				this.mainCamObj = Scene.Current.AllObjects.FirstByName("MainCamera");

				// Setup highscore text
				GameObject highscoreObj = Scene.Current.AllObjects.FirstByName("HighscoreText");
				if (highscoreObj != null && highscoreObj.GetComponent<TextRenderer>() != null)
				{
					string text = "/ac/cBBDDBBFF/f[0]Highscore/n/f[1]";
					var highscore = MetaHelper.QueryHighscore().ToArray();
					bool currentPassed = false;
					if (highscore.Length > 0)
					{
						foreach (var entry in highscore)
						{
							string entryString = entry.Name + new string(' ', 20 - entry.Name.Length - entry.ScoreString.Length) + entry.ScoreString;
							if (entry.Score == Player.Score && !currentPassed)
							{
								text += "/cFFBBBBFF" + entryString + "/cBBDDBBFF/n";
								currentPassed = true;
							}
							else
								text += entryString + "/n";
						}
					}
					else
					{
						text += "/n/n/n/n- empty -";
					}
					TextRenderer r = highscoreObj.GetComponent<TextRenderer>();
					r.Text.SourceText = text;
					r.UpdateMetrics();
				}
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
				DualityApp.Keyboard.KeyDown -= this.Keyboard_KeyDown;
		}

		private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Scene.Current.Dispose();
				Scene.Current = GameRes.Data.Scenes.GameScene_Scene.Res;
			}
			else if (e.Key == Key.Escape) DualityApp.Terminate();
		}
	}
}
