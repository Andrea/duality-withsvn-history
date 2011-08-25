﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Duality.Serialization.MetaFormat;

namespace Duality.Serialization
{
	/// <summary>
	/// De/Serializes abstract object data using <see cref="Duality.Serialization.MetaFormat.DataNode">DataNodes</see>.
	/// </summary>
	/// <seealso cref="Duality.Serialization.BinaryFormatter"/>
	public class BinaryMetaFormatter : BinaryFormatterBase
	{
		public BinaryMetaFormatter() : base() {}
		public BinaryMetaFormatter(Stream stream) : base(stream) {}

		/// <summary>
		/// Serializes the specified data node tree to the underlying <see cref="System.IO.Stream"/>.
		/// </summary>
		/// <param name="data">The data node tree to serialize.</param>
		public void WriteObject(DataNode data)
		{
			base.WriteObject(data);
		}
		/// <summary>
		/// Deserializes a data node tree from the underlying <see cref="System.IO.Stream"/>.
		/// </summary>
		/// <returns>A data node tree that has been deserialized.</returns>
		public new DataNode ReadObject()
		{
			return base.ReadObject() as DataNode;
		}
		
		protected override void GetWriteObjectData(object obj, out SerializeType objSerializeType, out DataType dataType, out uint objId)
		{
			DataNode node = obj as DataNode;
			objSerializeType = null;
			objId = 0;
			dataType = node.NodeType;

			if		(node is ObjectNode)	objId = (node as ObjectNode).ObjId;
			else if (node is ObjectRefNode) objId = (node as ObjectRefNode).ObjRefId;
		}
		protected override void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId)
		{
			if (dataType.IsPrimitiveType())				this.WritePrimitive((obj as PrimitiveNode).PrimitiveValue);
			else if (dataType == DataType.String)		this.WriteString((obj as StringNode).StringValue);
			else if (dataType == DataType.Enum)			this.WriteEnum(obj as EnumNode);
			else if (dataType == DataType.Struct)		this.WriteStruct(obj as StructNode);
			else if (dataType == DataType.ObjectRef)	this.writer.Write((obj as ObjectRefNode).ObjRefId);
			else if	(dataType == DataType.Array)		this.WriteArray(obj as ArrayNode);
			else if (dataType == DataType.Class)		this.WriteStruct(obj as StructNode);
			else if (dataType == DataType.Delegate)		this.WriteDelegate(obj as DelegateNode);
			else if (dataType.IsMemberInfoType())		this.WriteMemberInfo(obj as MemberInfoNode);
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.MetaFormat.MemberInfoNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected void WriteMemberInfo(MemberInfoNode node)
		{
			if (node == null) throw new ArgumentNullException("node");

			this.writer.Write(node.ObjId);
			if (node is TypeInfoNode)
			{
				TypeInfoNode type = node as TypeInfoNode;

				this.writer.Write(type.TypeString);
			}
			else if (node is FieldInfoNode)
			{
				FieldInfoNode field = node as FieldInfoNode;

				this.writer.Write(field.IsStatic);
				this.writer.Write(field.TypeString);
				this.writer.Write(field.FieldName);
			}
			else if (node is PropertyInfoNode)
			{
				PropertyInfoNode property = node as PropertyInfoNode;

				this.writer.Write(property.IsStatic);
				this.writer.Write(property.TypeString);
				this.writer.Write(property.PropertyName);
				this.writer.Write(property.PropertyType);
				this.writer.Write(property.ParameterTypes.Length);
				for (int i = 0; i < property.ParameterTypes.Length; i++)
					this.writer.Write(property.ParameterTypes[i]);
			}
			else if (node is MethodInfoNode)
			{
				MethodInfoNode method = node as MethodInfoNode;

				this.writer.Write(method.IsStatic);
				this.writer.Write(method.TypeString);
				this.writer.Write(method.MethodName);
				this.writer.Write(method.ParameterTypes.Length);
				for (int i = 0; i < method.ParameterTypes.Length; i++)
					this.writer.Write(method.ParameterTypes[i]);
			}
			else if (node is ConstructorInfoNode)
			{
				ConstructorInfoNode method = node as ConstructorInfoNode;

				this.writer.Write(method.IsStatic);
				this.writer.Write(method.TypeString);
				this.writer.Write(method.ParameterTypes.Length);
				for (int i = 0; i < method.ParameterTypes.Length; i++)
					this.writer.Write(method.ParameterTypes[i]);
			}
			else if (node is EventInfoNode)
			{
				EventInfoNode e = node as EventInfoNode;

				this.writer.Write(e.IsStatic);
				this.writer.Write(e.TypeString);
				this.writer.Write(e.EventName);
			}
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.MetaFormat.ArrayNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected void WriteArray(ArrayNode node)
		{
			if (node.Rank != 1) throw new ArgumentException("Non single-Rank arrays are not supported");

			this.writer.Write(node.TypeString);
			this.writer.Write(node.ObjId);
			this.writer.Write(node.Rank);

			
			if (node.PrimitiveData != null)
			{
				this.writer.Write(node.PrimitiveData.Length);
				Array objAsArray = node.PrimitiveData;
				if		(objAsArray is bool[])		this.WriteArrayData(objAsArray as bool[]);
				else if (objAsArray is byte[])		this.WriteArrayData(objAsArray as byte[]);
				else if (objAsArray is sbyte[])		this.WriteArrayData(objAsArray as sbyte[]);
				else if (objAsArray is short[])		this.WriteArrayData(objAsArray as short[]);
				else if (objAsArray is ushort[])	this.WriteArrayData(objAsArray as ushort[]);
				else if (objAsArray is int[])		this.WriteArrayData(objAsArray as int[]);
				else if (objAsArray is uint[])		this.WriteArrayData(objAsArray as uint[]);
				else if (objAsArray is long[])		this.WriteArrayData(objAsArray as long[]);
				else if (objAsArray is ulong[])		this.WriteArrayData(objAsArray as ulong[]);
				else if (objAsArray is float[])		this.WriteArrayData(objAsArray as float[]);
				else if (objAsArray is double[])	this.WriteArrayData(objAsArray as double[]);
				else if (objAsArray is decimal[])	this.WriteArrayData(objAsArray as decimal[]);
				else if (objAsArray is char[])		this.WriteArrayData(objAsArray as char[]);
				else if (objAsArray is string[])	this.WriteArrayData(objAsArray as string[]);
			}
			else
			{
				this.writer.Write(node.SubNodes.Count());
				foreach (DataNode subNode in node.SubNodes)
					this.WriteObject(subNode);
			}
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.MetaFormat.StructNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected void WriteStruct(StructNode node)
		{
			// Write the structs data type
			this.writer.Write(node.TypeString);
			this.writer.Write(node.ObjId);
			this.writer.Write(node.CustomSerialization);
			this.writer.Write(node.SurrogateSerialization);

			if (node.SurrogateSerialization)
			{
				CustomSerialIO customIO = new CustomSerialIO();
				DummyNode surrogateConstructor = node.SubNodes.FirstOrDefault() as DummyNode;
				if (surrogateConstructor != null)
				{
					var enumerator = surrogateConstructor.SubNodes.GetEnumerator();
					while (enumerator.MoveNext())
					{
						StringNode key = enumerator.Current as StringNode;
						if (enumerator.MoveNext() && key != null)
						{
							DataNode value = enumerator.Current;
							customIO.WriteValue(key.StringValue, value);
						}
					}
				}
				customIO.Serialize(this);
			}

			if (node.CustomSerialization || node.SurrogateSerialization)
			{
				CustomSerialIO customIO = new CustomSerialIO();
				var enumerator = node.SubNodes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StringNode key = enumerator.Current as StringNode;
					if (key != null && enumerator.MoveNext())
					{
						DataNode value = enumerator.Current;
						customIO.WriteValue(key.StringValue, value);
					}
				}
				customIO.Serialize(this);
			}
			else
			{
				bool skipLayout = false;
				if (node.SubNodes.FirstOrDefault() is TypeDataLayoutNode)
				{
					TypeDataLayoutNode typeDataLayout = node.SubNodes.FirstOrDefault() as TypeDataLayoutNode;
					this.WriteTypeDataLayout(typeDataLayout.Layout, node.TypeString);
					skipLayout = true;
				}
				else
				{
					this.WriteTypeDataLayout(node.TypeString);
				}

				// Write the structs fields
				foreach (DataNode subNode in node.SubNodes)
				{
					if (skipLayout)
					{
						skipLayout = false;
						continue;
					}
					if (subNode is DummyNode) continue;
					this.WriteObject(subNode);
				}
			}
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.MetaFormat.DelegateNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected void WriteDelegate(DelegateNode node)
		{
			// Write the delegates type
			this.writer.Write(node.TypeString);
			this.writer.Write(node.ObjId);
			this.writer.Write(node.InvokeList != null);

			this.WriteObject(node.Method);
			this.WriteObject(node.Target);
			if (node.InvokeList != null) this.WriteObject(node.InvokeList);
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.MetaFormat.EnumNode"/>.
		/// </summary>
		/// <param name="node"></param>
		protected void WriteEnum(EnumNode node)
		{
			this.writer.Write(node.EnumType);
			this.writer.Write(node.ValueName);
			this.writer.Write(node.Value);
		}

		protected override object GetNullObject()
		{
			return new PrimitiveNode(DataType.Unknown, null);
		}
		protected override object ReadObjectBody(DataType dataType)
		{
			DataNode result = null;

			if (dataType.IsPrimitiveType())				result = new PrimitiveNode(dataType, this.ReadPrimitive(dataType));
			else if (dataType == DataType.String)		result = new StringNode(this.ReadString());
			else if (dataType == DataType.Enum)			result = this.ReadEnum();
			else if (dataType == DataType.Struct)		result = this.ReadStruct();
			else if (dataType == DataType.ObjectRef)	result = this.ReadObjectRef();
			else if (dataType == DataType.Array)		result = this.ReadArray();
			else if (dataType == DataType.Class)		result = this.ReadStruct();
			else if (dataType == DataType.Delegate)		result = this.ReadDelegate();
			else if (dataType.IsMemberInfoType())		result = this.ReadMemberInfo(dataType);

			return result;
		}
		/// <summary>
		/// Reads a <see cref="Duality.Serialization.MetaFormat.MemberInfoNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected MemberInfoNode ReadMemberInfo(DataType dataType)
		{
			uint objId = this.reader.ReadUInt32();
			MemberInfoNode result;

			if (dataType == DataType.Type)
			{
				string typeString = this.reader.ReadString();
				result = new TypeInfoNode(typeString, objId);
			}
			else if (dataType == DataType.FieldInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string fieldName = this.reader.ReadString();
				result = new FieldInfoNode(declaringTypeString, objId, fieldName, isStatic);
			}
			else if (dataType == DataType.PropertyInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string propertyName = this.reader.ReadString();
				string propertyTypeString = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				result = new PropertyInfoNode(declaringTypeString, objId, propertyName, propertyTypeString, paramTypeStrings, isStatic);
			}
			else if (dataType == DataType.MethodInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string methodName = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				result = new MethodInfoNode(declaringTypeString, objId, methodName, paramTypeStrings, isStatic);
			}
			else if (dataType == DataType.ConstructorInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				result = new ConstructorInfoNode(declaringTypeString, objId, paramTypeStrings, isStatic);
			}
			else if (dataType == DataType.EventInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string eventName = this.reader.ReadString();

				result = new EventInfoNode(declaringTypeString, objId, eventName, isStatic);
			}
			else
				throw new ApplicationException(string.Format("Invalid DataType '{0}' in ReadMemberInfo method.", dataType));
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			return result;
		}
		/// <summary>
		/// Reads an <see cref="Duality.Serialization.MetaFormat.ArrayNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected ArrayNode ReadArray()
		{
			string	arrTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			int		arrRank			= this.reader.ReadInt32();
			int		arrLength		= this.reader.ReadInt32();
			Type	arrType			= ReflectionHelper.ResolveType(arrTypeString, false);

			ArrayNode result = new ArrayNode(arrTypeString, objId, arrRank, arrLength);
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			// Store primitive data block
			bool nonPrimitive = false;
			if (arrType != null)
			{
				Array arrObj = Array.CreateInstance(arrType.GetElementType(), arrLength);
				if		(arrObj is bool[])		this.ReadArrayData(arrObj as bool[]);
				else if (arrObj is byte[])		this.ReadArrayData(arrObj as byte[]);
				else if (arrObj is sbyte[])		this.ReadArrayData(arrObj as sbyte[]);
				else if (arrObj is short[])		this.ReadArrayData(arrObj as short[]);
				else if (arrObj is ushort[])	this.ReadArrayData(arrObj as ushort[]);
				else if (arrObj is int[])		this.ReadArrayData(arrObj as int[]);
				else if (arrObj is uint[])		this.ReadArrayData(arrObj as uint[]);
				else if (arrObj is long[])		this.ReadArrayData(arrObj as long[]);
				else if (arrObj is ulong[])		this.ReadArrayData(arrObj as ulong[]);
				else if (arrObj is float[])		this.ReadArrayData(arrObj as float[]);
				else if (arrObj is double[])	this.ReadArrayData(arrObj as double[]);
				else if (arrObj is decimal[])	this.ReadArrayData(arrObj as decimal[]);
				else if (arrObj is char[])		this.ReadArrayData(arrObj as char[]);
				else if (arrObj is string[])	this.ReadArrayData(arrObj as string[]);
				else
					nonPrimitive = true;

				if (!nonPrimitive) result.PrimitiveData = arrObj;
			}
			else
				nonPrimitive = true;

			// Store other data as sub-nodes
			if (nonPrimitive)
			{
				for (int i = 0; i < arrLength; i++)
				{
					DataNode child = this.ReadObject();
					child.Parent = result;
				}
			}

			return result;
		}
		/// <summary>
		/// Reads a <see cref="Duality.Serialization.MetaFormat.StructNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected StructNode ReadStruct()
		{
			// Read struct type
			string	objTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			bool	custom			= this.reader.ReadBoolean();
			bool	surrogate		= this.reader.ReadBoolean();

			StructNode result = new StructNode(objTypeString, objId, custom, surrogate);
			
			// Read surrogate constructor data
			if (surrogate)
			{
				custom = true;

				// Set fake object reference for surrogate constructor: No self-references allowed here.
				if (objId != 0) this.idObjRefMap[objId] = null;

				CustomSerialIO customIO = new CustomSerialIO();
				customIO.Deserialize(this);
				if (customIO.Values.Any())
				{
					DummyNode surrogateConstructor = new DummyNode();
					surrogateConstructor.Parent = result;
					foreach (var pair in customIO.Values)
					{
						StringNode key = new StringNode(pair.Key);
						DataNode value = pair.Value as DataNode;
						key.Parent = surrogateConstructor;
						value.Parent = surrogateConstructor;
					}
				}
			}

			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			if (custom)
			{
				CustomSerialIO customIO = new CustomSerialIO();
				customIO.Deserialize(this);
				foreach (var pair in customIO.Values)
				{
					StringNode key = new StringNode(pair.Key);
					DataNode value = pair.Value as DataNode;
					key.Parent = result;
					value.Parent = result;
				}
			}
			else
			{
				// Determine data layout
				bool wasThereBefore = this.IsTypeDataLayoutCached(objTypeString);
				TypeDataLayout layout = this.ReadTypeDataLayout(objTypeString);
				if (!wasThereBefore)
				{
					TypeDataLayoutNode layoutNode = new TypeDataLayoutNode(new TypeDataLayout(layout));
					layoutNode.Parent = result;
				}

				// Read fields
				for (int i = 0; i < layout.Fields.Length; i++)
				{
					DataNode fieldValue = this.ReadObject();
					fieldValue.Parent = result;
				}
			}

			return result;
		}
		/// <summary>
		/// Reads a <see cref="Duality.Serialization.MetaFormat.DelegateNode"/>, including possible child nodes.
		/// </summary>
		/// <param name="node"></param>
		protected DelegateNode ReadDelegate()
		{
			string		delegateTypeString	= this.reader.ReadString();
			uint		objId				= this.reader.ReadUInt32();
			bool		multi				= this.reader.ReadBoolean();

			DataNode method	= this.ReadObject();
			DataNode target	= null;

			// Create the delegate without target and fix it later, so we don't load its target object before setting this object id
			DelegateNode result = new DelegateNode(delegateTypeString, objId, method, target, null);

			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			// Load & fix the target object
			target = this.ReadObject();
			target.Parent = result;
			result.Target = target;

			// Combine multicast delegates
			if (multi)
			{
				DataNode invokeList = this.ReadObject();
				result.InvokeList = invokeList;
			}

			return result;
		}
		/// <summary>
		/// Reads an <see cref="Duality.Serialization.MetaFormat.EnumNode"/>.
		/// </summary>
		/// <param name="node"></param>
		protected EnumNode ReadEnum()
		{
			string typeName = this.reader.ReadString();
			string name = this.reader.ReadString();
			long val = this.reader.ReadInt64();
			return new EnumNode(typeName, name, val);
		}
		/// <summary>
		/// Reads an <see cref="Duality.Serialization.MetaFormat.ObjectRefNode"/>.
		/// </summary>
		/// <param name="node"></param>
		protected ObjectRefNode ReadObjectRef()
		{
			uint objId = this.reader.ReadUInt32();
			ObjectRefNode result = new ObjectRefNode(objId);
			return result;
		}
	}
}