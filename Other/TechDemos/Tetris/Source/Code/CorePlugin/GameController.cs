using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Resources;

using OpenTK;
using OpenTK.Input;

namespace Tetris
{
	[Serializable]
	public class GameController : Component, ICmpUpdatable, ICmpInitializable
	{
		private static GameController instance = null;
		public static GameController Instance
		{
			get { return instance; }
		}

		private	float	beginTime	= 0.0f;
		private bool	gameOver	= false;
		private	int		gameStage	= 0;

		void ICmpUpdatable.OnUpdate()
		{
			if (this.gameStage == 0)
			{
				float beginTime = this.beginTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro1 = Scene.Current.AllObjects.FirstByName("Intro1");
				TextRenderer text = intro1.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter])
				{
					intro1.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro1.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
			}
			else if (this.gameStage == 1)
			{
				float beginTime = 6000.0f + this.beginTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro2 = Scene.Current.AllObjects.FirstByName("Intro2");
				TextRenderer text = intro2.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter])
				{
					intro2.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro2.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
			}
			else if (this.gameStage == 2)
			{
				float beginTime = 12000.0f + this.beginTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro3 = Scene.Current.AllObjects.FirstByName("Intro3");
				TextRenderer text = intro3.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter])
				{
					intro3.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro3.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
			}
			else if (this.gameStage == 3)
			{
				float beginTime = 18000.0f + this.beginTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject blackPlate = Scene.Current.AllObjects.FirstByName("BlackPlate");
				GameObject intro4 = Scene.Current.AllObjects.FirstByName("Intro4");
				SpriteRenderer text = intro4.GetComponent<SpriteRenderer>();
				SpriteRenderer plate = blackPlate.GetComponent<SpriteRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter])
				{
					intro4.Active = false;
					blackPlate.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro4.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
					plate.ColorTint = plate.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
			}
			else if (!this.gameOver)
			{
				if (Block.ActiveBlock != null)
				{
					Collider activeCol = Block.ActiveBlock.GameObj.GetComponent<Collider>();
					Transform activeTrans = Block.ActiveBlock.GameObj.Transform;

					if (activeTrans.Vel.Y < 1.0f)
						activeCol.ApplyWorldForce(Vector2.UnitY);

					// Continous keyboard commands
					if (DualityApp.Keyboard[Key.Left])
						activeCol.ApplyWorldForce(-Vector2.UnitX * 2);
					else if (DualityApp.Keyboard[Key.Right])
						activeCol.ApplyWorldForce(Vector2.UnitX * 2);
					else if (DualityApp.Keyboard[Key.Down])
						activeCol.ApplyWorldForce(Vector2.UnitY * 2);
				}
				else
				{
					Block.Create();
				}

				bool anyLineRemoved = false;
				for (int i = 0; i < 10; i++)
				{
					List<Collider.ShapeInfo> line = Collider.PickShapesGlobal(new Vector2(-160, -48 - 32 * i + 16), new Vector2(320, 1));
					if (line.Count >= 10)
					{
						List<Collider> colliders = line.Select(s => s.Parent).Distinct().ToList();
						List<Transform> transforms = colliders.Select(c => c.GameObj.Transform).ToList();
						List<Block> blocks = colliders.Select(c => c.GameObj.GetComponent<Block>()).ToList();
						if (!blocks.Any(b => b == Block.ActiveBlock) && !transforms.Any(t => t.Vel.Length > 0.001f))
						{
							foreach (Collider.ShapeInfo shape in line)
							{
								shape.Parent.RemoveShape(shape);
							}
							foreach (Collider col in colliders)
							{
								if (!col.Shapes.Any())
									col.GameObj.DisposeLater();
								//else
								//{
								//    Block b = col.GameObj.GetComponent<Block>();
								//    if (b != null) b.SplitToPieces();
								//}
							}
							anyLineRemoved = true;
							DualityApp.Sound.PlaySound(GameRes.Data.Sound.LineRemoved_Sound);
						}
					}
				}
				if (anyLineRemoved)
				{
					foreach (Collider c in Scene.Current.ActiveObjects.GetComponents<Collider>())
						c.AwakeBody();
				}
			}
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				if (instance == null)
				{
					instance = this;
					DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;

					this.beginTime = Time.GameTimer;

					// Play some music
					SoundBudgetPad bgMusic = DualityApp.Sound.Music.Push(GameRes.Data.Music.tetrisloop_Sound, SoundBudgetPriority.Background, 0.0f);
					bgMusic.Sound.Looped = true;

					// Play some intro
					DualityApp.Sound.Music.Push(GameRes.Data.Music.tetrisintro_Sound, SoundBudgetPriority.Tension, 0.0f);
				}
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
			{
				if (instance == this)
				{
					instance = null;
					DualityApp.Keyboard.KeyDown -= this.Keyboard_KeyDown;
				}
			}
		}
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (Block.ActiveBlock != null)
			{
				Transform activeTrans = Block.ActiveBlock.GameObj.Transform;

				// Continous keyboard commands
				if (DualityApp.Keyboard[Key.Enter])
				{
					activeTrans.Angle += MathF.RadAngle90;
					DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockTurnRight_Sound);
				}
				else if (DualityApp.Keyboard[Key.ShiftRight])
				{
					activeTrans.Angle -= MathF.RadAngle90;
					DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockTurnLeft_Sound);
				}
			}
		}

		public void NotifyGameOver()
		{
			if (this.gameOver) return;
			this.gameOver = true;
			GameObject gameOverText = Scene.Current.AllObjects.FirstByName("GameOver");
			gameOverText.Active = true;
			DualityApp.Sound.PlaySound(GameRes.Data.Sound.GameOver_Sound);
			DualityApp.Sound.Music.PopHigh(0.1f);
		}
	}
}
