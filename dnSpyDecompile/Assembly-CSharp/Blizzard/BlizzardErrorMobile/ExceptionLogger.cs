using System;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001216 RID: 4630
	internal static class ExceptionLogger
	{
		// Token: 0x0600CFDD RID: 53213 RVA: 0x003DDE4D File Offset: 0x003DC04D
		public static void SetLogger(IExceptionLogger logger)
		{
			ExceptionLogger.s_logger = logger;
		}

		// Token: 0x0600CFDE RID: 53214 RVA: 0x003DDE55 File Offset: 0x003DC055
		public static bool IsExceptionLoggerError(string message)
		{
			return message.Contains(ExceptionLogger.s_indicatorLogger);
		}

		// Token: 0x0600CFDF RID: 53215 RVA: 0x003DDE64 File Offset: 0x003DC064
		public static void LogDebug(string format, params object[] args)
		{
			if (ExceptionLogger.s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					ExceptionLogger.s_logger.LogDebug(text, Array.Empty<object>());
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogDebug: {0}, Debug {1}", new object[]
					{
						ex.Message,
						text
					});
				}
			}
		}

		// Token: 0x0600CFE0 RID: 53216 RVA: 0x003DDEC4 File Offset: 0x003DC0C4
		public static void LogInfo(string format, params object[] args)
		{
			if (ExceptionLogger.s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					ExceptionLogger.s_logger.LogInfo(text, Array.Empty<object>());
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogInfo: {0}\n Info: {1}", new object[]
					{
						ex.Message,
						text
					});
				}
			}
		}

		// Token: 0x0600CFE1 RID: 53217 RVA: 0x003DDF24 File Offset: 0x003DC124
		public static void LogWarning(string format, params object[] args)
		{
			if (ExceptionLogger.s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					ExceptionLogger.s_logger.LogWarning(text, Array.Empty<object>());
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogWarning: {0}\n Warning: {1}", new object[]
					{
						ex.Message,
						text
					});
				}
			}
		}

		// Token: 0x0600CFE2 RID: 53218 RVA: 0x003DDF84 File Offset: 0x003DC184
		public static void LogError(string format, params object[] args)
		{
			if (ExceptionLogger.s_logger != null)
			{
				if (ExceptionReporter.Get().SendErrors)
				{
					format = ExceptionLogger.s_indicatorLogger + format;
				}
				string text = string.Format(format, args);
				try
				{
					ExceptionLogger.s_logger.LogError(text, Array.Empty<object>());
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogError: {0}\n Error: {1}", new object[]
					{
						ex.Message,
						text
					});
				}
			}
		}

		// Token: 0x0400A243 RID: 41539
		private static IExceptionLogger s_logger = null;

		// Token: 0x0400A244 RID: 41540
		private static string s_indicatorLogger = "ExceptionLogger: ";
	}
}
