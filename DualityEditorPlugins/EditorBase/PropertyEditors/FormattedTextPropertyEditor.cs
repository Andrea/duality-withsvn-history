using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class FormattedTextPropertyEditor : MemberwisePropertyEditor
	{
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			FormattedText[] text = targets.Cast<FormattedText>().NotNull().ToArray();
			foreach (FormattedText t in text) t.ApplySource();

			this.SetValues(targets);
		}
	}
}
