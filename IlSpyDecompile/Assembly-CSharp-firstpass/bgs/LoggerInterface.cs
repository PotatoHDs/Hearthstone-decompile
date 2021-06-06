namespace bgs
{
	public interface LoggerInterface
	{
		void Log(LogLevel logLevel, string str, string sourceName = "");

		void LogDebug(string message, string sourceName = "");

		void LogInfo(string message, string sourceName = "");

		void LogWarning(string message, string sourceName = "");

		void LogError(string message, string sourceName = "");

		void LogException(string message, string sourceName = "");

		void LogFatal(string message, string sourceName = "");

		void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "");
	}
}
