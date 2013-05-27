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
		private	List<uint>					idCounter		= new List<uint> { 0 };
		private	int							idStackHash		= 0;
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
			this.idCounter.Add(0);
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

			id = this.idCounter[this.idLevel];
			unchecked
			{
				const uint p = 16777619;
				uint idLevelHash = (uint)this.idStackHash;

				while (this.idObjRefMap.ContainsKey(id))
				{
					id = (id ^ idLevelHash) * p;
				}
			}
			this.idCounter[this.idLevel] = id;

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

		/// <summary>
		/// Increases the reference hierarchy level of the object id generator. Each level of id generation uses its own algorithm, so different levels of ids are unlikely to affect each other.
		/// </summary>
		public void PushIdLevel()
		{
			this.idCounter.Add(0);
			this.idLevel++;
			this.idStackHash = this.idCounter.GetCombinedHashCode(0, this.idLevel);
		}
		/// <summary>
		/// Decreases the reference hierarchy level of the object id generator. Each level of id generation uses its own algorithm, so different levels of ids are unlikely to affect each other.
		/// </summary>
		public void PopIdLevel()
		{
			if (this.idLevel == 0) throw new InvalidOperationException("Can't pop persistent id level, because it is already zero / root");
			this.idLevel--;
			this.idStackHash = this.idCounter.GetCombinedHashCode(0, this.idLevel);
		}
	}
}
