using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.ColorFormat;
using Duality.VertexFormat;

using OpenTK;
using OpenTK.Input;

namespace GamePlugin
{
	[Serializable]
    public class HUD : Component, ICmpScreenOverlayRenderer, ICmpInitializable
    {
		[NonSerialized]	private	List<HighscoreEntry>	highscore;
		[NonSerialized]	private	string					gameOverEnterName;

		void ICmpScreenOverlayRenderer.DrawOverlay(IDrawDevice device)
		{
			if (!GameRes.Data.Fonts.HUD_Font.IsAvailable) return;
			if (GameSceneController.Instance == null) return;

			Font hudFont = GameRes.Data.Fonts.HUD_Font.Res;
			Font highscoreFont = GameRes.Data.Fonts.Highscore_Font.Res;
			Font debugFont = Font.GenericMonospace10.Res;

			Canvas canvas = new Canvas(device);
			string txt;
			Vector2 size;

			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Add, ColorRgba.Green + ColorRgba.LightGrey));
			canvas.CurrentState.TextFont = hudFont;

			// Draw player score
			txt = string.Format("{0} pts", Player.Score);
			size = hudFont.MeasureText(txt);
			canvas.DrawText(txt, DualityApp.TargetResolution.X - size.X - 10, 10);

			// Draw level
			txt = string.Format("Level {0}", GameSceneController.Instance.CurrentLevel);
			canvas.DrawText(txt, 10, 10);

			// Draw asteroid timer
			txt = string.Format("New asteroid in {0} s", (int)GameSceneController.Instance.AsteroidCountdown / 1000);
			size = hudFont.MeasureText(txt);
			canvas.DrawText(txt, 10, DualityApp.TargetResolution.Y - size.Y - 10);

			canvas.CurrentState.TextFont = debugFont;

			// Draw performance info
			txt = string.Format("Update: {0} ms", MathF.RoundToInt(Performance.UpdateTime).ToString().PadLeft(4));
			size = debugFont.MeasureText(txt);
			canvas.DrawText(txt, 
				DualityApp.TargetResolution.X - size.X - 10, 
				DualityApp.TargetResolution.Y - size.Y - 10 - size.Y * 2);

			txt = string.Format("Render: {0} ms", MathF.RoundToInt(Performance.RenderTime).ToString().PadLeft(4));
			size = debugFont.MeasureText(txt);
			canvas.DrawText(txt, 
				DualityApp.TargetResolution.X - size.X - 10, 
				DualityApp.TargetResolution.Y - size.Y - 10 - size.Y * 1);

			txt = string.Format("Frame:  {0} ms", MathF.RoundToInt(Time.LastDelta).ToString().PadLeft(4));
			size = debugFont.MeasureText(txt);
			canvas.DrawText(txt, 
				DualityApp.TargetResolution.X - size.X - 10, 
				DualityApp.TargetResolution.Y - size.Y - 10);

			if (Player.Instance == null)
			{
				canvas.CurrentState.TextFont = hudFont;

				// Game over text
				txt = "GAME OVER";
				size = hudFont.MeasureText(txt);
				canvas.DrawText(txt, 
					DualityApp.TargetResolution.X / 2 - size.X / 2,
					DualityApp.TargetResolution.Y / 2 - size.Y / 2 - size.Y);

				if (this.highscore.Count() < 10 || Player.Score > this.highscore.Min(entry => entry.Score))
				{
					// Enter name
					txt = "Enter your name:";
					size = hudFont.MeasureText(txt);
					canvas.DrawText(txt, 
						DualityApp.TargetResolution.X / 2 - size.X / 2,
						DualityApp.TargetResolution.Y / 2 - size.Y / 2);

					txt = this.gameOverEnterName ?? "";
					txt = txt + new string('_', 10 - txt.Length);
					size = highscoreFont.MeasureText(txt);
					canvas.DrawText(txt, 
						DualityApp.TargetResolution.X / 2 - size.X / 2,
						DualityApp.TargetResolution.Y / 2 - size.Y / 2 + size.Y);
				}
			}
		}
		bool ICmpScreenOverlayRenderer.IsVisible(IDrawDevice device)
		{
			return true;
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				// Retrieve current highscore list from Duality MetaData
				this.highscore = MetaHelper.QueryHighscore().ToList();

				DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
			{
				DualityApp.Keyboard.KeyDown -= this.Keyboard_KeyDown;

				// Save highscore list to Duality MetaData. Could also use a custom file or modified Duality UserData
				for (int i = 0; i < this.highscore.Count; i++)
				{
					DualityApp.MetaData.WriteValue("Asteroids/Highscore/Entry" + i, this.highscore[i].Name + '\t' + this.highscore[i].ScoreString);
				}
				DualityApp.SaveMetaData();
			}
		}
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			// This method is handling the "You are dead, enter name" keyboard input.
			// It is a very, very primitive implementation, but it should work.
			if (Player.Instance == null && (this.highscore.Count() < 10 || Player.Score > this.highscore.Min(entry => entry.Score)))
			{
				DualityApp.Keyboard.KeyRepeat = true;
				if (this.gameOverEnterName == null) this.gameOverEnterName = "";

				if (e.Key == Key.Enter || e.Key == Key.KeypadEnter)
				{
					// Write to highscore - back to main menu happens because the GameSceneController reacts to it anyway.
					this.highscore.Insert(0, new HighscoreEntry(this.gameOverEnterName, Player.Score));
					this.highscore.StableSort((entry1, entry2) => entry2.Score - entry1.Score);
					if (this.highscore.Count > 10) this.highscore.RemoveAt(10);

					DualityApp.Keyboard.KeyRepeat = false;
				}
				else if (e.Key == Key.Escape)
				{
					// Don't save score, but reset key repeat
					DualityApp.Keyboard.KeyRepeat = false;
				}
				else if (e.Key == Key.BackSpace)
					this.gameOverEnterName = this.gameOverEnterName.Substring(0, MathF.Max(0, this.gameOverEnterName.Length - 1));
				else if (e.Key == Key.Delete)
					this.gameOverEnterName = "";
				else if (this.gameOverEnterName.Length < 10)
				{
					string enumName = e.Key.ToString();
					if (enumName.Length == 1)
						this.gameOverEnterName += (DualityApp.Keyboard[Key.ShiftLeft] || DualityApp.Keyboard[Key.ShiftRight]) ? enumName[0] : char.ToLower(enumName[0]);
					else
					{
						switch (e.Key)
						{
							case Key.Space:		{ this.gameOverEnterName += ' '; break; }
							case Key.Number0:	{ this.gameOverEnterName += '0'; break; }
							case Key.Number1:	{ this.gameOverEnterName += '1'; break; }
							case Key.Number2:	{ this.gameOverEnterName += '2'; break; }
							case Key.Number3:	{ this.gameOverEnterName += '3'; break; }
							case Key.Number4:	{ this.gameOverEnterName += '4'; break; }
							case Key.Number5:	{ this.gameOverEnterName += '5'; break; }
							case Key.Number6:	{ this.gameOverEnterName += '6'; break; }
							case Key.Number7:	{ this.gameOverEnterName += '7'; break; }
							case Key.Number8:	{ this.gameOverEnterName += '8'; break; }
							case Key.Number9:	{ this.gameOverEnterName += '9'; break; }
						}
					}
				}
			}
		}
	}
}
