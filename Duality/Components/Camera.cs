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
	public enum BlendMode
	{
		// Restore default settings
		Reset = -1,

		// Depth-Independent
		Solid,
		Mask,
		Add,

		// Depth-Dependent
		Alpha,
		Multiply,
		Light,
		Invert,

		// Number of BlendModes
		Count
	}

	public interface IDrawDevice
	{
		uint VisibilityMask { get; }
		ColorRGBA ClearColor { get; }
		float NearZ { get; }
		float FarZ { get; }

		void PreprocessCoords(ref Vector3 pos, ref float scale);
		void PreprocessCoords(Renderer r, ref Vector3 pos, ref float scale);
		bool IsCoordInView(Vector3 c, float boundRad = 1.0f);
		bool IsRendererInView(Renderer r);

		void AddVertices<T>(ContentRef<Material> material, BeginMode vertexType, params T[] vertices) where T : struct, IVertexData;
		void AddVertices<T>(BatchInfo material, BeginMode vertexType, params T[] vertices) where T : struct, IVertexData;
	}
}

namespace Duality.Components
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public sealed class Camera : Component, IDrawDevice
	{
		[Flags]
		public enum ClearFlags
		{
			None	= 0x0,

			Color	= 0x1,
			Depth	= 0x2,

			Default	= Color | Depth,
			All		= Color | Depth
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

		public const float DefaultParallaxRefDist	= 500.0f;

		private	bool	viewportRelative	= true;
		private	Rect	viewport			= new Rect(0, 0, 1, 1);
		private	bool	orthoRelative		= true;
		private	Rect	ortho				= new Rect(0, 0, 1, 1);
		private	float	nearZ				= 0.0f;
		private	float	farZ				= 10000.0f;
		private	float	zSortAccuracy		= 0.0f;
		private	float	parallaxRefDist		= DefaultParallaxRefDist;
		private	uint	visibilityMask		= uint.MaxValue;
		private	ColorRGBA	clearColor		= ColorRGBA.TransparentBlack;
		private	ClearFlags	clearMask		= ClearFlags.All;
		private	ContentRef<RenderTarget>	rt	= RenderTarget.None;

		private	Matrix4	matModelView		= Matrix4.Identity;
		private	Matrix4	matProjection		= Matrix4.Identity;
		private	Matrix4	matFinal			= Matrix4.Identity;

		private	uint				picking			= 0;
		private	List<Renderer>		pickingMap		= null;
		private	RenderTarget		pickingRT		= null;
		private	Texture				pickingTex		= null;
		private	int					pickingLast		= -1;
		private	byte[]				pickingBuffer	= new byte[4 * 256 * 256];
		private	uint				hndlPrimaryVBO	= 0;
		private	List<IDrawBatch>	drawBuffer		= new List<IDrawBatch>();
		private	List<IDrawBatch>	drawBufferZSort	= new List<IDrawBatch>();

		public Rect Viewport
		{
			get { return this.viewport; }
			set { this.viewport = value; }
		}
		public bool ViewportRelative
		{
			get { return this.viewportRelative; }
			set { this.viewportRelative = value; }
		}
		public Rect Ortho
		{
			get { return this.ortho; }
			set { this.ortho = value; }
		}
		public bool OrthoRelative
		{
			get { return this.orthoRelative; }
			set { this.orthoRelative = value; }
		}
		public float NearZ
		{
			get { return this.nearZ; }
			set { this.nearZ = value; this.UpdateZSortAccuracy(); }
		}
		public float FarZ
		{
			get { return this.farZ; }
			set { this.farZ = value; this.UpdateZSortAccuracy(); }
		}
		public float ParallaxRefDist
		{
			get { return this.parallaxRefDist; }
			set { this.parallaxRefDist = value; }
		}
		public uint VisibilityMask
		{
			get { return this.visibilityMask; }
			set { this.visibilityMask = value; }
		}
		public ClearFlags ClearMask
		{
			get { return this.clearMask; }
			set { this.clearMask = value; }
		}
		public ColorRGBA ClearColor
		{
			get { return this.clearColor; }
			set { this.clearColor = value; }
		}
		public ContentRef<RenderTarget> Target
		{
			get { return this.rt; }
			set { this.rt = value; }
		}

		public IDrawDevice DrawDevice
		{
			get { return this as IDrawDevice; }
		}
		public Vector2 TargetSize
		{
			get { return this.rt.IsExplicitNull ? DualityApp.TargetResolution : new Vector2(this.rt.Res.Width, this.rt.Res.Height); }
		}
		public Rect OrthoAbs
		{
			get { return this.ortho.Transform(this.orthoRelative ? this.TargetSize : Vector2.One); }
		}
		public Rect ViewportAbs
		{
			get { return this.viewport.Transform(this.viewportRelative ? this.TargetSize : Vector2.One); }
		}

		public Camera()
		{
			this.UpdateZSortAccuracy();
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Camera t = target as Camera;
			t.viewportRelative	= this.viewportRelative;
			t.viewport			= this.viewport;
			t.orthoRelative		= this.orthoRelative;
			t.ortho				= this.ortho;
			t.nearZ				= this.nearZ;
			t.farZ				= this.farZ;
			t.parallaxRefDist	= this.parallaxRefDist;
			t.visibilityMask	= this.visibilityMask;
			t.clearColor		= this.clearColor;
			t.clearMask			= this.clearMask;
		}

		public void Render()
		{
			// Determine rendering target data
			RenderTarget rtRes = this.rt.IsExplicitNull ? null : this.rt.Res;
			Vector2 refSize = this.TargetSize;
			Rect viewportAbs = this.ViewportAbs;

			// Update matrices
			this.GenerateModelView(out this.matModelView);
			this.GenerateProjection(this.OrthoAbs, out this.matProjection);
			this.matFinal = this.matModelView * this.matProjection;

			// Collect drawcalls
			if (this.picking != 0)
			{
				this.pickingMap = new List<Renderer>(Scene.Current.QueryVisibleRenderers(this.DrawDevice));
				foreach (Renderer r in this.pickingMap)
				{
					r.Draw(this);
					if (this.picking != 0) this.picking++;
				}
			}
			else
			{
				foreach (Renderer r in Scene.Current.QueryVisibleRenderers(this.DrawDevice))
					r.Draw(this);

				//FormattedText text = new FormattedText(
				//    "/f[0]Franz jagt im komplett verwahrlosten Taxi quer durch Bayern/n" + 
				//    "/f[1]Franz jagt im komplett verwahrlosten Taxi quer durch Bayern/n" + 
				//    "/f[2]Franz jagt im komplett verwahrlosten Taxi quer durch Bayern/n" + 
				//    "/f[2]      jagt      mplett     ahr   te  T xi  ue    r h   yern");
				//text.SetFonts(Font.GenericSerif12, Font.GenericSansSerif12, Font.GenericMonospace10);
				//text.SetIcons(
				//    new FormattedText.Icon(new Rect(0.0f, 0.0f, 0.5f, 0.5f), new Vector2(32, 32)), 
				//    new FormattedText.Icon(new Rect(0.5f, 0.0f, 0.5f, 0.5f), new Vector2(32, 32)), 
				//    new FormattedText.Icon(new Rect(0.5f, 0.5f, 0.5f, 0.5f), new Vector2(32, 32)), 
				//    new FormattedText.Icon(new Rect(0.0f, 0.5f, 0.5f, 0.5f), new Vector2(32, 32)));
				//FormattedText text = new FormattedText(
				//    "/i[0]/i[1]/i[3]/i[2]Das ist ein Test/n/f[1]/cFF0000FFFranz/cFFFFFFFF jagt im komplett verwahrlosten /f[2]Taxi quer durch Bayern");
				FormattedText text = new FormattedText("/alLorem ipsum dolor /i[0]sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod /i[0]tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et /f[1]justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna /f[2]aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed /f[0]diam nonumy eirmod tempor invidunt ut /f[1]labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Lorem ipsum dolor sit amet");
				text.SetFlowAreas(
				    new FormattedText.FlowArea(0, 150, 100, false),
				    new FormattedText.FlowArea(200, 150, 100, true));
				text.MaxWidth = 1 + MathF.RoundToInt((MathF.Sin(Time.GameTimer / 3000.0f) + 1.0f) * 0.5f * 500.0f);
				text.MaxHeight = 450;
				text.SetFonts(Font.GenericSerif12, Font.GenericSansSerif12, Font.GenericMonospace10);
				text.SetIcons(
					new FormattedText.Icon(new Rect(0.0f, 0.0f, 0.5f, 0.5f), new Vector2(32, 32)), 
					new FormattedText.Icon(new Rect(0.5f, 0.0f, 0.5f, 0.5f), new Vector2(32, 32)), 
					new FormattedText.Icon(new Rect(0.5f, 0.5f, 0.5f, 0.5f), new Vector2(32, 32)), 
					new FormattedText.Icon(new Rect(0.0f, 0.5f, 0.5f, 0.5f), new Vector2(32, 32)));

				VertexFormat.VertexC4P3T2[][] vertText = null;
				VertexFormat.VertexC4P3T2[] vertIcon = null;
				text.EmitVertices(ref vertText, ref vertIcon, -this.gameobj.Transform.Pos.X, -this.gameobj.Transform.Pos.Y);
				this.DrawDevice.AddVertices(Font.GenericSerif12.Res.Material, BeginMode.Quads, vertText[0]);
				this.DrawDevice.AddVertices(Font.GenericSansSerif12.Res.Material, BeginMode.Quads, vertText[1]);
				this.DrawDevice.AddVertices(Font.GenericMonospace10.Res.Material, BeginMode.Quads, vertText[2]);
				this.DrawDevice.AddVertices(Material.DualityLogo256, BeginMode.Quads, vertIcon);

				Vector2 size = text.Measure();
				this.DrawDevice.AddVertices(Material.InvertWhite, BeginMode.LineLoop, new VertexP3[] {
					new VertexP3(-0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(size.X + 0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(size.X + 0.5f - this.gameobj.Transform.Pos.X, size.Y + 0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(-0.5f - this.gameobj.Transform.Pos.X, size.Y + 0.5f - this.gameobj.Transform.Pos.Y, 0)});
				this.DrawDevice.AddVertices(Material.InvertWhite, BeginMode.Lines, new VertexP3[] {
					new VertexP3(text.MaxWidth + 0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(text.MaxWidth + 0.5f - this.gameobj.Transform.Pos.X, size.Y + 0.5f - this.gameobj.Transform.Pos.Y, 0)});
				this.DrawDevice.AddVertices(Material.InvertWhite, BeginMode.Lines, new VertexP3[] {
					new VertexP3(-0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y + text.MaxHeight, 0),
					new VertexP3(size.X + 0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y + text.MaxHeight, 0)});

				this.DrawDevice.AddVertices(Material.InvertWhite, BeginMode.LineLoop, new VertexP3[] {
					new VertexP3(-0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(100 + 0.5f - this.gameobj.Transform.Pos.X, -0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(100 + 0.5f - this.gameobj.Transform.Pos.X, 150 + 0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(-0.5f - this.gameobj.Transform.Pos.X, 150 + 0.5f - this.gameobj.Transform.Pos.Y, 0)});
				this.DrawDevice.AddVertices(Material.InvertWhite, BeginMode.LineLoop, new VertexP3[] {
					new VertexP3(text.MaxWidth - 100 - 0.5f - this.gameobj.Transform.Pos.X, 200 - 0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(text.MaxWidth + 0.5f - this.gameobj.Transform.Pos.X, 200 - 0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(text.MaxWidth + 0.5f - this.gameobj.Transform.Pos.X, 350 + 0.5f - this.gameobj.Transform.Pos.Y, 0),
					new VertexP3(text.MaxWidth - 100 - 0.5f - this.gameobj.Transform.Pos.X, 350 + 0.5f - this.gameobj.Transform.Pos.Y, 0)});
			}
			
			// Setup picking RT
			if (this.picking != 0)
			{
				if (this.pickingTex == null || 
					this.pickingTex.Width != MathF.RoundToInt(refSize.X) || 
					this.pickingTex.Height != MathF.RoundToInt(refSize.Y))
				{
					if (this.pickingTex != null) this.pickingTex.Dispose();
					if (this.pickingRT != null) this.pickingRT.Dispose();
					this.pickingTex = new Texture(
						MathF.RoundToInt(refSize.X), MathF.RoundToInt(refSize.Y), Texture.SizeMode.Default, 
						TextureMagFilter.Nearest, TextureMinFilter.Nearest);
					this.pickingRT = new RenderTarget(0, this.pickingTex);
				}
				rtRes = this.pickingRT;
			}

			// Bind rendertarget
			RenderTarget.Bind(this.picking != 0 ? this.pickingRT : this.rt);

			// Prepare Rendering
			GL.Enable(EnableCap.ScissorTest);
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);

			GL.ClearDepth(1.0d);
			if (this.picking != 0)
				GL.ClearColor(System.Drawing.Color.Black);
			else
				GL.ClearColor((OpenTK.Graphics.Color4)this.clearColor);

			GL.Viewport((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);
			GL.Scissor((int)viewportAbs.x, (int)refSize.Y - (int)viewportAbs.h - (int)viewportAbs.y, (int)viewportAbs.w, (int)viewportAbs.h);

			ClearBufferMask glClearMask = (this.picking != 0) ? ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit : 0;
			if ((this.clearMask & ClearFlags.Color) != ClearFlags.None) glClearMask |= ClearBufferMask.ColorBufferBit;
			if ((this.clearMask & ClearFlags.Depth) != ClearFlags.None) glClearMask |= ClearBufferMask.DepthBufferBit;
			GL.Clear(glClearMask);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref this.matModelView);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref this.matProjection);
			if (rtRes != null) GL.Scale(1.0f, -1.0f, 1.0f);

			// Process drawcalls
			this.OptimizeBatches();
			this.BeginBatchRendering();
			int vertexCount = 0;

			// Z-Independent: Sorted as needed by batch optimizer
			this.RenderBatches(this.drawBuffer, ref vertexCount);
			// Z-Sorted: Back to Front
			this.RenderBatches(this.drawBufferZSort, ref vertexCount);

			this.FinishBatchRendering();
			//if (this.picking == 0)
			//{
			//    Log.Core.WriteTimed(1000, this.gameobj.FullName + "_DrawStats", "Draw stats {0}:\n{1}\n{2}", this.gameobj.FullName,
			//        "\tBatches: " + (this.drawBuffer.Count + this.drawBufferZSort.Count),
			//        "\tVertices: " + vertexCount);
			//}
			this.drawBuffer.Clear();
			this.drawBufferZSort.Clear();

			RenderTarget.Bind(RenderTarget.None);
		}
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
			int pxNum = this.pickingTex.Width * this.pickingTex.Height;
			int pxByteNum = pxNum * 4;
			if (pxByteNum > this.pickingBuffer.Length) Array.Resize(ref this.pickingBuffer, Math.Max(this.pickingBuffer.Length * 2, pxByteNum));

			ContentRef<RenderTarget> lastTex = RenderTarget.BoundRT;
			RenderTarget.Bind(this.pickingRT);
			GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
			GL.ReadPixels(0, 0, this.pickingTex.Width, this.pickingTex.Height, PixelFormat.Rgba, PixelType.UnsignedByte, this.pickingBuffer);
			GL.ReadBuffer(ReadBufferMode.Back);
			RenderTarget.Bind(lastTex);

			return true;
		}
		public Renderer PickRendererAt(int x, int y)
		{
			this.RenderPickingMap();

			x = Math.Max(x, 0);
			y = Math.Max(y, 0);

			int rendererId = 
				(this.pickingBuffer[4 * (x + y * this.pickingTex.Width) + 0] << 16) |
				(this.pickingBuffer[4 * (x + y * this.pickingTex.Width) + 1] << 8) |
				(this.pickingBuffer[4 * (x + y * this.pickingTex.Width) + 2] << 0);
			return (rendererId <= 0 || rendererId >= this.pickingMap.Count) ? null : this.pickingMap[rendererId - 1];
		}
		public HashSet<Renderer> PickRenderersIn(int x, int y, int w, int h)
		{
			this.RenderPickingMap();

			x = Math.Max(x, 0);
			y = Math.Max(y, 0);
			w = Math.Min(w, this.pickingTex.Width - x);
			h = Math.Min(h, this.pickingTex.Height - y);
			int pxNum = w * h;
			int pxByteNum = pxNum * 4;

			HashSet<Renderer> result = new HashSet<Renderer>();
			int rendererId;
			int rendererIdLast = 0;
			unsafe { fixed (byte* pDataBegin = this.pickingBuffer) {
				byte* pData;
				for (int j = 0; j < h; ++j)
				{
					pData = pDataBegin + 4 * (x + (y + j) * this.pickingTex.Width);
					for (int i = 0; i < w; ++i)
					{
						rendererId = 
							(*pData << 16) |
							(*(pData + 1) << 8) |
							(*(pData + 2) << 0);
						if (rendererId != rendererIdLast)
						{
							if (rendererId != 0) result.Add(this.pickingMap[rendererId - 1]);
							rendererIdLast = rendererId;
						}
						pData += 4;
					}
				}
			}}

			return result;
		}

		public float GetScaleAtZ(float z)
		{
			Vector3 dummy = new Vector3(0, 0, z);
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref dummy, ref scale);
			return scale;
		}
		public Vector3 GetSpaceCoord(Vector3 screenPos)
		{
			Vector3 dummy = screenPos;
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref dummy, ref scale);

			Vector2 targetSize = this.TargetSize;
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
		public Vector3 GetSpaceCoord(Vector2 screenPos)
		{
			return this.GetSpaceCoord(new Vector3(screenPos));
		}
		public Vector3 GetScreenCoord(Vector3 spacePos)
		{
			float scale = 1.0f;
			this.DrawDevice.PreprocessCoords(ref spacePos, ref scale);
			MathF.TransformCoord(ref spacePos.X, ref spacePos.Y, -this.GameObj.Transform.Angle);

			Vector2 targetSize = this.TargetSize;
			spacePos.X += targetSize.X / 2;
			spacePos.Y += targetSize.Y / 2;

			return spacePos;
		}
		public Vector3 GetScreenCoord(Vector2 spacePos)
		{
			return this.GetScreenCoord(new Vector3(spacePos));
		}

		private void UpdateZSortAccuracy()
		{
			this.zSortAccuracy = 10000000.0f / Math.Max(1.0f, Math.Abs(this.farZ - this.nearZ));
		}
		private void GenerateModelView(out Matrix4 mvMat)
		{
			mvMat = Matrix4.Identity;

			// Translate objects contrary to the camera
			// Removed: Do this in software now for parallax support
			// modelViewMat *= Matrix4.CreateTranslation(-this.GameObj.Transform.Pos);

			// Rotate them according to the camera angle
			mvMat *= Matrix4.CreateRotationZ(-this.GameObj.Transform.Angle);
		}
		private void GenerateProjection(Rect orthoAbs, out Matrix4 projMat)
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
			// Non-ZSorted
			if (this.drawBuffer.Count > 1)
			{
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
		private void RenderBatches(List<IDrawBatch> buffer, ref int vertexCount)
		{
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
					{
						renderBatch.Render(ref vertexOffset, ref lastBatchRendered);
						vertexCount += renderBatch.VertexCount;
					}
					batchesSharingVBO[0].FinishVBO();
					batchesSharingVBO.Clear();
					lastBatch = null;
				}
				else
					lastBatch = currentBatch;
			}

			if (lastBatchRendered != null)
				lastBatchRendered.FinishRendering();
		}
		private void FinishBatchRendering()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		void IDrawDevice.PreprocessCoords(ref Vector3 pos, ref float scale)
		{
			pos -= this.GameObj.Transform.Pos;
			float scaleTemp = this.parallaxRefDist / (this.parallaxRefDist >= 0.0f ? Math.Max(pos.Z, 0.000001f) : -DefaultParallaxRefDist);
			pos.X *= scaleTemp;
			pos.Y *= scaleTemp;
			scale *= scaleTemp;
		}
		void IDrawDevice.PreprocessCoords(Renderer r, ref Vector3 pos, ref float scale)
		{
			pos -= this.GameObj.Transform.Pos;
			if ((r.RenderFlags & (RendererFlags.ParallaxPos | RendererFlags.ParallaxScale)) != RendererFlags.None)
			{
				float scaleTemp = this.parallaxRefDist / (this.parallaxRefDist >= 0.0f ? Math.Max(pos.Z, 0.000001f) : -DefaultParallaxRefDist);
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
		bool IDrawDevice.IsRendererInView(Renderer r)
		{
			// Retrieve center vertex coord
			Vector3 posTemp = r.GameObj.Transform.Pos;
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
					material = new BatchInfo(DrawTechnique.Picking, new ColorRGBA((this.picking << 8) | 0xFF), Texture.White);
				else
					material = new BatchInfo(DrawTechnique.Picking, new ColorRGBA((this.picking << 8) | 0xFF), material.Textures);
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
	}
}
