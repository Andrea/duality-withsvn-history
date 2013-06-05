﻿using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using Duality;
using Duality.Resources;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class FontContentPropertyEditor : ResourcePropertyEditor
	{
		protected override PropertyEditor AutoCreateMemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Font_Family))
			{
				ObjectSelectorPropertyEditor e = new ObjectSelectorPropertyEditor();
				e.EditedType = (info as System.Reflection.PropertyInfo).PropertyType;
				e.Items = System.Drawing.FontFamily.Families.Select(f => new ObjectItem(f.Name, f.Name));
				this.ParentGrid.ConfigureEditor(e);
				return e;
			}
			return base.AutoCreateMemberEditor(info);
		}
	}
}
