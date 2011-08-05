using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.Controls;

namespace EditorBase
{
	public partial class ResourceInspector : DockContent
	{
		private	ObjectSelection.Category	selSchedMouseCat	= ObjectSelection.Category.None;
		private	ObjectSelection				selSchedMouse		= null;

		public ResourceInspector()
		{
			this.InitializeComponent();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());

			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified -= this.EditorForm_ResourceModified;
		}

		private void UpdateSelection(ObjectSelection sel, ObjectSelection.Category lastSelChange)
		{
			this.selSchedMouse = null;
			this.selSchedMouseCat = ObjectSelection.Category.None;

			if ((lastSelChange & ObjectSelection.Category.Resource) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(sel.Resources, sel.Resources.Any(r => r.Path.Contains(':')), 300);
			else if ((lastSelChange & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(sel.OtherObjects, scheduleMs: 300);
		}

		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// If a mouse button is pressed, reschedule the selection for later - there might be a drag in progress
			if (Control.MouseButtons != System.Windows.Forms.MouseButtons.None)
			{
				this.selSchedMouse = e.Current;
				this.selSchedMouseCat = e.AffectedCategories;
				this.timerSelectSched.Enabled = true;
			}
			else
			{
				this.UpdateSelection(e.Current, e.AffectedCategories);
			}
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (!e.PrefabApplied && (sender is PropertyEditor) && (sender as PropertyEditor).ParentGrid == this.propertyGrid) return;

			// Update values if anything changed that relates to the grids current selection
			if (e.Objects.Objects.Any(o => this.propertyGrid.Selection.Contains(o)))
				this.propertyGrid.UpdateFromObjects(100);
		}
		private void EditorForm_ResourceModified(object sender, ResourceEventArgs e)
		{
			this.propertyGrid.UpdateFromObjects(100);
		}

		private void timerSelectSched_Tick(object sender, EventArgs e)
		{
			if (this.selSchedMouse == null)
			{
				this.timerSelectSched.Enabled = false;
			}
			else if (Control.MouseButtons == System.Windows.Forms.MouseButtons.None)
			{
				// If no more mouse buttons are currently pressed, process previously scheduled selection change...
				// .. but only if the cursor isn't located on this Control. That might mean something has been dragged here.
				Point curRelPos = this.PointToClient(Cursor.Position);
				if (curRelPos.X < 0 || curRelPos.Y < 0 || curRelPos.X > this.Bounds.Width || curRelPos.Y > this.Bounds.Height)
				{
					this.UpdateSelection(this.selSchedMouse, this.selSchedMouseCat);
				}
				this.timerSelectSched.Enabled = false;
			}
		}
	}
}
