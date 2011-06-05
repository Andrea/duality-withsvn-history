using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DualityEditor.Controls
{
	public partial class GroupedPropertyEditor : PropertyEditor
	{
		private	List<PropertyEditor>	propertyEditors		= new List<PropertyEditor>();
		private	bool					activeState			= true;
		private	bool					modifiedStateCache	= false;

		public override bool Expanded
		{
			get { return this.tableLayout.Visible; }
			set 
			{ 
				if (this.tableLayout.Visible != value)
				{
					this.tableLayout.Visible = value;
					this.header.Invalidate();
					if (value && !this.ContentInitialized) this.InitContent();
				}
			}
		}
		public override string PropertyName
		{
			get { return this.header.Text; }
			set { this.header.Text = value; }
		}
		public bool ActiveState
		{
			get { return this.activeState; }
			set 
			{ 
				if (this.activeState != value)
				{
					this.activeState = value; 
					this.Header.Invalidate();
					this.OnActiveStateChanged();
				}
			}
		}
		public int Indent
		{
			get { return this.tableLayout.Padding.Left; }
			set 
			{
				this.tableLayout.Padding = new Padding(
					value, 
					this.tableLayout.Padding.Top,
					this.tableLayout.Padding.Right,
					this.tableLayout.Padding.Bottom);
			}
		}
		public GroupedPropertyEditorHeader Header
		{
			get { return this.header; }
		}
		public IEnumerable<PropertyEditor> PropertyEditors
		{
			get { return this.propertyEditors; }
		}

		protected GroupedPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
		}
		
		public override void ClearContent()
		{
			base.ClearContent();
			this.ClearPropertyEditors();
		}
		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
			foreach (PropertyEditor e in this.propertyEditors)
				e.UpdateReadOnlyState();
		}
		public override void UpdateModifiedState()
		{
			base.UpdateModifiedState();
			this.modifiedStateCache = this.ValueModified;

			// Set font boldness according to modified value
			if (this.header.Font.Bold != this.modifiedStateCache)
				this.header.Font = new Font(this.header.Font, this.modifiedStateCache ? FontStyle.Bold : FontStyle.Regular);

			foreach (PropertyEditor e in this.propertyEditors)
				e.UpdateModifiedState();
		}
		protected override bool IsChildValueModified(PropertyEditor childEditor)
		{
			return this.modifiedStateCache;
		}

		protected virtual void OnActiveStateChanged()
		{

		}

		protected void BeginUpdate()
		{
			this.tableLayout.SuspendLayout();
			this.SuspendLayout();
		}
		protected void EndUpdate()
		{
			this.tableLayout.ResumeLayout(true);
			this.ResumeLayout(true);
		}
		protected void AddPropertyEditor(PropertyEditor editor)
		{
			editor.Dock = DockStyle.Top;
			editor.ValueEdited += this.OnValueEdited;

			this.propertyEditors.Add(editor);
			this.tableLayout.Controls.Add(editor);
		}
		protected void RemovePropertyEditor(PropertyEditor editor)
		{
			editor.ValueEdited -= this.OnValueEdited;

			this.propertyEditors.Remove(editor);
			this.tableLayout.Controls.Remove(editor);
		}
		protected void ClearPropertyEditors()
		{
			this.SuspendLayout();
			foreach (PropertyEditor e in this.propertyEditors)
			{
				e.ValueEdited -= this.OnValueEdited;
			}
			this.tableLayout.Controls.Clear();
			this.propertyEditors.Clear();
			this.ResumeLayout(true);
		}
	}

	public class GroupedPropertyEditorHeader : Control
	{
		private	bool	expandVisible	= true;
		private	bool	activeVisible	= false;
		private	bool	resetVisible	= true;
		private	bool	resetIsInit		= false;
		private	bool	expandEnabled	= true;
		private	bool	activeEnabled	= true;
		private	bool	resetEnabled	= true;
		private	string	valueText		= null;
		private	bool	expandHovered	= false;
		private	bool	activeHovered	= false;
		private	bool	resetHovered	= false;
		private	Image	icon			= null;

		public event EventHandler ResetClicked = null;

		public bool ExpandVisible
		{
			get { return this.expandVisible; }
			set { this.expandVisible = value; this.Invalidate(); }
		}
		public bool ActiveVisible
		{
			get { return this.activeVisible; }
			set { this.activeVisible = value; this.Invalidate(); }
		}
		public bool ResetVisible
		{
			get { return this.resetVisible; }
			set { this.resetVisible = value; this.Invalidate(); }
		}
		public bool ResetIsInit
		{
			get { return this.resetIsInit; }
			set { this.resetIsInit = value; this.Invalidate(); }
		}
		public bool ExpandEnabled
		{
			get { return this.expandEnabled; }
			set { this.expandEnabled = value; this.Invalidate(); }
		}
		public bool ActiveEnabled
		{
			get { return this.activeEnabled; }
			set { this.activeEnabled = value; this.Invalidate(); }
		}
		public bool ResetEnabled
		{
			get { return this.resetEnabled; }
			set { this.resetEnabled = value; this.Invalidate(); }
		}
		public string ValueText
		{
			get { return this.valueText; }
			set { this.valueText = value; this.Invalidate(); }
		}
		public Image Icon
		{
			get { return this.icon; }
			set { this.icon = value; this.Invalidate(); }
		}

		public GroupedPropertyEditor Editor
		{
			get { return this.Parent as GroupedPropertyEditor; }
		}
		public Rectangle ExpandButtonArea
		{
			get
			{
				if (!this.expandVisible) return new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, 0, 0);
				return new Rectangle(
					this.ClientRectangle.X + 3,
					this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - 6,
					12, 12);
			}
		}
		public Rectangle ActiveButtonArea
		{
			get
			{
				if (!this.activeVisible) return new Rectangle(this.ExpandButtonArea.Right, this.ClientRectangle.Y, 0, 0);
				return new Rectangle(
					this.ExpandButtonArea.Right + 3,
					this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - 6,
					12, 12);
			}
		}
		public Rectangle IconArea
		{
			get
			{
				if (this.icon == null) return new Rectangle(this.ActiveButtonArea.Right, this.ClientRectangle.Y, 0, 0);
				return new Rectangle(
					this.ActiveButtonArea.Right + 5,
					this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - 8,
					16, 16);
			}
		}
		public Rectangle ResetButtonArea
		{
			get
			{
				if (!this.resetVisible) return new Rectangle(this.ClientRectangle.Right, this.ClientRectangle.Y, 0, 0);
				return new Rectangle(
					this.ClientRectangle.Right - 16 - 3,
					this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - 8,
					16, 16);
			}
		}

		protected internal GroupedPropertyEditorHeader()
		{
			this.DoubleBuffered = true;
			this.ForeColor = Color.FromArgb(245, 245, 245);
			this.BackColor = Color.FromArgb(200, 200, 200);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (this.expandHovered && this.expandEnabled && this.expandVisible) this.Editor.Expanded = !this.Editor.Expanded;
			if (this.activeHovered && this.activeEnabled && this.activeVisible) this.Editor.ActiveState = !this.Editor.ActiveState;
			if (this.resetHovered && this.resetEnabled && this.resetVisible) this.OnResetClicked();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			bool lastHovered;

			lastHovered = this.expandHovered;
			if (this.expandVisible)
			{
				Rectangle expandButtonHoverArea = new Rectangle(
					this.ClientRectangle.X,
					this.ClientRectangle.Y,
					this.ExpandButtonArea.Right + 2 - this.ClientRectangle.X,
					this.ClientRectangle.Height);
				this.expandHovered = expandButtonHoverArea.Contains(e.Location);
			}
			else this.expandHovered = false;
			if (this.expandHovered != lastHovered) this.Invalidate();

			lastHovered = this.activeHovered;
			if (this.activeVisible)
			{
				Rectangle activeButtonHoverArea = new Rectangle(
					this.ExpandButtonArea.Right + 2,
					this.ClientRectangle.Y,
					this.ActiveButtonArea.Right + 2 - this.ExpandButtonArea.Right,
					this.ClientRectangle.Height);
				this.activeHovered = activeButtonHoverArea.Contains(e.Location);
			}
			else this.activeHovered = false;
			if (this.activeHovered != lastHovered) this.Invalidate();

			lastHovered = this.resetHovered;
			if (this.resetVisible)
			{
				Rectangle resetButtonHoverArea = this.ResetButtonArea;
				resetButtonHoverArea = new Rectangle(
					resetButtonHoverArea.Left - 4,
					this.ClientRectangle.Y,
					this.ClientRectangle.Right - (resetButtonHoverArea.Left - 4),
					this.ClientRectangle.Height);
				this.resetHovered = resetButtonHoverArea.Contains(e.Location);
			}
			else this.resetHovered = false;
			if (this.resetHovered != lastHovered) this.Invalidate();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.expandHovered = false;
			this.activeHovered = false;
			this.resetHovered = false;
			this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Pen upperLeftPen = new Pen(Color.FromArgb(64, Color.White));
			Pen lowerRightPen = new Pen(Color.FromArgb(64, Color.Black));
			LinearGradientBrush bgBrush = new LinearGradientBrush(
				this.ClientRectangle, 
				this.ForeColor, 
				this.BackColor,
				90.0f);

			e.Graphics.FillRectangle(bgBrush, this.ClientRectangle);
			e.Graphics.DrawLine(upperLeftPen, this.ClientRectangle.Left, this.ClientRectangle.Bottom, this.ClientRectangle.Left, this.ClientRectangle.Top);
			e.Graphics.DrawLine(upperLeftPen, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Right, this.ClientRectangle.Top);
			e.Graphics.DrawLine(lowerRightPen, this.ClientRectangle.Left, this.ClientRectangle.Bottom - 1, this.ClientRectangle.Right, this.ClientRectangle.Bottom - 1);
			e.Graphics.DrawLine(lowerRightPen, this.ClientRectangle.Right - 1, this.ClientRectangle.Bottom, this.ClientRectangle.Right - 1, this.ClientRectangle.Top);
			
			StringFormat headerTextFormat = StringFormat.GenericDefault;
			headerTextFormat.LineAlignment = StringAlignment.Center;
			if (this.Height <= 25)
			{
				headerTextFormat.Trimming = StringTrimming.EllipsisCharacter;
				headerTextFormat.FormatFlags = StringFormatFlags.NoWrap;
			}
			else
			{
				headerTextFormat.Trimming = StringTrimming.EllipsisCharacter;
				headerTextFormat.FormatFlags = StringFormatFlags.LineLimit;
			}

			Rectangle iconArea = this.IconArea;
			Rectangle expandButtonArea = this.ExpandButtonArea;
			Rectangle activeButtonArea = this.ActiveButtonArea;
			Rectangle resetButtonArea = this.ResetButtonArea;
			Rectangle headerTextArea = new Rectangle(
				iconArea.Right + 3,
				this.ClientRectangle.Y + 2,
				this.valueText != null ? ((this.Editor != null ? this.Editor.NameLabelWidth : this.ClientRectangle.Width / 2) - iconArea.Right - 3 - resetButtonArea.Width) : this.ClientRectangle.Width - iconArea.Right - 3 - resetButtonArea.Width,
				this.ClientRectangle.Height - 4);
			Rectangle valueTextArea = new Rectangle(
				!string.IsNullOrEmpty(this.Text) ? headerTextArea.Right : headerTextArea.Left,
				this.ClientRectangle.Y + 2,
				this.ClientRectangle.Width - ((!string.IsNullOrEmpty(this.Text) ? headerTextArea.Right : headerTextArea.Left) - this.ClientRectangle.Left) - (this.ClientRectangle.Right - resetButtonArea.Left),
				this.ClientRectangle.Height - 4);

			if (this.expandVisible)
			{
				bool expandEnabledTemp = this.expandEnabled && this.Enabled;
				CheckBoxRenderer.DrawCheckBox(e.Graphics,
 					expandButtonArea.Location,
					expandEnabledTemp ? (this.expandHovered ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal) : CheckBoxState.UncheckedDisabled);
				Color plusSignColor = Color.FromArgb(0, 0, 0); // (48, 64, 164);

				// Plus Shadow
				Pen expandLinePen = new Pen(Color.FromArgb(expandEnabledTemp ? 48 : 16, plusSignColor));
				e.Graphics.DrawLine(expandLinePen, 
					expandButtonArea.Left + 3,
					expandButtonArea.Top + expandButtonArea.Height / 2 + 1,
					expandButtonArea.Right - 3,
					expandButtonArea.Top + expandButtonArea.Height / 2 + 1);
				e.Graphics.DrawLine(expandLinePen, 
					expandButtonArea.Left + 3,
					expandButtonArea.Top + expandButtonArea.Height / 2 - 1,
					expandButtonArea.Right - 3,
					expandButtonArea.Top + expandButtonArea.Height / 2 - 1);
				if (this.Editor == null || !this.Editor.Expanded)
				{
					e.Graphics.DrawLine(expandLinePen, 
						expandButtonArea.Left + expandButtonArea.Width / 2 + 1,
						expandButtonArea.Top + 3,
						expandButtonArea.Left + expandButtonArea.Width / 2 + 1,
						expandButtonArea.Bottom - 3);
					e.Graphics.DrawLine(expandLinePen, 
						expandButtonArea.Left + expandButtonArea.Width / 2 - 1,
						expandButtonArea.Top + 3,
						expandButtonArea.Left + expandButtonArea.Width / 2 - 1,
						expandButtonArea.Bottom - 3);
				}
				// Plus
				expandLinePen = new Pen(Color.FromArgb(expandEnabledTemp ? 255 : 128, plusSignColor));
				e.Graphics.DrawLine(expandLinePen, 
					expandButtonArea.Left + 3,
					expandButtonArea.Top + expandButtonArea.Height / 2,
					expandButtonArea.Right - 3,
					expandButtonArea.Top + expandButtonArea.Height / 2);
				if (this.Editor == null || !this.Editor.Expanded)
				{
					e.Graphics.DrawLine(expandLinePen, 
						expandButtonArea.Left + expandButtonArea.Width / 2,
						expandButtonArea.Top + 3,
						expandButtonArea.Left + expandButtonArea.Width / 2,
						expandButtonArea.Bottom - 3);
				}
			}

			if (this.activeVisible)
			{
				bool activeEnabledTemp = this.activeEnabled && this.Enabled;
				CheckBoxState activeState;
				if (this.Editor.ActiveState)
					activeState = activeEnabledTemp ? (this.activeHovered ? CheckBoxState.CheckedHot : CheckBoxState.CheckedNormal) : CheckBoxState.CheckedDisabled;
				else
					activeState = activeEnabledTemp ? (this.activeHovered ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal) : CheckBoxState.UncheckedDisabled;
				CheckBoxRenderer.DrawCheckBox(e.Graphics, activeButtonArea.Location, activeState);
			}

			if (this.icon != null)
			{
				ImageAttributes ia = new ImageAttributes();
				if (!this.Enabled)
				{
					ColorMatrix cm = new ColorMatrix(new float[][]{
						new float[] {0.299f, 0.299f, 0.299f,	0,		0},
						new float[] {0.587f, 0.587f, 0.587f,	0,		0},
						new float[] {0.114f, 0.114f, 0.114f,	0,		0},
						new float[] {     0,      0,      0,	0.75f,	0},
						new float[] {     0,      0,      0,	0,		0}});
					ia.SetColorMatrix(cm);
				}
				e.Graphics.DrawImage(
					this.icon, 
					iconArea,
					0, 0, this.icon.Width, this.icon.Height,
					GraphicsUnit.Pixel,
					ia);
			}

			if (this.resetVisible)
			{
				ImageAttributes ia = new ImageAttributes();
				if (!this.resetEnabled || !this.Enabled)
				{
					ColorMatrix cm = new ColorMatrix(new float[][]{
						new float[] {0.299f, 0.299f, 0.299f,	0,		0},
						new float[] {0.587f, 0.587f, 0.587f,	0,		0},
						new float[] {0.114f, 0.114f, 0.114f,	0,		0},
						new float[] {     0,      0,      0,	0.75f,	0},
						new float[] {     0,      0,      0,	0,		0}});
					ia.SetColorMatrix(cm);
				}
				else if (this.resetHovered)
				{
					Rectangle resetButtonBgArea = new Rectangle(
						resetButtonArea.X - 1,
						resetButtonArea.Y - 1,
						resetButtonArea.Width + 1,
						resetButtonArea.Height + 1);
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, this.ForeColor)), resetButtonBgArea);
					e.Graphics.DrawRectangle(new Pen(this.ForeColor), resetButtonBgArea);
				}
				Image img = this.resetIsInit ? DualityEditor.Properties.Resources.add : DualityEditor.Properties.Resources.cross;
				e.Graphics.DrawImage(
					img, 
					resetButtonArea,
					0, 0, img.Width, img.Height,
					GraphicsUnit.Pixel,
					ia);
			}

			e.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlText, headerTextArea, headerTextFormat);
			if (this.valueText != null) e.Graphics.DrawString(this.valueText, this.Font, SystemBrushes.ControlText, valueTextArea, headerTextFormat);
		}

		protected void OnResetClicked()
		{
			if (this.ResetClicked != null)
				this.ResetClicked(this, null);
		}
	}
}
