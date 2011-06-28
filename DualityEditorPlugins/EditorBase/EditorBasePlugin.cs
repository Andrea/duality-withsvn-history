using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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
		private	ResourceInspector	resView			= null;
		private	List<CamView>		camViews		= new List<CamView>();
		private	bool				isLoading		= false;

		private	ToolStripMenuItem	menuItemProjectView	= null;
		private	ToolStripMenuItem	menuItemSceneView	= null;
		private	ToolStripMenuItem	menuItemObjView		= null;
		private	ToolStripMenuItem	menuItemResView		= null;
		private	ToolStripMenuItem	menuItemCamView		= null;
		private	ToolStripMenuItem	menuItemAppData		= null;
		private	ToolStripMenuItem	menuItemUserData	= null;


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
			else if (dockContentType == typeof(ResourceInspector))
				result = this.RequestResView();
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

			// Initialize main OpenGL context
			CamView.InitMainContext();
			ContentProvider.InitDefaultContent();

			// Register core resource lookups
			CorePluginHelper.RegisterTypeImage(typeof(DrawTechnique), EditorBaseRes.IconResDrawTechnique, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(FragmentShader), EditorBaseRes.IconResFragmentShader, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Material), EditorBaseRes.IconResMaterial, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Pixmap), EditorBaseRes.IconResPixmap, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Prefab), EditorBaseRes.IconResPrefabFull, CorePluginHelper.ImageContext_Icon + "_Full");
			CorePluginHelper.RegisterTypeImage(typeof(Prefab), EditorBaseRes.IconResPrefabEmpty, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(RenderTarget), EditorBaseRes.IconResRenderTarget, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(ShaderProgram), EditorBaseRes.IconResShaderProgram, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Texture), EditorBaseRes.IconResTexture, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(VertexShader), EditorBaseRes.IconResVertexShader, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Scene), EditorBaseRes.IconResScene, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(AudioData), EditorBaseRes.IconResAudioData, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Sound), EditorBaseRes.IconResSound, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Font), EditorBaseRes.IconResFont, CorePluginHelper.ImageContext_Icon);

			CorePluginHelper.RegisterTypeImage(typeof(GameObject), EditorBaseRes.IconGameObj, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(GameObject), EditorBaseRes.IconGameObjLink, CorePluginHelper.ImageContext_Icon + "_Link");
			CorePluginHelper.RegisterTypeImage(typeof(GameObject), EditorBaseRes.IconGameObjLinkBroken, CorePluginHelper.ImageContext_Icon + "_Link_Broken");
			CorePluginHelper.RegisterTypeImage(typeof(Component), EditorBaseRes.IconCmpUnknown, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(SpriteRenderer), EditorBaseRes.IconCmpSpriteRenderer, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(AnimSpriteRenderer), EditorBaseRes.IconCmpSpriteRenderer, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Transform), EditorBaseRes.IconCmpTransform, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Camera), EditorBaseRes.IconCmpCamera, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(SoundEmitter), EditorBaseRes.IconResSound, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(SoundListener), EditorBaseRes.IconCmpSoundListener, CorePluginHelper.ImageContext_Icon);

			CorePluginHelper.RegisterTypeCategory(typeof(Transform), "", CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(SpriteRenderer), EditorBaseRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(AnimSpriteRenderer), EditorBaseRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(Camera), EditorBaseRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(SoundEmitter), EditorBaseRes.Category_Sound, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(SoundListener), EditorBaseRes.Category_Sound, CorePluginHelper.CategoryContext_General);

			// Register conversion actions
			CorePluginHelper.RegisterEditorAction<Pixmap>(
				EditorBaseRes.ActionName_CreateTexture, 
				EditorBaseRes.IconResTexture,
				this.ActionPixmapCreateTexture, 
				CorePluginHelper.ActionContext_ContextMenu);
			CorePluginHelper.RegisterEditorAction<Texture>(
				EditorBaseRes.ActionName_CreateMaterial, 
				EditorBaseRes.IconResMaterial,
				this.ActionTextureCreateMaterial, 
				CorePluginHelper.ActionContext_ContextMenu);
			CorePluginHelper.RegisterEditorAction<AudioData>(
				EditorBaseRes.ActionName_CreateSound, 
				EditorBaseRes.IconResSound,
				this.ActionAudioDataCreateSound, 
				CorePluginHelper.ActionContext_ContextMenu);

			// Register open actions
			CorePluginHelper.RegisterEditorAction<Pixmap>(null, null, this.ActionPixmapOpenRes, CorePluginHelper.ActionContext_OpenRes);
			CorePluginHelper.RegisterEditorAction<AudioData>(null, null, this.ActionAudioDataOpenRes, CorePluginHelper.ActionContext_OpenRes);
			CorePluginHelper.RegisterEditorAction<AbstractShader>(null, null, this.ActionAbstractShaderOpenRes, CorePluginHelper.ActionContext_OpenRes);
			CorePluginHelper.RegisterEditorAction<Prefab>(null, null, this.ActionPrefabOpenRes, CorePluginHelper.ActionContext_OpenRes);
			CorePluginHelper.RegisterEditorAction<Scene>(null, null, this.ActionSceneOpenRes, CorePluginHelper.ActionContext_OpenRes);

			// Register PropertyEditor provider
			CorePluginHelper.RegisterPropertyEditorProvider(new PropertyEditors.PropertyEditorProvider());
		}
		public override void InitPlugin(MainForm main)
		{
			base.InitPlugin(main);

			// Request menus
			this.menuItemProjectView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_ProjectView));
			this.menuItemSceneView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_SceneView));
			this.menuItemObjView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_ObjView));
			this.menuItemResView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_ResView));
			this.menuItemCamView = main.RequestMenu(Path.Combine(GeneralRes.MenuName_View, EditorBaseRes.MenuItemName_CamView));
			this.menuItemAppData = main.RequestMenu(Path.Combine(GeneralRes.MenuName_Settings, EditorBaseRes.MenuItemName_AppData));
			this.menuItemUserData = main.RequestMenu(Path.Combine(GeneralRes.MenuName_Settings, EditorBaseRes.MenuItemName_UserData));

			// Configure menus
			this.menuItemProjectView.Image = EditorBaseRes.IconProjectView.ToBitmap();
			this.menuItemSceneView.Image = EditorBaseRes.IconSceneView.ToBitmap();
			this.menuItemObjView.Image = EditorBaseRes.IconObjView.ToBitmap();
			this.menuItemResView.Image = EditorBaseRes.IconResView.ToBitmap();
			this.menuItemCamView.Image = EditorBaseRes.IconEye.ToBitmap();

			this.menuItemProjectView.Click += new EventHandler(this.menuItemProjectView_Click);
			this.menuItemSceneView.Click += new EventHandler(this.menuItemSceneView_Click);
			this.menuItemObjView.Click += new EventHandler(this.menuItemObjView_Click);
			this.menuItemResView.Click += new EventHandler(this.menuItemResView_Click);
			this.menuItemCamView.Click += new EventHandler(this.menuItemCamView_Click);
			this.menuItemAppData.Click += new EventHandler(this.menuItemAppData_Click);
			this.menuItemUserData.Click += new EventHandler(this.menuItemUserData_Click);

			// Register file importers
			main.RegisterFileImporter(new PixmapFileImporter());
			main.RegisterFileImporter(new AudioDataFileImporter());
			main.RegisterFileImporter(new ShaderFileImporter());
			main.RegisterFileImporter(new FontFileImporter());
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
		public ResourceInspector RequestResView()
		{
			if (this.resView == null || this.resView.IsDisposed)
			{
				this.resView = new ResourceInspector();
				this.resView.FormClosed += delegate(object sender, FormClosedEventArgs e) { this.objView = null; };
			}

			if (!this.isLoading)
			{
				this.resView.Show(this.EditorForm.MainDockPanel);
				if (this.resView.Pane != null)
				{
					this.resView.Pane.Activate();
					this.resView.Focus();
				}
			}

			return this.resView;
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
		private void menuItemResView_Click(object sender, EventArgs e)
		{
			this.RequestResView();
		}
		private void menuItemCamView_Click(object sender, EventArgs e)
		{
			this.RequestCamView();
		}
		private void menuItemAppData_Click(object sender, EventArgs e)
		{
			this.EditorForm.Select(this, new ObjectSelection(DualityApp.AppData));
		}
		private void menuItemUserData_Click(object sender, EventArgs e)
		{
			this.EditorForm.Select(this, new ObjectSelection(DualityApp.UserData));
		}

		private void ActionPixmapCreateTexture(Pixmap pixmap)
		{
			string pathExt = Pixmap.FileExt;
			string texPath = PathHelper.GetFreePathName(pixmap.Path.Substring(0, pixmap.Path.Length - pathExt.Length), Texture.FileExt);
			Texture tex = new Texture(pixmap);
			tex.Save(texPath);
		}
		private void ActionTextureCreateMaterial(Texture tex)
		{
			string pathExt = Texture.FileExt;
			string matPath = PathHelper.GetFreePathName(tex.Path.Substring(0, tex.Path.Length - pathExt.Length), Material.FileExt);
			Material mat = new Material(DrawTechnique.Mask, ColorRGBA.White, tex);
			mat.Save(matPath);
		}
		private void ActionAudioDataCreateSound(AudioData data)
		{
			string pathExt = AudioData.FileExt;
			string sndPath = PathHelper.GetFreePathName(data.Path.Substring(0, data.Path.Length - pathExt.Length), Sound.FileExt);
			Sound snd = new Sound(data);
			snd.Save(sndPath);
		}

		private void ActionPixmapOpenRes(Pixmap pixmap)
		{
			if (pixmap == null) return;
			EditorHelper.OpenResourceSrcFile(pixmap, ".png", pixmap.PixelDataBasePath, pixmap.SavePixelData);
		}
		private void ActionAudioDataOpenRes(AudioData audio)
		{
			if (audio == null) return;
			EditorHelper.OpenResourceSrcFile(audio, ".ogg", audio.OggVorbisDataBasePath, audio.SaveOggVorbisData);
		}
		private void ActionAbstractShaderOpenRes(AbstractShader shader)
		{
			if (shader == null) return;
			EditorHelper.OpenResourceSrcFile(shader, shader is FragmentShader ? ".frag" : ".vert", shader.SourcePath, shader.SaveSource);
		}
		private void ActionPrefabOpenRes(Prefab prefab)
		{
			GameObject newObj = prefab.Instantiate();
			Duality.Resources.Scene.Current.Graph.RegisterObjDeep(newObj);
			EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(newObj));
		}
		private void ActionSceneOpenRes(Scene scene)
		{
			Scene.Current = scene;
		}
	}
}
