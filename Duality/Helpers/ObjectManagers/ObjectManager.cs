using System;
using System.Collections.Generic;
using System.Linq;

using Duality.Serialization;

namespace Duality
{
	/// <summary>
	/// Represents an object that can be managed by an <see cref="ObjectManager{T}"/>.
	/// </summary>
	public interface IManageableObject
	{
		/// <summary>
		/// [GET] Returns whether the object is considered disposed.
		/// </summary>
		bool Disposed { get; }
		/// <summary>
		/// [GET] Returns whether the object is currently active.
		/// </summary>
		bool Active { get; }

		/// <summary>
		/// Disposes the object.
		/// </summary>
		void Dispose();
	}

	/// <summary>
	/// Manages a set of objects and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	/// <typeparam name="T">The object type that is managed here.</typeparam>
	public class ObjectManager<T> where T : IManageableObject
	{
		protected	List<T>	allObj	= new List<T>();

		/// <summary>
		/// [GET] Enumerates all registered objects.
		/// </summary>
		public IEnumerable<T> AllObjects
		{
			get 
			{
				for (int i = this.allObj.Count - 1; i >= 0; i--)
					if (this.allObj[i].Disposed) this.UnregisterObj(this.allObj[i]);
				return this.allObj;
			}
		}
		/// <summary>
		/// [GET] Enumerates all registered objects that are currently active.
		/// </summary>
		public IEnumerable<T> ActiveObjects
		{
			get 
			{
				return this.AllObjects.Where(o => o.Active);
			}
		}

		/// <summary>
		/// Unregisters all objects.
		/// </summary>
		public virtual void Clear()
		{
			this.allObj.Clear();
		}
		/// <summary>
		/// Registers a single object
		/// </summary>
		/// <param name="obj"></param>
		public virtual bool RegisterObj(T obj)
		{
			if (this.allObj.Contains(obj)) return false;
			this.allObj.Add(obj);
			return true;
		}
		/// <summary>
		/// Registers a set of objects
		/// </summary>
		/// <param name="objEnum"></param>
		public void RegisterObj(IEnumerable<T> objEnum)
		{
			foreach (T obj in objEnum.ToArray()) this.RegisterObj(obj);
		}
		/// <summary>
		/// Unregisters a single object
		/// </summary>
		/// <param name="obj"></param>
		public virtual bool UnregisterObj(T obj)
		{
			return this.allObj.Remove(obj);
		}
		/// <summary>
		/// Unregisters a set of objects
		/// </summary>
		/// <param name="objEnum"></param>
		public void UnregisterObj(IEnumerable<T> objEnum)
		{
			foreach (T obj in objEnum.ToArray()) this.UnregisterObj(obj);
		}
	}
}
