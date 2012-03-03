using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using CustomPropertyGrid.Renderer;

namespace CustomPropertyGrid
{
	public abstract class GroupedPropertyEditor : PropertyEditor
	{
		public const int	DefaultIndent		= 16;
		public const int	DefaultHeaderHeight	= 18;
		public const int	BigHeaderHeight		= 30;

		private	int						headerHeight	= DefaultHeaderHeight;
		private	int						indent			= DefaultIndent;
		private	bool					expanded		= false;
		private	bool					active			= false;
		private	bool					contentInit		= false;
		private	List<PropertyEditor>	propertyEditors	= new List<PropertyEditor>();
		private	PropertyEditor			hoverEditor		= null;
		private	bool					hoverEditorLock	= false;
		private	IconImage				headerIcon		= null;
		private	Color					headerColor		= SystemColors.Control;
		private	GroupHeaderStyle		headerStyle		= GroupHeaderStyle.Flat;
		private	Rectangle				headerRect			= Rectangle.Empty;
		private	Rectangle				expandCheckRect		= Rectangle.Empty;
		private	bool					expandCheckHovered	= false;
		private	bool					expandCheckPressed	= false;
		private	Rectangle				activeCheckRect		= Rectangle.Empty;
		private	bool					activeCheckHovered	= false;
		private	bool					activeCheckPressed	= false;

		public event EventHandler<PropertyEditorEventArgs>	EditorAdded;
		public event EventHandler<PropertyEditorEventArgs>	EditorRemoving;
		public event EventHandler							ActiveChanged;
		

		public bool Expanded
		{
			get { return this.expanded; }
			set 
			{ 
				if (this.expanded != value)
				{
					this.expanded = value;
					this.Invalidate();
					if (this.expanded && !this.contentInit)
						this.InitContent();
					else
						this.UpdateHeight();
				}
			}
		}
		public bool Active
		{
			get { return this.active; }
			set 
			{ 
				if (this.active != value)
				{
					this.active = value;
					this.Invalidate();
					this.OnActiveChanged();
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
					this.UpdateChildWidth();
					this.Invalidate();
				}
			}
		}
		public int HeaderHeight
		{
			get { return this.headerHeight; }
			set
			{
				if (this.headerHeight != value)
				{
					this.headerHeight = value;
					this.UpdateHeight();
				}
			}
		}
		public Image HeaderIcon
		{
			get { return this.headerIcon.SourceImage; }
			set
			{
				if (this.headerIcon == null || this.headerIcon.SourceImage != value)
				{
					this.headerIcon = value != null ? new IconImage(value) : null;
					this.Invalidate();
				}
			}
		}
		public Color HeaderColor
		{
			get { return this.headerColor; }
			set
			{
				this.headerColor = value;
				this.Invalidate();
			}
		}
		public GroupHeaderStyle HeaderStyle
		{
			get { return this.headerStyle; }
			set
			{
				this.headerStyle = value;
				this.Invalidate();
			}
		}
		public bool ContentInitialized
		{
			get { return this.contentInit; }
		}
		public override IEnumerable<PropertyEditor> Children
		{
			get { return this.expanded ? this.propertyEditors : base.Children; }
		}
		protected override bool FocusOnClick
		{
			get { return false; }
		}


		public GroupedPropertyEditor()
		{
			this.Hints &= (~HintFlags.HasPropertyName);
			this.Hints |= HintFlags.HasExpandCheck | HintFlags.ExpandEnabled;

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

		public override void PerformSetValue() {}

		public PropertyEditor PickEditorAt(int x, int y, bool ownChildrenOnly)
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
					if (y >= curY && y < curY + e.Height) return ownChildrenOnly ? e : e.PickEditorAt(x - indentClientRect.X, y - curY - indentClientRect.Y);
					curY += e.Height;
				}
			}

