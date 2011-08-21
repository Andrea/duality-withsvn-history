using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality
{
	/// <summary>
	/// A <see cref="ILogOutput">Log output</see> that uses a <see cref="System.IO.TextWriter"/> as message destination.
	/// </summary>
	public class TextWriterLogOutput : ILogOutput
	{
		private	string			prefix	= null;
		private	TextWriter		writer	= null;
		private	LogOutputFormat	format	= null;

		public TextWriterLogOutput(TextWriter writer, string prefix = null, LogOutputFormat formatHolder = null)
		{
			if (formatHolder == null) formatHolder = new LogOutputFormat();

			this.writer = writer;
			this.prefix = prefix;
			this.format = formatHolder;
		}
		
		/// <summary>
		/// Increases the LogOutputs indent value.
		/// </summary>
		public void PushIndent()
		{
			this.format.Indent++;
		}
		/// <summary>
		/// Decreases the LogOutputs indent value.
		/// </summary>
		public void PopIndent()
		{
			this.format.Indent--;
		}
		
		/// <summary>
		/// Writes a single message to the output.
		/// </summary>
		/// <param name="type">The type of the log message.</param>
		/// <param name="msg">The message to write.</param>
		public virtual void Write(LogMessageType type, string msg)
		{
			string[] lines = msg.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < lines.Length; i++)
			{
				if (i == 0)
				{
					switch (type)
					{
						case LogMessageType.Message:
							lines[i] = this.prefix + "Info:    " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
						case LogMessageType.Warning:
							lines[i] = this.prefix + "Warning: " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
						case LogMessageType.Error:
							lines[i] = this.prefix + "ERROR:   " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
					}
				}
				else
				{
					lines[i] = this.prefix + "         " + new string(' ', this.format.Indent * 4) + lines[i];
				}
				this.writer.WriteLine(lines[i]);
			}
		}
	}
}
