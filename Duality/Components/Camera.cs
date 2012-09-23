using System;
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
	/// We're not using OpenTK.Graphics.OpenGL.BeginMode in order to keep the abstraction layer that
	/// is introduced by <see cref="IDrawDevice"/> consistent. Also, we're able to limit the supported
	/// vertex modes and remove obsolete or inefficient ones.
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
	/// Specifies a matrix setup used in a <see cref="Duality.Components.Camera.Pass"/>.
	/// </summary>
	public enum RenderMatrix
	{
		PerspectiveWorld,
		OrthoScreen
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
		AllWorld = All & (~ScreenOverlay),
		AllOverlay = All
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
		/// [GET] The devices view space bounding circle radius.
		/// </summary>
		float ViewBoundingRadius { get; }
		/// <summary>
		/// [GET] Returns whether the drawing device is currently rendering in screen overlay mode
		/// </summary>
		bool IsScreenOverlay { get; }
		/// <summary>
		/// [GET] The size of the surface this drawing device operates on.
		/// </summary>
		Vector2 TargetSize { get; }
		

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
	}
}

namespace Duality.Components
{
	/// <summary>
	/// A Camera is responsible for rendering the current <see cref="Duality.Resources.Scene"/>.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public sealed class Camera : Component, IDrawDevice
	{
		/// <summary>
		/// A Bitmask describing which components of the current (or back-)buffer to clear before rendering.
		/// </summary>
		[Flags]
		public enum ClearFlags
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
		/// <summary>
		/// Describes a single pass in the overall rendering process.
		/// </summary>
		[Serializable]
		public class Pass
		{
			private ColorRgba					clearColor		= ColorRgba.TransparentBlack;
			private float						clearDepth		= 1.0f;
			private ClearFlags					clearFlags		= ClearFlags.All;
			private RenderMatrix				matrixMode		= RenderMatrix.PerspectiveWorld;
			private	bool						fitOutput		= false;
			private	VisibilityFlag				visibilityMask	= VisibilityFlag.AllWorld;
			private	BatchInfo					input			= null;
			private	ContentRef<RenderTarget>	output			= ContentRef<RenderTarget>.Null;

			/// <summary>
			/// Fired when collecting drawcalls for this pass. Note that not all passes do collect drawcalls (see <see cref="Input"/>)
			/// </summary>
			public event EventHandler<CollectDrawcallEventArgs> CollectDrawcalls	= null;
			
			/// <summary>
			/// The input to use for rendering. This can for example be a <see cref="Duality.Resources.Texture"/> that
			/// has been rendered to before and is now bound to perform a postprocessing step. If this is null, the current
			/// <see cref="Duality.Resources.Scene"/> is used as input - which is usually the case in the first rendering pass.
			/// </summary>
			public BatchInfo Input
			{
				get { return this.input; }
				set { this.input = value; }
			}
			/// <summary>
			/// The output to render to in this pass. If this is null, the screen is used as rendering target.
			/// </summary>
			public ContentRef<RenderTarget> Output
			{
				get { return this.output; }
				set { this.output = value; }
			}
			/// <summary>
			/// Specifies whether this passes output shall be scaled in order to fit the specified outputs dimensions.
			/// </summary>
			public bool FitOutput
			{
				get { return this.fitOutput; }
				set { this.fitOutput = value; }
			}
			/// <summary>
			/// [GET / SET] The clear color to apply when clearing the color buffer
			/// </summary>
			public ColorRgba ClearColor
			{
				get { return this.clearColor; }
				set { this.clearColor = value; }
			}
			/// <summary>
			/// [GET / SET] The clear depth to apply when clearing the depth buffer
			/// </summary>
			public float ClearDepth
			{
				get { return this.clearDepth; }
				set { this.clearDepth = value; }
			}
			/// <summary>
			/// [GET / SET] Specifies which buffers to clean before rendering this pass
			/// </summary>
			public ClearFlags ClearFlags
			{
				get { return this.clearFlags; }
				set { this.clearFlags = value; }
			}
			/// <summary>
			/// [GET / SET] How to set up the coordinate space before rendering
			/// </summary>
			public RenderMatrix MatrixMode
			{
				get { return this.matrixMode; }
				set { this.matrixMode = value; }
			}
			/// <summary>
			/// [GET / SET] A Pass-local bitmask flagging all visibility groups that are considered visible to this drawing device.
			/// </summary>
			public VisibilityFlag VisibilityMask
			{
				get { return this.visibilityMask; }
				set { this.visibilityMask = value; }
			}

			public Pass() {}
			public Pass(Pass copyFrom)
			{
				this.input = copyFrom.input;
				this.output = copyFrom.output;
				this.fitOutput = copyFrom.fitOutput;
				this.clearColor = copyFrom.clearColor;
				this.clearDepth = copyFrom.clearDepth;
				this.clearFlags = copyFrom.clearFlags;
				this.matrixMode = copyFrom.matrixMode;
				this.visibilityMask = copyFrom.visibilityMask;

				this.MakeAvailable();
			}
			public Pass(Pass copyFrom, BatchInfo inputOverride)
			{
				this.input = inputOverride;
				this.output = copyFrom.output;
				this.fitOutput = copyFrom.fitOutput;
				this.clearColor = copyFrom.clearColor;
				this.clearDepth = copyFrom.clearDepth;
				this.clearFlags = copyFrom.clearFlags;
				this.matrixMode = copyFrom.matrixMode;
				this.visibilityMask = copyFrom.visibilityMask;

				this.MakeAvailable();
			}
			public void MakeAvailable()
			{
				this.output.MakeAvailable();
			}

			internal void NotifyCollectDrawcalls(IDrawDevice device)
			{
				Performance.timeCollectDrawcalls.BeginMeasure();

				if (this.CollectDrawcalls != null)
					this.CollectDrawcalls(this, new CollectDrawcallEventArgs(device));

				Performance.timeCollectDrawcalls.EndMeasure();
			}

			public override string ToString()
			{
				ContentRef<Texture> inputTex = input == null ? ContentRef<Texture>.Null : input.MainTexture;
				return string.Format("{0} => {1}{2}",
					inputTex.IsExplicitNull ? (input == null ? "Camera" : "Undefined") : inputTex.Name,
					output.IsExplicitNull ? "Screen" : output.Name,
					(this.visibilityMask & VisibilityFlag.ScreenOverlay) != VisibilityFlag.None ? " (Overlay)" : "");
			}
		}

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
			void AppendJIT(object vertexData);
			bool CanAppend(IDrawBatch other);
			void Append(IDrawBatch other);
		}
		private class DrawBatch<T> : IDrawBatch where T : struct, IVertexData
		{
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

