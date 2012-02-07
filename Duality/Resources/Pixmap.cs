using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

using Duality;
using Duality.ColorFormat;
using Duality.Serialization;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	/// <summary>
	/// A Pixmap stores pixel data in system memory. 
	/// </summary>
	/// <seealso cref="Duality.Resources.Texture"/>
	[Serializable]
	public class Pixmap : Resource, ISerializable
	{
		/// <summary>
		/// A Pixmap resources file extension.
		/// </summary>
		public new static string FileExt
		{ 
			get { return ".Pixmap" + Resource.FileExt; }
		}

		/// <summary>
		/// Represents an unknown Pixmap version.
		/// </summary>
		protected const int ResFormat_Version_Unknown	= 0;
		/// <summary>
		/// Represents the old, uncompressed Pixmap version using a <see cref="System.Drawing.Bitmap"/>.
		/// </summary>
		protected const int ResFormat_Version_Bitmap	= 1;
		/// <summary>
		/// Represents the PNG-compressed Pixmap version.
		/// </summary>
		protected const int ResFormat_Version_Png		= 2;
		
		/// <summary>
		/// (Virtual) base path for Duality's embedded default Pixmaps.
		/// </summary>
		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Pixmap:";
		/// <summary>
		/// (Virtual) path of the <see cref="DualityLogo256"/> Pixmap.
		/// </summary>
		public const string ContentPath_DualityLogo256	= VirtualContentPath + "DualityLogo256";
		/// <summary>
		/// (Virtual) path of the <see cref="DualityLogoB256"/> Pixmap.
		/// </summary>
		public const string ContentPath_DualityLogoB256	= VirtualContentPath + "DualityLogoB256";
		/// <summary>
		/// (Virtual) path of the <see cref="White"/> Pixmap.
		/// </summary>
		public const string ContentPath_White			= VirtualContentPath + "White";
		
		/// <summary>
		/// [GET] A Pixmap showing the Duality logo.
		/// </summary>
		public static ContentRef<Pixmap> DualityLogo256		{ get; private set; }
		/// <summary>
		/// [GET] A Pixmap showing the Duality logo without the text on it.
		/// </summary>
		public static ContentRef<Pixmap> DualityLogoB256	{ get; private set; }
		/// <summary>
		/// [GET] A plain white 1x1 Pixmap. Can be used as a dummy.
		/// </summary>
		public static ContentRef<Pixmap> White				{ get; private set; }

		internal static void InitDefaultContent()
		{
			Bitmap bm;
			Pixmap tmp;

			bm = new Bitmap(ReflectionHelper.GetEmbeddedResourceStream(Assembly.GetExecutingAssembly(), @"Resources\Default\DualityLogo256.png"));
			tmp = new Pixmap(bm.ColorTransparentPixels(ColorRgba.TransparentBlack));
			ContentProvider.RegisterContent(ContentPath_DualityLogo256, tmp);
			bm = new Bitmap(ReflectionHelper.GetEmbeddedResourceStream(Assembly.GetExecutingAssembly(), @"Resources\Default\DualityLogoB256.png"));
			tmp = new Pixmap(bm.ColorTransparentPixels(ColorRgba.TransparentBlack));
			ContentProvider.RegisterContent(ContentPath_DualityLogoB256, tmp);
			bm = new Bitmap(1, 1); bm.SetPixel(0, 0, Color.FromArgb(255, 255, 255, 255));
			tmp = new Pixmap(bm);
			ContentProvider.RegisterContent(ContentPath_White, tmp);

			DualityLogo256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogoB256);
			White			= ContentProvider.RequestContent<Pixmap>(ContentPath_White);
		}


		private	Bitmap	data			= null;
		private	string	dataBasePath	= null;

		/// <summary>
		/// [GET / SET] A <see cref="System.Drawing.Bitmap"/> representing the actual pixel data.
		/// </summary>
		public Bitmap PixelData
		{
			get { return this.data; }
			set { this.data = value; }
		}
		/// <summary>
		/// [GET / SET] The path from which the pixel data has been originally imported in Duality.
		/// This is only relevant when creating new Pixmaps at runtime or importing them in the editor
		/// environment.
		/// </summary>
		public string PixelDataBasePath
		{
			get { return this.dataBasePath; }
			set { this.dataBasePath = value; }
		}
		/// <summary>
		/// [GET] The Width of the actual pixel data.
		/// </summary>
		public int Width
		{
			get { return this.data != null ? this.data.Width : 0; }
		}
		/// <summary>
		/// [GET] The Height of the actual pixel data.
		/// </summary>
		public int Height
		{
			get { return this.data != null ? this.data.Height : 0; }
		}
 
		/// <summary>
		/// Creates a new, empty Pixmap.
		/// </summary>
		public Pixmap() 
		{
			this.data = new Bitmap(1, 1);
		}
		/// <summary>
		/// Creates a new Pixmap from the specified <see cref="System.Drawing.Bitmap"/>.
		/// </summary>
		/// <param name="image">The <see cref="System.Drawing.Bitmap"/> that will be used by the Pixmap.</param>
		public Pixmap(Bitmap image)
		{
			this.data = image;
		}
		/// <summary>
		/// Creates a new Pixmap from the specified image file.
		/// </summary>
		/// <param name="imagePath">A path to the image file that will be used as pixel data source.</param>
		public Pixmap(string imagePath)
		{
			this.LoadPixelData(imagePath);
		}

		/// <summary>
		/// Saves the Pixmaps pixel data as image file. Its image format is determined by the file extension.
		/// </summary>
		/// <param name="imagePath">The path of the file to which the pixel data is written.</param>
		public void SavePixelData(string imagePath = null)
		{
			if (imagePath == null) imagePath = this.dataBasePath;

			// We're saving this Pixmaps pixel data for the first time
			if (!this.path.Contains(':') && this.dataBasePath == null) this.dataBasePath = imagePath;

			this.data.Save(imagePath);
		}
		/// <summary>
		/// Replaces the Pixmaps pixel data with a new dataset that has been retrieved from file.
		/// </summary>
		/// <param name="imagePath">The path of the file from which to retrieve the new pixel data.</param>
		public void LoadPixelData(string imagePath = null)
		{
			if (imagePath == null) imagePath = this.dataBasePath;

			this.dataBasePath = imagePath;

			if (String.IsNullOrEmpty(this.dataBasePath) || !File.Exists(this.dataBasePath))
				this.data = null;
			else
			{
				byte[] buffer = File.ReadAllBytes(this.dataBasePath);
				this.data = new Bitmap(new MemoryStream(buffer));
				this.data = this.data.ColorTransparentPixels();
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Pixmap c = r as Pixmap;
			c.data			= this.data.Clone() as Bitmap;
			c.dataBasePath	= null;
		}

		void ISerializable.WriteData(IDataWriter writer)
		{
			writer.WriteValue("version", ResFormat_Version_Png);

			if (this.data != null)
			{
				MemoryStream str = new MemoryStream(1024 * 64);
				this.data.Save(str, System.Drawing.Imaging.ImageFormat.Png);
				writer.WriteValue("data", str.ToArray());
			}
			else
				writer.WriteValue("data", null);

			writer.WriteValue("dataBasePath", this.dataBasePath);

		}
		void ISerializable.ReadData(IDataReader reader)
		{
			int version;
			try { reader.ReadValue("version", out version); }
			catch (Exception) { version = ResFormat_Version_Unknown; }

			if (version == ResFormat_Version_Bitmap)
			{
				reader.ReadValue("data", out this.data);
			}
			else if (version == ResFormat_Version_Png)
			{
				byte[] dataBlock;
				reader.ReadValue("data", out dataBlock);
				this.data = dataBlock != null ? new Bitmap(new MemoryStream(dataBlock)) : null;
			}
			else
			{
				this.data = null;
			}

			reader.ReadValue("dataBasePath", out this.dataBasePath);
		}
	}
}
