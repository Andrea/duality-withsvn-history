using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public Vector3 pos;
		/// <summary>
		/// The vertices texture coordinate.
		/// </summary>
		public Vector2 texCoord;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
			set { this.pos = value; }
		}
		int IVertexData.TypeIndex
		{
			get { return VertexTypeIndex; }
		}
		
		void IVertexData.SetupVBO(Duality.Resources.BatchInfo mat)
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
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
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
		public const int VertexTypeIndex	= Duality.Resources.DrawTechnique.VertexType_P3T2;

		public VertexP3T2(float x, float y, float z, float u, float v)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = u;
			this.texCoord.Y = v;
		}
		public VertexP3T2(Vector3 p, Vector2 t)
		{
			this.pos = p;
			this.texCoord = t;
		}
	}
}
