using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System;

using OpenTK;

namespace Duality
{
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
			private	bool		isInt		= false;
			private	bool		used		= true;
			private	bool		lastUsed	= true;
			// Profiling report data
			private	double		accumValue		= 0.0d;
			private	float		accumMaxValue	= float.MinValue;
			private	float		accumMinValue	= float.MaxValue;
			private	int			accumSamples	= 0;


			public string Name
			{
				get { return this.name; }
			}
			public bool WasUsed
			{
				get { return this.lastUsed; }
			}
			public bool IsIntCounter
			{
				get { return this.isInt; }
			}
			public float LastValue
			{
				get { return this.lastValue; }
			}

			internal double ProfileAccumValue
			{
				get { return this.accumValue; }
			}
			internal float ProfileAverage
			{
				get { return (float)(this.accumValue / this.accumSamples); }
			}
			internal float ProfileMinimum
			{
				get { return this.accumMinValue; }
			}
			internal float ProfileMaximum
			{
				get { return this.accumMaxValue; }
			}
			internal int ProfileAccumSamples
			{
				get { return this.accumSamples; }
			}


			public Counter(string name, bool isInt = false)
			{
				this.name = name;
				this.isInt = isInt;
			}

			public void BeginMeasure()
			{
				this.watch.Restart();
			}
			public void EndMeasure()
			{
				this.value += this.watch.ElapsedTicks * 1000.0f / Stopwatch.Frequency;
				this.isInt = false;
				this.used = true;
			}
			public void Add(float value)
			{
				this.value += value;
				this.used = true;
			}
			public void Set(float value)
			{
				this.value = value;
				this.used = true;
			}
			public void Reset()
			{
				this.lastUsed = this.used;
				this.used = false;

				this.lastValue = this.value;
				this.value = 0.0f;
			}

