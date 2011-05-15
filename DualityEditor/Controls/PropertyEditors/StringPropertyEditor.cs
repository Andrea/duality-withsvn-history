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
	public partial class StringPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj	= false;

		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.valueEditor.Text, this.EditedType); }
		}

		public StringPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.Getter().ToArray();

			this.updatingFromObj = true;
			this.UpdateModifiedState();
			// Apply values to editors
			if (!values.Any())
				this.valueEditor.Text = null;
			else
			{
				string firstStr = (string)values.First();
				this.valueEditor.Text = firstStr;
				if (!this.ReadOnly && (values.Any(o => o == null || (string)o != firstStr)))
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
			this.valueEditor.ReadOnly = this.ReadOnly;
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

		private void valueEditor_TextChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}
		private void valueEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return) this.OnEditingFinished();
		}
	}
}
