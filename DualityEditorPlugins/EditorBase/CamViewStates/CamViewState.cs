using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duality;
using Duality.Components;
using Duality.Resources;
using Duality.ColorFormat;
using Duality.VertexFormat;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase
{
	public abstract class CamViewState
	{
		public enum CameraAction
		{
			None,
			MoveCam,
			TurnCam
		}


		private	CamView			view					= null;
		private	Point			camActionBeginLoc		= Point.Empty;
		private Vector3			camActionBeginLocSpace	= Vector3.Zero;
		private	CameraAction	camAction				= CameraAction.None;
		private	bool			camActionAllowed		= true;
		private	bool			camTransformChanged		= false;

		public CamView View
		{
			get { return this.view; }
			internal set { this.view = value; }
		}
		public abstract string StateName { get; }
		protected bool CameraActionAllowed
		{
			get { return this.camActionAllowed; }
			set
			{ 
				this.camActionAllowed = value;
				if (!this.camActionAllowed) this.camAction = CameraAction.None;
			}
		}
		protected bool CameraTransformChanged
		{
			get { return this.camTransformChanged; }
		}

		internal protected virtual void OnEnterState()
		{
			this.View.LocalGLControl.Paint		+= this.LocalGLControl_Paint;
			this.View.LocalGLControl.MouseDown	+= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	+= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	+= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel += this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.LostFocus	+= this.LocalGLControl_LostFocus;
			this.View.AccMovementChanged		+= this.View_AccMovementChanged;
			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp += this.EditorForm_AfterUpdateDualityApp;

			Scene.Leaving += this.Scene_Changed;
			Scene.Entered += this.Scene_Changed;
			Scene.GameObjectRegistered += this.Scene_Changed;
			Scene.GameObjectUnregistered += this.Scene_Changed;
			Scene.RegisteredObjectComponentAdded += this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved += this.Scene_Changed;

			if (Scene.Current != null) this.Scene_Changed(this, null);
		}
		internal protected virtual void OnLeaveState() 
		{
			this.View.LocalGLControl.Paint		-= this.LocalGLControl_Paint;
			this.View.LocalGLControl.MouseDown	-= this.LocalGLControl_MouseDown;
			this.View.LocalGLControl.MouseUp	-= this.LocalGLControl_MouseUp;
			this.View.LocalGLControl.MouseMove	-= this.LocalGLControl_MouseMove;
			this.View.LocalGLControl.MouseWheel -= this.LocalGLControl_MouseWheel;
			this.View.LocalGLControl.LostFocus	-= this.LocalGLControl_LostFocus;
			this.View.AccMovementChanged		-= this.View_AccMovementChanged;
			EditorBasePlugin.Instance.EditorForm.AfterUpdateDualityApp -= this.EditorForm_AfterUpdateDualityApp;
			
			Scene.Leaving -= this.Scene_Changed;
			Scene.Entered -= this.Scene_Changed;
			Scene.GameObjectRegistered -= this.Scene_Changed;
			Scene.GameObjectUnregistered -= this.Scene_Changed;
			Scene.RegisteredObjectComponentAdded -= this.Scene_Changed;
			Scene.RegisteredObjectComponentRemoved -= this.Scene_Changed;
		}
		protected virtual void OnPrepareDrawState()
		{
			this.DrawCamMoveIndicators();
		}
		protected virtual void OnDrawState()
		{
			// Render CamView
			this.View.CameraComponent.Render();
		}
		protected virtual void OnUpdateState()
		{
			GameObject camObj = this.View.CameraObj;
			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);

			this.camTransformChanged = false;
			if (this.View.AccMovement)
			{
				if (this.camAction == CameraAction.MoveCam)
				{
					Vector3 moveVec = new Vector3(
						0.125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f),
						0.125f * MathF.Sign(cursorPos.Y - this.camActionBeginLoc.Y) * MathF.Pow(MathF.Abs(cursorPos.Y - this.camActionBeginLoc.Y), 1.25f),
						camObj.Transform.RelativeVel.Z);

					MathF.TransformCoord(ref moveVec.X, ref moveVec.Y, camObj.Transform.Angle);
					camObj.Transform.RelativeVel = moveVec;

					this.camTransformChanged = true;
				}
				else if (camObj.Transform.RelativeVel.Length > 0.01f)
				{
					camObj.Transform.RelativeVel *= MathF.Pow(0.9f, Time.TimeMult);
					this.camTransformChanged = true;
				}
				else
					camObj.Transform.RelativeVel = Vector3.Zero;
			

				if (this.camAction == CameraAction.TurnCam)
				{
					float turnDir = 
						0.000125f * MathF.Sign(cursorPos.X - this.camActionBeginLoc.X) * 
						MathF.Pow(MathF.Abs(cursorPos.X - this.camActionBeginLoc.X), 1.25f);
					camObj.Transform.RelativeAngleVel = turnDir;

					this.camTransformChanged = true;
				}
				else if (Math.Abs(camObj.Transform.RelativeAngleVel) > 0.001f)
				{
					camObj.Transform.RelativeAngleVel *= MathF.Pow(0.9f, Time.TimeMult);
					this.camTransformChanged = true;
				}
				else
					camObj.Transform.RelativeAngleVel = 0.0f;
			}
			else
			{
				camObj.Transform.RelativeVel = Vector3.Zero;
				camObj.Transform.RelativeAngleVel = 0.0f;
			}


			if (this.camTransformChanged)
			{
				this.View.UpdateStatusTransformInfo();
				this.View.LocalGLControl.Invalidate();
			}
			
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Launcher)
				this.View.LocalGLControl.Invalidate();
		}
		protected virtual void OnSceneChanged()
		{
			this.View.LocalGLControl.Invalidate();
		}

		protected void DrawViewSpaceLine(float x, float y, float x2, float y2, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;

			// Turn with camera: Calculate transform vectors
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(cam.GameObj.Transform.Angle, out catDotX, out catDotY);

			Vector3 lineV1 = new Vector3(
				x - this.View.LocalGLControl.Width / 2, 
				y - this.View.LocalGLControl.Height / 2, 
				0);
			Vector3 lineV2 = new Vector3(
				x2 - this.View.LocalGLControl.Width / 2, 
				y2 - this.View.LocalGLControl.Height / 2, 
				0);

			// Apply transform vectors
			MathF.TransformDotVec(ref lineV1, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref lineV2, ref catDotX, ref catDotY);

			cam.DrawDevice.AddVertices(
				new BatchInfo(dt, clr), 
				BeginMode.Lines,
				new VertexP3(lineV1),
				new VertexP3(lineV2));
		}
		protected void DrawWorldSpaceSphere(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;
			Vector3 pos = new Vector3(x, y, z);
			if (!cam.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			Vector3 posTemp = pos;
			cam.DrawDevice.PreprocessCoords(ref posTemp, ref scale);

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r * scale, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				cam.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			cam.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);

			// XZ circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y;
				vertices[i].pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				cam.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			cam.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);

			// YZ circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X;
				vertices[i].pos.Y = pos.Y + (float)Math.Sin(angle) * r;
				vertices[i].pos.Z = pos.Z - (float)Math.Cos(angle) * r;
				cam.DrawDevice.PreprocessCoords(ref vertices[i].pos, ref scale);
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			cam.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);
		}
		protected void DrawWorldSpaceCircle(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;
			Vector3 pos = new Vector3(x, y, z);
			if (!cam.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			cam.DrawDevice.PreprocessCoords(ref pos, ref scale);
			r *= scale;

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle
			vertices = new VertexP3[segmentNum];
			angle = 0.0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			cam.DrawDevice.AddVertices(info, BeginMode.LineLoop, vertices);
		}
		protected void DrawWorldSpaceDot(float x, float y, float z, float r, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;
			Vector3 pos = new Vector3(x, y, z);
			if (!cam.DrawDevice.IsCoordInView(pos, r)) return;

			float scale = 1.0f;
			cam.DrawDevice.PreprocessCoords(ref pos, ref scale);
			r *= scale;

			BatchInfo info = new BatchInfo(dt, clr);
			int segmentNum = MathF.Clamp(MathF.RoundToInt(MathF.Pow(r, 0.65f) * 2.5f), 4, 128);
			VertexP3[] vertices;
			float angle;

			// XY circle (filled)
			vertices = new VertexP3[segmentNum + 2];
			angle = 0.0f;
			vertices[0].pos = pos;
			for (int i = 1; i < vertices.Length; i++)
			{
				vertices[i].pos.X = pos.X + (float)Math.Sin(angle) * r;
				vertices[i].pos.Y = pos.Y - (float)Math.Cos(angle) * r;
				vertices[i].pos.Z = pos.Z;
				angle += (MathF.TwoPi / (float)segmentNum);
			}
			cam.DrawDevice.AddVertices(info, BeginMode.TriangleFan, vertices);
		}
		protected void DrawWorldSpaceLine(float x, float y, float z, float x2, float y2, float z2, ContentRef<DrawTechnique> dt, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;
			Vector3 pos = new Vector3(x, y, z);
			Vector3 target = new Vector3(x2, y2, z2);
			float scale = 1.0f;

			BatchInfo info = new BatchInfo(dt, clr);
			VertexP3[] vertices = new VertexP3[2];

			vertices[0].pos = pos;
			vertices[1].pos = target;
			cam.DrawDevice.PreprocessCoords(ref vertices[0].pos, ref scale);
			cam.DrawDevice.PreprocessCoords(ref vertices[1].pos, ref scale);

			cam.DrawDevice.AddVertices(info, BeginMode.Lines, vertices);
		}
		protected void DrawSelectionMarkers(IEnumerable<GameObject> obj, ColorRgba clr)
		{
			Camera cam = this.View.CameraComponent;

			// Determine turned Camera axes for angle-independent drawing
			Vector2 catDotX, catDotY;
			MathF.GetTransformDotVec(cam.GameObj.Transform.Angle, out catDotX, out catDotY);
			Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
			Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
			MathF.TransformDotVec(ref right, ref catDotX, ref catDotY);
			MathF.TransformDotVec(ref down, ref catDotX, ref catDotY);

			foreach (GameObject selObj in obj)
			{
				if (selObj.Transform == null) continue;

				Vector3 posTemp = selObj.Transform.Pos;
				float scaleTemp = 1.0f;
				float radTemp = 0.0f;

				Renderer selObjRenderer = selObj.Renderer;
				if (selObjRenderer != null)		radTemp = selObjRenderer.BoundRadius;
				else							radTemp = CamView.DefaultDisplayBoundRadius;

				if (!cam.DrawDevice.IsCoordInView(posTemp, radTemp)) continue;
				cam.DrawDevice.PreprocessCoords(ref posTemp, ref scaleTemp);
				posTemp.Z = 0.0f;

				// Draw selection marker
				cam.DrawDevice.AddVertices(new BatchInfo(DrawTechnique.Solid, clr), BeginMode.Lines,
					new VertexP3(posTemp - right * 10.0f),
					new VertexP3(posTemp + right * 10.0f),
					new VertexP3(posTemp - down * 10.0f),
					new VertexP3(posTemp + down * 10.0f));

				// Draw angle marker
				cam.DrawDevice.AddVertices(new BatchInfo(DrawTechnique.Solid, clr), BeginMode.Lines,
					new VertexP3(posTemp),
					new VertexP3(posTemp + 
						radTemp * scaleTemp * right * MathF.Sin(selObj.Transform.Angle - cam.GameObj.Transform.Angle) - 
						radTemp * scaleTemp * down * MathF.Cos(selObj.Transform.Angle - cam.GameObj.Transform.Angle)));

				// Draw boundary
				if (radTemp > 0.0f)
				{
					this.DrawWorldSpaceCircle(
						selObj.Transform.Pos.X,
						selObj.Transform.Pos.Y,
						selObj.Transform.Pos.Z,
						radTemp,
						DrawTechnique.Solid,
						clr);
				}
			}
		}
		protected void DrawCamMoveIndicators()
		{
			Point cursorPos = this.View.LocalGLControl.PointToClient(Cursor.Position);

			// Draw camera movement indicators
			if (this.camAction == CameraAction.MoveCam)
				this.DrawViewSpaceLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, cursorPos.Y, DrawTechnique.Solid, this.View.FgColor);
			else if (this.camAction == CameraAction.TurnCam)
				this.DrawViewSpaceLine(this.camActionBeginLoc.X, this.camActionBeginLoc.Y, cursorPos.X, this.camActionBeginLoc.Y, DrawTechnique.Solid, this.View.FgColor);
		}
		
		private void LocalGLControl_Paint(object sender, PaintEventArgs e)
		{
			// Retrieve OpenGL context
 			try { this.View.MainContextControl.Context.MakeCurrent(this.View.LocalGLControl.WindowInfo); } catch (Exception) { return; }
			this.View.MakeDualityTarget();

			try
			{
				this.OnPrepareDrawState();
				this.OnDrawState();
			}
			catch (Exception exception)
			{
				Log.Editor.Write("An error occured during CamView {1} rendering: {0}", Log.Exception(exception), this.View.CameraComponent.ToString());
			}

			this.View.MainContextControl.SwapBuffers();
		}
		private void LocalGLControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.View.AccMovement)
			{
				if (this.camAction == CameraAction.MoveCam)
				{
					Vector3 movVec = new Vector3(
						5.0f * (e.X - this.camActionBeginLoc.X),
						5.0f * (e.Y - this.camActionBeginLoc.Y),
						0.0f);
					MathF.TransformCoord(ref movVec.X, ref movVec.Y, this.View.CameraObj.Transform.RelativeAngle);
					this.View.CameraObj.Transform.RelativePos = this.camActionBeginLocSpace + movVec;
					this.View.LocalGLControl.Invalidate();
					this.View.UpdateStatusTransformInfo();
				}
				else if (this.camAction == CameraAction.TurnCam)
				{
					this.View.CameraObj.Transform.RelativeAngle = MathF.NormalizeAngle(this.camActionBeginLocSpace.X + 0.01f * (e.X - this.camActionBeginLoc.X));
					this.View.LocalGLControl.Invalidate();
					this.View.UpdateStatusTransformInfo();
				}
			}
		}
		private void LocalGLControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.camAction == CameraAction.MoveCam && e.Button == MouseButtons.Middle)
				this.camAction = CameraAction.None;
			else if (this.camAction == CameraAction.TurnCam && e.Button == MouseButtons.Right)
				this.camAction = CameraAction.None;

			this.View.LocalGLControl.Invalidate();
		}
		private void LocalGLControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.camActionAllowed && this.camAction == CameraAction.None)
			{
				this.camActionBeginLoc = e.Location;
				if (e.Button == MouseButtons.Middle)
				{
					this.camAction = CameraAction.MoveCam;
					this.camActionBeginLocSpace = this.View.CameraObj.Transform.RelativePos;
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.camAction = CameraAction.TurnCam;
					this.camActionBeginLocSpace = new Vector3(this.View.CameraObj.Transform.RelativeAngle, 0.0f, 0.0f);
				}
			}
		}
		private void LocalGLControl_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta != 0)
			{
				if (this.View.ParallaxActive)
				{
					GameObject camObj = this.View.CameraObj;
					if (this.View.AccMovement)
					{
						float curVel = camObj.Transform.RelativeVel.Length * MathF.Sign(camObj.Transform.RelativeVel.Z);
						Vector2 curTemp = new Vector2(
							(e.X * 2.0f / this.View.LocalGLControl.Width) - 1.0f,
							(e.Y * 2.0f / this.View.LocalGLControl.Height) - 1.0f);
						MathF.TransformCoord(ref curTemp.X, ref curTemp.Y, camObj.Transform.RelativeAngle);

						if (MathF.Sign(e.Delta) == MathF.Sign(curVel))
							curVel *= 0.0125f * MathF.Abs(e.Delta);
						curVel += 0.075f * e.Delta;
						curVel = MathF.Sign(curVel) * MathF.Min(MathF.Abs(curVel), 500.0f);

						Vector3 movVec = new Vector3(
							MathF.Sign(e.Delta) * MathF.Sign(curTemp.X) * MathF.Pow(curTemp.X, 2.0f), 
							MathF.Sign(e.Delta) * MathF.Sign(curTemp.Y) * MathF.Pow(curTemp.Y, 2.0f), 
							1.0f);
						movVec.Normalize();
						camObj.Transform.RelativeVel = movVec * curVel;
					}
					else
					{
						camObj.Transform.Pos += new Vector3(0.0f, 0.0f, e.Delta * 5 / 12);
						this.View.LocalGLControl.Invalidate();
						this.View.UpdateStatusTransformInfo();
					}
				}
				else
				{
					this.View.ParallaxRefDist = this.View.ParallaxRefDist + this.View.ParallaxRefDistIncrement * e.Delta / 40;
				}
			}
		}
		private void LocalGLControl_LostFocus(object sender, EventArgs e)
		{
			this.camAction = CameraAction.None;
			this.View.LocalGLControl.Invalidate();
		}
		private void View_AccMovementChanged(object sender, EventArgs e)
		{
			Point curPos = this.View.LocalGLControl.PointToClient(Cursor.Position);
			if (this.camAction == CameraAction.MoveCam)
			{
				Vector3 movVec = new Vector3(
					5.0f * (curPos.X - this.camActionBeginLoc.X),
					5.0f * (curPos.Y - this.camActionBeginLoc.Y),
					0.0f);
				MathF.TransformCoord(ref movVec.X, ref movVec.Y, this.View.CameraObj.Transform.RelativeAngle);
				this.camActionBeginLocSpace = this.View.CameraObj.Transform.RelativePos - movVec;
			}
			else if (this.camAction == CameraAction.TurnCam)
			{
				this.camActionBeginLocSpace = new Vector3(this.View.CameraObj.Transform.RelativeAngle - 0.01f * (curPos.X - this.camActionBeginLoc.X), 0.0f, 0.0f);
			}
		}
		private void EditorForm_AfterUpdateDualityApp(object sender, EventArgs e)
		{
			this.OnUpdateState();
		}
		private void Scene_Changed(object sender, EventArgs e)
		{
			this.OnSceneChanged();
		}
	}
}
