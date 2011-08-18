using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		private	static	ContentRef<Scene>	current		= ContentRef<Scene>.Null;
		private	static	bool				curAutoGen	= false;
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
		/// Fired when a <see cref="GameObject"/> has been registered in the current Scenes <see cref="Scene.Graph"/>.
		/// </summary>
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectRegistered;
		/// <summary>
		/// Fired when a <see cref="GameObject"/> has been unregistered from the current Scenes <see cref="Scene.Graph"/>.
		/// </summary>
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectUnregistered;
		/// <summary>
		/// Fired when a <see cref="Component"/> has been added to a <see cref="GameObject"/> that is registered in the current Scenes <see cref="Scene.Graph"/>.
		/// </summary>
		public static event EventHandler<ComponentEventArgs> RegisteredObjectComponentAdded;
		/// <summary>
		/// Fired when a <see cref="Component"/> has been removed from a <see cref="GameObject"/> that is registered in the current Scenes <see cref="Scene.Graph"/>.
		/// </summary>
		public static event EventHandler<ComponentEventArgs> RegisteredObjectComponentRemoved;

		private static void OnLeaving()
		{
			if (Leaving != null) Leaving(current, null);
			if (current.ResWeak != null) foreach (GameObject o in current.ResWeak.Graph.ActiveObjects) o.OnDeactivate();
		}
		private static void OnEntered()
		{
			if (current.ResWeak != null) foreach (GameObject o in current.ResWeak.Graph.ActiveObjects) o.OnActivate();
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

		private	GameObjectManager		objectManager	= new GameObjectManager();
		private	ObjectManager<Camera>	cameraManager	= new ObjectManager<Camera>();
		private	RendererManager			rendererManager	= new RendererManager();
		private	OverlayRendererManager	overlayManager	= new OverlayRendererManager();

		/// <summary>
		/// [GET] The Scenes main <see cref="GameObject"/> manager.
		/// </summary>
		public GameObjectManager Graph
		{
			get { return this.objectManager; }
		}
		/// <summary>
		/// [GET] The name of the Scene.
		/// </summary>
		public string Name
		{
			get 
			{ 
				if (this.path == null) return null;
				if (this.path.Length < FileExt.Length) return "";
				return this.path.Substring(0, this.path.Length - FileExt.Length); 
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
			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj)
				obj.Update();
		}
		/// <summary>
		/// Updates the Scene in the editor.
		/// </summary>
		public void EditorUpdate()
		{
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
				throw new ApplicationException("This method may only be used in Editor execution context.");

			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj)
				obj.EditorUpdate();
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
			this.objectManager.RegisterObjDeep(scene.Res.Graph.RootObjects.Select(o => o.Clone()));
		}

		/// <summary>
		/// Enumerates all <see cref="Duality.Resources.Renderer">Renderers</see> that are visible to
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

		private void objectManager_Registered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			Camera cam = e.Object.Camera;
			if (cam != null) this.cameraManager.RegisterObj(cam);

			foreach (Renderer r in e.Object.GetComponents<Renderer>())
				this.rendererManager.RegisterObj(r);
			foreach (ICmpScreenOverlayRenderer r in e.Object.GetComponents<ICmpScreenOverlayRenderer>())
				this.overlayManager.RegisterObj(r as Component);

			if (this.IsCurrent) OnGameObjectRegistered(e);
		}
		private void objectManager_Unregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			Camera cam = e.Object.Camera;
			if (cam != null) this.cameraManager.UnregisterObj(cam);

			foreach (Renderer r in e.Object.GetComponents<Renderer>())
				this.rendererManager.UnregisterObj(r);
			foreach (ICmpScreenOverlayRenderer r in e.Object.GetComponents<ICmpScreenOverlayRenderer>())
				this.overlayManager.UnregisterObj(r as Component);

			if (this.IsCurrent) OnGameObjectUnregistered(e);
		}
		private void objectManager_RegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.RegisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.RegisterObj(e.Component as Renderer);

			if (e.Component is ICmpScreenOverlayRenderer)	this.overlayManager.RegisterObj(e.Component);

			if (this.IsCurrent) OnRegisteredObjectComponentAdded(e);
		}
		private void objectManager_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.UnregisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.UnregisterObj(e.Component as Renderer);

			if (e.Component is ICmpScreenOverlayRenderer)	this.overlayManager.UnregisterObj(e.Component);

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
			base.OnLoaded();
			this.ApplyPrefabLinks();
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.OnLoaded();
		}
		protected override void OnDisposing(bool manually)
		{
			base.OnDisposing(manually);

			if (current.ResWeak == this) current = ContentRef<Scene>.Null;

			GameObject[] obj = this.objectManager.AllObjects.ToArray();
			this.Graph.Clear();
			foreach (GameObject g in obj) g.DisposeLater();
		}
	}
}
