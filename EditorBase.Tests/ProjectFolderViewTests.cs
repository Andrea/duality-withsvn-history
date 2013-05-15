using Aga.Controls.Tree;
using NUnit.Framework;

namespace EditorBase.Tests
{
	[TestFixture]
	public class ProjectFolderViewTests
	{
		[Test]
		public void When_UnregisterNodeTree_Then_all_child_nodes_are_unregistered()
		{
			var view = new TestProjectFolderView();
			var root = new ProjectFolderView.DirectoryNode("root");
			var childDirNode = new ProjectFolderView.DirectoryNode("childDir");
			root.Nodes.Add(childDirNode);
			root.Nodes.Add(new ProjectFolderView.ResourceNode("resourceNode"));

			view.TestRegisterNodeTree(root);
			view.TestUnregisterTreeNode(root);

			Assert.AreEqual(0, view.PathIdToNode.Count);
		}

		private class TestProjectFolderView : ProjectFolderView
		{
			public void TestRegisterNodeTree(Node node)
			{
				RegisterNodeTree(node);
			}
			
			public void TestUnregisterTreeNode(Node node)
			{
				UnregisterNodeTree(node);
			}
		}
	}
}
