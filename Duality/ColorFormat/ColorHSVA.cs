using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Duality.ColorFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorHSVA : IColorData, IEquatable<ColorHSVA>
	{
		public const int CompSize	= sizeof(float);
		public const int OffsetH	= 0;
		public const int OffsetS	= OffsetH + CompSize;
		public const int OffsetV	= OffsetS + CompSize;
		public const int OffsetA	= OffsetV + CompSize;
		public const int Size		= OffsetA + CompSize;

		public static readonly ColorHSVA White				= ColorRGBA.White.ToHsva();
		public static readonly ColorHSVA Black				= ColorRGBA.Black.ToHsva();

		public static readonly ColorHSVA Red				= ColorRGBA.Red.ToHsva();
		public static readonly ColorHSVA Green				= ColorRGBA.Green.ToHsva();
		public static readonly ColorHSVA Blue				= ColorRGBA.Blue.ToHsva();

		public static readonly ColorHSVA VeryLightGrey		= ColorRGBA.VeryLightGrey.ToHsva();
		public static readonly ColorHSVA LightGrey			= ColorRGBA.LightGrey.ToHsva();
		public static readonly ColorHSVA Grey				= ColorRGBA.Grey.ToHsva();
		public static readonly ColorHSVA DarkGrey			= ColorRGBA.DarkGrey.ToHsva();
		public static readonly ColorHSVA VeryDarkGrey		= ColorRGBA.VeryDarkGrey.ToHsva();

		public static readonly ColorHSVA TransparentWhite	= ColorRGBA.TransparentWhite.ToHsva();
		public static readonly ColorHSVA TransparentBlack	= ColorRGBA.TransparentBlack.ToHsva();

		public	float	h;
		public	float	s;
		public	float	v;
		public	float	a;

		public ColorHSVA(float h, float s, float v, float a = 1.0f)
		{
			this.h = h;
			this.s = s;
			this.v = v;
			this.a = a;
		}

		public float GetLuminance()
		{
			return this.ToRgba().GetLuminance();
		}

		public uint ToIntRgba()
		{
			return this.ToRgba().ToIntRgba();
		}
		public uint ToIntArgb()
		{
			return this.ToRgba().ToIntArgb();
		}
		public ColorRGBA ToRgba()
		{
			float hTemp = this.h * 360.0f / 60.0f;
			int hi = (int)MathF.Floor(hTemp) % 6;
			float f = hTemp - MathF.Floor(hTemp);

			float vTemp = this.v * 255.0f;
			byte v = (byte)vTemp;
			byte p = (byte)(vTemp * (1 - this.s));
			byte q = (byte)(vTemp * (1 - f * this.s));
			byte t = (byte)(vTemp * (1 - (1 - f) * this.s));

			if (hi == 0)		return new ColorRGBA(v, t, p, 255);
			else if (hi == 1)	return new ColorRGBA(q, v, p, 255);
			else if (hi == 2)	return new ColorRGBA(p, v, t, 255);
			else if (hi == 3)	return new ColorRGBA(p, q, v, 255);
			else if (hi == 4)	return new ColorRGBA(t, p, v, 255);
			else				return new ColorRGBA(v, p, q, 255);
		}

		public void SetIntRgba(uint rgba)
		{
			this.SetRgba(ColorRGBA.FromIntRgba(rgba));
		}
		public void SetIntArgb(uint argb)
		{
			this.SetRgba(ColorRGBA.FromIntArgb(argb));			
		}
		public void SetRgba(ColorRGBA rgba)
		{
			float	min		= Math.Min(Math.Min(rgba.r, rgba.g), rgba.b);
			float	max		= Math.Max(Math.Max(rgba.r, rgba.g), rgba.b);
			float	delta	= max - min;
			
			if (max > 0.0f)
			{
				this.s = delta / max;
				this.v = max / 255.0f;

				int maxInt = MathF.RoundToInt(max);
				if (delta != 0.0f)
				{
					if (MathF.RoundToInt((float)rgba.r) == maxInt)
					{
						this.h = (float)(rgba.g - rgba.b) / delta;
					}
					else if (MathF.RoundToInt((float)rgba.g) == maxInt)
					{
						this.h = 2.0f + (float)(rgba.b - rgba.r) / delta;
					}
					else
					{
						this.h = 4.0f + (float)(rgba.r - rgba.g) / delta;
					}
					this.h *= 60.0f;
					if (this.h < 0.0f) this.h += 360.0f;
				}
				else
				{
					this.h = 0.0f;
				}
			}
			else
			{
				this.h = 0.0f;
				this.s = 0.0f;
				this.v = 0.0f;
			}

			this.h /= 360.0f;
			this.a = (float)rgba.a / 255.0f;
		}

		public bool Equals(ColorHSVA other)
		{
			return this.h == other.h && this.s == other.s && this.v == other.v && this.a == other.a;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is ColorHSVA))
				return false;
			else
				return this.Equals((ColorHSVA)obj);
		}
		public override int GetHashCode()
		{
			return (int)this.ToIntRgba();
		}
		public override string ToString()
		{
			return string.Format("ColorHSVA ({0:F}, {1:F}, {2:F}, {3:F} / #{4:X8})", this.h, this.s, this.v, this.a, this.ToIntRgba());
		}

		public static ColorHSVA FromIntRgba(uint rgba)
		{
			ColorHSVA temp = new ColorHSVA();
			temp.SetIntRgba(rgba);
			return temp;
		}
		public static ColorHSVA FromIntArgb(uint argb)
		{
			ColorHSVA temp = new ColorHSVA();
			temp.SetIntArgb(argb);
			return temp;
		}
		public static ColorHSVA FromRgba(ColorRGBA rgba)
		{
			ColorHSVA temp = new ColorHSVA();
			temp.SetRgba(rgba);
			return temp;
		}

		public static bool operator ==(ColorHSVA left, ColorHSVA right)
        {
            return left.Equals(right);
        }
		public static bool operator !=(ColorHSVA left, ColorHSVA right)
        {
            return !left.Equals(right);
        }

		public static explicit operator ColorHSVA(uint c)
		{
			return ColorHSVA.FromIntRgba(c);
		}
		public static explicit operator ColorHSVA(ColorRGBA c)
		{
			return ColorHSVA.FromRgba(c);
		}
		public static explicit operator ColorHSVA(OpenTK.Graphics.Color4 c)
		{
			return ColorHSVA.FromRgba(new ColorRGBA(
				(byte)Math.Max(0, Math.Min(255, 255 * c.R)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.G)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.B)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.A))));
		}
		public static explicit operator uint(ColorHSVA c)
		{
			return c.ToIntRgba();
		}
		public static explicit operator ColorRGBA(ColorHSVA c)
		{
			return c.ToRgba();
		}
		public static explicit operator OpenTK.Graphics.Color4(ColorHSVA c)
		{
			ColorRGBA temp = c.ToRgba();
			return new OpenTK.Graphics.Color4(
				temp.r,
				temp.g,
				temp.b,
				temp.a);
		}
	}
}
