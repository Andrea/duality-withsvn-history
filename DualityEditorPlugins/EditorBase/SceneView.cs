using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;

using Duality;
using Duality.Resources;
using DualityEditor;

namespace EditorBase
{
	public partial class SceneView : DockContent
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
				this.Image = CorePluginHelper.RequestTypeImage(typeof(GameObject), obj.PrefabLink == null ? 
					CorePluginHelper.ImageContext_Icon : 
					CorePluginHelper.ImageContext_Icon + (obj.PrefabLink.Prefab.IsAvailable ? "_Link" : "_Link_Broken"));
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

			public ComponentNode(Component cmp) : base(cmp.TypeName)
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
				Image result = CorePluginHelper.RequestTypeImage(type, CorePluginHelper.ImageContext_Icon);
				if (result == null) result = PluginRes.EditorBaseRes.IconCmpUnknown;
				return result;
			}
			public static string[] GetTypeCategory(Type type)
			{
				string[] result = CorePluginHelper.RequestTypeCategory(type, CorePluginHelper.CategoryContext_General);
				if (result == null) result = new string[] { type.Assembly.FullName.Split(',')[0].Replace(".core", "") };
				return result;
			}
		}


		private	Dictionary<object,NodeBase>	objToNode		= new Dictionary<object,NodeBase>();
		private	FilteredTreeModel			objectModel		= null;
		private	NodeBase					editingNode		= null;

		private	NodeBase	flashNode		= null;
		private	float		flashDuration	= 0.0f;
		private	float		flashIntensity	= 0.0f;

		private	object		tempDropData	= null;
		private	NodeBase	tempDropTarget	= null;
		private	Dictionary<Node,bool>	tempNodeVisibilityCache	= new Dictionary<Node,bool>();
		private	string					tempUpperFilter			= null;

		
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

			this.objectModel = new FilteredTreeModel(this.objectModel_IsNodeVisible);
			this.objectView.Model = this.objectModel;

			this.nodeTextBoxName.ToolTipProvider = this.nodeStateIcon.ToolTipProvider = new ToolTipProvider();
			this.nodeTextBoxName.DrawText += new EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(nodeTextBoxName_DrawText);
			this.nodeTextBoxName.EditorShowing += new CancelEventHandler(nodeTextBoxName_EditorShowing);
			this.nodeTextBoxName.EditorHided += new EventHandler(nodeTextBoxName_EditorHided);
			this.nodeTextBoxName.ChangesApplied += new EventHandler(nodeTextBoxName_ChangesApplied);
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.InitObjects();

			EditorBasePlugin.Instance.EditorForm.SelectionChanged += this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged += this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceCreated += this.EditorForm_ResourceCreated;
			EditorBasePlugin.Instance.EditorForm.ResourceDeleted += this.EditorForm_ResourceDeleted;
			EditorBasePlugin.Instance.EditorForm.ResourceRenamed += this.EditorForm_ResourceRenamed;

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

			EditorBasePlugin.Instance.EditorForm.SelectionChanged -= this.EditorForm_SelectionChanged;
			EditorBasePlugin.Instance.EditorForm.ObjectPropertyChanged -= this.EditorForm_ObjectPropertyChanged;
			EditorBasePlugin.Instance.EditorForm.ResourceCreated -= this.EditorForm_ResourceCreated;
			EditorBasePlugin.Instance.EditorForm.ResourceDeleted -= this.EditorForm_ResourceDeleted;
			EditorBasePlugin.Instance.EditorForm.ResourceRenamed -= this.EditorForm_ResourceRenamed;

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
				this.objectView.EnsureVisible(viewNode);
				return true;
			}
			return false;
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
			this.objectModel.Refresh();
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
			this.objectModel.Nodes.Clear();
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

			foreach (GameObject obj in scene.Graph.RootObjects)
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
				Scene.Current.Graph.RegisterObjDeep(clonedObj);

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
			if (!EditorBasePlugin.Instance.EditorForm.ConfirmBreakPrefabLink(new ObjectSelection(objList.AsEnumerable<object>().Concat(cmpList)))) return;

			// Delete objects
			foreach (GameObject o in objList)
			{ 
				if (o.Disposed) continue;
				o.Dispose(); 
				Scene.Current.Graph.UnregisterObjDeep(o); 
			}
			foreach (Component c in cmpList)
			{
				if (c.Disposed) continue;
				c.Dispose();
			}
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
			Scene.Current.Graph.RegisterObjDeep(newObj);

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
				data.AppendComponentRefs(
					from c in nodes
					where c.Tag is ComponentNode
					select (c.Tag as ComponentNode).Component);
				data.AppendGameObjectRefs(
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

			this.toolStripButtonSelectSceneRes.Enabled = sceneAvail && !String.IsNullOrEmpty(Scene.Current.Path);
			this.toolStripLabelSceneName.Text = String.Format("{0}: {1}",
				PluginRes.EditorBaseRes.SceneNameLabel,
				(!sceneAvail || String.IsNullOrEmpty(Scene.Current.Name)) ? PluginRes.EditorBaseRes.SceneNameNotYetSaved : Scene.Current.Name);
		}
		
		private void textBoxFilter_TextChanged(object sender, EventArgs e)
		{
			this.ApplyNodeFilter();
		}
		private bool objectModel_IsNodeVisible(Node n)
		{
			if (this.tempUpperFilter == null) return true;
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

			var selObj = selGameObj.Union<object>(selComponent);
			if (selObj.Any())
			{
				if (!selComponent.Any()) EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.Component);
				if (!selGameObj.Any()) EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObject);
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(selObj));
			}
			else
				EditorBasePlugin.Instance.EditorForm.Deselect(this, ObjectSelection.Category.GameObject | ObjectSelection.Category.Component);
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
				else if (data.ContainsContentRefs<Prefab>())
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
				else if (data.ContainsContentRefs<Prefab>())
				{
					GameObject dropObj = (this.tempDropTarget is GameObjectNode) ? (this.tempDropTarget as GameObjectNode).Obj : null;
					ContentRef<Prefab>[] dropdata = data.GetContentRefs<Prefab>();
					foreach (ContentRef<Prefab> pRef in dropdata)
					{
						GameObject newObj = pRef.Res.Instantiate();
						newObj.Parent = dropObj;
						Scene.Current.Graph.RegisterObjDeep(newObj);

						this.objectView.ClearSelection();
						TreeNodeAdv dragObjViewNode = this.objectView.FindNode(this.objectModel.GetPath(this.FindNode(newObj)));
						dragObjViewNode.IsSelected = true;
						this.objectView.EnsureVisible(dragObjViewNode);
					}
				}
			}

			this.objectView.EndUpdate();
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
				// Highlight handling
				if (e.Context.DrawSelection != DrawSelectionMode.None)
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
					e.BackgroundBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
						e.Context.Bounds,
						hlUpper,
						hlLower,
						90.0f);
				}

				// Prefab-linked entities
				if (node.LinkState == NodeBase.PrefabLinkState.Active)
					e.TextColor = Color.Blue;
				else if (node.LinkState == NodeBase.PrefabLinkState.Broken)
					e.TextColor = Color.Red;

				// Flashing
				if (node == this.flashNode)
				{
					float intUpper = this.flashIntensity / 4.0f;
					float intLower = this.flashIntensity;
					Color hlBase = Color.FromArgb(255, 64, 32);
					Color hlUpper = Color.FromArgb(
						(int)(SystemColors.Window.R * (1.0f - intUpper) + hlBase.R * intUpper),
						(int)(SystemColors.Window.G * (1.0f - intUpper) + hlBase.G * intUpper),
						(int)(SystemColors.Window.B * (1.0f - intUpper) + hlBase.B * intUpper));
					Color hlLower = Color.FromArgb(
						(int)(SystemColors.Window.R * (1.0f - intLower) + hlBase.R * intLower),
						(int)(SystemColors.Window.G * (1.0f - intLower) + hlBase.G * intLower),
						(int)(SystemColors.Window.B * (1.0f - intLower) + hlBase.B * intLower));
					e.BackgroundBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
						e.Context.Bounds,
						hlUpper,
						hlLower,
						90.0f);
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
				this.editingNode = node;
				this.objectView.ContextMenuStrip = null;
			}
		}
		private void nodeTextBoxName_EditorHided(object sender, EventArgs e)
		{
			this.editingNode = null;
			this.objectView.ContextMenuStrip = this.contextMenuNode;
		}
		private void nodeTextBoxName_ChangesApplied(object sender, EventArgs e)
		{
			NodeBase node = this.objectView.SelectedNode.Tag as NodeBase;
			GameObjectNode objNode = node as GameObjectNode;
			if (objNode != null)
			{
				objNode.Obj.Name = objNode.Text;
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(objNode.Obj), ReflectionInfo.Property_GameObject_Name);
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
					Scene.Current.Graph.RegisterObjDeep(dragObjClone);

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

			if (!EditorBasePlugin.Instance.EditorForm.ConfirmBreakPrefabLink(new ObjectSelection(this.tempDropData as IEnumerable<object>))) return;

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
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(draggedObj), ReflectionInfo.Property_GameObject_Parent);
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
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(cmpList), ReflectionInfo.Property_Component_GameObj);
			}
		}

		private void contextMenuNode_Opening(object sender, CancelEventArgs e)
		{
			List<NodeBase> selNodeData = new List<NodeBase>(
				from vn in this.objectView.SelectedNodes
				where vn.Tag is NodeBase
				select vn.Tag as NodeBase);

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

			// Populate the "New" menu
			this.gameObjectToolStripMenuItem.Image = CorePluginHelper.RequestTypeImage(typeof(GameObject), CorePluginHelper.ImageContext_Icon);
			this.newGameObjectSeparator.Visible = gameObjSelect;

			while (this.newToolStripMenuItem.DropDownItems.Count > 2) this.newToolStripMenuItem.DropDownItems.RemoveAt(2);
			if (gameObjSelect)
			{
				GameObject targetObj = (selNodeData[0] as GameObjectNode).Obj;
				foreach (Type cmpType in this.QueryComponentTypes())
				{
					bool hasRequirements = Component.GetRequiredComponents(cmpType).All(t => targetObj.GetComponent(t) != null);

					// Generalte category item
					string[] category = ComponentNode.GetTypeCategory(cmpType);
					ToolStripMenuItem categoryItem = this.newToolStripMenuItem;
					for (int i = 0; i < category.Length; i++)
					{
						ToolStripMenuItem subCatItem = categoryItem.DropDownItems.Find(category[i], false).FirstOrDefault() as ToolStripMenuItem;
						if (subCatItem == null)
						{
							subCatItem = new ToolStripMenuItem(category[i]);
							subCatItem.Name = category[i];
							subCatItem.Tag = cmpType.Assembly;
							subCatItem.DropDownItemClicked += this.newToolStripMenuItem_DropDownItemClicked;
							categoryItem.DropDownItems.Add(subCatItem);
						}
						categoryItem = subCatItem;
					}

					ToolStripMenuItem cmpTypeItem = new ToolStripMenuItem(cmpType.Name, ComponentNode.GetTypeImage(cmpType));
					cmpTypeItem.Tag = cmpType;
					cmpTypeItem.Enabled = hasRequirements;
					categoryItem.DropDownItems.Add(cmpTypeItem);
				}

				// Sort components and categories
				List<ToolStripMenuItem> newItems = new List<ToolStripMenuItem>(this.newToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>());
				newItems.RemoveAt(0);
				while (this.newToolStripMenuItem.DropDownItems.Count > 2) this.newToolStripMenuItem.DropDownItems.RemoveAt(2);
				newItems.Sort(delegate(ToolStripMenuItem item1, ToolStripMenuItem item2)
				{
					int result;

					System.Reflection.Assembly assembly1 = item1.Tag is Type ? (item1.Tag as Type).Assembly : item1.Tag as System.Reflection.Assembly;
					System.Reflection.Assembly assembly2 = item2.Tag is Type ? (item2.Tag as Type).Assembly : item2.Tag as System.Reflection.Assembly;
					int score1 = assembly1 == typeof(DualityApp).Assembly ? 1 : 0;
					int score2 = assembly2 == typeof(DualityApp).Assembly ? 1 : 0;
					result = score2 - score1;
					if (result != 0) return result;

					result = Math.Sign(item1.DropDownItems.Count) - Math.Sign(item2.DropDownItems.Count);
					if (result != 0) return result;

					result = item1.Text.CompareTo(item2.Text);
					return result;
				});
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

		private void toolStripButtonSelectSceneRes_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Scene.Current.Path))
				EditorBasePlugin.Instance.EditorForm.Select(this, new ObjectSelection(Scene.Current));
		}
		private void toolStripButtonSaveScene_Click(object sender, EventArgs e)
		{
			EditorBasePlugin.Instance.EditorForm.SaveCurrentScene(false);
			this.UpdateSceneLabel();
			this.toolStripButtonSelectSceneRes_Click(sender, e);
		}

		private void EditorForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender == this) return;
			if ((e.AffectedCategories & (ObjectSelection.Category.GameObject | ObjectSelection.Category.Component)) == ObjectSelection.Category.None) return;

			foreach (GameObject o in e.Removed.GameObjects)	this.SelectNode(this.FindNode(o), false);
			foreach (GameObject o in e.Added.GameObjects)	this.SelectNode(this.FindNode(o));
			foreach (Component o in e.Removed.Components)	this.SelectNode(this.FindNode(o), false);
			foreach (Component o in e.Added.Components)		this.SelectNode(this.FindNode(o));
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
			Node parentNode = oldObjNode.Parent;
			parentNode.Nodes.Remove(oldObjNode);
			this.UnregisterNodeTree(oldObjNode);
		}
		private void Scene_GameObjectUnregistered(object sender, ObjectManagerEventArgs<GameObject> e)
		{
			GameObjectNode oldObjNode = this.FindNode(e.Object);
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
	}
}
