using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Duality.Resources;

namespace Duality
{
	public interface ICmpUpdatable
	{
		void OnUpdate();
	}
	public interface ICmpEditorUpdatable
	{
		void OnUpdate();
	}
	public interface ICmpComponentListener
	{
		void OnComponentAdded(Component comp);
		void OnComponentRemoving(Component comp);
	}
	public interface ICmpGameObjectListener
	{
		void OnGameObjectParentChanged(GameObject oldParent, GameObject newParent);
	}
	public interface ICmpInitializable
	{
		void OnInit(Component.InitContext context);
		void OnShutdown(Component.ShutdownContext context);
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class RequiredComponentAttribute : Attribute
	{
		private	Type	cmpType;

		public Type RequiredComponentType
		{
			get { return this.cmpType; }
		}

		public RequiredComponentAttribute(Type cmpType)
		{
			this.cmpType = cmpType;
		}
	}

	[Serializable]
	[System.Diagnostics.DebuggerDisplay("{TypeName} in {GameObj.FullName}")]
	public abstract class Component : IManageableObject
	{
		public enum InitContext
		{
			Saved,
			Loaded,
			Activate,
			AddToGameObject
		}
		public enum ShutdownContext
		{
			Saving,
			Deactivate,
			RemovingFromGameObject
		}


		internal	GameObject	gameobj		= null;
		private		bool		disposed	= false;
		private		bool		active		= true;


		public bool Active
		{
			get { return this.ActiveSingle && this.gameobj != null && this.gameobj.Active; }
			set { this.ActiveSingle = value; }
		}
		public bool ActiveSingle
		{
			get { return this.active && !this.disposed; }
			set 
			{
				if (this.active != value)
				{
					if (value)
					{
						ICmpInitializable cInit = this as ICmpInitializable;
						if (cInit != null) cInit.OnInit(Component.InitContext.Activate);
					}
					else
					{
						ICmpInitializable cInit = this as ICmpInitializable;
						if (cInit != null) cInit.OnShutdown(ShutdownContext.Deactivate);
					}

					this.active = value;
				}
			}
		}
		public bool Disposed
		{
			get { return this.disposed; }
		}
		public GameObject GameObj
		{
			get { return this.gameobj; }
		}
		public string TypeName
		{
			get { return this.GetType().Name; }
		}


		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;

				if (this.gameobj != null && !this.gameobj.Disposed) 
					this.gameobj.RemoveComponent(this.GetType());
			}
		}

		public Component Clone()
		{
			Component newObj = ReflectionHelper.CreateInstanceOf(this.GetType()) as Component;
			this.CopyTo(newObj);
			return newObj;
		}
		public void CopyTo(Component target)
		{
			// CopyTo for all basic Component types
			this.CopyToInternal(target);
			// CopyTo for custom Components - defaults to reflection
			this.OnCopyTo(target);
		}
		internal virtual void CopyToInternal(Component target)
		{
			// Copy "pure" data
			target.active	= this.active;
			target.disposed	= this.disposed;
		}
		protected virtual void OnCopyTo(Component target)
		{
			// Travel up the inheritance hierarchy until we hit an object located here
			Type curType = this.GetType();
			Type lastType = null;
			while (curType.Assembly != Assembly.GetExecutingAssembly())
			{
				lastType = curType;

				// Apply default behaviour to any class that doesn't have an OnCopyTo override
				if (curType.GetMethod("OnCopyTo", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, new Type[] { typeof(Component) }, null) == null)
				{
					SerializationHelper.DeepCopyFieldsExplicit(
						curType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly), 
						this, target, typeof(System.Collections.ICollection));
				}

				curType = curType.BaseType;
			}
		}

		public bool RequiresComponent(Type requiredType)
		{
			return RequiresComponent(this.GetType());
		}
		public bool IsComponentRequirementMet(Component evenWhenRemovingThis = null)
		{
			Type[] reqTypes = this.GetRequiredComponents();
			foreach (Type r in reqTypes)
			{
				if (!this.GameObj.GetComponents(r).Any(c => c != evenWhenRemovingThis)) return false;
			}

			return true;
		}
		public bool IsComponentRequirementMet(GameObject isMetInObj, IEnumerable<Component> whenAddingThose = null)
		{
			Type[] reqTypes = this.GetRequiredComponents();
			foreach (Type r in reqTypes)
			{
				if (isMetInObj.GetComponent(r) == null)
				{
					if (whenAddingThose == null) return false;
					else if (!whenAddingThose.Any(c => r.IsAssignableFrom(c.GetType()))) return false;
				}
			}

			return true;
		}
		public Type[] GetRequiredComponents()
		{
			return GetRequiredComponents(this.GetType());
		}

		public override string ToString()
		{
			return string.Format("{0} in {1}", this.GetType().Name, this.gameobj.FullName);
		}

		public static bool RequiresComponent(Type cmpType, Type requiredType)
		{
			RequiredComponentAttribute[] attribs = 
				cmpType.GetCustomAttributes(typeof(RequiredComponentAttribute), true).
				Cast<RequiredComponentAttribute>().
				ToArray();

			return attribs.Any(a => a.RequiredComponentType.IsAssignableFrom(cmpType));
		}
		public static Type[] GetRequiredComponents(Type cmpType)
		{
			RequiredComponentAttribute[] attribs = 
				cmpType.GetCustomAttributes(typeof(RequiredComponentAttribute), true).
				Cast<RequiredComponentAttribute>().
				ToArray();

			return attribs.Select(a => a.RequiredComponentType).ToArray();
		}
	}
}
