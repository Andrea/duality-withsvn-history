using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Components;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class ColliderShapePropertyEditor : MemberwisePropertyEditor
	{
		public ColliderShapePropertyEditor()
		{
			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colShapes = targets.OfType<Collider.ShapeInfo>().ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colShapes), property);

			var colliders = colShapes.Select(c => c.Parent).ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_Collider_Shapes);
		}
	}
}
