using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;

using Duality.Components;
using Duality.Components.Renderers;

namespace Duality
{
	public static class ReflectionHelper
	{
		public static readonly PropertyInfo Property_GameObject_Name;
		public static readonly PropertyInfo Property_GameObject_Active;
		public static readonly PropertyInfo Property_GameObject_ActiveSingle;
		public static readonly PropertyInfo Property_GameObject_Parent;
		public static readonly PropertyInfo Property_GameObject_PrefabLink;

		public static readonly PropertyInfo Property_Component_GameObj;
		public static readonly PropertyInfo Property_Component_Active;
		public static readonly PropertyInfo Property_Component_ActiveSingle;
		public static readonly PropertyInfo Property_Component_TypeName;

		public static readonly PropertyInfo	Property_Transform_RelativePos;
		public static readonly PropertyInfo	Property_Transform_RelativeAngle;
		public static readonly PropertyInfo	Property_Transform_RelativeScale;
		public static readonly PropertyInfo	Property_Transform_RelativeVel;
		public static readonly PropertyInfo	Property_Transform_RelativeAngleVel;

		public static readonly PropertyInfo	Property_Renderer_VisibilityGroup;

		public static readonly PropertyInfo	Property_SpriteRenderer_BoundRadius;

		public static readonly PropertyInfo	Property_Camera_OrthoAbs;
		public static readonly PropertyInfo	Property_Camera_ViewportAbs;
		public static readonly PropertyInfo	Property_Camera_DrawDevice;
		public static readonly PropertyInfo	Property_Camera_TargetSize;
		public static readonly PropertyInfo	Property_Camera_VisibilityMask;
		

		public static readonly FieldInfo Field_GameObject_Name;
		public static readonly FieldInfo Field_GameObject_PrefabLink;

		public static readonly FieldInfo Field_Transform_Pos;
		public static readonly FieldInfo Field_Transform_Angle;
		public static readonly FieldInfo Field_Transform_Scale;


		static ReflectionHelper()
		{
			// Retrieve PropertyInfo data
			Type gameobject = typeof(GameObject);
			Property_GameObject_Name			= gameobject.GetProperty("Name");
			Property_GameObject_Active			= gameobject.GetProperty("Active");
			Property_GameObject_ActiveSingle	= gameobject.GetProperty("ActiveSingle");
			Property_GameObject_Parent			= gameobject.GetProperty("Parent");
			Property_GameObject_PrefabLink		= gameobject.GetProperty("PrefabLink");

			Type component = typeof(Component);
			Property_Component_GameObj		= component.GetProperty("GameObj");
			Property_Component_Active		= component.GetProperty("Active");
			Property_Component_ActiveSingle	= component.GetProperty("ActiveSingle");
			Property_Component_TypeName		= component.GetProperty("TypeName");

			Type transform = typeof(Transform);
			Property_Transform_RelativePos		= transform.GetProperty("RelativePos");
			Property_Transform_RelativeAngle	= transform.GetProperty("RelativeAngle");
			Property_Transform_RelativeScale	= transform.GetProperty("RelativeScale");
			Property_Transform_RelativeVel		= transform.GetProperty("RelativeVel");
			Property_Transform_RelativeAngleVel	= transform.GetProperty("RelativeAngleVel");

			Type renderer = typeof(Renderer);
			Property_Renderer_VisibilityGroup	= renderer.GetProperty("VisibilityGroup");
			
			Type rendererSprite = typeof(SpriteRenderer);
			Property_SpriteRenderer_BoundRadius	= rendererSprite.GetProperty("BoundRadius");

			Type camera = typeof(Camera);
			Property_Camera_OrthoAbs		= camera.GetProperty("OrthoAbs");
			Property_Camera_ViewportAbs		= camera.GetProperty("ViewportAbs");
			Property_Camera_DrawDevice		= camera.GetProperty("DrawDevice");
			Property_Camera_TargetSize		= camera.GetProperty("TargetSize");
			Property_Camera_VisibilityMask	= camera.GetProperty("VisibilityMask");

			// Retrieve FieldInfo data
			BindingFlags fieldFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
			Field_GameObject_Name		= gameobject.GetField("name", fieldFlags);
			Field_GameObject_PrefabLink	= gameobject.GetField("prefabLink", fieldFlags);

			Field_Transform_Pos		= transform.GetField("pos", fieldFlags);
			Field_Transform_Angle	= transform.GetField("angle", fieldFlags);
			Field_Transform_Scale	= transform.GetField("scale", fieldFlags);
		}


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
						nameTemp = asmTypes[j].FullName;
						if (csCodeType == nameTemp || 
							csCodeType == nameTemp.Replace('+', '.'))
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

