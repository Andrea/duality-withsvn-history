using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;

namespace DualityEditor.Controls.PropertyEditors
{
	public partial class RectPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj	= false;

		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get 
			{ 
				return Convert.ChangeType(
					new Rect(
						(float)this.editorX.Value, 
						(float)this.editorY.Value, 
						(float)this.editorW.Value, 
						(float)this.editorH.Value), 
					this.EditedType);
			}
		}

		public RectPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
			this.UpdateReadOnlyState();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.Getter().ToArray();

			this.updatingFromObj = true;
			// Update modified state
			this.UpdateModifiedState();
			// Apply values to editors
			if (!values.Any())
			{
				this.editorX.Value = 0;
				this.editorY.Value = 0;
				this.editorW.Value = 0;
				this.editorH.Value = 0;
			}
			else
			{
				var valNotNull = values.NotNull();
				float avgX = valNotNull.Average(o => ((Rect)o).x);
				float avgY = valNotNull.Average(o => ((Rect)o).y);
				float avgW = valNotNull.Average(o => ((Rect)o).w);
				float avgH = valNotNull.Average(o => ((Rect)o).h);

				this.editorX.Value = (decimal)avgX;
				this.editorY.Value = (decimal)avgY;
				this.editorW.Value = (decimal)avgW;
				this.editorH.Value = (decimal)avgH;

				bool allXEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Rect)o).x == avgX));
				bool allYEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Rect)o).y == avgY));
				bool allWEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Rect)o).w == avgW));
				bool allHEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Rect)o).h == avgH));
				this.editorX.BackColor = allXEqual ? this.BackColorDefault : this.BackColorMultiple;
				this.editorY.BackColor = allYEqual ? this.BackColorDefault : this.BackColorMultiple;
				this.editorW.BackColor = allWEqual ? this.BackColorDefault : this.BackColorMultiple;
				this.editorH.BackColor = allHEqual ? this.BackColorDefault : this.BackColorMultiple;
			}
			this.updatingFromObj = false;
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
			this.editorX.Enabled = !this.ReadOnly;
			this.editorY.Enabled = !this.ReadOnly;
			this.editorW.Enabled = !this.ReadOnly;
			this.editorH.Enabled = !this.ReadOnly;
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

		private void editorX_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		private void editorY_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		private void editorW_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		private void editorH_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
	}
}
