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
	public partial class FlagPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj	= false;

		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.valueEditor.FlagValue, this.EditedType); }
		}

		public FlagPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
		}

		public void AddFlag(string name, ulong value)
		{
			this.valueEditor.Add(value, name);
		}
		public void ClearFlags()
		{
			this.valueEditor.Items.Clear();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.Getter().ToArray();

			this.updatingFromObj = true;
			this.UpdateModifiedState();
			// Apply values to editors
			if (!values.Any())
				this.valueEditor.FlagValue = 0;
			else
			{
				ulong firstVal = (ulong)Convert.ChangeType(values.NotNull().First(), typeof(ulong));

				this.valueEditor.FlagValue = firstVal;

				if (!this.ReadOnly && (values.Any(o => o == null) || !values.All(o => (ulong)Convert.ChangeType(o, typeof(ulong)) == firstVal)))
					this.valueEditor.BackColor = this.BackColorMultiple;
				else
					this.valueEditor.BackColor = this.BackColorDefault;
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
			this.valueEditor.Enabled = !this.ReadOnly;
		}
		public override void UpdateModifiedState()
		{
			base.UpdateModifiedState();
			// Set font boldness according to modified value
			bool modified = this.ValueModified;
			if (this.nameLabel.Font.Bold != modified)
				this.nameLabel.Font = new Font(this.nameLabel.Font, modified ? FontStyle.Bold : FontStyle.Regular);
		}

		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			this.updatingFromObj = true;
			this.valueEditor.FlagValue = 0;
			this.updatingFromObj = false;
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.nameLabel.Width = this.NameLabelWidth;
		}

		private void valueEditor_FlagValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
	}
}
