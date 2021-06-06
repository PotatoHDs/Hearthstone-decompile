using System;

namespace Hearthstone.UI.Logging
{
	// Token: 0x0200105C RID: 4188
	public class LogMessage
	{
		// Token: 0x0600B538 RID: 46392 RVA: 0x0037BA88 File Offset: 0x00379C88
		public LogMessage(string message, object context, LogLevel level = LogLevel.Info, string path = "", string type = "")
		{
			this.Type = type;
			this.Message = message;
			this.Context = context;
			this.Level = level;
			this.Path = path;
		}

		// Token: 0x0600B539 RID: 46393 RVA: 0x0037BAF4 File Offset: 0x00379CF4
		public LogMessage(string message, object context, DateTime timestamp, LogLevel level = LogLevel.Info, string path = "", string type = "")
		{
			this.Type = type;
			this.Message = message;
			this.Context = context;
			this.Timestamp = timestamp;
			this.Level = level;
			this.Path = path;
		}

		// Token: 0x0400972C RID: 38700
		public readonly string Type = "";

		// Token: 0x0400972D RID: 38701
		public readonly DateTime Timestamp = DateTime.Now;

		// Token: 0x0400972E RID: 38702
		public readonly object Context;

		// Token: 0x0400972F RID: 38703
		public readonly string Path = string.Empty;

		// Token: 0x04009730 RID: 38704
		public readonly string Message = string.Empty;

		// Token: 0x04009731 RID: 38705
		public readonly LogLevel Level = LogLevel.Info;
	}
}
