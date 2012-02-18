using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Duality;
using Duality.Components;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class ColliderPropertyEditor : ComponentPropertyEditor
	{
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			foreach (Collider c in targets.OfType<Collider>())
				c.AwakeBody();
		}
	}
}
