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
	public struct VertexP3T2 : IVertexData
	{
		public Vector3 pos;
		public Vector2 texCoord;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexDataFormat.VertexP3T2; }
		}

		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, Size, (IntPtr)OffsetTex0);

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);
		}

		public const int OffsetPos		= 0;
		public const int OffsetTex0		= OffsetPos + 3 * sizeof(float);
		public const int Size			= OffsetTex0 + 2 * sizeof(float);

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
