using System;
using System.Linq;

using Duality.ColorFormat;
using Duality.Resources;
using Duality.EditorHints;

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
		[NonSerialized]	protected	Rect							textRect	= Rect.Empty;
		[NonSerialized] protected	VertexFormat.VertexC1P3T2[][]	vertFont	= null;
		[NonSerialized] protected	VertexFormat.VertexC1P3T2[]		vertIcon	= null;

		[EditorHintFlags(MemberFlags.Invisible)]
		public override float BoundRadius
		{
			get { return this.textRect.Transform(this.gameobj.Transform.Scale.Xy).BoundingRadius; }
		}
		/// <summary>
		/// [GET / SET] The text blocks alignment relative to the <see cref="GameObject"/>.
		/// </summary>
		public Alignment Align
		{
			get { return this.align; }
			set
			{
				this.align = value;
				this.UpdateMetrics();
			}
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
		[EditorHintFlags(MemberFlags.Invisible)]
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
			this.textRect = Rect.Align(this.align, 0.0f, 0.0f, 
				MathF.Max(this.metrics.Size.X, this.text.MaxWidth), 
				MathF.Min(this.metrics.Size.Y, this.text.MaxHeight));
		}

		public override void Draw(IDrawDevice device)
		{
			Vector3 posTemp = this.gameobj.Transform.Pos;
			float scaleTemp = 1.0f;
			device.PreprocessCoords(this, ref posTemp, ref scaleTemp);

			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(this.GameObj.Transform.Angle, this.gameobj.Transform.Scale.Xy * scaleTemp, out xDot, out yDot);

			Rect rect = Rect.Align(this.align, 0.0f, 0.0f, MathF.Max(this.text.MaxWidth, this.metrics.Size.X), this.metrics.Size.Y);
			Vector2 textOffset = rect.TopLeft;
			MathF.TransformDotVec(ref textOffset, ref xDot, ref yDot);
			posTemp.X += textOffset.X;
			posTemp.Y += textOffset.Y;
			if (this.text.Fonts.All(r => !r.IsAvailable || r.Res.GlyphRenderHint == Font.RenderHint.Monochrome))
			{
				posTemp.X = MathF.Round(posTemp.X);
				posTemp.Y = MathF.Round(posTemp.Y);
				if (MathF.RoundToInt(DualityApp.TargetResolution.X) != (MathF.RoundToInt(DualityApp.TargetResolution.X) / 2) * 2)
					posTemp.X += 0.5f;
				if (MathF.RoundToInt(DualityApp.TargetResolution.Y) != (MathF.RoundToInt(DualityApp.TargetResolution.Y) / 2) * 2)
					posTemp.Y += 0.5f;
			}

			// Draw design time data
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
			{
				Vector3 textWidth = Vector3.UnitX * rect.W;
				Vector3 textMaxWidth = Vector3.UnitX * this.text.MaxWidth;
				Vector3 textHeight = Vector3.UnitY * rect.H;
				Vector3 textMaxHeight = Vector3.UnitY * MathF.Max(this.text.MaxHeight, rect.H);
				MathF.TransformDotVec(ref textWidth, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textMaxWidth, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textHeight, ref xDot, ref yDot);
				MathF.TransformDotVec(ref textMaxHeight, ref xDot, ref yDot);

				device.AddVertices(new BatchInfo(DrawTechnique.Alpha, this.colorTint.WithAlpha(128)), VertexMode.LineLoop,
					new VertexFormat.VertexP3(posTemp),
					new VertexFormat.VertexP3(posTemp + textWidth),
					new VertexFormat.VertexP3(posTemp + textWidth + textHeight),
					new VertexFormat.VertexP3(posTemp + textHeight));
				device.AddVertices(new BatchInfo(DrawTechnique.Alpha, (ColorRgba.Red * this.colorTint).WithAlpha(128)), VertexMode.LineLoop,
					new VertexFormat.VertexP3(posTemp),
					new VertexFormat.VertexP3(posTemp + textMaxWidth),
					new VertexFormat.VertexP3(posTemp + textMaxWidth + textMaxHeight),
					new VertexFormat.VertexP3(posTemp + textMaxHeight));
			}

			ColorRgba matColor = this.customMat != null ? this.customMat.MainColor : ColorRgba.White;
			this.text.EmitVertices(ref this.vertFont, ref this.vertIcon, posTemp.X, posTemp.Y, posTemp.Z, this.colorTint * matColor, xDot, yDot);
			if (this.text.Fonts != null)
			{
				for (int i = 0; i < this.text.Fonts.Length; i++)
				{
					if (this.text.Fonts[i] != null && this.text.Fonts[i].IsAvailable) 
					{
						if (this.customMat == null)
							device.AddVertices(this.text.Fonts[i].Res.Material, VertexMode.Quads, this.vertFont[i]);
						else
						{
							BatchInfo cm = new BatchInfo(this.customMat);
							cm.Textures = this.text.Fonts[i].Res.Material.Textures;
							device.AddVertices(cm, VertexMode.Quads, this.vertFont[i]);
						}
					}
				}
			}
			if (this.text.Icons != null && this.iconMat.IsAvailable)
			{
				device.AddVertices(this.iconMat, VertexMode.Quads, this.vertIcon);
			}
		}

		
		void ICmpInitializable.OnInit(InitContext context)
		{
			if (context == InitContext.Loaded) this.UpdateMetrics();
		}
		void ICmpInitializable.OnShutdown(ShutdownContext context) {}

		internal override void CopyToInternal(Component target, Duality.Cloning.CloneProvider provider)
		{
			base.CopyToInternal(target, provider);
			TextRenderer t = target as TextRenderer;
			t.align		= this.align;
			t.text		= this.text.Clone();
			t.colorTint	= this.colorTint;
			t.customMat	= this.customMat != null ? new BatchInfo(this.customMat) : null;
			t.UpdateMetrics();
		}
	}
}
