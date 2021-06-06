using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Logging
{
	// Token: 0x02001059 RID: 4185
	public interface ILog
	{
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600B530 RID: 46384
		// (remove) Token: 0x0600B531 RID: 46385
		event Action<LogMessage> OnMessage;

		// Token: 0x0600B532 RID: 46386
		void AddMessage(string message, object context, LogLevel logLevel = LogLevel.Info, string type = "");

		// Token: 0x0600B533 RID: 46387
		void AddMessage(LogMessage logMessage);

		// Token: 0x0600B534 RID: 46388
		void Clear();

		// Token: 0x0600B535 RID: 46389
		IList<LogMessage> GetMessages();

		// Token: 0x0600B536 RID: 46390
		IList<LogMessage> GetMessages(Predicate<LogMessage> predicate);
	}
}
