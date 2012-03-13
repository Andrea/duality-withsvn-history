using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

namespace EditorBase.PropertyEditors
{
	public class ResourcePropertyEditor : MemberwisePropertyEditor
	{
		public ResourcePropertyEditor()
		{
			this.PropertyName = "Resource";
			this.HeaderHeight = 20;
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.Emboss;
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			this.HeaderValueText = null;
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginHelper.RequestTypeImage(this.EditedType, CorePluginHelper.ImageContext_Icon) as System.Drawing.Bitmap;
			Duality.ColorFormat.ColorHsva avgClr = iconBitmap != null ? iconBitmap.GetAverageColor().ToHsva() : Duality.ColorFormat.ColorHsva.TransparentBlack;

			this.PropertyName = this.EditedType.GetTypeCSCodeName(true);
			this.HeaderIcon = iconBitmap;
			this.HeaderColor = ExtMethodsSystemDrawingColor.ColorFromHSV(avgClr.h, 0.2f + avgClr.s * 0.4f, 1.0f);

			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}
	}
}
