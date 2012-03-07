using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

using Duality;
using DualityEditor.CorePluginInterface;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace DualityEditor
{
	public static class CorePluginHelper
	{
		public const int Priority_None			= 0;
		public const int Priority_General		= 20;
		public const int Priority_Specialized	= 50;
		public const int Priority_Override		= 100;

		#region Resource Entries
		private interface IResEntry {}
		private struct ImageResEntry : IResEntry
		{
			public	Image	img;
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
		private struct EditorActionEntry : IResEntry
		{
			public	IEditorAction	action;
			public	string			context;

			public EditorActionEntry(IEditorAction action, string context)
			{
				this.action = action;
				this.context = context;
			}
		}
		private	struct CategoryEntry : IResEntry
		{
			public	string[]	categoryTree;
			public	string		context;

			public CategoryEntry(string category, string context)
			{
				this.categoryTree = category.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
				this.context = context;
			}
		}
		private struct DataSelectorEntry : IResEntry
		{
			public	DataConverter	selector;

			public DataSelectorEntry(DataConverter selector)
			{
				this.selector = selector;
			}
		}
		#endregion


		public const string ImageContext_Icon				= "Icon";
		public const string CategoryContext_General			= "General";
		public const string ActionContext_ContextMenu		= "ContextMenu";
		public const string ActionContext_OpenRes			= "OpenRes";

		private	static	Dictionary<string,List<IResEntry>>	corePluginRes	= new Dictionary<string,List<IResEntry>>();
		private	static	XmlCodeDoc							corePluginDoc	= new XmlCodeDoc();


		private static void RegisterCorePluginRes(Type type, IResEntry res)
		{
			if (type == null) throw new ArgumentNullException("type");
			string typeString = ReflectionHelper.GetTypeId(type);

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(typeString, out resList))
			{
				resList = new List<IResEntry>();
				corePluginRes[typeString] = resList;
			}
			if (!resList.Contains(res)) resList.Add(res);
		}
		private static IEnumerable<T> QueryPluginResCandidates<T>(Type type, Predicate<T> predicate) where T : IResEntry
		{
			string typeString = ReflectionHelper.GetTypeId(type);
			List<IResEntry> resList = null;
			if (corePluginRes.TryGetValue(typeString, out resList))
			{
				foreach (IResEntry res in resList)
				{
					if (typeof(T).IsAssignableFrom(res.GetType()))
					{
						T casted = (T)res;
						if (predicate == null || predicate(casted)) yield return casted;
					}
				}
			}
			yield break;
		}
		private static T RequestCorePluginRes<T>(Type type, bool contravariantType, Predicate<T> predicate) where T : IResEntry
		{
			if (type == null) return default(T);
			if (contravariantType)
			{
				List<Type> contravariantTypes = new List<Type>();
				contravariantTypes.Add(type);
				foreach (string key in corePluginRes.Keys)
				{
					Type keyType = ReflectionHelper.ResolveType(key);
					if (type.IsAssignableFrom(keyType) && !contravariantTypes.Contains(keyType)) contravariantTypes.Add(keyType);
				}
				foreach (Type contra in contravariantTypes)
				{
					foreach (T entry in QueryPluginResCandidates<T>(contra, predicate))
						return entry;
				}
				return default(T);
			}
			else
			{
				foreach (T entry in QueryPluginResCandidates<T>(type, predicate))
					return entry;

				if (type != typeof(object))
					return RequestCorePluginRes<T>(type.BaseType, contravariantType, predicate);
				else
					return default(T);
			}
		}
		private static List<T> RequestAllCorePluginRes<T>(Type type, bool contravariantType, Predicate<T> predicate) where T : IResEntry
		{
			if (contravariantType)
			{
				List<Type> contravariantTypes = new List<Type>();
				contravariantTypes.Add(type);
				foreach (string key in corePluginRes.Keys)
				{
					Type keyType = ReflectionHelper.ResolveType(key);
					if (type.IsAssignableFrom(keyType) && !contravariantTypes.Contains(keyType)) contravariantTypes.Add(keyType);
				}
				List<T> result = null;
				foreach (Type contra in contravariantTypes)
				{
					foreach (T entry in QueryPluginResCandidates<T>(contra, predicate))
					{
						if (result == null) result = new List<T>();
						result.Add(entry);
					}
				}
				if (result == null) result = new List<T>();
				return result;
			}
			else
			{
				List<T> result = null;

				while (type != null)
				{
					foreach (T entry in QueryPluginResCandidates<T>(type, predicate))
					{
						if (result == null) result = new List<T>();
						result.Add(entry);
					}
					type = type.BaseType;
				}

				if (result == null) result = new List<T>();
				return result;
			}
		}


		public static void RegisterTypeImage(Type type, Image image, string context)
		{
			RegisterCorePluginRes(type, new ImageResEntry(image, context));
		}
		public static Image RequestTypeImage(Type type, string context)
		{
			return RequestCorePluginRes<ImageResEntry>(type, false, e => e.context == context).img;
		}

		public static void RegisterTypeCategory(Type type, string category, string context)
		{
			RegisterCorePluginRes(type, new CategoryEntry(category, context));
		}
		public static string[] RequestTypeCategory(Type type, string context)
		{
			return RequestCorePluginRes<CategoryEntry>(type, false, e => e.context == context).categoryTree;
		}

		public static void RegisterPropertyEditorProvider(PropertyGrid.IPropertyEditorProvider provider)
		{
			RegisterCorePluginRes(typeof(object), new PropertyEditorProviderResEntry(provider));
		}
		public static IEnumerable<PropertyGrid.IPropertyEditorProvider> RequestPropertyEditorProviders()
		{
			return RequestAllCorePluginRes<PropertyEditorProviderResEntry>(typeof(object), false, null).Select(e => e.provider);
		}

		public static void RegisterEditorAction<T>(string name, Image icon, Action<T> action, string context)
		{
			RegisterCorePluginRes(typeof(T), new EditorActionEntry(new EditorAction<T>(name, icon, action), context));
		}
		public static void RegisterEditorGroupAction<T>(string name, Image icon, Action<IEnumerable<T>> action, string context, Predicate<IEnumerable<T>> actionPredicate = null)
		{
			IEditorAction editorAction = new EditorGroupAction<T>(name, icon, action, actionPredicate);
			RegisterCorePluginRes(typeof(T), new EditorActionEntry(editorAction, context));
		}
		public static IEnumerable<IEditorAction> RequestEditorActions<T>(string context, IEnumerable<object> forGroup = null)
		{
			return RequestEditorActions(typeof(T), context, forGroup);
		}
		public static IEnumerable<IEditorAction> RequestEditorActions(Type type, string context, IEnumerable<object> forGroup = null)
		{
			return RequestAllCorePluginRes<EditorActionEntry>(type, false, e => e.context == context && e.action.CanPerformOn(forGroup)).Select(e => e.action);
		}

		public static void RegisterDataConverter<T>(DataConverter selector)
		{
			RegisterCorePluginRes(typeof(T), new DataSelectorEntry(selector));
		}
		public static IEnumerable<DataConverter> RequestDataConverters<T>()
		{
			return RequestDataConverters(typeof(T)).OfType<DataConverter>();
		}
		public static IEnumerable<DataConverter> RequestDataConverters(Type type)
		{
			return RequestAllCorePluginRes<DataSelectorEntry>(type, true, null).Select(e => e.selector);
		}

		public static IEnumerable<T> ConvertFromDataObject<T>(IDataObject data)
		{
			IEnumerable<object> result = ConvertFromDataObject(typeof(T), data); 
			if (result == null)
				return null;
			else
				return result.OfType<T>();
		}
		public static bool CanConvertFromDataObject<T>(IDataObject data)
		{
			return CanConvertFromDataObject(typeof(T), data);
		}
		public static IEnumerable<object> ConvertFromDataObject(Type type, IDataObject data)
		{
			if (data == null) return null;
			return ConvertFromDataObject(type, new ConvertOperation(data));
		}
		public static IEnumerable<object> ConvertFromDataObject(Type type, ConvertOperation selection)
		{
			if (selection == null) return null;

			List<DataConverter> selectors = RequestDataConverters(type).Where(s => s.CanConvertFrom(selection.Data)).ToList();
			selectors.StableSort((s1, s2) => s2.Priority - s1.Priority);
			foreach (var s in selectors) s.Convert(selection);

			return selection.Result.Any() ? selection.Result : null;
		}
		public static bool CanConvertFromDataObject(Type type, IDataObject data)
		{
			if (data == null) return false;
			return RequestDataConverters(type).Where(s => s.CanConvertFrom(data)).Any();
		}

		public static void RegisterXmlCodeDoc(XmlCodeDoc doc)
		{
			corePluginDoc.AppendDoc(doc);
		}
		public static XmlCodeDoc.Entry RequestXmlCodeDoc(MemberInfo info)
		{
			return corePluginDoc.GetMemberDoc(info);
		}
	}
}