	public static class SerializationHelper
	{
		/// <summary>
		/// Returns whether the specified type may just be assigned in a clone operation (even if deep)
		/// instead of being investigated further.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static bool IsSafeAssignType(Type t)
		{
			return t.IsPrimitive || t.IsEnum || t == typeof(string) || typeof(MemberInfo).IsAssignableFrom(t);
		}

		public static T DeepCloneObject<T>(T instance)
		{
			return (T)DeepCloneObject(instance, new VisitedGraph());
		}
		public static object DeepCloneObject(object instance)
		{
			return DeepCloneObject(instance, new VisitedGraph());
		}
		public static T DeepCloneObjectExplicit<T>(T instance, params Type[] unwrapTypes)
		{
			return (T)DeepCloneObjectExplicit(instance, new VisitedGraph(), unwrapTypes);
		}
		public static object DeepCloneObjectExplicit(object instance, params Type[] unwrapTypes)
		{
			return DeepCloneObjectExplicit(instance, new VisitedGraph(), unwrapTypes);
		}

		public static void DeepCopyFields(FieldInfo[] fields, object source, object target)
		{
			DeepCopyFields(fields, source, target, new VisitedGraph());
		}
		public static void DeepCopyFieldsExplicit(FieldInfo[] fields, object source, object target, params Type[] unwrapTypes)
		{
			DeepCopyFieldsExplicit(fields, source, target, new VisitedGraph(), unwrapTypes);
		}

		/// <summary>
		/// Resets all references of object types assignable to any of the specified. typeof(Component) will
		/// result in all references to any kind of Component to be cleared / set null.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="resetTypes"></param>
		public static void DeepResetReferences(object instance, params Type[] resetTypes)
		{
			DeepResetReferenceFields(
				instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
				instance, new HashSet<object>(), resetTypes);
		}
		public static void DeepResetReferenceFields(FieldInfo[] fields, object source, params Type[] resetTypes)
		{
			DeepResetReferenceFields(fields, source, new HashSet<object>(), resetTypes);
		}

		/// <summary>
		/// Re-resolves all MemberInfo references using current Type information including Plugin data. When reloading
		/// Plugins, calling this method for an object will re-map its previously reflected MemberInfo references to
		/// the newly loaded plugin Assemblies equivalent
		/// </summary>
		/// <param name="instance"></param>
		public static void DeepResolveTypeReferences(object instance, SerializationBinder binder)
		{
			DeepResolveTypeReferenceFields(
				instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
				instance, binder, new HashSet<object>());
		}
		public static void DeepResolveTypeReferenceFields(FieldInfo[] fields, object source, SerializationBinder binder)
		{
			DeepResolveTypeReferenceFields(fields, source, binder, new HashSet<object>());
		}


		#region Private Methods
		private class VisitedGraph : Dictionary<object, object>
		{
			public new bool ContainsKey(object key)
			{
				if (key == null)
					return true;
				return base.ContainsKey(key);
			}
			public new object this[object key]
			{
				get { if (key == null) return null; return base[key]; }
			}
		}

