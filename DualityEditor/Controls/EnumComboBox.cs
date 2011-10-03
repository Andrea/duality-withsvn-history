using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DualityEditor.Controls
{
	public class EnumComboBox : ComboBox
	{
		private Type	enumType				= null;

		public event EventHandler EnumValueChanged = null;

		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public Enum EnumValue
		{
			get
			{
				return (Enum)Enum.Parse(this.enumType, (string)this.SelectedItem);
			}
			set
			{
				Type valEnumType = value.GetType();
                if (this.enumType != valEnumType)
				{
					this.Items.Clear();
					this.enumType = valEnumType; // Store enum type
					this.FillEnumMembers(); // Add items for enum members
				}
				this.SelectedItem = Enum.GetName(enumType, value) ?? ((long)Convert.ChangeType(value, typeof(long))).ToString();
				this.OnEnumValueChanged();
			}
		}
		
		public EnumComboBox()
		{
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		/// <summary>
		/// Adds items to the checklistbox based on the members of the enum
		/// </summary>
		private void FillEnumMembers()
		{
			foreach ( string name in Enum.GetNames(enumType))
			{
				this.Items.Add(name);
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			this.OnEnumValueChanged();
		}
		protected void OnEnumValueChanged()
		{
			if (this.EnumValueChanged != null)
				this.EnumValueChanged(this, null);
		}
	}
}
