using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.ColorFormat;

using DualityEditor;
using DualityEditor.CorePluginInterface;

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
			DualityEditorApp.NotifyObjPropChanged(this, 
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
		protected override bool IsChildNonPublic(PropertyEditor childEditor)
		{
			if (base.IsChildNonPublic(childEditor)) return true;
			if (childEditor.EditedMember is FieldInfo) return true; // Discourage use of fields in Components
			return false;
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
		protected override void OnValueChanged(object sender, PropertyEditorValueEventArgs args)
		{
			base.OnValueChanged(sender, args);

			// Find the direct descendant editor on the path to the changed one
			PropertyEditor directChild = args.Editor;
			while (directChild != null && !this.HasPropertyEditor(directChild))
				directChild = directChild.ParentEditor;

			// If an editor has changed that was NOT a direct descendant, invoke its setter to trigger OnPropertySet / OnFieldSet.
			// Always remember: If we don't emit a PropertyChanged event, PrefabLinks won't update their change lists!
			if (directChild != null && directChild != args.Editor && directChild.EditedMember != null)
				directChild.PerformSetValue();
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			DualityEditorApp.NotifyObjPropChanged(this.ParentGrid, new DualityEditor.ObjectSelection(targets), property);
		}
		protected override void OnFieldSet(FieldInfo field, IEnumerable<object> targets)
		{
			base.OnFieldSet(field, targets);
			// This is a bad workaround for having a purely Property-based change event system: Simply notify "something" changed.
			// Change to something better in the future.
			DualityEditorApp.NotifyObjPropChanged(this.ParentGrid, new DualityEditor.ObjectSelection(targets));
		}
		protected override void OnActiveChanged()
		{
			base.OnActiveChanged();
			if (!this.IsUpdatingFromObject) this.PerformSetActive(this.Active);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginRegistry.RequestTypeImage(this.EditedType, CorePluginRegistry.ImageContext_Icon) as System.Drawing.Bitmap;
			ColorHsva avgClr = iconBitmap != null ? 
				iconBitmap.GetAverageColor().ToHsva() : 
				Duality.ColorFormat.ColorHsva.TransparentWhite;
			if (avgClr.S <= 0.05f)
			{
				avgClr = new ColorHsva(
					0.001f * (float)(this.EditedType.Name.GetHashCode() % 1000), 
					1.0f, 
					0.5f);
			}

			this.PropertyName = this.EditedType.GetTypeCSCodeName(true);
			this.HeaderIcon = iconBitmap;
			this.HeaderColor = ExtMethodsSystemDrawingColor.ColorFromHSV(avgClr.H, 0.2f, 0.8f);

			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}
	}
}
