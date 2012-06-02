using System;
using System.Collections.Generic;
using System.Linq;

namespace Duality.ObjectManagers
{
	/// <summary>
	/// Manages a set of <see cref="Duality.Components.Renderer">Renderers</see> and exposes suitable object enumerations as well as un/registeration events.
	/// If a registered object has been disposed, it will be automatically unregistered.
	/// </summary>
	[Serializable]
	public class RendererManager : ObjectManager<Component>
	{
		/// <summary>
		/// Enumerates all <see cref="Duality.Components.Renderer">Renderers</see> that are visible to the specified <see cref="IDrawDevice"/>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public IEnumerable<ICmpRenderer> QueryVisible(IDrawDevice device)
		{
			return this.ActiveObjects.OfType<ICmpRenderer>().Where(renderer => renderer.IsVisible(device));
		}
	}
}
