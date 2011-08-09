using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Serialization;
using Duality.Serialization.MetaFormat;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace ResourceHacker.PropertyEditors
{
	public class ArrayNodePropertyEditor : DataNodePropertyEditor
	{
		protected	PropertyEditor	editorPrimitiveData	= null;

		public ArrayNodePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);

			ArrayNode arrayNode = values.NotNull().FirstOrDefault() as ArrayNode;
			Type actualType = ReflectionHelper.ResolveType(arrayNode.TypeString);
			bool primitiveDataEditable = actualType != null && actualType.IsArray && (actualType.GetElementType().IsPrimitive || actualType.GetElementType() == typeof(string));
			
			if (primitiveDataEditable) this.editorPrimitiveData.EditedType = actualType;
			this.editorPrimitiveData.Enabled = primitiveDataEditable;
			if (!primitiveDataEditable && this.editorPrimitiveData.Expanded) this.editorPrimitiveData.Expanded = false;
		}
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, typeof(ArrayNode).GetProperty("PrimitiveData")))
			{
				ArrayNode arrayNode = this.Getter().NotNull().FirstOrDefault() as ArrayNode;
				Type actualType = ReflectionHelper.ResolveType(arrayNode.TypeString);
				if (actualType == null || !actualType.IsArray || actualType.GetElementType() == null) actualType = (info as PropertyInfo).PropertyType;

				this.editorPrimitiveData = this.ParentGrid.PropertyEditorProvider.CreateEditor(actualType, this, this.ParentGrid);
				return this.editorPrimitiveData;
			}
			else
				return base.MemberEditor(info);
		}
	}
}
