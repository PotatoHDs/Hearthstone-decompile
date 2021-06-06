using System;
using bgs.Wrapper.Impl;

namespace bgs
{
	// Token: 0x02000269 RID: 617
	public class LogAdapter
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x00085D12 File Offset: 0x00083F12
		public static void SetLogger(LoggerInterface outputter)
		{
			LogAdapter.s_impl = outputter;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00085D1A File Offset: 0x00083F1A
		public static void Log(LogLevel logLevel, string str, string sourceName = "")
		{
			LogAdapter.s_impl.Log(logLevel, str, sourceName);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00085D29 File Offset: 0x00083F29
		public static void LogDebug(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogDebug(message, sourceName);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00085D37 File Offset: 0x00083F37
		public static void LogInfo(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogInfo(message, sourceName);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00085D45 File Offset: 0x00083F45
		public static void LogWarning(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogWarning(message, sourceName);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00085D53 File Offset: 0x00083F53
		public static void LogError(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogError(message, sourceName);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00085D61 File Offset: 0x00083F61
		public static void LogException(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogException(message, sourceName);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00085D6F File Offset: 0x00083F6F
		public static void LogFatal(string message, string sourceName = "")
		{
			LogAdapter.s_impl.LogFatal(message, sourceName);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00085D7D File Offset: 0x00083F7D
		public static void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "")
		{
			LogAdapter.s_impl.LogDebugStackTrace(message, maxFrames, skipFrames, logSource);
		}

		// Token: 0x04000FAE RID: 4014
		private static LoggerInterface s_impl = new DefaultLogger();
	}
}
