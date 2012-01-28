using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;

using Duality.Components;
using Duality.ObjectManagers;
using Duality.Serialization;

namespace Duality.Resources
{
	/// <summary>
	/// A Scene encapsulates an organized set of <see cref="GameObject">GameObjects</see> and provides
	/// update-, rendering- and maintenance functionality. In Duality, there is always exactly one Scene
	/// <see cref="Scene.Current"/> which represents a level, gamestate or a combination of both, depending
	/// on you own design.
	/// </summary>
	[Serializable]
	public sealed class Scene : Resource
	{
		/// <summary>
		/// A Scene resources file extension.
		/// </summary>
		public new const string FileExt = ".Scene" + Resource.FileExt;

		private	static	World				physicsWorld	= new World(Vector2.Zero);
		private	static	ContentRef<Scene>	current			= ContentRef<Scene>.Null;
		private	static	bool				curAutoGen		= false;
		/// <summary>
		/// [GET] Returns the current physics world.
		/// </summary>
		public static World CurrentPhysics
		{
			get { return physicsWorld; }
		}
		/// <summary>
		/// [GET / SET] The Scene that is currently active i.e. updated and rendered. This is never null.
		/// You may assign null in order to leave the current Scene and enter en empty dummy Scene.
		/// </summary>
		public static Scene Current
		{
			get
			{
				if (!curAutoGen && !current.IsAvailable)
				{
					curAutoGen = true;
					Current = new Scene();
					curAutoGen = false;
				}
				return current.Res;
			}
			set 
			{
				if (current.ResWeak != value)
				{
					OnLeaving();
					current.Res = (value != null) ? value : new Scene();
					OnEntered();
				}
				else
					current.Res = (value != null) ? value : new Scene();
			}
		}
		/// <summary>
		/// [GET] The Resource file path of the current Scene.
		/// </summary>
		public static string CurrentPath
		{
			get { return current.Res != null ? current.Res.Path : current.Path; }
		}

		/// <summary>
		/// Fired just before leaving the current Scene.
		/// </summary>
		public static event EventHandler Leaving;
		/// <summary>
		/// Fired right after entering the (now) current Scene.
		/// </summary>
		public static event EventHandler Entered;
		/// <summary>
		/// Fired when a <see cref="GameObject"/> has been registered in the current Scene.
		/// </summary>
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectRegistered;
		/// <summary>
		/// Fired when a <see cref="GameObject"/> has been unregistered from the current Scene.
		/// </summary>
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectUnregistered;
		/// <summary>
		/// Fired when a <see cref="Component"/> has been added to a <see cref="GameObject"/> that is registered in the current Scene.
		/// </summary>
		public static event EventHandler<ComponentEventArgs> RegisteredObjectComponentAdded;
		/// <summary>
		/// Fired when a <see cref="Component"/> has been removed from a <see cref="GameObject"/> that is registered in the current Scene.
		/// </summary>
		public static event EventHandler<ComponentEventArgs> RegisteredObjectComponentRemoved;

		private static void OnLeaving()
		{
			if (Leaving != null) Leaving(current, null);
			if (current.ResWeak != null)
			{
				foreach (GameObject o in current.ResWeak.ActiveObjects) o.OnDeactivate();
				physicsWorld.Clear();
			}
		}
		private static void OnEntered()
		{
			if (current.ResWeak != null)
			{
				physicsWorld.Gravity = current.ResWeak.GlobalGravity;
				foreach (GameObject o in current.ResWeak.ActiveObjects) o.OnActivate();
			}
			if (Entered != null) Entered(current, null);
		}
		private static void OnGameObjectRegistered(ObjectManagerEventArgs<GameObject> args)
		{
			args.Object.OnActivate();
			if (GameObjectRegistered != null) GameObjectRegistered(current, args);
		}
		private static void OnGameObjectUnregistered(ObjectManagerEventArgs<GameObject> args)
		{
			if (GameObjectUnregistered != null) GameObjectUnregistered(current, args);
			args.Object.OnDeactivate();
		}
		private static void OnRegisteredObjectComponentAdded(ComponentEventArgs args)
		{
			if (args.Component.Active)
			{
				ICmpInitializable cInit = args.Component as ICmpInitializable;
				if (cInit != null) cInit.OnInit(Component.InitContext.Activate);
			}
			if (RegisteredObjectComponentAdded != null) RegisteredObjectComponentAdded(current, args);
		}
		private static void OnRegisteredObjectComponentRemoved(ComponentEventArgs args)
		{
			if (RegisteredObjectComponentRemoved != null) RegisteredObjectComponentRemoved(current, args);
			if (args.Component.Active)
			{
				ICmpInitializable cInit = args.Component as ICmpInitializable;
				if (cInit != null) cInit.OnShutdown(Component.ShutdownContext.Deactivate);
			}
		}

		private	Vector2				globalGravity	= Vector2.UnitY * 20.0f;
		private	GameObjectManager	objectManager	= new GameObjectManager();
		[NonSerialized]	private	ObjectManager<Camera>	cameraManager	= new ObjectManager<Camera>();
		[NonSerialized]	private	RendererManager			rendererManager	= new RendererManager();
		[NonSerialized]	private	OverlayRendererManager	overlayManager	= new OverlayRendererManager();

