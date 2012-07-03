using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;
using OpenTK;

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
		/// Describes the state of a <see cref="Canvas"/>.
		/// </summary>
		public class State
		{
			private	BatchInfo			batchInfo;
			private	ColorRgba			color;
			private	ContentRef<Font>	font;
			private	bool		invariantTextScale;
			private	float		transformAngle;
			private	Vector2		transformScale;
			private	Vector2		transformHandle;

			private	Vector2		curTX;
			private	Vector2		curTY;


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
			/// [GET / SET] The <see cref="Duality.Resources.Font"/> to use for text rendering.
			/// </summary>
			public ContentRef<Font> TextFont
			{
				get { return this.font; }
				set { this.font = value.IsAvailable ? value : Font.GenericMonospace10; }
			}
			/// <summary>
			/// [GET / SET] If true, text does not scale due to its position in space
			/// </summary>
			public bool TextInvariantScale
			{
				get { return this.invariantTextScale; }
				set { this.invariantTextScale = value; }
			}
			/// <summary>
			/// [GET / SET] The color tint to use for drawing.
			/// </summary>
			public ColorRgba ColorTint
			{
				get { return this.color; }
				set { this.color = value; }
			}
			/// <summary>
			/// [GET / SET] The angle by which all shapes are transformed.
			/// </summary>
			public float TransformAngle
			{
				get { return this.transformAngle; }
				set { this.transformAngle = value; this.UpdateTransform(); }
			}
			/// <summary>
			/// [GET / SET] The scale by which all shapes are transformed.
			/// </summary>
			public Vector2 TransformScale
			{
				get { return this.transformScale; }
				set { this.transformScale = value; this.UpdateTransform(); }
			}
			/// <summary>
			/// [GET / SET] The handle used for transforming all shapes.
			/// </summary>
			public Vector2 TransformHandle
			{
				get { return this.transformHandle; }
				set { this.transformHandle = value; this.UpdateTransform(); }
			}


			public State() 
			{
				this.Reset();
			}
			public State(State other)
			{
				this.batchInfo = other.batchInfo;
				this.font = other.font;
				this.color = other.color;
				this.invariantTextScale = other.invariantTextScale;
				this.transformAngle = other.transformAngle;
				this.transformHandle = other.transformHandle;
				this.transformScale = other.transformScale;
				this.UpdateTransform();
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
				this.font = Font.GenericMonospace10;
				this.color = ColorRgba.White;
				this.invariantTextScale = false;
				this.transformAngle = 0.0f;
				this.transformHandle = Vector2.Zero;
				this.transformScale = Vector2.One;
				this.UpdateTransform();
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

			private void UpdateTransform()
			{
				MathF.GetTransformDotVec(
					this.transformAngle, 
					out this.curTX, 
					out this.curTY);
			}
			internal void TransformVertices<T>(T[] vertexData, Vector2 shapeHandle, float shapeHandleScale) where T : struct, IVertexData
			{
				this.UpdateTransform();
				Vector2 transformHandle = this.transformHandle;
				Vector2 transformScale = this.transformScale;
				for (int i = 0; i < vertexData.Length; i++)
				{
					Vector3 pos = vertexData[i].Pos;
					pos.X -= transformHandle.X * shapeHandleScale + shapeHandle.X;
					pos.Y -= transformHandle.Y * shapeHandleScale + shapeHandle.Y;
					pos.X *= transformScale.X;
					pos.Y *= transformScale.Y;
					MathF.TransformDotVec(ref pos, ref this.curTX, ref this.curTY);
					pos.X += shapeHandle.X;
					pos.Y += shapeHandle.Y;
					vertexData[i].Pos = pos;
				}
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
		/// Draws a predefined set of vertices using the Canvas transformation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="vertices"></param>
		/// <param name="mode"></param>
		public void DrawVertices<T>(T[] vertices, VertexMode mode) where T : struct, IVertexData
		{
			Vector3 pos = vertices[0].Pos;
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);

			this.CurrentState.TransformVertices(vertices, pos.Xy, scale);
			this.device.AddVertices<T>(this.CurrentState.MaterialDirect, mode, vertices);
		}

		/// <summary>
		/// Draws a convex polygon. All vertices share the same Z value.
		/// </summary>
		/// <param name="points"></param>
		/// <param name="z"></param>
		public void DrawConvexPolygon(Vector2[] points, float z = 0.0f)
		{
			Vector3 pos = new Vector3(points[0].X, points[0].Y, z);

			float scale = 1.0f;
			Vector3 posTemp = pos;
			this.device.PreprocessCoords(ref posTemp, ref scale);

			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				vertices[i].Pos.X = (points[i].X - pos.X) * scale + posTemp.X + 0.5f;
				vertices[i].Pos.Y = (points[i].Y - pos.Y) * scale + posTemp.Y + 0.5f;
				vertices[i].Pos.Z = (z - pos.Z) * scale + posTemp.Z;
				vertices[i].Color = shapeColor;
			}

			this.CurrentState.TransformVertices(vertices, pos.Xy, scale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);
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

			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r * scale, 0.65f) * 2.5f), 4, 128);
			Vector2 shapeHandle = pos.Xy;
			float shapeHandleScale = scale;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices;
			float angle;

			// XY circle
			vertices = new VertexC1P3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].Pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].Pos.Z = pos.Z;
				vertices[i].Color = shapeColor;
				this.device.PreprocessCoords(ref vertices[i].Pos, ref scale);
				angle += (MathF.TwoPi / segmentNum);
			}
			this.CurrentState.TransformVertices(vertices, shapeHandle, shapeHandleScale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);

			// XZ circle
			vertices = new VertexC1P3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].Pos.Y = pos.Y;
				vertices[i].Pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				vertices[i].Color = shapeColor;
				this.device.PreprocessCoords(ref vertices[i].Pos, ref scale);
				angle += (MathF.TwoPi / segmentNum);
			}
			this.CurrentState.TransformVertices(vertices, shapeHandle, shapeHandleScale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);

			// YZ circle
			vertices = new VertexC1P3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Pos.X = pos.X;
				vertices[i].Pos.Y = pos.Y + (float)Math.Sin(angle) * r;
				vertices[i].Pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				vertices[i].Color = shapeColor;
				this.device.PreprocessCoords(ref vertices[i].Pos, ref scale);
				angle += (MathF.TwoPi / segmentNum);
			}
			this.CurrentState.TransformVertices(vertices, shapeHandle, shapeHandleScale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);
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
			Vector3 pos = new Vector3(x, y, z);
			Vector3 target = new Vector3(x2, y2, z2);
			float scale = 1.0f;
			
			device.PreprocessCoords(ref pos, ref scale);
			device.PreprocessCoords(ref target, ref scale);

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[2];
			vertices[0].Pos = pos + new Vector3(0.5f, 0.5f, 0.0f);
			vertices[1].Pos = target + new Vector3(0.5f, 0.5f, 0.0f);
			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.Lines, vertices);
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
		/// Draws a thick, three-dimensional line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="z2"></param>
		public void DrawThickLine(float x, float y, float z, float x2, float y2, float z2, float width)
		{
			Vector3 pos = new Vector3(x, y, z);
			Vector3 target = new Vector3(x2, y2, z2);
			float scale = 1.0f;
			float scale2 = 1.0f;
			
			device.PreprocessCoords(ref pos, ref scale);
			device.PreprocessCoords(ref target, ref scale2);

			Vector2 dir = (target.Xy - pos.Xy).Normalized;
			Vector2 left = dir.PerpendicularLeft * width * 0.5f * scale;
			Vector2 right = dir.PerpendicularRight * width * 0.5f * scale;
			Vector2 left2 = dir.PerpendicularLeft * width * 0.5f * scale2;
			Vector2 right2 = dir.PerpendicularRight * width * 0.5f * scale2;

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[4];
			vertices[0].Pos = pos + new Vector3(left);
			vertices[1].Pos = target + new Vector3(left2);
			vertices[2].Pos = target + new Vector3(right2);
			vertices[3].Pos = pos + new Vector3(right);
			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			vertices[2].Color = shapeColor;
			vertices[3].Color = shapeColor;
			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);
		}
		/// <summary>
		/// Draws a thick, flat line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		public void DrawThickLine(float x, float y, float x2, float y2, float width)
		{
			this.DrawThickLine(x, y, 0, x2, y2, 0, width);
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
			if (w < 0.0f) { x += w; w = -w; }
			if (h < 0.0f) { y += h; h = -h; }

			Vector3 pos = new Vector3(x, y, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[4];
			vertices[0].Pos = new Vector3(pos.X + 0.5f, pos.Y + 0.5f, pos.Z);
			vertices[1].Pos = new Vector3(pos.X + w * scale + 0.5f, pos.Y + 0.5f, pos.Z);
			vertices[2].Pos = new Vector3(pos.X + w * scale + 0.5f, pos.Y + h * scale + 0.5f, pos.Z);
			vertices[3].Pos = new Vector3(pos.X + 0.5f, pos.Y + h * scale + 0.5f, pos.Z);

			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			vertices[2].Color = shapeColor;
			vertices[3].Color = shapeColor;

			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.LineLoop, vertices);
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
		/// Draws the section of an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawOvalSegment(float x, float y, float z, float w, float h, float minAngle, float maxAngle)
		{
			if (minAngle == maxAngle) return;
			if (w < 0.0f) { x += w; w = -w; }
			if (h < 0.0f) { y += h; h = -h; }
			w *= 0.5f; x += w;
			h *= 0.5f; y += h;

			Vector3 pos = new Vector3(x, y, z);
			if (!this.device.IsCoordInView(pos, MathF.Max(w, h) + this.CurrentState.TransformHandle.Length)) return;

			float scale = 1.0f;
			this.device.PreprocessCoords(ref pos, ref scale);
			w *= scale;
			h *= scale;

			minAngle = MathF.NormalizeAngle(minAngle);
			maxAngle = MathF.NormalizeAngle(maxAngle);
			if (maxAngle <= minAngle) maxAngle += MathF.RadAngle360;

			float angleRange = MathF.Min(maxAngle - minAngle, MathF.RadAngle360);
			bool loop = angleRange >= MathF.RadAngle360 - MathF.RadAngle1 * 0.001f;
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(MathF.Max(w, h), 0.65f) * 3.5f * angleRange / MathF.RadAngle360), 4, 128);
			float angleStep = angleRange / segmentNum;
			Vector2 shapeHandle = pos.Xy - new Vector2(w, h);
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[loop ? segmentNum : segmentNum + 1];
			float angle = minAngle;

			// XY circle
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].Pos.X = pos.X + (float)Math.Sin(angle) * w + 0.5f;
				vertices[i].Pos.Y = pos.Y - (float)Math.Cos(angle) * h + 0.5f;
				vertices[i].Pos.Z = pos.Z;
				vertices[i].Color = shapeColor;
				angle += angleStep;
			}
			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, loop ? VertexMode.LineLoop : VertexMode.LineStrip, vertices);
		}
		/// <summary>
		/// Draws the section of an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawOvalSegment(float x, float y, float w, float h, float minAngle, float maxAngle)
		{
			this.DrawOvalSegment(x, y, 0, w, h, minAngle, maxAngle);
		}
		/// <summary>
		/// Draws the section of a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawCircleSegment(float x, float y, float z, float r, float minAngle, float maxAngle)
		{
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.DrawOvalSegment(x, y, z, r * 2, r * 2, minAngle, maxAngle);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
		}
		/// <summary>
		/// Draws the section of a circle
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawCircleSegment(float x, float y, float r, float minAngle, float maxAngle)
		{
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.DrawOvalSegment(x, y, 0, r * 2, r * 2, minAngle, maxAngle);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
		}

		/// <summary>
		/// Draws the section of an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawOval(float x, float y, float z, float w, float h)
		{
			this.DrawOvalSegment(x, y, z, w, h, 0.0f, MathF.RadAngle360);
		}
		/// <summary>
		/// Draws the section of an oval.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawOval(float x, float y, float w, float h)
		{
			this.DrawOvalSegment(x, y, 0, w, h, 0.0f, MathF.RadAngle360);
		}
		/// <summary>
		/// Draws the section of a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawCircle(float x, float y, float z, float r)
		{
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.DrawOvalSegment(x, y, z, r * 2, r * 2, 0.0f, MathF.RadAngle360);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
		}
		/// <summary>
		/// Draws the section of a circle
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		/// <param name="minAngle"></param>
		/// <param name="maxAngle"></param>
		public void DrawCircle(float x, float y, float r)
		{
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.DrawOvalSegment(x, y, 0, r * 2, r * 2, 0.0f, MathF.RadAngle360);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
		}
		
		/// <summary>
		/// Fills a convex polygon. All vertices share the same Z value.
		/// </summary>
		/// <param name="points"></param>
		/// <param name="z"></param>
		public void FillConvexPolygon(Vector2[] points, float z = 0.0f)
		{
			Vector3 pos = new Vector3(points[0].X, points[0].Y, z);

			float scale = 1.0f;
			Vector3 posTemp = pos;
			this.device.PreprocessCoords(ref posTemp, ref scale);

			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				vertices[i].Pos.X = (points[i].X - pos.X) * scale + posTemp.X;
				vertices[i].Pos.Y = (points[i].Y - pos.Y) * scale + posTemp.Y;
				vertices[i].Pos.Z = (z - pos.Z) * scale + posTemp.Z;
				vertices[i].Color = shapeColor;
			}

			this.CurrentState.TransformVertices(vertices, pos.Xy, scale);
			this.device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.Polygon, vertices);
		}

		/// <summary>
		/// Fills a three-dimensional line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="z2"></param>
		public void FillThickLine(float x, float y, float z, float x2, float y2, float z2, float width)
		{
			Vector3 pos = new Vector3(x, y, z);
			Vector3 target = new Vector3(x2, y2, z2);
			float scale = 1.0f;
			float scale2 = 1.0f;
			
			device.PreprocessCoords(ref pos, ref scale);
			device.PreprocessCoords(ref target, ref scale2);

			Vector2 dir = (target.Xy - pos.Xy).Normalized;
			Vector2 left = dir.PerpendicularLeft * width * 0.5f * scale;
			Vector2 right = dir.PerpendicularRight * width * 0.5f * scale;
			Vector2 left2 = dir.PerpendicularLeft * width * 0.5f * scale2;
			Vector2 right2 = dir.PerpendicularRight * width * 0.5f * scale2;

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[4];
			vertices[0].Pos = pos + new Vector3(left);
			vertices[1].Pos = target + new Vector3(left2);
			vertices[2].Pos = target + new Vector3(right2);
			vertices[3].Pos = pos + new Vector3(right);
			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			vertices[2].Color = shapeColor;
			vertices[3].Color = shapeColor;
			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.Quads, vertices);
		}
		/// <summary>
		/// Fills a thick, flat line.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		public void FillThickLine(float x, float y, float x2, float y2, float width)
		{
			this.FillThickLine(x, y, 0, x2, y2, 0, width);
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
			if (w < 0.0f) { x += w; w = -w; }
			if (h < 0.0f) { y += h; h = -h; }
			w *= 0.5f; x += w;
			h *= 0.5f; y += h;

			Vector3 pos = new Vector3(x, y, z);
			if (!this.device.IsCoordInView(pos, MathF.Max(w, h) + this.CurrentState.TransformHandle.Length)) return;

			float scale = 1.0f;
			this.device.PreprocessCoords(ref pos, ref scale);
			w *= scale;
			h *= scale;

			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(MathF.Max(w, h), 0.65f) * 3.5f), 4, 128);
			float angle = 0.0f;
			Vector2 shapeHandle = pos.Xy - new Vector2(w, h);
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[segmentNum + 2];

			vertices[0].Pos = pos;
			vertices[0].Color = shapeColor;
			for (int i = 1; i < vertices.Length; i++)
			{
				vertices[i].Pos.X = pos.X + (float)Math.Sin(angle) * w;
				vertices[i].Pos.Y = pos.Y - (float)Math.Cos(angle) * h;
				vertices[i].Pos.Z = pos.Z;
				vertices[i].Color = shapeColor;
				angle += (MathF.TwoPi / segmentNum);
			}
			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.TriangleFan, vertices);
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
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.FillOval(x, y, z, r * 2, r * 2);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
		}
		/// <summary>
		/// Fills a circle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		public void FillCircle(float x, float y, float r)
		{
			this.CurrentState.TransformHandle += new Vector2(r, r);
			this.FillOval(x, y, 0, r * 2, r * 2);
			this.CurrentState.TransformHandle -= new Vector2(r, r);
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
			if (w < 0.0f) { x += w; w = -w; }
			if (h < 0.0f) { y += h; h = -h; }

			Vector3 pos = new Vector3(x, y, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3[] vertices = new VertexC1P3[4];
			vertices[0].Pos = new Vector3(pos.X, pos.Y, pos.Z);
			vertices[1].Pos = new Vector3(pos.X + w * scale, pos.Y, pos.Z);
			vertices[2].Pos = new Vector3(pos.X + w * scale, pos.Y + h * scale, pos.Z);
			vertices[3].Pos = new Vector3(pos.X, pos.Y + h * scale, pos.Z);

			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			vertices[2].Color = shapeColor;
			vertices[3].Color = shapeColor;

			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.Quads, vertices);
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

		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="uvX">UV x coordinate</param>
		/// <param name="uvY">UV y coordinate</param>
		/// <param name="uvW">UV coordinate width</param>
		/// <param name="uvH">UV coordinate height</param>
		public void DrawTexturedRect(float x, float y, float z, float w, float h, float uvX, float uvY, float uvW, float uvH)
		{
			if (w < 0.0f) { x += w; w = -w; }
			if (h < 0.0f) { y += h; h = -h; }

			Vector3 pos = new Vector3(x, y, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);

			Texture mainTex = this.CurrentState.MaterialDirect.MainTexture.Res;
			Vector2 mainTexUVRatio = mainTex != null ? mainTex.UVRatio : Vector2.One;

			Vector2 shapeHandle = pos.Xy;
			ColorRgba shapeColor = this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor;
			VertexC1P3T2[] vertices = new VertexC1P3T2[4];

			vertices[0].Pos = new Vector3(pos.X, pos.Y, pos.Z);
			vertices[1].Pos = new Vector3(pos.X + w * scale, pos.Y, pos.Z);
			vertices[2].Pos = new Vector3(pos.X + w * scale, pos.Y + h * scale, pos.Z);
			vertices[3].Pos = new Vector3(pos.X, pos.Y + h * scale, pos.Z);

			vertices[0].TexCoord = new Vector2(uvX * mainTexUVRatio.X, uvY * mainTexUVRatio.Y);
			vertices[1].TexCoord = new Vector2((uvX + uvW) * mainTexUVRatio.X, uvY * mainTexUVRatio.Y);
			vertices[2].TexCoord = new Vector2((uvX + uvW) * mainTexUVRatio.X, (uvY + uvH) * mainTexUVRatio.Y);
			vertices[3].TexCoord = new Vector2(uvX * mainTexUVRatio.X, (uvY + uvH) * mainTexUVRatio.Y);

			vertices[0].Color = shapeColor;
			vertices[1].Color = shapeColor;
			vertices[2].Color = shapeColor;
			vertices[3].Color = shapeColor;

			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			device.AddVertices(this.CurrentState.MaterialDirect, VertexMode.Quads, vertices);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="uvX">UV x coordinate</param>
		/// <param name="uvY">UV y coordinate</param>
		/// <param name="uvW">UV coordinate width</param>
		/// <param name="uvH">UV coordinate height</param>
		public void DrawTexturedRect(float x, float y, float z, float uvX, float uvY, float uvW, float uvH)
		{
			Texture mainTex = this.CurrentState.MaterialDirect.MainTexture.Res;
			Vector2 mainTexSize = mainTex != null ? mainTex.Size : Vector2.One * 10.0f;
			this.DrawTexturedRect(x, y, z, mainTexSize.X, mainTexSize.Y, uvX, uvY, uvW, uvH);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawTexturedRect(float x, float y, float z, float w, float h)
		{
			this.DrawTexturedRect(x, y, z, w, h, 0.0f, 0.0f, 1.0f, 1.0f);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void DrawTexturedRect(float x, float y, float z)
		{
			Texture mainTex = this.CurrentState.MaterialDirect.MainTexture.Res;
			Vector2 mainTexSize = mainTex != null ? mainTex.Size : Vector2.One * 10.0f;
			this.DrawTexturedRect(x, y, z, mainTexSize.X, mainTexSize.Y, 0.0f, 0.0f, 1.0f, 1.0f);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="uvX">UV x coordinate</param>
		/// <param name="uvY">UV y coordinate</param>
		/// <param name="uvW">UV coordinate width</param>
		/// <param name="uvH">UV coordinate height</param>
		public void DrawTexturedRect(float x, float y, float w, float h, float uvX, float uvY, float uvW, float uvH)
		{
			this.DrawTexturedRect(x, y, 0, w, h, uvX, uvY, uvW, uvH);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="uvX">UV x coordinate</param>
		/// <param name="uvY">UV y coordinate</param>
		/// <param name="uvW">UV coordinate width</param>
		/// <param name="uvH">UV coordinate height</param>
		public void DrawTexturedRect(float x, float y, float uvX, float uvY, float uvW, float uvH)
		{
			Texture mainTex = this.CurrentState.MaterialDirect.MainTexture.Res;
			Vector2 mainTexSize = mainTex != null ? mainTex.Size : Vector2.One * 10.0f;
			this.DrawTexturedRect(x, y, 0, mainTexSize.X, mainTexSize.Y, uvX, uvY, uvW, uvH);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public void DrawTexturedRect(float x, float y, float w, float h)
		{
			this.DrawTexturedRect(x, y, 0, w, h, 0.0f, 0.0f, 1.0f, 1.0f);
		}
		/// <summary>
		/// Draws a textured rectangle.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void DrawTexturedRect(float x, float y)
		{
			Texture mainTex = this.CurrentState.MaterialDirect.MainTexture.Res;
			Vector2 mainTexSize = mainTex != null ? mainTex.Size : Vector2.One * 10.0f;
			this.DrawTexturedRect(x, y, 0, mainTexSize.X, mainTexSize.Y, 0.0f, 0.0f, 1.0f, 1.0f);
		}

		/// <summary>
		/// Draws the specified text string.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void DrawText(string text, float x, float y, float z)
		{
			Vector3 pos = new Vector3(x, y, z);
			float scale = 1.0f;
			device.PreprocessCoords(ref pos, ref scale);
			if (this.CurrentState.TextInvariantScale) scale = 1.0f;

			Vector2 shapeHandle = pos.Xy;
			VertexC1P3T2[] vertices = null;
			Font font = this.CurrentState.TextFont.Res;

			font.EmitTextVertices(text, ref vertices, pos.X, pos.Y, pos.Z, this.CurrentState.ColorTint * this.CurrentState.MaterialDirect.MainColor, 0.0f, scale);

			this.CurrentState.TransformVertices(vertices, shapeHandle, scale);
			BatchInfo customMat = new BatchInfo(this.CurrentState.MaterialDirect);
			customMat.MainTexture = font.Material.MainTexture;
			device.AddVertices(customMat, VertexMode.Quads, vertices);
		}
		/// <summary>
		/// Draws the specified text string.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void DrawText(string text, float x, float y)
		{
			this.DrawText(text, x, y, 0);
		}

		/// <summary>
		/// Measures the specified text using the currently used <see cref="Duality.Resources.Font"/>.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public Vector2 MeasureText(string text)
		{
			Font font = this.CurrentState.TextFont.Res;
			return font.MeasureText(text);
		}
	}
}