using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components.Renderers;

namespace EditorBase.PropertyEditors
{
	public class TextRendererPropertyEditor : RendererPropertyEditor
	{
		protected override void OnPropertySet(System.Reflection.PropertyInfo property, IEnumerable<object> targets)
		{
			if (ReflectionHelper.MemberInfoEquals(property, ReflectionInfo.Property_TextRenderer_Text))
			{
				foreach (TextRenderer r in targets.Cast<TextRenderer>().NotNull())
					r.UpdateText();
			}
			base.OnPropertySet(property, targets);
		}
	}
}
