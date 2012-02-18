using System;
using System.Collections.Generic;
using System.Linq;

using Duality.EditorHints;

using OpenTK;

namespace Duality.Components
{
	/// <summary>
	/// A Renderer usually gives its <see cref="GameObject"/> a visual appearance in space.
	/// However, in general it may render anything and isn't bound by any conceptual restrictions.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public abstract class Renderer : Component, ICmpRenderer
	{
		private	RendererFlags	renderFlags		= RendererFlags.Default;
		private	uint			visibilityGroup	= 1;

		/// <summary>
		/// [GET / SET] The <see cref="RendererFlags">appearance flags</see> of this Renderer.
		/// </summary>
		public RendererFlags RenderFlags
		{
			get { return this.renderFlags; }
			set { this.renderFlags = value; }
		}
		/// <summary>
		/// [GET / SET] A bitmask that informs about the set of visibility groups to which this Renderer
		/// belongs. Usually, a Renderer is considered visible to a <see cref="Duality.Components.Camera"/> if they
		/// share at least one mutual visibility group.
		/// </summary>
		public uint VisibilityGroup
		{
			get { return this.visibilityGroup; }
			set { this.visibilityGroup = value; }
		}
		/// <summary>
		/// [GET] The Renderers bounding radius, originating from the <see cref="GameObject">GameObjects</see> position.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public abstract float BoundRadius { get; }
		/// <summary>
		/// [GET] Returns whether this Renderer visually stretches infinitely on the XY-plane.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public virtual bool IsInfiniteXY { get { return false; } }

		/// <summary>
		/// Performs the Renderers drawing operation.
		/// </summary>
		/// <param name="device"></param>
		public abstract void Draw(IDrawDevice device);

		/// <summary>
		/// Determines if the Renderer is visible to the specified <see cref="IDrawDevice"/>.
		/// This is usually the case if they share at least one mutual <see cref="VisibilityGroup">visibility group</see>.
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public bool IsVisible(IDrawDevice device)
		{
			if ((this.visibilityGroup & device.VisibilityMask) == 0) return false;
			return device.IsRendererInView(this);
		}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Renderer t = target as Renderer;
			t.renderFlags		= this.renderFlags;
			t.visibilityGroup	= this.visibilityGroup;
		}

		Vector3 ICmpRenderer.SpaceCoord
		{
			get { return this.gameobj.Transform.Pos; }
		}
	}
}
