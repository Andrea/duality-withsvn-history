using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Input;

using Duality;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Physics;

namespace PhysicsTestbed
{
	/// <summary>
	/// Displays information about the current testbed Scene and provides sandbox 
	/// functionality, such as dragging objects via mouse cursor, etc.
	/// </summary>
	[Serializable]
	public class Testbed : Component, ICmpRenderer, ICmpUpdatable, ICmpInitializable
	{
		private	FormattedText		name		= new FormattedText("Testbed");
		private	FormattedText		desc		= new FormattedText("Some cool description./nIt's even /cFF0000FFformatted/cFFFFFFFF!");

		// Temporary
		private	float	physicsTimeVal		= 0.0f;
		private	float	physicsTimeAcc		= 0.0f;
		private	int		physicsTimeCounter	= 100;
		private	FixedMouseJointInfo	mouseJoint	= null;
		private	FormattedText		controls	= new FormattedText();
		private	FormattedText		stats		= new FormattedText();


		/// <summary>
		/// [GET / SET] The current Testbed's name. May be formatted.
		/// </summary>
		public string Name
		{
			get { return this.name.SourceText; }
			set
			{
				this.name.Fonts = new ContentRef<Font>[] { GameRes.Data.BigFont_Font };
				this.name.MaxWidth = 500;
				this.name.MaxHeight = 500;
				this.name.ApplySource(value);
			}
		}
		/// <summary>
		/// [GET / SET] The current Testbed's description. May be formatted.
		/// </summary>
		public string Description
		{
			get { return this.desc.SourceText; }
			set
			{
				this.desc.Fonts = new ContentRef<Font>[] { GameRes.Data.SmallFont_Font };
				this.desc.MaxWidth = 500;
				this.desc.MaxHeight = 500;
				this.desc.ApplySource(value);
			}
		}

		float ICmpRenderer.BoundRadius
		{
			get { return float.MaxValue; }
		}
		bool ICmpRenderer.IsInfiniteXY
		{
			get { return true; }
		}
		RendererFlags ICmpRenderer.RenderFlags
		{
			get { return RendererFlags.Default; }
		}
		Vector3 ICmpRenderer.SpaceCoord
		{
			get { return Vector3.Zero; }
		}


