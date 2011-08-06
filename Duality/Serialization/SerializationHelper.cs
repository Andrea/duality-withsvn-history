using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Duality.Serialization
{
	public static class SerializationHelper
	{
		private	static	Dictionary<Type,CachedType>	typeCache			= new Dictionary<Type,CachedType>();
		private	static	Dictionary<string,Type>		typeResolveCache	= new Dictionary<string,Type>();

		public static void ClearTypeCache()
		{
			typeCache.Clear();
			typeResolveCache.Clear();
		}
		public static Type ResolveType(string typeString, bool throwOnError = true)
		{
			Type result;
			if (typeResolveCache.TryGetValue(typeString, out result)) return result;

			Assembly[] searchAsm = AppDomain.CurrentDomain.GetAssemblies().Except(DualityApp.DisposedPlugins).ToArray();
			result = ReflectionHelper.FindTypeByFullNameWithoutAssembly(typeString, searchAsm);
			typeResolveCache[typeString] = result;

			if (result == null && throwOnError) throw new ApplicationException(string.Format("Cannot resolve Type '{0}'. Type not found", typeString));
			return result;
		}
		public static CachedType GetCachedType(Type t)
		{
			CachedType result;
			if (typeCache.TryGetValue(t, out result)) return result;

			result = new CachedType(t);
			typeCache[t] = result;
			typeResolveCache[result.TypeString] = result.Type;
			return result;
		}

		public static DataType GetDataType(Type t)
		{
			if (t.IsPrimitive)
			{
				if		(t == typeof(bool))		return DataType.Bool;
				else if (t == typeof(byte))		return DataType.Byte;
				else if (t == typeof(char))		return DataType.Char;
				else if (t == typeof(sbyte))	return DataType.SByte;
				else if (t == typeof(short))	return DataType.Short;
				else if (t == typeof(ushort))	return DataType.UShort;
				else if (t == typeof(int))		return DataType.Int;
				else if (t == typeof(uint))		return DataType.UInt;
				else if (t == typeof(long))		return DataType.Long;
				else if (t == typeof(ulong))	return DataType.ULong;
				else if (t == typeof(float))	return DataType.Float;
				else if (t == typeof(double))	return DataType.Double;
				else if (t == typeof(decimal))	return DataType.Decimal;

				throw new NotSupportedException(string.Format("Primitive Type '{0}' is not supported", t));
			}
			else if (typeof(MemberInfo).IsAssignableFrom(t))
			{
				if		(typeof(Type).IsAssignableFrom(t))				return DataType.Type;
				else if (typeof(FieldInfo).IsAssignableFrom(t))			return DataType.FieldInfo;
				else if (typeof(PropertyInfo).IsAssignableFrom(t))		return DataType.PropertyInfo;
				else if (typeof(MethodInfo).IsAssignableFrom(t))		return DataType.MethodInfo;
				else if (typeof(ConstructorInfo).IsAssignableFrom(t))	return DataType.ConstructorInfo;
				else if (typeof(EventInfo).IsAssignableFrom(t))			return DataType.EventInfo;

				throw new NotSupportedException(string.Format("MemberInfo Type '{0}' is not supported", t));
			}
			else if (typeof(Delegate).IsAssignableFrom(t))
				return DataType.Delegate;
			else if (t == typeof(string))
				return DataType.String;
			else if (t.IsArray)
				return DataType.Array;
			else if (t.IsClass)
				return DataType.Class;
			else if (t.IsValueType)
				return DataType.Struct;

			// Should never happen in theory
			return DataType.Unknown;
		}
	}
}
