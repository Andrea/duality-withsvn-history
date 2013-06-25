﻿using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality.EditorHints;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Specifies the way in which incoming vertex data is interpreted in order to generate geometry.
	/// </summary>
	public enum VertexMode
	{
		Points = BeginMode.Points,
		Lines = BeginMode.Lines,
		LineStrip = BeginMode.LineStrip,
		LineLoop = BeginMode.LineLoop,
		Triangles = BeginMode.Triangles,
		TriangleStrip = BeginMode.TriangleStrip,
		TriangleFan = BeginMode.TriangleFan,
		Quads = BeginMode.Quads,
		QuadStrip = BeginMode.QuadStrip,
		Polygon = BeginMode.Polygon
	}

	/// <summary>
	/// Specifies the perspective effect that is applied when rendering the world.
	/// </summary>
	public enum PerspectiveMode
	{
		/// <summary>
		/// No perspective effect is applied. Z points into the screen and is only used for object sorting.
		/// </summary>
		Flat,
		/// <summary>
		/// Objects that are far away appear smaller. Z points into the screen and is used for scaling and sorting.
		/// </summary>
		Parallax
	}

	/// <summary>
	/// Specifies a rendering matrix setup.
	/// </summary>
	public enum RenderMatrix
	{
		PerspectiveWorld,
		OrthoScreen
	}

	/// <summary>
	/// Enumerates different behviours on how to blend color data onto existing background color.
	/// </summary>
	/// <seealso cref="Duality.Resources.DrawTechnique"/>
	public enum BlendMode
	{
		/// <summary>
		/// When passing this to a method, this value can be used to indicate "Restore to default settings".
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		Reset = -1,

		/// <summary>
		/// Incoming color overwrites background color completely. Doesn't need Z-Sorting.
		/// </summary>
		Solid,
		/// <summary>
		/// Incoming color overwrites background color but leaves out areas with low alpha. Doesn't need Z-Sorting.
		/// </summary>
		Mask,

		/// <summary>
		/// Incoming color is multiplied by its alpha value and then added to background color. Needs Z-Sorting.
		/// </summary>
		Add,
		/// <summary>
		/// Incoming color overwrites background color weighted by its alpha value. Needs Z-Sorting.
		/// </summary>
		Alpha,
		/// <summary>
		/// Incoming color scales background color. Needs Z-Sorting.
		/// </summary>
		Multiply,
		/// <summary>
		/// Incoming color is multiplied and then added to background color. Needs Z-Sorting.
		/// </summary>
		Light,
		/// <summary>
		/// Incoming color inverts background color. Needs Z-Sorting.
		/// </summary>
		Invert,

		/// <summary>
		/// The total number of available BlendModes.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		Count
	}
	
	/// <summary>
	/// A Bitmask describing which components of the current rendering buffer to clear.
	/// </summary>
	[Flags]
	public enum ClearFlag
	{
		/// <summary>
		/// Nothing.
		/// </summary>
		None	= 0x0,

		/// <summary>
		/// The buffers color components.
		/// </summary>
		Color	= 0x1,
		/// <summary>
		/// The buffers depth component.
		/// </summary>
		Depth	= 0x2,

		/// <summary>
		/// The default set of flags.
		/// </summary>
		Default	= Color | Depth,
		/// <summary>
		/// All flags set.
		/// </summary>
		All		= Color | Depth
	}

	[Flags]
	public enum VisibilityFlag : uint
	{
		None = 0U,

		// User-defined groups
		Group0 = 1U << 0,
		Group1 = 1U << 1,
		Group2 = 1U << 2,
		Group3 = 1U << 3,
		Group4 = 1U << 4,
		Group5 = 1U << 5,
		Group6 = 1U << 6,
		Group7 = 1U << 7,
		Group8 = 1U << 8,
		Group9 = 1U << 9,
		Group10 = 1U << 10,
		Group11 = 1U << 11,
		Group12 = 1U << 12,
		Group13 = 1U << 13,
		Group14 = 1U << 14,
		Group15 = 1U << 15,
		Group16 = 1U << 16,
		Group17 = 1U << 17,
		Group18 = 1U << 18,
		Group19 = 1U << 19,
		Group20 = 1U << 20,
		Group21 = 1U << 21,
		Group22 = 1U << 22,
		Group23 = 1U << 23,
		Group24 = 1U << 24,
		Group25 = 1U << 25,
		Group26 = 1U << 26,
		Group27 = 1U << 27,
		Group28 = 1U << 28,
		Group29 = 1U << 29,
		Group30 = 1U << 30,

		// Special groups (Might cause special behaviour)
		/// <summary>
		/// Special flag. This flag is set when rendering screen overlays.
		/// </summary>
		ScreenOverlay = 1U << 31,

		// Compound groups
		All = uint.MaxValue,
		AllFlags = ScreenOverlay,
		AllGroups = All & (~AllFlags)
	}

	/// <summary>
	/// Defines a general interface for drawing devices. Its main duty is to accept and collect parameterized vertex data.
	/// </summary>
	public interface IDrawDevice
	{
		/// <summary>
		/// [GET] Reference coordinate for rendering i.e. the position of the drawing device's Camera.
		/// </summary>
		Vector3 RefCoord { get; }
		/// <summary>
		/// [GET] Reference angle for rendering i.e. the angle of the drawing device's Camera.
		/// </summary>
		float RefAngle { get; }
		/// <summary>
		/// [GET] Reference distance for calculating the perspective effect. An object this far away from
		/// the Camera will appear in its original size.
		/// </summary>
		float FocusDist { get; }
		/// <summary>
		/// [GET] A bitmask flagging all visibility groups that are considered visible to this drawing device.
		/// </summary>
		VisibilityFlag VisibilityMask { get; }
		/// <summary>
		/// [GET] The lowest Z value that can be displayed by the device.
		/// </summary>
		float NearZ { get; }
		/// <summary>
		/// [GET] The highest Z value that can be displayed by the device.
		/// </summary>
		float FarZ { get; }
		/// <summary>
		/// [GET] Returns whether the drawing device allows writing to the depth buffer
		/// </summary>
		bool DepthWrite { get; }
		/// <summary>
		/// [GET] The size of the surface this drawing device operates on.
		/// </summary>
		Vector2 TargetSize { get; }
		

		
		/// <summary>
		/// Returns the scale factor of objects that are located at the specified (world space) z-Coordinate.
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		float GetScaleAtZ(float z);
		/// <summary>
		/// Transforms screen space coordinates to world space coordinates. The screen positions Z coordinate is
		/// interpreted as the target world Z coordinate.
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		Vector3 GetSpaceCoord(Vector3 screenPos);
		/// <summary>
		/// Transforms screen space coordinates to world space coordinates.
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		Vector3 GetSpaceCoord(Vector2 screenPos);
		/// <summary>
		/// Transforms world space coordinates to screen space coordinates.
		/// </summary>
		/// <param name="spacePos"></param>
		/// <returns></returns>
		Vector3 GetScreenCoord(Vector3 spacePos);
		/// <summary>
		/// Transforms world space coordinates to screen space coordinates.
		/// </summary>
		/// <param name="spacePos"></param>
		/// <returns></returns>
		Vector3 GetScreenCoord(Vector2 spacePos);

		/// <summary>
		/// Processes the specified world space position and scale values and transforms them to the IDrawDevices view space.
		/// This usually also applies a perspective effect, if applicable.
		/// </summary>
		/// <param name="pos">The position to process.</param>
		/// <param name="scale">The scale factor to process.</param>
		void PreprocessCoords(ref Vector3 pos, ref float scale);
		/// <summary>
		/// Returns whether the specified world-space position is visible in the drawing devices view space.
		/// </summary>
		/// <param name="c">The position to test.</param>
		/// <param name="boundRad">The visual bounding radius to assume for the specified position.</param>
		/// <returns>True, if the position or a portion of its bounding circle is visible, false if not.</returns>
		bool IsCoordInView(Vector3 c, float boundRad = 1.0f);

		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.Material"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData;
		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.BatchInfo"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(BatchInfo material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData;
		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.Material"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertexBuffer">A vertex data buffer that stores the vertices to add.</param>
		/// <param name="vertexCount">The number of vertices to add, from the beginning of the buffer.</param>
		void AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, T[] vertexBuffer, int vertexCount) where T : struct, IVertexData;
		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.BatchInfo"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertexBuffer">A vertex data buffer that stores the vertices to add.</param>
		/// <param name="vertexCount">The number of vertices to add, from the beginning of the buffer.</param>
		void AddVertices<T>(BatchInfo material, VertexMode vertexMode, T[] vertexBuffer, int vertexCount) where T : struct, IVertexData;
	}

	public class DrawDevice : IDrawDevice, IDisposable
	{
		private interface IDrawBatch
		{
			int SortIndex { get; }
			float ZSortIndex { get; }
			int VertexCount { get; }
			VertexMode VertexMode { get; }
			BatchInfo Material { get; }
			int VertexTypeIndex { get; }

			void UploadToVBO(List<IDrawBatch> batches);
			void SetupVBO();
			void FinishVBO();
			void Render(IDrawDevice device, ref int vertexOffset, ref IDrawBatch lastBatchRendered);
			void FinishRendering();

			bool CanShareVBO(IDrawBatch other);
			bool CanAppendJIT<T>(float invZSortAccuracy, float zSortIndex, BatchInfo material, VertexMode vertexMode) where T : struct, IVertexData;
			void AppendJIT(object vertexData, int length);
			bool CanAppend(IDrawBatch other);
			void Append(IDrawBatch other);
		}
		private class DrawBatch<T> : IDrawBatch where T : struct, IVertexData
		{
			private static T[] uploadBuffer = null;

			private	T[]			vertices	= null;
			private	int			vertexCount	= 0;
			private	int			sortIndex	= 0;
			private	float		zSortIndex	= 0.0f;
			private	VertexMode	vertexMode	= VertexMode.Points;
			private	BatchInfo	material	= null;

			public int SortIndex
			{
				get { return this.sortIndex; }
			}
			public float ZSortIndex
			{
				get { return this.zSortIndex; }
			}
			public int VertexCount
			{
				get { return this.vertexCount; }
			}
			public VertexMode VertexMode
			{
				get { return this.vertexMode; }
			}
			public int VertexTypeIndex
			{
				get { return this.vertices[0].TypeIndex; }
			}
			public BatchInfo Material
			{
				get { return this.material; }
			}

			public DrawBatch(BatchInfo material, VertexMode vertexMode, T[] vertices, int vertexCount, float zSortIndex)
			{
				if (vertices == null || vertices.Length == 0) throw new ArgumentException("A zero-vertex DrawBatch is invalid.");
				
				this.material = material;
				this.vertexMode = vertexMode;
				this.vertices = vertices;
				this.vertexCount = Math.Min(vertexCount, vertices.Length);
				this.zSortIndex = zSortIndex;

				if (!this.material.Technique.Res.NeedsZSort)
				{
					int vTypeSI = vertices[0].TypeIndex;
					int matHash = this.material.GetHashCode() % (1 << 23);

					// Bit significancy is used to achieve sorting by multiple traits at once.
					// The higher a traits bit significancy, the higher its priority when sorting.
					this.sortIndex = 
						(((int)vertexMode & 15) << 0) |		//							  XXXX	4 Bit	Vertex Mode		Offset 4
						((matHash & 8388607) << 4) |		//	   XXXXXXXXXXXXXXXXXXXXXXXaaaa	23 Bit	Material		Offset 27
						((vTypeSI & 15) << 27);				//	XXXbbbbbbbbbbbbbbbbbbbbbbbaaaa	4 Bit	Vertex Type		Offset 31

					// Keep an eye on this. If for example two material hash codes randomly have the same 23 lower bits, they
					// will be sorted as if equal, resulting in blocking batch aggregation.
				}
			}

			public void SetupVBO()
			{
				// Set up VBO
				this.vertices[0].SetupVBO(this.material);
			}
			public void UploadToVBO(List<IDrawBatch> batches)
			{
				int vertexCount = 0;
				T[] vertexData = null;

				if (batches.Count == 1)
				{
					// Only one batch? Don't bother copying data
					DrawBatch<T> b = batches[0] as DrawBatch<T>;
					vertexData = b.vertices;
					vertexCount = b.vertices.Length;
				}
				else
				{
					// Check how many vertices we got
					vertexCount = batches.Sum(t => t.VertexCount);
					
					// Allocate a static / shared buffer for uploading vertices
					if (uploadBuffer == null)
						uploadBuffer = new T[Math.Max(vertexCount, 64)];
					else if (uploadBuffer.Length < vertexCount)
						Array.Resize(ref uploadBuffer, Math.Max(vertexCount, uploadBuffer.Length * 2));

					// Collect vertex data in one array
					int curVertexPos = 0;
					vertexData = uploadBuffer;
					for (int i = 0; i < batches.Count; i++)
					{
						DrawBatch<T> b = batches[i] as DrawBatch<T>;
						Array.Copy(b.vertices, 0, vertexData, curVertexPos, b.vertexCount);
						curVertexPos += b.vertexCount;
					}
				}

				// Submit vertex data to GPU
				this.vertices[0].UploadToVBO(vertexData, vertexCount);
			}
			public void FinishVBO()
			{
				// Finish VBO
				this.vertices[0].FinishVBO(this.material);
			}
			public void Render(IDrawDevice device, ref int vertexOffset, ref IDrawBatch lastBatchRendered)
			{
				if (lastBatchRendered == null || lastBatchRendered.Material != this.material)
				    this.material.SetupForRendering(device, lastBatchRendered == null ? null : lastBatchRendered.Material);

				GL.DrawArrays((BeginMode)this.vertexMode, vertexOffset, this.vertexCount);

				vertexOffset += this.vertexCount;
				lastBatchRendered = this;
			}
			public void FinishRendering()
			{
				this.material.FinishRendering();
			}

			public bool CanShareVBO(IDrawBatch other)
			{
				return other is DrawBatch<T>;
			}
			public bool CanAppendJIT<U>(float invZSortAccuracy, float zSortIndex, BatchInfo material, VertexMode vertexMode) where U : struct, IVertexData
			{
				if (invZSortAccuracy > 0.0f && this.material.Technique.Res.NeedsZSort)
				{
					if (Math.Abs(zSortIndex - this.ZSortIndex) > invZSortAccuracy) return false;
				}
				return 
					vertexMode == this.vertexMode && 
					this is DrawBatch<U> &&
					IsVertexModeAppendable(this.VertexMode) &&
					material == this.material;
			}
			public void AppendJIT(object vertexData, int length)
			{
				this.AppendJIT((T[])vertexData, length);
			}
			public void AppendJIT(T[] data, int length)
			{
				if (this.vertexCount + length > this.vertices.Length)
				{
					int newArrSize = MathF.Max(16, this.vertexCount * 2, this.vertexCount + length);
					Array.Resize<T>(ref this.vertices, newArrSize);
				}
				Array.Copy(data, 0, this.vertices, this.vertexCount, length);
				this.vertexCount += length;
				
				if (this.material.Technique.Res.NeedsZSort)
					this.zSortIndex = CalcZSortIndex(this.vertices, this.vertexCount);
			}
			public bool CanAppend(IDrawBatch other)
			{
				return
					other.VertexMode == this.vertexMode && 
					other is DrawBatch<T> &&
					IsVertexModeAppendable(this.VertexMode) &&
					other.Material == this.material;
			}
			public void Append(IDrawBatch other)
			{
				this.Append((DrawBatch<T>)other);
			}
			public void Append(DrawBatch<T> other)
			{
				if (this.vertexCount + other.vertexCount > this.vertices.Length)
				{
					int newArrSize = MathF.Max(16, this.vertexCount * 2, this.vertexCount + other.vertexCount);
					Array.Resize<T>(ref this.vertices, newArrSize);
				}
				Array.Copy(other.vertices, 0, this.vertices, this.vertexCount, other.vertexCount);
				this.vertexCount += other.vertexCount;
				
				if (this.material.Technique.Res.NeedsZSort)
					this.zSortIndex = CalcZSortIndex(this.vertices, this.vertexCount);
			}

			public static bool IsVertexModeAppendable(VertexMode mode)
			{
				return 
					mode == VertexMode.Lines || 
					mode == VertexMode.Points || 
					mode == VertexMode.Quads || 
					mode == VertexMode.Triangles;
			}
			public static float CalcZSortIndex(T[] vertices, int count = -1)
			{
				if (count < 0) count = vertices.Length;
				float zSortIndex = 0.0f;
				for (int i = 0; i < count; i++)
				{
					zSortIndex += vertices[i].Pos.Z;
				}
				return zSortIndex / count;
			}
		}
		
		
		/// <summary>
		/// The default reference distance for perspective rendering.
		/// </summary>
		public const float DefaultFocusDist	= 500.0f;

		
		private	bool				disposed		= false;
		private	float				nearZ			= 0.0f;
		private	float				farZ			= 10000.0f;
		private	float				zSortAccuracy	= 0.0f;
		private	float				focusDist		= DefaultFocusDist;
		private	Vector3				refPos			= Vector3.Zero;
		private	float				refAngle		= 0.0f;
		private	RenderMatrix		renderMode		= RenderMatrix.OrthoScreen;
		private	PerspectiveMode		perspective		= PerspectiveMode.Parallax;
		private	Matrix4				matModelView	= Matrix4.Identity;
		private	Matrix4				matProjection	= Matrix4.Identity;
		private	Matrix4				matFinal		= Matrix4.Identity;
		private	VisibilityFlag		visibilityMask	= VisibilityFlag.All;
		private	int					pickingIndex	= 0;
		private	List<IDrawBatch>	drawBuffer		= new List<IDrawBatch>();
		private	List<IDrawBatch>	drawBufferZSort	= new List<IDrawBatch>();
		private	int					numRawBatches	= 0;
		private	ContentRef<RenderTarget> renderTarget = null;
		private	uint				hndlPrimaryVBO		= 0;


		public bool Disposed
		{
			get { return this.disposed; }
		}
		public Vector3 RefCoord
		{
			get { return this.refPos; }
			set { this.refPos = value; }
		}
		public float RefAngle
		{
			get { return this.refAngle; }
			set { this.refAngle = value; }
		}
		public float FocusDist
		{
			get { return this.focusDist; }
			set { this.focusDist = value; }
		}
		public VisibilityFlag VisibilityMask
		{
			get { return this.visibilityMask; }
			set { this.visibilityMask = value; }
		}
		public float NearZ
		{
			get { return this.nearZ; }
			set
			{
				if (this.nearZ != value)
				{
					this.nearZ = value;
					this.UpdateZSortAccuracy();
				}
			}
		}
		public float FarZ
		{
			get { return this.farZ; }
			set
			{
				if (this.farZ != value)
				{
					this.farZ = value;
					this.UpdateZSortAccuracy();
				}
			}
		}
		/// <summary>
		/// [GET / SET] Specified the perspective effect that is applied when rendering the world.
		/// </summary>
		public PerspectiveMode Perspective
		{
			get { return this.perspective; }
			set { this.perspective = value; }
		}
		public ContentRef<RenderTarget> Target
		{
			get { return this.renderTarget; }
			set { this.renderTarget = value; }
		}
		public int PickingIndex
		{
			get { return this.pickingIndex; }
			set { this.pickingIndex = value; }
		}
		public RenderMatrix RenderMode
		{
			get { return this.renderMode; }
			set { this.renderMode = value; }
		}
		public bool DepthWrite
		{
			get { return this.renderMode != RenderMatrix.OrthoScreen; }
		}
		public Vector2 TargetSize
		{
			get
			{
				if (this.renderTarget.IsAvailable)
				{
					RenderTarget target = this.renderTarget.Res;
					return new Vector2(target.Width, target.Height);
				}
				else
					return DualityApp.TargetResolution;
			}
		}

		
		public DrawDevice()
		{
			this.UpdateZSortAccuracy();
		}

		~DrawDevice()
		{
			this.Dispose(false);
		}
		/// <summary>
		/// Disposes the DrawDevice.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool manually)
		{
			if (!this.disposed)
			{
				if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
					this.hndlPrimaryVBO != 0)
				{
					DualityApp.GuardSingleThreadState();
					GL.DeleteBuffers(1, ref this.hndlPrimaryVBO);
					this.hndlPrimaryVBO = 0;
				}
				this.disposed = true;
			}
		}

		
		/// <summary>
		/// Returns the scale factor of objects that are located at the specified (world space) z-Coordinate.
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public float GetScaleAtZ(float z)
		{
			if (this.perspective == PerspectiveMode.Parallax)
				return this.focusDist / Math.Max(z - this.refPos.Z, this.nearZ);
			else
				return this.focusDist / DefaultFocusDist;
		}
		/// <summary>
		/// Transforms screen space coordinates to world space coordinates. The screen positions Z coordinate is
		/// interpreted as the target world Z coordinate.
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		public Vector3 GetSpaceCoord(Vector3 screenPos)
		{
			float targetZ = screenPos.Z;

			// Since screenPos.Z is expected to be a world coordinate, first make that relative
			Vector3 gameObjPos = this.refPos;
			screenPos.Z -= gameObjPos.Z;

			Vector2 targetSize = this.TargetSize;
			screenPos.X -= targetSize.X / 2;
			screenPos.Y -= targetSize.Y / 2;

			MathF.TransformCoord(ref screenPos.X, ref screenPos.Y, this.refAngle);
			
			// Revert active perspective effect
			float scaleTemp;
			if (this.perspective == PerspectiveMode.Flat)
			{
				// Scale globally
				scaleTemp = DefaultFocusDist / this.focusDist;
				screenPos.X *= scaleTemp;
				screenPos.Y *= scaleTemp;
			}
			else if (this.perspective == PerspectiveMode.Parallax)
			{
				// Scale distance-based
				scaleTemp = Math.Max(screenPos.Z, this.nearZ) / this.focusDist;
				screenPos.X *= scaleTemp;
				screenPos.Y *= scaleTemp;
			}
			//else if (this.perspective == PerspectiveMode.Isometric)
			//{
			//    // Scale globally
			//    scaleTemp = DefaultFocusDist / this.focusDist;
			//    screenPos.X *= scaleTemp;
			//    screenPos.Y *= scaleTemp;
				
			//    // Revert isometric projection
			//    screenPos.Z += screenPos.Y;
			//    screenPos.Y -= screenPos.Z;
			//    screenPos.Z += this.focusDist;
			//}
			
			// Make coordinates absolte
			screenPos.X += gameObjPos.X;
			screenPos.Y += gameObjPos.Y;
			screenPos.Z += gameObjPos.Z;

			//// For isometric projection, assure we'll meet the target Z value.
			//if (this.perspective == PerspectiveMode.Isometric)
			//{
			//    screenPos.Y += screenPos.Z - targetZ;
			//    screenPos.Z = targetZ;
			//}

			return screenPos;
		}
		/// <summary>
		/// Transforms screen space coordinates to world space coordinates.
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		public Vector3 GetSpaceCoord(Vector2 screenPos)
		{
			return this.GetSpaceCoord(new Vector3(screenPos));
		}
		/// <summary>
		/// Transforms world space coordinates to screen space coordinates.
		/// </summary>
		/// <param name="spacePos"></param>
		/// <returns></returns>
		public Vector3 GetScreenCoord(Vector3 spacePos)
		{
			// Make coordinates relative to the Camera
			Vector3 gameObjPos = this.refPos;
			spacePos.X -= gameObjPos.X;
			spacePos.Y -= gameObjPos.Y;
			spacePos.Z -= gameObjPos.Z;

			// Apply active perspective effect
			float scaleTemp;
			if (this.perspective == PerspectiveMode.Flat)
			{
				// Scale globally
				scaleTemp = this.focusDist / DefaultFocusDist;
				spacePos.X *= scaleTemp;
				spacePos.Y *= scaleTemp;
			}
			else if (this.perspective == PerspectiveMode.Parallax)
			{
				// Scale distance-based
				scaleTemp = this.focusDist / Math.Max(spacePos.Z, this.nearZ);
				spacePos.X *= scaleTemp;
				spacePos.Y *= scaleTemp;
			}
			//else if (this.perspective == PerspectiveMode.Isometric)
			//{
			//    // Apply isometric projection
			//    spacePos.Z -= this.focusDist;
			//    spacePos.Y += spacePos.Z;
			//    spacePos.Z -= spacePos.Y;

			//    // Scale globally
			//    scaleTemp = this.focusDist / DefaultFocusDist;
			//    spacePos.X *= scaleTemp;
			//    spacePos.Y *= scaleTemp;
			//}

			MathF.TransformCoord(ref spacePos.X, ref spacePos.Y, -this.refAngle);

			Vector2 targetSize = this.TargetSize;
			spacePos.X += targetSize.X / 2;
			spacePos.Y += targetSize.Y / 2;
			
			// Since the result Z value is expected to be a world coordinate, make it absolute
			spacePos.Z += gameObjPos.Z;
			return spacePos;
		}
		/// <summary>
		/// Transforms world space coordinates to screen space coordinates.
		/// </summary>
		/// <param name="spacePos"></param>
		/// <returns></returns>
		public Vector3 GetScreenCoord(Vector2 spacePos)
		{
			return this.GetScreenCoord(new Vector3(spacePos));
		}

		public void PreprocessCoords(ref Vector3 pos, ref float scale)
		{
			if (this.renderMode == RenderMatrix.OrthoScreen) return;
			
			// Make coordinates relative to the Camera
			pos.X -= this.refPos.X;
			pos.Y -= this.refPos.Y;
			pos.Z -= this.refPos.Z;

			// Apply active perspective effect
			float scaleTemp;
			if (this.perspective == PerspectiveMode.Flat)
			{
				// Scale globally
				scaleTemp = this.focusDist / DefaultFocusDist;
				pos.X *= scaleTemp;
				pos.Y *= scaleTemp;
				scale *= scaleTemp;
			}
			else if (this.perspective == PerspectiveMode.Parallax)
			{
				// Scale distance-based
				scaleTemp = this.focusDist / Math.Max(pos.Z, this.nearZ);
				pos.X *= scaleTemp;
				pos.Y *= scaleTemp;
				scale *= scaleTemp;
			}
			//else if (this.perspective == PerspectiveMode.Isometric)
			//{
			//    // Assure that objects at focus distance don't have an offset.
			//    pos.Z -= this.focusDist;
			//    // Apply Z to Y because of isometric perspective
			//    pos.Y += pos.Z;
			//    // Sort from "north" to "south" by applying Y to Z.
			//    pos.Z -= pos.Y;

			//    // Make sure nothing is culled away due to its Z value
			//    pos.Z += (this.farZ + this.nearZ) / 2;

			//    // Scale globally
			//    scaleTemp = this.focusDist / DefaultFocusDist;
			//    pos.X *= scaleTemp;
			//    pos.Y *= scaleTemp;
			//    scale *= scaleTemp;
			//}
		}
		public bool IsCoordInView(Vector3 c, float boundRad)
		{
			if (c.Z <= this.refPos.Z/* && this.perspective != PerspectiveMode.Isometric*/) return false;

			// Retrieve center vertex coord
			float scaleTemp = 1.0f;
			this.PreprocessCoords(ref c, ref scaleTemp);

			// Apply final (modelview and projection) matrix
			Vector3 oldPosTemp = c;
			Vector3.Transform(ref oldPosTemp, ref this.matFinal, out c);

			// Apply projection matrices XY rotation and scale to bounding radius
			boundRad *= scaleTemp;
			Vector2 boundRadVec = new Vector2(
				boundRad * Math.Abs(this.matFinal.Row0.X) + boundRad * Math.Abs(this.matFinal.Row1.X),
				boundRad * Math.Abs(this.matFinal.Row0.Y) + boundRad * Math.Abs(this.matFinal.Row1.Y));

			return 
				c.Z >= -1.0f &&
				c.Z <= 1.0f &&
				c.X >= -1.0f - boundRadVec.X &&
				c.Y >= -1.0f - boundRadVec.Y &&
				c.X <= 1.0f + boundRadVec.X &&
				c.Y <= 1.0f + boundRadVec.Y;
		}

		public void AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData
		{
			this.AddVertices<T>(material.IsAvailable ? material.Res.InfoDirect : Material.Checkerboard256.Res.InfoDirect, vertexMode, vertices, vertices.Length);
		}
		public void AddVertices<T>(BatchInfo material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData
		{
			this.AddVertices<T>(material, vertexMode, vertices, vertices.Length);
		}
		public void AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, T[] vertexBuffer, int vertexCount) where T : struct, IVertexData
		{
			this.AddVertices<T>(material.IsAvailable ? material.Res.InfoDirect : Material.Checkerboard256.Res.InfoDirect, vertexMode, vertexBuffer, vertexCount);
		}
		public void AddVertices<T>(BatchInfo material, VertexMode vertexMode, T[] vertexBuffer, int vertexCount) where T : struct, IVertexData
		{
			if (vertexCount == 0) return;
			if (vertexBuffer == null || vertexBuffer.Length == 0) return;
			if (vertexCount > vertexBuffer.Length) vertexCount = vertexBuffer.Length;
			if (material == null) material = Material.Checkerboard256.Res.InfoDirect;

			if (this.pickingIndex != 0)
			{
				ColorRgba clr = new ColorRgba((this.pickingIndex << 8) | 0xFF);
				for (int i = 0; i < vertexCount; ++i)
					vertexBuffer[i].Color = clr;

				material = new BatchInfo(material);
				material.Technique = DrawTechnique.Picking;
				if (material.Textures == null) material.MainTexture = Texture.White;
			}
			else if (material.Technique == null || !material.Technique.IsAvailable)
			{
				material = new BatchInfo(material);
				material.Technique = DrawTechnique.Invert;
			}
			else if (material.Technique.Res.NeedsPreprocess)
			{
				material = new BatchInfo(material);
				material.Technique.Res.PreprocessBatch<T>(this, material, ref vertexMode, ref vertexBuffer, ref vertexCount);
				if (vertexCount == 0) return;
				if (vertexBuffer == null || vertexBuffer.Length == 0) return;
				if (vertexCount > vertexBuffer.Length) vertexCount = vertexBuffer.Length;
				if (material.Technique == null || !material.Technique.IsAvailable)
					material.Technique = DrawTechnique.Invert;
			}
			
			// When rendering without depth writing, use z sorting everywhere - there's no real depth buffering!
			bool zSort = !this.DepthWrite || material.Technique.Res.NeedsZSort;
			List<IDrawBatch> buffer = zSort ? this.drawBufferZSort : this.drawBuffer;
			float zSortIndex = zSort ? DrawBatch<T>.CalcZSortIndex(vertexBuffer, vertexCount) : 0.0f;

			if (buffer.Count > 0 && buffer[buffer.Count - 1].CanAppendJIT<T>(	
					zSort ? 1.0f / this.zSortAccuracy : 0.0f, 
					zSortIndex, 
					material, 
					vertexMode))
			{
				buffer[buffer.Count - 1].AppendJIT(vertexBuffer, vertexCount);
			}
			else
			{
				buffer.Add(new DrawBatch<T>(material, vertexMode, vertexBuffer, vertexCount, zSortIndex));
			}
			++this.numRawBatches;
		}
		
		public void UpdateMatrices()
		{
			Vector2 refSize = this.TargetSize;
			this.GenerateModelView(out this.matModelView);
			this.GenerateProjection(new Rect(refSize), out this.matProjection);
			this.matFinal = this.matModelView * this.matProjection;
		}
		public void BeginRendering(ClearFlag clearFlags, ColorRgba clearColor, float clearDepth)
		{
			RenderTarget.Bind(this.renderTarget);

			// Setup viewport
			Vector2 refSize = this.TargetSize;
			Rect viewportAbs = new Rect(refSize);
			GL.Viewport((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);
			GL.Scissor((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);

			// Clear buffers
			ClearBufferMask glClearMask = 0;
			if ((clearFlags & ClearFlag.Color) != ClearFlag.None) glClearMask |= ClearBufferMask.ColorBufferBit;
			if ((clearFlags & ClearFlag.Depth) != ClearFlag.None) glClearMask |= ClearBufferMask.DepthBufferBit;
			GL.ClearColor((OpenTK.Graphics.Color4)clearColor);
			GL.ClearDepth((double)clearDepth); // The "float version" is from OpenGL 4.1..
			GL.Clear(glClearMask);

			// Configure Rendering params
			if (this.renderMode == RenderMatrix.OrthoScreen)
			{
				GL.Enable(EnableCap.ScissorTest);
				GL.Enable(EnableCap.DepthTest);
				GL.DepthFunc(DepthFunction.Always);
			}
			else
			{
				GL.Enable(EnableCap.ScissorTest);
				GL.Enable(EnableCap.DepthTest);
				GL.DepthFunc(DepthFunction.Lequal);
			}

			// Upload and adjust matrices
			this.UpdateMatrices();
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref this.matModelView);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref this.matProjection);
			if (this.renderTarget.IsAvailable)
			{
				if (this.renderMode == RenderMatrix.OrthoScreen) GL.Translate(0.0f, RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
				GL.Scale(1.0f, -1.0f, 1.0f);
				if (this.renderMode == RenderMatrix.OrthoScreen) GL.Translate(0.0f, -RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
			}
		}
		public void EndRendering()
		{
			// Process drawcalls
			this.OptimizeBatches();
			this.BeginBatchRendering();

			int drawCalls = 0;
			{
				// Z-Independent: Sorted as needed by batch optimizer
				drawCalls += this.RenderBatches(this.drawBuffer);
				// Z-Sorted: Back to Front
				drawCalls += this.RenderBatches(this.drawBufferZSort);
			}
			Performance.StatNumDrawcalls.Add(drawCalls);

			this.FinishBatchRendering();
			this.drawBuffer.Clear();
			this.drawBufferZSort.Clear();
		}


		private void UpdateZSortAccuracy()
		{
			this.zSortAccuracy = 10000000.0f / Math.Max(1.0f, Math.Abs(this.farZ - this.nearZ));
		}
		private void GenerateModelView(out Matrix4 mvMat)
		{
			mvMat = Matrix4.Identity;
			if (this.renderMode == RenderMatrix.OrthoScreen) return;

			// Translate objects contrary to the camera
			// Removed: Do this in software now for custom perspective / parallax support
			// modelViewMat *= Matrix4.CreateTranslation(-this.GameObj.Transform.Pos);

			// Rotate them according to the camera angle
			mvMat *= Matrix4.CreateRotationZ(-this.refAngle);
		}
		private void GenerateProjection(Rect orthoAbs, out Matrix4 projMat)
		{
			if (this.renderMode == RenderMatrix.OrthoScreen)
			{
				Matrix4.CreateOrthographicOffCenter(
					orthoAbs.X,
					orthoAbs.X + orthoAbs.W, 
					orthoAbs.Y + orthoAbs.H, 
					orthoAbs.Y, 
					this.nearZ, 
					this.farZ,
					out projMat);
				// Flip Z direction from "out of the screen" to "into the screen".
				projMat.M33 = -projMat.M33;
			}
			else
			{
				Matrix4.CreateOrthographicOffCenter(
					orthoAbs.X - orthoAbs.W * 0.5f, 
					orthoAbs.X + orthoAbs.W * 0.5f, 
					orthoAbs.Y + orthoAbs.H * 0.5f, 
					orthoAbs.Y - orthoAbs.H * 0.5f, 
					this.nearZ, 
					this.farZ,
					out projMat);
				// Flip Z direction from "out of the screen" to "into the screen".
				projMat.M33 = -projMat.M33;
			}
		}

		private int DrawBatchComparer(IDrawBatch first, IDrawBatch second)
		{
			return first.SortIndex - second.SortIndex;
		}
		private int DrawBatchComparerZSort(IDrawBatch first, IDrawBatch second)
		{
			return MathF.RoundToInt((second.ZSortIndex - first.ZSortIndex) * this.zSortAccuracy);
		}
		private void OptimizeBatches()
		{
			int batchCountBefore = this.drawBuffer.Count + this.drawBufferZSort.Count;
			if (this.pickingIndex == 0) Performance.TimeOptimizeDrawcalls.BeginMeasure();

			// Non-ZSorted
			if (this.drawBuffer.Count > 1)
			{
				this.drawBuffer.StableSort(this.DrawBatchComparer);
				this.drawBuffer = this.OptimizeBatches(this.drawBuffer);
			}

			// Z-Sorted
			if (this.drawBufferZSort.Count > 1)
			{
				// Stable sort assures maintaining draw order for batches of equal ZOrderIndex
				this.drawBufferZSort.StableSort(this.DrawBatchComparerZSort);
				this.drawBufferZSort = this.OptimizeBatches(this.drawBufferZSort);
			}

			if (this.pickingIndex == 0) Performance.TimeOptimizeDrawcalls.EndMeasure();
			int batchCountAfter = this.drawBuffer.Count + this.drawBufferZSort.Count;

			Performance.StatNumRawBatches.Add(this.numRawBatches);
			Performance.StatNumMergedBatches.Add(batchCountBefore);
			Performance.StatNumOptimizedBatches.Add(batchCountAfter);
			this.numRawBatches = 0;
		}
		private List<IDrawBatch> OptimizeBatches(List<IDrawBatch> sortedBuffer)
		{
			List<IDrawBatch> optimized = new List<IDrawBatch>(sortedBuffer.Count);
			IDrawBatch current = sortedBuffer[0];
			IDrawBatch next;
			optimized.Add(current);
			for (int i = 1; i < sortedBuffer.Count; i++)
			{
				next = sortedBuffer[i];

				if (current.CanAppend(next))
				{
					current.Append(next);
				}
				else
				{
					current = next;
					optimized.Add(current);
				}
			}

			return optimized;
		}

		private void BeginBatchRendering()
		{
			if (this.hndlPrimaryVBO == 0) GL.GenBuffers(1, out this.hndlPrimaryVBO);
			GL.BindBuffer(BufferTarget.ArrayBuffer, this.hndlPrimaryVBO);
		}
		private int RenderBatches(List<IDrawBatch> buffer)
		{
			if (this.pickingIndex == 0) Performance.TimeProcessDrawcalls.BeginMeasure();

			int drawCalls = 0;
			List<IDrawBatch> batchesSharingVBO = new List<IDrawBatch>();
			IDrawBatch lastBatchRendered = null;

			IDrawBatch lastBatch = null;
			for (int i = 0; i < buffer.Count; i++)
			{
				IDrawBatch currentBatch = buffer[i];
				IDrawBatch nextBatch = (i < buffer.Count - 1) ? buffer[i + 1] : null;

				if (lastBatch == null || lastBatch.CanShareVBO(currentBatch))
				{
					batchesSharingVBO.Add(currentBatch);
				}

				if (batchesSharingVBO.Count > 0 && (nextBatch == null || !currentBatch.CanShareVBO(nextBatch)))
				{
					int vertexOffset = 0;
					batchesSharingVBO[0].UploadToVBO(batchesSharingVBO);
					drawCalls++;

					foreach (IDrawBatch renderBatch in batchesSharingVBO)
					{
						renderBatch.SetupVBO();
						renderBatch.Render(this, ref vertexOffset, ref lastBatchRendered);
						renderBatch.FinishVBO();
						drawCalls++;
					}

					batchesSharingVBO.Clear();
					lastBatch = null;
				}
				else
					lastBatch = currentBatch;
			}

			if (lastBatchRendered != null)
				lastBatchRendered.FinishRendering();

			if (this.pickingIndex == 0) Performance.TimeProcessDrawcalls.EndMeasure();
			return drawCalls;
		}
		private void FinishBatchRendering()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		public static void RenderVoid()
		{
			RenderTarget.Bind(ContentRef<RenderTarget>.Null);
			
			Vector2 refSize = DualityApp.TargetResolution;
			Rect viewportAbs = new Rect(refSize);
			GL.Viewport((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);
			GL.Scissor((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);

			GL.ClearDepth(1.0d);
			GL.ClearColor((OpenTK.Graphics.Color4)ColorRgba.TransparentBlack);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}
	}
}
