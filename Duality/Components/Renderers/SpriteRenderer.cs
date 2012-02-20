﻿using System;
using System.Collections.Generic;
using System.Linq;

using Duality.ColorFormat;
using Duality.Resources;
using Duality.EditorHints;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Duality.Components.Renderers
{
	/// <summary>
	/// Renders a sprite to represent the <see cref="GameObject"/>.
	/// </summary>
	[Serializable]
	public class SpriteRenderer : Renderer
	{
		/// <summary>
		/// SPecifies, how the sprites uv-Coordinates are calculated.
		/// </summary>
		public enum UVMode
		{
			/// <summary>
			/// The uv-Coordinates are constant, stretching the supplied texture to fit the SpriteRenderers dimensions.
			/// </summary>
			Stretch			= 0x0,
			/// <summary>
			/// The u-Coordinate is calculated based on the available horizontal space, allowing the supplied texture to be
			/// tiled across the SpriteRenderers width.
			/// </summary>
			WrapHorizontal	= 0x1,
			/// <summary>
			/// The v-Coordinate is calculated based on the available vertical space, allowing the supplied texture to be
			/// tiled across the SpriteRenderers height.
			/// </summary>
			WrapVertical	= 0x2,
			/// <summary>
			/// The uv-Coordinates are calculated based on the available space, allowing the supplied texture to be
			/// tiled across the SpriteRenderers size.
			/// </summary>
			WrapBoth		= WrapHorizontal | WrapVertical
		}

		protected	Rect					rect		= Rect.AlignCenter(0, 0, 128, 128);
		protected	ContentRef<Material>	sharedMat	= Material.DualityLogo256;
		protected	BatchInfo				customMat	= null;
		protected	ColorRgba				colorTint	= ColorRgba.White;
		protected	UVMode					rectMode	= UVMode.Stretch;
		[NonSerialized]
		protected	VertexFormat.VertexC1P3T2[]	vertices	= null;

		[EditorHintFlags(MemberFlags.Invisible)]
		public override float BoundRadius
		{
			get { return this.rect.Transform(this.gameobj.Transform.Scale.Xy).BoundingRadius; }
		}
		/// <summary>
		/// [GET / SET] The rectangular area the sprite occupies. Relative to the <see cref="GameObject"/>.
		/// </summary>
		[EditorHintDecimalPlaces(1)]
		public Rect Rect
		{
			get { return this.rect; }
			set { this.rect = value; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.Material"/> that is used for rendering the sprite.
		/// </summary>
		public ContentRef<Material> SharedMaterial
		{
			get { return this.sharedMat; }
			set { this.sharedMat = value; }
		}
		/// <summary>
		/// [GET / SET] A custom, local <see cref="Duality.Resources.BatchInfo"/> overriding the <see cref="SharedMaterial"/>,
		/// allowing this sprite to look unique without having to create its own <see cref="Duality.Resources.Material"/> Resource.
		/// However, this feature should be used with caution: Performance is better using <see cref="SharedMaterial">shared Materials</see>.
		/// </summary>
		public BatchInfo CustomMaterial
		{
			get { return this.customMat; }
			set { this.customMat = value; }
		}
		/// <summary>
		/// [GET / SET] A color by which the sprite is tinted.
		/// </summary>
		public ColorRgba ColorTint
		{
			get { return this.colorTint; }
			set { this.colorTint = value; }
		}
		/// <summary>
		/// [GET / SET] Specifies how the sprites uv-Coordinates are calculated.
		/// </summary>
		public UVMode RectMode
		{
			get { return this.rectMode; }
			set { this.rectMode = value; }
		}


		public SpriteRenderer() {}
		public SpriteRenderer(Rect rect, ContentRef<Material> mainMat)
		{
			this.rect = rect;
			this.sharedMat = mainMat;
		}

		protected Texture RetrieveMainTex()
		{
			if (this.customMat != null)
				return this.customMat.MainTexture.Res;
			else if (this.sharedMat.IsAvailable)
				return this.sharedMat.Res.MainTexture.Res;
			else
				return null;
		}
		protected ColorRgba RetrieveMainColor()
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
		protected void PrepareVertices(ref VertexFormat.VertexC1P3T2[] vertices, IDrawDevice device, ColorRgba mainClr, Rect uvRect)
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

			MathF.TransformDotVec(ref edge1, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge2, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge3, ref xDot, ref yDot);
			MathF.TransformDotVec(ref edge4, ref xDot, ref yDot);

			if (vertices == null || vertices.Length != 4) vertices = new VertexFormat.VertexC1P3T2[4];

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
			ColorRgba mainClr = this.RetrieveMainColor();

			Rect uvRect;
			if (mainTex != null)
			{
				if (this.rectMode == UVMode.WrapBoth)
					uvRect = new Rect(mainTex.UVRatio.X * this.rect.w / mainTex.PxWidth, mainTex.UVRatio.Y * this.rect.h / mainTex.PxHeight);
				else if (this.rectMode == UVMode.WrapHorizontal)
					uvRect = new Rect(mainTex.UVRatio.X * this.rect.w / mainTex.PxWidth, mainTex.UVRatio.Y);
				else if (this.rectMode == UVMode.WrapVertical)
					uvRect = new Rect(mainTex.UVRatio.X, mainTex.UVRatio.Y * this.rect.h / mainTex.PxHeight);
				else
					uvRect = new Rect(mainTex.UVRatio.X, mainTex.UVRatio.Y);
			}
			else
				uvRect = new Rect(1.0f, 1.0f);

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
			t.customMat	= this.customMat != null ? new BatchInfo(this.customMat) : null;
			t.rect		= this.rect;
			t.colorTint	= this.colorTint;
			t.rectMode	= this.rectMode;
		}
	}
}
