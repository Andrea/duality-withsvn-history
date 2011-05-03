using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality
{
	public class Log
	{
		private	static	Log	logGame		= null;
		private	static	Log	logCore		= null;
		private	static	Log	logEditor	= null;

		public static Log Game
		{
			get { return logGame; }
		}
		public static Log Core
		{
			get { return logCore; }
		}
		public static Log Editor
		{
			get { return logEditor; }
		}

		static Log()
		{
			LogOutputFormat consoleSharedFormat = new LogOutputFormat();
			logGame		= new Log(new ConsoleLogOutput("[Game]   ", ConsoleColor.DarkGray, consoleSharedFormat));
			logCore		= new Log(new ConsoleLogOutput("[Core]   ", ConsoleColor.DarkBlue, consoleSharedFormat));
			logEditor	= new Log(new ConsoleLogOutput("[Editor] ", ConsoleColor.DarkMagenta, consoleSharedFormat));
		}


		private HashSet<string>				onceWritten = new HashSet<string>();
		private	Dictionary<string,float>	timedLast	= new Dictionary<string,float>();
		private	List<ILogOutput>			strOut		= null;

		public Log(params ILogOutput[] output)
		{
			this.strOut = new List<ILogOutput>(output);
		}

		public void RegisterOutput(ILogOutput writer)
		{
			this.strOut.Add(writer);
		}
		public void UnregisterOutput(ILogOutput writer)
		{
			this.strOut.Remove(writer);
		}

		public void Reset()
		{
			this.onceWritten.Clear();
			this.timedLast.Clear();
		}

		public void PushIndent()
		{
			foreach (ILogOutput log in this.strOut)
			{
				log.PushIndent();
			}
		}
		public void PopIndent()
		{
			foreach (ILogOutput log in this.strOut)
			{
				log.PopIndent();
			}
		}

		private void Write(LogMessageType type, string msg)
		{
			foreach (ILogOutput log in this.strOut)
			{
				log.Write(type, msg);
			}
		}

		public void Write(string format, params object[] obj)
		{
			this.Write(LogMessageType.Message, String.Format(format, obj));
		}
		public void WriteWarning(string format, params object[] obj)
		{
			this.Write(LogMessageType.Warning, String.Format(format, obj));
		}
		public void WriteError(string format, params object[] obj)
		{
			this.Write(LogMessageType.Error, String.Format(format, obj));
		}

		public void WriteOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Message, t);
			}
		}
		public void WriteWarningOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Warning, t);
			}
		}
		public void WriteErrorOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Error, t);
			}
		}

		public void WriteTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Message, String.Format(format, obj));
			}
		}
		public void WriteWarningTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Warning, String.Format(format, obj));
			}
		}
		public void WriteErrorTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Error, String.Format(format, obj));
			}
		}

		public static System.Diagnostics.StackFrame StackFrame(int skipFrames = 0)
		{
			return new System.Diagnostics.StackTrace(skipFrames + 1).GetFrame(0);
		}
		public static string MethodName(int skipFrames = 0)
		{
			return StackFrame(skipFrames + 1).GetMethod().Name;
		}
		public static string MethodAndTypeName(int skipFrames = 0)
		{
			System.Reflection.MethodBase m = StackFrame(skipFrames + 1).GetMethod();
			return m.Name + " from type " + m.DeclaringType.Name;
		}
		public static string TypeName(int skipFrames = 0)
		{
			return StackFrame(skipFrames + 1).GetMethod().DeclaringType.Name;
		}
	}

	public enum LogMessageType
	{
		Message,
		Warning,
		Error
	}

	public interface ILogOutput
	{
		void Write(LogMessageType type, string msg);
		void PushIndent();
		void PopIndent();
	}

	/// <summary>
	/// Holds log output format data that may be shared among different LogOutputs such as the
	/// current log indent in different outputs writing to the same file.
	/// </summary>
	public class LogOutputFormat
	{
		private	int	indent	= 0;

		public int Indent
		{
			get { return this.indent; }
			set { this.indent = value; }
		}

		public LogOutputFormat()
		{

		}
	}
}
