using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	public class BinaryMetaFormatter : BinaryFormatterBase
	{
		public abstract class DataNode
		{
			protected	DataType		dataType;
			protected	DataNode		parent;
			protected	List<DataNode>	subNodes;

			public IEnumerable<DataNode> SubNodes
			{
				get { return this.subNodes; }
			}
			public DataNode Parent
			{
				get { return this.parent; }
				set
				{
					if (this.parent == value) return;

					if (this.parent != null) this.parent.subNodes.Remove(this);
					this.parent = value;
					if (this.parent != null) this.parent.subNodes.Add(this);
				}
			}
			public DataType NodeType
			{
				get { return this.dataType; }
			}

			protected DataNode(DataType dataType)
			{
				this.dataType = dataType;
				this.subNodes = new List<DataNode>();
			}

			public List<string> GetTypeStrings(bool deep)
			{
				List<string> result = new List<string>();
				this.GetTypeStrings(ref result, deep);
				return result;
			}
			protected virtual void GetTypeStrings(ref List<string> list, bool deep)
			{
				if (!deep) return;
				foreach (DataNode n in this.subNodes)
					n.GetTypeStrings(ref list, deep);
			}
			public virtual bool IsObjectIdDefined(uint objId)
			{
				foreach (DataNode n in this.subNodes)
					if (n.IsObjectIdDefined(objId)) return true;
				return false;
			}
			public virtual int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = 0;
				foreach (DataNode n in this.subNodes)
					count += n.ReplaceTypeStrings(oldTypeString, newTypeString);
				return count;
			}
		}
		public class PrimitiveNode : DataNode
		{
			protected	object	value;

			public object PrimitiveValue
			{
				get { return this.value; }
				set { this.value = value; }
			}

			public PrimitiveNode(DataType dataType, object value) : base(dataType)
			{
				this.value = value;
			}
		}
		public class StringNode : DataNode
		{
			protected	string	value;

			public string StringValue
			{
				get { return this.value; }
				set { this.value = value; }
			}

			public StringNode(string value) : base(DataType.String)
			{
				this.value = value;
			}
		}
		public abstract class ObjectNode : DataNode
		{
			protected	string	typeString;
			protected	uint	objId;
			
			public string TypeString
			{
				get { return this.typeString; }
				set { this.typeString = value; }
			}
			public uint ObjId
			{
				get { return this.objId; }
				set { this.objId = value; }
			}

			public ObjectNode(DataType dataType, string typeString, uint objId) : base(dataType)
			{
				this.typeString = typeString;
				this.objId = objId;
			}

			protected override void GetTypeStrings(ref List<string> list, bool deep)
			{
				list.Add(this.typeString);
				base.GetTypeStrings(ref list, deep);
			}
			public override bool IsObjectIdDefined(uint objId)
			{
				if (this.objId == objId) return true;
				return base.IsObjectIdDefined(objId);
			}
			public override int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = base.ReplaceTypeStrings(oldTypeString, newTypeString);
				if (this.typeString == oldTypeString)
				{
					this.typeString = newTypeString;
					count++;
				}
				return count;
			}
		}
		public class ArrayNode : ObjectNode
		{
			protected	int		rank;
			protected	int		length;
			protected	Array	primitiveData;

			public Array PrimitiveData
			{
				get { return this.primitiveData; }
				set
				{ 
					this.primitiveData = value;
					if (this.primitiveData != null)
					{
						this.rank = this.primitiveData.Rank;
						this.length = this.primitiveData.Length;
					}
					else
					{
						this.rank = 0;
						this.length = 0;
					}
				}
			}
			public int Rank
			{
				get { return this.rank; }
			}
			public int Length
			{
				get { return this.length; }
			}

			public ArrayNode(string typeString, uint objId, int rank, int length) : base(DataType.Array, typeString, objId)
			{
				this.rank = rank;
				this.length = length;
			}
		}
		public class StructNode : ObjectNode
		{
			public StructNode(string typeString, uint objId) : base(DataType.Struct, typeString, objId)
			{

			}
		}
		public class ObjectRefNode : DataNode
		{
			protected uint objId;

			public uint ObjRefId
			{
				get { return this.objId; }
				set { this.objId = value; }
			}

			public ObjectRefNode(uint objId) : base(DataType.ObjectRef)
			{
				this.objId = objId;
			}
		}
		public class MemberInfoNode : ObjectNode
		{
			protected	bool	isStatic;

			public bool IsStatic
			{
				get { return this.isStatic; }
				set { this.isStatic = value; }
			}

			public MemberInfoNode(DataType dataType, string mainTypeString, uint objId, bool isStatic) : base(dataType, mainTypeString, objId)
			{
				this.isStatic = isStatic;
			}
		}
		public class TypeInfoNode : MemberInfoNode
		{
			public TypeInfoNode(string mainTypeString, uint objId) : base(DataType.Type, mainTypeString, objId, true)
			{

			}
		}
		public class FieldInfoNode : MemberInfoNode
		{
			protected string fieldName;

			public string FieldName
			{
				get { return this.fieldName; }
				set { this.fieldName = value; }
			}

			public FieldInfoNode(string mainTypeString, uint objId, string fieldName, bool isStatic) : base(DataType.FieldInfo, mainTypeString, objId, isStatic)
			{
				this.fieldName = fieldName;
			}
		}
		public class PropertyInfoNode : MemberInfoNode
		{
			protected string propertyName;
			protected string propertyType;
			protected string[] parameterTypeStrings;

			public string PropertyName
			{
				get { return this.propertyName; }
				set { this.propertyName = value; }
			}
			public string PropertyType
			{
				get { return this.propertyType; }
				set { this.propertyType = value; }
			}
			public string[] ParameterTypes
			{
				get { return this.parameterTypeStrings; }
				set { this.parameterTypeStrings = value; }
			}

			public PropertyInfoNode(string mainTypeString, uint objId, string propertyName, string propertyType, string[] parameterTypeStrings, bool isStatic) : base(DataType.PropertyInfo, mainTypeString, objId, isStatic)
			{
				this.propertyName = propertyName;
				this.propertyType = propertyType;
				this.parameterTypeStrings = parameterTypeStrings;
			}

			protected override void GetTypeStrings(ref List<string> list, bool deep)
			{
				list.Add(this.propertyType);
				list.AddRange(this.parameterTypeStrings);
				base.GetTypeStrings(ref list, deep);
			}
			public override int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = base.ReplaceTypeStrings(oldTypeString, newTypeString);
				if (this.propertyType == oldTypeString)
				{
					this.propertyType = newTypeString;
					count++;
				}
				for (int i = 0; i < this.parameterTypeStrings.Length; i++)
				{
					if (this.parameterTypeStrings[i] == oldTypeString)
					{
						this.parameterTypeStrings[i] = newTypeString;
						count++;
					}
				}
				return count;
			}
		}
		public class MethodInfoNode : MemberInfoNode
		{
			protected string methodName;
			protected string[] parameterTypeStrings;
			
			public string MethodName
			{
				get { return this.methodName; }
				set { this.methodName = value; }
			}
			public string[] ParameterTypes
			{
				get { return this.parameterTypeStrings; }
				set { this.parameterTypeStrings = value; }
			}

			public MethodInfoNode(string mainTypeString, uint objId, string methodName, string[] parameterTypeStrings, bool isStatic) : base(DataType.MethodInfo, mainTypeString, objId, isStatic)
			{
				this.methodName = methodName;
				this.parameterTypeStrings = parameterTypeStrings;
			}

			protected override void GetTypeStrings(ref List<string> list, bool deep)
			{
				list.AddRange(this.parameterTypeStrings);
				base.GetTypeStrings(ref list, deep);
			}
			public override int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = base.ReplaceTypeStrings(oldTypeString, newTypeString);
				for (int i = 0; i < this.parameterTypeStrings.Length; i++)
				{
					if (this.parameterTypeStrings[i] == oldTypeString)
					{
						this.parameterTypeStrings[i] = newTypeString;
						count++;
					}
				}
				return count;
			}
		}
		public class ConstructorInfoNode : MemberInfoNode
		{
			protected string[] parameterTypeStrings;

			public string[] ParameterTypes
			{
				get { return this.parameterTypeStrings; }
				set { this.parameterTypeStrings = value; }
			}

			public ConstructorInfoNode(string mainTypeString, uint objId, string[] parameterTypeStrings, bool isStatic) : base(DataType.ConstructorInfo, mainTypeString, objId, isStatic)
			{
				this.parameterTypeStrings = parameterTypeStrings;
			}
			
			protected override void GetTypeStrings(ref List<string> list, bool deep)
			{
				list.AddRange(this.parameterTypeStrings);
				base.GetTypeStrings(ref list, deep);
			}
			public override int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = base.ReplaceTypeStrings(oldTypeString, newTypeString);
				for (int i = 0; i < this.parameterTypeStrings.Length; i++)
				{
					if (this.parameterTypeStrings[i] == oldTypeString)
					{
						this.parameterTypeStrings[i] = newTypeString;
						count++;
					}
				}
				return count;
			}
		}
		public class EventInfoNode : MemberInfoNode
		{
			protected string eventName;
			
			public string EventName
			{
				get { return this.eventName; }
				set { this.eventName = value; }
			}

			public EventInfoNode(string mainTypeString, uint objId, string fieldName, bool isStatic) : base(DataType.EventInfo, mainTypeString, objId, isStatic)
			{
				this.eventName = fieldName;
			}
		}
		public class DelegateNode : ObjectNode
		{
			protected	DataNode	method;
			protected	DataNode	target;
			protected	DataNode	invokeList;

			public DataNode InvokeList
			{
				get { return this.invokeList; }
				set 
				{
					if (this.invokeList != null) this.invokeList.Parent = null;
					this.invokeList = value;
					if (this.invokeList != null) this.invokeList.Parent = this;
				}
			}
			public DataNode Method
			{
				get { return this.method; }
			}
			public DataNode Target
			{
				get { return this.target; }
			}

			public DelegateNode(string typeString, uint objId, DataNode method, DataNode target, DataNode invokeList) : base(DataType.Delegate, typeString, objId) 
			{
				this.method = method;
				this.target = target;
				this.invokeList = invokeList;

				if (this.method != null) this.method.Parent = this;
				if (this.target != null) this.target.Parent = this;
				if (this.invokeList != null) this.invokeList.Parent = this;
			}
		}
		public class TypeDataLayoutNode : DataNode
		{
			protected	TypeDataLayout	layout;

			public TypeDataLayout Layout
			{
				get { return this.layout; }
				set { this.layout = value; }
			}

			public TypeDataLayoutNode(TypeDataLayout layout) : base(DataType.Unknown)
			{
				this.layout = layout;
			}
			
			protected override void GetTypeStrings(ref List<string> list, bool deep)
			{
				list.AddRange(this.layout.Fields.Select(f => f.typeString));
				base.GetTypeStrings(ref list, deep);
			}
			public override int ReplaceTypeStrings(string oldTypeString, string newTypeString)
			{
				int count = base.ReplaceTypeStrings(oldTypeString, newTypeString);
				for (int i = 0; i < this.layout.Fields.Length; i++)
				{
					if (this.layout.Fields[i].typeString == oldTypeString)
					{
						TypeDataLayout.FieldDataInfo field = this.layout.Fields[i];
						field.typeString = newTypeString;
						this.layout.Fields[i] = field;
						count++;
					}
				}
				return count;
			}
		}


		public BinaryMetaFormatter() : base() {}
		public BinaryMetaFormatter(Stream stream) : base(stream) {}

		public void WriteObject(DataNode data)
		{
			base.WriteObject(data);
		}
		public new DataNode ReadObject()
		{
			object obj = base.ReadObject();
			return obj != null ? obj as DataNode : new PrimitiveNode(DataType.Unknown, null);
		}
		
		protected override void GetWriteObjectData(object obj, out CachedType objCachedType, out DataType dataType, out uint objId)
		{
			DataNode node = obj as DataNode;
			objCachedType = null;
			objId = 0;
			dataType = node.NodeType;

			if		(node is ObjectNode)	objId = (node as ObjectNode).ObjId;
			else if (node is ObjectRefNode) objId = (node as ObjectRefNode).ObjRefId;
		}
		protected override void WriteObjectBody(DataType dataType, object obj, CachedType objCachedType, uint objId)
		{
			if (dataType.IsPrimitiveType())				this.WritePrimitive((obj as PrimitiveNode).PrimitiveValue);
			else if (dataType == DataType.String)		this.writer.Write((obj as StringNode).StringValue);
			else if (dataType == DataType.Struct)		this.WriteStruct(obj as StructNode);
			else if (dataType == DataType.ObjectRef)	this.writer.Write((obj as ObjectRefNode).ObjRefId);
			else if	(dataType == DataType.Array)		this.WriteArray(obj as ArrayNode);
			else if (dataType == DataType.Class)		this.WriteStruct(obj as StructNode);
			else if (dataType == DataType.Delegate)		this.WriteDelegate(obj as DelegateNode);
			else if (dataType.IsMemberInfoType())		this.WriteMemberInfo(obj as MemberInfoNode);
		}
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
		protected void WriteStruct(StructNode node)
		{
			// Write the structs data type
			this.writer.Write(node.TypeString);
			this.writer.Write(node.ObjId);

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
				this.WriteObject(subNode);
			}
		}
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

		protected override object ReadObjectBody(DataType dataType)
		{
			DataNode result = null;

			if (dataType.IsPrimitiveType())				result = new PrimitiveNode(dataType, this.ReadPrimitive(dataType));
			else if (dataType == DataType.String)		result = new StringNode(this.reader.ReadString());
			else if (dataType == DataType.Struct)		result = this.ReadStruct();
			else if (dataType == DataType.ObjectRef)	result = this.ReadObjectRef();
			else if (dataType == DataType.Array)		result = this.ReadArray();
			else if (dataType == DataType.Class)		result = this.ReadStruct();
			else if (dataType == DataType.Delegate)		result = this.ReadDelegate();
			else if (dataType.IsMemberInfoType())		result = this.ReadMemberInfo(dataType);

			return result;
		}
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
		protected StructNode ReadStruct()
		{
			// Read struct type
			string	objTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();

			StructNode result = new StructNode(objTypeString, objId);
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

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

			return result;
		}
		protected ObjectRefNode ReadObjectRef()
		{
			uint objId = this.reader.ReadUInt32();
			ObjectRefNode result = new ObjectRefNode(objId);
			return result;
		}
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
		protected DelegateNode ReadDelegate()
		{
			string		delegateTypeString	= this.reader.ReadString();
			uint		objId				= this.reader.ReadUInt32();
			bool		multi				= this.reader.ReadBoolean();

			DataNode method	= this.ReadObject();
			DataNode target	= this.ReadObject();

			DelegateNode result = new DelegateNode(delegateTypeString, objId, method, target, null);

			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			// Combine multicast delegates
			if (multi)
			{
				DataNode invokeList = this.ReadObject();
				result.InvokeList = invokeList;
			}

			return result;
		}
	}
}
