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
			get { return "RigidBody Joints"; }
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
				float jointAlpha = (j.ColliderA == selectedBody || j.ColliderB == selectedBody) ? 0.5f : 0.25f;
				canvas.CurrentState.ColorTint = canvas.CurrentState.ColorTint.WithAlpha(jointAlpha);

				if (j is FixedAngleJointInfo)			this.DrawJointFixedAngle(canvas, j as FixedAngleJointInfo);
				else if (j is FixedDistanceJointInfo)	this.DrawJointFixedDistance(canvas, j as FixedDistanceJointInfo);
			}
		}

		protected void DrawJointFixedAngle(Canvas canvas, FixedAngleJointInfo joint)
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
					colliderPosA.X + errorVec.X + errorDir.X,
					colliderPosA.Y + errorVec.Y + errorDir.Y,
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
		protected void DrawJointFixedDistance(Canvas canvas, FixedDistanceJointInfo joint)
		{
			Vector3 colliderPosA = joint.ColliderA.GameObj.Transform.Pos;

			ColorRgba clr = this.JointColor;
			ColorRgba clrErr = this.JointErrorColor;

			Vector2 errorVec = joint.WorldAnchor - colliderPosA.Xy;
			Vector2 distVec = errorVec.Normalized * joint.TargetDistance;
			Vector2 lineNormal = errorVec.PerpendicularRight.Normalized;
			float dist = errorVec.Length;
			bool hasError = (errorVec - distVec).Length >= 0.1f;

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
