using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Resources;

using OpenTK;
using OpenTK.Input;

namespace Tetris
{
	[Serializable]
	public class GameController : Component, ICmpUpdatable
	{
		void ICmpUpdatable.OnUpdate()
		{
			if (Block.ActiveBlock != null)
			{
				Collider activeCol = Block.ActiveBlock.GameObj.GetComponent<Collider>();
				if (DualityApp.Keyboard[Key.Left])
					activeCol.ApplyWorldForce(-Vector2.UnitX);
				else if (DualityApp.Keyboard[Key.Right])
					activeCol.ApplyWorldForce(Vector2.UnitX);
			}
			else
			{
				Block.Create();
			}
		}
	}
}
