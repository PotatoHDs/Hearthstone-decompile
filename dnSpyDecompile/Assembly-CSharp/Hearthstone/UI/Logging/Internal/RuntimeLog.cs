using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Logging.Internal
{
	// Token: 0x0200105E RID: 4190
	public class RuntimeLog : ILog
	{
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x0600B543 RID: 46403 RVA: 0x0037BD34 File Offset: 0x00379F34
		// (remove) Token: 0x0600B544 RID: 46404 RVA: 0x0037BD6C File Offset: 0x00379F6C
		public event Action<LogMessage> OnMessage;

		// Token: 0x0600B545 RID: 46405 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "")
		{
		}

		// Token: 0x0600B546 RID: 46406 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void AddMessage(LogMessage logMessage)
		{
		}

		// Token: 0x0600B547 RID: 46407 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Clear()
		{
		}

		// Token: 0x0600B548 RID: 46408 RVA: 0x0037BDA1 File Offset: 0x00379FA1
		public IList<LogMessage> GetMessages()
		{
			return this.m_emptyList;
		}

		// Token: 0x0600B549 RID: 46409 RVA: 0x0037BDA1 File Offset: 0x00379FA1
		public IList<LogMessage> GetMessages(Predicate<LogMessage> predicate)
		{
			return this.m_emptyList;
		}

		// Token: 0x04009736 RID: 38710
		private IList<LogMessage> m_emptyList = new List<LogMessage>();
	}
}
