using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Duality.ColorFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorHsva : IColorData, IEquatable<ColorHsva>
	{
		public const int CompSize	= sizeof(float);
		public const int OffsetH	= 0;
		public const int OffsetS	= OffsetH + CompSize;
		public const int OffsetV	= OffsetS + CompSize;
		public const int OffsetA	= OffsetV + CompSize;
		public const int Size		= OffsetA + CompSize;

		public static readonly ColorHsva White				= ColorRgba.White.ToHsva();
		public static readonly ColorHsva Black				= ColorRgba.Black.ToHsva();

		public static readonly ColorHsva Red				= ColorRgba.Red.ToHsva();
		public static readonly ColorHsva Green				= ColorRgba.Green.ToHsva();
		public static readonly ColorHsva Blue				= ColorRgba.Blue.ToHsva();

		public static readonly ColorHsva VeryLightGrey		= ColorRgba.VeryLightGrey.ToHsva();
		public static readonly ColorHsva LightGrey			= ColorRgba.LightGrey.ToHsva();
		public static readonly ColorHsva Grey				= ColorRgba.Grey.ToHsva();
		public static readonly ColorHsva DarkGrey			= ColorRgba.DarkGrey.ToHsva();
		public static readonly ColorHsva VeryDarkGrey		= ColorRgba.VeryDarkGrey.ToHsva();

		public static readonly ColorHsva TransparentWhite	= ColorRgba.TransparentWhite.ToHsva();
		public static readonly ColorHsva TransparentBlack	= ColorRgba.TransparentBlack.ToHsva();

		public	float	h;
		public	float	s;
		public	float	v;
		public	float	a;

		public ColorHsva(float h, float s, float v, float a = 1.0f)
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
		public ColorRgba ToRgba()
		{
			float hTemp = this.h * 360.0f / 60.0f;
			int hi = (int)MathF.Floor(hTemp) % 6;
			float f = hTemp - MathF.Floor(hTemp);

			float vTemp = this.v * 255.0f;
			byte v = (byte)vTemp;
			byte p = (byte)(vTemp * (1 - this.s));
			byte q = (byte)(vTemp * (1 - f * this.s));
			byte t = (byte)(vTemp * (1 - (1 - f) * this.s));

			if (hi == 0)		return new ColorRgba(v, t, p, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
			else if (hi == 1)	return new ColorRgba(q, v, p, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
			else if (hi == 2)	return new ColorRgba(p, v, t, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
			else if (hi == 3)	return new ColorRgba(p, q, v, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
			else if (hi == 4)	return new ColorRgba(t, p, v, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
			else				return new ColorRgba(v, p, q, (byte)(int)MathF.Clamp(this.a * 255.0f, 0.0f, 255.0f));
		}

		public void SetIntRgba(uint rgba)
		{
			this.SetRgba(ColorRgba.FromIntRgba(rgba));
		}
		public void SetIntArgb(uint argb)
		{
			this.SetRgba(ColorRgba.FromIntArgb(argb));			
		}
		public void SetRgba(ColorRgba rgba)
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

		public bool Equals(ColorHsva other)
		{
			return this.h == other.h && this.s == other.s && this.v == other.v && this.a == other.a;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is ColorHsva))
				return false;
			else
				return this.Equals((ColorHsva)obj);
		}
		public override int GetHashCode()
		{
			return (int)this.ToIntRgba();
		}
		public override string ToString()
		{
			return string.Format("ColorHSVA ({0:F}, {1:F}, {2:F}, {3:F} / #{4:X8})", this.h, this.s, this.v, this.a, this.ToIntRgba());
		}

		public static ColorHsva FromIntRgba(uint rgba)
		{
			ColorHsva temp = new ColorHsva();
			temp.SetIntRgba(rgba);
			return temp;
		}
		public static ColorHsva FromIntArgb(uint argb)
		{
			ColorHsva temp = new ColorHsva();
			temp.SetIntArgb(argb);
			return temp;
		}
		public static ColorHsva FromRgba(ColorRgba rgba)
		{
			ColorHsva temp = new ColorHsva();
			temp.SetRgba(rgba);
			return temp;
		}

		public static bool operator ==(ColorHsva left, ColorHsva right)
        {
            return left.Equals(right);
        }
		public static bool operator !=(ColorHsva left, ColorHsva right)
        {
            return !left.Equals(right);
        }

		public static explicit operator ColorHsva(uint c)
		{
			return ColorHsva.FromIntRgba(c);
		}
		public static explicit operator ColorHsva(ColorRgba c)
		{
			return ColorHsva.FromRgba(c);
		}
		public static explicit operator ColorHsva(OpenTK.Graphics.Color4 c)
		{
			return ColorHsva.FromRgba(new ColorRgba(
				(byte)Math.Max(0, Math.Min(255, 255 * c.R)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.G)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.B)),
				(byte)Math.Max(0, Math.Min(255, 255 * c.A))));
		}
		public static explicit operator uint(ColorHsva c)
		{
			return c.ToIntRgba();
		}
		public static explicit operator ColorRgba(ColorHsva c)
		{
			return c.ToRgba();
		}
		public static explicit operator OpenTK.Graphics.Color4(ColorHsva c)
		{
			ColorRgba temp = c.ToRgba();
			return new OpenTK.Graphics.Color4(
				temp.r,
				temp.g,
				temp.b,
				temp.a);
		}
	}
}
