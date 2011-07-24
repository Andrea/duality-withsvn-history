using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	public class ObjectManagerEventArgs<T> : EventArgs where T : IManageableObject
	{
		private	T	obj;
		public T Object
		{
			get { return this.obj; }
		}
		public ObjectManagerEventArgs(T obj)
		{
			this.obj = obj;
		}
	}

	public class ComponentEventArgs : EventArgs
	{
		private	Component	cmp;
		public Component Component
		{
			get { return this.cmp; }
		}
		public ComponentEventArgs(Component cmp)
		{
			this.cmp = cmp;
		}
	}

	public class GameObjectParentChangedEventArgs : EventArgs
	{
		private	GameObject	oldParent;
		private	GameObject	newParent;

		public GameObject OldParent
		{
			get { return this.oldParent; }
		}
		public GameObject NewParent
		{
			get { return this.newParent; }
		}

		public GameObjectParentChangedEventArgs(GameObject oldParent, GameObject newParent)
		{
			this.oldParent = oldParent;
			this.newParent = newParent;
		}
	}
}
