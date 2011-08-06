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

using DualityEditor;

using Aga.Controls.Tree;
using WeifenLuo.WinFormsUI.Docking;

namespace ResourceHacker
{
	public partial class ResourceHacker : DockContent
	{
		protected class DataTreeNode : Node
		{
			protected	BinaryMetaFormatter.DataNode	data;

			public BinaryMetaFormatter.DataNode Data
			{
				get { return this.data; }
			}
			public string NodeTypeName
			{
				get { return ReflectionHelper.GetTypeString(this.data.GetType(), ReflectionHelper.TypeStringAttrib.Keyword); }
			}

			protected DataTreeNode(BinaryMetaFormatter.DataNode data)
			{
				this.data = data;
				this.Text = this.NodeTypeName;
				this.Image = GetIcon(this.data);
			}

			public static DataTreeNode Create(BinaryMetaFormatter.DataNode data)
			{
				if (data is BinaryMetaFormatter.ObjectNode)
					return new ObjectTreeNode(data as BinaryMetaFormatter.ObjectNode);
				else if (data is BinaryMetaFormatter.ObjectRefNode)
					return new ObjectRefTreeNode(data as BinaryMetaFormatter.ObjectRefNode);
				else if (data is BinaryMetaFormatter.PrimitiveNode)
					return new PrimitiveTreeNode(data as BinaryMetaFormatter.PrimitiveNode);
				else if (data is BinaryMetaFormatter.StringNode)
					return new StringTreeNode(data as BinaryMetaFormatter.StringNode);
				else
					return new DataTreeNode(data);
			}
			public static Image GetIcon(BinaryMetaFormatter.DataNode data)
			{
				if (data is BinaryMetaFormatter.PrimitiveNode) return PluginRes.ResourceHackerRes.IconPrimitive;
				else if (data is BinaryMetaFormatter.ArrayNode) return PluginRes.ResourceHackerRes.IconArray;
				else if (data is BinaryMetaFormatter.ConstructorInfoNode) return PluginRes.ResourceHackerRes.IconMethod;
				else if (data is BinaryMetaFormatter.DelegateNode) return PluginRes.ResourceHackerRes.IconDelegate;
				else if (data is BinaryMetaFormatter.EventInfoNode) return PluginRes.ResourceHackerRes.IconEvent;
				else if (data is BinaryMetaFormatter.FieldInfoNode) return PluginRes.ResourceHackerRes.IconField;
				else if (data is BinaryMetaFormatter.MethodInfoNode) return PluginRes.ResourceHackerRes.IconMethod;
				else if (data is BinaryMetaFormatter.PropertyInfoNode) return PluginRes.ResourceHackerRes.IconProperty;
				else if (data is BinaryMetaFormatter.StructNode) return PluginRes.ResourceHackerRes.IconObject;
				else if (data is BinaryMetaFormatter.TypeInfoNode) return PluginRes.ResourceHackerRes.IconClass;
				else if (data is BinaryMetaFormatter.ObjectRefNode) return PluginRes.ResourceHackerRes.IconObjectRef;
				else
					return PluginRes.ResourceHackerRes.IconPrimitive;
			}
		}
		protected class PrimitiveTreeNode : DataTreeNode
		{
			protected	BinaryMetaFormatter.PrimitiveNode	primitiveData;

			public string ResolvedTypeName
			{
				get 
				{ 
					Type actualType = this.primitiveData.NodeType.ToActualType();
					return actualType != null ? ReflectionHelper.GetTypeString(actualType, ReflectionHelper.TypeStringAttrib.Keyword) : "Unknown";
				}
			}

			public PrimitiveTreeNode(BinaryMetaFormatter.PrimitiveNode data) : base(data)
			{
				this.primitiveData = data;
			}
		}
		protected class StringTreeNode : DataTreeNode
		{
			protected	BinaryMetaFormatter.StringNode	stringData;

			public string ResolvedTypeName
			{
				get { return ReflectionHelper.GetTypeString(typeof(string), ReflectionHelper.TypeStringAttrib.Keyword); }
			}

