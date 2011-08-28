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
			string typeString = ReflectionHelper.GetTypeName(type);

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(typeString, out resList))
			{
				resList = new List<IResEntry>();
				corePluginRes[typeString] = resList;
			}
			if (!resList.Contains(res)) resList.Add(res);
		}
		private static T RequestCorePluginRes<T>(Type type, Predicate<T> predicate) where T : IResEntry
		{
			if (type == null) return default(T);
			string typeString = ReflectionHelper.GetTypeName(type);

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(typeString, out resList)) return default(T);
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
			string typeString = ReflectionHelper.GetTypeName(type);

			List<IResEntry> resList = null;
			if (!corePluginRes.TryGetValue(typeString, out resList)) yield break;
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
		public static List<PropertyGrid.IPropertyEditorProvider> RequestPropertyEditorProviders()
		{
			return new List<PropertyGrid.IPropertyEditorProvider>(
				RequestCorePluginRes<PropertyEditorProviderResEntry>(typeof(object)).Select(e => e.provider));
		}

		public static void RegisterEditorAction<T>(string name, Image icon, Action<T> action, string context)
		{
			RegisterCorePluginRes(typeof(T), new EditorActionEntry(new EditorAction<T>(name, icon, action), context));
		}
		public static IEnumerable<EditorAction<T>> RequestEditorActions<T>(string context)
		{
			return from entry in RequestCorePluginRes<EditorActionEntry>(typeof(T))
				   where entry.context == context
				   select entry.action as EditorAction<T>;
		}
		public static IEnumerable<IEditorAction> RequestEditorActions(Type type, string context)
		{
			return from entry in RequestCorePluginRes<EditorActionEntry>(type)
				   where entry.context == context
				   select entry.action;
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
