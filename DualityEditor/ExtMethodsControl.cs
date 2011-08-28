using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DualityEditor
{
	public static class ExtMethodsControl
	{
		public static Control GetChildAtPointDeep(this Control c, Point pt, GetChildAtPointSkip skip)
		{
			Point globalPt = c.PointToScreen(pt);
			Control child = c.GetChildAtPoint(pt, skip);
			Control deeperChild = child;
			while (deeperChild != null)
			{
				child = deeperChild;
				deeperChild = deeperChild.GetChildAtPoint(deeperChild.PointToClient(globalPt), skip);
			}
			return deeperChild ?? child;
		}
		public static T GetControlAncestor<T>(this Control c) where T : class
		{
			while (c != null)
			{
				if (c is T) return c as T;
				c = c.Parent;
			}
			return null;
		}
		public static IEnumerable<T> GetControlAncestors<T>(this Control c) where T : class
		{
			while (c != null)
			{
				if (c is T) yield return c as T;
				c = c.Parent;
			}
			yield break;
		}
	}
}
