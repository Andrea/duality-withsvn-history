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
	/// Vertex data providing each vertex a position (3x4 byte).
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct VertexP3 : IVertexData
	{
		/// <summary>
		/// The vertices position.
		/// </summary>
		public Vector3 pos;

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
			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
		}
		void IVertexData.UploadToVBO<T>(T[] vertexData)
		{
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.VertexArray);
		}
		
		/// <summary>
		/// Byte offset for the position.
		/// </summary>
		public const int OffsetPos			= 0;
		/// <summary>
		/// Total size in bytes.
		/// </summary>
		public const int Size				= OffsetPos + 3 * sizeof(float);
		public const int VertexTypeIndex	= Duality.Resources.DrawTechnique.VertexType_P3;

		public VertexP3(float x, float y, float z)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
		}
		public VertexP3(Vector3 p)
		{
			this.pos = p;
		}
	}
}
