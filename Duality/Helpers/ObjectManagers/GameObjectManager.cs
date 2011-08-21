using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality.ObjectManagers
{
	/// <summary>
	/// Manages a set of <see cref="GameObject">GameObject</see> and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	[Serializable]
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
		/// Fired when a <see cref="Duality.Component"/> is added to an already registered GameObject.
		/// </summary>
		public event EventHandler<ComponentEventArgs> RegisteredObjectComponentAdded;
		/// <summary>
		/// Fired when a <see cref="Duality.Component"/> is removed from an already registered GameObject.
		/// </summary>
		public event EventHandler<ComponentEventArgs> RegisteredObjectComponentRemoved;
		
		/// <summary>
		/// Registers a GameObject and all of its children.
		/// </summary>
		/// <param name="obj"></param>
		public void RegisterObjDeep(GameObject obj)
		{
			this.RegisterObj(obj);
			foreach (GameObject child in obj.Children)
			{
				this.RegisterObjDeep(child);
			}
		}
		/// <summary>
		/// Registers a set of GameObjects and all of their children
		/// </summary>
		/// <param name="obj"></param>
		public void RegisterObjDeep(IEnumerable<GameObject> obj)
		{
			foreach (GameObject o in obj.ToArray()) this.RegisterObjDeep(o);
		}
		/// <summary>
		/// Unregisters a GameObject and all of its children
		/// </summary>
		/// <param name="obj"></param>
		public void UnregisterObjDeep(GameObject obj)
		{
			foreach (GameObject child in obj.Children)
			{
				this.UnregisterObjDeep(child);
			}
			this.UnregisterObj(obj);
		}
		/// <summary>
		/// Unregisters a set of GameObjects and all of their children
		/// </summary>
		/// <param name="obj"></param>
		public void UnregisterObjDeep(IEnumerable<GameObject> obj)
		{
			foreach (GameObject o in obj.ToArray()) this.UnregisterObjDeep(o);
		}

		protected override void OnRegistered(GameObject obj)
		{
			base.OnRegistered(obj);
			obj.EventComponentAdded		+= this.OnRegisteredObjectComponentAdded;
			obj.EventComponentRemoving	+= this.OnRegisteredObjectComponentRemoved;
		}
		protected override void OnUnregistered(GameObject obj)
		{
			base.OnUnregistered(obj);
			obj.EventComponentAdded		-= this.OnRegisteredObjectComponentAdded;
			obj.EventComponentRemoving	-= this.OnRegisteredObjectComponentRemoved;
		}

		private void OnRegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			if (this.RegisteredObjectComponentAdded != null)
				this.RegisteredObjectComponentAdded(sender, e);
		}
		private void OnRegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (this.RegisteredObjectComponentRemoved != null)
				this.RegisteredObjectComponentRemoved(sender, e);
		}
	}
}
