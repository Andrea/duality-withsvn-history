using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Components;
using Duality.Resources;

namespace Duality.Components
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public sealed class SoundEmitter : Component, ICmpUpdatable, ICmpInitializable
	{
		[Serializable]
		public class Source
		{
			private	bool				disposed	= false;
			private	ContentRef<Sound>	sound		= ContentRef<Sound>.Null;
			private	bool				looped		= true;
			private	bool				paused		= true;
			private	float				volume		= 1.0f;
			private	float				pitch		= 1.0f;
			private	Vector3				offset		= Vector3.Zero;
			[NonSerializedPrefab]	private	bool			hasBeenPlayed	= false;
			[NonSerialized]			private	SoundInstance	instance		= null;

			public bool Disposed
			{
				get { return this.disposed; }
			}
			public SoundInstance Instance
			{
				get { return this.instance; }
			}
			public ContentRef<Sound> Sound
			{
				get { return this.sound; }
				set { this.sound = value; }
			}
			public bool Looped
			{
				get { return this.looped; }
				set 
				{ 
					if (this.instance != null) this.instance.Looped = value;
					this.looped = value;
				}
			}
			public bool Paused
			{
				get { return this.paused; }
				set 
				{ 
					if (this.instance != null) this.instance.Paused = value;
					this.paused = value;
				}
			}
			public float Volume
			{
				get { return this.volume; }
				set 
				{ 
					if (this.instance != null) this.instance.Volume = value;
					this.volume = value;
				}
			}
			public float Pitch
			{
				get { return this.pitch; }
				set 
				{ 
					if (this.instance != null) this.instance.Pitch = value;
					this.pitch = value;
				}
			}
			public Vector3 Offset
			{
				get { return this.offset; }
				set
				{
					if (this.instance != null) this.instance.Pos = value;
					this.offset = value;
				}
			}

			public Source() {}
			public Source(ContentRef<Sound> snd, bool looped, Vector3 offset)
			{
				this.sound = snd;
				this.looped = looped;
				this.offset = offset;
			}

			public bool Update(SoundEmitter emitter)
			{
				// If the SoundInstance has been disposed, set to null
				if (this.instance != null && this.instance.Disposed) this.instance = null;

				// If there is a SoundInstance playing, but it's the wrong one, stop it
				if (this.instance != null && this.instance.SoundRef != this.sound)
				{
					this.instance.Stop();
					this.instance = null;
				}

				if (this.instance == null)
				{
					// If this Source isn't looped and it HAS been played already, remove it
					if (!this.looped && this.hasBeenPlayed) return false;

					// Play the sound
					this.instance = DualityApp.Sound.PlaySound3D(this.sound, emitter.GameObj);
					this.instance.Pos = this.offset;
					this.instance.Looped = this.looped;
					this.instance.Volume = this.volume;
					this.instance.Paused = this.paused;
					this.hasBeenPlayed = true;
				}

				return true;
			}
		}

		private	List<Source>	sources	= new List<Source>();

		public List<Source> Sources
		{
			get { return this.sources; }
			set { this.sources = value; if (this.sources == null) this.sources = new List<Source>(); }
		}

		public SoundEmitter()
		{
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			SoundEmitter c = target as SoundEmitter;
		}

		void ICmpUpdatable.OnUpdate()
		{
			for (int i = this.sources.Count - 1; i >= 0; i--)
				if (this.sources[i] != null && !this.sources[i].Update(this)) this.sources.RemoveAt(i);
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate || context == ShutdownContext.RemovingFromGameObject)
			{
				for (int i = this.sources.Count - 1; i >= 0; i--)
					if (this.sources[i].Instance != null) this.sources[i].Instance.Stop();
			}
		}
	}
}
