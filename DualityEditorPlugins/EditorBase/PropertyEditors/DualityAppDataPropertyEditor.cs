using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

using Duality;

using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class DualityAppDataPropertyEditor : MemberwisePropertyEditor
	{
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			DualityApp.AppData = targets.Cast<DualityAppData>().NotNull().FirstOrDefault();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
		}
	}
	public class DualityUserDataPropertyEditor : MemberwisePropertyEditor
	{
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			DualityApp.UserData = targets.Cast<DualityUserData>().NotNull().FirstOrDefault();
			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(targets), property);
		}
	}
}
