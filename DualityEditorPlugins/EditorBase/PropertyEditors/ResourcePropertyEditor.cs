using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.ColorFormat;

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

			Resource[] resValues = values.OfType<Resource>().ToArray();
			if (resValues.Length == 1) this.HeaderValueText = resValues[0].FullName;
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();

			System.Drawing.Bitmap iconBitmap = CorePluginRegistry.RequestTypeImage(this.EditedType) as System.Drawing.Bitmap;
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
