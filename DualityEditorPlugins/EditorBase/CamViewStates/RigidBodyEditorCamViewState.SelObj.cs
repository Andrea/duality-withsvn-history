using System.Drawing;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Components.Physics;

using OpenTK;

namespace EditorBase.CamViewStates
{
	public partial class RigidBodyEditorCamViewState
	{
		public class SelCollider : SelObj
		{
			private	RigidBody	collider;

			public override object ActualObject
			{
				get { return this.collider == null || this.collider.Disposed ? null : this.collider; }
			}
			public override bool HasTransform
			{
				get { return this.collider != null && !this.collider.Disposed && this.collider.GameObj.Transform != null; }
			}
			public override Vector3 Pos
			{
				get { return this.collider.GameObj.Transform.Pos; }
				set { }
			}
			public override float Angle
			{
				get { return this.collider.GameObj.Transform.Angle; }
				set { }
			}
			public override Vector3 Scale
			{
				get { return this.collider.GameObj.Transform.Scale; }
				set { }
			}
			public override float BoundRadius
			{
				get
				{
					ICmpRenderer r = this.collider.GameObj.Renderer;
					if (r == null) return CamView.DefaultDisplayBoundRadius;
					else return r.BoundRadius;
				}
			}
			public override bool ShowPos
			{
				get { return false; }
			}

			public SelCollider(RigidBody obj)
			{
				this.collider = obj;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				return false;
			}
		}

		public abstract class SelShape : SelObj
		{
			private		ShapeInfo	shape;
			
			public override bool HasTransform
			{
				get { return this.Collider != null && !this.Collider.Disposed && this.Collider.GameObj.Transform != null; }
			}
			public override object ActualObject
			{
				get { return this.shape; }
			}
			public RigidBody Collider
			{
				get { return this.shape.Parent; }
			}

			protected SelShape(ShapeInfo shape)
			{
				this.shape = shape;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.MoveObj) return true;
				if (action == MouseAction.RotateObj) return true;
				if (action == MouseAction.ScaleObj) return true;
				return false;
			}

			public static SelShape Create(ShapeInfo shape)
			{
				if (shape is CircleShapeInfo)
					return new SelCircleShape(shape as CircleShapeInfo);
				else if (shape is PolyShapeInfo)
					return new SelPolyShape(shape as PolyShapeInfo);
				else
					return null;
			}
		}
		public class SelCircleShape : SelShape
		{
			private	CircleShapeInfo	circle;
			
			public override Vector3 Pos
			{
				get
				{
					return this.Collider.GameObj.Transform.GetWorldPoint(new Vector3(this.circle.Position));
				}
				set
				{
					value.Z = this.Collider.GameObj.Transform.Pos.Z;
					this.circle.Position = this.Collider.GameObj.Transform.GetLocalPoint(value).Xy;
				}
			}
			public override Vector3 Scale
			{
				get
				{
					return Vector3.One * this.circle.Radius;
				}
				set
				{
					this.circle.Radius = value.Length / MathF.Sqrt(3.0f);
				}
			}
			public override float BoundRadius
			{
				get { return this.circle.Radius * this.Collider.GameObj.Transform.Scale.Xy.Length / MathF.Sqrt(2.0f); }
			}

			public SelCircleShape(CircleShapeInfo shape) : base(shape)
			{
				this.circle = shape;
			}

			public override bool IsActionAvailable(MouseAction action)
			{
				if (action == MouseAction.RotateObj) return false;
				return base.IsActionAvailable(action);
			}
			public override void DrawActionGizmo(Canvas canvas, MouseAction action, Point beginLoc, Point curLoc)
			{
				base.DrawActionGizmo(canvas, action, beginLoc, curLoc);
				if (action == MouseAction.MoveObj)
				{
					canvas.DrawText(string.Format("Center X:{0,7:0.00}", this.circle.Position.X), curLoc.X + 30, curLoc.Y + 10);
					canvas.DrawText(string.Format("Center Y:{0,7:0.00}", this.circle.Position.Y), curLoc.X + 30, curLoc.Y + 18);
				}
				else if (action == MouseAction.ScaleObj)
				{
					canvas.DrawText(string.Format("Radius:{0,7:0.00}", this.circle.Radius), curLoc.X + 30, curLoc.Y + 10);
				}
			}
		}
		public class SelPolyShape : SelShape
		{
			private	PolyShapeInfo	poly;
			private	Vector2	center;
			private	float	boundRad;
			private	float	angle;
			private	Vector2	scale;

