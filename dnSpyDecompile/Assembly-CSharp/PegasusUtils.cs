using System;
using System.Diagnostics;
using System.IO;
using Hearthstone;
using UnityEngine;

// Token: 0x020009CF RID: 2511
public static class PegasusUtils
{
	// Token: 0x060088D0 RID: 35024 RVA: 0x002C0B76 File Offset: 0x002BED76
	public static bool IsTestScene()
	{
		return HearthstoneApplication.Get() == null;
	}

	// Token: 0x060088D1 RID: 35025 RVA: 0x002C0B84 File Offset: 0x002BED84
	public static string GetPatchDir()
	{
		string text = Directory.GetCurrentDirectory();
		text = text.Substring(0, text.LastIndexOf(Path.DirectorySeparatorChar));
		return text.Substring(0, text.LastIndexOf(Path.DirectorySeparatorChar));
	}

	// Token: 0x060088D2 RID: 35026 RVA: 0x002C0BBE File Offset: 0x002BEDBE
	public static Process RunPegasusCommonScriptWithParams(string scriptName, params string[] scriptParams)
	{
		return PegasusUtils.RunPegasusCommonScriptWithParamsAndWorkingDirectory(scriptName, null, scriptParams);
	}

	// Token: 0x060088D3 RID: 35027 RVA: 0x002C0BC8 File Offset: 0x002BEDC8
	public static Process RunPegasusCommonScriptWithParamsAndWorkingDirectory(string scriptName, string workingDirectory, params string[] scriptParams)
	{
		Process result;
		try
		{
			string patchDir = PegasusUtils.GetPatchDir();
			string text = string.Join(" ", scriptParams);
			string arg = "bat";
			string text2 = Path.Combine(patchDir, "Pegasus");
			text2 = Path.Combine(text2, "Common");
			text2 = Path.Combine(text2, string.Format("{0}.{1}", scriptName, arg));
			UnityEngine.Debug.LogFormat("Running command: {0} {1}", new object[]
			{
				text2,
				text
			});
			Process process = new Process();
			process.StartInfo.FileName = text2;
			process.StartInfo.Arguments = text;
			process.StartInfo.WorkingDirectory = workingDirectory;
			process.Start();
			result = process;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("Failed to run {0}: {1}", new object[]
			{
				scriptName,
				ex.Message
			});
			result = null;
		}
		return result;
	}

	// Token: 0x060088D4 RID: 35028 RVA: 0x002C0C98 File Offset: 0x002BEE98
	public static bool CompleteProcess(Process proc)
	{
		return PegasusUtils.CompleteProcess(proc, -1);
	}

	// Token: 0x060088D5 RID: 35029 RVA: 0x002C0CA4 File Offset: 0x002BEEA4
	public static bool CompleteProcess(Process proc, int millisecondTimout)
	{
		string stdoutStr = "";
		string stderrStr = "";
		if (proc.StartInfo.RedirectStandardOutput)
		{
			stdoutStr = proc.StandardOutput.ReadToEnd();
		}
		if (proc.StartInfo.RedirectStandardError)
		{
			stderrStr = proc.StandardError.ReadToEnd();
		}
		if (!proc.WaitForExit(millisecondTimout))
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				Path.GetFileNameWithoutExtension(proc.StartInfo.FileName),
				" timed out after ",
				millisecondTimout,
				"milliseconds"
			}));
			return false;
		}
		return PegasusUtils.LogCompletedProcess(proc, stdoutStr, stderrStr);
	}

	// Token: 0x060088D6 RID: 35030 RVA: 0x002C0D40 File Offset: 0x002BEF40
	public static bool LogCompletedProcess(Process proc, string stdoutStr, string stderrStr)
	{
		if (proc.StartInfo.RedirectStandardOutput || proc.StartInfo.RedirectStandardError)
		{
			string text = Path.GetFileNameWithoutExtension(proc.StartInfo.FileName) + " output (click to show):\n" + stdoutStr;
			if (proc.ExitCode == 0)
			{
				UnityEngine.Debug.Log(text);
			}
			else
			{
				text = text + "\n\nERRORS:\n" + stderrStr;
				UnityEngine.Debug.LogError(text);
			}
		}
		return proc.ExitCode == 0;
	}

	// Token: 0x060088D7 RID: 35031 RVA: 0x002C0DB5 File Offset: 0x002BEFB5
	public static string GetTokenFetcherFolderPath()
	{
		return FileUtils.CombinePath(new object[]
		{
			PegasusUtils.GetPatchDir(),
			"Pegasus/Assets/source/DesignData/Tools/token_fetcher/"
		});
	}

	// Token: 0x060088D8 RID: 35032 RVA: 0x002C0DD2 File Offset: 0x002BEFD2
	public static string GetTokenFetcherFolderNameWin()
	{
		return FileUtils.CombinePath(new object[]
		{
			PegasusUtils.GetTokenFetcherFolderPath(),
			"win/"
		});
	}

	// Token: 0x060088D9 RID: 35033 RVA: 0x002C0DEF File Offset: 0x002BEFEF
	public static string GetTokenFetcherFolderNameOsx()
	{
		return FileUtils.CombinePath(new object[]
		{
			PegasusUtils.GetTokenFetcherFolderPath(),
			"mac/"
		});
	}

	// Token: 0x060088DA RID: 35034 RVA: 0x002C0E0C File Offset: 0x002BF00C
	private static void UseStackTraceLoggingMinimum()
	{
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Assert, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
	}

	// Token: 0x060088DB RID: 35035 RVA: 0x002C0E23 File Offset: 0x002BF023
	public static void SetStackTraceLoggingOptions(bool forceUseMinimumLogging)
	{
		if (forceUseMinimumLogging)
		{
			PegasusUtils.UseStackTraceLoggingMinimum();
			return;
		}
		PegasusUtils.UseStackTraceLoggingMinimum();
	}
}
