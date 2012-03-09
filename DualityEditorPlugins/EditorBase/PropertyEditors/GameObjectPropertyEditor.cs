using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.Renderer;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

namespace EditorBase.PropertyEditors
{
	public partial class GameObjectPropertyEditor : PropertyEditor
	{
		private	Rectangle	rectHeader				= Rectangle.Empty;
		private	Rectangle	rectPrefab				= Rectangle.Empty;
		private	Rectangle	rectCheckActive			= Rectangle.Empty;
		private	Rectangle	rectLabelName			= Rectangle.Empty;
		private	Rectangle	rectLabelPrefab			= Rectangle.Empty;
		private	Rectangle	rectButtonsPrefab		= Rectangle.Empty;
		private	Rectangle	rectButtonPrefabShow	= Rectangle.Empty;
		private	Rectangle	rectButtonPrefabRevert	= Rectangle.Empty;
		private	Rectangle	rectButtonPrefabApply	= Rectangle.Empty;
		private	Rectangle	rectButtonPrefabBreak	= Rectangle.Empty;
		private	string		displayedName		= "GameObject";
		private	string		displayedNameExt	= "";
		
		public override object DisplayedValue
		{
			get { return this.GetValue(); }
		}

		public GameObjectPropertyEditor()
		{
			this.Height = 45;
			this.Hints = HintFlags.None;
			this.EditedType = typeof(GameObject);
			this.PropertyName = "GameObject";
		}

		public void PerformSetActive(bool active)
		{
			GameObject[] values = this.GetValue().Cast<GameObject>().ToArray();
			foreach (GameObject o in values) o.ActiveSingle = active;

			// Notify ActiveSingle changed
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, 
				new ObjectSelection(values), 
				ReflectionInfo.Property_GameObject_ActiveSingle);
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			GameObject[] values = this.GetValue().Cast<GameObject>().ToArray();

			this.BeginUpdate();
			if (values.Any())
			{
				GameObject parent = values.First().Parent;
				string name = values.First().Name;
				bool? active = values.First().ActiveSingle;
				if (!values.All(o => o.ActiveSingle == active.Value)) active = null;

				//this.activeEditor.CheckState = active.HasValue ? (active.Value ? CheckState.Checked : CheckState.Unchecked) : CheckState.Indeterminate;
				//this.nameEditor.Text = name;
				if (values.Count() == 1)
				{
					this.displayedName = name;
					this.displayedNameExt = values.First().Parent != null ? " in " + parent.FullName : "";
				}
				else
				{
					this.displayedName = string.Format(DualityEditor.EditorRes.GeneralRes.PropertyGrid_N_Objects, values.Count());
					this.displayedNameExt = "";
				}
				//this.tableLayout.Enabled = values.Any(o => o.PrefabLink != null);
				//this.labelPrefabLink.ForeColor = values.All(o => o.PrefabLink == null || o.PrefabLink.Prefab.IsAvailable) ? Color.Blue : Color.Red;

				this.Invalidate();
			}
			this.EndUpdate();
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			// General
			this.rectHeader = new Rectangle(
				this.ClientRectangle.X, 
				this.ClientRectangle.Y, 
				this.ClientRectangle.Width, 
				this.ClientRectangle.Height * 20 / 45);
			this.rectPrefab = new Rectangle(
				this.ClientRectangle.X, 
				this.ClientRectangle.Y + this.rectHeader.Height, 
				this.ClientRectangle.Width, 
				this.ClientRectangle.Height - this.rectHeader.Height);

			// Header
			this.rectCheckActive = new Rectangle(
				this.rectHeader.X + 2,
				this.rectHeader.Y + this.rectHeader.Height / 2 - ControlRenderer.CheckBoxSize.Height / 2 - 1,
				ControlRenderer.CheckBoxSize.Width,
				ControlRenderer.CheckBoxSize.Height);
			this.rectLabelName = new Rectangle(
				this.rectCheckActive.Right + 2,
				this.rectHeader.Y,
				this.rectHeader.Width - this.rectCheckActive.Right - 4,
				this.rectHeader.Height);

