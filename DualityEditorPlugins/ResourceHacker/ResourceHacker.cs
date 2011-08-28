using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
			protected	TypeDataLayout.FieldDataInfo	fieldInfo;

			public DataNode Data
			{
				get { return this.data; }
			}
			public string NodeTypeName
			{
				get { return this.data.GetType().GetTypeKeyword(); }
			}
			public TypeDataLayout.FieldDataInfo FieldInfo
			{
				get { return this.fieldInfo; }
				set
				{
					this.fieldInfo = value;
					if (!String.IsNullOrEmpty(this.fieldInfo.name))
						this.Text = this.fieldInfo.name;
					else
						this.Text = this.NodeTypeName;
				}
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
			public Type ResolvedType
			{
				get { return ReflectionHelper.ResolveType(this.objData.TypeString, false); }
			}
			public string ResolvedTypeName
			{
				get 
				{ 
					Type resType = this.ResolvedType;
					return resType != null ? resType.GetTypeCSCodeName(true) : objData.TypeString;
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
		private	BinaryMetaFormatter	formatter	= new BinaryMetaFormatter();
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
				this.formatter.ReadTarget = new BinaryReader(fileStream);
				this.formatter.WriteTarget = null;

				DataNode dataNode;
				try
				{
					this.treeView.BeginUpdate();
					while ((dataNode = this.formatter.ReadObject()) != null)
					{
						DataTreeNode data = this.AddData(dataNode);
						this.rootData.Add(data);
					}
				}
				catch (EndOfStreamException) {}
				finally
				{
					foreach (DataTreeNode n in this.rootData) this.dataModel.Nodes.Add(n);
					this.treeView.EndUpdate(); 
				}
			}
		}
		public void SaveFile(string filePath)
		{
			this.filePath = filePath;

			using (FileStream fileStream = File.Open(this.filePath, FileMode.Create, FileAccess.Write))
			{
				this.formatter.ReadTarget = null;
				this.formatter.WriteTarget = new BinaryWriter(fileStream);

				foreach (DataTreeNode dataNode in this.dataModel.Nodes)
					this.formatter.WriteObject(dataNode.Data);
			}

			// Assure reloading the modified resource
			string dataPath = PathHelper.MakePathRelative(this.filePath, ".");
			ContentProvider.UnregisterContent(dataPath, true);
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

			DataTreeNode dataNode = updateNode as DataTreeNode;
			StructNode structData = null;
			TypeDataLayoutNode structLayoutData = null;
			TypeDataLayout structLayout = null;
			if (dataNode != null) structData = dataNode.Data as StructNode;
			if (structData != null) this.typeDataLayout.TryGetValue(structData.TypeString, out structLayoutData);
			if (structLayoutData != null) structLayout = structLayoutData.Layout;
			if (structLayout != null)
			{
				int index = 0;
				foreach (DataTreeNode n in updateNode.Nodes)
				{
					if (n.Data is TypeDataLayoutNode) index--;
					if (index >= 0) n.FieldInfo = structLayout.Fields[index];
					index++;
				}
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
		protected int ReplaceTypeStrings(string oldTypeString, string newTypeString)
		{
			int count = 0;
			foreach (DataTreeNode dataNode in this.rootData)
				count += dataNode.Data.ReplaceTypeStrings(oldTypeString, newTypeString);
			return count;
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
			if (objNode != null && objNode.ResolvedType == null) e.TextColor = Color.Red;
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
				int replaced = this.ReplaceTypeStrings(dialog.SearchFor, dialog.ReplaceWith);
				MessageBox.Show(
					string.Format(PluginRes.ResourceHackerRes.MessageBox_RenameType_Text, replaced, dialog.SearchFor, dialog.ReplaceWith), 
					PluginRes.ResourceHackerRes.MessageBox_RenameType_Title, 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Information);
			}
		}
		private void actionRenameField_Click(object sender, EventArgs e)
		{

		}
	}
}
