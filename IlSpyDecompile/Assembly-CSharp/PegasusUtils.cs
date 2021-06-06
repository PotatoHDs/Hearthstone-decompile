using System;
using System.Diagnostics;
using System.IO;
using Hearthstone;
using UnityEngine;

public static class PegasusUtils
{
	public static bool IsTestScene()
	{
		return HearthstoneApplication.Get() == null;
	}

	public static string GetPatchDir()
	{
		string currentDirectory = Directory.GetCurrentDirectory();
		currentDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf(Path.DirectorySeparatorChar));
		return currentDirectory.Substring(0, currentDirectory.LastIndexOf(Path.DirectorySeparatorChar));
	}

	public static Process RunPegasusCommonScriptWithParams(string scriptName, params string[] scriptParams)
	{
		return RunPegasusCommonScriptWithParamsAndWorkingDirectory(scriptName, null, scriptParams);
	}

	public static Process RunPegasusCommonScriptWithParamsAndWorkingDirectory(string scriptName, string workingDirectory, params string[] scriptParams)
	{
		try
		{
			string patchDir = GetPatchDir();
			string text = string.Join(" ", scriptParams);
			string arg = "bat";
			string path = Path.Combine(patchDir, "Pegasus");
			path = Path.Combine(path, "Common");
			path = Path.Combine(path, $"{scriptName}.{arg}");
			UnityEngine.Debug.LogFormat("Running command: {0} {1}", path, text);
			Process process = new Process();
			process.StartInfo.FileName = path;
			process.StartInfo.Arguments = text;
			process.StartInfo.WorkingDirectory = workingDirectory;
			process.Start();
			return process;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("Failed to run {0}: {1}", scriptName, ex.Message);
			return null;
		}
	}

	public static bool CompleteProcess(Process proc)
	{
		return CompleteProcess(proc, -1);
	}

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
			UnityEngine.Debug.LogError(Path.GetFileNameWithoutExtension(proc.StartInfo.FileName) + " timed out after " + millisecondTimout + "milliseconds");
			return false;
		}
		return LogCompletedProcess(proc, stdoutStr, stderrStr);
	}

	public static bool LogCompletedProcess(Process proc, string stdoutStr, string stderrStr)
	{
		string text = "";
		if (proc.StartInfo.RedirectStandardOutput || proc.StartInfo.RedirectStandardError)
		{
			text = Path.GetFileNameWithoutExtension(proc.StartInfo.FileName) + " output (click to show):\n" + stdoutStr;
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

	public static string GetTokenFetcherFolderPath()
	{
		return FileUtils.CombinePath(GetPatchDir(), "Pegasus/Assets/source/DesignData/Tools/token_fetcher/");
	}

	public static string GetTokenFetcherFolderNameWin()
	{
		return FileUtils.CombinePath(GetTokenFetcherFolderPath(), "win/");
	}

	public static string GetTokenFetcherFolderNameOsx()
	{
		return FileUtils.CombinePath(GetTokenFetcherFolderPath(), "mac/");
	}

	private static void UseStackTraceLoggingMinimum()
	{
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Assert, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
	}

	public static void SetStackTraceLoggingOptions(bool forceUseMinimumLogging)
	{
		if (forceUseMinimumLogging)
		{
			UseStackTraceLoggingMinimum();
		}
		else
		{
			UseStackTraceLoggingMinimum();
		}
	}
}
