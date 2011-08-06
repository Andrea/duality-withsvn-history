using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Duality
{
	public static class ReflectionHelper
	{
		public const BindingFlags BindInstanceAll = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		public const BindingFlags BindStaticAll = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

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

		public static object CreateInstanceOf(Type instanceType, bool noConstructor = false)
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
		public static int GetTypeHierarchyLevel(Type t)
		{
			int level = 0;
			while (t.BaseType != null) { t = t.BaseType; level++; }
			return level;
		}

		/// <summary>
		/// A configuration enum for GetTypeString
		/// </summary>
		public enum TypeStringAttrib
		{
			/// <summary>
			/// The method will return a type keyword, its "short" name. Just the types "base", no generic
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
		/// <summary>
		/// Returns a string describing a certain Type.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <param name="attrib">How to describe the Type</param>
		/// <returns></returns>
		public static string GetTypeString(Type T, TypeStringAttrib attrib)
		{
			if (attrib == TypeStringAttrib.Keyword)
			{
				return T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.');
			}
			else if (attrib == TypeStringAttrib.FullNameWithoutAssembly)
			{
				return System.Text.RegularExpressions.Regex.Replace(T.FullName, @"(\[[^,]*?)(,[^\]]*?)(\])", "$1$3");
			}
			else if (attrib == TypeStringAttrib.CSCodeIdent || attrib == TypeStringAttrib.CSCodeIdentShort)
			{
				StringBuilder typeStr = new StringBuilder();

				if (T.IsArray)
				{
					typeStr.Append(GetTypeString(T.GetElementType(), attrib));
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
						string parentName = GetTypeString(declType, attrib);

						string[] nestedNameToken = attrib == TypeStringAttrib.CSCodeIdentShort ? T.Name.Split('+') : T.FullName.Split('+');
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
						if (attrib == TypeStringAttrib.CSCodeIdentShort)
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
								typeStr.Append(GetTypeString(genArgs[i], attrib));
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
		/// Searches a specific type (specified as would be valid in C# code) in an array of Assemblies.
		/// Generates the type if neccessary (generic). Also supports generic types combined using types 
		/// from different Assemblies.
		/// </summary>
		public static Type FindTypeByCSCodeIdent(string csCodeType, Assembly[] asmSearch, Type declaringType = null)
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
					for (int i = 0; i < asmSearch.Length; i++)
					{
						asmTypes = asmSearch[i].GetTypes();
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
		/// Searches a specific type (specified using FullNameWithoutAssembly) in an array of Assemblies.
		/// Generates the type if neccessary (generic). Also supports generic types combined using types 
		/// from different Assemblies.
		/// </summary>
		public static Type FindTypeByFullNameWithoutAssembly(string typeName, Assembly[] asmSearch)
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
}