			return base.PickEditorAt(x, y);
		}
		public override PropertyEditor PickEditorAt(int x, int y)
		{
			return this.PickEditorAt(x, y, false);
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
					result.X += this.ClientRectangle.X + this.indent;
					result.Y += this.ClientRectangle.Y + curY;
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
			this.UpdateChildWidth(editor);

			this.propertyEditors.Add(editor);
			this.OnEditorAdded(editor);
			this.UpdateHeight();
		}
		protected void RemovePropertyEditor(PropertyEditor editor)
		{
			editor.ParentEditor = null;
			editor.ValueChanged -= this.OnValueChanged;
			editor.EditingFinished -= this.OnEditingFinished;

			this.OnEditorRemoving(editor);
			this.propertyEditors.Remove(editor);
			this.UpdateHeight();
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
			this.UpdateHeight();
		}
		protected void UpdateHeight()
		{
			int h = this.headerHeight;
			if (this.expanded)
			{
				foreach (PropertyEditor e in this.propertyEditors)
					h += e.Height;
			}
			this.Height = h;
		}
		protected void UpdateChildWidth(PropertyEditor child = null)
		{
			if (child == null)
			{
				foreach (PropertyEditor e in this.propertyEditors)
					e.Width = this.ClientRectangle.Width - this.indent;
			}
			else
			{
				child.Width = this.ClientRectangle.Width - this.indent;
			}
		}
		protected override void UpdateGeometry()
		{
			base.UpdateGeometry();

			Rectangle clientRect = this.ClientRectangle;
			Rectangle buttonRect = this.ButtonRectangle;

			clientRect.Width += buttonRect.Width;
			buttonRect.Height = this.headerHeight;
			buttonRect.Width = Math.Min(buttonRect.Height, this.headerHeight - 2);
			buttonRect.X = this.Size.Width - buttonRect.Width - 1;
			buttonRect.Y = this.headerHeight / 2 - buttonRect.Height / 2;

			this.ClientRectangle = clientRect;
			this.ButtonRectangle = buttonRect;

			this.headerRect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.headerHeight);
			if ((this.Hints & HintFlags.HasExpandCheck) != HintFlags.None)
			{
				this.expandCheckRect = new Rectangle(
					this.headerRect.X + 2,
					this.headerRect.Y + this.headerRect.Height / 2 - ControlRenderer.CheckBoxSize.Height / 2 - 1,
					ControlRenderer.CheckBoxSize.Width,
					ControlRenderer.CheckBoxSize.Height);
			}
			else
			{
				this.expandCheckRect = new Rectangle(this.headerRect.X, this.headerRect.Y, 0, 0);
			}

			if ((this.Hints & HintFlags.HasActiveCheck) != HintFlags.None)
			{
				this.activeCheckRect = new Rectangle(
					this.expandCheckRect.X + 2,
					this.headerRect.Y + this.headerRect.Height / 2 - ControlRenderer.CheckBoxSize.Height / 2 - 1,
					ControlRenderer.CheckBoxSize.Width,
					ControlRenderer.CheckBoxSize.Height);
			}
			else
			{
				this.activeCheckRect = new Rectangle(this.expandCheckRect.Right, this.expandCheckRect.Y, 0, 0);
			}
		}
		
		protected void OnEditorAdded(PropertyEditor e)
		{
			e.SizeChanged += this.child_SizeChanged;
			if (this.EditorAdded != null)
				this.EditorAdded(this, new PropertyEditorEventArgs(e));
		}
		protected void OnEditorRemoving(PropertyEditor e)
		{
			if (this.EditorRemoving != null)
				this.EditorRemoving(this, new PropertyEditorEventArgs(e));
			e.SizeChanged -= this.child_SizeChanged;
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

		protected void PaintHeader(Graphics g)
		{
			if (this.headerHeight == 0) return;
			Rectangle buttonRect = this.ButtonRectangle;

			CheckBoxState activeState = CheckBoxState.UncheckedDisabled;
			CheckBoxState expandState = CheckBoxState.UncheckedDisabled;
			if ((this.Hints & HintFlags.HasExpandCheck) != HintFlags.None)
			{
				if (this.Enabled && (this.Hints & HintFlags.ExpandEnabled) != HintFlags.None)
				{
					if (!this.Expanded)
					{
						if (this.expandCheckPressed)		expandState = CheckBoxState.PlusPressed;
						else if (this.expandCheckHovered)	expandState = CheckBoxState.PlusHot;
						else								expandState = CheckBoxState.PlusNormal;
					}
					else
					{
						if (this.expandCheckPressed)		expandState = CheckBoxState.MinusPressed;
						else if (this.expandCheckHovered)	expandState = CheckBoxState.MinusHot;
						else								expandState = CheckBoxState.MinusNormal;
					}
				}
				else
				{
					if (this.Expanded)	expandState = CheckBoxState.PlusDisabled;
					else				expandState = CheckBoxState.MinusDisabled;
				}
			}
			if ((this.Hints & HintFlags.HasActiveCheck) != HintFlags.None)
			{
				if (this.Enabled && (this.Hints & HintFlags.ActiveEnabled) != HintFlags.None)
				{
					if (this.Active)
					{
						if (this.activeCheckPressed)		activeState = CheckBoxState.CheckedPressed;
						else if (this.activeCheckHovered)	activeState = CheckBoxState.CheckedHot;
						else								activeState = CheckBoxState.CheckedNormal;
					}
					else
					{
						if (this.activeCheckPressed)		activeState = CheckBoxState.UncheckedPressed;
						else if (this.activeCheckHovered)	activeState = CheckBoxState.UncheckedHot;
						else								activeState = CheckBoxState.UncheckedNormal;
					}
				}
				else
				{
					if (this.Active)	activeState = CheckBoxState.CheckedDisabled;
					else				activeState = CheckBoxState.UncheckedDisabled;
				}
			}

			Rectangle iconRect;
			if (this.headerIcon != null)
			{
				iconRect = new Rectangle(
					this.activeCheckRect.Right + 2,
					this.headerRect.Y + this.headerRect.Height / 2 - this.headerIcon.Height / 2, 
					this.headerIcon.Width,
					this.headerIcon.Height);
			}
			else
			{
				iconRect = new Rectangle(this.activeCheckRect.Right, this.headerRect.Y, 0, 0);
			}
			Rectangle textRect = new Rectangle(iconRect.Right, this.headerRect.Y, this.headerRect.Width - buttonRect.Width - iconRect.Width, this.headerRect.Height);


			ControlRenderer.DrawGroupHeaderBackground(g, this.headerRect, this.Focused ? this.headerColor.ScaleBrightness(0.85f) : this.headerColor, this.headerStyle);
			
			if ((this.Hints & HintFlags.HasExpandCheck) != HintFlags.None)
				ControlRenderer.DrawCheckBox(g, this.expandCheckRect.Location, expandState);
			if ((this.Hints & HintFlags.HasActiveCheck) != HintFlags.None)
				ControlRenderer.DrawCheckBox(g, this.activeCheckRect.Location, activeState);

			if (this.headerIcon != null)
				g.DrawImage(this.Enabled ? this.headerIcon.Normal : this.headerIcon.Disabled, iconRect);

			ControlRenderer.DrawStringLine(g, this.PropertyName, this.IsValueModified ? FontBold : FontNormal, textRect, this.Enabled ? SystemColors.ControlText : SystemColors.GrayText);
		}
		protected internal override void OnPaint(PaintEventArgs e)
		{
			int curY = 0;
			// Paint background and name label
			this.PaintBackground(e.Graphics);
			this.PaintNameLabel(e.Graphics);

			// Paint header
			this.PaintHeader(e.Graphics);
			curY += this.headerHeight;

			// Paint right button
			this.PaintButton(e.Graphics);
			
			// Paint children
			if (this.expanded)
			{
				Rectangle clipRectBase = new Rectangle(
					(int)e.Graphics.ClipBounds.X,
					(int)e.Graphics.ClipBounds.Y,
					(int)e.Graphics.ClipBounds.Width,
					(int)e.Graphics.ClipBounds.Height);
				foreach (PropertyEditor child in this.propertyEditors)
				{
					if (clipRectBase.IntersectsWith(new Rectangle(
						this.ClientRectangle.X + this.indent, 
						this.ClientRectangle.Y + curY,
						child.Width, 
						child.Height)))
					{
						GraphicsState oldState = e.Graphics.Save();
						Rectangle editorRect = new Rectangle(this.ClientRectangle.X + this.indent, this.ClientRectangle.Y + curY, child.Width, child.Height);
						editorRect.Intersect(this.ClientRectangle);
						Rectangle clipRect = editorRect;
						clipRect.Intersect(clipRectBase);
						e.Graphics.SetClip(clipRect);
						e.Graphics.TranslateTransform(this.ClientRectangle.X + this.indent, this.ClientRectangle.Y + curY);

						child.OnPaint(e);
						e.Graphics.Restore(oldState);
					}

					curY += child.Height;
				}
			}
		}

		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			PropertyEditor lastHoverEditor = this.hoverEditor;
			
			if (!this.hoverEditorLock)
			{
				this.hoverEditor = this.PickEditorAt(e.X, e.Y, true);
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
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.Delta));
			}
			else
			{
				bool lastExpandHovered = this.expandCheckHovered;
				bool lastActiveHovered = this.activeCheckHovered;
				this.expandCheckHovered = (this.Hints & HintFlags.ExpandEnabled) != HintFlags.None && this.expandCheckRect.Contains(e.Location);
				this.activeCheckHovered = !this.ReadOnly && (this.Hints & HintFlags.ActiveEnabled) != HintFlags.None && this.activeCheckRect.Contains(e.Location);
				if (lastExpandHovered != this.expandCheckHovered) this.Invalidate();
				if (lastActiveHovered != this.activeCheckHovered) this.Invalidate();
			}
		}
		protected internal override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}
		protected internal override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.hoverEditor != null)
			{
				PropertyEditor lastHoverEditor = this.hoverEditor;
				this.hoverEditor = null;

				if (lastHoverEditor != this.hoverEditor && lastHoverEditor != null)
					lastHoverEditor.OnMouseLeave(EventArgs.Empty);
			}

			if (this.expandCheckHovered) this.Invalidate();
			if (this.activeCheckHovered) this.Invalidate();
			this.expandCheckHovered = false;
			this.expandCheckPressed = false;
			this.activeCheckHovered = false;
			this.activeCheckPressed = false;
		}
		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.hoverEditorLock = Control.MouseButtons != MouseButtons.None;
			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseDown(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.Delta));
			}
			else
			{
				if (new Rectangle(0, 0, this.Width, this.Height).Contains(e.Location))
					this.Focus();
				if (this.expandCheckHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
				{
					this.expandCheckPressed = true;
					this.Invalidate();
					this.OnExpandCheckPressed();
				}
				else if (this.activeCheckHovered && (e.Button & MouseButtons.Left) != MouseButtons.None)
				{
					this.activeCheckPressed = true;
					this.Invalidate();
					this.OnActiveCheckPressed();
				}
			}
		}
		protected internal override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.hoverEditorLock = Control.MouseButtons != MouseButtons.None;
			if (this.hoverEditor != null)
			{
				Point editorLoc = this.GetChildLocation(this.hoverEditor);
				this.hoverEditor.OnMouseUp(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.Delta));
			}
			else
			{
				if (this.expandCheckPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
				{
					this.expandCheckPressed = false;
					this.Invalidate();
				}
				else if (this.activeCheckPressed && (e.Button & MouseButtons.Left) != MouseButtons.None)
				{
					this.activeCheckPressed = false;
					this.Invalidate();
				}
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
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
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
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.Delta));
			}
		}

		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.Focused && e.KeyCode == Keys.Return)
			{
				if ((this.Hints & HintFlags.ExpandEnabled) != HintFlags.None &&
					(this.Hints & HintFlags.HasExpandCheck) != HintFlags.None)
					this.OnExpandCheckPressed();
				e.Handled = true;
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
			if (this.hoverEditor != null)
			{
				this.hoverEditor.OnDragLeave(e);
				this.hoverEditor = null;
			}
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
				DragEventArgs childEvent = new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.AllowedEffect,
					e.Effect);
				this.hoverEditor.OnDragOver(childEvent);
				e.Effect = childEvent.Effect;
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
				DragEventArgs childEvent = new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - editorLoc.X, 
					e.Y - editorLoc.Y, 
					e.AllowedEffect,
					e.Effect);
				this.hoverEditor.OnDragDrop(childEvent);
				e.Effect = childEvent.Effect;
			}
		}

		protected internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.hoverEditorLock = false;
		}

		protected override void OnSizeChanged()
		{
			base.OnSizeChanged();
			this.UpdateChildWidth();
		}
		protected void OnExpandCheckPressed()
		{
			this.Expanded = !this.Expanded;
		}
		protected void OnActiveCheckPressed()
		{
			if (this.ReadOnly) return;
			this.Active = !this.Active;
		}
		protected void OnActiveChanged()
		{
			if (this.ActiveChanged != null)
				this.ActiveChanged(this, EventArgs.Empty);
		}

		private void child_SizeChanged(object sender, EventArgs e)
		{
			this.UpdateHeight();
		}
	}
}
