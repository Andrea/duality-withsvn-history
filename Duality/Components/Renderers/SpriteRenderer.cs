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
		protected	Rect					rect		= Rect.AlignCenter(0, 0, 128, 128);
		protected	ContentRef<Material>	sharedMat	= Material.DualityLogo256;
		protected	BatchInfo				customMat	= null;
		protected	ColorRGBA				colorTint	= ColorRGBA.White;
		[NonSerialized]
		protected	VertexFormat.VertexC4P3T2[]	vertices	= null;

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
		public ColorRGBA ColorTint
		{
			get { return this.colorTint; }
			set { this.colorTint = value; }
		}


		public SpriteRenderer() {}
		public SpriteRenderer(Rect rect, ContentRef<Material> mainMat)
		{
			this.rect = rect;
			this.sharedMat = mainMat;
		}

		protected Texture RetrieveMainTex()
		{
			if (this.customMat != null && 
				this.customMat.Textures != null && 
				this.customMat.Textures.Count > 0 && 
				this.customMat.Textures.Values.First().IsAvailable)
				return this.customMat.Textures.Values.First().Res;
			else if (this.sharedMat.IsAvailable && 
				this.sharedMat.Res.Textures != null && 
				this.sharedMat.Res.Textures.Count > 0 && 
				this.sharedMat.Res.Textures.Values.First().IsAvailable)
				return this.sharedMat.Res.Textures.Values.First().Res;
			else
				return null;
		}
		protected ColorRGBA RetrieveMainColor()
		{
			if (this.customMat != null)
				return this.customMat.MainColor * this.colorTint;
			else if (this.sharedMat.IsAvailable)
				return this.sharedMat.Res.MainColor * this.colorTint;
			else
				return this.colorTint;
		}
		protected DrawTechnique RetrieveDrawTechnique()
		{
			if (this.customMat != null)
				return this.customMat.Technique.Res;
			else if (this.sharedMat.IsAvailable)
				return this.sharedMat.Res.Technique.Res;
			else
				return null;
		}
		protected void PrepareVertices(ref VertexFormat.VertexC4P3T2[] vertices, IDrawDevice device, ColorRGBA mainClr, Rect uvRect)
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

			if (vertices == null || vertices.Length != 4) vertices = new VertexFormat.VertexC4P3T2[4];

			vertices[0].pos.X = posTemp.X + edge1.X;
			vertices[0].pos.Y = posTemp.Y + edge1.Y;
			vertices[0].pos.Z = posTemp.Z;
			vertices[0].texCoord.X = uvRect.x;
			vertices[0].texCoord.Y = uvRect.y;
			vertices[0].clr = mainClr;

			vertices[1].pos.X = posTemp.X + edge2.X;
			vertices[1].pos.Y = posTemp.Y + edge2.Y;
			vertices[1].pos.Z = posTemp.Z;
			vertices[1].texCoord.X = uvRect.x;
			vertices[1].texCoord.Y = uvRect.MaxY;
			vertices[1].clr = mainClr;

			vertices[2].pos.X = posTemp.X + edge3.X;
			vertices[2].pos.Y = posTemp.Y + edge3.Y;
			vertices[2].pos.Z = posTemp.Z;
			vertices[2].texCoord.X = uvRect.MaxX;
			vertices[2].texCoord.Y = uvRect.MaxY;
			vertices[2].clr = mainClr;
				
			vertices[3].pos.X = posTemp.X + edge4.X;
			vertices[3].pos.Y = posTemp.Y + edge4.Y;
			vertices[3].pos.Z = posTemp.Z;
			vertices[3].texCoord.X = uvRect.MaxX;
			vertices[3].texCoord.Y = uvRect.y;
			vertices[3].clr = mainClr;
		}

		public override void Draw(IDrawDevice device)
		{
			Texture mainTex = this.RetrieveMainTex();
			ColorRGBA mainClr = this.RetrieveMainColor();

			Rect uvRect = new Rect(1.0f, 1.0f);
			if (mainTex != null) uvRect = new Rect(mainTex.UVRatio.X, mainTex.UVRatio.Y);

			this.PrepareVertices(ref this.vertices, device, mainClr, uvRect);
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
			t.customMat	= new BatchInfo(this.customMat);
			t.rect		= this.rect;
			t.colorTint	= this.colorTint;
		}
	}
}
