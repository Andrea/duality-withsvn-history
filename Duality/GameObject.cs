using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Duality.Resources;

namespace Duality
{
	[Serializable]
	[System.Diagnostics.DebuggerDisplay("GameObject {FullName}")]
	public sealed class GameObject : IManageableObject
	{
		internal	PrefabLink					prefabLink	= null;
		private		GameObject					parent		= null;
		private		List<GameObject>			children	= null;
		private		Dictionary<Type,Component>	compMap		= new Dictionary<Type,Component>();
		private		List<Component>				compList	= new List<Component>();
		private		string						name		= "obj" + MathF.Rnd.Next().ToString();
		private		bool						active		= true;
		private		bool						disposed	= false;

		// Built-in heavily used component lookup
		private	Components.Transform		compTransform	= null;


		public GameObject Parent
		{
			get { return this.parent; }
			set
			{
				if (this.parent != value)
				{
					GameObject oldParent = this.parent;

					if (this.parent != null) this.parent.children.Remove(this);
					this.parent = value;
					if (this.parent != null)
					{
						if (this.parent.children == null) this.parent.children = new List<GameObject>();
						this.parent.children.Add(this);
					}

					this.OnParentChanged(oldParent, this.parent);
				}
			}
		}						//	GS
		public bool Active
		{
			get { return this.ActiveSingle && (this.parent == null || this.parent.Active); }
			set { this.ActiveSingle = value; }
		}							//	GS
		public bool ActiveSingle
		{
			get { return this.active && !this.disposed; }
			set 
			{ 
				if (this.active != value)
				{
					if (value)
					{
						this.OnActivate();
						foreach (GameObject child in this.ChildrenDeep)
						{
							if (!child.Active) child.OnActivate();
						}
					}
					else
					{
						this.OnDeactivate();
						foreach (GameObject child in this.ChildrenDeep)
						{
							if (child.Active) child.OnDeactivate();
						}
					}

					this.active = value;
				}
			}
		}						//	GS
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}							//	GS
		public string FullName
		{
			get { return (this.parent != null) ? this.parent.FullName + '/' + this.name : this.name; }
		}						//	G
		public int HierarchyLevel
		{
			get
			{
				if (this.parent == null) return 0;
				else return this.parent.HierarchyLevel + 1;
			}
		}					//	G
		public int ChildCount
		{
			get
			{
				return (this.children != null) ? this.children.Count : 0;
			}
		}						//  G
		public int ComponentCount
		{
			get
			{
				return this.compList.Count;
			}
		}					//  G
		public IEnumerable<GameObject> Children
		{
			get
			{
				if (this.children == null) yield break;
				foreach (GameObject c in this.children)
				{
					yield return c;
				}
			}
		}		//  G
		public IEnumerable<GameObject> ChildrenDeep
		{
			get
			{
				if (this.children == null) yield break;
				foreach (GameObject c in this.children)
				{
					yield return c;
					foreach (GameObject cc in c.ChildrenDeep)
					{
						yield return cc;
					}
				}
			}
		}	//  G
		public PrefabLink PrefabLink
		{
			get { return this.prefabLink; }
			private set { this.prefabLink = value; }
		}					//	G[S]
		public PrefabLink AffectedByPrefabLink
		{
			get
			{
				if (this.prefabLink != null) return this.prefabLink;
				else if (this.parent != null) return this.parent.AffectedByPrefabLink;
				else return null;
			}
		}		//	G
		public bool Disposed
		{
			get { return this.disposed; }
		}							//	G

		// Built-in heavily used component lookup
		public Components.Transform Transform
		{
			get { return this.compTransform; }
		}
		public Components.Camera Camera
		{
			get { return this.GetComponent<Components.Camera>(); }
		}
		public Components.Renderer Renderer
		{
			get { return this.GetComponent<Components.Renderer>(); }
		}
		

