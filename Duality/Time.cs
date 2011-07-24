﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Duality
{
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
		internal	static	float	perfUpdate	= 0.0f;
		internal	static	float	perfRender	= 0.0f;

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
		/// [GET] Time in milliseconds the last DualityApp.Update() call took
		/// </summary>
		public static float UpdatePerformance
		{
			get { return perfUpdate; }
		}	//	G
		/// <summary>
		/// [GET] Time in milliseconds the last DualityApp.Render() call took
		/// </summary>
		public static float RenderPerformance
		{
			get { return perfRender; }
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

		public static void Freeze()
		{
			timeFreeze = true;
		}
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
			lastDelta = mainTimer - frameBegin;
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
