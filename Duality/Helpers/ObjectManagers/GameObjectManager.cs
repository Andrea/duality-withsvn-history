using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality.ObjectManagers
{
	[Serializable]
	public class GameObjectManager : ObjectManager<GameObject>
	{
		public IEnumerable<GameObject> RootObjects
		{
			get
			{
				return this.AllObjects.Where(o => o.Parent == null);
			}
		}
		public IEnumerable<GameObject> ActiveRootObjects
		{
			get
			{
				return this.ActiveObjects.Where(o => o.Parent == null);
			}
		}

		public event EventHandler<ComponentEventArgs> RegisteredObjectComponentAdded;
		public event EventHandler<ComponentEventArgs> RegisteredObjectComponentRemoved;
		
		public void RegisterObjDeep(GameObject obj)
		{
			this.RegisterObj(obj);
			foreach (GameObject child in obj.Children)
			{
				this.RegisterObjDeep(child);
			}
		}
		public void RegisterObjDeep(IEnumerable<GameObject> obj)
		{
			foreach (GameObject o in obj.ToArray()) this.RegisterObjDeep(o);
		}
		public void UnregisterObjDeep(GameObject obj)
		{
			foreach (GameObject child in obj.Children)
			{
				this.UnregisterObjDeep(child);
			}
			this.UnregisterObj(obj);
		}
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
