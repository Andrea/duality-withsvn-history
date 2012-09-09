﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenTK;
using FarseerPhysics.Dynamics;

using Duality.EditorHints;
using Duality.Components;
using Duality.ObjectManagers;

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
		private const float PhysicsAccStart = Time.MsPFMult;

		private	static	World				physicsWorld	= new World(Vector2.Zero);
		private	static	float				physicsAcc		= 0.0f;
		private	static	bool				physicsLowFps	= false;
		private	static	ContentRef<Scene>	current			= ContentRef<Scene>.Null;
		private	static	bool				curAutoGen		= false;

		/// <summary>
		/// [GET] When using fixed-timestep physics, the alpha value [0.0 - 1.0] indicates how
		/// complete the next step is. This is used for linear interpolation inbetween fixed physics steps.
		/// </summary>
		public static float PhysicsAlpha
		{
			get { return physicsAcc / Time.MsPFMult; }
		}
		/// <summary>
		/// [GET] Is fixed-timestep physics calculation currently active?
		/// </summary>
		public static bool PhysicsFixedTime
		{
			get { return DualityApp.AppData.PhysicsFixedTime && !physicsLowFps; }
		}
		/// <summary>
		/// [GET] Returns the current physics world.
		/// </summary>
		public static World PhysicsWorld
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
					if (!current.IsExplicitNull) 
						OnLeaving();

					current.Res = value ?? new Scene();

					OnEntered();
				}
				else
					current.Res = value ?? new Scene();
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
				ResetPhysics();
			}
		}
		private static void OnEntered()
		{
			if (current.ResWeak != null)
			{
				ResetPhysics();
				physicsWorld.Gravity = PhysicsConvert.ToPhysicalUnit(current.ResWeak.GlobalGravity / Time.SPFMult);
				// When in the editor, apply prefab links
				if (DualityApp.ExecEnvironment == DualityApp.ExecutionEnvironment.Editor)
					current.ResWeak.ApplyPrefabLinks();
				// Activate GameObjects
				foreach (GameObject o in current.ResWeak.ActiveObjects)
					o.OnActivate();
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
			if (args.Component.Active)
			{
				ICmpInitializable cInit = args.Component as ICmpInitializable;
				if (cInit != null) cInit.OnShutdown(Component.ShutdownContext.Deactivate);
			}
			if (RegisteredObjectComponentRemoved != null) RegisteredObjectComponentRemoved(current, args);
		}

		private	Vector2				globalGravity	= Vector2.UnitY * 33.0f;
		private	GameObjectManager	objectManager	= new GameObjectManager();
		[NonSerialized]	private	ObjectManager<Camera>	cameraManager	= new ObjectManager<Camera>();
		[NonSerialized]	private	RendererManager			rendererManager	= new RendererManager();

		/// <summary>
		/// [GET] Enumerates all registered objects.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<GameObject> AllObjects
		{
			get { return this.objectManager.AllObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all registered objects that are currently active.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<GameObject> ActiveObjects
		{
			get { return this.objectManager.ActiveObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all root GameObjects, i.e. all GameObjects without a parent object.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<GameObject> RootObjects
		{
			get { return this.objectManager.RootObjects; }
		}
		/// <summary>
		/// [GET] Enumerates all <see cref="RootObjects"/> that are currently active.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<GameObject> ActiveRootObjects
		{
			get { return this.objectManager.ActiveRootObjects; }
		}
		/// <summary>
		/// [GET] Enumerates the Scenes <see cref="Camera"/> objects.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<Camera> Cameras
		{
			get { return this.cameraManager.AllObjects; }
		}
		/// <summary>
		/// [GET] Enumerates the Scenes <see cref="Renderer"/> objects.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public IEnumerable<ICmpRenderer> Renderers
		{
			get { return this.rendererManager.AllObjects.OfType<ICmpRenderer>(); }
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
				if (this.IsCurrent)
				{
					physicsWorld.Gravity = PhysicsConvert.ToPhysicalUnit(value / Time.SPFMult);
					foreach (Body b in physicsWorld.BodyList)
					{
						if (b.IgnoreGravity || b.BodyType != BodyType.Dynamic) continue;
						b.Awake = true;
					}
				}
			}
		}
		/// <summary>
		/// [GET] Returns whether this Scene is <see cref="Scene.Current"/>.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public bool IsCurrent
		{
			get { return current.ResWeak == this; }
		}
		/// <summary>
		/// [GET] Returns whether this Scene is completely empty.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public bool IsEmpty
		{
			get { return !this.objectManager.AllObjects.Any(); }
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
		internal void Render()
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
		internal void Update()
		{
			if (!this.IsCurrent) throw new InvalidOperationException("Can't update non-current Scene!");

			// Update physics
			bool physUpdate = false;
			float physBegin = Time.MainTimer;
			if (Scene.PhysicsFixedTime)
			{
				physicsAcc += Time.MsPFMult * Time.TimeMult;
				int iterations = 0;
				if (physicsAcc >= Time.MsPFMult)
				{
					Performance.timeUpdatePhysics.BeginMeasure();
					float timeUpdateBegin = Time.MainTimer;
					while (physicsAcc >= Time.MsPFMult)
					{
						// Catch up on updating progress
						FarseerPhysics.Settings.VelocityThreshold = PhysicsConvert.ToPhysicalUnit(DualityApp.AppData.PhysicsVelocityThreshold / Time.SPFMult);
						physicsWorld.Step(Time.SPFMult);
						physicsAcc -= Time.MsPFMult;
						iterations++;
							
						float timeSpent = Time.MainTimer - timeUpdateBegin;
						if (timeSpent >= Time.MsPFMult * 10.0f) break; // Emergency exit
					}
					physUpdate = true;
					Performance.timeUpdatePhysics.EndMeasure();
				}
			}
			else
			{
				Performance.timeUpdatePhysics.BeginMeasure();
				FarseerPhysics.Settings.VelocityThreshold = PhysicsConvert.ToPhysicalUnit(Time.TimeMult * DualityApp.AppData.PhysicsVelocityThreshold / Time.SPFMult);
				physicsWorld.Step(Time.TimeMult * Time.SPFMult);
				physicsAcc = PhysicsAccStart;
				physUpdate = true;
				Performance.timeUpdatePhysics.EndMeasure();
			}
			float physTime = Time.MainTimer - physBegin;

			// Apply Farseers internal measurements to Duality
			if (physUpdate)
			{
				Performance.timeUpdatePhysicsAddRemove.Set(1000.0f * physicsWorld.AddRemoveTime / System.Diagnostics.Stopwatch.Frequency);
				Performance.timeUpdatePhysicsContacts.Set(1000.0f * physicsWorld.ContactsUpdateTime / System.Diagnostics.Stopwatch.Frequency);
				Performance.timeUpdatePhysicsContinous.Set(1000.0f * physicsWorld.ContinuousPhysicsTime / System.Diagnostics.Stopwatch.Frequency);
				Performance.timeUpdatePhysicsController.Set(1000.0f * physicsWorld.ControllersUpdateTime / System.Diagnostics.Stopwatch.Frequency);
				Performance.timeUpdatePhysicsSolve.Set(1000.0f * physicsWorld.SolveUpdateTime / System.Diagnostics.Stopwatch.Frequency);
			}

			// Update low fps physics state
			if (!physicsLowFps)
				physicsLowFps = Time.LastDelta > Time.MsPFMult && physTime > Time.LastDelta * 0.85f;
			else
				physicsLowFps = !(Time.LastDelta < Time.MsPFMult * 0.9f || physTime < Time.LastDelta * 0.6f);

			Performance.timeUpdateScene.BeginMeasure();
			GameObject[] activeObj = this.objectManager.ActiveObjects.ToArray();
			foreach (GameObject obj in activeObj)
				obj.Update();
			Performance.timeUpdateScene.EndMeasure();
		}
		/// <summary>
		/// Updates the Scene in the editor.
		/// </summary>
		internal void EditorUpdate()
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
		public IEnumerable<ICmpRenderer> QueryVisibleRenderers(IDrawDevice device)
		{
			return this.rendererManager.QueryVisible(device);
		}

		private void AddToManagers(GameObject obj)
		{
			Camera cam = obj.Camera;
			if (cam != null) this.cameraManager.RegisterObj(cam);

			foreach (ICmpRenderer r in obj.GetComponents<ICmpRenderer>())
				this.rendererManager.RegisterObj(r as Component);
		}
		private void AddToManagers(Component cmp)
		{
			if (cmp is Camera)			this.cameraManager.RegisterObj(cmp as Camera);
			if (cmp is ICmpRenderer)	this.rendererManager.RegisterObj(cmp);
		}
		private void RemoveFromManagers(GameObject obj)
		{
			Camera cam = obj.Camera;
			if (cam != null) this.cameraManager.UnregisterObj(cam);

			foreach (ICmpRenderer r in obj.GetComponents<ICmpRenderer>())
				this.rendererManager.UnregisterObj(r as Component);
		}
		private void RemoveFromManagers(Component cmp)
		{
			if (cmp is Camera)			this.cameraManager.UnregisterObj(cmp as Camera);
			if (cmp is ICmpRenderer)	this.rendererManager.UnregisterObj(cmp);
		}
		private void RebuildManagers()
		{
			this.cameraManager.Clear();
			this.rendererManager.Clear();

			foreach (GameObject obj in this.objectManager.AllObjects)
				this.AddToManagers(obj);
		}

		private static void ResetPhysics()
		{
			physicsLowFps = false;
			physicsAcc = PhysicsAccStart;
		}
		/// <summary>
		/// Awakes all currently existing physical objects.
		/// </summary>
		public static void AwakePhysics()
		{
			foreach (Body b in physicsWorld.BodyList)
				b.Awake = true;
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

		protected override void OnCopyTo(Resource r, Duality.Cloning.CloneProvider provider)
		{
			base.OnCopyTo(r, provider);
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
			foreach (GameObject obj in this.objectManager.AllObjects)
				obj.PerformSanitaryCheck();

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
