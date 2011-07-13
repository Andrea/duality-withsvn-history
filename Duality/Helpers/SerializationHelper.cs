using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.IO;

namespace Duality
{
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
			return t.IsPrimitive || t.IsEnum || t == typeof(string) || typeof(MemberInfo).IsAssignableFrom(t) || typeof(IContentRef).IsAssignableFrom(t);
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

				if (!IsSafeAssignType(elemType) || typeof(MemberInfo).IsAssignableFrom(elemType))
				{
					for (int i = 0; i < src.Length; ++i)
						src.SetValue(DeepResolveTypeReferenceObject(((Array)instance).GetValue(i), binder, visited), i);
				}

				return instance;
			}
			// Special case: Dictionary<T,U> with T == type reference type: Need to rebuild due to GetHashCode-Stuff
			else if (
				instanceType.IsGenericType && 
				instanceType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && 
				typeof(MemberInfo).IsAssignableFrom(instanceType.GetGenericArguments()[0]))
			{
				visited.Add(instance);

				IDictionary dict = instance as IDictionary;
				List<DictionaryEntry> entries = new List<DictionaryEntry>(dict.Count);
				foreach (DictionaryEntry pair in dict)
					entries.Add(pair);
				
				MethodInfo m_Clear = instanceType.GetMethod("Clear");
				MethodInfo m_Add = instanceType.GetMethod("Add");

				m_Clear.Invoke(instance, null);
				foreach (DictionaryEntry pair in entries)
					m_Add.Invoke(instance, new object[] { DeepResolveTypeReferenceObject(pair.Key, binder, visited), pair.Value });

				return instance;
			}
			// Special case: HashSet<T> with T == type reference type: Need to rebuild due to GetHashCode-Stuff
			else if (
				instanceType.IsGenericType && 
				instanceType.GetGenericTypeDefinition() == typeof(HashSet<>) && 
				typeof(MemberInfo).IsAssignableFrom(instanceType.GetGenericArguments()[0]))
			{
				visited.Add(instance);

				IEnumerable set = instance as IEnumerable;
				List<object> entries = new List<object>();
				foreach (object obj in set)
					entries.Add(obj);
				
				MethodInfo m_Clear = instanceType.GetMethod("Clear");
				MethodInfo m_Add = instanceType.GetMethod("Add");

				m_Clear.Invoke(instance, null);
				foreach (DictionaryEntry pair in entries)
					m_Add.Invoke(instance, new object[] { DeepResolveTypeReferenceObject(pair.Key, binder, visited) });

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
