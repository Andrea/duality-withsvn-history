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
	public class EnumFlagCheckedListBox : CheckedListBox
	{
		private bool	isUpdatingCheckStates	= false;
		private Type	enumType				= null;
		private Enum	enumValue				= default(Enum);

		public event EventHandler EnumValueChanged = null;


		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public Enum EnumValue
		{
			get
			{
				object e = Enum.ToObject(enumType,GetCurrentValue());
				return (Enum)e;
			}
			set
			{
				enumValue = value; // Store the current enum value
				if (enumType != value.GetType())
				{
					enumType = value.GetType(); // Store enum type
					Items.Clear();
					FillEnumMembers(); // Add items for enum members
				}
				ApplyEnumValue(); // Check/uncheck items depending on enum value
				this.OnEnumValueChanged();
			}
		}


		public EnumFlagCheckedListBox()
		{
			this.CheckOnClick = true;
		}

		/// <summary>
		/// Adds an ulong value and its associated description
		/// </summary>
		/// <param name="v"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public FlagCheckedListBoxItem Add(ulong v, string c)
		{
			FlagCheckedListBoxItem item = new FlagCheckedListBoxItem(v, c);
			Items.Add(item);
			return item;
		}
		public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
		{
			Items.Add(item);
			return item;
		}

		/// <summary>
		/// Gets the current bit value corresponding to all checked items
		/// </summary>
		/// <returns></returns>
		public ulong GetCurrentValue()
		{
			ulong sum = 0;

			for (int i = 0; i < Items.Count; i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

				if (GetItemChecked(i))
					sum |= item.value;
			}

			return sum;
		}
		
		/// <summary>
		/// Checks/Unchecks items depending on the give bitvalue
		/// </summary>
		/// <param name="value"></param>
		protected void UpdateCheckedItems(ulong value)
		{

			isUpdatingCheckStates = true;

            // Iterate over all items
			for(int i=0;i<Items.Count;i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

				if(item.value==0)
				{
					SetItemChecked(i,value==0);
				}
				else
				{

					// If the bit for the current item is on in the bitvalue, check it
					if( (item.value & value)== item.value && item.value!=0)
						SetItemChecked(i,true);
						// Otherwise uncheck it
					else
						SetItemChecked(i,false);
				}
			}

			isUpdatingCheckStates = false;

		}
		/// <summary>
		/// Updates items in the checklistbox
		/// </summary>
		/// <param name="composite">The item that was checked/unchecked</param>
		/// <param name="cs">The check state of that item</param>
		protected void UpdateCheckedItems(FlagCheckedListBoxItem composite,CheckState cs)
		{

            // If the value of the item is 0, call directly.
			if(composite.value==0)
				UpdateCheckedItems(0);


            // Get the total value of all checked items
			ulong sum = 0;
			for(int i=0;i<Items.Count;i++)
			{
				FlagCheckedListBoxItem item = Items[i] as FlagCheckedListBoxItem;

                // If item is checked, add its value to the sum.
				if(GetItemChecked(i))
					sum |= item.value;
			}

            // If the item has been unchecked, remove its bits from the sum
			if(cs==CheckState.Unchecked)
				sum = sum & (~composite.value);
            // If the item has been checked, combine its bits with the sum
			else
				sum |= composite.value;

            // Update all items in the checklistbox based on the final bit value
			UpdateCheckedItems(sum);

		}

        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            base.OnItemCheck(e);

            if (isUpdatingCheckStates)
                return;

            // Get the checked/unchecked item
            FlagCheckedListBoxItem item = Items[e.Index] as FlagCheckedListBoxItem;
            // Update other items
            UpdateCheckedItems(item, e.NewValue);

			this.OnEnumValueChanged();
        }
		protected void OnEnumValueChanged()
		{
			if (this.EnumValueChanged != null)
				this.EnumValueChanged(this, null);
		}
		
		/// <summary>
		/// Adds items to the checklistbox based on the members of the enum
		/// </summary>
		private void FillEnumMembers()
		{
			foreach ( string name in Enum.GetNames(enumType))
			{
				object val = Enum.Parse(enumType,name);
				ulong ulongVal = (ulong)Convert.ChangeType(val, typeof(ulong));

				Add(ulongVal,name);
			}
		}
		/// <summary>
		/// Checks/unchecks items based on the current value of the enum variable
		/// </summary>
		private void ApplyEnumValue()
		{
			ulong ulongVal = (ulong)Convert.ChangeType(enumValue, typeof(ulong));
			UpdateCheckedItems(ulongVal);

		}
	}

	/// <summary>
	/// Represents an item in the checklistbox
	/// </summary>
    public class FlagCheckedListBoxItem
    {
        public ulong	value	= 0;
        public string	caption	= null;


		/// <summary>
		/// Returns true if the value corresponds to a single bit being set
		/// </summary>
        public bool IsFlag
        {
            get
            {
                return ((value & (value - 1)) == 0);
            }
        }


        public FlagCheckedListBoxItem(ulong v, string c)
        {
            value = v;
            caption = c;
        }
		/// <summary>
		/// Returns true if this value is a member of the composite bit value
		/// </summary>
		/// <param name="composite"></param>
		/// <returns></returns>
        public bool IsMemberFlag(FlagCheckedListBoxItem composite)
        {
            return (IsFlag && ((value & composite.value) == value));
        }
        public override string ToString()
        {
            return caption;
        }
    }
}
