using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GamePlugin
{
	[Serializable]
	[RequiredComponent(typeof(Camera))]
    public class CamStarfield : Renderer, ICmpInitializable
    {
		private struct StarInfo
		{
			public	Vector2	pos;
			public	float	brightness;
		}
		private class StarLayer
		{
			public	StarInfo[]	stars;

			public StarLayer(int starCount)
			{
				this.stars = new StarInfo[starCount];
			}
		}


		[NonSerialized]	private	Camera			camComp;
		[NonSerialized]	private	List<StarLayer>	starLayers;
		private	int		layerCount;
		private	int		starsPerLayer;
		private	float	layerDepth;
		private	float	brightness;
		private	float	trailLength;
		

		public override float BoundRadius
		{
			get { return this.camComp != null ? this.camComp.ViewBoundingRadius : 0.0f; }
		}
		public override bool IsInfiniteXY
		{
			get { return true; }
		}
		public int LayerCount
		{
			get { return this.layerCount; }
			set { this.layerCount = MathF.Clamp(value, 0, 100); this.GenerateStarField(); }
		}
		public int StarsPerLayer
		{
			get { return this.starsPerLayer; }
			set { this.starsPerLayer = MathF.Clamp(value, 0, 1000); this.GenerateStarField(); }
		}
		public float LayerDepth
		{
			get { return this.layerDepth; }
			set { this.layerDepth = MathF.Max(value, 1.0f); }
		}
		public float Brightness
		{
			get { return this.brightness; }
			set { this.brightness = MathF.Max(value, 0.0f); }
		}
		public float TrailLength
		{
			get { return this.trailLength; }
			set { this.trailLength = value; }
		}
		public float TileSize
		{
			get { return this.camComp.ViewBoundingRadius * 1.5f; }
		}


		private void GenerateStarField(int layerCount = -1, int starsPerLayer = -1)
		{
			if (layerCount == -1) layerCount = this.layerCount;
			if (starsPerLayer == -1) starsPerLayer = this.starsPerLayer;

			this.starLayers = new List<StarLayer>(layerCount);
			for (int i = 0; i < layerCount; i++)
			{
				this.starLayers.Add(this.GenerateSingleStarLayer(starsPerLayer));
			}
		}
		private StarLayer GenerateSingleStarLayer(int stars)
		{
			float cntSq = (int)Math.Sqrt(stars);
			float screenBoundRad = this.TileSize * 0.5f;
			StarLayer layer = new StarLayer(stars);
			for (int i = 0; i < layer.stars.Length; i++)
			{
				layer.stars[i].pos = new Vector2(
					-screenBoundRad + ((i % cntSq) / cntSq) * screenBoundRad * 2.0f + 
					(MathF.Rnd.NextFloat() - 0.5f) * screenBoundRad * 1.75f / cntSq,
					
					-screenBoundRad + ((i / cntSq) / cntSq) * screenBoundRad * 2.0f + 
					(MathF.Rnd.NextFloat() - 0.5f) * screenBoundRad * 1.75f / cntSq);
				layer.stars[i].brightness = 0.5f + MathF.Rnd.NextFloat();
			}
			return layer;
		}
		
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				this.camComp = this.GameObj.GetComponent<Camera>();
				this.GenerateStarField();
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context) {}
		public override void Draw(IDrawDevice device)
		{
			if (this.camComp.DrawDevice != device) return;

			List<VertexC1P3> vertices = new List<VertexC1P3>((2 * this.layerCount * this.starsPerLayer) * 2);
			float screenBoundRad = this.camComp.ViewBoundingRadius;
			float minZDist = this.camComp.NearZ + this.camComp.ParallaxRefDist / 50.0f;
				
			float stepTemp = this.TileSize;
			Vector2 minPos = new Vector2(-screenBoundRad, -screenBoundRad);
			Vector2 maxPos = new Vector2(screenBoundRad, screenBoundRad);

			// Iterate over star layers
			for (int layerIndex = 0; layerIndex < this.layerCount; layerIndex++)
			{
				StarLayer layer = this.starLayers[layerIndex];

				// Determine the layers Z value & perform layer culling if too near
				float layerZ = this.layerDepth * ((float)layerIndex / (float)this.layerCount) - this.GameObj.Transform.Pos.Z;
				if (layerZ < 0.0f) layerZ += this.layerDepth * (float)(1 + (int)(-layerZ / this.layerDepth));
				layerZ = layerZ % this.layerDepth;
				if (layerZ <= this.GameObj.Transform.Pos.Z + minZDist) continue;

				// Calculate transform data
				Vector3 posTemp = this.GameObj.Transform.Pos + Vector3.UnitZ * layerZ;
				Vector3 posTempTrail = this.GameObj.Transform.Pos + Vector3.UnitZ * (layerZ + this.GameObj.Transform.Vel.Z * this.trailLength);
				float scaleTemp = 1.0f;
				float scaleTempTrail = 1.0f;
				device.PreprocessCoords(this, ref posTemp, ref scaleTemp);
				device.PreprocessCoords(this, ref posTempTrail, ref scaleTempTrail);
				posTempTrail += new Vector3(this.GameObj.Transform.Vel.Xy * this.trailLength);

				// Prepare this layers transformation to calculate a stars trail position.
				Vector2 starPosTempTrailDotX;
				Vector2 starPosTempTrailDotY;
				MathF.GetTransformDotVec(this.GameObj.Transform.AngleVel * this.trailLength, scaleTempTrail, out starPosTempTrailDotX, out starPosTempTrailDotY);

				// Iterate over stars
				for (int starIndex = 0; starIndex < layer.stars.Length; starIndex++)
				{
					// Since it's an endless starfield, each star may show up on multiple positions at once. It's tiled.
					StarInfo star = layer.stars[starIndex];
					Vector2 starPosBase = star.pos - this.GameObj.Transform.Pos.Xy;

					// Move the topleft tiling corner to somewhere near the screens top left
					if (starPosBase.X > minPos.X)					starPosBase.X -= stepTemp * MathF.Ceiling((starPosBase.X - minPos.X) / stepTemp);
					else if (starPosBase.X < minPos.X - stepTemp)	starPosBase.X -= stepTemp * MathF.Ceiling((starPosBase.X - minPos.X) / stepTemp);
					if (starPosBase.Y > minPos.Y)					starPosBase.Y -= stepTemp * MathF.Ceiling((starPosBase.Y - minPos.Y) / stepTemp);
					else if (starPosBase.Y < minPos.Y - stepTemp)	starPosBase.Y -= stepTemp * MathF.Ceiling((starPosBase.Y - minPos.Y) / stepTemp);
					
					// Tiling!
					Vector2 startPos = starPosBase;
					while (starPosBase.X <= maxPos.X && starPosBase.Y <= maxPos.Y)
					{
						// Determine star pos and perform culling
						Vector3 starPosTemp = posTemp + new Vector3(starPosBase) * scaleTemp;
						if (starPosTemp.X > -screenBoundRad && starPosTemp.Y > -screenBoundRad &&
							starPosTemp.X < screenBoundRad && starPosTemp.Y < screenBoundRad)
						{
							// Determine trail pos
							Vector3 starPosTempTrail = posTempTrail + new Vector3(starPosBase);
							MathF.TransformDotVec(ref starPosTempTrail, ref starPosTempTrailDotX, ref starPosTempTrailDotY);

							// Calculate length factor for the star's alpha value. Reduce alpha if too small or too big
							float lenTemp = (starPosTemp - starPosTempTrail).Length;
							if (lenTemp < 1.0f)
								lenTemp = MathF.Max(0.25f, MathF.Sqrt(lenTemp));
							else if (lenTemp > 1.0f)
								lenTemp = 1.0f / MathF.Pow(lenTemp, 0.25f);

							// Determine the star's alpha value and generate its vertices (star and trail)
							float alpha = star.brightness * this.brightness * lenTemp * (1.0f - ((layerZ - minZDist) / (this.layerDepth - minZDist)));
							vertices.Add(new VertexC1P3(starPosTemp, ColorRgba.White.WithAlpha(alpha)));
							vertices.Add(new VertexC1P3(starPosTempTrail, ColorRgba.White.WithAlpha(alpha * 0.5f)));
						}

						// Advance X / Y grid
					    starPosBase.X += stepTemp;
					    if (starPosBase.X > maxPos.X)
					    {
					        starPosBase.X = startPos.X;
					        starPosBase.Y += stepTemp;
					    }
					}
				}
			}

			// Draw the stars all at once. Since they're not on the same Z layer, this may lead to wronz Z sorting
			// when interacting with a complex environment that needs Z-Sorting itsself. For this application, it should be sufficient.
			device.AddVertices(new BatchInfo(DrawTechnique.Add, ColorRgba.White), BeginMode.Lines, vertices.ToArray());
			// (Can be fixed by drawing each layer in its own batch because they can be properly Z-sorted by Duality)
		}
	}
}