		public event EventHandler<ComponentEventArgs>				EventComponentAdded		= null;
		public event EventHandler<ComponentEventArgs>				EventComponentRemoving	= null;


		public GameObject() {}
		public GameObject(ContentRef<Prefab> prefab)
		{
			this.LinkToPrefab(prefab);
			this.PrefabLink.Apply();
			this.OnLoaded(true);
		}

		public void LinkToPrefab(ContentRef<Prefab> prefab)
		{
			if (this.prefabLink == null)
			{
				// Not affected by another (higher) PrefabLink
				if (this.AffectedByPrefabLink == null)
				{
					this.prefabLink = new PrefabLink(this, prefab);
					// If a nested object is already PrefabLink'ed, add it to the changelist
					foreach (GameObject child in this.ChildrenDeep)
					{
						if (child.PrefabLink != null && child.PrefabLink.ParentLink == this.prefabLink)
						{
							this.prefabLink.PushChange(child, ReflectionInfo.Property_GameObject_PrefabLink, child.PrefabLink.Clone());
						}
					}
				}
				// Already affected by another (higher) PrefabLink
				else
				{
					this.prefabLink = new PrefabLink(this, prefab);
					this.prefabLink.ParentLink.RelocateChanges(this.prefabLink);
				}
			}
			else
				this.prefabLink = this.prefabLink.Clone(this, prefab);
		}
		public void BreakPrefabLink()
		{
			this.prefabLink = null;
		}

		public GameObject ChildByName(string name)
		{
			return this.children.FirstOrDefault(o => o.name == name);
		}
		public IEnumerable<GameObject> ChildrenByName(string name)
		{
			return this.children.Where(o => o.name == name);
		}
		public GameObject ChildAtIndex(int index)
		{
			if (this.children == null || index < 0 || index >= this.children.Count) return null;
			return this.children[index];
		}
		public GameObject ChildAtIndexPath(IEnumerable<int> indexPath)
		{
			GameObject curObj = this;
			foreach (int i in indexPath)
			{
				curObj = curObj.ChildAtIndex(i);
				if (curObj == null) return null;
			}
			return curObj;
		}
		public int IndexOfChild(GameObject child)
		{
			return this.children != null ? this.children.IndexOf(child) : -1;
		}
		public List<int> IndexPathOfChild(GameObject child)
		{
			List<int> path = new List<int>();
			while (child.parent != null && child != this)
			{
				path.Add(child.parent.children.IndexOf(child));
				child = child.parent;
			}
			path.Reverse();
			return path;
		}
		public bool IsChildOf(GameObject parent)
		{
			if (this.parent == parent)
				return true;
			else if (this.parent != null) 
				return this.parent.IsChildOf(parent);
			else
				return false;
		}

		public IEnumerable<T> GetComponents<T>(bool activeOnly = false) where T : class
		{
			foreach (Component c in this.compList)
			{
				if (c.Active || !activeOnly)
				{
					T CasT = c as T;
					if (CasT != null) yield return CasT;
				}
			}
		}
		public IEnumerable<T> GetComponentsInChildren<T>(bool activeOnly = false) where T : class
		{
			if (this.children == null) yield break;
			foreach (GameObject g in this.children)
			{
				foreach (T c in g.GetComponentsDeep<T>(activeOnly))
				{
					yield return c;
				}
			}
		}
		public IEnumerable<T> GetComponentsDeep<T>(bool activeOnly = false) where T : class
		{
			foreach (T c in this.GetComponents<T>(activeOnly))
			{
				yield return c;
			}
			foreach (T c in this.GetComponentsInChildren<T>(activeOnly))
			{
				yield return c;
			}
		}
		public IEnumerable<Component> GetComponents(Type t, bool activeOnly = false)
		{
			foreach (Component c in this.compList)
			{
				if ((c.Active || !activeOnly) && t.IsAssignableFrom(c.GetType())) yield return c;
			}
		}
		public IEnumerable<Component> GetComponentsInChildren(Type t, bool activeOnly = false)
		{
			if (this.children == null) yield break;
			foreach (GameObject g in this.children)
			{
				foreach (Component c in g.GetComponentsDeep(t, activeOnly))
				{
					yield return c;
				}
			}
		}
		public IEnumerable<Component> GetComponentsDeep(Type t, bool activeOnly = false)
		{
			foreach (Component c in this.GetComponents(t, activeOnly))
			{
				yield return c;
			}
			foreach (Component c in this.GetComponentsInChildren(t, activeOnly))
			{
				yield return c;
			}
		}

