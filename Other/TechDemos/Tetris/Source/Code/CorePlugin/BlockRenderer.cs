using System;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.ColorFormat;
using Duality.VertexFormat;
using Duality.Resources;
using Duality.EditorHints;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Tetris
{
	/// <summary>
	/// Renders a sprite to represent the <see cref="GameObject"/>.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	[RequiredComponent(typeof(Collider))]
	public class BlockRenderer : Renderer
	{
		protected	ContentRef<Material>	sharedMat	= Material.DualityLogo256;
		protected	ColorRgba				colorTint	= ColorRgba.White;
		[NonSerialized]
		protected	VertexC1P3T2[]	vertices	= null;

		[EditorHintFlags(MemberFlags.Invisible)]
		public override float BoundRadius
		{
			get { return this.GameObj.GetComponent<Collider>().BoundRadius; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.Material"/> that is used for rendering the block.
		/// </summary>
		public ContentRef<Material> SharedMaterial
		{
			get { return this.sharedMat; }
			set { this.sharedMat = value; }
		}
		/// <summary>
		/// [GET / SET] A color by which the sprite is tinted.
		/// </summary>
		public ColorRgba ColorTint
		{
			get { return this.colorTint; }
			set { this.colorTint = value; }
		}

		
		protected Texture RetrieveMainTexture()
		{
			if (this.sharedMat.IsAvailable)
				return this.sharedMat.Res.MainTexture.Res;
			else
				return null;
		}
		protected ColorRgba RetrieveMainColor()
		{
			if (this.sharedMat.IsAvailable)
				return this.sharedMat.Res.MainColor * this.colorTint;
			else
				return this.colorTint;
		}
		protected void PrepareVertices(ref VertexC1P3T2[] vertices, IDrawDevice device)
		{
			Texture mainTex = this.RetrieveMainTexture();
			ColorRgba mainClr = this.RetrieveMainColor();

			// Determine texture sprite rect
			Rect blockSpriteRect;
			if (mainTex != null)
				blockSpriteRect = Rect.AlignCenter(0, 0, mainTex.PxWidth, mainTex.PxHeight);
			else
				blockSpriteRect = Rect.AlignCenter(0, 0, 1, 1);

			// Determine block rects to draw
			Collider col = this.GameObj.GetComponent<Collider>();
			var blockShapes = col.Shapes.OfType<Collider.PolyShapeInfo>();
			var blockRects = blockShapes.Select(p => p.AABB).ToArray();

			Vector3 posTemp = this.GameObj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(this, ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, scaleTemp, out xDot, out yDot);

			if (vertices == null || vertices.Length != blockRects.Length * 4) vertices = new VertexC1P3T2[blockRects.Length * 4];
			for (int i = 0; i < blockRects.Length; i++)
			{
				Rect blockRect = blockRects[i];
				Rect uvRect = new Rect(
					(blockRect.x - blockSpriteRect.x) / blockSpriteRect.w,
					(blockRect.y - blockSpriteRect.y) / blockSpriteRect.h,
					blockRect.w / blockSpriteRect.w,
					blockRect.h / blockSpriteRect.h);

				Rect rectTemp = blockRect.Transform(this.GameObj.Transform.Scale.Xy);
				Vector2 edge1 = rectTemp.TopLeft;
				Vector2 edge2 = rectTemp.BottomLeft;
				Vector2 edge3 = rectTemp.BottomRight;
				Vector2 edge4 = rectTemp.TopRight;

				MathF.TransformDotVec(ref edge1, ref xDot, ref yDot);
				MathF.TransformDotVec(ref edge2, ref xDot, ref yDot);
				MathF.TransformDotVec(ref edge3, ref xDot, ref yDot);
				MathF.TransformDotVec(ref edge4, ref xDot, ref yDot);

				vertices[i * 4 + 0].pos.X = posTemp.X + edge1.X;
				vertices[i * 4 + 0].pos.Y = posTemp.Y + edge1.Y;
				vertices[i * 4 + 0].pos.Z = posTemp.Z;
				vertices[i * 4 + 0].texCoord.X = uvRect.x;
				vertices[i * 4 + 0].texCoord.Y = uvRect.y;
				vertices[i * 4 + 0].clr = mainClr;

				vertices[i * 4 + 1].pos.X = posTemp.X + edge2.X;
				vertices[i * 4 + 1].pos.Y = posTemp.Y + edge2.Y;
				vertices[i * 4 + 1].pos.Z = posTemp.Z;
				vertices[i * 4 + 1].texCoord.X = uvRect.x;
				vertices[i * 4 + 1].texCoord.Y = uvRect.MaxY;
				vertices[i * 4 + 1].clr = mainClr;

				vertices[i * 4 + 2].pos.X = posTemp.X + edge3.X;
				vertices[i * 4 + 2].pos.Y = posTemp.Y + edge3.Y;
				vertices[i * 4 + 2].pos.Z = posTemp.Z;
				vertices[i * 4 + 2].texCoord.X = uvRect.MaxX;
				vertices[i * 4 + 2].texCoord.Y = uvRect.MaxY;
				vertices[i * 4 + 2].clr = mainClr;
				
				vertices[i * 4 + 3].pos.X = posTemp.X + edge4.X;
				vertices[i * 4 + 3].pos.Y = posTemp.Y + edge4.Y;
				vertices[i * 4 + 3].pos.Z = posTemp.Z;
				vertices[i * 4 + 3].texCoord.X = uvRect.MaxX;
				vertices[i * 4 + 3].texCoord.Y = uvRect.y;
				vertices[i * 4 + 3].clr = mainClr;
			}
		}

		public override void Draw(IDrawDevice device)
		{
			this.PrepareVertices(ref this.vertices, device);
			device.AddVertices(this.sharedMat, BeginMode.Quads, this.vertices);
		}
	}
}
