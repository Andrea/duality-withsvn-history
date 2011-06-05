using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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

		
		public const int AlBuffer_NotAvailable = AudioData.AlBuffer_NotAvailable;
		public const int AlBuffer_StreamMe = AudioData.AlBuffer_StreamMe;


		private	int			maxInstances	= 5;
		private	float		minDistFactor	= 1.0f;
		private	float		maxDistFactor	= 1.0f;
		private	float		volFactor		= 1.0f;
		private	float		pitchFactor		= 1.0f;
		private	float		fadeOutAt		= 0.0f;
		private	float		fadeOutTime		= 0.0f;
		private	SoundType	type			= SoundType.EffectWorld;
		private	ContentRef<AudioData>	audioData	= ContentRef<AudioData>.Null;

		public ContentRef<AudioData> Data
		{
			get { return this.audioData; }
			set { this.LoadData(value); }
		}
		public SoundType Type
		{
			get { return this.type; }
			set { this.type = value; }
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
		public bool IsStreamed
		{
			get { return this.audioData.IsAvailable && this.audioData.Res.IsStreamed; }
		}
		public int AlBuffer
		{
			get { return this.audioData.IsAvailable ? this.audioData.Res.AlBuffer : AudioData.AlBuffer_NotAvailable; }
		}

		public Sound() {}
		public Sound(ContentRef<AudioData> baseData)
		{
			this.LoadData(baseData);
		}

		public void LoadData(ContentRef<AudioData> data)
		{
			this.audioData = data;
			if (this.audioData.IsAvailable) this.audioData.Res.SetupAlBuffer();
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Sound c = r as Sound;
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
