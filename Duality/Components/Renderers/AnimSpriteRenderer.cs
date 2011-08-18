using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.ColorFormat;
using Duality.Resources;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Components.Renderers
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public class AnimSpriteRenderer : SpriteRenderer, ICmpUpdatable, ICmpInitializable
	{
		public enum LoopMode
		{
			Once,
			Loop,
			PingPong,
			RandomSingle,
			FixedSingle
		}

		private	int			animFirstFrame		= 0;
		private	int			animFrameCount		= 0;
		private	float		animDuration		= 0.0f;
		private	LoopMode	animLoopMode		= LoopMode.Loop;
		private	float		animTime			= 0.0f;
		private	int			animCycle			= 0;

		private	VertexFormat.VertexC4P3T4A1[]	verticesSmooth	= null;


		public int AnimFirstFrame
		{
			get { return this.animFirstFrame; }
			set { this.animFirstFrame = value; }
		}
		public int AnimFrameCount
		{
			get { return this.animFrameCount; }
			set { this.animFrameCount = value; }
		}
		public float AnimDuration
		{
			get { return this.animDuration; }
			set { this.animDuration = value; }
		}
		public float AnimTime
		{
			get { return this.animTime; }
			set { this.animTime = value; }
		}
		public LoopMode AnimLoopMode
		{
			get { return this.animLoopMode; }
			set { this.animLoopMode = value; }
		}
		public bool IsAnimationRunning
		{
			get
			{
				switch (this.animLoopMode)
				{
					case LoopMode.FixedSingle:
					case LoopMode.RandomSingle:
						return false;
					case LoopMode.Loop:
					case LoopMode.PingPong:
						return true;
					case LoopMode.Once:
						return this.animTime < this.animDuration;
					default:
						return false;
				}
			}
		}


		public AnimSpriteRenderer() {}
		public AnimSpriteRenderer(Rect rect, ContentRef<Material> mainMat) : base(rect, mainMat) {}
		
		void ICmpUpdatable.OnUpdate()
		{
			if (this.animLoopMode == LoopMode.Loop)
			{
				this.animTime += Time.TimeMult * Time.SPFMult;
				if (this.animTime > this.animDuration)
				{
					int n = (int)(this.animTime / this.animDuration);
					this.animTime -= this.animDuration * n;
					this.animCycle += n;
				}
			}
			else if (this.animLoopMode == LoopMode.Once)
			{
				this.animTime = MathF.Min(this.animTime + Time.TimeMult * Time.SPFMult, this.animDuration);
			}
			else if (this.animLoopMode == LoopMode.PingPong)
			{
				if (this.animCycle % 2 == 0)
				{
					this.animTime += Time.TimeMult * Time.SPFMult;
					if (this.animTime > this.animDuration)
					{
						int n = (int)(this.animTime / this.animDuration);
						if (n % 2 == 1) this.animTime = this.animDuration;
						else this.animTime = 0.0f;
						this.animCycle += n;
					}
				}
				else
				{
					this.animTime -= Time.TimeMult * Time.SPFMult;
					if (this.animTime < 0.0f)
					{
						int n = (int)((this.animDuration - this.animTime) / this.animDuration);
						if (n % 2 == 1) this.animTime = 0.0f;
						else this.animTime = this.animDuration;
						this.animCycle += n;
					}
				}
			}
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Loaded)
			{
				if (this.animLoopMode == LoopMode.RandomSingle)
					this.animTime = MathF.Rnd.NextFloat(this.animDuration);
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context) {}
		
		protected void PrepareVerticesSmooth(ref VertexFormat.VertexC4P3T4A1[] vertices, IDrawDevice device, float curAnimFrameFade, ColorRgba mainClr, Rect uvRect, Rect uvRectNext)
		{
			Vector3 posTemp = this.gameobj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(this, ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, scaleTemp, out xDot, out yDot);

			Rect rectTemp = this.rect.Transform(this.gameobj.Transform.Scale.Xy);
			Vector2 edge1 = rectTemp.TopLeft;
			Vector2 edge2 = rectTemp.BottomLeft;
			Vector2 edge3 = rectTemp.BottomRight;
			Vector2 edge4 = rectTemp.TopRight;

			MathF.TransdormDotVec(ref edge1, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge2, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge3, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge4, ref xDot, ref yDot);

			if (vertices == null || vertices.Length != 4) vertices = new VertexFormat.VertexC4P3T4A1[4];

			vertices[0].pos.X = posTemp.X + edge1.X;
			vertices[0].pos.Y = posTemp.Y + edge1.Y;
			vertices[0].pos.Z = posTemp.Z;
			vertices[0].texCoord.X = uvRect.x;
			vertices[0].texCoord.Y = uvRect.y;
			vertices[0].texCoord.Z = uvRectNext.x;
			vertices[0].texCoord.W = uvRectNext.y;
			vertices[0].clr = mainClr;
			vertices[0].attrib = curAnimFrameFade;

			vertices[1].pos.X = posTemp.X + edge2.X;
			vertices[1].pos.Y = posTemp.Y + edge2.Y;
			vertices[1].pos.Z = posTemp.Z;
			vertices[1].texCoord.X = uvRect.x;
			vertices[1].texCoord.Y = uvRect.MaxY;
			vertices[1].texCoord.Z = uvRectNext.x;
			vertices[1].texCoord.W = uvRectNext.MaxY;
			vertices[1].clr = mainClr;
			vertices[1].attrib = curAnimFrameFade;

			vertices[2].pos.X = posTemp.X + edge3.X;
			vertices[2].pos.Y = posTemp.Y + edge3.Y;
			vertices[2].pos.Z = posTemp.Z;
			vertices[2].texCoord.X = uvRect.MaxX;
			vertices[2].texCoord.Y = uvRect.MaxY;
			vertices[2].texCoord.Z = uvRectNext.MaxX;
			vertices[2].texCoord.W = uvRectNext.MaxY;
			vertices[2].clr = mainClr;
			vertices[2].attrib = curAnimFrameFade;
				
			vertices[3].pos.X = posTemp.X + edge4.X;
			vertices[3].pos.Y = posTemp.Y + edge4.Y;
			vertices[3].pos.Z = posTemp.Z;
			vertices[3].texCoord.X = uvRect.MaxX;
			vertices[3].texCoord.Y = uvRect.y;
			vertices[3].texCoord.Z = uvRectNext.MaxX;
			vertices[3].texCoord.W = uvRectNext.y;
			vertices[3].clr = mainClr;
			vertices[3].attrib = curAnimFrameFade;
		}

		public override void Draw(IDrawDevice device)
		{
			Texture mainTex = this.RetrieveMainTex();
			ColorRgba mainClr = this.RetrieveMainColor();
			DrawTechnique tech = this.RetrieveDrawTechnique();

			bool smoothShaderInput = tech.PreferredVertexFormat == VertexFormat.VertexDataFormat.VertexC4P3T4A1;
			bool isAnimated = this.animFrameCount > 0 && this.animDuration > 0 && mainTex != null && mainTex.Atlas != null;
			int curAnimFrame = 0;
			int nextAnimFrame = 0;
			float curAnimFrameFade = 0.0f;

			Rect uvRect;
			Rect uvRectNext;
			if (mainTex != null)
			{
				if (isAnimated)
				{
					float frameTemp = this.animFrameCount * this.animTime / (float)this.animDuration;
					curAnimFrame = this.animFirstFrame + MathF.Clamp((int)frameTemp, 0, this.animFrameCount - 1);
					curAnimFrame = MathF.Clamp(curAnimFrame, 0, mainTex.Atlas.Count - 1);

					if (smoothShaderInput)
					{
						if (this.animLoopMode == LoopMode.Loop)
						{
							nextAnimFrame = MathF.NormalizeVar(curAnimFrame + 1, this.animFirstFrame, this.animFirstFrame + this.animFrameCount);
							nextAnimFrame = MathF.Clamp(nextAnimFrame, 0, mainTex.Atlas.Count - 1);
							curAnimFrameFade = frameTemp - (int)frameTemp;
						}
						else if (this.animLoopMode == LoopMode.Once)
						{
							nextAnimFrame = MathF.Clamp(curAnimFrame + 1, this.animFirstFrame, this.animFirstFrame + this.animFrameCount - 1);
							nextAnimFrame = MathF.Clamp(nextAnimFrame, 0, mainTex.Atlas.Count - 1);
							curAnimFrameFade = frameTemp - (int)frameTemp;
						}
						else if (this.animLoopMode == LoopMode.PingPong)
						{
							if (this.animCycle % 2 == 0)
							{
								nextAnimFrame = MathF.Clamp(curAnimFrame + 1, this.animFirstFrame, this.animFirstFrame + this.animFrameCount - 1);
								nextAnimFrame = MathF.Clamp(nextAnimFrame, 0, mainTex.Atlas.Count - 1);
								curAnimFrameFade = frameTemp - (int)frameTemp;
							}
							else
							{
								nextAnimFrame = MathF.Clamp(curAnimFrame - 1, this.animFirstFrame, this.animFirstFrame + this.animFrameCount - 1);
								nextAnimFrame = MathF.Clamp(nextAnimFrame, 0, mainTex.Atlas.Count - 1);
								curAnimFrameFade = 1.0f + MathF.Min((int)frameTemp, this.animFrameCount - 1) - frameTemp;
							}
						}
					}

					uvRect = mainTex.Atlas[curAnimFrame];
					uvRectNext = mainTex.Atlas[nextAnimFrame];
				}
				else
					uvRect = uvRectNext = new Rect(mainTex.UVRatio.X, mainTex.UVRatio.Y);
			}
			else
				uvRect = uvRectNext = new Rect(1.0f, 1.0f);

			if (!smoothShaderInput)
			{
				this.PrepareVertices(ref this.vertices, device, mainClr, uvRect);
				if (this.customMat != null)	device.AddVertices(this.customMat, BeginMode.Quads, this.vertices);
				else						device.AddVertices(this.sharedMat, BeginMode.Quads, this.vertices);
			}
			else
			{
				this.PrepareVerticesSmooth(ref this.verticesSmooth, device, curAnimFrameFade, mainClr, uvRect, uvRectNext);
				if (this.customMat != null)	device.AddVertices(this.customMat, BeginMode.Quads, this.verticesSmooth);
				else						device.AddVertices(this.sharedMat, BeginMode.Quads, this.verticesSmooth);
			}
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			AnimSpriteRenderer t = target as AnimSpriteRenderer;
			t.animCycle = this.animCycle;
			t.animDuration = this.animDuration;
			t.animFirstFrame = this.animFirstFrame;
			t.animFrameCount = this.animFrameCount;
			t.animLoopMode = this.animLoopMode;
			t.animTime = this.animTime;
		}
	}
}
