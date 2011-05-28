using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using OpenTK.Audio.OpenAL;

using Duality.OggVorbis;

namespace Duality.Resources
{
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


		private	ContentRef<AudioData>	audioData;
		[NonSerialized]	private	int		alBuffer;

		public int AlBuffer
		{
			get { return this.alBuffer; }
		}
		public ContentRef<AudioData> Data
		{
			get { return this.audioData; }
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
			if (this.alBuffer == 0) this.alBuffer = AL.GenBuffer();
			this.audioData = audioData;

			// Non-Streamed Audio
			PcmData pcm = OV.LoadFromMemory(audioData.Res.OggVorbisData);
			AL.BufferData(
				this.alBuffer,
				pcm.channelCount == 1 ? OpenTK.Audio.OpenAL.ALFormat.Mono16 : OpenTK.Audio.OpenAL.ALFormat.Stereo16,
				pcm.data.ToArray(), 
				(int)pcm.data.Length, 
				pcm.sampleRate);
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
				this.alBuffer != 0)
			{
				AL.DeleteBuffer(this.alBuffer);
				this.alBuffer = 0;
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Sound c = r as Sound;
			c.LoadData(this.audioData);
		}
	}
}
