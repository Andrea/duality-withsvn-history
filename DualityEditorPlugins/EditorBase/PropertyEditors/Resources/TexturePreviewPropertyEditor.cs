using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.EditorTemplates;
using AdamsLair.PropertyGrid.Renderer;
using BorderStyle = AdamsLair.PropertyGrid.Renderer.BorderStyle;

using Duality;
using Duality.Resources;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public partial class TexturePreviewPropertyEditor : PropertyEditor
	{
		protected const int HeaderHeight = 30;
		protected const int SmallHeight = 64 + HeaderHeight;
		protected const int BigHeight = 256 + HeaderHeight;

		private	Texture			value				= null;
		private	Bitmap			prevImage			= null;
		private	List<Bitmap>	prevImageFrame		= new List<Bitmap>();
		private	float			prevImageLum		= 0.0f;
		private	Texture			prevImageValue		= null;
		private	Rectangle	rectHeader			= Rectangle.Empty;
		private	Rectangle	rectPreview			= Rectangle.Empty;
		private	Rectangle	rectLabelName		= Rectangle.Empty;
		private	Rectangle	rectLabelWidth		= Rectangle.Empty;
		private	Rectangle	rectLabelHeight		= Rectangle.Empty;
		private	Rectangle	rectLabelWidthVal	= Rectangle.Empty;
		private	Rectangle	rectLabelHeightVal	= Rectangle.Empty;
		private	Rectangle	rectLabelOglWidth		= Rectangle.Empty;
		private	Rectangle	rectLabelOglHeight		= Rectangle.Empty;
		private	Rectangle	rectLabelOglWidthVal	= Rectangle.Empty;
		private	Rectangle	rectLabelOglHeightVal	= Rectangle.Empty;
		private	NumericEditorTemplate	subImageSelector	= null;

		public override object DisplayedValue
		{
			get { return this.value; }
		}


		public TexturePreviewPropertyEditor()
		{
			this.subImageSelector = new NumericEditorTemplate(this);
			this.subImageSelector.Edited += this.subImageSelector_Edited;
			this.subImageSelector.Invalidate += this.subImageSelector_Invalidate;
			this.subImageSelector.ReadOnly = false;
			this.subImageSelector.Minimum = -1;
			this.subImageSelector.Maximum = -1;
			this.subImageSelector.Value = -1;

			this.Height = SmallHeight;
			this.Hints = HintFlags.None;
		}
		
		protected void GeneratePreviewImage()
		{
			if (this.prevImageValue == this.value) return;
			this.prevImageValue = this.value;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;
			this.ClearFramePreviews();

			if (this.value != null)
			{
				int prevHeight = Math.Min(BigHeight - 2, this.value.PxHeight);
				this.prevImage = this.value.GetPreviewImage(this.ClientRectangle.Width - 2, prevHeight, PreviewSizeMode.FixedHeight);
				if (this.prevImage != null)
				{
					var avgColor = this.prevImage.GetAverageColor();
					this.prevImageLum = avgColor.GetLuminance();
				}
			}
		}
		protected void ClearFramePreviews()
		{
			foreach (Bitmap bmp in this.prevImageFrame.NotNull()) bmp.Dispose();
			this.prevImageFrame.Clear();
		}
		protected Bitmap GetPreviewFrame(int frameIndex)
		{
			if (this.value == null) return null;
			this.GeneratePreviewImage();

			if (frameIndex == -1) return this.prevImage;

			if (this.prevImage == null) return null;
			if (this.value.Atlas == null) return null;
			if (!this.value.BasePixmap.IsAvailable) return null;

			while (this.prevImageFrame.Count <= frameIndex) this.prevImageFrame.Add(null);
			if (this.prevImageFrame[frameIndex] == null)
			{
				Rect uvRect = this.value.Atlas[frameIndex];
				Rect pxRect = uvRect.Transform(this.value.OglWidth, this.value.OglHeight);
				this.prevImageFrame[frameIndex] = this.value.BasePixmap.Res.PixelData.SubImage((int)pxRect.x, (int)pxRect.y, (int)pxRect.w, (int)pxRect.h);
			}

			return this.prevImageFrame[frameIndex];
		}
		protected void AdjustPreviewHeight(bool toggle)
		{
			int targetHeight = MathF.Clamp(this.prevImage == null ? 0 : this.prevImage.Height + 2 + this.rectHeader.Height, SmallHeight, BigHeight);
			if (!toggle || this.Height != targetHeight)
				this.Height = targetHeight;
			else
				this.Height = SmallHeight;
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Texture[] values = this.GetValue().Cast<Texture>().ToArray();
			this.value = values.NotNull().FirstOrDefault() as Texture;
			this.ClearFramePreviews();
			this.GeneratePreviewImage();
			this.AdjustPreviewHeight(false);

			if (this.value != null && this.value.Atlas != null)
			{
				this.subImageSelector.ReadOnly = false;
				this.subImageSelector.Maximum = this.value.Atlas.Count - 1;
			}
			else
			{
				this.subImageSelector.ReadOnly = true;
				this.subImageSelector.Value = -1;
				this.subImageSelector.Maximum = -1;
			}
			this.UpdateGeometry();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Rectangle rectImage = new Rectangle(this.rectPreview.X + 1, this.rectPreview.Y + 1, this.rectPreview.Width - 2, this.rectPreview.Height - 2);
			Color brightChecker = this.prevImageLum > 0.5f ? Color.FromArgb(48, 48, 48) : Color.FromArgb(224, 224, 224);
			Color darkChecker = this.prevImageLum > 0.5f ? Color.FromArgb(32, 32, 32) : Color.FromArgb(192, 192, 192);
			Bitmap img = this.GetPreviewFrame((int)this.subImageSelector.Value);
			e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, brightChecker, darkChecker), rectImage);
			if (img != null)
			{
				Size imgSize = img.Size;
				float widthForHeight = (float)imgSize.Width / (float)imgSize.Height;
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
				e.Graphics.DrawImage(img, 
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
				focusBg ? ControlRenderer.ColorBackground.ScaleBrightness(0.85f) : ControlRenderer.ColorBackground, 
				GroupHeaderStyle.SmoothSunken);
			
			ControlRenderer.DrawStringLine(e.Graphics, 
				"Width:", 
				SystemFonts.DefaultFont, 
				this.rectLabelWidth, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				"Height:", 
				SystemFonts.DefaultFont, 
				this.rectLabelHeight, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.PxWidth.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelWidthVal, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.PxHeight.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelHeightVal, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);

			ControlRenderer.DrawStringLine(e.Graphics, 
				"OglWidth:", 
				SystemFonts.DefaultFont, 
				this.rectLabelOglWidth, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				"OglHeight:", 
				SystemFonts.DefaultFont, 
				this.rectLabelOglHeight, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.OglWidth.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelOglWidthVal, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);
			ControlRenderer.DrawStringLine(e.Graphics, 
				this.value != null ? this.value.OglHeight.ToString() : " - ", 
				SystemFonts.DefaultFont, 
				this.rectLabelOglHeightVal, 
				!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText);

			if (this.subImageSelector.Rect.Width > 0)
			{
				ControlRenderer.DrawStringLine(e.Graphics, 
					"Frame Index", 
					SystemFonts.DefaultFont, 
					this.rectLabelName, 
					!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText,
					StringAlignment.Far);
				this.subImageSelector.OnPaint(e, this.Enabled && !this.subImageSelector.ReadOnly, false);
			}
			else
			{
				ControlRenderer.DrawStringLine(e.Graphics, 
					this.value != null ? this.value.Name : " - ", 
					SystemFonts.DefaultFont, 
					this.rectLabelName, 
					!this.Enabled ? ControlRenderer.ColorGrayText : ControlRenderer.ColorText,
					StringAlignment.Far);
			}
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.subImageSelector.OnGotFocus(e);
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.subImageSelector.OnLostFocus(e);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			this.subImageSelector.OnKeyDown(e);
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			this.subImageSelector.OnKeyUp(e);
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.subImageSelector.OnKeyPress(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.subImageSelector.OnMouseLeave(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.subImageSelector.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.subImageSelector.OnMouseUp(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.subImageSelector.OnMouseMove(e);
		}
		protected override void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.AdjustPreviewHeight(true);
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			int subImageSelWidth = this.subImageSelector.ReadOnly ? 0 : 40;
			this.subImageSelector.Rect = new Rectangle(
				this.ClientRectangle.Right - subImageSelWidth,
				this.ClientRectangle.Y + HeaderHeight / 2 - 20 / 2,
				subImageSelWidth,
				20);
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
				this.rectHeader.Width - 4 - this.subImageSelector.Rect.Width,
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

			this.rectLabelOglWidth = new Rectangle(
				this.rectLabelWidthVal.Right,
				rectHeaderContent.Y,
				65,
				rectHeaderContent.Height / 2);
			this.rectLabelOglHeight = new Rectangle(
				this.rectLabelWidthVal.Right,
				this.rectLabelOglWidth.Bottom,
				65,
				rectHeaderContent.Height / 2);

			this.rectLabelOglWidthVal = new Rectangle(
				this.rectLabelOglWidth.Right,
				rectHeaderContent.Y,
				35,
				rectHeaderContent.Height / 2);
			this.rectLabelOglHeightVal = new Rectangle(
				this.rectLabelOglHeight.Right,
				this.rectLabelOglWidthVal.Bottom,
				35,
				rectHeaderContent.Height / 2);
			
			this.rectLabelName = new Rectangle(
				this.rectLabelOglWidthVal.Right,
				rectHeaderContent.Y,
				rectHeaderContent.Width - this.rectLabelOglWidthVal.Right,
				rectHeaderContent.Height);
		}

		private void subImageSelector_Edited(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		private void subImageSelector_Invalidate(object sender, EventArgs e)
		{
			this.Invalidate();
		}
	}
}
