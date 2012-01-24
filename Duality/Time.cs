using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

	/// <summary>
	/// This class houses several performance counters and performance measurement utility
	/// </summary>
	public static class Performance
	{
		internal class Counter
		{
			private	string		name		= null;
			private	Stopwatch	watch		= new Stopwatch();
			private	float		value		= 0.0f;
			private	float		lastValue	= 0.0f;
			private	bool		used		= true;
			private	bool		lastUsed	= true;
			private	bool		neverRemove	= true;

			public string Name
			{
				get { return this.name; }
			}
			public bool NeverRemove
			{
				get { return this.neverRemove; }
			}
			public bool WasUsed
			{
				get { return this.lastUsed; }
			}
			public float LastValue
			{
				get { return this.lastValue; }
			}

			public Counter(string name, bool timeout = false)
			{
				this.name = name;
				this.neverRemove = !timeout;
			}

			public void BeginMeasure()
			{
				this.watch.Restart();
			}
			public void EndMeasure()
			{
				this.value += this.watch.ElapsedTicks * 1000.0f / Stopwatch.Frequency;
				this.used = true;
			}
			public void Reset()
			{
				this.lastUsed = this.used;
				this.used = false;

				this.lastValue = this.value;
				this.value = 0.0f;
			}
		}

		internal	static	Counter	timeUpdate				= new Counter("Duality_Update");
		internal	static	Counter	timeUpdatePhysics		= new Counter("Duality_UpdatePhysics");
		internal	static	Counter	timeUpdateScene			= new Counter("Duality_UpdateScene");
		internal	static	Counter	timeUpdateAudio			= new Counter("Duality_UpdateAudio");

		internal	static	Counter	timeRender				= new Counter("Duality_Render");
		internal	static	Counter	timeCollectDrawcalls	= new Counter("Duality_CollectDrawcalls");
		internal	static	Counter	timeOptimizeDrawcalls	= new Counter("Duality_OptimizeDrawcalls");
		internal	static	Counter	timeProcessDrawcalls	= new Counter("Duality_ProcessDrawcalls");
		internal	static	Counter	timePostProcessing		= new Counter("Duality_PostProcessing");

		private	static	Dictionary<string,Counter>	counterMap	= new Dictionary<string,Counter>();

		/// <summary>
		/// [GET] Time in milliseconds the last DualityApp.Update() call took
		/// </summary>
		public static float UpdateTime
		{
			get { return timeUpdate.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for physics calculation
		/// </summary>
		public static float UpdatePhysicsTime
		{
			get { return timeUpdatePhysics.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for scene updates
		/// </summary>
		public static float UpdateSceneTime
		{
			get { return timeUpdateScene.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for audio updates (without streaming)
		/// </summary>
		public static float UpdateAudioTime
		{
			get { return timeUpdateAudio.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last DualityApp.Render() call took
		/// </summary>
		public static float RenderTime
		{
			get { return timeRender.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for collecting drawcalls
		/// </summary>
		public static float CollectDrawcallsTime
		{
			get { return timeCollectDrawcalls.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for optimizing drawcalls / batching
		/// </summary>
		public static float OptimizeDrawcallsTime
		{
			get { return timeOptimizeDrawcalls.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for processing drawcalls
		/// </summary>
		public static float ProcessDrawcallsTime
		{
			get { return timeProcessDrawcalls.LastValue; }
		}
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for postprocessing.
		/// </summary>
		public static float PostProcessingTime
		{
			get { return timePostProcessing.LastValue; }
		}
		
		public static void BeginMeasure(string counter)
		{
			Counter c;
			if (!counterMap.TryGetValue(counter, out c))
			{
				c = new Counter(counter, true);
				counterMap[counter] = c;
			}
			c.BeginMeasure();
		}
		public static void EndMeasure(string counter)
		{
			Counter c;
			if (counterMap.TryGetValue(counter, out c)) c.EndMeasure();
		}
		public static float GetMeasure(string counter)
		{
			Counter c;
			if (counterMap.TryGetValue(counter, out c))
				return c.LastValue;
			else
				return 0.0f;
		}
		public static KeyValuePair<string,float>[] GetAllMeasures()
		{
			return counterMap.Where(p => p.Value.WasUsed).Select(p => new KeyValuePair<string,float>(p.Key, p.Value.LastValue)).ToArray();
		}

		public static void DrawAllMeasures(Canvas canvas, float x = 10.0f, float y = 10.0f)
		{
			float yOff = 0.0f;
			foreach (var m in Performance.GetAllMeasures())
			{
				canvas.DrawText(string.Format(System.Globalization.CultureInfo.InvariantCulture, 
					"{0}: {1:F}", m.Key, m.Value), 
					x, y + yOff);
				yOff += canvas.CurrentState.TextFont.Res.Height;
			}
		}

		internal static void InitDualityCounters()
		{
			counterMap.Add(timeUpdate.Name, timeUpdate);
			counterMap.Add(timeUpdatePhysics.Name, timeUpdatePhysics);
			counterMap.Add(timeUpdateAudio.Name, timeUpdateAudio);
			counterMap.Add(timeUpdateScene.Name, timeUpdateScene);

			counterMap.Add(timeRender.Name, timeRender);
			counterMap.Add(timeCollectDrawcalls.Name, timeCollectDrawcalls);
			counterMap.Add(timeOptimizeDrawcalls.Name, timeOptimizeDrawcalls);
			counterMap.Add(timeProcessDrawcalls.Name, timeProcessDrawcalls);
			counterMap.Add(timePostProcessing.Name, timePostProcessing);
		}
		internal static void ResetCounters()
		{
			foreach (Counter c in counterMap.Values.ToArray())
			{
				if (!c.WasUsed && !c.NeverRemove) counterMap.Remove(c.Name);
				c.Reset();
			}
		}
	}
}
