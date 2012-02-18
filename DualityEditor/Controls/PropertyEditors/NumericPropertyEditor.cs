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
using Duality.EditorHints;

namespace DualityEditor.Controls.PropertyEditors
{
	public partial class NumericPropertyEditor : PropertyEditor
	{
		private	bool		updatingFromObj	= false;

		public NumericUpDown Editor
		{
			get { return this.valueEditor; }
		}
		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.valueEditor.Value, this.EditedType); }
		}

		public NumericPropertyEditor()
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
				this.valueEditor.Value = 0;
			else
			{
				decimal avg = values.Where(o => o != null).Average(o => Convert.ToDecimal(o));

				this.valueEditor.Value = avg;

				if (!this.ReadOnly && (values.Any(o => o == null) || !values.All(o => Convert.ToDecimal(o) == avg)))
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
			if (this.EditedType == typeof(byte))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = byte.MinValue;
				this.valueEditor.Maximum = byte.MaxValue;
			}
			else if (this.EditedType == typeof(sbyte))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = sbyte.MinValue;
				this.valueEditor.Maximum = sbyte.MaxValue;
			}
			else if (this.EditedType == typeof(short))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = short.MinValue;
				this.valueEditor.Maximum = short.MaxValue;
			}
			else if (this.EditedType == typeof(ushort))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = ushort.MinValue;
				this.valueEditor.Maximum = ushort.MaxValue;
			}
			else if (this.EditedType == typeof(int))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = int.MinValue;
				this.valueEditor.Maximum = int.MaxValue;
			}
			else if (this.EditedType == typeof(uint))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = uint.MinValue;
				this.valueEditor.Maximum = uint.MaxValue;
			}
			else if (this.EditedType == typeof(long))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = long.MinValue;
				this.valueEditor.Maximum = long.MaxValue;
			}
			else if (this.EditedType == typeof(ulong))
			{
				this.valueEditor.DecimalPlaces = 0;
				this.valueEditor.Minimum = ulong.MinValue;
				this.valueEditor.Maximum = ulong.MaxValue;
			}
			else if (this.EditedType == typeof(float))
			{
				this.valueEditor.DecimalPlaces = 2;
				this.valueEditor.Minimum = decimal.MinValue;
				this.valueEditor.Maximum = decimal.MaxValue;
			}
			else if (this.EditedType == typeof(double))
			{
				this.valueEditor.DecimalPlaces = 2;
				this.valueEditor.Minimum = decimal.MinValue;
				this.valueEditor.Maximum = decimal.MaxValue;
			}
			else if (this.EditedType == typeof(decimal))
			{
				this.valueEditor.DecimalPlaces = 2;
				this.valueEditor.Minimum = decimal.MinValue;
				this.valueEditor.Maximum = decimal.MaxValue;
			}
		}
		protected override void OnEditedMemberChanged()
		{
			base.OnEditedMemberChanged();
			EditorHintDecimalPlacesAttribute places = this.EditedMember.GetCustomAttributes(typeof(EditorHintDecimalPlacesAttribute), true).FirstOrDefault() as EditorHintDecimalPlacesAttribute;
			EditorHintRangeAttribute range = this.EditedMember.GetCustomAttributes(typeof(EditorHintRangeAttribute), true).FirstOrDefault() as EditorHintRangeAttribute;
			if (places != null) this.valueEditor.DecimalPlaces = places.Places;
			if (range != null)
			{
				this.valueEditor.Minimum = range.Min;
				this.valueEditor.Maximum = range.Max;
			}
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.nameLabel.Width = this.NameLabelWidth;
		}

		private void valueEditor_ValueChanged(object sender, EventArgs e)
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