		public T GetComponent<T>(bool exactType = false) where T : class
		{
			return GetComponent(typeof(T), exactType) as T;
		}
		public Component GetComponent(Type t, bool exactType = false)
		{
			Component result = null;
			if (!this.compMap.TryGetValue(t, out result) && !exactType)
				return this.GetComponents(t).FirstOrDefault();
			else
				return result;
		}

		public T AddComponent<T>() where T : Component, new()
		{
			if (this.compMap.ContainsKey(typeof(T))) return this.compMap[typeof(T)] as T;
			T newComp = new T();
			return this.AddComponent<T>(newComp);
		}
		public T AddComponent<T>(T newComp) where T : Component
		{
			if (newComp.gameobj != null) throw new ArgumentException(String.Format(
				"Specified Component '{0}' is already part of another GameObject '{1}'",
				newComp.GetType().Name,
				newComp.gameobj.FullName));
			
			Type cType = newComp.GetType();
			if (this.compMap.ContainsKey(cType)) return this.compMap[cType] as T;

			newComp.gameobj = this;
			this.compMap.Add(cType, newComp);
			this.compList.Add(newComp);

			if (newComp is Components.Transform) this.compTransform = (Components.Transform)(Component)newComp;

			this.OnComponentAdded(newComp);
			return newComp;
		}
		public T RemoveComponent<T>() where T : Component
		{
			return this.RemoveComponent(typeof(T)) as T;
		}
		public Component AddComponent(Type t)
		{
			if (this.compMap.ContainsKey(t)) return this.compMap[t];
			Component newComp = ReflectionHelper.CreateInstanceOf(t) as Component;
			return this.AddComponent<Component>(newComp);
		}
		public Component RemoveComponent(Type t)
		{
			Component cmp = this.GetComponent(t);
			if (cmp != null)
			{
				this.OnComponentRemoving(cmp);

				this.compMap.Remove(t);
				this.compList.Remove(cmp);

				if (cmp is Components.Transform) this.compTransform = null;

				cmp.gameobj = null;
			}
			return cmp;
		}
		public void ClearComponents()
		{
			foreach (Component c in this.compList)
			{
				this.OnComponentRemoving(c);
				c.gameobj = null;
			}
			this.compList.Clear();
			this.compMap.Clear();
			this.compTransform = null;
		}

		public void Dispose()
		{
			if (!this.disposed)
			{
				// Delete Components
				for (int i = this.compList.Count - 1; i >= 0; i--) this.compList[i].Dispose();
				// Delete child objects
				if (this.children != null) for (int i = this.children.Count - 1; i >= 0; i--) this.children[i].Dispose();
				// Remove from parent
				if (this.parent != null) this.Parent = null;

				this.disposed = true;
			}
		}
		public void DisposeLater()
		{
			DualityApp.DisposeLater(this);
		}
		public GameObject Clone()
		{
			GameObject target = new GameObject();
			this.CopyTo(target);
			target.Parent = this.Parent;
			return target;
		}
		public void CopyTo(GameObject target)
		{
			// Copy "pure" data
			target.name			= this.name;
			target.active		= this.active;
			target.disposed		= this.disposed;

			// Copy component data, create missing components
			foreach (Component c in this.compList)
			{
				c.CopyTo(target.AddComponent(c.GetType()));
			}

			// Copy child data, create missing children
			if (this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					GameObject thisChild	= this.children[i];
					GameObject targetChild	= (target.children != null && target.children.Count > i) ? target.children[i] : new GameObject();
					thisChild.CopyTo(targetChild);
					targetChild.Parent = target;
				}
			}

