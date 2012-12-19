﻿using System.Linq;

using Duality;
using Duality.Components;
using Duality.Resources;

namespace EditorBase.CamViewStates
{
	public class GameViewCamViewState : CamViewState
	{
		public override string StateName
		{
			get { return PluginRes.EditorBaseRes.CamViewState_GameView_Name; }
		}

		public GameViewCamViewState()
		{
			this.CameraActionAllowed = false;
			this.MouseActionAllowed = false;
			this.EngineUserInput = true;
		}

		protected internal override void OnEnterState()
		{
			base.OnEnterState();
			this.View.SetToolbarCamSettingsEnabled(false);
		}
		protected internal override void OnLeaveState()
		{
			base.OnLeaveState();
			this.View.SetToolbarCamSettingsEnabled(true);
		}
		protected override void OnRenderState()
		{
			// Render game pov
			if (!Scene.Current.Cameras.Any())	DrawDevice.RenderVoid();
			else								DualityApp.Render();
		}
	}
}
