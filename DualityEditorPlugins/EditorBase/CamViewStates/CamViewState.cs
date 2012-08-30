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
	public abstract class CamViewState : IHelpProvider
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
			Move,
			Rotate
		}
		public enum ObjectAction
		{
			None,
			RectSelect,
			Move,
			Rotate,
			Scale
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

			public virtual bool IsActionAvailable(ObjectAction action)
			{
				if (action == ObjectAction.Move) return true;
				return false;
			}
			public virtual void DrawActionGizmo(Canvas canvas, ObjectAction action, Vector2 curLoc, bool performing) {}
			
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
		private	bool			engineUserInput		= false;
		private	bool			actionAllowed		= true;
		private	Point			actionBeginLoc		= Point.Empty;
		private Vector3			actionBeginLocSpace	= Vector3.Zero;
		private ObjectAction		action				= ObjectAction.None;
		private	Vector3			selectionCenter		= Vector3.Zero;
		private	float			selectionRadius		= 0.0f;
		private	ObjectSelection	activeRectSel		= new ObjectSelection();
		private	ObjectAction		mouseoverAction		= ObjectAction.None;
		private	SelObj			mouseoverObject		= null;
		private	bool			mouseoverSelect		= false;
		private	CameraAction	drawCamGizmoState	= CameraAction.None;
		private	ObjectAction	drawSelGizmoState	= ObjectAction.None;
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
		public ObjectAction SelObjAction
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
			get { return this.view != null && this.view.ActiveState == this; }
		}
		public bool EngineUserInput
		{
			get { return this.engineUserInput; }
			protected set { this.engineUserInput = value; }
		}
		public bool MouseActionAllowed
		{
			get { return this.actionAllowed; }
			protected set
			{
				this.actionAllowed = value;
				if (!this.actionAllowed)
				{
					this.mouseoverAction = ObjectAction.None;
					this.mouseoverObject = null;
					this.mouseoverSelect = false;
					if (this.action != ObjectAction.None)
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
					this.InvalidateView();
				}
			}
		}
		public bool MouseoverSelect
		{
			get { return this.mouseoverSelect; }
		}
		public SelObj MouseoverObject
		{
			get { return this.mouseoverObject; }
		}
		public ObjectAction MouseoverAction
		{
			get { return this.mouseoverAction; }
		}
		public ObjectAction Action
		{
			get { return this.action; }
		}
		public ObjectAction VisibleAction
		{
			get
			{
				return 
					(this.drawSelGizmoState != ObjectAction.None ? this.drawSelGizmoState : 
					(this.action != ObjectAction.None ? this.action :
					(this.mouseoverAction != ObjectAction.RectSelect ? this.mouseoverAction :
					ObjectAction.None)));
			}
		}
		protected bool CameraTransformChanged
		{
			get { return this.camTransformChanged; }
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
			DualityEditorApp.UpdatingEngine += this.EditorForm_AfterUpdateDualityApp;
			DualityEditorApp.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;

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
			DualityEditorApp.UpdatingEngine -= this.EditorForm_AfterUpdateDualityApp;
			
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
			if (this.mouseoverObject != null && (this.mouseoverAction == ObjectAction.RectSelect || this.mouseoverSelect) && !transformObjSel.Contains(this.mouseoverObject)) 
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
			bool canMove = this.actionObjSel.Any(s => s.IsActionAvailable(ObjectAction.Move));
			bool canScale = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(ObjectAction.Scale));
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
			ObjectAction visibleObjectAction = this.VisibleAction;
			canvas.PushState();

			// Draw camera movement indicators
			if (this.camAction != CameraAction.None)
			{
				canvas.PushState();
				canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(0.5f);
				canvas.FillCircle(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, 3);
				if (this.camAction == CameraAction.Move)
					canvas.DrawLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, cursorPos.Y);
				else if (this.camAction == CameraAction.Rotate)
					canvas.DrawLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, this.camActionBeginLoc.Y);
				canvas.PopState();
			}
			
			// Draw hovered / selected objects action gizmo
			if (visibleObjectAction != ObjectAction.None && (this.mouseoverObject != null || this.actionObjSel.Count == 1))
			{
				canvas.PushState();
				Vector2 pos;
				SelObj obj;
				if (this.mouseoverObject != null || this.mouseoverAction == visibleObjectAction)
				{
					obj = this.mouseoverObject ?? this.actionObjSel[0];
					pos = new Vector2(cursorPos.X + 30, cursorPos.Y + 10);
				}
				else
				{
					obj = this.actionObjSel[0];
					pos = this.View.GetScreenCoord(this.actionObjSel[0].Pos).Xy;
				}
				obj.DrawActionGizmo(canvas, visibleObjectAction, pos, false);
				canvas.PopState();
			}

			// Draw current state text
			bool handled = false;
			this.DrawStatusText(canvas, ref handled);

			// Draw rect selection
			if (this.action == ObjectAction.RectSelect)
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
			CameraAction visibleCamAction = this.drawCamGizmoState != CameraAction.None ? this.drawCamGizmoState : this.camAction;
			ObjectAction visibleObjectAction = this.VisibleAction;

			// Draw camera action hints
			if (!handled)
			{
				int textYOff = -20;
				string[] text = null;
				if (visibleCamAction == CameraAction.Rotate)
				{
					if (MathF.Abs(this.camAngleVel) > 0.0f)
					{
						text = new string[] { string.Format("Cam Angle: {0,3:0}°", MathF.RadToDeg(this.view.CameraObj.Transform.Angle)) };
						handled = true;
					}
				}
				else if (visibleCamAction == CameraAction.Move || this.camVel.Z != 0.0f)
				{
					if (visibleCamAction == CameraAction.Move)
					{
						text = new string[]
						{
							string.Format("Cam X:{0,7:0}", this.view.CameraObj.Transform.Pos.X),
							string.Format("Cam Y:{0,7:0}", this.view.CameraObj.Transform.Pos.Y),
							string.Format("Cam Z:{0,7:0}", this.view.CameraObj.Transform.Pos.Z)
						};
						textYOff -= canvas.CurrentState.TextFont.Res.Height * 2;
						handled = true;
					}
					else if (this.camVel.Z != 0.0f)
					{
						text = new string[] { string.Format("Cam Z:{0,7:0}", this.view.CameraObj.Transform.Pos.Z) };
						handled = true;
					}
				}

				if (text != null)
				{
					canvas.DrawTextBackground(text, 10, viewSize.Height + textYOff);
					canvas.DrawText(text, 10, viewSize.Height + textYOff);
				}
			}

			// Draw action hints
			if (!handled)
			{
				string text = null;

				if (visibleObjectAction == ObjectAction.Move)				text = PluginRes.EditorBaseRes.CamView_Action_Move;
				else if (visibleObjectAction == ObjectAction.Rotate)		text = PluginRes.EditorBaseRes.CamView_Action_Rotate;
				else if (visibleObjectAction == ObjectAction.Scale)			text = PluginRes.EditorBaseRes.CamView_Action_Scale;
				else if (visibleObjectAction == ObjectAction.RectSelect)	text = PluginRes.EditorBaseRes.CamView_Action_Select_Active;

				if (text != null)
				{
					canvas.DrawTextBackground(text, 10, viewSize.Height - 20);
					canvas.DrawText(text, 10, viewSize.Height - 20);
				}

				if (visibleObjectAction != ObjectAction.None) handled = true;
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

			if (this.camAction == CameraAction.Move)
			{
				Vector3 moveVec = new Vector3(
					cursorPos.X - this.camActionBeginLoc.X,
					cursorPos.Y - this.camActionBeginLoc.Y,
					this.camVel.Z);

				const float BaseSpeedCursorLen = 25.0f;
				const float BaseSpeed = 2.0f;
				moveVec.X = BaseSpeed * MathF.Sign(moveVec.X) * MathF.Pow(MathF.Abs(moveVec.X) / BaseSpeedCursorLen, 1.5f);
				moveVec.Y = BaseSpeed * MathF.Sign(moveVec.Y) * MathF.Pow(MathF.Abs(moveVec.Y) / BaseSpeedCursorLen, 1.5f);

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
			

			if (this.camAction == CameraAction.Rotate)
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
				this.InvalidateView();
			}
			
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game)
			{
				this.UpdateSelectionStats();
				this.InvalidateView();
			}
		}
		protected virtual void OnBeginAction(ObjectAction action) {}
		protected virtual void OnEndAction(ObjectAction action) {}

		protected virtual void OnSceneChanged()
		{
			if (this.mouseoverObject != null && this.mouseoverObject.IsInvalid) this.mouseoverObject = null;

			this.InvalidateView();
		}
		protected virtual void OnCursorSpacePosChanged()
		{
			this.UpdateAction();
		}

		public void InvalidateView()
		{
			if (this.View == null || this.View.LocalGLControl == null) return;
			this.View.LocalGLControl.Invalidate();
		}

		public virtual SelObj PickSelObjAt(int x, int y)
		{
			return null;
		}
		public virtual List<SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			return new List<SelObj>();
		}
		public virtual void SelectObjects(IEnumerable<SelObj> selObjEnum, SelectMode mode = SelectMode.Set) {}
		public virtual void ClearSelection() {}
		protected virtual void PostPerformAction(IEnumerable<SelObj> selObjEnum, ObjectAction action) {}

		public virtual void DeleteObjects(IEnumerable<SelObj> objEnum) {}
		public virtual List<SelObj> CloneObjects(IEnumerable<SelObj> objEnum) { return new List<SelObj>(); }
		public void MoveSelectionBy(Vector3 move)
		{
			if (move == Vector3.Zero) return;

			foreach (SelObj s in this.actionObjSel)
				s.Pos += move;

			this.drawSelGizmoState = ObjectAction.Move;
			this.PostPerformAction(this.actionObjSel, ObjectAction.Move);
			this.UpdateSelectionStats();
			this.InvalidateView();
		}
		public void RotateSelectionBy(float rotation)
		{
			if (rotation == 0.0f) return;

			foreach (SelObj s in this.actionObjSel)
			{
				Vector3 posRelCenter = s.Pos - this.selectionCenter;
				Vector3 posRelCenterTarget = posRelCenter;
				MathF.TransformCoord(ref posRelCenterTarget.X, ref posRelCenterTarget.Y, rotation);

				s.Pos = this.selectionCenter + posRelCenterTarget;
				s.Angle += rotation;
			}

			this.drawSelGizmoState = ObjectAction.Rotate;
			this.PostPerformAction(this.actionObjSel, ObjectAction.Rotate);
			this.UpdateSelectionStats();
			this.InvalidateView();
		}
		public void ScaleSelectionBy(float scale)
		{
			if (scale == 1.0f) return;

			float lastRadius = this.selectionRadius;
			foreach (SelObj s in this.actionObjSel)
			{
				Vector3 scaleVec = new Vector3(scale, scale, scale);
				Vector3 posRelCenter = s.Pos - this.selectionCenter;
				Vector3 posRelCenterTarget;
				Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

				s.Pos = this.selectionCenter + posRelCenterTarget;
				s.Scale *= scaleVec;
			}
			this.drawSelGizmoState = ObjectAction.Scale;
			this.PostPerformAction(this.actionObjSel, ObjectAction.Scale);

			this.UpdateSelectionStats();

			// If scaling didn't change the total bounding radius, the selected object doesn't support it properly - roll back!
			if (lastRadius == this.selectionRadius)
			{
				float invScale = 1.0f / scale;
				foreach (SelObj s in this.actionObjSel)
				{
					Vector3 scaleVec = new Vector3(invScale, invScale, invScale);
					Vector3 posRelCenter = s.Pos - this.selectionCenter;
					Vector3 posRelCenterTarget;
					Vector3.Multiply(ref posRelCenter, ref scaleVec, out posRelCenterTarget);

					s.Pos = this.selectionCenter + posRelCenterTarget;
					s.Scale *= scaleVec;
				}
				this.PostPerformAction(this.actionObjSel, ObjectAction.Scale);
				this.UpdateSelectionStats();
			}

			this.InvalidateView();
		}
		protected bool DisplayConfirmDeleteSelectedObjects()
		{
			if (Sandbox.State == SandboxState.Playing) return true;
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
			this.lastActiveLayers = this.view.ActiveLayers.Select(l => l.GetType()).ToList();
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
					canvas.DrawLine(selObj.Pos.X, selObj.Pos.Y, selObj.Pos.Z - 1.0f, posTemp.X, posTemp.Y, posTemp.Z - 1.0f);
				}

				// Draw boundary
				if (selObj.ShowBoundRadius && radTemp > 0.0f)
					canvas.DrawCircle(selObj.Pos.X, selObj.Pos.Y, selObj.Pos.Z - 1.0f, radTemp);
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
		
		protected void BeginAction(ObjectAction action)
		{
			if (action == ObjectAction.None) return;
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			this.actionBeginLoc = mouseLoc;
			this.action = action;

			this.camVel = Vector3.Zero;

			if (Sandbox.State == SandboxState.Playing)
				Sandbox.Freeze();

			// Begin movement
			if (this.action == ObjectAction.Move)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
				this.actionBeginLocSpace.Z = this.View.CameraObj.Transform.Pos.Z;
			}
			// Begin rotation
			else if (this.action == ObjectAction.Rotate)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin scale
			else if (this.action == ObjectAction.Scale)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			}
			// Begin rect selection
			else if (this.action == ObjectAction.RectSelect)
			{
				this.actionBeginLocSpace = this.View.GetSpaceCoord(new Vector2(mouseLoc.X, mouseLoc.Y));
			}

			this.OnBeginAction(this.action);
		}
		protected void EndAction()
		{
			if (this.action == ObjectAction.None) return;
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			if (this.action == ObjectAction.RectSelect)
			{
				this.activeRectSel = new ObjectSelection();
			}

			if (Sandbox.State == SandboxState.Playing)
				Sandbox.UnFreeze();

			this.OnEndAction(this.action);
			this.action = ObjectAction.None;
		}
		protected void UpdateAction()
		{
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);

			if (this.action == ObjectAction.RectSelect)
				this.UpdateRectSelection(mouseLoc);
			else if (this.action == ObjectAction.Move)
				this.UpdateObjMove(mouseLoc);
			else if (this.action == ObjectAction.Rotate)
				this.UpdateObjRotate(mouseLoc);
			else if (this.action == ObjectAction.Scale)
				this.UpdateObjScale(mouseLoc);
			else
				this.UpdateMouseover(mouseLoc);

			if (this.action != ObjectAction.None)
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
			ObjectAction lastMouseoverAction = this.mouseoverAction;

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
				bool canMove = this.actionObjSel.Any(s => s.IsActionAvailable(ObjectAction.Move));
				bool canRotate = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(ObjectAction.Rotate));
				bool canScale = (canMove && this.actionObjSel.Count > 1) || this.actionObjSel.Any(s => s.IsActionAvailable(ObjectAction.Scale));

				// Select which action to propose
				this.mouseoverSelect = false;
				if (shift || ctrl)
					this.mouseoverAction = ObjectAction.RectSelect;
				else if (anySelection && !tooSmall && mouseOverBoundary && mouseAtCenterAxis && this.selectionRadius > 0.0f && canScale)
					this.mouseoverAction = ObjectAction.Scale;
				else if (anySelection && !tooSmall && mouseOverBoundary && canRotate)
					this.mouseoverAction = ObjectAction.Rotate;
				else if (anySelection && mouseInsideBoundary && canMove)
					this.mouseoverAction = ObjectAction.Move;
				else if (this.mouseoverObject != null && this.mouseoverObject.IsActionAvailable(ObjectAction.Move))
				{
					this.mouseoverAction = ObjectAction.Move; 
					this.mouseoverSelect = true;
				}
				else
					this.mouseoverAction = ObjectAction.RectSelect;
			}
			else
			{
				this.mouseoverObject = null;
				this.mouseoverSelect = false;
				this.mouseoverAction = ObjectAction.None;
			}

			// If mouseover changed..
			if (this.mouseoverObject != lastMouseoverObject || 
				this.mouseoverSelect != lastMouseoverSelect ||
				this.mouseoverAction != lastMouseoverAction)
			{
				// Adjust mouse cursor based on proposed action
				if (this.mouseoverAction == ObjectAction.Move)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionMove;
				else if (this.mouseoverAction == ObjectAction.Rotate)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionRotate;
				else if (this.mouseoverAction == ObjectAction.Scale)
					this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionScale;
				else
					this.View.LocalGLControl.Cursor = CursorHelper.Arrow;
			}
			
			// Redraw if action gizmos might be visible
			if (this.actionAllowed)
				this.InvalidateView();
		}
		private void UpdateRectSelection(Point mouseLoc)
		{
			if (DualityEditorApp.IsSelectionChanging) return; // Prevent Recursion in case SelectObjects triggers UpdateAction.

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
					this.SelectObjects(added.Objects.OfType<SelObj>(), shift ? SelectMode.Append : SelectMode.Toggle);
				}
			}
			else if (this.activeRectSel.ObjectCount > 0)
				this.SelectObjects(this.activeRectSel.Objects.OfType<SelObj>());
			else
				this.ClearSelection();

			this.InvalidateView();
		}
		private void UpdateObjMove(Point mouseLoc)
		{
			float zMovement = this.View.CameraObj.Transform.Pos.Z - this.actionBeginLocSpace.Z;
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z + zMovement));
			Vector3 movement = spaceCoord - this.actionBeginLocSpace;
			movement.Z = zMovement;
			movement = this.ApplyAxisLock(movement);

			this.MoveSelectionBy(movement);

			this.actionBeginLocSpace = spaceCoord;
			this.actionBeginLocSpace.Z = this.View.CameraObj.Transform.Pos.Z;
		}
		private void UpdateObjRotate(Point mouseLoc)
		{
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, this.actionBeginLocSpace.X, this.actionBeginLocSpace.Y);
			float curAngle = MathF.Angle(this.selectionCenter.X, this.selectionCenter.Y, spaceCoord.X, spaceCoord.Y);
			float rotation = curAngle - lastAngle;

			this.RotateSelectionBy(rotation);

			this.actionBeginLocSpace = spaceCoord;
		}
		private void UpdateObjScale(Point mouseLoc)
		{
			if (this.selectionRadius == 0.0f) return;

			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float lastRadius = this.selectionRadius;
			float curRadius = (this.selectionCenter - spaceCoord).Length;
			float scale = MathF.Clamp(curRadius / lastRadius, 0.0001f, 10000.0f);
			
			this.ScaleSelectionBy(scale);

			this.actionBeginLocSpace = spaceCoord;
			this.InvalidateView();
		}
		
		protected void CollectLayerDrawcalls(Canvas canvas)
		{
			var layers = this.View.ActiveLayers.ToArray();
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
			var layers = this.View.ActiveLayers.ToArray();
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
			var layers = this.View.ActiveLayers.ToArray();
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
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Terminated) return;

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
			this.drawCamGizmoState = CameraAction.None;
			this.drawSelGizmoState = ObjectAction.None;
			this.OnCursorSpacePosChanged();
		}
		private void LocalGLControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.action == ObjectAction.RectSelect && this.actionBeginLoc == e.Location)
				this.UpdateRectSelection(e.Location);

			if (e.Button == MouseButtons.Left)
				this.EndAction();

			if (this.camAction == CameraAction.Move && e.Button == MouseButtons.Middle)
				this.camAction = CameraAction.None;
			else if (this.camAction == CameraAction.Rotate && e.Button == MouseButtons.Right)
				this.camAction = CameraAction.None;

			this.InvalidateView();
		}
		private void LocalGLControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.action == ObjectAction.None)
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
					this.camAction = CameraAction.Move;
					this.camActionBeginLocSpace = this.View.CameraObj.Transform.RelativePos;
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.camAction = CameraAction.Rotate;
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

					if (MathF.Sign(e.Delta) != MathF.Sign(curVel))
						curVel = 0.0f;
					else
						curVel *= 1.5f;
					curVel += 0.01f * e.Delta;
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

			this.mouseoverAction = ObjectAction.None;
			this.mouseoverObject = null;
			this.mouseoverSelect = false;

			this.InvalidateView();
		}
		private void LocalGLControl_KeyDown(object sender, KeyEventArgs e)
		{
			bool ctrlPressed = e.Control;
			if (this.actionAllowed)
			{
				if (e.KeyCode == Keys.Delete) this.DeleteObjects(this.actionObjSel);
				else if (e.KeyCode == Keys.C && (Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					List<SelObj> cloneList = this.CloneObjects(this.actionObjSel);
					this.SelectObjects(cloneList);
				}
				else if (!ctrlPressed && e.KeyCode == Keys.Left)		this.MoveSelectionBy(-Vector3.UnitX);
				else if (!ctrlPressed && e.KeyCode == Keys.Right)		this.MoveSelectionBy(Vector3.UnitX);
				else if (!ctrlPressed && e.KeyCode == Keys.Up)			this.MoveSelectionBy(-Vector3.UnitY);
				else if (!ctrlPressed && e.KeyCode == Keys.Down)		this.MoveSelectionBy(Vector3.UnitY);
				else if (!ctrlPressed && e.KeyCode == Keys.Add)			this.MoveSelectionBy(Vector3.UnitZ);
				else if (!ctrlPressed && e.KeyCode == Keys.Subtract)	this.MoveSelectionBy(-Vector3.UnitZ);
				else
				{
					bool axisLockChanged = false;
					if (e.KeyCode == Keys.X) { this.lockedAxes |= AxisLock.X; axisLockChanged = true; }
					if (e.KeyCode == Keys.Y) { this.lockedAxes |= AxisLock.Y; axisLockChanged = true; }
					if (e.KeyCode == Keys.Z) { this.lockedAxes |= AxisLock.Z; axisLockChanged = true; }

					if (axisLockChanged) this.InvalidateView();
				}
			}

			if (this.camActionAllowed)
			{
				if (e.KeyCode == Keys.F)
					this.view.FocusOnObject(DualityEditorApp.Selection.MainGameObject);
				else if (ctrlPressed && e.KeyCode == Keys.Left)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.X = MathF.Round(pos.X - 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
				else if (ctrlPressed && e.KeyCode == Keys.Right)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.X = MathF.Round(pos.X + 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
				else if (ctrlPressed && e.KeyCode == Keys.Up)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.Y = MathF.Round(pos.Y - 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
				else if (ctrlPressed && e.KeyCode == Keys.Down)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.Y = MathF.Round(pos.Y + 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
				else if (ctrlPressed && e.KeyCode == Keys.Add)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.Z = MathF.Round(pos.Z + 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
				else if (ctrlPressed && e.KeyCode == Keys.Subtract)
				{
					this.drawCamGizmoState = CameraAction.Move;
					Vector3 pos = this.View.CameraObj.Transform.Pos;
					pos.Z = MathF.Round(pos.Z - 1.0f);
					this.View.CameraObj.Transform.Pos = pos;
					this.InvalidateView();
				}
			}
		}
		private void LocalGLControl_KeyUp(object sender, KeyEventArgs e)
		{
			bool axisLockChanged = false;
			if (e.KeyCode == Keys.X) { this.lockedAxes &= ~AxisLock.X; axisLockChanged = true; }
			if (e.KeyCode == Keys.Y) { this.lockedAxes &= ~AxisLock.Y; axisLockChanged = true; }
			if (e.KeyCode == Keys.Z) { this.lockedAxes &= ~AxisLock.Z; axisLockChanged = true; }

			if (axisLockChanged) this.InvalidateView();
		}
		private void LocalGLControl_LostFocus(object sender, EventArgs e)
		{
			if (DualityEditorApp.MainForm == null) return;

			this.camAction = CameraAction.None;
			this.EndAction();
			this.lockedAxes = AxisLock.None;
			this.InvalidateView();
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

			this.InvalidateView();
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

		public virtual HelpInfo ProvideHoverHelp(Point localPos, ref bool captured)
		{
			if (this.actionAllowed && this.SelectedObjects.Any())
			{
				return HelpInfo.FromText("Object Action Shortcuts", 
					"Press Delete to remove the selected objects.\n" +
					"Press Ctrl + C to clone the selected objects.\n" +
					"Press Arrow Keys, Add or Subtract to move the selected objects by a fixed step.\n" +
					"Press F to focus on the current selection.\n" +
					"Hold X, Y or Z to limit an action to one or two axes.\n");
			}
			else if (this.camActionAllowed)
			{
				return HelpInfo.FromText("Camera Control Shortcuts", 
					"Hold or scroll the Mouse Wheel to move.\n" +
					"Hold the Right Mouse button to rotate.\n" +
					"Press Ctrl + Arrow Keys, Add or Subtract to move by a fixed step.\n");
			}

			return null;
		}
		public virtual bool PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}
	}
}
