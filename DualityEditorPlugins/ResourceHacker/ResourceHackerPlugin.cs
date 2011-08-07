using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Duality;
using Duality.Serialization;

using DualityEditor;
using DualityEditor.Forms;
using DualityEditor.EditorRes;

using WeifenLuo.WinFormsUI.Docking;

using ResourceHacker.PluginRes;

namespace ResourceHacker
{
	public class ResourceHackerPlugin : EditorPlugin
	{
		private	static	ResourceHackerPlugin	instance	= null;
		internal static ResourceHackerPlugin Instance
		{
			get { return instance; }
		}


		private	bool				isLoading			= false;
		private	ResourceHacker		resHacker			= null;
		private	ToolStripMenuItem	menuItemResHacker	= null;

		public override string Id
		{
			get { return "ResourceHacker"; }
		}


		public ResourceHackerPlugin()
		{
			instance = this;
		}
		public override IDockContent DeserializeDockContent(Type dockContentType)
		{
			this.isLoading = true;
			IDockContent result;
			if (dockContentType == typeof(ResourceHacker))
				result = this.RequestResourceHacker();
			else
				result = base.DeserializeDockContent(dockContentType);
			this.isLoading = false;
			return result;
		}
		public override void LoadPlugin()
		{
			base.LoadPlugin();
			
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.DataNode), PluginRes.ResourceHackerRes.IconPrimitive, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.ArrayNode), PluginRes.ResourceHackerRes.IconArray, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.StructNode), PluginRes.ResourceHackerRes.IconObject, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.ObjectRefNode), PluginRes.ResourceHackerRes.IconObjectRef, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.TypeInfoNode), PluginRes.ResourceHackerRes.IconClass, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.TypeDataLayoutNode), PluginRes.ResourceHackerRes.IconClass, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.ConstructorInfoNode), PluginRes.ResourceHackerRes.IconMethod, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.MethodInfoNode), PluginRes.ResourceHackerRes.IconMethod, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.PropertyInfoNode), PluginRes.ResourceHackerRes.IconProperty, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.DelegateNode), PluginRes.ResourceHackerRes.IconDelegate, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.EventInfoNode), PluginRes.ResourceHackerRes.IconEvent, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(BinaryMetaFormatter.FieldInfoNode), PluginRes.ResourceHackerRes.IconField, CorePluginHelper.ImageContext_Icon);

			// Register PropertyEditor provider
			CorePluginHelper.RegisterPropertyEditorProvider(new PropertyEditors.PropertyEditorProvider());
		}
		public override void InitPlugin(MainForm main)
		{
			base.InitPlugin(main);

			// Request menus
			this.menuItemResHacker = main.RequestMenu(Path.Combine(GeneralRes.MenuName_Tools, ResourceHackerRes.MenuItemName_ResourceHacker));

			// Configure menus
			this.menuItemResHacker.Image = ResourceHackerRes.IconResourceHacker;
			this.menuItemResHacker.Click += new EventHandler(this.menuItemResHacker_Click);
		}

		public ResourceHacker RequestResourceHacker()
		{
			if (this.resHacker == null || this.resHacker.IsDisposed)
			{
				this.resHacker = new ResourceHacker();
				this.resHacker.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.resHacker = null; };
			}

			if (!this.isLoading)
			{
				this.resHacker.Show(this.EditorForm.MainDockPanel);
				if (this.resHacker.Pane != null)
				{
					this.resHacker.Pane.Activate();
					this.resHacker.Focus();
				}
			}

			return this.resHacker;
		}

		private void menuItemResHacker_Click(object sender, EventArgs e)
		{
			this.RequestResourceHacker();
		}
	}
}
