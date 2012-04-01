using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using Duality;
using Duality.Components;
using Duality.Resources;
using Duality.ColorFormat;
using Font = Duality.Resources.Font;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;

namespace EditorBase.CamViewStates
{
	public partial class ColliderEditorCamViewState : CamViewState
	{
		public static readonly Cursor ArrowCreateCircle		= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreateCircle, 0, 0);
		public static readonly Cursor ArrowCreatePolygon	= CursorHelper.CreateCursor(PluginRes.EditorBaseRes.CursorArrowCreatePolygon, 0, 0);

		private enum CursorState
		{
			Normal,
			CreateCircle,
			CreatePolygon
		}

		private	CursorState			mouseState			= CursorState.Normal;
		private	int					createPolyIndex		= 0;
		private	Collider			selectedCollider	= null;
		private	ToolStrip			toolstrip			= null;
		private	ToolStripButton		toolCreateCircle	= null;
		private	ToolStripButton		toolCreatePoly		= null;
		private	ContentRef<Font>	bigFont				= new ContentRef<Font>(null, "__editor__bigfont__");

		public override string StateName
		{
			get { return "Collider Editor"; }
		}
		public ColorRgba ShapeColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(ColorRgba.Blue, ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(ColorRgba.Blue, ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba ShapeSensorColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(255, 128, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(255, 128, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba ShapeErrorColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(255, 0, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(255, 0, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba JointColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(128, 255, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(128, 255, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}

		protected internal override void OnEnterState()
		{
			base.OnEnterState();

			// Init Resources
			this.RetrieveResources();

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
			this.toolstrip.Renderer = new DualityEditor.Controls.ToolStrip.DualitorToolStripProfessionalRenderer();
			this.toolstrip.BackColor = Color.FromArgb(212, 212, 212);

			this.View.Controls.Add(this.toolstrip);
			this.View.Controls.SetChildIndex(this.toolstrip, this.View.Controls.IndexOf(this.View.ToolbarCamera));
			this.toolstrip.ResumeLayout(true);
			this.View.ResumeLayout(true);

			// Register events
			this.View.LocalGLControl.KeyDown += this.LocalGLControl_KeyDown;
			this.View.CurrentCameraChanged += this.View_CurrentCameraChanged;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged		+= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;

			// Initial update
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(null, this.View.CameraComponent));
			this.selectedCollider = this.QuerySelectedCollider();
			this.UpdateSelectionStats();
			this.UpdateToolbar();

			// Debug:
			//Collider colA = Scene.Current.ActiveObjects.GetComponents<Collider>().Where(c => c.PhysicsBodyType == Collider.BodyType.Dynamic).ElementAtOrDefault(0);
			//Collider colB = Scene.Current.ActiveObjects.GetComponents<Collider>().Where(c => c.PhysicsBodyType == Collider.BodyType.Dynamic).ElementAtOrDefault(1);
			//if (colA != null && colB != null)
			//{
			//    colA.AddJoint(new Collider.WeldJointInfo(), colB);
			//}
		}
		protected internal override void OnLeaveState()
		{
			base.OnLeaveState();

			// Cleanup
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(this.View.CameraComponent, null));

			// Unregister events
			this.View.CurrentCameraChanged -= this.View_CurrentCameraChanged;
			this.View.LocalGLControl.KeyDown -= this.LocalGLControl_KeyDown;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged		-= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	-= this.EditorForm_ObjectPropertyChanged;

			// Cleanup GUI
			this.toolstrip.Dispose();
			this.toolCreateCircle.Dispose();

			this.toolstrip = null;
			this.toolCreateCircle = null;
		}
		protected override void OnCollectStateDrawcalls(Canvas canvas)
		{
			List<Collider> visibleColliders = this.QueryVisibleColliders().ToList();

			this.RetrieveResources();
			canvas.CurrentState.TextFont = this.bigFont;
			Font textFont = canvas.CurrentState.TextFont.Res;

			// Draw Shape layer
			foreach (Collider c in visibleColliders)
			{
				if (!c.Shapes.Any()) continue;
				float colliderAlpha = c == this.selectedCollider ? 1.0f : 0.25f;
				float maxDensity = c.Shapes.Max(s => s.Density);
				float minDensity = c.Shapes.Min(s => s.Density);
				float avgDensity = (maxDensity + minDensity) * 0.5f;
				Vector3 colliderPos = c.GameObj.Transform.Pos;
				Vector2 colliderScale = c.GameObj.Transform.Scale.Xy;
				int index = 0;
				foreach (Collider.ShapeInfo s in c.Shapes)
				{
					Collider.CircleShapeInfo circle = s as Collider.CircleShapeInfo;
					Collider.PolyShapeInfo poly = s as Collider.PolyShapeInfo;
					float shapeAlpha = colliderAlpha * (this.allObjSel.Any(sel => sel.ActualObject == s) ? 1.0f : 0.5f);
					float densityRelative = MathF.Abs(maxDensity - minDensity) < 0.01f ? 1.0f : s.Density / avgDensity;
					ColorRgba clr = s.IsSensor ? this.ShapeSensorColor : this.ShapeColor;
					ColorRgba fontClr = ColorRgba.Mix(clr, this.View.FgColor, 0.5f);
					if (circle != null)
					{
						float uniformScale = colliderScale.Length / MathF.Sqrt(2.0f);
						Vector2 circlePos = circle.Position * colliderScale;
						MathF.TransformCoord(ref circlePos.X, ref circlePos.Y, c.GameObj.Transform.Angle);

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha((0.25f + densityRelative * 0.25f) * colliderAlpha)));
						canvas.FillCircle(
							colliderPos.X + circlePos.X,
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawCircle(
							colliderPos.X + circlePos.X,
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + circlePos.X, 
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					else if (poly != null)
					{
						if (!MathF.IsPolygonConvex(poly.Vertices) && (this.mouseState != CursorState.CreatePolygon || this.createPolyIndex > 2))
							clr = this.ShapeErrorColor;

						Vector2[] polyVert = poly.Vertices.ToArray();
						Vector2 center = Vector2.Zero;
						for (int i = 0; i < polyVert.Length; i++)
						{
							center += polyVert[i];
							Vector2.Multiply(ref polyVert[i], ref colliderScale, out polyVert[i]);
							MathF.TransformCoord(ref polyVert[i].X, ref polyVert[i].Y, c.GameObj.Transform.Angle);
							polyVert[i] += colliderPos.Xy;
						}
						center /= polyVert.Length;
						Vector2.Multiply(ref center, ref colliderScale, out center);
						MathF.TransformCoord(ref center.X, ref center.Y, c.GameObj.Transform.Angle);

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha((0.25f + densityRelative * 0.25f) * colliderAlpha)));
						canvas.FillConvexPolygon(polyVert, colliderPos.Z - 0.01f);
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawConvexPolygon(polyVert, colliderPos.Z - 0.01f);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + center.X, 
							colliderPos.Y + center.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					index++;
				}
			}

			// Draw Joint layer
			List<Collider.JointInfo> visibleJoints = new List<Collider.JointInfo>();
			foreach (Collider c in visibleColliders) visibleJoints.AddRange(c.Joints.Where(j => !visibleJoints.Contains(j)));
			foreach (Collider.JointInfo j in visibleJoints)
			{
				Vector3 colliderPosA = j.ColliderA.GameObj.Transform.Pos;
				Vector2 colliderScaleA = j.ColliderA.GameObj.Transform.Scale.Xy;
				Vector3 colliderPosB = j.ColliderB != null ? j.ColliderB.GameObj.Transform.Pos : Vector3.Zero;
				Vector2 colliderScaleB = j.ColliderB != null ? j.ColliderB.GameObj.Transform.Scale.Xy : Vector2.One;

				float jointAlpha = (j.ColliderA == this.selectedCollider || j.ColliderB == this.selectedCollider) ? 0.5f : 0.25f;
				ColorRgba clr = this.JointColor;

				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(jointAlpha)));
				canvas.FillThickLine(
					colliderPosA.X,
					colliderPosA.Y,
					colliderPosA.Z - 0.01f, 
					colliderPosB.X,
					colliderPosB.Y,
					colliderPosB.Z - 0.01f,
					5.0f);
				canvas.FillCircle(
					colliderPosA.X,
					colliderPosA.Y,
					colliderPosA.Z - 0.01f,
					5.0f);
				canvas.FillCircle(
					colliderPosB.X,
					colliderPosB.Y,
					colliderPosB.Z - 0.01f,
					5.0f);
			}

			base.OnCollectStateDrawcalls(canvas);
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

			// Update shapes internally
			this.selectedCollider.UpdateBody();

			// Notify property changes
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
				new ObjectSelection(this.selectedCollider),
				ReflectionInfo.Property_Collider_Shapes);
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(selShapeArray.Select(s => s.ActualObject)));
		}
		protected override void OnCursorSpacePosChanged()
		{
			base.OnCursorSpacePosChanged();
			if (this.mouseState == CursorState.CreatePolygon && this.allObjSel.Any(sel => sel is SelPolyShape))
			{
				Point mouseLoc = this.View.LocalGLControl.PointToClient(Cursor.Position);
				Transform selTransform = this.selectedCollider.GameObj.Transform;
				Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(mouseLoc.X, mouseLoc.Y, selTransform.Pos.Z));
				Vector2 localPos = selTransform.GetLocalPoint(spaceCoord).Xy;

				SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
				Collider.PolyShapeInfo polyShape = selPolyShape.ActualObject as Collider.PolyShapeInfo;
				List<Vector2> vertices = polyShape.Vertices.ToList();

				vertices[this.createPolyIndex] = localPos;

				polyShape.Vertices = vertices.ToArray();
				selPolyShape.UpdatePolyStats();

				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
					new ObjectSelection(this.selectedCollider),
					ReflectionInfo.Property_Collider_Shapes);
			}
		}

		protected void UpdateToolbar()
		{
			this.toolCreateCircle.Enabled = this.selectedCollider != null && this.mouseState == CursorState.Normal;
			this.toolCreatePoly.Enabled = this.toolCreateCircle.Enabled;
		}

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			Collider pickedCollider = null;
			Collider.ShapeInfo pickedShape = null;
			Collider.JointInfo pickedJoint = null;

			Collider[] visibleColliders = this.QueryVisibleColliders().ToArray();
			visibleColliders.StableSort(delegate(Collider c1, Collider c2) 
			{ 
				return MathF.RoundToInt(1000.0f * (c1.GameObj.Transform.Pos.Z - c2.GameObj.Transform.Pos.Z));
			});

			foreach (Collider c in visibleColliders)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, c.GameObj.Transform.Pos.Z));

				foreach (Collider.JointInfo joint in c.Joints)
				{
					Vector3 jointAnchorA = Vector3.Zero;
					Vector3 jointAnchorB = Vector3.Zero;
					if (joint.ColliderA != null) jointAnchorA = joint.ColliderA.GameObj.Transform.Pos;
					if (joint.ColliderB != null) jointAnchorB = joint.ColliderB.GameObj.Transform.Pos;

					if (MathF.PointLineDistance(worldCoord.X, worldCoord.Y, jointAnchorA.X, jointAnchorA.Y, jointAnchorB.X, jointAnchorB.Y) < 5.0f)
					{
						pickedJoint = joint;
						break;
					}
				}

				if (pickedJoint == null) pickedShape = c.PickShape(worldCoord.Xy);
				if (pickedShape != null || pickedJoint != null)
				{
					pickedCollider = c;
					break;
				}
			}

			if (pickedShape != null) return SelShape.Create(pickedShape);
			if (pickedCollider != null) return new SelCollider(pickedCollider);

			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			List<CamViewState.SelObj> result = new List<SelObj>();
			
			Collider pickedCollider = null;
			Collider.ShapeInfo pickedShape = null;

			Collider[] visibleColliders = this.QueryVisibleColliders().ToArray();
			visibleColliders.StableSort(delegate(Collider c1, Collider c2) 
			{ 
				return MathF.RoundToInt(1000.0f * (c1.GameObj.Transform.Pos.Z - c2.GameObj.Transform.Pos.Z));
			});

			// Pick a collider
			foreach (Collider c in visibleColliders)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, c.GameObj.Transform.Pos.Z));
				float scale = this.View.GetScaleAtZ(c.GameObj.Transform.Pos.Z);
				pickedShape = c.PickShapes(worldCoord.Xy, new Vector2(w / scale, h / scale)).FirstOrDefault();
				if (pickedShape != null)
				{
					pickedCollider = c;
					result.Add(new SelCollider(pickedCollider));
					break;
				}
				else pickedShape = null;
			}

			// Pick shapes
			if (pickedCollider != null)
			{
				Vector3 worldCoord = this.View.GetSpaceCoord(new Vector3(x, y, pickedCollider.GameObj.Transform.Pos.Z));
				float scale = this.View.GetScaleAtZ(pickedCollider.GameObj.Transform.Pos.Z);
				List<Collider.ShapeInfo> picked = pickedCollider.PickShapes(worldCoord.Xy, new Vector2(w / scale, h / scale));
				if (picked.Count > 0) result.AddRange(picked.Select(s => SelShape.Create(s) as SelObj));
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
				SelShape[] selShapeArray = selObjEnum.OfType<SelShape>().ToArray();
				// First, select the associated Collider
				if (this.selectedCollider != selShapeArray[0].Collider)
					EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selShapeArray[0].Collider), MainForm.SelectMode.Set);
				// Then, select actual ShapeInfos
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selShapeArray.Select(s => s.ActualObject)), mode);
			}

			// Change collider selection
			else if (selObjEnum.OfType<SelCollider>().Any())
			{
				// Deselect ShapeInfos
				EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Other);
				// Select Collider
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selObjEnum.OfType<SelCollider>().Select(s => s.ActualObject)), mode);
			}
		}
		public override void ClearSelection()
		{
			base.ClearSelection();
			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObjCmp | ObjectSelection.Category.Other);
		}
		public override void DeleteObjects(IEnumerable<CamViewState.SelObj> objEnum)
		{
			if (objEnum.OfType<SelShape>().Any())
			{
				SelShape[] selShapes = objEnum.OfType<SelShape>().ToArray();
				foreach (SelShape selShape in selShapes)
				{
					Collider.ShapeInfo shape = selShape.ActualObject as Collider.ShapeInfo;
					this.selectedCollider.RemoveShape(shape);
				}
				EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Other);
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
					new ObjectSelection(this.selectedCollider),
					ReflectionInfo.Property_Collider_Shapes);
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
					Collider.ShapeInfo shape = selShape.ActualObject as Collider.ShapeInfo;
					shape = shape.Clone();
					this.selectedCollider.AddShape(shape);
					result.Add(SelShape.Create(shape));
				}
			}
			return result;
		}

		private void EnterCursorState(CursorState state)
		{
			this.mouseState = state;
			this.createPolyIndex = 0;
			this.MouseActionAllowed = false;
			this.View.LocalGLControl.Cursor = state == CursorState.CreateCircle ? ArrowCreateCircle : ArrowCreatePolygon;
			this.View.LocalGLControl.MouseDown += this.LocalGLControl_MouseDown;
			this.UpdateToolbar();
			this.View.LocalGLControl.Invalidate();

			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Other);
		}
		private void LeaveCursorState()
		{
			this.mouseState = CursorState.Normal;
			this.MouseActionAllowed = true;
			this.View.LocalGLControl.Cursor = CursorHelper.Arrow;
			this.View.LocalGLControl.MouseDown -= this.LocalGLControl_MouseDown;
			this.UpdateToolbar();
			this.View.LocalGLControl.Invalidate();
		}

		protected IEnumerable<Collider> QueryVisibleColliders()
		{
			this.View.MakeDualityTarget();
			IEnumerable<Collider> allColliders = Scene.Current.AllObjects.GetComponents<Collider>(true);
			IDrawDevice device = this.View.CameraComponent.DrawDevice;
			return allColliders.Where(c => device.IsCoordInView(c.GameObj.Transform.Pos, c.BoundRadius));
		}
		protected Collider QuerySelectedCollider()
		{
			return EditorBasePlugin.Instance.EditorForm.Selection.Components.OfType<Collider>().FirstOrDefault();
		}
		protected bool RendererFilter(ICmpRenderer r)
		{
			return (r as Component).GameObj.GetComponent<Collider>() != null && (r as Component).Active;
		}

		private void RetrieveResources()
		{
			if (!this.bigFont.IsAvailable)
			{
				Font bigFontRes = new Font();
				bigFontRes.Family = FontFamily.GenericSansSerif.Name;
				bigFontRes.Size = 32;
				bigFontRes.Kerning = true;
				bigFontRes.GlyphRenderHint = Duality.Resources.Font.RenderHint.AntiAlias;
				bigFontRes.ReloadData();
				ContentProvider.RegisterContent("__editor__bigfont__", bigFontRes);
			}
		}
		
		private void View_CurrentCameraChanged(object sender, CamView.CameraChangedEventArgs e)
		{
			if (e.PreviousCamera != null) e.PreviousCamera.RemoveEditorRendererFilter(this.RendererFilter);
			if (e.NextCamera != null) e.NextCamera.AddEditorRendererFilter(this.RendererFilter);
		}
	
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Objects.Any(o => o is Transform || o is Collider || o is Collider.ShapeInfo))
			{
				// Applying its Prefab invalidates a Collider's ShapeInfos: Deselect them.
				if (e.PrefabApplied)
					EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Other);
				else
				{
					foreach (SelPolyShape sps in this.allObjSel.OfType<SelPolyShape>())
						sps.UpdatePolyStats();
					this.UpdateSelectionStats();
				}
			}
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Collider selection changed
			if ((e.AffectedCategories & ObjectSelection.Category.GameObjCmp) != ObjectSelection.Category.None)
			{
				EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Other);
				this.selectedCollider = this.QuerySelectedCollider();
			}
			// Other selection changed
			if ((e.AffectedCategories & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
			{
				if (e.Current.Objects.OfType<Collider.ShapeInfo>().Any())
					this.allObjSel = e.Current.Objects.OfType<Collider.ShapeInfo>().Select(s => SelShape.Create(s) as SelObj).ToList();
				else
					this.allObjSel = new List<SelObj>();

				// Update indirect object selection
				this.indirectObjSel.Clear();
				// Update (parent-free) action object selection
				this.actionObjSel = this.allObjSel.ToList();
			}

			this.UpdateSelectionStats();
			this.UpdateToolbar();
			this.OnCursorSpacePosChanged();
			this.View.LocalGLControl.Invalidate();
		}

		private void toolCreateCircle_Clicked(object sender, EventArgs e)
		{
			if (this.selectedCollider == null) return;
			this.EnterCursorState(CursorState.CreateCircle);
		}
		private void toolCreatePoly_Clicked(object sender, EventArgs e)
		{
			if (this.selectedCollider == null) return;
			this.EnterCursorState(CursorState.CreatePolygon);
		}
		
		private void LocalGLControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (Control.ModifierKeys == Keys.None)
			{
				if (e.KeyCode == Keys.C)
					this.toolCreateCircle_Clicked(this, EventArgs.Empty);
				else if (e.KeyCode == Keys.P)
					this.toolCreatePoly_Clicked(this, EventArgs.Empty);
			}
		}
		private void LocalGLControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.mouseState == CursorState.CreateCircle)
			{
				if (e.Button == MouseButtons.Left)
				{
					Transform selTransform = this.selectedCollider.GameObj.Transform;
					Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(e.X, e.Y, selTransform.Pos.Z));
					Vector2 localPos = selTransform.GetLocalPoint(spaceCoord).Xy;

					Collider.CircleShapeInfo newShape = new Collider.CircleShapeInfo(16.0f, localPos, 1.0f);
					this.selectedCollider.AddShape(newShape);

					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
						new ObjectSelection(this.selectedCollider),
						ReflectionInfo.Property_Collider_Shapes);

					this.LeaveCursorState();
					this.SelectObjects(new[] { SelShape.Create(newShape) });
					this.BeginAction(MouseAction.ScaleObj);
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.LeaveCursorState();
				}
			}
			else if (this.mouseState == CursorState.CreatePolygon)
			{
				if (e.Button == MouseButtons.Left)
				{
					Transform selTransform = this.selectedCollider.GameObj.Transform;
					Vector3 spaceCoord = this.View.GetSpaceCoord(new Vector3(e.X, e.Y, selTransform.Pos.Z));
					Vector2 localPos = selTransform.GetLocalPoint(spaceCoord).Xy;

					bool success = false;
					if (!this.allObjSel.Any(sel => sel is SelPolyShape))
					{
						Collider.PolyShapeInfo newShape = new Collider.PolyShapeInfo(new Vector2[] { localPos, localPos + Vector2.UnitX, localPos + Vector2.One }, 1.0f);
						this.selectedCollider.AddShape(newShape);
						this.SelectObjects(new[] { SelShape.Create(newShape) });
						success = true;
					}
					else
					{
						SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
						Collider.PolyShapeInfo polyShape = selPolyShape.ActualObject as Collider.PolyShapeInfo;
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
						EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedCollider),
							ReflectionInfo.Property_Collider_Shapes);
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					if (this.allObjSel.Any(sel => sel is SelPolyShape))
					{
						SelPolyShape selPolyShape = this.allObjSel.OfType<SelPolyShape>().First();
						Collider.PolyShapeInfo polyShape = selPolyShape.ActualObject as Collider.PolyShapeInfo;
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

						EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
							new ObjectSelection(this.selectedCollider),
							ReflectionInfo.Property_Collider_Shapes);
					}

					this.LeaveCursorState();
				}
			}
		}
	}
}
