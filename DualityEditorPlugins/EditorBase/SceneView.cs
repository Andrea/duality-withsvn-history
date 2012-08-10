using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.Forms;
using DualityEditor.CorePluginInterface;

namespace EditorBase
{
	public partial class SceneView : DockContent, IHelpProvider, IToolTipProvider
	{
		private class ToolTipProvider : IToolTipProvider
		{
			public string GetToolTip(TreeNodeAdv node, Aga.Controls.Tree.NodeControls.NodeControl nodeControl)
			{
				NodeBase dataNode = node.Tag as NodeBase;
				GameObjectNode objNode = dataNode as GameObjectNode;

				if (dataNode.LinkState == NodeBase.PrefabLinkState.None) return null;
				if (objNode == null) return null;
				if (objNode.Obj.PrefabLink == null) return null;

				return String.Format(PluginRes.EditorBaseRes.SceneView_PrefabLink, objNode.Obj.PrefabLink.Prefab.Path);
			}
		}

		public abstract class NodeBase : Node
		{
			public enum PrefabLinkState
			{
				None,
				Active,
				Broken
			}

			private PrefabLinkState linkState = PrefabLinkState.None;

			protected virtual int TypeSortIndex { get { return 0; } }
			public PrefabLinkState LinkState
			{
				get { return this.linkState; }
			}

			public NodeBase(string name) : base(name)
			{
			}
			public bool UpdateLinkState()
			{
				PrefabLinkState lastState = this.linkState;

				ComponentNode cmpNode = this as ComponentNode;
				GameObjectNode objNode = (cmpNode != null ? cmpNode.Parent : this) as GameObjectNode;

				PrefabLink prefabLink = objNode != null ? objNode.Obj.AffectedByPrefabLink : null;
				bool affectedByPrefabLink = prefabLink != null;
				if (cmpNode != null) affectedByPrefabLink = affectedByPrefabLink && prefabLink.AffectsObject(cmpNode.Component);
				if (objNode != null) affectedByPrefabLink = affectedByPrefabLink && prefabLink.AffectsObject(objNode.Obj);

				// Prefab-linked entities
				if (affectedByPrefabLink && File.Exists(prefabLink.Prefab.Path)) //prefabLink.Prefab.IsAvailable) // Not sufficient - might be loaded but with a broken path
					this.linkState = PrefabLinkState.Active;
				else if (cmpNode == null && objNode.Obj.PrefabLink != null)
					this.linkState = PrefabLinkState.Broken;
				else
					this.linkState = PrefabLinkState.None;

				return this.linkState != lastState;
			}

			public static int Compare(NodeBase first, NodeBase second)
			{
				return first.TypeSortIndex - second.TypeSortIndex;
			}
		}
		public class GameObjectNode : NodeBase
		{
			private	GameObject	obj	= null;

			protected override int TypeSortIndex
			{
				get { return 1; }
			}
			public GameObject Obj
			{
				get { return this.obj; }
			}

			public GameObjectNode(GameObject obj) : base(obj.Name)
			{
				this.obj = obj;
				this.UpdateIcon();
			}
			public void UpdateIcon()
			{
				this.Image = CorePluginRegistry.RequestTypeImage(typeof(GameObject), obj.PrefabLink == null ? 
					CorePluginRegistry.ImageContext_Icon : 
					CorePluginRegistry.ImageContext_Icon + (obj.PrefabLink.Prefab.IsAvailable ? "_Link" : "_Link_Broken"));
			}
		}
		public class ComponentNode : NodeBase
		{
			private	Component	cmp	= null;

			protected override int TypeSortIndex
			{
				get { return 0; }				
			}
			public Component Component
			{
				get { return this.cmp; }
			}

			public ComponentNode(Component cmp) : base(cmp.GetType().Name)
			{
				this.cmp = cmp;
				this.UpdateIcon();
			}
			public void UpdateIcon()
			{
				this.Image = GetTypeImage(cmp.GetType());
			}

			public static Image GetTypeImage(Type type)
			{
				Image result = CorePluginRegistry.RequestTypeImage(type, CorePluginRegistry.ImageContext_Icon);
				if (result == null) result = PluginRes.EditorBaseRes.IconCmpUnknown;
				return result;
			}
			public static string[] GetTypeCategory(Type type)
			{
				string[] result = CorePluginRegistry.RequestTypeCategory(type, CorePluginRegistry.CategoryContext_General);
				if (result == null) result = new string[] { type.Assembly.FullName.Split(',')[0].Replace(".core", "") };
				return result;
			}
		}


		private	Dictionary<object,NodeBase>	objToNode		= new Dictionary<object,NodeBase>();
		private	TreeModel					objectModel		= null;
		private	NodeBase					lastEditedNode	= null;

		private	NodeBase	flashNode		= null;
		private	float		flashDuration	= 0.0f;
		private	float		flashIntensity	= 0.0f;

		private	object		tempDropData	= null;
		private	NodeBase	tempDropTarget	= null;
		private	Dictionary<Node,bool>	tempNodeVisibilityCache	= new Dictionary<Node,bool>();
		private	string					tempUpperFilter			= null;
		private bool	tempScheduleSelectionChange	= false;

		
		public IEnumerable<NodeBase> SelectedNodes
		{
			get
			{
				return 
					from c in this.objectView.SelectedNodes
					select c.Tag as NodeBase;
			}
		}
		public IEnumerable<ComponentNode> SelectedComponentNodes
		{
			get
			{
				return 
					from c in this.objectView.SelectedNodes
					where c.Tag is ComponentNode
					select c.Tag as ComponentNode;
			}
		}
		public IEnumerable<GameObjectNode> SelectedGameObjectNodes
		{
			get
			{
				return 
					from c in this.objectView.SelectedNodes
					where c.Tag is GameObjectNode
					select c.Tag as GameObjectNode;
			}
		}


