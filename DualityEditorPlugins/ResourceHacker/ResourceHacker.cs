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
		private	string				filePath	= null;
		private	BinaryMetaFormatter	formatter	= new BinaryMetaFormatter();
		private	TreeModel			dataModel	= new TreeModel();


		public ResourceHacker()
		{
			this.InitializeComponent();
			this.treeView.Model = this.dataModel;

			this.openFileDialog.InitialDirectory = Path.GetFullPath(EditorHelper.DataDirectory);
			this.openFileDialog.Filter = "Duality Resource|*" + Resource.FileExt;
			this.saveFileDialog.InitialDirectory = this.openFileDialog.InitialDirectory;
			this.saveFileDialog.Filter = this.openFileDialog.Filter;
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
		protected Node AddData(BinaryMetaFormatter.DataNode data)
		{
			Node dataNode = new Node(ReflectionHelper.GetTypeString(data.GetType(), ReflectionHelper.TypeStringAttrib.Keyword));
			foreach (BinaryMetaFormatter.DataNode child in data.SubNodes)
			{
				Node childDataNode = this.AddData(child);
				childDataNode.Parent = dataNode;
			}
			return dataNode;
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
	}
}
