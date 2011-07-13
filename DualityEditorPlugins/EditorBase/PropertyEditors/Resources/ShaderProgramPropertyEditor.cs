using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using DualityEditor;
using DualityEditor.Controls;

using DualityEditor.Controls.PropertyEditors;

namespace EditorBase.PropertyEditors
{
	public class ShaderProgramPropertyEditor : ResourcePropertyEditor
	{
		public ShaderProgramPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{

		}

		protected override void OnPropertySet(System.Reflection.PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_ShaderProgram_Vertex) ||
				ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_ShaderProgram_Fragment))
			{
				this.PerformGetValue();
			}
		}
	}
}
