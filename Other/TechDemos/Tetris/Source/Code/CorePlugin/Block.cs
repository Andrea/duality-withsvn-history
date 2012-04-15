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
	public class Block : Component, ICmpInitializable, ICmpCollisionListener
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
		void ICmpCollisionListener.OnCollisionBegin(Component sender, CollisionEventArgs args)
		{
			this.GameObj.GetComponent<Collider>().IgnoreGravity = false;
			if (activeBlock == this) activeBlock = null;
		}
		void ICmpCollisionListener.OnCollisionEnd(Component sender, CollisionEventArgs args) {}
		void ICmpCollisionListener.OnCollisionSolve(Component sender, CollisionEventArgs args) {}

		public static Block Create()
		{
			int blockId = MathF.Rnd.Next(7);
			GameObject blockObj = null;
			switch (blockId)
			{
				default:
				case 0: blockObj = GameRes.Data.Blocks.BlockA_Prefab.Res.Instantiate(); break;
				case 1: blockObj = GameRes.Data.Blocks.BlockB_Prefab.Res.Instantiate(); break;
				case 2: blockObj = GameRes.Data.Blocks.BlockC_Prefab.Res.Instantiate(); break;
				case 3: blockObj = GameRes.Data.Blocks.BlockD_Prefab.Res.Instantiate(); break;
				case 4: blockObj = GameRes.Data.Blocks.BlockE_Prefab.Res.Instantiate(); break;
				case 5: blockObj = GameRes.Data.Blocks.BlockF_Prefab.Res.Instantiate(); break;
				case 6: blockObj = GameRes.Data.Blocks.BlockG_Prefab.Res.Instantiate(); break;
			}
			blockObj.Transform.Pos = Vector3.UnitY * -550;
			Scene.Current.RegisterObj(blockObj);
			return blockObj.GetComponent<Block>();
		}
	}
}
