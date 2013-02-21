using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Cloning;
using Duality.Resources;

using DualityEditor.EditorRes;

namespace DualityEditor.UndoRedoActions
{
	public class UndoRedoMacroAction : UndoRedoAction
	{
		private	UndoRedoAction[]	macro	= null;
		private	string				name	= null;

		public override string Name
		{
			get { return this.name; }
		}
		public override bool IsVoid
		{
			get { return this.macro == null || this.macro.Length == 0; }
		}

		public UndoRedoMacroAction(string name, params UndoRedoAction[] macro)
		{
			if (macro == null) throw new ArgumentNullException("macro");
			this.macro = macro.Where(o => o != null && !o.IsVoid).ToArray();

			if (this.macro.Length == 1)
				this.name = this.macro[0].Name;
			else
				this.name = name;
		}

		public override void Do()
		{
			foreach (UndoRedoAction action in this.macro)
				action.Do();
		}
		public override void Undo()
		{
			foreach (UndoRedoAction action in this.macro.Reverse())
				action.Undo();
		}
	}
}
