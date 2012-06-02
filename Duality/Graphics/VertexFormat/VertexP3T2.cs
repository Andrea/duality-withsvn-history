using System;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.VertexFormat
{
	/// <summary>
	/// Vertex data providing each vertex a position (3x4 byte) and texture coordinate (2x4 byte)
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct VertexP3T2 : IVertexData
	{
		/// <summary>
		/// The vertices position.
		/// </summary>
		public Vector3 Pos;
		/// <summary>
		/// The vertices texture coordinate.
		/// </summary>
		public Vector2 TexCoord;

		Vector3 IVertexData.Pos
		{
			get { return this.Pos; }
			set { this.Pos = value; }
		}
		int IVertexData.TypeIndex
		{
			get { return VertexTypeIndex; }
		}
		
		void IVertexData.SetupVBO(Resources.BatchInfo mat)
		{
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, Size, (IntPtr)OffsetTex0);
		}
		void IVertexData.UploadToVBO<T>(T[] vertexData)
		{
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);
		}
		
		/// <summary>
		/// Byte offset for the position.
		/// </summary>
		public const int OffsetPos			= 0;
		/// <summary>
		/// Byte offset for the texture coordinate.
		/// </summary>
		public const int OffsetTex0			= OffsetPos + 3 * sizeof(float);
		/// <summary>
		/// Total size in bytes.
		/// </summary>
		public const int Size				= OffsetTex0 + 2 * sizeof(float);
		public const int VertexTypeIndex	= Resources.DrawTechnique.VertexType_P3T2;

		public VertexP3T2(float x, float y, float z, float u, float v)
		{
			this.Pos.X = x;
			this.Pos.Y = y;
			this.Pos.Z = z;
			this.TexCoord.X = u;
			this.TexCoord.Y = v;
		}
		public VertexP3T2(Vector3 p, Vector2 t)
		{
			this.Pos = p;
			this.TexCoord = t;
		}
	}
}
