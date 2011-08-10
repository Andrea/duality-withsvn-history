using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

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

		public static System.Diagnostics.StackFrame CurrentStackFrame(int skipFrames = 0)
		{
			return new System.Diagnostics.StackTrace(skipFrames + 1).GetFrame(0);
		}
		public static string CurrentMethod(int skipFrames = 0, bool includeDeclaringType = true)
		{
			return MethodInfo(CurrentStackFrame(skipFrames + 1).GetMethod(), includeDeclaringType);
		}
		public static string CurrentType(int skipFrames = 0)
		{
			return Type(CurrentStackFrame(skipFrames + 1).GetMethod().DeclaringType);
		}

		public static string Type(Type type)
		{
			return type.GetTypeName(TypeNameFormat.CSCodeIdentShort);
		}
		public static string MethodInfo(MethodBase info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string[] paramNames = info.GetParameters().Select(p => Type(p.ParameterType)).ToArray();
			return string.Format("{0}{1}({2})",
				includeDeclaringType ? declTypeName + "." : "",
				info.Name,
				paramNames.ToString(", "));
		}
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
		public static string FieldInfo(FieldInfo info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string fieldTypeName = Type(info.FieldType);
			return string.Format("{0} {1}{2}",
				fieldTypeName,
				includeDeclaringType ? declTypeName + "." : "",
				info.Name);
		}
		public static string EventInfo(EventInfo info, bool includeDeclaringType = true)
		{
			string declTypeName = Type(info.DeclaringType);
			string fieldTypeName = Type(info.EventHandlerType);
			return string.Format("{0} {1}{2}",
				fieldTypeName,
				includeDeclaringType ? declTypeName + "." : "",
				info.Name);
		}
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
