using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using Duality;
using Duality.Serialization;
using Duality.Serialization.MetaFormat;

using DualityEditor;

using Aga.Controls.Tree;
using WeifenLuo.WinFormsUI.Docking;

namespace ResourceHacker
{
	public partial class ResourceHacker : DockContent
	{
		protected class DataTreeNode : Node
		{
			protected	DataNode	data;

			public DataNode Data
			{
				get { return this.data; }
			}
			public string NodeTypeName
			{
				get { return this.data.GetType().GetTypeKeyword(); }
			}

			protected DataTreeNode(DataNode data)
			{
				this.data = data;
				this.Text = this.NodeTypeName;
				this.Image = GetIcon(this.data);
			}

			public static DataTreeNode Create(DataNode data)
			{
				if (data is ObjectNode)
					return new ObjectTreeNode(data as ObjectNode);
				else if (data is ObjectRefNode)
					return new ObjectRefTreeNode(data as ObjectRefNode);
				else if (data is PrimitiveNode)
					return new PrimitiveTreeNode(data as PrimitiveNode);
				else if (data is EnumNode)
					return new EnumTreeNode(data as EnumNode);
				else if (data is StringNode)
					return new StringTreeNode(data as StringNode);
				else
					return new DataTreeNode(data);
			}
			public static Image GetIcon(DataNode data)
			{
				Image result = CorePluginHelper.RequestTypeImage(data.GetType(), CorePluginHelper.ImageContext_Icon);
				if (result != null) 
					return result;
				else 
					return CorePluginHelper.RequestTypeImage(typeof(DataNode), CorePluginHelper.ImageContext_Icon);
			}
		}
		protected class PrimitiveTreeNode : DataTreeNode
		{
			protected	PrimitiveNode	primitiveData;

			public string ResolvedTypeName
			{
				get 
				{ 
					Type actualType = this.primitiveData.NodeType.ToActualType();
					return actualType != null ? actualType.GetTypeKeyword() : "Unknown";
				}
			}
			public object DataValue
			{
				get { return this.primitiveData.PrimitiveValue; }
			}

			public PrimitiveTreeNode(PrimitiveNode data) : base(data)
			{
				this.primitiveData = data;
			}
		}
		protected class EnumTreeNode : DataTreeNode
		{
			protected	EnumNode	enumData;

			public string ResolvedTypeName
			{
				get 
				{ 
					Type actualType = ReflectionHelper.ResolveType(this.enumData.EnumType, false);
					return actualType != null ? actualType.GetTypeKeyword() : "Unknown";
				}
			}
			public object DataValue
			{
				get { return this.enumData.ValueName; }
			}

			public EnumTreeNode(EnumNode data) : base(data)
			{
				this.enumData = data;
			}
		}
		protected class StringTreeNode : DataTreeNode
		{
			protected	StringNode	stringData;

			public string ResolvedTypeName
			{
				get { return typeof(string).GetTypeKeyword(); }
			}
			public object DataValue
			{
				get { return this.stringData.StringValue; }
			}

			public StringTreeNode(StringNode data) : base(data)
			{
				this.stringData = data;
			}
		}
		protected class ObjectTreeNode : DataTreeNode
		{
			protected	ObjectNode	objData;

			public uint ObjId
			{
				get { return this.objData.ObjId; }
			}
			public MemberInfo ResolvedMember
			{
				get
				{
					if (this.data.NodeType.IsMemberInfoType() && this.data.NodeType != DataType.Type)
						return ReflectionHelper.ResolveMember(this.objData.TypeString, false);
					else
						return ReflectionHelper.ResolveType(this.objData.TypeString, false);
				}
			}
			public string ResolvedTypeName
			{
				get 
				{ 
					MemberInfo resMember = this.ResolvedMember;
					Type resType = resMember as Type;
					if (resType != null)
						return resType.GetTypeCSCodeName(true);
					else if (resMember != null)
						return resMember.GetMemberId();
					else
						return objData.TypeString;
				}
			}

			public ObjectTreeNode(ObjectNode data) : base(data)
			{
				this.objData = data;
			}
		}
		protected class ObjectRefTreeNode : DataTreeNode
		{
			protected	ObjectRefNode	objRefData;

			public uint ObjId
			{
				get { return this.objRefData.ObjRefId; }
			}

			public ObjectRefTreeNode(ObjectRefNode data) : base(data)
			{
				this.objRefData = data;
			}
		}

		private	string				filePath	= null;
		private	TreeModel			dataModel	= new TreeModel();
		private	List<DataTreeNode>	rootData	= new List<DataTreeNode>();

		private	Dictionary<string,TypeDataLayoutNode>
			typeDataLayout = new Dictionary<string,TypeDataLayoutNode>();

