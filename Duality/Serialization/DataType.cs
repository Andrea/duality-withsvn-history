using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public enum DataType : ushort
	{
		Unknown,

		Bool,
		Byte,
		SByte,
		Short,
		UShort,
		Int,
		UInt,
		Long,
		ULong,
		Float,
		Double,
		Decimal,
		Char,

		Type,
		FieldInfo,
		PropertyInfo,
		MethodInfo,
		ConstructorInfo,
		EventInfo,

		Delegate,

		Enum,
		String,
		Array,
		Class,
		Struct,

		ObjectRef
	}

	public static class ExtMethodsDataType
	{
		public static bool IsPrimitiveType(this DataType dt)
		{
			return (ushort)dt >= (ushort)DataType.Bool && (ushort)dt <= (ushort)DataType.Char;
		}
		public static bool IsMemberInfoType(this DataType dt)
		{
			return (ushort)dt >= (ushort)DataType.Type && (ushort)dt <= (ushort)DataType.EventInfo;
		}
		public static Type ToActualType(this DataType dt)
		{
			switch (dt)
			{
				case DataType.Array:			return typeof(Array);
				case DataType.Bool:				return typeof(bool);
				case DataType.Byte:				return typeof(byte);
				case DataType.Char:				return typeof(char);
				case DataType.Class:			return typeof(object);
				case DataType.ConstructorInfo:	return typeof(ConstructorInfo);
				case DataType.Decimal:			return typeof(decimal);
				case DataType.Delegate:			return typeof(Delegate);
				case DataType.Double:			return typeof(double);
				case DataType.EventInfo:		return typeof(EventInfo);
				case DataType.FieldInfo:		return typeof(FieldInfo);
				case DataType.Float:			return typeof(float);
				case DataType.Int:				return typeof(int);
				case DataType.Long:				return typeof(long);
				case DataType.MethodInfo:		return typeof(MethodInfo);
				case DataType.ObjectRef:		return typeof(object);
				case DataType.PropertyInfo:		return typeof(PropertyInfo);
				case DataType.SByte:			return typeof(sbyte);
				case DataType.Short:			return typeof(short);
				case DataType.String:			return typeof(string);
				case DataType.Struct:			return typeof(object);
				case DataType.Type:				return typeof(Type);
				case DataType.UInt:				return typeof(uint);
				case DataType.ULong:			return typeof(ulong);
				case DataType.Unknown:			return null;
				case DataType.UShort:			return typeof(ushort);
				default:						return null;
			}
		}
	}
}
