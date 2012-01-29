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
	/// <summary>
	/// Renders a text to represent the <see cref="GameObject"/>.
	/// </summary>
	[Serializable]
	public class TextRenderer : Renderer, ICmpInitializable
	{
		protected	Alignment				align		= Alignment.Center;
		protected	FormattedText			text		= new FormattedText("Hello World");
		protected	BatchInfo				customMat	= null;
		protected	ColorRgba				colorTint	= ColorRgba.White;
		protected	ContentRef<Material>	iconMat		= ContentRef<Material>.Null;
		[NonSerialized] protected	FormattedText.Metrics			metrics		= new FormattedText.Metrics(Vector2.Zero, new Rect[0], new Rect[0]);
		[NonSerialized] protected	VertexFormat.VertexC1P3T2[][]	vertFont	= null;
		[NonSerialized] protected	VertexFormat.VertexC1P3T2[]		vertIcon	= null;

		public override float BoundRadius
		{
			get { return Rect.Align(this.align, 0.0f, 0.0f, this.metrics.Size.X, this.metrics.Size.Y).Transform(this.gameobj.Transform.Scale.Xy).BoundingRadius; }
		}
		/// <summary>
		/// [GET / SET] The text blocks alignment relative to the <see cref="GameObject"/>.
		/// </summary>
		public Alignment Align
		{
			get { return this.align; }
			set { this.align = value; }
		}
		/// <summary>
		/// [GET / SET] The text to display.
		/// </summary>
		public FormattedText Text
		{
			get { return this.text; }
			set { this.text = value; }
		}
		/// <summary>
		/// [GET / SET] A color by which the displayed text is tinted.
		/// </summary>
		public ColorRgba ColorTint
		{
			get { return this.colorTint; }
			set { this.colorTint = value; }
		}
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.Material"/> to use for displaying icons ithin the text.
		/// </summary>
		public ContentRef<Material> IconMat
		{
			get { return this.iconMat; }
			set { this.iconMat = value; }
		}
		/// <summary>
		/// [GET] The current texts metrics.
		/// </summary>
		public FormattedText.Metrics Metrics
		{
			get { return this.metrics; }
		}
		/// <summary>
		/// [GET / SET] A custom, local <see cref="Duality.Resources.BatchInfo"/> overriding the texts own <see cref="Duality.Resources.Font.Material">
		/// Materials</see>. Note that it does not override each <see cref="Duality.Resources.Font">Fonts</see> Texture, but their DrawTechniques and
		/// main colors.
		/// </summary>
		public BatchInfo CustomMaterial
		{
			get { return this.customMat; }
			set { this.customMat = value; }
		}


		public TextRenderer() 
		{
			this.text.Fonts = new[] { Font.GenericMonospace10 };
			this.UpdateMetrics();
		}

		/// <summary>
		/// Updates the texts <see cref="Metrics"/>. Should be called anytime the text changes.
		/// </summary>
		public void UpdateMetrics()
		{
			this.metrics = this.text.Measure();
		}

		public override void Draw(IDrawDevice device)
		{
			Vector3 posTemp = this.gameobj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(this, ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, this.gameobj.Transform.Scale.Xy * scaleTemp, out xDot, out yDot);

			Rect textRect = Rect.Align(this.align, 0.0f, 0.0f, MathF.Max(this.text.MaxWidth, this.metrics.Size.X), this.metrics.Size.Y);
			Vector2 textOffset = textRect.TopLeft;
			MathF.TransformDotVec(ref textOffset, ref xDot, ref yDot);
			posTemp.X += textOffset.X;
			posTemp.Y += textOffset.Y;

			// Draw design time data
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
			{
				Vector3 textWidth = Vector3.UnitX * textRect.w;
				Vector3 textMaxWidth = Vector3.UnitX * this.text.MaxWidth;
				Vector3 textHeight = Vector3.UnitY * textRect.h;
				Vector3 textMaxHeight = Vector3.UnitY * MathF.Max(this.text.MaxHeight, textRect.h);
				MathF.TransformDotVec(ref textWidth, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textMaxWidth, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textHeight, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textMaxHeight, ref xDot, ref yDot);

				device.AddVertices(new BatchInfo(DrawTechnique.Alpha, this.colorTint.WithAlpha(128)), BeginMode.LineLoop,
					new VertexFormat.VertexP3(posTemp),
					new VertexFormat.VertexP3(posTemp + textWidth),
					new VertexFormat.VertexP3(posTemp + textWidth + textHeight),
					new VertexFormat.VertexP3(posTemp + textHeight));
				device.AddVertices(new BatchInfo(DrawTechnique.Alpha, (ColorRgba.Red * this.colorTint).WithAlpha(128)), BeginMode.LineLoop,
					new VertexFormat.VertexP3(posTemp),
					new VertexFormat.VertexP3(posTemp + textMaxWidth),
					new VertexFormat.VertexP3(posTemp + textMaxWidth + textMaxHeight),
					new VertexFormat.VertexP3(posTemp + textMaxHeight));
			}

			this.text.EmitVertices(ref this.vertFont, ref this.vertIcon, posTemp.X, posTemp.Y, posTemp.Z, this.colorTint, xDot, yDot);
			if (this.text.Fonts != null)
			{
				for (int i = 0; i < this.text.Fonts.Length; i++)
				{
					if (this.text.Fonts[i] != null && this.text.Fonts[i].IsAvailable) 
					{
						if (this.customMat == null)
							device.AddVertices(this.text.Fonts[i].Res.Material, BeginMode.Quads, this.vertFont[i]);
						else
						{
							BatchInfo cm = new BatchInfo(this.customMat);
							cm.Textures = new Dictionary<string,ContentRef<Texture>>(this.text.Fonts[i].Res.Material.Textures);
							device.AddVertices(cm, BeginMode.Quads, this.vertFont[i]);
						}
					}
				}
			}
			if (this.text.Icons != null && this.iconMat.IsAvailable)
			{
				device.AddVertices(this.iconMat, BeginMode.Quads, this.vertIcon);
			}
		}

		
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Loaded) this.UpdateMetrics();
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context) {}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			TextRenderer t = target as TextRenderer;
			t.align		= this.align;
			t.text		= this.text.Clone();
			t.colorTint	= this.colorTint;
			t.customMat	= this.customMat != null ? new BatchInfo(this.customMat) : null;
			t.UpdateMetrics();
		}
	}
}
