using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysDrawFont = System.Drawing.Font;
using BitArray = System.Collections.BitArray;

using WeifenLuo.WinFormsUI.Docking;

using Duality;
using Duality.Components;
using Duality.ColorFormat;
using Duality.VertexFormat;
using Duality.Resources;
using Font = Duality.Resources.Font;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Key = OpenTK.Input.Key;
using MouseButton = OpenTK.Input.MouseButton;
using MouseButtonEventArgs = OpenTK.Input.MouseButtonEventArgs;
using KeyboardKeyEventArgs = OpenTK.Input.KeyboardKeyEventArgs;
using MouseMoveEventArgs = OpenTK.Input.MouseMoveEventArgs;
using MouseWheelEventArgs = OpenTK.Input.MouseWheelEventArgs;

namespace EditorBase
{
	public partial class CamView : DockContent, IHelpProvider, IMouseInput, IKeyboardInput
	{
		[Flags]
		public enum AxisLock
		{
			None	= 0x0,

			X		= 0x1,
			Y		= 0x2,
			Z		= 0x4,

			All		= X | Y | Z
		}
		public enum CameraAction
		{
			None,
			MoveCam,
			TurnCam
		}
		public enum MouseAction
		{
			None,
			RectSelection,
			MoveObj,
			RotateObj,
			ScaleObj
		}

		public const float DefaultDisplayBoundRadius = 25.0f;


		private	int					runtimeId		= 0;
		private	GLControl			glControl		= null;
		private	GameObject			camObj			= null;
		private	Camera				camComp			= null;
		private	bool				camInternal		= false;
		private	Point				camActionBeginLoc		= Point.Empty;
		private Vector3				camActionBeginLocSpace	= Vector3.Zero;
		private	CameraAction		camAction			= CameraAction.None;
		private	Point				actionBeginLoc		= Point.Empty;
		private Vector3				actionBeginLocSpace	= Vector3.Zero;
		private	AxisLock			actionAxisLock		= AxisLock.None;
		private MouseAction			action				= MouseAction.None;
		private	MouseAction			mouseoverAction		= MouseAction.None;
		private	GameObject			mouseoverObject		= null;
		private	bool				mouseoverSelect		= false;
		private	Vector3				selectionCenter	= Vector3.Zero;
		private	float				selectionRadius	= 0.0f;
		private	List<GameObject>	parentFreeSel	= new List<GameObject>();
		private	ObjectSelection		activeRectSel	= new ObjectSelection();
		private	ColorPickerDialog	bgColorDialog	= new ColorPickerDialog();
		private	GameObject			nativeCamObj	= null;

		private	int		inputMouseX			= 0;
		private	int		inputMouseY			= 0;
		private	int		inputMouseWheel		= 0;
		private	int		inputMouseButtons	= 0;
		private	event	EventHandler<MouseButtonEventArgs>	inputMouseDown			= null;
		private	event	EventHandler<MouseButtonEventArgs>	inputMouseUp			= null;
		private	event	EventHandler<MouseMoveEventArgs>	inputMouseMove			= null;
		private	event	EventHandler<MouseWheelEventArgs>	inputMouseWheelChanged	= null;

		private	bool		inputKeyRepeat	= false;
		private	BitArray	inputKeyPressed	= new BitArray((int)Key.LastKey + 1, false);
		private	event		EventHandler<KeyboardKeyEventArgs>	inputKeyDown	= null;
		private	event		EventHandler<KeyboardKeyEventArgs>	inputKeyUp		= null;

		public ColorRgba BgColor
		{
			get { return this.camComp.ClearColor; }
			set { this.camComp.ClearColor = value; }
		}
		public ColorRgba FgColor
		{
			get { return this.BgColor.GetLuminance() < 0.5f ? ColorRgba.White : ColorRgba.Black; }
		}
		public float NearZ
		{
			get { return this.camComp. NearZ; }
			set { this.camComp. NearZ = value; }
		}
		public float FarZ
		{
			get { return this.camComp.FarZ; }
			set { this.camComp.FarZ = value; }
		}
		public float ParallaxRefDist
		{
			get { return this.camComp.ParallaxRefDist; }
			set { this.camComp.ParallaxRefDist = value; }
		}
		public CameraAction Action
		{
			get { return this.camAction; }
		}
		public GLControl MainContextControl
		{
			get { return EditorBasePlugin.Instance.EditorForm.MainContextControl; }
		}

		public CamView(int runtimeId)
		{
			this.InitializeComponent();
			this.bgColorDialog.OldColor = Color.Gray;
			this.bgColorDialog.SelectedColor = this.bgColorDialog.OldColor;
			this.bgColorDialog.AlphaEnabled = false;
			this.Text = PluginRes.EditorBaseRes.MenuItemName_CamView + " #" + runtimeId;
			this.runtimeId = runtimeId;
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.Init();
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			if (this.camObj != null && !this.camInternal) EditorBasePlugin.Instance.EditorForm.EditorObjects.UnregisterObjDeep(this.camObj);
			if (this.nativeCamObj != null) this.nativeCamObj.Dispose();

			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp -= this.EditorForm_AfterUpdateDualityApp;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			Scene.Leaving -= this.Scene_Changed;
			Scene.Entered -= this.Scene_Changed;
			Scene.GameObjectRegistered -= this.Scene_Changed;
			Scene.GameObjectUnregistered -= this.Scene_Changed;
			Scene.RegisteredObjectComponentAdded -= this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved -= this.Scene_Changed;
		}
		public void Init()
		{
			this.InitGLControl();
			this.InitNativeCamera();
			this.InitCameraSelector();
			this.camSelector.SelectedIndex = 0;

			// Register DualityApp updater for camera steering behaviour
			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp += this.EditorForm_AfterUpdateDualityApp;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
			Scene.Leaving += this.Scene_Leaving;
			Scene.Entered += this.Scene_Changed;
			Scene.GameObjectRegistered += this.Scene_Changed;
			Scene.GameObjectUnregistered += this.Scene_GameObjectUnregistered;
			Scene.RegisteredObjectComponentAdded += this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved += this.Scene_RegisteredObjectComponentRemoved;

			if (Scene.Current != null) this.Scene_Changed(this, null);

			// Update Camera values according to GUI (which carries loaded or default settings)
			this.parallaxRefDist_ValueChanged(this.parallaxRefDist, null);
			this.toggleParallaxity_CheckStateChanged(this.toggleParallaxity, null);
			this.bgColorDialog_ValueChanged(this.bgColorDialog, null);

			// Update purely displaying GUI elements
			this.UpdateStatusTransformInfo();
		}

		protected void InitCameraSelector()
		{
			this.camSelector.Items.Clear();
			this.camSelector.Items.Add(this.nativeCamObj);
			foreach (Camera c in Scene.Current.Graph.AllObjects.GetComponents<Camera>())
			{
				this.camSelector.Items.Add(c);
			}
		}
		protected void InitNativeCamera()
		{
			// Create internal Camera object
			this.nativeCamObj = new GameObject();
			this.nativeCamObj.Name = "CamView Camera " + this.runtimeId;
			this.nativeCamObj.AddComponent<Transform>();
			this.nativeCamObj.AddComponent<SoundListener>().MakeCurrent();

			Camera c = this.nativeCamObj.AddComponent<Camera>();
			c.ClearColor = ColorRgba.DarkGrey;
			c.FarZ = 100000.0f;

			this.nativeCamObj.Transform.Pos = new Vector3(0.0f, 0.0f, -c.ParallaxRefDist);
			EditorBasePlugin.Instance.EditorForm.EditorObjects.RegisterObjDeep(this.nativeCamObj);
		}
		protected void SetCurrentCamera(Camera c)
		{
			if (this.camObj != null && !this.camInternal)
				EditorBasePlugin.Instance.EditorForm.EditorObjects.UnregisterObjDeep(this.camObj);

			if (c == null)
			{
				this.camInternal = true;
				this.camObj = this.nativeCamObj;
				this.camComp = this.camObj.Camera;
				this.camSelector.SelectedIndex = 0;
			}
			else
			{
				this.camInternal = false;
				this.camObj = c.GameObj;
				this.camComp = c;
				EditorBasePlugin.Instance.EditorForm.EditorObjects.RegisterObjDeep(this.camObj);
				this.camSelector.SelectedIndex = this.camSelector.Items.IndexOf(c);
			}

			this.glControl.Invalidate();
		}

