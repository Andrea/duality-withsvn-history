using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class GameObjectOverviewPropertyEditor : GroupedPropertyEditor
	{
		private	GameObjectPropertyEditor		gameObjEditor		= null;
		private	Dictionary<Type,PropertyEditor>	componentEditors	= new Dictionary<Type,PropertyEditor>();

		public override string PropertyName
		{
			get { return base.PropertyName; }
			set
			{
				if (this.PropertyName != value)
				{
					base.PropertyName = value;
					this.Header.Visible = !string.IsNullOrEmpty(this.PropertyName);
				}
			}
		}

		public GameObjectOverviewPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.Header.Visible = false;
			this.Header.ResetVisible = false;

			this.gameObjEditor = new GameObjectPropertyEditor(this, parentGrid);
			this.gameObjEditor.EditedType = typeof(GameObject);
			this.gameObjEditor.PropertyName = "GameObject";
		}

		public override void InitContent()
		{
			base.InitContent();

			this.ClearPropertyEditors();
			if (this.EditedType != null)
			{
				this.PerformGetValue();
			}
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			GameObject[] values = this.Getter().Cast<GameObject>().ToArray();
			if (values == null) return;

			if (!values.Any() || values.All(o => o == null))
			{
				this.ClearContent();
				this.Expanded = false;
			}
			else
			{
				if (this.ContentInitialized)
				{
					this.UpdateComponentEditors(values);
					foreach (PropertyEditor e in this.PropertyEditors)
						e.PerformGetValue();
				}
				if (!this.Header.Visible)
					this.Expanded = !this.ContentInitialized || this.PropertyEditors.Any();
			}
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (!this.PropertyEditors.Any()) return;

			foreach (PropertyEditor e in this.PropertyEditors)
				e.PerformSetValue();
		}

		protected void UpdateComponentEditors(GameObject[] values)
		{
			this.BeginUpdate();

			if (!this.PropertyEditors.Any())
			{
				this.gameObjEditor.Getter = this.Getter;
				this.gameObjEditor.Setter = this.Setter;
				this.AddPropertyEditor(this.gameObjEditor);
				this.gameObjEditor.UpdateReadOnlyState();
			}

			// Remove Component editors that aren't needed anymore
			var cmpEditorCopy = new Dictionary<Type,PropertyEditor>(this.componentEditors);
			foreach (var pair in cmpEditorCopy)
			{
				if (!values.Any(o => o.GetComponent(pair.Key, true) != null))
				{
					this.RemovePropertyEditor(pair.Value);
					this.componentEditors.Remove(pair.Key);
				}
			}

			// Create the ones that are needed now and not added yet
			foreach (Type t in values.GetComponents<Component>().Select(c => c.GetType()).Distinct())
			{
				if (!this.componentEditors.ContainsKey(t))
				{
					PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor(t, this, this.ParentGrid);
					e.EditedType = t;
					e.Getter = this.CreateComponentValueGetter(t);
					e.Setter = this.CreateComponentValueSetter(t);
					e.PropertyName = ReflectionHelper.GetTypeName(t, TypeNameFormat.CSCodeIdentShort);
					this.AddPropertyEditor(e);
					e.UpdateReadOnlyState();
					this.componentEditors[t] = e;
				}
			}

			this.EndUpdate();
		}

		protected Func<IEnumerable<object>> CreateComponentValueGetter(Type componentType)
		{
			return () => this.Getter().Select(o => o != null ? (o as GameObject).GetComponent(componentType) : null);
		}
		protected Action<IEnumerable<object>> CreateComponentValueSetter(Type componentType)
		{
			// We don't need a setter. At all.
			return v => {};
		}
	}
}