		/// <summary>
		/// [GET] Enumerates all registered objects.
		/// </summary>
		public IEnumerable<GameObject> AllObjects
		{
			get { return this.objectManager.AllObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all registered objects that are currently active.
		/// </summary>
		public IEnumerable<GameObject> ActiveObjects
		{
			get { return this.objectManager.ActiveObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all root GameObjects, i.e. all GameObjects without a parent object.
		/// </summary>
		public IEnumerable<GameObject> RootObjects
		{
			get { return this.objectManager.RootObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all <see cref="RootObjects"/> that are currently active.
		/// </summary>
		public IEnumerable<GameObject> ActiveRootObjects
		{
			get { return this.objectManager.ActiveRootObjects; }
		}
		/// <summary>
		/// [GET] Enumerates the Scenes <see cref="Camera"/> objects.
		/// </summary>
		public IEnumerable<Camera> Cameras
		{
			get { return this.cameraManager.AllObjects; }
		}
		/// <summary>
		/// [GET] Enumerates the Scenes <see cref="Renderer"/> objects.
		/// </summary>
		public IEnumerable<Renderer> Renderers
		{
			get { return this.rendererManager.AllObjects; }
		}
		/// <summary>
		/// [GET] Enumerates the Scenes <see cref="ICmpScreenOverlayRenderer"/> objects.
		/// </summary>
		public IEnumerable<ICmpScreenOverlayRenderer> OverlayRenderers
		{
			get { return this.overlayManager.AllObjects.OfType<ICmpScreenOverlayRenderer>(); }
		}
		/// <summary>
		/// [GET / SET] Global gravity force that is applied to all objects that obey the laws of physics.
		/// </summary>
		public Vector2 GlobalGravity
		{
			get { return this.globalGravity; }
			set
			{
				this.globalGravity = value;
				if (this.IsCurrent) physicsWorld.Gravity = value;
			}
		}
		/// <summary>
		/// [GET] Returns whether this Scene is <see cref="Scene.Current"/>.
		/// </summary>
		public bool IsCurrent
		{
			get { return current.ResWeak == this; }
		}

		/// <summary>
		/// Creates a new, empty scene which does not contain any <see cref="GameObject">GameObjects</see>.
		/// </summary>
		public Scene()
		{
			this.objectManager.Registered += this.objectManager_Registered;
			this.objectManager.Unregistered += this.objectManager_Unregistered;
			this.objectManager.RegisteredObjectComponentAdded += this.objectManager_RegisteredObjectComponentAdded;
			this.objectManager.RegisteredObjectComponentRemoved += this.objectManager_RegisteredObjectComponentRemoved;
		}

		/// <summary>
		/// Renders the Scene
		/// </summary>
		public void Render()
		{
			if (!this.IsCurrent) throw new InvalidOperationException("Can't render non-current Scene!");

			Camera[] cams = this.cameraManager.ActiveObjects.ToArray();
			// Maybe sort / process list first later on.
			foreach (Camera c in cams)
				c.Render();
		}
		/// <summary>
		/// Updates the Scene
		/// </summary>
		public void Update()
		{
			if (!this.IsCurrent) throw new InvalidOperationException("Can't update non-current Scene!");

			Performance.timeUpdatePhysics.BeginMeasure();
			physicsWorld.Step(Time.TimeMult * Time.SPFMult);
			Performance.timeUpdatePhysics.EndMeasure();

			Performance.timeUpdateScene.BeginMeasure();
			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj)
				obj.Update();
			Performance.timeUpdateScene.EndMeasure();
		}
		/// <summary>
		/// Updates the Scene in the editor.
		/// </summary>
		public void EditorUpdate()
		{
			if (!this.IsCurrent) throw new InvalidOperationException("Can't update non-current Scene!");

			Performance.timeUpdateScene.BeginMeasure();
			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj)
				obj.EditorUpdate();
			Performance.timeUpdateScene.EndMeasure();
		}

		/// <summary>
		/// Applies all <see cref="Duality.Resources.PrefabLink">PrefabLinks</see> contained withing this
		/// Scenes <see cref="GameObject">GameObjects</see>.
		/// </summary>
		public void ApplyPrefabLinks()
		{
			PrefabLink.ApplyAllLinks(this.objectManager.AllObjects);
		}
		/// <summary>
		/// Breaks all <see cref="Duality.Resources.PrefabLink">PrefabLinks</see> contained withing this
		/// Scenes <see cref="GameObject">GameObjects</see>.
		/// </summary>
		public void BreakPrefabLinks()
		{
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.BreakPrefabLink();
		}

		/// <summary>
		/// Clears the Scene, unregistering all GameObjects. This does not <see cref="GameObject.Dispose">dispose</see> them.
		/// </summary>
		public void Clear()
		{
			this.objectManager.Clear();
		}
		/// <summary>
		/// Appends a cloned version of the specified Scenes contents to this Scene.
		/// </summary>
		/// <param name="scene">The source Scene.</param>
		public void Append(ContentRef<Scene> scene)
		{
			if (!scene.IsAvailable) return;
			this.objectManager.RegisterObj(scene.Res.RootObjects.Select(o => o.Clone()));
		}

		/// <summary>
		/// Registers a GameObject and all of its children.
		/// </summary>
		/// <param name="obj"></param>
		public void RegisterObj(GameObject obj)
		{
			this.objectManager.RegisterObj(obj);
		}
		/// <summary>
		/// Registers a set of GameObjects ad all of their children.
		/// </summary>
		/// <param name="objEnum"></param>
		public void RegisterObj(IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject obj in objEnum.ToArray()) this.RegisterObj(obj);
		}
		/// <summary>
		/// Unregisters a GameObject and all of its children
		/// </summary>
		/// <param name="obj"></param>
		public void UnregisterObj(GameObject obj)
		{
			this.objectManager.UnregisterObj(obj);
		}
		/// <summary>
		/// Unregisters a set of GameObjects ad all of their children.
		/// </summary>
		/// <param name="objEnum"></param>
		public void UnregisterObj(IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject obj in objEnum.ToArray()) this.UnregisterObj(obj);
		}

		/// <summary>
		/// Enumerates all <see cref="Duality.Components.Renderer">Renderers</see> that are visible to
		/// the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public IEnumerable<Renderer> QueryVisibleRenderers(IDrawDevice device)
		{
			return this.rendererManager.QueryVisible(device);
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.ICmpScreenOverlayRenderer">Screen Overlays</see> that are visible to
		/// the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public IEnumerable<ICmpScreenOverlayRenderer> QueryVisibleOverlayRenderers(IDrawDevice device)
		{
			return this.overlayManager.QueryVisible(device);
		}

		private void AddToManagers(GameObject obj)
		{
			Camera cam = obj.Camera;
			if (cam != null) this.cameraManager.RegisterObj(cam);

			foreach (Renderer r in obj.GetComponents<Renderer>())
				this.rendererManager.RegisterObj(r);
			foreach (ICmpScreenOverlayRenderer r in obj.GetComponents<ICmpScreenOverlayRenderer>())
				this.overlayManager.RegisterObj(r as Component);
		}
		private void AddToManagers(Component cmp)
		{
			if (cmp is Camera)						this.cameraManager.RegisterObj(cmp as Camera);
			if (cmp is Renderer)					this.rendererManager.RegisterObj(cmp as Renderer);
			if (cmp is ICmpScreenOverlayRenderer)	this.overlayManager.RegisterObj(cmp);
		}
		private void RemoveFromManagers(GameObject obj)
		{
			Camera cam = obj.Camera;
			if (cam != null) this.cameraManager.UnregisterObj(cam);

			foreach (Renderer r in obj.GetComponents<Renderer>())
				this.rendererManager.UnregisterObj(r);
			foreach (ICmpScreenOverlayRenderer r in obj.GetComponents<ICmpScreenOverlayRenderer>())
				this.overlayManager.UnregisterObj(r as Component);
		}
		private void RemoveFromManagers(Component cmp)
		{
			if (cmp is Camera)						this.cameraManager.UnregisterObj(cmp as Camera);
			if (cmp is Renderer)					this.rendererManager.UnregisterObj(cmp as Renderer);
			if (cmp is ICmpScreenOverlayRenderer)	this.overlayManager.UnregisterObj(cmp);
		}
		private void RebuildManagers()
		{
			this.cameraManager.Clear();
			this.rendererManager.Clear();
			this.overlayManager.Clear();

			foreach (GameObject obj in this.objectManager.AllObjects)
				this.AddToManagers(obj);
		}

		private void objectManager_Registered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			this.AddToManagers(e.Object);
			if (this.IsCurrent) OnGameObjectRegistered(e);
		}
		private void objectManager_Unregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			this.RemoveFromManagers(e.Object);
			if (this.IsCurrent) OnGameObjectUnregistered(e);
		}
		private void objectManager_RegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			this.AddToManagers(e.Component);
			if (this.IsCurrent) OnRegisteredObjectComponentAdded(e);
		}
		private void objectManager_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			this.RemoveFromManagers(e.Component);
			if (this.IsCurrent) OnRegisteredObjectComponentRemoved(e);
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Scene s = r as Scene;
			s.Clear();
			s.Append(this);
		}
		protected override void OnSaving()
		{
			base.OnSaving();
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.OnSaving();
		}
		protected override void OnSaved()
		{
			base.OnSaved();
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.OnSaved();
		}
		protected override void OnLoaded()
		{
			this.RebuildManagers();

			base.OnLoaded();

			this.ApplyPrefabLinks();
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Game) this.BreakPrefabLinks();

			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.OnLoaded();
		}
		protected override void OnDisposing(bool manually)
		{
			base.OnDisposing(manually);

			if (current.ResWeak == this) Current = null;

			GameObject[] obj = this.objectManager.AllObjects.ToArray();
			this.objectManager.Clear();
			foreach (GameObject g in obj) g.DisposeLater();
		}
	}
}
