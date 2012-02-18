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
using Duality.EditorHints;

using DualityEditor.Forms;

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

	public class PropertyEditor : UserControl, IHelpProvider
	{
		private	Type			editedType			= null;
		private	MemberInfo		editedMember		= null;
		private	Func<IEnumerable<object>>	getter	= null;
		private	Action<IEnumerable<object>>	setter	= null;
		private	PropertyGrid	parentGrid			= null;
		private	PropertyEditor	parentEditor		= null;
		private	bool			contentInit			= false;
		private	bool			forceWriteBack		= false;

		public event EventHandler<PropertyGridValueEditedEventArgs> ValueEdited = null;
		public event EventHandler EditingFinished = null;
		
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
		public bool ForceWriteBack
		{
			get { return this.forceWriteBack; }
			set { this.forceWriteBack = value; }
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

		public PropertyGrid ParentGrid
		{
			get { return this.parentGrid; }
			internal set
			{
				this.parentGrid = value;
				if (this.parentGrid == null) this.parentEditor = null;
			}
		}
		public PropertyEditor ParentEditor
		{
			get { return this.parentEditor; }
			internal set
			{
				this.parentEditor = value;
				if (this.parentEditor != null) this.parentGrid = this.parentEditor.ParentGrid;
			}
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
					(this.parentGrid != null && this.parentGrid.ReadOnlySelection) || 
					(this.parentEditor != null && this.parentEditor.ReadOnly);
			}
		}
		public bool ContentInitialized
		{
			get { return this.contentInit; }
		}
		public virtual object DisplayedValue
		{
			get { return null; }
		}
		public bool ValueModified
		{
			get { return this.parentEditor == null ? false : this.parentEditor.IsChildValueModified(this); }
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
		public virtual void UpdateModifiedState() {}

		protected virtual bool IsChildValueModified(PropertyEditor childEditor) { return false; }

		protected virtual void OnEditedTypeChanged() {}
		protected virtual void OnEditedMemberChanged()
		{
			if (this.editedMember != null)
			{
				EditorHintFlagsAttribute flagsAttrib = this.editedMember.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
				this.forceWriteBack = flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.ForceWriteback) != MemberFlags.None;
			}
			else
				this.forceWriteBack = false;
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			this.OnEditingFinished();
		}
		protected void OnValueEdited(object sender, PropertyGridValueEditedEventArgs args)
		{
			if (this.ValueEdited != null)
				this.ValueEdited(sender, args);
		}
		protected void OnValueEdited(object value)
		{
			this.OnValueEdited(this, new PropertyGridValueEditedEventArgs(this, value));
		}
		protected void OnEditingFinished(object sender, EventArgs e)
		{
			if (this.EditingFinished != null)
				this.EditingFinished(sender, e);
		}
		protected void OnEditingFinished()
		{
			this.OnEditingFinished(this, EventArgs.Empty);
		}

		public virtual HelpInfo ProvideHoverHelp(Point localPos, ref bool captured)
		{
			if (this.EditedMember != null)
				return HelpInfo.FromMember(this.EditedMember);
			else if (this.EditedType != null)
				return HelpInfo.FromMember(this.EditedType);
			else
				return null;
		}
		public virtual bool PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}
	}
}
