using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;

namespace Duality
{
	public static class ReflectionHelper
	{
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

		public static object CreateInstanceOf(Type instanceType)
		{
			try
			{
				if (instanceType == typeof(string))
					return "";
				else if (typeof(Array).IsAssignableFrom(instanceType))
					return Array.CreateInstance(instanceType.GetElementType(), 0);
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
			/// The types full name, but without generic arguments, element types or assembly names
			/// </summary>
			LongName,
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
			if (attrib == TypeStringAttrib.LongName)
			{
				return T.FullName.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			else if (attrib == TypeStringAttrib.Keyword)
			{
				return T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.');
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
					typeStr.Append(GetTypeString(T, attrib == TypeStringAttrib.CSCodeIdentShort ? TypeStringAttrib.Keyword : TypeStringAttrib.LongName));
					
					if (T.IsGenericTypeDefinition)
					{
						typeStr.Append('<');
						Type[] genArg = T.GetGenericArguments();
						typeStr.Append(',', genArg.Length - 1);
						typeStr.Append('>');
					}
					else if (T.IsGenericType)
					{
						typeStr.Append('<');
						Type[] genArg = T.GetGenericArguments();
						for (int i = 0; i < genArg.Length; i++)
						{
							typeStr.Append(GetTypeString(genArg[i], attrib));
							if (i < genArg.Length - 1)
								typeStr.Append(',');
						}
						typeStr.Append('>');
					}
				}

				return typeStr.Replace('+', '.').ToString();
			}
			return null;
		}
		/// <summary>
		/// Converts a full name type string from CS code style to GetType compatible. Warning:
		/// As GetType only searches in a specific assembly, type identifiers combined out of types from
		/// different Assemblies won't be found.
		/// </summary>
		/// <param name="csCodeType"></param>
		/// <returns></returns>
		public static string ConvertTypeCodeToGetType(string csCodeType)
		{
			if (csCodeType.IndexOfAny(new char[]{'<','>'}) != -1)
			{
				int first = csCodeType.IndexOf('<');
				int last = 0;
				while (csCodeType.IndexOf('>', last + 1) > last)
				{
					last = csCodeType.IndexOf('>', last + 1);
				}
				string[] tokenTemp = new string[3];
				tokenTemp[0] = csCodeType.Substring(0, first);
				tokenTemp[1] = csCodeType.Substring(first + 1, last - (first + 1));
				tokenTemp[2] = csCodeType.Substring(last + 1, csCodeType.Length - (last + 1));
				string[] tokenTemp2 = tokenTemp[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					tokenTemp2[i] = ConvertTypeCodeToGetType(tokenTemp2[i]);
				}
				return tokenTemp[0] + '`' + tokenTemp2.Length.ToString() + '[' + String.Join(", ", tokenTemp2) + ']' + ((tokenTemp.Length > 2) ? tokenTemp[2] : "");
			}
			else
				return csCodeType;
		}
		/// <summary>
		/// Searches a specific type (specified as would be valid in C# code) in an array of Assemblies.
		/// Generates the type if neccessary (generic). Also supports generic types combined using types 
		/// from different Assemblies.
		/// </summary>
		/// <param name="csCodeType"></param>
		/// <param name="asmSearch"></param>
		/// <returns></returns>
		public static Type FindType(string csCodeType, Assembly[] asmSearch)
		{
			Type result = null;
			csCodeType = csCodeType.Trim();
			
			bool		array	= (csCodeType.IndexOfAny(new char[]{'[',']'}) != -1);
			string[]	arrRank	= null;
			if (array)
			{
				int first = 0;
				while (csCodeType.IndexOf('[', first + 1) > first)
				{
					first = csCodeType.IndexOf('[', first + 1);
				}
				int last = 0;
				while (csCodeType.IndexOf(']', last + 1) > last)
				{
					last = csCodeType.IndexOf(']', last + 1);
				}

				if (csCodeType.IndexOf('>', first + 1) == -1)
				{
					arrRank		= csCodeType.Substring(first + 1, last - (first + 1)).Split(new char[] {','}, StringSplitOptions.None);
					csCodeType	= csCodeType.TrimEnd('[', ']');
				}
				else
				{
					array	= false;
					arrRank	= null;
				}
			}

			if (csCodeType.IndexOfAny(new char[]{'<','>'}) != -1)
			{
				int first = csCodeType.IndexOf('<');
				int last = 0;
				while (csCodeType.IndexOf('>', last + 1) > last)
				{
					last = csCodeType.IndexOf('>', last + 1);
				}
				string[] tokenTemp = new string[3];
				tokenTemp[0] = csCodeType.Substring(0, first);
				tokenTemp[1] = csCodeType.Substring(first + 1, last - (first + 1));
				tokenTemp[2] = csCodeType.Substring(last + 1, csCodeType.Length - (last + 1));
				string[] tokenTemp2 = tokenTemp[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
				
				Type[]	types		= new Type[tokenTemp2.Length];
				Type	mainType	= FindType(tokenTemp[0] + '`' + tokenTemp2.Length, asmSearch);;
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					types[i] = FindType(tokenTemp2[i], asmSearch);
				}

				result = mainType.MakeGenericType(types);
			}
			else
			{
				Type[]	asmTypes;
				string	nameTemp;
				for (int i = 0; i < asmSearch.Length; i++)
				{
					asmTypes = asmSearch[i].GetTypes();
					for (int j = 0; j < asmTypes.Length; j++)
					{
						nameTemp = asmTypes[j].FullName.Replace('+', '.');
						if (csCodeType == nameTemp)
						{
							result = asmTypes[j];
							break;
						}
					}
					if (result != null) break;
				}
			}

			if (result == null)
				return null;
			else if (array && arrRank.Length != 1)
				return result.MakeArrayType(arrRank.Length);
			else if (array)
				return result.MakeArrayType();
			else
				return result;
		}
	}
}
