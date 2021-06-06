using System;
using System.Diagnostics;
using System.Text;
using bgs;
using Blizzard.T5.Core;
using Hearthstone;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class BattleNetLogger : LoggerBase
{
	// Token: 0x060053B1 RID: 21425 RVA: 0x001B56A0 File Offset: 0x001B38A0
	private void Log(bgs.LogLevel logLevel, string str)
	{
		Blizzard.T5.Core.LogLevel level = BattleNetLogger.ConvertBGSLogToILoggerLevel(logLevel);
		global::Log.BattleNet.QueueMainThreadLog(level, str, Array.Empty<object>());
		BattleNetLogger.LogToUnityConsoleIfAllowed(logLevel, str);
	}

	// Token: 0x060053B2 RID: 21426 RVA: 0x001B56CC File Offset: 0x001B38CC
	private static void LogToUnityConsoleIfAllowed(bgs.LogLevel logLevel, string str)
	{
		global::Log.LogLevel logLevel2 = BattleNetLogger.ConvertBGSLogToLogLevel(logLevel);
		if (logLevel2 >= global::Log.LogLevel.Warning && !global::Log.BattleNet.CanPrint(LogTarget.CONSOLE, logLevel2, false))
		{
			string message = string.Format("[{0}] {1}", "BattleNet", str);
			if (logLevel2 == global::Log.LogLevel.Warning)
			{
				UnityEngine.Debug.LogWarning(message);
				return;
			}
			UnityEngine.Debug.LogError(message);
		}
	}

	// Token: 0x060053B3 RID: 21427 RVA: 0x001B5715 File Offset: 0x001B3915
	private static global::Log.LogLevel ConvertBGSLogToLogLevel(bgs.LogLevel logLevel)
	{
		switch (logLevel)
		{
		case bgs.LogLevel.None:
			return global::Log.LogLevel.None;
		case bgs.LogLevel.Debug:
			return global::Log.LogLevel.Debug;
		case bgs.LogLevel.Info:
			return global::Log.LogLevel.Info;
		case bgs.LogLevel.Warning:
			return global::Log.LogLevel.Warning;
		case bgs.LogLevel.Error:
		case bgs.LogLevel.Exception:
		case bgs.LogLevel.Fatal:
			return global::Log.LogLevel.Error;
		default:
			return global::Log.LogLevel.Error;
		}
	}

	// Token: 0x060053B4 RID: 21428 RVA: 0x001B5746 File Offset: 0x001B3946
	private static Blizzard.T5.Core.LogLevel ConvertBGSLogToILoggerLevel(bgs.LogLevel logLevel)
	{
		switch (logLevel)
		{
		case bgs.LogLevel.None:
		case bgs.LogLevel.Debug:
			return Blizzard.T5.Core.LogLevel.Debug;
		case bgs.LogLevel.Info:
			return Blizzard.T5.Core.LogLevel.Information;
		case bgs.LogLevel.Warning:
			return Blizzard.T5.Core.LogLevel.Warning;
		case bgs.LogLevel.Error:
		case bgs.LogLevel.Exception:
		case bgs.LogLevel.Fatal:
			return Blizzard.T5.Core.LogLevel.Error;
		default:
			return Blizzard.T5.Core.LogLevel.Error;
		}
	}

	// Token: 0x060053B5 RID: 21429 RVA: 0x001B5775 File Offset: 0x001B3975
	public override void Log(bgs.LogLevel logLevel, string message, string sourceName)
	{
		if (logLevel == bgs.LogLevel.Fatal)
		{
			this.SendFatalEvent(message);
		}
		this.LogFromSource(logLevel, message, sourceName, true);
	}

	// Token: 0x060053B6 RID: 21430 RVA: 0x001B578C File Offset: 0x001B398C
	public override void LogDebug(string message, string sourceName = "")
	{
		this.LogFromSource(bgs.LogLevel.Debug, message, sourceName, true);
	}

	// Token: 0x060053B7 RID: 21431 RVA: 0x001B5798 File Offset: 0x001B3998
	public override void LogInfo(string message, string sourceName = "")
	{
		this.LogFromSource(bgs.LogLevel.Info, message, sourceName, true);
	}

	// Token: 0x060053B8 RID: 21432 RVA: 0x001B57A4 File Offset: 0x001B39A4
	public override void LogWarning(string message, string sourceName = "")
	{
		this.LogFromSource(bgs.LogLevel.Warning, message, sourceName, true);
	}

	// Token: 0x060053B9 RID: 21433 RVA: 0x001B57B0 File Offset: 0x001B39B0
	public override void LogError(string message, string sourceName = "")
	{
		this.LogFromSource(bgs.LogLevel.Error, message, sourceName, true);
	}

	// Token: 0x060053BA RID: 21434 RVA: 0x001B57BC File Offset: 0x001B39BC
	public override void LogException(string message, string sourceName = "")
	{
		this.SendLiveIssueTelemetry(message, sourceName);
		this.LogFromSource(bgs.LogLevel.Exception, message, sourceName, true);
	}

	// Token: 0x060053BB RID: 21435 RVA: 0x001B57D0 File Offset: 0x001B39D0
	public override void LogFatal(string message, string sourceName = "")
	{
		this.SendLiveIssueTelemetry(message, sourceName);
		this.SendFatalEvent("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
		this.LogFromSource(bgs.LogLevel.Fatal, message, sourceName, true);
	}

	// Token: 0x060053BC RID: 21436 RVA: 0x001B57F0 File Offset: 0x001B39F0
	private void SendLiveIssueTelemetry(string message, string sourceName)
	{
		string details;
		if (string.IsNullOrEmpty(sourceName))
		{
			details = "Exception: " + message;
		}
		else
		{
			details = string.Format("Source: {0}  Message: {1}", sourceName, message);
		}
		TelemetryManager.Client().SendLiveIssue("Battle.net Exception", details);
	}

	// Token: 0x060053BD RID: 21437 RVA: 0x001B5830 File Offset: 0x001B3A30
	private void SendFatalEvent(string message)
	{
		if (HearthstoneApplication.IsInternal())
		{
			global::Error.AddDevFatal(GameStrings.Get(message), Array.Empty<object>());
			return;
		}
		global::Error.AddFatal(FatalErrorReason.BNET_FATAL, message, Array.Empty<object>());
	}

	// Token: 0x060053BE RID: 21438 RVA: 0x001B5858 File Offset: 0x001B3A58
	private void LogFromSource(bgs.LogLevel logLevel, string message, string sourceName, bool includeFileName = true)
	{
		StackTrace stackTrace = new StackTrace(new StackFrame(2, true));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		stringBuilder.Append(sourceName);
		stringBuilder.Append("] ");
		stringBuilder.Append(message);
		if (includeFileName)
		{
			StackFrame frame = stackTrace.GetFrame(0);
			stringBuilder.Append(base.FormatStackTrace(frame, false));
		}
		this.Log(logLevel, stringBuilder.ToString());
	}
}
