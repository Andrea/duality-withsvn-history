using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Duality.Serialization
{
	/// <summary>
	/// Manages object IDs during de/serialization.
	/// </summary>
	public class ObjectIdManager
	{
		private	int							idLevel			= 0;
		private	List<Dictionary<Type,uint>>	idCounter		= new List<Dictionary<Type,uint>> { new Dictionary<Type,uint>() };
		private	Dictionary<object,uint>		objRefIdMap		= new Dictionary<object,uint>();
		private	Dictionary<uint,object>		idObjRefMap		= new Dictionary<uint,object>();
		private	Dictionary<Type,uint>		typeHashCache	= new Dictionary<Type,uint>();

		/// <summary>
		/// Clears all object id mappings.
		/// </summary>
		public void Clear()
		{
			this.typeHashCache.Clear();
			this.objRefIdMap.Clear();
			this.idObjRefMap.Clear();
			this.idCounter.Clear();
			this.idCounter.Add(new Dictionary<Type,uint>());
			this.idLevel = 0;
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

			Type objType = obj != null ? obj.GetType() : typeof(object);
			Dictionary<Type,uint> typeCounter = this.idCounter[this.idLevel];
			if (!typeCounter.TryGetValue(objType, out id))
				id = 0;
			unchecked
			{
				const uint p = 16777619;
				uint typeHash;
				if (!this.typeHashCache.TryGetValue(objType, out typeHash))
				{
					typeHash = (uint)objType.GetTypeId().GetHashCode();
					this.typeHashCache[objType] = typeHash;
				}
				uint idLevelHash = (uint)this.idLevel.GetHashCode() ^ typeHash;

				while (this.idObjRefMap.ContainsKey(id))
				{
					id = (id ^ idLevelHash) * p;
				}
			}
			typeCounter[objType] = id;

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

		public void PushIdLevel()
		{
			this.idCounter.Add(new Dictionary<Type,uint>());
			this.idLevel++;
		}
		public void PopIdLevel()
		{
			if (this.idLevel == 0) throw new InvalidOperationException("Can't pop persistent id level, because it is already zero / root");
			this.idLevel--;
		}
	}
}
