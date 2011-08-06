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

			public ArrayNode(string typeString, uint objId, int rank, int length) : base(DataType.Array, typeString, objId)
			{
				this.rank = rank;
				this.length = length;
			}
		}
		public class StructNode : ObjectNode
		{
			public StructNode(string typeString, uint objId) : base(DataType.Array, typeString, objId)
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
		}


		public BinaryMetaFormatter() : base() {}
		public BinaryMetaFormatter(Stream stream) : base(stream) {}

		public DataNode ReadObject()
		{
			if (!this.CanRead) return null;
			if (this.reader.BaseStream.Position == this.reader.BaseStream.Length) return null;
			if (this.lastOperation != Operation.Read)
			{
				this.ClearStreamSpecificData();
				this.ReadFormatterHeader();
			}
			this.lastOperation = Operation.Read;

			// Not null flag
			bool isNotNull = this.reader.ReadBoolean();
			if (!isNotNull) return new PrimitiveNode(DataType.Unknown, null);

			// Read data type header
			DataType dataType = this.ReadDataType();
			long lastPos = this.reader.BaseStream.Position;
			long offset = this.reader.ReadInt64();

			// Read object
			DataNode result = null;
			try
			{
				if (dataType.IsPrimitiveType())				result = this.ReadPrimitive(dataType);
				else if (dataType == DataType.String)		result = new StringNode(this.reader.ReadString());
				else if (dataType == DataType.Struct)		result = this.ReadStruct();
				else if (dataType == DataType.ObjectRef)	result = this.ReadObjectRef();
				else if (dataType == DataType.Array)		result = this.ReadArray();
				else if (dataType == DataType.Class)		result = this.ReadStruct();
				else if (dataType == DataType.Delegate)		result = this.ReadDelegate();
				else if (dataType.IsMemberInfoType())		result = this.ReadMemberInfo(dataType);

				// If we read the object properly and aren't where we're supposed to be, something went wrong
				if (this.reader.BaseStream.Position != lastPos + offset) throw new ApplicationException(string.Format("Wrong dataset offset: '{0}' instead of expected value '{1}'.", offset, this.reader.BaseStream.Position - lastPos));
			}
			catch (Exception e)
			{
				// If anything goes wrong, assure the stream position is valid and points to the next data entry
				this.reader.BaseStream.Seek(lastPos + offset, SeekOrigin.Begin);
				// Log the error
				Log.Core.WriteError("Error reading object at '{0:X8}'-'{1:X8}':\n{2}", lastPos, lastPos + offset, e.ToString());
			}

			return result;
		}
		protected PrimitiveNode ReadPrimitive(DataType dataType)
		{
			object value = null;
			switch (dataType)
			{
				case DataType.Bool:			value = this.reader.ReadBoolean();	break;
				case DataType.Byte:			value = this.reader.ReadByte();		break;
				case DataType.SByte:		value = this.reader.ReadSByte();	break;
				case DataType.Short:		value = this.reader.ReadInt16();	break;
				case DataType.UShort:		value = this.reader.ReadUInt16();	break;
				case DataType.Int:			value = this.reader.ReadInt32();	break;
				case DataType.UInt:			value = this.reader.ReadUInt32();	break;
				case DataType.Long:			value = this.reader.ReadInt64();	break;
				case DataType.ULong:		value = this.reader.ReadUInt64();	break;
				case DataType.Float:		value = this.reader.ReadSingle();	break;
				case DataType.Double:		value = this.reader.ReadDouble();	break;
				case DataType.Decimal:		value = this.reader.ReadDecimal();	break;
				case DataType.Char:			value = this.reader.ReadChar();		break;
				default:
					throw new ArgumentException(string.Format("DataType '{0}' is not a primitive.", dataType));
			}
			return new PrimitiveNode(dataType, value);
		}
		protected ArrayNode ReadArray()
		{
			string	arrTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			int		arrRank			= this.reader.ReadInt32();
			int		arrLength		= this.reader.ReadInt32();
			Type	arrType			= ReflectionHelper.ResolveType(arrTypeString);

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
