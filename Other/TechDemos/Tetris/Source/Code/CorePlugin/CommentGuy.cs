using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality;
using Duality.Serialization;
using Duality.Components.Renderers;

namespace Tetris
{
	public static class CommentGuy
	{
		public class Comment
		{
			private	List<Comment>		nextSupply	= null;
			private	Action<Comment>		action		= null;
			private	List<Line>			lines		= null;
			private	int					sayCount	= 0;
			private	Predicate<Comment>	condition	= null;
			private float				waitTime	= 0.0f;
			private float				waitedSoFar	= 0.0f;
			
			public Comment NextAction
			{
				get
				{
					if (this.nextSupply == null) return null;
					Comment next = this.nextSupply.FirstOrDefault(a => a.CanPerform());
					if (MathF.Rnd.Next(100) < 5)
					{
						this.nextSupply = this.nextSupply.Shuffle().ToList();
					}
					else if (next != null)
					{
						this.nextSupply.Remove(next);
						this.nextSupply.Add(next);
					}
					return next;
				}
			}

			public Comment(Line[] lines, Predicate<Comment> condition, Action<Comment> action, params Comment[] next)
			{
				this.action = action;
				this.nextSupply = next != null ? next.Shuffle().ToList() : null;
				this.lines = lines.Shuffle().ToList();
				this.condition = condition;
				this.Reset();
			}
			public void Reset()
			{
				this.waitTime = 0.0f;
				this.waitedSoFar = 0.0f;
			}
			public bool CanPerform()
			{
				return this.condition == null || this.condition(this);
			}
			public bool Perform()
			{
				if (this.waitedSoFar == 0.0f)
				{
					targetText.Text.ApplySource(this.Say(out this.waitTime));
					targetText.UpdateMetrics();
					if (this.action != null) this.action(this);
				}
				this.waitedSoFar += Time.TimeMult * Time.MsPFMult;
				bool finished = this.waitedSoFar >= this.waitTime;
				if (finished && targetText.Text.SourceText != "")
				{
					targetText.Text.ApplySource("");
					targetText.UpdateMetrics();
				}
				return finished;
			}
			
			private string Say(out float waitTime)
			{
				Line line = this.lines.FirstOrDefault(l => l.CanSay());
				this.sayCount++;

				if (MathF.Rnd.Next(this.sayCount) > this.lines.Count * 3 / 4)
				{
					this.lines = lines.Shuffle().ToList();
				}
				else if (line != null)
				{
					this.lines.Remove(line);
					this.lines.Add(line);
				}

				string text = line.Say();
				waitTime = line.TimeMult * (1500.0f + text.Length * 75.0f);
				return text;
			}

			public override string ToString()
			{
				return this.lines.Count == 0 ? "empty" : this.lines[0].ToString();
			}

			public static Comment Pause(float timeMult, params Comment[] next)
			{
				return new Comment(new Line[] { new Line("", null, timeMult) }, null, null, next);
			}
		}
		public class Line
		{
			private	string			text		= "";
			private	Func<object>[]	textParams	= null;
			private	float			timeMult	= 1.0f;
			private	int				sayCount	= 0;
			private	Predicate<Line>	condition	= null;


			public string Text
			{
				get { return this.text; }
			}
			public int SayCount
			{
				get { return this.sayCount; }
			}
			public float TimeMult
			{
				get { return this.timeMult; }
			}


			public Line(string text, Predicate<Line> condition = null, float timeMult = 1.0f)
			{
				this.text = text;
				this.condition = condition;
				this.timeMult = timeMult;
			}
			public Line(string text, params Func<object>[] textParams)
			{
				this.text = text;
				this.textParams = textParams;
			}
			public Line(string text, float timeMult, params Func<object>[] textParams)
			{
				this.text = text;
				this.textParams = textParams;
				this.timeMult = timeMult;
			}
			public Line(string text, Predicate<Line> condition, float timeMult, params Func<object>[] textParams)
			{
				this.text = text;
				this.textParams = textParams;
				this.condition = condition;
				this.timeMult = timeMult;
			}


