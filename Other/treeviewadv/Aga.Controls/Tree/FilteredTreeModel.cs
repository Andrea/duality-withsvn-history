using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Aga.Controls.Tree
{
	public class FilteredTreeModel : TreeModel
	{
		private Predicate<Node> filter = null;

		public Predicate<Node> Filter
		{
			get { return filter; }
			set 
			{ 
				if (value == null) throw new ArgumentNullException("value");
				if (this.filter != value)
				{
					this.filter = value;
					this.Refresh();
				}
			}
		}
		

		public FilteredTreeModel(Predicate<Node> filter)
		{
			if (filter == null) throw new ArgumentNullException("filter");
			this.filter = filter;
		}

		public void Refresh()
		{
			this.OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
		}
		
		private bool IsPathVisible(Node node)
		{
			if (node.Parent != null)
				return this.Filter(node) && this.IsPathVisible(node.Parent);
			else
				return true;
		}
		private int MapToFilteredIndex(Node parent, int index, Node node)
		{
			for (int i = index - 1; i >= 0; i--)
			{
				if (!this.Filter(parent.Nodes[i])) index--;
			}
			return index;
		}

		public override System.Collections.IEnumerable GetChildren(TreePath treePath)
		{
			Node node = FindNode(treePath);
			if (node != null)
			{
				if (this.Filter != null)
				{
					foreach (Node n in node.Nodes)
					{
						if (!this.Filter(n)) continue;
						yield return n;
					}
				}
				else
				{
					foreach (Node n in node.Nodes)
						yield return n;
				}
			}
			else yield break;
		}
		protected internal override void OnNodeRemoved(Node parent, int index, Node node)
		{
			//if (!this.IsPathVisible(node)) return;
			//index = this.MapToFilteredIndex(parent, index, node);
			base.OnNodeRemoved(parent, index, node);
		}
		protected internal override void OnNodeInserted(Node parent, int index, Node node)
		{
			//if (!this.IsPathVisible(node)) return;
			//index = this.MapToFilteredIndex(parent, index, node);
			base.OnNodeInserted(parent, index, node);
		}
		protected internal override void OnNodesChanged(Node parent, int index, Node node)
		{
			//if (!this.IsPathVisible(node)) return;
			//index = this.MapToFilteredIndex(parent, index, node);
			base.OnNodesChanged(parent, index, node);
		}
	}
}