		private static object DeepCloneObject(object instance, VisitedGraph visited)
		{
			if (instance == null) return null;
			if (visited.ContainsKey(instance)) return visited[instance];
			Type instanceType = instance.GetType();

			// Primitive types or anything else we don't want to clone
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Arrays
			else if (instanceType.IsArray)
			{
				int length = ((Array)instance).Length;
				Array copy = (Array)Activator.CreateInstance(instanceType, length);
				visited.Add(instance, copy);

				for (int i = 0; i < length; ++i)
					copy.SetValue(DeepCloneObject(((Array)instance).GetValue(i), visited), i);

				return copy;
			}
			// Reference types / complex objects
			else
			{
				object copy = ReflectionHelper.CreateInstanceOf(instanceType);
				if (instanceType.IsClass) visited.Add(instance, copy);

				DeepCopyFields(
					instanceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
					instance, copy, visited);

				return copy;
			}
		}
		private static object DeepCloneObjectExplicit(object instance, VisitedGraph visited, Type[] unwrapTypes)
		{
			if (instance == null) return null;
			if (visited.ContainsKey(instance)) return visited[instance];
			Type instanceType = instance.GetType();

			// Primitive types or anything else we don't want to clone
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Arrays
			else if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Array copy = (Array)Activator.CreateInstance(instanceType, src.Length);
				Type elemType = instanceType.GetElementType();
				visited.Add(instance, copy);

				bool unwrap = elemType.IsValueType && !IsSafeAssignType(elemType);
				if (!unwrap)
				{
					for (int i = 0; i < unwrapTypes.Length; i++)
					{
						if (unwrapTypes[i].IsAssignableFrom(elemType))
						{
							unwrap = true;
							break;
						}
					}
				}

				if (unwrap)
				{
					for (int i = 0; i < src.Length; ++i)
						copy.SetValue(DeepCloneObjectExplicit(((Array)instance).GetValue(i), visited, unwrapTypes), i);
				}
				else
				{
					src.CopyTo(copy, 0);
				}

				return copy;
			}
			// Reference types / complex objects
			else
			{
				object copy = ReflectionHelper.CreateInstanceOf(instanceType);
				if (instanceType.IsClass) visited.Add(instance, copy);

				DeepCopyFieldsExplicit(
					instanceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
					instance, copy, visited, unwrapTypes);

				return copy;
			}
		}

		private static void DeepCopyFields(FieldInfo[] fields, object source, object target, VisitedGraph visited)
		{
			foreach (FieldInfo field in fields)
			{
				field.SetValue(target, DeepCloneObject(field.GetValue(source), visited));
			}
		}
		private static void DeepCopyFieldsExplicit(FieldInfo[] fields, object source, object target, VisitedGraph visited, Type[] unwrapTypes)
		{
			foreach (FieldInfo f in fields)
			{
				bool unwrap = f.FieldType.IsValueType && !IsSafeAssignType(f.FieldType);
				if (!unwrap)
				{
					for (int i = 0; i < unwrapTypes.Length; i++)
					{
						if (unwrapTypes[i].IsAssignableFrom(f.FieldType))
						{
							unwrap = true;
							break;
						}
					}
				}

				if (unwrap)
					f.SetValue(target, DeepCloneObjectExplicit(f.GetValue(source), visited, unwrapTypes));
				else
					f.SetValue(target, f.GetValue(source));
			}
		} 
		
		private static object DeepResetReferenceObject(object instance, HashSet<object> visited, Type[] resetTypes)
		{
			if (instance == null) return null;
			if (visited.Contains(instance)) return instance;
			Type instanceType = instance.GetType();

			// Primitive types or anything else that isn't a reference object we want to reset
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Reset check
			else if (!instanceType.IsValueType)
			{
				for (int i = 0; i < resetTypes.Length; i++)
				{
					if (resetTypes[i].IsAssignableFrom(instanceType))
					{
						return null;
					}
				}
			}

			// Arrays
			if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Type elemType = instanceType.GetElementType();
				visited.Add(instance);

