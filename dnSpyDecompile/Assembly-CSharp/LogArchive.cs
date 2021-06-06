using System;
using System.IO;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020009C4 RID: 2500
internal class LogArchive
{
	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x06008892 RID: 34962 RVA: 0x002BFB51 File Offset: 0x002BDD51
	// (set) Token: 0x06008893 RID: 34963 RVA: 0x002BFB59 File Offset: 0x002BDD59
	public string LogPath { get; private set; }

	// Token: 0x06008894 RID: 34964 RVA: 0x002BFB62 File Offset: 0x002BDD62
	public static LogArchive Get()
	{
		if (LogArchive.s_instance == null)
		{
			LogArchive.s_instance = new LogArchive();
			LogArchive.s_instance.Initialize();
		}
		return LogArchive.s_instance;
	}

	// Token: 0x06008895 RID: 34965 RVA: 0x002BFB84 File Offset: 0x002BDD84
	private void Initialize()
	{
		string logsPath = global::Logger.LogsPath;
		this.MakeLogPath(global::Logger.LogsPath);
		try
		{
			Directory.CreateDirectory(logsPath);
			this.CleanOldLogs(logsPath);
			Application.logMessageReceived += this.HandleLog;
			Debug.LogFormat("Logging Unity output to: {0}", new object[]
			{
				this.LogPath
			});
		}
		catch (IOException ex)
		{
			Log.All.PrintWarning("Failed to write archive logs to: \"" + this.LogPath + "\"!", Array.Empty<object>());
			Log.All.PrintWarning(ex.ToString(), Array.Empty<object>());
		}
	}

	// Token: 0x06008896 RID: 34966 RVA: 0x002BFC28 File Offset: 0x002BDE28
	private void CleanOldLogs(string logFolderPath)
	{
		int num = 5;
		FileInfo[] files = new DirectoryInfo(logFolderPath).GetFiles();
		Array.Sort<FileInfo>(files, (FileInfo a, FileInfo b) => a.LastWriteTime.CompareTo(b.LastWriteTime));
		int num2 = files.Length - (num - 1);
		int num3 = 0;
		while (num3 < num2 && num3 < files.Length)
		{
			try
			{
				files[num3].Delete();
			}
			catch (Exception ex)
			{
				Log.All.PrintError("Failed to delete the file '{0}': {1}", new object[]
				{
					files[num3],
					ex.Message
				});
			}
			num3++;
		}
	}

	// Token: 0x06008897 RID: 34967 RVA: 0x002BFCC8 File Offset: 0x002BDEC8
	private void MakeLogPath(string logFolderPath)
	{
		if (!string.IsNullOrEmpty(this.LogPath))
		{
			return;
		}
		string text = LogArchive.GenerateTimestamp();
		text = text.Replace("-", "_").Replace(" ", "_").Replace(":", "_").Remove(text.Length - 4);
		string str = "hearthstone_" + text + ".log";
		this.LogPath = logFolderPath + "/" + str;
	}

	// Token: 0x06008898 RID: 34968 RVA: 0x002BFD48 File Offset: 0x002BDF48
	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (this.m_stopLogging)
		{
			return;
		}
		try
		{
			if (this.m_numLinesWritten % 100UL == 0UL)
			{
				FileInfo fileInfo = new FileInfo(this.LogPath);
				if (fileInfo.Exists && fileInfo.Length > (long)(this.m_maxFileSizeKB * 1024))
				{
					this.m_stopLogging = true;
					using (StreamWriter streamWriter = new StreamWriter(this.LogPath, true))
					{
						LogArchive.WriteLogLine(streamWriter, "", Array.Empty<object>());
						LogArchive.WriteLogLine(streamWriter, "", Array.Empty<object>());
						LogArchive.WriteLogLine(streamWriter, "==================================================================", Array.Empty<object>());
						LogArchive.WriteLogLine(streamWriter, "Truncating log, which has reached the size limit of {0}KB", new object[]
						{
							this.m_maxFileSizeKB
						});
						LogArchive.WriteLogLine(streamWriter, "==================================================================\n\n", Array.Empty<object>());
					}
					return;
				}
			}
			using (StreamWriter streamWriter2 = new StreamWriter(this.LogPath, true))
			{
				if (!string.IsNullOrEmpty(stackTrace))
				{
					LogArchive.WriteLogLine(streamWriter2, "{0}\n{1}", new object[]
					{
						logString,
						stackTrace
					});
				}
				else
				{
					LogArchive.WriteLogLine(streamWriter2, "{0}", new object[]
					{
						logString
					});
				}
				this.m_numLinesWritten += 1UL;
			}
		}
		catch (Exception ex)
		{
			Log.All.PrintError("LogArchive.HandleLog() - Failed to write \"{0}\". Exception={1}", new object[]
			{
				logString,
				ex.Message
			});
		}
	}

	// Token: 0x06008899 RID: 34969 RVA: 0x002BFEF4 File Offset: 0x002BE0F4
	private static string GenerateTimestamp()
	{
		return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
	}

	// Token: 0x0600889A RID: 34970 RVA: 0x002BFF14 File Offset: 0x002BE114
	private static void WriteLogLine(StreamWriter log, string format, params object[] args)
	{
		string str = GeneralUtils.SafeFormat(format, args);
		string value = LogArchive.GenerateTimestamp() + ": " + str;
		try
		{
			log.WriteLine(value);
			log.Flush();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x040072CE RID: 29390
	private ulong m_numLinesWritten;

	// Token: 0x040072CF RID: 29391
	private int m_maxFileSizeKB = 5000;

	// Token: 0x040072D0 RID: 29392
	private bool m_stopLogging;

	// Token: 0x040072D1 RID: 29393
	private static LogArchive s_instance;
}
