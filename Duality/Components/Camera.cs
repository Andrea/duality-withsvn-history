using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Components;
using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Enumerates different behviours on how to blend color data onto existing background color.
	/// </summary>
	/// <seealso cref="Duality.Resources.DrawTechnique"/>
	public enum BlendMode
	{
		/// <summary>
		/// When passing this to a method, this value can be used to indicate "Restore to default settings".
		/// </summary>
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
		Count
	}

	/// <summary>
	/// Defines a general interface for drawing devices. Its main duty is to accept and collect parameterized vertex data.
	/// </summary>
	public interface IDrawDevice
	{
		/// <summary>
		/// [GET] A bitmask flagging all visibility groups that are considered visible to this drawing device.
		/// </summary>
		uint VisibilityMask { get; }
		/// <summary>
		/// [GET] The devices general background color.
		/// </summary>
		ColorRgba ClearColor { get; }
		/// <summary>
		/// [GET] The lowest Z value that can be displayed by the device.
		/// </summary>
		float NearZ { get; }
		/// <summary>
		/// [GET] The highest Z value that can be displayed by the device.
		/// </summary>
		float FarZ { get; }

		/// <summary>
		/// Processes the specified world space position and scale values and transforms them to the IDrawDevices view space.
		/// This usually also applies a parallax effect, if applicable.
		/// </summary>
		/// <param name="pos">The position to process.</param>
		/// <param name="scale">The scale factor to process.</param>
		void PreprocessCoords(ref Vector3 pos, ref float scale);
		/// <summary>
		/// Processes the specified world space position and scale values and transforms them to the IDrawDevices view space.
		/// This usually also applies a parallax effect, if applicable.
		/// </summary>
		/// <param name="r">The <see cref="Duality.Components.ICmpRenderer"/> to which the data belongs.</param>
		/// <param name="pos">The position to process.</param>
		/// <param name="scale">The scale factor to process.</param>
		void PreprocessCoords(ICmpRenderer r, ref Vector3 pos, ref float scale);
		/// <summary>
		/// Returns whether the specified world-space position is visible in the drawing devices view space.
		/// </summary>
		/// <param name="c">The position to test.</param>
		/// <param name="boundRad">The visual bounding radius to assume for the specified position.</param>
		/// <returns>True, if the position or a portion of its bounding circle is visible, false if not.</returns>
		bool IsCoordInView(Vector3 c, float boundRad = 1.0f);
		/// <summary>
		/// Returns whether the specified ICmpRenderer is visible in the drawing devices view space. This doesn't yet mean, it's
		/// visible - the ICmpRenderer or IDrawDevice can still decide, not to display.
		/// </summary>
		/// <param name="r">The ICmpRenderer to test.</param>
		/// <returns>True, if the ICmpRenderer or a portion of its bounding circle is inside the view space, false if not.</returns>
		bool IsRendererInView(ICmpRenderer r);

		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.Material"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(ContentRef<Material> material, BeginMode vertexMode, params T[] vertices) where T : struct, IVertexData;
		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.BatchInfo"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(BatchInfo material, BeginMode vertexMode, params T[] vertices) where T : struct, IVertexData;
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
			private	BatchInfo					input			= null;
			private	ContentRef<RenderTarget>	output			= ContentRef<RenderTarget>.Null;
			private	bool						fitOutput		= false;
			private	bool						keepOutput		= false;
			private	uint						visibilityMask	= uint.MaxValue;
			
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
			/// Specifies whether previous image data on this passes rendering target should be kept, i.e. whether clearing
			/// it before beginning to render can be skipped.
			/// </summary>
			public bool KeepOutput
			{
				get { return this.keepOutput; }
				set { this.keepOutput = value; }
			}
			/// <summary>
			/// [GET / SET] A Pass-local bitmask flagging all visibility groups that are considered visible to this drawing device.
			/// </summary>
			public uint VisibilityMask
			{
				get { return this.visibilityMask; }
				set { this.visibilityMask = value; }
			}

			public Pass() {}
			public Pass(Pass copyFrom, BatchInfo inputOverride)
			{
				this.input = inputOverride;
				this.output = copyFrom.output;
				this.fitOutput = copyFrom.fitOutput;
				this.keepOutput = copyFrom.keepOutput;
				this.visibilityMask = copyFrom.visibilityMask;
			}

			public override string ToString()
			{
				ContentRef<Texture> inputTex = input == null ? ContentRef<Texture>.Null : input.MainTexture;
				return string.Format("{0} => {1}",
					inputTex.IsExplicitNull ? (input == null ? "Camera" : "Undefined") : inputTex.Name,
					output.IsExplicitNull ? "Screen" : output.Name);
			}
		}

		private interface IDrawBatch
		{
			int SortIndex { get; }
			float ZSortIndex { get; }
			int VertexCount { get; }
			BeginMode VertexMode { get; }
			BatchInfo Material { get; }

			void SetupVBO(List<IDrawBatch> batches);
			void FinishVBO();
			void Render(ref int vertexOffset, ref IDrawBatch lastBatchRendered);
			void FinishRendering();

			bool CanShareVBO(IDrawBatch other);
			bool CanAppendJIT<T>(float invZSortAccuracy, float zSortIndex, BatchInfo material, BeginMode vertexMode) where T : struct, IVertexData;
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
			private	BeginMode	vertexMode	= BeginMode.Points;
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
			public BeginMode VertexMode
			{
				get { return this.vertexMode; }
			}
			public BatchInfo Material
			{
				get { return this.material; }
			}

			public DrawBatch(BatchInfo material, BeginMode vertexMode, T[] vertices, float zSortIndex)
			{
				if (vertices == null || vertices.Length == 0) throw new ArgumentException("A zero-vertex DrawBatch is invalid.");
				
				this.material = material;
				this.vertexMode = vertexMode;
				this.vertices = vertices;
				this.vertexCount = vertices.Length;
				this.zSortIndex = zSortIndex;

				if (!this.material.Technique.Res.NeedsZSort)
				{
					int vTypeSI = vertices[0].VertexTypeIndex;
					int matHash = material.GetHashCode();

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

			public void SetupVBO(List<IDrawBatch> batches)
			{
				// Check how many vertices we got
				int totalVertexNum = 0;
				for (int i = 0; i < batches.Count; i++) 
					totalVertexNum += batches[i].VertexCount;

				// Collect vertex data in one array
				DrawBatch<T> b;
				int curVertexPos = 0;
				T[] vertexData = new T[totalVertexNum];
				int[] batchBeginIndices = new int[batches.Count];
				for (int i = 0; i < batches.Count; i++)
				{
					b = batches[i] as DrawBatch<T>;
					Array.Copy(b.vertices, 0, vertexData, curVertexPos, b.vertexCount);
					batchBeginIndices[i] = curVertexPos;
					curVertexPos += b.vertexCount;
				}

				// Set up VBO and submit vertex data to GPU
				this.vertices[0].SetupVBO(vertexData, this.material);
			}
			public void FinishVBO()
			{
				// Finish VBO
				this.vertices[0].FinishVBO(this.material);
			}
			public void Render(ref int vertexOffset, ref IDrawBatch lastBatchRendered)
			{
				if (lastBatchRendered == null || lastBatchRendered.Material != this.material)
				    this.material.SetupForRendering(lastBatchRendered == null ? null : lastBatchRendered.Material);

				GL.DrawArrays(this.vertexMode, vertexOffset, this.vertexCount);

				vertexOffset += this.vertexCount;
				lastBatchRendered = this;
			}
			public void FinishRendering()
			{
				this.material.FinishRendering();
			}

			public bool CanShareVBO(IDrawBatch other)
			{
				return other.GetType() == this.GetType();
			}
			public bool CanAppendJIT<U>(float invZSortAccuracy, float zSortIndex, BatchInfo material, BeginMode vertexMode) where U : struct, IVertexData
			{
				if (invZSortAccuracy > 0.0f && this.material.Technique.Res.NeedsZSort)
				{
					if (Math.Abs(zSortIndex - this.ZSortIndex) > invZSortAccuracy) return false;
				}
				return 
					vertexMode == this.VertexMode && 
					IsVertexModeAppendable(this.VertexMode) &&
					this is DrawBatch<U> &&
					material == this.Material;
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
					other.VertexMode == this.VertexMode && 
					IsVertexModeAppendable(this.VertexMode) &&
					other is DrawBatch<T> &&
					other.Material == this.Material;
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

			public static bool IsVertexModeAppendable(BeginMode mode)
			{
				return 
					mode == BeginMode.Lines || 
					mode == BeginMode.Points || 
					mode == BeginMode.Quads || 
					mode == BeginMode.Triangles;
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
		/// The default reference distance for parallax rendering.
		/// </summary>
		public const float DefaultParallaxRefDist	= 500.0f;

		private	float	nearZ				= 0.0f;
		private	float	farZ				= 10000.0f;
		private	float	zSortAccuracy		= 0.0f;
		private	float	parallaxRefDist		= DefaultParallaxRefDist;
		private	uint	visibilityMask		= uint.MaxValue;
		private	ColorRgba	clearColor		= ColorRgba.TransparentBlack;
		private	ClearFlags	clearMask		= ClearFlags.All;
		private	Pass[]		passes			= new Pass[] { new Pass() };

		[NonSerialized]	private	Matrix4	matModelView		= Matrix4.Identity;
		[NonSerialized]	private	Matrix4	matProjection		= Matrix4.Identity;
		[NonSerialized]	private	Matrix4	matFinal			= Matrix4.Identity;
		[NonSerialized]	private	bool	overlayMatrices		= false;

		[NonSerialized]	private	uint				hndlPrimaryVBO		= 0;
		[NonSerialized]	private	uint				deviceVisibility	= uint.MaxValue;
		[NonSerialized]	private	bool				deviceCacheValid	= false;
		[NonSerialized]	private	Vector3				deviceCachePos		= Vector3.Zero;
		[NonSerialized]	private	int					picking				= 0;
		[NonSerialized]	private	List<ICmpRenderer>		pickingMap			= null;
		[NonSerialized]	private	RenderTarget		pickingRT			= null;
		[NonSerialized]	private	Texture				pickingTex			= null;
		[NonSerialized]	private	int					pickingLast			= -1;
		[NonSerialized]	private	byte[]				pickingBuffer		= new byte[4 * 256 * 256];
		[NonSerialized]	private	List<IDrawBatch>	drawBuffer			= new List<IDrawBatch>();
		[NonSerialized]	private	List<IDrawBatch>	drawBufferZSort		= new List<IDrawBatch>();
		[NonSerialized]	private	List<Predicate<ICmpRenderer>>	editorRenderFilter	= new List<Predicate<ICmpRenderer>>();

		/// <summary>
		/// Fired as soon as <see cref="Duality.Components.ICmpRenderer"/> drawcalls have been collected, but
		/// right before processing them.
		/// </summary>
		public event EventHandler CollectRendererDrawcalls	= null;
		/// <summary>
		/// Fired as soon as <see cref="Duality.ICmpScreenOverlayICmpRenderer"/> drawcalls have been collected, but
		/// right before processing them.
		/// </summary>
		public event EventHandler CollectOverlayDrawcalls	= null;
		
		/// <summary>
		/// [GET / SET] The lowest Z value that can be displayed by the device.
		/// </summary>
		public float NearZ
		{
			get { return this.nearZ; }
			set { this.nearZ = value; this.UpdateZSortAccuracy(); }
		}
		/// <summary>
		/// [GET / SET] The highest Z value that can be displayed by the device.
		/// </summary>
		public float FarZ
		{
			get { return this.farZ; }
			set { this.farZ = value; this.UpdateZSortAccuracy(); }
		}
		/// <summary>
		/// [GET / SET] Reference distance for calculating the parallax effect. An object this far away from
		/// the Camera will appear in its original size.
		/// </summary>
		public float ParallaxRefDist
		{
			get { return this.parallaxRefDist; }
			set { this.parallaxRefDist = value; }
		}
		/// <summary>
		/// [GET / SET] A bitmask flagging all visibility groups that are considered visible to this drawing device.
		/// </summary>
		public uint VisibilityMask
		{
			get { return this.visibilityMask; }
			set { this.visibilityMask = value; }
		}
		uint IDrawDevice.VisibilityMask
		{
			get { return this.deviceVisibility; }
		}
		/// <summary>
		/// [GET / SET] A Bitmask describing which components of the current (or back-)buffer to clear before rendering.
		/// </summary>
		public ClearFlags ClearMask
		{
			get { return this.clearMask; }
			set { this.clearMask = value; }
		}
		/// <summary>
		/// [GET / SET] The background color of the rendered image.
		/// </summary>
		public ColorRgba ClearColor
		{
			get { return this.clearColor; }
			set { this.clearColor = value; }
		}
		/// <summary>
		/// [GET / SET] A set of passes that describes the Cameras rendering process. Is never null nor empty.
		/// </summary>
		public Pass[] Passes
		{
			get { return this.passes; }
			set 
			{ 
				if (value == null || value.Length < 1) value = new Pass[1];
				Array.Resize(ref this.passes, value.Length);
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] == null) value[i] = new Pass();
					if (i == 0)
						this.passes[i] = new Pass(value[i], null);
					else
						this.passes[i] = new Pass(value[i], value[i].Input == null ? new BatchInfo() : value[i].Input);
				}
			}
		}

		/// <summary>
		/// [GET] The drawing device which this Camera uses for rendering.
		/// </summary>
		public IDrawDevice DrawDevice
		{
			get { return this as IDrawDevice; }
		}
		/// <summary>
		/// [GET] The drawing devices target size for rendering the Scene.
		/// </summary>
		public Vector2 SceneTargetSize
		{
			get
			{
				for (int i = 0; i < this.passes.Length; i++)
				{
					if (this.passes[i].Input == null)
					{
						return !this.passes[i].Output.IsAvailable ? 
							DualityApp.TargetResolution : 
							new Vector2(this.passes[i].Output.Res.Width, this.passes[i].Output.Res.Height);
					}
				}
				return DualityApp.TargetResolution;
			}
		}
		/// <summary>
		/// [GET] A Rect describing the Cameras absolute ortho value.
		/// </summary>
		public Rect SceneOrthoAbs
		{
			get { return new Rect(this.SceneTargetSize); }
		}
		/// <summary>
		/// [GET] A Rect describing the Cameras absolute viewport.
		/// </summary>
		public Rect SceneViewportAbs
		{
			get { return new Rect(this.SceneTargetSize); }
		}
		/// <summary>
		/// [GET] The Cameras view space bounding circle radius.
		/// </summary>
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
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Camera t = target as Camera;
			t.nearZ				= this.nearZ;
			t.farZ				= this.farZ;
			t.parallaxRefDist	= this.parallaxRefDist;
			t.visibilityMask	= this.visibilityMask;
			t.clearColor		= this.clearColor;
			t.clearMask			= this.clearMask;
			t.passes			= this.passes != null ? this.passes.Clone() as Pass[] : null;
		}

		/// <summary>
		/// Renders the current <see cref="Duality.Resources.Scene"/>.
		/// </summary>
		public void Render()
		{
			this.deviceCacheValid = true;
			this.deviceCachePos = this.GameObj.Transform.Pos;

			if (this.picking != 0)
			{
				this.SetupPickingRT();
				RenderTarget.Bind(this.pickingRT);
				
				Vector2 refSize = new Vector2(this.pickingRT.Width, this.pickingRT.Height);
				Rect viewportAbs = new Rect(refSize);
				GL.Viewport((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);
				GL.Scissor((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);

				GL.ClearDepth(1.0d);
				GL.ClearColor(System.Drawing.Color.Black);
				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

				this.deviceVisibility = this.visibilityMask;
				this.RenderBaseInput();
				RenderTarget.Bind(RenderTarget.None);
			}
			else
			{
				Performance.BeginMeasure("Camera_" + this.gameobj.Name + "_Render");
				Performance.timeRender.BeginMeasure();

				for (int i = 0; i < this.passes.Length; i++)
				{
					this.deviceVisibility = this.visibilityMask & this.passes[i].VisibilityMask;
					this.RenderSinglePass(this.passes[i]);
				}
				this.deviceVisibility = this.visibilityMask;

				RenderTarget.Bind(RenderTarget.None);
				this.RenderScreenOverlay();

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
			GL.ReadBuffer(ReadBufferMode.Back);
			RenderTarget.Bind(lastTex);

			return true;
		}
		/// <summary>
		/// Picks the <see cref="Duality.Components.ICmpRenderer"/> that owns the pixel at the specified position.
		/// </summary>
		/// <param name="x">x-Coordinate of the pixel to check.</param>
		/// <param name="y">y-Coordinate of the pixel to check.</param>
		/// <returns>The <see cref="Duality.Components.ICmpRenderer"/> that owns the pixel.</returns>
		public ICmpRenderer PickRendererAt(int x, int y)
		{
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
				return this.pickingMap[rendererId - 1];
			else
				return null;
		}
		/// <summary>
		/// Picks all <see cref="Duality.Components.ICmpRenderer">ICmpRenderers</see> contained within the specified
		/// rectangular area.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect.</param>
		/// <param name="y">y-Coordinate of the Rect.</param>
		/// <param name="w">Width of the Rect.</param>
		/// <param name="h">Height of the Rect.</param>
		/// <returns>A set of all <see cref="Duality.Components.ICmpRenderer">ICmpRenderers</see> that have been picked.</returns>
		public HashSet<ICmpRenderer> PickRenderersIn(int x, int y, int w, int h)
		{
			this.RenderPickingMap();

			x = Math.Max(x, 0);
			y = Math.Max(y, 0);
			w = Math.Min(w, this.pickingTex.PxWidth - x);
			h = Math.Min(h, this.pickingTex.PxHeight - y);
			int pxNum = w * h;
			int pxByteNum = pxNum * 4;

			HashSet<ICmpRenderer> result = new HashSet<ICmpRenderer>();
			int rendererId;
			int rendererIdLast = 0;
			unsafe { fixed (byte* pDataBegin = this.pickingBuffer) {
				byte* pData;
				for (int j = 0; j < h; ++j)
				{
					pData = pDataBegin + 4 * (x + (y + j) * this.pickingTex.PxWidth);
					for (int i = 0; i < w; ++i)
					{
						rendererId = 
							(*pData << 16) |
							(*(pData + 1) << 8) |
							(*(pData + 2) << 0);
						if (rendererId != rendererIdLast)
						{
							if (rendererId - 1 > this.pickingMap.Count)
								Log.Core.WriteWarning("Unexpected picking result: {0}", ColorRgba.FromIntArgb(rendererId));
							else if (rendererId != 0)
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
			Vector3 dummy = screenPos;
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref dummy, ref scale);

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
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref spacePos, ref scale);
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
			GL.Viewport((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);
			GL.Scissor((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);

			if (!p.KeepOutput)
			{
				if (p.Input == null)
				{
					GL.ClearDepth(1.0d);
					GL.ClearColor((OpenTK.Graphics.Color4)this.clearColor);
					ClearBufferMask glClearMask = 0;
					if ((this.clearMask & ClearFlags.Color) != ClearFlags.None) glClearMask |= ClearBufferMask.ColorBufferBit;
					if ((this.clearMask & ClearFlags.Depth) != ClearFlags.None) glClearMask |= ClearBufferMask.DepthBufferBit;
					GL.Clear(glClearMask);
				}
				else
				{
					GL.ClearDepth(1.0d);
					GL.ClearColor((OpenTK.Graphics.Color4)ColorRgba.TransparentBlack);
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				}
			}

			if (p.Input == null)
				this.RenderBaseInput();
			else
			{
				this.SetupMatrices(true);

				Texture mainTex = 
					p.Input.Textures != null && 
					p.Input.Textures.Values.Any() &&
					p.Input.Textures.Values.First().IsAvailable ?
					p.Input.Textures.Values.First().Res : null;
				Vector2 uvRatio = mainTex != null ? mainTex.UVRatio : Vector2.One;
				Vector2 inputSize = mainTex != null ? new Vector2(mainTex.PxWidth, mainTex.PxHeight) : Vector2.One;
				Rect targetRect = new Rect(p.FitOutput ? refSize : inputSize);
				if (!p.FitOutput)
				{
					targetRect.x = MathF.Round(refSize.X * 0.5f - inputSize.X * 0.5f);
					targetRect.y = MathF.Round(refSize.Y * 0.5f - inputSize.Y * 0.5f);
				}

				IDrawDevice device = this.DrawDevice;
				device.AddVertices(p.Input, BeginMode.Quads,
					new VertexP3T2(targetRect.MinX,	targetRect.MinY,	0.0f,	0.0f,		0.0f),
					new VertexP3T2(targetRect.MaxX,	targetRect.MinY,	0.0f,	uvRatio.X,	0.0f),
					new VertexP3T2(targetRect.MaxX,	targetRect.MaxY,	0.0f,	uvRatio.X,	uvRatio.Y),
					new VertexP3T2(targetRect.MinX,	targetRect.MaxY,	0.0f,	0.0f,		uvRatio.Y));

				Performance.timePostProcessing.BeginMeasure();
				this.ProcessDrawcalls(true);
				Performance.timePostProcessing.EndMeasure();
				this.SetupMatrices();
			}
		}
		private void RenderBaseInput()
		{
			this.SetupMatrices();

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

				if (this.CollectRendererDrawcalls != null)
					this.CollectRendererDrawcalls(this, EventArgs.Empty);

				Performance.timeCollectDrawcalls.EndMeasure();
			}

			this.ProcessDrawcalls();
		}
		private void RenderScreenOverlay()
		{
			this.SetupMatrices(true);

			foreach (ICmpScreenOverlayRenderer r in Scene.Current.QueryVisibleOverlayRenderers(this.DrawDevice))
				r.DrawOverlay(this);
			
			if (this.CollectOverlayDrawcalls != null)
				this.CollectOverlayDrawcalls(this, EventArgs.Empty);

			this.ProcessDrawcalls(true);
			this.SetupMatrices();
		}
		private void ProcessDrawcalls(bool screenOverlay = false)
		{
			if (screenOverlay)
			{
				// Prepare Rendering
				GL.Enable(EnableCap.ScissorTest);
				GL.Disable(EnableCap.DepthTest);
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
				if (screenOverlay) GL.Translate(0.0f, RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
				GL.Scale(1.0f, -1.0f, 1.0f);
				if (screenOverlay) GL.Translate(0.0f, -RenderTarget.BoundRT.Res.Height * 0.5f, 0.0f);
			}

			// Process drawcalls
			this.OptimizeBatches(screenOverlay);
			this.BeginBatchRendering();

			// Z-Independent: Sorted as needed by batch optimizer
			this.RenderBatches(this.drawBuffer);
			// Z-Sorted: Back to Front
			this.RenderBatches(this.drawBufferZSort);

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
		private void SetupMatrices(bool screenOverlay = false)
		{
			ContentRef<RenderTarget> rt = RenderTarget.BoundRT.Res;
			Vector2 refSize = rt.IsAvailable ? new Vector2(rt.Res.Width, rt.Res.Height) : DualityApp.TargetResolution;

			// If we're expecting a crunched image, also crunch the picking pass
			if (this.picking != 0)
			{
				for (int i = 0; i < this.passes.Length; i++)
				{
					if (this.passes[i].Output == null && this.passes[i].FitOutput)
					{
						Vector2 targetSize = this.passes[i].Input == null || 
							this.passes[i].Input.Textures == null || 
							this.passes[i].Input.Textures.Values.FirstOrDefault() == null || 
							!this.passes[i].Input.Textures.Values.FirstOrDefault().IsAvailable ? 
							DualityApp.TargetResolution : 
							new Vector2(this.passes[i].Input.Textures.Values.First().Res.PxWidth, this.passes[i].Input.Textures.Values.First().Res.PxHeight);
						refSize = targetSize;
						break;
					}
				}
			}

			this.GenerateModelView(out this.matModelView, screenOverlay);
			this.GenerateProjection(rt.IsAvailable ? new Rect(refSize) : new Rect(DualityApp.TargetResolution), out this.matProjection, screenOverlay);
			this.matFinal = this.matModelView * this.matProjection;
			this.overlayMatrices = screenOverlay;
		}
		private void GenerateModelView(out Matrix4 mvMat, bool screenOverlay = false)
		{
			mvMat = Matrix4.Identity;
			if (screenOverlay) return;

			// Translate objects contrary to the camera
			// Removed: Do this in software now for parallax support
			// modelViewMat *= Matrix4.CreateTranslation(-this.GameObj.Transform.Pos);

			// Rotate them according to the camera angle
			mvMat *= Matrix4.CreateRotationZ(-this.GameObj.Transform.Angle);
		}
		private void GenerateProjection(Rect orthoAbs, out Matrix4 projMat, bool screenOverlay = false)
		{
			if (screenOverlay)
			{
				Matrix4.CreateOrthographicOffCenter(
					orthoAbs.x,
					orthoAbs.x + orthoAbs.w, 
					orthoAbs.y + orthoAbs.h, 
					orthoAbs.y, 
					this.nearZ, 
					this.farZ,
					out projMat);
				// Flip Z direction from "out of the screen" to "into the screen".
				projMat.M33 = -projMat.M33;
			}
			else
			{
				Matrix4.CreateOrthographicOffCenter(
					orthoAbs.x - orthoAbs.w * 0.5f, 
					orthoAbs.x + orthoAbs.w * 0.5f, 
					orthoAbs.y + orthoAbs.h * 0.5f, 
					orthoAbs.y - orthoAbs.h * 0.5f, 
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
			if (this.picking == 0) Performance.timeOptimizeDrawcalls.BeginMeasure();

			// Non-ZSorted
			if (this.drawBuffer.Count > 1)
			{
				if (screenOverlay)
				{
					// When rendering the screen overlay, use z sorting everywhere - there's no depth buffering!
					this.drawBuffer.StableSort(this.DrawBatchComparerZSort);
				}
				else
					this.drawBuffer.Sort(this.DrawBatchComparer);
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
		private void RenderBatches(List<IDrawBatch> buffer)
		{
			if (this.picking == 0) Performance.timeProcessDrawcalls.BeginMeasure();

			int vertexOffset;
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
					vertexOffset = 0;
					batchesSharingVBO[0].SetupVBO(batchesSharingVBO);

					foreach (IDrawBatch renderBatch in batchesSharingVBO)
						renderBatch.Render(ref vertexOffset, ref lastBatchRendered);

					batchesSharingVBO[0].FinishVBO();
					batchesSharingVBO.Clear();
					lastBatch = null;
				}
				else
					lastBatch = currentBatch;
			}

			if (lastBatchRendered != null)
				lastBatchRendered.FinishRendering();

			if (this.picking == 0) Performance.timeProcessDrawcalls.EndMeasure();
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
			float scaleTemp = this.parallaxRefDist / (this.parallaxRefDist >= 0.0f ? Math.Max(pos.Z, this.nearZ) : -DefaultParallaxRefDist);
			pos.X *= scaleTemp;
			pos.Y *= scaleTemp;
			scale *= scaleTemp;
		}
		void IDrawDevice.PreprocessCoords(ICmpRenderer r, ref Vector3 pos, ref float scale)
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
			if ((r.RenderFlags & (RendererFlags.ParallaxPos | RendererFlags.ParallaxScale)) != RendererFlags.None)
			{
				float scaleTemp = this.parallaxRefDist / (this.parallaxRefDist >= 0.0f ? Math.Max(pos.Z, this.nearZ) : -DefaultParallaxRefDist);
				if ((r.RenderFlags & RendererFlags.ParallaxPos) != RendererFlags.None)
				{
					pos.X *= scaleTemp;
					pos.Y *= scaleTemp;
				}
				if ((r.RenderFlags & RendererFlags.ParallaxScale) != RendererFlags.None) 
					scale *= scaleTemp;
			}
		}
		bool IDrawDevice.IsCoordInView(Vector3 c, float boundRad)
		{
			if (c.Z <= this.GameObj.Transform.Pos.Z) return false;

			// Retrieve center vertex coord
			float scaleTemp = 1.0f;
			this.DrawDevice.PreprocessCoords(ref c, ref scaleTemp);

			// Apply final (modelview and projection) matrix
			Vector3.Transform(ref c, ref this.matFinal, out c);

			// Apply projection matrices XY rotation and scale to bounding radius
			boundRad *= scaleTemp;
			Vector2 boundRadVec = new Vector2(
				boundRad * this.matProjection.Row0.X - boundRad * this.matProjection.Row1.X,
				boundRad * this.matProjection.Row0.Y - boundRad * this.matProjection.Row1.Y);

			return 
				c.Z >= -1.0f &&
				c.Z <= 1.0f &&
				c.X >= -1.0f - boundRadVec.X &&
				c.Y >= -1.0f - boundRadVec.Y &&
				c.X <= 1.0f + boundRadVec.X &&
				c.Y <= 1.0f + boundRadVec.Y;
		}
		bool IDrawDevice.IsRendererInView(ICmpRenderer r)
		{
			// Retrieve center vertex coord
			Vector3 posTemp = r.SpaceCoord;

			if (r.IsInfiniteXY) return posTemp.Z >= this.GameObj.Transform.Pos.Z;
			if (posTemp.Z <= this.GameObj.Transform.Pos.Z) return false;

			// Process center vertex coord
			float scaleTemp = 1.0f;
			this.DrawDevice.PreprocessCoords(r, ref posTemp, ref scaleTemp);

			// Apply final (modelview and projection) matrix
			Vector3.Transform(ref posTemp, ref this.matFinal, out posTemp);

			// Apply projection matrices XY rotation and scale to bounding radius
			float boundRad = r.BoundRadius * scaleTemp;
			Vector2 boundRadVec = new Vector2(
				boundRad * this.matProjection.Row0.X - boundRad * this.matProjection.Row1.X,
				boundRad * this.matProjection.Row0.Y - boundRad * this.matProjection.Row1.Y);

			return 
				posTemp.Z >= -1.0f &&
				posTemp.Z <= 1.0f &&
				posTemp.X >= -1.0f - boundRadVec.X &&
				posTemp.Y >= -1.0f - boundRadVec.Y &&
				posTemp.X <= 1.0f + boundRadVec.X &&
				posTemp.Y <= 1.0f + boundRadVec.Y;
		}
		void IDrawDevice.AddVertices<T>(BatchInfo material, BeginMode vertexMode, params T[] vertices)
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
			
			if (material.Technique.Res.NeedsVertexPreprocess)
			{
				material = new BatchInfo(material);
				vertices = vertices.Clone() as T[];
				material.Technique.Res.PreprocessVertices<T>(ref material, ref vertexMode, ref vertices);
			}

			bool zSort = material.Technique.Res.NeedsZSort;
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
		}
		void IDrawDevice.AddVertices<T>(ContentRef<Material> material, BeginMode vertexMode, params T[] vertices)
		{
			if (!material.IsAvailable) return;
			(this as IDrawDevice).AddVertices<T>(material.Res.InfoDirect, vertexMode, vertices);
		}

		public static void RenderVoid()
		{
			RenderTarget.Bind(ContentRef<RenderTarget>.Null);
			
			Vector2 refSize = DualityApp.TargetResolution;
			Rect viewportAbs = new Rect(refSize);
			GL.Viewport((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);
			GL.Scissor((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);

			GL.ClearDepth(1.0d);
			GL.ClearColor((OpenTK.Graphics.Color4)ColorRgba.TransparentBlack);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}
	}
}
