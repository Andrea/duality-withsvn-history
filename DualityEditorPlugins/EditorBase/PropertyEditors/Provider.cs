using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Renderers;
using DualityEditor.Controls;

namespace EditorBase.PropertyEditors
{
	public class PropertyEditorProvider : PropertyGrid.IPropertyEditorProvider
	{
		public PropertyGrid.ProvidedEditorType IsResponsibleFor(Type baseType)
		{
			if (baseType == typeof(GameObject))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(Transform))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(SpriteRenderer))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(Camera))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (baseType == typeof(BatchInfo) || baseType == typeof(Material))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (typeof(Texture).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.Specialized;
			else if (typeof(Component).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.General;
			else if (typeof(Resource).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.General;
			else if (typeof(IContentRef).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.General;
			else if (typeof(DualityAppData).IsAssignableFrom(baseType))
				return PropertyGrid.ProvidedEditorType.General;
			else if (typeof(DualityUserData).IsAssignableFrom(baseType))
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
			else if (baseType == typeof(Camera))
				e = new CameraPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(BatchInfo) || baseType == typeof(Material))
				e = new BatchInfoPropertyEditor(parentEditor, parentGrid);
			else if (typeof(Texture).IsAssignableFrom(baseType))
				e = new TexturePropertyEditor(parentEditor, parentGrid);
			else if (typeof(Component).IsAssignableFrom(baseType))
				e = new ComponentPropertyEditor(parentEditor, parentGrid);
			else if (typeof(Resource).IsAssignableFrom(baseType))
				e = new ResourcePropertyEditor(parentEditor, parentGrid);
			else if (typeof(IContentRef).IsAssignableFrom(baseType))
				e = new ContentRefPropertyEditor(parentEditor, parentGrid);
			else if (typeof(DualityAppData).IsAssignableFrom(baseType))
				e = new DualityAppDataPropertyEditor(parentEditor, parentGrid);
			else if (typeof(DualityUserData).IsAssignableFrom(baseType))
				e = new DualityUserDataPropertyEditor(parentEditor, parentGrid);

			e.EditedType = baseType;
			return e;
		}
	}
}
