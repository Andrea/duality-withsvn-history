using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality.VertexFormat
{
	public enum VertexFormat
	{
		VertexP3,
		VertexC4P3,
		VertexP3T2,
		VertexP3T2A2,

		Count
	}

	public interface IVertexData
	{
		#region Static Members
		int VertexTypeIndex { get; }

		void SetupVBO<T>(T[] vertexData, Duality.Resources.BatchInfo mat) where T : struct, IVertexData;
		void FinishVBO(Duality.Resources.BatchInfo mat);
		#endregion

		Vector3 Pos { get; }
	}
}
