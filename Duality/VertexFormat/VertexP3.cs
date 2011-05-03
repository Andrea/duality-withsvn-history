using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.VertexFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct VertexP3 : IVertexData
	{
		public Vector3 pos;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexFormat.VertexP3; }
		}

		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			GL.EnableClientState(ArrayCap.VertexArray);

			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.VertexArray);
		}

		public const int OffsetPos		= 0;
		public const int Size			= OffsetPos + 3 * sizeof(float);

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
