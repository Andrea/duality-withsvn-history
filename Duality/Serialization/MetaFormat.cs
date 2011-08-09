using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization.MetaFormat
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
	public class EnumNode : DataNode
	{
		protected	string	enumType;
		protected	string	name;
		protected	long	value;

		public string EnumType
		{
			get { return this.enumType; }
			set { this.enumType = value; }
		}
		public string ValueName
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public long Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		public EnumNode(string enumType, string name, long value) : base(DataType.Enum)
		{
			this.enumType = enumType;
			this.name = name;
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
		protected	bool	customSerialization;
		protected	bool	surrogateSerialization;

		public bool CustomSerialization
		{
			get { return this.customSerialization; }
		}
		public bool SurrogateSerialization
		{
			get { return this.surrogateSerialization; }
		}

		public StructNode(string typeString, uint objId, bool customSerialization, bool surrogateSerialization) : base(DataType.Struct, typeString, objId)
		{
			this.customSerialization = customSerialization;
			this.surrogateSerialization = surrogateSerialization;
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
			internal set { this.target = value; }
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
	public class DummyNode : DataNode
	{
		public DummyNode() : base(DataType.Unknown) {}
	}
}
