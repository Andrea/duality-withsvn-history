using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using Duality.Serialization;
using Duality.Serialization.MetaFormat;

using DualityEditor;

namespace ResourceHacker.PropertyEditors
{
	public class PrimitiveNodePropertyEditor : MemberwisePropertyEditor
	{
		protected	PropertyEditor	editorPrimitiveValue	= null;
		protected	bool			isInitializingContent	= false;
		

		public override void PerformGetValue()
		{
			if (this.isInitializingContent) return;

			this.isInitializingContent = true;
			this.InitContent();
			this.isInitializingContent = false;

			base.PerformGetValue();
		}
		protected override PropertyEditor AutoCreateMemberEditor(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, typeof(PrimitiveNode).GetProperty("PrimitiveValue")))
			{
				PrimitiveNode primitiveNode = this.GetValue().NotNull().FirstOrDefault() as PrimitiveNode;
				Type actualType = primitiveNode.NodeType.ToActualType();
				if (actualType == null) actualType = (info as PropertyInfo).PropertyType;

				this.editorPrimitiveValue = this.ParentGrid.CreateEditor(actualType);
				return this.editorPrimitiveValue;
			}
			else
				return base.AutoCreateMemberEditor(info);
		}
	}
}
