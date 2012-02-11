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
	/// <summary>
	/// IContentRef is a general interface for <see cref="ContentRef{T}">content references</see> of any <see cref="Resource"/> type.
	/// </summary>
	/// <seealso cref="Resource"/>
	/// <seealso cref="ContentProvider"/>
	/// <seealso cref="ContentRef{T}"/>
	public interface IContentRef
	{
		/// <summary>
		/// [GET] Returns the actual <see cref="Resource"/>. If currently unavailable, it is loaded and then returned.
		/// Because of that, this Property is only null if the references Resource is missing, invalid, or
		/// this content reference has been explicitly set to null. Never returns disposed Resources.
		/// </summary>
		Resource Res { get; }
		/// <summary>
		/// [GET] Returns the current reference to the Resource that is stored locally. No attemp is made to load or reload
		/// the Resource if currently unavailable.
		/// </summary>
		Resource ResWeak { get; }
		/// <summary>
		/// [GET] The <see cref="System.Type"/> of the referenced Resource. If currently unavailable, this is determined by
		/// the Resource file path.
		/// </summary>
		Type ResType { get; }
		/// <summary>
		/// [GET / SET] The path where to look for the Resource, if it is currently unavailable.
		/// </summary>
		string Path { get; set; }
		/// <summary>
		/// [GET] Returns whether this content reference has been explicitly set to null.
		/// </summary>
		bool IsExplicitNull { get; }
		/// <summary>
		/// [GET] Returns whether this content reference is available in general. This may trigger loading it, if currently unavailable.
		/// </summary>
		bool IsAvailable { get; }
		/// <summary>
		/// [GET] Returns whether the referenced Resource is currently loaded.
		/// </summary>
		bool IsLoaded { get; }
		/// <summary>
		/// [GET] Returns whether the referenced Resource is part of Duality's embedded default content.
		/// </summary>
		bool IsDefaultContent { get; }
		/// <summary>
		/// [GET] The name of the referenced Resource.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Determines if the references Resource's Type is assignable to the specified Type.
		/// </summary>
		/// <param name="resType">The Resource Type in question.</param>
		/// <returns>True, if the referenced Resource is of the specified Type or subclassing it.</returns>
		bool Is(Type resType);
		/// <summary>
		/// Determines if the references Resource's Type is assignable to the specified Type.
		/// </summary>
		/// <typeparam name="U">The Resource Type in question.</typeparam>
		/// <returns>True, if the referenced Resource is of the specified Type or subclassing it.</returns>
		bool Is<U>() where U : Resource;
		/// <summary>
		/// Creates a <see cref="ContentRef{T}"/> of the specified Type, referencing the same Resource.
		/// </summary>
		/// <typeparam name="U">The Resource Type to create a reference of.</typeparam>
		/// <returns>
		/// A <see cref="ContentRef{T}"/> of the specified Type, referencing the same Resource.
		/// Returns a <see cref="ContentRef{T}.Null">null reference</see> if the Resource is not assignable
		/// to the specified Type.
		/// </returns>
		ContentRef<U> As<U>() where U : Resource;
	}
	
	/// <summary>
	/// This lightweight struct references <see cref="Resource">Resources</see> in an abstract way. It
	/// is tightly connected to the <see cref="ContentProvider"/> and takes care of keeping or making 
	/// the referenced content available when needed. Never store actual Resource references permanently,
	/// instead use a ContentRef to it. However, you may retrieve and store a direct Resource reference
	/// temporarily, although this is only recommended at method-local scope.
	/// </summary>
	/// <example>
	/// The following example retrieves a ContentRef:
	/// <code>
	/// ContentRef<Pixmap> pixmapRef = ContentProvider.RequestResource<Pixmap>(@"Data\Test.Pixmap.res");
	/// // Accessing the Pixmap
	/// pixmapRef.Res.DoSomething();
	/// // Temporarily obtaining a direct Pixmap reference (Never store it)
	/// Pixmap tempRef = pixmapRef.Res;
	/// </code>
	/// </example>
	/// <seealso cref="Resource"/>
	/// <seealso cref="ContentProvider"/>
	/// <seealso cref="IContentRef"/>
	[Serializable]
	[System.Diagnostics.DebuggerDisplay("ContentRef {Path}, {IsAvailable}")]
	public struct ContentRef<T> : IEquatable<ContentRef<T>>, IContentRef where T : Resource
	{
		/// <summary>
		/// An explicit null reference.
		/// </summary>
		public static readonly ContentRef<T> Null = new ContentRef<T>(null);

		[NonSerialized]
		private	T		contentInstance;
		private	string	contentPath;
		
		/// <summary>
		/// [GET / SET] The actual <see cref="Resource"/>. If currently unavailable, it is loaded and then returned.
		/// Because of that, this Property is only null if the references Resource is missing, invalid, or
		/// this content reference has been explicitly set to null. Never returns disposed Resources.
		/// </summary>
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
		/// <summary>
		/// [GET] Returns the current reference to the Resource that is stored locally. No attemp is made to load or reload
		/// the Resource if currently unavailable.
		/// </summary>
		public T ResWeak
		{
			get { return (this.contentInstance == null || this.contentInstance.Disposed) ? null : this.contentInstance; }
		}
		/// <summary>
		/// [GET] The <see cref="System.Type"/> of the referenced Resource. If currently unavailable, this is determined by
		/// the Resource file path.
		/// </summary>
		public Type ResType
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return this.contentInstance.GetType();
				return Resource.GetTypeByFileName(this.contentPath);
			}
		}
		/// <summary>
		/// [GET / SET] The path where to look for the Resource, if it is currently unavailable.
		/// </summary>
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
		/// <summary>
		/// [GET] Returns whether this content reference has been explicitly set to null.
		/// </summary>
		public bool IsExplicitNull
		{
			get
			{
				return this.contentInstance == null && String.IsNullOrEmpty(this.contentPath);
			}
		}
		/// <summary>
		/// [GET] Returns whether this content reference is available in general. This may trigger loading it, if currently unavailable.
		/// </summary>
		public bool IsAvailable
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return true;
				this.RetrieveInstance();
				return this.contentInstance != null;
			}
		}
		/// <summary>
		/// [GET] Returns whether the referenced Resource is currently loaded.
		/// </summary>
		public bool IsLoaded
		{
			get
			{
				if (this.contentInstance != null && !this.contentInstance.Disposed) return true;
				return ContentProvider.IsContentRegistered(this.contentPath);
			}
		}
		/// <summary>
		/// [GET] Returns whether the referenced Resource is part of Duality's embedded default content.
		/// </summary>
		public bool IsDefaultContent
		{
			get { return this.contentPath != null && this.contentPath.Contains(':'); }
		}
		/// <summary>
		/// [GET] The name of the referenced Resource.
		/// </summary>
		public string Name
		{
			get
			{
				if (this.IsExplicitNull) return "null";
				string nameTemp = this.contentPath;
				if (this.IsDefaultContent) nameTemp = nameTemp.Replace(':', '/');
				return System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(nameTemp));
			}
		}

		/// <summary>
		/// Creates a ContentRef pointing to the specified <see cref="Resource"/>, assuming the
		/// specified path as its origin, if the Resource itsself is either null or doesn't
		/// provide a valid <see cref="Resource.Path"/>.
		/// </summary>
		/// <param name="res">The Resource to reference.</param>
		/// <param name="altPath">The referenced Resource's file path.</param>
		public ContentRef(T res, string altPath)
		{
			this.contentInstance = res;
			if (res != null && !String.IsNullOrEmpty(res.Path))
				this.contentPath = res.Path;
			else 
				this.contentPath = altPath;
		}
		/// <summary>
		/// Creates a ContentRef pointing to the specified <see cref="Resource"/>.
		/// </summary>
		/// <param name="res">The Resource to reference.</param>
		public ContentRef(T res)
		{
			this.contentInstance = res;
			this.contentPath = (res != null) ? res.Path : null;
		}
		
		/// <summary>
		/// Determines if the references Resource's Type is assignable to the specified Type.
		/// </summary>
		/// <param name="resType">The Resource Type in question.</param>
		/// <returns>True, if the referenced Resource is of the specified Type or subclassing it.</returns>
		public bool Is(Type resType)
		{
			return resType.IsAssignableFrom(this.ResType);
		}
		/// <summary>
		/// Determines if the references Resource's Type is assignable to the specified Type.
		/// </summary>
		/// <typeparam name="U">The Resource Type in question.</typeparam>
		/// <returns>True, if the referenced Resource is of the specified Type or subclassing it.</returns>
		public bool Is<U>() where U : Resource
		{
			return (this.contentInstance != null && !this.contentInstance.Disposed) ? 
				this.contentInstance is U : 
				typeof(U).IsAssignableFrom(this.ResType);
		}
		/// <summary>
		/// Creates a <see cref="ContentRef{T}"/> of the specified Type, referencing the same Resource.
		/// </summary>
		/// <typeparam name="U">The Resource Type to create a reference of.</typeparam>
		/// <returns>
		/// A <see cref="ContentRef{T}"/> of the specified Type, referencing the same Resource.
		/// Returns a <see cref="ContentRef{T}.Null">null reference</see> if the Resource is not assignable
		/// to the specified Type.
		/// </returns>
		public ContentRef<U> As<U>() where U : Resource
		{
			if (!Is<U>()) return ContentRef<U>.Null;
			return new ContentRef<U>(this.contentInstance as U, this.contentPath);
		}

		/// <summary>
		/// Loads the associated content as if it was accessed now.
		/// You don't usually need to call this method. It is invoked implicitly by trying to access the ContentRef/>
		/// </summary>
		public void MakeAvailable()
		{
			if (this.contentInstance == null || this.contentInstance.Disposed) this.RetrieveInstance();
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
		public override int GetHashCode()
		{
			if (this.contentInstance != null) return this.contentInstance.GetHashCode();
			else if (this.contentPath != null) return this.contentPath.GetHashCode();
			else return base.GetHashCode();
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

		/// <summary>
		/// Compares two ContentRefs for equality.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		/// <remarks>
		/// This is a two-step comparison. First, their actual Resources references are compared.
		/// If they're both not null and equal, true is returned. Otherwise, their Resource paths
		/// are compared for equality
		/// </remarks>
		public static bool operator ==(ContentRef<T> first, ContentRef<T> second)
		{
			if (first.contentInstance != null && second.contentInstance != null)
				return first.contentInstance == second.contentInstance;
			else
				return first.contentPath == second.contentPath;
		}
		/// <summary>
		/// Compares two ContentRefs for inequality.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static bool operator !=(ContentRef<T> first, ContentRef<T> second)
		{
			return !(first == second);
		}
	}

	/// <summary>
	/// <para>
	/// The ContentProvider is Duality's main instance for content management. If you need any kind of <see cref="Resource"/>,
	/// simply request it from the ContentProvider. It keeps track of which Resources are loaded and valid and prevents
	/// Resources from being loaded more than once at a time, thus reducing loading times and redundancy.
	/// </para>
	/// <para>
	/// You can also manually <see cref="RegisterContent">register Resources</see> that have been created at runtime 
	/// using a string alias of your choice.
	/// </para>
	/// </summary>
	/// <seealso cref="Resource"/>
	/// <seealso cref="ContentRef{T}"/>
	/// <seealso cref="IContentRef"/>
	public static class ContentProvider
	{
		/// <summary>
		/// (Virtual) base path for Duality's embedded default content.
		/// </summary>
		public const string	VirtualContentPath = "Default:";

		private	static	bool	defaultContentInitialized	= false;
		private	static	Dictionary<string,Resource>	resLibrary		= new Dictionary<string,Resource>();
		private	static	List<Resource>				defaultContent	= new List<Resource>();

		/// <summary>
		/// Initializes Dualitys embedded default content.
		/// </summary>
		public static void InitDefaultContent()
		{
			if (defaultContentInitialized) return;
			Log.Core.Write("Initializing default content..");
			Log.Core.PushIndent();

			var oldResLib = resLibrary.Values.ToArray();

			VertexShader.InitDefaultContent();
			FragmentShader.InitDefaultContent();
			ShaderProgram.InitDefaultContent();
			DrawTechnique.InitDefaultContent();
			Pixmap.InitDefaultContent();
			Texture.InitDefaultContent();
			Material.InitDefaultContent();
			Font.InitDefaultContent();
			AudioData.InitDefaultContent();
			Sound.InitDefaultContent();

			// Make a list of all default content available
			foreach (KeyValuePair<string,Resource> pair in resLibrary)
			{
				if (oldResLib.Contains(pair.Value)) continue;
				defaultContent.Add(pair.Value);
			}

			defaultContentInitialized = true;
			Log.Core.PopIndent();
			Log.Core.Write("..done");
		}
		/// <summary>
		/// Returns a list of all available embedded default content.
		/// </summary>
		/// <returns></returns>
		public static List<ContentRef<Resource>> GetAllDefaultContent()
		{
			return defaultContent.Select(r => new ContentRef<Resource>(r)).ToList();
		}
		/// <summary>
		/// Returns a list of all available content matching the specified Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<ContentRef<T>> GetAvailContent<T>() where T : Resource
		{
			return resLibrary.Values.OfType<T>().Where(r => !r.Disposed).Select(r => new ContentRef<T>(r)).ToList();
		}
		/// <summary>
		/// Returns a list of all available content matching the specified Type
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static List<IContentRef> GetAvailContent(Type t)
		{
			return resLibrary.Values.Where(r => t.IsAssignableFrom(r.GetType()) && !r.Disposed).Select(r => r.GetContentRef()).ToList();
		}
		/// <summary>
		/// Clears all non-default content.
		/// </summary>
		/// <param name="dispose">If true, unregistered content is also disposed.</param>
		public static void ClearContent(bool dispose = true)
		{
			var nonDefaultContent = resLibrary.Where(p => !p.Key.Contains(':')).ToArray();
			foreach (var pair in nonDefaultContent)
				UnregisterContent(pair.Key, dispose);
		}

		/// <summary>
		/// Registers a <see cref="Resource"/> and maps it to the specified path key.
		/// </summary>
		/// <param name="path">The path key to map the Resource to</param>
		/// <param name="content">The Resource to register.</param>
		public static void RegisterContent(string path, Resource content)
		{
			if (String.IsNullOrEmpty(path)) return;
			if (String.IsNullOrEmpty(content.Path)) content.Path = path;
			resLibrary[path] = content;
		}
		/// <summary>
		/// Returns whether or not there is any content currently registered under the specified path key.
		/// </summary>
		/// <param name="path">The path key to look for content</param>
		/// <returns>True, if there is content available for that path key, false if not.</returns>
		public static bool IsContentRegistered(string path)
		{
			if (String.IsNullOrEmpty(path)) return false;
			Resource res;
			return resLibrary.TryGetValue(path, out res) && !res.Disposed;
		}

		/// <summary>
		/// Unregisters content that has been registered using the specified path key.
		/// </summary>
		/// <param name="path">The path key to unregister.</param>
		/// <param name="dispose">If true, unregistered content is also disposed.</param>
		/// <returns>True, if the content has been found and successfully removed. False, if no</returns>
		public static bool UnregisterContent(string path, bool dispose = true)
		{
			if (String.IsNullOrEmpty(path)) return false;
			if (path.Contains(':')) return false;

			// Dispose cached content
			Resource res;
			if (dispose && resLibrary.TryGetValue(path, out res)) res.Dispose();

			return resLibrary.Remove(path);
		}
		/// <summary>
		/// Unregisters all content that has been registered using paths contained within
		/// the specified directory.
		/// </summary>
		/// <param name="dir">The directory to unregister</param>
		/// <param name="dispose">If true, unregistered content is also disposed.</param>
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
		/// <summary>
		/// Unregisters all content of the specified Type or subclassed Types.
		/// </summary>
		/// <typeparam name="T">The content Type to look for.</typeparam>
		/// <param name="dispose">If true, unregistered content is also disposed.</param>
		public static void UnregisterAllContent<T>(bool dispose = true) where T : Resource
		{
			var affectedContent = GetAvailContent<T>().Where(c => !c.IsDefaultContent);
			foreach (ContentRef<T> content in affectedContent)
				UnregisterContent(content.Path, dispose);
		}
		/// <summary>
		/// Unregisters all content of the specified Type or subclassed Types.
		/// </summary>
		/// <param name="t">The content Type to look for.</param>
		/// <param name="dispose">If true, unregistered content is also disposed.</param>
		public static void UnregisterAllContent(Type t, bool dispose = true)
		{
			var affectedContent = GetAvailContent(t).Where(c => !c.IsDefaultContent).ToArray();
			foreach (IContentRef content in affectedContent)
				UnregisterContent(content.Path, dispose);
		}
		internal static void UnregisterPluginContent(bool dispose = true)
		{
			foreach (Resource res in resLibrary.Values.ToArray())
			{
				if (res is Prefab || res.GetType().Assembly != typeof(ContentProvider).Assembly)
					UnregisterContent(res.Path, dispose);
			}
		}

		/// <summary>
		/// Changes the path key under which a specific Resource can be found.
		/// </summary>
		/// <param name="path">The Resources current path key.</param>
		/// <param name="newPath">The Resources new path key.</param>
		/// <returns>True, if the renaming operation was successful. False, if not.</returns>
		public static bool RenameContent(string path, string newPath)
		{
			if (String.IsNullOrEmpty(path)) return false;

			Resource res;
			if (resLibrary.TryGetValue(path, out res))
			{
				res.Path = newPath;
				resLibrary[newPath] = res;
				resLibrary.Remove(path);
				return true;
			}
			else
				return false;
		}
		/// <summary>
		/// Changes the path key under which a set of Resource can be found, i.e.
		/// renames all path keys located inside the specified directory.
		/// </summary>
		/// <param name="dir">The Resources current directory</param>
		/// <param name="newDir">The Resources new directory</param>
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

		/// <summary>
		/// Requests a <see cref="Resource"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The requested Resource type. Does not affect actual data, only the kind of <see cref="ContentRef{T}"/> that is obtained.
		/// </typeparam>
		/// <param name="path">
		/// The path key to identify the Resource. If there is no matching Resource available yet, the ContentProvider attemps
		/// to load a Resource from that path.
		/// </param>
		/// <returns>A <see cref="ContentRef{T}"/> to the requested Resource.</returns>
		public static ContentRef<T> RequestContent<T>(string path) where T : Resource
		{
			if (String.IsNullOrEmpty(path)) return null;

			// Return cached content
			Resource res;
			if (resLibrary.TryGetValue(path, out res) && !res.Disposed) return new ContentRef<T>(res as T, path);

			// Load new content
			return new ContentRef<T>(LoadContent(path) as T, path);
		}
		/// <summary>
		/// Requests a <see cref="Resource"/>.
		/// </summary>
		/// <param name="path">
		/// The path key to identify the Resource. If there is no matching Resource available yet, the ContentProvider attemps
		/// to load a Resource from that path.
		/// </param>
		/// <returns>A <see cref="IContentRef"/> to the requested Resource.</returns>
		public static IContentRef RequestContent(string path)
		{
			return RequestContent<Resource>(path);
		}

		private static Resource LoadContent(string path)
		{
			Resource res = Resource.LoadResource<Resource>(path);
			if (res != null) RegisterContent(path, res);
			return res;
		}
	}
}
