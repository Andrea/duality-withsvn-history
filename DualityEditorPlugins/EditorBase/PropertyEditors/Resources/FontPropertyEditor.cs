using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using Duality;
using Duality.Resources;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class FontPropertyEditor : ResourcePropertyEditor
	{
		protected override PropertyEditor AutoCreateMemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Font_Family))
			{
				ObjectSelectorPropertyEditor e = new ObjectSelectorPropertyEditor();
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				e.Items = System.Drawing.FontFamily.Families.Select(f => f.Name);
				this.ParentGrid.ConfigureEditor(e);
				return e;
			}
			return base.AutoCreateMemberEditor(info);
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
