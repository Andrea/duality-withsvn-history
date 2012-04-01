namespace Duality
{
	/// <summary>
	/// Represents a 2D spatial alignment.
	/// </summary>
	public enum Alignment
	{
		/// <summary>
		/// Align to its center.
		/// </summary>
		Center			= 0x0,

		/// <summary>
		/// Align to its left.
		/// </summary>
		Left			= 0x1,
		/// <summary>
		/// Align to its right.
		/// </summary>
		Right			= 0x2,
		/// <summary>
		/// Align to its top.
		/// </summary>
		Top				= 0x4,
		/// <summary>
		/// Align to its bottom.
		/// </summary>
		Bottom			= 0x8,

		/// <summary>
		/// Align to its top left.
		/// </summary>
		TopLeft			= Top | Left,
		/// <summary>
		/// Align to its top right.
		/// </summary>
		TopRight		= Top | Right,
		/// <summary>
		/// Align to its bottom left.
		/// </summary>
		BottomLeft		= Bottom | Left,
		/// <summary>
		/// Align to its bottom right.
		/// </summary>
		BottomRight		= Bottom | Right
	}
}
