using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality.Components
{
	[Serializable]
	public sealed class Transform : Component, ICmpUpdatable, ICmpEditorUpdatable, ICmpGameObjectListener, ICmpInitializable
	{
		private		Vector3		pos				= Vector3.Zero;
		private		Vector3		vel				= Vector3.Zero;
		private		float		angle			= 0.0f;
		private		float		angleVel		= 0.0f;
		private		Vector3		scale			= Vector3.One;
		private		bool		deriveAngle		= true;

		// Cached values, recalc on change
		private		Transform	parentTransform	= null;
		private		Vector3		posAbs			= Vector3.Zero;
		private		Vector3		velAbs			= Vector3.Zero;
		private		float		angleAbs		= 0.0f;
		private		float		angleVelAbs		= 0.0f;
		private		Vector3		scaleAbs		= Vector3.One;

		public Vector3 RelativePos
		{
			get { return this.pos; }
			set { this.pos = value; this.UpdateAbs(); }
		}
		public Vector3 RelativeVel
		{
			get { return this.vel; }
			set { this.vel = value; this.UpdateAbs(); }
		}
		public float RelativeAngle
		{
			get { return this.angle; }
			set { this.angle = value; this.UpdateAbs(); }
		}
		public float RelativeAngleVel
		{
			get { return this.angleVel; }
			set { this.angleVel = value; this.UpdateAbs(); }
		}
		public Vector3 RelativeScale
		{
			get { return this.scale; }
			set { this.scale = value; this.UpdateAbs(); }
		}
		public bool DeriveAngle
		{
			get { return this.deriveAngle; }
			set { this.deriveAngle = value; this.UpdateAbs(); }
		}

		public Vector3 RelativeForward
		{
			get 
			{ 
				return new Vector3(
					MathF.Sin(this.RelativeAngle),
					-MathF.Cos(this.RelativeAngle),
					0.0f);
			}
		}
		public Vector3 RelativeRight
		{
			get 
			{
				return new Vector3(
					-MathF.Cos(this.RelativeAngle),
					-MathF.Sin(this.RelativeAngle),
					0.0f);
			}
		}

		public Vector3 Pos
		{
			get { return this.posAbs; }
			set 
			{ 
				this.posAbs = value;

				if (this.parentTransform != null)
				{
					Vector3 temp;
					this.pos = this.posAbs;
					temp = this.parentTransform.posAbs;
					Vector3.Subtract(ref this.pos, ref temp, out this.pos);
					temp = this.parentTransform.scaleAbs;
					Vector3.Divide(ref this.pos, ref temp, out this.pos);
					MathF.TransformCoord(ref this.pos.X, ref this.pos.Y, -this.parentTransform.angleAbs);
				}
				else
				{
					this.pos = value;
				}

				this.UpdateAbs(true);
			}
		}
		public Vector3 Vel
		{
			get { return this.velAbs; }
			set 
			{ 
				this.velAbs = value;

				if (this.parentTransform != null)
				{
					this.vel = this.velAbs;
					Vector3 temp;
					temp = this.parentTransform.velAbs;
					Vector3.Subtract(ref this.vel, ref temp, out this.vel);
					temp = this.parentTransform.scaleAbs;
					Vector3.Divide(ref this.vel, ref temp, out this.vel);
					MathF.TransformCoord(ref this.vel.X, ref this.vel.Y, -this.parentTransform.angleAbs);
				}
				else
				{
					this.vel = value;
				}

				this.UpdateAbs(true);
			}
		}
		public float Angle
		{
			get { return this.angleAbs; }
			set 
			{ 
				this.angleAbs = value;

				if (this.parentTransform != null && this.deriveAngle)
					this.angle = this.angleAbs - this.parentTransform.angleAbs;
				else
					this.angle = value;

				this.UpdateAbs(true);
			}
		}
		public float AngleVel
		{
			get { return this.angleVelAbs; }
			set 
			{ 
				this.angleVelAbs = value;

				if (this.parentTransform != null && this.deriveAngle)
					this.angleVel = this.angleVelAbs - this.parentTransform.angleVelAbs;
				else
					this.angleVel = value;

				this.UpdateAbs(true);
			}
		}
		public Vector3 Scale
		{
			get { return this.scaleAbs; }
			set 
			{ 
				this.scaleAbs = value;

				if (this.parentTransform != null)
				{
					this.scale.X = this.scaleAbs.X / this.parentTransform.scaleAbs.X;
					this.scale.Y = this.scaleAbs.Y / this.parentTransform.scaleAbs.Y;
					this.scale.Z = this.scaleAbs.Z / this.parentTransform.scaleAbs.Z;
				}
				else
				{
					this.scale = value;
				}

				this.UpdateAbs(true);
			}
		}

		public Vector3 Forward
		{
			get 
			{ 
				return new Vector3(
					MathF.Sin(this.Angle),
					-MathF.Cos(this.Angle),
					0.0f);
			}
		}
		public Vector3 Right
		{
			get 
			{
				return new Vector3(
					-MathF.Cos(this.Angle),
					-MathF.Sin(this.Angle),
					0.0f);
			}
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (this.angleVel != 0.0f || this.vel != Vector3.Zero)
			{
				this.angle	+= this.angleVel	* Time.TimeMult;
				this.pos	+= this.vel			* Time.TimeMult;

				this.angle = MathF.NormalizeAngle(this.angle);
				this.UpdateAbs();
			}
		}
		void ICmpEditorUpdatable.OnUpdate()
		{
			this.UpdateAbs();
		}
		void ICmpGameObjectListener.OnGameObjectParentChanged(GameObject oldParent, GameObject newParent)
		{
			if (oldParent != null)
			{
				if (this.parentTransform == null)
					oldParent.EventComponentAdded -= this.Parent_EventComponentAdded;
				else
					oldParent.EventComponentRemoving -= this.Parent_EventComponentRemoving;
			}

			if (newParent != null)
			{
				this.parentTransform = newParent.Transform;
				if (this.parentTransform == null)
					newParent.EventComponentAdded += this.Parent_EventComponentAdded;
				else
					newParent.EventComponentRemoving += this.Parent_EventComponentRemoving;
			}
			else
				this.parentTransform = null;

			this.UpdateAbs();
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.AddToGameObject)
			{
				if (this.gameobj.Parent != null)
				{
					this.parentTransform = this.gameobj.Parent.Transform;
					if (this.parentTransform == null)
						this.gameobj.Parent.EventComponentAdded += this.Parent_EventComponentAdded;
					else
						this.gameobj.Parent.EventComponentRemoving += this.Parent_EventComponentRemoving;
				}
				else
					this.parentTransform = null;
				this.UpdateAbs();
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.RemovingFromGameObject)
			{
				if (this.gameobj.Parent != null)
				{
					if (this.parentTransform == null)
						this.gameobj.Parent.EventComponentAdded -= this.Parent_EventComponentAdded;
					else
						this.gameobj.Parent.EventComponentRemoving -= this.Parent_EventComponentRemoving;
				}

				this.parentTransform = null;
				this.UpdateAbs();
			}
		}
		private void Parent_EventComponentAdded(object sender, ComponentEventArgs e)
		{
			Transform cmpTransform = e.Component as Transform;
			if (cmpTransform != null)
			{
				cmpTransform.GameObj.EventComponentAdded -= this.Parent_EventComponentAdded;
				cmpTransform.GameObj.EventComponentRemoving += this.Parent_EventComponentRemoving;
				this.parentTransform = cmpTransform;
				this.UpdateAbs();
			}
		}
		private void Parent_EventComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (this.parentTransform != null)
			{
				Transform cmpTransform = e.Component as Transform;
				if (cmpTransform == this.parentTransform)
				{
					cmpTransform.GameObj.EventComponentAdded += this.Parent_EventComponentAdded;
					cmpTransform.GameObj.EventComponentRemoving -= this.Parent_EventComponentRemoving;
					this.parentTransform = null;
					this.UpdateAbs();
				}
			}
		}

		private void UpdateAbs(bool childOnly = false)
		{
			if (!childOnly)
			{
				if (this.parentTransform == null)
				{
					this.angleAbs = this.angle;
					this.angleVelAbs = this.angleVel;
					this.posAbs = this.pos;
					this.velAbs = this.vel;
					this.scaleAbs = this.scale;
				}
				else
				{
					if (this.deriveAngle)
					{
						this.angleAbs = this.angle + this.parentTransform.angleAbs;
						this.angleAbs = MathF.NormalizeAngle(this.angleAbs);
						this.angleVelAbs = this.angleVel + this.parentTransform.angleVelAbs;
					}
					else
					{
						this.angleAbs = this.angle;
						this.angleVelAbs = this.angleVel;
					}
					Vector3.Multiply(ref this.scale, ref this.parentTransform.scaleAbs, out this.scaleAbs);

					Vector3.Multiply(ref this.pos, ref this.parentTransform.scaleAbs, out this.posAbs);
					MathF.TransformCoord(ref this.posAbs.X, ref this.posAbs.Y, this.parentTransform.angleAbs);
					Vector3.Add(ref this.posAbs, ref this.parentTransform.posAbs, out this.posAbs);

					Vector3.Multiply(ref this.vel, ref this.parentTransform.scaleAbs, out this.velAbs);
					MathF.TransformCoord(ref this.velAbs.X, ref this.velAbs.Y, this.parentTransform.angleAbs);
					Vector3.Add(ref this.velAbs, ref this.parentTransform.velAbs, out this.velAbs);
				}
			}

			if (this.gameobj != null)
			{
				foreach (GameObject obj in this.gameobj.Children)
				{
					if (obj.Transform != null) obj.Transform.UpdateAbs();
				}
			}
		}

		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			Transform t = target as Transform;
			t.pos		= this.pos;
			t.vel		= this.vel;
			t.angle		= this.angle;
			t.angleVel	= this.angleVel;
			t.scale		= this.scale;
			t.deriveAngle = this.deriveAngle;
			t.UpdateAbs();
		}
	}
}
