using System;
using System.Collections.Generic;

namespace Duality
{
	/// <summary>
	/// Provides extension methods for lists.
	/// </summary>
	public static class ExtMethodsIList
	{
		/// <summary>
		/// Performs a stable sort.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="list">List to perform the sort operation on.</param>
		public static void StableSort<T>(this IList<T> list)
		{
			StableSort<T>(list, Comparer<T>.Default);
		}
		/// <summary>
		/// Performs a stable sort.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="list">List to perform the sort operation on.</param>
		/// <param name="comparer">The comparer to use.</param>
		public static void StableSort<T>(this IList<T> list, Comparer<T> comparer)
		{
			StableSort<T>(list, comparer.Compare);
		}
		/// <summary>
		/// Performs a stable sort.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="list">List to perform the sort operation on.</param>
		/// <param name="comparison">The comparison to use.</param>
		public static void StableSort<T>(this IList<T> list, Comparison<T> comparison)
		{
			if (comparison == null) throw new ArgumentNullException( "comparison" );

			int count = list.Count;
			for (int j = 1; j < count; j++)
			{
				T key = list[j];

				int i = j - 1;
				for (; i >= 0 && comparison( list[i], key ) > 0; i--)
				{
					list[i + 1] = list[i];
				}
				list[i + 1] = key;
			}
		}

		/// <summary>
		/// Returns the index of the first object matching the specified one.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="collection">List to perform the sort operation on.</param>
		/// <param name="val">Object to compare the lists contents to.</param>
		/// <returns></returns>
		public static int IndexOfFirst<T>(this IList<T> collection, T val) where T : class
		{
			for (int i = 0; i < collection.Count; i++)
				if (collection[i] == val) return i;
			return -1;
		}
		/// <summary>
		/// Returns the index of the first object matching the specified predicate.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="collection">List to perform the sort operation on.</param>
		/// <param name="pred">The predicate to use on the lists contents.</param>
		/// <returns></returns>
		public static int IndexOfFirst<T>(this IList<T> collection, Predicate<T> pred) where T : class
		{
			for (int i = 0; i < collection.Count; i++)
				if (pred(collection[i])) return i;
			return -1;
		}
		/// <summary>
		/// Returns the index of the last object matching the specified one.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="collection">List to perform the sort operation on.</param>
		/// <param name="val">Object to compare the lists contents to.</param>
		/// <returns></returns>
		public static int IndexOfLast<T>(this IList<T> collection, T val) where T : class
		{
			for (int i = collection.Count - 1; i >= 0; i--)
				if (collection[i] == val) return i;
			return -1;
		}
		/// <summary>
		/// Returns the index of the last object matching the specified predicate.
		/// </summary>
		/// <typeparam name="T">The lists object type.</typeparam>
		/// <param name="collection">List to perform the sort operation on.</param>
		/// <param name="pred">The predicate to use on the lists contents.</param>
		/// <returns></returns>
		public static int IndexOfLast<T>(this IList<T> collection, Predicate<T> pred) where T : class
		{
			for (int i = collection.Count - 1; i >= 0; i--)
				if (pred(collection[i])) return i;
			return -1;
		}

		/// <summary>
		/// Returns the combined hash code of the specified byte list.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int GetCombinedHashCode(this IList<byte> list)
		{ unchecked {
			const int p = 16777619;
			int hash = (int)2166136261;

			for (int i = 0; i < list.Count; i++)
					hash = (hash ^ list[i]) * p;

			hash += hash << 13;
			hash ^= hash >> 7;
			hash += hash << 3;
			hash ^= hash >> 17;
			hash += hash << 5;
			return hash;
		} }
	}
}
