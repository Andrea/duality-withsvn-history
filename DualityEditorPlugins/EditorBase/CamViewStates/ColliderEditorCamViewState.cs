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
		private	Collider			selectedCollider	= null;

		public override string StateName
		{
			get { return "Collider Editor"; }
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
			//if (e.Objects.Components.Any(c => c is Transform || c is Renderer))
			//	this.UpdateSelectionStats();
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & (ObjectSelection.Category.GameObject | ObjectSelection.Category.Component)) == ObjectSelection.Category.None) return;

			this.selectedCollider = this.QuerySelectedCollider();

			//this.UpdateSelectionStats();
			this.View.LocalGLControl.Invalidate();
		}
	}
}