			public override Vector3 Pos
			{
				get
				{
					return this.Collider.GameObj.Transform.GetWorldPoint(new Vector3(this.center));
				}
				set
				{
					value.Z = this.Collider.GameObj.Transform.Pos.Z;
					this.MoveCenterTo(this.Collider.GameObj.Transform.GetLocalPoint(value).Xy);
				}
			}
			public override Vector3 Scale
			{
				get
				{
					return new Vector3(this.scale);
				}
				set
				{
					this.ScaleTo(value.Xy);
				}
			}
			public override float Angle
			{
				get
				{
					return this.angle;
				}
				set
				{
					this.RotateTo(value);
				}
			}
			public override float BoundRadius
			{
				get { return this.boundRad * this.Collider.GameObj.Transform.Scale.Xy.Length / MathF.Sqrt(2.0f); }
			}

			public SelPolyShape(PolyShapeInfo shape) : base(shape)
			{
				this.poly = shape;
				this.UpdatePolyStats();
			}

			public override void DrawActionGizmo(Canvas canvas, MouseAction action, Point beginLoc, Point curLoc)
			{
				base.DrawActionGizmo(canvas, action, beginLoc, curLoc);
				if (action == MouseAction.MoveObj)
				{
				    canvas.DrawText(string.Format("Center X:{0,7:0.00}", this.center.X), curLoc.X + 30, curLoc.Y + 10);
				    canvas.DrawText(string.Format("Center Y:{0,7:0.00}", this.center.Y), curLoc.X + 30, curLoc.Y + 18);
				}
				else if (action == MouseAction.ScaleObj)
				{
					if (MathF.Abs(this.scale.X - this.scale.Y) >= 0.01f)
					{
						canvas.DrawText(string.Format("Scale X:{0,7:0.00}", this.scale.X), curLoc.X + 30, curLoc.Y + 10);
						canvas.DrawText(string.Format("Scale Y:{0,7:0.00}", this.scale.Y), curLoc.X + 30, curLoc.Y + 18);
					}
					else
					{
						canvas.DrawText(string.Format("Scale:{0,7:0.00}", this.scale.X), curLoc.X + 30, curLoc.Y + 10);
					}
				}
				else if (action == MouseAction.RotateObj)
				{
					canvas.DrawText(string.Format("Angle:{0,6:0.0}", MathF.RadToDeg(this.angle)), curLoc.X + 30, curLoc.Y + 10);
				}
			}

			public void UpdatePolyStats()
			{
				this.center = Vector2.Zero;
				for (int i = 0; i < this.poly.Vertices.Length; i++)
					this.center += this.poly.Vertices[i];
				this.center /= this.poly.Vertices.Length;

				this.scale = Vector2.Zero;
				for (int i = 0; i < this.poly.Vertices.Length; i++)
				{
					this.scale.X = MathF.Max(this.scale.X, MathF.Abs(this.poly.Vertices[i].X - this.center.X));
					this.scale.Y = MathF.Max(this.scale.Y, MathF.Abs(this.poly.Vertices[i].Y - this.center.Y));
				}

				this.boundRad = 0.0f;
				for (int i = 0; i < this.poly.Vertices.Length; i++)
					this.boundRad = MathF.Max(this.boundRad, (this.poly.Vertices[i] - this.center).Length);

				this.angle = MathF.Angle(this.center.X, this.center.Y, this.poly.Vertices[0].X, this.poly.Vertices[0].Y);
			}
			private void MoveCenterTo(Vector2 newPos)
			{
				Vector2 mov = newPos - this.center;

				Vector2[] movedVertices = this.poly.Vertices.ToArray();
				for (int i = 0; i < movedVertices.Length; i++)
					movedVertices[i] += mov;

				this.poly.Vertices = movedVertices;
				this.UpdatePolyStats();
			}
			private void ScaleTo(Vector2 newScale)
			{
				Vector2 scaleRatio = newScale / this.scale;

				Vector2[] scaledVertices = this.poly.Vertices.ToArray();
				for (int i = 0; i < scaledVertices.Length; i++)
				{
					scaledVertices[i].X = (scaledVertices[i].X - this.center.X) * scaleRatio.X + this.center.X;
					scaledVertices[i].Y = (scaledVertices[i].Y - this.center.Y) * scaleRatio.Y + this.center.Y;
				}

				this.poly.Vertices = scaledVertices;
				this.UpdatePolyStats();
			}
			private void RotateTo(float newAngle)
			{
				float rot = newAngle - this.angle;

				Vector2[] rotatedVertices = this.poly.Vertices.ToArray();
				for (int i = 0; i < rotatedVertices.Length; i++)
					MathF.TransformCoord(ref rotatedVertices[i].X, ref rotatedVertices[i].Y, rot, 1.0f, this.center.X, this.center.Y);

				this.poly.Vertices = rotatedVertices;
				this.UpdatePolyStats();
			}
		}
	}
}
