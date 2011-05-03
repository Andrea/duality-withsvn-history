using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;

namespace DualityEditor
{
	public class ObjectSelection : IEquatable<ObjectSelection>
	{
		[Flags]
		public enum Category
		{
			None		= 0x0,

			Other		= 0x1,
			GameObject	= 0x2,
			Component	= 0x4,
			Resource	= 0x8,

			All			= Other | GameObject | Component | Resource
		}
		public static Category GetObjCategory(object obj)
		{
			if (obj is GameObject) return Category.GameObject;
			else if (obj is Component) return Category.Component;
			else if (obj is Resource) return Category.Resource;
			else return Category.Other;
		}

		public static readonly ObjectSelection Null	= new ObjectSelection();

		private	List<object>	obj	= null;

		public object MainObject
		{
			get { return this.obj.Count == 0 ? null : this.obj[0]; }
		}
		public GameObject MainGameObject
		{
			get { return this.obj.Count == 0 ? null : obj[0] as GameObject; }
		}
		public Component MainComponent
		{
			get { return this.obj.Count == 0 ? null : obj[0] as Component; }
		}
		public Resource MainResource
		{
			get { return this.obj.Count == 0 ? null : obj[0] as Resource; }
		}
		public object MainOtherObject
		{
			get { return this.OtherObjects.First(); }
		}

		public IEnumerable<object> Objects
		{
			get { return this.obj; }
		}
		public IEnumerable<GameObject> GameObjects
		{
			get
			{
				return from o in this.obj
					   where o is GameObject
					   select o as GameObject;
			}
		}
		public IEnumerable<Component> Components
		{
			get
			{
				return from o in this.obj
					   where o is Component
					   select o as Component;
			}
		}
		public IEnumerable<Resource> Resources
		{
			get
			{
				return from o in this.obj
					   where o is Resource
					   select o as Resource;
			}
		}
		public IEnumerable<object> OtherObjects
		{
			get { return this.obj.Where(o => !(o is GameObject) && !(o is Component) && !(o is Resource)); }
		}

		public int ObjectCount
		{
			get { return this.obj.Count; }
		}
		public int GameObjectCount
		{
			get { return this.obj.Count(o => o is GameObject); }
		}
		public int ComponentCount
		{
			get { return this.obj.Count(o => o is Component); }
		}
		public int ResourceCount
		{
			get { return this.obj.Count(o => o is Resource); }
		}
		public int OtherObjectCount
		{
			get { return this.OtherObjects.Count(); }
		}

		public ObjectSelection()
		{
			this.obj = new List<object>();
		}
		public ObjectSelection(ObjectSelection other)
		{
			this.obj = new List<object>(other.obj);
		}
		public ObjectSelection(IEnumerable<object> obj)
		{
			this.obj = new List<object>(obj.Where(o => o != null));
		}
		public ObjectSelection(params object[] obj) : this(obj as IEnumerable<object>) {}

		protected void LocalClear()
		{
			this.obj.Clear();
		}
		protected void LocalClear(Category clearCat)
		{
			this.obj.RemoveAll(o => (GetObjCategory(o) & clearCat) != Category.None);	
		}
		protected void LocalTransform(ObjectSelection target)
		{
			// Group source objects by their selection category
			var typedQuerySrc = 
				from o in this.obj
				group o by GetObjCategory(o) into g
				select new { ObjType = g.Key, Obj = g };

			// Iterate through currently existent categories
			Category clearCat = Category.None;
			foreach (var categoryGroup in typedQuerySrc)
			{
				// If any object of a currently available category is present in the target: Deselect current category
				if (target.obj.Any(o => GetObjCategory(o) == categoryGroup.ObjType))
					clearCat |= categoryGroup.ObjType;				
			}
			this.LocalClear(clearCat);

			// Append new selection
			this.LocalAppend(target);
		}
		protected void LocalAppend(ObjectSelection other)
		{
			this.obj = new List<object>(this.obj.Union(other.obj));
		}
		protected void LocalRemove(ObjectSelection other)
		{
			this.obj = new List<object>(this.obj.Except(other.obj));
		}
		protected void LocalToggle(ObjectSelection other)
		{
			var common = this.obj.Intersect(other.obj);
			var added = other.obj.Except(this.obj);
			this.obj = new List<object>(this.obj.Except(common).Union(added));
		}
		protected void LocalHierarchyExpand()
		{
			var gameobjQuery = this.GameObjects.Concat(this.GameObjects.ChildrenDeep());
			var componentQuery = this.GameObjects.GetComponentsDeep<Component>();
			this.obj = new List<object>(gameobjQuery.AsEnumerable<object>().Concat(componentQuery.AsEnumerable<object>()).Distinct());
		}

