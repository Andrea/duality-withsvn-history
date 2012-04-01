using System;
using System.Runtime.InteropServices;

namespace Duality.ColorFormat
{
	/// <summary>
	/// Represents a 4-byte Rgba color value.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorRgba : IColorData, IEquatable<ColorRgba>
	{
		/// <summary>
		/// Size of a single color component in bytes.
		/// </summary>
		public const int CompSize	= sizeof(byte);
		/// <summary>
		/// Byte offset of the red value.
		/// </summary>
		public const int OffsetR	= 0;
		/// <summary>
		/// Byte offset of the green value.
		/// </summary>
		public const int OffsetG	= OffsetR + CompSize;
		/// <summary>
		/// Byte offset of the blue value.
		/// </summary>
		public const int OffsetB	= OffsetG + CompSize;
		/// <summary>
		/// Byte offset of the alpha value.
		/// </summary>
		public const int OffsetA	= OffsetB + CompSize;
		/// <summary>
		/// Total size of the struct in bytes.
		/// </summary>
		public const int Size		= OffsetA + CompSize;

		/// <summary>
		/// White.
		/// </summary>
		public static readonly ColorRgba White				= new ColorRgba(255,	255,	255);
		/// <summary>
		/// Black.
		/// </summary>
		public static readonly ColorRgba Black				= new ColorRgba(0,		0,		0);

		/// <summary>
		/// Fully saturated and max-brightness red. Also known as [255,0,0].
		/// </summary>
		public static readonly ColorRgba Red				= new ColorRgba(255,	0,		0);
		/// <summary>
		/// Fully saturated and max-brightness green. Also known as [0,255,0].
		/// </summary>
		public static readonly ColorRgba Green				= new ColorRgba(0,		255,	0);
		/// <summary>
		/// Fully saturated and max-brightness blue. Also known as [0,0,255].
		/// </summary>
		public static readonly ColorRgba Blue				= new ColorRgba(0,		0,		255);

		/// <summary>
		/// A very light grey. Value: 224.
		/// </summary>
		public static readonly ColorRgba VeryLightGrey		= new ColorRgba(224,	224,	224);
		/// <summary>
		/// A light grey. Value: 192.
		/// </summary>
		public static readonly ColorRgba LightGrey			= new ColorRgba(192,	192,	192);
		/// <summary>
		/// Medium grey. Value: 128.
		/// </summary>
		public static readonly ColorRgba Grey				= new ColorRgba(128,	128,	128);
		/// <summary>
		/// A dark grey. Value: 64.
		/// </summary>
		public static readonly ColorRgba DarkGrey			= new ColorRgba(64,		64,		64);
		/// <summary>
		/// A very dark grey. Value: 32.
		/// </summary>
		public static readonly ColorRgba VeryDarkGrey		= new ColorRgba(32,		32,		32);

		/// <summary>
		/// Transparent white. Completely invisible, when drawn, but might make a difference as
		/// a background color.
		/// </summary>
		public static readonly ColorRgba TransparentWhite	= new ColorRgba(255,	255,	255,	0);
		/// <summary>
		/// Transparent black. Completely invisible, when drawn, but might make a difference as
		/// a background color.
		/// </summary>
		public static readonly ColorRgba TransparentBlack	= new ColorRgba(0,		0,		0,		0);

		/// <summary>
		/// Red color component.
		/// </summary>
		public	byte	r;
		/// <summary>
		/// Green color component.
		/// </summary>
		public	byte	g;
		/// <summary>
		/// Blue color component.
		/// </summary>
		public	byte	b;
		/// <summary>
		/// Alpha color component. Usually treated as opacity.
		/// </summary>
		public	byte	a;

		/// <summary>
		/// Creates a new color based on an existing one. This is basically a copy-constructor.
		/// </summary>
		/// <param name="clr"></param>
		public ColorRgba(ColorRgba clr)
		{
			this.r = clr.r;
			this.g = clr.g;
			this.b = clr.b;
			this.a = clr.a;
		}
		/// <summary>
		/// Creates a new color based on an int-Rgba value.
		/// </summary>
		/// <param name="rgba"></param>
		public ColorRgba(int rgba)
		{
			this.r = (byte)((rgba & 0xFF000000) >> 24);
			this.g = (byte)((rgba & 0x00FF0000) >> 16);
			this.b = (byte)((rgba & 0x0000FF00) >> 8);
			this.a = (byte)(rgba & 0x000000FF);
		}
		/// <summary>
		/// Creates a new color.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="a">The alpha component.</param>
		public ColorRgba(byte r, byte g, byte b, byte a = 255)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}
		/// <summary>
		/// Creates a new color based on value (brightness) and alpha.
		/// </summary>
		/// <param name="value">The value / brightness of the color.</param>
		/// <param name="a">The colors alpha value.</param>
		public ColorRgba(byte value, byte a = 255)
		{
			this.r = value;
			this.g = value;
			this.b = value;
			this.a = a;
		}
		/// <summary>
		/// Creates a new color.
		/// </summary>
		/// <param name="r">The red component as float [0.0f - 1.0f].</param>
		/// <param name="g">The green component as float [0.0f - 1.0f].</param>
		/// <param name="b">The blue component as float [0.0f - 1.0f].</param>
		/// <param name="a">The alpha component as float [0.0f - 1.0f].</param>
		public ColorRgba(float r, float g, float b, float a = 1.0f)
		{
			this.r = (byte)MathF.Clamp((int)(r * 255.0f), 0, 255);
			this.g = (byte)MathF.Clamp((int)(g * 255.0f), 0, 255);
			this.b = (byte)MathF.Clamp((int)(b * 255.0f), 0, 255);
			this.a = (byte)MathF.Clamp((int)(a * 255.0f), 0, 255);
		}
		/// <summary>
		/// Creates a new color based on value (brightness) and alpha.
		/// </summary>
		/// <param name="value">The value / brightness of the color as float [0.0f - 1.0f].</param>
		/// <param name="a">The colors alpha value as float [0.0f - 1.0f].</param>
		public ColorRgba(float value, float a = 1.0f)
		{
			this.r = (byte)MathF.Clamp((int)(value * 255.0f), 0, 255);
			this.g = this.r;
			this.b = this.r;
			this.a = (byte)MathF.Clamp((int)(a * 255.0f), 0, 255);
		}
		