		internal void SaveUserData(System.Xml.XmlElement node)
		{
			node.SetAttribute("toggleParallaxity", this.toggleParallaxity.Checked.ToString());
			node.SetAttribute("parallaxRefDist", this.nativeCamObj.Camera.ParallaxRefDist.ToString());
			node.SetAttribute("bgColorArgb", this.nativeCamObj.Camera.ClearColor.ToIntArgb().ToString());
			node.SetAttribute("toggleAccMove", this.toggleAccMove.Checked.ToString());
		}
		internal void LoadUserData(System.Xml.XmlElement node)
		{
			bool tryParseBool;
			decimal tryParseDecimal;
			int tryParseInt;

			if (bool.TryParse(node.GetAttribute("toggleParallaxity"), out tryParseBool))
				this.toggleParallaxity.Checked = tryParseBool;
			if (decimal.TryParse(node.GetAttribute("parallaxRefDist"), out tryParseDecimal))
				this.parallaxRefDist.Value = tryParseDecimal;
			if (int.TryParse(node.GetAttribute("bgColorArgb"), out tryParseInt))
			{
				this.bgColorDialog.OldColor = Color.FromArgb(tryParseInt);
				this.bgColorDialog.SelectedColor = this.bgColorDialog.OldColor;
			}
			if (bool.TryParse(node.GetAttribute("toggleAccMove"), out tryParseBool))
				this.toggleAccMove.Checked = tryParseBool;
		}

		protected void InitGLControl()
		{
			this.SuspendLayout();

			// Get rid of a possibly existing old glControl
			if (this.glControl != null)
			{
				this.glControl.Dispose();
				this.Controls.Remove(this.glControl);
			}

			// Create a new glControl
			this.glControl = new GLControl(this.MainContextControl.GraphicsMode);
			this.glControl.BackColor = Color.Black;
			this.glControl.Dock = DockStyle.Fill;
			this.glControl.Name = "glControl";
			this.glControl.VSync = false;
			this.glControl.AllowDrop = true;
			this.glControl.Paint += new PaintEventHandler(this.glControl_Paint);
			this.glControl.MouseDown += new MouseEventHandler(this.glControl_MouseDown);
			this.glControl.MouseUp += new MouseEventHandler(this.glControl_MouseUp);
			this.glControl.MouseWheel += new MouseEventHandler(this.glControl_MouseWheel);
			this.glControl.MouseMove += new MouseEventHandler(this.glControl_MouseMove);
			this.glControl.LostFocus += new EventHandler(this.glControl_LostFocus);
			this.glControl.GotFocus += new EventHandler(this.glControl_GotFocus);
			this.glControl.PreviewKeyDown += new PreviewKeyDownEventHandler(glControl_PreviewKeyDown);
			this.glControl.KeyDown += new KeyEventHandler(this.glControl_KeyDown);
			this.glControl.KeyUp += new KeyEventHandler(this.glControl_KeyUp);
			this.glControl.Resize += new EventHandler(this.glControl_Resize);
			this.glControl.DragEnter += new DragEventHandler(this.glControl_DragEnter);
			this.glControl.DragLeave += new EventHandler(this.glControl_DragLeave);
			this.glControl.DragOver += new DragEventHandler(this.glControl_DragOver);
			this.glControl.DragDrop += new DragEventHandler(this.glControl_DragDrop);
			this.Controls.Add(this.glControl);
			this.Controls.SetChildIndex(this.glControl, 0);

			this.ResumeLayout(true);
		}

		protected void UpdateStatusTransformInfo()
		{
			if (!this.camInternal)
			{
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
					this, new ObjectSelection(this.camObj.Transform),
					ReflectionInfo.Property_Transform_RelativeVel,
					ReflectionInfo.Property_Transform_RelativeAngleVel,
					ReflectionInfo.Property_Transform_RelativeAngle,
					ReflectionInfo.Property_Transform_RelativePos);
			}

			System.Globalization.CultureInfo formatProvider = System.Threading.Thread.CurrentThread.CurrentUICulture;
			this.posXStatusLabel.Text = String.Format(formatProvider, PluginRes.EditorBaseRes.CamView_Status_PosX, this.camObj.Transform.Pos.X);
			this.posYStatusLabel.Text = String.Format(formatProvider, PluginRes.EditorBaseRes.CamView_Status_PosY, this.camObj.Transform.Pos.Y);
			this.posZStatusLabel.Text = String.Format(formatProvider, PluginRes.EditorBaseRes.CamView_Status_PosZ, this.camObj.Transform.Pos.Z);
			this.angleStatusLabel.Text = String.Format(formatProvider, PluginRes.EditorBaseRes.CamView_Status_Angle, MathF.RadToDeg(this.camObj.Transform.Angle));
		}
		protected void UpdateAxisLockInfo()
		{
			this.axisLockXLabel.Enabled = (this.actionAxisLock & AxisLock.X) != AxisLock.None;
			this.axisLockYLabel.Enabled = (this.actionAxisLock & AxisLock.Y) != AxisLock.None;
			this.axisLockZLabel.Enabled = (this.actionAxisLock & AxisLock.Z) != AxisLock.None;
		}

		protected void DrawViewSpaceLine(float x, float y, float x2, float y2, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			// Turn with camera: Calculate transform vectors
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(this.camObj.Transform.Angle, out catDotX, out catDotY);

			Vector3 lineV1 = new Vector3(
				x - this.glControl.Width / 2, 
				y - this.glControl.Height / 2, 
				0);
			Vector3 lineV2 = new Vector3(
				x2 - this.glControl.Width / 2, 
				y2 - this.glControl.Height / 2, 
				0);

			// Apply transform vectors
			MathF.TransformDotVec(ref lineV1, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref lineV2, ref catDotX, ref catDotY);

			this.camComp.DrawDevice.AddVertices(
				new BatchInfo(dt, clr), 
				BeginMode.Lines,
				new VertexP3(lineV1),
				new VertexP3(lineV2));
		}
		protected void DrawWorldSpaceSphere(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Vector3 pos = new Vector3(x, y, z);
			if (!this.camComp.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			Vector3 posTemp = pos;
			this.camComp.DrawDevice.PreprocessCoords(ref posTemp, ref scale);

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r * scale, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				this.camComp.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			this.camComp.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);

			// XZ circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y;
				vertices[i].pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				this.camComp.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			this.camComp.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);

