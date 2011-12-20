using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Aga.Controls.Tree;

namespace TestApp
{
	public partial class MainForm : Form
	{
		private TreeModel model;
		private FilteredTreeModel filteredModel;

		public MainForm()
		{
			InitializeComponent();

			this.model = new TreeModel();
			this.model.Nodes.Add(new Node("Root0"));
			this.model.Nodes.Add(new Node("Root1"));
			this.model.Nodes.Add(new Node("Root2"));
			this.model.Nodes[1].Nodes.Add(new Node("Child0"));
			this.model.Nodes[1].Nodes.Add(new Node("Child1"));
			this.model.Nodes[1].Nodes.Add(new Node("Child2"));

			this.filteredModel = new FilteredTreeModel(this.filter, this.model);
			//this.filteredModel = new FilteredTreeModel(this.filter);
			//this.filteredModel.Nodes.Add(new Node("Root0"));
			//this.filteredModel.Nodes.Add(new Node("Root1"));
			//this.filteredModel.Nodes.Add(new Node("Root2"));
			//this.filteredModel.Nodes[1].Nodes.Add(new Node("Child0"));
			//this.filteredModel.Nodes[1].Nodes.Add(new Node("Child1"));
			//this.filteredModel.Nodes[1].Nodes.Add(new Node("Child2"));
			this.treeViewAdv1.Model = this.filteredModel;
		}

		private bool filter(object obj)
		{
			Node n = obj as Node;
			return n.Text.ToUpper().Contains(this.textBox1.Text.ToUpper()) || n.Nodes.Any(filter);
		}
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			this.filteredModel.Refresh();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			Node n = (this.treeViewAdv1.SelectedNode.Tag as Node);
			n.Parent.Nodes.Remove(n);
		}
	}
}
