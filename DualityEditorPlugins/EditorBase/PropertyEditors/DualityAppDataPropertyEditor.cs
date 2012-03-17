using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AdamsLair.PropertyGrid;

using Duality;
using DualityEditor;

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
