using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Components;
using Duality.Resources;

namespace Duality.Components
{
	[Serializable]
	[RequiredComponent(typeof(Transform))]
	public sealed class SoundListener : Component, ICmpInitializable
	{
		public SoundListener()
		{
		}
		internal override void CopyToInternal(Component target)
		{
			base.CopyToInternal(target);
			SoundListener c = target as SoundListener;
		}

		public void MakeCurrent()
		{
			if (!this.Active) return;
			DualityApp.Sound.Listener = this.GameObj;
		}

		void ICmpInitializable.OnInit(Component.InitContext context)
		{
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Editor &&
				(context == InitContext.Activate || context == InitContext.AddToGameObject || context == InitContext.Loaded))
				this.MakeCurrent();
		}
		void ICmpInitializable.OnShutdown(Component.ShutdownContext context)
		{
		}
	}
}
