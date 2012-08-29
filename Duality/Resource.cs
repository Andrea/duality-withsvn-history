﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Reflection;

using Duality.Serialization;
using Duality.EditorHints;
using Duality.Cloning;

using ICloneable = Duality.Cloning.ICloneable;

namespace Duality
{
	/// <summary>
	/// The abstract Resource class is inherited by any kind of Duality content. Instances of it or one of its subclasses
	/// are usually handled wrapped inside a <see cref="ContentRef{T}"/> and requested from the <see cref="ContentProvider"/>.
	/// </summary>
	/// <seealso cref="ContentRef{T}"/>
	/// <seealso cref="ContentProvider"/>
	[Serializable]
	public abstract class Resource : IManageableObject, IDisposable, ICloneable
	{
		/// <summary>
		/// A Resource files extension.
		/// </summary>
		public const string FileExt = ".res";

		private	static	List<Resource>	finalizeSched	= new List<Resource>();

		public static event EventHandler<ResourceEventArgs>	ResourceDisposed = null;
		public static event EventHandler<ResourceEventArgs>	ResourceLoaded = null;
		public static event EventHandler<ResourceEventArgs>	ResourceSaved = null;
		
		/// <summary>
		/// The path of the file from which the Resource has been originally imported or initialized.
		/// </summary>
		protected	string	sourcePath	= null;
		/// <summary>
		/// The path of this Resource.
		/// </summary>
		[NonSerialized]	protected	string		path		= null;
		[NonSerialized]	private		InitState	initState	= InitState.Initialized;

