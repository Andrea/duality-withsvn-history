using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.Resources;

namespace Duality
{
	/// <summary>
	/// Defines priorities for the budget-based sound system
	/// </summary>
	public enum SoundBudgetPriority : int
	{
		Highest		= 100,
		Action		= 80,
		Alarm		= 60,
		Tension		= 40,
		Background	= 20,
		Lowest		= 0,

		// Not priorities but offset units
		StepOne		= 20,
		StepHalf	= 10,
		StepQuarter	= 5,
		StepMicro	= 1
	}

	/// <summary>
	/// Wraps a SoundInstance in order to make it budget-based. Intended
	/// only to be used for longer sound pads.
	/// </summary>
	public class SoundBudgetPad : IDisposable
	{
		private static int idCounter = 0;

		private	bool			disposed	= false;
		private	SoundInstance	sound		= null;
		private	SoundBudgetPriority	prio	= SoundBudgetPriority.Lowest;
		private	bool			giveUp		= false;
		private	bool			weak		= false;
		private	float			weight		= 1.0f;
		private	int				sortId		= ++idCounter;

		public bool Disposed
		{
			get { return disposed; }
		}
		public SoundInstance Sound
		{
			get { return this.sound; }
		}			//	G
		public SoundBudgetPriority Priority
		{
			get { return this.prio; }
		}	//	G
		public int PriorityValue
		{
			get { return (int)this.prio; }
		}				//	G
		public int SortId
		{
			get { return this.sortId; }
		}					//	GS
		public bool GivesUpWithoutBudget
		{
			get { return this.giveUp; }
			set { this.giveUp = value; }
		}		//	GS
		public bool Weak
		{
			get { return this.weak; }
			set { this.weak = value; }
		}						//	GS
		public float Weight
		{
			get { return this.weight; }
			set { this.weight = value; }
		}					//	GS
		
		internal SoundBudgetPad(SoundInstance snd, SoundBudgetPriority prio, bool music = true)
		{
			this.sound = snd;
			this.prio = prio;
		}
		~SoundBudgetPad()
		{
			this.OnDisposed(false);
		}
		public void Dispose()
		{
			this.OnDisposed(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void OnDisposed(bool disposing)
		{
			if (!this.disposed)
			{
				this.prio = SoundBudgetPriority.Lowest;
				if (this.sound != null)
				{
					this.sound.Stop();
					this.sound = null;
				}
				this.disposed = true;
			}
		}

		public void Update(bool nonWeakAbove, ref float budget)
		{
			if (this.sound.Disposed || this.sound == null)
			{
				this.Dispose();
				return;
			}

			float myBudget = Math.Min(this.weight, budget);
			float myFadedBudget = Math.Min(this.weight * this.sound.CurrentFade, budget);
			budget -= myFadedBudget;

			if (myBudget < 0.01f && (this.giveUp || (this.weak && nonWeakAbove)))
			{
				this.Dispose();
				return;
			}

			this.sound.Volume = myFadedBudget;
			this.sound.Paused = (myBudget <= 0.01f && this.sound.FadeTarget > 0.0f);
		}
	}

	public class SoundBudgetQueue
	{
		public const float DefaultFadeInTime = 3.0f;
		public const float DefaultFadeOutTime = 3.0f;

		private	List<SoundBudgetPad>	budgetPads	= new List<SoundBudgetPad>();

		public void Update()
		{
			float budget = 1.0f;
			bool nonWeakPassed = false;

			// Lowest priority at begin - we're travelling backwards
			this.budgetPads.StableSort(delegate(SoundBudgetPad obj1, SoundBudgetPad obj2)
				{
					int result = obj1.PriorityValue - obj2.PriorityValue;
					if (result != 0)	return result;
					else				return obj2.SortId - obj1.SortId;
				});

			for (int i = this.budgetPads.Count - 1; i >= 0; i--)
			{
				this.budgetPads[i].Update(nonWeakPassed, ref budget);
				if (!this.budgetPads[i].Weak) nonWeakPassed = true;
				if (this.budgetPads[i].Disposed) this.budgetPads.RemoveAt(i);
			}
		}

		public bool IsAnyScheduled()
		{
			if (this.budgetPads.Count == 0) return false;
			foreach (SoundBudgetPad pad in this.budgetPads)
			{
				if (pad.Sound.FadeTarget > 0.0f)
					return true;
			}
			return false;
		}
		public SoundBudgetPad Push(ContentRef<Sound> snd, SoundType type, SoundBudgetPriority priority, float fadeInTimeSec = DefaultFadeInTime)
		{
			SoundInstance inst = DualityApp.Sound.PlaySound2D(snd, type);
			SoundBudgetPad bP = new SoundBudgetPad(inst, priority);
			bP.Sound.Paused = true;
			if (fadeInTimeSec > 0.05f) bP.Sound.BeginFadeIn(fadeInTimeSec);
			this.budgetPads.Add(bP);
			return bP;
		}
		public void Pop(ContentRef<Sound> snd, float fadeOutTimeSec = DefaultFadeOutTime)
		{
			for (int i = 0; i < this.budgetPads.Count; i++)
			{
				if (this.budgetPads[i].Sound.SoundRef.Res == snd.Res)
				{
					this.budgetPads[i].Sound.FadeOut(fadeOutTimeSec);
				}
			}
		}
		public void PopHigh(float fadeOutTimeSec = DefaultFadeOutTime)
		{
			if (this.budgetPads.Count == 0) return;
			this.budgetPads[this.budgetPads.Count - 1].Sound.FadeOut(fadeOutTimeSec);
		}
		public void PopLow(float fadeOutTimeSec = DefaultFadeOutTime)
		{
			if (this.budgetPads.Count == 0) return;
			this.budgetPads[0].Sound.FadeOut(fadeOutTimeSec);
		}
		public void Clear(float fadeOutTimeSec = DefaultFadeOutTime)
		{
			if (this.budgetPads.Count == 0) return;
			for (int i = 0; i < this.budgetPads.Count; i++) this.budgetPads[i].Sound.FadeOut(fadeOutTimeSec);
		}
	}
}
