using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

using Duality;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace DualityEditor
{
	public static class CorePluginHelper
	{
		private interface IResEntry {}
		private struct ImageResEntry : IResEntry
		{
			public	Image		img;
			public	string	context;

			public ImageResEntry(Image img, string context)
			{
				this.img = img;
				this.context = context;
			}
		}
		private struct PropertyEditorProviderResEntry : IResEntry
		{
			public	PropertyGrid.IPropertyEditorProvider	provider;

			public PropertyEditorProviderResEntry(PropertyGrid.IPropertyEditorProvider provider)
			{
				this.provider = provider;
			}
		}

		public const string ImageContext_Icon = "Icon";

		private	static	Dictionary<Type,List<IResEntry>>	corePluginRes	= new Dictionary<Type,List<IResEntry>>();

		private static void RegisterCorePluginRes(Type type, IResEntry res)
		{
			if (type == null) throw new ArgumentNullException("type");

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(type, out resList))
			{
				resList = new List<IResEntry>();
				corePluginRes[type] = resList;
			}
			if (!resList.Contains(res)) resList.Add(res);
		}
		private static T RequestCorePluginRes<T>(Type type, Predicate<T> predicate) where T : IResEntry
		{
			if (type == null) return default(T);

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(type, out resList)) return default(T);
			foreach (IResEntry res in resList)
			{
				if (typeof(T).IsAssignableFrom(res.GetType()))
				{
					T casted = (T)res;
					if (predicate(casted)) return casted;
				}
			}
			return default(T);
		}
		private static IEnumerable<T> RequestCorePluginRes<T>(Type type) where T : IResEntry
		{
			if (type == null) yield break;

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(type, out resList)) yield break;
			foreach (IResEntry res in resList)
			{
				if (typeof(T).IsAssignableFrom(res.GetType()))
				{
					T casted = (T)res;
					yield return casted;
				}
			}
			yield break;
		}

		public static void RegisterTypeImage(Type type, Image image, string context)
		{
			RegisterCorePluginRes(type, new ImageResEntry(image, context));
		}
		public static Image RequestTypeImage(Type type, string context)
		{
			return RequestCorePluginRes<ImageResEntry>(type, e => e.context == context).img;
		}

		public static void RegisterPropertyEditorProvider(PropertyGrid.IPropertyEditorProvider provider)
		{
			RegisterCorePluginRes(typeof(object), new PropertyEditorProviderResEntry(provider));
		}
		public static List<PropertyGrid.IPropertyEditorProvider> RequestPropertyEditorProviders()
		{
			return new List<PropertyGrid.IPropertyEditorProvider>(
				RequestCorePluginRes<PropertyEditorProviderResEntry>(typeof(object)).Select(e => e.provider));
		}
	}
}
