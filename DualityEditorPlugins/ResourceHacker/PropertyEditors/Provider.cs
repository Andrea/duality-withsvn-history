using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Serialization;
using Duality.Serialization.MetaFormat;
using DualityEditor.Controls;

namespace ResourceHacker.PropertyEditors
{
	public class PropertyEditorProvider : PropertyGrid.IPropertyEditorProvider
	{
		public int IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(ArrayNode))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(PrimitiveNode))	return PropertyGrid.EditorPriority_Specialized;
			else if (typeof(DataNode).IsAssignableFrom(baseType))	return PropertyGrid.EditorPriority_General;
			else return PropertyGrid.EditorPriority_None;
		}
		public PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid)
		{
			PropertyEditor e = null;

			if (baseType == typeof(ArrayNode))			e = new ArrayNodePropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(PrimitiveNode))	e = new PrimitiveNodePropertyEditor(parentEditor, parentGrid);
			else if (typeof(DataNode).IsAssignableFrom(baseType))	e = new DataNodePropertyEditor(parentEditor, parentGrid);

			e.EditedType = baseType;
			return e;
		}
	}
}
