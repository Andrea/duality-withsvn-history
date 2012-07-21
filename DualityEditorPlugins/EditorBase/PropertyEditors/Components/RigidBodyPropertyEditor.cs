﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;

using Duality;
using Duality.Components.Physics;

using DualityEditor;
using DualityEditor.CorePluginInterface;


namespace EditorBase.PropertyEditors
{
	public class RigidBodyPropertyEditor : ComponentPropertyEditor
	{
		private ColliderJointAddNewPropertyEditor	addJointEditor	= null;
		private	List<ColliderJointPropertyEditor>	jointEditors	= new List<ColliderJointPropertyEditor>();
		
		public override void ClearContent()
		{
			base.ClearContent();
			this.jointEditors.Clear();
			this.addJointEditor = null;
		}
		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			this.UpdateJointEditors(this.GetValue().Cast<RigidBody>());
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			this.UpdateJointEditors(values.Cast<RigidBody>());
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			foreach (RigidBody c in targets.OfType<RigidBody>())
				c.AwakeBody();
		}
		protected override bool IsChildValueModified(PropertyEditor childEditor)
		{
			if (this.jointEditors.Contains(childEditor))
			{
				Component[] values = this.GetValue().Cast<Component>().NotNull().ToArray();
				return values.Any(c => 
				{
					Duality.Resources.PrefabLink l = c.GameObj.AffectedByPrefabLink;
					return l != null && l.HasChange(c, ReflectionInfo.Property_RigidBody_Joints);
				});
			}
			else return base.IsChildValueModified(childEditor);
		}

