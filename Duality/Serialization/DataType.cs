using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality.Serialization
{
	public enum DataType : byte
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

		String,
		Array,
		Class,
		Struct,

		ObjectRef
	}
}