		public SceneView()
		{
			this.InitializeComponent();

			this.objectView.DefaultToolTipProvider = this;
			this.objectModel = new TreeModel();
			this.objectView.Model = this.objectModel;

			this.nodeTextBoxName.ToolTipProvider = this.nodeStateIcon.ToolTipProvider = new ToolTipProvider();
			this.nodeTextBoxName.DrawText += new EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(nodeTextBoxName_DrawText);
			this.nodeTextBoxName.EditorShowing += new CancelEventHandler(nodeTextBoxName_EditorShowing);
			this.nodeTextBoxName.EditorHided += new EventHandler(nodeTextBoxName_EditorHided);
			this.nodeTextBoxName.ChangesApplied += new EventHandler(nodeTextBoxName_ChangesApplied);

			this.toolStrip.Renderer = new DualityEditor.Controls.ToolStrip.DualitorToolStripProfessionalRenderer();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.InitObjects();

			DualityEditorApp.SelectionChanged += this.EditorForm_SelectionChanged;
			DualityEditorApp.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			DualityEditorApp.ResourceCreated += this.EditorForm_ResourceCreated;
			DualityEditorApp.ResourceDeleted += this.EditorForm_ResourceDeleted;
			DualityEditorApp.ResourceRenamed += this.EditorForm_ResourceRenamed;

			Scene.Entered += this.Scene_Entered;
			Scene.Leaving += this.Scene_Leaving;
			Scene.GameObjectRegistered += this.Scene_GameObjectRegistered;
			Scene.GameObjectUnregistered += this.Scene_GameObjectUnregistered;
			Scene.RegisteredObjectComponentAdded += this.Scene_RegisteredObjectComponentAdded;
			Scene.RegisteredObjectComponentRemoved += this.Scene_RegisteredObjectComponentRemoved;

			if (Scene.Current != null) this.Scene_Entered(this, null);
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			DualityEditorApp.SelectionChanged -= this.EditorForm_SelectionChanged;
			DualityEditorApp.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			DualityEditorApp.ResourceCreated -= this.EditorForm_ResourceCreated;
			DualityEditorApp.ResourceDeleted -= this.EditorForm_ResourceDeleted;
			DualityEditorApp.ResourceRenamed -= this.EditorForm_ResourceRenamed;

			Scene.Entered -= this.Scene_Entered;
			Scene.Leaving -= this.Scene_Leaving;
			Scene.GameObjectRegistered -= this.Scene_GameObjectRegistered;
			Scene.GameObjectUnregistered -= this.Scene_GameObjectUnregistered;
			Scene.RegisteredObjectComponentAdded -= this.Scene_RegisteredObjectComponentAdded;
			Scene.RegisteredObjectComponentRemoved -= this.Scene_RegisteredObjectComponentRemoved;
		}

		public void FlashNode(NodeBase node)
		{
			if (node == null) return;

			this.flashNode = node;
			this.flashDuration = 0.0f;
			this.timerFlashItem.Enabled = true;

			this.objectView.EnsureVisible(this.objectView.FindNode(this.objectModel.GetPath(this.flashNode)));
		}
		public bool SelectNode(NodeBase node, bool select = true)
		{
			if (node == null) return false;
			TreeNodeAdv viewNode = this.objectView.FindNode(this.objectModel.GetPath(node));
			if (viewNode != null)
			{
				viewNode.IsSelected = select;
				if (select) this.objectView.EnsureVisible(viewNode);
				return true;
			}
			return false;
		}
		public void SelectNodes(IEnumerable<NodeBase> nodes, bool select = true)
		{
			this.objectView.BeginUpdate();
			TreeNodeAdv viewNode = null;
			foreach (NodeBase node in nodes.NotNull())
			{
				viewNode = this.objectView.FindNode(this.objectModel.GetPath(node));
				if (viewNode != null) viewNode.IsSelected = select;
			}
			this.objectView.EndUpdate();
			if (select && viewNode != null) this.objectView.EnsureVisible(viewNode);
		}
		public GameObjectNode FindNode(GameObject obj)
		{
			NodeBase result;
			if (!this.objToNode.TryGetValue(obj, out result)) return null;
			return result as GameObjectNode;
		}
		public ComponentNode FindNode(Component cmp)
		{
			NodeBase result;
			if (!this.objToNode.TryGetValue(cmp, out result)) return null;
			return result as ComponentNode;
		}

		protected void ApplyNodeFilter()
		{
			this.tempUpperFilter = String.IsNullOrEmpty(this.textBoxFilter.Text) ? null : this.textBoxFilter.Text.ToUpper();
			this.tempNodeVisibilityCache.Clear();
			this.objectView.NodeFilter = this.tempUpperFilter != null ? this.objectModel_IsNodeVisible : (Predicate<object>)null;
		}

		protected IEnumerable<Type> QueryComponentTypes()
		{
			return
				from t in DualityApp.GetAvailDualityTypes(typeof(Component))
				where !t.IsAbstract
				select t;
		}

		protected void InitObjects()
		{
			this.UpdateSceneLabel();

			Node nodeTree = this.ScanScene(Scene.Current);

			this.objectView.BeginUpdate();
			this.ClearObjects();
			while (nodeTree.Nodes.Count > 0) this.InsertNodeSorted(nodeTree.Nodes[0], this.objectModel.Root);
			this.RegisterNodeTree(this.objectModel.Root);
			this.objectView.EndUpdate();
		}
		protected void ClearObjects()
		{
			this.objectView.BeginUpdate();
			this.objectModel.Nodes.Clear();
			this.objToNode.Clear();
			this.objectView.EndUpdate();
		}
		protected void RegisterNodeTree(Node node)
		{
			this.RegisterNode(node);
			foreach (Node c in node.Nodes) this.RegisterNodeTree(c);
		}
		protected void UnregisterNodeTree(Node node)
		{
			this.UnregisterNode(node);
			foreach (Node c in node.Nodes) this.UnregisterNodeTree(c);
		}
		protected void RegisterNode(Node node)
		{
			GameObjectNode nodeObj = node as GameObjectNode;
			ComponentNode nodeCmp = node as ComponentNode;
			if (nodeObj != null) this.objToNode[nodeObj.Obj] = nodeObj;
			if (nodeCmp != null) this.objToNode[nodeCmp.Component] = nodeCmp;
		}
		protected void UnregisterNode(Node node)
		{
			GameObjectNode nodeObj = node as GameObjectNode;
			ComponentNode nodeCmp = node as ComponentNode;
			if (nodeObj != null) this.objToNode.Remove(nodeObj.Obj);
			if (nodeCmp != null) this.objToNode.Remove(nodeCmp.Component);
		}
		protected void InsertNodeSorted(Node newNode, Node parentNode)
		{
			Node insertBeforeNode = parentNode.Nodes.FirstOrDefault(node => 
				(NodeBase.Compare(newNode as NodeBase, node as NodeBase) == 0 && String.Compare(newNode.Text, node.Text) < 0) ||
				NodeBase.Compare(newNode as NodeBase, node as NodeBase) < 0);
			if (insertBeforeNode == null) parentNode.Nodes.Add(newNode);
			else parentNode.Nodes.Insert(parentNode.Nodes.IndexOf(insertBeforeNode), newNode);

			if (newNode is NodeBase) (newNode as NodeBase).UpdateLinkState();
		}
		
		protected ComponentNode ScanComponent(Component cmp)
		{
			if (cmp == null) return null;
			ComponentNode thisNode = new ComponentNode(cmp);
			return thisNode;
		}
		protected GameObjectNode ScanGameObject(GameObject obj, bool scanChildren)
		{
			if (obj == null) return null;
			GameObjectNode thisNode = new GameObjectNode(obj);
			foreach (Component c in obj.GetComponents<Component>())
			{
				ComponentNode compNode = this.ScanComponent(c);
				if (compNode != null) this.InsertNodeSorted(compNode, thisNode);
			}
			if (scanChildren)
			{
				foreach (GameObject c in obj.Children)
				{
					GameObjectNode childNode = this.ScanGameObject(c, scanChildren);
					if (childNode != null) this.InsertNodeSorted(childNode, thisNode);
				}
			}
			return thisNode;
		}
		protected Node ScanScene(Scene scene)
		{
			Node thisNode = new Node("Scene");

			foreach (GameObject obj in scene.RootObjects)
			{
				NodeBase objNode = this.ScanGameObject(obj, true);
				this.InsertNodeSorted(objNode, thisNode);
			}

			return thisNode;
		}
		
