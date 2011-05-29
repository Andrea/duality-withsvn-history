using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Duality.Resources;

namespace Duality
{
	public interface IContentRef
	{
		Resource Res { get; }
		Resource ResWeak { get; }
		Type ResType { get; }
		string Path { get; set; }
		bool IsExplicitNull { get; }
		bool IsAvailable { get; }
		bool IsLoaded { get; }
		bool IsDefaultContent { get; }

		bool Is(Type resType);
		bool Is<U>() where U : Resource;
		ContentRef<U> As<U>() where U : Resource;
	}

	[Serializable]
	[System.Diagnostics.DebuggerDisplay("ContentRef {Path}, {IsAvailable}")]
	public struct ContentRef<T> : IEquatable<ContentRef<T>>, IContentRef where T : Resource
	{
		public static readonly ContentRef<T> Null = new ContentRef<T>(null);

		[NonSerialized]
		private	T		contentInstance;
		private	string	contentPath;

		public T Res
		{
			get 
			{ 
				if (this.contentInstance == null || this.contentInstance.Disposed) this.RetrieveInstance();
				return this.contentInstance;
			}
			set
			{
				if (value == null)	this.contentPath = null;
				else				this.contentPath = value.Path;
				this.contentInstance = value;
			}
		}
		public T ResWeak
		{
			get { return (this.contentInstance == null || this.contentInstance.Disposed) ? null : this.contentInstance; }
		}
		public Type ResType
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return this.contentInstance.GetType();
				return Resource.GetTypeByFileName(this.contentPath);
			}
		}
		public string Path
		{
			get { return this.contentPath; }
			set
			{
				this.contentPath = value;
				if (this.contentInstance != null && this.contentInstance.Path != value)
					this.contentInstance = null;
			}
		}
		public bool IsExplicitNull
		{
			get
			{
				return this.contentInstance == null && String.IsNullOrEmpty(this.contentPath);
			}
		}
		public bool IsAvailable
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return true;
				this.RetrieveInstance();
				return this.contentInstance != null;
			}
		}
		public bool IsLoaded
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return true;
				return ContentProvider.IsContentRegistered(this.contentPath);
			}
		}
		public bool IsDefaultContent
		{
			get { return this.contentPath != null && this.contentPath.Contains(':'); }
		}

		public ContentRef(T res, string altPath)
		{
			this.contentInstance = res;
			if (res != null && !String.IsNullOrEmpty(res.Path))
				this.contentPath = res.Path;
			else 
				this.contentPath = altPath;
		}
		public ContentRef(T res)
		{
			this.contentInstance = res;
			this.contentPath = (res != null) ? res.Path : null;
		}
		
		public bool Is(Type resType)
		{
			return resType.IsAssignableFrom(this.ResType);
		}
		public bool Is<U>() where U : Resource
		{
			return (this.contentInstance != null && !this.contentInstance.Disposed) ? 
				this.contentInstance is U : 
				typeof(U).IsAssignableFrom(this.ResType);
		}
		public ContentRef<U> As<U>() where U : Resource
		{
			if (!Is<U>()) return ContentRef<U>.Null;
			return new ContentRef<U>(this.contentInstance as U, this.contentPath);
		}

		private void RetrieveInstance()
		{
			if (!String.IsNullOrEmpty(this.contentPath))
				this = ContentProvider.RequestContent<T>(this.contentPath);
			else if (this.contentInstance != null && !String.IsNullOrEmpty(this.contentInstance.Path))
				this = ContentProvider.RequestContent<T>(this.contentInstance.Path);
			else
				this.contentInstance = null;
		}

		public override string ToString()
		{
			if (this.IsExplicitNull) return "null";
			else if (this.IsAvailable) return this.contentPath;
			else return "not available: " + this.contentPath;
		}
		public override bool Equals(object obj)
		{
			if (obj is ContentRef<T>)
				return this == (ContentRef<T>)obj;
			else
				return base.Equals(obj);
		}
		public bool Equals(ContentRef<T> other)
		{
			return this == other;
		}

		Resource IContentRef.Res
		{
			get { return this.Res; }
		}
		Resource IContentRef.ResWeak
		{
			get { return this.ResWeak; }
		}

		public static implicit operator ContentRef<T>(T res)
		{
			return new ContentRef<T>(res);
		}
		public static explicit operator T(ContentRef<T> res)
		{
			return res.Res;
		}

		public static bool operator ==(ContentRef<T> first, ContentRef<T> second)
		{
			if (first.contentInstance != null && second.contentInstance != null)
				return first.contentInstance == second.contentInstance;
			else
				return first.contentPath == second.contentPath;
		}
		public static bool operator !=(ContentRef<T> first, ContentRef<T> second)
		{
			return !(first == second);
		}
	}

	public static class ContentProvider
	{
		public const string	VirtualContentPath = "Default:";

		private	static	bool	defaultContentInitialized	= false;
		private	static	Dictionary<string,Resource>	resLibrary		= new Dictionary<string,Resource>();
		private	static	List<ContentRef<Resource>>	defaultContent	= new List<ContentRef<Resource>>();

		public static void InitDefaultContent()
		{
			if (defaultContentInitialized) return;
			Log.Core.Write("Initializing default content..");
			Log.Core.PushIndent();

			VertexShader.InitDefaultContent();
			FragmentShader.InitDefaultContent();
			ShaderProgram.InitDefaultContent();
			DrawTechnique.InitDefaultContent();
			Pixmap.InitDefaultContent();
			Texture.InitDefaultContent();
			Material.InitDefaultContent();
			AudioData.InitDefaultContent();
			Sound.InitDefaultContent();

			// Make a list of all default content available
			foreach (KeyValuePair<string,Resource> pair in resLibrary)
			{
				defaultContent.Add(new ContentRef<Resource>(pair.Value, pair.Key));
			}

			defaultContentInitialized = true;
			Log.Core.PopIndent();
			Log.Core.Write("..done");
		}
		public static List<ContentRef<Resource>> GetAllDefaultContent()
		{
			return new List<ContentRef<Resource>>(defaultContent);
		}
		public static void ClearContent()
		{
			foreach (var pair in resLibrary)
			{
				pair.Value.Dispose();
			}
			resLibrary.Clear();
		}

		public static void RegisterContent(string path, Resource content)
		{
			if (String.IsNullOrEmpty(path)) return;
			resLibrary[path] = content;
		}
		public static bool IsContentRegistered(string path)
		{
			if (String.IsNullOrEmpty(path)) return false;
			return resLibrary.ContainsKey(path);
		}

		public static bool UnregisterContent(string path, bool dispose = true)
		{
			if (String.IsNullOrEmpty(path)) return false;

			// Dispose cached content
			Resource res;
			if (dispose && resLibrary.TryGetValue(path, out res)) res.Dispose();

			return resLibrary.Remove(path);
		}
		public static void UnregisterContentTree(string dir, bool dispose = true)
		{
			if (String.IsNullOrEmpty(dir)) return;

			List<string> unregisterList = new List<string>(
				from p in resLibrary.Keys
				where !p.Contains(':') && PathHelper.IsPathLocatedIn(p, dir)
				select p);

			foreach (string p in unregisterList)
				UnregisterContent(p, dispose);
		}
		public static void UnregisterAllContent<T>(bool dispose = true) where T : Resource
		{
			foreach (ContentRef<T> content in RequestAllContent<T>())
				UnregisterContent(content.Path, dispose);
		}

		public static bool RenameContent(string path, string newPath)
		{
			if (String.IsNullOrEmpty(path)) return false;

			Resource res;
			if (resLibrary.TryGetValue(path, out res))
			{
				res.ChangePath(newPath);
				resLibrary[newPath] = res;
				resLibrary.Remove(path);
				return true;
			}
			else
				return false;
		}
		public static void RenameContentTree(string dir, string newDir)
		{
			if (String.IsNullOrEmpty(dir)) return;

			List<string> renameList = new List<string>(
				from p in resLibrary.Keys
				where !p.Contains(':') && PathHelper.IsPathLocatedIn(p, dir)
				select p);

			foreach (string p in renameList)
			{
				RenameContent(p, p.Replace(
					dir + Path.DirectorySeparatorChar,
					newDir + Path.DirectorySeparatorChar));
			}
		}

		public static ContentRef<T> RequestContent<T>(string path) where T : Resource
		{
			if (String.IsNullOrEmpty(path)) return null;

			// Return cached content
			Resource res;
			if (resLibrary.TryGetValue(path, out res)) return new ContentRef<T>(res as T, path);

			// Load new content
			return new ContentRef<T>(LoadContent(path) as T, path);
		}
		public static List<ContentRef<T>> RequestAllContent<T>() where T : Resource
		{
			List<ContentRef<T>> allContent = new List<ContentRef<T>>();
			foreach (var v in resLibrary.Values)
			{
				if (v is T) allContent.Add((T)v);
			}
			return allContent;
		}
		public static IContentRef RequestContent(string path)
		{
			return RequestContent<Resource>(path);
		}

		private static Resource LoadContent(string path)
		{
			Log.Core.Write("Loading Ressource '{0}'...", path);

			Resource res = Resource.LoadResource<Resource>(path);
			if (res != null) RegisterContent(path, res);
			return res;
		}
	}
}
