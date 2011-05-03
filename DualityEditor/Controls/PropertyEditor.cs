using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;

using Duality;

namespace DualityEditor.Controls
{
	public class PropertyGridValueEditedEventArgs : EventArgs
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

		public PropertyGridValueEditedEventArgs(PropertyEditor editor, object value)
		{
			this.editor = editor;
			this.value = value;
		}
	}

	public class PropertyEditor : UserControl
	{
		private	Type			editedType			= null;
		private	Func<IEnumerable<object>>	getter	= null;
		private	Action<IEnumerable<object>>	setter	= null;
		private	PropertyGrid	parentGrid			= null;
		private	PropertyEditor	parentEditor		= null;
		private	bool			contentInit			= false;

		public event EventHandler<PropertyGridValueEditedEventArgs> ValueEdited = null;
		
		public Type EditedType
		{
			get { return this.editedType; }
			set 
			{
				if (this.editedType != value)
				{
					this.editedType = value;
					this.OnEditedTypeChanged();
				}
			}
		}
		public Func<IEnumerable<object>> Getter
		{
			get { return this.getter; }
			set { this.getter = value; }
		}
		public Action<IEnumerable<object>> Setter
		{
			get { return this.setter; }
			set
			{ 
				if (this.setter != value)
				{
					bool lastReadOnly = this.ReadOnly;
					this.setter = value;
					if (this.ReadOnly != lastReadOnly) this.UpdateReadOnlyState();
				}
			}
		}
		public Action<object> SetterSingle
		{
			get { return o => this.Setter(new object[1] { o }); }
		}
		public virtual bool Expanded
		{
			get { return true; }
			set {}
		}
		public virtual string PropertyName
		{
			get { return null; }
			set {}
		}
		public virtual object DisplayedValue
		{
			get { return null; }
		}

		public PropertyGrid ParentGrid
		{
			get { return this.parentGrid; }
		}
		public PropertyEditor ParentEditor
		{
			get { return this.parentEditor; }
		}
		public int NameLabelWidth
		{
			get { return this.Width * 2 / 5; }
		}
		public bool ReadOnly
		{
			get 
			{ 
				return 
					this.setter == null ||
					this.parentGrid.ReadOnlySelection || 
					(this.parentEditor != null && this.parentEditor.ReadOnly);
			}
		}
		public bool ContentInitialized
		{
			get { return this.contentInit; }
		}

		public Color BackColorMultiple
		{
			get { return Color.Bisque; }
		}
		public Color BackColorDefault
		{
			get { return this.ReadOnly ? SystemColors.Control : SystemColors.Window; }
		}

		protected PropertyEditor() {}
		protected PropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid)
		{
			this.parentEditor = parentEditor;
			this.parentGrid = parentGrid;
		}

		public virtual void PerformGetValue() {}
		public virtual void PerformSetValue() {}
		public virtual void InitContent()
		{
			this.contentInit = true;
		}
		public virtual void ClearContent()
		{
			this.contentInit = false;
		}

		public virtual void UpdateReadOnlyState() {}
		protected virtual void OnEditedTypeChanged() {}
		protected void OnValueEdited(object sender, PropertyGridValueEditedEventArgs args)
		{
			if (this.ValueEdited != null)
				this.ValueEdited(sender, args);
		}
		protected void OnValueEdited(object value)
		{
			this.OnValueEdited(this, new PropertyGridValueEditedEventArgs(this, value));
		}
	}
}
