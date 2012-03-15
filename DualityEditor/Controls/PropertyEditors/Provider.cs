using System;
using System.Collections.Generic;
using System.Linq;

using AdamsLair.PropertyGrid;

using OpenTK;

using Duality;

namespace DualityEditor.Controls.PropertyEditors
{
	public class DualityPropertyEditorProvider : IPropertyEditorProvider
	{
		public int IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(Vector2))	return PropertyGrid.EditorPriority_General;

			else return PropertyGrid.EditorPriority_None;
		}
		public PropertyEditor CreateEditor(Type baseType)
		{
			PropertyEditor e = null;

			if (baseType == typeof(Vector2))	e = new Vector2PropertyEditor();

			return e;
		}
	}
}
