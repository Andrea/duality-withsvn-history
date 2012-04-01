using System.Linq;

using Duality;
using Duality.Components;
using Duality.Resources;

namespace EditorBase.CamViewStates
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
			this.MouseActionAllowed = false;
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
			if (!Scene.Current.Cameras.Any())	Camera.RenderVoid();
			else								DualityApp.Render();
		}
	}
}
