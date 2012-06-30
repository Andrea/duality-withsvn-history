using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Components;

using DualityEditor;
using DualityEditor.CorePluginInterface;


namespace EditorBase.PropertyEditors
{
	public class ColliderPropertyEditor : ComponentPropertyEditor
	{
		private	List<ColliderJointPropertyEditor>	jointEditors	= new List<ColliderJointPropertyEditor>();
		
		public override void ClearContent()
		{
			base.ClearContent();
			this.jointEditors.Clear();
		}

		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			this.UpdateJointEditors(this.GetValue().Cast<Collider>());
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			this.UpdateJointEditors(values.Cast<Collider>());
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			foreach (Collider c in targets.OfType<Collider>())
				c.AwakeBody();
		}

		protected void UpdateJointEditors(IEnumerable<Collider> values)
		{
			values = values.NotNull();
			int visibleElementCount = values.Min(o => o.Joints.Count());

			// Add missing editors
			for (int i = 0; i < visibleElementCount; i++)
			{
				Collider.JointInfo joint = values.First().Joints.ElementAtOrDefault(i);
				Type jointType = joint.GetType();
				ColliderJointPropertyEditor elementEditor;
				if (i < this.jointEditors.Count)
				{
					elementEditor = this.jointEditors[i];
					if (elementEditor.EditedType != jointType)
					{
						elementEditor.EditedType = jointType;
						this.ParentGrid.ConfigureEditor(elementEditor);
					}
				}
				else
				{
					elementEditor = new ColliderJointPropertyEditor();
					elementEditor.EditedType = jointType;
					this.ParentGrid.ConfigureEditor(elementEditor);
					this.jointEditors.Add(elementEditor);
					this.AddPropertyEditor(elementEditor);
				}
				elementEditor.PropertyName = string.Format("Joints[{0}]", i);
				elementEditor.Getter = this.CreateJointValueGetter(i);
				elementEditor.Setter = this.CreateJointValueSetter(i);
			}
			// Remove overflowing editors
			for (int i = this.jointEditors.Count - 1; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.jointEditors[i]);
				this.jointEditors.RemoveAt(i);
			}
		}

		protected Func<IEnumerable<object>> CreateJointValueGetter(int index)
		{
			return () => this.GetValue().Cast<Collider>().Select(o => o != null ? o.Joints.ElementAtOrDefault(index) : null);
		}
		protected Action<IEnumerable<object>> CreateJointValueSetter(int index)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerable<Collider.JointInfo> valuesCast = values.Cast<Collider.JointInfo>();
				Collider[] targetArray = this.GetValue().Cast<Collider>().ToArray();

				// Explicitly setting a value to null: Remove corresponding joint
				if (valuesCast.All(v => v == null))
				{
					foreach (Collider target in targetArray)
					{
						target.RemoveJoint(target.Joints.ElementAt(index));
					}
					this.PerformGetValue();
				}
			};
		}
	}

	public class ColliderJointPropertyEditor : MemberwisePropertyEditor
	{
		public ColliderJointPropertyEditor()
		{
			this.EditedType = typeof(Collider.JointInfo);
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.SmoothSunken;
			this.HeaderHeight = 30;
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			IEnumerable<Collider.JointInfo> joints = values.Cast<Collider.JointInfo>().NotNull();

			this.HeaderValueText = null;
			if (joints.Any())
				this.HeaderValueText = joints.First().GetType().Name;
			else
				this.HeaderValueText = "null";
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colJoints = targets.OfType<Collider.JointInfo>().ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colJoints), property);

			var colliders = 
				colJoints.Select(c => c.ColliderA).Concat(
				colJoints.Select(c => c.ColliderB))
				.Distinct().NotNull().ToArray();
			foreach (var c in colliders) c.AwakeBody();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_Collider_Joints);
		}
	}
}
