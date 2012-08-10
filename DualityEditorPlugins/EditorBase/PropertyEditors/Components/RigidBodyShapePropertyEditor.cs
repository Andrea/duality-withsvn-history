using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Components.Physics;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class RigidBodyShapePropertyEditor : MemberwisePropertyEditor
	{
		public RigidBodyShapePropertyEditor()
		{
			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colShapes = targets.OfType<ShapeInfo>().ToArray();
			DualityEditorApp.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colShapes), property);

			var colliders = colShapes.Select(c => c.Parent).ToArray();
			foreach (var c in colliders) c.AwakeBody();
			DualityEditorApp.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_RigidBody_Shapes);
		}
	}
}
