using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Duality.Serialization;

namespace Duality
{
	[Serializable]
	public abstract class Resource : IManageableObject, IDisposable
	{
		public const string FileExt = ".res";

		private	static	List<Resource>	finalizeSched	= new List<Resource>();

		[NonSerialized]	protected	string	path		= null;
		[NonSerialized]	private		bool	disposed	= false;

		public bool Disposed
		{
			get { return this.disposed; }
		}
		public string Path
		{
			get { return this.path; }
		}
		bool IManageableObject.Active
		{
			get { return !this.Disposed; }
		}

		internal void ChangePath(string newPath)
		{
			this.path = newPath;
		}
		public void Save(string saveAsPath = null)
		{
			if (this.disposed) throw new ApplicationException("Can't save Ressource that already has been disposed.");
			if (saveAsPath == null) saveAsPath = this.path;

			// We're saving a new Ressource for the first time: Register it in the library
			if (this.path == null)
			{
				this.path = saveAsPath;
				ContentProvider.RegisterContent(this.path, this);
			}

			string dirName = System.IO.Path.GetDirectoryName(saveAsPath);
			if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
			using (FileStream str = File.Open(saveAsPath, FileMode.Create))
			{
				this.Save(str);
			}
		}
		public void Save(Stream str)
		{
			this.OnSaving();
			BinaryFormatter formatter = DualityApp.RequestSerializer(str);
			formatter.AddFieldBlocker(NonSerializedResourceBlocker);
			formatter.WriteObject(this);
			this.OnSaved();
		}

		public Resource Clone()
		{
			Resource r = ReflectionHelper.CreateInstanceOf(this.GetType()) as Resource;
			this.CopyTo(r);
			return r;
		}
		public virtual void CopyTo(Resource r)
		{
			r.path	= this.path;
		}

		protected virtual void OnSaving() {}
		protected virtual void OnSaved() {}
		protected virtual void OnLoaded() {}

		~Resource()
		{
			finalizeSched.Add(this);
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool manually)
		{
			if (!this.disposed)
			{
				this.OnDisposing(manually);
				this.disposed = true;
			}
		}
		protected virtual void OnDisposing(bool manually)
		{

		}

		public IContentRef GetContentRef()
		{
			Type refType = typeof(ContentRef<>).MakeGenericType(this.GetType());
			return Activator.CreateInstance(refType, this) as IContentRef;
		}

		public static T LoadResource<T>(string path) where T : Resource
		{
			if (!File.Exists(path)) return null;

			T newContent = null;
			using (FileStream str = File.OpenRead(path))
			{
				newContent = LoadResource<T>(str, path);
			}
			return newContent;
		}
		public static T LoadResource<T>(Stream str, string resPath = null) where T : Resource
		{
			T newContent = null;
			try
			{
				BinaryFormatter formatter = DualityApp.RequestSerializer(str);
				Resource res = formatter.ReadObject() as Resource;
				if (res == null) throw new ApplicationException("Loading Resource failed");

				res.path = resPath;
				res.OnLoaded();
				newContent = res as T;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				Log.Core.WriteError("Can't load {0} from Stream '{1}'",
					ReflectionHelper.GetTypeName(typeof(T), TypeNameFormat.CSCodeIdentShort),
					(str is FileStream) ? (str as FileStream).Name : str.ToString());
			}
			return newContent;
		}

		public static string GetFileExtByType(Type resType)
		{
			return resType.Name;
		}
		public static Type GetTypeByFileName(string filePath)
		{
			if (filePath == null || filePath.Contains(':')) return null;
			filePath = System.IO.Path.GetFileNameWithoutExtension(filePath);
			string[] token = filePath.Split('.');
			if (token.Length < 2) return null;
			return DualityApp.FindDualityRessourceType(token[token.Length - 1]);
		}

		protected static bool NonSerializedResourceBlocker(FieldInfo field)
		{
			return field.GetCustomAttributes(typeof(NonSerializedResourceAttribute), true).Any();
		}

		internal static void RunCleanup()
		{
			while (finalizeSched.Count > 0)
			{
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
		public NonSerializedResourceAttribute() {}
	}
}