			public bool CanSay()
			{
				return this.condition == null || this.condition(this);
			}
			public string Say()
			{
				this.sayCount++;
				if (this.textParams == null)
					return this.text;
				else
					return string.Format(this.text, (object[])this.textParams.Select(p => p()).ToArray());
			}

			public override string ToString()
			{
				return this.text;
			}
		}
		[Serializable]
		public class PermanentData
		{
			public int		Highscore			= 0;
			public int		CountGamesLost		= 0;
			public int		CountBlockFellOver	= 0;
			public bool		FirstTimePlayed		= false;
			public DateTime	TimeShutdown		= DateTime.Now;
		}
		public class TemporaryData
		{
			public int		CountGamesLost		= 0;
			public int		CountBlockFellOver	= 0;
			public float	TimeBlockFellOver	= -10000.0f;
			public string	UserName			= System.Environment.UserName;
			public TimeSpan	TimeSinceLastMet	= TimeSpan.Zero;
			public float	TimeNextBored;
		}

		private static	TextRenderer	targetText	= null;
		private	static	List<Comment>	schedule	= null;
		private	static	List<Comment>	supply		= null;

		private	static	bool			initialized				= false;
		private	static	PermanentData	permanentMind			= null;
		private	static	TemporaryData	temporaryMind			= null;