			public DrawBatch(BatchInfo material, VertexMode vertexMode, T[] vertices, float zSortIndex)
			{
				if (vertices == null || vertices.Length == 0) throw new ArgumentException("A zero-vertex DrawBatch is invalid.");
				
				this.material = material;
				this.vertexMode = vertexMode;
				this.vertices = vertices;
				this.vertexCount = vertices.Length;
				this.zSortIndex = zSortIndex;

				if (!this.material.Technique.Res.NeedsZSort)
				{
					int vTypeSI = vertices[0].TypeIndex;
					int matHash = 
						(this.material.GetTechniqueHashCode() & 2047) << 0 |	// 11 Bit Technique
						(this.material.GetTextureHashCode() & 4095) << 11;		// 12 Bit Used Textures

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
				// Check how many vertices we got
				int totalVertexNum = batches.Sum(t => t.VertexCount);

				// Collect vertex data in one array
				int curVertexPos = 0;
				T[] vertexData = new T[totalVertexNum];
				int[] batchBeginIndices = new int[batches.Count];
				for (int i = 0; i < batches.Count; i++)
				{
					DrawBatch<T> b = batches[i] as DrawBatch<T>;
					Array.Copy(b.vertices, 0, vertexData, curVertexPos, b.vertexCount);
					batchBeginIndices[i] = curVertexPos;
					curVertexPos += b.vertexCount;
				}

				// Submit vertex data to GPU
				this.vertices[0].UploadToVBO(vertexData);
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
			public void AppendJIT(object vertexData)
			{
				this.AppendJIT((T[])vertexData);
			}
			public void AppendJIT(T[] vertexData)
			{
				if (this.vertexCount + vertexData.Length > this.vertices.Length)
				{
					int newArrSize = MathF.Max(16, this.vertexCount * 2, this.vertexCount + vertexData.Length);
					Array.Resize<T>(ref this.vertices, newArrSize);
				}
				Array.Copy(vertexData, 0, this.vertices, this.vertexCount, vertexData.Length);
				this.vertexCount += vertexData.Length;
				
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

		private	float	nearZ				= 0.0f;
		private	float	farZ				= 10000.0f;
		private	float	zSortAccuracy		= 0.0f;
		private	float	focusDist			= DefaultFocusDist;
		private	VisibilityFlag	visibilityMask	= VisibilityFlag.All;
		private	List<Pass>	passes			= new List<Pass>();

		[NonSerialized]	private	Matrix4	matModelView		= Matrix4.Identity;
		[NonSerialized]	private	Matrix4	matProjection		= Matrix4.Identity;
		[NonSerialized]	private	Matrix4	matFinal			= Matrix4.Identity;
		[NonSerialized]	private	bool	overlayMatrices		= false;

		[NonSerialized]	private	uint				hndlPrimaryVBO		= 0;
		[NonSerialized]	private	Pass				devicePass			= null;
		[NonSerialized]	private	VisibilityFlag		deviceVisibility	= VisibilityFlag.All;
		[NonSerialized]	private	bool				deviceCacheValid	= false;
		[NonSerialized]	private	Vector3				deviceCachePos		= Vector3.Zero;
		[NonSerialized]	private	bool				deviceScreenOverlay	= false;
		[NonSerialized]	private	int					picking				= 0;
		[NonSerialized]	private	List<ICmpRenderer>	pickingMap			= null;
		[NonSerialized]	private	RenderTarget		pickingRT			= null;
		[NonSerialized]	private	Texture				pickingTex			= null;
		[NonSerialized]	private	int					pickingLast			= -1;
		[NonSerialized]	private	byte[]				pickingBuffer		= new byte[4 * 256 * 256];
		[NonSerialized]	private	int					numRawBatches		= 0;
		[NonSerialized]	private	List<IDrawBatch>	drawBuffer			= new List<IDrawBatch>();
		[NonSerialized]	private	List<IDrawBatch>	drawBufferZSort		= new List<IDrawBatch>();
		[NonSerialized]	private	List<Predicate<ICmpRenderer>>	editorRenderFilter	= new List<Predicate<ICmpRenderer>>();

		
		/// <summary>
		/// [GET / SET] The lowest Z value that can be displayed by the device.
		/// </summary>
		[EditorHintDecimalPlaces(1)]
		[EditorHintIncrement(10.0f)]
		public float NearZ
		{
			get { return this.nearZ; }
			set { this.nearZ = value; this.UpdateZSortAccuracy(); }
		}
		/// <summary>
		/// [GET / SET] The highest Z value that can be displayed by the device.
		/// </summary>
		[EditorHintDecimalPlaces(1)]
		[EditorHintIncrement(10.0f)]
		public float FarZ
		{
			get { return this.farZ; }
			set { this.farZ = value; this.UpdateZSortAccuracy(); }
		}
		/// <summary>
		/// [GET / SET] Reference distance for calculating the view perspective. An object this far away from
		/// the Camera will appear in its original size and without offset.
		/// </summary>
		[EditorHintDecimalPlaces(1)]
		[EditorHintIncrement(10.0f)]
		public float FocusDist
		{
			get { return this.focusDist; }
			set { this.focusDist = value; }
		}
		/// <summary>
		/// [GET / SET] A bitmask flagging all visibility groups that are considered visible to this drawing device.
		/// </summary>
		public VisibilityFlag VisibilityMask
		{
			get { return this.visibilityMask; }
			set { this.visibilityMask = value; }
		}
		/// <summary>
		/// [GET / SET] The background color of the rendered image.
		/// </summary>
		public ColorRgba ClearColor
		{
			get
			{
				Pass clearPass = this.passes.FirstOrDefault(p => (p.ClearFlags & ClearFlags.Color) != ClearFlags.None);
				if (clearPass == null) return ColorRgba.TransparentBlack;
				return clearPass.ClearColor;
			}
			set
			{
				Pass clearPass = this.passes.FirstOrDefault(p => (p.ClearFlags & ClearFlags.Color) != ClearFlags.None);
				if (clearPass != null) clearPass.ClearColor = value;
			}
		}
		/// <summary>
		/// [GET / SET] A set of passes that describes the Cameras rendering process. Is never null nor empty.
		/// </summary>
		[EditorHintFlags(MemberFlags.ForceWriteback)]
		public List<Pass> Passes
		{
			get { return this.passes; }
			set 
			{ 
				if (value != null)
					this.passes = value.Select(v => v ?? new Pass()).ToList();
				else
					this.passes = new List<Pass>();
			}
		}

		/// <summary>
		/// [GET] The drawing device which this Camera uses for rendering.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IDrawDevice DrawDevice
		{
			get { return this; }
		}
		/// <summary>
		/// [GET] The drawing devices target size for rendering the Scene.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Vector2 SceneTargetSize
		{
			get
			{
				foreach (Pass t in this.passes)
				{
					if (t.Input == null)
					{
						return !t.Output.IsAvailable ?
							DualityApp.TargetResolution :
							new Vector2(t.Output.Res.Width, t.Output.Res.Height);
					}
				}
				return DualityApp.TargetResolution;
			}
		}
		/// <summary>
		/// [GET] A Rect describing the Cameras absolute ortho value.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Rect SceneOrthoAbs
		{
			get { return new Rect(this.SceneTargetSize); }
		}
		/// <summary>
		/// [GET] A Rect describing the Cameras absolute viewport.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Rect SceneViewportAbs
		{
			get { return new Rect(this.SceneTargetSize); }
		}
		/// <summary>
		/// [GET] The Cameras view space bounding circle radius.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public float ViewBoundingRadius
		{
			get 
			{ 
				Rect orthoAbs = this.SceneOrthoAbs;
				return orthoAbs.Size.Length * 0.5f;
			}
		}

		public Camera()
		{
			this.UpdateZSortAccuracy();

			// Set up default rendering
			Pass worldPass = new Pass();
			Pass overlayPass = new Pass();
			overlayPass.MatrixMode = RenderMatrix.OrthoScreen;
			overlayPass.ClearFlags = ClearFlags.None;
			overlayPass.VisibilityMask = VisibilityFlag.AllOverlay;

			this.passes.Add(worldPass);
			this.passes.Add(overlayPass);
		}
		protected override void OnCopyTo(Component target, Duality.Cloning.CloneProvider provider)
		{
			base.OnCopyTo(target, provider);
			Camera t = target as Camera;
			t.nearZ				= this.nearZ;
			t.farZ				= this.farZ;
			t.focusDist	= this.focusDist;
			t.visibilityMask	= this.visibilityMask;
			t.passes			= this.passes != null ? new List<Pass>(this.passes.Select(p => new Pass(p))) : null;
		}
		public void MakeAvailable()
		{
			foreach (var pass in this.passes)
				pass.MakeAvailable();
		}

		/// <summary>
		/// Renders the current <see cref="Duality.Resources.Scene"/>.
		/// </summary>
		public void Render()
		{
			this.MakeAvailable();
			this.deviceCacheValid = true;
			this.deviceCachePos = this.GameObj.Transform.Pos;

			if (this.picking != 0)
			{
				this.SetupPickingRT();
				RenderTarget.Bind(this.pickingRT);
				
				Vector2 refSize = new Vector2(this.pickingRT.Width, this.pickingRT.Height);
				Rect viewportAbs = new Rect(refSize);
				GL.Viewport((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);
				GL.Scissor((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);

				GL.ClearDepth(1.0d);
				GL.ClearColor(System.Drawing.Color.Black);
				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

				this.deviceVisibility = this.visibilityMask & VisibilityFlag.AllWorld;
				this.devicePass = null;
				this.deviceScreenOverlay = false;
				// Setup matrices
				this.SetupMatrices();
				// Render Scene
				this.CollectDrawcalls();
				this.ProcessDrawcalls();
				RenderTarget.Bind(RenderTarget.None);
			}
			else
			{
				Performance.BeginMeasure("Camera_" + this.gameobj.Name + "_Render");
				Performance.timeRender.BeginMeasure();

				foreach (Pass t in this.passes)
				{
					this.deviceVisibility = this.visibilityMask & t.VisibilityMask;
					this.devicePass = t;
					this.deviceScreenOverlay = this.devicePass.MatrixMode == RenderMatrix.OrthoScreen || this.devicePass.Input != null;
					this.RenderSinglePass(t);
				}
				this.deviceVisibility = this.visibilityMask;
				this.devicePass = null;
				this.deviceScreenOverlay = false;
				this.SetupMatrices(); // Reset matrices for projection calculations during update
				RenderTarget.Bind(RenderTarget.None);

				Performance.timeRender.EndMeasure();
				Performance.EndMeasure("Camera_" + this.gameobj.Name + "_Render");
			}

			this.deviceCacheValid = false;
		}
		/// <summary>
		/// Renders a picking map of the current <see cref="Duality.Resources.Scene"/>.
		/// If picking is required, this will be (automatically) done each frame a picking operation needs to
		/// be performed. 
		/// </summary>
		/// <returns>True, if the picking map has been rendered. False, if this frames cached version is used.</returns>
		public bool RenderPickingMap()
		{
			if (this.pickingLast == Time.FrameCount) return false;
			this.pickingLast = Time.FrameCount;
			Performance.timeVisualPicking.BeginMeasure();

			// Render picking map
			this.picking = 1;
			this.Render();
			GL.Finish();
			this.picking = 0;

			// Move data to local buffer
			int pxNum = this.pickingTex.PxWidth * this.pickingTex.PxHeight;
			int pxByteNum = pxNum * 4;
			if (pxByteNum > this.pickingBuffer.Length) Array.Resize(ref this.pickingBuffer, Math.Max(this.pickingBuffer.Length * 2, pxByteNum));

			ContentRef<RenderTarget> lastTex = RenderTarget.BoundRT;
			RenderTarget.Bind(this.pickingRT);
			GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
			GL.ReadPixels(0, 0, this.pickingTex.PxWidth, this.pickingTex.PxHeight, PixelFormat.Rgba, PixelType.UnsignedByte, this.pickingBuffer);
			RenderTarget.Bind(lastTex);
			GL.ReadBuffer(ReadBufferMode.Back);

			Performance.timeVisualPicking.EndMeasure();
			return true;
		}
		/// <summary>
		/// Picks the <see cref="Duality.ICmpRenderer"/> that owns the pixel at the specified position.
		/// </summary>
		/// <param name="x">x-Coordinate of the pixel to check.</param>
		/// <param name="y">y-Coordinate of the pixel to check.</param>
		/// <returns>The <see cref="Duality.ICmpRenderer"/> that owns the pixel.</returns>
		public ICmpRenderer PickRendererAt(int x, int y)
		{
			if (x < 0 || x >= DualityApp.TargetResolution.X) return null;
			if (y < 0 || y >= DualityApp.TargetResolution.Y) return null;
			
			this.RenderPickingMap();

			x = MathF.Clamp(x, 0, this.pickingTex.PxWidth - 1);
			y = MathF.Clamp(y, 0, this.pickingTex.PxHeight - 1);

			int rendererId = 
				(this.pickingBuffer[4 * (x + y * this.pickingTex.PxWidth) + 0] << 16) |
				(this.pickingBuffer[4 * (x + y * this.pickingTex.PxWidth) + 1] << 8) |
				(this.pickingBuffer[4 * (x + y * this.pickingTex.PxWidth) + 2] << 0);
			if (rendererId > this.pickingMap.Count)
			{
				Log.Core.WriteWarning("Unexpected picking result: {0}", ColorRgba.FromIntArgb(rendererId));
				return null;
			}
			else if (rendererId != 0)
			{
				if ((this.pickingMap[rendererId - 1] as Component).Disposed)
					return null;
				else
					return this.pickingMap[rendererId - 1];
			}
			else
				return null;
		}
		/// <summary>
		/// Picks all <see cref="Duality.ICmpRenderer">ICmpRenderers</see> contained within the specified
		/// rectangular area.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect.</param>
		/// <param name="y">y-Coordinate of the Rect.</param>
		/// <param name="w">Width of the Rect.</param>
		/// <param name="h">Height of the Rect.</param>
		/// <returns>A set of all <see cref="Duality.ICmpRenderer">ICmpRenderers</see> that have been picked.</returns>
		public HashSet<ICmpRenderer> PickRenderersIn(int x, int y, int w, int h)
		{
			Rect dstRect = new Rect(x, y, w, h);
			Rect srcRect = new Rect(DualityApp.TargetResolution);
			if (!dstRect.Intersects(srcRect)) return new HashSet<ICmpRenderer>();
			dstRect = dstRect.Intersection(srcRect);

			this.RenderPickingMap();

			x = Math.Max((int)dstRect.X, 0);
			y = Math.Max((int)dstRect.Y, 0);
			w = Math.Min((int)dstRect.W, this.pickingTex.PxWidth - x);
			h = Math.Min((int)dstRect.H, this.pickingTex.PxHeight - y);

			HashSet<ICmpRenderer> result = new HashSet<ICmpRenderer>();
			int rendererIdLast = 0;
			unsafe { fixed (byte* pDataBegin = this.pickingBuffer) {
				for (int j = 0; j < h; ++j)
				{
					byte* pData = pDataBegin + 4 * (x + (y + j) * this.pickingTex.PxWidth);
					for (int i = 0; i < w; ++i)
					{
						int rendererId = (*pData << 16) |
						                 (*(pData + 1) << 8) |
						                 (*(pData + 2) << 0);
						if (rendererId != rendererIdLast)
						{
							if (rendererId - 1 > this.pickingMap.Count)
								Log.Core.WriteWarning("Unexpected picking result: {0}", ColorRgba.FromIntArgb(rendererId));
							else if (rendererId != 0 && !(this.pickingMap[rendererId - 1] as Component).Disposed)
								result.Add(this.pickingMap[rendererId - 1]);
							rendererIdLast = rendererId;
						}
						pData += 4;
					}
				}
			}}

			return result;
		}

		/// <summary>
		/// Returns the scale factor of objects that are located at the specified (world space) z-Coordinate.
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public float GetScaleAtZ(float z)
		{
			Vector3 dummy = new Vector3(0, 0, z);
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref dummy, ref scale);
			return scale;
		}
		/// <summary>
		/// Transforms screen space coordinates to world space coordinates.
		/// </summary>
		/// <param name="screenPos"></param>
		/// <returns></returns>
		public Vector3 GetSpaceCoord(Vector3 screenPos)
		{
			Vector3 gameObjPos = this.GameObj.Transform.Pos;
			float scale = this.GetScaleAtZ(screenPos.Z);

			Vector2 targetSize = this.SceneTargetSize;
			screenPos = new Vector3(
				(screenPos.X - targetSize.X / 2) / scale,
				(screenPos.Y - targetSize.Y / 2) / scale,
				screenPos.Z);

			MathF.TransformCoord(ref screenPos.X, ref screenPos.Y, this.GameObj.Transform.Angle);

			return new Vector3(
				screenPos.X + this.GameObj.Transform.Pos.X,
				screenPos.Y + this.GameObj.Transform.Pos.Y,
				screenPos.Z);
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
			Vector3 gameObjPos = this.GameObj.Transform.Pos;
			Vector3.Subtract(ref spacePos, ref gameObjPos, out spacePos);
			float scale = this.GetScaleAtZ(spacePos.Z);
			spacePos.X *= scale;
			spacePos.Y *= scale;

			MathF.TransformCoord(ref spacePos.X, ref spacePos.Y, -this.GameObj.Transform.Angle);

			Vector2 targetSize = this.SceneTargetSize;
			spacePos.X += targetSize.X / 2;
			spacePos.Y += targetSize.Y / 2;

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

		private void RenderSinglePass(Pass p)
		{
			RenderTarget.Bind(p.Output);
			
			Vector2 refSize = p.Output.IsAvailable ? new Vector2(p.Output.Res.Width, p.Output.Res.Height) : DualityApp.TargetResolution;
			Rect viewportAbs = new Rect(refSize);
			GL.Viewport((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);
			GL.Scissor((int)viewportAbs.X, (int)refSize.Y - (int)viewportAbs.H - (int)viewportAbs.Y, (int)viewportAbs.W, (int)viewportAbs.H);

			// Clear buffers
			ClearBufferMask glClearMask = 0;
			if ((p.ClearFlags & ClearFlags.Color) != ClearFlags.None) glClearMask |= ClearBufferMask.ColorBufferBit;
			if ((p.ClearFlags & ClearFlags.Depth) != ClearFlags.None) glClearMask |= ClearBufferMask.DepthBufferBit;
			GL.ClearColor((OpenTK.Graphics.Color4)p.ClearColor);
			GL.ClearDepth((double)p.ClearDepth); // The "float version" is from OpenGL 4.1..
			GL.Clear(glClearMask);

			if (p.Input == null)
			{
				// Setup matrices
				this.SetupMatrices();
				// Render Scene
				this.CollectDrawcalls();
				p.NotifyCollectDrawcalls(this.DrawDevice);
				this.ProcessDrawcalls();
			}
			else
			{
				this.SetupMatrices();

				Texture mainTex = p.Input.MainTexture.Res;
				Vector2 uvRatio = mainTex != null ? mainTex.UVRatio : Vector2.One;
				Vector2 inputSize = mainTex != null ? new Vector2(mainTex.PxWidth, mainTex.PxHeight) : Vector2.One;
				Rect targetRect = new Rect(p.FitOutput ? refSize : inputSize);
				if (!p.FitOutput)
				{
					targetRect.X = MathF.Round(refSize.X * 0.5f - inputSize.X * 0.5f);
					targetRect.Y = MathF.Round(refSize.Y * 0.5f - inputSize.Y * 0.5f);
				}

				IDrawDevice device = this.DrawDevice;
				device.AddVertices(p.Input, VertexMode.Quads,
					new VertexP3T2(targetRect.MinX,	targetRect.MinY,	0.0f,	0.0f,		0.0f),
					new VertexP3T2(targetRect.MaxX,	targetRect.MinY,	0.0f,	uvRatio.X,	0.0f),
					new VertexP3T2(targetRect.MaxX,	targetRect.MaxY,	0.0f,	uvRatio.X,	uvRatio.Y),
					new VertexP3T2(targetRect.MinX,	targetRect.MaxY,	0.0f,	0.0f,		uvRatio.Y));

				Performance.timePostProcessing.BeginMeasure();
				this.ProcessDrawcalls();
				Performance.timePostProcessing.EndMeasure();
			}
		}
		private void CollectDrawcalls()
		{
			// Query renderers
			IEnumerable<ICmpRenderer> rendererQuery = Scene.Current.QueryVisibleRenderers(this.DrawDevice);
			foreach (Predicate<ICmpRenderer> p in this.editorRenderFilter) rendererQuery = rendererQuery.Where(r => p(r));

			// Collect drawcalls
			if (this.picking != 0)
			{
				this.pickingMap = new List<ICmpRenderer>(rendererQuery);
				foreach (ICmpRenderer r in this.pickingMap)
				{
					r.Draw(this);
					this.picking++;
				}
			}
			else
			{
				Performance.timeCollectDrawcalls.BeginMeasure();

				foreach (ICmpRenderer r in rendererQuery)
					r.Draw(this);

				Performance.timeCollectDrawcalls.EndMeasure();
			}
		}
		private void ProcessDrawcalls()
		{
			if (this.deviceScreenOverlay)
			{
				// Prepare Rendering
				GL.Enable(EnableCap.ScissorTest);
				GL.Enable(EnableCap.DepthTest);
				GL.DepthFunc(DepthFunction.Always);
			}
			else
			{
				// Prepare Rendering
				GL.Enable(EnableCap.ScissorTest);
				GL.Enable(EnableCap.DepthTest);
				GL.DepthFunc(DepthFunction.Lequal);
			}

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref this.matModelView);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref this.matProjection);
			if (RenderTarget.BoundRT.IsAvailable)
			{
				if (this.deviceScreenOverlay) GL.Translate(0.0f, RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
				GL.Scale(1.0f, -1.0f, 1.0f);
				if (this.deviceScreenOverlay) GL.Translate(0.0f, -RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
			}

			// Process drawcalls
			this.OptimizeBatches(this.deviceScreenOverlay);
			this.BeginBatchRendering();

			int drawCalls = 0;
			{
				// Z-Independent: Sorted as needed by batch optimizer
				drawCalls += this.RenderBatches(this.drawBuffer);
				// Z-Sorted: Back to Front
				drawCalls += this.RenderBatches(this.drawBufferZSort);
			}
			Performance.statNumDrawcalls.Add(drawCalls);

			this.FinishBatchRendering();
			this.drawBuffer.Clear();
			this.drawBufferZSort.Clear();
		}
		private void SetupPickingRT()
		{
			Vector2 refSize = DualityApp.TargetResolution;
			if (this.pickingTex == null || 
				this.pickingTex.PxWidth != MathF.RoundToInt(refSize.X) || 
				this.pickingTex.PxHeight != MathF.RoundToInt(refSize.Y))
			{
				if (this.pickingTex != null) this.pickingTex.Dispose();
				if (this.pickingRT != null) this.pickingRT.Dispose();
				this.pickingTex = new Texture(
					MathF.RoundToInt(refSize.X), MathF.RoundToInt(refSize.Y), Texture.SizeMode.Default, 
					TextureMagFilter.Nearest, TextureMinFilter.Nearest);
				this.pickingRT = new RenderTarget(false, this.pickingTex);
			}
		}

		private void UpdateZSortAccuracy()
		{
			this.zSortAccuracy = 10000000.0f / Math.Max(1.0f, Math.Abs(this.farZ - this.nearZ));
		}
		private void SetupMatrices()
		{
			ContentRef<RenderTarget> rt = RenderTarget.BoundRT.Res;
			Vector2 refSize = rt.IsAvailable ? new Vector2(rt.Res.Width, rt.Res.Height) : DualityApp.TargetResolution;

			// If we're expecting a crunched image, also crunch the picking pass
			if (this.picking != 0)
			{
				foreach (Pass t in this.passes)
				{
					if (t.Output == null && t.FitOutput)
					{
						Vector2 targetSize = t.Input == null || !t.Input.MainTexture.IsAvailable ? 
							DualityApp.TargetResolution : 
							new Vector2(t.Input.MainTexture.Res.PxWidth, t.Input.MainTexture.Res.PxHeight);
						refSize = targetSize;
						break;
					}
				}
			}

			this.GenerateModelView(out this.matModelView);
			this.GenerateProjection(rt.IsAvailable ? new Rect(refSize) : new Rect(DualityApp.TargetResolution), out this.matProjection);
			this.matFinal = this.matModelView * this.matProjection;
			this.overlayMatrices = this.deviceScreenOverlay;
		}
		private void GenerateModelView(out Matrix4 mvMat)
		{
			mvMat = Matrix4.Identity;
			if (this.deviceScreenOverlay) return;

			// Translate objects contrary to the camera
			// Removed: Do this in software now for custom perspective / parallax support
			// modelViewMat *= Matrix4.CreateTranslation(-this.GameObj.Transform.Pos);

			// Rotate them according to the camera angle
			mvMat *= Matrix4.CreateRotationZ(-this.GameObj.Transform.Angle);
		}
		private void GenerateProjection(Rect orthoAbs, out Matrix4 projMat)
		{
			if (this.deviceScreenOverlay)
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
		private void OptimizeBatches(bool screenOverlay)
		{
			int batchCountBefore = this.drawBuffer.Count + this.drawBufferZSort.Count;
			if (this.picking == 0) Performance.timeOptimizeDrawcalls.BeginMeasure();

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

			if (this.picking == 0) Performance.timeOptimizeDrawcalls.EndMeasure();
			int batchCountAfter = this.drawBuffer.Count + this.drawBufferZSort.Count;

			Performance.statNumRawBatches.Add(this.numRawBatches);
			Performance.statNumMergedBatches.Add(batchCountBefore);
			Performance.statNumOptimizedBatches.Add(batchCountAfter);
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
			if (this.picking == 0) Performance.timeProcessDrawcalls.BeginMeasure();

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
						renderBatch.Render(this.DrawDevice, ref vertexOffset, ref lastBatchRendered);
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

			if (this.picking == 0) Performance.timeProcessDrawcalls.EndMeasure();
			return drawCalls;
		}
		private void FinishBatchRendering()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		internal void AddEditorRendererFilter(Predicate<ICmpRenderer> filter)
		{
			if (this.editorRenderFilter.Contains(filter)) return;
			this.editorRenderFilter.Add(filter);
		}
		internal void RemoveEditorRendererFilter(Predicate<ICmpRenderer> filter)
		{
			this.editorRenderFilter.Remove(filter);
		}

		#region IDrawDevice implementation
		Vector3 IDrawDevice.RefCoord
		{
			get { return this.deviceCacheValid ? this.deviceCachePos : this.gameobj.Transform.Pos; }
		}
		float IDrawDevice.RefAngle
		{
			get { return this.gameobj.Transform.Angle; }
		}
		VisibilityFlag IDrawDevice.VisibilityMask
		{
			get { return this.deviceVisibility; }
		}
		bool IDrawDevice.IsScreenOverlay
		{
			get { return this.deviceScreenOverlay; }
		}
		Vector2 IDrawDevice.TargetSize
		{
			get
			{
				if (this.devicePass == null || !this.devicePass.Output.IsAvailable)
					return this.SceneTargetSize;

				RenderTarget target = this.devicePass.Output.Res;
				return new Vector2(target.Width, target.Height);
			}
		}

		void IDrawDevice.PreprocessCoords(ref Vector3 pos, ref float scale)
		{
			if (this.overlayMatrices) return;
			if (this.deviceCacheValid)
			{
				Vector3.Subtract(ref pos, ref this.deviceCachePos, out pos);
			}
			else
			{
				Vector3 gameObjPos = this.GameObj.Transform.Pos;
				Vector3.Subtract(ref pos, ref gameObjPos, out pos);
			}
			float scaleTemp = this.focusDist / (this.focusDist >= 0.0f ? Math.Max(pos.Z, this.nearZ) : -DefaultFocusDist);
			pos.X *= scaleTemp;
			pos.Y *= scaleTemp;
			scale *= scaleTemp;
		}
		bool IDrawDevice.IsCoordInView(Vector3 c, float boundRad)
		{
			if (c.Z <= this.GameObj.Transform.Pos.Z) return false;

			// Retrieve center vertex coord
			float scaleTemp = 1.0f;
			this.DrawDevice.PreprocessCoords(ref c, ref scaleTemp);

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
		void IDrawDevice.AddVertices<T>(BatchInfo material, VertexMode vertexMode, params T[] vertices)
		{
			if (material == null || material.Technique == null || !material.Technique.IsAvailable) return;
			if (vertices == null || vertices.Length == 0) return;

			if (this.picking != 0)
			{
				if (material.Textures == null)
					material = new BatchInfo(DrawTechnique.Picking, new ColorRgba((this.picking << 8) | 0xFF), Texture.White);
				else
					material = new BatchInfo(DrawTechnique.Picking, new ColorRgba((this.picking << 8) | 0xFF), material.Textures);
			}
			
			if (material.Technique.Res.NeedsPreprocess)
			{
				material = new BatchInfo(material);
				material.Technique.Res.PreprocessBatch<T>(this, material, ref vertexMode, ref vertices);
				if (vertices == null || vertices.Length == 0) return;
			}
			
			// When rendering the screen overlay, use z sorting everywhere - there's no depth buffering!
			bool zSort = this.deviceScreenOverlay || material.Technique.Res.NeedsZSort;
			List<IDrawBatch> buffer = zSort ? this.drawBufferZSort : this.drawBuffer;
			float zSortIndex = zSort ? DrawBatch<T>.CalcZSortIndex(vertices) : 0.0f;

			if (buffer.Count > 0 && buffer[buffer.Count - 1].CanAppendJIT<T>(	
					zSort ? 1.0f / this.zSortAccuracy : 0.0f, 
					zSortIndex, 
					material, 
					vertexMode))
			{
				buffer[buffer.Count - 1].AppendJIT(vertices);
			}
			else
			{
				buffer.Add(new DrawBatch<T>(material, vertexMode, vertices, zSortIndex));
			}
			++this.numRawBatches;
		}
		void IDrawDevice.AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, params T[] vertices)
		{
			if (!material.IsAvailable) return;
			(this as IDrawDevice).AddVertices<T>(material.Res.InfoDirect, vertexMode, vertices);
		}
		#endregion

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
