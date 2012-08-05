using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using Aga.Controls;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using Ionic.Zip;

using Duality;

using DualityEditor.Controls.TreeModels.FileSystem;

namespace DualityEditor.Forms
{
	public partial class NewProjectDialog : Form
	{
		private class TemplateEntry
		{
			private string	file;
			private	Bitmap	icon;
			private	string	name;
			private	string	desc;

			public string FilePath
			{
				get { return this.file; }
				set { this.file = value; }
			}
			public Bitmap Icon
			{
				get { return this.icon; }
				set { this.icon = value; }
			}
			public string Name
			{
				get { return this.name; }
				set { this.name = value; }
			}
			public string Description
			{
				get { return this.desc; }
				set { this.desc = value; }
			}

			public TemplateEntry() {}
			public TemplateEntry(string templatePath)
			{
				if (string.IsNullOrEmpty(templatePath)) throw new ArgumentNullException("templatePath");
				if (Path.GetExtension(templatePath) != ".zip") throw new ArgumentException("The specified template path is expected to be a .zip file.", "templatePath");
				if (!File.Exists(templatePath)) throw new FileNotFoundException("Template file does not exist", templatePath);

				using (FileStream str = File.OpenRead(templatePath)) { this.InitFrom(str); }
				this.file = templatePath;
			}
			public TemplateEntry(Stream templateStream)
			{
				this.InitFrom(templateStream);
			}

			public void InitFrom(Stream templateStream)
			{
				if (templateStream == null) throw new ArgumentNullException("templateStream");

				this.file = null;
				this.name = "Unknown";

				using (ZipFile templateZip = ZipFile.Read(templateStream))
				{
					ZipEntry entryInfo = templateZip.FirstOrDefault(z => !z.IsDirectory && z.FileName == "TemplateInfo.xml");
					ZipEntry entryIcon = templateZip.FirstOrDefault(z => !z.IsDirectory && z.FileName == "TemplateIcon.png");

					if (entryIcon != null)
					{
						using (MemoryStream str = new MemoryStream())
						{
							entryIcon.Extract(str);
							str.Seek(0, SeekOrigin.Begin);
							this.icon = new Bitmap(str);
						}
					}

					if (entryInfo != null)
					{
						string xmlSource = null;
						using (MemoryStream str = new MemoryStream())
						{
							entryInfo.Extract(str);
							str.Seek(0, SeekOrigin.Begin);
							
							using (StreamReader reader = new StreamReader(str))
							{
								xmlSource = reader.ReadToEnd();
							}
						}

						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.LoadXml(xmlSource);

						XmlElement elemName = xmlDoc.DocumentElement["name"];
						if (elemName != null) this.name = elemName.InnerText;

						XmlElement elemDesc = xmlDoc.DocumentElement["description"];
						if (elemDesc != null) this.desc = elemDesc.InnerText;
					}
				}

				return;
			}
		}

		private	FolderBrowserTreeModel	folderModel				= null;
		private	string					selectedTemplatePath	= null;

		public NewProjectDialog()
		{
			this.InitializeComponent();
			this.templateView_Resize(this, EventArgs.Empty); // Trigger update tile size

			this.folderModel = new FolderBrowserTreeModel(EditorHelper.GlobalProjectTemplateDirectory);
			this.folderModel.Filter = s => Directory.Exists(s); // Only show directories
			this.folderView.Model = this.folderModel;

			this.folderViewControlName.DrawText += this.folderViewControlName_DrawText;

			this.selectedTemplatePath = this.folderModel.BasePath;

			// Hilde folder selector, if empty
			if (Directory.GetDirectories(this.folderModel.BasePath).Length == 0)
			{
				this.folderView.Enabled = false;
				this.splitFolderTemplate.Panel1Collapsed = true;
			}
		}

		protected void UpdateTemplateList()
		{
			this.templateView.BeginUpdate();
			this.templateView.Items.Clear();
			this.imageListTemplateView.Images.Clear();

			string[] templateFiles = Directory.GetFiles(this.selectedTemplatePath, "*.zip", SearchOption.TopDirectoryOnly);
			List<TemplateEntry> templateEntries = new List<TemplateEntry>();
			foreach (string templateFile in templateFiles)
			{
				try
				{
					TemplateEntry entry = new TemplateEntry(templateFile);
					templateEntries.Add(entry);
				}
				catch (Exception e)
				{
					Log.Editor.WriteError("Can't load project template {0} because an error occured in the process: {1}", templateFile, Log.Exception(e));
				}
			}

			foreach (TemplateEntry entry in templateEntries)
			{
				Bitmap icon = entry.Icon;
				if (icon.Size != this.imageListTemplateView.ImageSize)
					icon = icon.Rescale(this.imageListTemplateView.ImageSize.Width, this.imageListTemplateView.ImageSize.Height);
				this.imageListTemplateView.Images.Add(entry.FilePath, icon);

				ListViewItem item = new ListViewItem(new string[] { entry.Name, entry.Description }, entry.FilePath);
				item.Tag = entry;
				item.ToolTipText = entry.Description;
				this.templateView.Items.Add(item);
			}

			this.templateView.EndUpdate();
		}

		private void folderViewControlName_DrawText(object sender, DrawEventArgs e)
		{
			e.TextColor = Color.Black;
		}

		private void templateView_Resize(object sender, EventArgs e)
		{
			this.templateView.TileSize = new Size(this.templateView.ClientSize.Width, this.templateView.TileSize.Height);
		}
		private void templateView_SelectedIndexChanged(object sender, EventArgs e)
		{
			TemplateEntry entry = this.templateView.SelectedItems.Count > 0 ? this.templateView.SelectedItems[0].Tag as TemplateEntry : null;
			if (entry == null) return;
			
			this.textBoxTemplate.Text = entry.FilePath;
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
			this.selectedTemplatePath = folderItem != null ? folderItem.ItemPath : this.folderModel.BasePath;
			this.UpdateTemplateList();
		}
	}
}
