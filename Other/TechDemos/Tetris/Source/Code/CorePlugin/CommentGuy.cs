using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
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
		}
		public class Line
		{
			private	string			text;
			private	float			timeMult;
			private	int				sayCount;
			private	Predicate<Line>	condition;

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
			public bool CanSay()
			{
				return this.condition == null || this.condition(this);
			}
			public string Say()
			{
				this.sayCount++;
				return this.text;
			}

			public override string ToString()
			{
				return this.text;
			}
		}

		private static	TextRenderer	targetText	= null;
		private	static	List<Comment>	schedule	= null;
		private	static	List<Comment>	supply		= null;

		private	static	int				blockFellOverCount		= 0;
		private	static	float			timeLastBlockFellOver	= -10000.0f;

		static CommentGuy()
		{
			Comment actionHello5 = new Comment(new Line[] {
				new Line("hehehe."), 
				new Line("hehe..")},
				null,
				null);
			Comment actionHello4 = new Comment(new Line[] {
				new Line("It's not the classic version."), 
				new Line("This isn't quite the original."),
				new Line("It's not the regular version."),
				new Line("You might not know this one.")},
				null,
				null,
				actionHello5);
			Comment actionHello3 = new Comment(new Line[] {
				new Line("but.."), 
				new Line("however.."),
				new Line("well..")},
				null,
				null,
				actionHello4);
			Comment actionHello2 = new Comment(new Line[] {
				new Line("You probably knew that already."), 
				new Line("Well, you might know that."),
				new Line("You should know it."),
				new Line("Everybody knows it.") ,
				new Line("This probably isn't new to you.")},
				null,
				null,
				actionHello3);
			Comment actionHello1 = new Comment(new Line[] {
				new Line("This game is called Tetris."), 
				new Line("This is Tetris."),
				new Line("What you can see here is called Tetris."),
				new Line("You are playing a game that is called Tetris.") ,
				new Line("This game goes by the name of Tetris.")},
				null,
				null,
				actionHello2);
			Comment actionHello0 = new Comment(new Line[] {
				new Line("Hi."), 
				new Line("Hello."),
				new Line("Hey there."),
				new Line("Hello there.") },
				c => GameController.Instance.TotalPlayTime < 5000.0f && GameController.Instance.FirstGameInSession,
				null,
				actionHello1);
			
			Comment actionBlockFellOver0 = new Comment(new Line[] {
				new Line("Too bad, those blocks can fall over."),
				new Line("Physics isn't fair, huh?"),
				new Line("Oh noes! They can fall over!"),
				new Line("DAMN YOU, PHYSICS"),
				new Line("I guess, Newton sneaked in here somewhere."),
				new Line("Let there be physics."),
				new Line("I bet you didn't expect that would happen.") },
				c => GameController.Instance.BlockJustFellOver && blockFellOverCount == 0,
				c => { blockFellOverCount++; timeLastBlockFellOver = Time.GameTimer; });
			Comment actionBlockFellOver1 = new Comment(new Line[] {
				new Line("Aw, man. Another block fallen over."),
				new Line("FTONK."),
				new Line("Roll, roll, roll those blocks"),
				new Line("Aaaand another one."),
				new Line("Man, that sucks."),
				new Line("I'm almost sorry for you. Almost."),
				new Line("I hate when that happens."),
				new Line("Ba-dum."),
				new Line("What we need is more chaos."),
				new Line("Uh-oh.") },
				c => GameController.Instance.BlockJustFellOver && blockFellOverCount > 0 && Time.GameTimer - timeLastBlockFellOver > 10000.0f,
				c => { blockFellOverCount++; timeLastBlockFellOver = Time.GameTimer; });
			Comment actionBlockFellOver2 = new Comment(new Line[] {
				new Line("Are you TRYING to make them fall over?"),
				new Line("In soviet russia, TETRIS plays YOU."),
				new Line("There is nothing like the sound of a falling block."),
				new Line("You must be the worst player I've ever seen."),
				new Line("You know you're supposed to arrange those blocks, do you?"),
				new Line("Have you even played Tetris before?") },
				c => GameController.Instance.BlockJustFellOver && blockFellOverCount >= 8 && Time.GameTimer - timeLastBlockFellOver > 10000.0f,
				c => { blockFellOverCount++; timeLastBlockFellOver = Time.GameTimer; });

			Comment actionLineClear = new Comment(new Line[] {
				new Line("BAM!"),
				new Line("Booyah!."),
				new Line("Here you go."),
				new Line("Another 1000 points for you."),
				new Line("Nice."),
				new Line("One line less to worry about."),
				new Line("Score!"),
				new Line("Yoink."),
				new Line("Kabóom!"),
				new Line("Baaaaam!") },
				c => GameController.Instance.LineJustCleared && MathF.Rnd.NextBool(),
				null);
			
			Comment actionGameOver5 = new Comment(new Line[] {
				new Line("Press RETURN to try again.", null, 2.0f), 
				new Line("Let me know if you want to try again. (RETURN key)", null, 2.0f),
				new Line("Another try? Press RETURN.", null, 2.0f),
				new Line("Go get yourself some coffee. Then press RETURN if you want to try again.", null, 2.0f) },
				null,
				null);
			Comment actionGameOver4 = new Comment(new Line[] {
				new Line("", null, 2.0f) },
				null,
				null,
				actionGameOver5);
			Comment actionGameOver3a3 = new Comment(new Line[] {
				new Line("Someday.", null, 0.4f), 
				new Line("Maybe.", null, 0.4f),
				new Line("*cough*", null, 0.4f),
				new Line("Sooner or later. Maybe later."),
				new Line("or not", null, 0.4f),
				new Line("More or less.", null, 0.6f) },
				null,
				null,
				actionGameOver4);
			Comment actionGameOver3a2 = new Comment(new Line[] {
				new Line("I'm sure you'll improve.", null, 1.5f), 
				new Line("You can do it.", null, 1.5f),
				new Line("You'll make it.", null, 1.5f),
				new Line("I'm sure it will work out.", null, 1.5f) },
				null,
				null,
				actionGameOver3a3);
			Comment actionGameOver3a = new Comment(new Line[] {
				new Line("") },
				null,
				null,
				actionGameOver3a2);
			Comment actionGameOver3b2 = new Comment(new Line[] {
				new Line("Not that it would change anything.", null, 0.3f), 
				new Line("It won't help.", null, 0.4f),
				new Line("It won't work.", null, 0.4f),
				new Line("What a waste of time.", null, 0.3f),
				new Line("Or not.", null, 0.4f) },
				null,
				null,
				actionGameOver4);
			Comment actionGameOver3b = new Comment(new Line[] {
				new Line("") },
				null,
				null,
				actionGameOver3b2);
			Comment actionGameOver2 = new Comment(new Line[] {
				new Line("Of course, you could try again."), 
				new Line("Well, you might give it another try."),
				new Line("Maybe you should practice a little more."),
				new Line("Do you want to play again?"),
				new Line("Will you try again?"),
				new Line("You could improve your reflexes."),
				new Line("You could try that again. I suppose."),
				new Line("Will you play another round?") },
				null,
				null,
				actionGameOver3a, actionGameOver3b);
			Comment actionGameOver1 = new Comment(new Line[] {
				new Line("It was inevitable, I guess."), 
				new Line("That sucks."),
				new Line("I'm sorry."),
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
				new Line("That's it.") },
				c => GameController.Instance.GameJustEnded,
				null,
				actionGameOver1);

			supply = new List<Comment>();
			supply.Add(actionHello0);
			supply.Add(actionBlockFellOver0);
			supply.Add(actionBlockFellOver1);
			supply.Add(actionBlockFellOver2);
			supply.Add(actionLineClear);
			supply.Add(actionGameOver0);

			supply = supply.Shuffle().ToList();
		}
		public static void Init(TextRenderer targetText)
		{
			CommentGuy.targetText = targetText;
			CommentGuy.schedule = new List<Comment>();
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
					if (next != null) schedule.Insert(0, next);
					curAction.Reset();
				}
			}
			else if (schedule.Count > 0)
				schedule.RemoveAt(0);
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
	}

}
