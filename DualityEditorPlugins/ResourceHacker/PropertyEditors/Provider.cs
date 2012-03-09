using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Serialization;
using Duality.Serialization.MetaFormat;
using DualityEditor.Controls;

namespace ResourceHacker.PropertyEditors
{
	public class PropertyEditorProvider : IPropertyEditorProvider
	{
		public int IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(ArrayNode))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(PrimitiveNode))	return PropertyGrid.EditorPriority_Specialized;
			else return PropertyGrid.EditorPriority_None;
		}
		public PropertyEditor CreateEditor(Type baseType)
		{
			PropertyEditor e = null;

			if (baseType == typeof(ArrayNode))			e = new ArrayNodePropertyEditor();
			else if (baseType == typeof(PrimitiveNode))	e = new PrimitiveNodePropertyEditor();

			return e;
		}
	}
}
