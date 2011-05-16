using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class ComponentPropertyEditor : MemberwisePropertyEditor
	{
		public ComponentPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
			this.Header.ResetVisible = false;
			this.Header.ActiveVisible = true;
			this.Header.Text = null;
			this.Header.ValueText = "Component";
		}
		
		public void PerformSetActive(bool active)
		{
			Component[] values = this.Getter().Cast<Component>().NotNull().ToArray();
			foreach (Component c in values) c.ActiveSingle = active;

			// Notify ActiveSingle changed
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, 
				new ObjectSelection(values), 
				ReflectionHelper.Property_Component_ActiveSingle);
		}

		protected override bool IsChildValueModified(PropertyEditor childEditor)
		{
			Component[] values = this.Getter().Cast<Component>().NotNull().ToArray();
			return values.Any(delegate (Component c)
			{
				Duality.Resources.PrefabLink l = c.GameObj.AffectedByPrefabLink;
				return l != null && l.HasChange(c, this.memberMap[childEditor] as PropertyInfo);
			});
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (info.DeclaringType == typeof(Component)) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);

			this.Header.Text = null;
			this.Header.ValueText = ReflectionHelper.GetTypeString(this.EditedType, ReflectionHelper.TypeStringAttrib.CSCodeIdentShort);
			if (!values.Any() || values.All(o => o == null))
				this.ActiveState = false;
			else
				this.ActiveState = (values.First(o => o is Component) as Component).ActiveSingle;
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new DualityEditor.ObjectSelection(targets), property);
		}
		protected override void OnActiveStateChanged()
		{
			base.OnActiveStateChanged();
			this.PerformSetActive(this.ActiveState);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginHelper.RequestTypeImage(this.EditedType, CorePluginHelper.ImageContext_Icon) as System.Drawing.Bitmap;
			Duality.ColorFormat.ColorHSVA avgClr = iconBitmap != null ? iconBitmap.GetAverageColor().ToHsva() : Duality.ColorFormat.ColorHSVA.TransparentBlack;

			this.Header.Text = null;
			this.Header.ValueText = ReflectionHelper.GetTypeString(this.EditedType, ReflectionHelper.TypeStringAttrib.CSCodeIdentShort);
			this.Header.Icon = iconBitmap;
			this.Header.ForeColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
				avgClr.h, 
				0.15f, 
				this.Header.ForeColor.GetHSVBrightness());
			this.Header.BackColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
				avgClr.h, 
				0.15f, 
				this.Header.BackColor.GetHSVBrightness());
		}
	}
}