		public ResourceHacker()
		{
			this.InitializeComponent();
			this.treeView.Model = this.dataModel;
			this.ClearData();

			this.nodeTextBoxObjId.DrawText += this.nodeTextBoxObjId_DrawText;
			this.nodeTextBoxType.DrawText += this.nodeTextBoxType_DrawText;
			this.propertyGrid.EditingFinished += this.propertyGrid_EditingFinished;

			this.openFileDialog.InitialDirectory = EditorHelper.DataDirectory;
			this.openFileDialog.Filter = "Duality Resource|*" + Resource.FileExt;
			this.saveFileDialog.InitialDirectory = this.openFileDialog.InitialDirectory;
			this.saveFileDialog.Filter = this.openFileDialog.Filter;
		}
		protected override void OnShown(EventArgs e)
		{
			this.propertyGrid.RegisterEditorProvider(CorePluginHelper.RequestPropertyEditorProviders());
			base.OnShown(e);
		}

		public void LoadFile(string filePath)
		{
			if (!File.Exists(filePath)) throw new FileNotFoundException("Can't open Resource file. File not found.", filePath);

			this.ClearData();
			this.actionRenameType.Enabled = true;
			this.actionSave.Enabled = true;
			this.filePath = filePath;
			using (FileStream fileStream = File.OpenRead(this.filePath))
			{
				using (var formatter = Formatter.CreateMeta(fileStream))
				{
					DataNode dataNode;
					try
					{
						this.treeView.BeginUpdate();
						while ((dataNode = formatter.ReadObject() as DataNode) != null)
						{
							DataTreeNode data = this.AddData(dataNode);
							this.rootData.Add(data);
						}
					}
					catch (EndOfStreamException) {}
					catch (Exception e)
					{
						Log.Editor.WriteError("Can't load file {0} because an error occured in the process: \n{1}",
							this.filePath,
							Log.Exception(e));
						return;
					}
					finally
					{
						foreach (DataTreeNode n in this.rootData) this.dataModel.Nodes.Add(n);
						this.treeView.EndUpdate(); 
					}
				}
			}
		}
		public void SaveFile(string filePath)
		{
			this.filePath = filePath;

			using (FileStream fileStream = File.Open(this.filePath, FileMode.Create, FileAccess.Write))
			{
				using (var formatter = Formatter.CreateMeta(fileStream))
				{
					foreach (DataTreeNode dataNode in this.dataModel.Nodes)
						formatter.WriteObject(dataNode.Data);
				}
			}

			// Assure reloading the modified resource
			if (PathHelper.IsPathLocatedIn(this.filePath, "."))
			{
				string dataPath = PathHelper.MakePathRelative(this.filePath, ".");
				ContentProvider.UnregisterContent(dataPath, true);
			}
		}

		protected void ClearData()
		{
			this.dataModel.Nodes.Clear();
			this.rootData.Clear();
			this.typeDataLayout.Clear();

			this.actionRenameType.Enabled = false;
			this.actionSave.Enabled = false;
		}
		protected DataTreeNode AddData(DataNode data)
		{
			// Register type data layout nodes
			if (data is TypeDataLayoutNode)
				this.typeDataLayout[(data.Parent as ObjectNode).TypeString] = data as TypeDataLayoutNode;

			DataTreeNode dataNode = DataTreeNode.Create(data);
			foreach (DataNode child in data.SubNodes)
			{
				DataTreeNode childDataNode = this.AddData(child);
				childDataNode.Parent = dataNode;
			}
			this.UpdateTypeDataLayout(dataNode, false);

			return dataNode;
		}
		protected void UpdateTypeDataLayout(Node updateNode = null, bool recursive = true)
		{
			if (updateNode == null) updateNode = this.dataModel.Root;
			foreach (DataTreeNode n in updateNode.Nodes)
			{
				if (!String.IsNullOrEmpty(n.Data.Name))
					n.Text = n.Data.Name;
				else
					n.Text = n.NodeTypeName;
			}

			if (recursive)
			{
				foreach (Node n in updateNode.Nodes)
					this.UpdateTypeDataLayout(n);
			}
		}
		protected bool IsObjectIdExisting(uint objId, DataNode baseNode = null)
		{
			if (objId == 0) return false;
			
			foreach (DataTreeNode dataNode in this.rootData)
				if (dataNode.Data.IsObjectIdDefined(objId)) return true;

			return false;
		}
		protected string[] GetAvailTypes(DataNode baseNode = null)
		{
			List<string> availTypes = new List<string>();

			foreach (DataTreeNode dataNode in this.rootData)
				availTypes.AddRange(dataNode.Data.GetTypeStrings(true));

			return availTypes.Distinct().ToArray();
		}

		protected void CurrentPerformAction(Action<DataNode> action)
		{
			foreach (DataTreeNode dataNode in this.rootData)
				action(dataNode.Data);
		}
		protected void FilePerformAction(string filePath, Action<DataNode> action, bool updateContent = true)
		{
			List<DataNode> data = new List<DataNode>();

			// Load data
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				using (var formatter = Formatter.CreateMeta(fileStream))
				{
					DataNode dataNode;
					try
					{
						while ((dataNode = formatter.ReadObject() as DataNode) != null)
							data.Add(dataNode);
					}
					catch (EndOfStreamException) {}
					catch (Exception e)
					{
						Log.Editor.WriteError("Can't perform batch action on {0} because an error occured in the process: \n{1}",
							filePath,
							Log.Exception(e));
						return;
					}
				}
			}

