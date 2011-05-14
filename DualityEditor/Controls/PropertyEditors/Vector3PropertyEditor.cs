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

namespace DualityEditor.Controls.PropertyEditors
{
	public partial class Vector3PropertyEditor : PropertyEditor
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
					new Vector3(
						(float)this.editorX.Value, 
						(float)this.editorY.Value, 
						(float)this.editorZ.Value), 
					this.EditedType);
			}
		}

		public Vector3PropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
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
				this.editorZ.Value = 0;
			}
			else
			{
				var valNotNull = values.NotNull();
				float avgX = valNotNull.Average(o => ((Vector3)o).X);
				float avgY = valNotNull.Average(o => ((Vector3)o).Y);
				float avgZ = valNotNull.Average(o => ((Vector3)o).Z);

				this.editorX.Value = (decimal)avgX;
				this.editorY.Value = (decimal)avgY;
				this.editorZ.Value = (decimal)avgZ;

				bool allXEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Vector3)o).X == avgX));
				bool allYEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Vector3)o).Y == avgY));
				bool allZEqual = this.ReadOnly || (values.All(o => o != null) && values.All(o => ((Vector3)o).Z == avgZ));
				this.editorX.BackColor = allXEqual ? this.BackColorDefault : this.BackColorMultiple;
				this.editorY.BackColor = allYEqual ? this.BackColorDefault : this.BackColorMultiple;
				this.editorZ.BackColor = allZEqual ? this.BackColorDefault : this.BackColorMultiple;
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
			this.editorZ.Enabled = !this.ReadOnly;
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
		private void editorZ_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
	}
}
