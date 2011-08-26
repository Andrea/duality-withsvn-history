using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Duality.Serialization;

namespace Duality
{
	/// <summary>
	/// Provides reflection-related helper methods.
	/// </summary>
	public static class ReflectionHelper
	{
		private	static	Dictionary<Type,SerializeType>	typeCache			= new Dictionary<Type,SerializeType>();
		private	static	Dictionary<string,Type>			typeResolveCache	= new Dictionary<string,Type>();

		/// <summary>
		/// Equals <c>BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic</c>.
		/// </summary>
		public const BindingFlags BindInstanceAll = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		/// <summary>
		/// Equals <c>BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic</c>.
		/// </summary>
		public const BindingFlags BindStaticAll = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		/// <summary>
		/// Equals <c>BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic</c>.
		/// </summary>
		public const BindingFlags BindAll = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		/// <summary>
		/// Returns a Stream to an Assemblies embedded resource.
		/// </summary>
		/// <param name="asm">The Assembly that embeds the desired resource.</param>
		/// <param name="fileName">The name of the desired file.</param>
		/// <returns></returns>
		public static Stream GetEmbeddedResourceStream(Assembly asm, string fileName)
		{
			if (String.IsNullOrEmpty(fileName)) return null;
			string resName = asm.GetName().Name + '.' + fileName.
				Replace(Path.DirectorySeparatorChar, '.').
				Replace(Path.AltDirectorySeparatorChar, '.').
				Trim('.');

			string[] names = asm.GetManifestResourceNames();
			ManifestResourceInfo info = asm.GetManifestResourceInfo(resName);
			if (info == null) return null;

			return asm.GetManifestResourceStream(resName);
		}

		/// <summary>
		/// Creates an instance of a Type.
		/// </summary>
		/// <param name="instanceType">The Type to create an instance of.</param>
		/// <param name="noConstructor">If true, the instance will be generated without invoking any constructor.</param>
		/// <returns>An instance of the Type. Null, if instanciation wasn't possible.</returns>
		public static object CreateInstanceOf(this Type instanceType, bool noConstructor = false)
		{
			try
			{
				if (instanceType == typeof(string))
					return "";
				else if (typeof(Array).IsAssignableFrom(instanceType) && instanceType.GetArrayRank() == 1)
					return Array.CreateInstance(instanceType.GetElementType(), 0);
				else if (noConstructor)
					return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(instanceType);
				else
					return Activator.CreateInstance(instanceType, true);
			}
			catch (Exception)
			{
				return null;
			}
		}
		/// <summary>
		/// Returns the default instance of a Type. Equals <c>default(T)</c>, but works for Reflection.
		/// </summary>
		/// <param name="instanceType">The Type to create a default instance of.</param>
		/// <returns></returns>
		public static object GetDefaultInstanceOf(this Type instanceType)
		{
			if (instanceType.IsValueType)
				return Activator.CreateInstance(instanceType, true);
			else
				return null;
		}

		/// <summary>
		/// Returns whether two MemberInfo objects are equal.
		/// </summary>
		/// <param name="lhs">The first MemberInfo.</param>
		/// <param name="rhs">The second MemberInfo.</param>
		/// <returns></returns>
		public static bool MemberInfoEquals(MemberInfo lhs, MemberInfo rhs)
		{
			if (lhs == rhs)
				return true;
 
			if (lhs.DeclaringType != rhs.DeclaringType)
				return false;
 
			// Methods on arrays do not have metadata tokens but their ReflectedType
			// always equals their DeclaringType
			if (lhs.DeclaringType != null && lhs.DeclaringType.IsArray)
				return false;
 
			if (lhs.MetadataToken != rhs.MetadataToken || lhs.Module != rhs.Module)
				return false;
 
			if (lhs is MethodInfo)
			{
				MethodInfo lhsMethod = lhs as MethodInfo;
 
				if (lhsMethod.IsGenericMethod)
				{
					MethodInfo rhsMethod = rhs as MethodInfo;
 
					Type[] lhsGenArgs = lhsMethod.GetGenericArguments();
					Type[] rhsGenArgs = rhsMethod.GetGenericArguments();
					for (int i = 0; i < rhsGenArgs.Length; i++)
					{
						if (lhsGenArgs[i] != rhsGenArgs[i])
							return false;
					}
				}
			}
			return true;
		}
		/// <summary>
		/// Returns a Types inheritance level. The <c>object</c>-Type has an inheritance level of
		/// zero, each subsequent inheritance increases it by one.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int GetTypeHierarchyLevel(this Type t)
		{
			int level = 0;
			while (t.BaseType != null) { t = t.BaseType; level++; }
			return level;
		}

