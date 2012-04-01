using System;
using System.Linq;

using AdamsLair.PropertyGrid.EditorTemplates;

using Duality;

namespace DualityEditor.Controls.PropertyEditors
{
	public class RectPropertyEditor : VectorPropertyEditor
	{
		public override object DisplayedValue
		{
			get 
			{ 
				return new Rect((float)this.editor[0].Value, (float)this.editor[1].Value, (float)this.editor[2].Value, (float)this.editor[3].Value);
			}
		}


		public RectPropertyEditor() : base(4, 2)
		{
			this.editor[0].Edited += this.editorX_Edited;
			this.editor[1].Edited += this.editorY_Edited;
			this.editor[2].Edited += this.editorW_Edited;
			this.editor[3].Edited += this.editorH_Edited;
		}


		public override void PerformGetValue()
		{
			base.PerformGetValue();
			object[] values = this.GetValue().ToArray();

			this.BeginUpdate();
			if (!values.Any())
			{
				this.editor[0].Value = 0;
				this.editor[1].Value = 0;
				this.editor[2].Value = 0;
				this.editor[3].Value = 0;
			}
			else
			{
				var valNotNull = values.NotNull();
				float avgX = valNotNull.Average(o => ((Rect)o).x);
				float avgY = valNotNull.Average(o => ((Rect)o).y);
				float avgW = valNotNull.Average(o => ((Rect)o).w);
				float avgH = valNotNull.Average(o => ((Rect)o).h);

				this.editor[0].Value = MathF.SafeToDecimal(avgX);
				this.editor[1].Value = MathF.SafeToDecimal(avgY);
				this.editor[2].Value = MathF.SafeToDecimal(avgW);
				this.editor[3].Value = MathF.SafeToDecimal(avgH);

				this.multiple[0] = (values.Any(o => o == null) || values.Any(o => ((Rect)o).x != avgX));
				this.multiple[1] = (values.Any(o => o == null) || values.Any(o => ((Rect)o).y != avgY));
				this.multiple[2] = (values.Any(o => o == null) || values.Any(o => ((Rect)o).w != avgW));
				this.multiple[3] = (values.Any(o => o == null) || values.Any(o => ((Rect)o).h != avgH));
			}
			this.EndUpdate();
		}
		protected override void ApplyDefaultSubEditorConfig(NumericEditorTemplate subEditor)
		{
			base.ApplyDefaultSubEditorConfig(subEditor);
			subEditor.DecimalPlaces = 0;
			subEditor.Increment = 1;
		}

		private void editorX_Edited(object sender, EventArgs e)
		{
			if (this.IsUpdatingFromObject) return;
			if (!this.ReadOnly)
			{
				object[] values = this.GetValue().ToArray();
				Rect newVal = (Rect)this.DisplayedValue;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
						values[i] = this.DisplayedValue;
					else
					{
						Rect oldVal = (Rect)values[i];
						values[i] = new Rect(newVal.x, oldVal.y, oldVal.w, oldVal.h);
					}
				}
				this.SetValues(values);
				this.OnValueChanged();
			}
			this.PerformGetValue();
		}
		private void editorY_Edited(object sender, EventArgs e)
		{
			if (this.IsUpdatingFromObject) return;
			if (!this.ReadOnly)
			{
				object[] values = this.GetValue().ToArray();
				Rect newVal = (Rect)this.DisplayedValue;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
						values[i] = this.DisplayedValue;
					else
					{
						Rect oldVal = (Rect)values[i];
						values[i] = new Rect(oldVal.x, newVal.y, oldVal.w, oldVal.h);
					}
				}
				this.SetValues(values);
				this.OnValueChanged();
			}
			this.PerformGetValue();
		}
		private void editorW_Edited(object sender, EventArgs e)
		{
			if (this.IsUpdatingFromObject) return;
			if (!this.ReadOnly)
			{
				object[] values = this.GetValue().ToArray();
				Rect newVal = (Rect)this.DisplayedValue;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
						values[i] = this.DisplayedValue;
					else
					{
						Rect oldVal = (Rect)values[i];
						values[i] = new Rect(oldVal.x, oldVal.y, newVal.w, oldVal.h);
					}
				}
				this.SetValues(values);
				this.OnValueChanged();
			}
			this.PerformGetValue();
		}
		private void editorH_Edited(object sender, EventArgs e)
		{
			if (this.IsUpdatingFromObject) return;
			if (!this.ReadOnly)
			{
				object[] values = this.GetValue().ToArray();
				Rect newVal = (Rect)this.DisplayedValue;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
						values[i] = this.DisplayedValue;
					else
					{
						Rect oldVal = (Rect)values[i];
						values[i] = new Rect(oldVal.x, oldVal.y, oldVal.w, newVal.h);
					}
				}
				this.SetValues(values);
				this.OnValueChanged();
			}
			this.PerformGetValue();
		}
	}
}

