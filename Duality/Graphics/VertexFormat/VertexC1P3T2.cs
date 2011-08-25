﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using Duality.ColorFormat;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.VertexFormat
{
	/// <summary>
	/// Vertex data providing each vertex a position (3x4 byte), color (1x4 byte) and texture coordinate (2x4 byte)
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct VertexC1P3T2 : IVertexData
	{
		/// <summary>
		/// The vertices color.
		/// </summary>
		public ColorRgba clr;
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
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexDataFormat.VertexC1P3T2; }
		}
		
		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.EnableClientState(ArrayCap.ColorArray);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.ColorPointer(4, ColorPointerType.UnsignedByte, Size, (IntPtr)OffsetColor);
			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, Size, (IntPtr)OffsetTex0);

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.ColorArray);
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);
		}
		
		/// <summary>
		/// Byte offset for the color.
		/// </summary>
		public const int OffsetColor	= 0;
		/// <summary>
		/// Byte offset for the position.
		/// </summary>
		public const int OffsetPos		= OffsetColor + 4 * sizeof(byte);
		/// <summary>
		/// Byte offset for the texture coordinate.
		/// </summary>
		public const int OffsetTex0		= OffsetPos + 3 * sizeof(float);
		/// <summary>
		/// Total size in bytes.
		/// </summary>
		public const int Size			= OffsetTex0 + 2 * sizeof(float);

		public VertexC1P3T2(float x, float y, float z, float u, float v, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = u;
			this.texCoord.Y = v;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC1P3T2(float x, float y, float z, float u, float v, ColorRgba clr)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = u;
			this.texCoord.Y = v;
			this.clr = clr;
		}
		public VertexC1P3T2(Vector3 pos, Vector2 uv, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos = pos;
			this.texCoord = uv;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC1P3T2(Vector3 pos, Vector2 uv, ColorRgba clr)
		{
			this.pos = pos;
			this.texCoord = uv;
			this.clr = clr;
		}
	}
}