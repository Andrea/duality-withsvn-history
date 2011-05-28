using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace Duality
{
	public class SoundDevice : IDisposable
	{
		private	bool			disposed	= false;
		private	AudioContext	context		= null;

		public AudioContext Context
		{
			get { return this.context; }
		}

		public SoundDevice()
		{
			this.context = new AudioContext();
		}

		~SoundDevice()
		{
			this.Dispose(false);
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool manually)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.OnDisposed(manually);
			}
		}
		protected virtual void OnDisposed(bool manually)
		{
			this.context.Dispose();
			this.context = null;
		}
	}
}
