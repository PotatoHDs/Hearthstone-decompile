using System;

namespace bgs
{
	// Token: 0x0200026B RID: 619
	public interface LoggerInterface
	{
		// Token: 0x06002599 RID: 9625
		void Log(LogLevel logLevel, string str, string sourceName = "");

		// Token: 0x0600259A RID: 9626
		void LogDebug(string message, string sourceName = "");

		// Token: 0x0600259B RID: 9627
		void LogInfo(string message, string sourceName = "");

		// Token: 0x0600259C RID: 9628
		void LogWarning(string message, string sourceName = "");

		// Token: 0x0600259D RID: 9629
		void LogError(string message, string sourceName = "");

		// Token: 0x0600259E RID: 9630
		void LogException(string message, string sourceName = "");

		// Token: 0x0600259F RID: 9631
		void LogFatal(string message, string sourceName = "");

		// Token: 0x060025A0 RID: 9632
		void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "");
	}
}
