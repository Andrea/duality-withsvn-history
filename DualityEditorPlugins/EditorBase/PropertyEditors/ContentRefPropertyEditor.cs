using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public partial class ContentRefPropertyEditor : PropertyEditor
	{
		private	string	contentPath		= null;
		private	Point	dragBeginPos	= Point.Empty;

		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get 
			{ 
				IContentRef ctRef = ReflectionHelper.CreateInstanceOf(this.EditedType) as IContentRef;
				ctRef.Path = this.contentPath;
				return ctRef;
			}
		}

		public ContentRefPropertyEditor()
		{
			this.InitializeComponent();
			this.UpdateReadOnlyState();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.Getter().ToArray();

			// Update modified state
			this.UpdateModifiedState();
			// Apply values to editors
			if (!values.Any())
			{
				this.labelLinkedTo.Text = "null";
				this.contentPath = null;

				this.buttonSetNull.Enabled = false;
				this.buttonShow.Enabled = false;
				this.buttonReload.Enabled = false;
			}
			else
			{
				IContentRef first = values.NotNull().FirstOrDefault() as IContentRef;

				this.contentPath = first.Path;
				this.labelLinkedTo.Text = first.ToString();
				this.labelLinkedTo.ForeColor = first.IsAvailable ? SystemColors.ControlText : Color.Red;

				bool pathSet = !String.IsNullOrEmpty(this.contentPath);

				this.buttonSetNull.Enabled = pathSet && !this.ReadOnly;
				this.buttonShow.Enabled = pathSet;
				this.buttonReload.Enabled = pathSet && !values.NotNull().Any(o => (o as IContentRef).IsDefaultContent);

				bool allEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((IContentRef)o).Path == first.Path));
				this.labelLinkedTo.BackColor = allEqual ? this.BackColorDefault : this.BackColorMultiple;
			}
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;

			this.SetterSingle(this.DisplayedValue);
		}
		public override void UpdateReadOnlyState()
		{
		    base.UpdateReadOnlyState();
			this.buttonSetNull.Enabled = !this.ReadOnly;
			this.labelLinkedTo.AllowDrop = !this.ReadOnly;
		}
		public override void UpdateModifiedState()
		{
			base.UpdateModifiedState();
			// Set font boldness according to modified value
			bool modified = this.ValueModified;
			if (this.nameLabel.Font.Bold != modified)
				this.nameLabel.Font = new Font(this.nameLabel.Font, modified ? FontStyle.Bold : FontStyle.Regular);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.nameLabel.Width = this.NameLabelWidth;
		}

		private void buttonShow_Click(object sender, EventArgs e)
		{
			ProjectFolderView view = EditorBasePlugin.Instance.RequestProjectView();
			view.FlashNode(view.NodeFromPath(this.contentPath));
			System.Media.SystemSounds.Beep.Play();
		}
		private void buttonSetNull_Click(object sender, EventArgs e)
		{
			this.contentPath = null;

			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		private void buttonReload_Click(object sender, EventArgs e)
		{
			ContentProvider.UnregisterContent(this.contentPath);

			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		
		private void labelLinkedTo_DragEnter(object sender, DragEventArgs e)
		{
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs(this.EditedType.GetGenericArguments()[0]))
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
				this.labelLinkedTo.BackColor = Color.FromArgb(220, 255, 200);
			}
		}
		private void labelLinkedTo_DragDrop(object sender, DragEventArgs e)
		{
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs(this.EditedType.GetGenericArguments()[0]))
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
				this.labelLinkedTo.BackColor = this.BackColorDefault;

				ContentRef<Resource>[] ctRefs = dragDropData.GetContentRefs(this.EditedType.GetGenericArguments()[0]);
				if (this.contentPath != ctRefs[0].Path)
				{
					this.contentPath = ctRefs[0].Path;
					this.PerformSetValue();
					this.OnValueEdited(this.DisplayedValue);
					this.PerformGetValue();
				}
			}
		}
		private void labelLinkedTo_DragLeave(object sender, EventArgs e)
		{
			this.labelLinkedTo.BackColor = this.BackColorDefault;
		}
		private void labelLinkedTo_DoubleClick(object sender, EventArgs e)
		{
			this.buttonShow_Click(sender, e);
		}
		private void labelLinkedTo_MouseDown(object sender, MouseEventArgs e)
		{
			this.dragBeginPos = e.Location;
		}
		private void labelLinkedTo_MouseLeave(object sender, EventArgs e)
		{
			this.dragBeginPos = Point.Empty;
		}
		private void labelLinkedTo_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.dragBeginPos != Point.Empty)
			{
				if (Math.Abs(this.dragBeginPos.X - e.X) > 5 || Math.Abs(this.dragBeginPos.Y - e.Y) > 5)
				{
					DataObject dragDropData = new DataObject();
					dragDropData.AppendContentRefs(new ContentRef<Resource>[] { (this.Getter().NotNull().First() as IContentRef).As<Resource>() } );
					this.DoDragDrop(dragDropData, DragDropEffects.All | DragDropEffects.Link);
				}
			}
		}
		private void labelLinkedTo_MouseUp(object sender, MouseEventArgs e)
		{
			this.dragBeginPos = Point.Empty;
		}
	}
}
