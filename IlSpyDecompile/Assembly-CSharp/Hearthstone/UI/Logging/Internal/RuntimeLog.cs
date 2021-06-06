using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Logging.Internal
{
	public class RuntimeLog : ILog
	{
		private IList<LogMessage> m_emptyList = new List<LogMessage>();

		public event Action<LogMessage> OnMessage;

		public void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "")
		{
		}

		public void AddMessage(LogMessage logMessage)
		{
		}

		public void Clear()
		{
		}

		public IList<LogMessage> GetMessages()
		{
			return m_emptyList;
		}

		public IList<LogMessage> GetMessages(Predicate<LogMessage> predicate)
		{
			return m_emptyList;
		}
	}
}
