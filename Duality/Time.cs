using System.Diagnostics;

namespace Duality
{
	/// <summary>
	/// The Time class provides a global interface for time measurement and control. It affects all time-dependent computations. 
	/// Use the <see cref="TimeMult"/> Property to make your own computations time-dependent instead of frame-dependent. Otherwise, your
	/// game logic will depend on how many FPS the player's machine achieves and mit behave differently on very slow or fast machines.
	/// </summary>
	public static class Time
	{
		/// <summary>
		/// Milliseconds a frame takes at the desired refresh rate of 60 FPS
		/// </summary>
		public const	float	MsPFMult	= 1000.0f / 60.0f;
		/// <summary>
		/// Seconds a frame takes at the desired refresh rate of 60 FPS
		/// </summary>
		public const	float	SPFMult		= 1.0f / 60.0f;

		private	static	Stopwatch	watch		= new Stopwatch();
		private	static	float		mainTimer	= 0.0f;
		private	static	float		gameTimer	= 0.0f;
		private	static	float		frameBegin	= 0.0f;
		private	static	float		lastDelta	= 0.0f;
		private	static	float		timeMult	= 0.0f;
		private	static	float		timeScale	= 1.0f;
		private	static	bool		timeFreeze	= false;
		private	static	int			frameCount	= 0;
		private	static	int			fps			= 0;
		private	static	int			fps_frames	= 0;
		private	static	float		fps_last	= 0.0f;

		/// <summary>
		/// [GET] Returns the number of milliseconds that have passed in real time.
		/// </summary>
		public static float MainTimer
		{
			get { return mainTimer; }
		}	//	G
		/// <summary>
		/// [GET] MainTimer value at the beginning of the current frame
		/// </summary>
		public static float FrameBegin
		{
			get { return frameBegin; }
		}	//	G
		/// <summary>
		/// [GET] Time in milliseconds the last frame took
		/// </summary>
		public static float LastDelta
		{
			get { return lastDelta; }
		}	//	G
		/// <summary>
		/// [GET] Frames per Second
		/// </summary>
		public static float Fps
		{
			get { return fps; }
		}			//	G
		/// <summary>
		/// [GET] Returns the number of milliseconds that have passed in game time.
		/// </summary>
		public static float GameTimer
		{
			get { return gameTimer; }
		}	//	G
		/// <summary>
		/// [GET] Multiply any frame-independend movement or change with this factor.
		/// It also applies the time scale you set.
		/// </summary>
		public static float TimeMult
		{
			get { return timeMult; }
		}		//	G
		/// <summary>
		/// [GET / SET] Specifies how fast game time runs compared to real time i.e. how
		/// fast the game runs. May be used for slow motion effects.
		/// </summary>
		public static float TimeScale
		{
			get { return timeScale; }
			set { timeScale = value; }
		}	//	GS
		/// <summary>
		/// [GET] The number of frames passed since startup
		/// </summary>
		public static int FrameCount
		{
			get { return frameCount; }
		}		//	G

		/// <summary>
		/// Freezes game time. This will cause the GameTimer to stop and TimeMult to equal zero.
		/// </summary>
		public static void Freeze()
		{
			timeFreeze = true;
		}
		/// <summary>
		/// Unfreezes game time. TimeMult resumes to its normal value and GameTimer starts running again.
		/// </summary>
		public static void Resume()
		{
			timeFreeze = false;
		}

		internal static void FrameTick()
		{
			// Initial timer start
			if (!watch.IsRunning) watch.Restart();

			frameCount++;

			mainTimer = watch.ElapsedMilliseconds;
			lastDelta = MathF.Min(mainTimer - frameBegin, MsPFMult * 2); // Don't skip more than 2 frames / fall below 30 fps
			frameBegin = mainTimer;

			gameTimer += timeFreeze ? 0.0f : lastDelta * timeScale;
			timeMult = timeFreeze ? 0.0f : timeScale * lastDelta / MsPFMult;

			fps_frames++;
			if (mainTimer - fps_last >= 1000.0f)
			{
				fps = fps_frames;
				fps_frames = 0;
				fps_last = mainTimer;
				//Log.Core.Write("FPS: {0},\tms: {1}", fps, lastDelta);
			}
		}
	}
}
