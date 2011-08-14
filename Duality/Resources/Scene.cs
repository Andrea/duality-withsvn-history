using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.Components;
using Duality.ObjectManagers;
using Duality.Serialization;

namespace Duality.Resources
{
	[Serializable]
	public sealed class Scene : Resource
	{
		public new const string FileExt = ".Scene" + Resource.FileExt;

		private	static	ContentRef<Scene>	current		= ContentRef<Scene>.Null;
		private	static	bool				curAutoGen	= false;
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
		public static string CurrentPath
		{
			get { return current.Res != null ? current.Res.Path : current.Path; }
		}

		public static event EventHandler Leaving;
		public static event EventHandler Entered;
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectRegistered;
		public static event EventHandler<ObjectManagerEventArgs<GameObject>> GameObjectUnregistered;
		public static event EventHandler<ComponentEventArgs> RegisteredObjectComponentAdded;
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

		public GameObjectManager Graph
		{
			get { return this.objectManager; }
		}
		public string Name
		{
			get 
			{ 
				if (this.path == null) return null;
				if (this.path.Length < 6) return "";

				string fileName = System.IO.Path.GetFileNameWithoutExtension(this.path);
				return fileName == null ? null : fileName.Substring(0, fileName.Length - 6); 
			}
		}

		public Scene()
		{
			this.objectManager.Registered += this.objectManager_Registered;
			this.objectManager.Unregistered += this.objectManager_Unregistered;
			this.objectManager.RegisteredObjectComponentAdded += this.objectManager_RegisteredObjectComponentAdded;
			this.objectManager.RegisteredObjectComponentRemoved += this.objectManager_RegisteredObjectComponentRemoved;
		}

		public void Render()
		{
			List<Camera> camList = new List<Camera>(this.cameraManager.ActiveObjects);
			// Maybe sort / process list first later on.
			foreach (Camera c in camList) c.Render();
		}
		public void Update()
		{
			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj) obj.Update();
		}
		public void EditorUpdate()
		{
			foreach (GameObject obj in this.objectManager.ActiveObjects)
			{
			    obj.EditorUpdate();
			}
		}

		public void ApplyPrefabLinks()
		{
			PrefabLink.ApplyAllLinks(this.objectManager.AllObjects);
		}
		public void AbandonPrefabLinks()
		{
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.prefabLink = null;
		}

		public IEnumerable<Renderer> QueryVisibleRenderers(IDrawDevice device)
		{
			return this.rendererManager.QueryVisible(device);
		}
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

			OnGameObjectRegistered(e);
		}
		private void objectManager_Unregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			Camera cam = e.Object.Camera;
			if (cam != null) this.cameraManager.UnregisterObj(cam);

			foreach (Renderer r in e.Object.GetComponents<Renderer>())
				this.rendererManager.UnregisterObj(r);
			foreach (ICmpScreenOverlayRenderer r in e.Object.GetComponents<ICmpScreenOverlayRenderer>())
				this.overlayManager.UnregisterObj(r as Component);

			OnGameObjectUnregistered(e);
		}
		private void objectManager_RegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.RegisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.RegisterObj(e.Component as Renderer);

			if (e.Component is ICmpScreenOverlayRenderer)	this.overlayManager.RegisterObj(e.Component);

			OnRegisteredObjectComponentAdded(e);
		}
		private void objectManager_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.UnregisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.UnregisterObj(e.Component as Renderer);

			if (e.Component is ICmpScreenOverlayRenderer)	this.overlayManager.UnregisterObj(e.Component);

			OnRegisteredObjectComponentRemoved(e);
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Scene s = r as Scene;

			foreach (GameObject o in this.objectManager.RootObjects)
				s.objectManager.RegisterObjDeep(o.Clone());
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
