using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;
using AdamsLair.PropertyGrid.Renderer;
using BorderStyle = AdamsLair.PropertyGrid.Renderer.BorderStyle;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public partial class PixmapPreviewPropertyEditor : PropertyEditor
	{
		protected const int HeaderHeight = 30;
		protected const int SmallHeight = 64 + HeaderHeight;
		protected const int BigHeight = 256 + HeaderHeight;

		private	Pixmap		value				= null;
		private	Bitmap		prevImage			= null;
		private	float		prevImageLum		= 0.0f;
		private	Pixmap		prevImageValue		= null;
		private	Rectangle	rectHeader			= Rectangle.Empty;
		private	Rectangle	rectPreview			= Rectangle.Empty;
		private	Rectangle	rectLabelName		= Rectangle.Empty;
		private	Rectangle	rectLabelWidth		= Rectangle.Empty;
		private	Rectangle	rectLabelHeight		= Rectangle.Empty;
		private	Rectangle	rectLabelWidthVal	= Rectangle.Empty;
		private	Rectangle	rectLabelHeightVal	= Rectangle.Empty;

		public override object DisplayedValue
		{
			get { return this.value; }
		}


		public PixmapPreviewPropertyEditor()
		{
			this.Height = SmallHeight;
			this.Hints = HintFlags.None;
		}
		
		protected void GeneratePreviewImage()
		{
			if (this.prevImageValue == this.value) return;
			this.prevImageValue = this.value;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;

			if (this.value != null)
			{
				int prevHeight = Math.Min(BigHeight - 2, this.value.Height);
				this.prevImage = this.value.GetPreviewImage(this.ClientRectangle.Width - 2, prevHeight, PreviewSizeMode.FixedHeight);
				if (this.prevImage != null)
				{
					var avgColor = this.prevImage.GetAverageColor();
					this.prevImageLum = avgColor.GetLuminance();
				}
			}
		}
		protected void AdjustPreviewHeight(bool toggle)
		{
			int targetHeight = MathF.Clamp(this.prevImage.Height + 2 + this.rectHeader.Height, SmallHeight, BigHeight);
			if (!toggle || this.Height != targetHeight)
				this.Height = targetHeight;
			else
				this.Height = SmallHeight;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Pixmap[] values = this.GetValue().Cast<Pixmap>().ToArray();
			this.value = values.NotNull().FirstOrDefault() as Pixmap;
			this.GeneratePreviewImage();
			this.AdjustPreviewHeight(false);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Rectangle rectImage = new Rectangle(this.rectPreview.X + 1, this.rectPreview.Y + 1, this.rectPreview.Width - 2, this.rectPreview.Height - 2);
			Color brightChecker = this.prevImageLum > 0.5f ? Color.FromArgb(48, 48, 48) : Color.FromArgb(224, 224, 224);
			Color darkChecker = this.prevImageLum > 0.5f ? Color.FromArgb(32, 32, 32) : Color.FromArgb(192, 192, 192);
			e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, brightChecker, darkChecker), rectImage);
			if (this.prevImage != null)
			{
				Size imgSize = this.prevImage.Size;
				float widthForHeight = (float)this.prevImage.Width / (float)this.prevImage.Height;
				if (widthForHeight * (imgSize.Height - rectImage.Height) > imgSize.Width - rectImage.Width)
				{
					imgSize.Height = Math.Min(rectImage.Height, imgSize.Height);
					imgSize.Width = MathF.RoundToInt(widthForHeight * imgSize.Height);
				}
				else
				{
					imgSize.Width = Math.Min(rectImage.Width, imgSize.Width);
					imgSize.Height = MathF.RoundToInt(imgSize.Width / widthForHeight);
				}
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				e.Graphics.DrawImage(this.prevImage, 
					rectImage.X + rectImage.Width / 2 - imgSize.Width / 2,
					rectImage.Y + rectImage.Height / 2 - imgSize.Height / 2,
					imgSize.Width,
					imgSize.Height);
				e.Graphics.InterpolationMode = InterpolationMode.Default;
			}

			ControlRenderer.DrawBorder(e.Graphics, 
				this.rectPreview, 
				BorderStyle.Simple, 
				!this.Enabled ? BorderState.Disabled : BorderState.Normal);

			bool focusBg = this.Focused || (this is IPopupControlHost && (this as IPopupControlHost).IsDropDownOpened);
			ControlRenderer.DrawGroupHeaderBackground(
				e.Graphics, 
				this.rectHeader, 
				focusBg ? SystemColors.Control.ScaleBrightness(0.85f) : SystemColors.Control, 
				GroupHeaderStyle.SmoothSunken);

			ControlRenderer.DrawStringLine(e.Graphics, 
				"Width:", 
				SystemFonts.DefaultFont, 
				this.rectLabelWidth, 
				!this.Enabled ? SystemColors.GrayText : SystemColors.ControlText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				"Height:", 
				SystemFonts.DefaultFont, 
				this.rectLabelHeight, 
				!this.Enabled ? SystemColors.GrayText : SystemColors.ControlText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.Width.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelWidthVal, 
				!this.Enabled ? SystemColors.GrayText : SystemColors.ControlText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.Height.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelHeightVal, 
				!this.Enabled ? SystemColors.GrayText : SystemColors.ControlText);

			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.Name : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelName, 
				!this.Enabled ? SystemColors.GrayText : SystemColors.ControlText,
				StringAlignment.Far);
		}
		protected override void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.AdjustPreviewHeight(true);
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			this.rectHeader = new Rectangle(
				this.ClientRectangle.X,
				this.ClientRectangle.Y,
				this.ClientRectangle.Width,
				HeaderHeight);
			this.rectPreview = new Rectangle(
				this.ClientRectangle.X,
				this.ClientRectangle.Y + HeaderHeight,
				this.ClientRectangle.Width,
				this.ClientRectangle.Height - HeaderHeight);

			Rectangle rectHeaderContent = new Rectangle(
				this.rectHeader.X + 2,
				this.rectHeader.Y + 2,
				this.rectHeader.Width - 4,
				this.rectHeader.Height - 4);

			this.rectLabelWidth = new Rectangle(
				rectHeaderContent.X,
				rectHeaderContent.Y,
				45,
				rectHeaderContent.Height / 2);
			this.rectLabelHeight = new Rectangle(
				rectHeaderContent.X,
				this.rectLabelWidth.Bottom,
				45,
				rectHeaderContent.Height / 2);

			this.rectLabelWidthVal = new Rectangle(
				this.rectLabelWidth.Right,
				rectHeaderContent.Y,
				35,
				rectHeaderContent.Height / 2);
			this.rectLabelHeightVal = new Rectangle(
				this.rectLabelHeight.Right,
				this.rectLabelWidthVal.Bottom,
				35,
				rectHeaderContent.Height / 2);
			
			this.rectLabelName = new Rectangle(
				this.rectLabelWidthVal.Right,
				rectHeaderContent.Y,
				rectHeaderContent.Width - this.rectLabelWidthVal.Right,
				rectHeaderContent.Height);
		}
	}
}
