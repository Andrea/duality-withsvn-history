using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;

using Duality;
using Duality.Resources;
using DualityEditor;

namespace EditorBase
{
	public partial class ProjectFolderView : DockContent, IHelpProvider
	{
		public abstract class NodeBase : Node
		{
			public static string GetNodePathId(string nodePath)
			{
				if (nodePath.Contains(':'))
					return nodePath.ToUpper();
				else
					return Path.GetFullPath(nodePath).ToUpper();
			}

			private	string	nodePath		= null;
			private	bool	readOnly		= false;

			public string NodePath
			{
				get { return this.nodePath; }
				set
				{
					if (this.nodePath != value && !this.readOnly)
					{
						string oldPath = this.nodePath;
						this.nodePath = value;
						this.OnNodePathChanged(oldPath);
					}
				}
			}
			public string NodePathId
			{
				get { return GetNodePathId(this.NodePath); }
			}
			public bool ReadOnly
			{
				get { return this.readOnly; }
			}

			public NodeBase(string path, string name, bool readOnly = false) : base(name)
			{
				this.nodePath = path;
				this.readOnly = readOnly;
			}
			
			public void NotifyVisible()
			{
				this.OnFirstVisible();
			}
			public void ApplyPathToName()
			{
				this.Text = this.GetNameFromPath(this.nodePath, this.nodePath.Contains(':'));
			}
			public bool ApplyNameToPath()
			{
				string outVar;
				return this.ApplyNameToPath(out outVar);
			}
			public virtual bool ApplyNameToPath(out string conflictingPath)
			{
				conflictingPath = null;
				return false;
			}

			protected virtual string GetNameFromPath(string path, bool defaultContentPath)
			{
				if (defaultContentPath)
				{
					string[] pathSplit = path.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
					return pathSplit[pathSplit.Length - 1];
				}
				else
				{
					return path;
				}
			}
			protected virtual void OnNodePathChanged(string oldPath)
			{

			}
			protected virtual void OnFirstVisible() {}
		}
		public class DirectoryNode : NodeBase
		{
			public DirectoryNode(string path, bool readOnly = false) : base(path, null, readOnly)
			{
				this.ApplyPathToName();
			}
			
			public override bool ApplyNameToPath(out string conflictingPath)
			{
				conflictingPath = null;
 				if (this.ReadOnly) return false;

				string oldPath = this.NodePath;
				string oldDirName = Path.GetFileName(oldPath);
				string newPathBase = oldPath.Remove(oldPath.Length - oldDirName.Length, oldDirName.Length);
				string newPath = newPathBase + this.Text;
				bool equalsCaseInsensitive = newPath.ToUpper() == oldPath.ToUpper();

				if (Directory.Exists(newPath) && !equalsCaseInsensitive)
				{
					conflictingPath = newPath;
					return false;
				}

				if (equalsCaseInsensitive)
				{
					string tempPath = newPath + "_sSJencn83rhfSHhfn3ns456omvmvs28fndDN84ns";
					Directory.Move(oldPath, tempPath);
					Directory.Move(tempPath, newPath);
				}
				else
					Directory.Move(oldPath, newPath);

				this.NodePath = newPath;
				foreach (NodeBase node in this.Nodes)
				{
					node.NodePath = newPath + node.NodePath.Remove(0, oldPath.Length);
				}
				return true;
			}

			protected override string GetNameFromPath(string path, bool defaultContentPath)
			{
				if (!defaultContentPath)
					return Path.GetFileName(path);
				else
					return base.GetNameFromPath(path, defaultContentPath);
			}
		}
		public class ResourceNode : NodeBase
		{
			private	ContentRef<Resource>	res		= ContentRef<Resource>.Null;
			private	Type					resType	= null;

			public ContentRef<Resource> ResLink
			{
				get { return this.res; }
			}
			public Type ResType
			{
				get { return this.resType; }
			}

			public ResourceNode(string path) : base(path, null, false)
			{
				string[] fileNameSplit = Path.GetFileNameWithoutExtension(path).Split('.');

				this.res.Path = path;
				this.resType = Resource.GetTypeByFileName(path);
				this.ApplyPathToName();
			}
			public ResourceNode(ContentRef<Resource> res) : base(res.Path, null, res.Path.Contains(':'))
			{
				this.res = res;
				this.resType = TypeFromContent(res);
				this.ApplyPathToName();
			}

			public void UpdateImage()
			{
				this.Image = GetTypeImage(this.resType, this.res);				
			}

			public override bool ApplyNameToPath(out string conflictingPath)
			{
				conflictingPath = null;
				if (this.ReadOnly) return false;

				string oldPath = this.NodePath;
				string oldFileName = Path.GetFileName(oldPath);
				string newPathBase = oldPath.Remove(oldPath.Length - oldFileName.Length, oldFileName.Length);
				string newPath = newPathBase + this.Text + Resource.GetFileExtByType(this.resType);

				if (File.Exists(newPath) && newPath.ToUpper() != oldPath.ToUpper())
				{
					conflictingPath = newPath;
					return false;
				}

				File.Move(oldPath, newPath);
				this.NodePath = newPath;
				return true;
			}

			protected override string GetNameFromPath(string path, bool defaultContentPath)
			{
				if (!defaultContentPath)
				{
					string fileName = Path.GetFileNameWithoutExtension(path);
					string[] fileNameSplit = fileName.Split('.');
					return fileNameSplit[0];
				}
				else
					return base.GetNameFromPath(path, defaultContentPath);
			}
			protected override void OnNodePathChanged(string oldPath)
			{
				base.OnNodePathChanged(oldPath);
				this.res.Path = this.NodePath;
			}
			protected override void OnFirstVisible()
			{
				base.OnFirstVisible();
				this.UpdateImage();
			}

			public static Type TypeFromContent(ContentRef<Resource> content)
			{
				return content.Res.GetType();
			}
			public static Image GetTypeImage(Type type, ContentRef<Resource> resLink)
			{
				Image result = null;
				if (type == typeof(Duality.Resources.Prefab))
					result = CorePluginHelper.RequestTypeImage(type, (resLink.IsAvailable && (resLink.Res as Duality.Resources.Prefab).ContainsData) ? 
						CorePluginHelper.ImageContext_Icon + "_Full" : 
						CorePluginHelper.ImageContext_Icon);
				else
					result = CorePluginHelper.RequestTypeImage(type, CorePluginHelper.ImageContext_Icon);

				if (result == null) result = PluginRes.EditorBaseRes.IconResUnknown;

				return result;
			}
			public static Image GetTypeImage(Type type)
			{
				return GetTypeImage(type, ContentRef<Resource>.Null);
			}
			public static string[] GetTypeCategory(Type type)
			{
				string[] result = CorePluginHelper.RequestTypeCategory(type, CorePluginHelper.CategoryContext_General);
				if (result == null) result = new string[] { type.Assembly.FullName.Split(',')[0].Replace(".core", "") };
				return result;
			}
		}


