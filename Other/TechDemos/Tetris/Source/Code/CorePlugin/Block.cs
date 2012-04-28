using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Resources;

using OpenTK;

namespace Tetris
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	[RequiredComponent(typeof(Collider))]
	public class Block : Component, ICmpInitializable, ICmpCollisionListener, ICmpUpdatable
	{
		private	static	List<Block>	blocks		= new List<Block>();
		private	static	Block		activeBlock	= null;

		public static IEnumerable<Block> Instances
		{
			get { return blocks; }
		}
		public static Block ActiveBlock
		{
			get { return activeBlock; }
		}

		private	float timeFirstContact	= 0.0f;

		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				blocks.Add(this);
				activeBlock = this;
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
			{
				blocks.Remove(this);
				if (activeBlock == this) activeBlock = null;
			}
		}
		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args) {}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args) {}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args)
		{
			if (args.CollisionData.NormalSpeed > 1.0f && this.GameObj.Transform.Vel.Length > 0.5f)
			{
				SoundInstance collisionSound = DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockCollide_Sound);
				collisionSound.Pitch = MathF.Rnd.NextFloat(0.8f, 1.25f);
				collisionSound.Volume = MathF.Clamp(args.CollisionData.NormalSpeed / 5.0f, 0.0f, 0.5f);
			}

			if (activeBlock == this && timeFirstContact == 0.0f && MathF.Abs(args.CollisionData.Normal.Y) > 0.05f)
			{
				timeFirstContact = Time.GameTimer;
				if (this.GameObj.Transform.Pos.Y <= -500) GameController.Instance.NotifyGameOver();
				this.GameObj.GetComponent<Collider>().IgnoreGravity = false;
				this.GameObj.GetComponent<Collider>().FixedAngle = false;
			}

			if (this.timeFirstContact != 0.0f && Time.GameTimer - timeFirstContact < 3000.0f && this.GameObj.Transform.Vel.Length <= 0.1f)
			{
				float angleDistFromIdeal = MathF.CircularDist(0.0f, this.GameObj.Transform.Angle, 0.0f, MathF.PiOver2);
				if (angleDistFromIdeal >= MathF.Pi * 0.1f) GameController.Instance.NotifyBlockFellOver(this.GameObj);
			}
		}
		void ICmpUpdatable.OnUpdate()
		{
			if (activeBlock != this) return;
			if ((this.timeFirstContact != 0.0f && Time.GameTimer - this.timeFirstContact > 750.0f) || this.GameObj.Transform.Vel.Length <= 0.1f)
			{
				activeBlock = null;
				DualityApp.Sound.PlaySound(GameRes.Data.Sound.BlockDrop_Sound);

				float angleDistFromIdeal = MathF.CircularDist(0.0f, this.GameObj.Transform.Angle, 0.0f, MathF.PiOver2);
				float angleDistTolerance = MathF.Pi * 0.1f;
				GameController.Instance.Score += MathF.RoundToInt(50.0f * (1.0f - MathF.Clamp(angleDistFromIdeal / angleDistTolerance, 0.0f, 1.0f)));
			}
		}

		public void SplitToPieces()
		{
			List<GameObject> pieces = new List<GameObject>();
			foreach (Collider.ShapeInfo shape in this.GameObj.GetComponent<Collider>().Shapes)
			{
				GameObject pieceObj = this.GameObj.Clone();
				pieceObj.RemoveComponent<Block>();
				Collider pieceCollider = pieceObj.GetComponent<Collider>();
				foreach (Collider.ShapeInfo pieceShape in pieceCollider.Shapes.Where(s => s.AABB != shape.AABB).ToArray())
				{
					pieceCollider.RemoveShape(pieceShape);
				}
				pieces.Add(pieceObj);
			}

			Scene.Current.UnregisterObj(this.GameObj);
			this.GameObj.DisposeLater();

			Scene.Current.RegisterObj(pieces);
		}

		public static Block Create()
		{
			int blockId = MathF.Rnd.Next(GameController.Instance.TotalPlayTime < 10000.0f ? 5 : 7);
			GameObject blockObj = null;
			switch (blockId)
			{
				default:
				case 0: blockObj = GameRes.Data.Blocks.BlockA_Prefab.Res.Instantiate(); break;
				case 1: blockObj = GameRes.Data.Blocks.BlockD_Prefab.Res.Instantiate(); break;
				case 2: blockObj = GameRes.Data.Blocks.BlockE_Prefab.Res.Instantiate(); break;
				case 3: blockObj = GameRes.Data.Blocks.BlockF_Prefab.Res.Instantiate(); break;
				case 4: blockObj = GameRes.Data.Blocks.BlockG_Prefab.Res.Instantiate(); break;
				case 5: blockObj = GameRes.Data.Blocks.BlockB_Prefab.Res.Instantiate(); break;
				case 6: blockObj = GameRes.Data.Blocks.BlockC_Prefab.Res.Instantiate(); break;
			}
			blockObj.Transform.Pos = Vector3.UnitY * -500;
			blockObj.Transform.Vel = Vector3.UnitY;
			Scene.Current.RegisterObj(blockObj);
			return blockObj.GetComponent<Block>();
		}
	}
}
