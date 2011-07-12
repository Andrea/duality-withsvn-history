using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
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
	public partial class PixmapPreviewPropertyEditor : PropertyEditor
	{
		public PixmapPreviewPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
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
			Pixmap[] values = this.Getter().Cast<Pixmap>().ToArray();
			Pixmap first = values.NotNull().FirstOrDefault() as Pixmap;

			if (first == null)
			{
				this.labelPath.Text = "null";
				this.labelSizeValue.Text = "-\n-";
				this.previewBox.BackgroundImage = null;
			}
			else
			{
				this.labelPath.Text = first.Path;
				this.labelSizeValue.Text = string.Format("{0}\n{1}", first.PixelData.Width, first.PixelData.Height);
				this.previewBox.BackgroundImage = first.PixelData;
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
		}
		
		protected void AdjustPreviewHeight(bool toggle)
		{
			int preferredHeight = MathF.Min(this.previewBox.BackgroundImage.Height, (int)MathF.Ceiling(
				(float)this.previewBox.BackgroundImage.Height * 
				(float)this.previewBox.ClientSize.Width / 
				(float)this.previewBox.BackgroundImage.Width));
			int targetHeight = MathF.Clamp(preferredHeight + this.labelSize.Height + 2, 70, 250);
			if (!toggle || this.Height != targetHeight)
				this.Height = targetHeight;
			else
				this.Height = 70;
		}

		private void previewBox_DoubleClick(object sender, EventArgs e)
		{
			this.AdjustPreviewHeight(true);
		}
	}
}