		static CommentGuy()
		{
			Comment actionFirstHello5 = new Comment(new Line[] {
				new Line("hehehe."), 
				new Line("hehe..")},
				null,
				null);
			Comment actionFirstHello4 = new Comment(new Line[] {
				new Line("It's not the classic version."), 
				new Line("This isn't quite the original."),
				new Line("It's not the regular version."),
				new Line("You might not know this one.")},
				null,
				null,
				actionFirstHello5);
			Comment actionFirstHello3 = new Comment(new Line[] {
				new Line("But.."), 
				new Line("However.."),
				new Line("Well..")},
				null,
				null,
				actionFirstHello4);
			Comment actionFirstHello2 = new Comment(new Line[] {
				new Line("You probably knew that already."), 
				new Line("Well, you might know that."),
				new Line("You should know it."),
				new Line("Everybody knows it.") ,
				new Line("It probably isn't new to you.")},
				null,
				null,
				actionFirstHello3);
			Comment actionFirstHello1 = new Comment(new Line[] {
				new Line("This game is called Tetris."), 
				new Line("This is Tetris."),
				new Line("What you can see here is called Tetris."),
				new Line("You are playing a game that is called Tetris.") ,
				new Line("This game goes by the name of Tetris.")},
				null,
				null,
				actionFirstHello2);
			Comment actionFirstHello0 = new Comment(new Line[] {
				new Line("Hi."), 
				new Line("Hello."),
				new Line("Hey there."),
				new Line("Welcome."),
				new Line("Hello there.") },
				c => GameController.Instance.TotalPlayTime < 5000.0f && !permanentMind.FirstTimePlayed,
				c => { permanentMind.FirstTimePlayed = true; temporaryMind.TimeNextBored += 20000.0f; },
				actionFirstHello1);
			
			Comment actionHello3 = new Comment(new Line[] {
				new Line("Good luck."), 
				new Line("Have fun."), 
				new Line("Do your best."), 
				new Line("Do your worst."), 
				new Line("Make the best out of it."), 
				new Line("Make the worst out of it."), 
				new Line("Go get those points."), 
				new Line("Go stack 'em.")},
				null,
				null,
				null);
			Comment actionHello2 = new Comment(new Line[] {
				new Line("Anyway.."), 
				new Line("Whatever.."), 
				new Line("However.."), 
				new Line("Well.."), 
				new Line("*sigh*"), 
				new Line("But nevermind."), 
				new Line("")},
				null,
				null,
				actionHello3);
			Comment actionHello1 = new Comment(new Line[] {
				new Line("I knew you would return."), 
				new Line("I hoped you would come back."),
				new Line("We both knew that day would come."),
				new Line("We both knew you'd return."),
				new Line("I have waited for you."),
				new Line("I didn't expect to see you again."),
				new Line("Did you miss me already? It's only been {0} since we last met.", l => temporaryMind.TimeSinceLastMet.TotalHours < 1, 1.0f, SayTimeLastMet),
				new Line("I'm surprised. It's only been {0} since we last met.", l => temporaryMind.TimeSinceLastMet.TotalHours < 1, 1.0f, SayTimeLastMet),
				new Line("Finally. After {0} you have returned.", l => temporaryMind.TimeSinceLastMet.TotalHours >= 1, 1.0f, SayTimeLastMet),
				new Line("It's been a long time.", l => temporaryMind.TimeSinceLastMet.TotalDays >= 5),
				new Line("What did you do all the time?", l => temporaryMind.TimeSinceLastMet.TotalDays >= 2),
				new Line("How's life going?", l => temporaryMind.TimeSinceLastMet.TotalDays >= 5),
				new Line("You haven't shown up here lately.", l => temporaryMind.TimeSinceLastMet.TotalDays >= 5),
				new Line("How long has it been? {0}?", l => temporaryMind.TimeSinceLastMet.TotalDays >= 2, 1.0f, SayTimeLastMet)},
				null,
				null,
				actionHello2);
			Comment actionHello0 = new Comment(new Line[] {
				new Line("Hi."), 
				new Line("Hello."),
				new Line("Hey there."),
				new Line("Hello there."),
				new Line("Oh. It's you again."),
				new Line("Look at that, my favourite player."),
				new Line("Look who we've got there. My favourite player."),
				new Line("There you are."),
				new Line("Hello again."),
				new Line("Welcome back, {0}", () => temporaryMind.UserName),
				new Line("So we meet again, {0}", () => temporaryMind.UserName),
				new Line("{0}! Good to see you.", () => temporaryMind.UserName),
				new Line("Hey {0}.", () => temporaryMind.UserName),
				new Line("Hi {0}.", () => temporaryMind.UserName),
				new Line("Hello {0}.", () => temporaryMind.UserName) },
				c => GameController.Instance.TotalPlayTime < 5000.0f && permanentMind.FirstTimePlayed && GameController.Instance.FirstGameInSession,
				null,
				actionHello1);
			
			Comment actionBlockFellOver0 = new Comment(new Line[] {
				new Line("Too bad, those blocks can fall over."),
				new Line("Physics isn't fair, huh?"),
				new Line("Oh noes! Those blocks can fall over!"),
				new Line("DAMN YOU, PHYSICS"),
				new Line("I guess, Newton sneaked in here somewhere."),
				new Line("Let there be physics."),
				new Line("Physics, huh?"),
				new Line("I love physics. Who doesn't?"),
				new Line("I bet you didn't expect that would happen.", l => !permanentMind.FirstTimePlayed),
				new Line("Man, you should've seen your face.", l => !permanentMind.FirstTimePlayed) },
				c => GameController.Instance.BlockJustFellOver && temporaryMind.CountBlockFellOver == 0,
				c => { temporaryMind.CountBlockFellOver++; temporaryMind.TimeBlockFellOver = Time.GameTimer; });
			Comment actionBlockFellOver1 = new Comment(new Line[] {
				new Line("Aw, man. Another block fallen over."),
				new Line("FTONK."),
				new Line("Roll, roll, roll those blocks"),
				new Line("Aaaand another one."),
				new Line("Man, that sucks."),
				new Line("I'm almost sorry for you. Almost."),
				new Line("I hate when that happens."),
				new Line("Ba-dum."),
				new Line("Tricky."),
				new Line("What we need is more chaos."),
				new Line("More chaos!"),
				new Line("That doesn't look right."),
				new Line("Uh-oh."),
				new Line("hehe..") },
				c => GameController.Instance.BlockJustFellOver && temporaryMind.CountBlockFellOver > 0 && MathF.Rnd.Next((int)(Time.GameTimer - temporaryMind.TimeBlockFellOver)) > 6000,
				c => { permanentMind.CountBlockFellOver++; temporaryMind.CountBlockFellOver++; temporaryMind.TimeBlockFellOver = Time.GameTimer; });
			Comment actionBlockFellOver2 = new Comment(new Line[] {
				new Line("Are you TRYING to make them fall over?"),
				new Line("In soviet russia, TETRIS plays YOU."),
				new Line("Dude, wtf."),
				new Line("Come on.. seriously?"),
				new Line("You truly are the architect of chaos."),
				new Line("There is nothing like the sound of a block falling over."),
				new Line("You must be the worst player I've ever seen."),
				new Line("You know you're supposed to arrange those blocks, do you?"),
				new Line("Have you even played Tetris before?") },
				c => GameController.Instance.BlockJustFellOver && temporaryMind.CountBlockFellOver >= 8 && MathF.Rnd.Next((int)(Time.GameTimer - temporaryMind.TimeBlockFellOver)) > 6000,
				c => { permanentMind.CountBlockFellOver++; temporaryMind.CountBlockFellOver++; temporaryMind.TimeBlockFellOver = Time.GameTimer; });

			Comment actionNothingFellOver0 = new Comment(new Line[] {
				new Line("I'm bored."),
				new Line("tzzz...."),
				new Line("Man, you're boring."),
				new Line("I feel a little useless."),
				new Line("*sigh*"),
				new Line("I'm still here, by the way."),
				new Line("Seems you don't need me."),
				new Line("Dub-de-dub", null, 0.5f),
				new Line("Seems like you're done screwing up stuff.", l => temporaryMind.CountBlockFellOver > 0),
				new Line("Are you done throwing around blocks?", l => temporaryMind.CountBlockFellOver > 0),
				new Line("No more chaos?", l => temporaryMind.CountBlockFellOver > 0) },
				c => !GameController.Instance.GameOver && GameController.Instance.TimeSinceBlockFellOver > 20000.0f && Time.GameTimer > temporaryMind.TimeNextBored,
				c => temporaryMind.TimeNextBored = Time.GameTimer + MathF.Rnd.NextFloat(15000.0f, 25000.0f));
			Comment actionNothingFellOverB1 = new Comment(new Line[] {
				new Line("For a human."),
				new Line("For someone like you."),
				new Line("For someone who's obviously never played Tetris."),
				new Line("At least, if you don't have hands."),
				new Line("At least, if you are blind."),
				new Line("Except.. well.. nevermind."),
				new Line("NOT."),
				new Line("No, really."),
				new Line("Not kidding. Totally."),
				new Line("*cough*"),
				new Line("*sigh*") },
				null,
				null);
			Comment actionNothingFellOverB0 = new Comment(new Line[] {
				new Line("You're doing good."),
				new Line("You're good."),
				new Line("Good game."),
				new Line("You're making good progress."),
				new Line("Excellent game."),
				new Line("You're getting better and better."),
				new Line("Man, look at your score. That's cool.", l => GameController.Instance.Score > 5000.0f),
				new Line("Wow, look at your score value. It's great.", l => GameController.Instance.Score > 5000.0f),
				new Line("Nice score.", l => GameController.Instance.Score > 5000.0f),
				new Line("I like your playstyle.") },
				c => !GameController.Instance.GameOver && GameController.Instance.TimeSinceBlockFellOver > 20000.0f && Time.GameTimer > temporaryMind.TimeNextBored,
				c => temporaryMind.TimeNextBored = Time.GameTimer + MathF.Rnd.NextFloat(15000.0f, 25000.0f),
				actionNothingFellOverB1);

			Comment actionLineClear = new Comment(new Line[] {
				new Line("BAM!"),
				new Line("Booyah!."),
				new Line("Here you go."),
				new Line("Another 1000 points for you."),
				new Line("Nice."),
				new Line("One line less to worry about."),
				new Line("Score!"),
				new Line("Yoink."),
				new Line("Kaboom!"),
				new Line("Baaaaam!") },
				c => GameController.Instance.LineJustCleared && MathF.Rnd.NextBool(),
				null);
			
			Comment actionGameOver4 = new Comment(new Line[] {
				new Line("Press RETURN to try again.", null, 2.0f), 
				new Line("Let me know if you want to try again. (RETURN key)", null, 2.0f),
				new Line("Another try? Press RETURN.", null, 2.0f),
				new Line("Go get yourself some coffee. Then press RETURN if you want to try again.", null, 2.0f) },
				null,
				null);
			Comment actionGameOver3a2 = new Comment(new Line[] {
				new Line("Someday.", null, 0.4f), 
				new Line("Maybe.", null, 0.4f),
				new Line("*cough*", null, 0.4f),
				new Line("Sooner or later. Maybe later."),
				new Line("Or not.", null, 0.4f),
				new Line("More or less.", null, 0.6f) },
				null,
				null,
				Comment.Pause(2.0f, actionGameOver4));
			Comment actionGameOver3a = new Comment(new Line[] {
				new Line("I'm sure you'll improve.", null, 1.5f), 
				new Line("You can do it.", null, 1.5f),
				new Line("You'll make it.", null, 1.5f),
				new Line("I'm sure it will work out.", null, 1.5f),
				new Line("You can win.", null, 1.5f),
				new Line("Practice makes perfect.", null, 1.5f) },
				null,
				null,
				Comment.Pause(0.5f, actionGameOver3a2));
			Comment actionGameOver3b = new Comment(new Line[] {
				new Line("Not that it would change anything.", null, 0.3f), 
				new Line("It won't help.", null, 0.4f),
				new Line("It won't work.", null, 0.4f),
				new Line("It won't change anything.", null, 0.3f),
				new Line("What a waste of time.", null, 0.3f),
				new Line("Or stop getting on my nerves and do something useful instead.", null, 0.2f),
				new Line("*sigh*", null, 0.3f),
				new Line("Or not.", null, 0.4f) },
				null,
				null,
				Comment.Pause(2.0f, actionGameOver4));
			Comment actionGameOver2 = new Comment(new Line[] {
				new Line("Of course, you could try again."), 
				new Line("You might give it another try."),
				new Line("Maybe you should practice a little more."),
				new Line("Do you want to play again?"),
				new Line("Do you want to give it another try?"),
				new Line("Will you try again?"),
				new Line("Another round?"),
				new Line("Another try?"),
				new Line("You should improve your reflexes."),
				new Line("You could try that again, I suppose."),
				new Line("Will you play another round?") },
				null,
				null,
				actionGameOver3a, Comment.Pause(1.0f, actionGameOver3b), actionGameOver4);
			Comment actionGameOver1 = new Comment(new Line[] {
				new Line("It was inevitable, I guess."), 
				new Line("That sucks."),
				new Line("I'm sorry."),
				new Line("It's okay I guess."),
				new Line("Time to make lemonade."),
				new Line("That's just life."),
				new Line("I'm making a note here: HUGE SUCCESS"),
				new Line("It's hard to overstate my satisfaction."),
				new Line("You couldn't have done better"),
				new Line("Don't worry. To fail is human."),
				new Line("Well.. you did okay.") },
				null,
				null,
				actionGameOver2);
			Comment actionGameOver0 = new Comment(new Line[] {
				new Line("Aw, man."), 
				new Line("Oh no.."),
				new Line("Game Over man! Game Over!"),
				new Line("I've seen that coming"),
				new Line("Aaaaand thats you losing the game."),
				new Line("You just lost the game"),
				new Line("Bam. You're dead."),
				new Line("DING! You're done."),
				new Line("That's it."),
				new Line("When life gives you lemons"),
				new Line("If at first you don't succeed") },
				c => GameController.Instance.GameJustEnded,
				c => { permanentMind.Highscore = MathF.Max(permanentMind.Highscore, GameController.Instance.Score); permanentMind.CountGamesLost++; temporaryMind.CountGamesLost++; },
				actionGameOver1);
			
			Comment actionGameOverB1 = new Comment(new Line[] {
				new Line("Not that I keep track or something", null, 0.35f),
				new Line("Not that someone would ask.", null, 0.35f),
				new Line("I don't really care.", null, 0.4f),
				new Line("Good enough.", null, 0.4f),
				new Line("But it's okay."),
				new Line("But I like you anyway.") },
				null,
				null,
				Comment.Pause(0.5f, actionGameOver2));
			Comment actionGameOverB0 = new Comment(new Line[] {
				new Line("And that was game number {0} you lost.", () => (permanentMind.CountGamesLost + 1).ToString()), 
				new Line("You've lost {0}.", () => SayQuantity("game", permanentMind.CountGamesLost + 1)),
				new Line("Damn. GameOver again. Had that {0} now.", l => permanentMind.CountGamesLost > 1, 1.0f, () => SayQuantity("time", permanentMind.CountGamesLost + 1)),
				new Line("Man, you lost a lot of games. {0} to be precise.", l => permanentMind.CountGamesLost > 4, 1.0f, () => (permanentMind.CountGamesLost + 1).ToString()) },
				c => GameController.Instance.GameJustEnded && MathF.Rnd.NextBool(),
				c => { permanentMind.Highscore = MathF.Max(permanentMind.Highscore, GameController.Instance.Score); permanentMind.CountGamesLost++; temporaryMind.CountGamesLost++; },
				Comment.Pause(1.0f, actionGameOverB1));

			supply = new List<Comment>();
			supply.Add(actionHello0);
			supply.Add(actionFirstHello0);
			supply.Add(actionBlockFellOver0);
			supply.Add(actionBlockFellOver1);
			supply.Add(actionBlockFellOver2);
			supply.Add(actionLineClear);
			supply.Add(actionGameOver0);
			supply.Add(actionGameOverB0);
			supply.Add(actionNothingFellOver0);
			supply.Add(actionNothingFellOverB0);

			supply = supply.Shuffle().ToList();
		}
		public static void Init(TextRenderer targetText)
		{
			CommentGuy.targetText = targetText;
			CommentGuy.schedule = new List<Comment>();

			// Load permanent data, if existing
			if (permanentMind == null)
			{
				string mindPath = Path.Combine(Path.GetDirectoryName(DualityApp.UserDataPath), "mind.dat");
				if (File.Exists(mindPath))
				{
					try
					{
						using (FileStream stream = File.OpenRead(mindPath))
						{
							using (Formatter formatter = Formatter.Create(stream))
							{
								permanentMind = formatter.ReadObject() as PermanentData;
							}
						}
					}
					catch (Exception e)
					{
						Log.Game.WriteError("Error loading permanent data: {0}", Log.Exception(e));
					}
				}
			}

			// Create new permanent data, if not
			if (permanentMind == null) permanentMind = new PermanentData();
			if (temporaryMind == null) temporaryMind = new TemporaryData();
			
			// Only do once what is behind this line.
			if (initialized) return;
			initialized = true;

			DualityApp.Terminating += (s, e) => Terminate();
			temporaryMind.TimeSinceLastMet = DateTime.Now - permanentMind.TimeShutdown;
		}
		public static void Terminate()
		{
			// Set shutdown time
			permanentMind.TimeShutdown = DateTime.Now;

			// Save permanent data
			string mindPath = Path.Combine(Path.GetDirectoryName(DualityApp.UserDataPath), "mind.dat");
			if (permanentMind != null)
			{
				try
				{
					string mindDir = Path.GetDirectoryName(mindPath);
					if (!Directory.Exists(mindDir)) Directory.CreateDirectory(mindDir);
					using (FileStream stream = File.Open(mindPath, FileMode.Create)) {
					using (Formatter formatter = Formatter.Create(stream))
					{
						formatter.WriteObject(permanentMind);
					}}
				}
				catch (Exception e)
				{
					Log.Game.WriteError("Error saving permanent data: {0}", Log.Exception(e));
				}
			}
		}
		public static void Update()
		{
			if (!schedule.Any()) schedule.Add(SelectNewComment());

			Comment curAction = schedule.FirstOrDefault();
			if (curAction != null)
			{
				if (curAction.Perform())
				{
					Comment next = curAction.NextAction;
					schedule.RemoveAt(0);
					if (next != null)
					{
						schedule.Insert(0, next);
						next.Reset();
					}
					curAction.Reset();
				}
			}
			else if (schedule.Count > 0)
				schedule.RemoveAt(0);
		}
		public static void OnBeginGameRound()
		{
			temporaryMind.TimeNextBored = Time.GameTimer + (GameController.Instance.FirstGameInSession ? 40000.0f : 30000.0f);
		}

