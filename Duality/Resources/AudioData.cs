using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality.OggVorbis;

namespace Duality.Resources
{
	/// <summary>
	/// Stores compressed audio data (Ogg Vorbis) in system memory
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


		private	byte[]	data			= null;
		private	string	dataBasePath	= null;

		public byte[] OggVorbisData
		{
			get { return this.data; }
			set { this.data = value; }
		}
		public string OggVorbisDataBasePath
		{
			get { return this.dataBasePath; }
			set { this.dataBasePath = value; }
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
			if (this.dataBasePath == null) this.dataBasePath = oggVorbisPath;

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