		/// <summary>
		/// [GET] Returns whether the Resource has been disposed. 
		/// Disposed Resources are not to be used and are treated the same as a null value by most methods.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public bool Disposed
		{
			get { return this.initState == InitState.Disposed; }
		}
		/// <summary>
		/// [GET] The path where this Resource has been originally loaded from or was first saved to.
		/// It is also the path under which this Resource is registered at the ContentProvider.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public string Path
		{
			get { return this.path; }
			internal set { this.path = value; }
		}
		/// <summary>
		/// [GET / SET] The path of the file from which the Resource has been originally imported or initialized.
		/// Setting this does not affect the Resource in any way.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public string SourcePath
		{
			get { return this.sourcePath; }
			set { this.sourcePath = value; }
		}
		/// <summary>
		/// [GET] The name of the Resource.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public string Name
		{
			get
			{
				if (this.IsRuntimeResource) return this.GetHashCode().ToString(CultureInfo.InvariantCulture);
				string nameTemp = this.path;
				if (this.IsDefaultContent) nameTemp = nameTemp.Replace(':', '/');
				return System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(nameTemp));
			}
		}
		/// <summary>
		/// [GET] The full name of the Resource, including its path but not its file extension
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public string FullName
		{
			get
			{
				if (this.IsRuntimeResource) return this.GetHashCode().ToString(CultureInfo.InvariantCulture);
				string nameTemp = this.path;
				if (this.IsDefaultContent) nameTemp = nameTemp.Replace(':', '/');
				return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(nameTemp), this.Name);
			}
		}
		/// <summary>
		/// [GET] Returns whether the Resource is part of Duality's embedded default content.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public bool IsDefaultContent
		{
			get { return this.path != null && this.path.Contains(':'); }
		}
		/// <summary>
		/// [GET] Returns whether the Resource has been generated at runtime and  cannot be retrieved via content path.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public bool IsRuntimeResource
		{
			get { return string.IsNullOrEmpty(this.path); }
		}
		bool IManageableObject.Active
		{
			get { return !this.Disposed; }
		}

		/// <summary>
		/// Saves the Resource to the specified path. If it has been generated at runtime, i.e. has
		/// not been loaded from file before, this will set the Resources <see cref="Path"/> Property.
		/// </summary>
		/// <param name="saveAsPath">The path to which this Resource is saved to. If null, the Resources <see cref="Path"/> is used as destination.</param>
		public void Save(string saveAsPath = null)
		{
			if (this.initState != InitState.Initialized) throw new ApplicationException("Can't save Ressource that already has been disposed.");
			if (saveAsPath == null) saveAsPath = this.path;

#if DEBUG
			if (!PathHelper.IsPathLocatedIn(saveAsPath, "Data"))
			{
				Log.Editor.WriteWarning("Saving Resource '{0}' outside of data folder.. is this really intended? Target path: {1}", this.FullName, saveAsPath);
				if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
			}
#endif

			// We're saving a new Ressource for the first time: Register it in the library
			if (this.path == null)
			{
				this.path = saveAsPath;
				ContentProvider.RegisterContent(this.path, this);
			}

			string dirName = System.IO.Path.GetDirectoryName(saveAsPath);
			if (!string.IsNullOrEmpty(dirName) && !Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
			using (FileStream str = File.Open(saveAsPath, FileMode.Create))
			{
				this.Save(str);
			}
		}
		/// <summary>
		/// Saves the Resource to the specified stream.
		/// </summary>
		/// <param name="str"></param>
		public void Save(Stream str)
		{
			this.OnSaving();
			using (var formatter = Formatter.Create(str))
			{
				formatter.AddFieldBlocker(NonSerializedResourceBlocker);
				formatter.WriteObject(this);
			}
			this.OnSaved();

			string streamName;
			if (str is FileStream)
			{
				FileStream fileStream = str as FileStream;
				if (PathHelper.IsPathLocatedIn(fileStream.Name, "."))
					streamName = PathHelper.MakeFilePathRelative(fileStream.Name, ".");
				else
					streamName = fileStream.Name;
			}
			else
				streamName = str.ToString();
			Log.Core.Write("Resource saved: {0}", streamName);
		}

		/// <summary>
		/// Creates a deep copy of this Resource.
		/// </summary>
		/// <returns></returns>
		public Resource Clone()
		{
			return CloneProvider.DeepClone(this);
		}
		/// <summary>
		/// Deep-copies this Resource to the specified target Resource. The target Resource's Type must
		/// match this Resource's Type.
		/// </summary>
		/// <param name="r">The target Resource to copy this Resource's data to</param>
		public void CopyTo(Resource r)
		{
			CloneProvider.DeepCopyTo(this, r);
		}
		object ICloneable.CreateTargetObject(CloneProvider provider)
		{
			return this.GetType().CreateInstanceOf();
		}
		void ICloneable.CopyDataTo(object targetObj, CloneProvider provider)
		{
			this.OnCopyTo(targetObj as Resource, provider);
		}

		/// <summary>
		/// Deep-copies this Resource to the specified target Resource. The target Resource's Type must
		/// match this Resource's Type.
		/// </summary>
		/// <param name="r">The target Resource to copy this Resource's data to</param>
		protected virtual void OnCopyTo(Resource r, CloneProvider provider)
		{
			r.path			= null; //this.path;
			r.sourcePath	= null;
		}
		/// <summary>
		/// Called when this Resource is now beginning to be saved.
		/// </summary>
		protected virtual void OnSaving() {}
		/// <summary>
		/// Called when this Resource has just been saved.
		/// </summary>
		protected virtual void OnSaved()
		{
			if (ResourceSaved != null)
				ResourceSaved(this, new ResourceEventArgs(this));
		}
		/// <summary>
		/// Called when this Resource has just been loaded.
		/// </summary>
		protected virtual void OnLoaded()
		{
			if (ResourceLoaded != null)
				ResourceLoaded(this, new ResourceEventArgs(this));
		}

		~Resource()
		{
			finalizeSched.Add(this);
		}
		/// <summary>
		/// Disposes the Resource.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool manually)
		{
			if (this.initState == InitState.Initialized)
			{
				this.initState = InitState.Disposing;
				this.OnDisposing(manually);
				this.initState = InitState.Disposed;
			}
		}
		/// <summary>
		/// Called when beginning to dispose the Resource.
		/// </summary>
		/// <param name="manually"></param>
		protected virtual void OnDisposing(bool manually)
		{
			if (ResourceDisposed != null)
				ResourceDisposed(this, new ResourceEventArgs(this));
		}

		/// <summary>
		/// Creates a <see cref="ContentRef{T}"/> referring to this Resource.
		/// </summary>
		/// <returns>A <see cref="ContentRef{T}"/> referring to this Resource.</returns>
		public IContentRef GetContentRef()
		{
			Type refType = typeof(ContentRef<>).MakeGenericType(this.GetType());
			return Activator.CreateInstance(refType, this) as IContentRef;
		}
		
		public override string ToString()
		{
			return string.Format("{0} \"{1}\"", this.GetType().Name, this.FullName);
		}

		/// <summary>
		/// Loads the Resource that is located at the specified path. You usually don't need this method. 
		/// Consider requesting the Resource from the <see cref="ContentProvider"/> instead.
		/// </summary>
		/// <typeparam name="T">
		/// Desired Type of the returned reference. Does not affect the loaded Resource in any way - it is simply returned as T.
		/// Results in returning null if the loaded Resource's Type isn't assignable to T.
		/// </typeparam>
		/// <param name="path">The path to load the Resource from.</param>
		/// <param name="loadCallback">An optional callback that is invoked right after loading the Resource, but before initializing it.</param>
		/// <returns>The Resource that has been loaded.</returns>
		public static T LoadResource<T>(string path, Action<T> loadCallback = null) where T : Resource
		{
			if (!File.Exists(path)) return null;

			T newContent;
			using (FileStream str = File.OpenRead(path))
			{
				newContent = LoadResource<T>(str, path, loadCallback);
			}
			return newContent;
		}
		/// <summary>
		/// Loads the Resource from the specified <see cref="Stream"/>. You usually don't need this method. 
		/// Consider requesting the Resource from the <see cref="ContentProvider"/> instead.
		/// </summary>
		/// <typeparam name="T">
		/// Desired Type of the returned reference. Does not affect the loaded Resource in any way - it is simply returned as T.
		/// Results in returning null if the loaded Resource's Type isn't assignable to T.
		/// </typeparam>
		/// <param name="str">The stream to load the Resource from.</param>
		/// <param name="resPath">The path that is assumed as the loaded Resource's origin.</param>
		/// <param name="loadCallback">An optional callback that is invoked right after loading the Resource, but before initializing it.</param>
		/// <returns>The Resource that has been loaded.</returns>
		public static T LoadResource<T>(Stream str, string resPath = null, Action<T> loadCallback = null) where T : Resource
		{
			Log.Core.Write("Loading Ressource '{0}'...", resPath);
			Log.Core.PushIndent();

			T newContent = null;
			try
			{
				Resource res;
				using (var formatter = Formatter.Create(str))
				{
					res = formatter.ReadObject() as Resource;
				}
				if (res == null) throw new ApplicationException("Loading Resource failed");

				res.path = resPath;
				if (loadCallback != null) loadCallback(res as T); // Callback before initializing.
				res.OnLoaded();
				newContent = res as T;
			}
			catch (Exception e)
			{
				Log.Core.WriteError("Can't load {0} from Stream '{1}', because an error occured: \n{2}",
					Log.Type(typeof(T)),
					(str is FileStream) ? (str as FileStream).Name : str.ToString(),
					Log.Exception(e));
			}

			Log.Core.PopIndent();
			return newContent;
		}

		/// <summary>
		/// Determines whether or not the specified path points to a Duality Resource file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static bool IsResourceFile(string filePath)
		{
			return System.IO.Path.GetExtension(filePath).ToLower() == FileExt;
		}
		/// <summary>
		/// Returns all Resource files that are located in the specified directory. This doesn't affect
		/// any actual content- or load states.
		/// </summary>
		/// <param name="folderPath"></param>
		/// <returns></returns>
		public static List<string> GetResourceFiles(string folderPath = null)
		{
			if (string.IsNullOrEmpty(folderPath)) folderPath = "Data";
			List<string> result = new List<string>();
			foreach (string file in Directory.GetFiles(folderPath, "*" + Resource.FileExt, SearchOption.AllDirectories))
			{
				if (Resource.IsResourceFile(file))
					result.Add(file);
			}
			return result;
		}
		/// <summary>
		/// Returns the Resource file extension for a specific Resource Type.
		/// </summary>
		/// <param name="resType">The Resource Type to return the file extension from.</param>
		/// <returns>The specified Resource Type's file extension.</returns>
		public static string GetFileExtByType(Type resType)
		{
			return "." + resType.Name + FileExt;
		}
		/// <summary>
		/// Returns the Resource Type that is associated with the specified file, based on its extension.
		/// </summary>
		/// <param name="filePath">Path to the file of whichs Resource Type will be returned</param>
		/// <returns>The Resource Type of the specified file</returns>
		public static Type GetTypeByFileName(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || filePath.Contains(':')) return null;
			filePath = System.IO.Path.GetFileNameWithoutExtension(filePath);
			string[] token = filePath.Split('.');
			if (token.Length < 2) return null;
			return DualityApp.GetAvailDualityTypes(typeof(Resource)).FirstOrDefault(t => t.Name == token[token.Length - 1]);
		}

		/// <summary>
		/// A <see cref="Duality.Serialization.Formatter.FieldBlockers">FieldBlocker</see> to prevent
		/// fields flagged with a <see cref="NonSerializedResourceAttribute"/> from being serialized.
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public static bool NonSerializedResourceBlocker(FieldInfo field)
		{
			return field.GetCustomAttributes(typeof(NonSerializedResourceAttribute), true).Any();
		}

		internal static void RunCleanup()
		{
			while (finalizeSched.Count > 0)
			{
				if (finalizeSched[finalizeSched.Count - 1] != null)
					finalizeSched[finalizeSched.Count - 1].Dispose(false);
				finalizeSched.RemoveAt(finalizeSched.Count - 1);
			}
		}
	}

	/// <summary>
	/// Indicates that a field will be assumed null when serializing it as part of a Resource serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class NonSerializedResourceAttribute : Attribute
	{
	}
}
