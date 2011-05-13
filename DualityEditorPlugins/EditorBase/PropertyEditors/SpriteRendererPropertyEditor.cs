using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

namespace EditorBase.PropertyEditors
{
	public class SpriteRendererPropertyEditor : ComponentPropertyEditor
	{
		public SpriteRendererPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{

		}

		protected override bool MemberPredicate(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_SpriteRenderer_BoundRadius)) return false;
			return base.MemberPredicate(info);
		}
	}
}
