using System;

namespace bgs
{
	// Token: 0x02000216 RID: 534
	public class BattleNetLogSource
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x00078DC4 File Offset: 0x00076FC4
		public BattleNetLogSource(string sourceName)
		{
			this.m_sourceName = sourceName;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00078DD3 File Offset: 0x00076FD3
		public void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0)
		{
			LogAdapter.LogDebugStackTrace(message, maxFrames, skipFrames, this.m_sourceName);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00078DE3 File Offset: 0x00076FE3
		public void Log(LogLevel level, string message)
		{
			LogAdapter.Log(level, message, this.m_sourceName);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00078DF4 File Offset: 0x00076FF4
		public void Log(LogLevel level, string format, params object[] args)
		{
			string str = string.Format(format, args);
			LogAdapter.Log(level, str, this.m_sourceName);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00078E16 File Offset: 0x00077016
		public void LogDebug(string message)
		{
			LogAdapter.LogDebug(message, this.m_sourceName);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00078E24 File Offset: 0x00077024
		public void LogDebug(string format, params object[] args)
		{
			LogAdapter.LogDebug(string.Format(format, args), this.m_sourceName);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00078E38 File Offset: 0x00077038
		public void LogInfo(string message)
		{
			LogAdapter.LogInfo(message, this.m_sourceName);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00078E46 File Offset: 0x00077046
		public void LogInfo(string format, params object[] args)
		{
			LogAdapter.LogInfo(string.Format(format, args), this.m_sourceName);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00078E5A File Offset: 0x0007705A
		public void LogWarning(string message)
		{
			LogAdapter.LogWarning(message, this.m_sourceName);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00078E68 File Offset: 0x00077068
		public void LogWarning(string format, params object[] args)
		{
			LogAdapter.LogWarning(string.Format(format, args), this.m_sourceName);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00078E7C File Offset: 0x0007707C
		public void LogError(string message)
		{
			LogAdapter.LogError(message, this.m_sourceName);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00078E8A File Offset: 0x0007708A
		public void LogError(string format, params object[] args)
		{
			LogAdapter.LogError(string.Format(format, args), this.m_sourceName);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00078E9E File Offset: 0x0007709E
		public void LogException(string format, params object[] args)
		{
			LogAdapter.LogException(string.Format(format, args), this.m_sourceName);
		}

		// Token: 0x04000E45 RID: 3653
		private string m_sourceName;
	}
}
