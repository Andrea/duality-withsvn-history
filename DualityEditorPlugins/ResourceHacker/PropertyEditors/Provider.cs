using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Serialization;
using DualityEditor.Controls;

namespace ResourceHacker.PropertyEditors
{
	public class PropertyEditorProvider : PropertyGrid.IPropertyEditorProvider
	{
		public int IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(BinaryMetaFormatter.ArrayNode))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(BinaryMetaFormatter.PrimitiveNode))	return PropertyGrid.EditorPriority_Specialized;
			else if (typeof(BinaryMetaFormatter.DataNode).IsAssignableFrom(baseType))	return PropertyGrid.EditorPriority_General;
			else return PropertyGrid.EditorPriority_None;
		}
		public PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid)
		{
			PropertyEditor e = null;

			if (baseType == typeof(BinaryMetaFormatter.ArrayNode))			e = new ArrayNodePropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(BinaryMetaFormatter.PrimitiveNode))	e = new PrimitiveNodePropertyEditor(parentEditor, parentGrid);
			else if (typeof(BinaryMetaFormatter.DataNode).IsAssignableFrom(baseType))	e = new DataNodePropertyEditor(parentEditor, parentGrid);

			e.EditedType = baseType;
			return e;
		}
	}
}
