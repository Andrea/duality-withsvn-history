using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Implement this interface in <see cref="Component">Components</see> that require per-frame updates.
	/// </summary>
	public interface ICmpUpdatable
	{
		/// <summary>
		/// Called once per frame in order to update the Component.
		/// </summary>
		void OnUpdate();
	}
	/// <summary>
	/// Implement this interface in C<see cref="Component">Components</see> that require per-frame updates in the editor.
	/// </summary>
	public interface ICmpEditorUpdatable
	{
		/// <summary>
		/// Called once per frame in order to update the Component in the editor.
		/// </summary>
		void OnUpdate();
	}
	/// <summary>
	/// Implement this interface in <see cref="Component">Components</see> that require notifications for other Components 
	/// being added or removed at the same GameObject.
	/// </summary>
	public interface ICmpComponentListener
	{
		/// <summary>
		/// Called whenever another Component has been added to this Components GameObject.
		/// </summary>
		/// <param name="comp">The Component that has been added</param>
		void OnComponentAdded(Component comp);
		/// <summary>
		/// Called whenever another Component is being removed from this Components GameObject.
		/// </summary>
		/// <param name="comp">The Component that is being removed</param>
		void OnComponentRemoving(Component comp);
	}
	/// <summary>
	/// Implement this interface in <see cref="Component">Components</see> that require notification if the location of 
	/// their GameObject inside the scene graph changes.
	/// </summary>
	public interface ICmpGameObjectListener
	{
		/// <summary>
		/// Called whenever this Components GameObjects <see cref="GameObject.Parent"/> has changed.
		/// </summary>
		/// <param name="oldParent">The old parent object.</param>
		/// <param name="newParent">The new parent object.</param>
		void OnGameObjectParentChanged(GameObject oldParent, GameObject newParent);
	}
	/// <summary>
	/// Implement this interface in <see cref="Component">Components</see> that require specific init and shutdown logic.
	/// </summary>
	public interface ICmpInitializable
	{
		/// <summary>
		/// Called in order to initialize the Component in a specific way.
		/// </summary>
		/// <param name="context">The kind of initialization that is intended.</param>
		void OnInit(Component.InitContext context);
		/// <summary>
		/// Called in order to shutdown the Component in a specific way.
		/// </summary>
		/// <param name="context">The kind of shutdown that is intended.</param>
		void OnShutdown(Component.ShutdownContext context);
	}
	/// <summary>
	/// Implement this interface in <see cref="Component">Components</see> that are able to render a screen overlay. 
	/// This is useful for HUD, GUI or debug rendering. Screen overlays are rendered after both regular rendering and 
	/// postprocessing. Unlike regular <see cref="Duality.Components.Renderer">Renderers</see> they do not operate in 
	/// <see cref="Duality.Components.Camera"/>-local space but screen space.
	/// </summary>
	public interface ICmpScreenOverlayRenderer
	{
		/// <summary>
		/// Determines whether or not this screen overlay is visible to the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device">The <see cref="IDrawDevice"/> to which visibility is determined.</param>
		/// <returns>True, if this screen overlay is visible to the <see cref="IDrawDevice"/>. False, if not.</returns>
		bool IsVisible(IDrawDevice device);
		/// <summary>
		/// Draws the screen overlay.
		/// </summary>
		/// <param name="device">The <see cref="IDrawDevice"/> to which the screen overlay is drawn.</param>
		void DrawOverlay(IDrawDevice device);
	}

	/// <summary>
	/// This attribute indicates a <see cref="Component">Components</see> requirement for another Component
	/// of a specific Type, that is attached to the same <see cref="GameObject"/>.
	/// </summary>
	/// <example>
	/// The following code uses a RequiredComponentAttribute to indicate that a <see cref="Duality.Components.SoundEmitter"/>
	/// always needs a <see cref="Duality.Components.Transform"/> available as well.
	/// <code>
	/// [RequiredComponent(typeof(Transform))]
	/// public sealed class SoundEmitter : Component, ICmpUpdatable, ICmpInitializable
	/// {
	///		// ...
	/// }
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class RequiredComponentAttribute : Attribute
	{
		private	Type	cmpType;

		/// <summary>
		/// The Component Type that is required by this Component.
		/// </summary>
		public Type RequiredComponentType
		{
			get { return this.cmpType; }
		}

		public RequiredComponentAttribute(Type cmpType)
		{
			this.cmpType = cmpType;
		}
	}

	/// <summary>
	/// Components are isolated logic units that can independently be added to and removed from <see cref="GameObject">GameObjects</see>.
	/// Each Component has a distinct purpose, thus it is not possible to add multiple Components of the same Type to one GameObject.
	/// Also, a Component may not belong to multiple GameObjects at once.
	/// </summary>
	[Serializable]
	[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	public abstract class Component : IManageableObject
	{
		/// <summary>
		/// Describes the kind of initialization that can be performed on a Component
		/// </summary>
		public enum InitContext
		{
			/// <summary>
			/// A saving process has just finished.
			/// </summary>
			Saved,
			/// <summary>
			/// The Component has been fully loaded.
			/// </summary>
			Loaded,
			/// <summary>
			/// The Component is being activated. This can be the result of <see cref="Active">activating</see> it,
			/// <see cref="GameObject.Active">activating</see> its GameObject, adding itsself or its GameObject
			/// to the <see cref="Duality.Resources.Scene.Current">current Scene</see> or entering a <see cref="Scene"/>
			/// in which this Component is registered.
			/// </summary>
			Activate,
			/// <summary>
			/// The Component has just been added to a GameObject
			/// </summary>
			AddToGameObject
		}
		/// <summary>
		/// Describes the kind of shutdown that can be performed on a Component
		/// </summary>
		public enum ShutdownContext
		{
			/// <summary>
			/// A saving process is about to start
			/// </summary>
			Saving,
			/// <summary>
			/// The Component has been deactivated. This can be the result of <see cref="Active">deactivating</see> it,
			/// <see cref="GameObject.Active">deactivating</see> its GameObject, removing itsself or its GameObject
			/// from the <see cref="Duality.Resources.Scene.Current">current Scene</see> or leaving a <see cref="Scene"/>
			/// in which this Component is registered.
			/// </summary>
			Deactivate,
			/// <summary>
			/// The Component is being removed from its GameObject.
			/// </summary>
			RemovingFromGameObject
		}


		internal	GameObject	gameobj		= null;
		private		bool		disposed	= false;
		private		bool		active		= true;

		
		/// <summary>
		/// [GET / SET] Whether or not the Component is currently active. To return true,
		/// both the Component itsself and its parent GameObject need to be active.
		/// </summary>
		/// <seealso cref="ActiveSingle"/>
		public bool Active
		{
			get { return this.ActiveSingle && this.gameobj != null && this.gameobj.Active; }
			set { this.ActiveSingle = value; }
		}
		/// <summary>
		/// [GET / SET] Whether or not the Component is currently active. Unlike <see cref="Active"/>,
		/// this property ignores parent activation states and depends only on this single Component.
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
		/// <summary>
		/// [GET] Returns whether this Component has been disposed. Disposed Components are not to be used and should
		/// be treated specifically or as null references by your code.
		/// </summary>
		public bool Disposed
		{
			get { return this.disposed; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="GameObject"/> to which this Component belongs.
		/// </summary>
		public GameObject GameObj
		{
			get { return this.gameobj; }
			set
			{
				if (this.gameobj != null) this.gameobj.RemoveComponent(this);
				if (value != null) value.AddComponent(this);
			}
		}


		/// <summary>
		/// Disposes this Component. You usually don't need this - use <see cref="DisposeLater"/> instead.
		/// </summary>
		/// <seealso cref="DisposeLater"/>
		public void Dispose()
		{
			if (!this.disposed)
			{
				// Remove from GameObject
				if (this.gameobj != null) this.gameobj.RemoveComponent(this.GetType());

				this.disposed = true;
			}
		}
		/// <summary>
		/// Schedules this Component for disposal. It is guaranteed to be executed until the next update cycle starts.
		/// </summary>
		/// <seealso cref="Dispose"/>
		public void DisposeLater()
		{
			DualityApp.DisposeLater(this);
		}
		
		/// <summary>
		/// Creates a deep copy of this Component.
		/// </summary>
		/// <returns>A reference to a newly created deep copy of this Component.</returns>
		public Component Clone()
		{
			Component newObj = ReflectionHelper.CreateInstanceOf(this.GetType()) as Component;
			this.CopyTo(newObj);
			return newObj;
		}
		/// <summary>
		/// Deep-copies this Components data to the specified target Component. If source and 
		/// target Component Type do not match, the operation will fail.
		/// </summary>
		/// <param name="target">The target Component to copy to.</param>
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
		/// <summary>
		/// This method Performs the <see cref="CopyTo"/> operation for custom Component Types.
		/// It uses reflection to copy each field that is declared inside a Duality plugin automatically.
		/// However, you may override this method to specify your own behaviour or simply speed things
		/// up a bit by not using Reflection.
		/// </summary>
		/// <param name="target">The target Component where this Components data is copied to.</param>
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

		/// <summary>
		/// Returns whether this Component requires a Component of the specified Type.
		/// </summary>
		/// <param name="requiredType">The Component Type that might be required.</param>
		/// <returns>True, if there is a requirement, false if not</returns>
		public bool RequiresComponent(Type requiredType)
		{
			return RequiresComponent(this.GetType(), requiredType);
		}
		/// <summary>
		/// Returns whether this objects Component requirement is met.
		/// </summary>
		/// <param name="evenWhenRemovingThis">If not null, the specified Component is assumed to be missing.</param>
		/// <returns>True, if the Component requirement is met, false if not.</returns>
		public bool IsComponentRequirementMet(Component evenWhenRemovingThis = null)
		{
			Type[] reqTypes = this.GetRequiredComponents();
			foreach (Type r in reqTypes)
			{
				if (!this.GameObj.GetComponents(r).Any(c => c != evenWhenRemovingThis)) return false;
			}

			return true;
		}
		/// <summary>
		/// Returns whether this objects Component requirement is met assuming a different <see cref="GameObj">parent GameObject</see>
		/// </summary>
		/// <param name="isMetInObj">The specified object is assumed as parent object.</param>
		/// <param name="whenAddingThose">If not null, the specified Components are assumed to be present in the specified parent object.</param>
		/// <returns>True, if the Component requirement is met, false if not.</returns>
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
		/// <summary>
		/// Returns all Component Types this Component requires.
		/// </summary>
		/// <returns>An array of required Component Types.</returns>
		public Type[] GetRequiredComponents()
		{
			return GetRequiredComponents(this.GetType());
		}

		public override string ToString()
		{
			return string.Format("{0} in {1}", this.GetType().Name, this.gameobj != null ? this.gameobj.FullName : "null");
		}

		/// <summary>
		/// Returns whether a Component Type requires another Component Type to work properly.
		/// </summary>
		/// <param name="cmpType">The Component Type that might require another Component Type.</param>
		/// <param name="requiredType">The Component Type that might be required.</param>
		/// <returns>True, if there is a requirement, false if not</returns>
		public static bool RequiresComponent(Type cmpType, Type requiredType)
		{
			RequiredComponentAttribute[] attribs = 
				cmpType.GetCustomAttributes(typeof(RequiredComponentAttribute), true).
				Cast<RequiredComponentAttribute>().
				ToArray();

			return attribs.Any(a => a.RequiredComponentType.IsAssignableFrom(cmpType));
		}
		/// <summary>
		/// Returns all required Component Types of a specified Component Type.
		/// </summary>
		/// <param name="cmpType">The Component Type that might require other Component Types.</param>
		/// <returns>An array of Component Types to require.</returns>
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
