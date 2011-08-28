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
		private	static	Dictionary<string,MemberInfo>	memberResolveCache	= new Dictionary<string,MemberInfo>();

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
			memberResolveCache.Clear();
		}
		/// <summary>
		/// Resolves a Type based on its <see cref="GetTypeId">type id</see>.
		/// </summary>
		/// <param name="typeString">The type string to resolve.</param>
		/// <param name="throwOnError">If true, an Exception is thrown on failure.</param>
		/// <param name="declaringMethod">The generic method that is declaring the Type. Only necessary when resolving a generic methods parameter Type.</param>
		/// <returns></returns>
		public static Type ResolveType(string typeString, bool throwOnError = true, MethodInfo declaringMethod = null)
		{
			Type result;
			if (typeResolveCache.TryGetValue(typeString, out result)) return result;

			Assembly[] searchAsm = AppDomain.CurrentDomain.GetAssemblies().Except(DualityApp.DisposedPlugins).ToArray();
			result = ReflectionHelper.FindType(typeString, searchAsm, declaringMethod);
			if (result != null && declaringMethod == null) typeResolveCache[typeString] = result;

			if (result == null && throwOnError) throw new ApplicationException(string.Format("Can't resolve Type '{0}'. Type not found", typeString));
			return result;
		}
		/// <summary>
		/// Resolves a Member based on its <see cref="GetMemberId">member id</see>.
		/// </summary>
		/// <param name="memberString">The <see cref="GetMemberId">member id</see> of the member.</param>
		/// <param name="throwOnError">If true, an Exception is thrown on failure.</param>
		/// <returns></returns>
		public static MemberInfo ResolveMember(string memberString, bool throwOnError = true)
		{
			MemberInfo result;
			if (memberResolveCache.TryGetValue(memberString, out result)) return result;

			Assembly[] searchAsm = AppDomain.CurrentDomain.GetAssemblies().Except(DualityApp.DisposedPlugins).ToArray();
			result = ReflectionHelper.FindMember(memberString, searchAsm);
			if (result != null) memberResolveCache[memberString] = result;

			if (result == null && throwOnError) throw new ApplicationException(string.Format("Can't resolve MemberInfo '{0}'. Member not found", memberString));
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
		/// Returns a Types keyword, its "short" name. Just the types "base", no generic
		/// type parameters or array specifications.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <returns></returns>
		public static string GetTypeKeyword(this Type T)
		{
			return T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.');
		}
		/// <summary>
		/// Returns a string describing a certain Type.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <returns></returns>
		public static string GetTypeCSCodeName(this Type T, bool shortName = false)
		{
			StringBuilder typeStr = new StringBuilder();

			if (T.IsGenericParameter)
			{
				return T.Name;
			}
			if (T.IsArray)
			{
				typeStr.Append(GetTypeCSCodeName(T.GetElementType(), shortName));
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
					string parentName = GetTypeCSCodeName(declType, shortName);

					string[] nestedNameToken = shortName ? T.Name.Split('+') : T.FullName.Split('+');
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
					if (shortName)
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
							typeStr.Append(GetTypeCSCodeName(genArgs[i], shortName));
							if (i < genArgs.Length - 1)
								typeStr.Append(',');
						}
						typeStr.Append('>');
					}
				}
			}

			return typeStr.Replace('+', '.').ToString();
		}
		/// <summary>
		/// Returns a string describing a certain Type.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <returns></returns>
		public static string GetTypeId(this Type T)
		{
			return T.FullName != null ? Regex.Replace(T.FullName, @"(, [^\]\[]*)", "") : T.Name;
		}
		/// <summary>
		/// Returns a string describing a certain Member of a Type.
		/// </summary>
		/// <param name="member">The Member to describe.</param>
		/// <returns></returns>
		public static string GetMemberId(this MemberInfo member)
		{
			if (member is Type) return "T:" + GetTypeId(member as Type);
			string declType = member.DeclaringType.GetTypeId();

			FieldInfo field = member as FieldInfo;
			if (field != null) return "F:" + declType + ':' + field.Name;

			EventInfo ev = member as EventInfo;
			if (ev != null) return "E:" + declType + ':' + ev.Name;

			PropertyInfo property = member as PropertyInfo;
			if (property != null)
			{
				ParameterInfo[] parameters = property.GetIndexParameters();
				if (parameters.Length == 0)
					return "P:" + declType + ':' + property.Name;
				else
					return "P:" + declType + ':' + property.Name + '(' + parameters.ToString(p => p.ParameterType.GetTypeId(), ",") + ')';
			}

			MethodInfo method = member as MethodInfo;
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();
				Type[] genArgs = method.GetGenericArguments();

				string result = "M:" + declType + ':' + method.Name;
				
				if (genArgs.Length != 0)
				{
					if (method.IsGenericMethodDefinition)
						result += "``" + genArgs.Length.ToString();
					else
						result += "``" + genArgs.Length.ToString() + '[' + genArgs.ToString(t => "[" + t.GetTypeId() + "]", ",") + ']';
				}
				if (parameters.Length != 0)
					result += '(' + parameters.ToString(p => p.ParameterType.GetTypeId(), ",") + ')';

				return result;
			}

			ConstructorInfo ctor = member as ConstructorInfo;
			if (ctor != null)
			{
				ParameterInfo[] parameters = ctor.GetParameters();

				string result = "C:" + declType + ':' + (ctor.IsStatic ? "s" : "i");

				if (parameters.Length != 0)
					result += '(' + parameters.ToString(p => p.ParameterType.GetTypeId(), ", ") + ')';

				return result;
			}

			throw new NotSupportedException(string.Format("Member Type '{0} not supported", Log.Type(member.GetType())));
		}
		/// <summary>
		/// Retrieves a Type based on a C# code type string.
		/// </summary>
		/// <param name="csCodeType">The type string to use for the search.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <param name="declaringType">The searched Type's declaring Type.</param>
		/// <returns></returns>
		private static Type FindTypeByCSCode(string csCodeType, IEnumerable<Assembly> asmSearch, Type declaringType = null)
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
				Type elementType = FindTypeByCSCode(elementTypeName, asmSearch, declaringType);
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
				Type	mainType	= FindTypeByCSCode(tokenTemp[0] + '`' + tokenTemp2.Length, asmSearch, declaringType);
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					types[i] = FindTypeByCSCode(tokenTemp2[i], asmSearch);
				}
				
				// Nested type support
				if (tokenTemp[2].Length > 1 && tokenTemp[2][0] == '.')
					mainType = FindTypeByCSCode(tokenTemp[2].Substring(1, tokenTemp[2].Length - 1), asmSearch, mainType.MakeGenericType(types));

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
		/// Retrieves a Type based on a <see cref="GetTypeId">type id</see> string.
		/// </summary>
		/// <param name="typeName">The type id to use for the search.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <param name="declaringMethod">The generic method that is declaring the Type. Only necessary when resolving a generic methods parameter Type.</param>
		/// <returns></returns>
		private static Type FindType(string typeName, IEnumerable<Assembly> asmSearch, MethodInfo declaringMethod = null)
		{
			typeName = typeName.Trim();

			// Retrieve generic parameters
			Match genericParamsMatch = Regex.Match(typeName, @"(\[)(\[.+\])(\])");
			string[] genericParams = null;
			if (genericParamsMatch.Groups.Count > 3)
			{
				string genericParamList = genericParamsMatch.Groups[2].Value;
				genericParams = SplitArgs(genericParamList, '[', ']', ',', 0);
				for (int i = 0; i < genericParams.Length; i++)
					genericParams[i] = genericParams[i].Substring(1, genericParams[i].Length - 2);
			}

			// Process type name
			string[] token = Regex.Split(typeName, @"\[\[.+\]\]");
			string typeNameBase = token[0];
			string elementTypeName = typeName;

			// Handle Reference
			if (token[token.Length - 1].LastOrDefault() == '&')
			{
				Type elementType = FindType(typeName.Substring(0, typeName.Length - 1), asmSearch, declaringMethod);
				if (elementType == null) return null;
				return elementType.MakeByRefType();
			}

			// Retrieve array ranks
			int arrayRank = 0;
			MatchCollection arrayMatches = Regex.Matches(token[token.Length - 1], @"\[,*\]");
			if (arrayMatches.Count > 0)
			{
				string rankStr = arrayMatches[arrayMatches.Count - 1].Value;
				arrayRank = 1 + rankStr.Count(c => c == ',');
				elementTypeName = elementTypeName.Substring(0, elementTypeName.Length - rankStr.Length);
			}
			
			// Handle Arrays
			if (arrayRank > 0)
			{
				Type elementType = FindType(elementTypeName, asmSearch, declaringMethod);
				if (elementType == null) return null;
				return arrayRank == 1 ? elementType.MakeArrayType() : elementType.MakeArrayType(arrayRank);
			}

			Type baseType = null;
			// Generic method parameter Types
			if (typeNameBase.StartsWith("``"))
			{
				if (declaringMethod != null)
				{
					int methodGenArgIndex = int.Parse(typeNameBase.Substring(2, typeNameBase.Length - 2));
					baseType = declaringMethod.GetGenericArguments()[methodGenArgIndex];
				}
			}
			else
			{
				// Retrieve base type
				foreach (Assembly a in asmSearch)
				{
					baseType = a.GetType(typeNameBase);
					if (baseType != null) break;
				}
				// Failed to retrieve base type? Try manually and ignore plus / dot difference.
				if (baseType == null)
				{
					foreach (Assembly a in asmSearch)
					{
						foreach (Type t in a.GetTypes())
						{
							string nameTemp = t.FullName.Replace('+', '.');
							if (typeNameBase == nameTemp)
							{
								baseType = t;
								break;
							}
						}
						if (baseType != null) break;
					}
				}
			}
			
			// Retrieve generic type params
			if (genericParams != null)
			{
				Type[] genericParamTypes = new Type[genericParams.Length];

				for (int i = 0; i < genericParamTypes.Length; i++)
				{
					// Explicitly referring to generic type definition params: Don't attemp to make it generic.
					if ((genericParams[i].Length > 0 && genericParams[i][0] == '`') && 
						(genericParams[i].Length < 2 || genericParams[i][1] != '`')) return baseType;

					genericParamTypes[i] = FindType(genericParams[i], asmSearch, declaringMethod);

					// Can't find the generic type argument: Fail.
					if (genericParamTypes[i] == null) return null;
				}

				if (baseType == null)
					return null;
				else
					return baseType.MakeGenericType(genericParamTypes);
			}

			return baseType;
		}
		/// <summary>
		/// Retrieves a MemberInfo based on a <see cref="GetMemberId">member id</see>.
		/// </summary>
		/// <param name="typeName">The member string to use for the search.</param>
		/// <param name="asmSearch">An enumeration of all Assemblies the searched Type could be located in.</param>
		/// <returns></returns>
		/// <seealso cref="GetMemberId(MemberInfo)"/>
		private static MemberInfo FindMember(string memberString, IEnumerable<Assembly> asmSearch)
		{
			string[] token = memberString.Split(':');

			MemberTypes memberType;
			switch (token[0][0])
			{
				case 'T':	memberType = MemberTypes.TypeInfo;		break;
				case 'M':	memberType = MemberTypes.Method;		break;
				case 'F':	memberType = MemberTypes.Field;			break;
				case 'E':	memberType = MemberTypes.Event;			break;
				case 'C':	memberType = MemberTypes.Constructor;	break;
				case 'P':	memberType = MemberTypes.Property;		break;
				default:	throw new NotSupportedException(string.Format("Member Type '{0}' unknown or not supported", token[0][0]));
			}

			Type declaringType = FindType(token[1], asmSearch);
			if (memberType == MemberTypes.TypeInfo) return declaringType;
			if (declaringType == null) return null;

			if (memberType == MemberTypes.Field)
				return declaringType.GetField(token[2], BindAll);
			else if (memberType == MemberTypes.Event)
				return declaringType.GetEvent(token[2], BindAll);

			int memberParamListStartIndex = token[2].IndexOf('(');
			int memberParamListEndIndex = token[2].IndexOf(')');
			string memberParamList = memberParamListStartIndex != -1 ? token[2].Substring(memberParamListStartIndex + 1, memberParamListEndIndex - memberParamListStartIndex - 1) : null;
			string[] memberParams = SplitArgs(memberParamList, '[', ']', ',', 0);
			string memberName = memberParamListStartIndex != -1 ? token[2].Substring(0, memberParamListStartIndex) : token[2];
			Type[] memberParamTypes = memberParams.Select(p => FindType(p, asmSearch)).ToArray();

			if (memberType == MemberTypes.Constructor)
			{
				ConstructorInfo[] availCtors = declaringType.GetConstructors(memberName == "s" ? BindStaticAll : BindInstanceAll).Where(
					m => m.GetParameters().Length == memberParams.Length).ToArray();
				foreach (ConstructorInfo ctor in availCtors)
				{
					bool possibleMatch = true;
					ParameterInfo[] methodParams = ctor.GetParameters();
					for (int i = 0; i < methodParams.Length; i++)
					{
						string methodParamTypeName = methodParams[i].ParameterType.Name;
						if (methodParams[i].ParameterType != memberParamTypes[i] && methodParamTypeName != memberParams[i])
						{
							possibleMatch = false;
							break;
						}
					}
					if (possibleMatch) return ctor;
				}
			}
			else if (memberType == MemberTypes.Property)
			{
				PropertyInfo[] availProps = declaringType.GetProperties(BindAll).Where(
					m => m.Name == memberName && 
					m.GetIndexParameters().Length == memberParams.Length).ToArray();
				foreach (PropertyInfo prop in availProps)
				{
					bool possibleMatch = true;
					ParameterInfo[] methodParams = prop.GetIndexParameters();
					for (int i = 0; i < methodParams.Length; i++)
					{
						string methodParamTypeName = methodParams[i].ParameterType.Name;
						if (methodParams[i].ParameterType != memberParamTypes[i] && methodParamTypeName != memberParams[i])
						{
							possibleMatch = false;
							break;
						}
					}
					if (possibleMatch) return prop;
				}
			}

			int genArgTokenStartIndex = token[2].IndexOf("``");
			int genArgTokenEndIndex = memberParamListStartIndex != -1 ? memberParamListStartIndex : token[2].Length;
			string genArgToken = genArgTokenStartIndex != -1 ? token[2].Substring(genArgTokenStartIndex + 2, genArgTokenEndIndex - genArgTokenStartIndex - 2) : "";
			if (genArgTokenStartIndex != -1) memberName = token[2].Substring(0, genArgTokenStartIndex);			

			int genArgListStartIndex = genArgToken.IndexOf('[');
			int genArgListEndIndex = genArgToken.LastIndexOf(']');
			string genArgList = genArgListStartIndex != -1 ? genArgToken.Substring(genArgListStartIndex + 1, genArgListEndIndex - genArgListStartIndex - 1) : null;
			string[] genArgs = SplitArgs(genArgList, '[', ']', ',', 0);
			for (int i = 0; i < genArgs.Length; i++) genArgs[i] = genArgs[i].Substring(1, genArgs[i].Length - 2);

			int genArgCount = genArgToken.Length > 0 ? int.Parse(genArgToken.Substring(0, genArgListStartIndex != -1 ? genArgListStartIndex : genArgToken.Length)) : 0;

			// Select the method that fits
			MethodInfo[] availMethods = declaringType.GetMethods(ReflectionHelper.BindAll).Where(
				m => m.Name == memberName && 
				m.GetGenericArguments().Length == genArgCount &&
				m.GetParameters().Length == memberParams.Length).ToArray();
			foreach (MethodInfo method in availMethods)
			{
				bool possibleMatch = true;
				ParameterInfo[] methodParams = method.GetParameters();
				for (int i = 0; i < methodParams.Length; i++)
				{
					string methodParamTypeName = methodParams[i].ParameterType.Name;
					if (methodParams[i].ParameterType != memberParamTypes[i] && methodParamTypeName != memberParams[i])
					{
						possibleMatch = false;
						break;
					}
				}
				if (possibleMatch) return method;
			}

			return null;
		}

		/// <summary>
		/// Performs a selective split operation on the specified string. Intended to be used on hierarchial argument lists.
		/// </summary>
		/// <param name="argList">The argument list to split.</param>
		/// <param name="pushLevel">The char that increases the current hierarchy level.</param>
		/// <param name="popLevel">The char that decreases the current hierarchy level.</param>
		/// <param name="separator">The char that separates two arguments.</param>
		/// <param name="splitLevel">The hierarchy level at which to perform the split operation.</param>
		/// <returns></returns>
		public static string[] SplitArgs(string argList, char pushLevel, char popLevel, char separator, int splitLevel)
		{
			if (argList == null) return new string[0];

			// Splitting the parameter list without destroying generic arguments
			int lastSplitIndex = -1;
			int genArgLevel = 0;
			List<string> ptm = new List<string>();
			for (int i = 0; i < argList.Length; i++)
			{
				if (argList[i] == separator && genArgLevel == splitLevel)
				{
					ptm.Add(argList.Substring(lastSplitIndex + 1, i - lastSplitIndex - 1));
					lastSplitIndex = i;
				}
				else if (argList[i] == pushLevel)
					genArgLevel++;
				else if (argList[i] == popLevel)
					genArgLevel--;
			}
			ptm.Add(argList.Substring(lastSplitIndex + 1, argList.Length - lastSplitIndex - 1));
			return ptm.ToArray();
		}
	}
}
