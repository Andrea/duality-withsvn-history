using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Duality.Components;
using Duality.ObjectManagers;

namespace Duality.Resources
{
	[Serializable]
	public sealed class Scene : Resource
	{
		private	static	Scene	current	= new Scene();
		public static Scene Current
		{
			get { return current; }
			set 
			{
				if (current != value)
				{
					if (current != null) OnLeaving();
					current = value;
					if (current != null) OnEntered();
				}
			}
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
		}
		private static void OnEntered()
		{
			if (Entered != null) Entered(current, null);
		}
		private static void OnGameObjectRegistered(ObjectManagerEventArgs<GameObject> args)
		{
			if (GameObjectRegistered != null) GameObjectRegistered(current, args);
		}
		private static void OnGameObjectUnregistered(ObjectManagerEventArgs<GameObject> args)
		{
			if (GameObjectUnregistered != null) GameObjectUnregistered(current, args);
		}
		private static void OnRegisteredObjectComponentAdded(ComponentEventArgs args)
		{
			if (RegisteredObjectComponentAdded != null) RegisteredObjectComponentAdded(current, args);
		}
		private static void OnRegisteredObjectComponentRemoved(ComponentEventArgs args)
		{
			if (RegisteredObjectComponentRemoved != null) RegisteredObjectComponentRemoved(current, args);
		}

		private	GameObjectManager		objectManager	= new GameObjectManager();
		private	ObjectManager<Camera>	cameraManager	= new ObjectManager<Camera>();
		private	RendererManager			rendererManager	= new RendererManager();

		public GameObjectManager Graph
		{
			get { return this.objectManager; }
		}
		public string Name
		{
			get 
			{ 
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
			foreach (GameObject obj in this.objectManager.ActiveObjects)
			{
			    obj.Update();
			}
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

		private void objectManager_Registered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			Camera cam = e.Object.Camera;
			if (cam != null) this.cameraManager.RegisterObj(cam);

			foreach (Renderer r in e.Object.GetComponents<Renderer>())
			{
				this.rendererManager.RegisterObj(r);
			}

			OnGameObjectRegistered(e);
		}
		private void objectManager_Unregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			Camera cam = e.Object.Camera;
			if (cam != null) this.cameraManager.UnregisterObj(cam);

			foreach (Renderer r in e.Object.GetComponents<Renderer>())
			{
				this.rendererManager.UnregisterObj(r);
			}

			OnGameObjectUnregistered(e);
		}
		private void objectManager_RegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.RegisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.RegisterObj(e.Component as Renderer);

			OnRegisteredObjectComponentAdded(e);
		}
		private void objectManager_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (e.Component is Camera)			this.cameraManager.UnregisterObj(e.Component as Camera);
			else if (e.Component is Renderer)	this.rendererManager.UnregisterObj(e.Component as Renderer);

			OnRegisteredObjectComponentRemoved(e);
		}

		protected override void CopyTo(Resource r)
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
	}
}
