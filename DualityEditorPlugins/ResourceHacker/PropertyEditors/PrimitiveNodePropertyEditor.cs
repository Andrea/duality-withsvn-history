using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Serialization;

using DualityEditor;
using DualityEditor.Controls;
using DualityEditor.Controls.PropertyEditors;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace ResourceHacker.PropertyEditors
{
	public class PrimitiveNodePropertyEditor : DataNodePropertyEditor
	{
		protected	PropertyEditor	editorPrimitiveValue	= null;
		protected	bool			isInitializingContent	= false;
		
		public PrimitiveNodePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		public override void PerformGetValue()
		{
			if (this.isInitializingContent) return;

			this.isInitializingContent = true;
			this.InitContent();
			this.isInitializingContent = false;

			base.PerformGetValue();
		}
		protected override PropertyEditor MemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, typeof(BinaryMetaFormatter.PrimitiveNode).GetProperty("PrimitiveValue")))
			{
				BinaryMetaFormatter.PrimitiveNode primitiveNode = this.Getter().NotNull().FirstOrDefault() as BinaryMetaFormatter.PrimitiveNode;
				Type actualType = primitiveNode.NodeType.ToActualType();
				if (actualType == null) actualType = (info as PropertyInfo).PropertyType;

				this.editorPrimitiveValue = this.ParentGrid.PropertyEditorProvider.CreateEditor(actualType, this, this.ParentGrid);
				return this.editorPrimitiveValue;
			}
			else
				return base.MemberEditor(info);
		}
	}
}
