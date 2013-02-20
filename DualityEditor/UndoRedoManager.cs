using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Cloning;

namespace DualityEditor
{
	public static class UndoRedoManager
	{
		private static List<UndoRedoAction> actionStack = new List<UndoRedoAction>();
		private static int actionIndex = -1;


		public static event EventHandler StackChanged = null;

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
				action.Do();
				actionStack.Add(action);
				actionIndex++;
			}
			OnStackChanged();
		}
		public static void Do<T>(string name, T affectedResource, Action<T> action) where T : Resource
		{
			Do(new GenericUndoRedoAction<T>(name, affectedResource, action));
		}
		public static void Redo()
		{
			UndoRedoAction action = NextAction;
			if (action == null) return;
			action.Do();
			actionIndex++;
			OnStackChanged();
		}
		public static void Undo()
		{
			UndoRedoAction action = PrevAction;
			if (action == null) return;
			action.Undo();
			actionIndex--;
			OnStackChanged();
		}

		private static void OnStackChanged()
		{
			if (StackChanged != null)
				StackChanged(null, EventArgs.Empty);
		}
	}

	public interface IUndoRedoActionInfo
	{
		string Name { get; }
		HelpInfo Help { get; }
	}

	public abstract class UndoRedoAction : IUndoRedoActionInfo
	{
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

	public class GenericUndoRedoAction<T> : UndoRedoAction where T : Resource
	{
		private static readonly CloneProviderContext CloneContext = new CloneProviderContext(false);

		private	ContentRef<T>	affectedResource;
		private	Resource		resUndoBackup;
		private	Resource		resRedoBackup;
		private	Action<T>		action;
		private	string			name;


		public override string Name
		{
			get { return this.name; }
		}


		public GenericUndoRedoAction(string name, T affectedResource, Action<T> action)
		{
			if (name == null)				throw new ArgumentNullException("name");
			if (affectedResource == null)	throw new ArgumentNullException("affectedResource");
			if (affectedResource.Disposed)	throw new ObjectDisposedException(affectedResource.FullName);
			if (action == null)				throw new ArgumentNullException("action");

			this.name = name;
			this.affectedResource = affectedResource;
			this.action = action;
		}

		public override void Do()
		{
			T res = this.affectedResource.Res;

			if (this.resRedoBackup == null)
			{
				if (this.resUndoBackup == null)
					this.resUndoBackup = CloneProvider.DeepClone(res, CloneContext);
				this.action(res);
			}
			else
			{
				CloneProvider.DeepCopyTo(this.resRedoBackup, res, CloneContext);
				DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(res));
			}
		}
		public override void Undo()
		{
			if (this.resUndoBackup == null) throw new InvalidOperationException("Can't undo an action that hasn't been done yet");

			T res = this.affectedResource.Res;

			if (this.resRedoBackup == null)
				this.resRedoBackup = Duality.Cloning.CloneProvider.DeepClone(res, CloneContext);
			CloneProvider.DeepCopyTo(this.resUndoBackup, res, CloneContext);

			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(res));
		}
	}
}
