using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

using Duality;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Resources
{
	/// <summary>
	/// A Pixmap stores pixel data in system memory. 
	/// </summary>
	[Serializable]
	public class Pixmap : Resource
	{
		public new const string FileExt = ".Pixmap" + Resource.FileExt;

		public const string VirtualContentPath = ContentProvider.VirtualContentPath + "Pixmap:";
		public const string ContentPath_DualityLogo256	= VirtualContentPath + "DualityLogo256";
		public const string ContentPath_DualityLogoB256	= VirtualContentPath + "DualityLogoB256";
		public const string ContentPath_White			= VirtualContentPath + "White";

		public static ContentRef<Pixmap> DualityLogo256		{ get; private set; }
		public static ContentRef<Pixmap> DualityLogoB256	{ get; private set; }
		public static ContentRef<Pixmap> White				{ get; private set; }

		internal static void InitDefaultContent()
		{
			Bitmap bm;
			Pixmap tmp;

			bm = new Bitmap(ReflectionHelper.GetEmbeddedResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), @"Resources\Default\DualityLogo256.png"));
			tmp = new Pixmap(bm.ColorTransparentPixels(ColorRGBA.TransparentBlack)); tmp.path = ContentPath_DualityLogo256;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			bm = new Bitmap(ReflectionHelper.GetEmbeddedResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), @"Resources\Default\DualityLogoB256.png"));
			tmp = new Pixmap(bm.ColorTransparentPixels(ColorRGBA.TransparentBlack)); tmp.path = ContentPath_DualityLogoB256;
			ContentProvider.RegisterContent(tmp.Path, tmp);
			bm = new Bitmap(1, 1); bm.SetPixel(0, 0, Color.FromArgb(255, 255, 255, 255));
			tmp = new Pixmap(bm); tmp.path = ContentPath_White;
			ContentProvider.RegisterContent(tmp.Path, tmp);

			DualityLogo256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogoB256);
			White			= ContentProvider.RequestContent<Pixmap>(ContentPath_White);
		}


		private	Bitmap	data			= null;
		private	string	dataBasePath	= null;

		public Bitmap PixelData
		{
			get { return this.data; }
			set { this.data = value; }
		}
		public string PixelDataBasePath
		{
			get { return this.dataBasePath; }
			set { this.dataBasePath = value; }
		}
 
		public Pixmap() {}
		public Pixmap(Bitmap image)
		{
			this.data = image;
		}
		public Pixmap(string imagePath)
		{
			this.LoadPixelData(imagePath);
		}

		public void SavePixelData(string imagePath = null)
		{
			if (imagePath == null) imagePath = this.dataBasePath;

			// We're saving this Pixmaps pixel data for the first time
			if (this.dataBasePath == null)
			{
				this.dataBasePath = imagePath;
			}

			this.data.Save(this.dataBasePath);
		}
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
				this.data.ColorTransparentPixels();
			}
		}

		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			Pixmap c = r as Pixmap;
			c.data			= this.data.Clone() as Bitmap;
			c.dataBasePath	= this.dataBasePath;
		}
	}
}
