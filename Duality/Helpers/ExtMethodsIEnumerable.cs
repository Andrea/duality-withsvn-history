using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	/// <summary>
	/// Provides extension methods for enumerations.
	/// </summary>
	public static class ExtMethodsIEnumerable
	{
		/// <summary>
		/// Enumerates the <see cref="Duality.GameObject">GameObjects</see> children.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <returns></returns>
		public static IEnumerable<GameObject> Children(this IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject o in objEnum)
				foreach (GameObject c in o.Children) yield return c;
		}
		/// <summary>
		/// Enumerates the <see cref="Duality.GameObject">GameObjects</see> children, grandchildren, etc.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <returns></returns>
		public static IEnumerable<GameObject> ChildrenDeep(this IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject o in objEnum)
				foreach (GameObject c in o.ChildrenDeep) yield return c;
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> that match the specified name.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IEnumerable<GameObject> ByName(this IEnumerable<GameObject> objEnum, string name)
		{
			return objEnum.Where(o => o.Name == name);
		}
		/// <summary>
		/// Returns the first <see cref="Duality.GameObject"/> that matches the specified name.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static GameObject FirstByName(this IEnumerable<GameObject> objEnum, string name)
		{
			return objEnum.FirstOrDefault(o => o.Name == name);
		}

		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> <see cref="Component">Components</see> of the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetComponents<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : class
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponents<T>(activeOnly)) yield return c;
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> childrens <see cref="Component">Components</see> of the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetComponentsInChildren<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : class
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponentsInChildren<T>(activeOnly)) yield return c;
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> (and their childrens) <see cref="Component">Components</see> of the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<T> GetComponentsDeep<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : class
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponentsDeep<T>(activeOnly)) yield return c;
		}

		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> <see cref="Duality.Components.Transform"/> Components.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<Components.Transform> Transform(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Transform c = o.Transform;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> <see cref="Duality.Components.Camera"/> Components.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<Components.Camera> Camera(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Camera c = o.Camera;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> <see cref="Duality.Components.Renderer"/> Components.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<ICmpRenderer> Renderer(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				ICmpRenderer c = o.Renderer;
				if (c != null && (!activeOnly || (c as Component).Active)) yield return c;
			}
		}
		/// <summary>
		/// Enumerates all <see cref="Duality.GameObject">GameObjects</see> <see cref="Duality.Components.Collider"/> Components.
		/// </summary>
		/// <param name="objEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<Components.Collider> Collider(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Collider c = o.Collider;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		
		/// <summary>
		/// Enumerates all <see cref="Component">Components</see> parent <see cref="Duality.GameObject">GameObjects</see>.
		/// </summary>
		/// <param name="compEnum"></param>
		/// <param name="activeOnly"></param>
		/// <returns></returns>
		public static IEnumerable<GameObject> GameObject(this IEnumerable<Component> compEnum, bool activeOnly = false)
		{
			foreach (Component c in compEnum)
				if (c.GameObj != null && (!activeOnly || c.GameObj.Active)) yield return c.GameObj;
		}

		/// <summary>
		/// Creates a separated list of the string versions of a set of objects.
		/// </summary>
		/// <typeparam name="T">The type of the incoming objects.</typeparam>
		/// <param name="collection">A set of objects.</param>
		/// <param name="separator">The string to use as separator between two string values.</param>
		/// <returns></returns>
		public static string ToString<T>(this IEnumerable<T> collection, string separator)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in collection)
			{
				sb.Append(item.ToString());
				sb.Append(separator);
			}
			return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
		}
		/// <summary>
		/// Creates a separated list of the string versions of a set of objects.
		/// </summary>
		/// <typeparam name="T">The type of the incoming objects.</typeparam>
		/// <param name="collection">A set of objects.</param>
		/// <param name="toString">A function that transforms objects to strings.</param>
		/// <param name="separator">The string to use as separator between two string values.</param>
		/// <returns></returns>
		public static string ToString<T>(this IEnumerable<T> collection, Func<T, string> toString, string separator)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in collection)
			{
				sb.Append(toString(item));
				sb.Append(separator);
			}
			return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
		}

		/// <summary>
		/// Enumerates objects that aren't null.
		/// </summary>
		/// <typeparam name="T">The type of the incoming objects.</typeparam>
		/// <param name="collection">A set of objects.</param>
		/// <returns></returns>
		public static IEnumerable<T> NotNull<T>(this IEnumerable<T> collection) where T : class
		{
			return collection.Where(i => i != null);
		}

		/// <summary>
		/// Enumerates a all objects within a specific index range.
		/// </summary>
		/// <typeparam name="T">The type of the incoming objects.</typeparam>
		/// <param name="collection">A set of objects.</param>
		/// <param name="startIndex">Index of the first object to be enumerated.</param>
		/// <param name="length">Number of objects to be enumerated.</param>
		/// <returns></returns>
		public static IEnumerable<T> Range<T>(this IEnumerable<T> collection, int startIndex, int length)
		{
			return collection.Skip(startIndex).Take(length);
		}

		/// <summary>
		/// Converts an enumeration of Resources to an enumeration of content references to it.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="res"></param>
		/// <returns></returns>
		public static IEnumerable<ContentRef<T>> Ref<T>(this IEnumerable<T> res) where T : Resource
		{
			return res.Select(r => new ContentRef<T>(r));
		}
		/// <summary>
		/// Converts an enumeration of content references to an enumeration of Resources.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="res"></param>
		/// <returns></returns>
		public static IEnumerable<T> Res<T>(this IEnumerable<ContentRef<T>> res) where T : Resource
		{
			return res.Select(r => r.Res);
		}
		/// <summary>
		/// Converts an enumeration of content references to an enumeration of Resources.
		/// </summary>
		/// <param name="res"></param>
		/// <returns></returns>
		public static IEnumerable<Resource> Res(this IEnumerable<IContentRef> res)
		{
			return res.Select(r => r.Res);
		}
	}
}
