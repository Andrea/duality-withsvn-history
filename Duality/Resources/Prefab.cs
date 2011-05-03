using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace Duality.Resources
{
	/// <summary>
	/// Indicates that a field will be assumed null when serializing it as part of a prefab serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class NonSerializedPrefabAttribute : Attribute
	{
		private	object	defaultVal	= null;

		public object DefaultValue
		{
			get { return defaultVal; }
		}

		public NonSerializedPrefabAttribute() {}
		public NonSerializedPrefabAttribute(object defaultVal)
		{
			this.defaultVal = defaultVal;
		}
	}

	[Serializable]
	public class Prefab : Resource
	{
		private	GameObject	objTree	= null;

		public bool ContainsData
		{
			get { return this.objTree != null; }
		}

		public Prefab() : this(null) 
		{

		}
		public Prefab(GameObject obj)
		{
			this.Inject(obj);
		}

		public void Inject(GameObject obj)
		{
			if (obj == null)
			{
				if (this.objTree != null)
				{
					this.objTree.Dispose();
					this.objTree = null;
				}
			}
			else
			{
				obj.OnSaving(true);
				this.objTree = obj.Clone();
				obj.OnSaved(true);

				this.objTree.Parent = null;
				this.objTree.prefabLink = null;

				// Reset flagged fields
				ProcessPrefabObjectFields(this.objTree, new HashSet<object>());

				// Prevent recursion
				foreach (GameObject child in this.objTree.ChildrenDeep)
					if (child.PrefabLink != null && child.PrefabLink.Prefab == this)
						child.BreakPrefabLink();

				// Reset any custom component fields referencing GameObjects or Components
				// Removed: It's the users duty to take care of which fields go intro prefabs and which do not.
				//foreach (Component c in this.objTree.ComponentsDeep)
				//{
				//    Type curType = c.GetType();
				//    Type lastType = null;
				//    while (curType.Assembly != Assembly.GetExecutingAssembly())
				//    {
				//        lastType = curType;

				//        ReflectionHelper.DeepResetReferenceFields(
				//            curType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly), 
				//            c, typeof(GameObject), typeof(Component));

				//        curType = curType.BaseType;
				//    }
				//}
			}
		}
		public GameObject Instantiate()
		{
			GameObject instance = new GameObject(new ContentRef<Prefab>(this));
			instance.OnLoaded(true);
			return instance;
		}
		public void CopyTo(GameObject obj)
		{
			this.objTree.CopyTo(obj);
		}

		public bool HasGameObject(IEnumerable<int> indexPath)
		{
			return this.objTree != null && this.objTree.ChildAtIndexPath(indexPath) != null;
		}
		public bool HasComponent(IEnumerable<int> gameObjIndexPath, Type cmpType)
		{
			if (this.objTree == null) return false;

			GameObject child = this.objTree.ChildAtIndexPath(gameObjIndexPath);
			if (child == null) return false;
			return child.GetComponent(cmpType) != null;
		}

		protected override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Prefab c = r as Prefab;
			c.objTree = this.objTree.Clone();
		}

		private static void ProcessPrefabObjectFields(object obj, HashSet<object> visited)
		{
			visited.Add(obj);

			Type fixupRootType = obj.GetType();
			foreach (FieldInfo f in fixupRootType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				// Reset flagged fields to null / default
				if (f.IsDefined(typeof(NonSerializedPrefabAttribute), false))
				{
					object val = (f.GetCustomAttributes(typeof(NonSerializedPrefabAttribute), false)[0] as NonSerializedPrefabAttribute).DefaultValue;
					f.SetValue(obj, val ?? (f.FieldType.IsValueType ? Activator.CreateInstance(f.FieldType) : null));
				}
				// Traverse other types
				else if (!SerializationHelper.IsSafeAssignType(f.FieldType))
				{
					object val = f.GetValue(obj);
					if (val != null && !visited.Contains(val))
					{
						ProcessPrefabObjectFields(val, visited);
					}
				}
			}
		}
	}

	[Serializable]
	public sealed class PrefabLink
	{
		[Serializable]
		private struct VarMod
		{
			public	PropertyInfo	prop;
			public	Type			componentType;
			public	List<int>		childIndex;
			public	object			val;

			public VarMod Clone()
			{
				VarMod newVarMod = this;
				newVarMod.childIndex = new List<int>(this.childIndex);
				return newVarMod;
			}
		}

		private	ContentRef<Prefab>	prefab;
		private	GameObject			obj;
		private	List<VarMod>		changes;

		public GameObject Obj
		{
			get { return this.obj; }
		}
		public ContentRef<Prefab> Prefab
		{
			get { return this.prefab; }
		}
		public PrefabLink ParentLink
		{
			get
			{
				return this.obj.Parent != null ? this.obj.Parent.AffectedByPrefabLink : null;
			}
		}

		public PrefabLink(GameObject obj, ContentRef<Prefab> prefab)
		{
			this.obj = obj;
			this.prefab = prefab;
		}

		public void RelocateChanges(PrefabLink other)
		{
			if (this.changes == null || this.changes.Count == 0) return;
			if (!other.obj.IsChildOf(this.obj)) return;
			List<int> childPath = this.obj.IndexPathOfChild(other.obj);

			for (int i = this.changes.Count - 1; i >= 0; i--)
			{
				if (this.changes[i].childIndex.Take(childPath.Count).SequenceEqual(childPath))
				{
					GameObject targetObj;
					object target;

					targetObj = this.obj.ChildAtIndexPath(this.changes[i].childIndex);
					if (this.changes[i].componentType != null)
						target = targetObj.GetComponent(this.changes[i].componentType);
					else
						target = targetObj;

					other.PushChange(target, this.changes[i].prop, this.changes[i].val);
					this.changes.RemoveAt(i);
				}
			}
		}

		public PrefabLink Clone(GameObject newObj, ContentRef<Prefab> newPrefab)
		{
			PrefabLink newLink = new PrefabLink(newObj, newPrefab);
			if (this.changes != null)
			{
				newLink.changes = new List<VarMod>(this.changes.Count);
				for (int i = 0; i < this.changes.Count; i++)
					newLink.changes.Add(this.changes[i].Clone());
			}
			return newLink;
		}
		public PrefabLink Clone(GameObject newObj)
		{
			return this.Clone(newObj, this.prefab);
		}
		public PrefabLink Clone()
		{
			return this.Clone(this.obj, this.prefab);
		}

		public void Apply()
		{
			this.Apply(true);
		}
		private void Apply(bool deep)
		{
			this.ApplyPrefab();
			this.ApplyChanges();

			// Lower prefab links later
			if (deep)
			{
				foreach (GameObject child in this.obj.Children)
				{
					if (child.PrefabLink != null) child.PrefabLink.Apply(true);
				}
			}
		}
		public void ApplyPrefab()
		{
			if (!this.prefab.IsAvailable) return;
			if (!this.prefab.Res.ContainsData) return;
			this.prefab.Res.CopyTo(this.obj);
		}
		public void ApplyChanges()
		{
			if (this.changes == null || this.changes.Count == 0) return;

			GameObject targetObj;
			object target;
			for (int i = 0; i < this.changes.Count; i++)
			{
				targetObj = this.obj.ChildAtIndexPath(this.changes[i].childIndex);
				if (this.changes[i].componentType != null)
					target = targetObj.GetComponent(this.changes[i].componentType);
				else
					target = targetObj;

				this.changes[i].prop.SetValue(target, this.changes[i].val, null);
			}
		}
		public void UpdateChanges()
		{
			if (this.changes == null || this.changes.Count == 0) return;

			GameObject targetObj;
			object target;
			for (int i = 0; i < this.changes.Count; i++)
			{
				targetObj = this.obj.ChildAtIndexPath(this.changes[i].childIndex);
				if (this.changes[i].componentType != null)
					target = targetObj.GetComponent(this.changes[i].componentType);
				else
					target = targetObj;

				VarMod modTmp = this.changes[i];
				modTmp.val = this.changes[i].prop.GetValue(target, null);
				this.changes[i] = modTmp;
			}
		}

		public void PushChange(object target, PropertyInfo prop, object value)
		{
			if (ReflectionHelper.MemberInfoEquals(prop, ReflectionHelper.Property_GameObject_Parent)) return; // Reject changing "Parent" as it would destroy the PrefabLink
			if (this.changes == null) this.changes = new List<VarMod>();

			GameObject targetObj = target as GameObject;
			Component targetComp = target as Component;
			if (targetObj == null && targetComp != null) targetObj = targetComp.gameobj;

			if (targetObj == null) 
				throw new ArgumentException("Target object is not a valid child of this PrefabLinks GameObject", "target");
			if (value == null && prop.PropertyType.IsValueType)
				throw new ArgumentException("Target field cannot be assigned from null value.", "value");
			if (value != null && !prop.PropertyType.IsAssignableFrom(value.GetType()))
				throw new ArgumentException("Target field not assignable from Type " + value.GetType().Name + ".", "value");

			VarMod change;
			change.childIndex		= this.obj.IndexPathOfChild(targetObj);
			change.componentType	= (targetComp != null) ? targetComp.GetType() : null;
			change.prop			= prop;
			change.val				= value;

			this.PopChange(target, prop);
			this.changes.Add(change);
		}
		public void PushChange(object target, PropertyInfo prop)
		{
			this.PushChange(target, prop, prop.GetValue(target, null));
		}
		public void PopChange(object target, PropertyInfo prop)
		{
			if (this.changes == null || this.changes.Count == 0) return;

			GameObject targetObj = target as GameObject;
			Component targetComp = target as Component;
			if (targetObj == null && targetComp != null) targetObj = targetComp.gameobj;

			if (targetObj == null) 
				throw new ArgumentException("Target object is not a valid child of this PrefabLinks GameObject", "target");

			List<int> indexPath = this.obj.IndexPathOfChild(targetObj);
			for (int i = 0; i < this.changes.Count; i++)
			{
				if (this.changes[i].childIndex.SequenceEqual(indexPath) && this.changes[i].prop == prop)
				{
					this.changes.RemoveAt(i);
					break;
				}
			}
		}
		public void ClearChanges()
		{
			if (this.changes != null) this.changes.Clear();
		}

		public bool AffectsObject(Component cmp)
		{
			return this.prefab.IsAvailable && this.prefab.Res.HasComponent(this.obj.IndexPathOfChild(cmp.GameObj), cmp.GetType());
		}
		public bool AffectsObject(GameObject obj)
		{
			return this.prefab.IsAvailable && this.prefab.Res.HasGameObject(this.obj.IndexPathOfChild(obj));
		}

		public static List<PrefabLink> ApplyAllLinks(IEnumerable<GameObject> objEnum, Predicate<PrefabLink> predicate = null)
		{
			if (predicate == null) predicate = p => true;
			List<PrefabLink> appliedLinks = new List<PrefabLink>();

			var sortedQuery = from obj in objEnum
							  where obj.PrefabLink != null && predicate(obj.PrefabLink)
							  group obj by obj.HierarchyLevel into g
							  orderby g.Key
							  select g;

			foreach (var group in sortedQuery)
				foreach (GameObject obj in group)
				{
					obj.PrefabLink.Apply();
					appliedLinks.Add(obj.PrefabLink);
				}

			return appliedLinks;
		}
	}
}
