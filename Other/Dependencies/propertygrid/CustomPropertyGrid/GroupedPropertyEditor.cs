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
		private	PropertyEditor			hoverEditor		= null;
		private	bool					hoverEditorLock	= false;

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
					this.UpdateSize();
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
					this.UpdateSize();
					this.Invalidate();
				}
			}
		}
		public bool ContentInitialized
		{
			get { return this.contentInit; }
		}
		public override IEnumerable<PropertyEditor> Children
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
			Rectangle indentClientRect = this.ClientRectangle;
			indentClientRect.X += this.indent;
			indentClientRect.Width -= this.indent;
			if (this.expanded && indentClientRect.Contains(new Point(x, y)))
			{
				foreach (PropertyEditor e in this.propertyEditors)
				{
					if (y >= curY && y < curY + e.Height) return e.PickEditorAt(x - this.indent, y - curY);
					curY += e.Height;
				}
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
					result.X += this.indent;
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
			this.OnEditorAdded(editor);
			this.UpdateSize();
		}
		protected void RemovePropertyEditor(PropertyEditor editor)
		{
			editor.ParentEditor = null;
			editor.ValueChanged -= this.OnValueChanged;
			editor.EditingFinished -= this.OnEditingFinished;

			this.OnEditorRemoving(editor);
			this.propertyEditors.Remove(editor);
			this.UpdateSize();
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
			this.UpdateSize();
		}
		protected void UpdateSize()
		{
			int h = this.headerHeight;
			if (this.expanded)
			{
				foreach (PropertyEditor e in this.propertyEditors)
				{
					e.Width = this.ClientRectangle.Width - this.indent;
					h += e.Height;
				}
			}
			this.Height = h;
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			Rectangle clientRect = this.ClientRectangle;
			Rectangle rightButtonRect = this.ButtonRectangle;

			clientRect.Width += rightButtonRect.Width;
			rightButtonRect.Y = this.headerHeight / 2 - rightButtonRect.Height / 2;

			this.ClientRectangle = clientRect;
			this.ButtonRectangle = rightButtonRect;
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

		protected void PaintHeader(Graphics g, Rectangle rect)
		{
			g.FillRectangle(Brushes.Red, rect);
		}
		protected internal override void OnPaint(PaintEventArgs e)
		{
			int curY = 0;
			// Paint background and name label
			this.PaintBackground(e.Graphics);
			this.PaintNameLabel(e.Graphics);

			// Paint header
			this.PaintHeader(e.Graphics, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y + curY, this.ClientRectangle.Width, this.headerHeight));
			curY += this.headerHeight;

			// Paint right button
			this.PaintButton(e.Graphics);
			
			// Paint children
			if (this.expanded)
			{
				foreach (PropertyEditor child in this.propertyEditors)
				{
					GraphicsState oldState = e.Graphics.Save();
					Rectangle editorRect = new Rectangle(this.ClientRectangle.X + this.indent, this.ClientRectangle.Y + curY, child.Width, child.Height);
					editorRect.Intersect(this.ClientRectangle);
					e.Graphics.SetClip(editorRect);
					e.Graphics.TranslateTransform(this.ClientRectangle.X + this.indent, this.ClientRectangle.Y + curY);

					child.OnPaint(e);
					curY += child.Height;

					e.Graphics.Restore(oldState);
				}
			}
		}

		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			PropertyEditor lastHoverEditor = this.hoverEditor;
			
			if (!this.hoverEditorLock)
			{
				this.hoverEditor = this.PickEditorAt(e.X, e.Y);
				if (this.hoverEditor == this) this.hoverEditor = null;

				if (lastHoverEditor != this.hoverEditor && lastHoverEditor != null)
					lastHoverEditor.OnMouseLeave(EventArgs.Empty);
				if (lastHoverEditor != this.hoverEditor && this.hoverEditor != null)
					this.hoverEditor.OnMouseEnter(EventArgs.Empty);
			}

			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseMove(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.Delta));
			}
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
			if (this.hoverEditor != null)
			{
				this.hoverEditorLock = Control.MouseButtons != MouseButtons.None;
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseDown(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.Delta));
			}
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.hoverEditor != null)
			{
				this.hoverEditorLock = Control.MouseButtons != MouseButtons.None;
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseUp(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.Delta));
			}
		}
		protected internal override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseClick(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.Delta));
			}
		}
		protected internal override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseDoubleClick(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.Delta));
			}
		}

		protected internal override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			e.Effect = e.AllowedEffect; // Accept anything to pass it on to children
		}
		protected internal override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
		}
		protected internal override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			PropertyEditor lastHoverEditor = this.hoverEditor;
			
			this.hoverEditor = this.PickEditorAt(e.X, e.Y);
			if (this.hoverEditor == this) this.hoverEditor = null;

			if (lastHoverEditor != this.hoverEditor && lastHoverEditor != null)
				lastHoverEditor.OnDragLeave(EventArgs.Empty);
			if (lastHoverEditor != this.hoverEditor && this.hoverEditor != null)
			{
				e.Effect = DragDropEffects.None;
				this.hoverEditor.OnDragEnter(e);
			}

			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnDragOver(new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.AllowedEffect,
					e.Effect));
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
		protected internal override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnDragDrop(new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - this.ClientRectangle.X - editorLoc.X, 
					e.Y - this.ClientRectangle.Y - editorLoc.Y, 
					e.AllowedEffect,
					e.Effect));
			}
		}

		protected override void OnSizeChanged()
		{
			base.OnSizeChanged();
			this.UpdateSize();
		}
	}
}
