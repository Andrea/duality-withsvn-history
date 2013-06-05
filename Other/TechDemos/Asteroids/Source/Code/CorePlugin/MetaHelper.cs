using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;

namespace GamePlugin
{
	public static class MetaHelper
	{
		public static IEnumerable<HighscoreEntry> QueryHighscore()
		{
			var highscoreQuery = DualityApp.MetaData.ReadSubValues("Asteroids/Highscore");
			foreach (var pair in highscoreQuery)
			{
				string name = pair.Value.Split('\t')[0];
				string score = pair.Value.Split('\t')[1];
				yield return new HighscoreEntry(name, score);
			}
		}
	}

	public struct HighscoreEntry
	{
		public string Name;
		public int Score;
		public string ScoreString;
		
		public HighscoreEntry(string name, int score)
		{
			this.Name = name;
			this.ScoreString = score.ToString();
			this.Score = score;
		}
		public HighscoreEntry(string name, string scoreString)
		{
			this.Name = name;
			this.ScoreString = scoreString;
			this.Score = int.Parse(this.ScoreString);
		}
	}
}