			internal void SampleProfile()
			{
				if (this.used)
				{
					this.accumSamples++;
					this.accumMaxValue = MathF.Max(this.value, this.accumMaxValue);
					this.accumMinValue = MathF.Min(this.value, this.accumMinValue);
					this.accumValue += this.value;
				}
			}
		}

		internal	static	Counter	timeFrame				= new Counter("Duality_Frame");
		internal	static	Counter	timeUpdate				= new Counter("Duality_Update");
		internal	static	Counter	timeUpdateScene				= new Counter("Duality_Update_Scene");
		internal	static	Counter	timeUpdateAudio				= new Counter("Duality_Update_Audio");

		internal	static	Counter	timeUpdatePhysics			= new Counter("Duality_Update_Physics");
		internal	static	Counter	timeUpdatePhysicsAddRemove		= new Counter("Duality_Update_Physics_AddRemove");
		internal	static	Counter	timeUpdatePhysicsContacts		= new Counter("Duality_Update_Physics_Contacts");
		internal	static	Counter	timeUpdatePhysicsContinous		= new Counter("Duality_Update_Physics_Continous");
		internal	static	Counter	timeUpdatePhysicsController		= new Counter("Duality_Update_Physics_Controller");
		internal	static	Counter	timeUpdatePhysicsSolve			= new Counter("Duality_Update_Physics_Solve");

		internal	static	Counter	timeRender				= new Counter("Duality_Render");
		internal	static	Counter	timeSwapBuffers				= new Counter("Duality_Render_SwapBuffers");
		internal	static	Counter	timeCollectDrawcalls		= new Counter("Duality_Render_CollectDrawcalls");
		internal	static	Counter	timeOptimizeDrawcalls		= new Counter("Duality_Render_OptimizeDrawcalls");
		internal	static	Counter	timeProcessDrawcalls		= new Counter("Duality_Render_ProcessDrawcalls");
		internal	static	Counter	timePostProcessing			= new Counter("Duality_Render_PostProcessing");
		internal	static	Counter	timeVisualPicking			= new Counter("Duality_Render_Picking");

		internal	static	Counter	statNumPlaying2D			= new Counter("Duality_Audio_StatNumPlaying2D", true);
		internal	static	Counter	statNumPlaying3D			= new Counter("Duality_Audio_StatNumPlaying3D", true);
		internal	static	Counter	statNumDrawcalls			= new Counter("Duality_Render_StatNumDrawcalls", true);
		internal	static	Counter	statNumRawBatches			= new Counter("Duality_Render_StatNumRawBatches", true);
		internal	static	Counter	statNumMergedBatches		= new Counter("Duality_Render_StatNumMergedBatches", true);
		internal	static	Counter	statNumOptimizedBatches		= new Counter("Duality_Render_StatNumOptimizedBatches", true);

		internal	static	Counter	timeLog					= new Counter("Duality_Log");

		private	static	Dictionary<string,Counter>	counterMap	= new Dictionary<string,Counter>();
		
		/// <summary>
		/// [GET] Time in milliseconds the last frame took.
		/// </summary>
		public static float FrameTime
		{
			get { return timeFrame.LastValue; }
		}
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
		/// <summary>
		/// [GET] Time in milliseconds the last frame used for visual picking operations.
		/// </summary>
		public static float PickingTime
		{
			get { return timeVisualPicking.LastValue; }
		}
		/// <summary>
		/// [GET] The number of drawcalls that have been emitted last frame.
		/// </summary>
		public static int StatNumDrawcalls
		{
			get { return MathF.RoundToInt(statNumDrawcalls.LastValue); }
		}
		/// <summary>
		/// [GET] The number of raw, submitted batches without merging.
		/// </summary>
		public static int StatNumRawBatches
		{
			get { return MathF.RoundToInt(statNumRawBatches.LastValue); }
		}
		/// <summary>
		/// [GET] The number of batches before optimization.
		/// </summary>
		public static int StatNumMergedBatches
		{
			get { return MathF.RoundToInt(statNumMergedBatches.LastValue); }
		}
		/// <summary>
		/// [GET] The number of batches after optimization.
		/// </summary>
		public static int StatNumOptimizedBatches
		{
			get { return MathF.RoundToInt(statNumOptimizedBatches.LastValue); }
		}
		/// <summary>
		/// [GET] The number of currently playing 2d sounds.
		/// </summary>
		public static int StatNumPlaying2D
		{
			get { return MathF.RoundToInt(statNumPlaying2D.LastValue); }
		}
		/// <summary>
		/// [GET] The number of currently playing 3d sounds.
		/// </summary>
		public static int StatNumPlaying3D
		{
			get { return MathF.RoundToInt(statNumPlaying3D.LastValue); }
		}
		
		public static void BeginMeasure(string counter)
		{
			Counter c;
			if (!counterMap.TryGetValue(counter, out c))
			{
				c = new Counter(counter);
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
			return GetUsedCounters()
				.Where(p => !p.IsIntCounter)
				.Select(p => new KeyValuePair<string,float>(p.Name, p.LastValue))
				.ToArray();
		}
		public static void AddToStat(string counter, int value)
		{
			Counter c;
			if (!counterMap.TryGetValue(counter, out c))
			{
				c = new Counter(counter, true);
				counterMap[counter] = c;
			}
			c.Add(value);
		}
		public static int GetStat(string counter)
		{
			Counter c;
			if (counterMap.TryGetValue(counter, out c))
				return MathF.RoundToInt(c.LastValue);
			else
				return 0;
		}
		public static KeyValuePair<string,int>[] GetAllStats()
		{
			return GetUsedCounters()
				.Where(p => p.IsIntCounter)
				.Select(p => new KeyValuePair<string,int>(p.Name, MathF.RoundToInt(p.LastValue)))
				.ToArray();
		}
		private static IEnumerable<Counter> GetUsedCounters()
		{
			return counterMap.Values.Where(p => p.WasUsed);
		}

		public static void DrawAllMeasures(Canvas canvas, float x = 10.0f, float y = 10.0f, bool background = true)
		{
			string[] text = GetUsedCounters()
				.Where(m => m.LastValue >= 0.005f)
				.Select(m => string.Format(System.Globalization.CultureInfo.InvariantCulture, m.IsIntCounter ? "{0}: {1}" : "{0}: {1:F}", m.Name, m.IsIntCounter ? MathF.Round(m.LastValue) : m.LastValue))
				.ToArray();

			if (background)
				canvas.DrawTextBackground(text, x, y);
			canvas.DrawText(text, x, y);
		}
		public static void SaveTextReport(string filePath)
		{
			using (FileStream str = File.Open(filePath, FileMode.Create))
			{
				SaveTextReport(str);
			}
		}
		public static void SaveTextReport(Stream stream)
		{
			using (StreamWriter writer = new StreamWriter(stream))
			{
				Counter[] timeCounters = counterMap.Values.Where(c => !c.IsIntCounter).ToArray();
				Counter[] statCounters = counterMap.Values.Where(c => c.IsIntCounter).ToArray();
				timeCounters.StableSort((a, b) => (int)(100.0d * (b.ProfileAccumValue - a.ProfileAccumValue)));
				statCounters.StableSort((a, b) => StringComparer.InvariantCulture.Compare(a.Name, b.Name));
				
				int maxNameLen;
				int maxSamples;
				maxNameLen = timeCounters.Max(c => c.Name.Length);
				maxSamples = timeCounters.Max(c => c.ProfileAccumSamples);
				
				// Write time header
				writer.Write("Name".PadRight(maxNameLen));
				writer.Write(": ");
				writer.Write("Samples".PadRight(18));
				writer.Write(" ");
				writer.Write("Total value (ms)".PadRight(18));
				writer.Write(" ");
				writer.Write("Total impact (ms)".PadRight(18));
				writer.Write(" ");
				writer.Write("Avg. value (ms)");
				writer.WriteLine();

				// Write time data
				foreach (Counter c in timeCounters)
				{
					writer.Write(c.Name.PadRight(maxNameLen));
					writer.Write(": ");
					writer.Write(string.Format("{0}", c.ProfileAccumSamples).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0:F2}", c.ProfileAccumValue).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0:F2}", c.ProfileAccumValue / (double)maxSamples).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0:F2}", c.ProfileAverage));
					writer.WriteLine();
				}

				writer.WriteLine();
				writer.WriteLine("-----------------------------------------------------------------");
				writer.WriteLine();

				maxNameLen = statCounters.Max(c => c.Name.Length);
				maxSamples = statCounters.Max(c => c.ProfileAccumSamples);
				
				// Write stat header
				writer.Write("Name".PadRight(maxNameLen));
				writer.Write(": ");
				writer.Write("Samples".PadRight(15));
				writer.Write(" ");
				writer.Write("Min. value".PadRight(15));
				writer.Write(" ");
				writer.Write("Avg. value".PadRight(15));
				writer.Write(" ");
				writer.Write("Max. value");
				writer.WriteLine();

				// Write stat data
				foreach (Counter c in statCounters)
				{
					writer.Write(c.Name.PadRight(maxNameLen));
					writer.Write(": ");
					writer.Write(string.Format("{0}", c.ProfileAccumSamples).PadRight(15));
					writer.Write(" ");
					writer.Write(string.Format("{0}", MathF.RoundToInt(c.ProfileMinimum)).PadRight(15));
					writer.Write(" ");
					writer.Write(string.Format("{0}", MathF.RoundToInt(c.ProfileAverage)).PadRight(15));
					writer.Write(" ");
					writer.Write(string.Format("{0}", MathF.RoundToInt(c.ProfileMaximum)));
					writer.WriteLine();
				}
			}
		}

		internal static void InitDualityCounters()
		{
			foreach (System.Reflection.FieldInfo field in typeof(Performance).GetAllFields(ReflectionHelper.BindStaticAll))
			{
				if (field.FieldType != typeof(Counter)) continue;

				Counter counter = field.GetValue(null) as Counter;
				if (counter != null) counterMap.Add(counter.Name, counter);
			}
		}
		internal static void FrameTick()
		{
			foreach (Counter c in counterMap.Values.ToArray())
			{
				// Gather profiling data
				c.SampleProfile();
				// Reset counter values
				c.Reset();
			}
		}
	}
}
