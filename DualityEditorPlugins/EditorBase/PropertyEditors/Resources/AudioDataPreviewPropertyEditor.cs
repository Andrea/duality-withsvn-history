using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.Renderer;
using BorderStyle = AdamsLair.PropertyGrid.Renderer.BorderStyle;

using Duality;
using Duality.Resources;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public partial class AudioDataPreviewPropertyEditor : PropertyEditor
	{
		protected const int HeaderHeight = 32;
		protected const int PreferredHeight = 64 + HeaderHeight;

		private	AudioData	value				= null;
		private	Bitmap		prevImage			= null;
		private	float		prevImageLum		= 0.0f;
		private	int			prevImageValue		= 0;

		public override object DisplayedValue
		{
			get { return this.value; }
		}
		public override bool CanGetFocus
		{
			get { return false; }
		}


		public AudioDataPreviewPropertyEditor()
		{
			this.Height = PreferredHeight;
			this.Hints = HintFlags.None;
		}
		
		protected void GeneratePreviewImage()
		{
			int ovLen = this.value != null ? this.value.OggVorbisData.Length : 0;
			if (this.prevImageValue == ovLen) return;
			this.prevImageValue = ovLen;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;

			if (this.value != null)
			{
				this.prevImage = PreviewProvider.GetPreviewImage(this.value, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - 2, PreviewSizeMode.FixedHeight);
				if (this.prevImage != null)
				{
					var avgColor = this.prevImage.GetAverageColor();
					this.prevImageLum = avgColor.GetLuminance();
				}
			}

			this.Invalidate();
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			AudioData lastValue = this.value;
			AudioData[] values = this.GetValue().Cast<AudioData>().ToArray();
			this.value = values.NotNull().FirstOrDefault() as AudioData;
			this.GeneratePreviewImage();
			if (this.value != lastValue) this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Rectangle rectPreview = this.ClientRectangle;
			Rectangle rectImage = new Rectangle(rectPreview.X + 1, rectPreview.Y + 1, rectPreview.Width - 2, rectPreview.Height - 2);
			Color brightChecker = this.prevImageLum > 0.5f ? Color.FromArgb(48, 48, 48) : Color.FromArgb(224, 224, 224);
			Color darkChecker = this.prevImageLum > 0.5f ? Color.FromArgb(32, 32, 32) : Color.FromArgb(192, 192, 192);
			e.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeCheckerBoard, brightChecker, darkChecker), rectImage);
			if (this.prevImage != null)
			{
				TextureBrush bgImageBrush = new TextureBrush(this.prevImage);
				bgImageBrush.ResetTransform();
				bgImageBrush.TranslateTransform(rectImage.X, rectImage.Y);
				e.Graphics.FillRectangle(bgImageBrush, rectImage);
			}

			ControlRenderer.DrawBorder(e.Graphics, 
				rectPreview, 
				BorderStyle.Simple, 
				!this.Enabled ? BorderState.Disabled : BorderState.Normal);
		}
	}
}
