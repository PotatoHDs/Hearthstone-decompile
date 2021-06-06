using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hearthstone.UI.Logging.Internal
{
	// Token: 0x0200105D RID: 4189
	public class EditorLog : ILog
	{
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x0600B53A RID: 46394 RVA: 0x0037BB68 File Offset: 0x00379D68
		// (remove) Token: 0x0600B53B RID: 46395 RVA: 0x0037BBA0 File Offset: 0x00379DA0
		public event Action<LogMessage> OnMessage;

		// Token: 0x0600B53C RID: 46396 RVA: 0x0037BBD5 File Offset: 0x00379DD5
		public void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "")
		{
			this.AddMessage(new LogMessage(message, context, logLevel, EditorLog.GetContextPath(context), type));
		}

		// Token: 0x0600B53D RID: 46397 RVA: 0x0037BBF0 File Offset: 0x00379DF0
		public void AddMessage(LogMessage logMessage)
		{
			this.m_logMessages.Add(logMessage.Timestamp, logMessage);
			if (this.m_logMessages.Count >= 100000)
			{
				for (int i = 0; i < 1000; i++)
				{
					this.m_logMessages.RemoveAt(0);
				}
			}
			if (this.OnMessage != null)
			{
				this.OnMessage(logMessage);
			}
		}

		// Token: 0x0600B53E RID: 46398 RVA: 0x0037BC51 File Offset: 0x00379E51
		public void Clear()
		{
			this.m_logMessages.Clear();
		}

		// Token: 0x0600B53F RID: 46399 RVA: 0x0037BC5E File Offset: 0x00379E5E
		public IList<LogMessage> GetMessages()
		{
			return this.m_logMessages.Values;
		}

		// Token: 0x0600B540 RID: 46400 RVA: 0x0037BC6C File Offset: 0x00379E6C
		public IList<LogMessage> GetMessages(Predicate<LogMessage> predicate)
		{
			return (from a in this.m_logMessages.Values
			where predicate(a)
			select a).ToList<LogMessage>();
		}

		// Token: 0x0600B541 RID: 46401 RVA: 0x0037BCA8 File Offset: 0x00379EA8
		private static string GetContextPath(object context)
		{
			IDebugContext debugContext = context as IDebugContext;
			if (debugContext != null)
			{
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
			if (context == null)
			{
				return "";
			}
			return context.ToString();
		}

		// Token: 0x04009732 RID: 38706
		private const int MESSAGE_TRIMMING_THRESHOLD = 100000;

		// Token: 0x04009733 RID: 38707
		private const int MESSAGE_TRIMMING_AMOUNT = 1000;

		// Token: 0x04009735 RID: 38709
		private SortedList<DateTime, LogMessage> m_logMessages = new SortedList<DateTime, LogMessage>(new EditorLog.DuplicateKeyComparer<DateTime>());

		// Token: 0x02002869 RID: 10345
		private class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
		{
			// Token: 0x06013BC5 RID: 80837 RVA: 0x0053BC16 File Offset: 0x00539E16
			public int Compare(TKey x, TKey y)
			{
				if (x.CompareTo(y) != 0)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
