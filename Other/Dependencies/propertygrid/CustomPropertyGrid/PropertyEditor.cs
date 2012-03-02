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
			HasRemoveButton	= 0x2,

			All = HasPropertyName | HasRemoveButton,
			Default = HasPropertyName
		}

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
		private	bool			removeButtonHovered	= false;
		private	bool			removeButtonPressed	= false;
		private	Func<IEnumerable<object>>	getter	= null;
		private	Action<IEnumerable<object>>	setter	= null;


		public event EventHandler	RemoveButtonPressed	= null;
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
					this.UpdateClientRectangle();
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
		}
		public Rectangle NameLabelRectangle
		{
			get
			{
				if ((this.hints & HintFlags.HasPropertyName) != HintFlags.None)
					return new Rectangle(0, 0, this.size.Width * 2 / 5, this.size.Height);
				else
					return Rectangle.Empty;
			}
		}
		public Rectangle RemoveButtonRectangle
		{
			get
			{
				if ((this.hints & HintFlags.HasRemoveButton) != HintFlags.None)
					return new Rectangle(this.size.Width - 15, 0, 15, this.size.Height);
				else
					return Rectangle.Empty;
			}
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

		protected void UpdateClientRectangle()
		{
			this.clientRect = new Rectangle(0, 0, this.size.Width, this.size.Height);
			if ((this.hints & HintFlags.HasPropertyName) != HintFlags.None)
			{
				int nameLabelWidth = this.NameLabelRectangle.Width;
				this.clientRect.X += nameLabelWidth;
				this.clientRect.Width -= nameLabelWidth;
			}
			if ((this.hints & HintFlags.HasRemoveButton) != HintFlags.None)
			{
				int removeButtonWidth = this.RemoveButtonRectangle.Width;
				this.clientRect.Width -= removeButtonWidth;
			}
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

		internal protected virtual void OnPaint(PaintEventArgs e)
		{
			// Draw the background
			e.Graphics.FillRectangle(this.Focused ? SystemBrushes.ControlLight : SystemBrushes.Control, 0, 0, this.size.Width, this.size.Height);

			// Draw the name label if requested
			if ((this.hints & HintFlags.HasPropertyName) != HintFlags.None)
			{
				Rectangle nameLabelRect = this.NameLabelRectangle;
				Rectangle nameLabelTextRect = nameLabelRect; nameLabelTextRect.Width -= 5;
				StringFormat nameLabelFormat = StringFormat.GenericDefault;
				nameLabelFormat.Alignment = StringAlignment.Near;
				nameLabelFormat.LineAlignment = StringAlignment.Center;
				nameLabelFormat.Trimming = StringTrimming.Character;
				nameLabelFormat.FormatFlags |= StringFormatFlags.NoWrap;

				Font nameLabelFont = this.IsValueModified ? new Font(SystemFonts.DefaultFont, FontStyle.Bold) : SystemFonts.DefaultFont;

				int charsFit, lines;
				SizeF nameLabelSize = e.Graphics.MeasureString(this.propertyName, nameLabelFont, nameLabelTextRect.Size, nameLabelFormat, out charsFit, out lines);
				e.Graphics.DrawString(this.propertyName, nameLabelFont, SystemBrushes.ControlText, nameLabelTextRect, nameLabelFormat);

				if (charsFit < this.propertyName.Length)
				{
					Pen ellipsisPen = new Pen(SystemColors.ControlText);
					ellipsisPen.DashStyle = DashStyle.Dot;
					e.Graphics.DrawLine(ellipsisPen, 
						nameLabelTextRect.Right, 
						(nameLabelTextRect.Y + nameLabelTextRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f), 
						nameLabelRect.Right - 2, 
						(nameLabelTextRect.Y + nameLabelTextRect.Height * 0.5f) + (nameLabelSize.Height * 0.3f));
				}
			}

			// Draw the remove button if requested
			if ((this.hints & HintFlags.HasRemoveButton) != HintFlags.None)
			{
				Rectangle removeButtonRect = this.RemoveButtonRectangle;
				int removeButtonSize = 4;
				Point removeButtonCenter = new Point(removeButtonRect.X + removeButtonRect.Width / 2, removeButtonRect.Y + removeButtonRect.Height / 2);
				Pen removeButtonPen = null;
				
				if (this.removeButtonPressed)
				{
					e.Graphics.FillRectangle(Brushes.LightGray, removeButtonRect);
					removeButtonPen = new Pen(Color.Black, 2.0f);
				}
				else if (this.removeButtonHovered)
				{
					e.Graphics.FillRectangle(Brushes.White, removeButtonRect);
					removeButtonPen = new Pen(Color.FromArgb(64, 64, 64), 2.0f);
				}
				else
				{
					removeButtonPen = new Pen(Color.Gray, 2.0f);
				}

				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.DrawLine(removeButtonPen, 
					removeButtonCenter.X - removeButtonSize, 
					removeButtonCenter.Y - removeButtonSize,
					removeButtonCenter.X + removeButtonSize, 
					removeButtonCenter.Y + removeButtonSize);
				e.Graphics.DrawLine(removeButtonPen, 
					removeButtonCenter.X + removeButtonSize, 
					removeButtonCenter.Y - removeButtonSize,
					removeButtonCenter.X - removeButtonSize, 
					removeButtonCenter.Y + removeButtonSize);
				e.Graphics.SmoothingMode = SmoothingMode.Default;
			}
		}
		
		internal protected virtual void OnMouseEnter(EventArgs e) {}
		internal protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.removeButtonHovered) this.Invalidate();
			this.removeButtonHovered = false;
			this.removeButtonPressed = false;
		}
		internal protected virtual void OnMouseMove(MouseEventArgs e)
		{
			bool lastHovered = this.removeButtonHovered;
			this.removeButtonHovered = this.RemoveButtonRectangle.Contains(e.Location);
			if (lastHovered != this.removeButtonHovered) this.Invalidate();
		}
		internal protected virtual void OnMouseDown(MouseEventArgs e)
		{
			if (this.removeButtonHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.removeButtonPressed = true;
				this.Invalidate();
			}
		}
		internal protected virtual void OnMouseUp(MouseEventArgs e)
		{
			if (this.removeButtonPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.removeButtonPressed = false;
				this.Invalidate();
			}
		}
		internal protected virtual void OnMouseClick(MouseEventArgs e)
		{
			if (this.removeButtonHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
				this.OnRemoveButtonPressed();
			else if (new Rectangle(0, 0, this.size.Width, this.size.Height).Contains(e.Location))
				this.Focus();
		}
		internal protected virtual void OnMouseDoubleClick(MouseEventArgs e) {}

		internal protected virtual void OnKeyDown(KeyEventArgs e) {}
		internal protected virtual void OnKeyUp(KeyEventArgs e) {}
		internal protected virtual void OnKeyPress(KeyPressEventArgs e) {}

		internal protected virtual void OnDragEnter(DragEventArgs drgevent) {}
		internal protected virtual void OnDragLeave(EventArgs e) {}
		internal protected virtual void OnDragOver(DragEventArgs drgevent) {}
		internal protected virtual void OnDragDrop(DragEventArgs drgevent) {}

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
			this.UpdateClientRectangle();
		}
		protected virtual void OnRemoveButtonPressed()
		{
			if (this.RemoveButtonPressed != null)
				this.RemoveButtonPressed(this, EventArgs.Empty);
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
