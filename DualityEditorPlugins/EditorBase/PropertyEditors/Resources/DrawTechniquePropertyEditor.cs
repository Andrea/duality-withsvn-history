﻿using System.Linq;
using System.Reflection;

using Duality;
using Duality.Resources;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class DrawTechniquePropertyEditor : ResourcePropertyEditor
	{
		protected override PropertyEditor AutoCreateMemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_DrawTechnique_PreferredVertexFormat))
			{
				ObjectSelectorPropertyEditor e = new ObjectSelectorPropertyEditor();
				e.EditedType = (info as PropertyInfo).PropertyType;
				e.Items = DrawTechnique.VertexTypeIndices.Select(pair => new ObjectItem(pair.Value, pair.Key));
				this.ParentGrid.ConfigureEditor(e);
				return e;
			}
			return base.AutoCreateMemberEditor(info);
		}
	}
}
