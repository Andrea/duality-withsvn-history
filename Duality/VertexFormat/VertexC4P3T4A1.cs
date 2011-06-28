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
	public struct VertexC4P3T4A1 : IVertexData
	{
		public ColorRGBA clr;
		public Vector3 pos;
		public Vector4 texCoord;
		public float attrib;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexDataFormat.VertexC4P3T4A1; }
		}

		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.EnableClientState(ArrayCap.ColorArray);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			if (mat.Technique != Duality.Resources.DrawTechnique.Picking) GL.ColorPointer(4, ColorPointerType.UnsignedByte, Size, (IntPtr)OffsetColor);
			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
			GL.TexCoordPointer(4, TexCoordPointerType.Float, Size, (IntPtr)OffsetTex0);

			if (mat.Technique.Res.Shader.IsAvailable)
			{
				Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
				for (int i = 0; i < varInfo.Length; i++)
				{
					if (varInfo[i].glVarLoc == -1) continue;
					if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
					if (varInfo[i].type != Resources.ShaderVarType.Float) continue;
				
					GL.EnableVertexAttribArray(varInfo[i].glVarLoc);
					GL.VertexAttribPointer(varInfo[i].glVarLoc, 1, VertexAttribPointerType.Float, false, Size, (IntPtr)OffsetAttrib);
					break;
				}
			}

			// --- From VertexP3T2A2 ---
			//Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
			//for (int i = 0; i < varInfo.Length; i++)
			//{
			//    if (varInfo[i].glVarLoc == -1) continue;
			//    if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
				
			//    GL.EnableVertexAttribArray(varInfo[i].glVarLoc);

			//    int curIndex = 0;
			//    switch (varInfo[i].type)
			//    {
			//        case Resources.ShaderVarType.Float:
			//            GL.VertexAttribPointer(varInfo[i].glVarLoc, varInfo[i].arraySize, VertexAttribPointerType.Float, false, Size, (IntPtr)(OffsetAttrib + sizeof(float) * curIndex));
			//            curIndex += varInfo[i].arraySize;
			//            break;
			//        case Resources.ShaderVarType.Vec2:
			//            GL.VertexAttribPointer(varInfo[i].glVarLoc, 2, VertexAttribPointerType.Float, false, Size, (IntPtr)(OffsetAttrib + sizeof(float) * curIndex));
			//            curIndex += 2;
			//            break;
			//    }
			//}

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.ColorArray);
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

			if (mat.Technique.Res.Shader.IsAvailable)
			{
				Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
				for (int i = 0; i < varInfo.Length; i++)
				{
					if (varInfo[i].glVarLoc == -1) continue;
					if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
					if (varInfo[i].type != Resources.ShaderVarType.Float) continue;
				
					GL.DisableVertexAttribArray(varInfo[i].glVarLoc);
					break;
				}
			}

			// --- From VertexP3T2A2 ---
			//Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
			//for (int i = 0; i < varInfo.Length; i++)
			//{
			//    if (varInfo[i].glVarLoc == -1) continue;
			//    if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
				
			//    GL.DisableVertexAttribArray(varInfo[i].glVarLoc);
			//}
		}

		public const int OffsetColor	= 0;
		public const int OffsetPos		= OffsetColor + 4 * sizeof(byte);
		public const int OffsetTex0		= OffsetPos + 3 * sizeof(float);
		public const int OffsetAttrib	= OffsetTex0 + 4 * sizeof(float);
		public const int Size			= OffsetAttrib + 1 * sizeof(float);

		public VertexC4P3T4A1(float x, float y, float z, float s, float t, float p, float q, float attrib, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = s;
			this.texCoord.Y = t;
			this.texCoord.Z = p;
			this.texCoord.W = q;
			this.attrib = attrib;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC4P3T4A1(float x, float y, float z, float s, float t, float p, float q, float attrib, ColorRGBA clr)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = s;
			this.texCoord.Y = t;
			this.texCoord.Z = p;
			this.texCoord.W = q;
			this.attrib = attrib;
			this.clr = clr;
		}
		public VertexC4P3T4A1(Vector3 pos, Vector4 stuv, float attrib, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
		{
			this.pos = pos;
			this.texCoord = stuv;
			this.attrib = attrib;
			this.clr.r = r;
			this.clr.g = g;
			this.clr.b = b;
			this.clr.a = a;
		}
		public VertexC4P3T4A1(Vector3 pos, Vector4 stuv, float attrib, ColorRGBA clr)
		{
			this.pos = pos;
			this.texCoord = stuv;
			this.attrib = attrib;
			this.clr = clr;
		}
	}
}