		/// <summary>
		/// Returns all fields matching the specified bindingflags, even if private and inherited.
		/// </summary>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static FieldInfo[] GetAllFields(this Type type, BindingFlags flags)
		{
			List<FieldInfo> result = new List<FieldInfo>();

			do { result.AddRange(type.GetFields(flags | BindingFlags.DeclaredOnly)); }
			while ((type = type.BaseType) != null);

			return result.ToArray();
		}

		
		/// <summary>
		/// Clears the ReflectionHelpers Type cache.
		/// </summary>
		public static void ClearTypeCache()
		{
			typeCache.Clear();
			typeResolveCache.Clear();
		}
		/// <summary>
		/// Resolves a Type based on its <see cref="GetTypeName">type string</see>.
		/// </summary>
		/// <param name="typeString">The type string to resolve. Needs to be in <see cref="TypeNameFormat.FullNameWithoutAssembly"/> format.</param>
		/// <param name="throwOnError">If true, an Exception is thrown on failure.</param>
		/// <returns></returns>
		public static Type ResolveType(string typeString, bool throwOnError = true)
		{
			Type result;
			if (typeResolveCache.TryGetValue(typeString, out result)) return result;

			Assembly[] searchAsm = AppDomain.CurrentDomain.GetAssemblies().Except(DualityApp.DisposedPlugins).ToArray();
			result = ReflectionHelper.FindTypeByFullNameWithoutAssembly(typeString, searchAsm);
			if (result != null) typeResolveCache[typeString] = result;

			if (result == null && throwOnError) throw new ApplicationException(string.Format("Can't resolve Type '{0}'. Type not found", typeString));
			return result;
		}
		/// <summary>
		/// Returns the <see cref="SerializeType"/> of a Type.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static SerializeType GetSerializeType(this Type t)
		{
			SerializeType result;
			if (typeCache.TryGetValue(t, out result)) return result;

			result = new SerializeType(t);
			typeCache[t] = result;
			typeResolveCache[result.TypeString] = result.Type;
			return result;
		}
		/// <summary>
		/// Returns the <see cref="DataType"/> of a Type.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static DataType GetDataType(this Type t)
		{
			if (t.IsEnum)
				return DataType.Enum;
			else if (t.IsPrimitive)
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

		/// <summary>
		/// Returns a string describing a certain Type.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <param name="attrib">How to describe the Type</param>
		/// <returns></returns>
		public static string GetTypeName(this Type T, TypeNameFormat attrib)
		{
			if (attrib == TypeNameFormat.Keyword)
			{
				return T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.');
			}
			else if (attrib == TypeNameFormat.FullNameWithoutAssembly)
			{
				return Regex.Replace(T.FullName, @"(, [^\]\[]*)", "");
			}
			else if (attrib == TypeNameFormat.CSCodeIdent || attrib == TypeNameFormat.CSCodeIdentShort)
			{
				StringBuilder typeStr = new StringBuilder();

				if (T.IsGenericParameter)
				{
					return T.Name;
				}
				if (T.IsArray)
				{
					typeStr.Append(GetTypeName(T.GetElementType(), attrib));
					typeStr.Append('[');
					typeStr.Append(',', T.GetArrayRank() - 1);
					typeStr.Append(']');
				}
				else
				{
					Type[] genArgs = T.IsGenericType ? T.GetGenericArguments() : null;

					if (T.IsNested)
					{
						Type declType = T.DeclaringType;
						if (declType.IsGenericTypeDefinition)
						{
							Array.Resize(ref genArgs, declType.GetGenericArguments().Length);
							declType = declType.MakeGenericType(genArgs);
							genArgs = T.GetGenericArguments().Skip(genArgs.Length).ToArray();
						}
						string parentName = GetTypeName(declType, attrib);

						string[] nestedNameToken = attrib == TypeNameFormat.CSCodeIdentShort ? T.Name.Split('+') : T.FullName.Split('+');
						string nestedName = nestedNameToken[nestedNameToken.Length - 1];
						
						int genTypeSepIndex = nestedName.IndexOf("[[");
						if (genTypeSepIndex != -1) nestedName = nestedName.Substring(0, genTypeSepIndex);
						genTypeSepIndex = nestedName.IndexOf('`');
						if (genTypeSepIndex != -1) nestedName = nestedName.Substring(0, genTypeSepIndex);

						typeStr.Append(parentName);
						typeStr.Append('.');
						typeStr.Append(nestedName);
					}
					else
					{
						if (attrib == TypeNameFormat.CSCodeIdentShort)
							typeStr.Append(T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.'));
						else
							typeStr.Append(T.FullName.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.'));
					}

					if (genArgs != null && genArgs.Length > 0)
					{
						if (T.IsGenericTypeDefinition)
						{
							typeStr.Append('<');
							typeStr.Append(',', genArgs.Length - 1);
							typeStr.Append('>');
						}
						else if (T.IsGenericType)
						{
							typeStr.Append('<');
							for (int i = 0; i < genArgs.Length; i++)
							{
								typeStr.Append(GetTypeName(genArgs[i], attrib));
								if (i < genArgs.Length - 1)
									typeStr.Append(',');
							}
							typeStr.Append('>');
						}
					}
				}

				return typeStr.Replace('+', '.').ToString();
			}
			return null;
		}
		/// <summary>
		/// Retrieves a Type based on a string in the specified TypeNameFormat.
		/// </summary>
		/// <param name="typeName">The type string to use for the search.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <param name="format">The format of the provided type string.</param>
		/// <returns></returns>
		public static Type FindType(string typeName, IEnumerable<Assembly> asmSearch, TypeNameFormat format = TypeNameFormat.FullNameWithoutAssembly)
		{
			if (format == TypeNameFormat.CSCodeIdent)
				return FindTypeByCSCodeIdent(typeName, asmSearch);
			else if (format == TypeNameFormat.FullNameWithoutAssembly)
				return FindTypeByFullNameWithoutAssembly(typeName, asmSearch);
			else
				throw new NotImplementedException("TypeNameFormat " + format.ToString() + " is not supported.");
		}
		/// <summary>
		/// Retrieves a Type based on a string in the <see cref="TypeNameFormat.CSCodeIdent"/> format.
		/// </summary>
		/// <param name="csCodeType">The type string to use for the search, in <see cref="TypeNameFormat.CSCodeIdent"/> format.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <param name="declaringType">The searched Type's declaring Type.</param>
		/// <returns></returns>
		private static Type FindTypeByCSCodeIdent(string csCodeType, IEnumerable<Assembly> asmSearch, Type declaringType = null)
		{
			csCodeType = csCodeType.Trim();
			
			// Retrieve array ranks
			string[] token = Regex.Split(csCodeType, @"<.+>").Where(s => s.Length > 0).ToArray();
			int arrayRank = 0;
			string elementTypeName = csCodeType;
			if (token.Length > 0)
			{
				MatchCollection arrayMatches = Regex.Matches(token[token.Length - 1], @"\[,*\]");
				if (arrayMatches.Count > 0)
				{
					string rankStr = arrayMatches[arrayMatches.Count - 1].Value;
					arrayRank = 1 + rankStr.Count(c => c == ',');
					elementTypeName = elementTypeName.Substring(0, elementTypeName.Length - rankStr.Length);
				}
			}
			
			// Handle Arrays
			if (arrayRank > 0)
			{
				Type elementType = FindTypeByCSCodeIdent(elementTypeName, asmSearch, declaringType);
				return arrayRank == 1 ? elementType.MakeArrayType() : elementType.MakeArrayType(arrayRank);
			}

			if (csCodeType.IndexOfAny(new char[]{'<','>'}) != -1)
			{
				int first = csCodeType.IndexOf('<');
				int eof = csCodeType.IndexOf('<', first + 1);
				int last = 0;
				while (csCodeType.IndexOf('>', last + 1) > last)
				{
					int cur = csCodeType.IndexOf('>', last + 1);
					if (cur < eof || eof == -1) last = cur;
					else break;
				}
				string[] tokenTemp = new string[3];
				tokenTemp[0] = csCodeType.Substring(0, first);
				tokenTemp[1] = csCodeType.Substring(first + 1, last - (first + 1));
				tokenTemp[2] = csCodeType.Substring(last + 1, csCodeType.Length - (last + 1));
				string[] tokenTemp2 = tokenTemp[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
				
				Type[]	types		= new Type[tokenTemp2.Length];
				Type	mainType	= FindTypeByCSCodeIdent(tokenTemp[0] + '`' + tokenTemp2.Length, asmSearch, declaringType);
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					types[i] = FindTypeByCSCodeIdent(tokenTemp2[i], asmSearch);
				}
				
				// Nested type support
				if (tokenTemp[2].Length > 1 && tokenTemp[2][0] == '.')
					mainType = FindTypeByCSCodeIdent(tokenTemp[2].Substring(1, tokenTemp[2].Length - 1), asmSearch, mainType.MakeGenericType(types));

				if (mainType.IsGenericTypeDefinition)
				{
					if (declaringType != null)
						return mainType.MakeGenericType(declaringType.GetGenericArguments().Concat(types).ToArray());
					else
						return mainType.MakeGenericType(types);
				}
				else
					return mainType;
			}
			else
			{
				if (declaringType == null)
				{
					Type[]	asmTypes;
					string	nameTemp;
					foreach (Assembly asm in asmSearch)
					{
						asmTypes = asm.GetTypes();
						for (int j = 0; j < asmTypes.Length; j++)
						{
							nameTemp = asmTypes[j].FullName.Replace('+', '.');
							if (csCodeType == nameTemp) return asmTypes[j];
						}
					}
				}
				else
				{
					Type[] nestedTypes = declaringType.GetNestedTypes(BindStaticAll);
					string nameTemp;
					for (int j = 0; j < nestedTypes.Length; j++)
					{
						nameTemp = nestedTypes[j].FullName;
						nameTemp = nameTemp.Remove(0, nameTemp.LastIndexOf('+') + 1);
						nameTemp = nameTemp.Replace('+', '.');
						if (csCodeType == nameTemp) return nestedTypes[j];
					}
				}
			}

			return null;
		}
		/// <summary>
		/// Retrieves a Type based on a string in the <see cref="TypeNameFormat.FullNameWithoutAssembly"/> format.
		/// </summary>
		/// <param name="typeName">The type string to use for the search, in <see cref="TypeNameFormat.FullNameWithoutAssembly"/> format.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <returns></returns>
		private static Type FindTypeByFullNameWithoutAssembly(string typeName, IEnumerable<Assembly> asmSearch)
		{
			typeName = typeName.Trim();

			// Retrieve generic parameters
			Match genericParamsMatch = Regex.Match(typeName, @"(\[)(\[.+\])(\])");
			string[] genericParams = null;
			if (genericParamsMatch.Groups.Count > 3)
				genericParams = genericParamsMatch.Groups[2].Value.Split(',').Select(s => s.Substring(1, s.Length - 2)).ToArray();

			// Process type name
			string[] token = Regex.Split(typeName, @"\[\[.+\]\]");
			string typeNameBase = token[0];
			string elementTypeName = typeName;

			// Retrieve array ranks
			int arrayRank = 0;
			if (token.Length > 1)
			{
				MatchCollection arrayMatches = Regex.Matches(token[1], @"\[,*\]");
				if (arrayMatches.Count > 0)
				{
					string rankStr = arrayMatches[arrayMatches.Count - 1].Value;
					arrayRank = 1 + rankStr.Count(c => c == ',');
					elementTypeName = elementTypeName.Substring(0, elementTypeName.Length - rankStr.Length);
				}
			}
			
			// Handle Arrays
			if (arrayRank > 0)
			{
				Type elementType = FindTypeByFullNameWithoutAssembly(elementTypeName, asmSearch);
				if (elementType == null) return null;
				return arrayRank == 1 ? elementType.MakeArrayType() : elementType.MakeArrayType(arrayRank);
			}

			// Retrieve base type
			Type baseType = null;
			foreach (Assembly a in asmSearch)
			{
				baseType = a.GetType(typeNameBase);
				if (baseType != null) break;
			}
			
			// Retrieve generic type params
			if (genericParams != null)
			{
				Type[] genericParamTypes = new Type[genericParams.Length];
				for (int i = 0; i < genericParamTypes.Length; i++)
				{
					genericParamTypes[i] = FindTypeByFullNameWithoutAssembly(genericParams[i], asmSearch);
					if (genericParamTypes[i] == null) return null;
				}
				if (baseType == null) return null;
				return baseType.MakeGenericType(genericParamTypes);
			}

			return baseType;
		}
	}

	/// <summary>
	/// An enumeration of possible formats in which Type data can be displayed in a string.
	/// </summary>
	public enum TypeNameFormat
	{
		/// <summary>
		/// A type keyword, its "short" name. Just the types "base", no generic
		/// type parameters or array specifications.
		/// </summary>
		Keyword,
		/// <summary>
		/// Exactly the same as a Types FullName, but without any Assembly names, versions, keys, etc.
		/// </summary>
		FullNameWithoutAssembly,
		/// <summary>
		/// A type name / definition as you would see it in normal C# code. Complete with generic parameters
		/// or possible array specifications.
		/// </summary>
		CSCodeIdent,
		/// <summary>
		/// As CSCodeIdent, but shortened to Keywords
		/// </summary>
		CSCodeIdentShort
	}
}