			// Copy & maintain PrefabLink. Don't replace an existing link with null.
			if (this.PrefabLink != null)
			{
				target.prefabLink = this.prefabLink.Clone(target);
				target.PrefabLink.UpdateChanges();
			}
		}

		internal void Update()
		{
			// Update Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpUpdatable selfUpd = c as ICmpUpdatable;
			    if (selfUpd != null) selfUpd.OnUpdate();
			}
		}
		internal void EditorUpdate()
		{
			// Update Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpEditorUpdatable selfUpd = c as ICmpEditorUpdatable;
			    if (selfUpd != null) selfUpd.OnUpdate();
			}
		}
		
		internal void OnLoaded(bool deep = false)
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpInitializable cInit = c as ICmpInitializable;
				if (cInit != null) cInit.OnInit(Component.InitContext.Loaded);
			}

			if (deep && this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					GameObject c = this.children[i];
					c.OnLoaded(deep);
				}
			}
		}
		internal void OnSaving(bool deep = false)
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpInitializable cInit = c as ICmpInitializable;
				if (cInit != null) cInit.OnShutdown(Component.ShutdownContext.Saving);
			}

			if (deep && this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					GameObject c = this.children[i];
					c.OnSaving(deep);
				}
			}
		}
		internal void OnSaved(bool deep = false)
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpInitializable cInit = c as ICmpInitializable;
				if (cInit != null) cInit.OnInit(Component.InitContext.Saved);
			}

			if (deep && this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					GameObject c = this.children[i];
					c.OnSaved(deep);
				}
			}
		}
		internal void OnActivate()
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpInitializable cInit = c as ICmpInitializable;
				if (cInit != null) cInit.OnInit(Component.InitContext.Activate);
			}
		}
		internal void OnDeactivate()
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpInitializable cInit = c as ICmpInitializable;
				if (cInit != null) cInit.OnShutdown(Component.ShutdownContext.Deactivate);
			}
		}
		private void OnParentChanged(GameObject oldParent, GameObject newParent)
		{
			// Notify Components
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpGameObjectListener cParent = c as ICmpGameObjectListener;
				if (cParent != null) cParent.OnGameObjectParentChanged(oldParent, this.parent);
			}
		}
		private void OnComponentAdded(Component cmp)
		{
			// Notify Components
			ICmpInitializable cmpInit = cmp as ICmpInitializable;
			if (cmpInit != null) cmpInit.OnInit(Component.InitContext.AddToGameObject);
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpComponentListener cTemp = c as ICmpComponentListener;
				if (cTemp != null) cTemp.OnComponentAdded(cmp);
			}

			// Public event
			if (this.EventComponentAdded != null)
				this.EventComponentAdded(this, new ComponentEventArgs(cmp));
		}
		private void OnComponentRemoving(Component cmp)
		{
			// Notify Components
			ICmpInitializable cmpInit = cmp as ICmpInitializable;
			if (cmpInit != null) cmpInit.OnShutdown(Component.ShutdownContext.RemovingFromGameObject);
			for (int i = 0; i < this.compList.Count; i++)
			{
				Component c = this.compList[i];

				if (!c.Active) continue;
				ICmpComponentListener cTemp = c as ICmpComponentListener;
				if (cTemp != null) cTemp.OnComponentRemoving(cmp);
			}

			// Public event
			if (this.EventComponentRemoving != null)
				this.EventComponentRemoving(this, new ComponentEventArgs(cmp));
		}

		public override string ToString()
		{
			return this.FullName;
		}
	}
}