			// YZ circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X;
				vertices[i].pos.Y = pos.Y + (float)Math.Sin(angle) * r;
				vertices[i].pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				this.camComp.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			this.camComp.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);
		}
		protected void DrawWorldSpaceCircle(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Vector3 pos = new Vector3(x, y, z);
			if (!this.camComp.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			this.camComp.DrawDevice.PreprocessCoords(ref pos, ref scale);
			r *= scale;

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			this.camComp.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);
		}
		protected void DrawWorldSpaceDot(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Vector3 pos = new Vector3(x, y, z);
			if (!this.camComp.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			this.camComp.DrawDevice.PreprocessCoords(ref pos, ref scale);
			r *= scale;

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle (filled)
			vertices = new VertexP3[segmentNum + 2];
			angle = 0.0f;
			vertices[0].pos = pos;
			for (int i = 1; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			this.camComp.DrawDevice.AddVertices(info, BeginMode.TriangleFan, vertices);
		}
		protected void DrawWorldSpaceLine(float x, float y, float z, float x2, float y2, float z2, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Vector3 pos = new Vector3(x, y, z);
			Vector3 target = new Vector3(x2, y2, z2);
			float scale = 1.0f;

			BatchInfo info = new BatchInfo(dt, clr);
			VertexP3[] vertices = new VertexP3[2];

			vertices[0].pos = pos;
			vertices[1].pos = target;
			this.camComp.DrawDevice.PreprocessCoords(ref vertices[0].pos, ref scale);
			this.camComp.DrawDevice.PreprocessCoords(ref vertices[1].pos, ref scale);

			this.camComp.DrawDevice.AddVertices(info, BeginMode.Lines, vertices);
		}
		protected void DrawSelectionMarkers(IEnumerable<GameObject> obj, ColorRgba clr)
		{
			// Determine turned Camera axes for angle-independent drawing
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(this.camObj.Transform.Angle, out catDotX, out catDotY);
			Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
			Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
			MathF.TransformDotVec(ref right, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref down, ref catDotX, ref catDotY);

			foreach (GameObject selObj in obj)
			{
				if (selObj.Transform == null) continue;

				Vector3 posTemp = selObj.Transform.Pos;
				float scaleTemp = 1.0f;
				float radTemp = 0.0f;

				Renderer selObjRenderer = selObj.Renderer;
				if (selObjRenderer != null)		radTemp = selObjRenderer.BoundRadius;
				else							radTemp = DefaultDisplayBoundRadius;

				if (!this.camComp.DrawDevice.IsCoordInView(posTemp, radTemp)) continue;
				this.camComp.DrawDevice.PreprocessCoords(ref posTemp, ref scaleTemp);
				posTemp.Z = 0.0f;

				// Draw selection marker
				this.camComp.DrawDevice.AddVertices(new BatchInfo(DrawTechnique.Solid, clr), BeginMode.Lines,
					new VertexP3(posTemp - right * 10.0f),
					new VertexP3(posTemp + right * 10.0f),
					new VertexP3(posTemp - down * 10.0f),
					new VertexP3(posTemp + down * 10.0f));

				// Draw angle marker
				this.camComp.DrawDevice.AddVertices(new BatchInfo(DrawTechnique.Solid, clr), BeginMode.Lines,
					new VertexP3(posTemp),
					new VertexP3(posTemp + 
						radTemp * scaleTemp * right * MathF.Sin(selObj.Transform.Angle - this.camObj.Transform.Angle) - 
						radTemp * scaleTemp * down * MathF.Cos(selObj.Transform.Angle - this.camObj.Transform.Angle)));

				// Draw boundary
				if (radTemp > 0.0f)
				{
					this.DrawWorldSpaceCircle(
						selObj.Transform.Pos.X,
						selObj.Transform.Pos.Y,
						selObj.Transform.Pos.Z,
						radTemp,
						DrawTechnique.Solid,
						clr);
				}
			}
		}

		protected void MakeDualityTarget()
		{
			DualityApp.TargetMode = this.MainContextControl.Context.GraphicsMode;
			DualityApp.TargetResolution = new OpenTK.Vector2(this.glControl.Width, this.glControl.Height);
			EditorBasePlugin.Instance.EditorForm.SetCurrentDualityAppInput(this, this);
		}
		protected Renderer PickRendererAt(int x, int y)
		{
			x = MathF.Clamp(x, 0, this.glControl.Width - 1);
			y = MathF.Clamp(y, 0, this.glControl.Height - 1);

			this.MainContextControl.Context.MakeCurrent(this.glControl.WindowInfo);
			this.MakeDualityTarget();
			return this.camComp.PickRendererAt(x, y);
		}
		protected HashSet<Renderer> PickRenderersIn(int x, int y, int w, int h)
		{
			x = MathF.Clamp(x, 0, this.glControl.Width - 1);
			y = MathF.Clamp(y, 0, this.glControl.Height - 1);
			w = MathF.Clamp(w, 1, this.glControl.Width - x);
			h = MathF.Clamp(h, 1, this.glControl.Height - y);

			this.MainContextControl.Context.MakeCurrent(this.glControl.WindowInfo);
			this.MakeDualityTarget();
			return this.camComp.PickRenderersIn(x, y, w, h);
		}
		protected float GetScaleAtZ(float z)
		{
			this.MakeDualityTarget();
			return this.camComp.GetScaleAtZ(z);
		}
		protected Vector3 GetSpaceCoord(Vector3 screenCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetSpaceCoord(screenCoord);
		}
		protected Vector3 GetSpaceCoord(Vector2 screenCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetSpaceCoord(screenCoord);
		}
		protected Vector3 GetScreenCoord(Vector3 spaceCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetScreenCoord(spaceCoord);
		}

		protected void SelectGameObj(ObjectSelection sel, MainForm.SelectMode mode = MainForm.SelectMode.Set)
		{
			if (mode == MainForm.SelectMode.Set) EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Component);
			EditorBasePlugin.Instance.EditorForm.Select(this, sel, mode);
		}
		protected void ClearSelection()
		{
			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObject);
		}
		protected IEnumerable<GameObject> SelectedGameObj()
		{
			return EditorBasePlugin.Instance.EditorForm.Selection.GameObjects;
		}
		protected IEnumerable<GameObject> SelectedGameObjIndirect()
		{
			var indirectViaCmpQuery = EditorBasePlugin.Instance.EditorForm.Selection.Components.GameObject();
			var indirectViaChildQuery = this.SelectedGameObj().ChildrenDeep();
			var indirectQuery = indirectViaCmpQuery.Concat(indirectViaChildQuery).Except(this.SelectedGameObj()).Distinct();
			foreach (GameObject o in indirectQuery) yield return o;

			if (this.mouseoverObject != null && 
				(this.mouseoverAction == MouseAction.RectSelection || this.mouseoverSelect) &&
				!this.SelectedGameObj().Contains(this.mouseoverObject)) 
				yield return this.mouseoverObject;
		}

		protected bool IsInSelection(Point mouseLoc)
		{
			// Check which renderer is picked
			Renderer picked = this.PickRendererAt(mouseLoc.X, mouseLoc.Y);
			if (picked == null) return false;

			return this.SelectedGameObj().Contains(picked.GameObj);
		}
		protected void UpdateMouseover(Point mouseLoc)
		{
			bool lastMouseoverSelect = this.mouseoverSelect;
			GameObject lastMouseoverObject = this.mouseoverObject;

			// Determine object at mouse position
			Renderer mouseoverRenderer = this.PickRendererAt(mouseLoc.X, mouseLoc.Y);
			this.mouseoverObject = mouseoverRenderer != null ? mouseoverRenderer.GameObj : null;

			// Determine action variables
			Vector3 mouseSpaceCoord = this.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float scale = this.GetScaleAtZ(this.selectionCenter.Z);
			float boundaryThickness = MathF.Max(10.0f, 5.0f / scale);
			bool mouseOverBoundary = MathF.Abs((mouseSpaceCoord - this.selectionCenter).Length - this.selectionRadius) < boundaryThickness;
			bool mouseInsideBoundary = !mouseOverBoundary && (mouseSpaceCoord - this.selectionCenter).Length < this.selectionRadius;
			bool mouseAtCenterAxis = MathF.Abs(mouseSpaceCoord.X - this.selectionCenter.X) < boundaryThickness || MathF.Abs(mouseSpaceCoord.Y - this.selectionCenter.Y) < boundaryThickness;
			bool shift = (Control.ModifierKeys & Keys.Shift) != Keys.None;
			bool ctrl = (Control.ModifierKeys & Keys.Control) != Keys.None;
			bool anySelection = this.SelectedGameObj().Any();

			// Select which action to propose
			this.mouseoverSelect = false;
			if (shift || ctrl)
				this.mouseoverAction = MouseAction.RectSelection;
			else if (anySelection && mouseOverBoundary && mouseAtCenterAxis && this.selectionRadius > 0.0f)
				this.mouseoverAction = MouseAction.ScaleObj;
			else if (anySelection && mouseOverBoundary)
				this.mouseoverAction = MouseAction.RotateObj;
			else if (anySelection && mouseInsideBoundary)
				this.mouseoverAction = MouseAction.MoveObj;
			else if (this.mouseoverObject != null)
			{
				this.mouseoverAction = MouseAction.MoveObj; 
				this.mouseoverSelect = true;
			}
			else
				this.mouseoverAction = MouseAction.RectSelection;

			// Adjust mouse cursor based on proposed action
			if (this.mouseoverAction == MouseAction.MoveObj)
				this.glControl.Cursor = CursorHelper.ArrowActionMove;
			else if (this.mouseoverAction == MouseAction.RotateObj)
				this.glControl.Cursor = CursorHelper.ArrowActionRotate;
			else if (this.mouseoverAction == MouseAction.ScaleObj)
				this.glControl.Cursor = CursorHelper.ArrowActionScale;
			else
				this.glControl.Cursor = CursorHelper.Arrow;

			// Redraw if mouseover changed
			if (this.mouseoverObject != lastMouseoverObject || 
				this.mouseoverSelect != lastMouseoverSelect)
				this.glControl.Invalidate();
		}
		protected void BeginAction(MouseAction action, Point mouseLoc)
		{
			this.actionBeginLoc = mouseLoc;
			this.action = action;

			if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				Time.Freeze();

			// Begin movement
			if (this.action == MouseAction.MoveObj)
			{
				this.actionBeginLocSpace = this.camComp.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
				this.actionBeginLocSpace.Z = this.camObj.Transform.Pos.Z;
			}
			// Begin rotation
			else if (this.action == MouseAction.RotateObj)
			{
				this.actionBeginLocSpace = this.camComp.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin scale
			else if (this.action == MouseAction.ScaleObj)
			{
				this.actionBeginLocSpace = this.camComp.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin rect selection
			else if (this.action == MouseAction.RectSelection)
			{
				this.actionBeginLocSpace = this.camComp.GetSpaceCoord(new Vector2(mouseLoc.X, mouseLoc.Y));
			}
		}
		protected void EndAction(Point mouseLoc)
		{
			if (this.action == MouseAction.RectSelection)
			{
				this.activeRectSel = new ObjectSelection();
			}

			if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				Time.Resume();

			this.action = MouseAction.None;
		}
		protected Vector3 LockAxis(Vector3 vec, AxisLock lockAxes, float lockedVal = 0.0f)
		{
			if (lockAxes == AxisLock.None) return vec;
			if ((lockAxes & AxisLock.X) == AxisLock.None) vec.X = lockedVal;
			if ((lockAxes & AxisLock.Y) == AxisLock.None) vec.Y = lockedVal;
			if ((lockAxes & AxisLock.Z) == AxisLock.None) vec.Z = lockedVal;
			return vec;
		}
		protected void UpdateRectSelection(Point mouseLoc)
		{
			bool shift = (Control.ModifierKeys & Keys.Shift) != Keys.None;
			bool ctrl = (Control.ModifierKeys & Keys.Control) != Keys.None;

			// Determine picked rect
			int pX = Math.Max(Math.Min(mouseLoc.X, this.actionBeginLoc.X), 0);
			int pY = Math.Max(Math.Min(mouseLoc.Y, this.actionBeginLoc.Y), 0);
			int pX2 = Math.Max(mouseLoc.X, this.actionBeginLoc.X);
			int pY2 = Math.Max(mouseLoc.Y, this.actionBeginLoc.Y);
			int pW = Math.Max(pX2 - pX, 1);
			int pH = Math.Max(pY2 - pY, 1);

			// Check which renderers are picked
			HashSet<Renderer> picked = this.PickRenderersIn(pX, pY, pW, pH);

			// Store in internal rect selection
			ObjectSelection oldRectSel = this.activeRectSel;
			this.activeRectSel = new ObjectSelection(picked.GameObject());

			// Apply internal selection to actual editor selection
			if (shift || ctrl)
			{
				if (this.activeRectSel.ObjectCount > 0)
				{
					ObjectSelection added = (this.activeRectSel - oldRectSel) + (oldRectSel - this.activeRectSel);
					this.SelectGameObj(added, shift ? MainForm.SelectMode.Append : MainForm.SelectMode.Toggle);
				}
			}
			else if (this.activeRectSel.ObjectCount > 0)
				this.SelectGameObj(this.activeRectSel);
			else
				this.ClearSelection();

			this.glControl.Invalidate();
		}
		protected void UpdateObjMove(Point mouseLoc)
		{
			float zMovement = this.camObj.Transform.Pos.Z - this.actionBeginLocSpace.Z;
			Vector3 spaceCoord = this.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z + zMovement));
			Vector3 movement = spaceCoord - this.actionBeginLocSpace;
			movement.Z = zMovement;
			movement = this.LockAxis(movement, this.actionAxisLock);
			if (movement != Vector3.Zero)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					foreach (Transform t in this.parentFreeSel.Transform())
					{
						t.Pos += movement;
						t.Vel = Vector3.Zero;
						t.AngleVel = 0.0f;
					}
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeVel);
				}
				else
				{
					foreach (Transform t in this.parentFreeSel.Transform())
						t.Pos += movement;
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos);
				}
			}
			this.UpdateSelectionStats();
			this.actionBeginLocSpace = spaceCoord;
			this.actionBeginLocSpace.Z = this.camObj.Transform.Pos.Z;
			this.glControl.Invalidate();
		}
		protected void UpdateObjRotate(Point mouseLoc)
		{
			Vector3 spaceCoord = this.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, this.actionBeginLocSpace.X, this.actionBeginLocSpace.Y);
			float curAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, spaceCoord.X, spaceCoord.Y);
			float rotation = curAngle - lastAngle;
			if (rotation != 0.0f)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					foreach (Transform t in this.parentFreeSel.Transform())
					{
						Vector3 posRelCenter = t.Pos - this.selectionCenter;
						Vector3 posRelCenterTarget = posRelCenter;
						MathF.TransformCoord(ref posRelCenterTarget.X, ref posRelCenterTarget.Y, rotation);
						//posRelCenterTarget = this.LockAxis(posRelCenterTarget, this.actionAxisLock, 1.0f);

						t.Pos = this.selectionCenter + posRelCenterTarget;
						t.Angle += rotation;
						t.Vel = Vector3.Zero;
						t.AngleVel = 0.0f;
					}
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeVel,
						ReflectionInfo.Property_Transform_RelativeAngle,
						ReflectionInfo.Property_Transform_RelativeAngleVel);
				}
				else
				{
					foreach (Transform t in this.parentFreeSel.Transform())
					{
						Vector3 posRelCenter = t.Pos - this.selectionCenter;
						Vector3 posRelCenterTarget = posRelCenter;
						MathF.TransformCoord(ref posRelCenterTarget.X, ref posRelCenterTarget.Y, rotation);
						//posRelCenterTarget = this.LockAxis(posRelCenterTarget, this.actionAxisLock, 1.0f);

						t.Pos = this.selectionCenter + posRelCenterTarget;
						t.Angle += rotation;
					}
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeAngle);
				}
			}
			this.UpdateSelectionStats();
			this.actionBeginLocSpace = spaceCoord;
			this.glControl.Invalidate();
		}
		protected void UpdateObjScale(Point mouseLoc)
		{
			if (this.selectionRadius == 0.0f) return;

			Vector3 spaceCoord = this.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastRadius = this.selectionRadius;
			float curRadius = (this.selectionCenter - spaceCoord).Length;
			float scale = MathF.Clamp(curRadius / lastRadius, 0.0001f, 10000.0f);
			if (scale != 1.0f)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					foreach (Transform t in this.parentFreeSel.Transform())
					{
						Vector3 scaleVec = new Vector3(scale, scale, scale);
						//scaleVec = this.LockAxis(scaleVec, this.actionAxisLock, 1.0f);
						Vector3 posRelCenter = t.Pos - this.selectionCenter;
						Vector3 posRelCenterTarget;
						Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

						t.Pos = this.selectionCenter + posRelCenterTarget;
						t.Scale = Vector3.Multiply(t.Scale, scaleVec);
						t.Vel = Vector3.Zero;
						t.AngleVel = 0.0f;
					}
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeVel,
						ReflectionInfo.Property_Transform_RelativeScale,
						ReflectionInfo.Property_Transform_RelativeAngleVel);
				}
				else
				{
					foreach (Transform t in this.parentFreeSel.Transform())
					{
						Vector3 scaleVec = new Vector3(scale, scale, scale);
						//scaleVec = this.LockAxis(scaleVec, this.actionAxisLock, 1.0f);
						Vector3 posRelCenter = t.Pos - this.selectionCenter;
						Vector3 posRelCenterTarget;
						Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

						t.Pos = this.selectionCenter + posRelCenterTarget;
						t.Scale = Vector3.Multiply(t.Scale, scaleVec);
					}
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(this.parentFreeSel.Transform()),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeScale);
				}
			}
			this.UpdateSelectionStats();
			this.actionBeginLocSpace = spaceCoord;
			this.glControl.Invalidate();
		}
		protected void UpdateSelectionStats()
		{
			this.selectionCenter = Vector3.Zero;
			this.selectionRadius = 0.0f;

			List<Transform> selTransform = new List<Transform>(this.SelectedGameObj().Transform());
			foreach (Transform t in selTransform) this.selectionCenter += t.Pos;
			this.selectionCenter /= selTransform.Count;

			float boundRad;
			foreach (Transform t in selTransform)
			{
				Renderer r = t.GameObj.Renderer;
				if (r != null) boundRad = r.BoundRadius;
				else boundRad = DefaultDisplayBoundRadius;

				this.selectionRadius = MathF.Max(this.selectionRadius, boundRad + (t.Pos - this.selectionCenter).Length);
			}
		}

		protected void DeleteObjects(IEnumerable<GameObject> obj)
		{
			var objList = new List<GameObject>(obj);
			if (objList.Count == 0) return;

			// Ask user if he really wants to delete stuff
			if (!this.DisplayConfirmDeleteSelectedObjects()) return;
			if (!EditorBasePlugin.Instance.EditorForm.ConfirmBreakPrefabLink(new ObjectSelection(obj))) return;

			// Delete objects
			foreach (GameObject o in objList)
			{ 
				if (o.Disposed) continue;
				o.Dispose(); 
				Scene.Current.Graph.UnregisterObjDeep(o); 
			}
		}
		protected List<GameObject> CloneObjects(IEnumerable<GameObject> obj)
		{
			List<GameObject> clones = new List<GameObject>();
			foreach (GameObject o in obj)
			{ 
				if (o.Disposed) continue;
				GameObject clone = o.Clone();
				Scene.Current.Graph.RegisterObjDeep(clone); 
				clones.Add(clone);
			}
			return clones;
		}
		protected bool DisplayConfirmDeleteSelectedObjects()
		{
			if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing) return true;
			DialogResult result = MessageBox.Show(
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Text, 
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}

		private void glControl_MouseDown(object sender, MouseEventArgs e)
		{
			MouseButton inputButton = e.Button.ToOpenTKSingle();
			this.inputMouseButtons |= e.Button.ToOpenTK();
			if (this.inputMouseDown != null) this.inputMouseDown(this, new MouseButtonEventArgs(e.X, e.Y, inputButton, true));

			if (this.camAction == CameraAction.None)
			{
				this.camActionBeginLoc = e.Location;
				if (e.Button == MouseButtons.Middle)
				{
					this.camAction = CameraAction.MoveCam;
					this.camActionBeginLocSpace = this.camObj.Transform.RelativePos;
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.camAction = CameraAction.TurnCam;
					this.camActionBeginLocSpace = new Vector3(this.camObj.Transform.RelativeAngle, 0.0f, 0.0f);
				}
			}

			if (this.action == MouseAction.None)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (this.mouseoverSelect)
					{
						// To interact with an object that isn't selected yet: Select it.
						if (!this.SelectedGameObj().Contains(this.mouseoverObject))
							this.SelectGameObj(new ObjectSelection(this.mouseoverObject));
					}
					this.BeginAction(this.mouseoverAction, e.Location);
				}
			}
		}
		private void glControl_MouseUp(object sender, MouseEventArgs e)
		{
			MouseButton inputButton = e.Button.ToOpenTKSingle();
			this.inputMouseButtons &= ~e.Button.ToOpenTK();
			if (this.inputMouseUp != null) this.inputMouseUp(this, new MouseButtonEventArgs(e.X, e.Y, inputButton, false));

			if (this.action == MouseAction.RectSelection && this.actionBeginLoc == e.Location)
				this.UpdateRectSelection(e.Location);

			if (this.camAction == CameraAction.MoveCam && e.Button == MouseButtons.Middle)
				this.camAction = CameraAction.None;
			else if (this.camAction == CameraAction.TurnCam && e.Button == MouseButtons.Right)
				this.camAction = CameraAction.None;


			if (e.Button == MouseButtons.Left)
			{
				this.EndAction(e.Location);
			}

			this.glControl.Invalidate();
		}
		private void glControl_MouseWheel(object sender, MouseEventArgs e)
		{
			this.inputMouseWheel += e.Delta;
			if (this.inputMouseWheelChanged != null) this.inputMouseWheelChanged(this, new MouseWheelEventArgs(e.X, e.Y, this.inputMouseWheel, e.Delta));

			if (e.Delta != 0)
			{
				if (this.toggleParallaxity.Checked)
				{
					if (this.toggleAccMove.Checked)
					{
						float curVel = this.camObj.Transform.RelativeVel.Length * MathF.Sign(this.camObj.Transform.RelativeVel.Z);
						Vector2 curTemp = new Vector2(
							(e.X * 2.0f / this.glControl.Width) - 1.0f,
							(e.Y * 2.0f / this.glControl.Height) - 1.0f);
						MathF.TransformCoord(ref curTemp.X, ref curTemp.Y, this.camObj.Transform.RelativeAngle);

						if (MathF.Sign(e.Delta) == MathF.Sign(curVel))
							curVel *= 0.0125f * MathF.Abs(e.Delta);
						curVel += 0.075f * e.Delta;
						curVel = MathF.Sign(curVel) * MathF.Min(MathF.Abs(curVel), 500.0f);

						Vector3 movVec = new Vector3(
							MathF.Sign(e.Delta) * MathF.Sign(curTemp.X) * MathF.Pow(curTemp.X, 2.0f), 
							MathF.Sign(e.Delta) * MathF.Sign(curTemp.Y) * MathF.Pow(curTemp.Y, 2.0f), 
							1.0f);
						movVec.Normalize();
						this.camObj.Transform.RelativeVel = movVec * curVel;
					}
					else
					{
						this.camObj.Transform.Pos += new Vector3(0.0f, 0.0f, e.Delta * 5 / 12);
						this.glControl.Invalidate();
						this.UpdateStatusTransformInfo();
					}
				}
				else
				{
					this.parallaxRefDist.Value = 
						Math.Max(this.parallaxRefDist.Minimum, Math.Min(this.parallaxRefDist.Maximum,
						this.parallaxRefDist.Value + this.parallaxRefDist.Increment * e.Delta / 40));
				}
			}
		}
		private void glControl_MouseMove(object sender, MouseEventArgs e)
		{
			int lastX = this.inputMouseX;
			int lastY = this.inputMouseY;
			this.inputMouseX = e.X;
			this.inputMouseY = e.Y;
			if (this.inputMouseMove != null) this.inputMouseMove(this, new MouseMoveEventArgs(e.X, e.Y, e.X - lastX, e.Y - lastY));

			if (!this.toggleAccMove.Checked)
			{
				if (this.camAction == CameraAction.MoveCam)
				{
					Vector3 movVec = new Vector3(
						5.0f * (e.X - this.camActionBeginLoc.X),
						5.0f * (e.Y - this.camActionBeginLoc.Y),
						0.0f);
					MathF.TransformCoord(ref movVec.X, ref movVec.Y, this.camObj.Transform.RelativeAngle);
					this.camObj.Transform.RelativePos = this.camActionBeginLocSpace + movVec;
					this.glControl.Invalidate();
					this.UpdateStatusTransformInfo();
				}
				else if (this.camAction == CameraAction.TurnCam)
				{
					this.camObj.Transform.RelativeAngle = MathF.NormalizeAngle(this.camActionBeginLocSpace.X + 0.01f * (e.X - this.camActionBeginLoc.X));
					this.glControl.Invalidate();
					this.UpdateStatusTransformInfo();
				}
			}

			if (this.action == MouseAction.RectSelection)
				this.UpdateRectSelection(e.Location);
			else if (this.action == MouseAction.MoveObj)
				this.UpdateObjMove(e.Location);
			else if (this.action == MouseAction.RotateObj)
				this.UpdateObjRotate(e.Location);
			else if (this.action == MouseAction.ScaleObj)
				this.UpdateObjScale(e.Location);
			else
				this.UpdateMouseover(e.Location);
		}
		private void glControl_Paint(object sender, PaintEventArgs e)
		{
			if (this.DesignMode) return;
			Point cursorPos = this.glControl.PointToClient(Cursor.Position);

			// Retrieve OpenGL context
 			try { this.MainContextControl.Context.MakeCurrent(this.glControl.WindowInfo); } catch (Exception) { return; }
			this.MakeDualityTarget();

			// Determine turned camera axes
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(this.camObj.Transform.Angle, out catDotX, out catDotY);
			Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
			Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
			MathF.TransformDotVec(ref right, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref down, ref catDotX, ref catDotY);

			// Draw indirectly selected object overlay
			this.DrawSelectionMarkers(this.SelectedGameObjIndirect(), ColorRgba.Mix(this.FgColor, this.BgColor, 0.75f));

			// Draw selected object overlay
			List<GameObject> selObjList = new List<GameObject>(this.SelectedGameObj());
			this.DrawSelectionMarkers(selObjList, this.FgColor);

			// Draw overall selection boundary
			if (selObjList.Count > 1 && selObjList.Transform().Any())
			{
				float midZ = selObjList.Transform().Average(t => t.Pos.Z);
				float maxZDiff = selObjList.Transform().Max(t => MathF.Abs(t.Pos.Z - midZ));
				if (maxZDiff > 0.001f)
				{
					this.DrawWorldSpaceSphere(
						this.selectionCenter.X, 
						this.selectionCenter.Y, 
						this.selectionCenter.Z, 
						this.selectionRadius,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.FgColor, this.BgColor, 0.5f));
				}
				else
				{
					this.DrawWorldSpaceCircle(
						this.selectionCenter.X, 
						this.selectionCenter.Y, 
						this.selectionCenter.Z, 
						this.selectionRadius,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.FgColor, this.BgColor, 0.5f));
				}
			}

			// Draw scale action dots
			if (selObjList.Count > 0)
			{
				float dotR = 3.0f / this.GetScaleAtZ(this.selectionCenter.Z);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X + this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X - this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X, 
					this.selectionCenter.Y + this.selectionRadius, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X, 
					this.selectionCenter.Y - this.selectionRadius, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.FgColor);
			}

			// Draw action lock axes
			if (this.action == MouseAction.MoveObj)
			{
				if ((this.actionAxisLock & AxisLock.X) != AxisLock.None)
				{
					this.DrawWorldSpaceLine(
						this.selectionCenter.X - this.selectionRadius * 4,
						this.selectionCenter.Y,
						this.selectionCenter.Z,
						this.selectionCenter.X + this.selectionRadius * 4,
						this.selectionCenter.Y,
						this.selectionCenter.Z,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.FgColor, ColorRgba.Red, 0.5f));
				}
				if ((this.actionAxisLock & AxisLock.Y) != AxisLock.None)
				{
					this.DrawWorldSpaceLine(
						this.selectionCenter.X,
						this.selectionCenter.Y - this.selectionRadius * 4,
						this.selectionCenter.Z,
						this.selectionCenter.X,
						this.selectionCenter.Y + this.selectionRadius * 4,
						this.selectionCenter.Z,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.FgColor, ColorRgba.Green, 0.5f));
				}
				if ((this.actionAxisLock & AxisLock.Z) != AxisLock.None)
				{
					this.DrawWorldSpaceLine(
						this.selectionCenter.X,
						this.selectionCenter.Y,
						this.selectionCenter.Z - this.selectionRadius * 4,
						this.selectionCenter.X,
						this.selectionCenter.Y,
						this.selectionCenter.Z + this.selectionRadius * 4,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.FgColor, ColorRgba.Blue, 0.5f));
				}
			}

			// Draw camera movement indicators
			if (this.camAction == CameraAction.MoveCam)
				this.DrawViewSpaceLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, cursorPos.Y, DrawTechnique.Solid, this.FgColor);
			else if (this.camAction == CameraAction.TurnCam)
				this.DrawViewSpaceLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, this.camActionBeginLoc.Y, DrawTechnique.Solid, this.FgColor);

			// Draw rect selection
			if (this.action == MouseAction.RectSelection)
			{
				this.DrawViewSpaceLine(this.actionBeginLoc.X, this.actionBeginLoc.Y, cursorPos.X, this.actionBeginLoc.Y, DrawTechnique.Solid, this.FgColor);
				this.DrawViewSpaceLine(cursorPos.X, this.actionBeginLoc.Y, cursorPos.X, cursorPos.Y, DrawTechnique.Solid, this.FgColor);
				this.DrawViewSpaceLine(cursorPos.X, cursorPos.Y, this.actionBeginLoc.X, cursorPos.Y, DrawTechnique.Solid, this.FgColor);
				this.DrawViewSpaceLine(this.actionBeginLoc.X, cursorPos.Y, this.actionBeginLoc.X, this.actionBeginLoc.Y, DrawTechnique.Solid, this.FgColor);
			}

			// Render CamView
			try
			{
				this.camComp.Render();
			}
			catch (Exception exception)
			{
				Log.Editor.Write("An exception occured during CamView {1} rendering: {0}", Log.Exception(exception), this.camComp.ToString());
			}
			this.MainContextControl.SwapBuffers();
		}
		private void glControl_GotFocus(object sender, EventArgs e)
		{
			if (this.camObj.GetComponent<SoundListener>() != null)
				this.camObj.GetComponent<SoundListener>().MakeCurrent();
		}
		private void glControl_LostFocus(object sender, EventArgs e)
		{
			this.camAction = CameraAction.None;
			this.EndAction(this.PointToClient(Cursor.Position));
			this.glControl.Invalidate();
		}
		private void glControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Launcher) 
				e.IsInputKey = true;
		}
		private void glControl_KeyDown(object sender, KeyEventArgs e)
		{
			Key inputKey = e.KeyCode.ToOpenTKSingle();
			bool wasPressed = this.inputKeyPressed[(int)inputKey];
			this.inputKeyPressed = this.inputKeyPressed.Or(e.KeyCode.ToOpenTK());
			if (this.inputKeyDown != null)
			{
				if (this.inputKeyRepeat || !wasPressed)
					this.inputKeyDown(this, inputKey.ToEventArgs());
			}
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Launcher) return;


			if (e.KeyCode == Keys.Delete)
			{
				this.DeleteObjects(this.SelectedGameObj());
			}
			else if (e.KeyCode == Keys.C && (Control.ModifierKeys & Keys.Control) != Keys.None)
			{
				List<GameObject> cloneList = this.CloneObjects(this.SelectedGameObj());
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(cloneList));
			}
			else if (e.KeyCode == Keys.A)
			{
				this.toggleAccMove.Checked = !this.toggleAccMove.Checked;

				Point curPos = this.glControl.PointToClient(Cursor.Position);
				if (this.camAction == CameraAction.MoveCam)
				{
					Vector3 movVec = new Vector3(
						5.0f * (curPos.X - this.camActionBeginLoc.X),
						5.0f * (curPos.Y - this.camActionBeginLoc.Y),
						0.0f);
					MathF.TransformCoord(ref movVec.X, ref movVec.Y, this.camObj.Transform.RelativeAngle);
					this.camActionBeginLocSpace = this.camObj.Transform.RelativePos - movVec;
				}
				else if (this.camAction == CameraAction.TurnCam)
				{
					this.camActionBeginLocSpace = new Vector3(this.camObj.Transform.RelativeAngle - 0.01f * (curPos.X - this.camActionBeginLoc.X), 0.0f, 0.0f);
				}
			}
			else
			{
				bool axisLockChanged = false;
				if (e.KeyCode == Keys.X) { this.actionAxisLock ^= AxisLock.X; axisLockChanged = true; }
				if (e.KeyCode == Keys.Y) { this.actionAxisLock ^= AxisLock.Y; axisLockChanged = true; }
				if (e.KeyCode == Keys.Z) { this.actionAxisLock ^= AxisLock.Z; axisLockChanged = true; }

				if (axisLockChanged)
				{
					this.UpdateAxisLockInfo();
					this.glControl.Invalidate();
				}
			}
		}
		private void glControl_KeyUp(object sender, KeyEventArgs e)
		{
			Key inputKey = e.KeyCode.ToOpenTKSingle();
			this.inputKeyPressed = this.inputKeyPressed.And(e.KeyCode.ToOpenTK().Not());
			if (this.inputKeyUp != null) this.inputKeyUp(this, inputKey.ToEventArgs());
		}
		private void glControl_Resize(object sender, EventArgs e)
		{
			this.glControl.Invalidate();
		}
		private void glControl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.action == MouseAction.None) return;

			Point mouseLoc = this.glControl.PointToClient(new Point(e.X, e.Y));
			this.EndAction(mouseLoc);
		}
		private void glControl_DragOver(object sender, DragEventArgs e)
		{
			if (this.action == MouseAction.None) return;

			Point mouseLoc = this.glControl.PointToClient(new Point(e.X, e.Y));
			this.UpdateObjMove(mouseLoc);
		}
		private void glControl_DragLeave(object sender, EventArgs e)
		{
			if (this.action == MouseAction.None) return;
			
			Point mouseLoc = this.glControl.PointToClient(Cursor.Position);
			this.EndAction(mouseLoc);

			// Destroy temporarily instantiated objects
			foreach (GameObject obj in this.SelectedGameObj())
			{
				obj.Dispose();
				Scene.Current.Graph.UnregisterObjDeep(obj);
			}
			this.ClearSelection();
		}
		private void glControl_DragEnter(object sender, DragEventArgs e)
		{
			Point mouseLoc = this.glControl.PointToClient(new Point(e.X, e.Y));
			Vector3 spaceCoord = this.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.camObj.Transform.Pos.Z + this.camComp.ParallaxRefDist));

			e.Effect = DragDropEffects.None;

			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				if (data.ContainsContentRefs<Prefab>())
				{
					ContentRef<Prefab>[] dropdata = data.GetContentRefs<Prefab>();

					// Instantiate Prefabs
					List<GameObject> dragObj = new List<GameObject>();
					foreach (ContentRef<Prefab> pRef in dropdata)
					{
						GameObject newObj = pRef.Res.Instantiate();
						if (newObj.Transform != null)
						{
							newObj.Transform.Pos = spaceCoord;
							newObj.Transform.Angle += this.camObj.Transform.Angle;
						}
						Scene.Current.Graph.RegisterObjDeep(newObj);
						dragObj.Add(newObj);
					}

					// Select them & begin action
					this.SelectGameObj(new ObjectSelection(dragObj));
					this.BeginAction(MouseAction.MoveObj, mouseLoc);

					// Get focused
					this.glControl.Focus();

					e.Effect = e.AllowedEffect;
				}
			}
		}

		private void toggleParallaxity_CheckStateChanged(object sender, EventArgs e)
		{
			if (this.camComp == null) return;

			this.camComp.ParallaxRefDist = this.toggleParallaxity.Checked ? (float)this.parallaxRefDist.Value : -(float)this.parallaxRefDist.Value;
			if (!this.camInternal)
			{
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
					this, new ObjectSelection(this.camComp),
					ReflectionInfo.Property_Camera_ParallaxRefDist);
			}
			this.glControl.Invalidate();
		}
		private void parallaxRefDist_ValueChanged(object sender, EventArgs e)
		{
			if (this.camComp == null) return;

			if (this.parallaxRefDist.Value < 30m)
				this.parallaxRefDist.Increment = 1m;
			else if (this.parallaxRefDist.Value < 150m)
				this.parallaxRefDist.Increment = 5m;
			else if (this.parallaxRefDist.Value < 300m)
				this.parallaxRefDist.Increment = 10m;
			else if (this.parallaxRefDist.Value < 1500m)
				this.parallaxRefDist.Increment = 50m;
			else if (this.parallaxRefDist.Value < 3000m)
				this.parallaxRefDist.Increment = 100m;
			else if (this.parallaxRefDist.Value < 15000m)
				this.parallaxRefDist.Increment = 500m;
			else if (this.parallaxRefDist.Value < 30000m)
				this.parallaxRefDist.Increment = 1000m;
			else if (this.parallaxRefDist.Value < 150000m)
				this.parallaxRefDist.Increment = 5000m;
			else
				this.parallaxRefDist.Increment = 10000m;


			this.camComp.ParallaxRefDist = this.toggleParallaxity.Checked ? (float)this.parallaxRefDist.Value : -(float)this.parallaxRefDist.Value;
			
			Point mouseLoc = this.glControl.PointToClient(Cursor.Position);
			if (this.action == MouseAction.MoveObj)
				this.UpdateObjMove(mouseLoc);
			else if (this.action == MouseAction.RotateObj)
				this.UpdateObjRotate(mouseLoc);
			else if (this.action == MouseAction.ScaleObj)
				this.UpdateObjScale(mouseLoc);
			this.glControl.Invalidate();
		}
		private void showBgColorDialog_Click(object sender, EventArgs e)
		{
			this.bgColorDialog.OldColor = Color.FromArgb(
				255,
				this.camComp.ClearColor.r,
				this.camComp.ClearColor.g,
				this.camComp.ClearColor.b);
			this.bgColorDialog.PrimaryAttribute = ColorPickerDialog.PrimaryAttrib.Hue;
			if (this.bgColorDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				this.bgColorDialog_ValueChanged(this.bgColorDialog, null);
		}
		private void bgColorDialog_ValueChanged(object sender, EventArgs e)
		{
			this.camComp.ClearColor = new ColorRgba(
				this.bgColorDialog.SelectedColor.R,
				this.bgColorDialog.SelectedColor.G,
				this.bgColorDialog.SelectedColor.B,
				0);
			if (!this.camInternal)
			{
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
					this, new ObjectSelection(this.camComp),
					ReflectionInfo.Property_Camera_ClearColor);
			}
			this.glControl.Invalidate();
		}
		
		private void EditorForm_AfterUpdateDualityApp(object sender, EventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Launcher)
			{
				this.glControl.Invalidate();
				this.UpdateSelectionStats();
			}

			bool transformChanged = false;
			Point cursorPos = this.glControl.PointToClient(Cursor.Position);

			if (this.toggleAccMove.Checked)
			{
				if (this.camAction == CameraAction.MoveCam)
				{
					Vector3 moveVec = new Vector3(
						0.125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f),
						0.125f * MathF.Sign(cursorPos.Y - this.camActionBeginLoc.Y) * MathF.Pow(MathF.Abs(cursorPos.Y - this.camActionBeginLoc.Y), 1.25f),
						this.camObj.Transform.RelativeVel.Z);

					MathF.TransformCoord(ref moveVec.X, ref moveVec.Y, this.camObj.Transform.Angle);
					this.camObj.Transform.RelativeVel = moveVec;

					transformChanged = true;
				}
				else if (this.camObj.Transform.RelativeVel.Length > 0.01f)
				{
					this.camObj.Transform.RelativeVel *= MathF.Pow(0.9f, Time.TimeMult);
					transformChanged = true;
				}
				else
					this.camObj.Transform.RelativeVel = Vector3.Zero;
			

				if (this.camAction == CameraAction.TurnCam)
				{
					float turnDir = 
						0.000125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * 
						MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f);
					this.camObj.Transform.RelativeAngleVel = turnDir;

					transformChanged = true;
				}
				else if (Math.Abs(this.camObj.Transform.RelativeAngleVel) > 0.001f)
				{
					this.camObj.Transform.RelativeAngleVel *= MathF.Pow(0.9f, Time.TimeMult);
					transformChanged = true;
				}
				else
					this.camObj.Transform.RelativeAngleVel = 0.0f;
			}
			else
			{
				this.camObj.Transform.RelativeVel = Vector3.Zero;
				this.camObj.Transform.RelativeAngleVel = 0.0f;
			}


			if (transformChanged)
			{
				if (this.action == MouseAction.RectSelection) this.UpdateRectSelection(cursorPos);
				else if (this.action == MouseAction.MoveObj) this.UpdateObjMove(cursorPos);
				else if (this.action == MouseAction.RotateObj) this.UpdateObjRotate(cursorPos);
				else if (this.action == MouseAction.ScaleObj) this.UpdateObjScale(cursorPos);
				else this.UpdateMouseover(cursorPos);

				this.UpdateStatusTransformInfo();

				this.glControl.Invalidate();
			}
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & (ObjectSelection.Category.GameObject | ObjectSelection.Category.Component)) == ObjectSelection.Category.None) return;

			// Remove removed objects
			foreach (GameObject o in e.Removed.GameObjects) this.parentFreeSel.Remove(o);
			// Remove objects whichs parents are now added
			this.parentFreeSel.RemoveAll(t => e.Added.GameObjects.Any(o => t.IsChildOf(o)));
			// Add objects whichs parents are not located in the current selection
			this.parentFreeSel.AddRange(e.Added.GameObjects.Where(t => !this.SelectedGameObj().Any(o => t.IsChildOf(o))));

			this.UpdateSelectionStats();
			this.glControl.Invalidate();
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.HasProperty(ReflectionInfo.Property_GameObject_ActiveSingle) ||
				e.Objects.Components.Any(c => c is Transform || c is Renderer || c is Camera) ||
				e.Objects.Resources.Any())
			{
				this.UpdateSelectionStats();
				this.glControl.Invalidate();
			}
		}
		private void EditorForm_ResourceModified(object sender, ResourceEventArgs e)
		{
			if (!e.IsResource) return;
			this.glControl.Invalidate();
		}

		private void Scene_Changed(object sender, EventArgs e)
		{
			this.UpdateSelectionStats();
			this.glControl.Invalidate();
		}
		private void Scene_Leaving(object sender, EventArgs e)
		{
			this.Scene_Changed(sender, e);
			if (!this.camInternal) this.SetCurrentCamera(null);
		}
		private void Scene_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			this.Scene_Changed(sender, e);
			if (this.camComp == e.Component) this.SetCurrentCamera(null);
		}
		private void Scene_GameObjectUnregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			this.Scene_Changed(sender, e);
			if (this.camObj == e.Object) this.SetCurrentCamera(null);
		}

		private void camSelector_DropDown(object sender, EventArgs e)
		{
			this.InitCameraSelector();
		}
		private void camSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.camSelector.SelectedIndex == -1) { this.camSelector.SelectedIndex = 0; return; }
			this.SetCurrentCamera(this.camSelector.SelectedItem as Camera);
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			Point globalPos = this.PointToScreen(localPos);

			Point glLocalPos = this.glControl.PointToClient(globalPos);
			if (this.glControl.ClientRectangle.Contains(glLocalPos))
			{
				GameObject[] selObj = this.SelectedGameObj().ToArray();

				if (this.mouseoverObject != null && this.mouseoverSelect)
					result = HelpInfo.FromGameObject(this.mouseoverObject);
				else if (this.mouseoverAction != MouseAction.None && this.mouseoverAction != MouseAction.RectSelection && selObj.Contains(this.mouseoverObject))
					result = HelpInfo.FromGameObject(this.mouseoverObject);
				else if (this.mouseoverAction != MouseAction.None && this.mouseoverAction != MouseAction.RectSelection && selObj.Length == 1)
					result = HelpInfo.FromSelection(new ObjectSelection(selObj));
			}

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}

		int IMouseInput.X
		{
			get { return this.inputMouseX; }
		}
		int IMouseInput.Y
		{
			get { return this.inputMouseY; }
		}
		int IMouseInput.Wheel
		{
			get { return this.inputMouseWheel; }
		}
		bool IMouseInput.this[MouseButton btn]
		{
			get { return (this.inputMouseButtons & (1 << (int)btn)) != 0; }
		}
		event EventHandler<MouseButtonEventArgs> IMouseInput.ButtonUp
		{
			add { this.inputMouseUp += value; }
			remove { this.inputMouseUp -= value; }
		}
		event EventHandler<MouseButtonEventArgs> IMouseInput.ButtonDown
		{
			add { this.inputMouseDown += value; }
			remove { this.inputMouseDown -= value; }
		}
		event EventHandler<MouseMoveEventArgs> IMouseInput.Move
		{
			add { this.inputMouseMove += value; }
			remove { this.inputMouseMove -= value; }
		}
		event EventHandler<MouseWheelEventArgs> IMouseInput.WheelChanged
		{
			add { this.inputMouseWheelChanged += value; }
			remove { this.inputMouseWheelChanged -= value; }
		}

		bool IKeyboardInput.KeyRepeat
		{
			get { return this.inputKeyRepeat; }
			set { this.inputKeyRepeat = value; }
		}
		bool IKeyboardInput.this[Key key]
		{
			get { return this.inputKeyPressed[(int)key]; }
		}
		event EventHandler<KeyboardKeyEventArgs> IKeyboardInput.KeyUp
		{
			add { this.inputKeyUp += value; }
			remove { this.inputKeyUp -= value; }
		}
		event EventHandler<KeyboardKeyEventArgs> IKeyboardInput.KeyDown
		{
			add { this.inputKeyDown += value; }
			remove { this.inputKeyDown -= value; }
		}
	}
}
