using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Duality.ColorFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorRGBA : IColorData, IEquatable<ColorRGBA>
	{
		public const int CompSize	= sizeof(byte);
		public const int OffsetR	= 0;
		public const int OffsetG	= OffsetR + CompSize;
		public const int OffsetB	= OffsetG + CompSize;
		public const int OffsetA	= OffsetB + CompSize;
		public const int Size		= OffsetA + CompSize;

		public static readonly ColorRGBA White				= new ColorRGBA(255,	255,	255);
		public static readonly ColorRGBA Black				= new ColorRGBA(0,		0,		0);

		public static readonly ColorRGBA Red				= new ColorRGBA(255,	0,		0);
		public static readonly ColorRGBA Green				= new ColorRGBA(0,		255,	0);
		public static readonly ColorRGBA Blue				= new ColorRGBA(0,		0,		255);

		public static readonly ColorRGBA VeryLightGrey		= new ColorRGBA(224,	224,	224);
		public static readonly ColorRGBA LightGrey			= new ColorRGBA(192,	192,	192);
		public static readonly ColorRGBA Grey				= new ColorRGBA(128,	128,	128);
		public static readonly ColorRGBA DarkGrey			= new ColorRGBA(64,		64,		64);
		public static readonly ColorRGBA VeryDarkGrey		= new ColorRGBA(32,		32,		32);

		public static readonly ColorRGBA TransparentWhite	= new ColorRGBA(255,	255,	255,	0);
		public static readonly ColorRGBA TransparentBlack	= new ColorRGBA(0,		0,		0,		0);

		public	byte	r;
		public	byte	g;
		public	byte	b;
		public	byte	a;

		public ColorRGBA(uint rgba)
		{
			this.r = (byte)((rgba & 0xFF000000) >> 24);
			this.g = (byte)((rgba & 0x00FF0000) >> 16);
			this.b = (byte)((rgba & 0x0000FF00) >> 8);
			this.a = (byte)(rgba & 0x000000FF);
		}
		public ColorRGBA(byte r, byte g, byte b, byte a = 255)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
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
		public ColorHSVA ToHsva()
		{
			return ColorHSVA.FromRgba(this);
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
		public void SetHsva(ColorHSVA hsva)
		{
			this = hsva.ToRgba();
		}

		public bool Equals(ColorRGBA other)
		{
			return this.r == other.r && this.g == other.g && this.b == other.b && this.a == other.a;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is ColorRGBA))
				return false;
			else
				return this.Equals((ColorRGBA)obj);
		}
		public override int GetHashCode()
		{
			return (int)this.ToIntRgba();
		}
		public override string ToString()
		{
			return string.Format("ColorRGBA ({0}, {1}, {2}, {3} / #{4:X8})", this.r, this.g, this.b, this.a, this.ToIntRgba());
		}

		public static ColorRGBA FromIntRgba(uint rgba)
		{
			ColorRGBA temp = new ColorRGBA();
			temp.SetIntRgba(rgba);
			return temp;
		}
		public static ColorRGBA FromIntArgb(uint argb)
		{
			ColorRGBA temp = new ColorRGBA();
			temp.SetIntArgb(argb);
			return temp;
		}
		public static ColorRGBA FromHsva(ColorHSVA hsva)
		{
			return hsva.ToRgba();
		}

		public static ColorRGBA Mix(ColorRGBA first, ColorRGBA second, float factor)
		{
			float invFactor = 1.0f - factor;
			return new ColorRGBA(
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.r * invFactor + second.r * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.g * invFactor + second.g * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.b * invFactor + second.b * factor)),
				(byte)Math.Min(255.0f, Math.Max(0.0f, first.a * invFactor + second.a * factor)));
		}

		public static bool operator ==(ColorRGBA left, ColorRGBA right)
        {
            return left.Equals(right);
        }
		public static bool operator !=(ColorRGBA left, ColorRGBA right)
        {
            return !left.Equals(right);
        }
		public static ColorRGBA operator &(ColorRGBA left, ColorRGBA right)
        {
            return new ColorRGBA(left.ToIntRgba() & right.ToIntRgba());
        }
		public static ColorRGBA operator |(ColorRGBA left, ColorRGBA right)
        {
            return new ColorRGBA(left.ToIntRgba() | right.ToIntRgba());
        }
		public static ColorRGBA operator +(ColorRGBA left, ColorRGBA right)
        {
            return new ColorRGBA(
				(byte)Math.Min(255, left.r + right.r), 
				(byte)Math.Min(255, left.g + right.g), 
				(byte)Math.Min(255, left.b + right.b), 
				(byte)Math.Min(255, left.a + right.a));
        }
		public static ColorRGBA operator -(ColorRGBA left, ColorRGBA right)
        {
            return new ColorRGBA(
				(byte)Math.Max(0, left.r - right.r), 
				(byte)Math.Max(0, left.g - right.g), 
				(byte)Math.Max(0, left.b - right.b), 
				(byte)Math.Max(0, left.a - right.a));
        }
		public static ColorRGBA operator *(ColorRGBA left, ColorRGBA right)
        {
            return new ColorRGBA(
				(byte)(left.r * right.r / 255), 
				(byte)(left.g * right.g / 255), 
				(byte)(left.b * right.b / 255), 
				(byte)(left.a * right.a / 255));
        }
		public static ColorRGBA operator -(ColorRGBA c)
        {
            return new ColorRGBA(
				(byte)(255 - c.r), 
				(byte)(255 - c.g), 
				(byte)(255 - c.b), 
				(byte)(255 - c.a));
        }
		
		public static explicit operator ColorRGBA(uint c)
		{
			return new ColorRGBA(c);
		}
		public static explicit operator ColorRGBA(ColorHSVA c)
		{
			return c.ToRgba();
		}
		public static explicit operator ColorRGBA(OpenTK.Graphics.Color4 c)
		{
			return new ColorRGBA(
				(byte)Math.Max(0, Math.Min(255, 255 * c.R)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.G)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.B)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.A)));
		}
		public static explicit operator uint(ColorRGBA c)
		{
			return c.ToIntRgba();
		}
		public static explicit operator ColorHSVA(ColorRGBA c)
		{
			return ColorHSVA.FromRgba(c);
		}
		public static explicit operator OpenTK.Graphics.Color4(ColorRGBA c)
		{
			return new OpenTK.Graphics.Color4(
				c.r,
				c.g,
				c.b,
				c.a);
		}
	}
}
