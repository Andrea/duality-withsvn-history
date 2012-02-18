using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Components;
using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class ColliderShapePropertyEditor : MemberwisePropertyEditor
	{
		public ColliderShapePropertyEditor()
		{
			this.Header.ResetVisible = false;
			this.Header.ActiveVisible = false;
			this.Header.Text = null;
			this.Header.ValueText = "ColliderShape";
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
