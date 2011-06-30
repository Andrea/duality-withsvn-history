using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class RenderTargetPropertyEditor : ResourcePropertyEditor
	{
		public RenderTargetPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}
		
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_RenderTarget_Targets))
			{
				PropertyEditor e = this.ParentGrid.PropertyEditorProvider.CreateEditor((info as PropertyInfo).PropertyType, this, this.ParentGrid);
				IListPropertyEditor listEdit = e as IListPropertyEditor;
				if (listEdit != null) listEdit.ForceWriteBack = true;
				return e;
			}
			return base.MemberEditor(info);
		}
	}
}
