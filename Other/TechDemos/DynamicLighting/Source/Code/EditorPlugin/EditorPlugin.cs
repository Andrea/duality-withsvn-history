using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DualityEditor;

namespace DynamicLighting
{
	/// <summary>
	/// Defines a Duality editor plugin.
	/// </summary>
    public class DynamicLightingEditorPlugin : EditorPlugin
	{
		public override string Id
		{
			get { return "DynamicLightingEditorPlugin"; }
		}
	}
}
