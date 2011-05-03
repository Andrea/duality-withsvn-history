using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality
{
	public static class MathF
	{
		public const float E = (float)System.Math.E;
		public const float Pi = (float)System.Math.PI;

		public const float PiOver2 = Pi / 2.0f;
		public const float PiOver3 = Pi / 3.0f;
		public const float PiOver4 = Pi / 4.0f;
		public const float PiOver6 = Pi / 6.0f;
		public const float TwoPi = Pi * 2.0f;

		public const float RadAngle30 = PiOver6;
		public const float RadAngle45 = PiOver4;
		public const float RadAngle90 = PiOver2;
		public const float RadAngle180 = Pi;
		public const float RadAngle360 = TwoPi;

		private static Random rnd = new Random();
		public static Random Rnd
		{
			get { return rnd; }
		}


		public static float Abs(float v)
		{
			return System.Math.Abs(v);
		}
		public static int Abs(int v)
		{
			return System.Math.Abs(v);
		}

		public static float Ceiling(float v)
		{
			return (float)System.Math.Ceiling(v);
		}
		public static float Floor(float v)
		{
			return (float)System.Math.Floor(v);
		}

		public static float Round(float v)
		{
			return (float)System.Math.Round(v);
		}
		public static float Round(float v, int digits)
		{
			return (float)System.Math.Round(v, digits);
		}
		public static float Round(float v, MidpointRounding mode)
		{
			return (float)System.Math.Round(v, mode);
		}
		public static float Round(float v, int digits, MidpointRounding mode)
		{
			return (float)System.Math.Round(v, digits, mode);
		}

		public static int RoundToInt(float v)
		{
			return (int)System.Math.Round(v);
		}
		public static int RoundToInt(float v, MidpointRounding mode)
		{
			return (int)System.Math.Round(v, mode);
		}

		public static float Sign(float v)
		{
			return System.Math.Sign(v);
		}
		public static int Sign(int v)
		{
			return System.Math.Sign(v);
		}

		public static float Sqrt(float v)
		{
			return (float)System.Math.Sqrt(v);
		}
		/// <summary>
		/// Returns an approximation of a numbers inverse square root.
		/// </summary>
		/// <param name="x">A number.</param>
		/// <returns>An approximation of the inverse square root of the specified number, with an upper error bound of 0.001</returns>
		public static float InvSqrtFast(float x)
		{
			unsafe
			{
				float xhalf = 0.5f * x;
				int i = *(int*)&x;              // Read bits as integer.
				i = 0x5f375a86 - (i >> 1);      // Make an initial guess for Newton-Raphson approximation
				x = *(float*)&i;                // Convert bits back to float
				x = x * (1.5f - xhalf * x * x); // Perform left single Newton-Raphson step.
				return x;
			}
		}

		public static int Factorial(int n)
		{
			int r = 1;
			for (; n > 1; n--) r *= n;
			return r;
		}

		public static float Min(float v1, float v2)
		{
			return System.Math.Min(v1, v2);
		}
		public static float Min(float v1, float v2, float v3)
		{
			return System.Math.Min(System.Math.Min(v1, v2), v3);
		}
		public static float Min(float v1, float v2, float v3, float v4)
		{
			return System.Math.Min(System.Math.Min(System.Math.Min(v1, v2), v3), v4);
		}
		public static float Min(params float[] v)
		{
			float m = float.MaxValue;
			foreach (float val in v) m = System.Math.Min(m, val);
			return m;
		}
		public static int Min(int v1, int v2)
		{
			return System.Math.Min(v1, v2);
		}
		public static int Min(int v1, int v2, int v3)
		{
			return System.Math.Min(System.Math.Min(v1, v2), v3);
		}
		public static int Min(int v1, int v2, int v3, int v4)
		{
			return System.Math.Min(System.Math.Min(System.Math.Min(v1, v2), v3), v4);
		}
		public static int Min(params int[] v)
		{
			int m = int.MaxValue;
			foreach (int val in v) m = System.Math.Min(m, val);
			return m;
		}

		public static float Max(float v1, float v2)
		{
			return System.Math.Max(v1, v2);
		}
		public static float Max(float v1, float v2, float v3)
		{
			return System.Math.Max(System.Math.Max(v1, v2), v3);
		}
		public static float Max(float v1, float v2, float v3, float v4)
		{
			return System.Math.Max(System.Math.Max(System.Math.Max(v1, v2), v3), v4);
		}
		public static float Max(params float[] v)
		{
			float m = float.MinValue;
			foreach (float val in v) m = System.Math.Max(m, val);
			return m;
		}
		public static int Max(int v1, int v2)
		{
			return System.Math.Max(v1, v2);
		}
		public static int Max(int v1, int v2, int v3)
		{
			return System.Math.Max(System.Math.Max(v1, v2), v3);
		}
		public static int Max(int v1, int v2, int v3, int v4)
		{
			return System.Math.Max(System.Math.Max(System.Math.Max(v1, v2), v3), v4);
		}
		public static int Max(params int[] v)
		{
			int m = int.MinValue;
			foreach (int val in v) m = System.Math.Max(m, val);
			return m;
		}

		public static float Clamp(float v, float min, float max)
		{
			return System.Math.Max(min, System.Math.Min(v, max));
		}
		public static int Clamp(int v, int min, int max)
		{
			return System.Math.Max(min, System.Math.Min(v, max));
		}

		public static float Exp(float v)
		{
			return (float)System.Math.Exp(v);
		}
		public static float Log(float v)
		{
			return (float)System.Math.Log(v);
		}
		public static float Pow(float v, float e)
		{
			return (float)System.Math.Pow(v, e);
		}
		public static float Log(float v, float newBase)
		{
			return (float)System.Math.Log(v, newBase);
		}

		public static float Sin(float angle)
		{
			return (float)System.Math.Sin(angle);
		}
		public static float Cos(float angle)
		{
			return (float)System.Math.Cos(angle);
		}
		public static float Tan(float angle)
		{
			return (float)System.Math.Tan(angle);
		}
		public static float Asin(float sin)
		{
			return (float)System.Math.Asin(sin);
		}
		public static float Acos(float cos)
		{
			return (float)System.Math.Acos(cos);
		}
		public static float Atan(float tan)
		{
			return (float)System.Math.Atan(tan);
		}
		public static float Atan2(float y, float x)
		{
			return (float)System.Math.Atan2(y, x);
		}

		public static float DegToRad(float deg)
		{
			const float factor = (float)System.Math.PI / 180.0f;
			return deg * factor;
		}
		public static float RadToDeg(float rad)
		{
			const float factor = 180.0f / (float)System.Math.PI;
			return rad * factor;
		}

		/// <summary>
		/// Normalizes the given variable to the given circular area.
		/// Example: NormalizeVar(480, 0, 360) returns 120.
		/// </summary>
		/// <returns>The normalized value between min (inclusive) and max (exclusive).</returns>
		public static float NormalizeVar(float var, float min, float max)
		{
			if (var >= min && var < max) return var;

			if (var < min)
				var = max + (var % max);
			else
				var = var % max;

			return var;
		}
		public static int NormalizeVar(int var, int min, int max)
		{
			if (var >= min && var < max) return var;

			if (var < min)
				var = max + (var % max);
			else
				var = var % max;

			return var;
		}
		public static float NormalizeAngle(float var)
		{
			if (var >= 0.0f && var < RadAngle360) return var;

			if (var < 0.0f)
				var = RadAngle360 + (var % RadAngle360);
			else
				var = var % RadAngle360;

			return var;
		}

		public static float Distance(float x1, float y1, float x2, float y2)
		{
			return ((float)System.Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
		}
		public static float Distance(float x, float y)
		{
			return ((float)System.Math.Sqrt(x * x + y * y));
		}
		public static float DistanceQuad(float x1, float y1, float x2, float y2)
		{
			return (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
		}
		public static float DistanceQuad(float x, float y)
		{
			return x * x + y * y;
		}

		/// <summary>
		/// Calculates the angle between two points in 2D space.
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <returns>The angle between [x1,y1] and [x2,y2] in radian measure</returns>
		public static float Angle(float x1, float y1, float x2, float y2)
		{
			return (float)((System.Math.Atan2((y2 - y1), (x2 - x1)) + PiOver2 + TwoPi) % TwoPi);
		}
		public static float Angle(float x, float y)
		{
			return (float)((System.Math.Atan2(y, x) + PiOver2 + TwoPi) % TwoPi);
		}

		/// <summary>
		/// Assuming a circular value area, this method returns the direction to "turn" value 1 to
		/// when it comes to take the shortest way to value 2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <param name="minVal"></param>
		/// <param name="maxVal"></param>
		/// <returns>-1 for "left" / lower, 1 for "right" / higher and 0 for "stay" / equal</returns>
		public static float TurnDir(float val1, float val2, float minVal, float maxVal)
		{
			if (val1 == val2) return 0.0f;

			if (Math.Abs(val1 - val2) > (maxVal - minVal) * 0.5f)
			{
				if (val1 > val2) return 1.0f;
				else return -1.0f;
			}
			else
			{
				if (val1 > val2) return -1.0f;
				else return 1.0f;
			}
		}
		public static int TurnDir(int val1, int val2, int minVal, int maxVal)
		{
			if (val1 == val2) return 0;

			if (Math.Abs(val1 - val2) > (maxVal - minVal) * 0.5f)
			{
				if (val1 > val2) return 1;
				else return -1;
			}
			else
			{
				if (val1 > val2) return -1;
				else return 1;
			}
		}
		public static float TurnDir(float val1, float val2)
		{
			if (val1 == val2) return 0.0f;

			if (Math.Abs(val1 - val2) > RadAngle180)
			{
				if (val1 > val2) return 1.0f;
				else return -1.0f;
			}
			else
			{
				if (val1 > val2) return -1.0f;
				else return 1.0f;
			}
		}

		/// <summary>
		/// Calculates the distance between two values assuming a circular value area.
		/// </summary>
		/// <param name="v1">Value 1</param>
		/// <param name="v2">Value 2</param>
		/// <param name="vMin">Value area minimum</param>
		/// <param name="vMax">Value area maximum</param>
		/// <returns>Value distance</returns>
		public static float CircularDist(float v1, float v2, float vMin, float vMax)
		{
			float vTemp = System.Math.Abs(NormalizeVar(v1, vMin, vMax) - NormalizeVar(v2, vMin, vMax));
			if (vTemp * 2.0f <= vMax - vMin)
				return vTemp;
			else
				return (vMax - vMin) - vTemp;
		}
		public static int CircularDist(int v1, int v2, int vMin, int vMax)
		{
			int vTemp = System.Math.Abs(NormalizeVar(v1, vMin, vMax) - NormalizeVar(v2, vMin, vMax));
			if (vTemp * 2 <= vMax - vMin)
				return vTemp;
			else
				return (vMax - vMin) - vTemp;
		}
		public static float CircularDist(float v1, float v2)
		{
			float vTemp = System.Math.Abs(NormalizeAngle(v1) - NormalizeAngle(v2));
			if (vTemp * 2.0f <= RadAngle360)
				return vTemp;
			else
				return RadAngle360 - vTemp;
		}

		/// <summary>
		/// Turns and scales a specific coordinate around the specified center point
		/// </summary>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="rot"></param>
		/// <param name="scale"></param>
		/// <param name="xCenter"></param>
		/// <param name="yCenter"></param>
		public static void TransformCoord(ref float xCoord, ref float yCoord, float rot, float scale, float xCenter, float yCenter)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			float lastX = xCoord;
			xCoord = xCenter + ((xCoord - xCenter) * cos - (yCoord - yCenter) * sin) * scale;
			yCoord = yCenter + ((lastX - xCenter) * sin + (yCoord - yCenter) * cos) * scale;
		}
		public static void TransformCoord(ref float xCoord, ref float yCoord, float rot, float scale)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			float lastX = xCoord;
			xCoord = (xCoord * cos - yCoord * sin) * scale;
			yCoord = (lastX * sin + yCoord * cos) * scale;
		}
		public static void TransformCoord(ref float xCoord, ref float yCoord, float rot)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			float lastX = xCoord;
			xCoord = xCoord * cos - yCoord * sin;
			yCoord = lastX * sin + yCoord * cos;
		}

		public static void GetTransformDotVec(float rot, Vector2 scale, out Vector2 xDot, out Vector2 yDot)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			xDot = new Vector2(cos * scale.X, -sin * scale.X);
			yDot = new Vector2(sin * scale.Y, cos * scale.Y);
		}
		public static void GetTransformDotVec(float rot, float scale, out Vector2 xDot, out Vector2 yDot)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			xDot = new Vector2(cos * scale, -sin * scale);
			yDot = new Vector2(sin * scale, cos * scale);
		}
		public static void GetTransformDotVec(float rot, out Vector2 xDot, out Vector2 yDot)
		{
			float sin = (float)System.Math.Sin(rot);
			float cos = (float)System.Math.Cos(rot);
			xDot = new Vector2(cos, -sin);
			yDot = new Vector2(sin, cos);
		}

		public static void TransdormDotVec(ref Vector2 vec, ref Vector2 xDot, ref Vector2 yDot)
		{
			float oldX = vec.X;
			vec.X = vec.X * xDot.X + vec.Y * xDot.Y;
			vec.Y = oldX * yDot.X + vec.Y * yDot.Y;
		}
		public static void TransdormDotVec(ref Vector2 vec, Vector2 xDot, Vector2 yDot)
		{
			float oldX = vec.X;
			vec.X = vec.X * xDot.X + vec.Y * xDot.Y;
			vec.Y = oldX * yDot.X + vec.Y * yDot.Y;
		}
		public static Vector2 TransdormDotVec(Vector2 vec, Vector2 xDot, Vector2 yDot)
		{
			return new Vector2(
				vec.X * xDot.X + vec.Y * xDot.Y,
				vec.X * yDot.X + vec.Y * yDot.Y);
		}
		public static void TransdormDotVec(ref Vector3 vec, ref Vector2 xDot, ref Vector2 yDot)
		{
			float oldX = vec.X;
			vec.X = vec.X * xDot.X + vec.Y * xDot.Y;
			vec.Y = oldX * yDot.X + vec.Y * yDot.Y;
		}
		public static void TransdormDotVec(ref Vector3 vec, Vector2 xDot, Vector2 yDot)
		{
			float oldX = vec.X;
			vec.X = vec.X * xDot.X + vec.Y * xDot.Y;
			vec.Y = oldX * yDot.X + vec.Y * yDot.Y;
		}
		public static Vector3 TransdormDotVec(Vector3 vec,Vector2 xDot, Vector2 yDot)
		{
			return new Vector3(
				vec.X * xDot.X + vec.Y * xDot.Y,
				vec.X * yDot.X + vec.Y * yDot.Y,
				0);
		}

		/// <summary>
		/// Returns true, if the two specified rects overlap.
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="w1"></param>
		/// <param name="h1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="w2"></param>
		/// <param name="h2"></param>
		/// <returns></returns>
		public static bool RectsOverlap(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
		{
			if (x1 > (x2 + w2) || (x1 + w1) < x2) return false;
			if (y1 > (y2 + h2) || (y1 + h1) < y2) return false;
			return true;
		}
		public static bool RectsOverlap(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
		{
			if (x1 > (x2 + w2) || (x1 + w1) < x2) return false;
			if (y1 > (y2 + h2) || (y1 + h1) < y2) return false;
			return true;
		}

		/// <summary>
		/// Returns the common rectangular are of both specified rectangular areas.
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="w1"></param>
		/// <param name="h1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="w2"></param>
		/// <param name="h2"></param>
		/// <param name="x3"></param>
		/// <param name="y3"></param>
		/// <param name="w3"></param>
		/// <param name="h3"></param>
		public static void GetCommonRect(
			float x1, float y1, float w1, float h1, 
			float x2, float y2, float w2, float h2, 
			out float x3, out float y3, out float w3, out float h3)
		{
			float tempWidth = Math.Min(w2, w1 - (x2 - x1));
			float tempHeight = Math.Min(h2, h1 - (y2 - y1));
			if ((x1 - x2) > 0.0f) tempWidth -= (x1 - x2);
			if ((y1 - y2) > 0.0f) tempHeight -= (y1 - y2);

			x3 = Math.Max(x1, x2);
			y3 = Math.Max(y1, y2);
			w3 = Math.Min(w1, tempWidth);
			h3 = Math.Min(h1, tempHeight);
			return;
		}
		public static void GetCommonRect(
			int x1, int y1, int w1, int h1, 
			int x2, int y2, int w2, int h2, 
			out int x3, out int y3, out int w3, out int h3)
		{
			int tempWidth = Math.Min(w2, w1 - (x2 - x1));
			int tempHeight = Math.Min(h2, h1 - (y2 - y1));
			if ((x1 - x2) > 0) tempWidth -= (x1 - x2);
			if ((y1 - y2) > 0) tempHeight -= (y1 - y2);

			x3 = Math.Max(x1, x2);
			y3 = Math.Max(y1, y2);
			w3 = Math.Min(w1, tempWidth);
			h3 = Math.Min(h1, tempHeight);
			return;
		}

		/// <summary>
		/// Checks, if two line segments (or optionally an infinite lines) cross and determines their mutual point.
		/// </summary>
		/// <param name="startX1"></param>
		/// <param name="startY1"></param>
		/// <param name="endX1"></param>
		/// <param name="endY1"></param>
		/// <param name="startX2"></param>
		/// <param name="startY2"></param>
		/// <param name="endX2"></param>
		/// <param name="endY2"></param>
		/// <param name="infinite"></param>
		/// <param name="crossX"></param>
		/// <param name="crossY"></param>
		/// <returns>Are the lines crossing?</returns>
		public static bool LinesCross(
			float startX1, float startY1, float endX1, float endY1,
			float startX2, float startY2, float endX2, float endY2,
			out float crossX, out float crossY,
			bool infinite = false)
		{
			float n = (startY1 - startY2) * (endX2 - startX2) - (startX1 - startX2) * (endY2 - startY2);
			float d = (endX1 - startX1) * (endY2 - startY2) - (endY1 - startY1) * (endX2 - startX2);

			crossX = 0.0f;
			crossY = 0.0f;

			if (Math.Abs(d) < 0.0001)
				return false;
			else
			{
				float sn = (startY1 - startY2) * (endX1 - startX1) - (startX1 - startX2) * (endY1 - startY1);
				float ab = n / d;
				if (infinite)
				{
					crossX = startX1 + ab * (endX1 - startX1);
					crossY = startY1 + ab * (endY1 - startY1);
					return true;
				}
				else if (ab > 0.0 && ab < 1.0)
				{
					float cd = sn / d;
					if (cd > 0.0 && cd < 1.0)
					{
						crossX = startX1 + ab * (endX1 - startX1);
						crossY = startY1 + ab * (endY1 - startY1);
						return true;
					}
				}
			}

			return false;
		}
		public static bool LinesCross(
			float startX1, float startY1, float endX1, float endY1,
			float startX2, float startY2, float endX2, float endY2,
			bool infinite = false)
		{
			float crossX;
			float crossY;
			return LinesCross(
				startX1, startY1, endX1, endY1,
				startX2, startY2, endX2, endY2,
				out crossX, out crossY,
				infinite);
		}

		/// <summary>
		/// Calculates the point on a line segment (or optionally an infinite line) that has the smalles possible
		/// distance to a seperate point.
		/// </summary>
		/// <param name="pX"></param>
		/// <param name="pY"></param>
		/// <param name="lX1"></param>
		/// <param name="lY1"></param>
		/// <param name="lX2"></param>
		/// <param name="lY2"></param>
		/// <param name="infinite"></param>
		/// <param name="nX"></param>
		/// <param name="nY"></param>
		public static Vector2 PointLineNearestPoint(
			float pX, float pY,
			float lX1, float lY1, float lX2, float lY2,
			bool infinite = false)
		{
			if (lX1 == lX2 && lY1 == lY2) return new Vector2(lX1, lY1);
			float sX = lX2 - lX1;
			float sY = lY2 - lY1;
			float q = ((pX - lX1) * sX + (pY - lY1) * sY) / (sX * sX + sY * sY);

			if (!infinite)
			{
				if (q < 0.0) q = 0.0f;
				if (q > 1.0) q = 1.0f;
			}

			return new Vector2((1.0f - q) * lX1 + q * lX2, (1.0f - q) * lY1 + q * lY2);
		}

		/// <summary>
		/// Calculates the distance between a point and a line segment (or optionally an infinite line)
		/// </summary>
		/// <param name="pX"></param>
		/// <param name="pY"></param>
		/// <param name="lX1"></param>
		/// <param name="lY1"></param>
		/// <param name="lX2"></param>
		/// <param name="lY2"></param>
		/// <param name="infinite"></param>
		/// <returns></returns>
		public static float PointLineDistance(
			float pX, float pY,
			float lX1, float lY1, float lX2, float lY2,
			bool infinite = false)
		{
			Vector2 n = PointLineNearestPoint(pX, pY, lX1, lY1, lX2, lY2, infinite);
			return Distance(pX, pY, n.X, n.Y);
		}

		/// <summary>
		/// Assuming two objects travelling at a linear course with constant speed and angle, this method
		/// calculates at which point they may collide if the angle of object 1 is not defined by a specific
		/// (but constant!) value.
		/// In other words: If object 1 tries to hit object 2, let object 1 move towards the calculated point.
		/// </summary>
		/// <param name="obj1X"></param>
		/// <param name="obj1Y"></param>
		/// <param name="obj1Speed"></param>
		/// <param name="obj2X"></param>
		/// <param name="obj2Y"></param>
		/// <param name="obj2Speed"></param>
		/// <param name="obj2Angle"></param>
		/// <param name="colX"></param>
		/// <param name="colY"></param>
		/// <returns>
		/// False if it is not possible for object 1 to collide with object 2 at any course of object 1.
		/// This is, for example, the case if object 1 and to move to the same direction but object 2 is faster.
		/// A "collision point" is calculated either way, though it is not a collision point but only a 
		/// "directional idea" if false is returned.
		/// </returns>
		public static bool GetLinearPrediction(
			float obj1X, float obj1Y, float obj1Speed,
			float obj2X, float obj2Y, float obj2Speed, float obj2Angle,
			out float colX, out float colY)
		{
			if (obj2Speed <= float.Epsilon)
			{
				colX = obj2X;
				colY = obj2Y;
				return true;
			}
			else if (obj1Speed <= float.Epsilon)
			{
				colX = obj2X;
				colY = obj2Y;
				return false;
			}

			obj2Angle = NormalizeAngle(obj2Angle);
			float targetDist = Distance(obj2X, obj2Y, obj1X, obj1Y);
			float targetAngle = Angle(obj2X, obj2Y, obj1X, obj1Y);
			float tmpAngle1 = targetAngle - obj2Angle;
			float tmpAngle2;

			if (tmpAngle1 < 0.0d) tmpAngle1 = RadAngle360 - tmpAngle1;
			if (Math.Abs(tmpAngle1) > Pi) tmpAngle1 = RadAngle360 - Math.Abs(tmpAngle1);
			tmpAngle1 = Math.Abs(tmpAngle1);
			tmpAngle2 = Math.Abs(Asin(Sin(tmpAngle1) * obj2Speed / obj1Speed));

			float tmp;
			if (double.IsNaN(tmpAngle2) || tmpAngle1 + tmpAngle2 >= Pi)
			{
				tmp = targetDist * obj2Speed * obj2Speed / obj1Speed;
				colX = obj2X + (Sin(obj2Angle) * tmp);
				colY = obj2Y + (-Cos(obj2Angle) * tmp);
				return false;
			}
			else
			{
				tmp = targetDist * Math.Abs(Sin(tmpAngle2) / Sin(Pi - tmpAngle1 - tmpAngle2));
				colX = obj2X + (Sin(obj2Angle) * tmp);
				colY = obj2Y + (-Cos(obj2Angle) * tmp);
				return true;
			}
		}

		/// <summary>
		/// Assuming two objects travelling at a linear course with constant speed and angle, this method
		/// calculates the time from now at which the distance between the two objects will be minimal. If
		/// this has already passed, the returned time is negative.
		/// </summary>
		/// <param name="obj1X"></param>
		/// <param name="obj1Y"></param>
		/// <param name="obj1XSpeed"></param>
		/// <param name="obj1YSpeed"></param>
		/// <param name="obj2X"></param>
		/// <param name="obj2Y"></param>
		/// <param name="obj2XSpeed"></param>
		/// <param name="obj2YSpeed"></param>
		/// <returns></returns>
		public static float GetLinearPrediction2(
			float obj1X, float obj1Y, float obj1XSpeed, float obj1YSpeed,
			float obj2X, float obj2Y, float obj2XSpeed, float obj2YSpeed)
		{
			float timeTemp;
			timeTemp = -((obj1Y - obj2Y) * (obj1YSpeed - obj2YSpeed)) - ((obj1X - obj2X) * (obj1XSpeed - obj2XSpeed));
			timeTemp /= (((obj1XSpeed - obj2XSpeed) * (obj1XSpeed - obj2XSpeed)) + ((obj1YSpeed - obj2YSpeed) * (obj1YSpeed - obj2YSpeed)));
			return timeTemp;
		}
	}
}
