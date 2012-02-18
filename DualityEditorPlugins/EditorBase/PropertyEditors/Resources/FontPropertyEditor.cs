using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class FontPropertyEditor : ResourcePropertyEditor
	{
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Font_Family))
			{
				StringEnumPropertyEditor e = new StringEnumPropertyEditor();
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				e.Editor.Items.Clear();
				foreach (System.Drawing.FontFamily f in System.Drawing.FontFamily.Families)
					e.Editor.Items.Add(f.Name);
				return e;
			}
			return base.MemberEditor(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			Font[] fntArr = targets.Cast<Font>().ToArray();
			bool anyReload = false;
			foreach (Font fnt in fntArr)
			{
				if (fnt.NeedsReload) 
				{
					fnt.ReloadData();
					anyReload = true;
				}
			}

			if (anyReload)
			{
				this.PerformGetValue();
			}
		}
	}
}
