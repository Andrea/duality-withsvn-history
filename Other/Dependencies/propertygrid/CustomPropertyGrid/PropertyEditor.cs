using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace CustomPropertyGrid
{
	public class PropertyEditorEventArgs : EventArgs
	{
		PropertyEditor editor;

		public PropertyEditor Editor
		{
			get { return this.editor; }
		}

		public PropertyEditorEventArgs(PropertyEditor e)
		{
			this.editor = e;
		}
	}

	public class PropertyEditorValueEventArgs : EventArgs
	{
		private	PropertyEditor	editor	= null;
		private	object			value	= null;

		public PropertyEditor Editor
		{
			get { return this.editor; }
		}
		public object Value
		{
			get { return this.value; }
		}

		public PropertyEditorValueEventArgs(PropertyEditor editor, object value)
		{
			this.editor = editor;
			this.value = value;
		}
	}
	
	public abstract class PropertyEditor
	{
		[Flags]
		public enum HintFlags
		{
			None			= 0x0,

			HasPropertyName	= 0x1,
			HasButton	= 0x2,

			All = HasPropertyName | HasButton,
			Default = All | HasPropertyName
		}

		private	static	Font	fontNormal		= SystemFonts.DefaultFont;
		private	static	Font	fontModified	= new Font(SystemFonts.DefaultFont, FontStyle.Bold);

		private	PropertyGrid	parentGrid		= null;
		private	PropertyEditor	parentEditor	= null;
		private	Type			editedType		= null;
		private	MemberInfo		editedMember	= null;
		private	string			propertyName	= "Hello World, I'm a PropertyEditor. Yay!";
		private	bool			forceWriteBack	= false;
		private	bool			updatingFromObj	= false;
		private	HintFlags		hints			= HintFlags.Default;
		private	Size			size			= new Size(0, 20);
		private	Rectangle		clientRect		= Rectangle.Empty;
		private	Rectangle		nameLabelRect	= Rectangle.Empty;
		private	Rectangle		buttonRect		= Rectangle.Empty;
		private	bool			buttonHovered	= false;
		private	bool			buttonPressed	= false;
		private	Image			buttonIcon		= CustomPropertyGrid.Properties.Resources.ImageDelete;
		private	Func<IEnumerable<object>>	getter	= null;
		private	Action<IEnumerable<object>>	setter	= null;


		public event EventHandler	RightButtonPressed	= null;
		public event EventHandler<PropertyEditorValueEventArgs>	EditingFinished = null;
		public event EventHandler<PropertyEditorValueEventArgs>	ValueChanged	= null;


		public PropertyGrid ParentGrid
		{
			get { return this.parentGrid; }
			internal set
			{
				bool lastReadOnly = this.ReadOnly;
				this.parentGrid = value;
				if (this.parentGrid == null) this.parentEditor = null;
				if (this.ReadOnly != lastReadOnly) this.OnReadOnlyChanged();
			}
		}
		public PropertyEditor ParentEditor
		{
			get { return this.parentEditor; }
			internal set
			{
				bool lastReadOnly = this.ReadOnly;
				this.parentEditor = value;
				if (this.parentEditor != null) this.parentGrid = this.parentEditor.ParentGrid;
				if (this.ReadOnly != lastReadOnly) this.OnReadOnlyChanged();
			}
		}
		public PropertyEditor NextEditor
		{
			get
			{
				if (this.parentEditor == null) return null;
				bool foundMe = false;
				foreach (PropertyEditor child in this.parentEditor.Children)
				{
					if (foundMe) return child;
					if (child == this) foundMe = true;
				}
				return null;
			}
		}
		public PropertyEditor PrevEditor
		{
			get
			{
				if (this.parentEditor == null) return null;
				PropertyEditor last = null;
				foreach (PropertyEditor child in this.parentEditor.Children)
				{
					if (child == this) return last;
					last = child;
				}
				return null;
			}
		}
		public virtual IEnumerable<PropertyEditor> Children
		{
			get { return new PropertyEditor[0]; }
		}
		
		public Type EditedType
		{
			get { return this.editedType; }
			internal set 
			{
				if (this.editedType != value)
				{
					this.editedType = value;
					this.OnEditedTypeChanged();
				}
			}
		}
		public MemberInfo EditedMember
		{
			get { return this.editedMember; }
			set 
			{
				if (this.editedMember != value)
				{
					this.editedMember = value;
					this.OnEditedMemberChanged();
				}
			}
		}
		public string PropertyName
		{
			get { return this.propertyName; }
			internal set 
			{
				this.propertyName = value;
				this.Invalidate();
			}
		}
		public bool ForceWriteBack
		{
			get { return this.forceWriteBack; }
			set { this.forceWriteBack = value; }
		}
		public bool IsValueModified
		{
			get { return this.parentEditor == null ? false : this.parentEditor.IsChildValueModified(this); }
		}
		public Func<IEnumerable<object>> Getter
		{
			set { this.getter = value; }
		}
		public Action<IEnumerable<object>> Setter
		{
			set
			{ 
				if (this.setter != value)
				{
					bool lastReadOnly = this.ReadOnly;
					this.setter = value;
					if (this.ReadOnly != lastReadOnly) this.OnReadOnlyChanged();
				}
			}
		}
		public abstract object DisplayedValue { get; }
		public bool ReadOnly
		{
			get 
			{ 
				return this.setter == null || (this.parentEditor != null && this.parentEditor.ReadOnly);
			}
		}
		public bool Enabled
		{
			get 
			{ 
				return this.parentGrid != null && this.parentGrid.Enabled;
			}
		}
		public bool Focused
		{
			get
			{
				if (this.parentGrid == null) return false;
				return this.parentGrid.Focused && this.parentGrid.FocusEditor == this;
			}
		}
		public HintFlags Hints
		{
			get { return this.hints; }
			set
			{
				if (this.hints != value)
				{
					this.hints = value;
					this.UpdateGeometry();
				}
			}
		}
		public Image ButtonIcon
		{
			get { return this.buttonIcon; }
			set
			{
				if (this.buttonIcon != value)
				{
					this.buttonIcon = value;
					this.Invalidate();
				}
			}
		}
		protected bool IsUpdatingFromObject
		{
			get { return this.updatingFromObj; }
		}

		public Size Size
		{
			get { return this.size; }
			set
			{
				if (this.size != value)
				{
					this.size = value;
					this.OnSizeChanged();
				}
			}
		}
		public int Width
		{
			get { return this.size.Width; }
			set { this.Size = new Size(value, this.size.Height); }
		}
		public int Height
		{
			get { return this.size.Height; }
			set { this.Size = new Size(this.size.Width, value); }
		}
		
		public Rectangle ClientRectangle
		{
			get { return this.clientRect; }
			protected set { this.clientRect = value; }
		}
		protected Rectangle NameLabelRectangle
		{
			get { return this.nameLabelRect; }
			set { this.nameLabelRect = value; }
		}
		protected Rectangle ButtonRectangle
		{
			get { return this.buttonRect; }
			set { this.buttonRect = value; }
		}
		

		public virtual void PerformGetValue()
		{
			this.Invalidate();
		}
		public virtual void PerformSetValue()
		{
			this.Invalidate();
		}

		protected IEnumerable<object> GetValue()
		{
			return this.getter();
		}
		protected void SetValue(IEnumerable<object> objEnum)
		{
			this.setter(objEnum);
		}
		protected void SetValue(object obj)
		{
			this.SetValue(new object[] { obj });
		}

		public void Invalidate()
		{
			if (this.parentGrid != null) this.parentGrid.Invalidate();
		}
		public void Focus()
		{
			if (this.parentGrid != null) this.parentGrid.Focus(this);
		}
		public bool IsChildOf(PropertyEditor parent)
		{
			if (this.parentEditor == parent) return true;
			if (this.parentEditor == null) return false;
			return this.parentEditor.IsChildOf(parent);
		}
		public virtual PropertyEditor PickEditorAt(int x, int y)
		{
			return this;
		}
		public virtual Point GetChildLocation(PropertyEditor child)
		{
			return Point.Empty;
		}

		protected virtual void UpdateGeometry()
		{
			if ((this.hints & HintFlags.HasPropertyName) != HintFlags.None)
				this.nameLabelRect = new Rectangle(0, 0, this.size.Width * 2 / 5, this.size.Height);
			else
				this.nameLabelRect = Rectangle.Empty;

			Size rightButtonSize = this.buttonIcon != null ? this.buttonIcon.Size : new Size(10, 10);
			if ((this.hints & HintFlags.HasButton) != HintFlags.None)
				this.buttonRect = new Rectangle(this.size.Width - rightButtonSize.Width - 4, this.size.Height / 2 - rightButtonSize.Height / 2 - 2, rightButtonSize.Width + 4, rightButtonSize.Height + 4);
			else
				this.buttonRect = Rectangle.Empty;

			this.clientRect = new Rectangle(0, 0, this.size.Width, this.size.Height);
			this.clientRect.X += this.nameLabelRect.Width;
			this.clientRect.Width -= this.nameLabelRect.Width;
			this.clientRect.Width -= this.buttonRect.Width;
		}
		protected void BeginUpdate()
		{
			if (this.updatingFromObj) throw new InvalidOperationException("The PropertyEditor already is updating");
			this.updatingFromObj = true;
		}
		protected void EndUpdate()
		{
			if (!this.updatingFromObj) throw new InvalidOperationException("The PropertyEditor was not updating");
			this.updatingFromObj = false;
		}
		
		protected virtual bool IsChildValueModified(PropertyEditor childEditor) { return false; }

		protected void PaintBackground(Graphics g, Rectangle rect)
		{
			g.FillRectangle(this.Focused ? SystemBrushes.ControlLight : SystemBrushes.Control, rect);
		}
		protected void PaintButton(Graphics g, Rectangle rect)
		{
			if ((this.hints & HintFlags.HasButton) == HintFlags.None || this.buttonIcon == null) return;

			Size rightButtonSize = this.buttonIcon.Size;
			Point rightButtonCenter = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

			var imgAttribs = new System.Drawing.Imaging.ImageAttributes();
			System.Drawing.Imaging.ColorMatrix colorMatrix = null;
			if (this.buttonPressed)
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {1.3f, 0.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 1.3f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 1.3f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
			}
			else if (this.buttonHovered)
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
			}
			else
			{
				colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][] {
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.34f, 0.34f, 0.34f, 0.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
					new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}});
			}
			imgAttribs.SetColorMatrix(colorMatrix);
				
			if (this.buttonHovered)
			{
				Rectangle rightButtonBgRect = rect;
				rightButtonBgRect.X += 1;
				rightButtonBgRect.Y += 1;
				rightButtonBgRect.Width -= 3;
				rightButtonBgRect.Height -= 3;
				g.FillRectangle(new SolidBrush(Color.FromArgb(128, SystemColors.Window)), rightButtonBgRect);
				g.DrawRectangle(new Pen(Color.FromArgb(255, SystemColors.Window)), rightButtonBgRect);
			}
				
			g.DrawImage(this.buttonIcon, 
				new Rectangle(
					rightButtonCenter.X - rightButtonSize.Width / 2,
					rightButtonCenter.Y - rightButtonSize.Height / 2,
					rightButtonSize.Width,
					rightButtonSize.Height), 
				0, 0, this.buttonIcon.Width, this.buttonIcon.Height, GraphicsUnit.Pixel, 
				imgAttribs);
		}
		protected void PaintNameLabel(Graphics g, Rectangle rect)
		{
			if ((this.hints & HintFlags.HasPropertyName) == HintFlags.None) return;

			Rectangle nameLabelTextRect = rect;
			nameLabelTextRect.Width -= 5;

			StringFormat nameLabelFormat = StringFormat.GenericDefault;
			nameLabelFormat.Alignment = StringAlignment.Near;
			nameLabelFormat.LineAlignment = StringAlignment.Center;
			nameLabelFormat.Trimming = StringTrimming.Character;
			nameLabelFormat.FormatFlags |= StringFormatFlags.NoWrap;

			Font nameLabelFont = this.IsValueModified ? fontModified : fontNormal;

			int charsFit, lines;
			SizeF nameLabelSize = g.MeasureString(this.propertyName, nameLabelFont, nameLabelTextRect.Size, nameLabelFormat, out charsFit, out lines);
			g.DrawString(this.propertyName, nameLabelFont, SystemBrushes.ControlText, nameLabelTextRect, nameLabelFormat);

			if (charsFit < this.propertyName.Length)
			{
				Pen ellipsisPen = new Pen(SystemColors.ControlText);
				ellipsisPen.DashStyle = DashStyle.Dot;
				g.DrawLine(ellipsisPen, 
					nameLabelTextRect.Right, 
					(nameLabelTextRect.Y + nameLabelTextRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f), 
					rect.Right - 2, 
					(nameLabelTextRect.Y + nameLabelTextRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f));
			}
		}
		internal protected virtual void OnPaint(PaintEventArgs e)
		{
			this.PaintBackground(e.Graphics, new Rectangle(Point.Empty, this.size));
			this.PaintNameLabel(e.Graphics, this.nameLabelRect);
			this.PaintButton(e.Graphics, this.buttonRect);
		}
		
		internal protected virtual void OnMouseEnter(EventArgs e) {}
		internal protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.buttonHovered) this.Invalidate();
			this.buttonHovered = false;
			this.buttonPressed = false;
		}
		internal protected virtual void OnMouseMove(MouseEventArgs e)
		{
			bool lastHovered = this.buttonHovered;
			this.buttonHovered = this.ButtonRectangle.Contains(e.Location);
			if (lastHovered != this.buttonHovered) this.Invalidate();
		}
		internal protected virtual void OnMouseDown(MouseEventArgs e)
		{
			if (this.buttonHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.buttonPressed = true;
				this.Invalidate();
			}
		}
		internal protected virtual void OnMouseUp(MouseEventArgs e)
		{
			if (this.buttonPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.buttonPressed = false;
				this.Invalidate();
			}
		}
		internal protected virtual void OnMouseClick(MouseEventArgs e)
		{
			if (this.buttonHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
				this.OnRemoveButtonPressed();
			if (new Rectangle(0, 0, this.size.Width, this.size.Height).Contains(e.Location))
				this.Focus();
		}
		internal protected virtual void OnMouseDoubleClick(MouseEventArgs e) {}

		internal protected virtual void OnKeyDown(KeyEventArgs e)
		{
			if (!e.Handled && this.parentEditor != null) this.parentEditor.OnKeyDown(e);
		}
		internal protected virtual void OnKeyUp(KeyEventArgs e)
		{
			if (!e.Handled && this.parentEditor != null) this.parentEditor.OnKeyUp(e);
		}
		internal protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			if (!e.Handled && this.parentEditor != null) this.parentEditor.OnKeyPress(e);
		}

		internal protected virtual void OnDragEnter(DragEventArgs e) {}
		internal protected virtual void OnDragLeave(EventArgs e) {}
		internal protected virtual void OnDragOver(DragEventArgs e) {}
		internal protected virtual void OnDragDrop(DragEventArgs e) {}

		internal protected virtual void OnReadOnlyChanged()
		{
			this.Invalidate();
		}
		protected virtual void OnEditedTypeChanged()
		{
			this.Invalidate();
		}
		protected virtual void OnEditedMemberChanged()
		{
			this.Invalidate();
			if (this.editedMember != null)
			{
				bool flaggedForceWriteBack = false;
				this.forceWriteBack = flaggedForceWriteBack;
			}
			else
				this.forceWriteBack = false;
		}
		protected virtual void OnSizeChanged()
		{
			this.UpdateGeometry();
		}
		protected virtual void OnRemoveButtonPressed()
		{
			if (this.RightButtonPressed != null)
				this.RightButtonPressed(this, EventArgs.Empty);
		}
		protected virtual void OnValueChanged(object sender, PropertyEditorValueEventArgs args)
		{
			if (this.ValueChanged != null)
				this.ValueChanged(sender, args);
		}
		protected virtual void OnEditingFinished(object sender, PropertyEditorValueEventArgs args)
		{
			if (this.EditingFinished != null)
				this.EditingFinished(sender, args);
		}

		protected void OnValueChanged()
		{
			this.OnValueChanged(this, new PropertyEditorValueEventArgs(this, this.DisplayedValue));
		}
		protected void OnEditingFinished()
		{
			this.OnEditingFinished(this, new PropertyEditorValueEventArgs(this, this.DisplayedValue));
		}
	}
}
