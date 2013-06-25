﻿using System;
using System.Collections.Generic;
using System.Linq;

using Duality.EditorHints;
using Duality.ColorFormat;
using Duality.VertexFormat;
using Duality.Resources;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Components.Renderers
{
	/// <summary>
	/// Renders an animated sprite to represent the <see cref="GameObject"/>.
	/// </summary>
	[Serializable]
	public class AnimSpriteRenderer : SpriteRenderer, ICmpUpdatable, ICmpInitializable
	{
		/// <summary>
		/// Describes the sprite animations loop behaviour.
		/// </summary>
		public enum LoopMode
		{
			/// <summary>
			/// The animation is played once an then remains in its last frame.
			/// </summary>
			Once,
			/// <summary>
			/// The animation is looped: When reaching the last frame, it begins again at the first one.
			/// </summary>
			Loop,
			/// <summary>
			/// The animation plays forward until reaching the end, then reverses and plays backward until 
			/// reaching the start again. This "pingpong" behaviour is looped.
			/// </summary>
			PingPong,
			/// <summary>
			/// A single frame is selected randomly each time the object is initialized and remains static
			/// for its whole lifetime.
			/// </summary>
			RandomSingle,
			/// <summary>
			/// A fixed, single frame is displayed. Which one depends on the one you set in the editor or
			/// in source code.
			/// </summary>
			FixedSingle,
			/// <summary>
			/// The <see cref="CustomFrameSequence"/> is interpreted and processed as a queue where <see cref="AnimDuration"/>
			/// is the time a single frame takes.
			/// </summary>
			Queue
		}

		private	int			animFirstFrame		= 0;
		private	int			animFrameCount		= 0;
		private	float		animDuration		= 5.0f;
		private	LoopMode	animLoopMode		= LoopMode.Loop;
		private	float		animTime			= 0.0f;
		private	bool		animPaused			= false;
		private	List<int>	customFrameSequence	= null;

		[NonSerialized] private int		curAnimFrame		= 0;
		[NonSerialized] private int		nextAnimFrame		= 0;
		[NonSerialized] private float	curAnimFrameFade	= 0.0f;

		[NonSerialized] private	VertexC1P3T4A1[]	verticesSmooth	= null;


		/// <summary>
		/// [GET / SET] The index of the first frame to display. Ignored if <see cref="CustomFrameSequence"/> is set.
		/// </summary>
		/// <remarks>
		/// Animation indices are looked up in the <see cref="Duality.Resources.Pixmap.Atlas"/> map
		/// of the <see cref="Duality.Resources.Texture"/> that is used.
		/// </remarks>
		[EditorHintRange(0, int.MaxValue)]
		public int AnimFirstFrame
		{
			get { return this.animFirstFrame; }
			set { this.animFirstFrame = MathF.Max(0, value); }
		}
		/// <summary>
		/// [GET / SET] The number of continous frames to use for the animation. Ignored if <see cref="CustomFrameSequence"/> is set.
		/// </summary>
		/// <remarks>
		/// Animation indices are looked up in the <see cref="Duality.Resources.Pixmap.Atlas"/> map
		/// of the <see cref="Duality.Resources.Texture"/> that is used.
		/// </remarks>
		[EditorHintRange(0, int.MaxValue)]
		public int AnimFrameCount
		{
			get { return this.animFrameCount; }
			set { this.animFrameCount = MathF.Max(0, value); }
		}
		/// <summary>
		/// [GET / SET] The time a single animation cycle needs to complete, in seconds.
		/// </summary>
		[EditorHintRange(0.0f, float.MaxValue)]
		public float AnimDuration
		{
			get { return this.animDuration; }
			set
			{
				float lastDuration = this.animDuration;

				this.animDuration = MathF.Max(0.0f, value);

				if (lastDuration != 0.0f && this.animDuration != 0.0f)
					this.animTime *= this.animDuration / lastDuration;
			}
		}
		/// <summary>
		/// [GET / SET] The animations current play time, i.e. the current state of the animation.
		/// </summary>
		[EditorHintRange(0.0f, float.MaxValue)]
		public float AnimTime
		{
			get { return this.animTime; }
			set { this.animTime = MathF.Max(0.0f, value); }
		}
		/// <summary>
		/// [GET / SET] If true, the animation is paused and won't advance over time. <see cref="AnimTime"/> will stay constant until resumed.
		/// </summary>
		public bool AnimPaused
		{
			get { return this.animPaused; }
			set { this.animPaused = value; }
		}
		/// <summary>
		/// [GET / SET] The animations loop behaviour.
		/// </summary>
		public LoopMode AnimLoopMode
		{
			get { return this.animLoopMode; }
			set { this.animLoopMode = value; }
		}
		/// <summary>
		/// [GET / SET] A custom sequence of frame indices that will be used instead of the default range
		/// specified by <see cref="AnimFirstFrame"/> and <see cref="AnimFrameCount"/>. Unused if set to null.
		/// </summary>
		/// <remarks>
		/// Animation indices are looked up in the <see cref="Duality.Resources.Pixmap.Atlas"/> map
		/// of the <see cref="Duality.Resources.Texture"/> that is used.
		/// </remarks>
		[EditorHintFlags(MemberFlags.ForceWriteback)]
		public List<int> CustomFrameSequence
		{
			get { return this.customFrameSequence; }
			set { this.customFrameSequence = value; }
		}
		/// <summary>
		/// [GET] Whether the animation is currently running, i.e. if there is anything animated right now.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
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
						return !this.animPaused;
					case LoopMode.Once:
						return !this.animPaused && this.animTime < this.animDuration;
					case LoopMode.Queue:
						return !this.animPaused && this.customFrameSequence != null && this.customFrameSequence.Count > 1;
					default:
						return false;
				}
			}
		}
		/// <summary>
		/// [GET] The currently visible animation frames index.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public int CurrentFrame
		{
			get { return this.curAnimFrame; }
		}
		/// <summary>
		/// [GET] The next visible animation frames index.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public int NextFrame
		{
			get { return this.nextAnimFrame; }
		}
		/// <summary>
		/// [GET] The current animation frames progress where zero means "just entered the current frame"
		/// and one means "about to leave the current frame". This value is also used for smooth animation blending.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public float CurrentFrameProgress
		{
			get { return this.curAnimFrameFade; }
		}


		public AnimSpriteRenderer() {}
		public AnimSpriteRenderer(Rect rect, ContentRef<Material> mainMat) : base(rect, mainMat) {}
		
		/// <summary>
		/// Updates the <see cref="CurrentFrame"/>, <see cref="NextFrame"/> and <see cref="CurrentFrameProgress"/> properties immediately.
		/// This is called implicitly once each frame before drawing, so you don't normally call this. However, when changing animation
		/// parameters and requiring updated animation frame data immediately, this could be helpful.
		/// </summary>
		public void UpdateVisibleFrames()
		{
			int actualFrameBegin = this.customFrameSequence != null ? 0 : this.animFirstFrame;
			int actualFrameCount = this.customFrameSequence != null ? this.customFrameSequence.Count : this.animFrameCount;

			// Calculate visible frames
			this.curAnimFrame = 0;
			this.nextAnimFrame = 0;
			this.curAnimFrameFade = 0.0f;
			if (actualFrameCount > 0 && this.animDuration > 0)
			{
				// Queued behavior
				if (this.animLoopMode == LoopMode.Queue)
				{
					this.curAnimFrameFade = MathF.Clamp(this.animTime / this.animDuration, 0.0f, 1.0f);
					this.curAnimFrame = 0;
					this.nextAnimFrame = 1;
				}
				// Non-queued behavior
				else
				{
					// Calculate currently visible frame
					float frameTemp = actualFrameCount * this.animTime / this.animDuration;
					this.curAnimFrame = (int)frameTemp;

					// Handle extended frame range for ping pong mode
					if (this.animLoopMode == LoopMode.PingPong)
					{
						if (this.curAnimFrame >= actualFrameCount)
							this.curAnimFrame = (actualFrameCount - 1) * 2 - this.curAnimFrame;
					}

					// Normalize current frame when exceeding anim duration
					if (this.animLoopMode == LoopMode.Once)
						this.curAnimFrame = MathF.Clamp(this.curAnimFrame, 0, actualFrameCount - 1);
					else
						this.curAnimFrame = MathF.NormalizeVar(this.curAnimFrame, 0, actualFrameCount);

					// Calculate second frame and fade value
					this.curAnimFrameFade = frameTemp - (int)frameTemp;
					if (this.animLoopMode == LoopMode.Loop)
					{
						this.nextAnimFrame = MathF.NormalizeVar(this.curAnimFrame + 1, 0, actualFrameCount);
					}
					else if (this.animLoopMode == LoopMode.PingPong)
					{
						if ((int)frameTemp < actualFrameCount)
						{
							this.nextAnimFrame = this.curAnimFrame + 1;
							if (this.nextAnimFrame >= actualFrameCount)
								this.nextAnimFrame = (actualFrameCount - 1) * 2 - this.nextAnimFrame;
						}
						else
						{
							this.nextAnimFrame = this.curAnimFrame - 1;
							if (this.nextAnimFrame < 0)
								this.nextAnimFrame = -this.nextAnimFrame;
						}
					}
					else
					{
						this.nextAnimFrame = this.curAnimFrame + 1;
					}
				}
			}
			this.curAnimFrame	= actualFrameBegin + MathF.Clamp(this.curAnimFrame, 0, actualFrameCount - 1);
			this.nextAnimFrame	= actualFrameBegin + MathF.Clamp(this.nextAnimFrame, 0, actualFrameCount - 1);

			// Map to custom sequence
			if (this.customFrameSequence != null)
			{
				if (this.customFrameSequence.Count > 0)
				{
					this.curAnimFrame	= this.customFrameSequence[this.curAnimFrame];
					this.nextAnimFrame	= this.customFrameSequence[this.nextAnimFrame];
				}
				else
				{
					this.curAnimFrame	= 0;
					this.nextAnimFrame	= 0;
				}
			}
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (!this.IsAnimationRunning) return;
			if (this.animPaused) return;

			int actualFrameBegin = this.customFrameSequence != null ? 0 : this.animFirstFrame;
			int actualFrameCount = this.customFrameSequence != null ? this.customFrameSequence.Count : this.animFrameCount;

			// Advance animation timer
			if (this.animLoopMode == LoopMode.Loop)
			{
				this.animTime += Time.TimeMult * Time.SPFMult;
				if (this.animTime > this.animDuration)
				{
					int n = (int)(this.animTime / this.animDuration);
					this.animTime -= this.animDuration * n;
				}
			}
			else if (this.animLoopMode == LoopMode.Once)
			{
				this.animTime = MathF.Min(this.animTime + Time.TimeMult * Time.SPFMult, this.animDuration);
			}
			else if (this.animLoopMode == LoopMode.PingPong)
			{
				float frameTime = this.animDuration / actualFrameCount;
				float pingpongDuration = (this.animDuration - frameTime) * 2.0f;

				this.animTime += Time.TimeMult * Time.SPFMult;
				if (this.animTime > pingpongDuration)
				{
					int n = (int)(this.animTime / pingpongDuration);
					this.animTime -= pingpongDuration * n;
				}
			}
			else if (this.animLoopMode == LoopMode.Queue)
			{
				this.animTime += Time.TimeMult * Time.SPFMult;
				if (this.animTime > this.animDuration)
				{
					int n = (int)(this.animTime / this.animDuration);
					this.animTime -= this.animDuration * n;

					if (this.customFrameSequence != null)
					{
						while (n > 0 && this.customFrameSequence.Count > 1)
						{
							this.customFrameSequence.RemoveAt(0);
							n--;
						}
					}
				}
			}

			this.UpdateVisibleFrames();
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
		
		protected void PrepareVerticesSmooth(ref VertexFormat.VertexC1P3T4A1[] vertices, IDrawDevice device, float curAnimFrameFade, ColorRgba mainClr, Rect uvRect, Rect uvRectNext)
		{
			Vector3 posTemp = this.gameobj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, scaleTemp, out xDot, out yDot);

			Rect rectTemp = this.rect.Transform(this.gameobj.Transform.Scale, this.gameobj.Transform.Scale);
			Vector2 edge1 = rectTemp.TopLeft;
			Vector2 edge2 = rectTemp.BottomLeft;
			Vector2 edge3 = rectTemp.BottomRight;
			Vector2 edge4 = rectTemp.TopRight;

			MathF.TransformDotVec(ref edge1, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge2, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge3, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge4, ref xDot, ref yDot);

			if (vertices == null || vertices.Length != 4) vertices = new VertexFormat.VertexC1P3T4A1[4];

			vertices[0].Pos.X = posTemp.X + edge1.X;
			vertices[0].Pos.Y = posTemp.Y + edge1.Y;
			vertices[0].Pos.Z = posTemp.Z + this.VertexZOffset;
			vertices[0].TexCoord.X = uvRect.X;
			vertices[0].TexCoord.Y = uvRect.Y;
			vertices[0].TexCoord.Z = uvRectNext.X;
			vertices[0].TexCoord.W = uvRectNext.Y;
			vertices[0].Color = mainClr;
			vertices[0].Attrib = curAnimFrameFade;

			vertices[1].Pos.X = posTemp.X + edge2.X;
			vertices[1].Pos.Y = posTemp.Y + edge2.Y;
			vertices[1].Pos.Z = posTemp.Z + this.VertexZOffset;
			vertices[1].TexCoord.X = uvRect.X;
			vertices[1].TexCoord.Y = uvRect.MaxY;
			vertices[1].TexCoord.Z = uvRectNext.X;
			vertices[1].TexCoord.W = uvRectNext.MaxY;
			vertices[1].Color = mainClr;
			vertices[1].Attrib = curAnimFrameFade;

			vertices[2].Pos.X = posTemp.X + edge3.X;
			vertices[2].Pos.Y = posTemp.Y + edge3.Y;
			vertices[2].Pos.Z = posTemp.Z + this.VertexZOffset;
			vertices[2].TexCoord.X = uvRect.MaxX;
			vertices[2].TexCoord.Y = uvRect.MaxY;
			vertices[2].TexCoord.Z = uvRectNext.MaxX;
			vertices[2].TexCoord.W = uvRectNext.MaxY;
			vertices[2].Color = mainClr;
			vertices[2].Attrib = curAnimFrameFade;
				
			vertices[3].Pos.X = posTemp.X + edge4.X;
			vertices[3].Pos.Y = posTemp.Y + edge4.Y;
			vertices[3].Pos.Z = posTemp.Z + this.VertexZOffset;
			vertices[3].TexCoord.X = uvRect.MaxX;
			vertices[3].TexCoord.Y = uvRect.Y;
			vertices[3].TexCoord.Z = uvRectNext.MaxX;
			vertices[3].TexCoord.W = uvRectNext.Y;
			vertices[3].Color = mainClr;
			vertices[3].Attrib = curAnimFrameFade;
		}
		protected void GetAnimData(Texture mainTex, DrawTechnique tech, bool smoothShaderInput, out Rect uvRect, out Rect uvRectNext)
		{
			this.UpdateVisibleFrames();
			if (mainTex != null)
			{
				mainTex.LookupAtlas(this.curAnimFrame, out uvRect);
				if (smoothShaderInput)
					mainTex.LookupAtlas(this.nextAnimFrame, out uvRectNext);
				else
					uvRectNext = uvRect;
			}
			else if (mainTex != null)
				uvRect = uvRectNext = new Rect(mainTex.UVRatio.X, mainTex.UVRatio.Y);
			else
				uvRect = uvRectNext = new Rect(1.0f, 1.0f);
		}

		public override void Draw(IDrawDevice device)
		{
			Texture mainTex = this.RetrieveMainTex();
			ColorRgba mainClr = this.RetrieveMainColor();
			DrawTechnique tech = this.RetrieveDrawTechnique();

			Rect uvRect;
			Rect uvRectNext;
			bool smoothShaderInput = tech != null && tech.PreferredVertexFormat == DrawTechnique.VertexType_C1P3T4A1;
			this.GetAnimData(mainTex, tech, smoothShaderInput, out uvRect, out uvRectNext);

			if (!smoothShaderInput)
			{
				this.PrepareVertices(ref this.vertices, device, mainClr, uvRect);
				if (this.customMat != null)	device.AddVertices(this.customMat, VertexMode.Quads, this.vertices);
				else						device.AddVertices(this.sharedMat, VertexMode.Quads, this.vertices);
			}
			else
			{
				this.PrepareVerticesSmooth(ref this.verticesSmooth, device, this.curAnimFrameFade, mainClr, uvRect, uvRectNext);
				if (this.customMat != null)	device.AddVertices(this.customMat, VertexMode.Quads, this.verticesSmooth);
				else						device.AddVertices(this.sharedMat, VertexMode.Quads, this.verticesSmooth);
			}
		}
		protected override void OnCopyTo(Component target, Duality.Cloning.CloneProvider provider)
		{
			base.OnCopyTo(target, provider);
			AnimSpriteRenderer t = target as AnimSpriteRenderer;
			t.animDuration = this.animDuration;
			t.animFirstFrame = this.animFirstFrame;
			t.animFrameCount = this.animFrameCount;
			t.animLoopMode = this.animLoopMode;
			t.animTime = this.animTime;
			t.customFrameSequence = this.customFrameSequence != null ? this.customFrameSequence.ToList() : null;
		}
	}
}
