using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class ScenePropertyEditor : ResourcePropertyEditor
	{
		public ScenePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{

		}

		protected override bool MemberPredicate(System.Reflection.MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Scene_GlobalGravity)) return true;
			return false;
		}
	}
}
