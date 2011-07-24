using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Duality
{
	public interface IManageableObject
	{
		bool Disposed { get; }
		bool Active { get; }

		void Dispose();
	}

	[Serializable]
	public class ObjectManager<T> where T : IManageableObject
	{
		protected	List<T>	allObj	= new List<T>();

		public event EventHandler<ObjectManagerEventArgs<T>> Registered;
		public event EventHandler<ObjectManagerEventArgs<T>> Unregistered;

		public IEnumerable<T> AllObjects
		{
			get 
			{
				for (int i = this.allObj.Count - 1; i >= 0; i--)
					if (this.allObj[i].Disposed) this.UnregisterObj(this.allObj[i]);
				return this.allObj;
			}
		}
		public IEnumerable<T> ActiveObjects
		{
			get 
			{
				return this.AllObjects.Where(o => o.Active);
			}
		}

		public ObjectManager()
		{

		}

		public void Clear()
		{
			this.allObj.ForEach(this.OnUnregistered);
			this.allObj.Clear();
		}
		public void RegisterObj(T obj)
		{
			if (this.allObj.Contains(obj)) return;
			this.allObj.Add(obj);
			this.OnRegistered(obj);
		}
		public void UnregisterObj(T obj)
		{
			this.allObj.Remove(obj);
			this.OnUnregistered(obj);
		}

		protected virtual void OnRegistered(T obj) 
		{
			if (this.Registered != null)
				this.Registered(this, new ObjectManagerEventArgs<T>(obj));
		}
		protected virtual void OnUnregistered(T obj)
		{
			if (this.Unregistered != null)
				this.Unregistered(this, new ObjectManagerEventArgs<T>(obj));
		}
	}
}
