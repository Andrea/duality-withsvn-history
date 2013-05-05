using System;
using System.Collections.Generic;
using System.Linq;

namespace Duality.ObjectManagers
{
	/// <summary>
	/// Manages a set of <see cref="GameObject">GameObject</see> and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	public class GameObjectManager : ObjectManager<GameObject>
	{
		/// <summary>
		/// [GET] Enumerates all root GameObjects, i.e. all GameObjects without a parent object.
		/// </summary>
		public IEnumerable<GameObject> RootObjects
		{
			get
			{
				return this.AllObjects.Where(o => o.Parent == null);
			}
		}
		/// <summary>
		/// [GET] Enumerates all <see cref="RootObjects"/> that are currently active.
		/// </summary>
		public IEnumerable<GameObject> ActiveRootObjects
		{
			get
			{
				return this.ActiveObjects.Where(o => o.Parent == null);
			}
		}
		
		/// <summary>
		/// Fired when a GameObject is registered
		/// </summary>
		public event EventHandler<GameObjectEventArgs>	Registered;
		/// <summary>
		/// Fired when a GameObject is unregistered
		/// </summary>
		public event EventHandler<GameObjectEventArgs>	Unregistered;
		/// <summary>
		/// Fired when a registered GameObjects parent has changed
		/// </summary>
		public event EventHandler<GameObjectParentChangedEventArgs>	ParentChanged;
		/// <summary>
		/// Fired when a <see cref="Duality.Component"/> is added to an already registered GameObject.
		/// </summary>
		public event EventHandler<ComponentEventArgs> ComponentAdded;
		/// <summary>
		/// Fired when a <see cref="Duality.Component"/> is removed from an already registered GameObject.
		/// </summary>
		public event EventHandler<ComponentEventArgs> ComponentRemoving;
		
		/// <summary>
		/// Registers a GameObject and all of its children.
		/// </summary>
		/// <param name="obj"></param>
		public override bool RegisterObj(GameObject obj)
		{
			bool added = base.RegisterObj(obj);
			if (added) this.OnRegistered(obj);
			foreach (GameObject child in obj.Children)
			{
				this.RegisterObj(child);
			}
			return added;
		}
		/// <summary>
		/// Unregisters a GameObject and all of its children
		/// </summary>
		/// <param name="obj"></param>
		public override bool UnregisterObj(GameObject obj)
		{
			foreach (GameObject child in obj.Children)
			{
				this.UnregisterObj(child);
			}
			bool removed = base.UnregisterObj(obj);
			if (removed) this.OnUnregistered(obj);
			return removed;
		}
		/// <summary>
		/// Unregisters all GameObjects.
		/// </summary>
		public override void Clear()
		{
			foreach (GameObject obj in this.allObj)
				this.OnUnregistered(obj);
			base.Clear();
		}
		
		private void RegisterEvents(GameObject obj)
		{
			obj.EventParentChanged		+= this.OnParentChanged;
			obj.EventComponentAdded		+= this.OnComponentAdded;
			obj.EventComponentRemoving	+= this.OnComponentRemoving;
		}
		private void UnregisterEvents(GameObject obj)
		{
			obj.EventParentChanged		-= this.OnParentChanged;
			obj.EventComponentAdded		-= this.OnComponentAdded;
			obj.EventComponentRemoving	-= this.OnComponentRemoving;
		}

		private void OnRegistered(GameObject obj)
		{
			this.RegisterEvents(obj);
			if (this.Registered != null)
				this.Registered(this, new GameObjectEventArgs(obj));
		}
		private void OnUnregistered(GameObject obj)
		{
			this.UnregisterEvents(obj);
			if (this.Unregistered != null)
				this.Unregistered(this, new GameObjectEventArgs(obj));
		}
		private void OnParentChanged(object sender, GameObjectParentChangedEventArgs e)
		{
			if (this.ParentChanged != null)
				this.ParentChanged(sender, e);
		}
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			if (this.ComponentAdded != null)
				this.ComponentAdded(sender, e);
		}
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (this.ComponentRemoving != null)
				this.ComponentRemoving(sender, e);
		}
	}
}
