using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	/// <summary>
	/// Provides event arguments regarding the objects registered in an <see cref="ObjectManager{T}"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectManagerEventArgs<T> : EventArgs where T : IManageableObject
	{
		private	T	obj;
		/// <summary>
		/// [GET] The managed object affected by the event.
		/// </summary>
		public T Object
		{
			get { return this.obj; }
		}
		public ObjectManagerEventArgs(T obj)
		{
			this.obj = obj;
		}
	}

	/// <summary>
	/// Provides event arguments for <see cref="Duality.Component"/>-related events.
	/// </summary>
	public class ComponentEventArgs : EventArgs
	{
		private	Component	cmp;
		/// <summary>
		/// [GET] The affected Component.
		/// </summary>
		public Component Component
		{
			get { return this.cmp; }
		}
		public ComponentEventArgs(Component cmp)
		{
			this.cmp = cmp;
		}
	}

	/// <summary>
	/// Provides event arguments for a <see cref="GameObject">GameObjects</see> "<see cref="GameObject.Parent"/> changed" events.
	/// </summary>
	public class GameObjectParentChangedEventArgs : EventArgs
	{
		private	GameObject	oldParent;
		private	GameObject	newParent;

		/// <summary>
		/// [GET] The GameObjects old parent.
		/// </summary>
		public GameObject OldParent
		{
			get { return this.oldParent; }
		}
		/// <summary>
		/// [GET] The GameObjects new parent.
		/// </summary>
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

	public class ResourceEventArgs : EventArgs
	{
		private string		path;
		private	bool		isDirectory;

		public string Path
		{
			get { return this.path; }
		}
		public bool IsDirectory
		{
			get { return this.isDirectory; }
		}
		public bool IsResource
		{
			get { return !this.isDirectory; }
		}
		public Type ContentType
		{
			get 
			{
				if (isDirectory) return null;
				else return Resource.GetTypeByFileName(this.path);
			}
		}
		public ContentRef<Resource> Content
		{
			get { return this.isDirectory ? ContentRef<Resource>.Null : new ContentRef<Resource>(null, this.path); }
		}

		public ResourceEventArgs(string path)
		{
			this.path = path;

			this.isDirectory = System.IO.Directory.Exists(this.path);
		}
		public ResourceEventArgs(ContentRef<Resource> resRef) : this(resRef.Path) {}
	}

	public class CollisionEventArgs : EventArgs
	{
		private	GameObject		colWith;
		private	CollisionData	colData;

		public GameObject CollideWith
		{
			get { return this.colWith; }
		}
		public CollisionData CollisionData
		{
			get { return this.colData; }
		}

		public CollisionEventArgs(GameObject obj, CollisionData data)
		{
			this.colWith = obj;
			this.colData = data;
		}
	}
}
