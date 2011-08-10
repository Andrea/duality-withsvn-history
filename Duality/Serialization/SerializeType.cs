using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public sealed class SerializeType
	{
		private	Type		type;
		private	FieldInfo[]	fields;
		private	string		typeString;
		private	DataType	dataType;

		public Type Type
		{
			get { return this.type; }
		}
		public FieldInfo[] Fields
		{
			get { return this.fields; }
		}
		public string TypeString
		{
			get { return this.typeString; }
		}
		public DataType DataType
		{
			get { return this.dataType; }
		}

		public SerializeType(Type t)
		{
			this.type = t;
			this.fields = ReflectionHelper.GetAllFields(this.type, ReflectionHelper.BindInstanceAll).Where(f => !f.IsNotSerialized).ToArray();
			this.typeString = ReflectionHelper.GetTypeName(this.type, TypeNameFormat.FullNameWithoutAssembly);
			this.dataType = ReflectionHelper.GetDataType(this.type);
		}
	}
}
