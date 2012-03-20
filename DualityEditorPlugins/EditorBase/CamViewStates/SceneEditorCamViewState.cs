﻿using System;
using System.Collections.Generic;
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
using DualityEditor.CorePluginInterface;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase.CamViewStates
{
	public class SceneEditorCamViewState : CamViewState, IHelpProvider
	{
		public class SelGameObj : SelObj
		{
			private	GameObject	gameObj;

			public override object ActualObject
			{
				get { return this.gameObj == null || this.gameObj.Disposed ? null : this.gameObj; }
			}
			public override bool HasTransform
			{
				get { return this.gameObj != null && !this.gameObj.Disposed && this.gameObj.Transform != null; }
			}
			public override Vector3 Pos
			{
				get { return this.gameObj.Transform.Pos; }
				set { this.gameObj.Transform.Pos = value; this.OnTransformChanged(); }
			}
			public override float Angle
			{
				get { return this.gameObj.Transform.Angle; }
				set { this.gameObj.Transform.Angle = value; this.OnTransformChanged(); }
			}
			public override Vector3 Scale
			{
				get { return this.gameObj.Transform.Scale; }
				set { this.gameObj.Transform.Scale = value; this.OnTransformChanged(); }
			}
			public override float BoundRadius
			{
				get
				{
					ICmpRenderer r = this.gameObj.Renderer;
					if (r == null)
					{
						if (this.gameObj.Transform != null)
							return CamView.DefaultDisplayBoundRadius * this.gameObj.Transform.Scale.Xy.Length;
						else
							return CamView.DefaultDisplayBoundRadius;
					}
					else
						return r.BoundRadius;
				}
			}
			public override bool ShowAngle
			{
				get { return true; }
			}

			public SelGameObj(GameObject obj)
			{
				this.gameObj = obj;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.MoveObj) return true;
				if (action == MouseAction.RotateObj) return true;
				if (action == MouseAction.ScaleObj) return true;
				return false;
			}
			public override void DrawActionGizmo(Canvas canvas, MouseAction action, Point beginLoc, Point curLoc)
			{
				base.DrawActionGizmo(canvas, action, beginLoc, curLoc);
				if (action == MouseAction.MoveObj)
				{
					canvas.DrawText(string.Format("X:{0,7:0}", this.gameObj.Transform.RelativePos.X), curLoc.X + 30, curLoc.Y + 10);
					canvas.DrawText(string.Format("Y:{0,7:0}", this.gameObj.Transform.RelativePos.Y), curLoc.X + 30, curLoc.Y + 18);
					canvas.DrawText(string.Format("Z:{0,7:0}", this.gameObj.Transform.RelativePos.Z), curLoc.X + 30, curLoc.Y + 26);
				}
				else if (action == MouseAction.ScaleObj)
				{
					if (this.gameObj.Transform.RelativeScale.X == this.gameObj.Transform.RelativeScale.Y &&
						this.gameObj.Transform.RelativeScale.X == this.gameObj.Transform.RelativeScale.Z)
					{
						canvas.DrawText(string.Format("Scale:{0,5:0.00}", this.gameObj.Transform.RelativeScale.X), curLoc.X + 30, curLoc.Y + 10);
					}
					else
					{
						canvas.DrawText(string.Format("Scale X:{0,5:0.00}", this.gameObj.Transform.RelativeScale.X), curLoc.X + 30, curLoc.Y + 10);
						canvas.DrawText(string.Format("Scale Y:{0,5:0.00}", this.gameObj.Transform.RelativeScale.Y), curLoc.X + 30, curLoc.Y + 18);
						canvas.DrawText(string.Format("Scale Z:{0,5:0.00}", this.gameObj.Transform.RelativeScale.Z), curLoc.X + 30, curLoc.Y + 26);
					}
				}
				else if (action == MouseAction.RotateObj)
				{
					canvas.DrawText(string.Format("Angle:{0,5:0}", MathF.RadToDeg(this.gameObj.Transform.RelativeAngle)), curLoc.X + 30, curLoc.Y + 10);
				}
			}

			protected void OnTransformChanged()
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState != MainForm.SandboxState.Playing) return;
				this.gameObj.Transform.Vel = Vector3.Zero;
				this.gameObj.Transform.AngleVel = 0.0f;
			}
		}

		private ObjectSelection selBeforeDrag	= null;
		private	DateTime		dragTime		= DateTime.Now;
		private	Point			dragLastLoc		= Point.Empty;

		public override string StateName
		{
			get { return "Scene Editor"; }
		}
		private bool DragMustWait
		{
			get { return this.DragMustWaitProgress < 1.0f; }
		}
		private float DragMustWaitProgress
		{
			get { return MathF.Clamp((float)(DateTime.Now - this.dragTime).TotalMilliseconds / 500.0f, 0.0f, 1.0f); }
		}

		internal protected override void OnEnterState()
		{
			base.OnEnterState();
			this.View.LocalGLControl.DragDrop	+= this.LocalGLControl_DragDrop;
			this.View.LocalGLControl.DragEnter	+= this.LocalGLControl_DragEnter;
			this.View.LocalGLControl.DragLeave	+= this.LocalGLControl_DragLeave;
			this.View.LocalGLControl.DragOver	+= this.LocalGLControl_DragOver;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		+= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;

			// Initial selection update
			ObjectSelection current = EditorBasePlugin.Instance.EditorForm.Selection;
			this.allObjSel = current.GameObjects.Select(g => new SelGameObj(g) as SelObj).ToList();
			{
				var selectedGameObj = current.GameObjects;
				var indirectViaCmpQuery = current.Components.GameObject();
				var indirectViaChildQuery = selectedGameObj.ChildrenDeep();
				var indirectQuery = indirectViaCmpQuery.Concat(indirectViaChildQuery).Except(selectedGameObj).Distinct();
				this.indirectObjSel = indirectQuery.Select(g => new SelGameObj(g) as SelObj).ToList();
			}
			{
				var parentlessGameObj = current.GameObjects.Where(g => !current.GameObjects.Any(g2 => g.IsChildOf(g2))).ToList();
				this.actionObjSel = parentlessGameObj.Select(g => new SelGameObj(g) as SelObj).Where(s => s.HasTransform).ToList();
			}
			this.UpdateSelectionStats();
		}
		internal protected override void OnLeaveState()
		{
			base.OnLeaveState();
			this.View.LocalGLControl.DragDrop	-= this.LocalGLControl_DragDrop;
			this.View.LocalGLControl.DragEnter	-= this.LocalGLControl_DragEnter;
			this.View.LocalGLControl.DragLeave	-= this.LocalGLControl_DragLeave;
			this.View.LocalGLControl.DragOver	-= this.LocalGLControl_DragOver;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		-= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	-= this.EditorForm_ObjectPropertyChanged;

			this.View.LocalGLControl.Cursor = CursorHelper.Arrow;
		}
		protected override void OnSceneChanged()
		{
			base.OnSceneChanged();
			this.UpdateSelectionStats();
		}
		protected override void OnCollectStateOverlayDrawcalls(Canvas canvas)
		{
			base.OnCollectStateOverlayDrawcalls(canvas);
			if (this.DragMustWait && !this.dragLastLoc.IsEmpty)
			{
				canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(this.DragMustWaitProgress);
				canvas.FillCircle(
					this.dragLastLoc.X, 
					this.dragLastLoc.Y, 
					15.0f);
				canvas.CurrentState.ColorTint = ColorRgba.White;
				canvas.DrawCircle(
					this.dragLastLoc.X, 
					this.dragLastLoc.Y, 
					15.0f);
			}
		}

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			Component picked = this.View.PickRendererAt(x, y) as Component;
			if (picked != null) return new SelGameObj(picked.GameObj);
			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			HashSet<ICmpRenderer> picked = this.View.PickRenderersIn(x, y, w, h);
			return picked.OfType<Component>().Select(r => new SelGameObj(r.GameObj) as SelObj).ToList();
		}

		public override void ClearSelection()
		{
			base.ClearSelection();
			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObjCmp);
		}
		public override void SelectObjects(IEnumerable<CamViewState.SelObj> selObjEnum, MainForm.SelectMode mode = MainForm.SelectMode.Set)
		{
			base.SelectObjects(selObjEnum, mode);
			EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selObjEnum.Select(s => s.ActualObject)), mode);
		}
		protected override void PostPerformAction(IEnumerable<CamViewState.SelObj> selObjEnum, CamViewState.MouseAction action)
		{
			base.PostPerformAction(selObjEnum, action);
			if (action == MouseAction.MoveObj)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos);
				}
				else
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeVel,
						ReflectionInfo.Property_Transform_RelativeAngleVel);
				}
			}
			else if (action == MouseAction.RotateObj)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeAngle);
				}
				else
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeAngle,
						ReflectionInfo.Property_Transform_RelativeVel,
						ReflectionInfo.Property_Transform_RelativeAngleVel);
				}
			}
			else if (action == MouseAction.ScaleObj)
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState == MainForm.SandboxState.Playing)
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeScale);
				}
				else
				{
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(
						this,
						new ObjectSelection(selObjEnum.Select(s => (s.ActualObject as GameObject).Transform)),
						ReflectionInfo.Property_Transform_RelativePos,
						ReflectionInfo.Property_Transform_RelativeScale,
						ReflectionInfo.Property_Transform_RelativeVel,
						ReflectionInfo.Property_Transform_RelativeAngleVel);
				}
			}
		}

		public override void DeleteObjects(IEnumerable<SelObj> objEnum)
		{
			var objList = objEnum.Select(s => s.ActualObject as GameObject).ToList();
			if (objList.Count == 0) return;

			// Ask user if he really wants to delete stuff
			if (!this.DisplayConfirmDeleteSelectedObjects()) return;
			if (!EditorBasePlugin.Instance.EditorForm.DisplayConfirmBreakPrefabLink(new ObjectSelection(objList))) return;

			// Delete objects
			foreach (GameObject o in objList)
			{ 
			    if (o.Disposed) continue;
			    o.Dispose(); 
			    Scene.Current.UnregisterObj(o); 
			}
		}
		public override List<SelObj> CloneObjects(IEnumerable<SelObj> objEnum)
		{
			var objList = objEnum.Select(s => s.ActualObject as GameObject).ToList();

			List<SelObj> clones = new List<SelObj>();
			foreach (GameObject o in objList)
			{ 
				if (o.Disposed) continue;
				GameObject clone = o.Clone();
				Scene.Current.RegisterObj(clone); 
				clones.Add(new SelGameObj(clone));
			}
			return clones;
		}

		private void LocalGLControl_DragOver(object sender, DragEventArgs e)
		{
			if (e.Effect == DragDropEffects.None) return;
			if (this.SelObjAction == MouseAction.None && !this.DragMustWait)
				this.DragBeginAction(e);
			
			Point clientCoord = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
			if (Math.Abs(clientCoord.X - this.dragLastLoc.X) > 20 || Math.Abs(clientCoord.Y - this.dragLastLoc.Y) > 20)
				this.dragTime = DateTime.Now;
			this.dragLastLoc = clientCoord;
			this.View.LocalGLControl.Invalidate();

			if (this.SelObjAction != MouseAction.None) this.UpdateAction();
		}
		private void LocalGLControl_DragLeave(object sender, EventArgs e)
		{
			this.dragLastLoc = Point.Empty;
			this.dragTime = DateTime.Now;
			this.View.LocalGLControl.Invalidate();
			if (this.SelObjAction == MouseAction.None) return;
			
			this.EndAction();

			// Destroy temporarily instantiated objects
			foreach (SelObj obj in this.allObjSel)
			{
				GameObject gameObj = obj.ActualObject as GameObject;
				Scene.Current.UnregisterObj(gameObj);
				gameObj.Dispose();
			}
			EditorBasePlugin.Instance.EditorForm.Select(this, this.selBeforeDrag);
		}
		private void LocalGLControl_DragEnter(object sender, DragEventArgs e)
		{
			DataObject data = e.Data as DataObject;
			if (new ConvertOperation(data, ConvertOperation.Operation.All).CanPerform<GameObject>())
			{
				e.Effect = e.AllowedEffect;
				this.dragTime = DateTime.Now;
				this.dragLastLoc = new Point(e.X, e.Y);
			}
			else
			{
				e.Effect = DragDropEffects.None;
				this.dragLastLoc = Point.Empty;
				this.dragTime = DateTime.Now;
			}
		}
		private void LocalGLControl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.SelObjAction == MouseAction.None)
			{
				this.DragBeginAction(e);
				if (this.SelObjAction != MouseAction.None) this.UpdateAction();
			}
			
			this.dragLastLoc = Point.Empty;
			this.dragTime = DateTime.Now;

			if (this.SelObjAction != MouseAction.None) this.EndAction();
		}
		private void DragBeginAction(DragEventArgs e)
		{
			DataObject data = e.Data as DataObject;
			var dragObjQuery = new ConvertOperation(data, ConvertOperation.Operation.All).Perform<GameObject>();
			if (dragObjQuery != null)
			{
				List<GameObject> dragObj = dragObjQuery.ToList();

				Point mouseLoc = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
				Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.View.CameraObj.Transform.Pos.Z + this.View.CameraComponent.ParallaxRefDist));

				// Setup GameObjects
				foreach (GameObject newObj in dragObj)
				{
					if (newObj.Transform != null)
					{
						newObj.Transform.Pos = spaceCoord;
						newObj.Transform.Angle += this.View.CameraObj.Transform.Angle;
					}
					Scene.Current.RegisterObj(newObj);
				}

				// Select them & begin action
				this.selBeforeDrag = EditorBasePlugin.Instance.EditorForm.Selection;
				this.SelectObjects(dragObj.Select(g => new SelGameObj(g) as SelObj));
				this.BeginAction(MouseAction.MoveObj);

				// Get focused
				this.View.LocalGLControl.Focus();

				e.Effect = e.AllowedEffect;
			}
		}

		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Components.Any(c => c is Transform || c is Renderer))
				this.UpdateSelectionStats();
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & ObjectSelection.Category.GameObjCmp) == ObjectSelection.Category.None) return;

			// Update object selection
			this.allObjSel = e.Current.GameObjects.Select(g => new SelGameObj(g) as SelObj).ToList();

			// Update indirect object selection
			{
				var selectedGameObj = e.Current.GameObjects;
				var indirectViaCmpQuery = e.Current.Components.GameObject();
				var indirectViaChildQuery = selectedGameObj.ChildrenDeep();
				var indirectQuery = indirectViaCmpQuery.Concat(indirectViaChildQuery).Except(selectedGameObj).Distinct();
				this.indirectObjSel = indirectQuery.Select(g => new SelGameObj(g) as SelObj).ToList();
			}

			// Update (parent-free) action object selection
			{
				// Remove removed objects
				foreach (SelObj s in e.Removed.GameObjects.Select(g => new SelGameObj(g) as SelObj)) this.actionObjSel.Remove(s);
				// Remove objects whichs parents are now added
				this.actionObjSel.RemoveAll(s => e.Added.GameObjects.Any(o => (s.ActualObject as GameObject).IsChildOf(o)));
				// Add objects whichs parents are not located in the current selection
				var addedParentFreeGameObj = e.Added.GameObjects.Where(o => !this.allObjSel.Any(s => o.IsChildOf(s.ActualObject as GameObject)));
				this.actionObjSel.AddRange(addedParentFreeGameObj.Select(g => new SelGameObj(g) as SelObj).Where(s => s.HasTransform));
			}

			this.UpdateSelectionStats();
			this.OnCursorSpacePosChanged();
			this.View.LocalGLControl.Invalidate();
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			GameObject[] selObj = this.allObjSel.Select(s => s.ActualObject as GameObject).ToArray();
			GameObject mouseoverGameObj = this.MouseoverObject != null ? this.MouseoverObject.ActualObject as GameObject : null;

			if (this.MouseoverObject != null && this.MouseoverSelect)
				result = HelpInfo.FromGameObject(mouseoverGameObj);
			else if (this.MouseoverAction != MouseAction.None && this.MouseoverAction != MouseAction.RectSelection && selObj.Contains(mouseoverGameObj))
				result = HelpInfo.FromGameObject(mouseoverGameObj);
			else if (this.MouseoverAction != MouseAction.None && this.MouseoverAction != MouseAction.RectSelection && selObj.Length == 1)
				result = HelpInfo.FromSelection(new ObjectSelection(selObj));

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}
	}
}
