using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using OpenTK;

using Duality;
using Duality.ColorFormat;

using DualityEditor.Forms;

namespace DualityEditor.Controls.PropertyEditors
{
	public partial class IColorDataPropertyEditor : PropertyEditor
	{
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
				IColorData clr = this.EditedType.CreateInstanceOf(true) as IColorData;
				clr.SetIntArgb(this.colorShowBox.Color.ToArgb());
				return clr;
			}
		}

		public IColorDataPropertyEditor()
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
				this.colorShowBox.Color = Color.Transparent;
			}
			else
			{
				IColorData first = (IColorData)values.NotNull().FirstOrDefault();
				this.colorShowBox.Color = Color.FromArgb((int)first.ToIntArgb());

				// No visual appearance of "multiple values" yet - need one?
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
			this.buttonOpenEditor.Enabled = !this.ReadOnly;
			this.colorShowBox.AllowDrop = !this.ReadOnly;
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

		private void buttonOpenEditor_Click(object sender, EventArgs e)
		{
			ColorPickerDialog dialog = new ColorPickerDialog();
			dialog.OldColor = this.colorShowBox.Color;
			dialog.PrimaryAttribute = ColorPickerDialog.PrimaryAttrib.Hue;
			if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)	
			{
				this.colorShowBox.Color = dialog.SelectedColor;
				this.PerformSetValue();
				this.OnValueEdited(this.DisplayedValue);
				this.PerformGetValue();
				this.OnEditingFinished();
			}
		}
		
		private void colorShowBox_MouseDown(object sender, MouseEventArgs e)
		{
			this.dragBeginPos = e.Location;
		}
		private void colorShowBox_MouseLeave(object sender, EventArgs e)
		{
			this.dragBeginPos = Point.Empty;
		}
		private void colorShowBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.dragBeginPos != Point.Empty)
			{
				if (Math.Abs(this.dragBeginPos.X - e.X) > 5 || Math.Abs(this.dragBeginPos.Y - e.Y) > 5)
				{
					DataObject dragDropData = new DataObject();
					dragDropData.AppendIColorData(new IColorData[] { this.DisplayedValue as IColorData });
					this.DoDragDrop(dragDropData, DragDropEffects.All | DragDropEffects.Link);
				}
			}
		}
		private void colorShowBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.buttonOpenEditor_Click(sender, e);
		}
		private void colorShowBox_MouseUp(object sender, MouseEventArgs e)
		{
			this.dragBeginPos = Point.Empty;
		}
		private void colorShowBox_DragEnter(object sender, DragEventArgs e)
		{
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsIColorData())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
			}
		}
		private void colorShowBox_DragDrop(object sender, DragEventArgs e)
		{
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsIColorData())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;

				IColorData[] clr = dragDropData.GetIColorData<IColorData>();
				Color newClr = Color.FromArgb((int)clr[0].ToIntArgb());
				if (this.colorShowBox.Color != newClr)
				{
					this.colorShowBox.Color = newClr;
					this.PerformSetValue();
					this.OnValueEdited(this.DisplayedValue);
					this.PerformGetValue();
					this.OnEditingFinished();
				}
			}
		}
	}
}
