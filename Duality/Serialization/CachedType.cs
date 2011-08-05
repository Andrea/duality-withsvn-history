using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public sealed class CachedType
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

		public CachedType(Type t)
		{
			this.type = t;
			this.fields = this.type.GetFields(ReflectionHelper.BindInstanceAll);
			this.typeString = ReflectionHelper.GetTypeString(this.type, ReflectionHelper.TypeStringAttrib.CSCodeIdent);
			this.dataType = SerializationHelper.GetDataType(this.type);
		}
	}
}
