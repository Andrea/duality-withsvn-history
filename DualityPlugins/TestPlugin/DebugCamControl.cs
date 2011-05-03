using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

using Duality;
using Duality.Components;

namespace TestPlugin
{
	[Serializable]
	public class DebugCamControl : Component, ICmpUpdatable
	{
		public void OnUpdate()
		{
			this.GameObj.Transform.Pos = 
				Vector3.Transform(
					new Vector3(4.0f * (DualityApp.Mouse.X - DualityApp.TargetResolution.X * 0.5f), 4.0f * (DualityApp.Mouse.Y - DualityApp.TargetResolution.Y * 0.5f), this.GameObj.Transform.Pos.Z),
					Matrix4.CreateRotationZ(this.GameObj.Transform.Angle));
		}
	}
}
