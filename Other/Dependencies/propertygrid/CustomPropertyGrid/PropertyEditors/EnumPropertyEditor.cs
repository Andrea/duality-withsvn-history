using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;
using CustomPropertyGrid.EditorTemplates;

namespace CustomPropertyGrid.PropertyEditors
{
	public class EnumPropertyEditor : PropertyEditor
	{
		private	ComboBoxEditorTemplate	stringSelector	= null;
		private Enum	val				= null;
		private	bool	valMultiple		= false;

		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.val, this.EditedType); }
		}
		

		public EnumPropertyEditor()
		{
			this.stringSelector = new ComboBoxEditorTemplate(this);
			this.stringSelector.Invalidate += this.stringSelector_Invalidate;
			this.stringSelector.Edited += this.stringSelector_Edited;

			this.Height = 18;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			this.BeginUpdate();
			object[] values = this.GetValue().ToArray();

			// Apply values to editors
			if (!values.Any())
				this.val = null;
			else
			{
				Enum firstVal = (Enum)values.Where(o => o != null).First();

				this.val = firstVal;
				this.valMultiple = values.Any(o => o == null) || !values.All(o => Enum.Equals(o, firstVal));
			}

			this.stringSelector.SelectedText = this.val.ToString();
			this.EndUpdate();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.stringSelector.OnPaint(e, this.Enabled, this.valMultiple);
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();
			this.stringSelector.Rect = new Rectangle(
				this.ClientRectangle.X + 1,
				this.ClientRectangle.Y + 1,
				this.ClientRectangle.Width - 2,
				this.ClientRectangle.Height - 1);
		}
		protected internal override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			this.stringSelector.ReadOnly = this.ReadOnly;
		}

		private void stringSelector_Invalidate(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		private void stringSelector_Edited(object sender, EventArgs e)
		{
			if (this.IsUpdatingFromObject) return;

			this.val = (Enum)Enum.Parse(this.EditedType, this.stringSelector.SelectedText);
			this.Invalidate();
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}
	}
}