		private	Dictionary<string,NodeBase>	pathIdToNode	= new Dictionary<string,NodeBase>();
		private	TreeModel					folderModel		= null;
		private	NodeBase					lastEditedNode	= null;

		private	NodeBase	flashNode		= null;
		private	float		flashDuration	= 0.0f;
		private	float		flashIntensity	= 0.0f;

		private	List<string>	scheduleSelectPath	= new List<string>();

		private	Dictionary<Node,bool>	tempNodeVisibilityCache	= new Dictionary<Node,bool>();
		private	string					tempUpperFilter			= null;
		private	string					tempDropBasePath		= null;
		private	StringCollection		tempFileDropList		= null;


		public ProjectFolderView()
		{
			this.InitializeComponent();

			this.folderModel = new TreeModel();
			this.folderView.Model = this.folderModel;

			this.nodeTextBoxName.DrawText += new EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(nodeTextBoxName_DrawText);
			this.nodeTextBoxName.EditorShowing += new CancelEventHandler(nodeTextBoxName_EditorShowing);
			this.nodeTextBoxName.EditorHided += new EventHandler(nodeTextBoxName_EditorHided);
			this.nodeTextBoxName.ChangesApplied += new EventHandler(nodeTextBoxName_ChangesApplied);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.InitRessources();
			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceCreated += this.EditorForm_ResourceCreated;
			EditorBasePlugin.Instance.EditorForm.ResourceDeleted += this.EditorForm_ResourceDeleted;
			EditorBasePlugin.Instance.EditorForm.ResourceModified += this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.ResourceRenamed += this.EditorForm_ResourceRenamed;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceCreated -= this.EditorForm_ResourceCreated;
			EditorBasePlugin.Instance.EditorForm.ResourceDeleted -= this.EditorForm_ResourceDeleted;
			EditorBasePlugin.Instance.EditorForm.ResourceModified -= this.EditorForm_ResourceModified;
			EditorBasePlugin.Instance.EditorForm.ResourceRenamed -= this.EditorForm_ResourceRenamed;
		}

		public void FlashNode(NodeBase node)
		{
			if (node == null) return;

			this.flashNode = node;
			this.flashDuration = 0.0f;
			this.timerFlashItem.Enabled = true;

			this.folderView.EnsureVisible(this.folderView.FindNode(this.folderModel.GetPath(this.flashNode)));
		}
		public NodeBase NodeFromPath(string path)
		{
			NodeBase result;
			if (!this.pathIdToNode.TryGetValue(NodeBase.GetNodePathId(path), out result)) return null;
			return result;
		}

		public bool SelectNode(NodeBase node, bool select = true)
		{
			if (node == null) return false;
			TreeNodeAdv viewNode = this.folderView.FindNode(this.folderModel.GetPath(node));
			if (viewNode != null)
			{
				viewNode.IsSelected = select;
				this.folderView.EnsureVisible(viewNode);
				return true;
			}
			return false;
		}
		public void ScheduleSelect(string filePath)
		{
			filePath = Path.GetFullPath(filePath);
			if (!this.SelectNode(this.NodeFromPath(filePath))) this.scheduleSelectPath.Add(filePath);
		}
		protected void PerformScheduleSelect(string incomingFilePath)
		{
			if (!this.scheduleSelectPath.Contains(incomingFilePath)) return;
			if (this.SelectNode(this.NodeFromPath(incomingFilePath))) this.scheduleSelectPath.Remove(incomingFilePath);
		}

		protected void ApplyNodeFilter()
		{
			this.tempUpperFilter = String.IsNullOrEmpty(this.textBoxFilter.Text) ? null : this.textBoxFilter.Text.ToUpper();
			this.tempNodeVisibilityCache.Clear();
			this.folderView.NodeFilter = this.tempUpperFilter != null ? this.folderModel_IsNodeVisible : (Predicate<object>)null;
		}

		protected IEnumerable<Type> QueryResourceTypes()
		{
			return 
				from t in DualityApp.GetAvailDualityTypes(typeof(Resource))
				where !t.IsAbstract
				select t;
		}

		protected void InitRessources()
		{
			this.toolStripLabelProjectName.Text = String.Format("{0}: {1}",
				PluginRes.EditorBaseRes.ProjectNameLabel,
				EditorHelper.CurrentProjectName);

			Node nodeTree = this.ScanDirectory(EditorHelper.DataDirectory);
			nodeTree.Nodes.Insert(0, this.ScanDefaultContent());

			this.folderView.BeginUpdate();
			this.ClearRessources();
			while (nodeTree.Nodes.Count > 0)
			{
				Node n = nodeTree.Nodes[0];
				NodeBase nb = n as NodeBase;
				this.InsertNodeSorted(n, this.folderModel.Root);
				this.RegisterNodeTree(n);
				if (nb != null) nb.NotifyVisible();
			}
			this.folderView.EndUpdate();
		}
		protected void ClearRessources()
		{
			this.folderModel.Nodes.Clear();
			this.pathIdToNode.Clear();
		}
		protected void RegisterNodeTree(Node node)
		{
			this.RegisterNode(node);
			foreach (Node c in node.Nodes) this.RegisterNodeTree(c);
		}
		protected void UnregisterNodeTree(Node node)
		{
			this.UnregisterNode(node);
			foreach (Node c in node.Nodes) this.RegisterNodeTree(c);
		}
		protected void RegisterNode(Node node)
		{
			NodeBase nodeBase = node as NodeBase;
			if (nodeBase != null) this.pathIdToNode[nodeBase.NodePathId] = nodeBase;
		}
		protected void UnregisterNode(Node node)
		{
			NodeBase nodeBase = node as NodeBase;
			if (nodeBase != null) this.pathIdToNode.Remove(nodeBase.NodePathId);
		}
		protected void InsertNodeSorted(Node newNode, Node parentNode)
		{
			Node insertBeforeNode;
			if (newNode is DirectoryNode)
			{
				insertBeforeNode = parentNode.Nodes.FirstOrDefault(node => node is DirectoryNode && String.Compare(node.Text, newNode.Text) > 0);
				if (insertBeforeNode == null) insertBeforeNode = parentNode.Nodes.FirstOrDefault();
			}
			else
				insertBeforeNode = parentNode.Nodes.FirstOrDefault(node => !(node is DirectoryNode) && String.Compare(node.Text, newNode.Text) > 0);

			if (insertBeforeNode == null) parentNode.Nodes.Add(newNode);
			else parentNode.Nodes.Insert(parentNode.Nodes.IndexOf(insertBeforeNode), newNode);
		}

