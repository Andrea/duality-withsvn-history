using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Duality;
using Duality.ColorFormat;

namespace DualityDebugging
{
	public partial class BitmapForm : Form
	{
		private	Bitmap		bmp			= null;
		private	ColorRgba	avgColor	= ColorRgba.Black;

		public Bitmap Bitmap
		{
			get { return this.bmp; }
			set
			{
				this.bmp = value;
				this.avgColor = this.bmp != null ? this.bmp.GetAverageColor() : ColorRgba.Black;
				this.UpdateSize();
				this.Invalidate();
			}
		}

		public BitmapForm()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		protected void UpdateSize()
		{
			if (this.bmp == null) return;
			int xdiff = this.Width - this.ClientRectangle.Width;
			int ydiff = this.Height - this.ClientRectangle.Height;
			this.Width = Math.Min(xdiff + this.bmp.Width, 600);
			this.Height = Math.Min(ydiff + this.bmp.Height, 600);
			this.AutoScrollMinSize = this.bmp.Size;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.UpdateSize();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Color brightChecker = this.avgColor.GetLuminance() > 0.5f ? Color.FromArgb(48, 48, 48) : Color.FromArgb(224, 224, 224);
			Color darkChecker = this.avgColor.GetLuminance() > 0.5f ? Color.FromArgb(32, 32, 32) : Color.FromArgb(192, 192, 192);
			e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, brightChecker, darkChecker), this.ClientRectangle);
			e.Graphics.DrawImage(this.bmp, this.AutoScrollPosition.X, this.AutoScrollPosition.Y, this.bmp.Width, this.bmp.Height);
		}
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			this.Invalidate();
		}
	}
}
