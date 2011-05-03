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
	public struct VertexP3T2A2 : IVertexData
	{
		public Vector3 pos;
		public Vector2 texCoord;
		public Vector2 attrib;

		Vector3 IVertexData.Pos
		{
			get { return this.pos; }
		}
		int IVertexData.VertexTypeIndex
		{
			get { return (int)VertexFormat.VertexP3T2A2; }
		}

		void IVertexData.SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat)
		{
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			GL.VertexPointer(3, VertexPointerType.Float, Size, (IntPtr)OffsetPos);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, Size, (IntPtr)OffsetTex0);

			Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
			for (int i = 0; i < varInfo.Length; i++)
			{
				if (varInfo[i].glVarLoc == -1) continue;
				if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
				
				GL.EnableVertexAttribArray(varInfo[i].glVarLoc);

				int curIndex = 0;
				switch (varInfo[i].type)
				{
					case Resources.ShaderVarType.Float:
						GL.VertexAttribPointer(varInfo[i].glVarLoc, varInfo[i].arraySize, VertexAttribPointerType.Float, false, Size, (IntPtr)(OffsetAttrib + sizeof(float) * curIndex));
						curIndex += varInfo[i].arraySize;
						break;
					case Resources.ShaderVarType.Vec2:
						GL.VertexAttribPointer(varInfo[i].glVarLoc, 2, VertexAttribPointerType.Float, false, Size, (IntPtr)(OffsetAttrib + sizeof(float) * curIndex));
						curIndex += 2;
						break;
				}
			}

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), IntPtr.Zero, BufferUsageHint.StreamDraw);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Size * vertexData.Length), vertexData, BufferUsageHint.StreamDraw);
		}
		void IVertexData.FinishVBO(Duality.Resources.BatchInfo mat)
		{
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

			Resources.ShaderVarInfo[] varInfo = mat.Technique.Res.Shader.Res.VarInfo;
			for (int i = 0; i < varInfo.Length; i++)
			{
				if (varInfo[i].glVarLoc == -1) continue;
				if (varInfo[i].scope != Resources.ShaderVarScope.Attribute) continue;
				
				GL.DisableVertexAttribArray(varInfo[i].glVarLoc);
			}
		}

		public const int OffsetPos		= 0;
		public const int OffsetTex0		= OffsetPos + 3 * sizeof(float);
		public const int OffsetAttrib	= OffsetTex0 + 2 * sizeof(float);
		public const int Size			= OffsetAttrib + 2 * sizeof(float);

		public VertexP3T2A2(float x, float y, float z, float u, float v, float a0, float a1)
		{
			this.pos.X = x;
			this.pos.Y = y;
			this.pos.Z = z;
			this.texCoord.X = u;
			this.texCoord.Y = v;
			this.attrib.X = a0;
			this.attrib.Y = a1;
		}
		public VertexP3T2A2(Vector3 p, Vector2 t, Vector2 a)
		{
			this.pos = p;
			this.texCoord = t;
			this.attrib = a;
		}
	}
}
