using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Duality.Components
{
	/// <summary>
	/// Represents an object that takes care of <see cref="Transform"/> updates instead of
	/// the Components default behaviour. This is, for example, used by the <see cref="Collider"/>
	/// Component in order to apply physics.
	/// </summary>
	public interface ITransformUpdater
	{
		/// <summary>
		/// Called when the <see cref="Transform"/> Component is being updated.
		/// </summary>
		/// <param name="t"></param>
		void UpdateTransform(Transform t);
		/// <summary>
		/// Called when the <see cref="Transform"/> Component has been changed externally
		/// </summary>
		/// <param name="t"></param>
		/// <param name="changes"></param>
		void OnTransformChanged(Transform t, Transform.DirtyFlags changes);
	}

	/// <summary>
	/// Represents a <see cref="GameObject">GameObjects</see> physical location in the world, relative to its <see cref="GameObject.Parent"/>.
	/// </summary>
	[Serializable]
	public sealed class Transform : Component, ICmpUpdatable, ICmpEditorUpdatable, ICmpGameObjectListener, ICmpInitializable
	{
		/// <summary>
		/// Flags that are used to specify, whether certain Properties have been changed.
		/// </summary>
		[Flags]
		public enum DirtyFlags
		{
			None		= 0x00,

			Pos			= 0x01,
			Vel			= 0x02,
			Angle		= 0x04,
			AngleVel	= 0x08,
			Scale		= 0x10,

			All			= Pos | Vel | Angle | AngleVel | Scale
		}

		private	Vector3	pos			= Vector3.Zero;
		private	Vector3	vel			= Vector3.Zero;
		private	float	angle		= 0.0f;
		private	float	angleVel	= 0.0f;
		private	Vector3	scale		= Vector3.One;
		private	bool	deriveAngle	= true;
		private	List<ITransformUpdater>	extUpdaters = null;
		private	DirtyFlags	changes	= DirtyFlags.None;

		// Cached values, recalc on change
		private	Transform	parentTransform	= null;
		private	Vector3		posAbs			= Vector3.Zero;
		private	Vector3		velAbs			= Vector3.Zero;
		private	float		angleAbs		= 0.0f;
		private	float		angleVelAbs		= 0.0f;
		private	Vector3		scaleAbs		= Vector3.One;

		/// <summary>
		/// [GET / SET] The objects position relative to its parent object.
		/// </summary>
		public Vector3 RelativePos
		{
			get { return this.pos; }
			set { this.pos = value; this.changes |= DirtyFlags.Pos; this.UpdateAbs(); }
		}
		/// <summary>
		/// [GET / SET] The objects velocity relative to its parent object.
		/// </summary>
		public Vector3 RelativeVel
		{
			get { return this.vel; }
			set { this.vel = value; this.changes |= DirtyFlags.Vel; this.UpdateAbs(); }
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation relative to its parent object, in radians.
		/// </summary>
		public float RelativeAngle
		{
			get { return this.angle; }
			set { this.angle = value; this.changes |= DirtyFlags.Angle; this.UpdateAbs(); }
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation velocity relative to its parent object, in radians.
		/// </summary>
		public float RelativeAngleVel
		{
			get { return this.angleVel; }
			set { this.angleVel = value; this.changes |= DirtyFlags.AngleVel; this.UpdateAbs(); }
		}
		/// <summary>
		/// [GET / SET] The objects scale relative to its parent object.
		/// </summary>
		public Vector3 RelativeScale
		{
			get { return this.scale; }
			set { this.scale = value; this.changes |= DirtyFlags.Scale; this.UpdateAbs(); }
		}
		/// <summary>
		/// [GET / SET] If false, this objects rotation values aren't relative to its parent.
		/// However, its position, velocity, etc. still depend on parent rotation.
		/// </summary>
		public bool DeriveAngle
		{
			get { return this.deriveAngle; }
			set { this.deriveAngle = value; this.changes |= DirtyFlags.Angle; this.UpdateAbs(); }
		}

		/// <summary>
		/// [GET] The objects forward vector, relative to its parent object.
		/// </summary>
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
		/// <summary>
		/// [GET] The objects right (directional) vector, relative to its parent object.
		/// </summary>
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
		
		/// <summary>
		/// [GET / SET] The objects position.
		/// </summary>
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

				this.changes |= DirtyFlags.Pos;
				this.UpdateAbs(true);
			}
		}
		/// <summary>
		/// [GET / SET] The objects velocity.
		/// </summary>
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

				this.changes |= DirtyFlags.Vel;
				this.UpdateAbs(true);
			}
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation, in radians.
		/// </summary>
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

				this.changes |= DirtyFlags.Angle;
				this.UpdateAbs(true);
			}
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation velocity, in radians.
		/// </summary>
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

				this.changes |= DirtyFlags.AngleVel;
				this.UpdateAbs(true);
			}
		}
		/// <summary>
		/// [GET / SET] The objects scale.
		/// </summary>
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

				this.changes |= DirtyFlags.Scale;
				this.UpdateAbs(true);
			}
		}
		
		/// <summary>
		/// [GET] The objects forward vector.
		/// </summary>
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
		/// <summary>
		/// [GET] The objects right (directional) vector.
		/// </summary>
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

		/// <summary>
		/// Registers an external transform updater. As long as at least one is registered, the
		/// Transform component won't attempt to update by itsself in any way, as external updates
		/// are assumed to occur on a per-frame basis.
		/// </summary>
		/// <param name="updater"></param>
		public void RegisterExternalUpdater(ITransformUpdater updater)
		{
			if (this.extUpdaters == null) this.extUpdaters = new List<ITransformUpdater>();
			else if (this.extUpdaters.Contains(updater)) return;
			this.extUpdaters.Add(updater);
		}
		/// <summary>
		/// Unregisters an external transform updater. As long as at least one is registered, the
		/// Transform component won't attempt to update by itsself in any way, as external updates
		/// are assumed to occur on a per-frame basis.
		/// </summary>
		/// <param name="updater"></param>
		public void UnregisterExternalUpdater(ITransformUpdater updater)
		{
			if (this.extUpdaters == null) return;
			this.extUpdaters.Remove(updater);
			if (this.extUpdaters.Count == 0) this.extUpdaters = null;
		}
		/// <summary>
		/// Updates the Transforms data all at once.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="vel"></param>
		/// <param name="scale"></param>
		/// <param name="angle"></param>
		/// <param name="angleVel"></param>
		public void SetTransform(Vector3 pos, Vector3 vel, Vector3 scale, float angle, float angleVel)
		{
			this.posAbs = pos;
			this.velAbs = vel;
			this.angleAbs = angle;
			this.angleVelAbs = angleVel;
			this.scaleAbs = scale;

			this.changes |= DirtyFlags.All;
			this.UpdateRel();
			this.UpdateAbs(true);
		}

		void ICmpUpdatable.OnUpdate()
		{
			if (this.extUpdaters != null)
			{
				if (this.changes != DirtyFlags.None)
				{
					foreach (ITransformUpdater upd in this.extUpdaters)
						upd.OnTransformChanged(this, this.changes);
				}
				foreach (ITransformUpdater upd in this.extUpdaters)
					upd.UpdateTransform(this);
			}
			else if (this.angleVel != 0.0f || this.vel != Vector3.Zero)
			{
				this.angle	+= this.angleVel	* Time.TimeMult;
				this.pos	+= this.vel			* Time.TimeMult;

				this.angle = MathF.NormalizeAngle(this.angle);
				this.UpdateAbs();
			}
			this.changes = DirtyFlags.None;
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

			this.UpdateRel();
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
		private void UpdateRel()
		{
			if (this.parentTransform == null)
			{
				this.angle = this.angleAbs;
				this.angleVel = this.angleVelAbs;
				this.pos = this.posAbs;
				this.vel = this.velAbs;
				this.scale = this.scaleAbs;
			}
			else
			{
				if (this.deriveAngle)
				{
					this.angle = this.angleAbs - this.parentTransform.angleAbs;
					this.angle = MathF.NormalizeAngle(this.angle);
					this.angleVel = this.angleVelAbs - this.parentTransform.angleVelAbs;
				}
				else
				{
					this.angle = this.angleAbs;
					this.angleVel = this.angleVelAbs;
				}

				Vector3.Divide(ref this.scaleAbs, ref this.parentTransform.scaleAbs, out this.scale);
				
				Vector3.Subtract(ref this.posAbs, ref this.parentTransform.posAbs, out this.pos);
				MathF.TransformCoord(ref this.pos.X, ref this.pos.Y, -this.parentTransform.angleAbs);
				Vector3.Divide(ref this.pos, ref this.parentTransform.scaleAbs, out this.pos);

				Vector3.Subtract(ref this.velAbs, ref this.parentTransform.velAbs, out this.vel);
				MathF.TransformCoord(ref this.vel.X, ref this.vel.Y, -this.parentTransform.angleAbs);
				Vector3.Divide(ref this.vel, ref this.parentTransform.scaleAbs, out this.vel);
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
