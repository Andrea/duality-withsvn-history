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
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class TexturePropertyEditor : ResourcePropertyEditor
	{
		public TexturePropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Width)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Height)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Diameter)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_OglWidth)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_OglHeight)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_UVRatio)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_Mipmaps)) return false;
			if (ReflectionHelper.MemberInfoEquals(info, ReflectionHelper.Property_Texture_NeedsReload)) return false;
			return base.MemberPredicate(info);
		}
		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			Texture[] texArr = targets.Cast<Texture>().ToArray();
			foreach (Texture tex in texArr) if (tex.NeedsReload) tex.ReloadData();
		}
	}
}
