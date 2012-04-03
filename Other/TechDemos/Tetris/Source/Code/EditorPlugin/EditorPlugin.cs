using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DualityEditor;

namespace Tetris
{
	/// <summary>
	/// Defines a Duality editor plugin.
	/// </summary>
    public class TetrisEditorPlugin : EditorPlugin
	{
		public override string Id
		{
			get { return "TetrisEditorPlugin"; }
		}
	}
}
