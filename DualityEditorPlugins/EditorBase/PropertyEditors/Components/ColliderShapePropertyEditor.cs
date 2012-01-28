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
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class ColliderShapePropertyEditor : MemberwisePropertyEditor
	{
		public ColliderShapePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
			this.Header.ResetVisible = false;
			this.Header.ActiveVisible = false;
			this.Header.Text = null;
			this.Header.ValueText = "ColliderShape";
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Collider_ShapeInfo_Parent)) return false;
			if (info is PropertyInfo && !(info as PropertyInfo).CanWrite) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			var colShapes = targets.OfType<Collider.ShapeInfo>().ToArray();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colShapes), property);

			var colliders = colShapes.Select(c => c.Parent).ToArray();
			foreach (var col in colliders) col.UpdateBodyShape();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new ObjectSelection(colliders), ReflectionInfo.Property_Collider_Shapes);
		}
	}
}
