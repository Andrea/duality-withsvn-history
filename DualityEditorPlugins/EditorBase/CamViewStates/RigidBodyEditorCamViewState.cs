using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using Duality;
using Duality.Components;
using Duality.Components.Physics;
using Duality.Resources;
using Duality.ColorFormat;
using Font = Duality.Resources.Font;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;

namespace EditorBase.CamViewStates
{
	public partial class RigidBodyEditorCamViewState : CamViewState
	{
		public static readonly Cursor ArrowCreateCircle		= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreateCircle, 0, 0);
		public static readonly Cursor ArrowCreatePolygon	= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreatePolygon, 0, 0);
		public static readonly Cursor ArrowCreateEdge		= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreateEdge, 0, 0);
		public static readonly Cursor ArrowCreateLoop		= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreateLoop, 0, 0);

		private enum CursorState
		{
			Normal,
			CreateCircle,
			CreatePolygon,
		//	CreateEdge,
			CreateLoop
		}

		private	CursorState			mouseState			= CursorState.Normal;
		private	int					createPolyIndex		= 0;
		private	RigidBody			selectedBody		= null;
		private	ToolStrip			toolstrip			= null;
		private	ToolStripButton		toolCreateCircle	= null;
		private	ToolStripButton		toolCreatePoly		= null;
	//	private	ToolStripButton		toolCreateEdge		= null;
		private	ToolStripButton		toolCreateLoop		= null;

		public override string StateName
		{
			get { return PluginRes.EditorBaseRes.CamViewState_RigidBodyEditor_Name; }
		}

		public RigidBodyEditorCamViewState()
		{
			this.SetDefaultActiveLayers(
				typeof(CamViewLayers.RigidBodyJointCamViewLayer),
				typeof(CamViewLayers.RigidBodyShapeCamViewLayer));
		}

		protected internal override void OnEnterState()
		{
			base.OnEnterState();

			// Init GUI
			this.View.SuspendLayout();
			this.toolstrip = new ToolStrip();
			this.toolstrip.SuspendLayout();

			this.toolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolstrip.Name = "toolstrip";
			this.toolstrip.Text = "Collider Editor Tools";

			this.toolCreateCircle = new ToolStripButton("Create Circle Shape (C)", EditorBase.PluginRes.EditorBaseRes.IconCmpCircleCollider, this.toolCreateCircle_Clicked);
			this.toolCreateCircle.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolCreateCircle.AutoToolTip = true;
			this.toolstrip.Items.Add(this.toolCreateCircle);

			this.toolCreatePoly = new ToolStripButton("Create Polygon Shape (P)", EditorBase.PluginRes.EditorBaseRes.IconCmpRectCollider, this.toolCreatePoly_Clicked);
			this.toolCreatePoly.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolCreatePoly.AutoToolTip = true;
			this.toolstrip.Items.Add(this.toolCreatePoly);

		//	this.toolCreateEdge = new ToolStripButton("Create Edge Shape (E)", EditorBase.PluginRes.EditorBaseRes.IconCmpEdgeCollider, this.toolCreateEdge_Clicked);
		//	this.toolCreateEdge.DisplayStyle = ToolStripItemDisplayStyle.Image;
		//	this.toolCreateEdge.AutoToolTip = true;
		//	this.toolstrip.Items.Add(this.toolCreateEdge);

			this.toolCreateLoop = new ToolStripButton("Create Loop Shape (L)", EditorBase.PluginRes.EditorBaseRes.IconCmpLoopCollider, this.toolCreateLoop_Clicked);
			this.toolCreateLoop.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolCreateLoop.AutoToolTip = true;
			this.toolstrip.Items.Add(this.toolCreateLoop);

			this.toolstrip.Renderer = new DualityEditor.Controls.ToolStrip.DualitorToolStripProfessionalRenderer();
			this.toolstrip.BackColor = Color.FromArgb(212, 212, 212);

			this.View.Controls.Add(this.toolstrip);
			this.View.Controls.SetChildIndex(this.toolstrip, this.View.Controls.IndexOf(this.View.ToolbarCamera));
			this.toolstrip.ResumeLayout(true);
			this.View.ResumeLayout(true);

			// Register events
			this.View.LocalGLControl.KeyDown += this.LocalGLControl_KeyDown;
			this.View.CurrentCameraChanged += this.View_CurrentCameraChanged;
			MainForm.Instance.SelectionChanged		+= this.EditorForm_SelectionChanged;
			MainForm.Instance.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;

			// Initial update
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(null, this.View.CameraComponent));
			this.selectedBody = this.QuerySelectedCollider();
			this.UpdateSelectionStats();
			this.UpdateToolbar();

			this.View.LockLayer(typeof(CamViewLayers.RigidBodyShapeCamViewLayer));
		}
		protected internal override void OnLeaveState()
		{
			base.OnLeaveState();

			// Cleanup
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(this.View.CameraComponent, null));

			// Unregister events
			this.View.CurrentCameraChanged -= this.View_CurrentCameraChanged;
			this.View.LocalGLControl.KeyDown -= this.LocalGLControl_KeyDown;
			MainForm.Instance.SelectionChanged		-= this.EditorForm_SelectionChanged;
			MainForm.Instance.ObjectPropertyChanged	-= this.EditorForm_ObjectPropertyChanged;

			// Cleanup GUI
			this.toolstrip.Dispose();
			this.toolCreateCircle.Dispose();

			this.toolstrip = null;
			this.toolCreateCircle = null;

			this.View.UnlockLayer(typeof(CamViewLayers.RigidBodyShapeCamViewLayer));
		}
		protected override void DrawStatusText(Canvas canvas, ref bool handled)
		{
			base.DrawStatusText(canvas, ref handled);

			if (!handled && this.mouseState != CursorState.Normal)
			{
				Size viewSize = this.View.LocalGLControl.ClientSize;
				if (this.mouseState == CursorState.CreateCircle)		canvas.DrawText(PluginRes.EditorBaseRes.ColliderEditor_CreateCircle, 10, viewSize.Height - 20);
				else if (this.mouseState == CursorState.CreatePolygon)	canvas.DrawText(PluginRes.EditorBaseRes.ColliderEditor_CreatePolygon, 10, viewSize.Height - 20);
				handled = true;
			}
		}
		protected override void PostPerformAction(IEnumerable<CamViewState.SelObj> selObjEnum, CamViewState.MouseAction action)
		{
			base.PostPerformAction(selObjEnum, action);
			SelShape[] selShapeArray = selObjEnum.OfType<SelShape>().ToArray();

			// Update the body directly after modifying it
			if (this.selectedBody != null) this.selectedBody.SynchronizeBodyShape();

			// Notify property changes
			MainForm.Instance.NotifyObjPropChanged(this,
				new ObjectSelection(this.selectedBody),
				ReflectionInfo.Property_RigidBody_Shapes);
			MainForm.Instance.NotifyObjPropChanged(this, new ObjectSelection(selShapeArray.Select(s => s.ActualObject)));
		}
		protected override void OnCursorSpacePosChanged()
		{
			base.OnCursorSpacePosChanged();

			Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);
			Transform selTransform = this.selectedBody != null && this.selectedBody.GameObj != null ? this.selectedBody.GameObj.Transform : null;
			Vector3 spaceCoord = selTransform != null ? this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, selTransform.Pos.Z)) : Vector3.Zero;
			Vector2 localPos = selTransform != null ? selTransform.GetLocalPoint(spaceCoord).Xy : Vector2.Zero;

			if (this.mouseState != CursorState.Normal) this.UpdateCursorImage();

			if (this.mouseState == CursorState.CreatePolygon && this.allObjSel.Any(sel => sel is SelPolyShape))
			{
				SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
				PolyShapeInfo polyShape = selPolyShape.ActualObject as PolyShapeInfo;
				List<Vector2> vertices = polyShape.Vertices.ToList();

				vertices[this.createPolyIndex] = localPos;

				polyShape.Vertices = vertices.ToArray();
				selPolyShape.UpdatePolyStats();

				MainForm.Instance.NotifyObjPropChanged(this,
					new ObjectSelection(this.selectedBody),
					ReflectionInfo.Property_RigidBody_Shapes);
			}
			else if (this.mouseState == CursorState.CreateLoop && this.allObjSel.Any(sel => sel is SelLoopShape))
			{
				SelLoopShape selPolyShape = this.allObjSel.OfType<SelLoopShape>().First();
				LoopShapeInfo polyShape = selPolyShape.ActualObject as LoopShapeInfo;
				List<Vector2> vertices = polyShape.Vertices.ToList();

				vertices[this.createPolyIndex] = localPos;

				polyShape.Vertices = vertices.ToArray();
				selPolyShape.UpdateLoopStats();

				MainForm.Instance.NotifyObjPropChanged(this,
					new ObjectSelection(this.selectedBody),
					ReflectionInfo.Property_RigidBody_Shapes);
			}
		}
		protected override void OnBeginAction(CamViewState.MouseAction action)
		{
			base.OnBeginAction(action);
			bool shapeAction = 
				action != MouseAction.RectSelection && 
				action != MouseAction.None;
			if (this.selectedBody != null && shapeAction) this.selectedBody.BeginUpdateBodyShape();
		}
		protected override void OnEndAction(CamViewState.MouseAction action)
		{
			base.OnEndAction(action);
			bool shapeAction = 
				action != MouseAction.RectSelection && 
				action != MouseAction.None;
			if (this.selectedBody != null && shapeAction) this.selectedBody.EndUpdateBodyShape();
		}

		protected void UpdateToolbar()
		{
			this.toolCreateCircle.Enabled = this.selectedBody != null && this.mouseState == CursorState.Normal;
			this.toolCreatePoly.Enabled = this.toolCreateCircle.Enabled;
		//	this.toolCreateEdge.Enabled = this.toolCreateCircle.Enabled;
			this.toolCreateLoop.Enabled = this.toolCreateCircle.Enabled && this.selectedBody.BodyType == BodyType.Static;
		}

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			RigidBody pickedCollider = null;
			ShapeInfo pickedShape = null;

			RigidBody[] visibleColliders = this.QueryVisibleColliders().ToArray();
			visibleColliders.StableSort(delegate(RigidBody c1, RigidBody c2) 
			{ 
				return MathF.RoundToInt(1000.0f * (c1.GameObj.Transform.Pos.Z - c2.GameObj.Transform.Pos.Z));
			});

			foreach (RigidBody c in visibleColliders)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, c.GameObj.Transform.Pos.Z));

				// Do a physical picking operation
				pickedShape = this.PickShape(c, worldCoord.Xy);

				// Shape picked.
				if (pickedShape != null)
				{
					pickedCollider = c;
					break;
				}
			}

			if (pickedShape != null) return SelShape.Create(pickedShape);
			if (pickedCollider != null) return new SelBody(pickedCollider);

			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			List<CamViewState.SelObj> result = new List<SelObj>();
			
			RigidBody pickedCollider = null;
			ShapeInfo pickedShape = null;

			RigidBody[] visibleColliders = this.QueryVisibleColliders().ToArray();
			visibleColliders.StableSort(delegate(RigidBody c1, RigidBody c2) 
			{ 
				return MathF.RoundToInt(1000.0f * (c1.GameObj.Transform.Pos.Z - c2.GameObj.Transform.Pos.Z));
			});

			// Pick a collider
			foreach (RigidBody c in visibleColliders)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, c.GameObj.Transform.Pos.Z));
				float scale = this.View.GetScaleAtZ(c.GameObj.Transform.Pos.Z);
				pickedShape = this.PickShapes(c, worldCoord.Xy, new Vector2(w / scale, h / scale)).FirstOrDefault();
				if (pickedShape != null)
				{
					pickedCollider = c;
					result.Add(new SelBody(pickedCollider));
					break;
				}
				else pickedShape = null;
			}

			// Pick shapes
			if (pickedCollider != null)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, pickedCollider.GameObj.Transform.Pos.Z));
				float scale = this.View.GetScaleAtZ(pickedCollider.GameObj.Transform.Pos.Z);
				List<ShapeInfo> picked = this.PickShapes(pickedCollider, worldCoord.Xy, new Vector2(w / scale, h / scale));
				if (picked.Count > 0) result.AddRange(picked.Select(s => SelShape.Create(s) as SelObj));
			}

			return result;
		}

		private ShapeInfo PickShape(RigidBody body, Vector2 worldCoord)
		{
			// Special case for EdgeShapes, because they are by definition unpickable
		//	foreach (EdgeShapeInfo edge in body.Shapes.OfType<EdgeShapeInfo>())
		//	{
		//		Vector2 worldV1 = body.GameObj.Transform.GetWorldPoint(edge.VertexStart);
		//		Vector2 worldV2 = body.GameObj.Transform.GetWorldPoint(edge.VertexEnd);
		//		float dist = MathF.PointLineDistance(worldCoord.X, worldCoord.Y, worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
		//		if (dist < 5.0f) return edge;
		//	}

			// Special case for LoopShapes, because they are by definition unpickable
			foreach (LoopShapeInfo loop in body.Shapes.OfType<LoopShapeInfo>())
			{
				for (int i = 0; i < loop.Vertices.Length; i++)
				{
					Vector2 worldV1 = body.GameObj.Transform.GetWorldPoint(loop.Vertices[i]);
					Vector2 worldV2 = body.GameObj.Transform.GetWorldPoint(loop.Vertices[(i + 1) % loop.Vertices.Length]);
					float dist = MathF.PointLineDistance(worldCoord.X, worldCoord.Y, worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
					if (dist < 5.0f) return loop;
				}
			}

			// Do a physical picking operation
			return body.PickShape(worldCoord);
		}
		private List<ShapeInfo> PickShapes(RigidBody body, Vector2 worldCoord, Vector2 worldSize)
		{
			Rect worldRect = new Rect(worldCoord.X, worldCoord.Y, worldSize.X, worldSize.Y);

			// Do a physical picking operation
			List<ShapeInfo> result = body.PickShapes(worldCoord, worldSize);

			// Special case for EdgeShapes, because they are by definition unpickable
			//foreach (EdgeShapeInfo edge in body.Shapes.OfType<EdgeShapeInfo>())
			//{
			//    Vector2 worldV1 = body.GameObj.Transform.GetWorldPoint(edge.VertexStart);
			//    Vector2 worldV2 = body.GameObj.Transform.GetWorldPoint(edge.VertexEnd);
			//    bool hit = false;
			//    hit = hit || MathF.LinesCross(
			//        worldRect.TopLeft.X, 
			//        worldRect.TopLeft.Y, 
			//        worldRect.TopRight.X, 
			//        worldRect.TopRight.Y, 
			//        worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
			//    hit = hit || MathF.LinesCross(
			//        worldRect.TopLeft.X, 
			//        worldRect.TopLeft.Y, 
			//        worldRect.BottomLeft.X, 
			//        worldRect.BottomLeft.Y, 
			//        worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
			//    hit = hit || MathF.LinesCross(
			//        worldRect.BottomRight.X, 
			//        worldRect.BottomRight.Y, 
			//        worldRect.TopRight.X, 
			//        worldRect.TopRight.Y, 
			//        worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
			//    hit = hit || MathF.LinesCross(
			//        worldRect.BottomRight.X, 
			//        worldRect.BottomRight.Y, 
			//        worldRect.BottomLeft.X, 
			//        worldRect.BottomLeft.Y, 
			//        worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
			//    hit = hit || worldRect.Contains(worldV1) || worldRect.Contains(worldV2);
			//    if (hit)
			//    {
			//        result.Add(edge);
			//        continue;
			//    }
			//}

			// Special case for LoopShapes, because they are by definition unpickable
			foreach (LoopShapeInfo loop in body.Shapes.OfType<LoopShapeInfo>())
			{
				bool hit = false;
				for (int i = 0; i < loop.Vertices.Length; i++)
				{
					Vector2 worldV1 = body.GameObj.Transform.GetWorldPoint(loop.Vertices[i]);
					Vector2 worldV2 = body.GameObj.Transform.GetWorldPoint(loop.Vertices[(i + 1) % loop.Vertices.Length]);
					hit = hit || MathF.LinesCross(
						worldRect.TopLeft.X, 
						worldRect.TopLeft.Y, 
						worldRect.TopRight.X, 
						worldRect.TopRight.Y, 
						worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
					hit = hit || MathF.LinesCross(
						worldRect.TopLeft.X, 
						worldRect.TopLeft.Y, 
						worldRect.BottomLeft.X, 
						worldRect.BottomLeft.Y, 
						worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
					hit = hit || MathF.LinesCross(
						worldRect.BottomRight.X, 
						worldRect.BottomRight.Y, 
						worldRect.TopRight.X, 
						worldRect.TopRight.Y, 
						worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
					hit = hit || MathF.LinesCross(
						worldRect.BottomRight.X, 
						worldRect.BottomRight.Y, 
						worldRect.BottomLeft.X, 
						worldRect.BottomLeft.Y, 
						worldV1.X, worldV1.Y, worldV2.X, worldV2.Y);
					hit = hit || worldRect.Contains(worldV1) || worldRect.Contains(worldV2);
					if (hit) break;
				}
				if (hit)
				{
					result.Add(loop);
					continue;
				}
			}

			return result;
		}

		public override void SelectObjects(IEnumerable<CamViewState.SelObj> selObjEnum, MainForm.SelectMode mode = MainForm.SelectMode.Set)
		{
			base.SelectObjects(selObjEnum, mode);
			if (!selObjEnum.Any()) return;
			
			// Change shape selection
			if (selObjEnum.OfType<SelShape>().Any())
			{
				var shapeQuery = selObjEnum.OfType<SelShape>();
				var distinctShapeQuery = shapeQuery.GroupBy(s => s.Body).First(); // Assure there is only one collider active.
				SelShape[] selShapeArray = distinctShapeQuery.ToArray();

				// First, select the associated Collider
				MainForm.Instance.Select(this, new ObjectSelection(selShapeArray[0].Body), MainForm.SelectMode.Set);
				// Then, select actual ShapeInfos
				MainForm.Instance.Select(this, new ObjectSelection(selShapeArray.Select(s => s.ActualObject)), mode);
			}

			// Change collider selection
			else if (selObjEnum.OfType<SelBody>().Any())
			{
				// Deselect ShapeInfos
				MainForm.Instance.Deselect(this, ObjectSelection.Category.Other);
				// Select Collider
				MainForm.Instance.Select(this, new ObjectSelection(selObjEnum.OfType<SelBody>().Select(s => s.ActualObject)), mode);
			}
		}
		public override void ClearSelection()
		{
			base.ClearSelection();
			MainForm.Instance.Deselect(this, ObjectSelection.Category.GameObjCmp | ObjectSelection.Category.Other);
		}
		public override void DeleteObjects(IEnumerable<CamViewState.SelObj> objEnum)
		{
			if (objEnum.OfType<SelShape>().Any())
			{
				SelShape[] selShapes = objEnum.OfType<SelShape>().ToArray();
				foreach (SelShape selShape in selShapes)
				{
					ShapeInfo shape = selShape.ActualObject as ShapeInfo;
					this.selectedBody.RemoveShape(shape);
				}
				MainForm.Instance.Deselect(this, ObjectSelection.Category.Other);
				MainForm.Instance.NotifyObjPropChanged(this,
					new ObjectSelection(this.selectedBody),
					ReflectionInfo.Property_RigidBody_Shapes);
			}
		}
		public override List<CamViewState.SelObj> CloneObjects(IEnumerable<CamViewState.SelObj> objEnum)
		{
			List<SelObj> result = new List<SelObj>();
			if (objEnum.OfType<SelShape>().Any())
			{
				SelShape[] selShapes = objEnum.OfType<SelShape>().ToArray();
				foreach (SelShape selShape in selShapes)
				{
					ShapeInfo shape = selShape.ActualObject as ShapeInfo;
					shape = shape.Clone();
					this.selectedBody.AddShape(shape);
					result.Add(SelShape.Create(shape));
				}
			}
			return result;
		}

		private void EnterCursorState(CursorState state)
		{
			this.mouseState = state;
			this.createPolyIndex = 0;
			this.selectedBody.BeginUpdateBodyShape();
			this.MouseActionAllowed = false;
			this.View.LocalGLControl.MouseDown += this.LocalGLControl_MouseDown;
			this.UpdateToolbar();
			this.UpdateCursorImage();
			this.View.LocalGLControl.Invalidate();

			if (MainForm.Instance.CurrentSandboxState == MainForm.SandboxState.Playing)
				MainForm.Instance.SandboxPause();
			MainForm.Instance.Deselect(this, ObjectSelection.Category.Other);
		}
		private void LeaveCursorState()
		{
			this.mouseState = CursorState.Normal;
			this.MouseActionAllowed = true;
			this.selectedBody.EndUpdateBodyShape();
			this.View.LocalGLControl.MouseDown -= this.LocalGLControl_MouseDown;
			this.UpdateToolbar();
			this.View.LocalGLControl.Invalidate();
		}
		private void UpdateCursorImage()
		{
			switch (this.mouseState)
			{
				default:						this.View.LocalGLControl.Cursor = CursorHelper.Arrow;	break;
				case CursorState.CreatePolygon:	this.View.LocalGLControl.Cursor = ArrowCreatePolygon;	break;
				case CursorState.CreateLoop:	this.View.LocalGLControl.Cursor = ArrowCreateLoop;		break;
				case CursorState.CreateCircle:	this.View.LocalGLControl.Cursor = ArrowCreateCircle;	break;
			}
		}

		protected IEnumerable<RigidBody> QueryVisibleColliders()
		{
			this.View.MakeDualityTarget();
			IEnumerable<RigidBody> allColliders = Scene.Current.AllObjects.GetComponents<RigidBody>(true);
			IDrawDevice device = this.View.CameraComponent.DrawDevice;
			return allColliders.Where(c => device.IsCoordInView(c.GameObj.Transform.Pos, c.BoundRadius));
		}
		protected RigidBody QuerySelectedCollider()
		{
			return MainForm.Instance.Selection.Components.OfType<RigidBody>().FirstOrDefault();
		}
		protected bool RendererFilter(ICmpRenderer r)
		{
			return (r as Component).GameObj.GetComponent<RigidBody>() != null && (r as Component).Active;
		}
		
		private void View_CurrentCameraChanged(object sender, CamView.CameraChangedEventArgs e)
		{
			if (e.PreviousCamera != null) e.PreviousCamera.RemoveEditorRendererFilter(this.RendererFilter);
			if (e.NextCamera != null) e.NextCamera.AddEditorRendererFilter(this.RendererFilter);
		}
	
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Objects.Any(o => o is Transform || o is RigidBody || o is ShapeInfo))
			{
				// Applying its Prefab invalidates a Collider's ShapeInfos: Deselect them.
				if (e.PrefabApplied)
					MainForm.Instance.Deselect(this, ObjectSelection.Category.Other);
				else
				{
					foreach (SelPolyShape sps in this.allObjSel.OfType<SelPolyShape>()) sps.UpdatePolyStats();
				//	foreach (SelEdgeShape sps in this.allObjSel.OfType<SelEdgeShape>()) sps.UpdateEdgeStats();
					foreach (SelLoopShape sps in this.allObjSel.OfType<SelLoopShape>()) sps.UpdateLoopStats();
					this.UpdateSelectionStats();
				}
				this.UpdateToolbar();
			}
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.SameObjects) return;

			// Collider selection changed
			if ((e.AffectedCategories & ObjectSelection.Category.GameObjCmp) != ObjectSelection.Category.None)
			{
				MainForm.Instance.Deselect(this, ObjectSelection.Category.Other);
				this.selectedBody = this.QuerySelectedCollider();
			}
			// Other selection changed
			if ((e.AffectedCategories & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
			{
				if (e.Current.Objects.OfType<ShapeInfo>().Any())
					this.allObjSel = e.Current.Objects.OfType<ShapeInfo>().Select(s => SelShape.Create(s) as SelObj).ToList();
				else
					this.allObjSel = new List<SelObj>();

				// Update indirect object selection
				this.indirectObjSel.Clear();
				// Update (parent-free) action object selection
				this.actionObjSel = this.allObjSel.ToList();
			}

			this.UpdateSelectionStats();
			this.UpdateToolbar();
			this.View.LocalGLControl.Invalidate();
		}

		private void toolCreateCircle_Clicked(object sender, EventArgs e)
		{
			if (this.selectedBody == null) return;
			this.EnterCursorState(CursorState.CreateCircle);
		}
		private void toolCreatePoly_Clicked(object sender, EventArgs e)
		{
			if (this.selectedBody == null) return;
			this.EnterCursorState(CursorState.CreatePolygon);
		}
		private void toolCreateLoop_Clicked(object sender, EventArgs e)
		{
			if (this.selectedBody == null) return;
			this.EnterCursorState(CursorState.CreateLoop);
		}
		
		private void LocalGLControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (Control.ModifierKeys == Keys.None)
			{
				if (e.KeyCode == Keys.C && this.toolCreateCircle.Enabled)
					this.toolCreateCircle_Clicked(this, EventArgs.Empty);
				else if (e.KeyCode == Keys.P && this.toolCreatePoly.Enabled)
					this.toolCreatePoly_Clicked(this, EventArgs.Empty);
				else if (e.KeyCode == Keys.L && this.toolCreateLoop.Enabled)
					this.toolCreateLoop_Clicked(this, EventArgs.Empty);
			}
		}
		private void LocalGLControl_MouseDown(object sender, MouseEventArgs e)
		{
			Transform selTransform = this.selectedBody.GameObj.Transform;
			Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(e.X, e.Y, selTransform.Pos.Z));
			Vector2 localPos = selTransform.GetLocalPoint(spaceCoord).Xy;

			if (this.mouseState == CursorState.CreateCircle)
			{
				#region CreateCircle
				if (e.Button == MouseButtons.Left)
				{
					CircleShapeInfo newShape = new CircleShapeInfo(16.0f, localPos, 1.0f);
					this.selectedBody.AddShape(newShape);

					MainForm.Instance.NotifyObjPropChanged(this,
						new ObjectSelection(this.selectedBody),
						ReflectionInfo.Property_RigidBody_Shapes);

					this.LeaveCursorState();
					this.SelectObjects(new[] { SelShape.Create(newShape) });
					this.BeginAction(MouseAction.ScaleObj);
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.LeaveCursorState();
				}
				#endregion
			}
			else if (this.mouseState == CursorState.CreatePolygon)
			{
				#region CreatePolygon
				if (e.Button == MouseButtons.Left)
				{
					bool success = false;
					if (!this.allObjSel.Any(sel => sel is SelPolyShape))
					{
						PolyShapeInfo newShape = new PolyShapeInfo(new Vector2[] { localPos, localPos + Vector2.UnitX, localPos + Vector2.One }, 1.0f);
						this.selectedBody.AddShape(newShape);
						this.SelectObjects(new[] { SelShape.Create(newShape) });
						success = true;
					}
					else
					{
						SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
						PolyShapeInfo polyShape = selPolyShape.ActualObject as PolyShapeInfo;
						if (this.createPolyIndex <= 2 || MathF.IsPolygonConvex(polyShape.Vertices))
						{
							List<Vector2> vertices = polyShape.Vertices.ToList();

							vertices[this.createPolyIndex] = localPos;
							if (this.createPolyIndex >= vertices.Count - 1)
								vertices.Add(localPos);

							polyShape.Vertices = vertices.ToArray();
							selPolyShape.UpdatePolyStats();
							success = true;
						}
					}

					if (success)
					{
						this.createPolyIndex++;
						MainForm.Instance.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedBody),
							ReflectionInfo.Property_RigidBody_Shapes);
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					if (this.allObjSel.Any(sel => sel is SelPolyShape))
					{
						SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
						PolyShapeInfo polyShape = selPolyShape.ActualObject as PolyShapeInfo;
						List<Vector2> vertices = polyShape.Vertices.ToList();

						vertices.RemoveAt(this.createPolyIndex);
						if (vertices.Count < 3 || this.createPolyIndex < 2)
						{
							this.DeleteObjects(new SelPolyShape[] { selPolyShape });
						}
						else
						{
							polyShape.Vertices = vertices.ToArray();
							selPolyShape.UpdatePolyStats();
						}

						MainForm.Instance.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedBody),
							ReflectionInfo.Property_RigidBody_Shapes);
					}

					this.LeaveCursorState();
				}
				#endregion
			}
			else if (this.mouseState == CursorState.CreateLoop)
			{
				#region CreateLoop
				if (e.Button == MouseButtons.Left)
				{
					bool success = false;
					if (!this.allObjSel.Any(sel => sel is SelLoopShape))
					{
						LoopShapeInfo newShape = new LoopShapeInfo(new Vector2[] { localPos, localPos + Vector2.UnitX, localPos + Vector2.One });
						this.selectedBody.AddShape(newShape);
						this.SelectObjects(new[] { SelShape.Create(newShape) });
						success = true;
					}
					else
					{
						SelLoopShape selPolyShape = this.allObjSel.OfType<SelLoopShape>().First();
						LoopShapeInfo polyShape = selPolyShape.ActualObject as LoopShapeInfo;
						List<Vector2> vertices = polyShape.Vertices.ToList();

						vertices[this.createPolyIndex] = localPos;
						if (this.createPolyIndex >= vertices.Count - 1)
							vertices.Add(localPos);

						polyShape.Vertices = vertices.ToArray();
						selPolyShape.UpdateLoopStats();
						success = true;
					}

					if (success)
					{
						this.createPolyIndex++;
						MainForm.Instance.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedBody),
							ReflectionInfo.Property_RigidBody_Shapes);
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					if (this.allObjSel.Any(sel => sel is SelLoopShape))
					{
						SelLoopShape selPolyShape = this.allObjSel.OfType<SelLoopShape>().First();
						LoopShapeInfo polyShape = selPolyShape.ActualObject as LoopShapeInfo;
						List<Vector2> vertices = polyShape.Vertices.ToList();

						vertices.RemoveAt(this.createPolyIndex);
						if (vertices.Count < 3 || this.createPolyIndex < 2)
						{
							this.DeleteObjects(new SelLoopShape[] { selPolyShape });
						}
						else
						{
							polyShape.Vertices = vertices.ToArray();
							selPolyShape.UpdateLoopStats();
						}

						MainForm.Instance.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedBody),
							ReflectionInfo.Property_RigidBody_Shapes);
					}

					this.LeaveCursorState();
				}
				#endregion
			}
			//else if (this.mouseState == CursorState.CreateEdge)
			//{
			//    #region CreateEdge
			//    if (e.Button == MouseButtons.Left)
			//    {
			//        bool success = false;
			//        if (!this.allObjSel.Any(sel => sel is SelEdgeShape))
			//        {
			//            EdgeShapeInfo newShape = new EdgeShapeInfo(localPos, localPos + Vector2.UnitX);

			//            this.selectedCollider.AddShape(newShape);
			//            this.SelectObjects(new[] { SelShape.Create(newShape) });
			//            success = true;
			//        }
			//        else
			//        {
			//            SelEdgeShape selEdgeShape = this.allObjSel.OfType<SelEdgeShape>().First();
			//            EdgeShapeInfo edgeShape = selEdgeShape.ActualObject as EdgeShapeInfo;
						
			//            switch (this.createPolyIndex)
			//            {
			//                case 0:	edgeShape.VertexStart = localPos;	break;
			//                case 1:	edgeShape.VertexEnd = localPos;		break;
			//            }

			//            selEdgeShape.UpdateEdgeStats();
			//            success = true;
			//        }

			//        if (success)
			//        {
			//            this.createPolyIndex++;
			//            MainForm.Instance.NotifyObjPropChanged(this,
			//                new ObjectSelection(this.selectedCollider),
			//                ReflectionInfo.Property_RigidBody_Shapes);

			//            if (this.createPolyIndex >= 2)
			//                this.LeaveCursorState();
			//        }
			//    }
			//    else if (e.Button == MouseButtons.Right)
			//    {
			//        if (this.allObjSel.Any(sel => sel is SelEdgeShape))
			//        {
			//            SelEdgeShape selEdgeShape = this.allObjSel.OfType<SelEdgeShape>().First();
			//            EdgeShapeInfo edgeShape = selEdgeShape.ActualObject as EdgeShapeInfo;

			//            if (this.createPolyIndex < 1)
			//                this.DeleteObjects(new SelEdgeShape[] { selEdgeShape });
			//            else
			//                selEdgeShape.UpdateEdgeStats();

			//            MainForm.Instance.NotifyObjPropChanged(this,
			//                new ObjectSelection(this.selectedCollider),
			//                ReflectionInfo.Property_RigidBody_Shapes);
			//        }

			//        this.LeaveCursorState();
			//    }
			//    #endregion
			//}
		}
	}
}
