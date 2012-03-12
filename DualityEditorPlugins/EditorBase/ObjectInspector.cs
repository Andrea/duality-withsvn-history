using System;
using System.Collections.Generic;

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using AdamsLair.PropertyGrid;
using PropertyGrid = AdamsLair.PropertyGrid.PropertyGrid;

using Duality;
using DualityEditor;

namespace EditorBase
{
	public partial class ObjectInspector : DockContent
	{
		private	int							runtimeId			= 0;
		private	ObjectSelection.Category	acceptedCats		= ObjectSelection.Category.All;
		private	float						lastAutoRefresh		= 0.0f;
		private	ObjectSelection.Category	selSchedMouseCat	= ObjectSelection.Category.None;
		private	ObjectSelection				selSchedMouse		= null;

		public ObjectSelection.Category AcceptedCategories
		{
			get { return this.acceptedCats; }
			set { this.acceptedCats = value; }
		}

		public ObjectInspector(int runtimeId)
		{
			this.InitializeComponent();
			this.runtimeId = runtimeId;
		}
		public ObjectInspector(ObjectInspector other) : this(-1)
		{
			this.buttonAutoRefresh.Checked = other.buttonAutoRefresh.Checked;
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());
			this.buttonClone.Enabled = this.propertyGrid.Selection.Any();

			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp += this.EditorForm_AfterUpdateDualityApp;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.FormClosing += this.EditorForm_FormClosing;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp -= this.EditorForm_AfterUpdateDualityApp;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified -= this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.FormClosing -= this.EditorForm_FormClosing;
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.propertyGrid.Focus();
		}

		internal void SaveUserData(System.Xml.XmlElement node)
		{
			node.SetAttribute("acceptedCats", ((uint)this.acceptedCats).ToString());
			node.SetAttribute("autoRefresh", this.buttonAutoRefresh.Checked.ToString());
			node.SetAttribute("titleText", this.Text);
		}
		internal void LoadUserData(System.Xml.XmlElement node)
		{
			uint tryParseUInt;
			bool tryParseBool;

			if (uint.TryParse(node.GetAttribute("acceptedCats"), out tryParseUInt))
				this.acceptedCats = (ObjectSelection.Category)tryParseUInt;
			if (bool.TryParse(node.GetAttribute("autoRefresh"), out tryParseBool))
				this.buttonAutoRefresh.Checked = tryParseBool;
			this.Text = node.GetAttribute("titleText");
		}

		private void UpdateSelection(ObjectSelection sel, ObjectSelection.Category lastSelChange)
		{
			this.selSchedMouse = null;
			this.selSchedMouseCat = ObjectSelection.Category.None;

			if (lastSelChange == ObjectSelection.Category.None) return;

			if ((lastSelChange & ObjectSelection.Category.GameObjCmp) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(sel.GameObjects.Union(sel.Components.GameObject()));
			else if ((lastSelChange & ObjectSelection.Category.Resource) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(sel.Resources, sel.Resources.Any(r => r.Path.Contains(':')));
			else if ((lastSelChange & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
				this.propertyGrid.SelectObjects(sel.OtherObjects);

			this.buttonClone.Enabled = this.propertyGrid.Selection.Any();
		}
		
		private void EditorForm_AfterUpdateDualityApp(object sender, EventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game && 
				this.buttonAutoRefresh.Checked && 
				Time.MainTimer - this.lastAutoRefresh > 1000.0f)
			{
				this.lastAutoRefresh = Time.MainTimer;
				this.propertyGrid.UpdateFromObjects(100);
			}
		}
		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((e.AffectedCategories & this.acceptedCats) == ObjectSelection.Category.None) return;

			// If a mouse button is pressed, reschedule the selection for later - there might be a drag in progress
			if (Control.MouseButtons != System.Windows.Forms.MouseButtons.None)
			{
				this.selSchedMouse = e.Current;
				this.selSchedMouseCat = e.AffectedCategories & this.acceptedCats;
				this.timerSelectSched.Enabled = true;
			}
			else
			{
				this.UpdateSelection(e.Current, e.AffectedCategories & this.acceptedCats);
			}
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (!e.PrefabApplied && (sender is PropertyEditor) && (sender as PropertyEditor).ParentGrid == this.propertyGrid) return;
			if (!e.PrefabApplied && sender is PropertyGrid) return;

			// Update values if anything changed that relates to the grids current selection
			if (e.Objects.Components.GameObject().Any(o => this.propertyGrid.Selection.Contains(o)) ||
				e.Objects.Objects.Any(o => this.propertyGrid.Selection.Contains(o)))
				this.propertyGrid.UpdateFromObjects(100);
		}
		private void EditorForm_ResourceModified(object sender, ResourceEventArgs e)
		{
			this.propertyGrid.UpdateFromObjects(100);
		}
		private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.runtimeId == -1 && !e.Cancel) this.Close();
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

		private void buttonClone_Click(object sender, EventArgs e)
		{
			ObjectInspector objView = new ObjectInspector(this);
			objView.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());

			DockPanel mainDoc = EditorBasePlugin.Instance.EditorForm.MainDockPanel;
			objView.Show(EditorBasePlugin.Instance.EditorForm.MainDockPanel, DockState.Float);

			objView.Text = string.Format("Inspecting: {0}", this.propertyGrid.Selection.First().ToString());
			objView.AcceptedCategories = ObjectSelection.Category.None;
			objView.propertyGrid.SelectObjects(this.propertyGrid.Selection);
		}
		private void buttonAutoRefresh_CheckedChanged(object sender, EventArgs e)
		{
			if (this.buttonAutoRefresh.Checked)
			{
				this.lastAutoRefresh = Time.MainTimer;
				this.propertyGrid.UpdateFromObjects(100);
			}
		}
	}
}
