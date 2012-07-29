using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Windows.Forms;

using Duality;
using Duality.Components;
using Duality.Resources;
using Duality.ColorFormat;
using Duality.VertexFormat;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase.CamViewStates
{
	public abstract class CamViewState
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
		public abstract class SelObj : IEquatable<SelObj>
		{
			public abstract object ActualObject { get; }
			public abstract bool HasTransform { get; }
			public abstract float BoundRadius { get; }
			public abstract Vector3 Pos { get; set; }
			public virtual Vector3 Scale
			{
				get { return Vector3.One; }
				set {}
			}
			public virtual float Angle
			{
				get { return 0.0f; }
				set {}
			}
			public virtual bool ShowBoundRadius
			{
				get { return true; }
			}
			public virtual bool ShowPos
			{
				get { return true; }
			}
			public virtual bool ShowAngle
			{
				get { return false; }
			}
			public bool IsInvalid
			{
				get { return this.ActualObject == null; }
			}

			public virtual bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.MoveObj) return true;
				return false;
			}
			public virtual void DrawActionGizmo(Canvas canvas, MouseAction action, Point beginLoc, Point curLoc) {}
			
			public override bool Equals(object obj)
			{
				if (obj is SelObj)
					return this == (SelObj)obj;
				else
					return base.Equals(obj);
			}
			public override int GetHashCode()
			{
				return this.ActualObject.GetHashCode();
			}
			public bool Equals(SelObj other)
			{
				return this == other;
			}

			public static bool operator ==(SelObj first, SelObj second)
			{
				if (object.ReferenceEquals(first, null))
				{
					if (object.ReferenceEquals(second, null)) return true;
					else return false;
				}
				else if (object.ReferenceEquals(second, null))
					return false;

				return first.ActualObject == second.ActualObject;
			}
			public static bool operator !=(SelObj first, SelObj second)
			{
				return !(first == second);
			}
		}


		private	CamView			view					= null;
		private	AxisLock		lockedAxes				= AxisLock.None;
		private Vector3			camVel					= Vector3.Zero;
		private	float			camAngleVel				= 0.0f;
		private	Point			camActionBeginLoc		= Point.Empty;
		private Vector3			camActionBeginLocSpace	= Vector3.Zero;
		private	CameraAction	camAction				= CameraAction.None;
		private	bool			camActionAllowed		= true;
		private	bool			camTransformChanged		= false;
		private	Camera.Pass		camPassBg			= null;
		private	Camera.Pass		camPassEdWorld		= null;
		private Camera.Pass		camPassEdScreen		= null;
		private	bool			actionAllowed		= true;
		private	Point			actionBeginLoc		= Point.Empty;
		private Vector3			actionBeginLocSpace	= Vector3.Zero;
		private MouseAction		action				= MouseAction.None;
		private	Vector3			selectionCenter		= Vector3.Zero;
		private	float			selectionRadius		= 0.0f;
		private	ObjectSelection	activeRectSel		= new ObjectSelection();
		private	MouseAction		mouseoverAction		= MouseAction.None;
		private	SelObj			mouseoverObject		= null;
		private	bool			mouseoverSelect		= false;
		private	List<Type>		lastActiveLayers	= new List<Type>();
		protected	List<SelObj>	actionObjSel	= new List<SelObj>();
		protected	List<SelObj>	allObjSel		= new List<SelObj>();
		protected	List<SelObj>	indirectObjSel	= new List<SelObj>();

		public CamView View
		{
			get { return this.view; }
			internal set { this.view = value; }
		}
		public AxisLock LockedAxes
		{
			get { return this.lockedAxes; }
		}
		public MouseAction SelObjAction
		{
			get { return this.action; }
		}
		public abstract string StateName { get; }

		public IEnumerable<SelObj> SelectedObjects
		{
			get { return this.allObjSel; }
		}
		public bool IsActive
		{
			get { return this.view != null && this.view.ViewState == this; }
		}
		public bool MouseActionAllowed
		{
			get { return this.actionAllowed; }
			protected set
			{
				this.actionAllowed = value;
				if (!this.actionAllowed)
				{
					this.mouseoverAction = MouseAction.None;
					this.mouseoverObject = null;
					this.mouseoverSelect = false;
					if (this.action != MouseAction.None)
					{
						this.EndAction();
						this.UpdateAction();
					}
				}
			}
		}
		public bool CameraActionAllowed
		{
			get { return this.camActionAllowed; }
			protected set
			{ 
				this.camActionAllowed = value;
				if (!this.camActionAllowed && this.camAction != CameraAction.None)
				{
					this.camAction = CameraAction.None;
					this.view.LocalGLControl.Invalidate();
				}
			}
		}
		protected bool CameraTransformChanged
		{
			get { return this.camTransformChanged; }
		}
		protected bool MouseoverSelect
		{
			get { return this.mouseoverSelect; }
		}
		protected SelObj MouseoverObject
		{
			get { return this.mouseoverObject; }
		}
		protected MouseAction MouseoverAction
		{
			get { return this.mouseoverAction; }
		}
		protected MouseAction Action
		{
			get { return this.action; }
		}

		internal protected virtual void OnEnterState()
		{
			this.RestoreActiveLayers();

			// Create re-usable render passes for editor gizmos
			this.camPassBg = new Camera.Pass();
			this.camPassBg.MatrixMode = RenderMatrix.OrthoScreen;
			this.camPassBg.ClearFlags = Camera.ClearFlags.None;
			this.camPassBg.VisibilityMask = VisibilityFlag.ScreenOverlay;
			this.camPassEdWorld = new Camera.Pass();
			this.camPassEdWorld.ClearFlags = Camera.ClearFlags.None;
			this.camPassEdWorld.VisibilityMask = VisibilityFlag.None;
			this.camPassEdScreen = new Camera.Pass();
			this.camPassEdScreen.MatrixMode = RenderMatrix.OrthoScreen;
			this.camPassEdScreen.ClearFlags = Camera.ClearFlags.None;
			this.camPassEdScreen.VisibilityMask = VisibilityFlag.ScreenOverlay;

			this.camPassBg.CollectDrawcalls			+= this.camPassBg_CollectDrawcalls;
			this.camPassEdWorld.CollectDrawcalls	+= this.camPassEdWorld_CollectDrawcalls;
			this.camPassEdScreen.CollectDrawcalls	+= this.camPassEdScreen_CollectDrawcalls;

			this.View.LocalGLControl.Paint		+= this.LocalGLControl_Paint;
			this.View.LocalGLControl.MouseDown	+= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	+= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	+= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel += this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.MouseLeave	+= this.LocalGLControl_MouseLeave;
			this.View.LocalGLControl.KeyDown	+= this.LocalGLControl_KeyDown;
			this.View.LocalGLControl.KeyUp		+= this.LocalGLControl_KeyUp;
			this.View.LocalGLControl.LostFocus	+= this.LocalGLControl_LostFocus;
			this.View.ParallaxRefDistChanged	+= this.View_ParallaxRefDistChanged;
			MainForm.Instance.AfterUpdateDualityApp += this.EditorForm_AfterUpdateDualityApp;
			MainForm.Instance.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;

			Scene.Leaving += this.Scene_Changed;
			Scene.Entered += this.Scene_Changed;
			Scene.GameObjectRegistered += this.Scene_Changed;
			Scene.GameObjectUnregistered += this.Scene_Changed;
			Scene.RegisteredObjectComponentAdded += this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved += this.Scene_Changed;

			if (Scene.Current != null) this.Scene_Changed(this, EventArgs.Empty);
		}
		internal protected virtual void OnLeaveState() 
		{
			this.View.LocalGLControl.Paint		-= this.LocalGLControl_Paint;
			this.View.LocalGLControl.MouseDown	-= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	-= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	-= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel -= this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.MouseLeave	-= this.LocalGLControl_MouseLeave;
			this.View.LocalGLControl.KeyDown	-= this.LocalGLControl_KeyDown;
			this.View.LocalGLControl.KeyUp		-= this.LocalGLControl_KeyUp;
			this.View.LocalGLControl.LostFocus	-= this.LocalGLControl_LostFocus;
			this.View.ParallaxRefDistChanged	-= this.View_ParallaxRefDistChanged;
			MainForm.Instance.AfterUpdateDualityApp -= this.EditorForm_AfterUpdateDualityApp;
			
			Scene.Leaving -= this.Scene_Changed;
			Scene.Entered -= this.Scene_Changed;
			Scene.GameObjectRegistered -= this.Scene_Changed;
			Scene.GameObjectUnregistered -= this.Scene_Changed;
			Scene.RegisteredObjectComponentAdded -= this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved -= this.Scene_Changed;

			this.SaveActiveLayers();
		}
		
		internal protected virtual void SaveUserData(XmlElement node)
		{
			if (this.IsActive) this.SaveActiveLayers();

			XmlElement activeLayersNode = node.OwnerDocument.CreateElement("activeLayers");
			foreach (Type t in this.lastActiveLayers)
			{
				XmlElement typeEntry = node.OwnerDocument.CreateElement(t.GetTypeId());
				activeLayersNode.AppendChild(typeEntry);
			}
			node.AppendChild(activeLayersNode);
		}
		internal protected virtual void LoadUserData(XmlElement node)
		{
			XmlElement activeLayersNode = node.ChildNodes.OfType<XmlElement>().FirstOrDefault(e => e.Name == "activeLayers");
			if (activeLayersNode != null)
			{
				this.lastActiveLayers.Clear();
				foreach (XmlElement layerNode in activeLayersNode.ChildNodes.OfType<XmlElement>())
				{
					Type layerType = ReflectionHelper.ResolveType(layerNode.Name, false);
					if (layerType != null) this.lastActiveLayers.Add(layerType);
				}
			}

			if (this.IsActive) this.RestoreActiveLayers();
		}

		protected virtual void OnCollectStateDrawcalls(Canvas canvas)
		{
			// Collect the views layer drawcalls
			this.CollectLayerDrawcalls(canvas);

			List<SelObj> transformObjSel = this.allObjSel.Where(s => s.HasTransform).ToList();
			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);
			canvas.PushState();
			
			// Draw indirectly selected object overlay
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.75f)));
			this.DrawSelectionMarkers(canvas, this.indirectObjSel);
			if (this.mouseoverObject != null && (this.mouseoverAction == MouseAction.RectSelection || this.mouseoverSelect) && !transformObjSel.Contains(this.mouseoverObject)) 
				this.DrawSelectionMarkers(canvas, new [] { this.mouseoverObject });

			// Draw selected object overlay
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, this.View.FgColor));
			this.DrawSelectionMarkers(canvas, transformObjSel);

			// Draw overall selection boundary
			if (transformObjSel.Count > 1)
			{
				float midZ = transformObjSel.Average(t => t.Pos.Z);
				float maxZDiff = transformObjSel.Max(t => MathF.Abs(t.Pos.Z - midZ));
				if (maxZDiff > 0.001f)
				{
					canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.5f)));
					canvas.DrawSphere(
						this.selectionCenter.X, 
						this.selectionCenter.Y, 
						this.selectionCenter.Z - 0.1f, 
						this.selectionRadius);
				}
				else
				{
					canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.5f)));
					canvas.DrawCircle(
						this.selectionCenter.X, 
						this.selectionCenter.Y, 
						this.selectionCenter.Z - 0.1f, 
						this.selectionRadius);
				}
			}

			// Draw scale action dots
			bool canMove = this.actionObjSel.Any(s => s.IsActionAvailable(MouseAction.MoveObj));
			bool canScale = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(MouseAction.ScaleObj));
			if (canScale)
			{
				float dotR = 3.0f / this.View.GetScaleAtZ(this.selectionCenter.Z);
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, this.View.FgColor));
				canvas.FillCircle(
					this.selectionCenter.X + this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z - 0.1f,
					dotR);
				canvas.FillCircle(
					this.selectionCenter.X - this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z - 0.1f,
					dotR);
				canvas.FillCircle(
					this.selectionCenter.X, 
					this.selectionCenter.Y + this.selectionRadius, 
					this.selectionCenter.Z - 0.1f,
					dotR);
				canvas.FillCircle(
					this.selectionCenter.X, 
					this.selectionCenter.Y - this.selectionRadius, 
					this.selectionCenter.Z - 0.1f,
					dotR);
			}

			// Draw action lock axes
			this.DrawLockedAxes(canvas, this.selectionCenter.X, this.selectionCenter.Y, this.selectionCenter.Z, this.selectionRadius * 4);

			canvas.PopState();
		}
		protected virtual void OnCollectStateOverlayDrawcalls(Canvas canvas)
		{
			// Collect the views overlay layer drawcalls
			this.CollectLayerOverlayDrawcalls(canvas);

			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);
			canvas.PushState();

			// Draw camera movement indicators
			if (this.camAction != CameraAction.None)
			{
				canvas.PushState();
				canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(0.5f);
				canvas.FillCircle(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, 3);
				canvas.PopState();
			}
			
			// Draw selected objects action gizmo
			if (this.action != MouseAction.None)
			{
				canvas.PushState();
				foreach (SelObj selShape in this.actionObjSel)
					selShape.DrawActionGizmo(canvas, this.Action, this.camActionBeginLoc, cursorPos);
				canvas.PopState();
			}

			// Draw current state text
			bool handled = false;
			this.DrawStatusText(canvas, ref handled);

			// Draw rect selection
			if (this.action == MouseAction.RectSelection)
				canvas.DrawRect(this.actionBeginLoc.X, this.actionBeginLoc.Y, cursorPos.X - this.actionBeginLoc.X, cursorPos.Y - this.actionBeginLoc.Y);

			#if DEBUG
			canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(0.35f);
			Performance.DrawAllMeasures(canvas);
			#endif

			canvas.PopState();
		}
		protected virtual void OnCollectStateBackgroundDrawcalls(Canvas canvas)
		{
			// Collect the views overlay layer drawcalls
			this.CollectLayerBackgroundDrawcalls(canvas);
		}
		protected virtual void DrawStatusText(Canvas canvas, ref bool handled)
		{
			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);
			Size viewSize = this.View.LocalGLControl.ClientSize;

			// Draw camera action hints
			if (!handled)
			{
				if (this.camAction == CameraAction.TurnCam)
				{
					canvas.DrawLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, this.camActionBeginLoc.Y);

					if (MathF.Abs(this.camAngleVel) > 0.0f)
					{
						canvas.DrawText(string.Format("Cam Angle: {0,3:0}°", MathF.RadToDeg(this.view.CameraObj.Transform.Angle)), 10, viewSize.Height - 20);
						handled = true;
					}
				}
				else if (this.camAction == CameraAction.MoveCam || this.camVel.Z != 0.0f)
				{
					if (this.camAction == CameraAction.MoveCam)
					{
						canvas.DrawLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, cursorPos.Y);
						canvas.DrawText(string.Format("Cam X:{0,7:0}", this.view.CameraObj.Transform.Pos.X), 10, viewSize.Height - 36);
						canvas.DrawText(string.Format("Cam Y:{0,7:0}", this.view.CameraObj.Transform.Pos.Y), 10, viewSize.Height - 28);
						canvas.DrawText(string.Format("Cam Z:{0,7:0}", this.view.CameraObj.Transform.Pos.Z), 10, viewSize.Height - 20);
						handled = true;
					}
					else if (this.camVel.Z != 0.0f)
					{
						canvas.DrawText(string.Format("Cam Z:{0,7:0}", this.view.CameraObj.Transform.Pos.Z), 10, viewSize.Height - 20);
						handled = true;
					}
				}
			}

			// Draw action hints
			if (!handled)
			{
				MouseAction actionHint = this.action != MouseAction.None ? this.action : this.mouseoverAction;
				if (actionHint == MouseAction.MoveObj)				canvas.DrawText(PluginRes.EditorBaseRes.CamView_Action_Move, 10, viewSize.Height - 20);
				else if (actionHint == MouseAction.RotateObj)		canvas.DrawText(PluginRes.EditorBaseRes.CamView_Action_Rotate, 10, viewSize.Height - 20);
				else if (actionHint == MouseAction.ScaleObj)		canvas.DrawText(PluginRes.EditorBaseRes.CamView_Action_Scale, 10, viewSize.Height - 20);
				else if (this.action == MouseAction.RectSelection)	canvas.DrawText(PluginRes.EditorBaseRes.CamView_Action_Select_Active, 10, viewSize.Height - 20);

				if (actionHint != MouseAction.None) handled = true;
			}
		}
		protected virtual void OnRenderState()
		{
			// Render CamView
			this.View.CameraComponent.Render();
		}
		protected virtual void OnUpdateState()
		{
			GameObject camObj = this.View.CameraObj;
			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);

			this.camTransformChanged = false;

			if (this.camAction == CameraAction.MoveCam)
			{
				Vector3 moveVec = new Vector3(
					0.125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f),
					0.125f * MathF.Sign(cursorPos.Y - this.camActionBeginLoc.Y) * MathF.Pow(MathF.Abs(cursorPos.Y - this.camActionBeginLoc.Y), 1.25f),
					this.camVel.Z);

				MathF.TransformCoord(ref moveVec.X, ref moveVec.Y, camObj.Transform.Angle);
				this.camVel = moveVec;

				this.camTransformChanged = true;
			}
			else if (this.camVel.Length > 0.01f)
			{
				this.camVel *= MathF.Pow(0.9f, Time.TimeMult);
				this.camTransformChanged = true;
			}
			else
			{
				this.camTransformChanged = this.camTransformChanged || (this.camVel != Vector3.Zero);
				this.camVel = Vector3.Zero;
			}
			

			if (this.camAction == CameraAction.TurnCam)
			{
				float turnDir = 
					0.000125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * 
					MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f);
				this.camAngleVel = turnDir;

				this.camTransformChanged = true;
			}
			else if (Math.Abs(this.camAngleVel) > 0.001f)
			{
				this.camAngleVel *= MathF.Pow(0.9f, Time.TimeMult);
				this.camTransformChanged = true;
			}
			else
			{
				this.camTransformChanged = this.camTransformChanged || (this.camAngleVel != 0.0f);
				this.camAngleVel = 0.0f;
			}


			if (this.camTransformChanged)
			{
				camObj.Transform.MoveBy(this.camVel);
				camObj.Transform.TurnBy(this.camAngleVel);

				this.View.OnCamTransformChanged();
				this.View.LocalGLControl.Invalidate();
			}
			
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game)
			{
				this.UpdateSelectionStats();
				this.View.LocalGLControl.Invalidate();
			}
		}
		protected virtual void OnBeginAction(MouseAction action) {}
		protected virtual void OnEndAction(MouseAction action) {}

		protected virtual void OnSceneChanged()
		{
			if (this.mouseoverObject != null && this.mouseoverObject.IsInvalid) this.mouseoverObject = null;

			this.View.LocalGLControl.Invalidate();
		}
		protected virtual void OnCursorSpacePosChanged()
		{
			this.UpdateAction();
		}

		public virtual SelObj PickSelObjAt(int x, int y)
		{
			return null;
		}
		public virtual List<SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			return new List<SelObj>();
		}
		public virtual void SelectObjects(IEnumerable<SelObj> selObjEnum, MainForm.SelectMode mode = MainForm.SelectMode.Set) {}
		public virtual void ClearSelection() {}
		protected virtual void PostPerformAction(IEnumerable<SelObj> selObjEnum, MouseAction action) {}

		public virtual void DeleteObjects(IEnumerable<SelObj> objEnum) {}
		public virtual List<SelObj> CloneObjects(IEnumerable<SelObj> objEnum) { return new List<SelObj>(); }
		protected bool DisplayConfirmDeleteSelectedObjects()
		{
			if (MainForm.Instance.CurrentSandboxState == MainForm.SandboxState.Playing) return true;
			DialogResult result = MessageBox.Show(
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Text, 
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}
		
		protected void SetDefaultActiveLayers(params Type[] activeLayers)
		{
			this.lastActiveLayers = activeLayers.ToList();
		}
		protected void SaveActiveLayers()
		{
			this.lastActiveLayers = this.view.ActiveViewLayers.Select(l => l.GetType()).ToList();
		}
		protected void RestoreActiveLayers()
		{
			this.view.SetActiveLayers(this.lastActiveLayers);
		}

		protected void DrawSelectionMarkers(Canvas canvas, IEnumerable<SelObj> obj)
		{
			// Determine turned Camera axes for angle-independent drawing
			Vector2 catDotX, catDotY;
			float camAngle = this.View.CameraObj.Transform.Angle;
			MathF.GetTransformDotVec(camAngle, out catDotX, out catDotY);
			Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
			Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
			MathF.TransformDotVec(ref right, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref down, ref catDotX, ref catDotY);

			foreach (SelObj selObj in obj)
			{
				if (!selObj.HasTransform) continue;
				Vector3 posTemp = selObj.Pos;
				float scaleTemp = 1.0f;
				float radTemp = selObj.BoundRadius;

				if (!canvas.DrawDevice.IsCoordInView(posTemp, radTemp)) continue;

				// Draw selection marker
				if (selObj.ShowPos)
				{
					canvas.DrawDevice.PreprocessCoords(ref posTemp, ref scaleTemp);
					posTemp.Z = 0.0f;
					canvas.DrawDevice.AddVertices(canvas.CurrentState.Material, VertexMode.Lines,
						new VertexP3(posTemp - right * 10.0f),
						new VertexP3(posTemp + right * 10.0f),
						new VertexP3(posTemp - down * 10.0f),
						new VertexP3(posTemp + down * 10.0f));
				}

				// Draw angle marker
				if (selObj.ShowAngle)
				{
					posTemp = selObj.Pos + 
						radTemp * right * MathF.Sin(selObj.Angle - camAngle) - 
						radTemp * down * MathF.Cos(selObj.Angle - camAngle);
					canvas.DrawLine(selObj.Pos.X, selObj.Pos.Y, selObj.Pos.Z - 0.1f, posTemp.X, posTemp.Y, posTemp.Z - 0.1f);
				}

				// Draw boundary
				if (selObj.ShowBoundRadius && radTemp > 0.0f)
					canvas.DrawCircle(selObj.Pos.X, selObj.Pos.Y, selObj.Pos.Z - 0.1f, radTemp);
			}
		}
		protected void DrawLockedAxes(Canvas canvas, float x, float y, float z, float r)
		{
			canvas.PushState();
			if ((this.LockedAxes & AxisLock.X) != AxisLock.None)
			{
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, ColorRgba.Red, 0.5f)));
				canvas.DrawLine(x - r, y, z, x + r, y, z);
			}
			if ((this.LockedAxes & AxisLock.Y) != AxisLock.None)
			{
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, ColorRgba.Green, 0.5f)));
				canvas.DrawLine(x, y - r, z, x, y + r, z);
			}
			if ((this.LockedAxes & AxisLock.Z) != AxisLock.None)
			{
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Solid, ColorRgba.Mix(this.View.FgColor, ColorRgba.Blue, 0.5f)));
				canvas.DrawLine(x, y, z - r, x, y, z);
				canvas.DrawLine(x, y, z, x, y, z + r);
			}
			canvas.PopState();
		}
		
		protected Vector3 ApplyAxisLock(Vector3 vec, float lockedVal = 0.0f)
		{
			AxisLock lockAxes = this.lockedAxes;
			if (lockAxes == AxisLock.None) return vec;
			if ((lockAxes & AxisLock.X) == AxisLock.None) vec.X = lockedVal;
			if ((lockAxes & AxisLock.Y) == AxisLock.None) vec.Y = lockedVal;
			if ((lockAxes & AxisLock.Z) == AxisLock.None) vec.Z = lockedVal;
			return vec;
		}
		
		protected void BeginAction(MouseAction action)
		{
			if (action == MouseAction.None) return;
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			this.actionBeginLoc = mouseLoc;
			this.action = action;

			this.camVel = Vector3.Zero;

			if (MainForm.Instance.CurrentSandboxState == MainForm.SandboxState.Playing)
				MainForm.Instance.SandboxSceneStartFreeze();

			// Begin movement
			if (this.action == MouseAction.MoveObj)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
				this.actionBeginLocSpace.Z = this.View.CameraObj.Transform.Pos.Z;
			}
			// Begin rotation
			else if (this.action == MouseAction.RotateObj)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin scale
			else if (this.action == MouseAction.ScaleObj)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin rect selection
			else if (this.action == MouseAction.RectSelection)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector2(mouseLoc.X, mouseLoc.Y));
			}

			this.OnBeginAction(this.action);
		}
		protected void EndAction()
		{
			if (this.action == MouseAction.None) return;
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			if (this.action == MouseAction.RectSelection)
			{
				this.activeRectSel = new ObjectSelection();
			}

			if (MainForm.Instance.CurrentSandboxState == MainForm.SandboxState.Playing)
				MainForm.Instance.SandboxSceneStopFreeze();

			this.OnEndAction(this.action);
			this.action = MouseAction.None;
		}
		protected void UpdateAction()
		{
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			if (this.action == MouseAction.RectSelection)
				this.UpdateRectSelection(mouseLoc);
			else if (this.action == MouseAction.MoveObj)
				this.UpdateObjMove(mouseLoc);
			else if (this.action == MouseAction.RotateObj)
				this.UpdateObjRotate(mouseLoc);
			else if (this.action == MouseAction.ScaleObj)
				this.UpdateObjScale(mouseLoc);
			else
				this.UpdateMouseover(mouseLoc);

			if (this.action != MouseAction.None)
				this.UpdateSelectionStats();
		}
		protected void UpdateSelectionStats()
		{
			List<SelObj> transformObjSel = this.allObjSel.Where(s => s.HasTransform).ToList();

			this.selectionCenter = Vector3.Zero;
			this.selectionRadius = 0.0f;

			foreach (SelObj s in transformObjSel)
				this.selectionCenter += s.Pos;
			if (transformObjSel.Count > 0) this.selectionCenter /= transformObjSel.Count;

			foreach (SelObj s in transformObjSel)
				this.selectionRadius = MathF.Max(this.selectionRadius, s.BoundRadius + (s.Pos - this.selectionCenter).Length);
		}
		protected void UpdateMouseover(Point mouseLoc)
		{
			bool lastMouseoverSelect = this.mouseoverSelect;
			SelObj lastMouseoverObject = this.mouseoverObject;
			MouseAction lastMouseoverAction = this.mouseoverAction;

			if (this.actionAllowed)
			{
				// Determine object at mouse position
				this.mouseoverObject = this.PickSelObjAt(mouseLoc.X, mouseLoc.Y);

				// Determine action variables
				Vector3 mouseSpaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
				float scale = this.View.GetScaleAtZ(this.selectionCenter.Z);
				const float boundaryThickness = 10.0f;
				bool tooSmall = this.selectionRadius * scale <= boundaryThickness * 2.0f;
				bool mouseOverBoundary = MathF.Abs((mouseSpaceCoord - this.selectionCenter).Length - this.selectionRadius) * scale < boundaryThickness;
				bool mouseInsideBoundary = !mouseOverBoundary && (mouseSpaceCoord - this.selectionCenter).Length < this.selectionRadius;
				bool mouseAtCenterAxis = 
					MathF.Abs(mouseSpaceCoord.X - this.selectionCenter.X) * scale < boundaryThickness || 
					MathF.Abs(mouseSpaceCoord.Y - this.selectionCenter.Y) * scale < boundaryThickness;
				bool shift = (Control.ModifierKeys & Keys.Shift) != Keys.None;
				bool ctrl = (Control.ModifierKeys & Keys.Control) != Keys.None;

				bool anySelection = this.actionObjSel.Count > 0;
				bool canMove = this.actionObjSel.Any(s => s.IsActionAvailable(MouseAction.MoveObj));
				bool canRotate = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(MouseAction.RotateObj));
				bool canScale = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(MouseAction.ScaleObj));

				// Select which action to propose
				this.mouseoverSelect = false;
				if (shift || ctrl)
					this.mouseoverAction = MouseAction.RectSelection;
				else if (anySelection && !tooSmall && mouseOverBoundary && mouseAtCenterAxis && this.selectionRadius > 0.0f && canScale)
					this.mouseoverAction = MouseAction.ScaleObj;
				else if (anySelection && !tooSmall && mouseOverBoundary && canRotate)
					this.mouseoverAction = MouseAction.RotateObj;
				else if (anySelection && mouseInsideBoundary && canMove)
					this.mouseoverAction = MouseAction.MoveObj;
				else if (this.mouseoverObject != null && this.mouseoverObject.IsActionAvailable(MouseAction.MoveObj))
				{
					this.mouseoverAction = MouseAction.MoveObj; 
					this.mouseoverSelect = true;
				}
				else
					this.mouseoverAction = MouseAction.RectSelection;
			}
			else
			{
				this.mouseoverObject = null;
				this.mouseoverSelect = false;
				this.mouseoverAction = MouseAction.None;
			}

			// If mouseover changed..
			if (this.mouseoverObject != lastMouseoverObject || 
				this.mouseoverSelect != lastMouseoverSelect ||
				this.mouseoverAction != lastMouseoverAction)
			{
				// Adjust mouse cursor based on proposed action
				if (this.mouseoverAction == MouseAction.MoveObj)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionMove;
				else if (this.mouseoverAction == MouseAction.RotateObj)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionRotate;
				else if (this.mouseoverAction == MouseAction.ScaleObj)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionScale;
				else
					this.View.LocalGLControl.Cursor = CursorHelper.Arrow;

				// Redraw
				this.View.LocalGLControl.Invalidate();
			}
		}
		private void UpdateRectSelection(Point mouseLoc)
		{
			if (MainForm.Instance.IsSelectionChanging) return; // Prevent Recursion in case SelectObjects triggers UpdateAction.

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
			List<SelObj> picked = this.PickSelObjIn(pX, pY, pW, pH);

			// Store in internal rect selection
			ObjectSelection oldRectSel = this.activeRectSel;
			this.activeRectSel = new ObjectSelection(picked);

			// Apply internal selection to actual editor selection
			if (shift || ctrl)
			{
				if (this.activeRectSel.ObjectCount > 0)
				{
					ObjectSelection added = (this.activeRectSel - oldRectSel) + (oldRectSel - this.activeRectSel);
					this.SelectObjects(added.Objects.OfType<SelObj>(), shift ? MainForm.SelectMode.Append : MainForm.SelectMode.Toggle);
				}
			}
			else if (this.activeRectSel.ObjectCount > 0)
				this.SelectObjects(this.activeRectSel.Objects.OfType<SelObj>());
			else
				this.ClearSelection();

			this.View.LocalGLControl.Invalidate();
		}
		private void UpdateObjMove(Point mouseLoc)
		{
			float zMovement = this.View.CameraObj.Transform.Pos.Z - this.actionBeginLocSpace.Z;
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z + zMovement));
			Vector3 movement = spaceCoord - this.actionBeginLocSpace;
			movement.Z = zMovement;
			movement = this.ApplyAxisLock(movement);
			if (movement != Vector3.Zero)
			{
				foreach (SelObj s in this.actionObjSel) s.Pos += movement;
				this.PostPerformAction(this.actionObjSel, this.action);
			}
			this.UpdateSelectionStats();
			this.actionBeginLocSpace = spaceCoord;
			this.actionBeginLocSpace.Z = this.View.CameraObj.Transform.Pos.Z;
			this.View.LocalGLControl.Invalidate();
		}
		private void UpdateObjRotate(Point mouseLoc)
		{
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, this.actionBeginLocSpace.X, this.actionBeginLocSpace.Y);
			float curAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, spaceCoord.X, spaceCoord.Y);
			float rotation = curAngle - lastAngle;
			if (rotation != 0.0f)
			{
				foreach (SelObj s in this.actionObjSel)
				{
					Vector3 posRelCenter = s.Pos - this.selectionCenter;
					Vector3 posRelCenterTarget = posRelCenter;
					MathF.TransformCoord(ref posRelCenterTarget.X, ref posRelCenterTarget.Y, rotation);
					//posRelCenterTarget = this.LockAxis(posRelCenterTarget, this.actionAxisLock, 1.0f);

					s.Pos = this.selectionCenter + posRelCenterTarget;
					s.Angle += rotation;
				}
				this.PostPerformAction(this.actionObjSel, this.action);
			}
			this.UpdateSelectionStats();
			this.actionBeginLocSpace = spaceCoord;
			this.View.LocalGLControl.Invalidate();
		}
		private void UpdateObjScale(Point mouseLoc)
		{
			if (this.selectionRadius == 0.0f) return;

			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastRadius = this.selectionRadius;
			float curRadius = (this.selectionCenter - spaceCoord).Length;
			float scale = MathF.Clamp(curRadius / lastRadius, 0.0001f, 10000.0f);
			if (scale != 1.0f)
			{
				foreach (SelObj s in this.actionObjSel)
				{
					Vector3 scaleVec = new Vector3(scale, scale, scale);
					//scaleVec = this.LockAxis(scaleVec, this.actionAxisLock, 1.0f);
					Vector3 posRelCenter = s.Pos - this.selectionCenter;
					Vector3 posRelCenterTarget;
					Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

					s.Pos = this.selectionCenter + posRelCenterTarget;
					s.Scale *= scaleVec;
				}
				this.PostPerformAction(this.actionObjSel, this.action);
			}

			this.UpdateSelectionStats();

			// If scaling didn't change the total bounding radius, the selected object doesn't support it properly - roll back!
			if (lastRadius == this.selectionRadius)
			{
				float invScale = 1.0f / scale;
				foreach (SelObj s in this.actionObjSel)
				{
					Vector3 scaleVec = new Vector3(invScale, invScale, invScale);
					//scaleVec = this.LockAxis(scaleVec, this.actionAxisLock, 1.0f);
					Vector3 posRelCenter = s.Pos - this.selectionCenter;
					Vector3 posRelCenterTarget;
					Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

					s.Pos = this.selectionCenter + posRelCenterTarget;
					s.Scale *= scaleVec;
				}
				this.PostPerformAction(this.actionObjSel, this.action);
				this.UpdateSelectionStats();
			}

			this.actionBeginLocSpace = spaceCoord;
			this.View.LocalGLControl.Invalidate();
		}
		
		protected void CollectLayerDrawcalls(Canvas canvas)
		{
			var layers = this.View.ActiveViewLayers.ToArray();
			layers.StableSort((a, b) => a.Priority - b.Priority);
			foreach (var layer in layers)
			{
				canvas.PushState();
				layer.OnCollectDrawcalls(canvas);
				canvas.PopState();
			}
		}
		protected void CollectLayerOverlayDrawcalls(Canvas canvas)
		{
			var layers = this.View.ActiveViewLayers.ToArray();
			layers.StableSort((a, b) => a.Priority - b.Priority);
			foreach (var layer in layers)
			{
				canvas.PushState();
				layer.OnCollectOverlayDrawcalls(canvas);
				canvas.PopState();
			}
		}
		protected void CollectLayerBackgroundDrawcalls(Canvas canvas)
		{
			var layers = this.View.ActiveViewLayers.ToArray();
			layers.StableSort((a, b) => a.Priority - b.Priority);
			foreach (var layer in layers)
			{
				canvas.PushState();
				layer.OnCollectBackgroundDrawcalls(canvas);
				canvas.PopState();
			}
		}

		private void LocalGLControl_Paint(object sender, PaintEventArgs e)
		{
			// Retrieve OpenGL context
 			try { this.View.MainContextControl.Context.MakeCurrent(this.View.LocalGLControl.WindowInfo); } catch (Exception) { return; }
			this.View.MakeDualityTarget();

			try
			{
				this.View.CameraComponent.Passes.Add(this.camPassBg);
				this.View.CameraComponent.Passes.Add(this.camPassEdWorld);
				this.View.CameraComponent.Passes.Add(this.camPassEdScreen);
				this.OnRenderState();
				this.View.CameraComponent.Passes.Remove(this.camPassBg);
				this.View.CameraComponent.Passes.Remove(this.camPassEdWorld);
				this.View.CameraComponent.Passes.Remove(this.camPassEdScreen);
			}
			catch (Exception exception)
			{
				Log.Editor.WriteError("An error occured during CamView {1} rendering: {0}", Log.Exception(exception), this.View.CameraComponent.ToString());
			}

			this.View.MainContextControl.SwapBuffers();
		}
		private void LocalGLControl_MouseMove(object sender, MouseEventArgs e)
		{
			this.OnCursorSpacePosChanged();
		}
		private void LocalGLControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.action == MouseAction.RectSelection && this.actionBeginLoc == e.Location)
				this.UpdateRectSelection(e.Location);

			if (e.Button == MouseButtons.Left)
				this.EndAction();

			if (this.camAction == CameraAction.MoveCam && e.Button == MouseButtons.Middle)
				this.camAction = CameraAction.None;
			else if (this.camAction == CameraAction.TurnCam && e.Button == MouseButtons.Right)
				this.camAction = CameraAction.None;

			this.View.LocalGLControl.Invalidate();
		}
		private void LocalGLControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.action == MouseAction.None)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (this.mouseoverSelect)
					{
						// To interact with an object that isn't selected yet: Select it.
						if (!this.allObjSel.Contains(this.mouseoverObject))
							this.SelectObjects(new [] { this.mouseoverObject });
					}
					this.BeginAction(this.mouseoverAction);
				}
			}

			if (this.camActionAllowed && this.camAction == CameraAction.None)
			{
				this.camActionBeginLoc = e.Location;
				if (e.Button == MouseButtons.Middle)
				{
					this.camAction = CameraAction.MoveCam;
					this.camActionBeginLocSpace = this.View.CameraObj.Transform.RelativePos;
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.camAction = CameraAction.TurnCam;
					this.camActionBeginLocSpace = new Vector3(this.View.CameraObj.Transform.RelativeAngle, 0.0f, 0.0f);
				}
			}
		}
		private void LocalGLControl_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta != 0)
			{
				if (this.View.ParallaxActive)
				{
					GameObject camObj = this.View.CameraObj;
					float curVel = this.camVel.Length * MathF.Sign(this.camVel.Z);
					Vector2 curTemp = new Vector2(
						(e.X * 2.0f / this.View.LocalGLControl.Width) - 1.0f,
						(e.Y * 2.0f / this.View.LocalGLControl.Height) - 1.0f);
					MathF.TransformCoord(ref curTemp.X, ref curTemp.Y, camObj.Transform.RelativeAngle);

					if (MathF.Sign(e.Delta) == MathF.Sign(curVel))
						curVel *= 0.0125f * MathF.Abs(e.Delta);
					curVel += 0.075f * e.Delta;
					curVel = MathF.Sign(curVel) * MathF.Min(MathF.Abs(curVel), 500.0f);

					Vector3 movVec = new Vector3(
						MathF.Sign(e.Delta) * MathF.Sign(curTemp.X) * MathF.Pow(curTemp.X, 2.0f), 
						MathF.Sign(e.Delta) * MathF.Sign(curTemp.Y) * MathF.Pow(curTemp.Y, 2.0f), 
						1.0f);
					movVec.Normalize();
					this.camVel = movVec * curVel;
				}
				else
				{
					this.View.ParallaxRefDist = this.View.ParallaxRefDist + this.View.ParallaxRefDistIncrement * e.Delta / 40;
				}
			}
		}
		private void LocalGLControl_MouseLeave(object sender, EventArgs e)
		{
			this.OnCursorSpacePosChanged();

			this.mouseoverAction = MouseAction.None;
			this.mouseoverObject = null;
			this.mouseoverSelect = false;

			this.View.LocalGLControl.Invalidate();
		}
		private void LocalGLControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.actionAllowed)
			{
				if (e.KeyCode == Keys.Delete)
				{
					this.DeleteObjects(this.actionObjSel);
				}
				else if (e.KeyCode == Keys.C && (Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					List<SelObj> cloneList = this.CloneObjects(this.actionObjSel);
					this.SelectObjects(cloneList);
				}
				else if (e.KeyCode == Keys.F)
				{
					this.view.FocusOnObject(MainForm.Instance.Selection.MainGameObject);
				}
				else
				{
					bool axisLockChanged = false;
					if (e.KeyCode == Keys.X) { this.lockedAxes |= AxisLock.X; axisLockChanged = true; }
					if (e.KeyCode == Keys.Y) { this.lockedAxes |= AxisLock.Y; axisLockChanged = true; }
					if (e.KeyCode == Keys.Z) { this.lockedAxes |= AxisLock.Z; axisLockChanged = true; }

					if (axisLockChanged) this.View.LocalGLControl.Invalidate();
				}
			}
		}
		private void LocalGLControl_KeyUp(object sender, KeyEventArgs e)
		{
			bool axisLockChanged = false;
			if (e.KeyCode == Keys.X) { this.lockedAxes &= ~AxisLock.X; axisLockChanged = true; }
			if (e.KeyCode == Keys.Y) { this.lockedAxes &= ~AxisLock.Y; axisLockChanged = true; }
			if (e.KeyCode == Keys.Z) { this.lockedAxes &= ~AxisLock.Z; axisLockChanged = true; }

			if (axisLockChanged) this.View.LocalGLControl.Invalidate();
		}
		private void LocalGLControl_LostFocus(object sender, EventArgs e)
		{
			if (MainForm.Instance == null) return;

			this.camAction = CameraAction.None;
			this.EndAction();
			this.lockedAxes = AxisLock.None;
			this.View.LocalGLControl.Invalidate();
		}
		private void View_ParallaxRefDistChanged(object sender, EventArgs e)
		{
			this.OnCursorSpacePosChanged();
		}
		private void EditorForm_AfterUpdateDualityApp(object sender, EventArgs e)
		{
			this.OnUpdateState();
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.HasAnyProperty(ReflectionInfo.Property_Transform_RelativePos, ReflectionInfo.Property_Transform_RelativeAngle) &&
				e.Objects.Components.Any(c => c.GameObj == this.view.CameraObj))
			{
				this.OnCursorSpacePosChanged();
			}
		}
		private void Scene_Changed(object sender, EventArgs e)
		{
			this.OnSceneChanged();
		}
		private void camPassEdScreen_CollectDrawcalls(object sender, CollectDrawcallEventArgs e)
		{
			Canvas canvas = new Canvas(this.View.CameraComponent.DrawDevice);
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Mask, this.View.FgColor));
			canvas.CurrentState.TextFont = Duality.Resources.Font.GenericMonospace8;

			this.OnCollectStateOverlayDrawcalls(canvas);
		}
		private void camPassEdWorld_CollectDrawcalls(object sender, CollectDrawcallEventArgs e)
		{
			Canvas canvas = new Canvas(this.View.CameraComponent.DrawDevice);
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Mask, this.View.FgColor));
			canvas.CurrentState.TextFont = Duality.Resources.Font.GenericMonospace8;

			this.OnCollectStateDrawcalls(canvas);
		}
		private void camPassBg_CollectDrawcalls(object sender, CollectDrawcallEventArgs e)
		{
			Canvas canvas = new Canvas(this.View.CameraComponent.DrawDevice);
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Mask, this.View.FgColor));
			canvas.CurrentState.TextFont = Duality.Resources.Font.GenericMonospace8;

			this.OnCollectStateBackgroundDrawcalls(canvas);
		}
	}
}
