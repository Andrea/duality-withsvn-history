using System.Collections.Generic;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using DualityEditor;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public class ResourcePropertyEditor : MemberwisePropertyEditor
	{
		private	bool	preventFocus	= false;

		public override bool CanGetFocus
		{
			get { return base.CanGetFocus && !this.preventFocus; }
		}
		public bool PreventFocus
		{
			get { return this.preventFocus; }
			set { this.preventFocus = value; }
		}

		public ResourcePropertyEditor()
		{
			this.PropertyName = "Resource";
			this.HeaderHeight = 20;
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.Emboss;
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginRegistry.RequestTypeImage(this.EditedType, CorePluginRegistry.ImageContext_Icon) as System.Drawing.Bitmap;
			Duality.ColorFormat.ColorHsva avgClr = iconBitmap != null ? iconBitmap.GetAverageColor().ToHsva() : Duality.ColorFormat.ColorHsva.TransparentBlack;

			this.PropertyName = this.EditedType.GetTypeCSCodeName(true);
			this.HeaderIcon = iconBitmap;
			this.HeaderColor = ExtMethodsSystemDrawingColor.ColorFromHSV(avgClr.H, 0.2f + avgClr.S * 0.2f, 0.85f);

			this.Hints &= ~HintFlags.HasButton;
			this.Hints &= ~HintFlags.ButtonEnabled;
		}
	}
}
