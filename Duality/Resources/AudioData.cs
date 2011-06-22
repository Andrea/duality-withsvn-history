using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Duality.OggVorbis;
using OpenTK.Audio.OpenAL;

namespace Duality.Resources
{
	/// <summary>
	/// Stores compressed audio data (Ogg Vorbis) in system memory as well as a reference to the
	/// OpenAL buffer containing actual PCM data, once set up. The OpenAL buffer is set up lazy
	/// i.e. as soon as demanded by accessing the AlBuffer property or calling SetupAlBuffer.
	/// </summary>
	[Serializable]
	public class AudioData : Resource
	{
		public new const string FileExt = ".AudioData" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "AudioData:";
		public const string ContentPath_Beep		= VirtualContentPath + "Beep";
		public const string ContentPath_DroneLoop	= VirtualContentPath + "DroneLoop";
		public const string ContentPath_LogoJingle	= VirtualContentPath + "LogoJingle";

		public static ContentRef<AudioData> Beep		{ get; private set; }
		public static ContentRef<AudioData> DroneLoop	{ get; private set; }
		public static ContentRef<AudioData> LogoJingle	{ get; private set; }

		internal static void InitDefaultContent()
		{
			AudioData tmp;

			tmp = new AudioData(ReflectionHelper.GetEmbeddedResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), @"Resources\Default\Beep.ogg"));
			tmp.path = ContentPath_Beep;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new AudioData(ReflectionHelper.GetEmbeddedResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), @"Resources\Default\DroneLoop.ogg"));
			tmp.path = ContentPath_DroneLoop;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			tmp = new AudioData(ReflectionHelper.GetEmbeddedResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), @"Resources\Default\LogoJingle.ogg"));
			tmp.path = ContentPath_LogoJingle;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			Beep		= ContentProvider.RequestContent<AudioData>(ContentPath_Beep);
			DroneLoop	= ContentProvider.RequestContent<AudioData>(ContentPath_DroneLoop);
			LogoJingle	= ContentProvider.RequestContent<AudioData>(ContentPath_LogoJingle);
		}
		

		public const int AlBuffer_NotAvailable = 0;
		public const int AlBuffer_StreamMe = -1;


		private	byte[]	data			= null;
		private	string	dataBasePath	= null;
		private	bool	forceStream		= false;
		[NonSerialized]	private	int	alBuffer	= AlBuffer_NotAvailable;

		public byte[] OggVorbisData
		{
			get { return this.data; }
			set { this.data = value; this.DisposeAlBuffer(); }
		}
		public string OggVorbisDataBasePath
		{
			get { return this.dataBasePath; }
			set { this.LoadOggVorbisData(value); }
		}
		public bool ForceStream
		{
			get { return this.forceStream; }
			set { this.forceStream = value; this.DisposeAlBuffer(); }
		}
		public bool IsStreamed
		{
			get { return this.forceStream || (this.data != null && this.data.Length > 1024 * 100); }
		}
		public int AlBuffer
		{
			get 
			{ 
				if (this.alBuffer == AlBuffer_NotAvailable) this.SetupAlBuffer();
				return this.alBuffer;
			}
		}

		public AudioData() {}
		public AudioData(byte[] oggVorbisData)
		{
			this.data = oggVorbisData;
		}
		public AudioData(Stream oggVorbisDataStream)
		{
			this.data = new byte[oggVorbisDataStream.Length];
			oggVorbisDataStream.Read(this.data, 0, (int)oggVorbisDataStream.Length);
		}
		public AudioData(string filepath)
		{
			this.LoadOggVorbisData(filepath);
		}

		public void SaveOggVorbisData(string oggVorbisPath = null)
		{
			if (oggVorbisPath == null) oggVorbisPath = this.dataBasePath;

			// We're saving this data for the first time
			if (!this.path.Contains(':') && this.dataBasePath == null) this.dataBasePath = oggVorbisPath;

			File.WriteAllBytes(oggVorbisPath, this.data);
		}
		public void LoadOggVorbisData(string oggVorbisPath = null)
		{
			if (oggVorbisPath == null) oggVorbisPath = this.dataBasePath;

			this.dataBasePath = oggVorbisPath;

			if (String.IsNullOrEmpty(this.dataBasePath) || !File.Exists(this.dataBasePath))
				this.data = null;
			else
				this.data = File.ReadAllBytes(this.dataBasePath);

			this.DisposeAlBuffer();
		}
		
		public void DisposeAlBuffer()
		{
			if (this.alBuffer > AlBuffer_NotAvailable) AL.DeleteBuffer(this.alBuffer);
			this.alBuffer = AlBuffer_NotAvailable;
			return;
		}
		public void SetupAlBuffer()
		{
			// No AudioData available
			if (this.data.Length == 0 || this.data == null)
			{
				this.DisposeAlBuffer();
				return;
			}

			// Streamed Audio
			if (this.IsStreamed)
			{
				this.DisposeAlBuffer();
				this.alBuffer = AlBuffer_StreamMe;
			}
			// Non-Streamed Audio
			else
			{
				if (this.alBuffer <= AlBuffer_NotAvailable) this.alBuffer = AL.GenBuffer();
				PcmData pcm = OV.LoadFromMemory(this.data);
				AL.BufferData(
					this.alBuffer,
					pcm.channelCount == 1 ? ALFormat.Mono16 : ALFormat.Stereo16,
					pcm.data.ToArray(), 
					(int)pcm.data.Length, 
					pcm.sampleRate);
			}
		}
		
		protected override void OnDisposed(bool manually)
		{
			base.OnDisposed(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated)
				this.DisposeAlBuffer();
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			AudioData c = r as AudioData;
			c.data			= (byte[])this.data.Clone();
			c.dataBasePath	= this.dataBasePath;
		}
	}
}
