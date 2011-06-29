using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class FormattedTextPropertyEditor : MemberwisePropertyEditor
	{
		public FormattedTextPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_FormattedText_DisplayedText)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_FormattedText_Elements)) return false;
			return base.MemberPredicate(info);
		}
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_FormattedText_Icons) ||
				ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_FormattedText_FlowAreas) ||
				ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_FormattedText_Fonts))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor((info as PropertyInfo).PropertyType, this, this.ParentGrid);
				IListPropertyEditor listEdit = e as IListPropertyEditor;
				if (listEdit != null) listEdit.ValueEdited += delegate(object sender, PropertyGridValueEditedEventArgs args) { listEdit.PerformSetValue(); };
				return e;
			}
			return base.MemberEditor(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			FormattedText[] text = targets.Cast<FormattedText>().NotNull().ToArray();
			foreach (FormattedText t in text) t.ApplySource();

			this.Setter(targets);
		}
	}
}
