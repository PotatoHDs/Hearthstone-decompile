namespace bgs
{
	public class BattleNetLogSource
	{
		private string m_sourceName;

		public BattleNetLogSource(string sourceName)
		{
			m_sourceName = sourceName;
		}

		public void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0)
		{
			LogAdapter.LogDebugStackTrace(message, maxFrames, skipFrames, m_sourceName);
		}

		public void Log(LogLevel level, string message)
		{
			LogAdapter.Log(level, message, m_sourceName);
		}

		public void Log(LogLevel level, string format, params object[] args)
		{
			string str = string.Format(format, args);
			LogAdapter.Log(level, str, m_sourceName);
		}

		public void LogDebug(string message)
		{
			LogAdapter.LogDebug(message, m_sourceName);
		}

		public void LogDebug(string format, params object[] args)
		{
			LogAdapter.LogDebug(string.Format(format, args), m_sourceName);
		}

		public void LogInfo(string message)
		{
			LogAdapter.LogInfo(message, m_sourceName);
		}

		public void LogInfo(string format, params object[] args)
		{
			LogAdapter.LogInfo(string.Format(format, args), m_sourceName);
		}

		public void LogWarning(string message)
		{
			LogAdapter.LogWarning(message, m_sourceName);
		}

		public void LogWarning(string format, params object[] args)
		{
			LogAdapter.LogWarning(string.Format(format, args), m_sourceName);
		}

		public void LogError(string message)
		{
			LogAdapter.LogError(message, m_sourceName);
		}

		public void LogError(string format, params object[] args)
		{
			LogAdapter.LogError(string.Format(format, args), m_sourceName);
		}

		public void LogException(string format, params object[] args)
		{
			LogAdapter.LogException(string.Format(format, args), m_sourceName);
		}
	}
}
