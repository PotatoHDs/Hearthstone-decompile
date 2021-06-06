using System;
using Blizzard.Telemetry;

namespace Hearthstone.Telemetry
{
	// Token: 0x0200106D RID: 4205
	public class TelemetryLogWrapper : ILogger
	{
		// Token: 0x0600B57B RID: 46459 RVA: 0x0037C6B2 File Offset: 0x0037A8B2
		public void LogDebug(string message)
		{
			global::Log.Telemetry.PrintDebug("[SDK] {0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600B57C RID: 46460 RVA: 0x0037C6CD File Offset: 0x0037A8CD
		public void LogInfo(string message)
		{
			global::Log.Telemetry.PrintInfo("[SDK] {0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600B57D RID: 46461 RVA: 0x0037C6E8 File Offset: 0x0037A8E8
		public void LogWarning(string message)
		{
			global::Log.Telemetry.PrintWarning("[SDK] {0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600B57E RID: 46462 RVA: 0x0037C703 File Offset: 0x0037A903
		public void LogError(string message)
		{
			global::Log.Telemetry.PrintError("[SDK] {0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600B57F RID: 46463 RVA: 0x0037C703 File Offset: 0x0037A903
		public void LogFatal(string message)
		{
			global::Log.Telemetry.PrintError("[SDK] {0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600B580 RID: 46464 RVA: 0x000052EC File Offset: 0x000034EC
		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}
	}
}
