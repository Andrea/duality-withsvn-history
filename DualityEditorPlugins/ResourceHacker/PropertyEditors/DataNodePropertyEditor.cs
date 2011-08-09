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
	public class DataNodePropertyEditor : MemberwisePropertyEditor
	{
		public DataNodePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, typeof(DataNode).GetProperty("Parent"))) return false;
			if (ReflectionHelper.MemberInfoEquals(info, typeof(DataNode).GetProperty("SubNodes"))) return false;
			if (ReflectionHelper.MemberInfoEquals(info, typeof(DataNode).GetProperty("NodeType"))) return false;
			return base.MemberPredicate(info);
		}
	}
}
