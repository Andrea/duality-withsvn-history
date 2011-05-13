using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using DualityEditor.Controls;

namespace EditorBase.PropertyEditors
{
	public class GameObjectPropertyEditorProvider : PropertyGrid.IPropertyEditorProvider
	{
		public PropertyGrid.ProvidedEditorType IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(GameObject))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(Transform))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(SpriteRenderer))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (typeof(Component).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.General;
			else
				return PropertyGrid.ProvidedEditorType.None;
		}
		public PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid)
		{
			PropertyEditor e = null;
			if (baseType == typeof(GameObject))
				e = new GameObjectOverviewPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Transform))
				e = new TransformPropertyEditorContainer(parentEditor, parentGrid);
			else if (baseType == typeof(SpriteRenderer))
				e = new SpriteRendererPropertyEditor(parentEditor, parentGrid);
			else if (typeof(Component).IsAssignableFrom(baseType))
				e = new ComponentPropertyEditor(parentEditor, parentGrid);

			e.EditedType = baseType;
			return e;
		}
	}
}
