using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.ColorFormat;
using Duality.Resources;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Components.Renderers
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public class SpriteRenderer : Renderer
	{
		private	Rect						rect		= Rect.AlignCenter(0, 0, 128, 128);
		private	ContentRef<Material>		sharedMat	= Material.DualityLogo256;
		private BatchInfo					customMat	= null;
		[NonSerialized]
		private	VertexFormat.VertexP3T2[]	vertices	= null;

		public override float BoundRadius
		{
			get { return this.rect.Transform(this.gameobj.Transform.Scale.Xy).BoundingRadius; }
		}
		public Rect Rect
		{
			get { return this.rect; }
			set { this.rect = value; }
		}
		public ContentRef<Material> SharedMaterial
		{
			get { return this.sharedMat; }
			set { this.sharedMat = value; }
		}
		public BatchInfo CustomMaterial
		{
			get { return this.customMat; }
			set { this.customMat = value; }
		}


		public SpriteRenderer() {}
		public SpriteRenderer(Rect rect, ContentRef<Material> mainMat)
		{
			this.rect = rect;
			this.sharedMat = mainMat;
		}

		public override void Draw(IDrawDevice device)
		{
			Vector3 posTemp = this.gameobj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(this, ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, scaleTemp, out xDot, out yDot);

			Rect rectTemp = this.rect.Transform(this.gameobj.Transform.Scale.Xy);
			Vector2 edge1 = rectTemp.TopLeft;
			Vector2 edge2 = rectTemp.BottomLeft;
			Vector2 edge3 = rectTemp.BottomRight;
			Vector2 edge4 = rectTemp.TopRight;

			MathF.TransdormDotVec(ref edge1, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge2, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge3, ref xDot, ref yDot);
			MathF.TransdormDotVec(ref edge4, ref xDot, ref yDot);

			Vector2 uvRatio = Vector2.One;
			if (this.customMat != null)
			{
				if (this.customMat.Textures != null && this.customMat.Textures.Count > 0)
					uvRatio = this.customMat.Textures.Values.First().Res.UVRatio;
			}
			else if (this.sharedMat.IsAvailable)
			{
				if (this.sharedMat.Res.Textures != null && this.sharedMat.Res.Textures.Count > 0)
					uvRatio = this.sharedMat.Res.Textures.Values.First().Res.UVRatio;
			}

			if (this.vertices == null) this.vertices = new VertexFormat.VertexP3T2[4];

			this.vertices[0].pos.X = posTemp.X + edge1.X;
			this.vertices[0].pos.Y = posTemp.Y + edge1.Y;
			this.vertices[0].pos.Z = posTemp.Z;
			this.vertices[0].texCoord.X = 0.0f;
			this.vertices[0].texCoord.Y = 0.0f;

			this.vertices[1].pos.X = posTemp.X + edge2.X;
			this.vertices[1].pos.Y = posTemp.Y + edge2.Y;
			this.vertices[1].pos.Z = posTemp.Z;
			this.vertices[1].texCoord.X = 0.0f;
			this.vertices[1].texCoord.Y = uvRatio.Y;

			this.vertices[2].pos.X = posTemp.X + edge3.X;
			this.vertices[2].pos.Y = posTemp.Y + edge3.Y;
			this.vertices[2].pos.Z = posTemp.Z;
			this.vertices[2].texCoord.X = uvRatio.X;
			this.vertices[2].texCoord.Y = uvRatio.Y;
				
			this.vertices[3].pos.X = posTemp.X + edge4.X;
			this.vertices[3].pos.Y = posTemp.Y + edge4.Y;
			this.vertices[3].pos.Z = posTemp.Z;
			this.vertices[3].texCoord.X = uvRatio.X;
			this.vertices[3].texCoord.Y = 0.0f;

			if (this.customMat != null)
				device.AddVertices(this.customMat, BeginMode.Quads, this.vertices);
			else
				device.AddVertices(this.sharedMat, BeginMode.Quads, this.vertices);
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			SpriteRenderer t = target as SpriteRenderer;
			t.sharedMat	= this.sharedMat;
			t.customMat	= this.customMat;
			t.rect		= this.rect;
		}
	}
}
