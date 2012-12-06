﻿using System;

namespace Duality
{
	/// <summary>
	/// Provides event arguments related to <see cref="Duality.CorePlugin"/> instances.
	/// </summary>
	public class CorePluginEventArgs : EventArgs
	{
		private	CorePlugin	plugin;
		public CorePlugin Plugin
		{
			get { return this.plugin; }
		}
		public CorePluginEventArgs(CorePlugin plugin)
		{
			this.plugin = plugin;
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
	/// Provides event arguments for <see cref="Duality.GameObject"/>-related events.
	/// </summary>
	public class GameObjectEventArgs : EventArgs
	{
		private	GameObject	obj;
		/// <summary>
		/// [GET] The affected GameObject.
		/// </summary>
		public GameObject Object
		{
			get { return this.obj; }
		}
		public GameObjectEventArgs(GameObject obj)
		{
			this.obj = obj;
		}
	}

	/// <summary>
	/// Provides event arguments for a <see cref="GameObject">GameObjects</see> "<see cref="GameObject.Parent"/> changed" events.
	/// </summary>
	public class GameObjectParentChangedEventArgs : GameObjectEventArgs
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

		public GameObjectParentChangedEventArgs(GameObject obj, GameObject oldParent, GameObject newParent) : base(obj)
		{
			this.oldParent = oldParent;
			this.newParent = newParent;
		}
	}

	public class ResourceEventArgs : EventArgs
	{
		private ContentRef<Resource>	content;
		private	bool					isDirectory;
		private	bool					isResource;

		public string Path
		{
			get { return this.content.Path; }
		}
		public bool IsDirectory
		{
			get { return this.isDirectory; }
		}
		public bool IsResource
		{
			get { return this.isResource; }
		}
		public bool IsDefaultContent
		{
			get { return this.content.IsDefaultContent; }
		}
		public Type ContentType
		{
			get 
			{
				if (isDirectory) return null;
				else return this.content.ResType;
			}
		}
		public ContentRef<Resource> Content
		{
			get { return this.isDirectory ? ContentRef<Resource>.Null : this.content; }
		}

		public ResourceEventArgs(string path) : this(new ContentRef<Resource>(null, path)) {}
		public ResourceEventArgs(IContentRef resRef) : this(new ContentRef<Resource>(resRef.ResWeak, resRef.Path)) {}
		public ResourceEventArgs(ContentRef<Resource> resRef)
		{
			this.content = resRef;
			this.isDirectory = System.IO.Directory.Exists(this.content.Path);
			this.isResource = System.IO.File.Exists(this.content.Path);
			if (!this.isDirectory && !this.isResource)
			{
				this.isDirectory = string.IsNullOrEmpty(System.IO.Path.GetExtension(this.content.Path));
				this.isResource = !this.isDirectory;
			}
		}
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

	public class TransformChangedEventArgs : ComponentEventArgs
	{
		private	Components.Transform.DirtyFlags	dirtyFlags;
		/// <summary>
		/// [GET] The changes that have been made since the last update.
		/// </summary>
		public Components.Transform.DirtyFlags Changes
		{
			get { return this.dirtyFlags; }
		}
		public TransformChangedEventArgs(Component transform, Components.Transform.DirtyFlags dirtyFlags) : base(transform)
		{
			this.dirtyFlags = dirtyFlags;
		}
	}

	public class CollectDrawcallEventArgs : EventArgs
	{
		private	IDrawDevice device;
		public IDrawDevice Device
		{
			get { return this.device; }
		}

		public CollectDrawcallEventArgs(IDrawDevice device)
		{
			this.device = device;
		}
	}
}
