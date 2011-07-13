using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using Duality.Components;

namespace Duality.ObjectManagers
{
	[Serializable]
	public class RendererManager : ObjectManager<Renderer>
	{
		public IEnumerable<Renderer> QueryVisible(IDrawDevice device)
		{
			foreach (Renderer r in this.ActiveObjects)
			{
				if (r.IsVisible(device))
					yield return r;
			}
		}
	}

	[Serializable]
	public class OverlayRendererManager : ObjectManager<Component>
	{
		public IEnumerable<ICmpScreenOverlayRenderer> QueryVisible(IDrawDevice device)
		{
			foreach (Component r in this.ActiveObjects)
			{
				ICmpScreenOverlayRenderer overlayRenderer = r as ICmpScreenOverlayRenderer;
				if (overlayRenderer != null && overlayRenderer.IsVisible(device)) yield return overlayRenderer;
			}
		}
	}
}
