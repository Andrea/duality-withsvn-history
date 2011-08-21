using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality
{
	/// <summary>
	/// Listens for log entries and writes them to registered <see cref="ILogOutput">ILogOutputs</see>.
	/// </summary>
	public class Log
	{
		private	static	Log	logGame		= null;
		private	static	Log	logCore		= null;
		private	static	Log	logEditor	= null;

		/// <summary>
		/// [GET] A log for game-related entries. Use this for logging data from game plugins.
		/// </summary>
		public static Log Game
		{
			get { return logGame; }
		}
		/// <summary>
		/// [GET] A log for core-related entries. This is normally only used by Duality itsself.
		/// </summary>
		public static Log Core
		{
			get { return logCore; }
		}
		/// <summary>
		/// [GET] A log for editor-related entries. This is used by the Duality editor and its plugins.
		/// </summary>
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

		/// <summary>
		/// Creates a new Log.
		/// </summary>
		/// <param name="output">It will be initially connected to the specified outputs.</param>
		public Log(params ILogOutput[] output)
		{
			this.strOut = new List<ILogOutput>(output);
		}

		/// <summary>
		/// Registers an output to write log entries to.
		/// </summary>
		/// <param name="writer"></param>
		public void RegisterOutput(ILogOutput writer)
		{
			this.strOut.Add(writer);
		}
		/// <summary>
		/// Unregisters a registered output.
		/// </summary>
		/// <param name="writer"></param>
		public void UnregisterOutput(ILogOutput writer)
		{
			this.strOut.Remove(writer);
		}

		/// <summary>
		/// Resets the Logs timing and "written once" counters. Outputs will remain attached.
		/// </summary>
		public void Reset()
		{
			this.onceWritten.Clear();
			this.timedLast.Clear();
		}

		/// <summary>
		/// Increases the current log entry indent.
		/// </summary>
		public void PushIndent()
		{
			foreach (ILogOutput log in this.strOut)
			{
				log.PushIndent();
			}
		}
		/// <summary>
		/// Decreases the current log entry indent.
		/// </summary>
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

		/// <summary>
		/// Writes a new log entry.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void Write(string format, params object[] obj)
		{
			this.Write(LogMessageType.Message, String.Format(format, obj));
		}
		/// <summary>
		/// Writes a new warning log entry.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteWarning(string format, params object[] obj)
		{
			this.Write(LogMessageType.Warning, String.Format(format, obj));
		}
		/// <summary>
		/// Writes a new error log entry.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteError(string format, params object[] obj)
		{
			this.Write(LogMessageType.Error, String.Format(format, obj));
		}

		/// <summary>
		/// Writes a new log entry once. If the same message is attempted to be written again, it will be ignored.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Message, t);
			}
		}
		/// <summary>
		/// Writes a warning new log entry once. If the same message is attempted to be written again, it will be ignored.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteWarningOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Warning, t);
			}
		}
		/// <summary>
		/// Writes a error new log entry once. If the same message is attempted to be written again, it will be ignored.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteErrorOnce(string format, params object[] obj)
		{
			string t = String.Format(format, obj);
			if (!this.onceWritten.Contains(t))
			{
				this.onceWritten.Add(t);
				this.Write(LogMessageType.Error, t);
			}
		}

		/// <summary>
		/// Writes a new log entry, but a maximum of one entry per fixed amount of time.
		/// </summary>
		/// <param name="delayMs">The time span in which only one message is written.</param>
		/// <param name="timerId">The id of the timer that is used for measuring the time span. Simply use any string id that seems to fit.</param>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Message, String.Format(format, obj));
			}
		}
		/// <summary>
		/// Writes a new warning log entry, but a maximum of one entry per fixed amount of time.
		/// </summary>
		/// <param name="delayMs">The time span in which only one message is written.</param>
		/// <param name="timerId">The id of the timer that is used for measuring the time span. Simply use any string id that seems to fit.</param>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteWarningTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Warning, String.Format(format, obj));
			}
		}
		/// <summary>
		/// Writes a new error log entry, but a maximum of one entry per fixed amount of time.
		/// </summary>
		/// <param name="delayMs">The time span in which only one message is written.</param>
		/// <param name="timerId">The id of the timer that is used for measuring the time span. Simply use any string id that seems to fit.</param>
		/// <param name="format"></param>
		/// <param name="obj"></param>
		public void WriteErrorTimed(int delayMs, string timerId, string format, params object[] obj)
		{
			float last;
			if (!this.timedLast.TryGetValue(timerId, out last) || Time.MainTimer - last > delayMs)
			{
				this.timedLast[timerId] = Time.MainTimer;
				this.Write(LogMessageType.Error, String.Format(format, obj));
			}
		}

		/// <summary>
		/// Retrieves the current stack frame.
		/// </summary>
		/// <param name="skipFrames">The number of frames to skip. This function itsself is omitted by default.</param>
		/// <returns>The caller's stack frame.</returns>
		public static System.Diagnostics.StackFrame CurrentStackFrame(int skipFrames = 0)
		{
			return new System.Diagnostics.StackTrace(skipFrames + 1).GetFrame(0);
		}
		/// <summary>
		/// Returns the name of the caller method.
		/// </summary>
		/// <param name="skipFrames">The number of frames to skip. This function itsself is omitted by default.</param>
		/// <param name="includeDeclaringType">If true, the methods declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string CurrentMethod(int skipFrames = 0, bool includeDeclaringType = true)
		{
			return MethodInfo(CurrentStackFrame(skipFrames + 1).GetMethod(), includeDeclaringType);
		}
		/// <summary>
		/// Returns the name of the caller methods declaring type.
		/// </summary>
		/// <param name="skipFrames">The number of frames to skip. This function itsself is omitted by default.</param>
		/// <returns></returns>
		public static string CurrentType(int skipFrames = 0)
		{
			return Type(CurrentStackFrame(skipFrames + 1).GetMethod().DeclaringType);
		}

		/// <summary>
		/// Returns a string that can be used for representing a <see cref="System.Type"/> in log entries.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string Type(Type type)
		{
			return type.GetTypeName(TypeNameFormat.CSCodeIdentShort);
		}
		/// <summary>
		/// Returns a string that can be used for representing a method in log entries.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="includeDeclaringType">If true, the methods declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string MethodInfo(MethodBase info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string[] paramNames = info.GetParameters().Select(p => Type(p.ParameterType)).ToArray();
			return string.Format("{0}{1}({2})",
				includeDeclaringType ? declTypeName + "." : "",
				info.Name,
				paramNames.ToString(", "));
		}
		/// <summary>
		/// Returns a string that can be used for representing a property in log entries.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="includeDeclaringType">If true, the properties declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string PropertyInfo(PropertyInfo info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string propTypeName = Type(info.PropertyType);
			string[] paramNames = info.GetIndexParameters().Select(p => Type(p.ParameterType)).ToArray();
			return string.Format("{0} {1}{2}{3}",
				propTypeName,
				includeDeclaringType ? declTypeName + "." : "",
				info.Name,
				paramNames.Any() ? "[" + paramNames.ToString(", ") + "]" : "");
		}
		/// <summary>
		/// Returns a string that can be used for representing a field in log entries.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="includeDeclaringType">If true, the fields declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string FieldInfo(FieldInfo info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string fieldTypeName = Type(info.FieldType);
			return string.Format("{0} {1}{2}",
				fieldTypeName,
				includeDeclaringType ? declTypeName + "." : "",
				info.Name);
		}
		/// <summary>
		/// Returns a string that can be used for representing an event in log entries.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="includeDeclaringType">If true, the events declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string EventInfo(EventInfo info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string fieldTypeName = Type(info.EventHandlerType);
			return string.Format("{0} {1}{2}",
				fieldTypeName,
				includeDeclaringType ? declTypeName + "." : "",
				info.Name);
		}
		/// <summary>
		/// Returns a string that can be used for representing a(ny) member in log entries.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="includeDeclaringType">If true, the members declaring type is included in the returned name.</param>
		/// <returns></returns>
		public static string MemberInfo(MemberInfo info, bool includeDeclaringType = true)
		{
			if (info is MethodBase)
				return MethodInfo(info as MethodBase, includeDeclaringType);
			else if (info is PropertyInfo)
				return PropertyInfo(info as PropertyInfo, includeDeclaringType);
			else if (info is FieldInfo)
				return FieldInfo(info as FieldInfo, includeDeclaringType);
			else if (info is EventInfo)
				return EventInfo(info as EventInfo, includeDeclaringType);
			else if (info is Type)
				return Type(info as Type);
			else
				return info.ToString();
		}
		
		/// <summary>
		/// Returns a string that can be used for representing an exception in log entries.
		/// It usually does not include the full call stack and is significantly shorter than
		/// an <see cref="System.Exception">Exceptions</see> ToString method.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static string Exception(Exception e)
		{
			if (e == null) return null;

			string eName = Type(e.GetType());
			string eSite = e.TargetSite != null ? MemberInfo(e.TargetSite) : null;

			return string.Format("{0}{1}: {2}",
				eName,
				eSite != null ? " at " + eSite : "",
				e.Message);
		}
	}

	/// <summary>
	/// The type of a log message / entry.
	/// </summary>
	public enum LogMessageType
	{
		/// <summary>
		/// Just a regular message. Nothing special. Neutrally informs about what's going on.
		/// </summary>
		Message,
		/// <summary>
		/// A warning message. It informs about unexpected data or behaviour that might not have caused any errors yet, but can lead to them.
		/// It might also be used for expected errors from which Duality is likely to recover.
		/// </summary>
		Warning,
		/// <summary>
		/// An error message. It informs about an unexpected and/or critical error that has occured.
		/// </summary>
		Error
	}

	/// <summary>
	/// Represents a single <see cref="Log"/> output and provides actual writing functionality for 
	/// </summary>
	public interface ILogOutput
	{
		/// <summary>
		/// Writes a single message to the output.
		/// </summary>
		/// <param name="type">The type of the log message.</param>
		/// <param name="msg">The message to write.</param>
		void Write(LogMessageType type, string msg);
		/// <summary>
		/// Increases the LogOutputs indent value.
		/// </summary>
		void PushIndent();
		/// <summary>
		/// Decreases the LogOutputs indent value.
		/// </summary>
		void PopIndent();
	}

	/// <summary>
	/// Holds log output format data that may be shared among different <see cref="ILogOutput">ILogOutputs</see> such as the
	/// current log indent in different outputs writing to the same file.
	/// </summary>
	/// <example>
	/// The internal console log output is initialized like this:
	/// <code>
	/// 	LogOutputFormat consoleSharedFormat = new LogOutputFormat();
	/// 	logOutGame		= new ConsoleLogOutput("[Game]   ", ConsoleColor.DarkGray, consoleSharedFormat);
	/// 	logOutCore		= new ConsoleLogOutput("[Core]   ", ConsoleColor.DarkBlue, consoleSharedFormat);
	/// 	logOutEditor	= new ConsoleLogOutput("[Editor] ", ConsoleColor.DarkMagenta, consoleSharedFormat);
	/// </code>
	/// Since we want all three LogOutputs to share the same <see cref="LogOutputFormat.Indent"/> value even though they
	/// are attached to different <see cref="Log">Logs</see>, a single LogOutputFormat is shared among all three outputs.
	/// A similar setup is used for the logfile outputs.
	/// </example>
	public class LogOutputFormat
	{
		private	int	indent	= 0;

		/// <summary>
		/// [GET / SET] The current indent value of log message entries.
		/// </summary>
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
