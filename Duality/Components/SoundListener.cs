using System;

namespace Duality.Components
{
	/// <summary>
	/// Makes this <see cref="GameObject"/> the 3d sound listener.
	/// </summary>
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public sealed class SoundListener : Component, ICmpInitializable
	{
		internal override void CopyToInternal(Component target, Duality.Cloning.CloneProvider provider)
		{
			base.CopyToInternal(target, provider);
			SoundListener c = target as SoundListener;
		}

		public void MakeCurrent()
		{
			if (!this.Active) return;
			DualityApp.Sound.Listener = this.GameObj;
		}

		void ICmpInitializable.OnInit(InitContext context)
		{
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Editor && context == InitContext.Activate)
				this.MakeCurrent();
		}
		void ICmpInitializable.OnShutdown(ShutdownContext context)
		{
		}
	}
}
