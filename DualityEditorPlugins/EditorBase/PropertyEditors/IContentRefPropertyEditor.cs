using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.EditorTemplates;
using AdamsLair.PropertyGrid.Renderer;
using ButtonState = AdamsLair.PropertyGrid.Renderer.ButtonState;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public class IContentRefPropertyEditor : PropertyEditor
	{
		private static readonly IconImage iconShow = new IconImage(EditorBase.PluginRes.EditorBaseRes.IconEye.ToBitmap());
		private static readonly IconImage iconReset = new IconImage(EditorBase.Properties.Resources.cross);

		protected	string		contentPath			= null;
		protected	bool		multiple			= false;
		protected	bool		dragHover			= false;
		protected	Rectangle	rectPanel			= Rectangle.Empty;
		protected	Rectangle	rectButtonReset		= Rectangle.Empty;
		protected	Rectangle	rectButtonShow		= Rectangle.Empty;
		protected	bool		buttonResetHovered	= false;
		protected	bool		buttonResetPressed	= false;
		protected	bool		buttonShowHovered	= false;
		protected	bool		buttonShowPressed	= false;
		protected	bool		panelHovered		= false;
		private		Point		panelDragBegin		= Point.Empty;
		private		Bitmap		bgImage				= null;
		private		Color		bgImageColor		= Color.White;
		protected	string		bgImagePath			= null;
		
		public override object DisplayedValue
		{
			get 
			{ 
				IContentRef ctRef = ReflectionHelper.CreateInstanceOf(this.EditedType) as IContentRef;
				ctRef.Path = this.contentPath;
				return ctRef;
			}
		}


		public IContentRefPropertyEditor()
		{
			this.Height = 22;
		}

		public void ShowReferencedContent()
		{
			if (string.IsNullOrEmpty(this.contentPath)) return;
			ProjectFolderView view = EditorBasePlugin.Instance.RequestProjectView();
			view.FlashNode(view.NodeFromPath(this.contentPath));
			System.Media.SystemSounds.Beep.Play();
		}
		public void ResetReference()
		{
			if (this.ReadOnly) return;
			this.contentPath = null;
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			IContentRef[] values = this.GetValue().Cast<IContentRef>().ToArray();

			this.BeginUpdate();
			if (!values.Any())
			{
				this.contentPath = null;
			}
			else
			{
				IContentRef first = values.NotNull().FirstOrDefault();
				this.contentPath = first.Path;
				this.multiple = (values.Any(o => o == null) || values.Any(o => o.Path != first.Path));

				this.GenerateBackgroundImage();
			}
			this.EndUpdate();
		}

		protected void GenerateBackgroundImage()
		{
			if (this.bgImagePath == this.contentPath) return;
			this.bgImagePath = this.contentPath;

			if (this.bgImage != null) this.bgImage.Dispose();
			this.bgImage = null;
			this.Height = 22;

			Resource res = (this.DisplayedValue as IContentRef).Res;
			if (res != null)
			{
				this.bgImage = res.GetPreviewImage(this.ClientRectangle.Width - 4 - 22, 64 - 4, PreviewSizeMode.FixedHeight);
				if (this.bgImage != null)
				{
					this.Height = 64;
					var avgColor = this.bgImage.GetAverageColor();
					float luminance = avgColor.GetLuminance();
					if (luminance < 0.4f)
						luminance = 1.0f;
					else
						luminance = 0.0f;
					int lumVal = MathF.RoundToInt(255.0f * luminance);
					this.bgImageColor = Color.FromArgb(lumVal, lumVal, lumVal);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Color bgColorBright = Color.White;
			if (this.dragHover) bgColorBright = bgColorBright.MixWith(Color.FromArgb(192, 255, 0), 0.4f);
			else if (this.multiple) bgColorBright = Color.Bisque;

			Rectangle rectImage = new Rectangle(this.rectPanel.X + 2, this.rectPanel.Y + 2, this.rectPanel.Width - 4, this.rectPanel.Height - 4);
			if (this.bgImage == null)
			{
				if (this.ReadOnly || !this.Enabled)
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, bgColorBright)), rectImage);
				else
					e.Graphics.FillRectangle(new SolidBrush(bgColorBright), rectImage);
			}
			else
			{
				Color bgImageBaseColor = this.bgImageColor;
				if (this.dragHover) bgImageBaseColor = bgImageBaseColor.MixWith(Color.FromArgb(192, 255, 0), 0.4f);
				else if (this.multiple) bgImageBaseColor = bgImageBaseColor.MixWith(Color.FromArgb(255, 200, 128), 0.4f);

				e.Graphics.FillRectangle(new SolidBrush(bgImageBaseColor), rectImage);

				TextureBrush bgImageBrush = new TextureBrush(this.bgImage);
				bgImageBrush.ResetTransform();
				bgImageBrush.TranslateTransform(rectImage.X, rectImage.Y);
				e.Graphics.FillRectangle(bgImageBrush, rectImage);

				if (this.ReadOnly || !this.Enabled)
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, bgImageBaseColor)), rectImage);
			}

			StringFormat format = StringFormat.GenericDefault;
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			format.Trimming = StringTrimming.EllipsisPath;
			SizeF textSize = e.Graphics.MeasureString(
				(this.DisplayedValue as IContentRef).FullName,
				SystemFonts.DefaultFont,
				new SizeF(this.rectPanel.Width, this.rectPanel.Height),
				format);

			Rectangle rectText;
			if (this.bgImage == null)
				rectText = this.rectPanel;
			else
				rectText = new Rectangle(
					this.rectPanel.X, this.rectPanel.Bottom - (int)textSize.Height - 2, this.rectPanel.Width, (int)textSize.Height);

			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, bgColorBright)), 
				rectText.X + rectText.Width / 2 - textSize.Width / 2 - 2, 
				rectText.Y + rectText.Height / 2 - textSize.Height / 2 - 2, 
				textSize.Width + 2, 
				textSize.Height + 2);
			e.Graphics.DrawString(
				(this.DisplayedValue as IContentRef).FullName,
				SystemFonts.DefaultFont,
				new SolidBrush(this.Enabled ? SystemColors.ControlText : SystemColors.GrayText),
				rectText,
				format);
			
			e.Graphics.DrawRectangle(new Pen((this.ReadOnly || !this.Enabled) ? Color.FromArgb(128, SystemColors.ControlLightLight) : SystemColors.ControlLightLight), 
				this.rectPanel.X + 1,
				this.rectPanel.Y + 1,
				this.rectPanel.Width - 3,
				this.rectPanel.Height - 3);
			e.Graphics.DrawRectangle(new Pen((this.ReadOnly || !this.Enabled) ? Color.FromArgb(128, SystemColors.ControlDark) : SystemColors.ControlDark),
				this.rectPanel.X,
				this.rectPanel.Y,
				this.rectPanel.Width - 1,
				this.rectPanel.Height - 1);

			ButtonState buttonStateReset = ButtonState.Disabled;
			if (!this.ReadOnly && this.Enabled && !string.IsNullOrEmpty(this.contentPath))
			{
				if (this.buttonResetPressed)		buttonStateReset = ButtonState.Pressed;
				else if (this.buttonResetHovered)	buttonStateReset = ButtonState.Hot;
				else								buttonStateReset = ButtonState.Normal;
			}
			ControlRenderer.DrawButton(
				e.Graphics, 
				this.rectButtonReset, 
				buttonStateReset, 
				null, 
				iconReset);

			ButtonState buttonStateShow = ButtonState.Disabled;
			if (this.Enabled && !string.IsNullOrEmpty(this.contentPath))
			{
				if (this.buttonShowPressed)							buttonStateShow = ButtonState.Pressed;
				else if (this.buttonShowHovered || this.Focused)	buttonStateShow = ButtonState.Hot;
				else												buttonStateShow = ButtonState.Normal;
			}
			ControlRenderer.DrawButton(
				e.Graphics, 
				this.rectButtonShow, 
				buttonStateShow, 
				null, 
				iconShow);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return)
			{
				this.ShowReferencedContent();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				DataObject data = new DataObject();
				data.SetContentRefs(new[] { this.DisplayedValue as IContentRef });
				Clipboard.SetDataObject(data);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.V && e.Control)
			{
				DataObject data = Clipboard.GetDataObject() as DataObject;
				if (data.ContainsContentRefs())
				{
					IContentRef[] refArray = data.GetContentRefs();
					this.contentPath = (refArray != null && refArray.Length > 0) ? refArray[0].Path : null;
					this.PerformSetValue();
					this.OnValueChanged();
					this.PerformGetValue();
					this.OnEditingFinished();
				}
				else
					System.Media.SystemSounds.Beep.Play();

				e.Handled = true;
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			bool lastButtonResetHovered = this.buttonResetHovered;
			bool lastButtonShowHovered = this.buttonShowHovered;
			bool lastPanelHovered = this.panelHovered;

			this.buttonResetHovered = !this.ReadOnly && this.rectButtonReset.Contains(e.Location);
			this.buttonShowHovered = this.rectButtonShow.Contains(e.Location);
			this.panelHovered = this.rectPanel.Contains(e.Location);

			if (lastButtonResetHovered != this.buttonResetHovered || 
				lastButtonShowHovered != this.buttonShowHovered || 
				lastPanelHovered != this.panelHovered) 
				this.Invalidate();
			
			if (this.panelDragBegin != Point.Empty)
			{
				if (Math.Abs(this.panelDragBegin.X - e.X) > 5 || Math.Abs(this.panelDragBegin.Y - e.Y) > 5)
				{
					DataObject dragDropData = new DataObject();
					dragDropData.SetContentRefs(new[] { this.DisplayedValue as IContentRef });
					//dragDropData.SetAllowedConvertOp(ConvertOperation.Operation.Convert);
					this.ParentGrid.DoDragDrop(dragDropData, DragDropEffects.All | DragDropEffects.Link);
				}
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.buttonResetHovered || this.buttonShowHovered || this.panelHovered) this.Invalidate();
			this.buttonResetHovered = false;
			this.buttonShowHovered = false;
			this.panelHovered = false;
			this.panelDragBegin = Point.Empty;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.buttonResetHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.buttonResetPressed = true;
				this.Invalidate();
			}
			else if (this.buttonShowHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.buttonShowPressed = true;
				this.Invalidate();
			}
			else if (this.panelHovered)
			{
				this.panelDragBegin = e.Location;
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.buttonResetPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.buttonResetPressed && this.buttonResetHovered) this.ResetReference();
				this.buttonResetPressed = false;
				this.Invalidate();
			}
			else if (this.buttonShowPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				if (this.buttonShowPressed && this.buttonShowHovered) this.ShowReferencedContent();
				this.buttonShowPressed = false;
				this.Invalidate();
			}
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (this.panelHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
				this.ShowReferencedContent();
		}
		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			if (this.ReadOnly || !this.Enabled) return;
			DataObject data = e.Data as DataObject;
			if (data.ContainsContentRefs())
			{
				e.Effect = e.AllowedEffect;
				if (!this.dragHover) this.Invalidate();
				this.dragHover = true;
			}
		}
		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			if (this.ReadOnly || !this.Enabled) return;
			if (this.dragHover) this.Invalidate();
			this.dragHover = false;
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (this.ReadOnly || !this.Enabled) return;
			if (this.dragHover) this.Invalidate();
			this.dragHover = false;

			DataObject data = e.Data as DataObject;
			if (data.ContainsContentRefs())
			{
				IContentRef[] refArray = data.GetContentRefs();
				this.contentPath = (refArray != null && refArray.Length > 0) ? refArray[0].Path : null;
				this.PerformSetValue();
				this.OnValueChanged();
				this.PerformGetValue();
				this.OnEditingFinished();
			}
		}

		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			int buttonWidth = 22;

			if (this.Height >= 44)
			{
				this.rectButtonShow = new Rectangle(
					this.ClientRectangle.Right - buttonWidth,
					this.ClientRectangle.Top + this.ClientRectangle.Height / 2 - buttonWidth,
					buttonWidth,
					buttonWidth);
				this.rectButtonReset = new Rectangle(
					this.ClientRectangle.Right - buttonWidth,
					this.rectButtonShow.Bottom,
					buttonWidth,
					buttonWidth);
				this.rectPanel = new Rectangle(
					this.ClientRectangle.X,
					this.ClientRectangle.Y,
					this.ClientRectangle.Width - buttonWidth,
					this.ClientRectangle.Height);
			}
			else
			{
				this.rectButtonShow = new Rectangle(
					this.ClientRectangle.Right - buttonWidth - buttonWidth,
					this.ClientRectangle.Top,
					buttonWidth,
					this.ClientRectangle.Height);
				this.rectButtonReset = new Rectangle(
					this.ClientRectangle.Right - buttonWidth,
					this.ClientRectangle.Top,
					buttonWidth,
					this.ClientRectangle.Height);
				this.rectPanel = new Rectangle(
					this.ClientRectangle.X,
					this.ClientRectangle.Y,
					this.ClientRectangle.Width - buttonWidth * 2,
					this.ClientRectangle.Height);
			}
		}
	}
}

