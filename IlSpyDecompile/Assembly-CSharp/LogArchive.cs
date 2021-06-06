using System;
using System.IO;
using Blizzard.T5.Core;
using UnityEngine;

internal class LogArchive
{
	private ulong m_numLinesWritten;

	private int m_maxFileSizeKB = 5000;

	private bool m_stopLogging;

	private static LogArchive s_instance;

	public string LogPath { get; private set; }

	public static LogArchive Get()
	{
		if (s_instance == null)
		{
			s_instance = new LogArchive();
			s_instance.Initialize();
		}
		return s_instance;
	}

	private void Initialize()
	{
		string logsPath = Logger.LogsPath;
		MakeLogPath(Logger.LogsPath);
		try
		{
			Directory.CreateDirectory(logsPath);
			CleanOldLogs(logsPath);
			Application.logMessageReceived += HandleLog;
			Debug.LogFormat("Logging Unity output to: {0}", LogPath);
		}
		catch (IOException ex)
		{
			Log.All.PrintWarning("Failed to write archive logs to: \"" + LogPath + "\"!");
			Log.All.PrintWarning(ex.ToString());
		}
	}

	private void CleanOldLogs(string logFolderPath)
	{
		int num = 5;
		FileInfo[] files = new DirectoryInfo(logFolderPath).GetFiles();
		Array.Sort(files, (FileInfo a, FileInfo b) => a.LastWriteTime.CompareTo(b.LastWriteTime));
		int num2 = files.Length - (num - 1);
		for (int i = 0; i < num2 && i < files.Length; i++)
		{
			try
			{
				files[i].Delete();
			}
			catch (Exception ex)
			{
				Log.All.PrintError("Failed to delete the file '{0}': {1}", files[i], ex.Message);
			}
		}
	}

	private void MakeLogPath(string logFolderPath)
	{
		if (string.IsNullOrEmpty(LogPath))
		{
			string text = GenerateTimestamp();
			text = text.Replace("-", "_").Replace(" ", "_").Replace(":", "_")
				.Remove(text.Length - 4);
			string text2 = "hearthstone_" + text + ".log";
			LogPath = logFolderPath + "/" + text2;
		}
	}

	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (m_stopLogging)
		{
			return;
		}
		try
		{
			if (m_numLinesWritten % 100uL != 0L)
			{
				goto IL_00cc;
			}
			FileInfo fileInfo = new FileInfo(LogPath);
			if (!fileInfo.Exists || fileInfo.Length <= m_maxFileSizeKB * 1024)
			{
				goto IL_00cc;
			}
			m_stopLogging = true;
			using (StreamWriter log = new StreamWriter(LogPath, append: true))
			{
				WriteLogLine(log, "");
				WriteLogLine(log, "");
				WriteLogLine(log, "==================================================================");
				WriteLogLine(log, "Truncating log, which has reached the size limit of {0}KB", m_maxFileSizeKB);
				WriteLogLine(log, "==================================================================\n\n");
			}
			goto end_IL_000a;
			IL_00cc:
			using (StreamWriter log2 = new StreamWriter(LogPath, append: true))
			{
				if (!string.IsNullOrEmpty(stackTrace))
				{
					WriteLogLine(log2, "{0}\n{1}", logString, stackTrace);
				}
				else
				{
					WriteLogLine(log2, "{0}", logString);
				}
				m_numLinesWritten++;
			}
			end_IL_000a:;
		}
		catch (Exception ex)
		{
			Log.All.PrintError("LogArchive.HandleLog() - Failed to write \"{0}\". Exception={1}", logString, ex.Message);
		}
	}

	private static string GenerateTimestamp()
	{
		return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
	}

	private static void WriteLogLine(StreamWriter log, string format, params object[] args)
	{
		string text = GeneralUtils.SafeFormat(format, args);
		string value = GenerateTimestamp() + ": " + text;
		try
		{
			log.WriteLine(value);
			log.Flush();
		}
		catch (Exception)
		{
		}
	}
}
