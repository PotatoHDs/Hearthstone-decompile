using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hearthstone.UI.Logging.Internal
{
	public class EditorLog : ILog
	{
		private class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
		{
			public int Compare(TKey x, TKey y)
			{
				if (x.CompareTo(y) != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		private const int MESSAGE_TRIMMING_THRESHOLD = 100000;

		private const int MESSAGE_TRIMMING_AMOUNT = 1000;

		private SortedList<DateTime, LogMessage> m_logMessages = new SortedList<DateTime, LogMessage>(new DuplicateKeyComparer<DateTime>());

		public event Action<LogMessage> OnMessage;

		public void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "")
		{
			AddMessage(new LogMessage(message, context, logLevel, GetContextPath(context), type));
		}

		public void AddMessage(LogMessage logMessage)
		{
			m_logMessages.Add(logMessage.Timestamp, logMessage);
			if (m_logMessages.Count >= 100000)
			{
				for (int i = 0; i < 1000; i++)
				{
					m_logMessages.RemoveAt(0);
				}
			}
			if (this.OnMessage != null)
			{
				this.OnMessage(logMessage);
			}
		}

		public void Clear()
		{
			m_logMessages.Clear();
		}

		public IList<LogMessage> GetMessages()
		{
			return m_logMessages.Values;
		}

		public IList<LogMessage> GetMessages(Predicate<LogMessage> predicate)
		{
			return m_logMessages.Values.Where((LogMessage a) => predicate(a)).ToList();
		}

		private static string GetContextPath(object context)
		{
			IDebugContext debugContext = context as IDebugContext;
			if (debugContext == null)
			{
				if (context == null)
				{
					return "";
				}
				return context.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (debugContext != null)
			{
				if (!string.IsNullOrEmpty(debugContext.DebugPath))
				{
					stringBuilder.Insert(0, debugContext.DebugPath + ((stringBuilder.Length == 0) ? "" : "/"));
				}
				debugContext = debugContext.ParentContext;
			}
			return stringBuilder.ToString();
		}
	}
}
