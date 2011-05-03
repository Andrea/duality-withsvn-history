using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics;

namespace Duality
{
	public class GraphicsModeComparer : IEqualityComparer<GraphicsMode>
	{
		public bool Equals(GraphicsMode x, GraphicsMode y)
		{
			return 
				x.AccumulatorFormat == y.AccumulatorFormat &&
				x.Buffers == y.Buffers &&
				x.ColorFormat == y.ColorFormat &&
				x.Depth == y.Depth &&
				x.Samples == y.Samples &&
				x.Stencil == y.Stencil &&
				x.Stereo == y.Stereo;
		}
		public int GetHashCode(GraphicsMode obj)
		{
			return 
				obj.AccumulatorFormat.GetHashCode() ^
				obj.Buffers.GetHashCode() ^
				obj.ColorFormat.GetHashCode() ^
				obj.Depth.GetHashCode() ^
				obj.Samples.GetHashCode() ^
				obj.Stencil.GetHashCode() ^
				obj.Stereo.GetHashCode();
		}
	}
}
