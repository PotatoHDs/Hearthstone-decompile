using System;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	internal static class ExceptionLogger
	{
		private static IExceptionLogger s_logger = null;

		private static string s_indicatorLogger = "ExceptionLogger: ";

		public static void SetLogger(IExceptionLogger logger)
		{
			s_logger = logger;
		}

		public static bool IsExceptionLoggerError(string message)
		{
			return message.Contains(s_indicatorLogger);
		}

		public static void LogDebug(string format, params object[] args)
		{
			if (s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					s_logger.LogDebug(text);
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogDebug: {0}, Debug {1}", ex.Message, text);
				}
			}
		}

		public static void LogInfo(string format, params object[] args)
		{
			if (s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					s_logger.LogInfo(text);
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogInfo: {0}\n Info: {1}", ex.Message, text);
				}
			}
		}

		public static void LogWarning(string format, params object[] args)
		{
			if (s_logger != null)
			{
				string text = string.Format(format, args);
				try
				{
					s_logger.LogWarning(text);
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogWarning: {0}\n Warning: {1}", ex.Message, text);
				}
			}
		}

		public static void LogError(string format, params object[] args)
		{
			if (s_logger != null)
			{
				if (ExceptionReporter.Get().SendErrors)
				{
					format = s_indicatorLogger + format;
				}
				string text = string.Format(format, args);
				try
				{
					s_logger.LogError(text);
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Failed to record LogError: {0}\n Error: {1}", ex.Message, text);
				}
			}
		}
	}
}
