using System;
using System.Linq;
using System.Reflection;

namespace Duality
{
	public abstract class CorePlugin
	{
		private	bool		disposed	= false;
		private	Assembly	assembly	= null;
		private	string		asmName		= null;

		public bool Disposed
		{
			get { return this.disposed; }
		}
		public Assembly PluginAssembly
		{
			get { return this.assembly; }
		}
		public string AssemblyName
		{
			get { return this.asmName; }
		}

		protected CorePlugin()
		{
			this.assembly = this.GetType().Assembly;
			this.asmName = this.assembly.FullName.Split(',')[0];
		}
		internal void Dispose()
		{
			if (this.disposed) return;

			this.OnDisposePlugin();

			Type[] oldResTypes = (
				from t in this.assembly.GetExportedTypes()
				where typeof(Resource).IsAssignableFrom(t)
				select t).ToArray();
			foreach (Type oldResType in oldResTypes) ContentProvider.UnregisterAllContent(oldResType);

			this.disposed = true;
		}
		/// <summary>
		/// Called when initializing the plugin.
		/// </summary>
		internal protected virtual void InitPlugin() {}
		/// <summary>
		/// Called when unloading / disposing the plugin.
		/// </summary>
		protected virtual void OnDisposePlugin() {}
		/// <summary>
		/// Called before Duality updates the game scene
		/// </summary>
		internal protected virtual void OnBeforeUpdate() {}
		/// <summary>
		/// Called after Duality updates the game scene
		/// </summary>
		internal protected virtual void OnAfterUpdate() {}
		/// <summary>
		/// Called when Dualitys <see cref="DualityApp.ExecutionContext"/> changes.
		/// </summary>
		internal protected virtual void OnExecContextChanged(DualityApp.ExecutionContext previousContext) {}
	}
}
