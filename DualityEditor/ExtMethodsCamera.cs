using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Components;

namespace DualityEditor
{
	public static class ExtMethodsCamera
	{
		public static void AddEditorRendererFilter(this Camera c, Predicate<ICmpRenderer> predicate)
		{
			c.AddEditorRendererFilter(predicate);
		}
		public static void RemoveEditorRendererFilter(this Camera c, Predicate<ICmpRenderer> predicate)
		{
			c.RemoveEditorRendererFilter(predicate);
		}
	}
}
