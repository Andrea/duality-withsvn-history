using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;
using Duality.Components;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class SoundEmitterPropertyEditor : ComponentPropertyEditor
	{
		private	List<SoundEmitterSourcePropertyEditor>	soundSourceEditors	= new List<SoundEmitterSourcePropertyEditor>();

		public SoundEmitterPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}
		
		public override void ClearContent()
		{
			base.ClearContent();
			this.soundSourceEditors.Clear();
		}
		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
			this.AllowDrop = !this.ReadOnly;
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Sources)) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			this.UpdateSourceEditors(values.Cast<SoundEmitter>());
		}
		
		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs<Sound>())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
			}
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs<Sound>())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;

				ContentRef<Sound>[] ctRefs = dragDropData.GetContentRefs<Sound>();
				IEnumerable<SoundEmitter> values = this.Getter().Cast<SoundEmitter>().NotNull();

				foreach (SoundEmitter emit in values) emit.Sources.Add(new SoundEmitter.Source(ctRefs[0]));

				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
					new ObjectSelection(values),
					ReflectionInfo.Property_SoundEmitter_Sources);
				this.PerformGetValue();
			}
		}

		protected void UpdateSourceEditors(IEnumerable<SoundEmitter> values)
		{
			int visibleElementCount = values.Where(o => o != null).Min(o => o.Sources.Count);

			this.BeginUpdate();
			// Add missing editors
			for (int i = 0; i < visibleElementCount; i++)
			{
				SoundEmitterSourcePropertyEditor elementEditor;
				if (i < this.soundSourceEditors.Count)
					elementEditor = this.soundSourceEditors[i];
				else
				{
					elementEditor = new SoundEmitterSourcePropertyEditor(this, this.ParentGrid);
					this.soundSourceEditors.Add(elementEditor);
					this.AddPropertyEditor(elementEditor);
				}
				elementEditor.Getter = this.CreateSourceValueGetter(i);
				elementEditor.Setter = this.CreateSourceValueSetter(i);
			}
			// Remove overflowing editors
			for (int i = this.soundSourceEditors.Count - 1; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.soundSourceEditors[i]);
				this.soundSourceEditors.RemoveAt(i);
			}
			this.EndUpdate();
		}

		protected Func<IEnumerable<object>> CreateSourceValueGetter(int index)
		{
			return () => this.Getter().Cast<SoundEmitter>().Select(o => (o != null && o.Sources.Count > index) ? o.Sources[index] : null);
		}
		protected Action<IEnumerable<object>> CreateSourceValueSetter(int index)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerable<SoundEmitter.Source> valuesCast = values.Cast<SoundEmitter.Source>();
				SoundEmitter[] targetArray = this.Getter().Cast<SoundEmitter>().ToArray();

				// Explicitly setting the values to null: Remove corresponding source list entry
				if (valuesCast.All(v => v == null))
				{
					foreach (SoundEmitter target in targetArray)
					{
						target.Sources.RemoveAt(index);
					}
					this.UpdateModifiedState();
					this.PerformGetValue();
				}
				// Otherwise, just set the values
				else
				{
					IEnumerator<SoundEmitter.Source> valuesCastEnum = valuesCast.GetEnumerator();
					SoundEmitter.Source curValue = null;
					if (valuesCastEnum.MoveNext()) curValue = valuesCastEnum.Current;
					foreach (SoundEmitter target in targetArray)
					{
						if (target != null) target.Sources[index] = curValue;
						if (valuesCastEnum.MoveNext()) curValue = valuesCastEnum.Current;
					}
					this.UpdateModifiedState();
				}
			};
		}
	}

	public class SoundEmitterSourcePropertyEditor : MemberwisePropertyEditor
	{
		public SoundEmitterSourcePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
			this.EditedType = typeof(SoundEmitter.Source);
			this.Header.Style = GroupedPropertyEditorHeader.HeaderStyle.Big;
			this.Header.Height = GroupedPropertyEditorHeader.DefaultBigHeight;
			this.Indent = DefaultIndent;
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Source_Disposed)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Source_Instance)) return false;
			return base.MemberPredicate(info);
		}
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Source_Volume))
			{
				NumericPropertyEditor e = new NumericPropertyEditor(this, this.ParentGrid);
				e.EditedType = ReflectionInfo.Property_SoundEmitter_Source_Volume.PropertyType;
				e.Editor.Minimum = 0.0m;
				e.Editor.Maximum = 2.0m;
				e.Editor.Increment = 0.1m;
				return e;
			}
			else if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Source_Pitch))
			{
				NumericPropertyEditor e = new NumericPropertyEditor(this, this.ParentGrid);
				e.EditedType = ReflectionInfo.Property_SoundEmitter_Source_Volume.PropertyType;
				e.Editor.Minimum = 0.0m;
				e.Editor.Maximum = 10.0m;
				e.Editor.Increment = 0.1m;
				return e;
			}
			return base.MemberEditor(info);
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			IEnumerable<SoundEmitter.Source> sources = values.Cast<SoundEmitter.Source>();

			this.Header.ValueText = null;
			if (sources.NotNull().Any())
				this.Header.Text = sources.NotNull().First().Sound.ToString();
			else
				this.Header.Text = "null";
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_SoundEmitter_Source_Sound))
				this.PerformGetValue();
		}
	}
}
