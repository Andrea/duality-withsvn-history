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
		public class CameraChangedEventArgs : EventArgs
		{
			private	Camera	prevCam	= null;
			private	Camera	nextCam	= null;

			public Camera PreviousCamera
			{
				get { return this.prevCam; }
			}
			public Camera NextCamera
			{
				get { return this.nextCam; }
			}

			public CameraChangedEventArgs(Camera prev, Camera next)
			{
				this.prevCam = prev;
				this.nextCam = next;
			}
		}
		public class StateComboboxEntry
		{
			private Type stateType;
			private CamViewState state;

			public Type StateType
			{
				get { return this.stateType; }
			}

			public StateComboboxEntry(Type stateType)
			{
				this.stateType = stateType;
				this.state = stateType.CreateInstanceOf() as CamViewState;
			}

			public override string ToString()
			{
				return this.state.StateName;
			}
		}

		public const float DefaultDisplayBoundRadius = 25.0f;

		private	int					runtimeId		= 0;
		private	GLControl			glControl		= null;
		private	GameObject			camObj			= null;
		private	Camera				camComp			= null;
		private	bool				camInternal		= false;
		private	CamViewState		state			= null;
		private	ColorPickerDialog	bgColorDialog	= new ColorPickerDialog();
		private	GameObject			nativeCamObj	= null;
		private	string				loadTempState	= null;

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

		public event EventHandler ParallaxRefDistChanged	= null;
		public event EventHandler AccMovementChanged		= null;
		public event EventHandler<CameraChangedEventArgs> CurrentCameraChanged	= null;

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
			get { return this.camComp.NearZ; }
			set { this.camComp.NearZ = value; }
		}
		public float FarZ
		{
			get { return this.camComp.FarZ; }
			set { this.camComp.FarZ = value; }
		}
		public float ParallaxRefDist
		{
			get { return (float)this.parallaxRefDist.Value; }
			set { this.parallaxRefDist.Value = Math.Min(Math.Max((decimal)value, this.parallaxRefDist.Minimum), this.parallaxRefDist.Maximum); }
		}
		public float ParallaxRefDistIncrement
		{
			get { return (float)this.parallaxRefDist.Increment; }
		}
		public bool ParallaxActive
		{
			get { return this.toggleParallaxity.Checked; }
			set { this.toggleParallaxity.Checked = value; }
		}
		public bool AccMovement
		{
			get { return this.toggleAccMove.Checked; }
		}
		public Camera CameraComponent
		{
			get { return this.camComp; }
		}
		public GameObject CameraObj
		{
			get { return this.camObj; }
		}
		public GLControl LocalGLControl
		{
			get { return this.glControl; }
		}
		public GLControl MainContextControl
		{
			get { return EditorBasePlugin.Instance.EditorForm.MainContextControl; }
		}
		public ToolStripStatusLabel ToolLabelAxisX
		{
			get { return this.axisLockXLabel; }
		}
		public ToolStripStatusLabel ToolLabelAxisY
		{
			get { return this.axisLockYLabel; }
		}
		public ToolStripStatusLabel ToolLabelAxisZ
		{
			get { return this.axisLockZLabel; }
		}
		public ToolStrip ToolbarCamera
		{
			get { return this.toolbarCamera; }
		}
		public StatusStrip StatusbarCamera
		{
			get { return this.statusbarCamera; }
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

			if (this.camObj != null && !this.camInternal) EditorBasePlugin.Instance.EditorForm.EditorObjects.UnregisterObj(this.camObj);
			if (this.nativeCamObj != null) this.nativeCamObj.Dispose();

			EditorBasePlugin.Instance.EditorForm.ResourceModified -= this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			Scene.Leaving -= this.Scene_Leaving;
			Scene.GameObjectUnregistered -= this.Scene_GameObjectUnregistered;
			Scene.RegisteredObjectComponentRemoved -= this.Scene_RegisteredObjectComponentRemoved;

			this.SetCurrentState((CamViewState)null);
		}
		public void Init()
		{
			this.InitGLControl();
			this.InitNativeCamera();
			this.InitCameraSelector();
			this.InitStateSelector();
			this.SetCurrentCamera(null);

			// Initialize state
			Type stateType = ReflectionHelper.ResolveType(this.loadTempState, false);
			if (stateType == null) stateType = typeof(SceneEditorCamViewState);
			this.SetCurrentState(stateType);

			// Register DualityApp updater for camera steering behaviour
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			Scene.Leaving += this.Scene_Leaving;
			Scene.GameObjectUnregistered += this.Scene_GameObjectUnregistered;
			Scene.RegisteredObjectComponentRemoved += this.Scene_RegisteredObjectComponentRemoved;

			// Update Camera values according to GUI (which carries loaded or default settings)
			this.parallaxRefDist_ValueChanged(this.parallaxRefDist, null);
			this.toggleParallaxity_CheckStateChanged(this.toggleParallaxity, null);
			this.bgColorDialog_ValueChanged(this.bgColorDialog, null);

			// Update camera transform properties & GUI
			this.UpdateStatusTransformInfo();
		}

		protected void InitStateSelector()
		{
			this.stateSelector.Items.Clear();

			IEnumerable<Type> camViewStateTypeQuery = 
				from t in EditorBasePlugin.Instance.EditorForm.GetAvailDualityEditorTypes(typeof(CamViewState))
				where !t.IsAbstract
				select t;

			foreach (Type camViewState in camViewStateTypeQuery)
				this.stateSelector.Items.Add(new StateComboboxEntry(camViewState));
		}
		protected void InitCameraSelector()
		{
			this.camSelector.Items.Clear();
			this.camSelector.Items.Add(this.nativeCamObj.Camera);

			foreach (Camera c in Scene.Current.AllObjects.GetComponents<Camera>())
				this.camSelector.Items.Add(c);
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
			EditorBasePlugin.Instance.EditorForm.EditorObjects.RegisterObj(this.nativeCamObj);
		}
		public void SetCurrentCamera(Camera c)
		{
			if (c == null) c = this.nativeCamObj.Camera;
			if (c == this.camComp) return;

			Camera prev = this.camComp;
			if (this.camObj != null && !this.camInternal)
				EditorBasePlugin.Instance.EditorForm.EditorObjects.UnregisterObj(this.camObj);

			if (c.GameObj == this.nativeCamObj)
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
				EditorBasePlugin.Instance.EditorForm.EditorObjects.RegisterObj(this.camObj);
				this.camSelector.SelectedIndex = this.camSelector.Items.IndexOf(c);
			}

			this.OnCurrentCameraChanged(prev, this.camComp);
			this.glControl.Invalidate();
		}
		public void SetCurrentState(Type stateType)
		{
			if (!typeof(CamViewState).IsAssignableFrom(stateType)) return;

			CamViewState state = stateType.CreateInstanceOf() as CamViewState;
			state.View = this;

			this.SetCurrentState(state);
		}
		public void SetCurrentState(CamViewState state)
		{
			if (this.state == state) return;
			if (this.state != null) this.state.OnLeaveState();

			this.state = state;
			this.stateSelector.SelectedIndex = this.state != null ? this.stateSelector.Items.IndexOf(this.stateSelector.Items.Cast<StateComboboxEntry>().FirstOrDefault(e => e.StateType == this.state.GetType())) : -1;

			if (this.state != null) this.state.OnEnterState();
			this.glControl.Invalidate();
		}

		internal void SaveUserData(System.Xml.XmlElement node)
		{
			node.SetAttribute("toggleParallaxity", this.toggleParallaxity.Checked.ToString());
			node.SetAttribute("parallaxRefDist", this.nativeCamObj.Camera.ParallaxRefDist.ToString());
			node.SetAttribute("bgColorArgb", this.nativeCamObj.Camera.ClearColor.ToIntArgb().ToString());
			node.SetAttribute("toggleAccMove", this.toggleAccMove.Checked.ToString());

			if (this.state != null) 
				node.SetAttribute("state", this.state.GetType().GetTypeId());
		}
		internal void LoadUserData(System.Xml.XmlElement node)
		{
			bool tryParseBool;
			decimal tryParseDecimal;
			int tryParseInt;

			if (bool.TryParse(node.GetAttribute("toggleParallaxity"), out tryParseBool))
				this.toggleParallaxity.Checked = tryParseBool;
			if (decimal.TryParse(node.GetAttribute("parallaxRefDist"), out tryParseDecimal))
				this.parallaxRefDist.Value = Math.Abs(tryParseDecimal);
			if (int.TryParse(node.GetAttribute("bgColorArgb"), out tryParseInt))
			{
				this.bgColorDialog.OldColor = Color.FromArgb(tryParseInt);
				this.bgColorDialog.SelectedColor = this.bgColorDialog.OldColor;
			}
			if (bool.TryParse(node.GetAttribute("toggleAccMove"), out tryParseBool))
				this.toggleAccMove.Checked = tryParseBool;

			this.loadTempState = node.GetAttribute("state");
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
			this.glControl.MouseDown += new MouseEventHandler(this.glControl_MouseDown);
			this.glControl.MouseUp += new MouseEventHandler(this.glControl_MouseUp);
			this.glControl.MouseWheel += new MouseEventHandler(this.glControl_MouseWheel);
			this.glControl.MouseMove += new MouseEventHandler(this.glControl_MouseMove);
			this.glControl.GotFocus += new EventHandler(this.glControl_GotFocus);
			this.glControl.PreviewKeyDown += new PreviewKeyDownEventHandler(glControl_PreviewKeyDown);
			this.glControl.KeyDown += new KeyEventHandler(this.glControl_KeyDown);
			this.glControl.KeyUp += new KeyEventHandler(this.glControl_KeyUp);
			this.glControl.Resize += new EventHandler(this.glControl_Resize);
			this.Controls.Add(this.glControl);
			this.Controls.SetChildIndex(this.glControl, 0);

			this.ResumeLayout(true);
		}
		public void UpdateStatusTransformInfo()
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
		public void SetToolbarCamSettingsEnabled(bool value)
		{
			this.toggleAccMove.Enabled = value;
			this.toggleParallaxity.Enabled = value;
			this.parallaxRefDist.Enabled = value;
			this.camSelector.Enabled = value;
			this.showBgColorDialog.Enabled = value;
		}

		public void MakeDualityTarget()
		{
			DualityApp.TargetMode = this.MainContextControl.Context.GraphicsMode;
			DualityApp.TargetResolution = new OpenTK.Vector2(this.glControl.Width, this.glControl.Height);
			if (this.ContainsFocus) EditorBasePlugin.Instance.EditorForm.SetCurrentDualityAppInput(this, this);
		}
		public Renderer PickRendererAt(int x, int y)
		{
			x = MathF.Clamp(x, 0, this.glControl.Width - 1);
			y = MathF.Clamp(y, 0, this.glControl.Height - 1);

			this.MainContextControl.Context.MakeCurrent(this.glControl.WindowInfo);
			this.MakeDualityTarget();
			return this.camComp.PickRendererAt(x, y);
		}
		public HashSet<Renderer> PickRenderersIn(int x, int y, int w, int h)
		{
			x = MathF.Clamp(x, 0, this.glControl.Width - 1);
			y = MathF.Clamp(y, 0, this.glControl.Height - 1);
			w = MathF.Clamp(w, 1, this.glControl.Width - x);
			h = MathF.Clamp(h, 1, this.glControl.Height - y);

			this.MainContextControl.Context.MakeCurrent(this.glControl.WindowInfo);
			this.MakeDualityTarget();
			return this.camComp.PickRenderersIn(x, y, w, h);
		}
		public float GetScaleAtZ(float z)
		{
			this.MakeDualityTarget();
			return this.camComp.GetScaleAtZ(z);
		}
		public Vector3 GetSpaceCoord(Vector3 screenCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetSpaceCoord(screenCoord);
		}
		public Vector3 GetSpaceCoord(Vector2 screenCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetSpaceCoord(screenCoord);
		}
		public Vector3 GetScreenCoord(Vector3 spaceCoord)
		{
			this.MakeDualityTarget();
			return this.camComp.GetScreenCoord(spaceCoord);
		}

		private void OnParallaxRefDistChanged()
		{
			if (!this.camInternal)
			{
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
					this, new ObjectSelection(this.camComp),
					ReflectionInfo.Property_Camera_ParallaxRefDist);
			}
			this.glControl.Invalidate();

			if (this.ParallaxRefDistChanged != null)
				this.ParallaxRefDistChanged(this, EventArgs.Empty);
		}
		private void OnAccMovementChanged()
		{
			if (this.AccMovementChanged != null)
				this.AccMovementChanged(this, EventArgs.Empty);
		}
		private void OnCurrentCameraChanged(Camera prev, Camera next)
		{
			if (this.CurrentCameraChanged != null)
				this.CurrentCameraChanged(this, new CameraChangedEventArgs(prev, next));
		}

		private void glControl_MouseDown(object sender, MouseEventArgs e)
		{
			MouseButton inputButton = e.Button.ToOpenTKSingle();
			this.inputMouseButtons |= e.Button.ToOpenTK();
			if (this.inputMouseDown != null) this.inputMouseDown(this, new MouseButtonEventArgs(e.X, e.Y, inputButton, true));
		}
		private void glControl_MouseUp(object sender, MouseEventArgs e)
		{
			MouseButton inputButton = e.Button.ToOpenTKSingle();
			this.inputMouseButtons &= ~e.Button.ToOpenTK();
			if (this.inputMouseUp != null) this.inputMouseUp(this, new MouseButtonEventArgs(e.X, e.Y, inputButton, false));
		}
		private void glControl_MouseWheel(object sender, MouseEventArgs e)
		{
			this.inputMouseWheel += e.Delta;
			if (this.inputMouseWheelChanged != null) this.inputMouseWheelChanged(this, new MouseWheelEventArgs(e.X, e.Y, this.inputMouseWheel, e.Delta));
		}
		private void glControl_MouseMove(object sender, MouseEventArgs e)
		{
			int lastX = this.inputMouseX;
			int lastY = this.inputMouseY;
			this.inputMouseX = e.X;
			this.inputMouseY = e.Y;
			if (this.inputMouseMove != null) this.inputMouseMove(this, new MouseMoveEventArgs(e.X, e.Y, e.X - lastX, e.Y - lastY));
		}
		private void glControl_GotFocus(object sender, EventArgs e)
		{
			if (this.camObj.GetComponent<SoundListener>() != null)
				this.camObj.GetComponent<SoundListener>().MakeCurrent();
		}
		private void glControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game) 
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
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game) return;


			if (e.KeyCode == Keys.A) this.toggleAccMove.Checked = !this.toggleAccMove.Checked;
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

		private void toggleParallaxity_CheckStateChanged(object sender, EventArgs e)
		{
			if (this.camComp == null) return;

			this.camComp.ParallaxRefDist = this.toggleParallaxity.Checked ? (float)this.parallaxRefDist.Value : -(float)this.parallaxRefDist.Value;
			this.OnParallaxRefDistChanged();
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
			this.OnParallaxRefDistChanged();
		}
		private void toggleAccMove_CheckedChanged(object sender, EventArgs e)
		{
			this.OnAccMovementChanged();
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
		
		private void EditorForm_ResourceModified(object sender, ResourceEventArgs e)
		{
			if (!e.IsResource) return;
			this.glControl.Invalidate();
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Components.Contains(this.camComp) || e.Objects.GameObjects.Contains(this.camObj))
				this.UpdateStatusTransformInfo();
			this.glControl.Invalidate();
		}

		private void Scene_Leaving(object sender, EventArgs e)
		{
			if (!this.camInternal) this.SetCurrentCamera(null);
		}
		private void Scene_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (this.camComp == e.Component) this.SetCurrentCamera(null);
		}
		private void Scene_GameObjectUnregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			if (this.camObj == e.Object) this.SetCurrentCamera(null);
		}

		private void camSelector_DropDown(object sender, EventArgs e)
		{
			this.InitCameraSelector();
		}
		private void camSelector_DropDownClosed(object sender, EventArgs e)
		{
			if (this.camSelector.SelectedIndex == -1)
			{
				this.camSelector.SelectedIndex = this.camSelector.Items.IndexOf(this.camComp);
				return;
			}
			this.SetCurrentCamera(this.camSelector.SelectedItem as Camera);
		}
		private void stateSelector_DropDown(object sender, EventArgs e)
		{
			this.InitStateSelector();
		}
		private void stateSelector_DropDownClosed(object sender, EventArgs e)
		{
			if (this.stateSelector.SelectedIndex == -1)
			{
				this.stateSelector.SelectedIndex = this.state != null ? this.stateSelector.Items.IndexOf(this.stateSelector.Items.Cast<StateComboboxEntry>().FirstOrDefault(sce => sce.StateType == this.state.GetType())) : -1;
				return;
			}
			this.SetCurrentState(((StateComboboxEntry)this.stateSelector.SelectedItem).StateType);
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			Point globalPos = this.PointToScreen(localPos);

			Point glLocalPos = this.glControl.PointToClient(globalPos);
			if (this.glControl.ClientRectangle.Contains(glLocalPos))
			{
				if (this.state is IHelpProvider)
				{
					IHelpProvider stateHelper = this.state as IHelpProvider;
					result = stateHelper.ProvideHoverHelp(glLocalPos, ref captured);
				}
			}

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			if (this.state is IHelpProvider)
			{
				IHelpProvider stateHelper = this.state as IHelpProvider;
				return stateHelper.PerformHelpAction(info);
			}
			else
			{
				return this.DefaultPerformHelpAction(info);
			}
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
