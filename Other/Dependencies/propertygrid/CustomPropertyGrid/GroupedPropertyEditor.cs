using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CustomPropertyGrid
{
	public abstract class GroupedPropertyEditor : PropertyEditor
	{
		public const int	DefaultIndent		= 20;
		public const int	DefaultHeaderHeight	= 20;
		public const int	BigHeaderHeight		= 30;

		private	int						headerHeight	= DefaultHeaderHeight;
		private	int						indent			= DefaultIndent;
		private	bool					expanded		= false;
		private	bool					contentInit		= false;
		private	List<PropertyEditor>	propertyEditors	= new List<PropertyEditor>();

		public event EventHandler<PropertyEditorEventArgs>	EditorAdded;
		public event EventHandler<PropertyEditorEventArgs>	EditorRemoving;
		

		public bool Expanded
		{
			get { return this.expanded; }
			set 
			{ 
				if (this.expanded != value)
				{
					this.expanded = value;
					this.Invalidate();
					if (this.expanded && !this.contentInit) this.InitContent();
				}
			}
		}
		public int Indent
		{
			get { return this.indent; }
			set 
			{
				if (this.indent != value)
				{
					this.indent = value;
					this.Invalidate();
				}
			}
		}
		public IEnumerable<PropertyEditor> PropertyEditors
		{
			get { return this.propertyEditors; }
		}


		public GroupedPropertyEditor()
		{
			this.Hints &= (~HintFlags.HasPropertyName);
			this.ClearContent();
		}

		public virtual void InitContent()
		{
			this.contentInit = true;
		}
		public virtual void ClearContent()
		{
			this.contentInit = false;
			this.ClearPropertyEditors();
		}

		public override PropertyEditor PickEditorAt(int x, int y)
		{
			// Pick child editor, if applying
			int curY = this.headerHeight;
			foreach (PropertyEditor e in this.propertyEditors)
			{
				if (y >= curY && y < curY + e.Height) return e.PickEditorAt(x, y - curY);
				curY += e.Height;
			}

			return base.PickEditorAt(x, y);
		}
		public override Point GetChildLocation(PropertyEditor child)
		{
			// Pick child editor, if applying
			int curY = this.headerHeight;
			foreach (PropertyEditor e in this.propertyEditors)
			{
				if (child == e || child.IsChildOf(e))
				{
					Point result = e.GetChildLocation(child);
					result.Y += curY;
					return result;
				}
				curY += e.Height;
			}

			return base.GetChildLocation(child);
		}
		
		protected void AddPropertyEditor(PropertyEditor editor)
		{
			editor.ParentEditor = this;
			editor.ValueChanged += this.OnValueChanged;
			editor.EditingFinished += this.OnEditingFinished;

			this.propertyEditors.Add(editor);
			this.Height += editor.Height;
			this.OnEditorAdded(editor);
		}
		protected void RemovePropertyEditor(PropertyEditor editor)
		{
			editor.ParentEditor = null;
			editor.ValueChanged -= this.OnValueChanged;
			editor.EditingFinished -= this.OnEditingFinished;

			this.OnEditorRemoving(editor);
			this.Height -= editor.Height;
			this.propertyEditors.Remove(editor);
		}
		protected void ClearPropertyEditors()
		{
			foreach (PropertyEditor e in this.propertyEditors)
			{
				e.ParentEditor = null;
				e.ValueChanged -= this.OnValueChanged;
				e.EditingFinished -= this.OnEditingFinished;
				this.OnEditorRemoving(e);
			}
			this.propertyEditors.Clear();
			this.Height = this.headerHeight;
		}
		
		protected void OnEditorAdded(PropertyEditor e)
		{
			if (this.EditorAdded != null)
				this.EditorAdded(this, new PropertyEditorEventArgs(e));
		}
		protected void OnEditorRemoving(PropertyEditor e)
		{
			if (this.EditorRemoving != null)
				this.EditorRemoving(this, new PropertyEditorEventArgs(e));
		}

		protected override bool IsChildValueModified(PropertyEditor childEditor)
		{
			return base.IsChildValueModified(childEditor);
			// Do not propagate the modified state to child editors. It's really nasty.
			//return this.modifiedStateCache;
		}
		internal protected override void OnReadOnlyChanged()
		{
			base.OnReadOnlyChanged();

			foreach (PropertyEditor e in this.propertyEditors)
				e.OnReadOnlyChanged();
		}

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int curY = 0;
			e.Graphics.FillRectangle(Brushes.Red, this.ClientRectangle.X, this.ClientRectangle.Y + curY, this.ClientRectangle.Width, this.headerHeight);
			curY += this.headerHeight;
			
			// Paint children
			foreach (PropertyEditor child in this.propertyEditors)
			{
				GraphicsState oldState = e.Graphics.Save();
				Rectangle editorRect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y + curY, child.Width, child.Height);
				editorRect.Intersect(this.ClientRectangle);
				e.Graphics.SetClip(editorRect);
				e.Graphics.TranslateTransform(this.ClientRectangle.X, this.ClientRectangle.Y + curY);

				child.OnPaint(e);
				curY += child.Height;

				e.Graphics.Restore(oldState);
			}
		}

		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}
		protected internal override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}
		protected internal override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
		}
		protected internal override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
		}

		protected internal override void OnDragEnter(DragEventArgs drgevent)
		{
			base.OnDragEnter(drgevent);
		}
		protected internal override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
		}
		protected internal override void OnDragOver(DragEventArgs drgevent)
		{
			base.OnDragOver(drgevent);
		}
		protected internal override void OnDragDrop(DragEventArgs drgevent)
		{
			base.OnDragDrop(drgevent);
		}

		protected override void OnSizeChanged()
		{
			base.OnSizeChanged();
			foreach (PropertyEditor e in this.propertyEditors)
				e.Width = this.ClientRectangle.Width;
		}
	}
}
