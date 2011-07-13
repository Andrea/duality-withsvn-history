using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using Duality.ColorFormat;

namespace Duality
{
	public static class ExtMethodsRandom
	{
		public static byte NextByte(this Random r)
		{
			return (byte)(r.Next() % 256);
		}
		public static byte NextByte(this Random r, byte max)
		{
			return (byte)(r.Next() % ((int)max + 1));
		}
		public static byte NextByte(this Random r, byte min, byte max)
		{
			return (byte)(min + (r.Next() % ((int)max - min + 1)));
		}

		public static float NextFloat(this Random r)
		{
			return (float)r.NextDouble();
		}
		public static float NextFloat(this Random r, float max)
		{
			return max * (float)r.NextDouble();
		}
		public static float NextFloat(this Random r, float min, float max)
		{
			return min + (max - min) * (float)r.NextDouble();
		}

		public static bool NextBool(this Random r)
		{
			return r.NextDouble() > 0.5d;
		}
		
		public static Vector2 NextVector2(this Random r)
		{
			float angle = r.NextFloat(0.0f, MathF.RadAngle360);
			return new Vector2(MathF.Sin(angle), -MathF.Cos(angle));
		}
		public static Vector2 NextVector2(this Random r, float radius)
		{
			float angle = r.NextFloat(0.0f, MathF.RadAngle360);
			return new Vector2(MathF.Sin(angle), -MathF.Cos(angle)) * r.NextFloat(0.0f, radius);
		}
		public static Vector2 NextVector2(this Random r, float x, float y, float w, float h)
		{
			return new Vector2(r.NextFloat(x, x + w), r.NextFloat(y, y + h));
		}
		public static Vector2 NextVector2(this Random r, Rect rect)
		{
			return new Vector2(r.NextFloat(rect.x, rect.x + rect.w), r.NextFloat(rect.y, rect.y + rect.h));
		}
		
		public static Vector3 NextVector3(this Random r)
		{
			Quaternion rot = Quaternion.Identity;
			rot *= Quaternion.FromAxisAngle(Vector3.UnitZ, r.NextFloat(MathF.RadAngle360));
			rot *= Quaternion.FromAxisAngle(Vector3.UnitX, r.NextFloat(MathF.RadAngle360));
			rot *= Quaternion.FromAxisAngle(Vector3.UnitY, r.NextFloat(MathF.RadAngle360));
			return Vector3.Transform(Vector3.UnitX, rot);
		}
		public static Vector3 NextVector3(this Random r, float radius)
		{
			Quaternion rot = Quaternion.Identity;
			rot *= Quaternion.FromAxisAngle(Vector3.UnitZ, r.NextFloat(MathF.RadAngle360));
			rot *= Quaternion.FromAxisAngle(Vector3.UnitX, r.NextFloat(MathF.RadAngle360));
			rot *= Quaternion.FromAxisAngle(Vector3.UnitY, r.NextFloat(MathF.RadAngle360));
			return Vector3.Transform(new Vector3(r.NextFloat(radius), 0, 0), rot);
		}
		public static Vector3 NextVector3(this Random r, float x, float y, float z, float w, float h, float d)
		{
			return new Vector3(r.NextFloat(x, x + w), r.NextFloat(y, y + h), r.NextFloat(z, z + d));
		}

		public static ColorRGBA NextColorRGBA(this Random r)
		{
			return new ColorRGBA(
				r.NextByte(),
				r.NextByte(),
				r.NextByte(),
				255);
		}
		public static ColorHSVA NextColorHSVA(this Random r)
		{
			return new ColorHSVA(
				r.NextFloat(),
				r.NextFloat(),
				r.NextFloat(),
				1.0f);
		}
	}
}
