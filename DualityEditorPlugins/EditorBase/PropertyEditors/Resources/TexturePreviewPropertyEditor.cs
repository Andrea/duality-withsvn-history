using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public partial class TexturePreviewPropertyEditor : PropertyEditor
	{
		private	Bitmap[]	frameCache	= null;

		public TexturePreviewPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Texture[] values = this.Getter().Cast<Texture>().ToArray();
			Texture first = values.NotNull().FirstOrDefault() as Texture;

			this.HideFooter();
			this.labelFrameValue.Enabled = false;
			this.labelFrame.Enabled = false;

			if (first == null)
			{
				this.frameCache = null;

				this.labelPath.Text = "null";
				this.labelSizeValue.Text = "-\n-";
				this.previewBox.BackgroundImage = null;
				this.scrollAtlas.Enabled = false;
				this.scrollAtlas.Maximum = 0;
			}
			else
			{
				this.frameCache = new Bitmap[1 + (first.Atlas != null ? first.Atlas.Count : 0)];
				this.frameCache[0] = first.BasePixmap.IsAvailable ? first.BasePixmap.Res.PixelData : null;

				this.labelPath.Text = first.Path;
				this.labelSizeValue.Text = string.Format("{0}\n{1}", first.PxWidth, first.PxHeight);
				this.labelOglSizeValue.Text = string.Format("{0}\n{1}", first.OglWidth, first.OglHeight);
				this.scrollAtlas.Enabled = first.Atlas != null;
				this.scrollAtlas.Maximum = first.Atlas != null ? first.Atlas.Count : 0;
				this.UpdatePreview();

				if (this.scrollAtlas.Enabled) this.ShowFooter();
				this.AdjustPreviewHeight(false);
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle headerRect = new Rectangle(
				this.labelSize.Bounds.X,
				this.labelSize.Bounds.Y,
				this.ClientRectangle.Width,
				this.labelSize.Bounds.Height);
			Rectangle footerRect = new Rectangle(
				this.labelFrame.Bounds.X,
				this.labelFrame.Bounds.Y,
				this.ClientRectangle.Width,
				this.labelFrame.Bounds.Height);
			
			Color upperGradClr = ExtMethodsSystemDrawingColor.ColorFromHSV(
				this.ParentEditor.BackColor.GetHSVHue(),
				this.ParentEditor.BackColor.GetHSVSaturation(),
				0.78f);
			Color lowerGradClr = ExtMethodsSystemDrawingColor.ColorFromHSV(
				this.ParentEditor.BackColor.GetHSVHue(),
				this.ParentEditor.BackColor.GetHSVSaturation(),
				0.86f);

			e.Graphics.FillRectangle(
				new LinearGradientBrush(headerRect, upperGradClr, lowerGradClr, 90.0f), 
				headerRect);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(64, Color.White)),
				headerRect.Left, headerRect.Top, headerRect.Right, headerRect.Top);

			e.Graphics.FillRectangle(
				new LinearGradientBrush(footerRect, upperGradClr, lowerGradClr, 90.0f), 
				footerRect);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(64, Color.White)),
				footerRect.Left, footerRect.Top, footerRect.Right, footerRect.Top);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(128, Color.Black)),
				footerRect.Left, footerRect.Bottom - 1, footerRect.Right, footerRect.Bottom - 1);
		}

		protected void ShowFooter()
		{
			if (this.labelFrame.Visible) return;
			this.labelFrameValue.Visible = true;
			this.labelFrame.Visible = true;
			this.previewBox.Height -= this.labelFrameValue.Height;
		}
		protected void HideFooter()
		{
			if (!this.labelFrame.Visible) return;
			this.previewBox.Height += this.labelFrameValue.Height;
			this.labelFrameValue.Visible = false;
			this.labelFrame.Visible = false;
		}
		protected void AdjustPreviewHeight(bool toggle)
		{
			if (this.previewBox.BackgroundImage == null) return;

			int preferredHeight = MathF.Min(this.previewBox.BackgroundImage.Height, (int)MathF.Ceiling(
				(float)this.previewBox.BackgroundImage.Height * 
				(float)this.previewBox.ClientSize.Width / 
				(float)this.previewBox.BackgroundImage.Width));
			int targetHeight = MathF.Clamp(preferredHeight + this.labelSize.Height + (this.labelFrame.Visible ? this.labelFrame.Height : 0) + 2, 70, 275);
			if (!toggle || this.Height != targetHeight)
				this.Height = targetHeight;
			else
				this.Height = 70;
		}
		protected void UpdatePreview()
		{
			if (this.scrollAtlas.Value == 0)
			{
				this.labelFrameValue.Text = "-";
				this.labelFrameValue.Enabled = false;
				this.labelFrame.Enabled = false;
			}
			else
			{
				this.labelFrameValue.Text = (this.scrollAtlas.Value - 1).ToString();
				this.labelFrameValue.Enabled = true;
				this.labelFrame.Enabled = true;
			}

			if (this.frameCache[this.scrollAtlas.Value] == null)
			{
				Texture[] values = this.Getter().Cast<Texture>().ToArray();
				Texture first = values.NotNull().FirstOrDefault() as Texture;

				Bitmap baseBitmap = first.BasePixmap.IsAvailable ? first.BasePixmap.Res.PixelData : null;
				if (baseBitmap != null && first.Atlas != null && this.scrollAtlas.Value > 0)
				{
					Rect uvRect = first.Atlas[this.scrollAtlas.Value - 1];
					Rect pxRect = uvRect.Transform(first.OglWidth, first.OglHeight);
					this.frameCache[this.scrollAtlas.Value] = baseBitmap.SubImage((int)pxRect.x, (int)pxRect.y, (int)pxRect.w, (int)pxRect.h);
				}
				else
					this.frameCache[this.scrollAtlas.Value] = baseBitmap;
			}

			this.previewBox.BackgroundImage = this.frameCache[this.scrollAtlas.Value];
		}

		private void previewBox_DoubleClick(object sender, EventArgs e)
		{
			this.AdjustPreviewHeight(true);
		}
		private void scrollAtlas_ValueChanged(object sender, EventArgs e)
		{
			this.UpdatePreview();
		}
	}
}
