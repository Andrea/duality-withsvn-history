using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality
{
	[Serializable]
	public struct Rect : IEquatable<Rect>
	{
		public static readonly Rect Empty = new Rect(0, 0, 0, 0);

		public	float	x;
		public	float	y;
		public	float	w;
		public	float	h;

		public Vector2 Pos
		{
			get { return new Vector2(this.MinX, this.MinY); }
		}
		public Vector2 Size
		{
			get { return new Vector2(MathF.Abs(w), MathF.Abs(h)); }
		}

		public float MinX
		{
			get { return MathF.Min(x, x + w); }
		}
		public float MinY
		{
			get { return MathF.Min(y, y + h); }
		}
		public float MaxY
		{
			get { return MathF.Max(y, y + h); }
		}
		public float MaxX
		{
			get { return MathF.Max(x, x + w); }
		}
		public float CenterX
		{
			get { return x + w * 0.5f; }
		}
		public float CenterY
		{
			get { return y + h * 0.5f; }
		}

		public Vector2 TopLeft
		{
			get { return new Vector2(this.MinX, this.MinY); }
		}
		public Vector2 TopRight
		{
			get { return new Vector2(this.MaxX, this.MinY); }
		}
		public Vector2 TopCenter
		{
			get { return new Vector2(this.CenterX, this.MinY); }
		}
		public Vector2 BottomLeft
		{
			get { return new Vector2(this.MinX, this.MaxY); }
		}
		public Vector2 BottomRight
		{
			get { return new Vector2(this.MaxX, this.MaxY); }
		}
		public Vector2 BottomCenter
		{
			get { return new Vector2(this.CenterX, this.MaxY); }
		}
		public Vector2 LeftCenter
		{
			get { return new Vector2(this.MinX, this.CenterY); }
		}
		public Vector2 RightCenter
		{
			get { return new Vector2(this.MaxX, this.CenterY); }
		}
		public Vector2 Center
		{
			get { return new Vector2(this.CenterX, this.CenterY); }
		}

		public float Area
		{
			get { return w * h; }
		}
		public float Perimeter
		{
			get { return 2 * w + 2 * h; }
		}
		public float BoundingRadius
		{
			get 
			{ 
				return MathF.Max(
					MathF.Distance(this.MaxX, this.MaxY),
					MathF.Distance(this.MinX, this.MinY),
					MathF.Distance(this.MaxX, this.MinY),
					MathF.Distance(this.MinX, this.MaxY)); 
			}
		}
		
		public Rect(Vector2 size)
		{
			this.x = 0;
			this.y = 0;
			this.w = size.X;
			this.h = size.Y;
		}
		public Rect(float w, float h)
		{
			this.x = 0;
			this.y = 0;
			this.w = w;
			this.h = h;
		}
		public Rect(float x, float y, float w, float h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}

		public Rect Offset(float x, float y)
		{
			Rect newRect = this;
			newRect.x += x;
			newRect.y += y;
			return newRect;
		}
		public Rect Offset(Vector2 offset)
		{
			Rect newRect = this;
			newRect.x += offset.X;
			newRect.y += offset.Y;
			return newRect;
		}

		public Rect Scale(float x, float y)
		{
			Rect newRect = this;
			newRect.w *= x;
			newRect.h *= y;
			return newRect;
		}
		public Rect Scale(Vector2 offset)
		{
			Rect newRect = this;
			newRect.w *= offset.X;
			newRect.h *= offset.Y;
			return newRect;
		}
		public Rect Transform(float x, float y)
		{
			Rect newRect = this;
			newRect.x *= x;
			newRect.y *= y;
			newRect.w *= x;
			newRect.h *= y;
			return newRect;
		}
		public Rect Transform(Vector2 offset)
		{
			Rect newRect = this;
			newRect.x *= offset.X;
			newRect.y *= offset.Y;
			newRect.w *= offset.X;
			newRect.h *= offset.Y;
			return newRect;
		}

		public Rect ExpandToContain(float x, float y, float w, float h)
		{
			return this.ExpandToContain(x, y).ExpandToContain(x + w, y + h);
		}
		public Rect ExpandToContain(float x, float y)
		{
			Rect newRect = this;
			if (x < newRect.x)
			{
				newRect.w += newRect.x - x;
				newRect.x = x;
			}
			if (y < newRect.y)
			{
				newRect.h += newRect.y - y;
				newRect.y = y;
			}
			if (x > newRect.x + newRect.w) newRect.w = x - newRect.x;
			if (y > newRect.y + newRect.h) newRect.h = y - newRect.y;
			return newRect;
		}

		public Rect Round()
		{
			return new Rect(MathF.Round(x), MathF.Round(y), MathF.Round(w), MathF.Round(h));
		}
		public Rect Ceiling()
		{
			return new Rect(MathF.Ceiling(x), MathF.Ceiling(y), MathF.Ceiling(w), MathF.Ceiling(h));
		}
		public Rect Floor()
		{
			return new Rect(MathF.Floor(x), MathF.Floor(y), MathF.Floor(w), MathF.Floor(h));
		}

		public bool Contains(float x, float y)
		{
			return x >= this.MinX && x <= this.MaxX && y >= this.MinY && y <= this.MaxY;
		}
		public bool Contains(Vector2 pos)
		{
			return pos.X >= this.MinX && pos.X <= this.MaxX && pos.Y >= this.MinY && pos.Y <= this.MaxY;
		}
		public bool Contains(float x, float y, float w, float h)
		{
			return this.Contains(x, y) && this.Contains(x + w, y + h);
		}
		public bool Contains(Rect rect)
		{
			return this.Contains(rect.x, rect.y) && this.Contains(rect.x + rect.w, rect.y + rect.h);
		}

		public bool Equals(Rect other)
		{
			return 
				this.x == other.x &&
				this.y == other.y &&
				this.w == other.w &&
				this.h == other.h;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Rect))
				return false;
			else
				return this.Equals((Rect)obj);
		}
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.w.GetHashCode() ^ this.h.GetHashCode();
		}
		public override string ToString()
		{
			return string.Format("Rect ({0}, {1}, {2}, {3})", this.x, this.y, this.w, this.h);
		}

		public static Rect Align(Alignment align, float x, float y, float w, float h)
		{
			if (align == Alignment.Bottom)
				return AlignBottom(x, y, w, h);
			else if (align == Alignment.BottomLeft)
				return AlignBottomLeft(x, y, w, h);
			else if (align == Alignment.BottomRight)
				return AlignBottomRight(x, y, w, h);
			else if (align == Alignment.Center)
				return AlignCenter(x, y, w, h);
			else if (align == Alignment.Left)
				return AlignLeft(x, y, w, h);
			else if (align == Alignment.Right)
				return AlignRight(x, y, w, h);
			else if (align == Alignment.Top)
				return AlignTop(x, y, w, h);
			else if (align == Alignment.TopLeft)
				return AlignTopLeft(x, y, w, h);
			else if (align == Alignment.TopRight)
				return AlignTopRight(x, y, w, h);
			else
				return new Rect(x, y, w, h);
		}
		public static Rect AlignCenter(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y - h * 0.5f, w, h);
		}
		public static Rect AlignTop(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y, w, h);
		}
		public static Rect AlignBottom(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y - h, w, h);
		}
		public static Rect AlignLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y - h * 0.5f, w, h);
		}
		public static Rect AlignRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y - h * 0.5f, w, h);
		}
		public static Rect AlignTopLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y, w, h);
		}
		public static Rect AlignTopRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y, w, h);
		}
		public static Rect AlignBottomLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y - h, w, h);
		}
		public static Rect AlignBottomRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y - h, w, h);
		}

		public static bool operator ==(Rect left, Rect right)
        {
            return left.Equals(right);
        }
		public static bool operator !=(Rect left, Rect right)
        {
            return !left.Equals(right);
        }

		public static implicit operator System.Drawing.Rectangle(Rect r)
		{
			return new System.Drawing.Rectangle((int)r.x, (int)r.y, (int)r.w, (int)r.h);
		}
		public static implicit operator Rect(System.Drawing.Rectangle r)
		{
			return new Rect(r.X, r.Y, r.Width, r.Height);
		}
	}
}
