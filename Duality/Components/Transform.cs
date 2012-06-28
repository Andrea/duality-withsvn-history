using System;

using OpenTK;

using Duality.EditorHints;

namespace Duality.Components
{
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
			Angle		= 0x04,
			Scale		= 0x10,

			All			= Pos | Angle | Scale
		}

		private const float MinScale = 0.0000001f;

		private	Vector3	pos			= Vector3.Zero;
		private	float	angle		= 0.0f;
		private	Vector3	scale		= Vector3.One;
		private	bool	deriveAngle		= true;
		private	bool	ignoreParent	= false;
		private	DirtyFlags	changes	= DirtyFlags.None;

		// Cached values, recalc on change
		private	Transform	parentTransform	= null;
		private	Vector3		posAbs			= Vector3.Zero;
		private	float		angleAbs		= 0.0f;
		private	Vector3		scaleAbs		= Vector3.One;
		// Auto-calculated values
		private	Vector3		vel				= Vector3.Zero;
		private	Vector3		velAbs			= Vector3.Zero;
		private	float		angleVel		= 0.0f;
		private	float		angleVelAbs		= 0.0f;
		// Last frame's values
		private	Vector3		lastPos			= Vector3.Zero;
		private	Vector3		lastPosAbs		= Vector3.Zero;
		private	float		lastAngle		= 0.0f;
		private	float		lastAngleAbs	= 0.0f;


		public event EventHandler<TransformChangedEventArgs> OnTransformChanged = null;


		/// <summary>
		/// [GET / SET] The objects position relative to its parent object.
		/// </summary>
		public Vector3 RelativePos
		{
			get { return this.pos; }
			set
			{ 
				// Move last value as well to keep velocity constant - because we're "teleporting" here
				this.lastPos += value - this.pos;
				this.MoveTo(value);
			}
		}
		/// <summary>
		/// [GET / SET] The objects velocity relative to its parent object.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Vector3 RelativeVel
		{
			get { return this.vel; }
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation relative to its parent object, in radians.
		/// </summary>
		public float RelativeAngle
		{
			get { return this.angle; }
			set 
			{ 
				// Move last value as well to keep velocity constant - because we're "teleporting" here
				this.lastAngle += value - this.angle;
				this.TurnTo(value);
			}
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation velocity relative to its parent object, in radians.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public float RelativeAngleVel
		{
			get { return this.angleVel; }
		}
		/// <summary>
		/// [GET / SET] The objects scale relative to its parent object.
		/// </summary>
		public Vector3 RelativeScale
		{
			get { return this.scale; }
			set
			{ 
				this.scale.X = MathF.Max(value.X, MinScale);
				this.scale.Y = MathF.Max(value.Y, MinScale);
				this.scale.Z = MathF.Max(value.Z, MinScale);
				this.changes |= DirtyFlags.Scale; 
				this.UpdateAbs();
			}
		}
		/// <summary>
		/// [GET / SET] Specifies whether the Transform component should ignore its parent transform.
		/// </summary>
		public bool IgnoreParent
		{
			get { return this.ignoreParent; }
			set
			{
				if (this.ignoreParent != value)
				{
					this.ignoreParent = value;
					this.UpdateAbs();
				}
			}
		}
		/// <summary>
		/// [GET / SET] If false, this objects rotation values aren't relative to its parent.
		/// However, its position, velocity, etc. still depend on parent rotation.
		/// </summary>
		[EditorHintFlags(MemberFlags.AffectsOthers)]
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
				// Move last value as well to keep velocity constant - because we're "teleporting" here
				this.lastPosAbs += value - this.posAbs;
				if (this.parentTransform != null)
				{
					this.lastPos = this.lastPosAbs;
					Vector3.Subtract(ref this.lastPos, ref this.parentTransform.posAbs, out this.lastPos);
					Vector3.Divide(ref this.lastPos, ref this.parentTransform.scaleAbs, out this.lastPos);
					MathF.TransformCoord(ref this.lastPos.X, ref this.lastPos.Y, -this.parentTransform.angleAbs);
				}
				else
				{
					this.lastPos = this.lastPosAbs;
				}

				this.MoveToAbs(value);
			}
		}
		/// <summary>
		/// [GET] The objects velocity.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public Vector3 Vel
		{
			get { return this.velAbs; }
		}
		/// <summary>
		/// [GET / SET] The objects angle / rotation, in radians.
		/// </summary>
		public float Angle
		{
			get { return this.angleAbs; }
			set 
			{ 
				// Move last value as well to keep velocity constant - because we're "teleporting" here
				this.lastAngleAbs += value - this.angleAbs;
				if (this.parentTransform != null && this.deriveAngle)
					this.lastAngle = this.lastAngleAbs - this.parentTransform.angleAbs;
				else
					this.lastAngle = this.lastAngleAbs;

				this.TurnToAbs(value);
			}
		}
		/// <summary>
		/// [GET] The objects angle / rotation velocity, in radians.
		/// </summary>
		[EditorHintFlags(MemberFlags.Invisible)]
		public float AngleVel
		{
			get { return this.angleVelAbs; }
		}
		/// <summary>
		/// [GET / SET] The objects scale.
		/// </summary>
		public Vector3 Scale
		{
			get { return this.scaleAbs; }
			set 
			{ 
				this.scaleAbs.X = MathF.Max(value.X, MinScale);
				this.scaleAbs.Y = MathF.Max(value.Y, MinScale);
				this.scaleAbs.Z = MathF.Max(value.Z, MinScale);

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
		/// Calculates a world coordinate from a Transform-local coordinate.
		/// </summary>
		/// <param name="local"></param>
		/// <returns></returns>
		public Vector3 GetWorldPoint(Vector3 local)
		{
			Vector3 world;

			Vector3.Multiply(ref local, ref this.scaleAbs, out world);
			MathF.TransformCoord(ref world.X, ref world.Y, this.angleAbs);
			Vector3.Add(ref world, ref this.posAbs, out world);

			return world;
		}
		/// <summary>
		/// Calculates a Transform-local coordinate from a world coordinate.
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		public Vector3 GetLocalPoint(Vector3 world)
		{
			Vector3 local;
			
			Vector3.Subtract(ref world, ref this.posAbs, out local);
			MathF.TransformCoord(ref local.X, ref local.Y, -this.angleAbs);
			Vector3.Divide(ref local, ref this.scaleAbs, out local);

			return local;
		}
		/// <summary>
		/// Calculates a world vector from a Transform-local vector.
		/// Does only take scale and rotation into account, but not position.
		/// </summary>
		/// <param name="local"></param>
		/// <returns></returns>
		public Vector3 GetWorldVector(Vector3 local)
		{
			Vector3 world;

			Vector3.Multiply(ref local, ref this.scaleAbs, out world);
			MathF.TransformCoord(ref world.X, ref world.Y, this.angleAbs);

			return world;
		}
		/// <summary>
		/// Calculates a Transform-local vector from a world vector.
		/// Does only take scale and rotation into account, but not position.
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		public Vector3 GetLocalVector(Vector3 world)
		{
			Vector3 local = world;
			
			MathF.TransformCoord(ref local.X, ref local.Y, -this.angleAbs);
			Vector3.Divide(ref local, ref this.scaleAbs, out local);

			return local;
		}
		/// <summary>
		/// Calculates the Transforms world velocity at a given world coordinate;
		/// </summary>
		/// <param name="world"></param>
		/// <returns></returns>
		public Vector3 GetWorldVelocityAt(Vector3 world)
		{
			return GetWorldVector(GetLocalVelocityAt(GetLocalPoint(world)));
		}
		/// <summary>
		/// Calculates the Transforms local velocity at a given local coordinate;
		/// </summary>
		/// <param name="local"></param>
		/// <returns></returns>
		public Vector3 GetLocalVelocityAt(Vector3 local)
		{
			Vector3 vel = this.velAbs;
			Vector2 angleVel = local.Xy.PerpendicularRight * this.angleVelAbs;
			return vel + new Vector3(angleVel);
		}

		/// <summary>
		/// Moves the object by the given vector. This will affect the Transforms <see cref="Vel">velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void MoveBy(Vector3 value)
		{
			this.MoveTo(this.pos + value);
		}
		/// <summary>
		/// Moves the object by given absolute vector. This will affect the Transforms <see cref="Vel">velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void MoveByAbs(Vector3 value)
		{
			this.MoveToAbs(this.posAbs + value);
		}
		/// <summary>
		/// Moves the object to the given relative position. This will affect the Transforms <see cref="Vel">velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void MoveTo(Vector3 value)
		{
			this.pos = value; 
			this.changes |= DirtyFlags.Pos; 
			this.UpdateAbs();
		}
		/// <summary>
		/// Moves the object to the given absolute position. This will affect the Transforms <see cref="Vel">velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void MoveToAbs(Vector3 value)
		{
			this.posAbs = value;

			if (this.parentTransform != null)
			{
				this.pos = this.posAbs;
				Vector3.Subtract(ref this.pos, ref this.parentTransform.posAbs, out this.pos);
				Vector3.Divide(ref this.pos, ref this.parentTransform.scaleAbs, out this.pos);
				MathF.TransformCoord(ref this.pos.X, ref this.pos.Y, -this.parentTransform.angleAbs);
			}
			else
			{
				this.pos = this.posAbs;
			}

			this.changes |= DirtyFlags.Pos;
			this.UpdateAbs(true);
		}
		/// <summary>
		/// Turns the object by the given radian angle. This will affect the Transforms <see cref="AngleVel">angular velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void TurnBy(float value)
		{
			this.TurnTo(this.angle + value);
		}
		/// <summary>
		/// Turns the object by the given absolute radian angle. This will affect the Transforms <see cref="AngleVel">angular velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void TurnByAbs(float value)
		{
			this.TurnToAbs(this.angleAbs + value);
		}
		/// <summary>
		/// Turns the object to the given relative radian angle. This will affect the Transforms <see cref="AngleVel">angular velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void TurnTo(float value)
		{
			this.angle = value; 
			this.changes |= DirtyFlags.Angle; 
			this.UpdateAbs();
		}
		/// <summary>
		/// Turns the object to the given absolute radian angle. This will affect the Transforms <see cref="AngleVel">angular velocity</see> value.
		/// </summary>
		/// <param name="value"></param>
		public void TurnToAbs(float value)
		{
			this.angleAbs = value;

			if (this.parentTransform != null && this.deriveAngle)
				this.angle = this.angleAbs - this.parentTransform.angleAbs;
			else
				this.angle = this.angleAbs;

			this.changes |= DirtyFlags.Angle;
			this.UpdateAbs(true);
		}

		/// <summary>
		/// Updates the Transforms data all at once.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="vel"></param>
		/// <param name="scale"></param>
		/// <param name="angle"></param>
		/// <param name="angleVel"></param>
		public void SetTransform(Vector3 pos, Vector3 scale, float angle)
		{
			// Move last value as well to keep velocity constant - because we're "teleporting" here
			this.lastPosAbs += pos - this.posAbs;
			if (this.parentTransform != null)
			{
				this.lastPos = this.lastPosAbs;
				Vector3.Subtract(ref this.lastPos, ref this.parentTransform.posAbs, out this.lastPos);
				Vector3.Divide(ref this.lastPos, ref this.parentTransform.scaleAbs, out this.lastPos);
				MathF.TransformCoord(ref this.lastPos.X, ref this.lastPos.Y, -this.parentTransform.angleAbs);
			}
			else
			{
				this.lastPos = this.lastPosAbs;
			}
			this.lastAngleAbs += angle - this.angleAbs;
			if (this.parentTransform != null && this.deriveAngle)
				this.lastAngle = this.lastAngleAbs - this.parentTransform.angleAbs;
			else
				this.lastAngle = this.lastAngleAbs;

			this.posAbs = pos;
			this.angleAbs = angle;
			this.scaleAbs = scale;

			this.changes |= DirtyFlags.All;
			this.UpdateRel();
			this.UpdateAbs(true);
		}
		/// <summary>
		/// Updates the Transforms data all at once.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="vel"></param>
		/// <param name="scale"></param>
		/// <param name="angle"></param>
		/// <param name="angleVel"></param>
		public void SetRelativeTransform(Vector3 pos, Vector3 scale, float angle)
		{
			// Move last value as well to keep velocity constant - because we're "teleporting" here
			this.lastPos += pos - this.pos;
			this.lastAngle += angle - this.angle;

			this.pos = pos;
			this.angle = angle;
			this.scale = scale;

			this.changes |= DirtyFlags.All;
			this.UpdateAbs();
		}
		
		/// <summary>
		/// Checks whether transform values have been changed, clears the changelist and fires the appropriate events
		/// </summary>
		/// <param name="sender"></param>
		public void CommitChanges(Component sender = null)
		{
			if (this.changes == DirtyFlags.None) return;
			if (sender == null) sender = this;
			if (this.OnTransformChanged != null)
				this.OnTransformChanged(sender, new TransformChangedEventArgs(this, this.changes));
			this.changes = DirtyFlags.None;
		}

		void ICmpUpdatable.OnUpdate()
		{
			this.CheckValidTransform();

			// Calculate velocity values
			if (MathF.Abs(Time.TimeMult) > float.Epsilon)
			{
				this.vel = (this.pos - this.lastPos) / Time.TimeMult;
				this.velAbs = (this.posAbs - this.lastPosAbs) / Time.TimeMult;
				this.angleVel = MathF.CircularDist(this.angle, this.lastAngle) * MathF.TurnDir(this.lastAngle, this.angle) / Time.TimeMult;
				this.angleVelAbs = MathF.CircularDist(this.angleAbs, this.lastAngleAbs) * MathF.TurnDir(this.lastAngleAbs, this.angleAbs) / Time.TimeMult;
				this.CheckValidTransform();
			}

			// Clear change flags
			this.CommitChanges();

			// Set last values
			this.lastPos = this.pos;
			this.lastPosAbs = this.posAbs;
			this.lastAngle = this.angle;
			this.lastAngleAbs = this.angleAbs;

			this.CheckValidTransform();
		}
		void ICmpEditorUpdatable.OnUpdate()
		{
			this.CheckValidTransform();

			this.UpdateAbs();
			// Clear change flags
			this.CommitChanges();

			this.CheckValidTransform();
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
		void ICmpInitializable.OnInit(InitContext context)
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
		void ICmpInitializable.OnShutdown(ShutdownContext context)
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
			this.CheckValidTransform();

			if (!childOnly)
			{
				if (this.parentTransform == null)
				{
					this.angleAbs = this.angle;
					this.lastAngleAbs = this.lastAngle;
					this.posAbs = this.pos;
					this.lastPosAbs = this.lastPos;
					this.scaleAbs = this.scale;
				}
				else if (this.ignoreParent && DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
				{
					// If there is an external updater, ignore scene graph relations and just keep relative data updated.
					this.UpdateRel();
				}
				else
				{
					if (this.deriveAngle)
					{
						this.angleAbs = this.angle + this.parentTransform.angleAbs;
						this.angleAbs = MathF.NormalizeAngle(this.angleAbs);
						this.lastAngleAbs = this.lastAngle + this.parentTransform.angleAbs;
					}
					else
					{
						this.angleAbs = this.angle;
						this.lastAngleAbs = this.lastAngle;
					}
					Vector3.Multiply(ref this.scale, ref this.parentTransform.scaleAbs, out this.scaleAbs);

					Vector3.Multiply(ref this.pos, ref this.parentTransform.scaleAbs, out this.posAbs);
					MathF.TransformCoord(ref this.posAbs.X, ref this.posAbs.Y, this.parentTransform.angleAbs);
					Vector3.Add(ref this.posAbs, ref this.parentTransform.posAbs, out this.posAbs);

					Vector3.Multiply(ref this.lastPos, ref this.parentTransform.scaleAbs, out this.lastPosAbs);
					MathF.TransformCoord(ref this.lastPosAbs.X, ref this.lastPosAbs.Y, this.parentTransform.angleAbs);
					Vector3.Add(ref this.lastPosAbs, ref this.parentTransform.posAbs, out this.lastPosAbs);
				}
			}

			if (this.gameobj != null)
			{
				foreach (GameObject obj in this.gameobj.Children)
				{
					Transform t = obj.Transform;
					if (t == null) continue;
					if (!t.ignoreParent || DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
					{
						t.UpdateAbs();

						t.changes |= this.changes;
						if ((this.changes & DirtyFlags.Scale) != DirtyFlags.None || (this.changes & DirtyFlags.Angle) != DirtyFlags.None)
							t.changes |= DirtyFlags.Pos;
					}
					else
					{
						t.UpdateRel();
					}
				}
			}

			this.CheckValidTransform();
		}
		private void UpdateRel()
		{
			this.CheckValidTransform();

			if (this.parentTransform == null)
			{
				this.angle = this.angleAbs;
				this.lastAngle = this.lastAngleAbs;
				this.pos = this.posAbs;
				this.lastPos = this.lastPosAbs;
				this.scale = this.scaleAbs;
			}
			else
			{
				if (this.deriveAngle)
				{
					this.angle = this.angleAbs - this.parentTransform.angleAbs;
					this.angle = MathF.NormalizeAngle(this.angle);
					this.lastAngle = this.lastAngleAbs - this.parentTransform.angleAbs;
				}
				else
				{
					this.angle = this.angleAbs;
					this.lastAngle = this.lastAngleAbs;
				}

				Vector3.Divide(ref this.scaleAbs, ref this.parentTransform.scaleAbs, out this.scale);
				
				Vector3.Subtract(ref this.posAbs, ref this.parentTransform.posAbs, out this.pos);
				MathF.TransformCoord(ref this.pos.X, ref this.pos.Y, -this.parentTransform.angleAbs);
				Vector3.Divide(ref this.pos, ref this.parentTransform.scaleAbs, out this.pos);

				Vector3.Subtract(ref this.lastPosAbs, ref this.parentTransform.posAbs, out this.lastPos);
				MathF.TransformCoord(ref this.lastPos.X, ref this.lastPos.Y, -this.parentTransform.angleAbs);
				Vector3.Divide(ref this.lastPos, ref this.parentTransform.scaleAbs, out this.lastPos);
			}

			this.CheckValidTransform();
		}

		internal override void CopyToInternal(Component target, Duality.Cloning.CloneProvider provider)
		{
			base.CopyToInternal(target, provider);
			Transform t = target as Transform;

			t.deriveAngle	= this.deriveAngle;
			t.ignoreParent	= this.ignoreParent;

			t.pos		= this.pos;
			t.angle		= this.angle;
			t.scale		= this.scale;

			t.posAbs		= this.posAbs;
			t.angleAbs		= this.angleAbs;
			t.scaleAbs		= this.scaleAbs;

			t.lastPos		= this.lastPos;
			t.lastPosAbs	= this.lastPosAbs;
			t.lastAngle		= this.lastAngle;
			t.lastAngleAbs	= this.lastAngleAbs;

			t.velAbs		= this.velAbs;
			t.vel			= this.vel;
			t.angleVel		= this.angleVel;
			t.angleVelAbs	= this.angleVelAbs;

			t.UpdateRel();
		}

		[System.Diagnostics.Conditional("DEBUG")]
		private void CheckValidTransform()
		{
			CheckValidValue(ref this.pos.X);
			CheckValidValue(ref this.pos.Y);
			CheckValidValue(ref this.pos.Z);
			CheckValidValue(ref this.lastPos.X);
			CheckValidValue(ref this.lastPos.Y);
			CheckValidValue(ref this.lastPos.Z);
			CheckValidValue(ref this.vel.X);
			CheckValidValue(ref this.vel.Y);
			CheckValidValue(ref this.vel.Z);
			CheckValidValue(ref this.scale.X);
			CheckValidValue(ref this.scale.Y);
			CheckValidValue(ref this.scale.Z);
			CheckValidValue(ref this.angle);
			CheckValidValue(ref this.lastAngle);
			CheckValidValue(ref this.angleVel);
			
			CheckValidValue(ref this.posAbs.X);
			CheckValidValue(ref this.posAbs.Y);
			CheckValidValue(ref this.posAbs.Z);
			CheckValidValue(ref this.lastPosAbs.X);
			CheckValidValue(ref this.lastPosAbs.Y);
			CheckValidValue(ref this.lastPosAbs.Z);
			CheckValidValue(ref this.velAbs.X);
			CheckValidValue(ref this.velAbs.Y);
			CheckValidValue(ref this.velAbs.Z);
			CheckValidValue(ref this.scaleAbs.X);
			CheckValidValue(ref this.scaleAbs.Y);
			CheckValidValue(ref this.scaleAbs.Z);
			CheckValidValue(ref this.angleAbs);
			CheckValidValue(ref this.lastAngleAbs);
			CheckValidValue(ref this.angleVelAbs);
		}
		[System.Diagnostics.Conditional("DEBUG")]
		private static void CheckValidValue(ref float value)
		{
			if (float.IsNaN(value))
			{
				Log.Core.WriteError("Invalid transform value (NaN)");
				value = 0.0f;
			}
			else if (float.IsInfinity(value))
			{
				Log.Core.WriteError("Invalid transform value (Infinity)");
				value = 0.0f;
			}
		}
	}
}
