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
		public int IsResponsibleFor(Type baseType)
		{
			// -------- Specialized area --------
			if (baseType == typeof(GameObject))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Transform))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Camera))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Camera.Pass))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(SoundEmitter))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(BatchInfo))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Material))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Texture))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(RenderTarget))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Pixmap))		return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Sound))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Font))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(Scene))			return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(DrawTechnique))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(ShaderProgram))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(FormattedText))	return PropertyGrid.EditorPriority_Specialized;
			else if (baseType == typeof(TextRenderer))	return PropertyGrid.EditorPriority_Specialized;

			// -------- Semi-Specialized area --------
			else if (typeof(SpriteRenderer).IsAssignableFrom(baseType))		return PropertyGrid.EditorPriority_General + 1;
			else if (typeof(Collider).IsAssignableFrom(baseType))			return PropertyGrid.EditorPriority_General + 1;

			// -------- General area --------
			else if (typeof(Component).IsAssignableFrom(baseType))			return PropertyGrid.EditorPriority_General;
			else if (typeof(Resource).IsAssignableFrom(baseType))			return PropertyGrid.EditorPriority_General;
			else if (typeof(IContentRef).IsAssignableFrom(baseType))		return PropertyGrid.EditorPriority_General;
			else if (typeof(DualityAppData).IsAssignableFrom(baseType))		return PropertyGrid.EditorPriority_General;
			else if (typeof(DualityUserData).IsAssignableFrom(baseType))	return PropertyGrid.EditorPriority_General;
			
			else return PropertyGrid.EditorPriority_None;
		}
		public PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid)
		{
			PropertyEditor e = null;

			// -------- Specialized area --------
			if (baseType == typeof(GameObject))			e = new GameObjectOverviewPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Transform))		e = new TransformPropertyEditorContainer(parentEditor, parentGrid);
			else if (baseType == typeof(Camera))		e = new CameraPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Camera.Pass))	e = new CameraRenderPassPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(SoundEmitter))	e = new SoundEmitterPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(BatchInfo))		e = new BatchInfoPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Material))		e = new MaterialPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Texture))		e = new TexturePropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(RenderTarget))	e = new RenderTargetPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Pixmap))		e = new PixmapPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Sound))			e = new SoundPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Font))			e = new FontPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(Scene))			e = new ScenePropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(DrawTechnique))	e = new DrawTechniquePropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(ShaderProgram))	e = new ShaderProgramPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(FormattedText))	e = new FormattedTextPropertyEditor(parentEditor, parentGrid);
			else if (baseType == typeof(TextRenderer))	e = new TextRendererPropertyEditor(parentEditor, parentGrid);

			// -------- Semi-Specialized area --------
			else if (typeof(SpriteRenderer).IsAssignableFrom(baseType))		e = new SpriteRendererPropertyEditor(parentEditor, parentGrid);
			else if (typeof(Collider).IsAssignableFrom(baseType))			e = new ColliderPropertyEditor(parentEditor, parentGrid);

			// -------- General area --------
			else if (typeof(Component).IsAssignableFrom(baseType))			e = new ComponentPropertyEditor(parentEditor, parentGrid);
			else if (typeof(Resource).IsAssignableFrom(baseType))			e = new ResourcePropertyEditor(parentEditor, parentGrid);
			else if (typeof(IContentRef).IsAssignableFrom(baseType))		e = new ContentRefPropertyEditor(parentEditor, parentGrid);
			else if (typeof(DualityAppData).IsAssignableFrom(baseType))		e = new DualityAppDataPropertyEditor(parentEditor, parentGrid);
			else if (typeof(DualityUserData).IsAssignableFrom(baseType))	e = new DualityUserDataPropertyEditor(parentEditor, parentGrid);

			e.EditedType = baseType;
			return e;
		}
	}
}
