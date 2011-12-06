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
		public class SelGameObj : SelObj
		{
			private	GameObject	gameObj;

			public override object ActualObject
			{
				get { return this.gameObj == null || this.gameObj.Disposed ? null : this.gameObj; }
			}
			public override bool HasTransform
			{
				get { return this.gameObj.Transform != null; }
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
					Renderer r = this.gameObj.Renderer;
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

			protected void OnTransformChanged()
			{
				if (EditorBasePlugin.Instance.EditorForm.CurrentSandboxState != MainForm.SandboxState.Playing) return;
				this.gameObj.Transform.Vel = Vector3.Zero;
				this.gameObj.Transform.AngleVel = 0.0f;
			}
		}

		public override string StateName
		{
			get { return "Scene Editor"; }
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

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			Renderer picked = this.View.PickRendererAt(x, y);
			if (picked != null) return new SelGameObj(picked.GameObj);
			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			HashSet<Renderer> picked = this.View.PickRenderersIn(x, y, w, h);
			return picked.Select(r => new SelGameObj(r.GameObj) as SelObj).ToList();
		}

		public override void ClearSelection()
		{
			base.ClearSelection();
			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObject);
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
			var objList = new List<GameObject>(objEnum.Select(s => s.ActualObject as GameObject));
			if (objList.Count == 0) return;

			// Ask user if he really wants to delete stuff
			if (!this.DisplayConfirmDeleteSelectedObjects()) return;
			if (!EditorBasePlugin.Instance.EditorForm.ConfirmBreakPrefabLink(new ObjectSelection(objList))) return;

			// Delete objects
			foreach (GameObject o in objList)
			{ 
			    if (o.Disposed) continue;
			    o.Dispose(); 
			    Scene.Current.Graph.UnregisterObjDeep(o); 
			}
		}
		public override List<SelObj> CloneObjects(IEnumerable<SelObj> objEnum)
		{
			List<SelObj> clones = new List<SelObj>();
			foreach (GameObject o in objEnum.Select(s => s.ActualObject as GameObject))
			{ 
				if (o.Disposed) continue;
				GameObject clone = o.Clone();
				Scene.Current.Graph.RegisterObjDeep(clone); 
				clones.Add(new SelGameObj(clone));
			}
			return clones;
		}

		private void LocalGLControl_DragOver(object sender, DragEventArgs e)
		{
			if (this.SelObjAction == MouseAction.None) return;

			this.UpdateAction();
		}
		private void LocalGLControl_DragLeave(object sender, EventArgs e)
		{
			if (this.SelObjAction == MouseAction.None) return;
			
			this.EndAction();

			// Destroy temporarily instantiated objects
			foreach (SelObj obj in this.allObjSel)
			{
				GameObject gameObj = obj.ActualObject as GameObject;
				gameObj.Dispose();
				Scene.Current.Graph.UnregisterObjDeep(gameObj);
			}
			this.ClearSelection();
		}
		private void LocalGLControl_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;

			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				if (data.ContainsContentRefs<Prefab>())
				{
					ContentRef<Prefab>[] dropdata = data.GetContentRefs<Prefab>();

					Point mouseLoc = this.View.LocalGLControl.PointToClient(new Point(e.X, e.Y));
					Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, this.View.CameraObj.Transform.Pos.Z + this.View.CameraComponent.ParallaxRefDist));

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
					this.SelectObjects(dragObj.Select(g => new SelGameObj(g) as SelObj));
					this.BeginAction(MouseAction.MoveObj);

					// Get focused
					this.View.LocalGLControl.Focus();

					e.Effect = e.AllowedEffect;
				}
			}
		}
		private void LocalGLControl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.SelObjAction == MouseAction.None) return;

			this.EndAction();
		}

		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Components.Any(c => c is Transform || c is Renderer))
				this.UpdateSelectionStats();
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & (ObjectSelection.Category.GameObject | ObjectSelection.Category.Component)) == ObjectSelection.Category.None) return;

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
			this.View.LocalGLControl.Invalidate();
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			GameObject[] selObj = this.allObjSel.Select(s => s.ActualObject as GameObject).ToArray();
			GameObject mouseoverGameObj = this.mouseoverObject != null ? this.mouseoverObject.ActualObject as GameObject : null;

			if (this.mouseoverObject != null && this.mouseoverSelect)
				result = HelpInfo.FromGameObject(mouseoverGameObj);
			else if (this.mouseoverAction != MouseAction.None && this.mouseoverAction != MouseAction.RectSelection && selObj.Contains(mouseoverGameObj))
				result = HelpInfo.FromGameObject(mouseoverGameObj);
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
