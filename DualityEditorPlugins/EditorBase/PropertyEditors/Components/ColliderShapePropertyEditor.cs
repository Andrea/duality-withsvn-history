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
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Collider_ShapeInfo_Friction) ||
				ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Collider_ShapeInfo_Restitution))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor((info as PropertyInfo).PropertyType, this, this.ParentGrid);
				NumericPropertyEditor numEdit = e as NumericPropertyEditor;
				if (numEdit != null)
				{
					numEdit.Editor.Minimum = 0.0m;
					numEdit.Editor.Maximum = 1.0m;
					numEdit.Editor.Increment = 0.05m;
				}
				return e;
			}
			else if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_Collider_ShapeInfo_Density))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor((info as PropertyInfo).PropertyType, this, this.ParentGrid);
				NumericPropertyEditor numEdit = e as NumericPropertyEditor;
				if (numEdit != null)
				{
					numEdit.Editor.Minimum = 0.0m;
					numEdit.Editor.Maximum = 100.0m;
					numEdit.Editor.Increment = 0.05m;
				}
				return e;
			}
			else
			{
				PropertyEditor e = base.MemberEditor(info);
				// Force write-back of any array type that might be defined in a shape. The shape might need a write-back (set) to update itsself.
				if (e is IListPropertyEditor) (e as IListPropertyEditor).ForceWriteBack = true;
				return e;
			}
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
