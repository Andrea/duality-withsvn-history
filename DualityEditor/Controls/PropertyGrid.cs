using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;

using OpenTK;
using Duality;
using DualityEditor.Controls.PropertyEditors;

namespace DualityEditor.Controls
{
	public partial class PropertyGrid : UserControl
	{
		public enum ProvidedEditorType
		{
			None		= 0,
			General		= 1,
			Specialized	= 2,
			Override	= 3
		}
		public interface IPropertyEditorProvider
		{
			ProvidedEditorType IsResponsibleFor(Type baseType);
			PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid);
		}
		public class MainEditorProvider : IPropertyEditorProvider
		{
			private	List<IPropertyEditorProvider>	subProviders	= new List<IPropertyEditorProvider>();

			public List<IPropertyEditorProvider> SubProviders
			{
				get { return this.subProviders; }
				set { this.subProviders = value; }
			}

			public ProvidedEditorType IsResponsibleFor(Type baseType)
			{
				return ProvidedEditorType.General;
			}
			public PropertyEditor CreateEditor(Type baseType, PropertyEditor parentEditor, PropertyGrid parentGrid)
			{
				PropertyEditor e = null;

				// Basic numeric data types
				if (baseType == typeof(sbyte) || baseType == typeof(byte) ||
					baseType == typeof(short) || baseType == typeof(ushort) ||
					baseType == typeof(int) || baseType == typeof(uint) ||
					baseType == typeof(long) || baseType == typeof(ulong) ||
					baseType == typeof(float) || baseType == typeof(double) || baseType == typeof(decimal))
					e = new NumericPropertyEditor(parentEditor, parentGrid);
				// Basic data type: Boolean
				else if (baseType == typeof(bool))
					e = new BoolPropertyEditor(parentEditor, parentGrid);
				// Basic data type: Flagged Enum
				else if (baseType.IsEnum && baseType.GetCustomAttributes(typeof(FlagsAttribute), true).Any())
					e = new FlagEnumPropertyEditor(parentEditor, parentGrid);
				// Basic data type: Other Enums
				else if (baseType.IsEnum)
					e = new EnumPropertyEditor(parentEditor, parentGrid);
				// Basic data type: String
				else if (baseType == typeof(string))
					e = new StringPropertyEditor(parentEditor, parentGrid);
				// Rect
				else if (baseType == typeof(Rect))
					e = new RectPropertyEditor(parentEditor, parentGrid);
				// Vector2
				else if (baseType == typeof(Vector2))
					e = new Vector2PropertyEditor(parentEditor, parentGrid);
				// Vector3
				else if (baseType == typeof(Vector3))
					e = new Vector3PropertyEditor(parentEditor, parentGrid);
				// IList collection
				else if (typeof(System.Collections.IList).IsAssignableFrom(baseType))
					e = new IListPropertyEditor(parentEditor, parentGrid);
				// Unknown data type
				else
				{
					// Ask around if any sub-editor can handle it and choose the most specialized
					var availSubProviders = 
						from p in this.subProviders
						where p.IsResponsibleFor(baseType) != ProvidedEditorType.None
						orderby (int)p.IsResponsibleFor(baseType) descending
						select p;
					IPropertyEditorProvider subProvider = availSubProviders.FirstOrDefault();
					if (subProvider != null) return subProvider.CreateEditor(baseType, parentEditor, parentGrid);

					// If not, default to reflection-driven MemberwisePropertyEditor
					e = new MemberwisePropertyEditor(parentEditor, parentGrid, MemberwisePropertyEditor.MemberFlags.Default);
					if (parentEditor == null)
					{
						MemberwisePropertyEditor me = e as MemberwisePropertyEditor;
						me.Header.ResetVisible = false;
						me.Header.ExpandVisible = false;
						me.Header.Text = null;
					}
				}

				e.EditedType = baseType;
				return e;
			}
		}

		private	MainEditorProvider	propertyEditorProvider	= new MainEditorProvider();
		private	PropertyEditor		propertyEditor			= null;
		private	List<object>		selectedObjects			= new List<object>();
		private	bool				readOnlySelection		= false;
		private	Timer				updateTimer				= null;
		private	int					updateTimerChangeMs		= 0;
		private	bool				updateScheduled			= false;

		public IPropertyEditorProvider PropertyEditorProvider
		{
			get { return this.propertyEditorProvider; }
		}
		public IEnumerable<object> Selection
		{
			get { return this.selectedObjects; }
		}
		public bool ReadOnlySelection
		{
			get { return this.readOnlySelection; }
		}

		public PropertyGrid()
		{
			this.updateTimer = new Timer();
			this.updateTimer.Interval = 100;
			this.updateTimer.Tick += this.updateTimer_Tick;
			this.updateTimer.Enabled = true;

			this.AutoScroll = true;

			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		public void SelectObjects(IEnumerable<object> objEnum, bool readOnly = false, int scheduleMs = 0)
		{
			this.selectedObjects.Clear();
			this.selectedObjects.AddRange(objEnum);
			this.readOnlySelection = readOnly;

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

				if (this.propertyEditor == null || this.propertyEditor.EditedType != commonType)
				{
					this.InitPropertyEditor(commonType);
				}
				else
				{
					this.propertyEditor.Visible = true;
					this.propertyEditor.Enabled = true;
				}
				this.propertyEditor.UpdateReadOnlyState();
				this.propertyEditor.PerformGetValue();
			}
			else if (this.propertyEditor != null)
			{
				this.propertyEditor.Visible = false;
				this.propertyEditor.Enabled = false;
			}
		}

		protected void InitPropertyEditor(Type type)
		{
			if (this.propertyEditor != null) this.DisposePropertyEditor();

			this.propertyEditor = this.propertyEditorProvider.CreateEditor(type, null, this);

			this.propertyEditor.Dock = DockStyle.Top;
			this.Controls.Add(this.propertyEditor);
			
			this.propertyEditor.Getter = this.ValueGetter;
			this.propertyEditor.Setter = this.ValueSetter;
			this.propertyEditor.Expanded = true;
		}
		protected void DisposePropertyEditor()
		{
			if (this.propertyEditor != null)
			{
				this.Controls.Remove(this.propertyEditor);
				this.propertyEditor.Dispose();
				this.propertyEditor = null;
			}
		}

		public void RegisterEditorProvider(IPropertyEditorProvider provider)
		{
			if (this.propertyEditorProvider.SubProviders.Contains(provider)) return;
			this.propertyEditorProvider.SubProviders.Add(provider);
		}
		public void RegisterEditorProvider(IEnumerable<IPropertyEditorProvider> provider)
		{
			foreach (IPropertyEditorProvider prov in provider)
				this.RegisterEditorProvider(prov);
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
		}
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.updateScheduled) this.UpdateFromObjects();
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
