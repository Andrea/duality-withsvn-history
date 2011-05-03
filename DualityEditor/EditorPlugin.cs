using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.ObjectManagers;

using DualityEditor.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace DualityEditor
{
	public abstract class EditorPlugin
	{
		private	MainForm	editorForm	= null;


		public event EventHandler Init = null;


		/// <summary>
		/// The Plugins ID. Try to make this unique, it is used for serializing user data.
		/// </summary>
		public abstract string Id { get; }
		public MainForm EditorForm
		{
			get { return this.editorForm; }
		}

		/// <summary>
		/// This method is called as soon as the plugins assembly is loaded. Initializes the plugins internal data.
		/// </summary>
		/// <param name="main"></param>
		public virtual void LoadPlugin() {}
		/// <summary>
		/// This method is called when all plugins and the editors user data and layout are loaded. May initialize GUI.
		/// </summary>
		public virtual void InitPlugin(MainForm main)
		{
			this.editorForm = main;
			this.OnInit();
		}
		/// <summary>
		/// Saves the plugins user data to the provided Xml Node.
		/// </summary>
		/// <param name="node"></param>
		public virtual void SaveUserData(System.Xml.XmlDocument doc, System.Xml.XmlElement node) {}
		/// <summary>
		/// Loads the plugins user data from the provided Xml Node.
		/// </summary>
		/// <param name="node"></param>
		public virtual void LoadUserData(System.Xml.XmlElement node) {}
		/// <summary>
		/// Called when initializing the editors layout and trying to set up one of this plugins DockContent.
		/// Returns an IDockContent instance of the specified dockContentType. May return already existing
		/// DockContent, a new an pre-setup instance or null as default.
		/// </summary>
		/// <param name="dockContentType"></param>
		/// <returns></returns>
		public virtual IDockContent DeserializeDockContent(Type dockContentType) { return null; }

		private void OnInit()
		{
			if (this.Init != null)
				this.Init(this, null);
		}
	}
}
