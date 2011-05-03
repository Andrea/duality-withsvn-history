using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
	public partial class ObjectInspector : DockContent
	{
		public ObjectInspector()
		{
			this.InitializeComponent();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());

			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
		}

		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & ObjectSelection.Category.GameObject) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(e.Current.GameObjects, scheduleMs: 500);
			else if ((e.AffectedCategories & ObjectSelection.Category.Component) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(e.Current.Components, scheduleMs: 500);
			else if ((e.AffectedCategories & ObjectSelection.Category.Resource) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(e.Current.Resources, e.Current.Resources.Any(r => r.Path.Contains(':')), 500);
			else if ((e.AffectedCategories & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(e.Current.OtherObjects, scheduleMs: 500);
			else
				this.propertyGrid.SelectObjects(e.Current.Objects, scheduleMs: 500);
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if ((sender is PropertyEditor) && (sender as PropertyEditor).ParentGrid == this.propertyGrid) return;

			// Update values if anything changed that relates to the grids current selection
			if (e.Objects.Components.GameObject().Any(o => this.propertyGrid.Selection.Contains(o)) ||
				e.Objects.Objects.Any(o => this.propertyGrid.Selection.Contains(o)))
				this.propertyGrid.UpdateFromObjects(100);
		}
	}
}