		protected void CloneNodes(IEnumerable<TreeNodeAdv> nodes)
		{
			if (!nodes.Any()) return;

			var nodeQuery = 
				from viewNode in nodes
				select this.objectModel.FindNode(this.objectView.GetPath(viewNode)) as NodeBase;
			var objQuery =
				from objNode in nodeQuery
				where objNode is GameObjectNode
				select (objNode as GameObjectNode).Obj;
			var objArray = objQuery.ToArray();
			
			this.objectView.BeginUpdate();
			foreach (GameObject o in objArray)
			{ 
				GameObject clonedObj = o.Clone();
				Scene.Current.RegisterObj(clonedObj);

				// Deselect original node
				TreeNodeAdv dragObjViewNode;
				dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(o)));
				dragObjViewNode.IsSelected = false;

				// Select new node
				dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(clonedObj)));
				dragObjViewNode.IsSelected = true;
				this.objectView.EnsureVisible(dragObjViewNode);
			}
			this.objectView.EndUpdate();
		}
		protected void DeleteNodes(IEnumerable<TreeNodeAdv> nodes)
		{
			var nodeQuery = 
				from viewNode in nodes
				select this.objectModel.FindNode(this.objectView.GetPath(viewNode)) as NodeBase;
			var cmpQuery =
				from cmpNode in nodeQuery
				where cmpNode is ComponentNode
				select (cmpNode as ComponentNode).Component;
			var objQuery =
				from objNode in nodeQuery
				where objNode is GameObjectNode
				select (objNode as GameObjectNode).Obj;
			var cmpList = new List<Component>(cmpQuery);
			var objList = new List<GameObject>(objQuery);
			
			// Check which Components may be removed and which not
			Component conflictComp = this.CheckComponentsRemovable(cmpList, objList);
			if (conflictComp != null)
			{
				this.FlashNode(this.FindNode(conflictComp));
				System.Media.SystemSounds.Beep.Play();
			}
			if (objList.Count == 0 && cmpList.Count == 0) return;

			// Ask user if he really wants to delete stuff
			if (!this.DisplayConfirmDeleteSelectedObjects()) return;
			if (!DualityEditorApp.DisplayConfirmBreakPrefabLink(new ObjectSelection(objList.AsEnumerable<object>().Concat(cmpList)))) return;

			// Delete objects
			this.objectView.BeginUpdate();
			foreach (GameObject o in objList)
			{ 
				if (o.Disposed) continue;
				o.Dispose(); 
				Scene.Current.UnregisterObj(o); 
			}
			foreach (Component c in cmpList)
			{
				if (c.Disposed) continue;
				c.Dispose();
			}
			this.objectView.EndUpdate();
		}
		protected Component CheckComponentsRemovable(List<Component> cmpList, List<GameObject> ignoreGameObjList)
		{
			Component firstReqComp = null;
			for (int i = cmpList.Count - 1; i >= 0; --i)
			{
				Component c = cmpList[i];
				if (ignoreGameObjList != null && ignoreGameObjList.Contains(c.GameObj)) continue;

				Component reqComp = null;
				foreach (Component r in c.GameObj.GetComponents<Component>())
				{
					if (cmpList.Contains(r)) continue;
					if (!r.IsComponentRequirementMet(c))
					{
						reqComp = r;
						break;
					}
				}

				if (reqComp != null)
				{
					cmpList.RemoveAt(i);
					if (firstReqComp == null) firstReqComp = reqComp;
				}
			}
			return firstReqComp;
		}
		protected void CreateGameObject(TreeNodeAdv baseNode)
		{
			GameObjectNode baseObjNode = baseNode == null ? null : baseNode.Tag as GameObjectNode;
			GameObject baseObj = baseObjNode == null ? null : baseObjNode.Obj;
			GameObject newObj = new GameObject();
			newObj.Name = "GameObject";
			newObj.Parent = baseObj;
			Scene.Current.RegisterObj(newObj);

			// Deselect previous
			this.objectView.ClearSelection();

			// Select new node
			TreeNodeAdv dragObjViewNode;
			dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(newObj)));
			dragObjViewNode.IsSelected = true;
			this.objectView.EnsureVisible(dragObjViewNode);
		}
		protected void CreateComponent(TreeNodeAdv baseNode, Type cmpType)
		{
			GameObjectNode baseObjNode = baseNode == null ? null : baseNode.Tag as GameObjectNode;
			GameObject baseObj = baseObjNode == null ? null : baseObjNode.Obj;
			Component newCmp = baseObj.AddComponent(cmpType);

			// Deselect previous
			this.objectView.ClearSelection();

			// Select new node
			TreeNodeAdv dragObjViewNode;
			dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(newCmp)));
			dragObjViewNode.IsSelected = true;
			this.objectView.EnsureVisible(dragObjViewNode);

			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(baseObj));
		}
		protected bool OpenResource(TreeNodeAdv node)
		{
			GameObjectNode objNode = node.Tag as GameObjectNode;
			ComponentNode cmpNode = node.Tag as ComponentNode;

			IEditorAction action = null;
			object subject = null;
			if (objNode != null)
			{
				subject = objNode.Obj;
				action = this.GetResourceOpenAction(objNode.Obj);
			}
			if (cmpNode != null)
			{
				subject = cmpNode.Component;
				action = this.GetResourceOpenAction(cmpNode.Component);	
			}

			if (action != null)
			{
				action.Perform(subject);
				return true;
			}
			else return false;
		}
		protected IEditorAction GetResourceOpenAction(TreeNodeAdv node)
		{
			GameObjectNode objNode = node.Tag as GameObjectNode;
			ComponentNode cmpNode = node.Tag as ComponentNode;
			if (objNode != null) return this.GetResourceOpenAction(objNode.Obj);
			if (cmpNode != null) return this.GetResourceOpenAction(cmpNode.Component);			
			return null;
		}
		protected IEditorAction GetResourceOpenAction(GameObject obj)
		{
			var actions = CorePluginRegistry.RequestEditorActions<GameObject>(CorePluginRegistry.ActionContext_OpenRes, new[] { obj });
			return actions == null ? null : actions.FirstOrDefault();
		}
		protected IEditorAction GetResourceOpenAction(Component cmp)
		{
			var actions = CorePluginRegistry.RequestEditorActions(cmp.GetType(), CorePluginRegistry.ActionContext_OpenRes, new[] { cmp });
			return actions == null ? null : actions.FirstOrDefault();
		}

		protected void AssureMonoSelection()
		{
			if (this.SelectedGameObjectNodes.Any() && this.SelectedComponentNodes.Any())
			{
				List<TreeNodeAdv> selNodes = new List<TreeNodeAdv>(this.objectView.SelectedNodes);
				if (this.objectView.CurrentNode.Tag is ComponentNode)
				{
					foreach (TreeNodeAdv viewNode in selNodes)
						if (viewNode.Tag is GameObjectNode) viewNode.IsSelected = false;
				}
				else
				{
					foreach (TreeNodeAdv viewNode in selNodes)
						if (viewNode.Tag is ComponentNode) viewNode.IsSelected = false;
				}
			}
		}
		protected void AppendNodesToData(DataObject data, IEnumerable<TreeNodeAdv> nodes, bool guardRequiredComponents)
		{
			if (!guardRequiredComponents)
			{
				data.SetData(nodes.ToArray());
				data.SetComponentRefs(
					from c in nodes
					where c.Tag is ComponentNode
					select (c.Tag as ComponentNode).Component);
				data.SetGameObjectRefs(
					from c in nodes
					where c.Tag is GameObjectNode
					select (c.Tag as GameObjectNode).Obj);
			}
			else
			{
				// Query selected objects and components
				var nodeQuery = 
					from viewNode in this.objectView.SelectedNodes
					select this.objectModel.FindNode(this.objectView.GetPath(viewNode)) as NodeBase;
				var cmpQuery =
					from cmpNode in nodeQuery
					where cmpNode is ComponentNode
					select (cmpNode as ComponentNode).Component;
				var objQuery =
					from objNode in nodeQuery
					where objNode is GameObjectNode
					select (objNode as GameObjectNode).Obj;
				var cmpList = new List<Component>(cmpQuery);
				var objList = new List<GameObject>(objQuery);

				// Check which Components may be removed and which not
				Component conflictComp = this.CheckComponentsRemovable(cmpList, objList);
				if (conflictComp != null)
				{
					this.FlashNode(this.FindNode(conflictComp));
					System.Media.SystemSounds.Beep.Play();
				}

				var viewNodeQuery = 
							cmpList.Select(c => this.objectView.FindNodeByTag(this.FindNode(c))).
					Concat(	objList.Select(o => this.objectView.FindNodeByTag(this.FindNode(o))));

				this.AppendNodesToData(data, viewNodeQuery.ToArray(), false);
			}
		}

		protected bool DisplayConfirmDeleteSelectedObjects()
		{
			if (DualityEditorApp.SandboxState == SandboxState.Playing) return true;
			DialogResult result = MessageBox.Show(
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Text, 
				PluginRes.EditorBaseRes.SceneView_MsgBox_ConfirmDeleteSelectedObjects_Caption, 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
			return result == DialogResult.Yes;
		}
		protected void UpdatePrefabLinkStatus()
		{
			bool anyLinkStateChanged = false;
			foreach (NodeBase node in this.objToNode.Values)
			{
				bool result = node.UpdateLinkState();
				anyLinkStateChanged = anyLinkStateChanged || result;
			}

			if (anyLinkStateChanged)
			{
				foreach (GameObjectNode objNode in this.objToNode.Values.OfType<GameObjectNode>())
					objNode.UpdateIcon();

				this.objectView.Invalidate(false);
			}
		}
		protected void UpdateSceneLabel()
		{
			bool sceneAvail = Scene.Current != null;
			this.toolStripLabelSceneName.Text = (!sceneAvail || Scene.Current.IsRuntimeResource) ? 
				PluginRes.EditorBaseRes.SceneNameNotYetSaved : 
				Scene.Current.Name;
			this.toolStripButtonSaveScene.Enabled = DualityEditorApp.SandboxState == SandboxState.Inactive;
		}
		
		private void textBoxFilter_TextChanged(object sender, EventArgs e)
		{
			this.ApplyNodeFilter();
		}
		private bool objectModel_IsNodeVisible(object obj)
		{
			if (this.tempUpperFilter == null) return true;
			TreeNodeAdv vn = obj as TreeNodeAdv;
			Node n = vn != null ? vn.Tag as Node : obj as Node;
			if (n == null) return true;
			bool isVisible;
			if (!this.tempNodeVisibilityCache.TryGetValue(n, out isVisible))
			{
				isVisible = n.Text.ToUpper().Contains(this.tempUpperFilter);
				if (!isVisible) isVisible = n.Nodes.Any(sub => this.objectModel_IsNodeVisible(sub));
				this.tempNodeVisibilityCache[n] = isVisible;
			}
			return isVisible;
		}
		private void objectView_SelectionChanged(object sender, EventArgs e)
		{
			var selComponent =
					from vn in this.objectView.SelectedNodes
					where (vn.Tag is ComponentNode) && (vn.Tag as ComponentNode).Component != null
					select (vn.Tag as ComponentNode).Component;
			var selGameObj =
					from vn in this.objectView.SelectedNodes
					where (vn.Tag is GameObjectNode) && (vn.Tag as GameObjectNode).Obj != null
					select (vn.Tag as GameObjectNode).Obj;

			if (!DualityEditorApp.IsSelectionChanging)
			{
				var selObj = selGameObj.Union<object>(selComponent);
				if (selObj.Any())
				{
					if (!selGameObj.Any() || !selComponent.Any()) DualityEditorApp.Deselect(this, ObjectSelection.Category.GameObjCmp);
					DualityEditorApp.Select(this, new ObjectSelection(selObj));
				}
				else
					DualityEditorApp.Deselect(this, ObjectSelection.Category.GameObjCmp);
			}
		}
		private void objectView_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.objectView.SelectedNodes.Count > 0)
			{
				// Navigate left / collapse
				if (e.KeyCode == Keys.Back)
				{
					int lowLevel = this.objectView.SelectedNodes.Min(viewNode => viewNode.Level);
					TreeNodeAdv lowLevelNode = this.objectView.SelectedNodes.First(viewNode => viewNode.Level == lowLevel);

					if (this.objectView.SelectedNode.IsExpanded)
						this.objectView.SelectedNode.Collapse();
					else if (lowLevel > 1)
						this.objectView.SelectedNode = lowLevelNode.Parent;
				}
				// Navigate right / expand
				else if (e.KeyCode == Keys.Return)
				{
					if (!this.objectView.SelectedNode.IsExpanded)
						this.objectView.SelectedNode.Expand();
				}
				// Fobus object
				else if (e.KeyCode == Keys.F)
				{
					this.OpenResource(this.objectView.SelectedNode);
				}
			}
		}
		private void objectView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.objectView.SelectedNodes.Count > 0)
			{
				this.AssureMonoSelection();

				DataObject dragDropData = new DataObject();
				this.AppendNodesToData(dragDropData, this.objectView.SelectedNodes, false);
				this.DoDragDrop(dragDropData, DragDropEffects.All | DragDropEffects.Link);
			}
		}
		private void objectView_DragOver(object sender, DragEventArgs e)
		{
			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				NodeBase dropParent = this.DragDropGetTargetNode();
				if (data.ContainsGameObjectRefs())
				{
					DragDropEffects effect = (e.KeyState & 1) != 0 ?
						e.Effect = DragDropEffects.Move & e.AllowedEffect :
						e.Effect = (DragDropEffects.Move | DragDropEffects.Copy) & e.AllowedEffect;

					if (dropParent == null)
						e.Effect = effect;
					else if (dropParent is GameObjectNode)
					{
						GameObject dropObj = (dropParent as GameObjectNode).Obj;
						GameObject[] draggedObj = data.GetGameObjectRefs();
						bool canDropHere = true;

						// Can't drop in child of dragged objects
						foreach (GameObject dragObj in draggedObj)
						{
							if (dropObj == dragObj || dropObj.IsChildOf(dragObj))
							{
								canDropHere = false;
								break;
							}
						}

						e.Effect = canDropHere ? effect : DragDropEffects.None;
					}
					else
						e.Effect = DragDropEffects.None;
				}
				else if (data.ContainsComponentRefs())
				{
					DragDropEffects effect = (e.KeyState & 1) != 0 ?
						e.Effect = DragDropEffects.Move & e.AllowedEffect :
						e.Effect = (DragDropEffects.Move | DragDropEffects.Copy) & e.AllowedEffect;

					if (dropParent is GameObjectNode)
					{
						GameObject dropObj = (dropParent as GameObjectNode).Obj;
						Component[] draggedObj = data.GetComponentRefs();
						bool canDropHere = true;

						canDropHere = canDropHere && draggedObj.All(c => dropObj.GetComponent(c.GetType()) == null);
						canDropHere = canDropHere && draggedObj.All(c => c.IsComponentRequirementMet(dropObj, draggedObj));

						e.Effect = canDropHere ? effect : DragDropEffects.None;
					}
					else
						e.Effect = DragDropEffects.None;
				}
				else if (dropParent != null && new ConvertOperation(data, ConvertOperation.Operation.All).CanPerform<Component>())
				{
					e.Effect = e.AllowedEffect;
				}
				else if (new ConvertOperation(data, ConvertOperation.Operation.All).CanPerform<GameObject>())
				{
					if (dropParent is ComponentNode)
						e.Effect = DragDropEffects.None;
					else
						e.Effect = e.AllowedEffect;
				}
			}

			this.objectView.HighlightDropPosition = (e.Effect != DragDropEffects.None);
		}
		private void objectView_DragDrop(object sender, DragEventArgs e)
		{
			this.objectView.BeginUpdate();

			DataObject data = e.Data as DataObject;
			if (data != null)
			{
				ConvertOperation convertOp = new ConvertOperation(data, ConvertOperation.Operation.All);
				this.tempDropTarget = this.DragDropGetTargetNode();
				if (data.ContainsGameObjectRefs())
				{
					this.tempDropData = data.GetGameObjectRefs();

					// Display context menu if both moving and copying are availabled
					if ((e.Effect & DragDropEffects.Move) != DragDropEffects.None && (e.Effect & DragDropEffects.Copy) != DragDropEffects.None)
						this.contextMenuDragMoveCopy.Show(this, this.PointToClient(new Point(e.X, e.Y)));
					else
						this.moveHereToolStripMenuItem_Click(this, null);
				}
				else if (data.ContainsComponentRefs())
				{
					this.tempDropData = data.GetComponentRefs();

					// Display context menu if both moving and copying are availabled
					if ((e.Effect & DragDropEffects.Move) != DragDropEffects.None && (e.Effect & DragDropEffects.Copy) != DragDropEffects.None)
						this.contextMenuDragMoveCopy.Show(this, this.PointToClient(new Point(e.X, e.Y)));
					else
						this.moveHereToolStripMenuItem_Click(this, null);
				}
				else if (this.tempDropTarget != null && convertOp.CanPerform<Component>())
				{
					GameObject dropObj = null;
					if (this.tempDropTarget is GameObjectNode)
						dropObj = (this.tempDropTarget as GameObjectNode).Obj;
					else
						dropObj = (this.tempDropTarget as ComponentNode).Component.GameObj;
					// Make drop target available to converters by adding it to the result set.
					convertOp.AddResult(dropObj);

					var componentQuery = convertOp.Perform<Component>();
					if (componentQuery != null)
					{
						List<TreeNodeAdv> newNodes = new List<TreeNodeAdv>();
						foreach (Component newComponent in componentQuery)
						{
							if (newComponent.GameObj != null) continue;

							// Make sure all requirements are met
							foreach (Type t in newComponent.GetRequiredComponents())
								dropObj.AddComponent(t);

							dropObj.AddComponent(newComponent);
							if (newComponent.GameObj == dropObj)
								newNodes.Add(this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(newComponent))));
						}

						if (newNodes.Count > 0) this.objectView.ClearSelection();
						foreach (TreeNodeAdv viewNode in newNodes)
						{
							viewNode.IsSelected = true;
							this.objectView.EnsureVisible(viewNode);
						}

						DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(dropObj));
					}
				}
				else if (convertOp.CanPerform<GameObject>())
				{
					GameObject dropObj = (this.tempDropTarget is GameObjectNode) ? (this.tempDropTarget as GameObjectNode).Obj : null;
					var gameObjQuery = convertOp.Perform<GameObject>();
					if (gameObjQuery != null)
					{
						List<TreeNodeAdv> newNodes = new List<TreeNodeAdv>();
						foreach (GameObject newObj in gameObjQuery)
						{
							newObj.Parent = dropObj;
							Scene.Current.RegisterObj(newObj);
							newNodes.Add(this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(newObj))));
						}

						if (newNodes.Count > 0) this.objectView.ClearSelection();
						foreach (TreeNodeAdv viewNode in newNodes)
						{
							viewNode.IsSelected = true;
							this.objectView.EnsureVisible(viewNode);
						}

						DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(dropObj));
					}
				}
			}

			this.objectView.EndUpdate();
		}
		private void objectView_NodeMouseDoubleClick(object sender, TreeNodeAdvMouseEventArgs e)
		{
			e.Handled = this.OpenResource(e.Node);
		}
		private void objectView_Leave(object sender, EventArgs e)
		{
			this.objectView.Invalidate();
		}
		private void objectView_Enter(object sender, EventArgs e)
		{
			this.tempScheduleSelectionChange = true;
		}
		private void objectView_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.tempScheduleSelectionChange)
			{
				this.tempScheduleSelectionChange = false;
				this.objectView_SelectionChanged(this.objectView, EventArgs.Empty);
			}
		}
		private NodeBase DragDropGetTargetNode()
		{
			TreeNodeAdv dropViewNode		= this.objectView.DropPosition.Node;
			TreeNodeAdv dropViewNodeParent	= (dropViewNode != null && this.objectView.DropPosition.Position != NodePosition.Inside) ? dropViewNode.Parent : dropViewNode;
			NodeBase dropNodeParent			= (dropViewNodeParent != null) ? dropViewNodeParent.Tag as NodeBase : null;
			return dropNodeParent;
		}

		private void nodeTextBoxName_DrawText(object sender, Aga.Controls.Tree.NodeControls.DrawEventArgs e)
		{
			NodeBase node = e.Node.Tag as NodeBase;

			if (!e.Context.Bounds.IsEmpty)
			{
				// Prefab-linked entities
				if (node.LinkState == NodeBase.PrefabLinkState.Active)
					e.TextColor = Color.Blue;
				else if (node.LinkState == NodeBase.PrefabLinkState.Broken)
					e.TextColor = Color.DarkRed;
				else
					e.TextColor = Color.Black;

				// Flashing
				if (node == this.flashNode)
				{
					float intLower = this.flashIntensity;
					Color hlBase = Color.FromArgb(224, 64, 32);
					Color hlLower = Color.FromArgb(
						(int)(this.objectView.BackColor.R * (1.0f - intLower) + hlBase.R * intLower),
						(int)(this.objectView.BackColor.G * (1.0f - intLower) + hlBase.G * intLower),
						(int)(this.objectView.BackColor.B * (1.0f - intLower) + hlBase.B * intLower));
					e.BackgroundBrush = new SolidBrush(hlLower);
				}
			}
		}
		private void nodeTextBoxName_EditorShowing(object sender, CancelEventArgs e)
		{
			if (e.Cancel) return;
			if (this.objectView.SelectedNode == null)
			{
				e.Cancel = true;
				return;
			}

			NodeBase node = this.objectView.SelectedNode.Tag as NodeBase;
			if (!(node is GameObjectNode)) e.Cancel = true;

			if (!e.Cancel)
			{
				this.lastEditedNode = node;
				this.objectView.ContextMenuStrip = null;
			}
		}
		private void nodeTextBoxName_EditorHided(object sender, EventArgs e)
		{
			this.objectView.ContextMenuStrip = this.contextMenuNode;
		}
		private void nodeTextBoxName_ChangesApplied(object sender, EventArgs e)
		{
			NodeBase node = this.lastEditedNode;
			GameObjectNode objNode = node as GameObjectNode;
			if (objNode != null)
			{
				objNode.Obj.Name = objNode.Text;
				DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(objNode.Obj), ReflectionInfo.Property_GameObject_Name);
			}
		}
		private void timerFlashItem_Tick(object sender, EventArgs e)
		{
			this.flashDuration += (this.timerFlashItem.Interval / 1000.0f);
			this.flashIntensity = 1.0f - (this.flashDuration % 0.33f) / 0.33f;
			this.objectView.Invalidate();

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
			GameObject[] draggedObj = this.tempDropData as GameObject[];
			Component[] draggedComp = this.tempDropData as Component[];
			GameObject dropObj = (this.tempDropTarget is GameObjectNode) ? (this.tempDropTarget as GameObjectNode).Obj : null;

			if (draggedObj != null)
			{
				foreach (GameObject dragObj in draggedObj)
				{
					GameObjectNode dragObjNode = this.FindNode(dragObj);
					Node parent = dragObjNode.Parent;

					// Save transform data
					OpenTK.Vector3 oldPos = OpenTK.Vector3.Zero;
					OpenTK.Vector3 oldScale = OpenTK.Vector3.Zero;
					float oldAngle = 0.0f;
					if (dragObj.Transform != null)
					{
						oldPos = dragObj.Transform.Pos;
						oldAngle = dragObj.Transform.Angle;
						oldScale = dragObj.Transform.Scale;
					}

					// Clone, register and set parent
					GameObject dragObjClone = dragObj.Clone();
					dragObjClone.Parent = dropObj;
					Scene.Current.RegisterObj(dragObjClone);

					// Restore transform data
					if (dragObj.Transform != null)
					{
						dragObj.Transform.Pos = oldPos;
						dragObj.Transform.Angle = oldAngle;
						dragObj.Transform.Scale = oldScale;
					}

					TreeNodeAdv dragObjViewNode;
					// Deselect dragged node
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(dragObj)));
					dragObjViewNode.IsSelected = false;

					// Select new node
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(dragObjClone)));
					dragObjViewNode.IsSelected = true;
					this.objectView.EnsureVisible(dragObjViewNode);
				}
			}
			else if (draggedComp != null)
			{
				foreach (Component dragObj in draggedComp)
				{
					if (dragObj.GameObj == dropObj) continue;
					
					ComponentNode dragObjNode;
					TreeNodeAdv dragObjViewNode;

					// Save node data
					HashSet<object> expandedMap = new HashSet<object>();
					dragObjNode = this.FindNode(dragObj);
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.SaveNodesExpanded(dragObjViewNode, expandedMap);

					// Save transform data
					OpenTK.Vector3 oldPos = OpenTK.Vector3.Zero;
					OpenTK.Vector3 oldScale = OpenTK.Vector3.Zero;
					float oldAngle = 0.0f;
					if (dragObj is Duality.Components.Transform)
					{
						oldPos = (dragObj as Duality.Components.Transform).Pos;
						oldAngle = (dragObj as Duality.Components.Transform).Angle;
						oldScale = (dragObj as Duality.Components.Transform).Scale;
					}

					// Clone & add Component
					Component dragObjClone = dragObj.Clone();
					dropObj.AddComponent(dragObjClone);

					// Restore transform data
					if (dragObjClone is Duality.Components.Transform)
					{
						(dragObjClone as Duality.Components.Transform).Pos = oldPos;
						(dragObjClone as Duality.Components.Transform).Angle = oldAngle;
						(dragObjClone as Duality.Components.Transform).Scale = oldScale;
					}
					
					// Deselect dragged node
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(dragObj)));
					dragObjViewNode.IsSelected = false;

					// Restore node data
					dragObjNode = this.FindNode(dragObjClone);
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.RestoreNodesExpanded(dragObjViewNode, expandedMap);

					// Select node
					dragObjViewNode.IsSelected = true;
					this.objectView.EnsureVisible(dragObjViewNode);
				}
			}
		}
		private void moveHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameObject[] draggedObj = this.tempDropData as GameObject[];
			Component[] draggedComp = this.tempDropData as Component[];
			GameObject dropObj = (this.tempDropTarget is GameObjectNode) ? (this.tempDropTarget as GameObjectNode).Obj : null;

			if (!DualityEditorApp.DisplayConfirmBreakPrefabLink(new ObjectSelection(this.tempDropData as IEnumerable<object>))) return;

			if (draggedObj != null)
			{
				foreach (GameObject dragObj in draggedObj)
				{
					if (dragObj.Parent == dropObj) continue;

					GameObjectNode dragObjNode = this.FindNode(dragObj);
					Node parent = dragObjNode.Parent;

					// Remove node
					TreeNodeAdv dragObjViewNode;
					HashSet<object> expandedMap = new HashSet<object>();
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.SaveNodesExpanded(dragObjViewNode, expandedMap);
					parent.Nodes.Remove(dragObjNode);

					// Save transform data
					OpenTK.Vector3 oldPos = OpenTK.Vector3.Zero;
					OpenTK.Vector3 oldScale = OpenTK.Vector3.Zero;
					float oldAngle = 0.0f;
					if (dragObj.Transform != null)
					{
						oldPos = dragObj.Transform.Pos;
						oldAngle = dragObj.Transform.Angle;
						oldScale = dragObj.Transform.Scale;
					}

					// Set parent
					dragObj.Parent = dropObj;

					// Restore transform data
					if (dragObj.Transform != null)
					{
						dragObj.Transform.Pos = oldPos;
						dragObj.Transform.Angle = oldAngle;
						dragObj.Transform.Scale = oldScale;
					}

					// Re-add node
					parent = dragObj.Parent == null ? this.objectModel.Root : this.FindNode(dragObj.Parent);
					this.InsertNodeSorted(dragObjNode, parent);
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.RestoreNodesExpanded(dragObjViewNode, expandedMap);

					// Select node
					dragObjViewNode.IsSelected = true;
					this.objectView.EnsureVisible(dragObjViewNode);
				}
				DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(draggedObj), ReflectionInfo.Property_GameObject_Parent);
			}
			else if (draggedComp != null)
			{
				List<Component> cmpList = new List<Component>(draggedComp);

				// Check which Components may be removed and which not
				Component conflictComp = this.CheckComponentsRemovable(cmpList, null);
				if (conflictComp != null)
				{
					this.FlashNode(this.FindNode(conflictComp));
					System.Media.SystemSounds.Beep.Play();
				}

				foreach (Component dragObj in cmpList)
				{
					if (dragObj.GameObj == dropObj) continue;
					
					ComponentNode dragObjNode;
					TreeNodeAdv dragObjViewNode;

					// Save node data
					HashSet<object> expandedMap = new HashSet<object>();
					dragObjNode = this.FindNode(dragObj);
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.SaveNodesExpanded(dragObjViewNode, expandedMap);

					// Set parent
					dragObj.GameObj.RemoveComponent(dragObj.GetType());
					dropObj.AddComponent(dragObj);

					// Restore node data
					dragObjNode = this.FindNode(dragObj);
					dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(dragObjNode));
					this.objectView.RestoreNodesExpanded(dragObjViewNode, expandedMap);

					// Select node
					dragObjViewNode.IsSelected = true;
					this.objectView.EnsureVisible(dragObjViewNode);
				}
				DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(cmpList), ReflectionInfo.Property_Component_GameObj);
			}
		}

		private void contextMenuNode_Opening(object sender, CancelEventArgs e)
		{
			List<NodeBase> selNodeData = new List<NodeBase>(
				from vn in this.objectView.SelectedNodes
				where vn.Tag is NodeBase
				select vn.Tag as NodeBase);
			List<object> selObjData = 
				selNodeData.OfType<ComponentNode>().Select(n => n.Component).AsEnumerable<object>().Concat(
				selNodeData.OfType<GameObjectNode>().Select(n => n.Obj)).ToList();

			bool noSelect = selNodeData.Count == 0;
			bool singleSelect = selNodeData.Count == 1;
			bool multiSelect = selNodeData.Count > 1;
			bool gameObjSelect = selNodeData.Any(n => n is GameObjectNode);

			this.newToolStripMenuItem.Visible = (singleSelect && gameObjSelect) || noSelect;
			this.toolStripSeparatorNew.Visible = this.newToolStripMenuItem.Visible;

			this.renameToolStripMenuItem.Visible = !noSelect;
			this.cloneToolStripMenuItem.Visible = !noSelect && gameObjSelect;
			this.deleteToolStripMenuItem.Visible = !noSelect;

			this.renameToolStripMenuItem.Enabled = singleSelect;
			
			// Provide custom actions
			Type mainResType = null;
			if (selObjData.Any())
			{
				mainResType = selObjData.First().GetType();
				// Find mutual type
				foreach (var obj in selObjData)
				{
					Type resType = obj.GetType();
					while (mainResType != null && !mainResType.IsAssignableFrom(resType))
						mainResType = mainResType.BaseType;
				}
			}
			for (int i = this.contextMenuNode.Items.Count - 1; i >= 0; i--)
			{
				if (this.contextMenuNode.Items[i].Tag is IEditorAction)
					this.contextMenuNode.Items.RemoveAt(i);
			}
			if (mainResType != null)
			{
				this.toolStripSeparatorCustomActions.Visible = true;
				int baseIndex = this.contextMenuNode.Items.IndexOf(this.toolStripSeparatorCustomActions);
				var customActions = CorePluginRegistry.RequestEditorActions(
					mainResType, 
					CorePluginRegistry.ActionContext_ContextMenu, 
					selObjData)
					.ToArray();
				foreach (var actionEntry in customActions)
				{
					ToolStripMenuItem actionItem = new ToolStripMenuItem(actionEntry.Name, actionEntry.Icon);
					actionItem.Click += this.customObjectActionItem_Click;
					actionItem.Tag = actionEntry;
					actionItem.ToolTipText = actionEntry.Description;
					this.contextMenuNode.Items.Insert(baseIndex, actionItem);
					baseIndex++;
				}
				if (customActions.Length == 0) this.toolStripSeparatorCustomActions.Visible = false;
			}
			else
				this.toolStripSeparatorCustomActions.Visible = false;

			// Reset "New" menu to original state
			this.gameObjectToolStripMenuItem.Image = CorePluginRegistry.RequestTypeImage(typeof(GameObject), CorePluginRegistry.ImageContext_Icon);
			this.newGameObjectSeparator.Visible = gameObjSelect;

			List<ToolStripItem> oldItems = new List<ToolStripItem>(this.newToolStripMenuItem.DropDownItems.OfType<ToolStripItem>());
			this.newToolStripMenuItem.DropDownItems.Clear();
			foreach (ToolStripItem item in oldItems.Skip(2)) item.Dispose();
			this.newToolStripMenuItem.DropDownItems.AddRange(oldItems.Take(2).ToArray());

			// Populate the "New" menu
			if (gameObjSelect)
			{
				GameObject targetObj = selNodeData.OfType<GameObjectNode>().First().Obj;
				List<ToolStripItem> newItems = new List<ToolStripItem>();
				foreach (Type cmpType in this.QueryComponentTypes())
				{
					bool hasRequirements = Component.GetRequiredComponents(cmpType).All(t => targetObj.GetComponent(t) != null);

					// Generalte category item
					string[] category = ComponentNode.GetTypeCategory(cmpType);
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
							subCatItem.Tag = cmpType.Assembly;
							subCatItem.DropDownItemClicked += this.newToolStripMenuItem_DropDownItemClicked;
							if (categoryItem == this.newToolStripMenuItem)
								newItems.Add(subCatItem);
							else
								categoryItem.DropDownItems.Add(subCatItem);
						}
						categoryItem = subCatItem;
					}

					ToolStripMenuItem cmpTypeItem = new ToolStripMenuItem(cmpType.Name, ComponentNode.GetTypeImage(cmpType));
					cmpTypeItem.Tag = cmpType;
					cmpTypeItem.Enabled = hasRequirements;
					if (categoryItem == this.newToolStripMenuItem)
						newItems.Add(cmpTypeItem);
					else
						categoryItem.DropDownItems.Add(cmpTypeItem);
				}
				EditorBasePlugin.SortToolStripTypeItems(newItems);
				this.newToolStripMenuItem.DropDownItems.AddRange(newItems.ToArray());
			}
		}

		private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloneNodes(this.objectView.SelectedNodes);
		}
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DeleteNodes(this.objectView.SelectedNodes);
		}
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.nodeTextBoxName.BeginEdit();
		}

		private void gameObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CreateGameObject(this.objectView.SelectedNode);
		}
		private void newToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this.gameObjectToolStripMenuItem) return;
			if (e.ClickedItem.Tag as Type == null) return;
			Type clickedType = e.ClickedItem.Tag as Type;
			this.CreateComponent(this.objectView.SelectedNode, clickedType);
		}
		private void customObjectActionItem_Click(object sender, EventArgs e)
		{
			List<NodeBase> selNodeData = new List<NodeBase>(
				from vn in this.objectView.SelectedNodes
				where vn.Tag is NodeBase
				select vn.Tag as NodeBase);
			List<object> selObjData = 
				selNodeData.OfType<ComponentNode>().Select(n => n.Component).AsEnumerable<object>().Concat(
				selNodeData.OfType<GameObjectNode>().Select(n => n.Obj)).ToList();

			ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
			IEditorAction action = clickedItem.Tag as IEditorAction;
			action.Perform(selObjData);
		}

		private void toolStripButtonCreateScene_Click(object sender, EventArgs e)
		{
			DualityEditorApp.SaveCurrentScene(true);
			Scene.Current = null;
		}
		private void toolStripButtonSaveScene_Click(object sender, EventArgs e)
		{
			DualityEditorApp.SaveCurrentScene(false);
			this.UpdateSceneLabel();
		}

		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender == this) return;
			if ((e.AffectedCategories & ObjectSelection.Category.GameObjCmp) == ObjectSelection.Category.None) return;
			if (e.SameObjects);

			IEnumerable<NodeBase> removedObjQuery;
			removedObjQuery = e.Removed.GameObjects.Select(o => this.FindNode(o));
			removedObjQuery = removedObjQuery.Concat(e.Removed.Components.Select(o => this.FindNode(o)));

			IEnumerable<NodeBase> addedObjQuery;
			addedObjQuery = e.Added.GameObjects.Select(o => this.FindNode(o));
			addedObjQuery = addedObjQuery.Concat(e.Added.Components.Select(o => this.FindNode(o)));

			this.SelectNodes(removedObjQuery, false);
			this.SelectNodes(addedObjQuery, true);
		}
		private void EditorForm_ObjectPropertyChanged(object sender, ObjectPropertyChangedEventArgs e)
		{
			if (e.PrefabApplied || (e.HasProperty(ReflectionInfo.Property_GameObject_PrefabLink) && 
									e.Objects.GameObjects.Any(o => o.PrefabLink == null)))
				this.UpdatePrefabLinkStatus();

			if (e.HasProperty(ReflectionInfo.Property_GameObject_Name))
			{
				foreach (GameObjectNode node in e.Objects.GameObjects.Select(g => this.FindNode(g)))
					if (node != null) node.Text = node.Obj.Name;
			}
		}
		private void EditorForm_ResourceRenamed(object sender, ResourceRenamedEventArgs e)
		{
			if (e.Path == Scene.CurrentPath) this.UpdateSceneLabel();

			if (!e.IsDirectory && !typeof(Prefab).IsAssignableFrom(e.ContentType)) return;
			this.UpdatePrefabLinkStatus();
		}
		private void EditorForm_ResourceCreated(object sender, ResourceEventArgs e)
		{
			if (!e.IsDirectory && !typeof(Prefab).IsAssignableFrom(e.ContentType)) return;
			this.UpdatePrefabLinkStatus();
		}
		private void EditorForm_ResourceDeleted(object sender, ResourceEventArgs e)
		{
			if (!e.IsDirectory && !typeof(Prefab).IsAssignableFrom(e.ContentType)) return;
			this.UpdatePrefabLinkStatus();
		}

		private void Scene_Leaving(object sender, EventArgs e)
		{
			this.ClearObjects();
			this.UpdateSceneLabel();
		}
		private void Scene_Entered(object sender, EventArgs e)
		{
			this.InitObjects();
			this.UpdateSceneLabel();
		}
		private void Scene_RegisteredObjectComponentAdded(object sender, ComponentEventArgs e)
		{
			ComponentNode newObjNode = this.ScanComponent(e.Component);
			Node parentNode = e.Component.GameObj != null ? this.FindNode(e.Component.GameObj) : this.objectModel.Root;
			this.InsertNodeSorted(newObjNode, parentNode);
			this.RegisterNodeTree(newObjNode);
		}
		private void Scene_RegisteredObjectComponentRemoved(object sender, ComponentEventArgs e)
		{
			ComponentNode oldObjNode = this.FindNode(e.Component);
			if (oldObjNode == null) return;

			Node parentNode = oldObjNode.Parent;
			parentNode.Nodes.Remove(oldObjNode);
			this.UnregisterNodeTree(oldObjNode);
		}
		private void Scene_GameObjectUnregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			GameObjectNode oldObjNode = this.FindNode(e.Object);
			if (oldObjNode == null) return;

			Node parentNode = oldObjNode.Parent;
			parentNode.Nodes.Remove(oldObjNode);
			this.UnregisterNodeTree(oldObjNode);
		}
		private void Scene_GameObjectRegistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			GameObjectNode newObjNode = this.ScanGameObject(e.Object, false);
			Node parentNode = e.Object.Parent != null ? this.FindNode(e.Object.Parent) : this.objectModel.Root;
			this.InsertNodeSorted(newObjNode, parentNode);
			this.RegisterNodeTree(newObjNode);
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
				if (item == this.gameObjectToolStripMenuItem) itemType = typeof(GameObject);
				if (itemType != null)
				{
					result = HelpInfo.FromMember(itemType);
				}
				captured = true;
			}
			// Hovering Resource nodes
			else
			{
				Point treeLocalPos = this.objectView.PointToClient(globalPos);
				if (this.objectView.ClientRectangle.Contains(treeLocalPos))
				{
					TreeNodeAdv viewNode = this.objectView.GetNodeAt(treeLocalPos);
					ComponentNode cmpNode = viewNode != null ? viewNode.Tag as ComponentNode : null;
					GameObjectNode objNode = viewNode != null ? viewNode.Tag as GameObjectNode : null;
					if (cmpNode != null)
						result = HelpInfo.FromComponent(cmpNode.Component);
					else if (objNode != null)
						result = HelpInfo.FromGameObject(objNode.Obj);
				}
				captured = false;
			}

			return result;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return this.DefaultPerformHelpAction(info);
		}

		string IToolTipProvider.GetToolTip(TreeNodeAdv viewNode, Aga.Controls.Tree.NodeControls.NodeControl nodeControl)
		{
			IEditorAction action = this.GetResourceOpenAction(viewNode);
			if (action != null) return string.Format(
				EditorBase.PluginRes.EditorBaseRes.SceneView_Help_Doubleclick,
				action.Description);
			else return null;
		}
	}
}