			// PrefabLink
			this.rectLabelPrefab = new Rectangle(
				this.rectPrefab.X + 2,
				this.rectPrefab.Y,
				50,
				this.rectPrefab.Height);
			this.rectButtonsPrefab = new Rectangle(
				this.rectLabelPrefab.Right + 2,
				this.rectPrefab.Y,
				this.rectPrefab.Width - this.rectLabelPrefab.Right - 4,
				this.rectPrefab.Height);
			int buttonSpacing = 2;
			int buttonWidth = (this.rectButtonsPrefab.Width - (buttonSpacing * 3)) / 3;
			this.rectButtonPrefabShow = new Rectangle(
				this.rectButtonsPrefab.X,
				this.rectButtonsPrefab.Y,
				buttonWidth,
				this.rectButtonsPrefab.Height);
			this.rectButtonPrefabRevert = new Rectangle(
				this.rectButtonsPrefab.X + buttonWidth + buttonSpacing,
				this.rectButtonsPrefab.Y,
				buttonWidth,
				this.rectButtonsPrefab.Height);
			this.rectButtonPrefabApply = new Rectangle(
				this.rectButtonsPrefab.X + (buttonWidth + buttonSpacing) * 2,
				this.rectButtonsPrefab.Y,
				buttonWidth,
				this.rectButtonsPrefab.Height);
			this.rectButtonPrefabBreak = new Rectangle(
				this.rectButtonsPrefab.X + (buttonWidth + buttonSpacing) * 3,
				this.rectButtonsPrefab.Y,
				buttonWidth,
				this.rectButtonsPrefab.Height);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			ControlRenderer.DrawGroupHeaderBackground(e.Graphics, this.rectHeader, this.Focused ? SystemColors.Control.ScaleBrightness(0.85f) : SystemColors.Control, GroupHeaderStyle.Emboss);
			ControlRenderer.DrawGroupHeaderBackground(e.Graphics, this.rectPrefab, this.Focused ? SystemColors.Control.ScaleBrightness(0.85f) : SystemColors.Control, GroupHeaderStyle.SmoothSunken);
		}

		//private void activeEditor_CheckedChanged(object sender, EventArgs e)
		//{
		//    if (this.updatingFromObj) return;
		//    this.PerformSetActive(this.activeEditor.Checked);
		//    this.PerformGetValue();
		//}

		//private void buttonPrefabLinkShow_Click(object sender, EventArgs e)
		//{
		//    GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();
		//    Duality.Resources.PrefabLink link = values.First().PrefabLink;

		//    ProjectFolderView view = EditorBasePlugin.Instance.RequestProjectView();
		//    view.FlashNode(view.NodeFromPath(link.Prefab.Path));
		//    System.Media.SystemSounds.Beep.Play();
		//}
		//private void buttonPrefabLinkRevert_Click(object sender, EventArgs e)
		//{
		//    GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();

		//    // Clear all changes and re-apply Prefabs
		//    foreach (GameObject o in values) o.PrefabLink.ClearChanges();
		//    Duality.Resources.PrefabLink.ApplyAllLinks(values);

		//    EditorBasePlugin.Instance.EditorForm.NotifyObjPrefabApplied(this, new ObjectSelection(values));
		//    this.PerformGetValue();
		//}
		//private void buttonPrefabLinkApply_Click(object sender, EventArgs e)
		//{
		//    GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();
		//    foreach (GameObject o in values)
		//    {
		//        if (o.PrefabLink != null && o.PrefabLink.Prefab.IsAvailable)
		//        {
		//            Duality.Resources.Prefab prefab = o.PrefabLink.Prefab.Res;

		//            // Inject GameObject to Prefab
		//            prefab.Inject(o);
		//            prefab.Save();

		//            // Establish PrefabLink & clear previously existing changes
		//            if (o.PrefabLink != null) o.PrefabLink.ClearChanges();
		//            o.LinkToPrefab(prefab);
		//        }
		//    }
		//    EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_GameObject_PrefabLink);
		//}
		//private void buttonPrefabLinkDestroy_Click(object sender, EventArgs e)
		//{
		//    GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();

		//    // Destroy all PrefabLinks
		//    foreach (GameObject o in values) o.BreakPrefabLink();

		//    EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_GameObject_PrefabLink);
		//    this.PerformGetValue();
		//    this.ParentEditor.UpdateModifiedState();
		//}

		//public override HelpInfo ProvideHoverHelp(Point localPos, ref bool captured)
		//{
		//    HelpInfo result = base.ProvideHoverHelp(localPos, ref captured);

		//    if (localPos.Y > this.nameEditor.Bounds.Bottom)
		//    {
		//        result = HelpInfo.FromMember(typeof(Duality.Resources.PrefabLink));
		//    }

		//    return result;
		//}
	}
}