		void ICmpRenderer.Draw(IDrawDevice device)
		{
			Canvas canvas = new Canvas(device);

			if (device.IsScreenOverlay)
			{
				// Testbed text
				Vector2 nameSize = this.name.Measure().Size;
				Vector2 descSize = this.desc.Measure().Size;
				Vector2 ctrlSize = this.controls.Measure().Size;
				Vector2 statsSize = this.stats.Measure().Size;
				canvas.PushState();
				// Text background
				canvas.CurrentState.SetMaterial(new BatchInfo(DrawTechnique.Alpha, ColorRgba.White));
				canvas.CurrentState.ColorTint = ColorRgba.Black.WithAlpha(0.5f);
				canvas.FillRect(10, 10, MathF.Max(nameSize.X, descSize.X, ctrlSize.X) + 20, nameSize.Y + descSize.Y + 10 + ctrlSize.Y + 10);
				canvas.FillRect(10, DualityApp.TargetResolution.Y - 20 - statsSize.Y, statsSize.X + 20, statsSize.Y + 10);
				// Caption / Name
				canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(0.85f);
				canvas.DrawText(this.name, 20, 15);
				// Description, Controls, Stats
				canvas.CurrentState.ColorTint = ColorRgba.White.WithAlpha(0.65f);
				canvas.DrawText(this.desc, 20, 15 + nameSize.Y);
				canvas.DrawText(this.controls, 20, 15 + nameSize.Y + descSize.Y + 10);
				canvas.DrawText(this.stats, 20, DualityApp.TargetResolution.Y - 15 - statsSize.Y);
				canvas.PopState();

				// Mouse cursor
				canvas.DrawCross(DualityApp.Mouse.X, DualityApp.Mouse.Y, 5.0f);
			}
			else
			{
				// Mouse joint, if existing
				if (this.mouseJoint != null)
				{
					Vector3 jointBegin = this.mouseJoint.BodyA.GameObj.Transform.GetWorldPoint(new Vector3(this.mouseJoint.LocalAnchor, -0.01f));
					Vector3 jointEnd = new Vector3(this.mouseJoint.WorldAnchor, -0.01f);
					canvas.CurrentState.ColorTint = ColorRgba.Red.WithAlpha(0.5f);
					canvas.DrawLine(jointBegin.X, jointBegin.Y, jointBegin.Z, jointEnd.X, jointEnd.Y, jointEnd.Z);
				}
			}
		}
		bool ICmpRenderer.IsVisible(IDrawDevice device)
		{
			return device.VisibilityMask.HasFlag(VisibilityFlag.Group0);
		}
		void ICmpUpdatable.OnUpdate()
		{
			Camera cam = this.GameObj.Camera;

			// Mouse pressed? Drag stuff
			if (DualityApp.Mouse[MouseButton.Left])
			{
				Vector2 cursorScreen = new Vector2(DualityApp.Mouse.X, DualityApp.Mouse.Y);
				Vector2 cursorWorld = cam.GetSpaceCoord(cursorScreen).Xy;

				// Create mouse joint
				if (this.mouseJoint == null)
				{
					ShapeInfo shape = RigidBody.PickShapeGlobal(cursorWorld);
					RigidBody body = shape != null ? shape.Parent : null;
					if (body != null && body.BodyType == BodyType.Dynamic)
					{
						this.mouseJoint = new FixedMouseJointInfo();
						this.mouseJoint.MaxForce = 20.0f; // More power
						this.mouseJoint.LocalAnchor = body.GameObj.Transform.GetLocalPoint(cursorWorld);
						body.AddJoint(this.mouseJoint);
					}
				}

				// Update mouse joint
				if (this.mouseJoint != null)
				{
					this.mouseJoint.WorldAnchor = cursorWorld;
				}
			}
			// Mouse not pressed? Release stuff
			else
			{
				if (this.mouseJoint != null)
				{
					this.mouseJoint.BodyA.RemoveJoint(this.mouseJoint);
					this.mouseJoint = null;
				}
			}

			// Update stats
			// When using a fixed physics timestep, physics isn't updated every frame at high FPS, so
			// we'll just average the physics time value over several frames and display that value. Thats easier to read anyway.
			this.physicsTimeAcc += Performance.UpdatePhysicsTime;
			this.physicsTimeCounter++;
			if (this.physicsTimeCounter >= 25)
			{
				this.physicsTimeVal = this.physicsTimeAcc / this.physicsTimeCounter;
				this.physicsTimeCounter = 0;
				this.physicsTimeAcc = 0.0f;
			}
			// Update stats text
			this.UpdateStatsText();
		}
		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (context == InitContext.Activate)
			{
				// Initially update controls text
				this.UpdateControlsText();
				// Register input events
				DualityApp.Mouse.ButtonDown += this.Mouse_ButtonDown;
				DualityApp.Keyboard.KeyDown += this.Keyboard_KeyDown;
			}
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
			if (context == ShutdownContext.Deactivate)
			{
				// Unregister input events
				DualityApp.Mouse.ButtonDown -= this.Mouse_ButtonDown;
				DualityApp.Keyboard.KeyDown -= this.Keyboard_KeyDown;
			}
		}

