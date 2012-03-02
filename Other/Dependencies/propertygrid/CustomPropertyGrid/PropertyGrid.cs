using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;

using CustomPropertyGrid.PropertyEditors;

namespace CustomPropertyGrid
{
	public interface IPropertyEditorProvider
	{
		int IsResponsibleFor(Type baseType);
		PropertyEditor CreateEditor(Type baseType);
	}

	public partial class PropertyGrid : UserControl
	{
		public const int EditorPriority_None		= 0;
		public const int EditorPriority_General		= 20;
		public const int EditorPriority_Specialized	= 50;
		public const int EditorPriority_Override	= 100;

		private class MainEditorProvider : IPropertyEditorProvider
		{
			private	List<IPropertyEditorProvider>	subProviders	= new List<IPropertyEditorProvider>();
			public List<IPropertyEditorProvider> SubProviders
			{
				get { return this.subProviders; }
				set { this.subProviders = value; }
			}
			
			public int IsResponsibleFor(Type baseType)
			{
				return EditorPriority_General;
			}
			public PropertyEditor CreateEditor(Type baseType)
			{
				PropertyEditor e = null;

				//// Basic numeric data types
				//if (baseType == typeof(sbyte) || baseType == typeof(byte) ||
				//    baseType == typeof(short) || baseType == typeof(ushort) ||
				//    baseType == typeof(int) || baseType == typeof(uint) ||
				//    baseType == typeof(long) || baseType == typeof(ulong) ||
				//    baseType == typeof(float) || baseType == typeof(double) || baseType == typeof(decimal))
				//    e = new NumericPropertyEditor(parentEditor, parentGrid);
				// Basic data type: Boolean
				/* else */ if (baseType == typeof(bool))
				    e = new BoolPropertyEditor();
				//// Basic data type: Flagged Enum
				//else if (baseType.IsEnum && baseType.GetCustomAttributes(typeof(FlagsAttribute), true).Any())
				//    e = new FlagEnumPropertyEditor(parentEditor, parentGrid);
				//// Basic data type: Other Enums
				//else if (baseType.IsEnum)
				//    e = new EnumPropertyEditor(parentEditor, parentGrid);
				//// Basic data type: String
				//else if (baseType == typeof(string))
				//    e = new StringPropertyEditor(parentEditor, parentGrid);
				//// IList collection
				//else if (typeof(System.Collections.IList).IsAssignableFrom(baseType))
				//    e = new IListPropertyEditor(parentEditor, parentGrid);
				// Unknown data type
				else
				{
					// Ask around if any sub-editor can handle it and choose the most specialized
					var availSubProviders = 
						from p in this.subProviders
						where p.IsResponsibleFor(baseType) != EditorPriority_None
						orderby p.IsResponsibleFor(baseType) descending
						select p;
					IPropertyEditorProvider subProvider = availSubProviders.FirstOrDefault();
					if (subProvider != null)
					{
						e = subProvider.CreateEditor(baseType);
						return e;
					}

					// If not, default to reflection-driven MemberwisePropertyEditor
					e = new MemberwisePropertyEditor();
				}

				e.EditedType = baseType;
				return e;
			}
		}

		private	MainEditorProvider	editorProvider		= new MainEditorProvider();
		private	PropertyEditor		mainEditor			= null;
		private	PropertyEditor		focusEditor			= null;
		private	List<object>		selectedObjects		= new List<object>();
		private	bool				readOnly			= false;
		private	Timer				updateTimer			= null;
		private	int					updateTimerChangeMs	= 0;
		private	bool				updateScheduled		= false;

