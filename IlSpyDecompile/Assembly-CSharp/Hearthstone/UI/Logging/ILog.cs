using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Logging
{
	public interface ILog
	{
		event Action<LogMessage> OnMessage;

		void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "");

		void AddMessage(LogMessage logMessage);

		void Clear();

		IList<LogMessage> GetMessages();

		IList<LogMessage> GetMessages(Predicate<LogMessage> predicate);
	}
}
