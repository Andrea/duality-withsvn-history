using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using OpenTK;

using Duality;
using Duality.Components;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public partial class TransformPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj		= false;

		public TransformPropertyEditor()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public void PerformSetPos()
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			Vector3 newPos = new Vector3((float)this.editorPosX.Value, (float)this.editorPosY.Value, (float)this.editorPosZ.Value);

			if (this.relativeValues.Checked)	foreach (Transform t in values) t.RelativePos	= newPos;
			else								foreach (Transform t in values) t.Pos			= newPos;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_RelativePos);
		}
		public void PerformSetScale()
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			Vector3 newScale = new Vector3((float)this.editorScaleX.Value, (float)this.editorScaleY.Value, (float)this.editorScaleZ.Value);

			if (this.relativeValues.Checked)	foreach (Transform t in values) t.RelativeScale	= newScale;
			else								foreach (Transform t in values) t.Scale			= newScale;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_RelativeScale);
		}
		public void PerformSetVel()
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			Vector3 newVel = new Vector3((float)this.editorVelX.Value, (float)this.editorVelY.Value, (float)this.editorVelZ.Value);

			if (this.relativeValues.Checked)	foreach (Transform t in values) t.RelativeVel	= newVel;
			else								foreach (Transform t in values) t.Vel			= newVel;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_RelativeVel);
		}
		public void PerformSetAngle(bool deg)
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			float newAngle = deg ? MathF.DegToRad((float)this.editorAngleDeg.Value) : (float)this.editorAngleRad.Value;

			if (this.relativeValues.Checked)	foreach (Transform t in values) t.RelativeAngle	= newAngle;
			else								foreach (Transform t in values) t.Angle			= newAngle;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_RelativeAngle);
		}
		public void PerformSetAngleVel(bool deg)
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			float newAngleVel = deg ? MathF.DegToRad((float)this.editorAngleVelDeg.Value) : (float)this.editorAngleVelRad.Value;

			if (this.relativeValues.Checked)	foreach (Transform t in values) t.RelativeAngleVel	= newAngleVel;
			else								foreach (Transform t in values) t.AngleVel			= newAngleVel;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_RelativeAngleVel);
		}
		public void PerformSetDeriveAngle()
		{
			Transform[] values = this.Getter().OfType<Transform>().ToArray();

			foreach (Transform t in values) t.DeriveAngle = this.editorDeriveAngle.Checked;

			EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this, new ObjectSelection(values), ReflectionInfo.Property_Transform_DeriveAngle);
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Transform[] values = this.Getter().OfType<Transform>().ToArray();

			this.updatingFromObj = true;
			this.UpdateModifiedState();
			if (values.Any())
			{
				Vector3 pos, scale, vel;
				float angle, angleVel;
				bool deriveAngle;
				bool[] multiple = new bool[6];

				if (this.relativeValues.Checked)
				{
					pos.X = values.Average(t => t.RelativePos.X);
					pos.Y = values.Average(t => t.RelativePos.Y);
					pos.Z = values.Average(t => t.RelativePos.Z);
					multiple[0] = values.Any(t => t.RelativePos != values.First().RelativePos);

					scale.X = values.Average(t => t.RelativeScale.X);
					scale.Y = values.Average(t => t.RelativeScale.Y);
					scale.Z = values.Average(t => t.RelativeScale.Z);
					multiple[1] = values.Any(t => t.RelativeScale != values.First().RelativeScale);

					vel.X = values.Average(t => t.RelativeVel.X);
					vel.Y = values.Average(t => t.RelativeVel.Y);
					vel.Z = values.Average(t => t.RelativeVel.Z);
					multiple[2] = values.Any(t => t.RelativeVel != values.First().RelativeVel);

					angle = values.Average(t => t.RelativeAngle);
					multiple[3] = values.Any(t => t.RelativeAngle != values.First().RelativeAngle);

					angleVel = values.Average(t => t.RelativeAngleVel);
					multiple[4] = values.Any(t => t.RelativeAngleVel != values.First().RelativeAngleVel);
				}
				else
				{
					pos.X = values.Average(t => t.Pos.X);
					pos.Y = values.Average(t => t.Pos.Y);
					pos.Z = values.Average(t => t.Pos.Z);
					multiple[0] = values.Any(t => t.Pos != values.First().Pos);

					scale.X = values.Average(t => t.Scale.X);
					scale.Y = values.Average(t => t.Scale.Y);
					scale.Z = values.Average(t => t.Scale.Z);
					multiple[1] = values.Any(t => t.Scale != values.First().Scale);

					vel.X = values.Average(t => t.Vel.X);
					vel.Y = values.Average(t => t.Vel.Y);
					vel.Z = values.Average(t => t.Vel.Z);
					multiple[2] = values.Any(t => t.Vel != values.First().Vel);

					angle = values.Average(t => t.Angle);
					multiple[3] = values.Any(t => t.Angle != values.First().Angle);

					angleVel = values.Average(t => t.AngleVel);
					multiple[4] = values.Any(t => t.AngleVel != values.First().AngleVel);
				}

				deriveAngle = values.First().DeriveAngle;
				multiple[5] = values.Any(t => t.DeriveAngle != deriveAngle);

				angle = MathF.NormalizeAngle(angle);

				this.editorPosX.Value = (decimal)pos.X;
				this.editorPosY.Value = (decimal)pos.Y;
				this.editorPosZ.Value = (decimal)pos.Z;
				this.editorPosX.BackColor = this.editorPosY.BackColor = this.editorPosZ.BackColor = 
					multiple[0] ? this.BackColorMultiple : this.BackColorDefault;

				this.editorScaleX.Value = (decimal)scale.X;
				this.editorScaleY.Value = (decimal)scale.Y;
				this.editorScaleZ.Value = (decimal)scale.Z;
				this.editorScaleX.BackColor = this.editorScaleY.BackColor = this.editorScaleZ.BackColor = 
					multiple[1] ? this.BackColorMultiple : this.BackColorDefault;

				this.editorVelX.Value = (decimal)vel.X;
				this.editorVelY.Value = (decimal)vel.Y;
				this.editorVelZ.Value = (decimal)vel.Z;
				this.editorVelX.BackColor = this.editorVelY.BackColor = this.editorVelZ.BackColor = 
					multiple[2] ? this.BackColorMultiple : this.BackColorDefault;

				this.editorAngleRad.Value = (decimal)angle;
				this.editorAngleDeg.Value = (decimal)MathF.RadToDeg(angle);
				this.editorAngleRad.BackColor = this.editorAngleDeg.BackColor = 
					multiple[3] ? this.BackColorMultiple : this.BackColorDefault;

				this.editorAngleVelRad.Value = (decimal)angleVel;
				this.editorAngleVelDeg.Value = (decimal)MathF.RadToDeg(angleVel);
				this.editorAngleVelRad.BackColor = this.editorAngleVelDeg.BackColor = 
					multiple[4] ? this.BackColorMultiple : this.BackColorDefault;

				this.editorDeriveAngle.CheckState = 
					multiple[5] ? CheckState.Indeterminate : 
					(deriveAngle ? CheckState.Checked : CheckState.Unchecked);

				this.Invalidate();
			}
			this.updatingFromObj = false;
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;

			//this.SetterSingle(this.DisplayedValue);
		}
		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
		}
		public override void UpdateModifiedState()
		{
			base.UpdateModifiedState();
			Transform[] values = this.Getter().OfType<Transform>().ToArray();
			
			// Set font boldness according to modified value
			bool posModified			= false;
			bool scaleModified			= false;
			bool angleModified			= false;
			bool velModified			= false;
			bool angleVelModified		= false;
			bool deriveAngleModified	= false;
			foreach (Transform c in values)
			{
				Duality.Resources.PrefabLink l = (c != null && c.GameObj != null) ? c.GameObj.AffectedByPrefabLink : null;
				if (l == null) continue;
				if (!posModified)			posModified			= l.HasChange(c, ReflectionInfo.Property_Transform_RelativePos);
				if (!scaleModified)			scaleModified		= l.HasChange(c, ReflectionInfo.Property_Transform_RelativeScale);
				if (!angleModified)			angleModified		= l.HasChange(c, ReflectionInfo.Property_Transform_RelativeAngle);
				if (!velModified)			velModified			= l.HasChange(c, ReflectionInfo.Property_Transform_RelativeVel);
				if (!angleVelModified)		angleVelModified	= l.HasChange(c, ReflectionInfo.Property_Transform_RelativeAngleVel);
				if (!deriveAngleModified)	deriveAngleModified	= l.HasChange(c, ReflectionInfo.Property_Transform_DeriveAngle);
			}

			if (this.labelPos.Font.Bold != posModified) this.labelPos.Font = new Font(this.labelPos.Font, posModified ? FontStyle.Bold : FontStyle.Regular);
			if (this.labelScale.Font.Bold != scaleModified) this.labelScale.Font = new Font(this.labelScale.Font, scaleModified ? FontStyle.Bold : FontStyle.Regular);
			if (this.labelAngle.Font.Bold != angleModified) this.labelAngle.Font = new Font(this.labelAngle.Font, angleModified ? FontStyle.Bold : FontStyle.Regular);
			if (this.labelVel.Font.Bold != velModified) this.labelVel.Font = new Font(this.labelVel.Font, velModified ? FontStyle.Bold : FontStyle.Regular);
			if (this.labelAngleVel.Font.Bold != angleVelModified) this.labelAngleVel.Font = new Font(this.labelAngleVel.Font, angleVelModified ? FontStyle.Bold : FontStyle.Regular);
			if (this.editorDeriveAngle.Font.Bold != deriveAngleModified) this.editorDeriveAngle.Font = new Font(this.editorDeriveAngle.Font, deriveAngleModified ? FontStyle.Bold : FontStyle.Regular);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Color upperGradClr = ExtMethodsSystemDrawingColor.ColorFromHSV(
				this.ParentEditor.BackColor.GetHSVHue(),
				this.ParentEditor.BackColor.GetHSVSaturation(),
				0.78f);
			Color lowerGradClr = ExtMethodsSystemDrawingColor.ColorFromHSV(
				this.ParentEditor.BackColor.GetHSVHue(),
				this.ParentEditor.BackColor.GetHSVSaturation(),
				0.86f);

			e.Graphics.FillRectangle(
				new LinearGradientBrush(this.ClientRectangle, upperGradClr, lowerGradClr, 90.0f), 
				this.ClientRectangle);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(64, Color.White)),
				this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Right, this.ClientRectangle.Top);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(128, Color.Black)),
				this.ClientRectangle.Left, this.ClientRectangle.Bottom - 1, this.ClientRectangle.Right, this.ClientRectangle.Bottom - 1);
		}

		private void relativeValues_CheckedChanged(object sender, EventArgs e)
		{
			this.PerformGetValue();
		}
		private void editorPosX_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetPos();
			this.PerformGetValue();
		}
		private void editorPosY_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetPos();
			this.PerformGetValue();
		}
		private void editorPosZ_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetPos();
			this.PerformGetValue();
		}
		private void editorScaleX_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetScale();
			this.PerformGetValue();
		}
		private void editorScaleY_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetScale();
			this.PerformGetValue();
		}
		private void editorScaleZ_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetScale();
			this.PerformGetValue();
		}
		private void editorAngleDeg_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetAngle(true);
			this.PerformGetValue();
		}
		private void editorAngleRad_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetAngle(false);
			this.PerformGetValue();
		}
		private void editorVelX_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetVel();
			this.PerformGetValue();
		}
		private void editorVelY_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetVel();
			this.PerformGetValue();
		}
		private void editorVelZ_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetVel();
			this.PerformGetValue();
		}
		private void editorAngleVelDeg_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetAngleVel(true);
			this.PerformGetValue();
		}
		private void editorAngleVelRad_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetAngleVel(false);
			this.PerformGetValue();
		}
		private void editorDeriveAngle_CheckedChanged(object sender, EventArgs e)
		{
			if (this.updatingFromObj) return;
			this.PerformSetDeriveAngle();
			this.PerformGetValue();
		}
	}

	public class TransformPropertyEditorContainer : ComponentPropertyEditor
	{
		public TransformPropertyEditorContainer()
		{
			this.Indent = 0;
		}

		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			TransformPropertyEditor transformEdit = new TransformPropertyEditor();
			transformEdit.EditedType = this.EditedType;
			transformEdit.Getter = this.Getter;
			transformEdit.Setter = this.Setter;
			transformEdit.PropertyName = this.PropertyName;
			this.AddPropertyEditor(transformEdit);
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (info.DeclaringType == typeof(Transform))
			{
				return false;
			}
			return base.MemberPredicate(info);
		}
	}
}