		/// <summary>
		/// Returns a new version of the color with an adjusted red component.
		/// </summary>
		/// <param name="r">The new red component.</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithRed(byte r)
		{
			return new ColorRgba(r, this.g, this.b, this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted green component.
		/// </summary>
		/// <param name="g">The new green component.</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithGreen(byte g)
		{
			return new ColorRgba(this.r, g, this.b, this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted blue component.
		/// </summary>
		/// <param name="b">The new blue component.</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithBlue(byte b)
		{
			return new ColorRgba(this.r, this.g, b, this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted alpha component.
		/// </summary>
		/// <param name="a">The new alpha component.</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithAlpha(byte a)
		{
			return new ColorRgba(this.r, this.g, this.b, a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted red component.
		/// </summary>
		/// <param name="r">The new red component as float [0.0f - 1.0f].</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithRed(float r)
		{
			return new ColorRgba((byte)MathF.Clamp((int)(r * 255.0f), 0, 255), this.g, this.b, this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted green component.
		/// </summary>
		/// <param name="g">The new green component as float [0.0f - 1.0f].</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithGreen(float g)
		{
			return new ColorRgba(this.r, (byte)MathF.Clamp((int)(g * 255.0f), 0, 255), this.b, this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted blue component.
		/// </summary>
		/// <param name="b">The new blue component as float [0.0f - 1.0f].</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithBlue(float b)
		{
			return new ColorRgba(this.r, this.g, (byte)MathF.Clamp((int)(b * 255.0f), 0, 255), this.a);
		}
		/// <summary>
		/// Returns a new version of the color with an adjusted alpha component.
		/// </summary>
		/// <param name="a">The new alpha component as float [0.0f - 1.0f].</param>
		/// <returns>A new color with the specified adjustments.</returns>
		public ColorRgba WithAlpha(float a)
		{
			return new ColorRgba(this.r, this.g, this.b, (byte)MathF.Clamp((int)(a * 255.0f), 0, 255));
		}

		/// <summary>
		/// Calculates the colors luminance. It is an approximation on how bright the color actually looks to
		/// the human eye, weighting each color component differently.
		/// </summary>
		/// <returns>The colors luminance as float [0.0f - 1.0f].</returns>
		public float GetLuminance()
		{
			return (0.2126f * this.r + 0.7152f * this.g + 0.0722f * this.b) / 255.0f;
		}

		/// <summary>
		/// Converts the color to int-Rgba.
		/// </summary>
		/// <returns></returns>
		public int ToIntRgba()
		{
			return ((int)this.r << 24) | ((int)this.g << 16) | ((int)this.b << 8) | ((int)this.a);
		}
		/// <summary>
		/// Converts the color to int-Argb.
		/// </summary>
		/// <returns></returns>
		public int ToIntArgb()
		{
			return ((int)this.a << 24) | ((int)this.r << 16) | ((int)this.g << 8) | ((int)this.b);
		}
		/// <summary>
		/// Converts the color to Hsva.
		/// </summary>
		/// <returns></returns>
		public ColorHsva ToHsva()
		{
			return ColorHsva.FromRgba(this);
		}

		/// <summary>
		/// Adjusts the color to match the specified int-Argb color.
		/// </summary>
		/// <param name="argb"></param>
		public void SetIntArgb(int argb)
		{
			this.a = (byte)((argb & 0xFF000000) >> 24);
			this.r = (byte)((argb & 0x00FF0000) >> 16);
			this.g = (byte)((argb & 0x0000FF00) >> 8);
			this.b = (byte)(argb & 0x000000FF);
		}
		/// <summary>
		/// Adjusts the color to match the specified int-Rgba color.
		/// </summary>
		/// <param name="rgba"></param>
		public void SetIntRgba(int rgba)
		{
			this.r = (byte)((rgba & 0xFF000000) >> 24);
			this.g = (byte)((rgba & 0x00FF0000) >> 16);
			this.b = (byte)((rgba & 0x0000FF00) >> 8);
			this.a = (byte)(rgba & 0x000000FF);
		}
		/// <summary>
		/// Adjusts the color to match the specified Hsva color.
		/// </summary>
		/// <param name="hsva"></param>
		public void SetHsva(ColorHsva hsva)
		{
			this = hsva.ToRgba();
		}

		/// <summary>
		/// Returns whether this color equals the specified one.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ColorRgba other)
		{
			return this.r == other.r && this.g == other.g && this.b == other.b && this.a == other.a;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is ColorRgba))
				return false;
			else
				return this.Equals((ColorRgba)obj);
		}
		public override int GetHashCode()
		{
			return this.ToIntRgba();
		}
		public override string ToString()
		{
			return string.Format("ColorRGBA ({0}, {1}, {2}, {3} / #{4:X8})", this.r, this.g, this.b, this.a, this.ToIntRgba());
		}

		/// <summary>
		/// Creates a new color based on an int-Rgba value.
		/// </summary>
		/// <param name="rgba"></param>
		/// <returns></returns>
		public static ColorRgba FromIntRgba(int rgba)
		{
			ColorRgba temp = new ColorRgba();
			temp.SetIntRgba(rgba);
			return temp;
		}
		/// <summary>
		/// Creates a new color based on an int-Argb value.
		/// </summary>
		/// <param name="argb"></param>
		/// <returns></returns>
		public static ColorRgba FromIntArgb(int argb)
		{
			ColorRgba temp = new ColorRgba();
			temp.SetIntArgb(argb);
			return temp;
		}
		/// <summary>
		/// Creates a new color based on a Hsva value.
		/// </summary>
		/// <param name="hsva"></param>
		/// <returns></returns>
		public static ColorRgba FromHsva(ColorHsva hsva)
		{
			return hsva.ToRgba();
		}

		/// <summary>
		/// Mixes two colors by performing a linear interpolation between both.
		/// </summary>
		/// <param name="first">The first color.</param>
		/// <param name="second">The second color.</param>
		/// <param name="factor">The linear interpolation value. Zero equals the first color, one equals the second color.</param>
		/// <returns>The interpolated / mixed color.</returns>
		public static ColorRgba Mix(ColorRgba first, ColorRgba second, float factor)
		{
			float invFactor = 1.0f - factor;
			return new ColorRgba(
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.r * invFactor + second.r * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.g * invFactor + second.g * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.b * invFactor + second.b * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.a * invFactor + second.a * factor)));
		}

		/// <summary>
		/// Returns whether two colors are equal.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static bool operator ==(ColorRgba left, ColorRgba right)
        {
            return left.Equals(right);
        }
		/// <summary>
		/// Returns whether two colors are unequal.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static bool operator !=(ColorRgba left, ColorRgba right)
        {
            return !left.Equals(right);
        }
		/// <summary>
		/// Performs a logical AND operation on the int-Rgba values of two colors.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static ColorRgba operator &(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(left.ToIntRgba() & right.ToIntRgba());
        }
		/// <summary>
		/// Performs a logical OR operation on the int-Rgba values of two colors.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static ColorRgba operator |(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(left.ToIntRgba() | right.ToIntRgba());
        }
		/// <summary>
		/// Adds two colors.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static ColorRgba operator +(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)Math.Min(255, left.r + right.r), 
				(byte)Math.Min(255, left.g + right.g), 
				(byte)Math.Min(255, left.b + right.b), 
				(byte)Math.Min(255, left.a + right.a));
        }
		/// <summary>
		/// Subtracts the second color from the first.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static ColorRgba operator -(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)Math.Max(0, left.r - right.r), 
				(byte)Math.Max(0, left.g - right.g), 
				(byte)Math.Max(0, left.b - right.b), 
				(byte)Math.Max(0, left.a - right.a));
        }
		/// <summary>
		/// Multiplies two colors.
		/// </summary>
		/// <param name="left">The first color.</param>
		/// <param name="right">The second color.</param>
		/// <returns></returns>
		public static ColorRgba operator *(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)(left.r * right.r / 255), 
				(byte)(left.g * right.g / 255), 
				(byte)(left.b * right.b / 255), 
				(byte)(left.a * right.a / 255));
        }
		/// <summary>
		/// Returns the inverse of a color.
		/// </summary>
		/// <param name="c">The color to invert.</param>
		/// <returns></returns>
		public static ColorRgba operator -(ColorRgba c)
        {
            return new ColorRgba(
				(byte)(255 - c.r), 
				(byte)(255 - c.g), 
				(byte)(255 - c.b), 
				(byte)(255 - c.a));
        }
		
		public static explicit operator ColorRgba(int c)
		{
			return new ColorRgba(c);
		}
		public static explicit operator ColorRgba(ColorHsva c)
		{
			return c.ToRgba();
		}
		public static explicit operator ColorRgba(OpenTK.Graphics.Color4 c)
		{
			return new ColorRgba(
				(byte)Math.Max(0, Math.Min(255, 255 * c.R)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.G)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.B)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.A)));
		}
		public static explicit operator int(ColorRgba c)
		{
			return c.ToIntRgba();
		}
		public static explicit operator ColorHsva(ColorRgba c)
		{
			return ColorHsva.FromRgba(c);
		}
		public static explicit operator OpenTK.Graphics.Color4(ColorRgba c)
		{
			return new OpenTK.Graphics.Color4(
				c.r,
				c.g,
				c.b,
				c.a);
		}
	}
}
