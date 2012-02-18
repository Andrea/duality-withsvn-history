using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class FormattedTextPropertyEditor : MemberwisePropertyEditor
	{
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			FormattedText[] text = targets.Cast<FormattedText>().NotNull().ToArray();
			foreach (FormattedText t in text) t.ApplySource();

			this.Setter(targets);
		}
	}
}
