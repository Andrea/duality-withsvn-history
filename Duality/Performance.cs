using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System;

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
			private	bool		used		= true;
			private	bool		lastUsed	= true;
			private	bool		neverRemove	= true;
			// Profiling report data
			private	double		accumValue		= 0.0d;
			private	int			accumSamples	= 0;


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

			internal double ProfileAccumValue
			{
				get { return this.accumValue; }
			}
			internal double ProfileAverage
			{
				get { return this.accumValue / this.accumSamples; }
			}
			internal int ProfileAccumSamples
			{
				get { return this.accumSamples; }
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
			public void SetMeasure(float value)
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
					this.accumValue += this.value;
				}
			}
		}

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
		internal	static	Counter	timeCollectDrawcalls		= new Counter("Duality_Render_CollectDrawcalls");
		internal	static	Counter	timeOptimizeDrawcalls		= new Counter("Duality_Render_OptimizeDrawcalls");
		internal	static	Counter	timeProcessDrawcalls		= new Counter("Duality_Render_ProcessDrawcalls");
		internal	static	Counter	timePostProcessing			= new Counter("Duality_Render_PostProcessing");

		internal	static	Counter	timeLog					= new Counter("Duality_Log");

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
			foreach (var m in GetAllMeasures())
			{
				if (m.Value < 0.005f) continue;
				canvas.DrawText(string.Format(System.Globalization.CultureInfo.InvariantCulture, 
					"{0}: {1:F}", m.Key, m.Value), 
					x, y + yOff);
				yOff += canvas.CurrentState.TextFont.Res.Height;
			}
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
				Counter[] counters = counterMap.Values.ToArray();
				counters.StableSort((a, b) => (int)(100.0d * (b.ProfileAccumValue - a.ProfileAccumValue)));

				int maxNameLen = counters.Max(c => c.Name.Length);
				int maxSamples = counters.Max(c => c.ProfileAccumSamples);
				
				// Write header
				writer.Write("Name".PadRight(maxNameLen));
				writer.Write(": ");
				writer.Write("Total value (ms)".PadRight(18));
				writer.Write(" ");
				writer.Write("Total impact (ms)".PadRight(18));
				writer.Write(" ");
				writer.Write("Samples".PadRight(18));
				writer.Write(" ");
				writer.Write("Avg. value (ms)");
				writer.WriteLine();

				// Write data
				foreach (Counter c in counters)
				{
					writer.Write(c.Name.PadRight(maxNameLen));
					writer.Write(": ");
					writer.Write(string.Format("{0:F2}", c.ProfileAccumValue).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0:F2}", c.ProfileAccumValue / (double)maxSamples).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0}", c.ProfileAccumSamples).PadRight(18));
					writer.Write(" ");
					writer.Write(string.Format("{0:F2}", c.ProfileAverage));
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
				// Remove unused counters
				if (!c.WasUsed && !c.NeverRemove) counterMap.Remove(c.Name);
				// Gather profiling data
				c.SampleProfile();
				// Reset counter values
				c.Reset();
			}
		}
	}
}
