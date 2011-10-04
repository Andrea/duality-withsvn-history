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
	public class ColliderEditorCamViewState : CamViewState
	{
		public class SelCollider : SelObj
		{
			private	Collider	collider;

			public override object ActualObject
			{
				get { return this.collider; }
			}
			public override Vector3 Pos
			{
				get { return this.collider.GameObj.Transform.Pos; }
				set { }
			}
			public override float Angle
			{
				get { return this.collider.GameObj.Transform.Angle; }
				set { }
			}
			public override Vector3 Scale
			{
				get { return this.collider.GameObj.Transform.Scale; }
				set { }
			}
			public override float BoundRadius
			{
				get
				{
					Renderer r = this.collider.GameObj.Renderer;
					if (r == null) return CamView.DefaultDisplayBoundRadius;
					else return r.BoundRadius;
				}
			}
			public override bool ShowPos
			{
				get { return false; }
			}

			public SelCollider(Collider obj)
			{
				this.collider = obj;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				return false;
			}
		}
		public abstract class SelShape : SelObj
		{
			private		Collider.ShapeInfo	shape;
			protected	Collider			collider;

			public override object ActualObject
			{
				get { return this.shape; }
			}

			public SelShape(Collider obj, Collider.ShapeInfo shape)
			{
				this.collider = obj;
				this.shape = shape;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.MoveObj) return true;
				if (action == MouseAction.RotateObj) return true;
				if (action == MouseAction.ScaleObj) return true;
				return false;
			}

			public static SelShape Create(Collider collider, Collider.ShapeInfo shape)
			{
				if (shape is Collider.CircleShapeInfo)
				{
					return new SelCircleShape(collider, shape as Collider.CircleShapeInfo);
				}
				else
				{
					throw new NotImplementedException();
				}
			}
		}
		public class SelCircleShape : SelShape
		{
			private	Collider.CircleShapeInfo	circle;

			public override Vector3 Pos
			{
				get
				{
					return this.collider.GameObj.Transform.GetWorldFromLocal(new Vector3(this.circle.Position));
				}
				set
				{
					value.Z = this.collider.GameObj.Transform.Pos.Z;
					this.circle.Position = this.collider.GameObj.Transform.GetLocalFromWorld(value).Xy;
				}
			}
			public override Vector3 Scale
			{
				get
				{
					return Vector3.One * this.circle.Radius;
				}
				set
				{
					this.circle.Radius = value.Length / MathF.Sqrt(3.0f);
				}
			}
			public override float BoundRadius
			{
				get { return this.circle.Radius * this.collider.GameObj.Transform.Scale.Xy.Length / MathF.Sqrt(2.0f); }
			}

			public SelCircleShape(Collider obj, Collider.CircleShapeInfo shape) : base(obj, shape)
			{
				this.circle = shape;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.RotateObj) return false;
				return base.IsActionAvailable(action);
			}
		}

		private	Collider		selectedCollider	= null;
		private	ToolStrip		toolstrip			= null;
		private	ToolStripButton	toolCreateCircle	= null;

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

			this.toolCreateCircle = new ToolStripButton("Create Circle Shape", EditorBase.PluginRes.EditorBaseRes.IconCmpCircleCollider, this.toolCreateCircle_Clicked);
			this.toolCreateCircle.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolCreateCircle.AutoToolTip = true;
			this.toolstrip.Items.Add(this.toolCreateCircle);

			this.View.Controls.Add(this.toolstrip);
			this.View.Controls.SetChildIndex(this.toolstrip, 0);
			this.toolstrip.ResumeLayout(true);
			this.View.ResumeLayout(true);

			// Register events
			this.View.CurrentCameraChanged += this.View_CurrentCameraChanged;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged		+= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;

			// Initial update
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(null, this.View.CameraComponent));
			this.selectedCollider = this.QuerySelectedCollider();
		}
		protected internal override void OnLeaveState()
		{
			base.OnLeaveState();

			// Cleanup
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(this.View.CameraComponent, null));

			// Unregister events
			this.View.CurrentCameraChanged -= this.View_CurrentCameraChanged;
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
			base.OnCollectStateDrawcalls(canvas);
			List<Collider> visibleColliders = this.QueryVisibleColliders().ToList();

			foreach (Collider c in visibleColliders)
			{
				float colliderAlpha = c == this.selectedCollider ? 1.0f : 0.25f;
				float maxDensity = c.Shapes.Max(s => s.Density);
				float minDensity = c.Shapes.Min(s => s.Density);
				Vector3 colliderPos = c.GameObj.Transform.Pos;
				Vector2 colliderScale = c.GameObj.Transform.Scale.Xy;
				foreach (Collider.ShapeInfo s in c.Shapes)
				{
					Collider.CircleShapeInfo circle = s as Collider.CircleShapeInfo;
					Collider.PolyShapeInfo poly = s as Collider.PolyShapeInfo;
					float shapeAlpha = colliderAlpha * (this.allObjSel.Any(sel => sel.ActualObject == s) ? 1.0f : 0.5f);
					float densityRelative = MathF.Abs(maxDensity - minDensity) < 0.01f ? 0.5f : (s.Density - minDensity) / (maxDensity - minDensity);
					if (circle != null)
					{
						float uniformScale = colliderScale.Length / MathF.Sqrt(2.0f);
						Vector2 circlePos = circle.Position;
						MathF.TransformCoord(ref circlePos.X, ref circlePos.Y, c.GameObj.Transform.Angle);
						circlePos.X *= colliderScale.X;
						circlePos.Y *= colliderScale.Y;
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, this.ShapeColor.WithAlpha((0.25f + densityRelative * 0.5f) * colliderAlpha)));
						canvas.FillCircle(
							colliderPos.X + circlePos.X, 
							colliderPos.Y + circlePos.Y, 
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, this.ShapeColor.WithAlpha(shapeAlpha)));
						canvas.DrawCircle(
							colliderPos.X + circlePos.X, 
							colliderPos.Y + circlePos.Y, 
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);
					}
					else if (poly != null)
					{
						throw new NotImplementedException();
					}
				}
			}
		}
		protected override void PostPerformAction(IEnumerable<CamViewState.SelObj> selObjEnum, CamViewState.MouseAction action)
		{
			base.PostPerformAction(selObjEnum, action);

			// Update shapes internally by re-assigning them
			this.selectedCollider.Shapes = this.selectedCollider.Shapes;
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
				new ObjectSelection(this.selectedCollider),
				ReflectionInfo.Property_Collider_Shapes);
		}

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			if (this.selectedCollider != null)
			{
				// Pick a shape
				Collider.ShapeInfo picked = this.selectedCollider.PickShape(this.View.GetSpaceCoord(new Vector3(x, y, this.selectedCollider.GameObj.Transform.Pos.Z)).Xy);
				if (picked != null) return SelShape.Create(this.selectedCollider, picked);
			}

			// Pick a collider
			{
				Renderer picked = this.View.PickRendererAt(x, y);
				Collider collider = null;
				if (picked != null) collider = picked.GameObj.GetComponent<Collider>();
				if (collider != null && collider != this.selectedCollider) return new SelCollider(collider);
			}

			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			if (this.selectedCollider != null)
			{
				// Pick a shape
				HashSet<Collider.ShapeInfo> picked = this.selectedCollider.PickShapes(this.View.GetSpaceCoord(new Vector3(x, y, this.selectedCollider.GameObj.Transform.Pos.Z)).Xy, new Vector2(w, h));
				if (picked.Count > 0) return picked.Select(s => SelShape.Create(this.selectedCollider, s) as SelObj).ToList();
			}

			// Pick a collider
			{
				HashSet<Renderer> picked = this.View.PickRenderersIn(x, y, w, h);
				foreach (Renderer r in picked)
				{
					Collider c = r.GameObj.GetComponent<Collider>();
					if (c == null) continue;
					if (c == this.selectedCollider) continue;
					return new List<SelObj>{ new SelCollider(c) };
				}
			}

			return new List<SelObj>();
		}

		public override void SelectObjects(IEnumerable<CamViewState.SelObj> selObjEnum, MainForm.SelectMode mode = MainForm.SelectMode.Set)
		{
			base.SelectObjects(selObjEnum, mode);

			// Change collider selection globally.
			if (selObjEnum.OfType<SelCollider>().Any())
			{
				this.allObjSel.Clear();
				this.indirectObjSel.Clear();
				this.actionObjSel.Clear();
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selObjEnum.OfType<SelCollider>().Select(s => s.ActualObject)), mode);
			}
			// Change shape selection locally
			else
			{
				var selShapeEnum = selObjEnum.OfType<SelShape>();

				if (mode == MainForm.SelectMode.Set)
				{
					this.allObjSel.Clear();
					this.allObjSel.AddRange(selShapeEnum);
				}
				else if (mode == MainForm.SelectMode.Append)
					this.allObjSel.AddRange(selShapeEnum);
				else if (mode == MainForm.SelectMode.Toggle)
				{
					var mutual = selObjEnum.Union(this.allObjSel).ToArray();
					this.allObjSel.RemoveAll(s => mutual.Contains(s));
					this.allObjSel.AddRange(selObjEnum.Except(mutual));
				}

				this.actionObjSel.Clear();
				this.actionObjSel.AddRange(this.allObjSel);
				this.indirectObjSel.Clear();
			}

			this.UpdateSelectionStats();
		}
		public override void ClearSelection()
		{
			base.ClearSelection();

			this.allObjSel.Clear();
			this.indirectObjSel.Clear();
			this.actionObjSel.Clear();

			this.UpdateSelectionStats();
		}

		protected IEnumerable<Collider> QueryVisibleColliders()
		{
			this.View.MakeDualityTarget();
			IEnumerable<Collider> allColliders = Scene.Current.Graph.AllObjects.GetComponents<Collider>();
			IDrawDevice device = this.View.CameraComponent.DrawDevice;
			return allColliders.Where(c => device.IsCoordInView(c.GameObj.Transform.Pos, c.BoundRadius));
		}
		protected Collider QuerySelectedCollider()
		{
			return EditorBasePlugin.Instance.EditorForm.Selection.Components.OfType<Collider>().FirstOrDefault();
		}
		protected IEnumerable<Collider.ShapeInfo> SelectedShapes()
		{
			if (this.selectedCollider == null) yield break;
			foreach (Collider.ShapeInfo s in this.selectedCollider.Shapes)
				yield return s;
		}
		protected bool RendererFilter(Renderer r)
		{
			return r.GameObj.GetComponent<Collider>() != null;
		}
		
		private void View_CurrentCameraChanged(object sender, CamView.CameraChangedEventArgs e)
		{
			if (e.PreviousCamera != null) e.PreviousCamera.RemoveEditorRendererFilter(this.RendererFilter);
			if (e.NextCamera != null) e.NextCamera.AddEditorRendererFilter(this.RendererFilter);
		}
	
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.Objects.Components.Any(c => c is Transform || c is Collider))
				this.UpdateSelectionStats();
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & ObjectSelection.Category.Component) == ObjectSelection.Category.None) return;

			// Collider selection changed
			this.selectedCollider = this.QuerySelectedCollider();
			
			this.UpdateSelectionStats();
			this.View.LocalGLControl.Invalidate();
		}

		private void toolCreateCircle_Clicked(object sender, EventArgs e)
		{

		}
	}
}
