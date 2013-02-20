using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;
using Duality.Cloning;

namespace DualityEditor
{
	public static class UndoRedoManager
	{
		private static List<UndoRedoAction> actionStack = new List<UndoRedoAction>();
		private static int actionIndex = -1;
		private	static int maxActions = 50;


		public static event EventHandler StackChanged = null;


		public static int MaxUndoActions
		{
			get { return maxActions; }
			set { maxActions = value; }
		}
		public static bool CanUndo
		{
			get { return PrevAction != null; }
		}
		public static bool CanRedo
		{
			get { return NextAction != null; }
		}
		public static IUndoRedoActionInfo PrevActionInfo
		{
			get { return PrevAction; }
		}
		public static IUndoRedoActionInfo NextActionInfo
		{
			get { return NextAction; }
		}
		private static UndoRedoAction PrevAction
		{
			get { return actionIndex < actionStack.Count && actionIndex >= 0 ? actionStack[actionIndex] : null; }
		}
		private static UndoRedoAction NextAction
		{
			get { return actionIndex + 1 < actionStack.Count && actionIndex + 1 >= 0 ? actionStack[actionIndex + 1] : null; }
		}

		
		internal static void Init()
		{
			// Register events
			Scene.Leaving += Scene_Changed;
			Scene.Entered += Scene_Changed;
		}
		internal static void Terminate()
		{
			// Unregister events
			Scene.Leaving -= Scene_Changed;
			Scene.Entered -= Scene_Changed;
		}


		public static void Clear()
		{
			actionStack.Clear();
			actionIndex = -1;
			OnStackChanged();
		}
		public static void Do(UndoRedoAction action)
		{
			if (actionStack.Count - actionIndex - 1 > 0)
				actionStack.RemoveRange(actionIndex + 1, actionStack.Count - actionIndex - 1);

			UndoRedoAction prev = PrevAction;
			if (prev != null && prev.CanAppend(action))
			{
				prev.Append(action);
			}
			else
			{
			//	Deactivated Undo/Redo for now
			//	actionStack.Add(action);
			//	actionIndex++;
				action.Do();
			}

			if (actionStack.Count > maxActions)
			{
				actionIndex -= actionStack.Count - maxActions;
				actionStack.RemoveRange(0, actionStack.Count - maxActions);
			}

			OnStackChanged();
		}
		public static void Redo()
		{
			UndoRedoAction action = NextAction;
			if (action == null) return;
			actionIndex++;
			action.Do();
			OnStackChanged();
		}
		public static void Undo()
		{
			UndoRedoAction action = PrevAction;
			if (action == null) return;
			actionIndex--;
			action.Undo();
			OnStackChanged();
		}

		private static void OnStackChanged()
		{
			if (StackChanged != null)
				StackChanged(null, EventArgs.Empty);
		}

		private static void Scene_Changed(object sender, EventArgs e)
		{
			// Maybe reimplement later to only remove Scene-related actions?
			Clear();
		}
	}

	public interface IUndoRedoActionInfo
	{
		string Name { get; }
		HelpInfo Help { get; }
	}

	public abstract class UndoRedoAction : IUndoRedoActionInfo
	{
		protected static readonly CloneProviderContext BackupCloneContext = new CloneProviderContext(false);

		public abstract string Name { get; }
		public virtual HelpInfo Help
		{
			get { return null; }
		}

		public virtual bool CanAppend(UndoRedoAction action)
		{
			return false;
		}
		public virtual void Append(UndoRedoAction action) {}
		public abstract void Do();
		public abstract void Undo();
	}
}
