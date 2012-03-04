using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

using CustomPropertyGrid.Renderer;

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
			None			= 0x00,

			HasPropertyName	= 0x01,
			HasButton		= 0x02,
			HasExpandCheck	= 0x04,
			HasActiveCheck	= 0x08,

			ButtonEnabled	= 0x10,
			ExpandEnabled	= 0x20,
			ActiveEnabled	= 0x40,

			All = HasPropertyName | HasButton | HasExpandCheck | HasActiveCheck | ButtonEnabled | ExpandEnabled | ActiveEnabled,
			Default = HasPropertyName
		}

		protected	static	readonly	Font	FontNormal	= SystemFonts.DefaultFont;
		protected	static	readonly	Font	FontBold	= new Font(SystemFonts.DefaultFont, FontStyle.Bold);

		private	PropertyGrid	parentGrid		= null;
		private	PropertyEditor	parentEditor	= null;
		private	Type			editedType		= null;
		private	MemberInfo		editedMember	= null;
		private	string			propertyName	= CustomPropertyGrid.Properties.Resources.PropertyName_Default;
		private	bool			forceWriteBack	= false;
		private	bool			updatingFromObj	= false;
		private	HintFlags		hints			= HintFlags.Default;
		private	Size			size			= new Size(0, 20);
		private	Rectangle		clientRect		= Rectangle.Empty;
		private	Rectangle		nameLabelRect	= Rectangle.Empty;
		private	Rectangle		buttonRect		= Rectangle.Empty;
		private	bool			buttonHovered	= false;
		private	bool			buttonPressed	= false;
		private	IconImage		buttonIcon		= null;
		private	Func<IEnumerable<object>>	getter	= null;
		private	Action<IEnumerable<object>>	setter	= null;


		public event EventHandler	SizeChanged		= null;
		public event EventHandler	ButtonPressed	= null;
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
		protected virtual bool FocusOnClick
		{
			get { return true; }
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
			get { return this.buttonIcon.SourceImage; }
			set
			{
				if (value == null) value = CustomPropertyGrid.Properties.Resources.ImageDelete;
				if (this.buttonIcon == null || this.buttonIcon.SourceImage != value)
				{
					this.buttonIcon = new IconImage(value);
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
		

		public PropertyEditor()
		{
			this.ButtonIcon = null;
		}

		public virtual void PerformGetValue()
		{
			this.Invalidate();
		}
		public virtual void PerformSetValue()
		{
			if (this.ReadOnly) return;
			this.SetValue(this.DisplayedValue);
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
			if (this.parentGrid != null) 
			{
				Rectangle invalidateRect = new Rectangle(this.parentGrid.GetEditorLocation(this, true), this.size);
				this.parentGrid.Invalidate(invalidateRect);
			}
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

			if ((this.hints & HintFlags.HasButton) != HintFlags.None)
			{
				Size buttonSize = this.buttonIcon != null ? this.buttonIcon.Size : new Size(10, 10);
				this.buttonRect.Height = this.Size.Height;
				this.buttonRect.Width = Math.Min(this.size.Height - 2, buttonSize.Height + 2);
				this.buttonRect.X = this.Size.Width - buttonRect.Width - 1;
				this.buttonRect.Y = 0;
			}
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

		protected void PaintBackground(Graphics g)
		{
			g.FillRectangle(new SolidBrush(this.Focused ? SystemColors.Control.ScaleBrightness(0.9f) : SystemColors.Control), new Rectangle(Point.Empty, this.size));
		}
		protected void PaintButton(Graphics g)
		{
			if ((this.hints & HintFlags.HasButton) == HintFlags.None || this.buttonIcon == null) return;

			Size buttonSize = new Size(
				Math.Min(this.buttonIcon.Width, this.buttonRect.Width),
				Math.Min(this.buttonIcon.Height, this.buttonRect.Height));
			Point buttonCenter = new Point(this.buttonRect.X + this.buttonRect.Width / 2, this.buttonRect.Y + this.buttonRect.Height / 2);

			Image buttonImage;
			if ((this.Hints & HintFlags.ButtonEnabled) == HintFlags.None || this.ReadOnly || !this.Enabled)
				buttonImage = this.buttonIcon.Disabled;
			else if (this.buttonPressed)
				buttonImage = this.buttonIcon.Active;
			else if (this.buttonHovered)
				buttonImage = this.buttonIcon.Normal;
			else
				buttonImage = this.buttonIcon.Passive;
				
			if (this.buttonHovered)
			{
				Rectangle buttonBgRect = this.buttonRect;
				buttonBgRect.Height = Math.Min(buttonBgRect.Height, buttonBgRect.Width) - 1;
				buttonBgRect.Width = buttonBgRect.Height;
				buttonBgRect.X = this.buttonRect.X + this.buttonRect.Width / 2 - buttonBgRect.Width / 2 - 1;
				buttonBgRect.Y = this.buttonRect.Y + this.buttonRect.Height / 2 - buttonBgRect.Height / 2 - 1;
				g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.White)), buttonBgRect);
				g.DrawRectangle(new Pen(Color.FromArgb(255, Color.White)), buttonBgRect);
			}
				
			g.DrawImage(buttonImage, buttonCenter.X - buttonSize.Width / 2, buttonCenter.Y - buttonSize.Height / 2, buttonSize.Width, buttonSize.Height);
		}
		protected void PaintNameLabel(Graphics g)
		{
			if ((this.hints & HintFlags.HasPropertyName) == HintFlags.None) return;
			ControlRenderer.DrawStringLine(g, this.propertyName, this.IsValueModified ? FontBold : FontNormal, this.nameLabelRect, this.Enabled ? SystemColors.ControlText : SystemColors.GrayText);
		}
		internal protected virtual void OnPaint(PaintEventArgs e)
		{
			this.PaintBackground(e.Graphics);
			this.PaintNameLabel(e.Graphics);
			this.PaintButton(e.Graphics);
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
			this.buttonHovered = (this.Hints & HintFlags.ButtonEnabled) != HintFlags.None && !this.ReadOnly && this.ButtonRectangle.Contains(e.Location);
			if (lastHovered != this.buttonHovered) this.Invalidate();
		}
		internal protected virtual void OnMouseDown(MouseEventArgs e)
		{
			if (this.FocusOnClick && new Rectangle(0, 0, this.size.Width, this.size.Height).Contains(e.Location))
				this.Focus();
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
				this.OnButtonPressed();
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

		internal protected virtual void OnDragEnter(DragEventArgs e) { Console.WriteLine("DragEnter: {0}", this.PropertyName); }
		internal protected virtual void OnDragLeave(EventArgs e) { Console.WriteLine("DragLeabe: {0}", this.PropertyName);}
		internal protected virtual void OnDragOver(DragEventArgs e) {}
		internal protected virtual void OnDragDrop(DragEventArgs e) {}

		internal protected virtual void OnGotFocus(EventArgs e)
		{
			this.Invalidate();
		}
		internal protected virtual void OnLostFocus(EventArgs e)
		{
			this.Invalidate();
			this.OnEditingFinished();
		}

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
			if (this.SizeChanged != null)
				this.SizeChanged(this, EventArgs.Empty);
		}
		protected virtual void OnButtonPressed()
		{
			if (this.ButtonPressed != null)
				this.ButtonPressed(this, EventArgs.Empty);
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
