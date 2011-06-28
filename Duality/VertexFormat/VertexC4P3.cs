using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using Duality.ColorFormat;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.VertexFormat
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct VertexC4P3 : IVertexData
	{
		public ColorRGBA clr;
		public Vector3 pos;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexDataFormat.VertexC4P3; }
		}

		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.EnableClientState(ArrayCap.ColorArray);
			GL.EnableClientState(ArrayCap.VertexArray);

			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.ColorPointer(4, ColorPointerType.UnsignedByte, Size, (IntPtr)OffsetColor);
			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.ColorArray);
			GL.DisableClientState(ArrayCap.VertexArray);
		}

		public const int OffsetColor	= 0;
		public const int OffsetPos		= OffsetColor + 4 * sizeof(byte);
		public const int Size			= OffsetPos + 3 * sizeof(float);

		public VertexC4P3(float x, float y, float z, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC4P3(float x, float y, float z, ColorRGBA clr)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.clr = clr;
		}
		public VertexC4P3(Vector3 pos, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos = pos;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC4P3(Vector3 pos, ColorRGBA clr)
		{
			this.pos = pos;
			this.clr = clr;
		}
	}
}
