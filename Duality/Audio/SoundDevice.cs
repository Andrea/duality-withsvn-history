using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using AudioContext = OpenTK.Audio.AudioContext;
using OpenTK.Audio.OpenAL;

using Duality.Resources;
using Duality.Components;

namespace Duality
{
	public class SoundDevice : IDisposable
	{
		private	bool					disposed		= false;
		private	AudioContext			context			= null;
		private	GameObject				soundListener	= null;
		private	Stack<int>				alSourcePool	= new Stack<int>();
		private	List<SoundInstance>		sounds			= new List<SoundInstance>();
		private	SoundBudgetQueue		budgetMusic		= new SoundBudgetQueue();
		private	SoundBudgetQueue		budgetAmbient	= new SoundBudgetQueue();
		private	Dictionary<Sound,int>	resPlaying		= new Dictionary<Sound,int>();
		private	int						maxAlSources	= 0;
		private	int						numPlaying2D	= 0;
		private	int						numPlaying3D	= 0;
		private	bool					mute			= false;


		public AudioContext Context
		{
			get { return this.context; }
		}
		public SoundBudgetQueue Ambient
		{
			get { return this.budgetAmbient; }
		}
		public SoundBudgetQueue Music
		{
			get { return this.budgetMusic; }
		}

		public GameObject Listener
		{
			get { return this.soundListener; }
			set { this.soundListener = value; }
		}
		public Vector3 ListenerPos
		{
			get { return (this.soundListener != null && this.soundListener.Transform != null) ? this.soundListener.Transform.Pos : Vector3.Zero; }
		}
		public Vector3 ListenerVel
		{
			get { return (this.soundListener != null && this.soundListener.Transform != null) ? this.soundListener.Transform.Vel : Vector3.Zero; }
		}
		public float ListenerAngle
		{
			get { return (this.soundListener != null && this.soundListener.Transform != null) ? this.soundListener.Transform.Angle : 0.0f; }
		}
		
		public bool Mute
		{
			get { return this.mute; }
			set { this.mute = value; }
		}
		public float DefaultMinDist
		{
			get { return 350.0f; }
		}
		public float DefaultMaxDist
		{
			get { return 3500.0f; }
		}
		public int MaxOpenALSources
		{
			get { return this.maxAlSources; }
		}
		public int NumPlaying2D
		{
			get { return this.numPlaying2D; }
		}
		public int NumPlaying3D
		{
			get { return this.numPlaying3D; }
		}
		public int NumAvailable
		{
			get { return this.alSourcePool.Count; }
		}
		public IEnumerable<SoundInstance> Playing
		{
			get { return this.sounds; }
		}


		public SoundDevice()
		{
			this.context = new AudioContext();

			// Generate OpenAL source pool
			int newSrc;
			while (true)
			{
				newSrc = AL.GenSource();
				if (!this.CheckErrors(true))
					this.alSourcePool.Push(newSrc);
				else
					break;
			}
			this.maxAlSources = this.alSourcePool.Count;

			Log.Core.Write(
				"OpenAL initialized. {0} sound sources available",
				this.alSourcePool.Count);
		}
		public void Init()
		{
			AL.DistanceModel(ALDistanceModel.LinearDistanceClamped);
			AL.DopplerFactor(DualityApp.AppData.SoundDopplerFactor);
			AL.SpeedOfSound(DualityApp.AppData.SpeedOfSound);
		}

		public int GetNumPlaying(ContentRef<Sound> snd)
		{
			int curNumSoundRes;
			if (!snd.IsAvailable || !this.resPlaying.TryGetValue(snd.Res, out curNumSoundRes)) curNumSoundRes = 0;
			return curNumSoundRes;
		}
		public int RequestAlSource()
		{
			if (this.alSourcePool.Count == 0) return SoundInstance.AlSource_NotAvailable;
			return this.alSourcePool.Pop();
		}
		public void RegisterPlaying(ContentRef<Sound> snd, bool is3D)
		{
			if (is3D)	this.numPlaying3D++;
			else		this.numPlaying2D++;

			if (snd.IsAvailable)
			{
				if (!this.resPlaying.ContainsKey(snd.Res))
					this.resPlaying.Add(snd.Res, 1);
				else
					this.resPlaying[snd.Res]++;
			}
		}
		public void FreeAlSource(int alSource)
		{
			this.alSourcePool.Push(alSource);
		}
		public void UnregisterPlaying(ContentRef<Sound> snd, bool is3D)
		{
			if (is3D)				this.numPlaying3D--;
			else					this.numPlaying2D--;
			if (snd.IsAvailable)	this.resPlaying[snd.Res]--;
		}
		
