﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CustomPropertyGrid.Renderer;
using CustomPropertyGrid.EditorTemplates;

namespace CustomPropertyGrid.PropertyEditors
{
	public class FlaggedEnumPropertyEditor : BitmaskPropertyEditor
	{
		public override object DisplayedValue
		{
			get { return Enum.ToObject(this.EditedType, this.BitmaskValue); }
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			this.Items = Enum.GetNames(this.EditedType).Select(n => 
				new BitmaskItem((ulong)Convert.ToUInt64(Enum.Parse(this.EditedType, n)), n));
		}
	}
}
