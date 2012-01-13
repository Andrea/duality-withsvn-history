using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Provides high level drawing operations on top of an existing <see cref="IDrawDevice"/>. However, this class is not designed
	/// for drawing large batches of primitives / vertices at once. For large amounts of primitives you should consider directly 
	/// using the underlying IDrawDevice instead to achieve best performance.
	/// </summary>
	public class Canvas
	{
		/// <summary>
		/// Determines whether and how drawn shapes are textured.
		/// </summary>
		public enum TextureMode
		{
			/// <summary>
			/// Textures are ignored. No UV coordinates are generated.
			/// </summary>
			Plain,
			/// <summary>
			/// Shapes are textured. UV coordinates are generated based on <see cref="Canvas.State.TexUVTarget"/>.
			/// </summary>
			Textured
		}

		/// <summary>
		/// Describes the state of a <see cref="Canvas"/>.
		/// </summary>
		public class State
		{
			private VertexDataFactory	vertexDataFactory;
			private	BatchInfo			batchInfo;
			private TextureMode			texMode;
			private	Rect				texUVTarget;
			private	float				transformAngle;
			private	Vector2				transformScale;
			private	Vector2				transformHandle;


			internal VertexDataFactory VertexFactory
			{
				get { return this.vertexDataFactory; }
			}
			internal BatchInfo MaterialDirect
			{
				get { return this.batchInfo; }
			}
			/// <summary>
			/// [GET] The material that is used for drawing.
			/// </summary>
			public BatchInfo Material
			{
				get { return new BatchInfo(this.batchInfo); }
			}
			/// <summary>
			/// [GET / SET] The texturing mode that is used when drawing shapes.
			/// </summary>
			public TextureMode TexMode
			{
				get { return this.texMode; }
				set 
				{ 
					this.texMode = value;
					this.UpdateVertexDataFactory();
				}
			}
			/// <summary>
			/// [GET / SET] The target UV rect when texturing a shape.
			/// </summary>
			public Rect TexUVTarget
			{
				get { return this.texUVTarget; }
				set { this.texUVTarget = value; }
			}
			/// <summary>
			/// [GET / SET] The angle by which all shapes are transformed.
			/// </summary>
			public float TransformAngle
			{
				get { return this.transformAngle; }
				set { this.transformAngle = value; }
			}
			/// <summary>
			/// [GET / SET] The scale by which all shapes are transformed.
			/// </summary>
			public Vector2 TransformScale
			{
				get { return this.transformScale; }
				set { this.transformScale = value; }
			}
			/// <summary>
			/// [GET / SET] The handle used for transforming all shapes.
			/// </summary>
			public Vector2 TransformHandle
			{
				get { return this.transformHandle; }
				set { this.transformHandle = value; }
			}


			public State() 
			{
				this.Reset();
			}
			public State(State other)
			{
				this.batchInfo = other.batchInfo;
				this.texMode = other.texMode;
				this.texUVTarget = other.texUVTarget;
				this.transformAngle = other.transformAngle;
				this.transformHandle = other.transformHandle;
				this.transformScale = other.transformScale;
				this.UpdateVertexDataFactory();
			}

			/// <summary>
			/// Creates a clone of this State.
			/// </summary>
			/// <returns></returns>
			public State Clone()
			{
				return new State(this);
			}
			/// <summary>
			/// Resets this State to its initial settings.
			/// </summary>
			public void Reset()
			{
				this.batchInfo = new BatchInfo(DrawTechnique.Solid, ColorRgba.White);
				this.texMode = TextureMode.Plain;
				this.texUVTarget = new Rect(1.0f, 1.0f);
				this.transformAngle = 0.0f;
				this.transformHandle = Vector2.Zero;
				this.transformScale = Vector2.One;
				this.UpdateVertexDataFactory();
			}

			/// <summary>
			/// Sets the States drawing material.
			/// </summary>
			/// <param name="material"></param>
			public void SetMaterial(BatchInfo material)
			{
				this.batchInfo = material;
			}
			/// <summary>
			/// Sets the States drawing material.
			/// </summary>
			/// <param name="material"></param>
			public void SetMaterial(ContentRef<Material> material)
			{
				this.batchInfo = material.Res.InfoDirect;
			}

			private void UpdateVertexDataFactory()
			{
				if (this.texMode == TextureMode.Plain)
					this.vertexDataFactory = new PlainVertexFactory(this);
				else if (this.texMode == TextureMode.Textured)
					this.vertexDataFactory = new TexturedVertexFactory(this);
				else
					this.vertexDataFactory = new PlainVertexFactory(this);
			}
		}

		internal abstract class VertexDataFactory
		{
			protected	State	parentState;
			protected	Vector2	shapeHandle;
			protected	Vector2	curTX;
			protected	Vector2	curTY;

			protected VertexDataFactory(State state)
			{
				this.parentState = state;
			}

			public virtual void BeginShape(int vertexCount, float xHandle, float yHandle)
			{
				this.shapeHandle.X = xHandle;
				this.shapeHandle.Y = yHandle;
			}
			public abstract void PlotVertex(int index, float x, float y, float z);
			public abstract void SubmitShape(IDrawDevice device, BatchInfo batchInfo, BeginMode vertexMode);
			
			protected void UpdateTransform()
			{
				MathF.GetTransformDotVec(
					this.parentState.TransformAngle, 
					out this.curTX, 
					out this.curTY);
			}
			protected Rect CalcBoundingRect<T>(T[] vertexData) where T : IVertexData
			{
				Rect refRect = new Rect(10000000.0f, 10000000.0f, -10000000.0f, -10000000.0f);
				for (int i = 0; i < vertexData.Length; i++)
				{
					Vector3 pos = vertexData[i].Pos;
					refRect.x = MathF.Min(refRect.x, pos.X);
					refRect.y = MathF.Min(refRect.y, pos.Y);
					refRect.w = MathF.Max(refRect.w, pos.X);
					refRect.h = MathF.Max(refRect.h, pos.Y);
				}
				refRect.w -= refRect.x;
				refRect.h -= refRect.y;
				return refRect;
			}
			protected void TransformVertices<T>(T[] vertexData) where T : IVertexData
			{
				this.UpdateTransform();
				Vector2 transformHandle = this.parentState.TransformHandle;
				Vector2 transformScale = this.parentState.TransformScale;
				for (int i = 0; i < vertexData.Length; i++)
				{
					Vector3 pos = vertexData[i].Pos;
					pos.X -= transformHandle.X + this.shapeHandle.X;
					pos.Y -= transformHandle.Y + this.shapeHandle.Y;
					pos.X *= transformScale.X;
					pos.Y *= transformScale.Y;
					MathF.TransformDotVec(ref pos, ref this.curTX, ref this.curTY);
					pos.X += this.shapeHandle.X;
					pos.Y += this.shapeHandle.Y;
					vertexData[i].Pos = pos;
				}
			}
		}
		private class PlainVertexFactory : VertexDataFactory
		{
			private	VertexP3[] vertexData;

			public PlainVertexFactory(State state) : base(state) {}
			
			public override void BeginShape(int vertexCount, float xHandle, float yHandle)
			{
				base.BeginShape(vertexCount, xHandle, yHandle);
				this.vertexData = new VertexP3[vertexCount];
			}
			public override void PlotVertex(int index, float x, float y, float z)
			{
				this.vertexData[index].pos.X = x;
				this.vertexData[index].pos.Y = y;
				this.vertexData[index].pos.Z = z;
			}
			public override void SubmitShape(IDrawDevice device, BatchInfo batchInfo, BeginMode vertexMode)
			{
				// Transform vertices
				this.TransformVertices(this.vertexData);

				device.AddVertices(batchInfo, vertexMode, this.vertexData);
			}
		}
		private class TexturedVertexFactory : VertexDataFactory
		{
			private	VertexP3T2[]	vertexData;

			public TexturedVertexFactory(State state) : base(state) {}
			
			public override void BeginShape(int vertexCount, float xHandle, float yHandle)
			{
				base.BeginShape(vertexCount, xHandle, yHandle);
				this.vertexData = new VertexP3T2[vertexCount];
			}
			public override void PlotVertex(int index, float x, float y, float z)
			{
				this.vertexData[index].pos.X = x;
				this.vertexData[index].pos.Y = y;
				this.vertexData[index].pos.Z = z;
			}
			public override void SubmitShape(IDrawDevice device, BatchInfo batchInfo, BeginMode vertexMode)
			{
				// Calculate reference bounding rect
				Rect refRect = this.CalcBoundingRect(this.vertexData);

				// Calculate UV coordinates
				Rect targetUV = this.parentState.TexUVTarget;
				Texture mainTex = batchInfo.Textures.Count > 0 ? batchInfo.Textures.First().Value.Res : null;
				Vector2 matUVRatio = mainTex != null ? mainTex.UVRatio : Vector2.One;
				for (int i = 0; i < this.vertexData.Length; i++)
				{
					this.vertexData[i].texCoord.X = matUVRatio.X * (targetUV.x + ((this.vertexData[i].pos.X - refRect.x) / refRect.w) * targetUV.w);
					this.vertexData[i].texCoord.Y = matUVRatio.Y * (targetUV.y + ((this.vertexData[i].pos.Y - refRect.y) / refRect.h) * targetUV.h);
				}

				// Transform vertices
				this.TransformVertices(this.vertexData);

				device.AddVertices(batchInfo, vertexMode, this.vertexData);
			}
		}

		private	IDrawDevice		device		= null;
		private	Stack<State>	stateStack	= new Stack<State>(new [] { new State() });

		/// <summary>
		/// [GET] The underlying <see cref="IDrawDevice"/> that is used for drawing.
		/// </summary>
		public IDrawDevice DrawDevice
		{
			get { return this.device; }
		}
		/// <summary>
		/// [GET / SET] The Canvas' current <see cref="State"/>.
		/// </summary>
		public State CurrentState
		{
			get { return this.stateStack.Peek(); }
			set 
			{
				this.stateStack.Pop();
				this.stateStack.Push(value);
			}
		}

		public Canvas(IDrawDevice device)
		{
			this.device = device;
		}
		
		/// <summary>
		/// Adds a clone of the <see cref="Canvas.CurrentState">current state</see> on top of the internal
		/// <see cref="State"/> stack.
		/// </summary>
		public void PushState()
		{
			this.stateStack.Push(this.stateStack.Peek().Clone());
		}
		/// <summary>
		/// Removes the topmost <see cref="State"/> from the internal State stack.
		/// </summary>
		public void PopState()
		{
			this.stateStack.Pop();
			if (this.stateStack.Count == 0) this.stateStack.Push(new State());
		}

		
		/// <summary>
		/// Draws a three-dimensional sphere.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		public void DrawSphere(float x, float y, float z, float r)
		{
			r = MathF.Abs(r);
			Vector3 pos = new Vector3(x, y, z);
			if (!this.device.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			Vector3 posTemp = pos;
			this.device.PreprocessCoords(ref posTemp, ref scale);
			Vector3 handle = posTemp - new Vector3(r, r, 0);

			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r * scale, 0.65f) * 2.5f), 4, 128);
			float angle;
			VertexDataFactory vertices = this.CurrentState.VertexFactory;

			// XY circle
			vertices.BeginShape(segmentNum, handle.X, handle.Y);
			angle = 0.0f;
			for (int i = 0; i < segmentNum; i++)
			{
				posTemp.X = pos.X + (float)Math.Sin(angle) * r;
				posTemp.Y = pos.Y - (float)Math.Cos(angle) * r;
				posTemp.Z = pos.Z;
				this.device.PreprocessCoords(ref posTemp, ref scale);
				vertices.PlotVertex(i, posTemp.X, posTemp.Y, posTemp.Z);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.LineLoop);

			// XZ circle
			vertices.BeginShape(segmentNum, handle.X, handle.Y);
			angle = 0.0f;
			for (int i = 0; i < segmentNum; i++)
			{
				posTemp.X = pos.X + (float)Math.Sin(angle) * r;
				posTemp.Y = pos.Y;
				posTemp.Z = pos.Z - (float)Math.Cos(angle) * r;
				this.device.PreprocessCoords(ref posTemp, ref scale);
				vertices.PlotVertex(i, posTemp.X, posTemp.Y, posTemp.Z);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.LineLoop);

			// YZ circle
			vertices.BeginShape(segmentNum, handle.X, handle.Y);
			angle = 0.0f;
			for (int i = 0; i < segmentNum; i++)
			{
				posTemp.X = pos.X;
				posTemp.Y = pos.Y + (float)Math.Sin(angle) * r;
				posTemp.Z = pos.Z - (float)Math.Cos(angle) * r;
				this.device.PreprocessCoords(ref posTemp, ref scale);
				vertices.PlotVertex(i, posTemp.X, posTemp.Y, posTemp.Z);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.LineLoop);
		}

		/// <summary>
		/// Draws a three-dimensional line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="z2"></param>
		public void DrawLine(float x, float y, float z, float x2, float y2, float z2)
		{
			Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, z);
			Vector3 target = new Vector3(x2 + 0.5f, y2 + 0.5f, z2);
			float scale = 1.0f;
			
			device.PreprocessCoords(ref pos, ref scale);
			device.PreprocessCoords(ref target, ref scale);

			VertexDataFactory vertices = this.CurrentState.VertexFactory;

			vertices.BeginShape(2, pos.X, pos.Y);
			vertices.PlotVertex(0, pos.X, pos.Y, pos.Z);
			vertices.PlotVertex(1, target.X, target.Y, target.Z);
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.Lines);
		}
		/// <summary>
		/// Draws a flat line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		public void DrawLine(float x, float y, float x2, float y2)
		{
			this.DrawLine(x, y, 0, x2, y2, 0);
		}

		/// <summary>
		/// Draws a rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawRect(float x, float y, float z, float w, float h)
		{
			if (w < 0.0f) { x -= w; w = -w; }
			if (h < 0.0f) { y -= h; h = -h; }

			Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);
			
			VertexDataFactory vertices = this.CurrentState.VertexFactory;

			vertices.BeginShape(4, pos.X, pos.Y);
			vertices.PlotVertex(0, pos.X, pos.Y, pos.Z);
			vertices.PlotVertex(1, pos.X + w * scale, pos.Y, pos.Z);
			vertices.PlotVertex(2, pos.X + w * scale, pos.Y + h * scale, pos.Z);
			vertices.PlotVertex(3, pos.X, pos.Y + h * scale, pos.Z);
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.LineLoop);
		}
		/// <summary>
		/// Draws a rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawRect(float x, float y, float w, float h)
		{
			this.DrawRect(x, y, 0, w, h);
		}
		
		/// <summary>
		/// Draws an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawOval(float x, float y, float z, float w, float h)
		{
			if (w < 0.0f) { x -= w; w = -w; }
			if (h < 0.0f) { y -= h; h = -h; }
			w *= 0.5f; x += w;
			h *= 0.5f; y += h;

			Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, z);
			if (!this.device.IsCoordInView(pos, MathF.Max(w, h))) return;

			float scale = 1.0f;
			this.device.PreprocessCoords(ref pos, ref scale);
			w *= scale;
			h *= scale;

			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(MathF.Max(w, h), 0.65f) * 2.5f), 4, 128);
			float angle;

			VertexDataFactory vertices = this.CurrentState.VertexFactory;
			vertices.BeginShape(segmentNum, pos.X - w, pos.Y - h);
			angle = 0.0f;
			for (int i = 0; i < segmentNum; i++)
			{
				vertices.PlotVertex(i, 
					pos.X + (float)Math.Sin(angle) * w,
					pos.Y - (float)Math.Cos(angle) * h,
					pos.Z);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.LineLoop);
		}
		/// <summary>
		/// Draws an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawOval(float x, float y, float w, float h)
		{
			this.DrawOval(x, y, 0, w, h);
		}
		/// <summary>
		/// Draws a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		public void DrawCircle(float x, float y, float z, float r)
		{
			this.DrawOval(x - r, y - r, z, r * 2, r * 2);
		}
		/// <summary>
		/// Draws a circle
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		public void DrawCircle(float x, float y, float r)
		{
			this.DrawOval(x - r, y - r, 0, r * 2, r * 2);
		}

		/// <summary>
		/// Fills an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void FillOval(float x, float y, float z, float w, float h)
		{
			if (w < 0.0f) { x -= w; w = -w; }
			if (h < 0.0f) { y -= h; h = -h; }
			w *= 0.5f; x += w;
			h *= 0.5f; y += h;

			Vector3 pos = new Vector3(x, y, z);
			if (!this.device.IsCoordInView(pos, MathF.Max(w, h))) return;

			float scale = 1.0f;
			this.device.PreprocessCoords(ref pos, ref scale);
			w *= scale;
			h *= scale;

			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(MathF.Max(w, h), 0.65f) * 2.5f), 4, 128);
			float angle = 0.0f;

			VertexDataFactory vertices = this.CurrentState.VertexFactory;

			vertices.BeginShape(segmentNum + 2, pos.X - w, pos.Y - h);
			vertices.PlotVertex(0, pos.X, pos.Y, pos.Z);
			for (int i = 1; i < segmentNum + 2; i++)
			{
				vertices.PlotVertex(i, 
					pos.X + (float)Math.Sin(angle) * w,
					pos.Y - (float)Math.Cos(angle) * h,
					pos.Z);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.TriangleFan);
		}
		/// <summary>
		/// Fills an oval
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void FillOval(float x, float y, float w, float h)
		{
			this.FillOval(x, y, 0, w, h);
		}
		/// <summary>
		/// Fills a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		public void FillCircle(float x, float y, float z, float r)
		{
			this.FillOval(x - r, y - r, z, r * 2, r * 2);
		}
		/// <summary>
		/// Fills a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		public void FillCircle(float x, float y, float r)
		{
			this.FillOval(x - r, y - r, 0, r * 2, r * 2);
		}

		/// <summary>
		/// Fills a rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void FillRect(float x, float y, float z, float w, float h)
		{
			if (w < 0.0f) { x -= w; w = -w; }
			if (h < 0.0f) { y -= h; h = -h; }

			Vector3 pos = new Vector3(x, y, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);
			
			VertexDataFactory vertices = this.CurrentState.VertexFactory;

			vertices.BeginShape(4, pos.X, pos.Y);
			vertices.PlotVertex(0, pos.X, pos.Y, pos.Z);
			vertices.PlotVertex(1, pos.X + w * scale, pos.Y, pos.Z);
			vertices.PlotVertex(2, pos.X + w * scale, pos.Y + h * scale, pos.Z);
			vertices.PlotVertex(3, pos.X, pos.Y + h * scale, pos.Z);
			vertices.SubmitShape(device, this.CurrentState.MaterialDirect, BeginMode.Quads);
		}
		/// <summary>
		/// Fills a rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void FillRect(float x, float y, float w, float h)
		{
			this.FillRect(x, y, 0, w, h);
		}
	}
}