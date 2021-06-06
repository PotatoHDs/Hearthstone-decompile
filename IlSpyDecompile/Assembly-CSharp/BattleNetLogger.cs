using System.Diagnostics;
using System.Text;
using bgs;
using Blizzard.T5.Core;
using Hearthstone;
using UnityEngine;

public class BattleNetLogger : LoggerBase
{
	private void Log(bgs.LogLevel logLevel, string str)
	{
		Blizzard.T5.Core.LogLevel level = ConvertBGSLogToILoggerLevel(logLevel);
		global::Log.BattleNet.QueueMainThreadLog(level, str);
		LogToUnityConsoleIfAllowed(logLevel, str);
	}

	private static void LogToUnityConsoleIfAllowed(bgs.LogLevel logLevel, string str)
	{
		Log.LogLevel logLevel2 = ConvertBGSLogToLogLevel(logLevel);
		if (logLevel2 >= global::Log.LogLevel.Warning && !global::Log.BattleNet.CanPrint(LogTarget.CONSOLE, logLevel2, verbose: false))
		{
			string message = string.Format("[{0}] {1}", "BattleNet", str);
			if (logLevel2 == global::Log.LogLevel.Warning)
			{
				UnityEngine.Debug.LogWarning(message);
			}
			else
			{
				UnityEngine.Debug.LogError(message);
			}
		}
	}

	private static Log.LogLevel ConvertBGSLogToLogLevel(bgs.LogLevel logLevel)
	{
		switch (logLevel)
		{
		case bgs.LogLevel.Error:
		case bgs.LogLevel.Exception:
		case bgs.LogLevel.Fatal:
			return global::Log.LogLevel.Error;
		case bgs.LogLevel.Warning:
			return global::Log.LogLevel.Warning;
		case bgs.LogLevel.Info:
			return global::Log.LogLevel.Info;
		case bgs.LogLevel.Debug:
			return global::Log.LogLevel.Debug;
		case bgs.LogLevel.None:
			return global::Log.LogLevel.None;
		default:
			return global::Log.LogLevel.Error;
		}
	}

	private static Blizzard.T5.Core.LogLevel ConvertBGSLogToILoggerLevel(bgs.LogLevel logLevel)
	{
		switch (logLevel)
		{
		case bgs.LogLevel.Error:
		case bgs.LogLevel.Exception:
		case bgs.LogLevel.Fatal:
			return Blizzard.T5.Core.LogLevel.Error;
		case bgs.LogLevel.Warning:
			return Blizzard.T5.Core.LogLevel.Warning;
		case bgs.LogLevel.Info:
			return Blizzard.T5.Core.LogLevel.Information;
		case bgs.LogLevel.None:
		case bgs.LogLevel.Debug:
			return Blizzard.T5.Core.LogLevel.Debug;
		default:
			return Blizzard.T5.Core.LogLevel.Error;
		}
	}

	public override void Log(bgs.LogLevel logLevel, string message, string sourceName)
	{
		if (logLevel == bgs.LogLevel.Fatal)
		{
			SendFatalEvent(message);
		}
		LogFromSource(logLevel, message, sourceName);
	}

	public override void LogDebug(string message, string sourceName = "")
	{
		LogFromSource(bgs.LogLevel.Debug, message, sourceName);
	}

	public override void LogInfo(string message, string sourceName = "")
	{
		LogFromSource(bgs.LogLevel.Info, message, sourceName);
	}

	public override void LogWarning(string message, string sourceName = "")
	{
		LogFromSource(bgs.LogLevel.Warning, message, sourceName);
	}

	public override void LogError(string message, string sourceName = "")
	{
		LogFromSource(bgs.LogLevel.Error, message, sourceName);
	}

	public override void LogException(string message, string sourceName = "")
	{
		SendLiveIssueTelemetry(message, sourceName);
		LogFromSource(bgs.LogLevel.Exception, message, sourceName);
	}

	public override void LogFatal(string message, string sourceName = "")
	{
		SendLiveIssueTelemetry(message, sourceName);
		SendFatalEvent("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
		LogFromSource(bgs.LogLevel.Fatal, message, sourceName);
	}

	private void SendLiveIssueTelemetry(string message, string sourceName)
	{
		string details = ((!string.IsNullOrEmpty(sourceName)) ? $"Source: {sourceName}  Message: {message}" : ("Exception: " + message));
		TelemetryManager.Client().SendLiveIssue("Battle.net Exception", details);
	}

	private void SendFatalEvent(string message)
	{
		if (HearthstoneApplication.IsInternal())
		{
			Error.AddDevFatal(GameStrings.Get(message));
		}
		else
		{
			Error.AddFatal(FatalErrorReason.BNET_FATAL, message);
		}
	}

	private void LogFromSource(bgs.LogLevel logLevel, string message, string sourceName, bool includeFileName = true)
	{
		StackTrace stackTrace = new StackTrace(new StackFrame(2, fNeedFileInfo: true));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		stringBuilder.Append(sourceName);
		stringBuilder.Append("] ");
		stringBuilder.Append(message);
		if (includeFileName)
		{
			StackFrame frame = stackTrace.GetFrame(0);
			stringBuilder.Append(FormatStackTrace(frame));
		}
		Log(logLevel, stringBuilder.ToString());
	}
}
