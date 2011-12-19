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
				if (value == null) throw new ArgumentNullException("value");
				if (this._filter != value)
				{
					this._filter = value;
					this.Refresh();
				}
			}
		}
		

		public FilteredTreeModel(Predicate<object> filter, ITreeModel innerModel)
		{
			if (filter == null) throw new ArgumentNullException("filter");
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