			public StringTreeNode(BinaryMetaFormatter.StringNode data) : base(data)
			{
				this.stringData = data;
			}
		}
		protected class ObjectTreeNode : DataTreeNode
		{
			protected	BinaryMetaFormatter.ObjectNode	objData;

			public uint ObjId
			{
				get { return this.objData.ObjId; }
			}
			public Type ResolvedType
			{
				get { return Duality.Serialization.SerializationHelper.ResolveType(this.objData.TypeString, false); }
			}
			public string ResolvedTypeName
			{
				get 
				{ 
					Type resType = this.ResolvedType;
					return resType != null ? ReflectionHelper.GetTypeString(resType, ReflectionHelper.TypeStringAttrib.CSCodeIdentShort) : objData.TypeString;
				}
			}

			public ObjectTreeNode(BinaryMetaFormatter.ObjectNode data) : base(data)
			{
				this.objData = data;
			}
		}
		protected class ObjectRefTreeNode : DataTreeNode
		{
			protected	BinaryMetaFormatter.ObjectRefNode	objRefData;

			public uint ObjId
			{
				get { return this.objRefData.ObjRefId; }
			}

			public ObjectRefTreeNode(BinaryMetaFormatter.ObjectRefNode data) : base(data)
			{
				this.objRefData = data;
			}
		}

		private	string				filePath	= null;
		private	BinaryMetaFormatter	formatter	= new BinaryMetaFormatter();
		private	TreeModel			dataModel	= new TreeModel();


		public ResourceHacker()
		{
			this.InitializeComponent();
			this.treeView.Model = this.dataModel;

			this.nodeTextBoxObjId.DrawText += new EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(nodeTextBoxObjId_DrawText);
			this.nodeTextBoxType.DrawText += new EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(nodeTextBoxType_DrawText);

			this.openFileDialog.InitialDirectory = Path.GetFullPath(EditorHelper.DataDirectory);
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
			this.filePath = filePath;
			using (FileStream fileStream = File.OpenRead(this.filePath))
			{
				this.formatter.ReadTarget = new BinaryReader(fileStream);
				this.formatter.WriteTarget = null;

				BinaryMetaFormatter.DataNode dataNode;
				while ((dataNode = this.formatter.ReadObject()) != null)
				{
					this.dataModel.Nodes.Add(this.AddData(dataNode));
				}
			}
		}
		public void SaveFile(string filePath)
		{

		}

		protected void ClearData()
		{
			this.dataModel.Nodes.Clear();
		}
		protected DataTreeNode AddData(BinaryMetaFormatter.DataNode data)
		{
			DataTreeNode dataNode = DataTreeNode.Create(data);
			foreach (BinaryMetaFormatter.DataNode child in data.SubNodes)
			{
				DataTreeNode childDataNode = this.AddData(child);
				childDataNode.Parent = dataNode;
			}
			return dataNode;
		}
		protected bool IsObjectIdExisting(uint objId, BinaryMetaFormatter.DataNode baseNode = null)
		{
			if (objId == 0) return false;

			if (baseNode == null)
			{
				foreach (DataTreeNode treeNode in this.dataModel.Nodes)
					if (this.IsObjectIdExisting(objId, treeNode.Data)) return true;
			}
			else if (baseNode is BinaryMetaFormatter.ObjectNode && (baseNode as BinaryMetaFormatter.ObjectNode).ObjId == objId)
			{
				return true;
			}
			else
			{
				foreach (BinaryMetaFormatter.DataNode subNode in baseNode.SubNodes)
					if (this.IsObjectIdExisting(objId, subNode)) return true;
			}

			return false;
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
		}
		private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			this.SaveFile(this.saveFileDialog.FileName);
		}
		private void treeView_SelectionChanged(object sender, EventArgs e)
		{
			DataTreeNode selNode = this.treeView.SelectedNode.Tag as DataTreeNode;
			this.propertyGrid.SelectObjects(new[] {selNode.Data});
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
	}
}
