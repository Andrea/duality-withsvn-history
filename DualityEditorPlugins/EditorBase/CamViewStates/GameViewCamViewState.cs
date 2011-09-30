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
	public class GameViewCamViewState : CamViewState
	{
		public override string StateName
		{
			get { return "Game View"; }
		}

		public GameViewCamViewState()
		{
			this.CameraActionAllowed = false;
		}

		protected internal override void OnEnterState()
		{
			base.OnEnterState();
			this.View.SetToolbarCamSettingsEnabled(false);
			this.View.StatusbarCamera.Visible = false;
		}
		protected internal override void OnLeaveState()
		{
			base.OnLeaveState();
			this.View.SetToolbarCamSettingsEnabled(true);
			this.View.StatusbarCamera.Visible = true;
		}
		protected override void OnDrawState()
		{
			// Render game pov
			if (!Scene.Current.Cameras.Any())	Camera.RenderVoid();
			else								Scene.Current.Render();
		}
	}
}
