using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Aga.Controls.Tree
{
	public class FilteredTreeModel : TreeModelBase
	{
		private ITreeModel _innerModel;
		public ITreeModel InnerModel
		{
			get { return _innerModel; }
		}

		private Predicate<object> _filter = null;
		public Predicate<object> Filter
		{
			get { return _filter; }
			set 
			{ 
				if (this._filter != value)
				{
					this._filter = value;
					this.Refresh();
				}
			}
		}
		

		public FilteredTreeModel(Predicate<object> filter, ITreeModel innerModel)
		{
			this._filter = filter;

			if (innerModel == null) throw new ArgumentNullException("innerModel");
			this._innerModel = innerModel;
			this._innerModel.NodesChanged += this._innerModel_NodesChanged;
			this._innerModel.NodesInserted += this._innerModel_NodesInserted;
			this._innerModel.NodesRemoved += this._innerModel_NodesRemoved;
			this._innerModel.StructureChanged += this._innerModel_StructureChanged;
		}

		public override System.Collections.IEnumerable GetChildren(TreePath treePath)
		{
			if (this.Filter != null)
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList();
				System.Collections.IEnumerable res = InnerModel.GetChildren(treePath);
				if (res != null)
				{
					foreach (object obj in res)
					{
						if (!this.Filter(obj)) continue;
						list.Add(obj);
					}
					return list;
				}
				else
					return null;
			}
			else
				return InnerModel.GetChildren(treePath);
		}
		public override bool IsLeaf(TreePath treePath)
		{
			return InnerModel.IsLeaf(treePath);
		}

		private void _innerModel_StructureChanged(object sender, TreePathEventArgs e)
		{
			OnStructureChanged(e);
		}
		private void _innerModel_NodesRemoved(object sender, TreeModelEventArgs e)
		{
			if (this.Filter == null)
			{
				OnNodesRemoved(e);
				return;
			}

			bool allFiltered = true;
			foreach (object o in e.Children)
			{
				if (this.Filter(o))
				{
					allFiltered = false;
					break;
				}
			}
			if (allFiltered) return;
			// Properly implement this. Need to map incoming data indices and children to filtered view
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}
		private void _innerModel_NodesInserted(object sender, TreeModelEventArgs e)
		{
			if (this.Filter == null)
			{
				OnNodesInserted(e);
				return;
			}

			bool allFiltered = true;
			foreach (object o in e.Children)
			{
				if (this.Filter(o))
				{
					allFiltered = false;
					break;
				}
			}
			if (allFiltered) return;
			// Properly implement this. Need to map incoming data indices and children to filtered view
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}
		private void _innerModel_NodesChanged(object sender, TreeModelEventArgs e)
		{
			if (this.Filter == null)
			{
				OnNodesChanged(e);
				return;
			}

			bool allFiltered = true;
			foreach (object o in e.Children)
			{
				if (this.Filter(o))
				{
					allFiltered = false;
					break;
				}
			}
			if (allFiltered) return;
			// Properly implement this. Need to map incoming data indices and children to filtered view
			OnStructureChanged(new TreePathEventArgs(e.Path));
		}
	}
}


//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Collections.ObjectModel;

//namespace Aga.Controls.Tree
//{
//    public class FilteredTreeModel : TreeModel
//    {
//        private Predicate<Node> filter = null;

//        public Predicate<Node> Filter
//        {
//            get { return filter; }
//            set 
//            { 
//                if (value == null) throw new ArgumentNullException("value");
//                if (this.filter != value)
//                {
//                    this.filter = value;
//                    this.Refresh();
//                }
//            }
//        }
		

//        public FilteredTreeModel(Predicate<Node> filter)
//        {
//            if (filter == null) throw new ArgumentNullException("filter");
//            this.filter = filter;
//        }

//        public void Refresh()
//        {
//            this.OnStructureChanged(new TreePathEventArgs(TreePath.Empty));
//        }
		
//        private bool IsPathVisible(Node node)
//        {
//            if (node.Parent != null)
//                return this.Filter(node) && this.IsPathVisible(node.Parent);
//            else
//                return true;
//        }
//        private int MapToFilteredIndex(Node parent, int index, Node node)
//        {
//            for (int i = index - 1; i >= 0; i--)
//            {
//                if (!this.Filter(parent.Nodes[i])) index--;
//            }
//            return index;
//        }

//        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
//        {
//            Node node = FindNode(treePath);
//            if (node != null)
//            {
//                if (this.Filter != null)
//                {
//                    foreach (Node n in node.Nodes)
//                    {
//                        if (!this.Filter(n)) continue;
//                        yield return n;
//                    }
//                }
//                else
//                {
//                    foreach (Node n in node.Nodes)
//                        yield return n;
//                }
//            }
//            else yield break;
//        }
//        protected internal override void OnNodeRemoved(Node parent, int index, Node node)
//        {
//            //if (!this.IsPathVisible(node)) return;
//            //index = this.MapToFilteredIndex(parent, index, node);
//            base.OnNodeRemoved(parent, index, node);
//        }
//        protected internal override void OnNodeInserted(Node parent, int index, Node node)
//        {
//            //if (!this.IsPathVisible(node)) return;
//            //index = this.MapToFilteredIndex(parent, index, node);
//            base.OnNodeInserted(parent, index, node);
//        }
//        protected internal override void OnNodesChanged(Node parent, int index, Node node)
//        {
//            //if (!this.IsPathVisible(node)) return;
//            //index = this.MapToFilteredIndex(parent, index, node);
//            base.OnNodesChanged(parent, index, node);
//        }
//    }
//}
