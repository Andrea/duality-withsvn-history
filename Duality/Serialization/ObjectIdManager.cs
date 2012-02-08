using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	/// <summary>
	/// Manages object IDs during de/serialization.
	/// </summary>
	public class ObjectIdManager
	{
		private	uint	idCounter	= 0;
		private	Dictionary<object,uint>	objRefIdMap	= new Dictionary<object,uint>();
		private	Dictionary<uint,object>	idObjRefMap	= new Dictionary<uint,object>();

		/// <summary>
		/// Clears all object id mappings.
		/// </summary>
		public void Clear()
		{
			this.objRefIdMap.Clear();
			this.idObjRefMap.Clear();
			this.idCounter = 0;
		}
		/// <summary>
		/// Returns the id that is assigned to the specified object. Assigns one, if
		/// there is none yet.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="isNewId"></param>
		/// <returns></returns>
		public uint Request(object obj, out bool isNewId)
		{
			uint id;
			if (this.objRefIdMap.TryGetValue(obj, out id))
			{
				isNewId = false;
				return id;
			}

			id = ++idCounter;
			this.objRefIdMap[obj] = id;
			this.idObjRefMap[id] = obj;

			isNewId = true;
			return id;
		}
		/// <summary>
		/// Assigns an id to a specific object.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="id">The id to assign. Zero ids are rejected.</param>
		public void Inject(object obj, uint id)
		{
			if (id == 0) return;

			if (obj != null) this.objRefIdMap[obj] = id;
			this.idObjRefMap[id] = obj;
		}
		/// <summary>
		/// Tries to lookup an object based on its id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Lookup(uint id, out object obj)
		{
			return this.idObjRefMap.TryGetValue(id, out obj);
		}
	}
}