		protected void UpdateJointEditors(IEnumerable<RigidBody> values)
		{
			RigidBody[] valArray = values.ToArray();
			int visibleElementCount = valArray.NotNull().Min(o => o.Joints == null ? 0 : o.Joints.Count());

			// Add missing editors
			for (int i = 0; i < visibleElementCount; i++)
			{
				JointInfo joint = valArray.NotNull().First().Joints.ElementAtOrDefault(i);
				Type jointType = joint.GetType();
				bool matchesAll = valArray.NotNull().All(r => jointType.IsInstanceOfType(r.Joints.ElementAtOrDefault(i)));
				if (matchesAll)
				{
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
					}
					elementEditor.PropertyName = string.Format("Joints[{0}]", i);
					elementEditor.Getter = this.CreateJointValueGetter(i);
					elementEditor.Setter = this.CreateJointValueSetter(i);
					elementEditor.ParentCollider = valArray;
					if (!this.HasPropertyEditor(this.jointEditors[i])) this.AddPropertyEditor(this.jointEditors[i], i);
				}
				else if (this.jointEditors.Count > i)
				{
					this.RemovePropertyEditor(this.jointEditors[i]);
				}
			}
			// Remove overflowing editors
			for (int i = this.jointEditors.Count - 1; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.jointEditors[i]);
				this.jointEditors.RemoveAt(i);
			}

			// Add "Add joint" editor
			if (this.addJointEditor == null)
			{
				this.addJointEditor = new ColliderJointAddNewPropertyEditor();
				this.addJointEditor.Getter = this.CreateAddNewJointValueGetter();
				this.addJointEditor.Setter = v => {};
				this.ParentGrid.ConfigureEditor(this.addJointEditor);
				this.AddPropertyEditor(this.addJointEditor);
			}
		}

		protected Func<IEnumerable<object>> CreateJointValueGetter(int index)
		{
			return () => this.GetValue().Cast<RigidBody>().Select(o => o != null ? o.Joints.ElementAtOrDefault(index) : null);
		}
		protected Action<IEnumerable<object>> CreateJointValueSetter(int index)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerable<JointInfo> valuesCast = values.Cast<JointInfo>();
				RigidBody[] targetArray = this.GetValue().Cast<RigidBody>().ToArray();

				// Explicitly setting a value to null: Remove corresponding joint
				if (valuesCast.All(v => v == null))
				{
					foreach (RigidBody target in targetArray)
					{
						target.RemoveJoint(target.Joints.ElementAt(index));
					}
					this.PerformGetValue();
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(targetArray), ReflectionInfo.Property_RigidBody_Joints);
				}
			};
		}
		
		protected Func<IEnumerable<object>> CreateAddNewJointValueGetter()
		{
			return () => 
			{
				this.addJointEditor.TargetColliders = this.GetValue().Cast<RigidBody>().ToArray();
				return new object[] { this.addJointEditor.DisplayedValue };
			};
		}
	}

	public class ColliderJointPropertyEditor : MemberwisePropertyEditor
	{
		private	RigidBody[] parentCollider = null;
		private PropertyEditor otherColEditor = null;

		public RigidBody[] ParentCollider
		{
			get { return this.parentCollider; }
			internal set { this.parentCollider = value; }
		}

		public ColliderJointPropertyEditor()
		{
			this.EditedType = typeof(JointInfo);
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.SmoothSunken;
			this.HeaderHeight = 30;
		}

		public override void ClearContent()
		{
			base.ClearContent();
			this.otherColEditor = null;
		}
		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			JointInfo joint = this.GetValue().Cast<JointInfo>().FirstOrDefault();

			if (joint != null && joint.DualJoint)
			{
				if (this.otherColEditor == null)
				{
					this.otherColEditor = this.ParentGrid.CreateEditor(typeof(RigidBody), this);
					this.otherColEditor.Getter = this.CreateOtherColValueGetter();
					this.otherColEditor.Setter = this.CreateOtherColValueSetter();
					this.otherColEditor.PropertyName = PluginRes.EditorBaseRes.PropertyName_OtherCollider;
					this.otherColEditor.PropertyDesc = PluginRes.EditorBaseRes.PropertyDesc_OtherCollider;
					this.ParentGrid.ConfigureEditor(this.otherColEditor);
					this.AddPropertyEditor(this.otherColEditor);
				}
			}
			else if (this.otherColEditor != null)
			{
				this.RemovePropertyEditor(this.otherColEditor);
				this.otherColEditor = null;
			}
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			IEnumerable<JointInfo> joints = values.Cast<JointInfo>().NotNull();

			this.HeaderValueText = null;
			if (joints.Any())
				this.HeaderValueText = joints.First().GetType().Name;
			else
				this.HeaderValueText = "null";
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colJoints = targets.OfType<JointInfo>().ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colJoints), property);

			var colliders = 
				colJoints.Select(c => c.BodyA).Concat(
				colJoints.Select(c => c.BodyB))
				.Distinct().NotNull().ToArray();
			foreach (var c in colliders) c.AwakeBody();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_RigidBody_Joints);
		}

		protected Func<IEnumerable<object>> CreateOtherColValueGetter()
		{
			return () => 
			{
				JointInfo[] targetArray = this.GetValue().Cast<JointInfo>().ToArray();
				RigidBody[] otherCollider = new RigidBody[targetArray.Length];
				for (int i = 0; i < targetArray.Length; i++)
				{
					if (targetArray[i] != null)
						otherCollider[i] = targetArray[i].BodyA == parentCollider[i] ? targetArray[i].BodyB : targetArray[i].BodyA;
					else
						otherCollider[i] = null;
				}
				return otherCollider;
			};
		}
		protected Action<IEnumerable<object>> CreateOtherColValueSetter()
		{
			return delegate(IEnumerable<object> values)
			{
				RigidBody[] valueArray = values.Cast<RigidBody>().ToArray();
				JointInfo[] targetArray = this.GetValue().Cast<JointInfo>().ToArray();

				for (int i = 0; i < targetArray.Length; i++)
				{
					if (targetArray[i] == null) continue;
					parentCollider[i].AddJoint(targetArray[i], valueArray[i]);
				}

				this.PerformGetValue();
			};
		}
	}

	public class ColliderJointAddNewPropertyEditor : ObjectSelectorPropertyEditor
	{
		private RigidBody[]	targetArray	= null;

		public RigidBody[] TargetColliders
		{
			get { return this.targetArray; }
			set { this.targetArray = value; }
		}

		public ColliderJointAddNewPropertyEditor()
		{
			this.EditedType = typeof(Type);
			this.ButtonIcon = AdamsLair.PropertyGrid.EmbeddedResources.Resources.ImageAdd;
			this.Hints = HintFlags.Default | HintFlags.HasButton | HintFlags.ButtonEnabled;
			this.PropertyName = PluginRes.EditorBaseRes.PropertyName_AddJoint;
			this.PropertyDesc = PluginRes.EditorBaseRes.PropertyDesc_AddJoint;

			this.Items = 
				from t in DualityApp.GetAvailDualityTypes(typeof(JointInfo))
				where !t.IsAbstract
				select new ObjectItem(t, t.Name);
		}
		protected override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();
			if (this.ReadOnly)
				this.Hints &= ~HintFlags.ButtonEnabled;
			else
				this.Hints |= HintFlags.ButtonEnabled;
		}
		protected override void OnButtonPressed()
		{
			base.OnButtonPressed();
			Type jointType = this.DisplayedValue as Type;
			if (jointType == null) return;

			foreach (RigidBody c in this.targetArray)
			{
				JointInfo joint = (jointType.CreateInstanceOf() ?? jointType.CreateInstanceOf(true)) as JointInfo;
				c.AddJoint(joint);
			}
			this.ParentEditor.PerformGetValue();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(targetArray), ReflectionInfo.Property_RigidBody_Joints);
		}
	}
}