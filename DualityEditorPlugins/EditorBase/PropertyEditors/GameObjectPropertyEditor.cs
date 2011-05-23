using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public partial class GameObjectPropertyEditor : PropertyEditor
	{
		private	string	displayedName		= "GameObject";
		private	string	displayedNameExt	= "";
		private	bool	updatingFromObj		= false;

		public GameObjectPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public void BeginEditName()
		{
			this.nameEditor.Visible = true;
			this.nameEditor.Focus();
			this.nameEditor.SelectAll();
		}
		public void EndEditName(bool abort = false)
		{
			this.nameEditor.Visible = false;

			if (abort)
				this.nameEditor.Text = this.displayedName;
			else if (!string.IsNullOrEmpty(this.nameEditor.Text) && this.nameEditor.Text != this.displayedName)
				this.PerformSetName(this.nameEditor.Text);

			this.PerformGetValue();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			GameObject[] values = this.Getter().Cast<GameObject>().ToArray();

			this.updatingFromObj = true;
			if (values.Any())
			{
				GameObject parent = values.First().Parent;
				string name = values.First().Name;
				bool? active = values.First().ActiveSingle;
				if (!values.All(o => o.ActiveSingle == active.Value)) active = null;

				this.activeEditor.CheckState = active.HasValue ? (active.Value ? CheckState.Checked : CheckState.Unchecked) : CheckState.Indeterminate;
				this.nameEditor.Text = name;
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
				this.tableLayout.Enabled = values.Any(o => o.PrefabLink != null);
				this.labelPrefabLink.ForeColor = values.All(o => o.PrefabLink == null || o.PrefabLink.Prefab.IsAvailable) ? Color.Blue : Color.Red;

				this.Invalidate();
			}
			this.updatingFromObj = false;
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;

			//this.SetterSingle(this.DisplayedValue);
		}
		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
			this.activeEditor.Enabled = !this.ReadOnly;
			this.buttonPrefabLinkApply.Enabled = !this.ReadOnly;
			this.buttonPrefabLinkRevert.Enabled = !this.ReadOnly;
		}

		public void PerformSetName(string newName)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().ToArray();
			foreach (GameObject o in values) o.Name = newName;

			// Notify Name changed
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionHelper.Property_GameObject_Name);
		}
		public void PerformSetActive(bool active)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().ToArray();
			foreach (GameObject o in values) o.ActiveSingle = active;

			// Notify ActiveSingle changed
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, 
				new ObjectSelection(values), 
				ReflectionHelper.Property_GameObject_ActiveSingle);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle nameRect = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width, this.nameEditor.Height + 1);
			Rectangle propRect = new Rectangle(this.ClientRectangle.Left, this.nameEditor.Bottom + 1, this.ClientRectangle.Width, this.ClientRectangle.Height - (this.nameEditor.Height + 1));
			
			StringFormat nameTextFormat = StringFormat.GenericDefault;
			nameTextFormat.LineAlignment = StringAlignment.Center;
			nameTextFormat.Trimming = StringTrimming.EllipsisCharacter;
			nameTextFormat.FormatFlags |= StringFormatFlags.NoWrap;
			SizeF nameSize = e.Graphics.MeasureString(this.displayedName, new Font(this.Font, FontStyle.Bold), this.nameEditor.Size, nameTextFormat);
			RectangleF nameExtRect = new RectangleF(
				this.nameEditor.Bounds.X + nameSize.Width,
				this.nameEditor.Bounds.Y,
				this.nameEditor.Bounds.Width - nameSize.Width,
				this.nameEditor.Bounds.Height);

			Pen separatorPen = new Pen(Color.FromArgb(64, Color.Black));
			Pen separatorPenThick = new Pen(Color.FromArgb(128, Color.Black));

			e.Graphics.FillRectangle(
				new LinearGradientBrush(nameRect, Color.FromArgb(245, 245, 245), Color.FromArgb(200, 200, 200), 90.0f), 
				nameRect);
			if (propRect.Height > 0)
			{
				e.Graphics.FillRectangle(
					new LinearGradientBrush(propRect, Color.FromArgb(180, 180, 180), Color.FromArgb(245, 245, 245), 90.0f), 
					propRect);
				e.Graphics.DrawLine(separatorPen, nameRect.Left, nameRect.Bottom - 2, nameRect.Right, nameRect.Bottom - 2);
			}
			e.Graphics.DrawLine(separatorPenThick, propRect.Left, propRect.Bottom - 1, propRect.Right, propRect.Bottom - 1);

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			e.Graphics.DrawString(this.displayedName, new Font(this.Font, FontStyle.Bold), SystemBrushes.ControlText, this.nameEditor.Bounds, nameTextFormat);
			if (nameExtRect.Width > 20)
				e.Graphics.DrawString(this.displayedNameExt, this.Font, SystemBrushes.ControlText, nameExtRect, nameTextFormat);
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (this.nameEditor.Bounds.Contains(e.Location) && !this.ReadOnly)
				this.BeginEditName();
		}

		private void nameEditor_Leave(object sender, EventArgs e)
		{
			this.EndEditName();
		}
		private void nameEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
				this.EndEditName();
			else if (e.KeyCode == Keys.Escape)
				this.EndEditName(true);
		}
		private void activeEditor_CheckedChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetActive(this.activeEditor.Checked);
			this.PerformGetValue();
		}

		private void buttonPrefabLinkShow_Click(object sender, EventArgs e)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();
			PrefabLink link = values.First().PrefabLink;

			ProjectFolderView view = EditorBasePlugin.Instance.RequestProjectView();
			view.FlashNode(view.NodeFromPath(link.Prefab.Path));
			System.Media.SystemSounds.Beep.Play();
		}
		private void buttonPrefabLinkRevert_Click(object sender, EventArgs e)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();

			// Clear all changes and re-apply Prefabs
			foreach (GameObject o in values) o.PrefabLink.ClearChanges();
			PrefabLink.ApplyAllLinks(values);

			EditorBasePlugin.Instance.EditorForm.NotifyObjPrefabApplied(this, new ObjectSelection(values));
			this.PerformGetValue();
		}
		private void buttonPrefabLinkApply_Click(object sender, EventArgs e)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();
			foreach (GameObject o in values)
			{
				if (o.PrefabLink != null && o.PrefabLink.Prefab.IsAvailable)
				{
					Prefab prefab = o.PrefabLink.Prefab.Res;

					// Inject GameObject to Prefab
					prefab.Inject(o);
					prefab.Save();

					// Establish PrefabLink & clear previously existing changes
					if (o.PrefabLink != null) o.PrefabLink.ClearChanges();
					o.LinkToPrefab(prefab);
				}
			}
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionHelper.Property_GameObject_PrefabLink);
		}
		private void buttonPrefabLinkDestroy_Click(object sender, EventArgs e)
		{
			GameObject[] values = this.Getter().Cast<GameObject>().Where(o => o.PrefabLink != null).ToArray();

			// Destroy all PrefabLinks
			foreach (GameObject o in values) o.BreakPrefabLink();

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionHelper.Property_GameObject_PrefabLink);
			this.PerformGetValue();
			this.ParentEditor.UpdateModifiedState();
		}
	}
}
