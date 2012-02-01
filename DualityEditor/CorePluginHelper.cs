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

		public interface IEditorAction
		{
			string Name { get; }
			Image Icon { get; }

			void Perform(object obj);
		}
		public class EditorAction<T> : IEditorAction
		{
			private	string		name;
			private	Image		icon;
			private	Action<T>	action;

			public string Name
			{
				get { return this.name; }
			}
			public Image Icon
			{
				get { return this.icon; }
			}
			public Action<T> Action
			{
				get { return this.action; }
			}

			public EditorAction(string name, Image icon, Action<T> action)
			{
				this.name = name;
				this.icon = icon;
				this.action = action;
			}

			public void Perform(T obj)
			{
				this.action(obj);
			}
			void IEditorAction.Perform(object obj)
			{
				this.Perform((T)obj);
			}
		}

		public const string ImageContext_Icon			= "Icon";
		public const string CategoryContext_General		= "General";
		public const string ActionContext_ContextMenu	= "ContextMenu";
		public const string ActionContext_OpenRes		= "OpenRes";

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
		private static T RequestCorePluginRes<T>(Type type, Predicate<T> predicate = null) where T : IResEntry
		{
			if (type == null) return default(T);
			string typeString = ReflectionHelper.GetTypeId(type);

			List<IResEntry> resList = null;
			if (corePluginRes.TryGetValue(typeString, out resList))
			{
				foreach (IResEntry res in resList)
				{
					if (typeof(T).IsAssignableFrom(res.GetType()))
					{
						T casted = (T)res;
						if (predicate == null || predicate(casted)) return casted;
					}
				}
			}

			if (type != typeof(object))
				return RequestCorePluginRes<T>(type.BaseType, predicate);
			else
				return default(T);
		}
		private static List<T> RequestAllCorePluginRes<T>(Type type, Predicate<T> predicate = null) where T : IResEntry
		{
			if (type == null) return new List<T>();
			string typeString = ReflectionHelper.GetTypeId(type);

			List<T> result = null;
			List<IResEntry> resList = null;
			if (corePluginRes.TryGetValue(typeString, out resList))
			{
				foreach (IResEntry res in resList)
				{
					if (typeof(T).IsAssignableFrom(res.GetType()))
					{
						T casted = (T)res;
						if (predicate == null || predicate(casted))
						{
							if (result == null) result = new List<T>();
							result.Add(casted);
						}
					}
				}
			}

			if (result == null && type != typeof(object)) return RequestAllCorePluginRes<T>(type.BaseType, predicate);

			if (result == null) result = new List<T>();
			return result;
		}


		public static void RegisterTypeImage(Type type, Image image, string context)
		{
			RegisterCorePluginRes(type, new ImageResEntry(image, context));
		}
		public static Image RequestTypeImage(Type type, string context)
		{
			return RequestCorePluginRes<ImageResEntry>(type, e => e.context == context).img;
		}

		public static void RegisterTypeCategory(Type type, string category, string context)
		{
			RegisterCorePluginRes(type, new CategoryEntry(category, context));
		}
		public static string[] RequestTypeCategory(Type type, string context)
		{
			return RequestCorePluginRes<CategoryEntry>(type, e => e.context == context).categoryTree;
		}

		public static void RegisterPropertyEditorProvider(PropertyGrid.IPropertyEditorProvider provider)
		{
			RegisterCorePluginRes(typeof(object), new PropertyEditorProviderResEntry(provider));
		}
		public static IEnumerable<PropertyGrid.IPropertyEditorProvider> RequestPropertyEditorProviders()
		{
			return RequestAllCorePluginRes<PropertyEditorProviderResEntry>(typeof(object)).Select(e => e.provider);
		}

		public static void RegisterEditorAction<T>(string name, Image icon, Action<T> action, string context)
		{
			RegisterCorePluginRes(typeof(T), new EditorActionEntry(new EditorAction<T>(name, icon, action), context));
		}
		public static IEnumerable<EditorAction<T>> RequestEditorActions<T>(string context)
		{
			return RequestAllCorePluginRes<EditorActionEntry>(typeof(T), e => e.context == context).Select(e => e.action as EditorAction<T>);
		}
		public static IEnumerable<IEditorAction> RequestEditorActions(Type type, string context)
		{
			return RequestAllCorePluginRes<EditorActionEntry>(type, e => e.context == context).Select(e => e.action);
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
