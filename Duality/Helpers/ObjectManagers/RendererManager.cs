using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using Duality.Components;

namespace Duality.ObjectManagers
{
	/// <summary>
	/// Manages a set of <see cref="Duality.Components.Renderer">Renderers</see> and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	[Serializable]
	public class RendererManager : ObjectManager<Renderer>
	{
		/// <summary>
		/// Enumerates all <see cref="Duality.Components.Renderer">Renderers</see> that are visible to the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public IEnumerable<Renderer> QueryVisible(IDrawDevice device)
		{
			foreach (Renderer r in this.ActiveObjects)
			{
				if (r.IsVisible(device))
					yield return r;
			}
		}
	}
	
	/// <summary>
	/// Manages a set of <see cref="ICmpScreenOverlayRenderer">Screen Overlays</see> and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	[Serializable]
	public class OverlayRendererManager : ObjectManager<Component>
	{
		/// <summary>
		/// Enumerates all <see cref="ICmpScreenOverlayRenderer">Screen Overlays</see> that are visible to the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
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
