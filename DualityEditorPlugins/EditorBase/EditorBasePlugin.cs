using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Duality;
using Duality.ObjectManagers;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.ColorFormat;
using Duality.Resources;
using OpenTK;

using DualityEditor;
using DualityEditor.Forms;
using DualityEditor.EditorRes;

using EditorBase.PluginRes;

using WeifenLuo.WinFormsUI.Docking;

namespace EditorBase
{
	public class EditorBasePlugin : EditorPlugin
	{
		private	static	EditorBasePlugin	instance	= null;
		internal static EditorBasePlugin Instance
		{
			get { return instance; }
		}


		private	ProjectFolderView	projectView		= null;
		private	SceneView			sceneView		= null;
		private	ObjectInspector		objView			= null;
		private	List<CamView>		camViews		= new List<CamView>();
		private	bool				isLoading		= false;

		private	ToolStripMenuItem	menuItemProjectView	= null;
		private	ToolStripMenuItem	menuItemSceneView	= null;
		private	ToolStripMenuItem	menuItemObjView		= null;
		private	ToolStripMenuItem	menuItemCamView		= null;


		public override string Id
		{
			get { return "EditorBase"; }
		}


		public EditorBasePlugin()
		{
			instance = this;
		}
		public override IDockContent DeserializeDockContent(Type dockContentType)
		{
			this.isLoading = true;
			IDockContent result;
			if (dockContentType == typeof(CamView))
				result = this.RequestCamView();
			else if (dockContentType == typeof(ProjectFolderView))
				result = this.RequestProjectView();
			else if (dockContentType == typeof(SceneView))
				result = this.RequestSceneView();
			else if (dockContentType == typeof(ObjectInspector))
				result = this.RequestObjView();
			else
				result = base.DeserializeDockContent(dockContentType);
			this.isLoading = false;
			return result;
		}

		public override void SaveUserData(System.Xml.XmlDocument doc, System.Xml.XmlElement node)
		{
			for (int i = 0; i < this.camViews.Count; i++)
			{
				System.Xml.XmlElement camViewElem = doc.CreateElement("CamView_" + i);
				node.AppendChild(camViewElem);
				this.camViews[i].SaveUserData(camViewElem);
			}
		}
		public override void LoadUserData(System.Xml.XmlElement node)
		{
			this.isLoading = true;
			for (int i = 0; i < this.camViews.Count; i++)
			{
				System.Xml.XmlNodeList camViewElemQuery = node.GetElementsByTagName("CamView_" + i);
				if (camViewElemQuery.Count == 0) continue;

				System.Xml.XmlElement camViewElem = camViewElemQuery[0] as System.Xml.XmlElement;
				this.camViews[i].LoadUserData(camViewElem);
			}
			this.isLoading = false;
		}

