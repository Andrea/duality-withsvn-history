using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

using Duality.ColorFormat;
using Duality.EditorHints;
using Duality.Serialization;

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
		public new const string FileExt = ".Pixmap" + Resource.FileExt;

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
		/// Represents the PNG-compressed layered Pixmap version.
		/// </summary>
		protected const int ResFormat_Version_LayerPng	= 3;
		
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
			tmp = new Pixmap(bm);
			ContentProvider.RegisterContent(ContentPath_DualityLogo256, tmp);
			bm = new Bitmap(ReflectionHelper.GetEmbeddedResourceStream(Assembly.GetExecutingAssembly(), @"Resources\Default\DualityLogoB256.png"));
			tmp = new Pixmap(bm);
			ContentProvider.RegisterContent(ContentPath_DualityLogoB256, tmp);
			bm = new Bitmap(1, 1); bm.SetPixel(0, 0, Color.FromArgb(255, 255, 255, 255));
			tmp = new Pixmap(bm);
			ContentProvider.RegisterContent(ContentPath_White, tmp);

			DualityLogo256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogo256);
			DualityLogoB256	= ContentProvider.RequestContent<Pixmap>(ContentPath_DualityLogoB256);
			White			= ContentProvider.RequestContent<Pixmap>(ContentPath_White);
		}

		
		/// <summary>
		/// Represents a filtering method.
		/// </summary>
		public enum FilterMethod
		{
			/// <summary>
			/// Nearest neighbor filterting. (No interpolation)
			/// </summary>
			Nearest,
			/// <summary>
			/// Linear interpolation.
			/// </summary>
			Linear
		}
		/// <summary>
		/// Represents a pixel data layer.
		/// </summary>
		public class Layer : Duality.Cloning.ICloneable
		{
			private	int	width;
			private	int height;
			private	ColorRgba[]	data;

			/// <summary>
			/// [GET] The layers width in pixels
			/// </summary>
			public int Width
			{
				get { return this.width; }
			}
			/// <summary>
			/// [GET] The layers height in pixels
			/// </summary>
			public int Height
			{
				get { return this.height; }
			}
			/// <summary>
			/// [GET] The layers pixel data
			/// </summary>
			public ColorRgba[] Data
			{
				get { return this.data; }
			}
			/// <summary>
			/// [GET / SET] A single pixels color.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <returns></returns>
			public ColorRgba this[int x, int y]
			{
				get
				{
					int n = x + y * this.width;
					return this.data[n];
				}
				set
				{
					int n = x + y * this.width;
					this.data[n] = value;
				}
			}

			
			public Layer() : this(0, 0, ColorRgba.TransparentBlack) {}
			public Layer(int width, int height) : this(width, height, ColorRgba.TransparentBlack) {}
			public Layer(int width, int height, ColorRgba backColor)
			{
				if (width < 0) throw new ArgumentException("Width may not be negative.", "width");
				if (height < 0) throw new ArgumentException("Height may not be negative.", "height");

				this.width = width;
				this.height = height;
				this.data = new ColorRgba[width * height];

				for (int i = 0; i < this.data.Length; i++)
					this.data[i] = backColor;
			}
			public Layer(int width, int height, ColorRgba[] data)
			{
				if (data == null) throw new ArgumentNullException("data");
				if (width < 0) throw new ArgumentException("Width may not be negative.", "width");
				if (height < 0) throw new ArgumentException("Height may not be negative.", "height");

				this.SetPixelDataRgba(data, width, height);
			}
			public Layer(Bitmap image)
			{
				if (image == null) throw new ArgumentNullException("image");
				this.FromBitmap(image);
			}
			public Layer(string imagePath)
			{
				if (string.IsNullOrEmpty(imagePath)) throw new ArgumentNullException("imagePath");
				
				byte[] buffer = File.ReadAllBytes(imagePath);
				Bitmap bm = new Bitmap(new MemoryStream(buffer));
				this.FromBitmap(bm);
			}
			public Layer(Layer baseLayer)
			{
				if (baseLayer == null) throw new ArgumentNullException("baseLayer");
				baseLayer.CopyTo(this);
			}

			/// <summary>
			/// Clones the pixel data layer and returns the new instance.
			/// </summary>
			/// <returns></returns>
			public Layer Clone()
			{
				return Duality.Cloning.CloneProvider.DeepClone(this);
			}
			/// <summary>
			/// Copies all data contained in this pixel data layer to a target layer.
			/// </summary>
			/// <param name="target"></param>
			public void CopyTo(Layer target)
			{
				if (target == null) throw new ArgumentNullException("target");
				Duality.Cloning.CloneProvider.DeepCopyTo(this, target);
			}

			/// <summary>
			/// Saves the pixel data contained in this layer to the specified file.
			/// </summary>
			/// <param name="imagePath"></param>
			public void SavePixelData(string imagePath)
			{
				this.ToBitmap().Save(imagePath);
			}
			/// <summary>
			/// Loads the pixel data in this layer from the specified file.
			/// </summary>
			/// <param name="imagePath"></param>
			public void LoadPixelData(string imagePath)
			{
				this.FromBitmap(new Bitmap(imagePath));
			}
			/// <summary>
			/// Discards all pixel data in this Layer.
			/// </summary>
			public void ClearPixelData()
			{
				this.data = new ColorRgba[0];
				this.width = 0;
				this.height = 0;
			}
			
			/// <summary>
			/// Creates a <see cref="System.Drawing.Bitmap"/> out of this Layer.
			/// </summary>
			/// <returns></returns>
			public Bitmap ToBitmap()
			{
				int[] argbValues = this.GetPixelDataIntArgb();
				Bitmap bm = new Bitmap(this.width, this.height);
				BitmapData data = bm.LockBits(
					new Rectangle(0, 0, bm.Width, bm.Height),
					ImageLockMode.WriteOnly,
					PixelFormat.Format32bppArgb);
			
				int pixels = data.Width * data.Height;
				System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, data.Scan0, pixels);

				bm.UnlockBits(data);
				return bm;
			}
			/// <summary>
			/// Gets the Layers pixel data in the ColorRgba format. Note that this data is a clone and thus modifying it won't
			/// affect the Layer it has been retrieved from.
			/// </summary>
			/// <returns></returns>
			public ColorRgba[] GetPixelDataRgba()
			{
				return this.data.Clone() as ColorRgba[];
			}
			/// <summary>
			/// Gets the Layers pixel data in bytewise Rgba format. (Four elements per pixel)
			/// </summary>
			/// <returns></returns>
			public byte[] GetPixelDataByteRgba()
			{
				byte[] rgbaValues = new byte[this.data.Length * 4];
				for (int i = 0; i < this.data.Length; i++)
				{
					rgbaValues[i * 4 + 0] = this.data[i].R;
					rgbaValues[i * 4 + 1] = this.data[i].G;
					rgbaValues[i * 4 + 2] = this.data[i].B;
					rgbaValues[i * 4 + 3] = this.data[i].A;
				}
				return rgbaValues;
			}
			/// <summary>
			/// Gets the Layers pixel data in the integer Argb format. (One element per pixel)
			/// </summary>
			/// <returns></returns>
			public int[] GetPixelDataIntArgb()
			{
				int[] argbValues = new int[this.data.Length];
				unchecked
				{
					for (int i = 0; i < this.data.Length; i++)
						argbValues[i] = this.data[i].ToIntArgb();
				}
				return argbValues;
			}

			/// <summary>
			/// Sets this Layers pixel data to the one contained in the specified <see cref="System.Drawing.Bitmap"/>
			/// </summary>
			/// <param name="bm"></param>
			public void FromBitmap(Bitmap bm)
			{
				// Retrieve data
				BitmapData data = bm.LockBits(
					new Rectangle(0, 0, bm.Width, bm.Height),
					ImageLockMode.ReadOnly,
					PixelFormat.Format32bppArgb);
			
				int pixels = data.Width * data.Height;
				int[] argbValues = new int[pixels];
				System.Runtime.InteropServices.Marshal.Copy(data.Scan0, argbValues, 0, pixels);
				bm.UnlockBits(data);
				
				this.SetPixelDataArgb(argbValues, bm.Width, bm.Height);
			}
			/// <summary>
			/// Sets the layers pixel data in the ColorRgba format. Note that the specified data will be copied and thus modifying it
			/// outside won't affect the Layer it has been inserted into.
			/// </summary>
			/// <param name="pixelData"></param>
			/// <param name="width"></param>
			/// <param name="height"></param>
			public void SetPixelDataRgba(ColorRgba[] pixelData, int width = -1, int height = -1)
			{
				if (width < 0) width = this.width;
				if (height < 0) height = this.height;
				if (pixelData.Length != width * height) throw new ArgumentException("Data length doesn't match width * height", "pixelData");

				this.width = width;
				this.height = height;
				this.data = pixelData.Clone() as ColorRgba[];
			}
			/// <summary>
			/// Sets the Layers pixel data in bytewise Rgba format. (Four elements per pixel)
			/// </summary>
			/// <param name="pixelData"></param>
			/// <param name="width"></param>
			/// <param name="height"></param>
			public void SetPixelDataRgba(byte[] pixelData, int width = -1, int height = -1)
			{
				if (width < 0) width = this.width;
				if (height < 0) height = this.height;
				if (pixelData.Length != 4 * width * height) throw new ArgumentException("Data length doesn't match 4 * width * height", "pixelData");

				this.width = width;
				this.height = height;
				if (this.data == null || this.data.Length != this.width * this.height)
					this.data = new ColorRgba[this.width * this.height];

				for (int i = 0; i < this.data.Length; i++)
				{
					this.data[i].R = pixelData[i * 4 + 0];
					this.data[i].G = pixelData[i * 4 + 1];
					this.data[i].B = pixelData[i * 4 + 2];
					this.data[i].A = pixelData[i * 4 + 3];
				}
			}
			/// <summary>
			/// Sets the Layers pixel data in the integer Argb format. (One element per pixel)
			/// </summary>
			/// <param name="pixelData"></param>
			/// <param name="width"></param>
			/// <param name="height"></param>
			public void SetPixelDataArgb(int[] pixelData, int width = -1, int height = -1)
			{
				if (width < 0) width = this.width;
				if (height < 0) height = this.height;
				if (pixelData.Length != width * height) throw new ArgumentException("Data length doesn't match width * height", "pixelData");

				this.width = width;
				this.height = height;
				if (this.data == null || this.data.Length != this.width * this.height) 
					this.data = new ColorRgba[this.width * this.height];

				for (int i = 0; i < this.data.Length; i++)
					this.data[i].SetIntArgb(pixelData[i]);
			}

			/// <summary>
			/// Rescales the Layer, stretching it to the specified size.
			/// </summary>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="filter">The filtering method to use when rescaling</param>
			public void Rescale(int w, int h, FilterMethod filter = FilterMethod.Linear)
			{
				ColorRgba[] result = this.InternalRescale(w, h, filter);
				if (result == null) return;

				this.data = result;
				this.width = w;
				this.height = h;

				return;
			}
			/// <summary>
			/// Resizes the Layers boundaries.
			/// </summary>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="origin"></param>
			public void Resize(int w, int h, Alignment origin = Alignment.TopLeft)
			{
				int x = 0;
				int y = 0;

				if (origin == Alignment.Right || 
					origin == Alignment.TopRight || 
					origin == Alignment.BottomRight)
					x = w - this.width;
				else if (
					origin == Alignment.Center || 
					origin == Alignment.Top || 
					origin == Alignment.Bottom)
					x = (w - this.width) / 2;

				if (origin == Alignment.Bottom || 
					origin == Alignment.BottomLeft || 
					origin == Alignment.BottomRight)
					y = h - this.height;
				else if (
					origin == Alignment.Center || 
					origin == Alignment.Left || 
					origin == Alignment.Right)
					y = (h - this.height) / 2;

				this.SubImage(-x, -y, w, h);
			}
			/// <summary>
			/// Extracts a rectangular region of this Layer. If the extracted region is bigger than the original Layer,
			/// all new space is filled with a background color.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="w"></param>
			/// <param name="h"></param>
			public void SubImage(int x, int y, int w, int h)
			{
				this.SubImage(x, y, w, h, ColorRgba.TransparentBlack);
			}
			/// <summary>
			/// Extracts a rectangular region of this Layer. If the extracted region is bigger than the original Layer,
			/// all new space is filled with a background color.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="backColor"></param>
			public void SubImage(int x, int y, int w, int h, ColorRgba backColor)
			{
				Layer tempLayer = new Layer(w, h, backColor);
				this.DrawOnto(tempLayer, BlendMode.Solid, -x, -y);
				tempLayer.CopyTo(this);
			}
			/// <summary>
			/// Crops the Layer, removing transparent / empty border areas.
			/// </summary>
			/// <param name="cropX">Whether the Layer should be cropped in X-direction</param>
			/// <param name="cropY">Whether the Layer should be cropped in Y-direction</param>
			public void Crop(bool cropX = true, bool cropY = true)
			{
				if (!cropX && !cropY) return;
				Rectangle bounds = this.OpaqueBounds();
				this.SubImage(cropX ? bounds.X : 0, cropY ? bounds.Y : 0, cropX ? bounds.Width : this.width, cropY ? bounds.Height : this.height);
			}

			/// <summary>
			/// Measures the bounding rectangle of the Layers opaque pixels.
			/// </summary>
			/// <returns></returns>
			public Rectangle OpaqueBounds()
			{
				Rectangle bounds = new Rectangle(this.width, this.height, 0, 0);
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].A == 0) continue;
					int x = i % this.width;
					int y = i / this.width;
					bounds.X = Math.Min(bounds.X, x);
					bounds.Y = Math.Min(bounds.Y, y);
					bounds.Width = Math.Max(bounds.Width, x);
					bounds.Height = Math.Max(bounds.Height, y);
				}
				bounds.Width = 1 + Math.Max(0, bounds.Width - bounds.X);
				bounds.Height = 1 + Math.Max(0, bounds.Height - bounds.Y);

				return bounds;
			}
			/// <summary>
			/// Determines the average color of a Layer.
			/// </summary>
			/// <param name="weightTransparent">If true, the alpha value weights a pixels color value. </param>
			/// <returns></returns>
			public ColorRgba GetAverageColor(bool weightTransparent = true)
			{
				float[] sum = new float[4];
				int count = 0;

				if (weightTransparent)
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						sum[0] += this.data[i].R * ((float)this.data[i].A / 255.0f);
						sum[1] += this.data[i].G * ((float)this.data[i].A / 255.0f);
						sum[2] += this.data[i].B * ((float)this.data[i].A / 255.0f);
						sum[3] += (float)this.data[i].A / 255.0f;
						++count;
					}
					if (sum[3] <= 0.001f) return ColorRgba.TransparentBlack;

					return new ColorRgba(
						(byte)MathF.Clamp((int)(sum[0] / sum[3]), 0, 255),
						(byte)MathF.Clamp((int)(sum[1] / sum[3]), 0, 255),
						(byte)MathF.Clamp((int)(sum[2] / sum[3]), 0, 255),
						(byte)MathF.Clamp((int)(sum[3] / (float)count), 0, 255));
				}
				else
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						sum[0] += this.data[i].R;
						sum[1] += this.data[i].G;
						sum[2] += this.data[i].B;
						sum[3] += this.data[i].A;
						++count;
					}
					if (count == 0) return ColorRgba.TransparentBlack;

					return new ColorRgba(
						(byte)MathF.Clamp((int)(sum[0] / (float)count), 0, 255),
						(byte)MathF.Clamp((int)(sum[1] / (float)count), 0, 255),
						(byte)MathF.Clamp((int)(sum[2] / (float)count), 0, 255),
						(byte)MathF.Clamp((int)(sum[3] / (float)count), 0, 255));
				}
			}

			/// <summary>
			/// Sets the color of all transparent pixels based on the non-transparent color values next to them.
			/// This does not affect any alpha values but prepares the Layer for correct filtering once uploaded
			/// to <see cref="Duality.Resources.Texture"/>.
			/// </summary>
			public void ColorTransparentPixels()
			{
				Point	pos		= new Point();
				int[]	nPos	= new int[8];
				bool[]	nOk		= new bool[8];
				int[]	nMult	= new int[] {2, 2, 2, 2, 1, 1, 1, 1};
				int[]	mixClr	= new int[4];

				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].A != 0) continue;

					pos.Y	= i / this.width;
					pos.X	= i - (pos.Y * this.width);

					nPos[0] = (pos.X + ((pos.Y - 1) * this.width));
					nPos[1] = (pos.X + ((pos.Y + 1) * this.width));
					nPos[2] = ((pos.X - 1) + (pos.Y * this.width));
					nPos[3] = ((pos.X + 1) + (pos.Y * this.width));
					nPos[4] = ((pos.X - 1) + ((pos.Y - 1) * this.width));
					nPos[5] = ((pos.X - 1) + ((pos.Y + 1) * this.width));
					nPos[6] = ((pos.X + 1) + ((pos.Y - 1) * this.width));
					nPos[7] = ((pos.X + 1) + ((pos.Y + 1) * this.width));

					nOk[0]	= pos.Y > 0;
					nOk[1]	= pos.Y < this.height - 1;
					nOk[2]	= pos.X > 0;
					nOk[3]	= pos.X < this.width - 1;
					nOk[4]	= pos.X > 0 && pos.Y > 0;
					nOk[5]	= pos.X > 0 && pos.Y < this.height - 1;
					nOk[6]	= pos.X < this.width - 1 && pos.Y > 0;
					nOk[7]	= pos.X < this.width - 1 && pos.Y < this.height - 1;

					for (int j = 0; j < nPos.Length; j++)
					{
						if (!nOk[j]) continue;
						if (this.data[nPos[j]].A == 0) continue;

						mixClr[0] += this.data[nPos[j]].R * nMult[j];
						mixClr[1] += this.data[nPos[j]].G * nMult[j];
						mixClr[2] += this.data[nPos[j]].B * nMult[j];
						mixClr[3] += nMult[j];
					}

					if (mixClr != null)
					{
						this.data[i].R = (byte)Math.Round((float)mixClr[0] / (float)mixClr[3]);
						this.data[i].G = (byte)Math.Round((float)mixClr[1] / (float)mixClr[3]);
						this.data[i].B = (byte)Math.Round((float)mixClr[2] / (float)mixClr[3]);
					}
				}
			}
			/// <summary>
			/// Sets the color of all transparent pixels to the specified color.
			/// </summary>
			/// <param name="transparentColor"></param>
			public void ColorTransparentPixels(ColorRgba transparentColor)
			{
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].A != 0) continue;
					this.data[i] = transparentColor;
				}
			}
			
			/// <summary>
			/// Rescales the Layer, stretching it to the specified size.
			/// </summary>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="filter">The filtering method to use when rescaling</param>
			public Layer CloneRescale(int w, int h, FilterMethod filter = FilterMethod.Linear)
			{
				ColorRgba[] result = this.InternalRescale(w, h, filter);
				if (result == null) return this.Clone();

				return new Layer(w, h, result);
			}
			/// <summary>
			/// Resizes the Layers boundaries.
			/// </summary>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="origin"></param>
			public Layer CloneResize(int w, int h, Alignment origin = Alignment.TopLeft)
			{
				int x = 0;
				int y = 0;

				if (origin == Alignment.Right || 
					origin == Alignment.TopRight || 
					origin == Alignment.BottomRight)
					x = w - this.width;
				else if (
					origin == Alignment.Center || 
					origin == Alignment.Top || 
					origin == Alignment.Bottom)
					x = (w - this.width) / 2;

				if (origin == Alignment.Bottom || 
					origin == Alignment.BottomLeft || 
					origin == Alignment.BottomRight)
					y = h - this.height;
				else if (
					origin == Alignment.Center || 
					origin == Alignment.Left || 
					origin == Alignment.Right)
					y = (h - this.height) / 2;

				return this.CloneSubImage(-x, -y, w, h);
			}
			/// <summary>
			/// Extracts a rectangular region of this Layer. If the extracted region is bigger than the original Layer,
			/// all new space is filled with a background color.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="w"></param>
			/// <param name="h"></param>
			public Layer CloneSubImage(int x, int y, int w, int h)
			{
				return this.CloneSubImage(x, y, w, h, ColorRgba.TransparentBlack);
			}
			/// <summary>
			/// Extracts a rectangular region of this Layer. If the extracted region is bigger than the original Layer,
			/// all new space is filled with a background color.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="w"></param>
			/// <param name="h"></param>
			/// <param name="backColor"></param>
			public Layer CloneSubImage(int x, int y, int w, int h, ColorRgba backColor)
			{
				Layer tempLayer = new Layer(w, h, backColor);
				this.DrawOnto(tempLayer, BlendMode.Solid, -x, -y);
				return tempLayer;
			}
			/// <summary>
			/// Crops the Layer, removing transparent / empty border areas.
			/// </summary>
			/// <param name="cropX">Whether the Layer should be cropped in X-direction</param>
			/// <param name="cropY">Whether the Layer should be cropped in Y-direction</param>
			public Layer CloneCrop(bool cropX = true, bool cropY = true)
			{
				if (!cropX && !cropY) return this.Clone();
				Rectangle bounds = this.OpaqueBounds();
				return this.CloneSubImage(cropX ? bounds.X : 0, cropY ? bounds.Y : 0, cropX ? bounds.Width : this.width, cropY ? bounds.Height : this.height);
			}

			/// <summary>
			/// Performs a drawing operation from this Layer to a target layer.
			/// </summary>
			/// <param name="target"></param>
			/// <param name="blend"></param>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="w"></param>
			/// <param name="h"></param>
			public void DrawOnto(Layer target, BlendMode blend, int x, int y, int w = -1, int h = -1)
			{
				if (w == -1) w = this.width;
				if (h == -1) h = this.height;

				int beginX = Math.Max(0, -x);
				int beginY = Math.Max(0, -y);
				int endX = MathF.Min(w, this.width, target.width - x);
				int endY = MathF.Min(h, this.height, target.height - y);
				if (endX - beginX < 1) return;
				if (endY - beginY < 1) return;

				ColorRgba targetColor;
				for (int i = beginX; i < endX; i++)
				{
					for (int j = beginY; j < endY; j++)
					{
						int sourceN = i + this.width * (j);
						int targetN = x + i + target.width * (y + j);

						if (blend == BlendMode.Solid)
						{
							target.data[targetN] = this.data[sourceN];
						}
						else if (blend == BlendMode.Mask)
						{
							if (this.data[sourceN].A >= 0) target.data[targetN] = this.data[sourceN];
						}
						else if (blend == BlendMode.Add)
						{
							targetColor	= target.data[targetN];
							float alphaTemp = (float)this.data[sourceN].A / 255.0f;
							target.data[targetN].R = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.R + this.data[sourceN].R * alphaTemp)));
							target.data[targetN].G = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.G + this.data[sourceN].G * alphaTemp)));
							target.data[targetN].B = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.B + this.data[sourceN].B * alphaTemp)));
							target.data[targetN].A = (byte)Math.Min(255, Math.Max(0, (int)targetColor.A + (int)this.data[sourceN].A));
						}
						else if (blend == BlendMode.Alpha)
						{
							targetColor	= target.data[targetN];
							float alphaTemp = (float)this.data[sourceN].A / 255.0f;
							target.data[targetN].R = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.R * (1.0f - alphaTemp) + this.data[sourceN].R * alphaTemp)));
							target.data[targetN].G = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.G * (1.0f - alphaTemp) + this.data[sourceN].G * alphaTemp)));
							target.data[targetN].B = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.B * (1.0f - alphaTemp) + this.data[sourceN].B * alphaTemp)));
							target.data[targetN].A = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(targetColor.A * (1.0f - alphaTemp) + this.data[sourceN].A)));
						}
						else if (blend == BlendMode.Multiply)
						{
							targetColor	= target.data[targetN];
							float clrTempR = (float)targetColor.R / 255.0f;
							float clrTempG = (float)targetColor.G / 255.0f;
							float clrTempB = (float)targetColor.B / 255.0f;
							float clrTempA = (float)targetColor.A / 255.0f;
							target.data[targetN].R = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].R * clrTempR)));
							target.data[targetN].G = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].G * clrTempG)));
							target.data[targetN].B = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].B * clrTempB)));
							target.data[targetN].A = (byte)Math.Min(255, Math.Max(0, (int)targetColor.A + (int)this.data[sourceN].A));
						}
						else if (blend == BlendMode.Light)
						{
							targetColor	= target.data[targetN];
							float clrTempR = (float)targetColor.R / 255.0f;
							float clrTempG = (float)targetColor.G / 255.0f;
							float clrTempB = (float)targetColor.B / 255.0f;
							float clrTempA = (float)targetColor.A / 255.0f;
							target.data[targetN].R = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].R * clrTempR + targetColor.R)));
							target.data[targetN].G = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].G * clrTempG + targetColor.G)));
							target.data[targetN].B = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].B * clrTempB + targetColor.B)));
							target.data[targetN].A = (byte)Math.Min(255, Math.Max(0, (int)targetColor.A + (int)this.data[sourceN].A));
						}
						else if (blend == BlendMode.Invert)
						{
							targetColor	= target.data[targetN];
							float clrTempR = (float)targetColor.R / 255.0f;
							float clrTempG = (float)targetColor.G / 255.0f;
							float clrTempB = (float)targetColor.B / 255.0f;
							float clrTempA = (float)targetColor.A / 255.0f;
							float clrTempR2 = (float)this.data[sourceN].R / 255.0f;
							float clrTempG2 = (float)this.data[sourceN].G / 255.0f;
							float clrTempB2 = (float)this.data[sourceN].B / 255.0f;
							float clrTempA2 = (float)this.data[sourceN].A / 255.0f;
							target.data[targetN].R = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].R * (1.0f - clrTempR) + targetColor.R * (1.0f - clrTempR2))));
							target.data[targetN].G = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].G * (1.0f - clrTempG) + targetColor.G * (1.0f - clrTempG2))));
							target.data[targetN].B = (byte)Math.Min(255, Math.Max(0, (int)Math.Round(this.data[sourceN].B * (1.0f - clrTempB) + targetColor.B * (1.0f - clrTempB2))));
							target.data[targetN].A = (byte)Math.Min(255, Math.Max(0, (int)(targetColor.A + this.data[sourceN].A)));
						}
					}
				}
			}

			private ColorRgba[] InternalRescale(int w, int h, FilterMethod filter)
			{
				if (this.width == w && this.height == h) return null;
				
				ColorRgba[]	tempDestData	= new ColorRgba[w * h];
				if (filter == FilterMethod.Nearest)
				{
					//for (int i = 0; i < tempDestData.Length; i++)
					System.Threading.Tasks.Parallel.For(0, tempDestData.Length, i =>
					{
						int y = i / w;
						int x = i - (y * w);

						int xTmp	= (x * this.width) / w;
						int yTmp	= (y * this.height) / h;
						int nTmp	= xTmp + (yTmp * this.width);
						tempDestData[i].R = this.data[nTmp].R;
						tempDestData[i].G = this.data[nTmp].G;
						tempDestData[i].B = this.data[nTmp].B;
						tempDestData[i].A = this.data[nTmp].A;
					});
				}
				else if (filter == FilterMethod.Linear)
				{
					//for (int i = 0; i < tempDestData.Length; i++)
					System.Threading.Tasks.Parallel.For(0, tempDestData.Length, i =>
					{
						int y = i / w;
						int x = i - (y * w);

						float	xRatio	= ((float)(x * this.width) / (float)w) + ((w != this.width) ? ((w > this.width) ? -1.0f : 1.0f) : 0.0f) * 0.5f;
						float	yRatio	= ((float)(y * this.height) / (float)h) + ((h != this.height) ? ((h > this.height) ? -1.0f : 1.0f) : 0.0f) * 0.5f;
						int		xTmp	= (int)Math.Floor(xRatio);
						int		yTmp	= (int)Math.Floor(yRatio);
						bool	xLim1	= (xTmp < 0);
						bool	yLim1	= (yTmp < 0);
						bool	xLim2	= (xTmp >= this.width - 1);
						bool	yLim2	= (yTmp >= this.height - 1);
						int		nTmp0	= xTmp + (xLim1 ? 1 : 0) + ((yTmp + (yLim1 ? 1 : 0)) * this.width);
						int		nTmp1	= xTmp + (xLim2 ? 0 : 1) + ((yTmp + (yLim1 ? 1 : 0)) * this.width);
						int		nTmp2	= xTmp + (xLim1 ? 1 : 0) + ((yTmp + (yLim2 ? 0 : 1)) * this.width);
						int		nTmp3	= xTmp + (xLim2 ? 0 : 1) + ((yTmp + (yLim2 ? 0 : 1)) * this.width);
						xRatio -= xTmp;
						yRatio -= yTmp;

						tempDestData[i].R = 
							(byte)
							(
								((float)this.data[nTmp0].R * (1.0f - xRatio) * (1.0f - yRatio)) +
								((float)this.data[nTmp1].R * xRatio * (1.0f - yRatio)) + 
								((float)this.data[nTmp2].R * yRatio * (1.0f - xRatio)) +
								((float)this.data[nTmp3].R * xRatio * yRatio)
							);
						tempDestData[i].G = 
							(byte)
							(
								((float)this.data[nTmp0].G * (1.0f - xRatio) * (1.0f - yRatio)) +
								((float)this.data[nTmp1].G * xRatio * (1.0f - yRatio)) + 
								((float)this.data[nTmp2].G * yRatio * (1.0f - xRatio)) +
								((float)this.data[nTmp3].G * xRatio * yRatio)
							);
						tempDestData[i].B = 
							(byte)
							(
								((float)this.data[nTmp0].B * (1.0f - xRatio) * (1.0f - yRatio)) +
								((float)this.data[nTmp1].B * xRatio * (1.0f - yRatio)) + 
								((float)this.data[nTmp2].B * yRatio * (1.0f - xRatio)) +
								((float)this.data[nTmp3].B * xRatio * yRatio)
							);
						tempDestData[i].A = 
							(byte)
							(
								((float)this.data[nTmp0].A * (1.0f - xRatio) * (1.0f - yRatio)) +
								((float)this.data[nTmp1].A * xRatio * (1.0f - yRatio)) + 
								((float)this.data[nTmp2].A * yRatio * (1.0f - xRatio)) +
								((float)this.data[nTmp3].A * xRatio * yRatio)
							);
					});
				}
				
				return tempDestData;
			}

			object Cloning.ICloneable.CreateTargetObject(Cloning.CloneProvider provider)
			{
				return new Layer();
			}
			void Cloning.ICloneable.CopyDataTo(object targetObj, Cloning.CloneProvider provider)
			{
				Layer targetLayer = targetObj as Layer;
				targetLayer.width = this.width;
				targetLayer.height = this.height;
				targetLayer.data = this.data == null ? null : this.data.Clone() as ColorRgba[];
			}
		}


		private	List<Layer>	layers = new List<Layer>();
		
		/// <summary>
		/// [GET / SET] The main pixel data <see cref="Duality.Resources.Pixmap.Layer"/>.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Layer MainLayer
		{
			get { return this.layers.Count > 0 ? this.layers[0] : null; }
			set
			{
				if (value == null) value = new Layer();
				if (this.layers.Count > 0)
					this.layers[0] = value;
				else
					this.layers.Add(value);
			}
		}
		/// <summary>
		/// [GET / SET] A list of pixel data <see cref="Duality.Resources.Pixmap.Layer">Layers</see>.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public List<Layer> PixelData
		{
			get { return this.layers; }
			set
			{
				if (value == null)
					this.layers.Clear();
				else
					this.layers = value;
			}
		}
		/// <summary>
		/// [GET] The Width of the actual pixel data.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public int Width
		{
			get { return this.MainLayer != null ? this.MainLayer.Width : 0; }
		}
		/// <summary>
		/// [GET] The Height of the actual pixel data.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public int Height
		{
			get { return this.MainLayer != null ? this.MainLayer.Height : 0; }
		}
 
		/// <summary>
		/// Creates a new, empty Pixmap.
		/// </summary>
		public Pixmap() {}
		/// <summary>
		/// Creates a new Pixmap from the specified <see cref="System.Drawing.Bitmap"/>.
		/// </summary>
		/// <param name="image">The <see cref="System.Drawing.Bitmap"/> that will be used by the Pixmap.</param>
		public Pixmap(Bitmap image)
		{
			this.MainLayer = new Layer(image);
		}
		/// <summary>
		/// Creates a new Pixmap from the specified <see cref="Duality.Resources.Pixmap.Layer"/>.
		/// </summary>
		/// <param name="image"></param>
		public Pixmap(Layer image)
		{
			this.MainLayer = image;
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
			if (imagePath == null) imagePath = this.sourcePath;

			// We're saving this Pixmaps pixel data for the first time
			if (!this.path.Contains(':') && this.sourcePath == null) this.sourcePath = imagePath;

			this.MainLayer.SavePixelData(imagePath);
		}
		/// <summary>
		/// Replaces the Pixmaps pixel data with a new dataset that has been retrieved from file.
		/// </summary>
		/// <param name="imagePath">The path of the file from which to retrieve the new pixel data.</param>
		public void LoadPixelData(string imagePath = null)
		{
			if (imagePath == null) imagePath = this.sourcePath;

			this.sourcePath = imagePath;

			if (String.IsNullOrEmpty(this.sourcePath) || !File.Exists(this.sourcePath))
				this.MainLayer = null;
			else
			{
				this.MainLayer = new Layer(imagePath);
				this.MainLayer.ColorTransparentPixels();
			}
		}

		protected override void OnCopyTo(Resource r, Duality.Cloning.CloneProvider provider)
		{
			base.OnCopyTo(r, provider);
			Pixmap c = r as Pixmap;
			c.layers = this.layers != null ? new List<Layer>(this.layers.Select(l => l.Clone())) : null;
		}

		void ISerializable.WriteData(IDataWriter writer)
		{
			writer.WriteValue("version", ResFormat_Version_LayerPng);

			if (this.layers != null)
			{
				writer.WriteValue("layerCount", this.layers.Count);
				for (int i = 0; i < this.layers.Count; i++)
				{
					MemoryStream str = new MemoryStream(1024 * 64);
					this.layers[i].ToBitmap().Save(str, System.Drawing.Imaging.ImageFormat.Png);
					writer.WriteValue("layer" + i.ToString(), str.ToArray());
				}
			}
			else
				writer.WriteValue("layerCount", 0);

			writer.WriteValue("dataBasePath", this.sourcePath);

		}
		void ISerializable.ReadData(IDataReader reader)
		{
			int version;
			try { reader.ReadValue("version", out version); }
			catch (Exception) { version = ResFormat_Version_Unknown; }

			if (this.layers == null) this.layers = new List<Layer>();

			Bitmap bm;
			if (version == ResFormat_Version_Bitmap)
			{
				reader.ReadValue("data", out bm);
				this.layers.Clear();
				this.MainLayer = new Layer(bm);
			}
			else if (version == ResFormat_Version_Png)
			{
				byte[] dataBlock;
				reader.ReadValue("data", out dataBlock);
				bm = dataBlock != null ? new Bitmap(new MemoryStream(dataBlock)) : null;
				this.layers.Clear();
				this.MainLayer = new Layer(bm);
			}
			else if (version == ResFormat_Version_LayerPng)
			{
				int layerCount;
				reader.ReadValue("layerCount", out layerCount);
				this.layers.Clear();
				for (int i = 0; i < layerCount; i++)
				{
					byte[] dataBlock;
					reader.ReadValue("layer" + i.ToString(), out dataBlock);
					bm = dataBlock != null ? new Bitmap(new MemoryStream(dataBlock)) : null;
					this.layers.Add(new Layer(bm));
				}
			}
			else
			{
				this.layers.Clear();
			}

			reader.ReadValue("dataBasePath", out this.sourcePath);
		}
	}
}