		public ObjectSelection Clear(Category clearCat)
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalClear(clearCat);
			return result;
		}
		public ObjectSelection Transform(ObjectSelection target)
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalTransform(target);
			return result;
		}
		public ObjectSelection Append(ObjectSelection other)
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalAppend(other);
			return result;
		}
		public ObjectSelection Remove(ObjectSelection other)
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalRemove(other);
			return result;
		}
		public ObjectSelection Toggle(ObjectSelection other)
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalToggle(other);
			return result;
		}
		public ObjectSelection HierarchyExpand()
		{
			ObjectSelection result = new ObjectSelection(this);
			result.LocalHierarchyExpand();
			return result;
		}

		public override int GetHashCode()
		{
			int h = 0;
			for (int i = 0; i < this.obj.Count; i++) h = h ^ (this.obj[i] != null ? this.obj[i].GetHashCode() : 0);
			return h;
		}
		public override bool Equals(object obj)
		{
			if (obj is ObjectSelection) return this == (obj as ObjectSelection);

			return base.Equals(obj);
		}
		public bool Equals(ObjectSelection other)
		{
			return this == other;
		}

		public static Category GetCategoriesInSelection(ObjectSelection sel)
		{
			if (sel == null) return Category.None;

			Category catAvail = Category.None;
			for (int i = 0, catId = 0; (catId = (1 << i)) < (int)Category.All; i++)
			{
				Category curCat = (Category)catId;
				if (sel.obj.Any(o => GetObjCategory(o) == curCat))
				{
					catAvail |= curCat;
				}
			}
			return catAvail;
		}
		public static Category GetAffectedCategories(ObjectSelection first, ObjectSelection second)
		{
			if (first == null) return GetCategoriesInSelection(second);
			if (second == null) return GetCategoriesInSelection(first);

			Category catDiff = Category.None;
			for (int i = 0, catId = 0; (catId = (1 << i)) < (int)Category.All; i++)
			{
				Category curCat = (Category)catId;
				var firstCatQuery = first.obj.Where(o => GetObjCategory(o) == curCat);
				var secondCatQuery = second.obj.Where(o => GetObjCategory(o) == curCat);

				int firstCount = firstCatQuery.Count();
				int secondCount = secondCatQuery.Count();
				if (firstCount != secondCount)
				{
					catDiff |= curCat;
					continue;
				}

				var unionQuery = firstCatQuery.Union(secondCatQuery);
				if (unionQuery.Count() != firstCount) catDiff |= curCat;
			}
			return catDiff;
		}

		public static bool operator ==(ObjectSelection first, ObjectSelection second)
		{
			if (object.ReferenceEquals(first, null) && object.ReferenceEquals(second, null)) return true;
			if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null)) return false;

			if (first.obj.Count != second.obj.Count) return false;
			if (first.obj.Any(o => !second.obj.Contains(o))) return false;

			return true;
		}
		public static bool operator !=(ObjectSelection first, ObjectSelection second)
		{
			return !(first == second);
		}

		public static ObjectSelection operator -(ObjectSelection first, ObjectSelection second)
		{
			return new ObjectSelection(first.obj.Except(second.obj));
		}
		public static ObjectSelection operator +(ObjectSelection first, ObjectSelection second)
		{
			return new ObjectSelection(first.obj.Union(second.obj));
		}
	}
}
