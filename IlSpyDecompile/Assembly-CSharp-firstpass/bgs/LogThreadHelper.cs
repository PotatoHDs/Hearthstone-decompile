using System.Collections.Generic;
using System.Diagnostics;

namespace bgs
{
	public class LogThreadHelper
	{
		private class LogEntry
		{
			public string Message;

			public LogLevel Level;
		}

		private BattleNetLogSource m_logSource;

		private List<LogEntry> m_queuedLogs = new List<LogEntry>();

		public LogThreadHelper(string name)
		{
			m_logSource = new BattleNetLogSource(name);
		}

		public void Process()
		{
			lock (m_queuedLogs)
			{
				foreach (LogEntry queuedLog in m_queuedLogs)
				{
					switch (queuedLog.Level)
					{
					default:
						m_logSource.LogDebug(queuedLog.Message);
						break;
					case LogLevel.Info:
						m_logSource.LogInfo(queuedLog.Message);
						break;
					case LogLevel.Warning:
						m_logSource.LogWarning(queuedLog.Message);
						break;
					case LogLevel.Error:
						m_logSource.LogError(queuedLog.Message);
						break;
					}
				}
				m_queuedLogs.Clear();
			}
		}

		public void LogDebug(string message)
		{
			LogMessage(message, LogLevel.Debug);
		}

		public void LogDebug(string format, params object[] args)
		{
			string message = string.Format(format, args);
			LogMessage(message, LogLevel.Debug);
		}

		public void LogInfo(string message)
		{
			LogMessage(message, LogLevel.Info);
		}

		public void LogInfo(string format, params object[] args)
		{
			string message = string.Format(format, args);
			LogMessage(message, LogLevel.Info);
		}

		public void LogWarning(string message)
		{
			LogMessage(message, LogLevel.Warning);
		}

		public void LogWarning(string format, params object[] args)
		{
			string message = string.Format(format, args);
			LogMessage(message, LogLevel.Warning);
		}

		public void LogError(string message)
		{
			LogMessage(message, LogLevel.Error);
		}

		public void LogError(string format, params object[] args)
		{
			string message = string.Format(format, args);
			LogMessage(message, LogLevel.Error);
		}

		private void LogMessage(string message, LogLevel level)
		{
			if (level >= LogLevel.Warning)
			{
				StackTrace arg = new StackTrace(2, fNeedFileInfo: true);
				message = $"{message}\n{arg}\n-------------------------------";
			}
			lock (m_queuedLogs)
			{
				m_queuedLogs.Add(new LogEntry
				{
					Message = message,
					Level = level
				});
			}
		}
	}
}