		public void Update()
		{
			this.budgetAmbient.Update();
			this.budgetMusic.Update();

			this.UpdateListener();

			for (int i = this.sounds.Count - 1; i >= 0; i--)
			{
				this.sounds[i].Update();
				if (this.sounds[i].Disposed) this.sounds.RemoveAt(i);
			}
			this.CheckErrors();

			this.sounds.Sort(delegate(SoundInstance obj1, SoundInstance obj2) { return obj2.Priority - obj1.Priority; });
		}
		private void UpdateListener()
		{
			if (this.soundListener != null && (this.soundListener.Disposed || !this.soundListener.Active)) this.soundListener = null;

			// If no listener is defined, search one
			if (this.soundListener == null)
			{
				this.soundListener = Scene.Current.Graph.AllObjects.GetComponents<SoundListener>(true).GameObject().FirstOrDefault();
			}

			float[] orientation = new float[6];
			orientation[0] = 0.0f;	// forward vector x value
			orientation[1] = 0.0f;	// forward vector y value
			orientation[2] = -1.0f;	// forward vector z value
			orientation[5] = 0.0f;	// up vector z value
			Vector3 listenerPos = this.ListenerPos;
			Vector3 listenerVel = this.ListenerVel;
			float listenerAngle = this.ListenerAngle;
			AL.Listener(ALListener3f.Position, listenerPos.X, -listenerPos.Y, -listenerPos.Z);
			AL.Listener(ALListener3f.Velocity, listenerVel.X, -listenerVel.Y, -listenerVel.Z);
			orientation[3] = MathF.Sin(listenerAngle);	// up vector x value
			orientation[4] = MathF.Cos(listenerAngle);	// up vector y value
			AL.Listener(ALListenerfv.Orientation, ref orientation);
			AL.Listener(ALListenerf.Gain, this.mute ? 0.0f : 1.0f);
		}
		
		public SoundInstance PlaySound(ContentRef<Sound> snd)
		{
			SoundInstance inst = new SoundInstance(snd);
			this.sounds.Add(inst);
			return inst;
		}
		public SoundInstance PlaySound3D(ContentRef<Sound> snd, Vector3 pos)
		{
			SoundInstance inst = new SoundInstance(snd, pos);
			this.sounds.Add(inst);
			return inst;
		}
		public SoundInstance PlaySound3D(ContentRef<Sound> snd, GameObject attachTo)
		{
			SoundInstance inst = new SoundInstance(snd, attachTo);
			this.sounds.Add(inst);
			return inst;
		}
		public SoundInstance PlaySound3D(ContentRef<Sound> snd, GameObject attachTo, Vector3 relativePos)
		{
			SoundInstance inst = new SoundInstance(snd, attachTo);
			inst.Pos = relativePos;
			this.sounds.Add(inst);
			return inst;
		}

		~SoundDevice()
		{
			this.Dispose(false);
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool manually)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.OnDisposed(manually);
			}
		}
		protected virtual void OnDisposed(bool manually)
		{
			foreach (SoundInstance inst in this.sounds) inst.Dispose();
			this.sounds.Clear();

			ContentProvider.UnregisterAllContent<Sound>();

			this.context.Dispose();
			this.context = null;
		}

		public bool CheckErrors(bool silent = false)
		{
			ALError error;
			bool found = false;
			while ((error = AL.GetError()) != ALError.NoError)
			{
				if (!silent)
				{
					Log.Core.WriteError(
						"Internal OpenAL error, code {0} at {1}", 
						error,
						Log.CurrentMethod(1));
				}
				found = true;
			}
			if (found && !silent && System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
			return found;
		}
	}
}
