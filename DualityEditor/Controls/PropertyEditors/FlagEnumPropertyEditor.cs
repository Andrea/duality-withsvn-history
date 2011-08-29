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
	public partial class FlagEnumPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj	= false;

		public override string PropertyName
		{
			get { return this.nameLabel.Text; }
			set { this.nameLabel.Text = value; }
		}
		public override object DisplayedValue
		{
			get { return Convert.ChangeType(this.valueEditor.EnumValue, this.EditedType); }
		}

		public FlagEnumPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
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
				this.valueEditor.EnumValue = (Enum)ReflectionHelper.CreateInstanceOf(this.EditedType);
			else
			{
				Enum firstVal = (Enum)values.Where(o => o != null).First();

				this.valueEditor.EnumValue = firstVal;

				if (!this.ReadOnly && (values.Any(o => o == null) || !values.All(o => Enum.Equals(o, firstVal))))
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
			this.valueEditor.EnumValue = (Enum)ReflectionHelper.CreateInstanceOf(this.EditedType);
			this.updatingFromObj = false;
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.nameLabel.Width = this.NameLabelWidth;
		}

		private void valueEditor_EnumValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetValue();
			this.OnValueEdited(this.DisplayedValue);
			this.PerformGetValue();
		}

		public override HelpInfo ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = base.ProvideHoverHelp(localPos, ref captured);

			Point globalPos = this.PointToScreen(localPos);
			Point editorLocalPos = this.valueEditor.PointToClient(globalPos);
			if (this.valueEditor.ClientRectangle.Contains(editorLocalPos))
			{
				for (int i = 0; i < this.valueEditor.Items.Count; i++)
				{
					Rectangle itemRect = this.valueEditor.GetItemRectangle(i);
					if (itemRect.Contains(editorLocalPos))
					{
						FlagCheckedListBoxItem item = this.valueEditor.Items[i] as FlagCheckedListBoxItem;
						if (item != null) result = HelpInfo.FromMember(this.EditedType.GetField(item.Caption, ReflectionHelper.BindAll));
						break;
					}
				}
			}

			captured = this.DisplayRectangle.Contains(localPos);
			return result;
		}
	}
}