		public override void LoadPlugin()
		{
			base.LoadPlugin();

			// Register core resource lookups
			CorePluginHelper.RegisterTypeImage(typeof(DrawTechnique), PluginRes.EditorBaseRes.IconResDrawTechnique, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(FragmentShader), PluginRes.EditorBaseRes.IconResFragmentShader, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Material), PluginRes.EditorBaseRes.IconResMaterial, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Pixmap), PluginRes.EditorBaseRes.IconResPixmap, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Prefab), PluginRes.EditorBaseRes.IconResPrefabFull, CorePluginHelper.ImageContext_Icon + "_Full");
			CorePluginHelper.RegisterTypeImage(typeof(Prefab), PluginRes.EditorBaseRes.IconResPrefabEmpty, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(RenderTarget), PluginRes.EditorBaseRes.IconResRenderTarget, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(ShaderProgram), PluginRes.EditorBaseRes.IconResShaderProgram, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Texture), PluginRes.EditorBaseRes.IconResTexture, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(VertexShader), PluginRes.EditorBaseRes.IconResVertexShader, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Scene), PluginRes.EditorBaseRes.IconResScene, CorePluginHelper.ImageContext_Icon);

			CorePluginHelper.RegisterTypeImage(typeof(GameObject), PluginRes.EditorBaseRes.IconGameObj, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(GameObject), PluginRes.EditorBaseRes.IconGameObjLink, CorePluginHelper.ImageContext_Icon + "_Link");
			CorePluginHelper.RegisterTypeImage(typeof(GameObject), PluginRes.EditorBaseRes.IconGameObjLinkBroken, CorePluginHelper.ImageContext_Icon + "_Link_Broken");
			CorePluginHelper.RegisterTypeImage(typeof(Component), PluginRes.EditorBaseRes.IconCmpUnknown, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(SpriteRenderer), PluginRes.EditorBaseRes.IconCmpSpriteRenderer, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Transform), PluginRes.EditorBaseRes.IconCmpTransform, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Camera), PluginRes.EditorBaseRes.IconCmpCamera, CorePluginHelper.ImageContext_Icon);

			CorePluginHelper.RegisterPropertyEditorProvider(new PropertyEditors.GameObjectPropertyEditorProvider());
		}
		public override void InitPlugin(MainForm main)
		{
			base.InitPlugin(main);

			// Request menus
			this.menuItemProjectView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_ProjectView));
			this.menuItemSceneView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_SceneView));
			this.menuItemObjView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_ObjView));
			this.menuItemCamView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_CamView));

			// Configure menus
			this.menuItemProjectView.Image = PluginRes.EditorBaseRes.IconProjectView.ToBitmap();
			this.menuItemProjectView.Click += new EventHandler(this.menuItemProjectView_Click);
			this.menuItemSceneView.Image = PluginRes.EditorBaseRes.IconSceneView.ToBitmap();
			this.menuItemSceneView.Click += new EventHandler(this.menuItemSceneView_Click);
			this.menuItemObjView.Image = PluginRes.EditorBaseRes.IconObjView.ToBitmap();
			this.menuItemObjView.Click += new EventHandler(this.menuItemObjView_Click);
			this.menuItemCamView.Image = PluginRes.EditorBaseRes.IconEye.ToBitmap();
			this.menuItemCamView.Click += new EventHandler(this.menuItemCamView_Click);
		}
		
		public ProjectFolderView RequestProjectView()
		{
			if (this.projectView == null || this.projectView.IsDisposed)
			{
				this.projectView = new ProjectFolderView();
				this.projectView.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.projectView = null; };
			}

			if (!this.isLoading)
			{
				this.projectView.Show(this.EditorForm.MainDockPanel);
				if (this.projectView.Pane != null)
				{
					this.projectView.Pane.Activate();
					this.projectView.Focus();
				}
			}

			return this.projectView;
		}
		public SceneView RequestSceneView()
		{
			if (this.sceneView == null || this.sceneView.IsDisposed)
			{
				this.sceneView = new SceneView();
				this.sceneView.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.sceneView = null; };
			}

			if (!this.isLoading)
			{
				this.sceneView.Show(this.EditorForm.MainDockPanel);
				if (this.sceneView.Pane != null)
				{
					this.sceneView.Pane.Activate();
					this.sceneView.Focus();
				}
			}

			return this.sceneView;
		}
		public ObjectInspector RequestObjView()
		{
			if (this.objView == null || this.objView.IsDisposed)
			{
				this.objView = new ObjectInspector();
				this.objView.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.objView = null; };
			}

			if (!this.isLoading)
			{
				this.objView.Show(this.EditorForm.MainDockPanel);
				if (this.objView.Pane != null)
				{
					this.objView.Pane.Activate();
					this.objView.Focus();
				}
			}

			return this.objView;
		}
		public CamView RequestCamView()
		{
			CamView cam = new CamView(this.camViews.Count);
			this.camViews.Add(cam);
			cam.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.camViews.Remove(sender as CamView); };

			if (!this.isLoading)
			{
				cam.Show(this.EditorForm.MainDockPanel);
				if (cam.Pane != null)
				{
					cam.Pane.Activate();
					cam.Focus();
				}
			}
			return cam;
		}

		private void menuItemProjectView_Click(object sender, EventArgs e)
		{
			this.RequestProjectView();
		}
		private void menuItemSceneView_Click(object sender, EventArgs e)
		{
			this.RequestSceneView();
		}
		private void menuItemObjView_Click(object sender, EventArgs e)
		{
			this.RequestObjView();
		}
		private void menuItemCamView_Click(object sender, EventArgs e)
		{
			this.RequestCamView();
		}
	}
}
