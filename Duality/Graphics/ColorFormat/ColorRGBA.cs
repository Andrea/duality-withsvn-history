using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Duality.ColorFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorRgba : IColorData, IEquatable<ColorRgba>
	{
		public const int CompSize	= sizeof(byte);
		public const int OffsetR	= 0;
		public const int OffsetG	= OffsetR + CompSize;
		public const int OffsetB	= OffsetG + CompSize;
		public const int OffsetA	= OffsetB + CompSize;
		public const int Size		= OffsetA + CompSize;

		public static readonly ColorRgba White				= new ColorRgba(255,	255,	255);
		public static readonly ColorRgba Black				= new ColorRgba(0,		0,		0);

		public static readonly ColorRgba Red				= new ColorRgba(255,	0,		0);
		public static readonly ColorRgba Green				= new ColorRgba(0,		255,	0);
		public static readonly ColorRgba Blue				= new ColorRgba(0,		0,		255);

		public static readonly ColorRgba VeryLightGrey		= new ColorRgba(224,	224,	224);
		public static readonly ColorRgba LightGrey			= new ColorRgba(192,	192,	192);
		public static readonly ColorRgba Grey				= new ColorRgba(128,	128,	128);
		public static readonly ColorRgba DarkGrey			= new ColorRgba(64,		64,		64);
		public static readonly ColorRgba VeryDarkGrey		= new ColorRgba(32,		32,		32);

		public static readonly ColorRgba TransparentWhite	= new ColorRgba(255,	255,	255,	0);
		public static readonly ColorRgba TransparentBlack	= new ColorRgba(0,		0,		0,		0);

		public	byte	r;
		public	byte	g;
		public	byte	b;
		public	byte	a;

		public ColorRgba(uint rgba)
		{
			this.r = (byte)((rgba & 0xFF000000) >> 24);
			this.g = (byte)((rgba & 0x00FF0000) >> 16);
			this.b = (byte)((rgba & 0x0000FF00) >> 8);
			this.a = (byte)(rgba & 0x000000FF);
		}
		public ColorRgba(byte r, byte g, byte b, byte a = 255)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}
		public ColorRgba(byte lum, byte a = 255)
		{
			this.r = lum;
			this.g = lum;
			this.b = lum;
			this.a = a;
		}
		public ColorRgba(float r, float g, float b, float a = 1.0f)
		{
			this.r = (byte)MathF.Clamp((int)(r * 255.0f), 0, 255);
			this.g = (byte)MathF.Clamp((int)(g * 255.0f), 0, 255);
			this.b = (byte)MathF.Clamp((int)(b * 255.0f), 0, 255);
			this.a = (byte)MathF.Clamp((int)(a * 255.0f), 0, 255);
		}
		public ColorRgba(float lum, float a = 1.0f)
		{
			this.r = (byte)MathF.Clamp((int)(lum * 255.0f), 0, 255);
			this.g = this.r;
			this.b = this.r;
			this.a = (byte)MathF.Clamp((int)(a * 255.0f), 0, 255);
		}
		
		public ColorRgba WithRed(byte r)
		{
			return new ColorRgba(r, this.g, this.b, this.a);
		}
		public ColorRgba WithGreen(byte g)
		{
			return new ColorRgba(this.r, g, this.b, this.a);
		}
		public ColorRgba WithBlue(byte b)
		{
			return new ColorRgba(this.r, this.g, b, this.a);
		}
		public ColorRgba WithAlpha(byte a)
		{
			return new ColorRgba(this.r, this.g, this.b, a);
		}
		public ColorRgba WithRed(float r)
		{
			return new ColorRgba((byte)MathF.Clamp((int)(r * 255.0f), 0, 255), this.g, this.b, this.a);
		}
		public ColorRgba WithGreen(float g)
		{
			return new ColorRgba(this.r, (byte)MathF.Clamp((int)(g * 255.0f), 0, 255), this.b, this.a);
		}
		public ColorRgba WithBlue(float b)
		{
			return new ColorRgba(this.r, this.g, (byte)MathF.Clamp((int)(b * 255.0f), 0, 255), this.a);
		}
		public ColorRgba WithAlpha(float a)
		{
			return new ColorRgba(this.r, this.g, this.b, (byte)MathF.Clamp((int)(a * 255.0f), 0, 255));
		}

		public float GetLuminance()
		{
			return (0.2126f * this.r + 0.7152f * this.g + 0.0722f * this.b) / 255.0f;
		}

		public uint ToIntRgba()
		{
			return ((uint)this.r << 24) | ((uint)this.g << 16) | ((uint)this.b << 8) | ((uint)this.a);
		}
		public uint ToIntArgb()
		{
			return ((uint)this.a << 24) | ((uint)this.r << 16) | ((uint)this.g << 8) | ((uint)this.b);
		}
		public ColorHsva ToHsva()
		{
			return ColorHsva.FromRgba(this);
		}

		public void SetIntArgb(uint argb)
		{
			this.a = (byte)((argb & 0xFF000000) >> 24);
			this.r = (byte)((argb & 0x00FF0000) >> 16);
			this.g = (byte)((argb & 0x0000FF00) >> 8);
			this.b = (byte)(argb & 0x000000FF);
		}
		public void SetIntRgba(uint rgba)
		{
			this.r = (byte)((rgba & 0xFF000000) >> 24);
			this.g = (byte)((rgba & 0x00FF0000) >> 16);
			this.b = (byte)((rgba & 0x0000FF00) >> 8);
			this.a = (byte)(rgba & 0x000000FF);
		}
		public void SetHsva(ColorHsva hsva)
		{
			this = hsva.ToRgba();
		}

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
			return (int)this.ToIntRgba();
		}
		public override string ToString()
		{
			return string.Format("ColorRGBA ({0}, {1}, {2}, {3} / #{4:X8})", this.r, this.g, this.b, this.a, this.ToIntRgba());
		}

		public static ColorRgba FromIntRgba(uint rgba)
		{
			ColorRgba temp = new ColorRgba();
			temp.SetIntRgba(rgba);
			return temp;
		}
		public static ColorRgba FromIntArgb(uint argb)
		{
			ColorRgba temp = new ColorRgba();
			temp.SetIntArgb(argb);
			return temp;
		}
		public static ColorRgba FromHsva(ColorHsva hsva)
		{
			return hsva.ToRgba();
		}

		public static ColorRgba Mix(ColorRgba first, ColorRgba second, float factor)
		{
			float invFactor = 1.0f - factor;
			return new ColorRgba(
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.r * invFactor + second.r * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.g * invFactor + second.g * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.b * invFactor + second.b * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.a * invFactor + second.a * factor)));
		}

		public static bool operator ==(ColorRgba left, ColorRgba right)
        {
            return left.Equals(right);
        }
		public static bool operator !=(ColorRgba left, ColorRgba right)
        {
            return !left.Equals(right);
        }
		public static ColorRgba operator &(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(left.ToIntRgba() & right.ToIntRgba());
        }
		public static ColorRgba operator |(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(left.ToIntRgba() | right.ToIntRgba());
        }
		public static ColorRgba operator +(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)Math.Min(255, left.r + right.r), 
				(byte)Math.Min(255, left.g + right.g), 
				(byte)Math.Min(255, left.b + right.b), 
				(byte)Math.Min(255, left.a + right.a));
        }
		public static ColorRgba operator -(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)Math.Max(0, left.r - right.r), 
				(byte)Math.Max(0, left.g - right.g), 
				(byte)Math.Max(0, left.b - right.b), 
				(byte)Math.Max(0, left.a - right.a));
        }
		public static ColorRgba operator *(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
				(byte)(left.r * right.r / 255), 
				(byte)(left.g * right.g / 255), 
				(byte)(left.b * right.b / 255), 
				(byte)(left.a * right.a / 255));
        }
		public static ColorRgba operator -(ColorRgba c)
        {
            return new ColorRgba(
				(byte)(255 - c.r), 
				(byte)(255 - c.g), 
				(byte)(255 - c.b), 
				(byte)(255 - c.a));
        }
		
		public static explicit operator ColorRgba(uint c)
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
		public static explicit operator uint(ColorRgba c)
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
