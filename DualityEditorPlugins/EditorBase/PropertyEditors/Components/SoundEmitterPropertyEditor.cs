using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Components;
using Duality.Resources;

using DualityEditor;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public class SoundEmitterPropertyEditor : ComponentPropertyEditor
	{
		private	List<SoundEmitterSourcePropertyEditor>	soundSourceEditors	= new List<SoundEmitterSourcePropertyEditor>();

		public override void ClearContent()
		{
			base.ClearContent();
			this.soundSourceEditors.Clear();
		}

		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			this.UpdateSourceEditors(this.GetValue().Cast<SoundEmitter>());
		}

		protected override bool IsAutoCreateMember(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionInfo.Property_SoundEmitter_Sources)) return false;
			return base.IsAutoCreateMember(info);
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			this.UpdateSourceEditors(values.Cast<SoundEmitter>());
		}
		
		protected override void OnDragOver(DragEventArgs e)
		{
			bool handled = false;
			DataObject dragDropData = e.Data as DataObject;
			if (!this.ReadOnly && dragDropData != null && new ConvertOperation(dragDropData, ConvertOperation.Operation.All).CanPerform<Sound>())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
				handled = true;
			}

			if (!handled) base.OnDragOver(e);
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			bool handled = false;
			DataObject dragDropData = e.Data as DataObject;
			if (!this.ReadOnly && dragDropData != null)
			{
				ConvertOperation convert = new ConvertOperation(dragDropData, ConvertOperation.Operation.All);
				if (convert.CanPerform<Sound>())
				{
					// Accept drop
					e.Effect = e.AllowedEffect;

					Sound[] sounds = convert.Perform<Sound>().ToArray();
					IEnumerable<SoundEmitter> values = this.GetValue().Cast<SoundEmitter>().NotNull();

					foreach (Sound sound in sounds)
					{
						foreach (SoundEmitter emit in values)
						{
							emit.Sources.Add(new SoundEmitter.Source(sound));
						}
					}

					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
						new ObjectSelection(values),
						ReflectionInfo.Property_SoundEmitter_Sources);
					this.PerformGetValue();
					handled = true;
				}
			}

			if (!handled) base.OnDragDrop(e);
		}

		protected void UpdateSourceEditors(IEnumerable<SoundEmitter> values)
		{
			int visibleElementCount = values.Where(o => o != null).Min(o => o.Sources.Count);

			// Add missing editors
			for (int i = 0; i < visibleElementCount; i++)
			{
				SoundEmitterSourcePropertyEditor elementEditor;
				if (i < this.soundSourceEditors.Count)
					elementEditor = this.soundSourceEditors[i];
				else
				{
					elementEditor = new SoundEmitterSourcePropertyEditor();
					this.ParentGrid.ConfigureEditor(elementEditor);
					this.soundSourceEditors.Add(elementEditor);
					this.AddPropertyEditor(elementEditor);
				}
				elementEditor.PropertyName = string.Format("Sources[{0}]", i);
				elementEditor.Getter = this.CreateSourceValueGetter(i);
				elementEditor.Setter = this.CreateSourceValueSetter(i);
			}
			// Remove overflowing editors
			for (int i = this.soundSourceEditors.Count - 1; i >= visibleElementCount; i--)
			{
				this.RemovePropertyEditor(this.soundSourceEditors[i]);
				this.soundSourceEditors.RemoveAt(i);
			}
		}

		protected Func<IEnumerable<object>> CreateSourceValueGetter(int index)
		{
			return () => this.GetValue().Cast<SoundEmitter>().Select(o => (o != null && o.Sources.Count > index) ? o.Sources[index] : null);
		}
		protected Action<IEnumerable<object>> CreateSourceValueSetter(int index)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerable<SoundEmitter.Source> valuesCast = values.Cast<SoundEmitter.Source>();
				SoundEmitter[] targetArray = this.GetValue().Cast<SoundEmitter>().ToArray();

				// Explicitly setting the values to null: Remove corresponding source list entry
				if (valuesCast.All(v => v == null))
				{
					foreach (SoundEmitter target in targetArray)
					{
						if (target.Sources[index].Instance != null) target.Sources[index].Instance.Dispose();
						target.Sources.RemoveAt(index);
					}
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
				}
			};
		}
	}

	public class SoundEmitterSourcePropertyEditor : MemberwisePropertyEditor
	{
		public SoundEmitterSourcePropertyEditor()
		{
			this.EditedType = typeof(SoundEmitter.Source);
			this.HeaderStyle = AdamsLair.PropertyGrid.Renderer.GroupHeaderStyle.SmoothSunken;
			this.HeaderHeight = 30;
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);
			IEnumerable<SoundEmitter.Source> sources = values.Cast<SoundEmitter.Source>();

			this.HeaderValueText = null;
			if (sources.NotNull().Any())
				this.HeaderValueText = sources.NotNull().First().Sound.FullName;
			else
				this.HeaderValueText = "null";
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_SoundEmitter_Source_Sound))
				this.PerformGetValue();
		}
	}
}
