using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	public static class ExtMethodsIEnumerable
	{
		public static IEnumerable<GameObject> Children(this IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject o in objEnum)
				foreach (GameObject c in o.Children) yield return c;
		}
		public static IEnumerable<GameObject> ChildrenDeep(this IEnumerable<GameObject> objEnum)
		{
			foreach (GameObject o in objEnum)
				foreach (GameObject c in o.ChildrenDeep) yield return c;
		}
		public static IEnumerable<GameObject> ByName(this IEnumerable<GameObject> objEnum, string name)
		{
			return objEnum.Where(o => o.Name == name);
		}
		public static GameObject FirstByName(this IEnumerable<GameObject> objEnum, string name)
		{
			return objEnum.FirstOrDefault(o => o.Name == name);
		}

		public static IEnumerable<T> GetComponents<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : Component
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponents<T>(activeOnly)) yield return c;
		}
		public static IEnumerable<T> GetComponentsInChildren<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : Component
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponentsInChildren<T>(activeOnly)) yield return c;
		}
		public static IEnumerable<T> GetComponentsDeep<T>(this IEnumerable<GameObject> objEnum, bool activeOnly = false) where T : Component
		{
			foreach (GameObject o in objEnum)
				foreach (T c in o.GetComponentsDeep<T>(activeOnly)) yield return c;
		}

		public static IEnumerable<Components.Transform> Transform(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Transform c = o.Transform;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		public static IEnumerable<Components.Camera> Camera(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Camera c = o.Camera;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		public static IEnumerable<Components.Renderer> Renderer(this IEnumerable<GameObject> objEnum, bool activeOnly = false)
		{
			foreach (GameObject o in objEnum)
			{
				Components.Renderer c = o.Renderer;
				if (c != null && (!activeOnly || c.Active)) yield return c;
			}
		}
		
		public static IEnumerable<GameObject> GameObject(this IEnumerable<Component> compEnum, bool activeOnly = false)
		{
			foreach (Component c in compEnum)
				if (c.GameObj != null && (!activeOnly || c.GameObj.Active)) yield return c.GameObj;
		}

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

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
            return collection;
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T,int> action)
        {
            int i = 0;
            foreach (var item in collection)
            {
                action(item, i);
                i++;
            }
            return collection;
        }

		public static IEnumerable<T> NotNull<T>(this IEnumerable<T> collection) where T : class
		{
			return collection.Where(i => i != null);
		}

		public static IEnumerable<T> Range<T>(this IEnumerable<T> collection, int startIndex, int length)
		{
			return collection.Skip(startIndex).Take(length);
		}
	}
}