				if (!IsSafeAssignType(elemType))
				{
					for (int i = 0; i < src.Length; ++i)
						src.SetValue(DeepResetReferenceObject(((Array)instance).GetValue(i), visited, resetTypes), i);
				}

				return instance;
			}
			// Reference types / complex objects
			else
			{
				if (instanceType.IsClass) visited.Add(instance);
				DeepResetReferenceFields(
					instanceType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
					instance, visited, resetTypes);
				return instance;
			}
		}
		private static void DeepResetReferenceFields(FieldInfo[] fields, object source, HashSet<object> visited, Type[] resetTypes)
		{
			foreach (FieldInfo f in fields)
			{
				f.SetValue(source, DeepResetReferenceObject(f.GetValue(source), visited, resetTypes));
			}
		} 

		private static object DeepResolveTypeReferenceObject(object instance, SerializationBinder binder, HashSet<object> visited)
		{
			if (instance == null) return null;
			if (visited.Contains(instance)) return instance;
			Type instanceType = instance.GetType();

			// Check for Reflection Referecnes such as Type, FieldInfo, etc.
			if (typeof(MemberInfo).IsAssignableFrom(instanceType))
			{
				// Re-resolve it
				MemberInfo info = instance as MemberInfo;
				if (info is Type)
				{
					Type infoType = info as Type;
					return binder.BindToType(infoType.Assembly.FullName, infoType.FullName);
				}
				else
				{
					BindingFlags bindFlagsAll = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
					Type infoDeclarerType = binder.BindToType(info.DeclaringType.Assembly.FullName, info.DeclaringType.FullName);
					if (info is FieldInfo)
					{
						return infoDeclarerType.GetField(info.Name, bindFlagsAll);
					}
					else if (info is PropertyInfo)
					{
						return infoDeclarerType.GetProperty(info.Name, bindFlagsAll);
					}
					else if (info is EventInfo)
					{
						return infoDeclarerType.GetEvent(info.Name, bindFlagsAll);
					}
					else if (info is MethodBase)
					{
						MethodBase infoMethodBase = info as MethodBase;
						ParameterInfo[] parameters = infoMethodBase.GetParameters();
						Type[] paramTypes = new Type[parameters.Length];
						for (int i = 0; i < parameters.Length; i++)
						{
							paramTypes[i] = parameters[i].ParameterType;
						}
						if (info is MethodInfo)
						{
							return infoDeclarerType.GetMethod(info.Name, bindFlagsAll, Type.DefaultBinder, infoMethodBase.CallingConvention, paramTypes, null);
						}
						else if (info is ConstructorInfo)
						{
							return infoDeclarerType.GetConstructor(bindFlagsAll, Type.DefaultBinder, infoMethodBase.CallingConvention, paramTypes, null);
						}
					}
				}
				Log.Core.WriteWarning("Could not resolve MemberInfo reference of type '{0}': Unknown type.", info.GetType().FullName);
				return null;
			}
			else if (IsSafeAssignType(instanceType))
			{
				return instance;
			}

			// Arrays
			if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Type elemType = instanceType.GetElementType();
				visited.Add(instance);

				for (int i = 0; i < src.Length; ++i)
					src.SetValue(DeepResolveTypeReferenceObject(((Array)instance).GetValue(i), binder, visited), i);

				return instance;
			}
			// Reference types / complex objects
			else
			{
				if (instanceType.IsClass) visited.Add(instance);
				DeepResolveTypeReferenceFields(
					instanceType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
					instance, binder, visited);
				return instance;
			}
		}
		private static void DeepResolveTypeReferenceFields(FieldInfo[] fields, object source, SerializationBinder binder, HashSet<object> visited)
		{
			foreach (FieldInfo f in fields)
			{
				f.SetValue(source, DeepResolveTypeReferenceObject(f.GetValue(source), binder, visited));
			}
		} 
		#endregion
	}
}
