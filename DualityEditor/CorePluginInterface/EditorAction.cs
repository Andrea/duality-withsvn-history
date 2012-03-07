using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DualityEditor.CorePluginInterface
{
	public interface IEditorAction
	{
		string Name { get; }
		Image Icon { get; }

		void Perform(object obj);
		void Perform(IEnumerable<object> obj);
		bool CanPerformOn(IEnumerable<object> obj);
	}
	public abstract class EditorActionBase<T> : IEditorAction
	{
		private	string		name;
		private	Image		icon;

		public string Name
		{
			get { return this.name; }
		}
		public Image Icon
		{
			get { return this.icon; }
		}

		public EditorActionBase(string name, Image icon)
		{
			this.name = name;
			this.icon = icon;
		}

		public void Perform(T obj)
		{
			this.Perform(new T[] { obj });
		}
		public abstract void Perform(IEnumerable<T> objEnum);
		public abstract bool CanPerformOn(IEnumerable<T> objEnum);

		void IEditorAction.Perform(object obj)
		{
			this.Perform((T)obj);
		}
		void IEditorAction.Perform(IEnumerable<object> obj)
		{
			this.Perform(obj.Cast<T>());
		}
		bool IEditorAction.CanPerformOn(IEnumerable<object> obj)
		{
			if (obj == null) return true;
			return this.CanPerformOn(obj.Cast<T>());
		}
	}
	public class EditorAction<T> : EditorActionBase<T>
	{
		private	Action<T>	action;

		public EditorAction(string name, Image icon, Action<T> action) : base(name, icon)
		{
			this.action = action;
		}

		public override void Perform(IEnumerable<T> objEnum)
		{
			foreach (T obj in objEnum) this.action(obj);
		}
		public override bool CanPerformOn(IEnumerable<T> objEnum)
		{
			return true;
		}
	}
	public class EditorGroupAction<T> : EditorActionBase<T>
	{
		private	Action<IEnumerable<T>>		action;
		private	Predicate<IEnumerable<T>>	actionPredicate;

		public EditorGroupAction(string name, Image icon, Action<IEnumerable<T>> action, Predicate<IEnumerable<T>> predicate) : base(name, icon)
		{
			this.action = action;
			this.actionPredicate = predicate;
		}

		public override void Perform(IEnumerable<T> objEnum)
		{
			this.action(objEnum);
		}
		public override bool CanPerformOn(IEnumerable<T> obj)
		{
			if (obj == null) return true;
			if (this.actionPredicate == null) return true;
			return this.actionPredicate(obj);
		}
	}
}
