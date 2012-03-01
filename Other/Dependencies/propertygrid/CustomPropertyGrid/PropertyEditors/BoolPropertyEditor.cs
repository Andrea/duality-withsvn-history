using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace CustomPropertyGrid.PropertyEditors
{
	public class BoolPropertyEditor : PropertyEditor
	{
		private	CheckState	state	= CheckState.Unchecked;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.state == CheckState.Checked, this.EditedType); }
		}
		
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.BeginUpdate();
			object[] values = this.GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.state = CheckState.Unchecked;
			else
			{
				int trueCount = values.Count(o => o != null && (bool)Convert.ToBoolean(o));
				if (!this.ReadOnly && (values.Any(o => o == null) || (trueCount > 0 && trueCount < values.Count())))
					this.state = CheckState.Indeterminate;
				else
					this.state = trueCount > 0 ? CheckState.Checked : CheckState.Unchecked;
			}

			this.EndUpdate();
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;
			if (this.state == CheckState.Indeterminate) return;

			this.SetValue(this.DisplayedValue);
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.FillRectangle(new SolidBrush(Color.Red), this.ClientRectangle);
		}
	}
}
