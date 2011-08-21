using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duality
{
	/// <summary>
	/// A <see cref="ILogOutput">Log output</see> that uses the <see cref="System.Console"/> as message destination.
	/// </summary>
	public class ConsoleLogOutput : TextWriterLogOutput, ILogOutput
	{
		private	ConsoleColor	bgColor;

		public ConsoleLogOutput(string prefix = null, ConsoleColor bgColor = ConsoleColor.Black, LogOutputFormat formatHolder = null)
			: base(Console.Out, prefix, formatHolder)
		{
			this.bgColor = bgColor;
		}
		
		/// <summary>
		/// Writes a single message to the output.
		/// </summary>
		/// <param name="type">The type of the log message.</param>
		/// <param name="msg">The message to write.</param>
		public override void Write(LogMessageType type, string msg)
		{
			ConsoleColor clrBg = Console.BackgroundColor;
			ConsoleColor clrFg = Console.ForegroundColor;

			Console.BackgroundColor = this.bgColor;
			if (type == LogMessageType.Warning)		Console.ForegroundColor = ConsoleColor.Yellow;
			else if (type == LogMessageType.Error)	Console.ForegroundColor = ConsoleColor.Red;
			else									Console.ForegroundColor = ConsoleColor.Gray;

			base.Write(type, msg);

			Console.ForegroundColor = clrFg;
			Console.BackgroundColor = clrBg;
		}
	}
}
