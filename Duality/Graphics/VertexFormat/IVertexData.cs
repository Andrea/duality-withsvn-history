using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality.VertexFormat
{
	/// <summary>
	/// Enumerates Dualitys default vertex data formats.
	/// </summary>
	public enum VertexDataFormat
	{
		/// <summary>
		/// Unknown format.
		/// </summary>
		Unknown	= -1,

		/// <summary>
		/// <see cref="Duality.VertexFormat.VertexP3"/> format.
		/// </summary>
		VertexP3,
		/// <summary>
		/// <see cref="Duality.VertexFormat.VertexC1P3"/> format.
		/// </summary>
		VertexC1P3,
		/// <summary>
		/// <see cref="Duality.VertexFormat.VertexC1P3T2"/> format.
		/// </summary>
		VertexC1P3T2,
		/// <summary>
		/// <see cref="Duality.VertexFormat.VertexC1P3T4A1"/> format.
		/// </summary>
		VertexC1P3T4A1,
		/// <summary>
		/// <see cref="Duality.VertexFormat.VertexP3T2"/> format.
		/// </summary>
		VertexP3T2,

		/// <summary>
		/// Total number of default vertex data formats.
		/// </summary>
		Count
	}

	/// <summary>
	/// A general interface for different types of vertex data.
	/// </summary>
	public interface IVertexData
	{
		#region Static Members
		/// <summary>
		/// [GET] An integer id representing this kind of vertex data. Usually equals the respective <see cref="VertexDataFormat"/>.
		/// This member is static by design.
		/// </summary>
		int VertexTypeIndex { get; }

		/// <summary>
		/// Sets up the currently bound OpenGL VertexBufferObject and injects actual vertex data.
		/// </summary>
		/// <param name="mat">
		/// The <see cref="Duality.Resources.Material"/> that is currently active. Usually only needed
		/// for custom vertex attributes in order to access <see cref="Duality.Resources.ShaderVarInfo">shader variables</see>.
		/// </param>
		void SetupVBO(Duality.Resources.BatchInfo mat);
		/// <summary>
		/// Uploads vertex data to the currently bound OpenGL VertexBufferObject.
		/// </summary>
		/// <typeparam name="T">The type of input vertex data to use.</typeparam>
		/// <param name="vertexData">The vertex data to be uploaded into the VBO.</param>
		void UploadToVBO<T>(T[] vertexData) where T : struct, IVertexData;
		/// <summary>
		/// Resets the VBO configuration.
		/// </summary>
		/// <param name="mat">The <see cref="Duality.Resources.Material"/> that was active when setting it up.</param>
		void FinishVBO(Duality.Resources.BatchInfo mat);
		#endregion

		/// <summary>
		/// [GET] The vertices position.
		/// </summary>
		Vector3 Pos { get; set; }
	}
}