		public IEnumerable<object> Selection
		{
			get { return this.selectedObjects; }
		}
		public bool ReadOnly
		{
			get { return this.readOnly; }
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
					if (this.mainEditor != null) this.mainEditor.OnReadOnlyChanged();
				}
			}
		}
		public PropertyEditor MainEditor
		{
			get { return this.mainEditor; }
		}
		public PropertyEditor FocusEditor
		{
			get { return this.focusEditor; }
		}
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}

		public PropertyGrid()
		{
			this.updateTimer = new Timer();
			this.updateTimer.Interval = 100;
			this.updateTimer.Tick += this.updateTimer_Tick;
			this.updateTimer.Enabled = true;

			this.AutoScroll = true;
			//this.DoubleBuffered = true;

			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
		
		public void SelectObject(object obj, bool readOnly = false, int scheduleMs = 0)
		{
			if (obj == null)
				this.SelectObjects(new object[0], readOnly, scheduleMs);
			else
				this.SelectObjects(new[] {obj}, readOnly, scheduleMs);
		}
		public void SelectObjects(IEnumerable<object> objEnum, bool readOnly = false, int scheduleMs = 0)
		{
			this.selectedObjects.Clear();
			this.selectedObjects.AddRange(objEnum);
			this.readOnly = readOnly;

			this.UpdateFromObjects(scheduleMs);
		}
		public void UpdateFromObjects(int scheduleMs = 0)
		{
			if (scheduleMs > 0)
			{
				this.updateTimerChangeMs = scheduleMs;
				this.updateScheduled = true;
				return;
			}
			else
				this.updateScheduled = false;

			if (this.selectedObjects.Count > 0)
			{
				Type commonType = this.selectedObjects.First().GetType();
				foreach (object o in this.selectedObjects.Skip(1))
				{
					Type curType = o.GetType();
					while (commonType != curType && !commonType.IsAssignableFrom(curType))
						commonType = commonType.BaseType;
				}

				if (this.mainEditor == null || this.mainEditor.EditedType != commonType)
					this.InitPropertyEditor(commonType);
				else
					this.UpdatePropertyEditor();

				this.mainEditor.PerformGetValue();
			}
			else if (this.mainEditor != null)
				this.DisposePropertyEditor();
		}

		protected void InitPropertyEditor(Type type)
		{
			if (this.mainEditor != null) this.DisposePropertyEditor();

			this.mainEditor = this.editorProvider.CreateEditor(type);
			this.UpdatePropertyEditor();
			this.ConfigureEditor(this.mainEditor);
		}
		protected void UpdatePropertyEditor()
		{
			if (this.mainEditor == null) return;

			this.mainEditor.ParentGrid = this;
			this.mainEditor.Getter = this.ValueGetter;
			this.mainEditor.Setter = this.readOnly ? null : (Action<IEnumerable<object>>)this.ValueSetter;
			this.mainEditor.Width = this.ClientSize.Width;
			if (this.mainEditor is GroupedPropertyEditor)
			{
				GroupedPropertyEditor mainGroupEditor = this.mainEditor as GroupedPropertyEditor;
				mainGroupEditor.Expanded = true;
			}

			this.AutoScrollMinSize = new Size(0, this.mainEditor.Height);
		}
		protected void DisposePropertyEditor()
		{
			if (this.mainEditor == null) return;

			this.mainEditor = null;
		}

		public void RegisterEditorProvider(IPropertyEditorProvider provider)
		{
			if (this.editorProvider.SubProviders.Contains(provider)) return;
			this.editorProvider.SubProviders.Add(provider);
		}
		public void RegisterEditorProvider(IEnumerable<IPropertyEditorProvider> provider)
		{
			foreach (IPropertyEditorProvider prov in provider)
				this.RegisterEditorProvider(prov);
		}
		public PropertyEditor CreateEditor(Type editedType)
		{
			PropertyEditor e = this.editorProvider.CreateEditor(editedType);
			e.EditedType = editedType;
			e.ParentGrid = this;
			return e;
		}
		public virtual void ConfigureEditor(PropertyEditor editor)
		{

		}

		public void Focus(PropertyEditor editor)
		{
			if (this.focusEditor != editor) this.Invalidate();
			if (editor != null)
			{
				this.focusEditor = editor;
				if (!this.Focused) this.Focus();
			}
			else
			{
				this.focusEditor = null;
			}
		}
		public PropertyEditor PickEditorAt(int x, int y)
		{
			if (this.mainEditor == null) return null;
			return this.mainEditor.PickEditorAt(x - this.ClientRectangle.X, y - this.ClientRectangle.Y);
		}
		public Point GetEditorLocation(PropertyEditor editor)
		{
			if (this.mainEditor == null) return Point.Empty;
			Point result = this.mainEditor.GetChildLocation(editor);
			result.X += this.ClientRectangle.X;
			result.Y += this.ClientRectangle.Y;
			return result;
		}

		protected IEnumerable<object> ValueGetter()
		{
			return this.selectedObjects;
		}
		protected void ValueSetter(object val)
		{
			// Don't set anything. The PropertyGrid doesn't actually contain any value data.
			// What should be changed here anyway? The selection?
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Restart();

			GraphicsState originalState = e.Graphics.Save();
			if (this.mainEditor != null)
			{
				Rectangle editorRect = new Rectangle(this.ClientRectangle.Location, this.mainEditor.Size);
				editorRect.Intersect(this.ClientRectangle);
				e.Graphics.SetClip(editorRect);
				e.Graphics.TranslateTransform(this.ClientRectangle.X, this.ClientRectangle.Y + this.AutoScrollPosition.Y);
				this.mainEditor.OnPaint(e);
			}
			e.Graphics.Restore(originalState);

			Console.WriteLine("Paint: {0} ms", watch.ElapsedMilliseconds);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (this.updateScheduled) this.UpdateFromObjects();

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseEnter(EventArgs.Empty);
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseLeave(EventArgs.Empty);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseMove(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X, 
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y, 
					e.Delta));
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseDown(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X, 
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y, 
					e.Delta));
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseUp(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X, 
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y, 
					e.Delta));
			}
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseClick(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X, 
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y, 
					e.Delta));
			}
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnMouseDoubleClick(new MouseEventArgs(
					e.Button, 
					e.Clicks, 
					e.X - this.ClientRectangle.X, 
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y, 
					e.Delta));
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
			{
				KeyEventArgs args = new KeyEventArgs(keyData);
				args.Handled = false;
				this.OnKeyDown(args);
				return args.Handled;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.focusEditor != null) this.focusEditor.OnKeyDown(e);

			if (!e.Handled)
			{
				if (this.focusEditor != null)
				{
					if (e.KeyCode == Keys.Down)
					{
						PropertyEditor current = this.focusEditor;
						PropertyEditor next;
						if (current.ParentEditor == null && current.Children.Any())
						{
							next = current.Children.First();
							next.Focus();
						}
						else
						{
							while (current != null)
							{
								next = current.NextEditor;
								if (next != null)
								{
									next.Focus();
									break;
								}
								else
									current = current.ParentEditor;
							}
						}
						e.Handled = true;
					}
					else if (e.KeyCode == Keys.Up)
					{
						PropertyEditor current = this.focusEditor;
						PropertyEditor prev;
						while (current != null)
						{
							prev = current.PrevEditor;
							if (prev != null)
							{
								prev.Focus();
								break;
							}
							else if (current.ParentEditor != null)
							{
								prev = current.ParentEditor;
								prev.Focus();
								break;
							}
							else
								current = current.ParentEditor;
						}
						e.Handled = true;
					}
					else if (e.KeyCode == Keys.Left)
					{
						if (this.focusEditor.ParentEditor != null) this.focusEditor.ParentEditor.Focus();
						e.Handled = true;
					}
					else if (e.KeyCode == Keys.Right)
					{
						if (this.focusEditor.Children.Any()) this.focusEditor.Children.First().Focus();
						e.Handled = true;
					}
				}
			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (this.focusEditor != null) this.focusEditor.OnKeyUp(e);
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (this.focusEditor != null) this.focusEditor.OnKeyPress(e);
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);

			if (this.mainEditor != null)
			{
				DragEventArgs subEvent = new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - this.ClientRectangle.X,
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y,
					e.AllowedEffect,
					e.Effect);
				this.mainEditor.OnDragEnter(subEvent);
				e.Effect = subEvent.Effect;
			}
		}
		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);

			if (this.mainEditor != null)
			{
				this.mainEditor.OnDragLeave(EventArgs.Empty);
			}
		}
		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);

			if (this.mainEditor != null)
			{
				DragEventArgs subEvent = new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - this.ClientRectangle.X,
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y,
					e.AllowedEffect,
					e.Effect);
				this.mainEditor.OnDragOver(subEvent);
				e.Effect = subEvent.Effect;
			}
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);

			if (this.mainEditor != null)
			{
				DragEventArgs subEvent = new DragEventArgs(
					e.Data, 
					e.KeyState, 
					e.X - this.ClientRectangle.X,
					e.Y - this.ClientRectangle.Y - this.AutoScrollPosition.Y,
					e.AllowedEffect,
					e.Effect);
				this.mainEditor.OnDragDrop(subEvent);
				e.Effect = subEvent.Effect;
			}
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Focus(this.focusEditor ?? this.mainEditor);
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.UpdatePropertyEditor();
		}
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			this.Invalidate();
			if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
			{
				MouseEventArgs childArgs = new MouseEventArgs(Control.MouseButtons, 0, Control.MousePosition.X, Control.MousePosition.Y, 0);
				this.OnMouseMove(childArgs);
			}
		}
		protected override Point ScrollToControl(Control activeControl)
		{
			// Prevent AutoScroll on focus or content resize - will always scroll to top.
			// Solution: Just don't scroll. Won't be needed here anyway.
			return this.AutoScrollPosition;
			//return base.ScrollToControl(activeControl);
		}
		private void updateTimer_Tick(object sender, EventArgs e)
		{
			if (this.updateScheduled)
			{
				this.updateTimerChangeMs -= this.updateTimer.Interval;
				if (this.updateTimerChangeMs <= 0) this.UpdateFromObjects();
			}
		}
	}
}
