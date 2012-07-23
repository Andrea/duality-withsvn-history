using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

using Duality;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components.Physics;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase.CamViewLayers
{
	public class RigidBodyShapeCamViewLayer : CamViewLayer
	{
		private	ContentRef<Font>	bigFont	= new ContentRef<Font>(null, "__editor__bigfont__");

		public override string LayerName
		{
			get { return PluginRes.EditorBaseRes.CamViewLayer_RigidBodyShape_Name; }
		}
		public override string LayerDesc
		{
			get { return PluginRes.EditorBaseRes.CamViewLayer_RigidBodyShape_Desc; }
		}
		public ColorRgba ShapeColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(ColorRgba.Blue, ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(ColorRgba.Blue, ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba ShapeSensorColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(255, 128, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(255, 128, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba ShapeErrorColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(255, 0, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(255, 0, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}

		protected internal override void OnCollectDrawcalls(Canvas canvas)
		{
			base.OnCollectDrawcalls(canvas);
			List<RigidBody> visibleColliders = this.QueryVisibleColliders().ToList();

			this.RetrieveResources();
			RigidBody selectedBody = this.QuerySelectedCollider();

			canvas.CurrentState.TextFont = this.bigFont;
			Font textFont = canvas.CurrentState.TextFont.Res;

			// Draw Shape layer
			foreach (RigidBody c in visibleColliders)
			{
				if (!c.Shapes.Any()) continue;
				float colliderAlpha = c == selectedBody ? 1.0f : (selectedBody != null ? 0.25f : 0.5f);
				float maxDensity = c.Shapes.Max(s => s.Density);
				float minDensity = c.Shapes.Min(s => s.Density);
				float avgDensity = (maxDensity + minDensity) * 0.5f;
				Vector3 colliderPos = c.GameObj.Transform.Pos;
				Vector2 colliderScale = c.GameObj.Transform.Scale.Xy;
				int index = 0;
				foreach (ShapeInfo s in c.Shapes)
				{
					CircleShapeInfo circle = s as CircleShapeInfo;
					PolyShapeInfo poly = s as PolyShapeInfo;
					EdgeShapeInfo edge = s as EdgeShapeInfo;
					LoopShapeInfo loop = s as LoopShapeInfo;

					float shapeAlpha = colliderAlpha * (selectedBody == null || this.View.ViewState.SelectedObjects.Any(sel => sel.ActualObject == s) ? 1.0f : 0.5f);
					float densityRelative = MathF.Abs(maxDensity - minDensity) < 0.01f ? 1.0f : s.Density / avgDensity;
					ColorRgba clr = s.IsSensor ? this.ShapeSensorColor : this.ShapeColor;
					ColorRgba fontClr = ColorRgba.Mix(clr, this.View.FgColor, 0.5f);

					if (circle != null)
					{
						float uniformScale = colliderScale.Length / MathF.Sqrt(2.0f);
						Vector2 circlePos = circle.Position * colliderScale;
						MathF.TransformCoord(ref circlePos.X, ref circlePos.Y, c.GameObj.Transform.Angle);

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha((0.25f + densityRelative * 0.25f) * shapeAlpha)));
						canvas.FillCircle(
							colliderPos.X + circlePos.X,
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawCircle(
							colliderPos.X + circlePos.X,
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f, 
							circle.Radius * uniformScale);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + circlePos.X, 
							colliderPos.Y + circlePos.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					else if (poly != null)
					{
						if (!MathF.IsPolygonConvex(poly.Vertices))
							clr = this.ShapeErrorColor;

						Vector2[] polyVert = poly.Vertices.ToArray();
						Vector2 center = Vector2.Zero;
						for (int i = 0; i < polyVert.Length; i++)
						{
							center += polyVert[i];
							Vector2.Multiply(ref polyVert[i], ref colliderScale, out polyVert[i]);
							MathF.TransformCoord(ref polyVert[i].X, ref polyVert[i].Y, c.GameObj.Transform.Angle);
							polyVert[i] += colliderPos.Xy;
						}
						center /= polyVert.Length;
						Vector2.Multiply(ref center, ref colliderScale, out center);
						MathF.TransformCoord(ref center.X, ref center.Y, c.GameObj.Transform.Angle);

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha((0.25f + densityRelative * 0.25f) * shapeAlpha)));
						canvas.FillConvexPolygon(polyVert, colliderPos.Z - 0.01f);
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawConvexPolygon(polyVert, colliderPos.Z - 0.01f);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + center.X, 
							colliderPos.Y + center.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					else if (edge != null)
					{
						Vector2 v1 = edge.VertexStart;
						Vector2 v2 = edge.VertexEnd;
						Vector2 center = (v1 + v2) * 0.5f;

						Vector2.Multiply(ref v1, ref colliderScale, out v1);
						Vector2.Multiply(ref v2, ref colliderScale, out v2);
						Vector2.Multiply(ref center, ref colliderScale, out center);
						MathF.TransformCoord(ref v1.X, ref v1.Y, c.GameObj.Transform.Angle);
						MathF.TransformCoord(ref v2.X, ref v2.Y, c.GameObj.Transform.Angle);
						MathF.TransformCoord(ref center.X, ref center.Y, c.GameObj.Transform.Angle);
						v1 += colliderPos.Xy;
						v2 += colliderPos.Xy;

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawLine(v1.X, v1.Y, v2.X, v2.Y);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + center.X, 
							colliderPos.Y + center.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					else if (loop != null)
					{
						Vector2[] loopVert = loop.Vertices.ToArray();
						Vector2 center = Vector2.Zero;
						for (int i = 0; i < loopVert.Length; i++)
						{
							center += loopVert[i];
							Vector2.Multiply(ref loopVert[i], ref colliderScale, out loopVert[i]);
							MathF.TransformCoord(ref loopVert[i].X, ref loopVert[i].Y, c.GameObj.Transform.Angle);
							loopVert[i] += colliderPos.Xy;
						}
						center /= loopVert.Length;
						Vector2.Multiply(ref center, ref colliderScale, out center);
						MathF.TransformCoord(ref center.X, ref center.Y, c.GameObj.Transform.Angle);

						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr.WithAlpha(shapeAlpha)));
						canvas.DrawConvexPolygon(loopVert, colliderPos.Z - 0.01f);

						Vector2 textSize = textFont.MeasureText(index.ToString(CultureInfo.InvariantCulture));
						canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, fontClr.WithAlpha(shapeAlpha)));
						canvas.CurrentState.TransformHandle = textSize * 0.5f;
						canvas.DrawText(index.ToString(CultureInfo.InvariantCulture), 
							colliderPos.X + center.X, 
							colliderPos.Y + center.Y,
							colliderPos.Z - 0.01f);
						canvas.CurrentState.TransformHandle = Vector2.Zero;
					}
					index++;
				}
			}
		}
		
		private void RetrieveResources()
		{
			if (!this.bigFont.IsAvailable)
			{
				Font bigFontRes = new Font();
				bigFontRes.Family = System.Drawing.FontFamily.GenericSansSerif.Name;
				bigFontRes.Size = 32;
				bigFontRes.Kerning = true;
				bigFontRes.GlyphRenderHint = Duality.Resources.Font.RenderHint.AntiAlias;
				bigFontRes.ReloadData();
				ContentProvider.RegisterContent("__editor__bigfont__", bigFontRes);
			}
		}
		private IEnumerable<RigidBody> QueryVisibleColliders()
		{
			this.View.MakeDualityTarget();
			IEnumerable<RigidBody> allColliders = Scene.Current.AllObjects.GetComponents<RigidBody>(true);
			IDrawDevice device = this.View.CameraComponent.DrawDevice;
			return allColliders.Where(c => device.IsCoordInView(c.GameObj.Transform.Pos, c.BoundRadius));
		}
		private RigidBody QuerySelectedCollider()
		{
			return EditorBasePlugin.Instance.EditorForm.Selection.Components.OfType<RigidBody>().FirstOrDefault();
		}
	}
}
