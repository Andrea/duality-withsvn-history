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

			public SelCollider(Collider obj)
			{
				this.collider = obj;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				return false;
			}
		}

		private	Collider	selectedCollider	= null;

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
			this.View.CurrentCameraChanged += this.View_CurrentCameraChanged;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		+= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	+= this.EditorForm_ObjectPropertyChanged;

			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(null, this.View.CameraComponent));
		}
		protected internal override void OnLeaveState()
		{
			this.View_CurrentCameraChanged(this, new CamView.CameraChangedEventArgs(this.View.CameraComponent, null));

			base.OnLeaveState();
			this.View.CurrentCameraChanged -= this.View_CurrentCameraChanged;

			EditorBasePlugin.Instance.EditorForm.SelectionChanged		-= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged	-= this.EditorForm_ObjectPropertyChanged;
		}
		protected override void OnPrepareDrawState()
		{
			base.OnPrepareDrawState();
			List<Collider> visibleColliders = this.QueryVisibleColliders().ToList();

			foreach (Collider c in visibleColliders)
			{
				float maxDensity = c.Shapes.Max(s => s.Density);
				float minDensity = c.Shapes.Min(s => s.Density);
				Vector3 colliderPos = c.GameObj.Transform.Pos;
				Vector2 colliderScale = c.GameObj.Transform.Scale.Xy;
				foreach (Collider.ShapeInfo s in c.Shapes)
				{
					Collider.CircleShapeInfo circle = s as Collider.CircleShapeInfo;
					Collider.PolyShapeInfo poly = s as Collider.PolyShapeInfo;
					float densityRelative = MathF.Abs(maxDensity - minDensity) < 0.01f ? 0.5f : (s.Density - minDensity) / (maxDensity - minDensity);
					if (circle != null)
					{
						float uniformScale = colliderScale.Length / MathF.Sqrt(2.0f);
						this.FillWorldSpaceCircle(
							colliderPos.X + circle.Position.X, 
							colliderPos.Y + circle.Position.Y, 
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale,
							DrawTechnique.Alpha, 
							this.ShapeColor.WithAlpha(0.25f + densityRelative * 0.5f));
						this.DrawWorldSpaceCircle(
							colliderPos.X + circle.Position.X, 
							colliderPos.Y + circle.Position.Y, 
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale,
							DrawTechnique.Mask, 
							this.ShapeColor);
					}
					else if (poly != null)
					{
						throw new NotImplementedException();
					}
				}
			}
		}

		public override CamViewState.SelObj PickSelObjAt(int x, int y)
		{
			Renderer picked = this.View.PickRendererAt(x, y);
			Collider collider = null;
			if (picked != null) collider = picked.GameObj.GetComponent<Collider>();
			if (collider != null) return new SelCollider(collider);
			return null;
		}
		public override List<CamViewState.SelObj> PickSelObjIn(int x, int y, int w, int h)
		{
			HashSet<Renderer> picked = this.View.PickRenderersIn(x, y, w, h);
			return picked.Where(r => r.GameObj.GetComponent<Collider>() != null).Select(r => new SelCollider(r.GameObj.GetComponent<Collider>()) as SelObj).ToList();
		}

		public override void SelectObjects(IEnumerable<CamViewState.SelObj> selObjEnum, MainForm.SelectMode mode = MainForm.SelectMode.Set)
		{
			base.SelectObjects(selObjEnum, mode);
			EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selObjEnum.Select(s => s.ActualObject)), mode);
		}
		public override void ClearSelection()
		{
			base.ClearSelection();
			EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Component);
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
			Collider c = null;
			if (c == null) c = EditorBasePlugin.Instance.EditorForm.Selection.GameObjects.Select(g => g.GetComponent<Collider>()).NotNull().FirstOrDefault();
			if (c == null) c = EditorBasePlugin.Instance.EditorForm.Selection.Components.OfType<Collider>().FirstOrDefault();
			return c;
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

			this.selectedCollider = this.QuerySelectedCollider();

			// Update object selection
			this.allObjSel = e.Current.Components.Where(c => c is Collider).Select(c => new SelCollider(c as Collider) as SelObj).ToList();

			// Update indirect object selection
			{
				var indirectViaCmpQuery = e.Current.GameObjects.GetComponents<Collider>();
				var indirectQuery = indirectViaCmpQuery.Except(e.Current.Components).Distinct();
				this.indirectObjSel = indirectQuery.Select(c => new SelCollider(c as Collider) as SelObj).ToList();
			}

			// Update (parent-free) action object selection
			this.actionObjSel.AddRange(this.allObjSel);

			this.UpdateSelectionStats();
			this.View.LocalGLControl.Invalidate();
		}
	}
}
