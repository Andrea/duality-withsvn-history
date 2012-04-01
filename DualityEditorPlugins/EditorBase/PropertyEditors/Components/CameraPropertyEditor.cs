using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Duality;
using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class CameraPropertyEditor : ComponentPropertyEditor
	{
		protected override PropertyEditor AutoCreateMemberEditor(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Camera_VisibilityMask))
			{
				BitmaskPropertyEditor e = new BitmaskPropertyEditor();
				e.EditedType = (info as PropertyInfo).PropertyType;
				// ToDo: Use actual user-definable visibility groups
				List<BitmaskItem> items = Enumerable.Range(0, 31).Select(i => new BitmaskItem(1UL << i, "Group " + i)).ToList();
				items.Insert(0, new BitmaskItem(0, "None"));
				items.Add(new BitmaskItem((1UL << 32) - 1, "All"));
				e.Items = items;
				this.ParentGrid.ConfigureEditor(e);
				return e;
			}
			return base.AutoCreateMemberEditor(info);
		}
	}
}
