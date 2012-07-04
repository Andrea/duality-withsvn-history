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
	public class RigidBodyJointCamViewLayer : CamViewLayer
	{
		public override string LayerName
		{
			get { return PluginRes.EditorBaseRes.CamViewLayer_RigidBodyJoint_Name; }
		}
		public override string LayerDesc
		{
			get { return PluginRes.EditorBaseRes.CamViewLayer_RigidBodyJoint_Desc; }
		}
		public ColorRgba JointColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(128, 255, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(128, 255, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}
		public ColorRgba JointErrorColor
		{
			get
			{
				float fgLum = this.View.FgColor.GetLuminance();
				if (fgLum > 0.5f)
					return ColorRgba.Mix(new ColorRgba(255, 64, 0), ColorRgba.VeryLightGrey, 0.5f);
				else
					return ColorRgba.Mix(new ColorRgba(255, 64, 0), ColorRgba.VeryDarkGrey, 0.5f);
			}
		}

		protected internal override void OnCollectDrawcalls(Canvas canvas)
		{
			base.OnCollectDrawcalls(canvas);
			canvas.CurrentState.TextInvariantScale = true;

			RigidBody selectedBody = this.QuerySelectedCollider();

			List<RigidBody> visibleColliders = this.QueryVisibleColliders().ToList();
			List<JointInfo> visibleJoints = new List<JointInfo>();
			foreach (RigidBody c in visibleColliders) visibleJoints.AddRange(c.Joints.Where(j => !visibleJoints.Contains(j)));
			foreach (JointInfo j in visibleJoints)
			{
				float jointAlpha = selectedBody != null && (j.ColliderA == selectedBody || j.ColliderB == selectedBody) ? 1.0f : 0.5f;
				canvas.CurrentState.ColorTint = canvas.CurrentState.ColorTint.WithAlpha(jointAlpha);

				this.DrawJoint(canvas, j);
			}
		}

		protected void DrawJoint(Canvas canvas, JointInfo joint)
		{
			if (joint.ColliderA == null) return;
			if (joint.DualJoint && joint.ColliderB == null) return;

			if (joint is FixedAngleJointInfo)			this.DrawJoint(canvas, joint as FixedAngleJointInfo);
			else if (joint is FixedDistanceJointInfo)	this.DrawJoint(canvas, joint as FixedDistanceJointInfo);
			else if (joint is WeldJointInfo)			this.DrawJoint(canvas, joint as WeldJointInfo);
		}
		protected void DrawJoint(Canvas canvas, FixedAngleJointInfo joint)
		{
			Vector3 colliderPosA = joint.ColliderA.GameObj.Transform.Pos;

			ColorRgba clr = this.JointColor;
			ColorRgba clrErr = this.JointErrorColor;

			Vector2 angleVec = Vector2.FromAngleLength(joint.TargetAngle, joint.ColliderA.BoundRadius);
			Vector2 errorVec = Vector2.FromAngleLength(joint.ColliderA.GameObj.Transform.Angle, joint.ColliderA.BoundRadius);
			Vector2 angleDir = angleVec.Normalized;
			Vector2 errorDir = errorVec.Normalized;
			bool hasError = MathF.CircularDist(joint.TargetAngle, joint.ColliderA.GameObj.Transform.Angle) >= MathF.RadAngle1;

			if (hasError)
			{
				float circleBegin = joint.ColliderA.GameObj.Transform.Angle;
				float circleEnd = joint.TargetAngle;
				if (MathF.TurnDir(circleBegin, circleEnd) < 0)
				{
					MathF.Swap(ref circleBegin, ref circleEnd);
					circleEnd = circleBegin + MathF.CircularDist(circleBegin, circleEnd);
				}

				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clrErr));
				canvas.DrawLine(
					colliderPosA.X,
					colliderPosA.Y,
					colliderPosA.Z - 0.01f, 
					colliderPosA.X + errorVec.X,
					colliderPosA.Y + errorVec.Y,
					colliderPosA.Z - 0.01f);
				canvas.DrawCircleSegment(
					colliderPosA.X,
					colliderPosA.Y,
					colliderPosA.Z - 0.01f,
					joint.ColliderA.BoundRadius,
					circleBegin,
					circleEnd);
				canvas.CurrentState.TransformAngle = errorVec.Angle;
				canvas.CurrentState.TransformHandle = Vector2.UnitY * canvas.CurrentState.TextFont.Res.Height;
				canvas.DrawText(
					string.Format("{0:F0}°", MathF.RadToDeg(joint.ColliderA.GameObj.Transform.Angle)),
					colliderPosA.X + errorVec.X,
					colliderPosA.Y + errorVec.Y,
					colliderPosA.Z - 0.01f);
				canvas.CurrentState.TransformHandle = Vector2.Zero;
				canvas.CurrentState.TransformAngle = 0.0f;
			}
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr));
			canvas.DrawLine(
				colliderPosA.X,
				colliderPosA.Y,
				colliderPosA.Z - 0.01f, 
				colliderPosA.X + angleVec.X,
				colliderPosA.Y + angleVec.Y,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = angleVec.Angle;
			canvas.DrawText(
				string.Format("{0:F0}°", MathF.RadToDeg(joint.TargetAngle)),
				colliderPosA.X + angleVec.X,
				colliderPosA.Y + angleVec.Y,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = 0.0f;
		}
		protected void DrawJoint(Canvas canvas, FixedDistanceJointInfo joint)
		{
			Vector3 colliderPosA = joint.ColliderA.GameObj.Transform.Pos;

			ColorRgba clr = this.JointColor;
			ColorRgba clrErr = this.JointErrorColor;

			Vector2 errorVec = joint.WorldAnchor - colliderPosA.Xy;
			Vector2 distVec = errorVec.Normalized * joint.TargetDistance;
			Vector2 lineNormal = errorVec.PerpendicularRight.Normalized;
			float dist = errorVec.Length;
			bool hasError = (errorVec - distVec).Length >= 1.0f;

			if (hasError)
			{
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clrErr));
				canvas.DrawLine(
					colliderPosA.X + distVec.X,
					colliderPosA.Y + distVec.Y,
					colliderPosA.Z - 0.01f, 
					colliderPosA.X + errorVec.X,
					colliderPosA.Y + errorVec.Y,
					colliderPosA.Z - 0.01f);
				canvas.DrawLine(
					colliderPosA.X + errorVec.X - lineNormal.X * 5.0f,
					colliderPosA.Y + errorVec.Y - lineNormal.Y * 5.0f,
					colliderPosA.Z - 0.01f, 
					colliderPosA.X + errorVec.X + lineNormal.X * 5.0f,
					colliderPosA.Y + errorVec.Y + lineNormal.Y * 5.0f,
					colliderPosA.Z - 0.01f);
				canvas.CurrentState.TransformAngle = errorVec.Angle;
				canvas.DrawText(
					string.Format("{0:F1}", dist),
					colliderPosA.X + errorVec.X,
					colliderPosA.Y + errorVec.Y,
					colliderPosA.Z - 0.01f);
				canvas.CurrentState.TransformAngle = 0.0f;
			}
			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr));
			canvas.DrawLine(
				colliderPosA.X,
				colliderPosA.Y,
				colliderPosA.Z - 0.01f, 
				colliderPosA.X + distVec.X,
				colliderPosA.Y + distVec.Y,
				colliderPosA.Z - 0.01f);
			canvas.DrawLine(
				colliderPosA.X - lineNormal.X * 5.0f,
				colliderPosA.Y - lineNormal.Y * 5.0f,
				colliderPosA.Z - 0.01f, 
				colliderPosA.X + lineNormal.X * 5.0f,
				colliderPosA.Y + lineNormal.Y * 5.0f,
				colliderPosA.Z - 0.01f);
			canvas.DrawLine(
				colliderPosA.X + distVec.X - lineNormal.X * 5.0f,
				colliderPosA.Y + distVec.Y - lineNormal.Y * 5.0f,
				colliderPosA.Z - 0.01f, 
				colliderPosA.X + distVec.X + lineNormal.X * 5.0f,
				colliderPosA.Y + distVec.Y + lineNormal.Y * 5.0f,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = errorVec.Angle;
			canvas.DrawText(
				string.Format("{0:F1}", joint.TargetDistance),
				colliderPosA.X + distVec.X,
				colliderPosA.Y + distVec.Y,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = 0.0f;
		}
		protected void DrawJoint(Canvas canvas, WeldJointInfo joint)
		{
			Vector3 colliderPosA = joint.ColliderA.GameObj.Transform.Pos;
			Vector3 colliderPosB = joint.ColliderB.GameObj.Transform.Pos;

			ColorRgba clr = this.JointColor;
			ColorRgba clrErr = this.JointErrorColor;

			float angularCircleRadA = joint.ColliderA.BoundRadius * 0.25f;
			float angularCircleRadB = joint.ColliderB.BoundRadius * 0.25f;
			Vector2 anchorA = joint.ColliderA.GameObj.Transform.GetWorldVector(new Vector3(joint.LocalAnchorA)).Xy;
			Vector2 anchorB = joint.ColliderB.GameObj.Transform.GetWorldVector(new Vector3(joint.LocalAnchorB)).Xy;
			Vector2 errorVec = (colliderPosB.Xy + anchorB) - (colliderPosA.Xy + anchorA);
			bool displaySecondCollider = errorVec.Length >= angularCircleRadA + angularCircleRadB;
			
			bool hasError = errorVec.Length >= 1.0f;
			if (hasError)
			{
				string errorText = string.Format("{0:F1}", errorVec.Length);
				Vector2 errorTextSize = canvas.MeasureText(errorText);
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clrErr));
				canvas.DrawLine(
					colliderPosA.X + anchorA.X,
					colliderPosA.Y + anchorA.Y,
					colliderPosA.Z - 0.01f,
					colliderPosB.X + anchorB.X,
					colliderPosB.Y + anchorB.Y,
					colliderPosB.Z - 0.01f);
				canvas.CurrentState.TransformAngle = errorVec.PerpendicularLeft.Angle;
				canvas.CurrentState.TransformHandle = new Vector2(errorTextSize.X * 0.5f, 0.0f);
				canvas.DrawText(
					errorText,
					colliderPosA.X + anchorA.X + errorVec.X * 0.5f,
					colliderPosA.Y + anchorA.Y + errorVec.Y * 0.5f,
					colliderPosA.Z - 0.01f);
				canvas.CurrentState.TransformAngle = 0.0f;
				canvas.CurrentState.TransformHandle = Vector2.Zero;
			}

			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr));
			canvas.DrawLine(
				colliderPosA.X,
				colliderPosA.Y,
				colliderPosA.Z - 0.01f,
				colliderPosA.X + anchorA.X,
				colliderPosA.Y + anchorA.Y,
				colliderPosA.Z - 0.01f);
			canvas.FillCircle(
				colliderPosA.X + anchorA.X,
				colliderPosA.Y + anchorA.Y,
				colliderPosA.Z - 0.01f,
				3.0f);
			canvas.DrawLine(
				colliderPosB.X,
				colliderPosB.Y,
				colliderPosB.Z - 0.01f,
				colliderPosB.X + anchorB.X,
				colliderPosB.Y + anchorB.Y,
				colliderPosB.Z - 0.01f);
			canvas.FillCircle(
				colliderPosB.X + anchorB.X,
				colliderPosB.Y + anchorB.Y,
				colliderPosB.Z - 0.01f,
				3.0f);

			Vector2 angleVec = Vector2.FromAngleLength(joint.ColliderA.GameObj.Transform.Angle + joint.RefAngle, 1.0f);
			Vector2 angleErrorVec = Vector2.FromAngleLength(joint.ColliderB.GameObj.Transform.Angle, 1.0f);

			bool hasAngleError = MathF.CircularDist(joint.ColliderA.GameObj.Transform.Angle + joint.RefAngle, joint.ColliderB.GameObj.Transform.Angle) >= MathF.RadAngle1;
			if (hasAngleError)
			{
				float circleBegin = MathF.NormalizeAngle(joint.ColliderA.GameObj.Transform.Angle + joint.RefAngle);
				float circleEnd = MathF.NormalizeAngle(joint.ColliderB.GameObj.Transform.Angle);
				if (MathF.TurnDir(circleBegin, circleEnd) < 0)
				{
					MathF.Swap(ref circleBegin, ref circleEnd);
					circleEnd = circleBegin + MathF.CircularDist(circleBegin, circleEnd);
				}

				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clrErr));
				canvas.DrawLine(
					colliderPosA.X + anchorA.X,
					colliderPosA.Y + anchorA.Y,
					colliderPosA.Z - 0.01f,
					colliderPosA.X + anchorA.X + angleErrorVec.X * angularCircleRadA,
					colliderPosA.Y + anchorA.Y + angleErrorVec.Y * angularCircleRadA,
					colliderPosA.Z - 0.01f);
				canvas.DrawCircleSegment(
					colliderPosA.X + anchorA.X,
					colliderPosA.Y + anchorA.Y,
					colliderPosA.Z - 0.01f,
					angularCircleRadA,
					circleBegin,
					circleEnd);
				canvas.CurrentState.TransformAngle = angleErrorVec.Angle;
				canvas.CurrentState.TransformHandle = Vector2.UnitY * canvas.CurrentState.TextFont.Res.Height;
				canvas.DrawText(
					string.Format("{0:F0}°", MathF.RadToDeg(joint.ColliderB.GameObj.Transform.Angle)),
					colliderPosA.X + anchorA.X + angleErrorVec.X * angularCircleRadA,
					colliderPosA.Y + anchorA.Y + angleErrorVec.Y * angularCircleRadA,
					colliderPosA.Z - 0.01f);
				canvas.CurrentState.TransformHandle = Vector2.Zero;
				canvas.CurrentState.TransformAngle = 0.0f;
				if (displaySecondCollider)
				{
					canvas.DrawLine(
						colliderPosB.X + anchorB.X,
						colliderPosB.Y + anchorB.Y,
						colliderPosB.Z - 0.01f,
						colliderPosB.X + anchorB.X + angleErrorVec.X * angularCircleRadB,
						colliderPosB.Y + anchorB.Y + angleErrorVec.Y * angularCircleRadB,
						colliderPosB.Z - 0.01f);
					canvas.DrawCircleSegment(
						colliderPosB.X + anchorB.X,
						colliderPosB.Y + anchorB.Y,
						colliderPosB.Z - 0.01f,
						angularCircleRadB,
						circleBegin,
						circleEnd);
					canvas.CurrentState.TransformAngle = angleErrorVec.Angle;
					canvas.CurrentState.TransformHandle = Vector2.UnitY * canvas.CurrentState.TextFont.Res.Height;
					canvas.DrawText(
						string.Format("{0:F0}°", MathF.RadToDeg(joint.ColliderB.GameObj.Transform.Angle)),
						colliderPosB.X + anchorB.X + angleErrorVec.X * angularCircleRadB,
						colliderPosB.Y + anchorB.Y + angleErrorVec.Y * angularCircleRadB,
						colliderPosB.Z - 0.01f);
					canvas.CurrentState.TransformHandle = Vector2.Zero;
					canvas.CurrentState.TransformAngle = 0.0f;
				}
			}

			canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, clr));
			canvas.DrawLine(
				colliderPosA.X + anchorA.X,
				colliderPosA.Y + anchorA.Y,
				colliderPosA.Z - 0.01f,
				colliderPosA.X + anchorA.X + angleVec.X * angularCircleRadA,
				colliderPosA.Y + anchorA.Y + angleVec.Y * angularCircleRadA,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = angleVec.Angle;
			canvas.DrawText(
				string.Format("{0:F0}°", MathF.RadToDeg(MathF.NormalizeAngle(joint.ColliderA.GameObj.Transform.Angle + joint.RefAngle))),
				colliderPosA.X + anchorA.X + angleVec.X * angularCircleRadA,
				colliderPosA.Y + anchorA.Y + angleVec.Y * angularCircleRadA,
				colliderPosA.Z - 0.01f);
			canvas.CurrentState.TransformAngle = 0.0f;
			if (displaySecondCollider)
			{
				canvas.DrawLine(
					colliderPosB.X + anchorB.X,
					colliderPosB.Y + anchorB.Y,
					colliderPosB.Z - 0.01f,
					colliderPosB.X + anchorB.X + angleVec.X * angularCircleRadB,
					colliderPosB.Y + anchorB.Y + angleVec.Y * angularCircleRadB,
					colliderPosB.Z - 0.01f);
				canvas.CurrentState.TransformAngle = angleVec.Angle;
				canvas.DrawText(
					string.Format("{0:F0}°", MathF.RadToDeg(MathF.NormalizeAngle(joint.ColliderA.GameObj.Transform.Angle + joint.RefAngle))),
					colliderPosB.X + anchorB.X + angleVec.X * angularCircleRadB,
					colliderPosB.Y + anchorB.Y + angleVec.Y * angularCircleRadB,
					colliderPosB.Z - 0.01f);
				canvas.CurrentState.TransformAngle = 0.0f;
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
