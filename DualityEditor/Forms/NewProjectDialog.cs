using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Aga.Controls;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using DualityEditor.Controls.TreeModels.FileSystem;

namespace DualityEditor.Forms
{
	public partial class NewProjectDialog : Form
	{
		private	FolderBrowserTreeModel	folderModel	= null;

		public NewProjectDialog()
		{
			this.InitializeComponent();
			this.templateView_Resize(this, EventArgs.Empty); // Trigger update tile size

			this.folderModel = new FolderBrowserTreeModel(EditorHelper.GlobalProjectTemplateDirectory);
			this.folderModel.Filter = s => Directory.Exists(s); // Only show directories
			this.folderView.Model = this.folderModel;

			this.folderViewControlName.DrawText += this.folderViewControlName_DrawText;
		}

		private void folderViewControlName_DrawText(object sender, DrawEventArgs e)
		{
			e.TextColor = Color.Black;
		}

		private void templateView_Resize(object sender, EventArgs e)
		{
			this.templateView.TileSize = new Size(this.templateView.ClientSize.Width, this.templateView.TileSize.Height);
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		private void buttonOk_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			// Do stuff

			// Ask if the selected template should be copied to the template directory, if not located there (auto-install)

			this.Close();
		}

		private void buttonBrowseTemplate_Click(object sender, EventArgs e)
		{

		}
		private void buttonBrowseFolder_Click(object sender, EventArgs e)
		{

		}

		private void textBoxTemplate_TextChanged(object sender, EventArgs e)
		{

		}
		private void textBoxName_TextChanged(object sender, EventArgs e)
		{

		}
		private void textBoxFolder_TextChanged(object sender, EventArgs e)
		{

		}

		private void folderView_SelectionChanged(object sender, EventArgs e)
		{
			FolderItem folderItem = this.folderView.SelectedNode != null ? this.folderView.SelectedNode.Tag as FolderItem : null;
			string selectedPath = folderItem != null ? folderItem.ItemPath : this.folderModel.BasePath;
			Console.WriteLine(selectedPath);
		}
	}
}
