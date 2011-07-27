using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Duality.Components
{
	[Flags]
	public enum RendererFlags : uint
	{
		None				= 0x00000000,

		ParallaxPos			= 0x00000001,
		ParallaxScale		= 0x00000002,
		Parallax			= ParallaxPos | ParallaxScale,

		All					= Parallax,
		Default				= Parallax
	}

	[Serializable]
	public abstract class Renderer : Component
	{
		private	RendererFlags	renderFlags		= RendererFlags.Default;
		private	uint			visibilityGroup	= 1;

		public RendererFlags RenderFlags
		{
			get { return this.renderFlags; }
			set { this.renderFlags = value; }
		}
		public uint VisibilityGroup
		{
			get { return this.visibilityGroup; }
			set { this.visibilityGroup = value; }
		}
		public abstract float BoundRadius { get; }
		public virtual bool IsInfiniteXY { get { return false; } }

		public abstract void Draw(IDrawDevice device);

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
	}
}
