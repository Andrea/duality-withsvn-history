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
	/// <summary>
	/// GameObjects are what every <see cref="Duality.Resources.Scene"/> consists of. They represent nodes in the hierarchial scene graph and
	/// can maintain a <see cref="Duality.Resources.PrefabLink"/> connection. A GameObject's main duty is to group several <see cref="Component"/>s
	/// to form one logical instance of an actual object as part of the game, such as "Car" or "PlayerCharacter". However,
	/// the GameObjects itsself does not contain any game-related logic and, by default, doesn't even occupy a position in space.
	/// This is the job of its Components.
	/// </summary>
	/// <seealso cref="Component"/>
	/// <seealso cref="Duality.Resources.Scene"/>
	/// <seealso cref="Duality.Resources.PrefabLink"/>
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


		/// <summary>
		/// [GET / SET] This GameObject's parent object in the scene graph.
		/// A GameObject usually depends on its parent in some way, such as being
		/// positioned relative to it when occupying a position in space.
		/// </summary>
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
		/// <summary>
		/// [GET / SET] Whether or not the GameObject is currently active. To return true,
		/// both the GameObject itsself and all of its parent GameObjects need to be active.
		/// </summary>
		/// <seealso cref="ActiveSingle"/>
		public bool Active
		{
			get { return this.ActiveSingle && (this.parent == null || this.parent.Active); }
			set { this.ActiveSingle = value; }
		}							//	GS
		/// <summary>
		/// [GET / SET] Whether or not the GameObject is currently active. Unlike <see cref="Active"/>,
		/// this property ignores parent activation states and depends only on this single GameObject.
		/// The scene graph and other Duality instances usually check <see cref="Active"/>, not ActiveSingle.
		/// </summary>
		/// <seealso cref="Active"/>
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
		/// <summary>
		/// [GET / SET] The name of this GameObject.
		/// </summary>
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}							//	GS
		/// <summary>
		/// [GET] The path-like hierarchial name of this GameObject.
		/// </summary>
		/// <example>For an object called <c>Wheel</c> inside an object called <c>Car</c>, this would return <c>Car/Wheel</c>.</example>
		public string FullName
		{
			get { return (this.parent != null) ? this.parent.FullName + '/' + this.name : this.name; }
		}						//	G
		/// <summary>
		/// [GET] Returns the number of parents this object has when travelling upwards the scene graph hierarchy.
		/// </summary>
		/// <example>
		/// This will be zero for a root object (one that has no parent object), one for a root object's child,
		/// two for a root object's child's child, and so on.
		/// </example>
		public int HierarchyLevel
		{
			get
			{
				if (this.parent == null) return 0;
				else return this.parent.HierarchyLevel + 1;
			}
		}					//	G
		/// <summary>
		/// [GET] The number of child GameObjects this object has.
		/// </summary>
		public int ChildCount
		{
			get
			{
				return (this.children != null) ? this.children.Count : 0;
			}
		}						//  G
		/// <summary>
		/// [GET] The number of <see cref="Component"/>s this object consists of.
		/// </summary>
		public int ComponentCount
		{
			get
			{
				return this.compList.Count;
			}
		}					//  G
		/// <summary>
		/// [GET] Enumerates this objects child GameObjects.
		/// </summary>
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
		/// <summary>
		/// [GET] Enumerates all GameObjects that are directly or indirectly parented to this object, i.e. its
		/// children, grandchildren, etc.
		/// </summary>
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
		/// <summary>
		/// [GET] The <see cref="Duality.Resources.PrefabLink"/> that connects this object to a <see cref="Duality.Resources.Prefab"/>.
		/// </summary>
		/// <seealso cref="Duality.Resources.PrefabLink"/>
		/// <seealso cref="Duality.Resources.Prefab"/>
		public PrefabLink PrefabLink
		{
			get { return this.prefabLink; }
			private set { this.prefabLink = value; }
		}					//	G[S]
		/// <summary>
		/// [GET] The <see cref="Duality.Resources.PrefabLink"/> that connects this object or one or its parent GameObjects to a <see cref="Duality.Resources.Prefab"/>.
		/// </summary>
		/// <remarks>
		/// This does not necessarily mean that this GameObject will be affected by the PrefabLink, since it might not be part of
		/// the linked Prefab. It simply indicates the returned PrefabLink's potential to adjust this GameObject when being applied.
		/// </remarks>
		/// <seealso cref="Duality.Resources.PrefabLink"/>
		/// <seealso cref="Duality.Resources.Prefab"/>
		public PrefabLink AffectedByPrefabLink
		{
			get
			{
				if (this.prefabLink != null) return this.prefabLink;
				else if (this.parent != null) return this.parent.AffectedByPrefabLink;
				else return null;
			}
		}		//	G
		/// <summary>
		/// [GET] Returns whether this GameObject has been disposed. Disposed GameObjects are not to be used and should
		/// be treated specifically or as null references by your code.
		/// </summary>
		public bool Disposed
		{
			get { return this.disposed; }
		}							//	G

		/// <summary>
		/// [GET] The GameObject's <see cref="Duality.Components.Transform"/> Component, if existing.
		/// </summary>
		/// <seealso cref="Duality.Components.Transform"/>
		public Components.Transform Transform
		{
			get { return this.compTransform; }
		}
		/// <summary>
		/// [GET] The GameObject's <see cref="Duality.Components.Camera"/> Component, if existing.
		/// </summary>
		/// <seealso cref="Duality.Components.Camera"/>
		public Components.Camera Camera
		{
			get { return this.GetComponent<Components.Camera>(); }
		}
		/// <summary>
		/// [GET] The GameObject's <see cref="Duality.Components.Renderer"/> Component, if existing. 
		/// </summary>
		/// <remarks>Note that a GameObject may contain multiple Renderers in which case the return value of this property may be any of them.</remarks>
		/// <seealso cref="Duality.Components.Renderer"/>
		public Components.Renderer Renderer
		{
			get { return this.GetComponent<Components.Renderer>(); }
		}
		

		/// <summary>
		/// Fired when a Component has been added to the GameObject
		/// </summary>
		public event EventHandler<ComponentEventArgs>	EventComponentAdded		= null;
		/// <summary>
		/// Fired when a Component is about to be removed from the GameObject
		/// </summary>
		public event EventHandler<ComponentEventArgs>	EventComponentRemoving	= null;


		public GameObject() {}
		/// <summary>
		/// Creates a GameObject based on a specific <see cref="Duality.Resources.Prefab"/>.
		/// </summary>
		/// <param name="prefab">The Prefab that will be applied to this GameObject.</param>
		/// <seealso cref="Duality.Resources.Prefab"/>
		public GameObject(ContentRef<Prefab> prefab)
		{
			this.LinkToPrefab(prefab);
			this.PrefabLink.Apply();
			this.OnLoaded(true);
		}

		/// <summary>
		/// Sets or alters this GameObject's <see cref="Duality.Resources.PrefabLink"/> to reference the specified <see cref="Prefab"/>.
		/// </summary>
		/// <param name="prefab">The Prefab that will be linked to.</param>
		/// <seealso cref="Duality.Resources.PrefabLink"/>
		/// <seealso cref="Duality.Resources.Prefab"/>
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
		/// <summary>
		/// Breaks this GameObject's <see cref="Duality.Resources.PrefabLink"/>
		/// </summary>
		public void BreakPrefabLink()
		{
			this.prefabLink = null;
		}

		/// <summary>
		/// Returns the child GameObject that is internally stored at the specified index.
		/// </summary>
		/// <param name="index">The index at which the desired GameObject is located.</param>
		/// <returns>The child GameObject at the specified index. Null, if the index is not valid.</returns>
		public GameObject ChildAtIndex(int index)
		{
			if (this.children == null || index < 0 || index >= this.children.Count) return null;
			return this.children[index];
		}
		/// <summary>
		/// Executes a series of <see cref="ChildAtIndex"/> calls, beginning at this GameObject and 
		/// each on the last retrieved child object.
		/// </summary>
		/// <param name="indexPath">An enumeration of child indices.</param>
		/// <returns>The last retrieved GameObject after executing all indexing steps.</returns>
		/// <example>
		/// Calling <c>ChildAtIndexPath(new[] { 0, 0 })</c> will return the first child of the first child.
		/// </example>
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
		/// <summary>
		/// Determines the index of a specific child GameObject.
		/// </summary>
		/// <param name="child">The child GameObject of which the index is to be determined.</param>
		/// <returns>The index of the specified child GameObject</returns>
		/// <seealso cref="ChildAtIndex"/>
		public int IndexOfChild(GameObject child)
		{
			return this.children != null ? this.children.IndexOf(child) : -1;
		}
		/// <summary>
		/// Determines the index path from this GameObject to the specified child (or grandchild, etc.) of it.
		/// </summary>
		/// <param name="child">The child GameObject to lead to.</param>
		/// <returns>A <see cref="List{T}"/> of indices that lead from this GameObject to the specified child GameObject.</returns>
		/// <seealso cref="ChildAtIndexPath"/>
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
		/// <summary>
		/// Returns whether this GameObject is a child, grandchild or similar of the specified GameObject.
		/// </summary>
		/// <param name="parent">The GameObject to check whether or not it is a parent of this one.</param>
		/// <returns>True, if it is, false if not.</returns>
		public bool IsChildOf(GameObject parent)
		{
			if (this.parent == parent)
				return true;
			else if (this.parent != null) 
				return this.parent.IsChildOf(parent);
			else
				return false;
		}

		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this GameObject that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <typeparam name="T">The base Type to match when iterating through the Components.</typeparam>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponents(System.Type,bool)"/>
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
		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this object's child GameObjects that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <typeparam name="T">The base Type to match when iterating through the Components.</typeparam>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponentsInChildren(System.Type,bool)"/>
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
		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this GameObject or any child GameObject that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <typeparam name="T">The base Type to match when iterating through the Components.</typeparam>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponentsDeep(System.Type,bool)"/>
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
		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this GameObject that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <param name="t">The base Type to match when iterating through the Components.</param>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponents{T}(bool)"/>
		public IEnumerable<Component> GetComponents(Type t, bool activeOnly = false)
		{
			foreach (Component c in this.compList)
			{
				if ((c.Active || !activeOnly) && t.IsAssignableFrom(c.GetType())) yield return c;
			}
		}
		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this object's child GameObjects that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <param name="t">The base Type to match when iterating through the Components.</param>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponentsInChildren{T}(bool)"/>
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
		/// <summary>
		/// Enumerates all <see cref="Component"/>s of this GameObject or any child GameObject that match the specified <see cref="Type"/> or subclass it.
		/// </summary>
		/// <param name="t">The base Type to match when iterating through the Components.</param>
		/// <param name="activeOnly">If true, only <see cref="Component.Active">active</see> Components are enumerated.</param>
		/// <returns>An enumeration of all Components that match the specified conditions.</returns>
		/// <seealso cref="GetComponentsDeep{T}(bool)"/>
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

		/// <summary>
		/// Returns a single <see cref="Component"/> that matches the specified <see cref="System.Type"/>.
		/// </summary>
		/// <typeparam name="T">The Type to match the Components with.</typeparam>
		/// <param name="exactType">If true, the Component must match the specified Type exactly. If false, subclasses of it are also valid.</param>
		/// <returns>A single Component that matches the specified Type. Null, if none was found.</returns>
		public T GetComponent<T>(bool exactType = false) where T : class
		{
			return GetComponent(typeof(T), exactType) as T;
		}
		/// <summary>
		/// Returns a single <see cref="Component"/> that matches the specified <see cref="System.Type"/>.
		/// </summary>
		/// <param name="t">The Type to match the Components with.</param>
		/// <param name="exactType">If true, the Component must match the specified Type exactly. If false, subclasses of it are also valid.</param>
		/// <returns>A single Component that matches the specified Type. Null, if none was found.</returns>
		public Component GetComponent(Type t, bool exactType = false)
		{
			Component result = null;
			if (!this.compMap.TryGetValue(t, out result) && !exactType)
				return this.GetComponents(t).FirstOrDefault();
			else
				return result;
		}

		/// <summary>
		/// Adds a <see cref="Component"/> of the specified <see cref="System.Type"/> to this GameObject, if not existing yet.
		/// Simply uses the existing Component otherwise.
		/// </summary>
		/// <typeparam name="T">The Type of which to request a Component instance.</typeparam>
		/// <returns>A reference to a Component of the specified Type.</returns>
		/// <seealso cref="AddComponent(System.Type)"/>
		public T AddComponent<T>() where T : Component, new()
		{
			if (this.compMap.ContainsKey(typeof(T))) return this.compMap[typeof(T)] as T;
			T newComp = new T();
			return this.AddComponent<T>(newComp);
		}
		/// <summary>
		/// Adds a <see cref="Component"/> of the specified <see cref="System.Type"/> to this GameObject, if not existing yet.
		/// Simply uses the existing Component otherwise.
		/// </summary>
		/// <param name="t">The Type of which to request a Component instance.</param>
		/// <returns>A reference to a Component of the specified Type.</returns>
		/// <seealso cref="AddComponent{T}()"/>
		public Component AddComponent(Type t)
		{
			if (this.compMap.ContainsKey(t)) return this.compMap[t];
			Component newComp = ReflectionHelper.CreateInstanceOf(t) as Component;
			return this.AddComponent<Component>(newComp);
		}
		/// <summary>
		/// Adds the specified <see cref="Component"/> to this GameObject, if no Component of that Type is already part of this GameObject.
		/// Simply uses the already added Component otherwise.
		/// </summary>
		/// <typeparam name="T">The Components Type.</typeparam>
		/// <param name="newComp">The Component instance to add to this GameObject.</param>
		/// <returns>A reference to a Component of the specified Type</returns>
		/// <exception cref="System.ArgumentException">Thrown if the specified Component is already attached to a GameObject</exception>
		public T AddComponent<T>(T newComp) where T : Component
		{
			if (newComp.gameobj != null) throw new ArgumentException(String.Format(
				"Specified Component '{0}' is already part of another GameObject '{1}'",
				Log.Type(newComp.GetType()),
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
		/// <summary>
		/// Removes a <see cref="Component"/> of the specified <see cref="System.Type"/> from this GameObject, if existing.
		/// </summary>
		/// <typeparam name="T">The Type of which to remove a Component instance.</typeparam>
		/// <returns>A reference to the removed Component. Null otherwise.</returns>
		/// <seealso cref="RemoveComponent(Type)"/>
		/// <seealso cref="RemoveComponent(Component)"/>
		public T RemoveComponent<T>() where T : Component
		{
			return this.RemoveComponent(typeof(T)) as T;
		}
		/// <summary>
		/// Removes a <see cref="Component"/> of the specified <see cref="System.Type"/> from this GameObject, if existing.
		/// </summary>
		/// <param name="t">The Type of which to remove a Component instance.</param>
		/// <returns>A reference to the removed Component. Null otherwise.</returns>
		/// <seealso cref="RemoveComponent{T}()"/>
		/// <seealso cref="RemoveComponent(Component)"/>
		public Component RemoveComponent(Type t)
		{
			Component cmp = this.GetComponent(t);
			if (cmp != null) this.RemoveComponent(cmp);
			return cmp;
		}
		/// <summary>
		/// Removes a specific <see cref="Component"/> from this GameObject.
		/// </summary>
		/// <param name="cmp">The Component to remove from this GameObject</param>
		/// <exception cref="System.ArgumentNullException">Thrown when the specified Component is a null reference.</exception>
		/// <exception cref="System.ArgumentException">Thrown when the specified Component does not belong to this GameObject</exception>
		/// <seealso cref="RemoveComponent(Type)"/>
		/// <seealso cref="RemoveComponent{T}()"/>
		public void RemoveComponent(Component cmp)
		{
			if (cmp == null) throw new ArgumentNullException("cmp", "Can't remove a null reference Component");
			if (cmp.gameobj != this) throw new ArgumentException("cmp", "The specified Component does not belong to this GameObject");

			this.OnComponentRemoving(cmp);

			this.compMap.Remove(cmp.GetType());
			this.compList.Remove(cmp);

			if (cmp is Components.Transform) this.compTransform = null;

			cmp.gameobj = null;
		}
		/// <summary>
		/// Removes all <see cref="Component">Components</see> from this GameObject.
		/// </summary>
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

		/// <summary>
		/// Disposes this GameObject as well as all of its child GameObjects and <see cref="Component">Components</see>.
		/// You usually don't need this - use <see cref="DisposeLater"/> instead.
		/// </summary>
		/// <seealso cref="DisposeLater"/>
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
		/// <summary>
		/// Schedules this GameObject for disposal. It is guaranteed to be executed until the next update cycle starts.
		/// </summary>
		/// <seealso cref="Dispose"/>
		public void DisposeLater()
		{
			DualityApp.DisposeLater(this);
		}
		/// <summary>
		/// Creates a deep copy of this GameObject.
		/// </summary>
		/// <returns>A reference to a newly created deep copy of this GameObject.</returns>
		public GameObject Clone()
		{
			GameObject target = new GameObject();
			this.CopyTo(target);
			target.Parent = this.Parent;
			return target;
		}
		/// <summary>
		/// Deep-copies this GameObject's data to the specified target GameObject.
		/// </summary>
		/// <param name="target">The target GameObject to copy to.</param>
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
			// Sanitary check in case something failed deserializing
			for (int i = this.compList.Count - 1;  i >= 0; i--)
				if (this.compList[i] == null) this.compList.RemoveAt(i);
			foreach (Type key in this.compMap.Keys.ToArray())
				if (this.compMap[key] == null) this.compMap.Remove(key);

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
