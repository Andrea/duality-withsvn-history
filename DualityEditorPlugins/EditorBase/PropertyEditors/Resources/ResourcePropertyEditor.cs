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
	public class ResourcePropertyEditor : MemberwisePropertyEditor
	{
		public ResourcePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Resource_Disposed)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Resource_Path)) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
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
				0.15f + avgClr.s * 0.3f, 
				this.Header.ForeColor.GetHSVBrightness());
			this.Header.BackColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
				avgClr.h, 
				0.15f + avgClr.s * 0.3f, 
				this.Header.BackColor.GetHSVBrightness());

			// Nice at first glance, but far too many colors overall
			//this.BackColor = ExtMethodsSystemDrawingColor.ColorFromHSV(
			//    avgClr.h, 
			//    0.05f + avgClr.s * 0.05f, 
			//    Control.DefaultBackColor.GetHSVBrightness());
		}
	}
}
