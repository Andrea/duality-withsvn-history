using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	public static class ExtMethodsIList
	{
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

		public static int IndexOfFirst<T>(this IList<T> collection, T val) where T : class
		{
			for (int i = 0; i < collection.Count; i++)
				if (collection[i] == val) return i;
			return -1;
		}
		public static int IndexOfFirst<T>(this IList<T> collection, Predicate<T> pred) where T : class
		{
			for (int i = 0; i < collection.Count; i++)
				if (pred(collection[i])) return i;
			return -1;
		}
	}
}
