using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality
{
	/// <summary>
	/// Describes a rectangular area.
	/// </summary>
	[Serializable]
	public struct Rect : IEquatable<Rect>
	{
		/// <summary>
		/// An empty Rect.
		/// </summary>
		public static readonly Rect Empty = new Rect(0, 0, 0, 0);

		/// <summary>
		/// The Rects x-Coordinate.
		/// </summary>
		public	float	x;
		/// <summary>
		/// The Rects y-Coordinate.
		/// </summary>
		public	float	y;
		/// <summary>
		/// The Rects width.
		/// </summary>
		public	float	w;
		/// <summary>
		/// The Rects height.
		/// </summary>
		public	float	h;

		/// <summary>
		/// [GET / SET] The Rects position
		/// </summary>
		public Vector2 Pos
		{
			get { return new Vector2(this.x, this.y); }
			set { this.x = value.X; this.y = value.Y; }
		}
		/// <summary>
		/// [GET / SET] The Rects size.
		/// </summary>
		public Vector2 Size
		{
			get { return new Vector2(w, h); }
			set { this.w = value.X; this.h = value.Y; }
		}

		/// <summary>
		/// [GET] The minimum x-Coordinate occupied by the Rect. Accounts for negative sizes.
		/// </summary>
		public float MinX
		{
			get { return MathF.Min(x, x + w); }
		}
		/// <summary>
		/// [GET] The minimum y-Coordinate occupied by the Rect. Accounts for negative sizes.
		/// </summary>
		public float MinY
		{
			get { return MathF.Min(y, y + h); }
		}
		/// <summary>
		/// [GET] The maximum x-Coordinate occupied by the Rect. Accounts for negative sizes.
		/// </summary>
		public float MaxY
		{
			get { return MathF.Max(y, y + h); }
		}
		/// <summary>
		/// [GET] The maximum y-Coordinate occupied by the Rect. Accounts for negative sizes.
		/// </summary>
		public float MaxX
		{
			get { return MathF.Max(x, x + w); }
		}
		/// <summary>
		/// [GET] The center x-Coordinate occupied by the Rect.
		/// </summary>
		public float CenterX
		{
			get { return x + w * 0.5f; }
		}
		/// <summary>
		/// [GET] The center y-Coordinate occupied by the Rect.
		/// </summary>
		public float CenterY
		{
			get { return y + h * 0.5f; }
		}

		/// <summary>
		/// [GET] The Rects top left coordinates
		/// </summary>
		public Vector2 TopLeft
		{
			get { return new Vector2(this.MinX, this.MinY); }
		}
		/// <summary>
		/// [GET] The Rects top right coordinates
		/// </summary>
		public Vector2 TopRight
		{
			get { return new Vector2(this.MaxX, this.MinY); }
		}
		/// <summary>
		/// [GET] The Rects top coordinates
		/// </summary>
		public Vector2 Top
		{
			get { return new Vector2(this.CenterX, this.MinY); }
		}
		/// <summary>
		/// [GET] The Rects bottom left coordinates
		/// </summary>
		public Vector2 BottomLeft
		{
			get { return new Vector2(this.MinX, this.MaxY); }
		}
		/// <summary>
		/// [GET] The Rects bottom right coordinates
		/// </summary>
		public Vector2 BottomRight
		{
			get { return new Vector2(this.MaxX, this.MaxY); }
		}
		/// <summary>
		/// [GET] The Rects bottom coordinates
		/// </summary>
		public Vector2 Bottom
		{
			get { return new Vector2(this.CenterX, this.MaxY); }
		}
		/// <summary>
		/// [GET] The Rects left coordinates
		/// </summary>
		public Vector2 Left
		{
			get { return new Vector2(this.MinX, this.CenterY); }
		}
		/// <summary>
		/// [GET] The Rects right coordinates
		/// </summary>
		public Vector2 Right
		{
			get { return new Vector2(this.MaxX, this.CenterY); }
		}
		/// <summary>
		/// [GET] The Rects center coordinates
		/// </summary>
		public Vector2 Center
		{
			get { return new Vector2(this.CenterX, this.CenterY); }
		}

		/// <summary>
		/// [GET] The area that is occupied by the Rect.
		/// </summary>
		public float Area
		{
			get { return MathF.Abs(w * h); }
		}
		/// <summary>
		/// [GET] The Rects perimeter i.e. sum of all edge lengths.
		/// </summary>
		public float Perimeter
		{
			get { return 2 * MathF.Abs(w) + 2 * MathF.Abs(h); }
		}
		/// <summary>
		/// [GET] If this Rect was to fit inside a bounding circle originating from [0,0],
		/// this would be its radius.
		/// </summary>
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
		
		/// <summary>
		/// Creates a Rect of the given size.
		/// </summary>
		/// <param name="size"></param>
		public Rect(Vector2 size)
		{
			this.x = 0;
			this.y = 0;
			this.w = size.X;
			this.h = size.Y;
		}
		/// <summary>
		/// Creates a Rect of the given size.
		/// </summary>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public Rect(float w, float h)
		{
			this.x = 0;
			this.y = 0;
			this.w = w;
			this.h = h;
		}
		/// <summary>
		/// Creates a Rect of the given size and position.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public Rect(float x, float y, float w, float h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}

		/// <summary>
		/// Returns a new version of this Rect that has been moved by the specified offset.
		/// </summary>
		/// <param name="x">Movement in x-Direction.</param>
		/// <param name="y">Movement in y-Direction.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Offset(float x, float y)
		{
			Rect newRect = this;
			newRect.x += x;
			newRect.y += y;
			return newRect;
		}
		/// <summary>
		/// Returns a new version of this Rect that has been moved by the specified offset.
		/// </summary>
		/// <param name="offset">Movement vector.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Offset(Vector2 offset)
		{
			Rect newRect = this;
			newRect.x += offset.X;
			newRect.y += offset.Y;
			return newRect;
		}

		/// <summary>
		/// Returns a new version of this Rect that has been scaled by the specified factor.
		/// Scaling only affects a Rects size, not its position.
		/// </summary>
		/// <param name="x">x-Scale factor.</param>
		/// <param name="y">y-Scale factor.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Scale(float x, float y)
		{
			Rect newRect = this;
			newRect.w *= x;
			newRect.h *= y;
			return newRect;
		}
		/// <summary>
		/// Returns a new version of this Rect that has been scaled by the specified factor.
		/// Scaling only affects a Rects size, not its position.
		/// </summary>
		/// <param name="factor">Scale factor.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Scale(Vector2 factor)
		{
			Rect newRect = this;
			newRect.w *= factor.X;
			newRect.h *= factor.Y;
			return newRect;
		}
		/// <summary>
		/// Returns a new version of this Rect that has been transformed by the specified scale factor.
		/// Transforming both affects a Rects size and position.
		/// </summary>
		/// <param name="x">x-Scale factor.</param>
		/// <param name="y">y-Scale factor.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Transform(float x, float y)
		{
			Rect newRect = this;
			newRect.x *= x;
			newRect.y *= y;
			newRect.w *= x;
			newRect.h *= y;
			return newRect;
		}
		/// <summary>
		/// Returns a new version of this Rect that has been transformed by the specified scale factor.
		/// Transforming both affects a Rects size and position.
		/// </summary>
		/// <param name="scale">Scale factor.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Transform(Vector2 scale)
		{
			Rect newRect = this;
			newRect.x *= scale.X;
			newRect.y *= scale.Y;
			newRect.w *= scale.X;
			newRect.h *= scale.Y;
			return newRect;
		}

		/// <summary>
		/// Returns a new version of this Rect that has been expanded to contain
		/// the specified rectangular area.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect to contain.</param>
		/// <param name="y">y-Coordinate of the Rect to contain.</param>
		/// <param name="w">Width of the Rect to contain.</param>
		/// <param name="h">Height of the Rect to contain.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect ExpandToContain(float x, float y, float w, float h)
		{
			return this.ExpandToContain(x, y).ExpandToContain(x + w, y + h);
		}
		/// <summary>
		/// Returns a new version of this Rect that has been expanded to contain
		/// the specified Rect.
		/// </summary>
		/// <param name="other">The Rect to contain.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect ExpandToContain(Rect other)
		{
			return this.ExpandToContain(other.x, other.y).ExpandToContain(other.x + other.w, other.y + other.h);
		}
		/// <summary>
		/// Returns a new version of this Rect that has been expanded to contain
		/// the specified point.
		/// </summary>
		/// <param name="x">x-Coordinate of the point to contain.</param>
		/// <param name="y">y-Coordinate of the point to contain.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
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
		/// <summary>
		/// Returns a new version of this Rect that has been expanded to contain
		/// the specified point.
		/// </summary>
		/// <param name="p">The point to contain.</param>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect ExpandToContain(Vector2 p)
		{
			return this.ExpandToContain(p.X, p.Y);
		}

		/// <summary>
		/// Returns a new version of this Rect with integer coordinates and size.
		/// They have been <see cref="MathF.Round">rounded</see>.
		/// </summary>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Round()
		{
			return new Rect(MathF.Round(x), MathF.Round(y), MathF.Round(w), MathF.Round(h));
		}
		/// <summary>
		/// Returns a new version of this Rect with integer coordinates and size.
		/// They have been <see cref="MathF.Ceiling">ceiled</see>.
		/// </summary>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Ceiling()
		{
			return new Rect(MathF.Ceiling(x), MathF.Ceiling(y), MathF.Ceiling(w), MathF.Ceiling(h));
		}
		/// <summary>
		/// Returns a new version of this Rect with integer coordinates and size.
		/// They have been <see cref="MathF.Floor">floored</see>.
		/// </summary>
		/// <returns>A new Rect with the specified adjustments.</returns>
		public Rect Floor()
		{
			return new Rect(MathF.Floor(x), MathF.Floor(y), MathF.Floor(w), MathF.Floor(h));
		}

		/// <summary>
		/// Returns whether this Rect contains a given point.
		/// </summary>
		/// <param name="x">x-Coordinate of the point to test.</param>
		/// <param name="y">y-Coordinate of the point to test.</param>
		/// <returns>True, if the Rect contains the point, false if not.</returns>
		public bool Contains(float x, float y)
		{
			return x >= this.MinX && x <= this.MaxX && y >= this.MinY && y <= this.MaxY;
		}
		/// <summary>
		/// Returns whether this Rect contains a given point.
		/// </summary>
		/// <param name="pos">The point to test.</param>
		/// <returns>True, if the Rect contains the point, false if not.</returns>
		public bool Contains(Vector2 pos)
		{
			return pos.X >= this.MinX && pos.X <= this.MaxX && pos.Y >= this.MinY && pos.Y <= this.MaxY;
		}
		/// <summary>
		/// Returns whether this Rect contains a given rectangular area.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect to test.</param>
		/// <param name="y">y-Coordinate of the Rect to test.</param>
		/// <param name="w">Width of the Rect to test.</param>
		/// <param name="h">Height of the Rect to test.</param>
		/// <returns>True, if the Rect contains the other Rect, false if not.</returns>
		public bool Contains(float x, float y, float w, float h)
		{
			return this.Contains(x, y) && this.Contains(x + w, y + h);
		}
		/// <summary>
		/// Returns whether this Rect contains a given rectangular area.
		/// </summary>
		/// <param name="rect">The Rect to test.</param>
		/// <returns>True, if the Rect contains the other Rect, false if not.</returns>
		public bool Contains(Rect rect)
		{
			return this.Contains(rect.x, rect.y) && this.Contains(rect.x + rect.w, rect.y + rect.h);
		}
		
		/// <summary>
		/// Returns whether this Rect intersects a given rectangular area.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect to test.</param>
		/// <param name="y">y-Coordinate of the Rect to test.</param>
		/// <param name="w">Width of the Rect to test.</param>
		/// <param name="h">Height of the Rect to test.</param>
		/// <returns>True, if the Rect intersects the other Rect, false if not.</returns>
		public bool Intersects(float x, float y, float w, float h)
		{
			if (this.x > (x + w) || (this.x + this.w) < x) return false;
			if (this.y > (y + h) || (this.y + this.h) < y) return false;
			return true;
		}
		/// <summary>
		/// Returns whether this Rect intersects a given rectangular area.
		/// </summary>
		/// <param name="rect">The Rect to test.</param>
		/// <returns>True, if the Rect intersects the other Rect, false if not.</returns>
		public bool Intersects(Rect rect)
		{
			if (this.x > (rect.x + rect.w) || (this.x + this.w) < rect.x) return false;
			if (this.y > (rect.y + rect.h) || (this.y + this.h) < rect.y) return false;
			return true;
		}
		/// <summary>
		/// Returns a Rect that equals this Rects intersection with another Rect.
		/// </summary>
		/// <param name="x">x-Coordinate of the Rect to intersect with.</param>
		/// <param name="y">y-Coordinate of the Rect to intersect with.</param>
		/// <param name="w">Width of the Rect to intersect with.</param>
		/// <param name="h">Height of the Rect to intersect with.</param>
		/// <returns>A new Rect that describes both Rects intersection area.</returns>
		public Rect Intersection(float x, float y, float w, float h)
		{
			return this.Intersection(new Rect(x, y, w, h));
		}
		/// <summary>
		/// Returns a Rect that equals this Rects intersection with another Rect.
		/// </summary>
		/// <param name="rect">The other Rect to intersect with.</param>
		/// <returns>A new Rect that describes both Rects intersection area.</returns>
		public Rect Intersection(Rect rect)
		{
			float tempWidth = Math.Min(rect.w, this.w - (rect.x - this.x));
			float tempHeight = Math.Min(rect.h, this.h - (rect.y - this.y));
			if ((this.x - rect.x) > 0.0f) tempWidth -= (this.x - rect.x);
			if ((this.y - rect.y) > 0.0f) tempHeight -= (this.y - rect.y);

			return new Rect(
				Math.Max(this.x, rect.x),
				Math.Max(this.y, rect.y),
				Math.Min(this.w, tempWidth),
				Math.Min(this.h, tempHeight));
		}

		/// <summary>
		/// Tests if two Rects are equal.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned as specified.
		/// </summary>
		/// <param name="align">The alignment of the Rects x and y Coordinates.</param>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
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
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned centered.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignCenter(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y - h * 0.5f, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned at the top.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignTop(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned at the bottom.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignBottom(float x, float y, float w, float h)
		{
			return new Rect(x - w * 0.5f, y - h, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned left
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y - h * 0.5f, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned right
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y - h * 0.5f, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned top left.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignTopLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned top right.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignTopRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned bottom left.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignBottomLeft(float x, float y, float w, float h)
		{
			return new Rect(x, y - h, w, h);
		}
		/// <summary>
		/// Creates a Rect using x and y Coordinates that are assumed to be aligned bottom right.
		/// </summary>
		/// <param name="x">The Rects x-Coordinate.</param>
		/// <param name="y">The Rects y-Coordinate.</param>
		/// <param name="w">The Rects width.</param>
		/// <param name="h">The Rects height.</param>
		/// <returns></returns>
		public static Rect AlignBottomRight(float x, float y, float w, float h)
		{
			return new Rect(x - w, y - h, w, h);
		}

		/// <summary>
		/// Returns whether two Rects are equal.
		/// </summary>
		/// <param name="left">The first Rect.</param>
		/// <param name="right">The second Rect.</param>
		/// <returns></returns>
		public static bool operator ==(Rect left, Rect right)
        {
            return left.Equals(right);
        }
		/// <summary>
		/// Returns whether two Rects are unequal.
		/// </summary>
		/// <param name="left">The first Rect.</param>
		/// <param name="right">The second Rect.</param>
		/// <returns></returns>
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
