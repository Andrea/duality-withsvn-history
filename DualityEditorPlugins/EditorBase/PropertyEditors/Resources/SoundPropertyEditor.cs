using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class SoundPropertyEditor : ResourcePropertyEditor
	{
		public SoundPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Sound_AlBuffer)) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Sound_MinDist) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Sound_MinDistFactor) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Sound_MaxDist) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_Sound_MaxDistFactor))
			{
				this.PerformGetValue();
			}
		}
	}
}