		protected NodeBase ScanFile(string filePath)
		{
			if (!PathHelper.IsPathVisible(filePath)) return null;

			if (Resource.IsResourceFile(filePath))
				return new ResourceNode(filePath);
			else
				return null;
		}
		protected DirectoryNode ScanDirectory(string dirPath)
		{
			if (!PathHelper.IsPathVisible(dirPath)) return null;
			DirectoryNode thisNode = new DirectoryNode(dirPath);

			string[] subDirs = Directory.GetDirectories(dirPath);
			foreach (string dir in subDirs)
			{
				DirectoryNode dirNode = this.ScanDirectory(dir);
				if (dirNode != null) this.InsertNodeSorted(dirNode, thisNode);
			}

			string[] files = Directory.GetFiles(dirPath);
			foreach (string file in files)
			{
				NodeBase fileNode = this.ScanFile(file);
				if (fileNode != null) this.InsertNodeSorted(fileNode, thisNode);
			}

			return thisNode;
		}
		protected NodeBase ScanDefaultRessource(ContentRef<Resource> resRef)
		{
			if (!resRef.IsAvailable) return null;
			return new ResourceNode(resRef);
		}
		protected DirectoryNode ScanDefaultContent()
		{
			DirectoryNode thisNode = new DirectoryNode(ContentProvider.VirtualContentPath, true);

			List<ContentRef<Resource>> defaultContent = ContentProvider.GetAllDefaultContent().ToList();
			foreach (ContentRef<Resource> resRef in defaultContent)
			{
				string[] pathSplit = resRef.Path.Split(':');
				DirectoryNode curDirNode = thisNode;

				// Generate virtual subdirectory nodes
				string curDirPath = pathSplit[0];
				for (int i = 1; i < pathSplit.Length - 1; i++)
				{
					curDirPath += ":" + pathSplit[i];
					DirectoryNode subNode = curDirNode.Nodes.FirstOrDefault(delegate(Node n) 
						{ 
							return n is DirectoryNode && n.Text == pathSplit[i]; 
						}) as DirectoryNode;

					if (subNode == null)
					{
						subNode = new DirectoryNode(curDirPath + ":", true);
						this.InsertNodeSorted(subNode, curDirNode);
						curDirNode.Nodes.Add(subNode);
					}
					curDirNode = subNode;
				}

				// Generate virtual ressource node
				NodeBase res = this.ScanDefaultRessource(resRef);
				if (res != null) this.InsertNodeSorted(res, curDirNode);
			}

			return thisNode;
		}
		
		protected void ClipboardCopyNodes(IEnumerable<TreeNodeAdv> nodes)
		{
			DataObject data = new DataObject();
			this.AppendNodesToData(data, nodes);

			Clipboard.Clear();
			Clipboard.SetDataObject(data);
		}
		protected void ClipboardCutNodes(IEnumerable<TreeNodeAdv> nodes)
		{
			DataObject data = new DataObject();
			this.AppendNodesToData(data, nodes);
			Clipboard.SetDataObject(data, true);

			byte[] moveEffect = new byte[] {2, 0, 0, 0};
			MemoryStream dropEffect = new MemoryStream();
			dropEffect.Write(moveEffect, 0, moveEffect.Length);

			data.SetData("Preferred DropEffect", dropEffect);

			Clipboard.Clear();
			Clipboard.SetDataObject(data);
		}
		protected bool ClipboardHasCutNode(TreeNodeAdv node)
		{
			NodeBase baseNode = node.Tag as NodeBase;
			if (baseNode == null || baseNode.ReadOnly) return false;

			DataObject data = Clipboard.GetDataObject() as DataObject;
			if (data != null)
			{
				// Dropping files
				if (data.ContainsFileDropList())
				{
					// Retrieve preferred drop effect (Windows system stuff)
					MemoryStream dropEffect = data.GetData("Preferred DropEffect") as MemoryStream;
					bool move = false;
					if (dropEffect != null)
					{
						byte[] moveEffect = new byte[4];
						dropEffect.Read(moveEffect, 0, 4);
						move = moveEffect[0] == 2 && moveEffect[1] == 0 && moveEffect[2] == 0 && moveEffect[3] == 0;
					}

					return move && data.GetFileDropList().Contains(Path.GetFullPath(baseNode.NodePath));
				}
			}
			return false;
		}
		protected bool ClipboardCanPasteNodes(TreeNodeAdv baseNode)
		{
			DataObject data = Clipboard.GetDataObject() as DataObject;
			return data != null && data.ContainsFileDropList();
		}
		protected void ClipboardPasteNodes(TreeNodeAdv baseNode)
		{
			this.folderView.BeginUpdate();

			this.tempDropBasePath = this.GetInsertActionTargetBasePath(baseNode != null ? baseNode.Tag as NodeBase : null);
			DataObject data = Clipboard.GetDataObject() as DataObject;
			if (data != null)
			{
				// Dropping files
				if (data.ContainsFileDropList())
				{
					this.tempFileDropList = data.GetFileDropList();

					// Retrieve preferred drop effect (Windows system stuff)
					MemoryStream dropEffect = data.GetData("Preferred DropEffect") as MemoryStream;
					bool move = false;
					if (dropEffect != null)
					{
						byte[] moveEffect = new byte[4];
						dropEffect.Read(moveEffect, 0, 4);
						move = moveEffect[0] == 2 && moveEffect[1] == 0 && moveEffect[2] == 0 && moveEffect[3] == 0;
					}

					// Display context menu if both moving and copying are available
					if (move)
					{
						this.moveHereToolStripMenuItem_Click(this, null);
						Clipboard.Clear();
					}
					else
						this.copyHereToolStripMenuItem_Click(this, null);
				}
			}

			this.folderView.EndUpdate();
		}
		protected void DeleteNodes(IEnumerable<TreeNodeAdv> nodes)
		{
			nodes = nodes.ToList();
			bool allReadOnly = nodes.All(viewNode => (viewNode.Tag as NodeBase).ReadOnly);
			
			if (allReadOnly || !this.DisplayConfirmDeleteSelectedFiles()) return;

			var nodeQuery = 
				from viewNode in nodes
				select this.folderModel.FindNode(this.folderView.GetPath(viewNode)) as NodeBase;

			foreach (NodeBase nodeBase in nodeQuery)
			{
				if (nodeBase.ReadOnly) continue;
				RecycleBin.Send(nodeBase.NodePath);
			}
		}
		protected void CreateFolder(TreeNodeAdv baseNode)
		{
			string basePath = this.GetInsertActionTargetBasePath(baseNode != null ? baseNode.Tag as NodeBase : null);
			string dirPath = PathHelper.GetFreePath(Path.Combine(basePath, PluginRes.EditorBaseRes.NewFolderName), "");

			Directory.CreateDirectory(dirPath);

			this.folderView.ClearSelection();
			this.ScheduleSelect(dirPath);
		}
		protected IContentRef CreateResource(Type type, TreeNodeAdv baseNode, string desiredName = null)
		{
			return this.CreateResource(type, baseNode != null ? baseNode.Tag as NodeBase : null, desiredName);
		}
		protected IContentRef CreateResource(Type type, NodeBase baseNode, string desiredName = null)
		{
			if (desiredName == null) desiredName = type.Name;

			string basePath = this.GetInsertActionTargetBasePath(baseNode);
			string nameExt = Resource.GetFileExtByType(type);
			string resPath = PathHelper.GetFreePath(Path.Combine(basePath, desiredName), nameExt);
			resPath = PathHelper.MakeFilePathRelative(resPath, ".");

			Resource resInstance = ReflectionHelper.CreateInstanceOf(type) as Resource;
			resInstance.Save(resPath);

			this.folderView.ClearSelection();
			this.ScheduleSelect(resPath);

			return resInstance.GetContentRef();
		}
		protected void OpenResource(TreeNodeAdv node)
		{
			ResourceNode resNode = node.Tag as ResourceNode;
			if (resNode == null) return;

			// Determine applying open actions
			var actions = CorePluginHelper.RequestEditorActions(resNode.ResType, CorePluginHelper.ActionContext_OpenRes);

			// Perform first open action
			var action = actions.FirstOrDefault();
			if (action != null) action.Perform(resNode.ResLink.Res);
		}

