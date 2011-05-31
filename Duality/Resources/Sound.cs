using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using OpenTK.Audio.OpenAL;

using Duality.OggVorbis;

namespace Duality.Resources
{
	[Serializable]
	public class Sound : Resource
	{
		public new const string FileExt = ".Sound" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Sound:";
		public const string ContentPath_Beep		= VirtualContentPath + "Beep";
		public const string ContentPath_DroneLoop	= VirtualContentPath + "DroneLoop";
		public const string ContentPath_LogoJingle	= VirtualContentPath + "LogoJingle";

		public static ContentRef<Sound> Beep		{ get; private set; }
		public static ContentRef<Sound> DroneLoop	{ get; private set; }
		public static ContentRef<Sound> LogoJingle	{ get; private set; }

		internal static void InitDefaultContent()
		{
			Sound tmp;

			tmp = new Sound(AudioData.Beep); tmp.path = ContentPath_Beep;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Sound(AudioData.DroneLoop); tmp.path = ContentPath_DroneLoop;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new Sound(AudioData.LogoJingle); tmp.path = ContentPath_LogoJingle;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Beep		= ContentProvider.RequestContent<Sound>(ContentPath_Beep);
			DroneLoop	= ContentProvider.RequestContent<Sound>(ContentPath_DroneLoop);
			LogoJingle	= ContentProvider.RequestContent<Sound>(ContentPath_LogoJingle);
		}
		

		public const int AlBuffer_NotAvailable = 0;
		public const int AlBuffer_StreamMe = -1;


		private	bool		forceStream		= false;
		private	int			maxInstances	= 5;
		private	float		minDistFactor	= 1.0f;
		private	float		maxDistFactor	= 1.0f;
		private	float		volFactor		= 1.0f;
		private	float		pitchFactor		= 1.0f;
		private	float		fadeOutAt		= 0.0f;
		private	float		fadeOutTime		= 0.0f;
		private	SoundType	type			= SoundType.EffectWorld;
		private	ContentRef<AudioData>	audioData	= ContentRef<AudioData>.Null;
		[NonSerialized]	private	int		alBuffer	= AlBuffer_NotAvailable;

		public int AlBuffer
		{
			get { return this.alBuffer; }
		}
		public bool IsStreamed
		{
			get { return this.alBuffer == AlBuffer_StreamMe; }
		}
		public ContentRef<AudioData> Data
		{
			get { return this.audioData; }
			set { this.audioData = value; this.ReloadData(); }
		}
		public SoundType Type
		{
			get { return this.type; }
			set { this.type = value; }
		}
		public bool ForceStream
		{
			get { return this.forceStream; }
			set { this.forceStream = value; this.ReloadData(); }
		}
		public int MaxInstances
		{
			get { return this.maxInstances; }
			set { this.maxInstances = value; }
		}
		public float VolumeFactor
		{
			get { return this.volFactor; }
			set { this.volFactor = value; }
		}
		public float PitchFactor
		{
			get { return this.pitchFactor; }
			set { this.pitchFactor = value; }
		}
		public float FadeOutAt
		{
			get { return this.fadeOutAt; }
			set { this.fadeOutAt = value; }
		}
		public float FadeOutTime
		{
			get { return this.fadeOutTime; }
			set { this.fadeOutTime = value; }
		}
		public float MinDistFactor
		{
			get { return this.minDistFactor; }
			set { this.minDistFactor = value; }
		}
		public float MaxDistFactor
		{
			get { return this.maxDistFactor; }
			set { this.maxDistFactor = value; }
		}
		public float MinDist
		{
			get { return DualityApp.Sound.DefaultMinDist * this.minDistFactor; }
			set { this.minDistFactor = value / DualityApp.Sound.DefaultMinDist; }
		}
		public float MaxDist
		{
			get { return DualityApp.Sound.DefaultMaxDist * this.maxDistFactor; }
			set { this.maxDistFactor = value / DualityApp.Sound.DefaultMaxDist; }
		}

		public Sound() {}
		public Sound(ContentRef<AudioData> baseData)
		{
			this.LoadData(baseData);
		}

		public void ReloadData()
		{
			this.LoadData(this.audioData);
		}
		public void LoadData(ContentRef<AudioData> audioData)
		{
			this.audioData = audioData;

			// No AudioData available
			if (!this.audioData.IsAvailable)
			{
				if (this.alBuffer > AlBuffer_NotAvailable) AL.DeleteBuffer(this.alBuffer);
				this.alBuffer = AlBuffer_NotAvailable;
				return;
			}

			bool stream = this.forceStream ? true : this.audioData.Res.OggVorbisData.Length > 1024 * 100;

			// Streamed Audio
			if (stream)
			{
				if (this.alBuffer > AlBuffer_NotAvailable) AL.DeleteBuffer(this.alBuffer);
				this.alBuffer = AlBuffer_StreamMe;
			}
			// Non-Streamed Audio
			else
			{
				if (this.alBuffer <= AlBuffer_NotAvailable) this.alBuffer = AL.GenBuffer();
				PcmData pcm = OV.LoadFromMemory(audioData.Res.OggVorbisData);
				AL.BufferData(
					this.alBuffer,
					pcm.channelCount == 1 ? ALFormat.Mono16 : ALFormat.Stereo16,
					pcm.data.ToArray(), 
					(int)pcm.data.Length, 
					pcm.sampleRate);
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.LoadData(this.audioData);
		}
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated &&
				this.alBuffer > AlBuffer_NotAvailable)
			{
				AL.DeleteBuffer(this.alBuffer);
				this.alBuffer = 0;
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Sound c = r as Sound;
			c.forceStream = this.forceStream;
			c.maxInstances = this.maxInstances;
			c.minDistFactor = this.minDistFactor;
			c.maxDistFactor = this.maxDistFactor;
			c.volFactor = this.volFactor;
			c.pitchFactor = this.pitchFactor;
			c.fadeOutAt = this.fadeOutAt;
			c.fadeOutTime = this.fadeOutTime;
			c.LoadData(this.audioData);
		}
	}
}
