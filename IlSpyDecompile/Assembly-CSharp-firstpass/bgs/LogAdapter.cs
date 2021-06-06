using bgs.Wrapper.Impl;

namespace bgs
{
	public class LogAdapter
	{
		private static LoggerInterface s_impl = new DefaultLogger();

		public static void SetLogger(LoggerInterface outputter)
		{
			s_impl = outputter;
		}

		public static void Log(LogLevel logLevel, string str, string sourceName = "")
		{
			s_impl.Log(logLevel, str, sourceName);
		}

		public static void LogDebug(string message, string sourceName = "")
		{
			s_impl.LogDebug(message, sourceName);
		}

		public static void LogInfo(string message, string sourceName = "")
		{
			s_impl.LogInfo(message, sourceName);
		}

		public static void LogWarning(string message, string sourceName = "")
		{
			s_impl.LogWarning(message, sourceName);
		}

		public static void LogError(string message, string sourceName = "")
		{
			s_impl.LogError(message, sourceName);
		}

		public static void LogException(string message, string sourceName = "")
		{
			s_impl.LogException(message, sourceName);
		}

		public static void LogFatal(string message, string sourceName = "")
		{
			s_impl.LogFatal(message, sourceName);
		}

		public static void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "")
		{
			s_impl.LogDebugStackTrace(message, maxFrames, skipFrames, logSource);
		}
	}
}