		protected void AppendNodesToData(DataObject data, IEnumerable<TreeNodeAdv> nodes)
		{
			data.SetData(nodes.ToArray());
			data.AppendContentRefs(
				from c in nodes
				where c.Tag is ResourceNode
				select (c.Tag as ResourceNode).ResLink);
			data.AppendFiles(
				from c in nodes
				where c.Tag is NodeBase && !(c.Tag as NodeBase).NodePath.Contains(':')
				select (c.Tag as NodeBase).NodePath);
		}

		protected void DisplayErrorMoveFile(string targetPath)
		{
			NodeBase conflictNode = this.NodeFromPath(targetPath);
			if (conflictNode != null)
			{
				this.FlashNode(conflictNode);
				System.Media.SystemSounds.Beep.Play();
			}
			else
			{
				MessageBox.Show(
					String.Format(PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_CantMove_Text, targetPath), 
					PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_CantMove_Caption, 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}
		}
		protected void DisplayErrorRenameFile(string conflictPath)
		{
			NodeBase conflictNode = this.NodeFromPath(conflictPath);
			if (conflictNode != null)
			{
				this.FlashNode(conflictNode);
				System.Media.SystemSounds.Beep.Play();
			}
			else
			{
				MessageBox.Show(
					String.Format(PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_CantRename_Text, Path.GetFileNameWithoutExtension(conflictPath)), 
					PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_CantRename_Caption, 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}
		}
		protected bool DisplayConfirmDeleteSelectedFiles()
		{
			DialogResult result = MessageBox.Show(
				PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_ConfirmDeleteSelectedFiles_Text, 
				PluginRes.EditorBaseRes.ProjectFolderView_MsgBox_ConfirmDeleteSelectedFiles_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}

		protected string GetInsertActionTargetBasePath(NodeBase baseNode)
		{
			string baseTargetPath = (baseNode != null) ? baseNode.NodePath : EditorHelper.DataDirectory;
			if (!baseTargetPath.Contains(':'))
			{
				if (File.Exists(baseTargetPath)) baseTargetPath = Path.GetDirectoryName(baseTargetPath);
				baseTargetPath = Path.GetFullPath(baseTargetPath);
			}

			return baseTargetPath;
		}
		
		private void textBoxFilter_TextChanged(object sender, EventArgs e)
		{
			this.ApplyNodeFilter();
		}
		private bool folderModel_IsNodeVisible(object obj)
		{
			if (this.tempUpperFilter == null) return true;
			TreeNodeAdv vn = obj as TreeNodeAdv;
			Node n = vn != null ? vn.Tag as Node : obj as Node;
			if (n == null) return true;
			bool isVisible;
			if (!this.tempNodeVisibilityCache.TryGetValue(n, out isVisible))
			{
				isVisible = n.Text.ToUpper().Contains(this.tempUpperFilter);
				if (!isVisible) isVisible = n.Nodes.Any(sub => this.folderModel_IsNodeVisible(sub));
				this.tempNodeVisibilityCache[n] = isVisible;
			}
			return isVisible;
		}
		private void folderView_Expanding(object sender, TreeViewAdvEventArgs e)
		{
			NodeBase node = e.Node.Tag as NodeBase;
			if (node != null) foreach (NodeBase childNode in node.Nodes) childNode.NotifyVisible();
		}
		private void folderView_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
		{
			if (e.Node == null) return;
			this.OpenResource(e.Node);
		}
		private void folderView_SelectionChanged(object sender, EventArgs e)
		{
			// Retrieve selected ResourceNodes
			ResourceNode[] selResNode = (
				from vn in this.folderView.SelectedNodes
				where vn.Tag is ResourceNode
				select vn.Tag as ResourceNode
				).ToArray();
			// Load their Resource data, if not loaded yet
			Resource[] selRes = (
				from rn in selResNode
				where rn.ResLink.IsAvailable
				select rn.ResLink.Res
				).ToArray();

			// Some ResourceNodes might need to be loaded to display additional information.
			// After loading the selected Resources, update their image according to possibly available new information
			//foreach (ResourceNode resNode in selResNode)
			//	resNode.UpdateImage();
			// Note: Removed this. They'll now just grab their resource if available as soon as they need their data.

			// Adjust editor-wide selection
			if (!EditorBasePlugin.Instance.EditorForm.IsSelectionChanging)
			{
				if (selRes.Length > 0)
					EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selRes));
				else
					EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Resource);
			}
		}
		private void folderView_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.folderView.SelectedNodes.Count > 0)
			{
				// Navigate left / collapse
				if (e.KeyCode == Keys.Back)
				{
					int lowLevel = this.folderView.SelectedNodes.Min(viewNode => viewNode.Level);
					TreeNodeAdv lowLevelNode = this.folderView.SelectedNodes.First(viewNode => viewNode.Level == lowLevel);

					if (this.folderView.SelectedNode.IsExpanded)
						this.folderView.SelectedNode.Collapse();
					else if (lowLevel > 1)
						this.folderView.SelectedNode = lowLevelNode.Parent;
				}
				// Navigate right / expand
				else if (e.KeyCode == Keys.Return)
				{
					if (!this.folderView.SelectedNode.IsExpanded)
						this.folderView.SelectedNode.Expand();
				}
			}
		}
		private void folderView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.folderView.SelectedNodes.Count > 0)
			{
				DataObject dragDropData = new DataObject();
				this.AppendNodesToData(dragDropData, this.folderView.SelectedNodes);
				this.DoDragDrop(dragDropData, DragDropEffects.All | DragDropEffects.Link);
			}
		}
		private void folderView_DragOver(object sender, DragEventArgs e)
		{
			NodeBase baseTarget = this.DragDropGetTargetBaseNode();
			string baseTargetPath = this.GetInsertActionTargetBasePath(baseTarget);
			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				// Dragging files around
				if (data.ContainsFileDropList())
				{
					if (Directory.Exists(baseTargetPath))
					{
						// Do not accept drag if target is located in source
						bool targetInSource = false;
						foreach (string srcPath in data.GetFileDropList())
						{
							if (PathHelper.IsPathLocatedIn(baseTargetPath, srcPath))
							{
								targetInSource = true;
								break;
							}
						}

						if (targetInSource)
							e.Effect = DragDropEffects.None;
						else if ((e.KeyState & 1) != 0)
							e.Effect = DragDropEffects.Move & e.AllowedEffect;
						else
							e.Effect = (DragDropEffects.Move | DragDropEffects.Copy) & e.AllowedEffect;
					}
					else
						e.Effect = DragDropEffects.None;
				}
				// Dragging GameObjects around
				else if (data.ContainsGameObjectRefs())
				{
					ResourceNode targetResNode = baseTarget as ResourceNode;
					DirectoryNode targetDirNode = baseTarget as DirectoryNode;
					
					if (targetResNode != null && targetResNode.ResLink.Is<Duality.Resources.Prefab>() && data.GetGameObjectRefs().Length == 1)
						e.Effect = DragDropEffects.Link & e.AllowedEffect;
					else if (targetResNode == null && (targetDirNode == null || !targetDirNode.NodePath.Contains(':')))
						e.Effect = DragDropEffects.Link & e.AllowedEffect;
					else
						e.Effect = DragDropEffects.None;
				}
			}

			this.folderView.HighlightDropPosition = (e.Effect != DragDropEffects.None);
		}
		private void folderView_DragDrop(object sender, DragEventArgs e)
		{
			this.folderView.BeginUpdate();

			NodeBase baseTarget = this.DragDropGetTargetBaseNode();
			this.tempDropBasePath = this.GetInsertActionTargetBasePath(baseTarget);
			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				// Dropping files
				if (data.ContainsFileDropList())
				{
					this.tempFileDropList = data.GetFileDropList();

					// Display context menu if both moving and copying are availabled
					if ((e.Effect & DragDropEffects.Move) != DragDropEffects.None && (e.Effect & DragDropEffects.Copy) != DragDropEffects.None)
						this.contextMenuDragMoveCopy.Show(this, this.PointToClient(new Point(e.X, e.Y)));
					else
						this.moveHereToolStripMenuItem_Click(this, null);
				}
				// Dropping GameObjects
				else if (data.ContainsGameObjectRefs())
				{
					if (baseTarget is ResourceNode)
					{
						ResourceNode targetResNode = baseTarget as ResourceNode;
						Prefab prefab = targetResNode.ResLink.Res as Prefab;
						if (prefab != null)
						{
							GameObject draggedObj = data.GetGameObjectRefs()[0];

							// Prevent recursion
							foreach (GameObject child in draggedObj.ChildrenDeep)
								if (child.PrefabLink != null && child.PrefabLink.Prefab == prefab)
									child.BreakPrefabLink();

							// Inject GameObject to Prefab
							prefab.Inject(draggedObj);
							EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(prefab));

							// Establish PrefabLink & clear previously existing changes
							if (draggedObj.PrefabLink != null) draggedObj.PrefabLink.ClearChanges();
							draggedObj.LinkToPrefab(prefab);
							EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(draggedObj), ReflectionInfo.Property_GameObject_PrefabLink);
						}
					}
					else
					{
						DirectoryNode targetDirNode = baseTarget as DirectoryNode;
						GameObject[] draggedObjArray = data.GetGameObjectRefs();

						// Filter out GameObjects that are children of others
						draggedObjArray = draggedObjArray.Where(o => !draggedObjArray.Any(o2 => o.IsChildOf(o2))).ToArray();

						// Generate Prefabs
						List<Prefab> createdPrefabs = new List<Prefab>();
						foreach (GameObject draggedObj in draggedObjArray)
						{
							// Create Prefab
							ContentRef<Prefab> prefabRef = this.CreateResource(typeof(Prefab), targetDirNode, draggedObj.Name).As<Prefab>();
							Prefab prefab = prefabRef.Res;
							createdPrefabs.Add(prefab);

							// Inject GameObject to Prefab
							prefab.Inject(draggedObj);

							// Establish PrefabLink & clear previously existing changes
							if (draggedObj.PrefabLink != null) draggedObj.PrefabLink.ClearChanges();
							draggedObj.LinkToPrefab(prefabRef);
						}

						// Fire PropertyChanged events
						EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(createdPrefabs));
						EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(draggedObjArray), ReflectionInfo.Property_GameObject_PrefabLink);
					}
				}
			}

			this.folderView.EndUpdate();
		}
		private void folderView_Leave(object sender, EventArgs e)
		{
			this.folderView.Invalidate();
		}
		private NodeBase DragDropGetTargetBaseNode()
		{
			TreeNodeAdv dropViewNode		= this.folderView.DropPosition.Node;
			TreeNodeAdv dropViewNodeParent	= (dropViewNode != null && this.folderView.DropPosition.Position != NodePosition.Inside) ? dropViewNode.Parent : dropViewNode;
			NodeBase dropNodeParent			= (dropViewNodeParent != null) ? dropViewNodeParent.Tag as NodeBase : null;
			return dropNodeParent;
		}

		private void nodeTextBoxName_DrawText(object sender, Aga.Controls.Tree.NodeControls.DrawEventArgs e)
		{
			NodeBase node = e.Node.Tag as NodeBase;

			// Readonly nodes
			if (node.ReadOnly)
			{
				e.TextColor = SystemColors.GrayText;
			}

			// Highlight handling
			if (e.Context.DrawSelection != DrawSelectionMode.None && !e.Context.Bounds.IsEmpty)
			{
				e.TextColor = SystemColors.ControlText;
				Color hlUpper = Color.FromArgb(
					(SystemColors.Window.R * 5 + SystemColors.Highlight.R) / 6,
					(SystemColors.Window.G * 5 + SystemColors.Highlight.G) / 6,
					(SystemColors.Window.B * 5 + SystemColors.Highlight.B) / 6);
				Color hlLower = Color.FromArgb(
					(SystemColors.Window.R + SystemColors.Highlight.R) / 2,
					(SystemColors.Window.G + SystemColors.Highlight.G) / 2,
					(SystemColors.Window.B + SystemColors.Highlight.B) / 2);

				if (e.Control.Parent.Focused)
					e.BackgroundBrush = new SolidBrush(hlLower);
				else
					e.BackgroundBrush = new SolidBrush(hlUpper);
			}

			// Flashing
			if (node == this.flashNode && !e.Context.Bounds.IsEmpty)
			{
				float intLower = this.flashIntensity;
				Color hlBase = Color.FromArgb(255, 64, 32);
				Color hlLower = Color.FromArgb(
					(int)(SystemColors.Window.R * (1.0f - intLower) + hlBase.R * intLower),
					(int)(SystemColors.Window.G * (1.0f - intLower) + hlBase.G * intLower),
					(int)(SystemColors.Window.B * (1.0f - intLower) + hlBase.B * intLower));
				e.BackgroundBrush = new SolidBrush(hlLower);
			}
		}
		private void nodeTextBoxName_EditorShowing(object sender, CancelEventArgs e)
		{
			NodeBase node = this.folderView.SelectedNode.Tag as NodeBase;
			if (node.ReadOnly) e.Cancel = true;
			if (!e.Cancel)
			{
				this.lastEditedNode = node;
				this.folderView.ContextMenuStrip = null;
			}
		}
		private void nodeTextBoxName_EditorHided(object sender, EventArgs e)
		{
			this.folderView.ContextMenuStrip = this.contextMenuNode;
		}
		private void nodeTextBoxName_ChangesApplied(object sender, EventArgs e)
		{
			NodeBase node = this.lastEditedNode;
			Node parentNode = node.Parent;
			this.UnregisterNodeTree(node);
			node.Parent.Nodes.Remove(node);

			string conflictPath;
			if (!node.ApplyNameToPath(out conflictPath))
			{
				if (NodeBase.GetNodePathId(conflictPath) != node.NodePathId) this.DisplayErrorRenameFile(conflictPath);
				node.Text = null;
				node.ApplyPathToName();
			}

			this.InsertNodeSorted(node, parentNode);
			this.RegisterNodeTree(node);
		}
		private void timerFlashItem_Tick(object sender, EventArgs e)
		{
			this.flashDuration += (this.timerFlashItem.Interval / 1000.0f);
			this.flashIntensity = 1.0f - (this.flashDuration % 0.33f) / 0.33f;
			this.folderView.Invalidate();

			if (this.flashDuration > 0.66f)
			{
				this.flashNode = null;
				this.flashIntensity = 0.0f;
				this.flashDuration = 0.0f;
				this.timerFlashItem.Enabled = false;
			}
		}
		
		private void copyHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (string p in this.tempFileDropList)
			{
				string srcPath = Path.GetFullPath(p);
				string dstPath =  Path.GetFullPath(Path.Combine(this.tempDropBasePath, Path.GetFileName(p)));

				// Clone if target equals source
				if (srcPath == dstPath)
				{
					string dstPathBase = Path.GetFileNameWithoutExtension(p);
					dstPathBase = Path.GetFileNameWithoutExtension(dstPathBase);
					dstPathBase = Path.Combine(this.tempDropBasePath, dstPathBase);
					string dstPathExt = p.Remove(0, dstPathBase.Length);
					dstPath = PathHelper.GetFreePath(dstPathBase, dstPathExt);
				}
				// Skip if target is located inside source
				if (PathHelper.IsPathLocatedIn(dstPath, srcPath)) continue;

				// Move or copy files / directories
				bool errorMove = false;
				if (File.Exists(srcPath) && !File.Exists(dstPath))
					File.Copy(srcPath, dstPath);
				else if (Directory.Exists(srcPath) && !Directory.Exists(dstPath))
					EditorHelper.CopyDirectory(srcPath, dstPath);
				else
					errorMove = true;
						
				// Display error message if moving wasn't possible
				if (errorMove) this.DisplayErrorMoveFile(dstPath);

				this.ScheduleSelect(dstPath);
			}

			this.folderView.ClearSelection();
		}
		private void moveHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string dataDirPath = Path.GetFullPath(EditorHelper.DataDirectory);
			foreach (string p in this.tempFileDropList)
			{
				string srcPath = Path.GetFullPath(p);
				string dstPath =  Path.GetFullPath(Path.Combine(this.tempDropBasePath, Path.GetFileName(p)));
				bool localAction = srcPath.StartsWith(dataDirPath);

				// Skip if target equals source
				if (srcPath == dstPath) continue;
				// Skip if target is located inside source
				if (PathHelper.IsPathLocatedIn(dstPath, srcPath)) continue;

				// Move or copy files / directories
				bool errorMove = false;
				if (localAction)
				{
					if (File.Exists(srcPath) && !File.Exists(dstPath))
						File.Move(srcPath, dstPath);
					else if (Directory.Exists(srcPath) && !Directory.Exists(dstPath))
						Directory.Move(srcPath, dstPath);
					else
						errorMove = true;
				}
				else
				{
					if (File.Exists(srcPath) && !File.Exists(dstPath))
						File.Copy(srcPath, dstPath);
					else if (Directory.Exists(srcPath) && !Directory.Exists(dstPath))
						EditorHelper.CopyDirectory(srcPath, dstPath);
					else
						errorMove = true;
				}
						
				// Display error message if moving wasn't possible
				if (errorMove) this.DisplayErrorMoveFile(dstPath);

				this.ScheduleSelect(dstPath);
			}

			this.folderView.ClearSelection();
		}

		private void contextMenuNode_Opening(object sender, CancelEventArgs e)
		{
			List<NodeBase> selNodeData = new List<NodeBase>(
				from vn in this.folderView.SelectedNodes
				where vn.Tag is NodeBase
				select vn.Tag as NodeBase);
			List<ContentRef<Resource>> selResData = new List<ContentRef<Resource>>(
				from n in selNodeData
				where n is ResourceNode
				select (n as ResourceNode).ResLink);

			bool noSelect = selNodeData.Count == 0;
			bool singleSelect = selNodeData.Count == 1;
			bool multiSelect = selNodeData.Count > 1;
			bool anyReadOnly = this.folderView.SelectedNodes.Any(viewNode => (viewNode.Tag as NodeBase).ReadOnly);


			if (anyReadOnly) 
			{ 
				e.Cancel = true; 
				return;
			}

			this.newToolStripMenuItem.Visible = !anyReadOnly && !multiSelect;
			this.toolStripSeparatorNew.Visible = !anyReadOnly && !multiSelect;

			this.renameToolStripMenuItem.Visible = !noSelect && !anyReadOnly;
			this.cutToolStripMenuItem.Visible = !noSelect && !anyReadOnly;
			this.copyToolStripMenuItem.Visible = !noSelect && !anyReadOnly;
			this.deleteToolStripMenuItem.Visible = !noSelect && !anyReadOnly;

			this.pasteToolStripMenuItem.Visible = !anyReadOnly;

			this.pasteToolStripMenuItem.Enabled = this.ClipboardCanPasteNodes(this.folderView.SelectedNode);
			this.renameToolStripMenuItem.Enabled = singleSelect;

			this.showInExplorerToolStripMenuItem.Visible = singleSelect && !anyReadOnly;
			this.toolStripSeparatorShowInExplorer.Visible = singleSelect && !anyReadOnly;

			// Provide custom actions
			Type mainResType = null;
			if (selResData.Any())
			{
				mainResType = selResData.First().ResType;
				if (selResData.Any(cr => cr.ResType != mainResType)) mainResType = null;
			}
			for (int i = this.contextMenuNode.Items.Count - 1; i >= 0; i--)
			{
				if (this.contextMenuNode.Items[i].Tag is CorePluginHelper.IEditorAction)
					this.contextMenuNode.Items.RemoveAt(i);
			}
			if (mainResType != null)
			{
				this.toolStripSeparatorCustomActions.Visible = true;
				int baseIndex = this.contextMenuNode.Items.IndexOf(this.toolStripSeparatorCustomActions);
				var customActions = CorePluginHelper.RequestEditorActions(
						mainResType, 
						CorePluginHelper.ActionContext_ContextMenu)
					.ToArray();
				foreach (var actionEntry in customActions)
				{
					ToolStripMenuItem actionItem = new ToolStripMenuItem(actionEntry.Name, actionEntry.Icon);
					actionItem.Click += this.customResourceActionItem_Click;
					actionItem.Tag = actionEntry;
					this.contextMenuNode.Items.Insert(baseIndex, actionItem);
					baseIndex++;
				}
				if (customActions.Length == 0) this.toolStripSeparatorCustomActions.Visible = false;
			}
			else
				this.toolStripSeparatorCustomActions.Visible = false;

			// Reset "New" menu to original state
			List<ToolStripItem> oldItems = new List<ToolStripItem>(this.newToolStripMenuItem.DropDownItems.OfType<ToolStripItem>());
			this.newToolStripMenuItem.DropDownItems.Clear();
			foreach (ToolStripItem item in oldItems.Skip(2)) item.Dispose();
			this.newToolStripMenuItem.DropDownItems.AddRange(oldItems.Take(2).ToArray());
			
			// Create dynamic entries
			List<ToolStripItem> newItems = new List<ToolStripItem>();
			foreach (Type resType in this.QueryResourceTypes())
			{
				// Generate category item
				string[] category = ResourceNode.GetTypeCategory(resType);
				ToolStripMenuItem categoryItem = this.newToolStripMenuItem;
				for (int i = 0; i < category.Length; i++)
				{
					ToolStripMenuItem subCatItem;
					if (categoryItem == this.newToolStripMenuItem)
						subCatItem = newItems.FirstOrDefault(item => item.Name == category[i]) as ToolStripMenuItem;
					else
						subCatItem = categoryItem.DropDownItems.Find(category[i], false).FirstOrDefault() as ToolStripMenuItem;

					if (subCatItem == null)
					{
						subCatItem = new ToolStripMenuItem(category[i]);
						subCatItem.Name = category[i];
						subCatItem.Tag = resType.Assembly;
						subCatItem.DropDownItemClicked += this.newToolStripMenuItem_DropDownItemClicked;
						if (categoryItem == this.newToolStripMenuItem)
							newItems.Add(subCatItem);
						else
							categoryItem.DropDownItems.Add(subCatItem);
					}
					categoryItem = subCatItem;
				}

				ToolStripMenuItem resTypeItem = new ToolStripMenuItem(resType.Name, ResourceNode.GetTypeImage(resType));
				resTypeItem.Tag = resType;
				if (categoryItem == this.newToolStripMenuItem)
					newItems.Add(resTypeItem);
				else
					categoryItem.DropDownItems.Add(resTypeItem);
			}
			EditorBasePlugin.SortToolStripTypeItems(newItems);
			this.newToolStripMenuItem.DropDownItems.AddRange(newItems.ToArray());
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ClipboardCutNodes(this.folderView.SelectedNodes);
		}
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ClipboardCopyNodes(this.folderView.SelectedNodes);
		}
		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ClipboardPasteNodes(this.folderView.SelectedNode);
		}
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DeleteNodes(this.folderView.SelectedNodes);
		}
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.nodeTextBoxName.BeginEdit();
		}
		private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string filePath = Path.GetFullPath((this.folderView.SelectedNode.Tag as NodeBase).NodePath);
			string argument = @"/select, " + filePath;
			System.Diagnostics.Process.Start("explorer.exe", argument);
		}

		private void folderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CreateFolder(this.folderView.SelectedNode);
		}
		private void newToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this.folderToolStripMenuItem) return;
			if (e.ClickedItem.Tag as Type == null) return;
			Type clickedType = e.ClickedItem.Tag as Type;
			this.CreateResource(clickedType, this.folderView.SelectedNode);
		}
		private void customResourceActionItem_Click(object sender, EventArgs e)
		{
			List<NodeBase> selNodeData = new List<NodeBase>(
				from vn in this.folderView.SelectedNodes
				where vn.Tag is NodeBase
				select vn.Tag as NodeBase);
			List<ContentRef<Resource>> selResData = new List<ContentRef<Resource>>(
				from n in selNodeData
				where n is ResourceNode
				select (n as ResourceNode).ResLink);

			ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
			CorePluginHelper.IEditorAction action = clickedItem.Tag as CorePluginHelper.IEditorAction;
			foreach (ContentRef<Resource> resRef in selResData)
			{
				action.Perform(resRef.Res);
			}
		}

		private void toolStripButtonWorkDir_Click(object sender, EventArgs e)
		{
			string filePath = Path.GetFullPath(EditorHelper.DataDirectory);
			string argument = filePath;
			System.Diagnostics.Process.Start("explorer.exe", argument);
		}

		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender == this) return;
			if ((e.AffectedCategories & ObjectSelection.Category.Resource) == ObjectSelection.Category.None) return;

			foreach (Resource r in e.Removed.Resources)	this.SelectNode(this.NodeFromPath(r.Path), false);
			foreach (Resource r in e.Added.Resources)
			{
				if (!this.SelectNode(this.NodeFromPath(r.Path)))
					this.ScheduleSelect(r.Path);
			}
		}
		private void EditorForm_ResourceCreated(object sender, ResourceEventArgs e)
		{
			// Register newly detected ressource file
			if (File.Exists(e.Path) && Resource.IsResourceFile(e.Path))
			{
				NodeBase newNode = this.ScanFile(e.Path);

				Node parentNode = this.NodeFromPath(Path.GetDirectoryName(e.Path));
				if (parentNode == null) parentNode = this.folderModel.Root;
					
				this.InsertNodeSorted(newNode, parentNode);
				this.RegisterNodeTree(newNode);
				newNode.NotifyVisible();
			}
			// Add new directory tree
			else if (e.IsDirectory)
			{
				NodeBase newNode = this.ScanDirectory(e.Path);

				Node parentNode = this.NodeFromPath(Path.GetDirectoryName(e.Path));
				if (parentNode == null) parentNode = this.folderModel.Root;

				this.InsertNodeSorted(newNode, parentNode);
				this.RegisterNodeTree(newNode);
				newNode.NotifyVisible();
			}

			// Perform previously scheduled selection
			this.PerformScheduleSelect(Path.GetFullPath(e.Path));
		}
		private void EditorForm_ResourceDeleted(object sender, ResourceEventArgs e)
		{
			// Remove lost ressource file
			NodeBase node = this.NodeFromPath(e.Path);
			if (node != null)
			{
				this.UnregisterNodeTree(node);
				node.Parent.Nodes.Remove(node);
			}
		}
		private void EditorForm_ResourceModified(object sender, ResourceEventArgs e)
		{
			// If a Prefab has been modified, update its appearance
			if (e.IsResource && e.Content.Is<Duality.Resources.Prefab>())
			{
				ResourceNode modifiedNode = this.NodeFromPath(e.Content.Path) as ResourceNode;
				if (modifiedNode != null) modifiedNode.UpdateImage();
			}
		}
		private void EditorForm_ResourceRenamed(object sender, ResourceRenamedEventArgs e)
		{
			NodeBase node = this.NodeFromPath(e.OldPath);
			bool registerRes = false;

			// Modify existing node
			if (node != null)
			{
				// If its a file, remove and add it again
				if (File.Exists(e.Path))
				{
					// Remove it
					this.UnregisterNodeTree(node);
					node.Parent.Nodes.Remove(node);

					// Register it
					registerRes = true;
				}
				// Otherwise: Rename node according to file
				else
				{
					this.UnregisterNodeTree(node);
					node.NodePath = e.Path;
					node.ApplyPathToName();
					this.RegisterNodeTree(node);
				}
			}
			// Register newly detected ressource file
			else if (this.NodeFromPath(e.Path) == null)
			{
				registerRes = true;
			}

			// If neccessary, check if the file is a ressource file and add it, if yes
			if (registerRes && Resource.IsResourceFile(e.Path))
			{
				node = this.ScanFile(e.Path);

				Node parentNode = this.NodeFromPath(Path.GetDirectoryName(e.Path));
				if (parentNode == null) parentNode = this.folderModel.Root;

				this.InsertNodeSorted(node, parentNode);
				this.RegisterNodeTree(node);
				node.NotifyVisible();
			}

			// Perform previously scheduled selection
			this.PerformScheduleSelect(Path.GetFullPath(e.Path));
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			HelpInfo result = null;
			Point globalPos = this.PointToScreen(localPos);

			// Hovering "Create Resource" menu
			if (this.contextMenuNode.Visible)
			{
				ToolStripItem item = this.newToolStripMenuItem.DropDown.GetItemAtDeep(globalPos);
				Type itemType = item != null ? item.Tag as Type : null;
				if (itemType != null)
				{
					result = HelpInfo.FromMember(itemType);
				}
				captured = true;
			}
			// Hovering Resource nodes
			else
			{
				Point treeLocalPos = this.folderView.PointToClient(globalPos);
				if (this.folderView.ClientRectangle.Contains(treeLocalPos))
				{
					TreeNodeAdv viewNode = this.folderView.GetNodeAt(treeLocalPos);
					ResourceNode resNode = viewNode != null ? viewNode.Tag as ResourceNode : null;
					if (resNode != null)
					{
						result = HelpInfo.FromResource(resNode.ResLink);
					}
				}
				captured = false;
			}

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}
	}
}
