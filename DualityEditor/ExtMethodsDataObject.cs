using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Duality;
using Duality.ColorFormat;

namespace DualityEditor
{
	public static class ExtMethodsDataObject
	{
		public static void AppendComponentRefs(this DataObject data, IEnumerable<Component> cmp)
		{
			Component[] cmpArray = cmp.ToArray();
			if (cmpArray.Length > 0) data.SetData(cmpArray);
		}
		public static bool ContainsComponentRefs(this DataObject data)
		{
			return data.GetDataPresent(typeof(Component[]));
		}
		public static Component[] GetComponentRefs(this DataObject data)
		{
			return data.GetData(typeof(Component[])) as Component[];
		}

		public static void AppendGameObjectRefs(this DataObject data, IEnumerable<GameObject> obj)
		{
			GameObject[] objArray = obj.ToArray();
			if (objArray.Length > 0) data.SetData(objArray);
		}
		public static bool ContainsGameObjectRefs(this DataObject data)
		{
			return data.GetDataPresent(typeof(GameObject[]));
		}
		public static GameObject[] GetGameObjectRefs(this DataObject data)
		{
			return data.GetData(typeof(GameObject[])) as GameObject[];
		}

		public static void AppendContentRefs<T>(this DataObject data, IEnumerable<ContentRef<T>> content) where T : Resource
		{
			if (!content.Any()) return;
			ContentRef<Resource>[] refArray = content.Select(c => c.As<Resource>()).ToArray();
			data.SetData(refArray);
		}
		public static bool ContainsContentRefs<T>(this DataObject data) where T : Resource
		{
			if (!data.GetDataPresent(typeof(ContentRef<Resource>[]))) return false;
			ContentRef<Resource>[] refArray = data.GetData(typeof(ContentRef<Resource>[])) as ContentRef<Resource>[];
			return refArray.Any(r => r.Is<T>());
		}
		public static bool ContainsContentRefs(this DataObject data, Type resType = null)
		{
			if (resType == null) resType = typeof(Resource);
			if (!data.GetDataPresent(typeof(ContentRef<Resource>[]))) return false;
			ContentRef<Resource>[] refArray = data.GetData(typeof(ContentRef<Resource>[])) as ContentRef<Resource>[];
			return refArray.Any(r => r.Is(resType));
		}
		public static ContentRef<T>[] GetContentRefs<T>(this DataObject data) where T : Resource
		{
			if (!data.GetDataPresent(typeof(ContentRef<Resource>[]))) return null;
			ContentRef<Resource>[] refArray = data.GetData(typeof(ContentRef<Resource>[])) as ContentRef<Resource>[];
			return (
				from r in refArray
				where r.Is<T>()
				select r.As<T>()
				).ToArray();
		}
		public static ContentRef<Resource>[] GetContentRefs(this DataObject data, Type resType = null)
		{
			if (resType == null) resType = typeof(Resource);
			if (!data.GetDataPresent(typeof(ContentRef<Resource>[]))) return null;
			ContentRef<Resource>[] refArray = data.GetData(typeof(ContentRef<Resource>[])) as ContentRef<Resource>[];
			return (
				from r in refArray
				where r.Is(resType)
				select r
				).ToArray();
		}
		
		public static void AppendIColorData(this DataObject data, IEnumerable<IColorData> color)
		{
			if (!color.Any()) return;
			data.SetData(color.ToArray());
		}
		public static bool ContainsIColorData(this DataObject data)
		{
			return data.GetDataPresent(typeof(IColorData[]));
		}
		public static T[] GetIColorData<T>(this DataObject data) where T : IColorData
		{
			if (!data.GetDataPresent(typeof(IColorData[]))) return null;
			IColorData[] clrArray = data.GetData(typeof(IColorData[])) as IColorData[];

			// Don't care which format? Great, just return the array as is
			if (typeof(T) == typeof(IColorData)) return (T[])(object)clrArray;

			// Convert to specific format
			return clrArray.Select<IColorData,T>(ic => ic is T ? (T)ic : IColorDataCreator.FromIntRgba<T>(ic.ToIntRgba())).ToArray();
		}

		public static void AppendFiles(this DataObject data, IEnumerable<string> files)
		{
			var sc = new System.Collections.Specialized.StringCollection();
			foreach (string file in files)
			{
				string path = Path.GetFullPath(file);
				if (File.Exists(path) || Directory.Exists(path))
				{
					sc.Add(path);
				}
			}
			if (sc.Count > 0) data.SetFileDropList(sc);
		}
	}
}
