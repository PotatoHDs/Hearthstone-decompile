using System.Diagnostics;
using System.IO;
using System.Text;

namespace bgs
{
	public abstract class LoggerBase : LoggerInterface
	{
		public abstract void Log(LogLevel logLevel, string str, string sourceName = "");

		public abstract void LogDebug(string message, string sourceName = "");

		public abstract void LogInfo(string message, string sourceName = "");

		public abstract void LogWarning(string message, string sourceName = "");

		public abstract void LogError(string message, string sourceName = "");

		public abstract void LogException(string message, string sourceName = "");

		public abstract void LogFatal(string message, string sourceName = "");

		public void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(message + "\n");
			for (int i = 1 + skipFrames; i < maxFrames; i++)
			{
				StackFrame frame = new StackTrace(new StackFrame(i, true)).GetFrame(0);
				if (frame == null || !(frame.GetMethod() != null) || frame.GetMethod().ToString().StartsWith("<"))
				{
					break;
				}
				stringBuilder.Append($"File \"{Path.GetFileName(frame.GetFileName())}\", line {frame.GetFileLineNumber()} -- {frame.GetMethod()}\n");
			}
			Log(LogLevel.Debug, stringBuilder.ToString().TrimEnd(), logSource);
		}

		protected string FormatStackTrace(StackFrame sf, bool fullPath = false)
		{
			if (sf != null)
			{
				string arg = (fullPath ? sf.GetFileName() : Path.GetFileName(sf.GetFileName()));
				return string.Format(" ({2} at {0}:{1})", arg, sf.GetFileLineNumber(), sf.GetMethod());
			}
			return "";
		}
	}
}
