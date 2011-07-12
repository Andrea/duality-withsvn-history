using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

			using (FileStream str = File.Open(saveAsPath, FileMode.Create))
			{
				this.Save(str);
			}
		}
		public void Save(Stream str)
		{
			this.OnSaving();
			BinaryFormatter formatter = DualityApp.RequestSerializer(null, new StreamingContext(
				StreamingContextStates.File | StreamingContextStates.Persistence));
			formatter.Serialize(str, this);
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
				this.disposed = true;
				this.OnDisposed(manually);
			}
		}
		protected virtual void OnDisposed(bool manually)
		{

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
				BinaryFormatter formatter = DualityApp.RequestSerializer(null, new StreamingContext(
					StreamingContextStates.File | StreamingContextStates.Persistence));
				newContent = formatter.Deserialize(str) as T;

				//if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
				SerializationHelper.DeepResolveTypeReferences(newContent, DualityApp.PluginTypeBinder);

				if (newContent != null) newContent.path = resPath;
				newContent.OnLoaded();
			}
			catch (SerializationException)
			{
				Log.Core.WriteError("Can't load {0} from Stream '{1}'",
					ReflectionHelper.GetTypeString(typeof(T), ReflectionHelper.TypeStringAttrib.CSCodeIdentShort),
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

		internal static void RunCleanup()
		{
			while (finalizeSched.Count > 0)
			{
				finalizeSched[finalizeSched.Count - 1].Dispose(false);
				finalizeSched.RemoveAt(finalizeSched.Count - 1);
			}
		}
	}
}
