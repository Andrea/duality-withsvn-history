using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Duality;
using Duality.Resources;
using Duality.VertexFormat;
using Duality.Components;

namespace Debug
{
	public class LightingTechnique : DrawTechnique
	{
		public override bool NeedsPreparation
		{
			get { return true; }
		}

		protected override void PrepareRendering(IDrawDevice device, BatchInfo material)
		{
			base.PrepareRendering(device, material);

			Vector3 camPos = device.RefCoord;
			float camRefDist = MathF.Abs(device.ParallaxRefDist);

			material.SetUniform("_camRefDist", camRefDist);
			material.SetUniform("_camWorldPos", camPos.X, camPos.Y, camPos.Z);

			Debug.Light.SetupLighting(device, material);
		}
	}
}
