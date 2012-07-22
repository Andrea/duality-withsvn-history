using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

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
		private	int							stolenSelCount		= 0;
		private	float						lastAutoRefresh		= 0.0f;
		private	ObjectSelection.Category	selSchedMouseCat	= ObjectSelection.Category.None;
		private	ObjectSelection				selSchedMouse		= null;
		private	ObjectSelection				displaySel			= null;
		private	ObjectSelection.Category	displayCat			= ObjectSelection.Category.None;

		private	ExpandState					gridExpandState		= new ExpandState();


		public bool LockedSelection
		{
			get { return this.buttonLock.Checked; }
			set { this.buttonLock.Checked = value; }
		}
		public ObjectSelection DisplayedSelection
		{
			get { return (this.timerSelectSched.Enabled || this.selSchedMouse != null) ? this.selSchedMouse : this.displaySel; }
			set { this.UpdateSelection(value, value.Categories); }
		}
		public ObjectSelection.Category DisplayedCategory
		{
			get { return (this.timerSelectSched.Enabled || this.selSchedMouse != null) ? this.selSchedMouseCat : this.displayCat; }
		}
		public bool EmptySelection
		{
			get { return (this.displaySel == null || this.displaySel.Empty) && (!this.timerSelectSched.Enabled || this.selSchedMouse == null); }
		}


		public ObjectInspector(int runtimeId)
		{
			this.InitializeComponent();
			this.runtimeId = runtimeId;
			this.toolStrip.Renderer = new DualityEditor.Controls.ToolStrip.DualitorToolStripProfessionalRenderer();
		}
		public void CopyTo(ObjectInspector other)
		{
			this.gridExpandState.UpdateFrom(this.propertyGrid.MainEditor);

			other.buttonAutoRefresh.Checked = this.buttonAutoRefresh.Checked;
			other.buttonLock.Checked = this.buttonLock.Checked;

			this.gridExpandState.CopyTo(other.gridExpandState);
		}

		public bool AcceptsSelection(ObjectSelection sel)
		{
			if (this.LockedSelection) return false;
			return true;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());
			this.UpdateButtons();

			// Add the global selection event once
			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= GlobalUpdateSelection;
			EditorBasePlugin.Instance.EditorForm.SelectionChanged += GlobalUpdateSelection;

			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp += this.EditorForm_AfterUpdateDualityApp;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.FormClosing += this.EditorForm_FormClosing;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp -= this.EditorForm_AfterUpdateDualityApp;
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
			node.SetAttribute("autoRefresh", this.buttonAutoRefresh.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("locked", this.buttonLock.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("titleText", this.Text);
		}
		internal void LoadUserData(System.Xml.XmlElement node)
		{
			bool tryParseBool;

			if (bool.TryParse(node.GetAttribute("autoRefresh"), out tryParseBool))
				this.buttonAutoRefresh.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("locked"), out tryParseBool))
				this.buttonLock.Checked = tryParseBool;
			this.Text = node.GetAttribute("titleText");
		}

		private void UpdateButtons()
		{
			this.buttonClone.Enabled = this.propertyGrid.Selection.Any();
			this.buttonLock.Enabled = true;
		}
		private void UpdateSelection(ObjectSelection sel, ObjectSelection.Category showCat)
		{
			this.selSchedMouse = null;
			this.selSchedMouseCat = ObjectSelection.Category.None;
			this.displaySel = sel;
			this.displayCat = showCat;

			if (showCat == ObjectSelection.Category.None) return;
			this.gridExpandState.UpdateFrom(this.propertyGrid.MainEditor);

			if ((showCat & ObjectSelection.Category.GameObjCmp) != ObjectSelection.Category.None)
			{
				this.displaySel = new ObjectSelection(sel.GameObjects.Union(sel.Components.GameObject()));
				this.displayCat = ObjectSelection.Category.GameObjCmp;
				this.propertyGrid.SelectObjects(this.displaySel.Objects, false);
			}
			else if ((showCat & ObjectSelection.Category.Resource) != ObjectSelection.Category.None)
			{
				this.displaySel = new ObjectSelection(sel.Resources);
				this.displayCat = ObjectSelection.Category.Resource;
				this.propertyGrid.SelectObjects(this.displaySel.Objects, this.displaySel.Resources.Any(r => r.Path.Contains(':')));
			}
			else if ((showCat & ObjectSelection.Category.Other) != ObjectSelection.Category.None)
			{
				this.displaySel = new ObjectSelection(sel.OtherObjects);
				this.displayCat = ObjectSelection.Category.Other;
				this.propertyGrid.SelectObjects(this.displaySel.Objects, false);
			}

			this.gridExpandState.ApplyTo(this.propertyGrid.MainEditor);
			this.buttonClone.Enabled = this.propertyGrid.Selection.Any();
		}
		
		private void EditorForm_AfterUpdateDualityApp(object sender, EventArgs e)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game && 
				this.buttonAutoRefresh.Checked && 
				Time.MainTimer - this.lastAutoRefresh > 500.0f)
			{
				this.lastAutoRefresh = Time.MainTimer;
				this.propertyGrid.UpdateFromObjects();
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
		private static void GlobalUpdateSelection(object sender, SelectionChangedEventArgs e)
		{
			ObjectInspector target = null;
			
			foreach (ObjectSelection.Category cat in ObjectSelection.EnumerateCategories(e.AffectedCategories))
			{
				var objViews = 
					from v in EditorBasePlugin.Instance.ObjViews
					where v.AcceptsSelection(e.Current)
					select new { 
						View = v, 
						Empty = v.EmptySelection,
						PerfectFit = v.EmptySelection || (cat & v.DisplayedCategory) != ObjectSelection.Category.None,
						TypeShare = ObjectSelection.GetTypeShareLevel(e.Current.Exclusive(cat), v.DisplayedSelection) };
				var sortedQuery = 
					from o in objViews
					orderby o.PerfectFit descending, o.Empty ascending, o.TypeShare ascending, o.View.stolenSelCount ascending
					select o;
				var targetItem = sortedQuery.FirstOrDefault();
				if (targetItem == null) return;
				target = targetItem.View;

				// If the target view showed something else first, increase its stolen counter
				if (!targetItem.PerfectFit) target.stolenSelCount++;

				// If a mouse button is pressed, reschedule the selection for later - there might be a drag in progress
				if (Control.MouseButtons != System.Windows.Forms.MouseButtons.None)
				{
					target.selSchedMouse = e.Current;
					target.selSchedMouseCat = cat;
					target.timerSelectSched.Enabled = true;
				}
				else
				{
					target.UpdateSelection(e.Current, cat);
				}
			}
			
			//  Make sure disposed objects are deselected in non-target views (locked, etc.)
			foreach (ObjectInspector v in EditorBasePlugin.Instance.ObjViews)
			{
				if (v.EmptySelection) continue;
				if (v == target) continue;

				var disposedObj = e.Removed.Objects.OfType<IManageableObject>().Where(o => o.Disposed);
				if (disposedObj.Any())
				{
					ObjectSelection disposedSel = new ObjectSelection(disposedObj);
					v.UpdateSelection(v.DisplayedSelection - disposedSel, v.DisplayedCategory);
				}
			}
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
			ObjectInspector objView = EditorBasePlugin.Instance.RequestObjView(true);
			this.CopyTo(objView);
			objView.buttonLock.Checked = true;

			DockPanel mainDoc = EditorBasePlugin.Instance.EditorForm.MainDockPanel;
			objView.Show(this.DockHandler.Pane, DockAlignment.Bottom, 0.5d);
			
			// Need it before showing because of instant-selection
			objView.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());
			objView.propertyGrid.SelectObjects(this.propertyGrid.Selection);
			objView.gridExpandState.ApplyTo(objView.propertyGrid.MainEditor);
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