		private static Comment SelectNewComment()
		{
			Comment newAction = supply.FirstOrDefault(a => a.CanPerform() && !schedule.Contains(a));
			if (newAction != null)
			{
				if (MathF.Rnd.Next(100) < 5)
				{
					supply = supply.Shuffle().ToList();
				}
				else
				{
					supply.Remove(newAction);
					supply.Add(newAction);
				}
				newAction.Reset();
			}
			return newAction;
		}
		private static string SayTimeLastMet()
		{
			return SayTimeSpan(temporaryMind.TimeSinceLastMet);
		}
		private static string SayTimeSpan(TimeSpan span)
		{
			if (span.TotalDays >= 365) return SayQuantity("year", MathF.RoundToInt((float)span.TotalDays) / 365);
			if (span.TotalDays >= 30) return SayQuantity("month", MathF.RoundToInt((float)span.TotalDays) / 30);
			if (span.TotalDays >= 7) return SayQuantity("week", MathF.RoundToInt((float)span.TotalDays) / 7);
			if (span.TotalDays >= 1) return SayQuantity("day", MathF.RoundToInt((float)span.TotalDays));
			if (span.TotalHours >= 1) return SayQuantity("hour", MathF.RoundToInt((float)span.TotalHours));
			if (span.TotalMinutes >= 1) return SayQuantity("minute", MathF.RoundToInt((float)span.TotalMinutes));
			if (span.TotalSeconds >= 1) return SayQuantity("second", MathF.RoundToInt((float)span.TotalSeconds));
			return MathF.Rnd.OneOf("the blink of an eye", "the fraction of a second");
		}
		private static string SayQuantity(string subject, int count)
		{
			if (count == 1) return string.Format("{0} {1}", SayQuantity(count), subject);
			return string.Format("{0} {1}s", SayQuantity(count), subject);
		}
		private static string SayQuantity(int count)
		{
			if (count > 2 && count < 10 && MathF.Rnd.NextBool()) return MathF.Rnd.OneOf("some", "a couple of");
			if (count > 12 && MathF.Rnd.NextBool()) return MathF.Rnd.OneOf("quite some", "a lot of");
			if (count == 0) return "zero";
			if (count == 1) return MathF.Rnd.OneOf("one", "a");
			if (count == 2) return "two";
			if (count == 3) return "three";
			if (count == 4) return "four";
			if (count == 5) return "five";
			if (count == 6) return "six";
			if (count == 7) return "seven";
			if (count == 8) return "eight";
			if (count == 9) return "nine";
			if (count == 10) return "ten";
			if (count == 11) return "eleven";
			if (count == 12) return "twelve";
			return count.ToString();
		}
	}

}