			// Process data
			foreach (DataNode dataNode in data)
				action(dataNode);

			// Save data
			using (FileStream fileStream = File.Open(filePath, FileMode.Create))
			{
				using (var formatter = Formatter.CreateMeta(fileStream))
				{
					foreach (DataNode dataNode in data)
						formatter.WriteObject(dataNode);
				}
			}

			// Assure reloading the modified resource
			if (PathHelper.IsPathLocatedIn(filePath, "."))
			{
				string dataPath = PathHelper.MakePathRelative(filePath, ".");
				ContentProvider.UnregisterContent(dataPath, true);
			}
		}
		protected void BatchPerformAction(string folderPath, Action<DataNode> action, bool updateContent = true)
		{
			string[] files = Directory.GetFiles(folderPath);
			foreach (string file in files)
			{
				if (!Resource.IsResourceFile(file)) continue;
				this.FilePerformAction(file, action, false);
			}

			string[] dirs = Directory.GetDirectories(folderPath);
			foreach (string dir in dirs)
				this.BatchPerformAction(dir, action, false);

			// Assure reloading the modified resources
			if (updateContent && PathHelper.IsPathLocatedIn(folderPath, "."))
			{
				string dataPath = PathHelper.MakePathRelative(folderPath, ".");
				ContentProvider.UnregisterContentTree(dataPath, true);
			}
		}

		private void actionOpen_Click(object sender, EventArgs e)
		{
			this.openFileDialog.ShowDialog(this);
		}
		private void actionSave_Click(object sender, EventArgs e)
		{
			this.saveFileDialog.ShowDialog(this);
		}
		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			this.LoadFile(this.openFileDialog.FileName);
			this.openFileDialog.FileName = "";
		}
		private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			this.SaveFile(this.saveFileDialog.FileName);
			this.saveFileDialog.FileName = "";
		}
		private void treeView_SelectionChanged(object sender, EventArgs e)
		{
			TreeNodeAdv viewSelNode = this.treeView.SelectedNode;
			DataTreeNode selNode = viewSelNode != null ? viewSelNode.Tag as DataTreeNode : null;
			this.propertyGrid.SelectObject(selNode != null ? selNode.Data : null);
		}
		private void nodeTextBoxObjId_DrawText(object sender, Aga.Controls.Tree.NodeControls.DrawEventArgs e)
		{
			DataTreeNode node = e.Node.Tag as DataTreeNode;
			ObjectRefTreeNode objRefNode = node as ObjectRefTreeNode;
			if (e.Text == "0") e.TextColor = SystemColors.GrayText;
			else if (objRefNode != null)
			{
				if (this.IsObjectIdExisting(objRefNode.ObjId)) e.TextColor = Color.Blue;
				else e.TextColor = Color.Red;
			}
		}
		private void nodeTextBoxType_DrawText(object sender, Aga.Controls.Tree.NodeControls.DrawEventArgs e)
		{
			DataTreeNode node = e.Node.Tag as DataTreeNode;
			ObjectTreeNode objNode = node as ObjectTreeNode;
			if (objNode != null && objNode.ResolvedMember == null) e.TextColor = Color.Red;
		}
		private void propertyGrid_EditingFinished(object sender, EventArgs e)
		{
			var editor = sender as DualityEditor.Controls.PropertyEditor;
			if (typeof(TypeDataLayoutNode).IsAssignableFrom(editor.EditedType))
			{
				this.UpdateTypeDataLayout();
			}
		}

		private void actionRenameType_Click(object sender, EventArgs e)
		{
			RenameTypeDialog dialog = new RenameTypeDialog(this.GetAvailTypes());
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				int replaced = 0;
				this.CurrentPerformAction(n => replaced += n.ReplaceTypeStrings(dialog.SearchFor, dialog.ReplaceWith));
				MessageBox.Show(
					string.Format(PluginRes.ResourceHackerRes.MessageBox_RenameType_Text, replaced, dialog.SearchFor, dialog.ReplaceWith), 
					PluginRes.ResourceHackerRes.MessageBox_RenameType_Title, 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Information);
			}
		}
		private void batchActionRenameType_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.ShowNewFolderButton = false;
			folderDialog.SelectedPath = Path.GetFullPath(EditorHelper.DataDirectory);
			folderDialog.Description = "Select a folder to process..";
			if (folderDialog.ShowDialog(this) == DialogResult.OK)
			{
				RenameTypeDialog dialog = new RenameTypeDialog(this.GetAvailTypes());
				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					int replaced = 0;
					this.BatchPerformAction(folderDialog.SelectedPath, n => replaced += n.ReplaceTypeStrings(dialog.SearchFor, dialog.ReplaceWith));
					MessageBox.Show(
						string.Format(PluginRes.ResourceHackerRes.MessageBox_RenameType_Text, replaced, dialog.SearchFor, dialog.ReplaceWith), 
						PluginRes.ResourceHackerRes.MessageBox_RenameType_Title, 
						MessageBoxButtons.OK, 
						MessageBoxIcon.Information);
				}
			}
		}
	}
}
