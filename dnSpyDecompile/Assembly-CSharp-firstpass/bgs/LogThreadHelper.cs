using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace bgs
{
	// Token: 0x0200022A RID: 554
	public class LogThreadHelper
	{
		// Token: 0x06002328 RID: 9000 RVA: 0x0007B4BC File Offset: 0x000796BC
		public LogThreadHelper(string name)
		{
			this.m_logSource = new BattleNetLogSource(name);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0007B4DC File Offset: 0x000796DC
		public void Process()
		{
			List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
			lock (queuedLogs)
			{
				foreach (LogThreadHelper.LogEntry logEntry in this.m_queuedLogs)
				{
					switch (logEntry.Level)
					{
					default:
						this.m_logSource.LogDebug(logEntry.Message);
						break;
					case LogLevel.Info:
						this.m_logSource.LogInfo(logEntry.Message);
						break;
					case LogLevel.Warning:
						this.m_logSource.LogWarning(logEntry.Message);
						break;
					case LogLevel.Error:
						this.m_logSource.LogError(logEntry.Message);
						break;
					}
				}
				this.m_queuedLogs.Clear();
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0007B5C8 File Offset: 0x000797C8
		public void LogDebug(string message)
		{
			this.LogMessage(message, LogLevel.Debug);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0007B5D4 File Offset: 0x000797D4
		public void LogDebug(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Debug);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x0007B5F1 File Offset: 0x000797F1
		public void LogInfo(string message)
		{
			this.LogMessage(message, LogLevel.Info);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0007B5FC File Offset: 0x000797FC
		public void LogInfo(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Info);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0007B619 File Offset: 0x00079819
		public void LogWarning(string message)
		{
			this.LogMessage(message, LogLevel.Warning);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0007B624 File Offset: 0x00079824
		public void LogWarning(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Warning);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0007B641 File Offset: 0x00079841
		public void LogError(string message)
		{
			this.LogMessage(message, LogLevel.Error);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0007B64C File Offset: 0x0007984C
		public void LogError(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Error);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0007B66C File Offset: 0x0007986C
		private void LogMessage(string message, LogLevel level)
		{
			if (level >= LogLevel.Warning)
			{
				StackTrace arg = new StackTrace(2, true);
				message = string.Format("{0}\n{1}\n-------------------------------", message, arg);
			}
			List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
			lock (queuedLogs)
			{
				this.m_queuedLogs.Add(new LogThreadHelper.LogEntry
				{
					Message = message,
					Level = level
				});
			}
		}

		// Token: 0x04000E72 RID: 3698
		private BattleNetLogSource m_logSource;

		// Token: 0x04000E73 RID: 3699
		private List<LogThreadHelper.LogEntry> m_queuedLogs = new List<LogThreadHelper.LogEntry>();

		// Token: 0x020006C1 RID: 1729
		private class LogEntry
		{
			// Token: 0x0400221F RID: 8735
			public string Message;

			// Token: 0x04002220 RID: 8736
			public LogLevel Level;
		}
	}
}
