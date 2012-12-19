using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality.EditorHints;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Specifies the way in which incoming vertex data is interpreted in order to generate geometry.
	/// </summary>
	public enum VertexMode
	{
		Points = BeginMode.Points,
		Lines = BeginMode.Lines,
		LineStrip = BeginMode.LineStrip,
		LineLoop = BeginMode.LineLoop,
		Triangles = BeginMode.Triangles,
		TriangleStrip = BeginMode.TriangleStrip,
		TriangleFan = BeginMode.TriangleFan,
		Quads = BeginMode.Quads,
		QuadStrip = BeginMode.QuadStrip,
		Polygon = BeginMode.Polygon
	}

	/// <summary>
	/// Specifies the perspective effect that is applied when rendering the world.
	/// </summary>
	public enum PerspectiveMode
	{
		/// <summary>
		/// No perspective effect is applied. Z points into the screen and is only used for object sorting.
		/// </summary>
		Flat,
		/// <summary>
		/// Objects that are far away appear smaller. Z points into the screen and is used for scaling and sorting.
		/// </summary>
		Parallax,
		/// <summary>
		/// Like <see cref="Flat"/>, but Z points "downwards" while objects are sorted from "north" to "south" based on
		/// their Y value.
		/// </summary>
		//Isometric
	}

	/// <summary>
	/// Specifies a rendering matrix setup.
	/// </summary>
	public enum RenderMatrix
	{
		PerspectiveWorld,
		OrthoScreen
	}

	/// <summary>
	/// Enumerates different behviours on how to blend color data onto existing background color.
	/// </summary>
	/// <seealso cref="Duality.Resources.DrawTechnique"/>
	public enum BlendMode
	{
		/// <summary>
		/// When passing this to a method, this value can be used to indicate "Restore to default settings".
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		Reset = -1,

		/// <summary>
		/// Incoming color overwrites background color completely. Doesn't need Z-Sorting.
		/// </summary>
		Solid,
		/// <summary>
		/// Incoming color overwrites background color but leaves out areas with low alpha. Doesn't need Z-Sorting.
		/// </summary>
		Mask,

		/// <summary>
		/// Incoming color is multiplied by its alpha value and then added to background color. Needs Z-Sorting.
		/// </summary>
		Add,
		/// <summary>
		/// Incoming color overwrites background color weighted by its alpha value. Needs Z-Sorting.
		/// </summary>
		Alpha,
		/// <summary>
		/// Incoming color scales background color. Needs Z-Sorting.
		/// </summary>
		Multiply,
		/// <summary>
		/// Incoming color is multiplied and then added to background color. Needs Z-Sorting.
		/// </summary>
		Light,
		/// <summary>
		/// Incoming color inverts background color. Needs Z-Sorting.
		/// </summary>
		Invert,

		/// <summary>
		/// The total number of available BlendModes.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		Count
	}

	[Flags]
	public enum VisibilityFlag : uint
	{
		None = 0U,

		// User-defined groups
		Group0 = 1U << 0,
		Group1 = 1U << 1,
		Group2 = 1U << 2,
		Group3 = 1U << 3,
		Group4 = 1U << 4,
		Group5 = 1U << 5,
		Group6 = 1U << 6,
		Group7 = 1U << 7,
		Group8 = 1U << 8,
		Group9 = 1U << 9,
		Group10 = 1U << 10,
		Group11 = 1U << 11,
		Group12 = 1U << 12,
		Group13 = 1U << 13,
		Group14 = 1U << 14,
		Group15 = 1U << 15,
		Group16 = 1U << 16,
		Group17 = 1U << 17,
		Group18 = 1U << 18,
		Group19 = 1U << 19,
		Group20 = 1U << 20,
		Group21 = 1U << 21,
		Group22 = 1U << 22,
		Group23 = 1U << 23,
		Group24 = 1U << 24,
		Group25 = 1U << 25,
		Group26 = 1U << 26,
		Group27 = 1U << 27,
		Group28 = 1U << 28,
		Group29 = 1U << 29,
		Group30 = 1U << 30,

		// Special groups (Might cause special behaviour)
		/// <summary>
		/// Special flag. This flag is set when rendering screen overlays.
		/// </summary>
		ScreenOverlay = 1U << 31,

		// Compound groups
		All = uint.MaxValue,
		AllFlags = ScreenOverlay,
		AllGroups = All & (~AllFlags)
	}

	/// <summary>
	/// Defines a general interface for drawing devices. Its main duty is to accept and collect parameterized vertex data.
	/// </summary>
	public interface IDrawDevice
	{
		/// <summary>
		/// [GET] Reference coordinate for rendering i.e. the position of the drawing device's Camera.
		/// </summary>
		Vector3 RefCoord { get; }
		/// <summary>
		/// [GET] Reference angle for rendering i.e. the angle of the drawing device's Camera.
		/// </summary>
		float RefAngle { get; }
		/// <summary>
		/// [GET] Reference distance for calculating the perspective effect. An object this far away from
		/// the Camera will appear in its original size.
		/// </summary>
		float FocusDist { get; }
		/// <summary>
		/// [GET] A bitmask flagging all visibility groups that are considered visible to this drawing device.
		/// </summary>
		VisibilityFlag VisibilityMask { get; }
		/// <summary>
		/// [GET] The lowest Z value that can be displayed by the device.
		/// </summary>
		float NearZ { get; }
		/// <summary>
		/// [GET] The highest Z value that can be displayed by the device.
		/// </summary>
		float FarZ { get; }
		/// <summary>
		/// [GET] Returns whether the drawing device allows writing to the depth buffer
		/// </summary>
		bool DepthWrite { get; }
		/// <summary>
		/// [GET] The size of the surface this drawing device operates on.
		/// </summary>
		Vector2 TargetSize { get; }
		

		/// <summary>
		/// Processes the specified world space position and scale values and transforms them to the IDrawDevices view space.
		/// This usually also applies a perspective effect, if applicable.
		/// </summary>
		/// <param name="pos">The position to process.</param>
		/// <param name="scale">The scale factor to process.</param>
		void PreprocessCoords(ref Vector3 pos, ref float scale);
		/// <summary>
		/// Returns whether the specified world-space position is visible in the drawing devices view space.
		/// </summary>
		/// <param name="c">The position to test.</param>
		/// <param name="boundRad">The visual bounding radius to assume for the specified position.</param>
		/// <returns>True, if the position or a portion of its bounding circle is visible, false if not.</returns>
		bool IsCoordInView(Vector3 c, float boundRad = 1.0f);

		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.Material"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(ContentRef<Material> material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData;
		/// <summary>
		/// Adds a parameterized set of vertices to the drawing devices rendering schedule.
		/// </summary>
		/// <typeparam name="T">The type of vertex data to add.</typeparam>
		/// <param name="material">The <see cref="Duality.Resources.BatchInfo"/> to use for rendering the vertices.</param>
		/// <param name="vertexMode">The vertices drawing mode.</param>
		/// <param name="vertices">The vertex data to add.</param>
		void AddVertices<T>(BatchInfo material, VertexMode vertexMode, params T[] vertices) where T : struct, IVertexData;
	}
}
