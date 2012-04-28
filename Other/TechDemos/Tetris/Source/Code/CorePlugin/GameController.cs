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

		private	int		score		= 0;
		private	float	beginIntroTime	= 0.0f;
		private	float	beginGameTime	= 0.0f;
		private bool	gameOver	= false;
		private	int		gameStage	= 0;

		private	HashSet<GameObject>	fellOverBlocks	= new HashSet<GameObject>();
		private	float	gameOverTime		= -100000.0f;
		private	float	blockFellOverTime	= -100000.0f;
		private	float	lineClearTime		= -100000.0f;

		public bool FirstGameInSession
		{
			get { return this.beginIntroTime < 5.0f; }
		}
		public float TotalPlayTime
		{
			get { return this.beginGameTime != 0.0f ? Time.GameTimer - this.beginGameTime : 0.0f; }
		}
		public int Score
		{
			get { return this.score; }
			set
			{
				this.score = value;

				GameObject scoreObj = Scene.Current.AllObjects.FirstByName("PointDisplay");
				if (scoreObj != null)
				{
					TextRenderer scoreText = scoreObj.GetComponent<TextRenderer>();
					if (scoreText != null)
					{
						scoreText.Text.ApplySource("Score: " + this.score.ToString());
						scoreText.UpdateMetrics();
					}
				}
			}
		}
		public bool GameOver
		{
			get { return this.gameOver; }
		}
		
		public bool GameJustEnded
		{
			get { return this.gameOver && Time.GameTimer - this.gameOverTime < 3000.0f; }
		}
		public bool BlockJustFellOver
		{
			get { return Time.GameTimer - this.blockFellOverTime < 1000.0f; }
		}
		public bool LineJustCleared
		{
			get { return Time.GameTimer - this.lineClearTime < 1000.0f; }
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (!this.UpdateIntro())
				this.UpdateGame();
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				if (instance == null)
				{
					instance = this;
					DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;

					this.beginIntroTime = Time.GameTimer;

					CommentGuy.Init(Scene.Current.AllObjects.FirstByName("Commentary").GetComponent<TextRenderer>());

					// Play some music
					SoundBudgetPad bgMusic = DualityApp.Sound.Music.Push(GameRes.Data.Music.tetrisloop_Sound, SoundBudgetPriority.Background, 0.0f);
					bgMusic.Sound.Looped = true;

					// Play some intro
					if (this.FirstGameInSession) DualityApp.Sound.Music.Push(GameRes.Data.Music.tetrisintro_Sound, SoundBudgetPriority.Tension, 0.0f);
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
				if (e.Key == Key.Enter)
				{
					activeTrans.Angle += MathF.RadAngle90;
					DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockTurnRight_Sound);
				}
				else if (e.Key == Key.ShiftRight)
				{
					activeTrans.Angle -= MathF.RadAngle90;
					DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockTurnLeft_Sound);
				}
			}
		}

		private bool UpdateIntro()
		{
			if (this.gameStage == 0)
			{
				float beginTime = this.beginIntroTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro1 = Scene.Current.AllObjects.FirstByName("Intro1");
				TextRenderer text = intro1.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter] || !this.FirstGameInSession)
				{
					intro1.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro1.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
				return true;
			}
			else if (this.gameStage == 1)
			{
				float beginTime = 6000.0f + this.beginIntroTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro2 = Scene.Current.AllObjects.FirstByName("Intro2");
				TextRenderer text = intro2.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter] || !this.FirstGameInSession)
				{
					intro2.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro2.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
				return true;
			}
			else if (this.gameStage == 2)
			{
				float beginTime = 12000.0f + this.beginIntroTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject intro3 = Scene.Current.AllObjects.FirstByName("Intro3");
				TextRenderer text = intro3.GetComponent<TextRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter] || !this.FirstGameInSession)
				{
					intro3.Active = false;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro3.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
				return true;
			}
			else if (this.gameStage == 3)
			{
				float beginTime = 18000.0f + this.beginIntroTime;
				float localTime = Time.GameTimer - beginTime;
				GameObject blackPlate = Scene.Current.AllObjects.FirstByName("BlackPlate");
				GameObject intro4 = Scene.Current.AllObjects.FirstByName("Intro4");
				SpriteRenderer text = intro4.GetComponent<SpriteRenderer>();
				SpriteRenderer plate = blackPlate.GetComponent<SpriteRenderer>();

				if (localTime >= 6000.0f || DualityApp.Keyboard[Key.Enter] || !this.FirstGameInSession)
				{
					intro4.Active = false;
					blackPlate.Active = false;
					this.beginGameTime = Time.GameTimer;
					this.gameStage++;
				}
				else if (localTime >= 0.0f)
				{
					intro4.Active = true;
					text.ColorTint = text.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
					plate.ColorTint = plate.ColorTint.WithAlpha(1.0f - MathF.Clamp((localTime - 0.0f) / 6000.0f, 0.0f, 1.0f));
				}
				return true;
			}
			else return false;
		}
		private void UpdateGame()
		{
			CommentGuy.Update(); // Make comments

			if (!this.gameOver)
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

						if (!blocks.Any(b => b == Block.ActiveBlock) && 
							!transforms.Any(t => t.Vel.Length > 0.001f) &&
							line.Sum(s => MathF.CircularDist(0.0f, s.Parent.GameObj.Transform.Angle, 0.0f, MathF.PiOver2)) < Math.PI)
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
							this.Score += 1000;
							this.lineClearTime = Time.GameTimer;
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
			else
			{
				if (DualityApp.Keyboard[Key.Enter]) this.RestartGame();
			}
		}

		public void RestartGame()
		{
			Scene.Current.Dispose();
			Scene.Current = GameRes.Data.GameScene_Scene.Res;
		}
		public void NotifyGameOver()
		{
			if (this.gameOver) return;
			this.gameOver = true;
			this.gameOverTime = Time.GameTimer;
			GameObject gameOverText = Scene.Current.AllObjects.FirstByName("GameOver");
			gameOverText.Active = true;
			DualityApp.Sound.PlaySound(GameRes.Data.Sound.GameOver_Sound);
			DualityApp.Sound.Music.PopHigh(0.1f);
		}
		public void NotifyBlockFellOver(GameObject blockObj)
		{
			if (this.fellOverBlocks.Contains(blockObj)) return;
			this.fellOverBlocks.Add(blockObj);
			this.blockFellOverTime = Time.GameTimer;
		}
	}
}