		private void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
		{
			Camera cam = this.GameObj.Camera;
			Vector2 cursorScreen = new Vector2(e.X, e.Y);
			Vector2 cursorWorld = cam.GetSpaceCoord(cursorScreen).Xy;

			if (e.Button == MouseButton.Right)
			{
				// Instantiate randomly selected body Prefab
				int objType = MathF.Rnd.Next(4);
				GameObject obj = null;
				switch (objType)
				{
					default:
					case 0: obj = GameRes.Data.Bodies.Circle_Prefab.Res.Instantiate(); break;
					case 1: obj = GameRes.Data.Bodies.Square_Prefab.Res.Instantiate(); break;
					case 2: obj = GameRes.Data.Bodies.RoundSquare_Prefab.Res.Instantiate(); break;
					case 3: obj = GameRes.Data.Bodies.Complex_Prefab.Res.Instantiate(); break;
				}
				// Check for a space that isn't occupied
				float rad = 8.0f;
				while (true)
				{
					List<ShapeInfo> shapes = RigidBody.PickShapesGlobal(cursorWorld - Vector2.One * 25.0f, Vector2.One * 50.0f);
					if (shapes.Count > 0)
					{
						cursorWorld += MathF.Rnd.NextVector2(rad);
						rad *= 2.0f;
					}
					else
						break;
				}
				// Configure and register object
				obj.Transform.Pos = new Vector3(cursorWorld);
				obj.Transform.Angle = MathF.Rnd.NextFloat(MathF.RadAngle360);
				obj.Transform.Scale = Vector3.One * MathF.Rnd.NextFloat(0.1f, 0.3f);
				Scene.Current.RegisterObj(obj);
			}
		}
		private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Number1)		this.SwitchToScene(GameRes.Data.Examples.Basic_Scene);
			else if (e.Key == Key.Number2)	this.SwitchToScene(GameRes.Data.Examples.Stacking_Scene);
			else if (e.Key == Key.Number3)	this.SwitchToScene(GameRes.Data.Examples.Friction_Scene);
			else if (e.Key == Key.Number4)	this.SwitchToScene(GameRes.Data.Examples.Restitution_Scene);
			else if (e.Key == Key.Number5)	this.SwitchToScene(GameRes.Data.Examples.Mass_Scene);
			else if (e.Key == Key.Number6)	this.SwitchToScene(GameRes.Data.Examples.Landscape_Scene);
			else if (e.Key == Key.Number7)	this.SwitchToScene(GameRes.Data.Examples.Trigger_Scene);
			else if (e.Key == Key.Number8)	this.SwitchToScene(GameRes.Data.Examples.Handling_Scene);
			else if (e.Key == Key.Number9)	this.SwitchToScene(GameRes.Data.Examples.Joints_Scene);
			else if (e.Key == Key.Escape)	DualityApp.Terminate();
			else if (e.Key == Key.Space)
			{
				if (Time.TimeMult > 0.0f)
					Time.Freeze();
				else
					Time.Resume();
			}
		}

		private void SwitchToScene(ContentRef<Scene> newScene)
		{
			Scene.Current.Dispose();
			Scene.Current = newScene.Res;
		}
		private void UpdateControlsText()
		{
			string text =
				"/cFFAAAAFFLeft Mouse/cFFFFFFFF: Drag object/n" +
				"/cFFAAAAFFRight Mouse/cFFFFFFFF: Create object/n" +
				"/cFFAAAAFFNumber keys/cFFFFFFFF: Select testbed scene/n" +
				"/cFFAAAAFFSpace/cFFFFFFFF: Pause / Unpause";

			// Insert dynamic controls text here later

			this.controls.Fonts = new ContentRef<Font>[] { GameRes.Data.SmallFont_Font };
			this.controls.MaxWidth = 500;
			this.controls.MaxHeight = 500;
			this.controls.ApplySource(text);
		}
		private void UpdateStatsText()
		{
			RigidBody[] bodies = Scene.Current.ActiveObjects.RigidBody(true).ToArray();
			int staticCount = bodies.Count(b => b.BodyType == BodyType.Static);
			int dynCount = bodies.Length - staticCount;
			int awakeCount = bodies.Count(b => b.BodyType == BodyType.Dynamic && b.IsAwake);
			int sleepCount = dynCount - awakeCount;
			string text = string.Format(
				"FPS: {0}/n" +
				"Render Time: {1:F} ms/n" +
				"Physics Time: {2:F} ms/n" +
				"Active Bodies: {3}/n" +
				"Sleeping Bodies: {4}",
				MathF.RoundToInt(Time.Fps),
				Performance.RenderTime,
				this.physicsTimeVal,
				awakeCount,
				sleepCount);

			// Insert dynamic controls text here later

			this.stats.Fonts = new ContentRef<Font>[] { GameRes.Data.SmallFont_Font };
			this.stats.MaxWidth = 500;
			this.stats.MaxHeight = 500;
			this.stats.ApplySource(text);
		}
	}
}
