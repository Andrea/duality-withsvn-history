using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace EditorBase
{
	public class SceneEditorCamViewState : CamViewState, IHelpProvider
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
		public enum MouseAction
		{
			None,
			RectSelection,
			MoveObj,
			RotateObj,
			ScaleObj
		}
		
		private	Point				actionBeginLoc		= Point.Empty;
		private Vector3				actionBeginLocSpace	= Vector3.Zero;
		private	AxisLock			actionAxisLock		= AxisLock.None;
		private MouseAction			action				= MouseAction.None;
		private	MouseAction			mouseoverAction		= MouseAction.None;
		private	GameObject			mouseoverObject		= null;
		private	bool				mouseoverSelect		= false;
		private	Vector3				selectionCenter	= Vector3.Zero;
		private	float				selectionRadius	= 0.0f;
		private	ObjectSelection		activeRectSel	= new ObjectSelection();
		private	List<GameObject>	parentFreeSel	= new List<GameObject>();

		public override string StateName
		{
			get { return "Scene Editor"; }
		}

		internal protected override void OnEnterState()
		{
			base.OnEnterState();
			this.View.LocalGLControl.MouseDown	+= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	+= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	+= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel	+= this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.LostFocus	+= this.LocalGLControl_LostFocus;
			this.View.LocalGLControl.KeyDown	+= this.LocalGLControl_KeyDown;
			this.View.LocalGLControl.DragDrop	+= this.LocalGLControl_DragDrop;
			this.View.LocalGLControl.DragEnter	+= this.LocalGLControl_DragEnter;
			this.View.LocalGLControl.DragLeave	+= this.LocalGLControl_DragLeave;
			this.View.LocalGLControl.DragOver	+= this.LocalGLControl_DragOver;
			this.View.ParallaxRefDistChanged	+= this.View_ParallaxRefDistChanged;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		+= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;
		}
		internal protected override void OnLeaveState()
		{
			base.OnLeaveState();
			this.View.LocalGLControl.MouseDown	-= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	-= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	-= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel	-= this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.LostFocus	-= this.LocalGLControl_LostFocus;
			this.View.LocalGLControl.KeyDown	-= this.LocalGLControl_KeyDown;
			this.View.LocalGLControl.DragDrop	-= this.LocalGLControl_DragDrop;
			this.View.LocalGLControl.DragEnter	-= this.LocalGLControl_DragEnter;
			this.View.LocalGLControl.DragLeave	-= this.LocalGLControl_DragLeave;
			this.View.LocalGLControl.DragOver	-= this.LocalGLControl_DragOver;
			this.View.ParallaxRefDistChanged	-= this.View_ParallaxRefDistChanged;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		-= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	-= this.EditorForm_ObjectPropertyChanged;

			this.View.LocalGLControl.Cursor = CursorHelper.Arrow;
		}
		protected override void OnPrepareDrawState()
		{
			base.OnPrepareDrawState();

			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);

			// Determine turned camera axes
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(this.View.CameraObj.Transform.Angle, out catDotX, out catDotY);
			Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
			Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
			MathF.TransformDotVec(ref right, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref down, ref catDotX, ref catDotY);

			// Draw indirectly selected object overlay
			this.DrawSelectionMarkers(this.SelectedGameObjIndirect(), ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.75f));

			// Draw selected object overlay
			List<GameObject> selObjList = new List<GameObject>(this.SelectedGameObj());
			this.DrawSelectionMarkers(selObjList, this.View.FgColor);

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
						ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.5f));
				}
				else
				{
					this.DrawWorldSpaceCircle(
						this.selectionCenter.X, 
						this.selectionCenter.Y, 
						this.selectionCenter.Z, 
						this.selectionRadius,
						DrawTechnique.Solid,
						ColorRgba.Mix(this.View.FgColor, this.View.BgColor, 0.5f));
				}
			}

			// Draw scale action dots
			if (selObjList.Count > 0)
			{
				float dotR = 3.0f / this.View.GetScaleAtZ(this.selectionCenter.Z);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X + this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.View.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X - this.selectionRadius, 
					this.selectionCenter.Y, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.View.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X, 
					this.selectionCenter.Y + this.selectionRadius, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.View.FgColor);
				this.DrawWorldSpaceDot(
					this.selectionCenter.X, 
					this.selectionCenter.Y - this.selectionRadius, 
					this.selectionCenter.Z,
					dotR,
					DrawTechnique.Solid,
					this.View.FgColor);
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
						ColorRgba.Mix(this.View.FgColor, ColorRgba.Red, 0.5f));
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
						ColorRgba.Mix(this.View.FgColor, ColorRgba.Green, 0.5f));
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
						ColorRgba.Mix(this.View.FgColor, ColorRgba.Blue, 0.5f));
				}
			}

			// Draw camera movement
			this.DrawCamMoveIndicators();

			// Draw rect selection
			if (this.action == MouseAction.RectSelection)
			{
				this.DrawViewSpaceLine(this.actionBeginLoc.X, this.actionBeginLoc.Y, cursorPos.X, this.actionBeginLoc.Y, DrawTechnique.Solid, this.View.FgColor);
				this.DrawViewSpaceLine(cursorPos.X, this.actionBeginLoc.Y, cursorPos.X, cursorPos.Y, DrawTechnique.Solid, this.View.FgColor);
				this.DrawViewSpaceLine(cursorPos.X, cursorPos.Y, this.actionBeginLoc.X, cursorPos.Y, DrawTechnique.Solid, this.View.FgColor);
				this.DrawViewSpaceLine(this.actionBeginLoc.X, cursorPos.Y, this.actionBeginLoc.X, this.actionBeginLoc.Y, DrawTechnique.Solid, this.View.FgColor);
			}
		}
		protected override void OnUpdateState()
		{
			base.OnUpdateState();

			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);
			if (this.CameraTransformChanged)
			{
				if (this.action == MouseAction.RectSelection) this.UpdateRectSelection(cursorPos);
				else if (this.action == MouseAction.MoveObj) this.UpdateObjMove(cursorPos);
				else if (this.action == MouseAction.RotateObj) this.UpdateObjRotate(cursorPos);
				else if (this.action == MouseAction.ScaleObj) this.UpdateObjScale(cursorPos);
				else
				{
					this.UpdateMouseover(cursorPos);
					this.UpdateSelectionStats();
				}
			}
			else if (DualityApp.ExecContext == DualityApp.ExecutionContext.Launcher)
				this.UpdateSelectionStats();
		}
		protected override void OnSceneChanged()
		{
			base.OnSceneChanged();
			this.UpdateSelectionStats();
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
			Renderer picked = this.View.PickRendererAt(mouseLoc.X, mouseLoc.Y);
			if (picked == null) return false;

			return this.SelectedGameObj().Contains(picked.GameObj);
		}
		protected void UpdateMouseover(Point mouseLoc)
		{
			bool lastMouseoverSelect = this.mouseoverSelect;
			GameObject lastMouseoverObject = this.mouseoverObject;

			// Determine object at mouse position
			Renderer mouseoverRenderer = this.View.PickRendererAt(mouseLoc.X, mouseLoc.Y);
			this.mouseoverObject = mouseoverRenderer != null ? mouseoverRenderer.GameObj : null;

			// Determine action variables
			Vector3 mouseSpaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
			float scale = this.View.GetScaleAtZ(this.selectionCenter.Z);
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
				this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionMove;
			else if (this.mouseoverAction == MouseAction.RotateObj)
				this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionRotate;
			else if (this.mouseoverAction == MouseAction.ScaleObj)
				this.View.LocalGLControl.Cursor = CursorHelper.ArrowActionScale;
			else
				this.View.LocalGLControl.Cursor = CursorHelper.Arrow;

			// Redraw if mouseover changed
			if (this.mouseoverObject != lastMouseoverObject || 
				this.mouseoverSelect != lastMouseoverSelect)
				this.View.LocalGLControl.Invalidate();
		}
		protected void BeginAction(MouseAction action, Point mouseLoc)
		{
			this.actionBeginLoc = mouseLoc;
			this.action = action;

			if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				EditorBasePlugin.Instance.EditorForm.SandboxSceneStartFreeze();

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
		}
		protected void EndAction(Point mouseLoc)
		{
			if (this.action == MouseAction.RectSelection)
			{
				this.activeRectSel = new ObjectSelection();
			}

			if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				EditorBasePlugin.Instance.EditorForm.SandboxSceneStopFreeze();

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
		protected void UpdateAxisLockInfo()
		{
			this.View.ToolLabelAxisX.Enabled = (this.actionAxisLock & AxisLock.X) != AxisLock.None;
			this.View.ToolLabelAxisY.Enabled = (this.actionAxisLock & AxisLock.Y) != AxisLock.None;
			this.View.ToolLabelAxisZ.Enabled = (this.actionAxisLock & AxisLock.Z) != AxisLock.None;
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
			HashSet<Renderer> picked = this.View.PickRenderersIn(pX, pY, pW, pH);

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

			this.View.LocalGLControl.Invalidate();
		}
		protected void UpdateObjMove(Point mouseLoc)
		{
			float zMovement = this.View.CameraObj.Transform.Pos.Z - this.actionBeginLocSpace.Z;
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z + zMovement));
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
			this.actionBeginLocSpace.Z = this.View.CameraObj.Transform.Pos.Z;
			this.View.LocalGLControl.Invalidate();
		}
		protected void UpdateObjRotate(Point mouseLoc)
		{
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
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
			this.View.LocalGLControl.Invalidate();
		}
		protected void UpdateObjScale(Point mouseLoc)
		{
			if (this.selectionRadius == 0.0f) return;

			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.selectionCenter.Z));
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
			this.View.LocalGLControl.Invalidate();
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
				else boundRad = CamView.DefaultDisplayBoundRadius;

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

		private void LocalGLControl_MouseMove(object sender, MouseEventArgs e)
		{
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
		private void LocalGLControl_MouseWheel(object sender, MouseEventArgs e)
		{
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
		private void LocalGLControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.action == MouseAction.RectSelection && this.actionBeginLoc == e.Location)
				this.UpdateRectSelection(e.Location);

			if (e.Button == MouseButtons.Left)
				this.EndAction(e.Location);

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
						if (!this.SelectedGameObj().Contains(this.mouseoverObject))
							this.SelectGameObj(new ObjectSelection(this.mouseoverObject));
					}
					this.BeginAction(this.mouseoverAction, e.Location);
				}
			}
		}
		private void LocalGLControl_LostFocus(object sender, EventArgs e)
		{
			this.EndAction(this.View.LocalGLControl.PointToClient(Cursor.Position));
			this.View.LocalGLControl.Invalidate();
		}
		private void LocalGLControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.DeleteObjects(this.SelectedGameObj());
			}
			else if (e.KeyCode == Keys.C && (Control.ModifierKeys & Keys.Control) != Keys.None)
			{
				List<GameObject> cloneList = this.CloneObjects(this.SelectedGameObj());
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(cloneList));
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
					this.View.LocalGLControl.Invalidate();
				}
			}
		}
		private void LocalGLControl_DragOver(object sender, DragEventArgs e)
		{
			if (this.action == MouseAction.None) return;

			Point mouseLoc = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
			this.UpdateObjMove(mouseLoc);
		}
		private void LocalGLControl_DragLeave(object sender, EventArgs e)
		{
			if (this.action == MouseAction.None) return;
			
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);
			this.EndAction(mouseLoc);

			// Destroy temporarily instantiated objects
			foreach (GameObject obj in this.SelectedGameObj())
			{
				obj.Dispose();
				Scene.Current.Graph.UnregisterObjDeep(obj);
			}
			this.ClearSelection();
		}
		private void LocalGLControl_DragEnter(object sender, DragEventArgs e)
		{
			Point mouseLoc = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.View.CameraObj.Transform.Pos.Z + this.View.CameraComponent.ParallaxRefDist));

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
							newObj.Transform.Angle += this.View.CameraObj.Transform.Angle;
						}
						Scene.Current.Graph.RegisterObjDeep(newObj);
						dragObj.Add(newObj);
					}

					// Select them & begin action
					this.SelectGameObj(new ObjectSelection(dragObj));
					this.BeginAction(MouseAction.MoveObj, mouseLoc);

					// Get focused
					this.View.LocalGLControl.Focus();

					e.Effect = e.AllowedEffect;
				}
			}
		}
		private void LocalGLControl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.action == MouseAction.None) return;

			Point mouseLoc = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
			this.EndAction(mouseLoc);
		}
		private void View_ParallaxRefDistChanged(object sender, EventArgs e)
		{
			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);
			if (this.action == MouseAction.MoveObj)
				this.UpdateObjMove(mouseLoc);
			else if (this.action == MouseAction.RotateObj)
				this.UpdateObjRotate(mouseLoc);
			else if (this.action == MouseAction.ScaleObj)
				this.UpdateObjScale(mouseLoc);
		}

		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Components.Any(c => c is Transform || c is Renderer))
				this.UpdateSelectionStats();
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
			this.View.LocalGLControl.Invalidate();
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			GameObject[] selObj = this.SelectedGameObj().ToArray();

			if (this.mouseoverObject != null && this.mouseoverSelect)
				result = HelpInfo.FromGameObject(this.mouseoverObject);
			else if (this.mouseoverAction != MouseAction.None && this.mouseoverAction != MouseAction.RectSelection && selObj.Contains(this.mouseoverObject))
				result = HelpInfo.FromGameObject(this.mouseoverObject);
			else if (this.mouseoverAction != MouseAction.None && this.mouseoverAction != MouseAction.RectSelection && selObj.Length == 1)
				result = HelpInfo.FromSelection(new ObjectSelection(selObj));

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}
	}
}
