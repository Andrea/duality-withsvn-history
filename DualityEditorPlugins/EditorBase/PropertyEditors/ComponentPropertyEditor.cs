using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using DualityEditor;

namespace EditorBase.PropertyEditors
{
	public class ComponentPropertyEditor : MemberwisePropertyEditor
	{
		public ComponentPropertyEditor()
		{
			this.Hints |= HintFlags.HasActiveCheck | HintFlags.ActiveEnabled;
			this.PropertyName = "Component";
			this.HeaderHeight = 20;
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.Emboss;
		}

		public void PerformSetActive(bool active)
		{
			Component[] values = this.GetValue().Cast<Component>().NotNull().ToArray();
			foreach (Component c in values) c.ActiveSingle = active;

			// Notify ActiveSingle changed
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, 
				new ObjectSelection(values), 
				ReflectionInfo.Property_Component_ActiveSingle);
		}

		protected override bool IsAutoCreateMember(MemberInfo info)
		{
			return base.IsAutoCreateMember(info) && info.DeclaringType != typeof(Component);
		}
		protected override bool IsChildValueModified(PropertyEditor childEditor)
		{
			MemberInfo info = childEditor.EditedMember;
			if (info == null) return false;

			Component[] values = this.GetValue().Cast<Component>().NotNull().ToArray();
			return values.Any(delegate (Component c)
			{
				Duality.Resources.PrefabLink l = c.GameObj.AffectedByPrefabLink;
				return l != null && l.HasChange(c, info as PropertyInfo);
			});
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);

			this.PropertyName = this.EditedType.GetTypeCSCodeName(true);
			this.HeaderValueText = null;
			if (!values.Any() || values.All(o => o == null))
				this.Active = false;
			else
				this.Active = (values.First(o => o is Component) as Component).ActiveSingle;
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this.ParentGrid, new DualityEditor.ObjectSelection(targets), property);
		}
		protected override void OnActiveChanged()
		{
			base.OnActiveChanged();
			if (!this.IsUpdatingFromObject) this.PerformSetActive(this.Active);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginHelper.RequestTypeImage(this.EditedType, CorePluginHelper.ImageContext_Icon) as System.Drawing.Bitmap;
			Duality.ColorFormat.ColorHsva avgClr = iconBitmap != null ? iconBitmap.GetAverageColor().ToHsva() : Duality.ColorFormat.ColorHsva.TransparentBlack;

			this.PropertyName = this.EditedType.GetTypeCSCodeName(true);
			this.HeaderIcon = iconBitmap;
			this.HeaderColor = ExtMethodsSystemDrawingColor.ColorFromHSV(avgClr.H, 0.2f + avgClr.S * 0.2f, 0.85f);

			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}
	}
}
