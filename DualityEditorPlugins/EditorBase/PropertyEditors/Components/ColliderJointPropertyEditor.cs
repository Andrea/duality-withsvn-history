using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Components;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
#if FALSE // Removed for now. Joints are an experimental feature.
	public class ColliderJointPropertyEditor : MemberwisePropertyEditor
	{
		public ColliderJointPropertyEditor()
		{
			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colJoints = targets.OfType<Collider.JointInfo>().ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colJoints), property);

			var colliders = 
				colJoints.Select(c => c.ColliderA).Concat(
				colJoints.Select(c => c.ColliderB))
				.Distinct().ToArray();
			foreach (var c in colliders) c.AwakeBody();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_Collider_Joints);
		}
	}
#endif
}
