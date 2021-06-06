using System;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020009C3 RID: 2499
public class Logger : Blizzard.T5.Core.ILogger
{
	// Token: 0x0600885D RID: 34909 RVA: 0x002BF0DE File Offset: 0x002BD2DE
	public Logger(string name)
	{
		this.m_name = name;
		this.m_stringBuilder = new StringBuilder(128);
		Log.AllLoggers[name] = this;
	}

	// Token: 0x0600885E RID: 34910 RVA: 0x002BF10C File Offset: 0x002BD30C
	public bool CanPrint(LogTarget target, Log.LogLevel level, bool verbose)
	{
		LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
		if (level == Log.LogLevel.Error && target != LogTarget.SCREEN && this.ShouldAlwaysPrintErrors())
		{
			return true;
		}
		if (logInfo == null)
		{
			return false;
		}
		if (level < logInfo.m_minLevel)
		{
			return false;
		}
		if (verbose && !logInfo.m_verbose)
		{
			return false;
		}
		switch (target)
		{
		case LogTarget.CONSOLE:
			return logInfo.m_consolePrinting;
		case LogTarget.SCREEN:
			return logInfo.m_screenPrinting;
		case LogTarget.FILE:
			return logInfo.m_filePrinting;
		default:
			return false;
		}
	}

	// Token: 0x0600885F RID: 34911 RVA: 0x002BF188 File Offset: 0x002BD388
	public bool CanPrint(Log.LogLevel level, bool? verbose = null)
	{
		LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
		return (level == Log.LogLevel.Error && this.ShouldAlwaysPrintErrors()) || (logInfo != null && level >= logInfo.m_minLevel && (verbose == null || !verbose.Value || logInfo.m_verbose) && (logInfo.m_consolePrinting || logInfo.m_screenPrinting || logInfo.m_filePrinting));
	}

	// Token: 0x06008860 RID: 34912 RVA: 0x002BF200 File Offset: 0x002BD400
	public bool CanPrint()
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		return this.CanPrint(defaultLevel, new bool?(false));
	}

	// Token: 0x06008861 RID: 34913 RVA: 0x002BF224 File Offset: 0x002BD424
	public Log.LogLevel GetDefaultLevel()
	{
		LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
		if (logInfo == null)
		{
			return Log.LogLevel.Debug;
		}
		return logInfo.m_defaultLevel;
	}

	// Token: 0x06008862 RID: 34914 RVA: 0x002BF250 File Offset: 0x002BD450
	public bool ShouldAlwaysPrintErrors()
	{
		LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
		return logInfo == null || logInfo.m_alwaysPrintErrors;
	}

	// Token: 0x06008863 RID: 34915 RVA: 0x002BF27C File Offset: 0x002BD47C
	public bool IsVerbose()
	{
		LogInfo logInfo = Log.Get().GetLogInfo(this.m_name);
		return logInfo != null && logInfo.m_verbose;
	}

	// Token: 0x06008864 RID: 34916 RVA: 0x002BF2A8 File Offset: 0x002BD4A8
	public void Print(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.Print(defaultLevel, false, format, args);
	}

	// Token: 0x06008865 RID: 34917 RVA: 0x002BF2C6 File Offset: 0x002BD4C6
	public void Print(Log.LogLevel level, string format, params object[] args)
	{
		this.Print(level, false, format, args);
	}

	// Token: 0x06008866 RID: 34918 RVA: 0x002BF2D4 File Offset: 0x002BD4D4
	public void Print(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.Print(defaultLevel, verbose, format, args);
	}

	// Token: 0x06008867 RID: 34919 RVA: 0x002BF2F4 File Offset: 0x002BD4F4
	public void Print(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (!this.CanPrint(level, new bool?(verbose)))
		{
			return;
		}
		string message = GeneralUtils.SafeFormat(format, args);
		this.Print(level, verbose, message);
	}

	// Token: 0x06008868 RID: 34920 RVA: 0x002BF323 File Offset: 0x002BD523
	public void Print(Log.LogLevel level, bool verbose, string message)
	{
		this.FilePrint(level, verbose, message, false, true);
		this.ConsolePrint(level, verbose, message);
		this.ScreenPrint(level, verbose, message);
	}

	// Token: 0x06008869 RID: 34921 RVA: 0x002BF342 File Offset: 0x002BD542
	public void PrintAndForcePrintToScreen(Log.LogLevel level, bool verbose, string message)
	{
		this.FilePrint(level, verbose, message, false, true);
		this.ConsolePrint(level, verbose, message);
		this.ForceScreenPrint(level, verbose, message);
	}

	// Token: 0x0600886A RID: 34922 RVA: 0x002BF361 File Offset: 0x002BD561
	public void PrintDebug(string format, params object[] args)
	{
		this.PrintDebug(false, format, args);
	}

	// Token: 0x0600886B RID: 34923 RVA: 0x002BF36C File Offset: 0x002BD56C
	public void PrintDebug(bool verbose, string format, params object[] args)
	{
		this.Print(Log.LogLevel.Debug, verbose, format, args);
	}

	// Token: 0x0600886C RID: 34924 RVA: 0x002BF378 File Offset: 0x002BD578
	public void PrintInfo(string format, params object[] args)
	{
		this.PrintInfo(false, format, args);
	}

	// Token: 0x0600886D RID: 34925 RVA: 0x002BF383 File Offset: 0x002BD583
	public void PrintInfo(bool verbose, string format, params object[] args)
	{
		this.Print(Log.LogLevel.Info, verbose, format, args);
	}

	// Token: 0x0600886E RID: 34926 RVA: 0x002BF38F File Offset: 0x002BD58F
	public void PrintWarning(string format, params object[] args)
	{
		this.PrintWarning(false, format, args);
	}

	// Token: 0x0600886F RID: 34927 RVA: 0x002BF39A File Offset: 0x002BD59A
	public void PrintWarning(bool verbose, string format, params object[] args)
	{
		this.Print(Log.LogLevel.Warning, verbose, format, args);
	}

	// Token: 0x06008870 RID: 34928 RVA: 0x002BF3A6 File Offset: 0x002BD5A6
	public void PrintError(string format, params object[] args)
	{
		this.PrintError(false, format, args);
	}

	// Token: 0x06008871 RID: 34929 RVA: 0x002BF3B1 File Offset: 0x002BD5B1
	public void PrintError(bool verbose, string format, params object[] args)
	{
		this.Print(Log.LogLevel.Error, verbose, format, args);
	}

	// Token: 0x06008872 RID: 34930 RVA: 0x002BF3BD File Offset: 0x002BD5BD
	public void ForceFilePrint(Log.LogLevel level, string message)
	{
		this.FilePrint(level, false, message, true, false);
	}

	// Token: 0x06008873 RID: 34931 RVA: 0x002BF3CC File Offset: 0x002BD5CC
	public void FilePrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.FilePrint(defaultLevel, false, format, args);
	}

	// Token: 0x06008874 RID: 34932 RVA: 0x002BF3EA File Offset: 0x002BD5EA
	public void FilePrint(Log.LogLevel level, string format, params object[] args)
	{
		this.FilePrint(level, false, format, args);
	}

	// Token: 0x06008875 RID: 34933 RVA: 0x002BF3F8 File Offset: 0x002BD5F8
	public void FilePrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.FilePrint(defaultLevel, verbose, format, args);
	}

	// Token: 0x06008876 RID: 34934 RVA: 0x002BF418 File Offset: 0x002BD618
	public void FilePrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (!this.CanPrint(LogTarget.FILE, level, verbose))
		{
			return;
		}
		string message = GeneralUtils.SafeFormat(format, args);
		this.FilePrint(level, verbose, message, false, true);
	}

	// Token: 0x06008877 RID: 34935 RVA: 0x002BF448 File Offset: 0x002BD648
	public void FilePrint(Log.LogLevel level, bool verbose, string message, bool forced = false, bool printContext = true)
	{
		if (!forced && !this.CanPrint(LogTarget.FILE, level, verbose))
		{
			return;
		}
		this.InitFileWriter(false);
		if (this.m_fileWriter == null)
		{
			return;
		}
		this.m_stringBuilder.Clear();
		this.m_stringBuilder.EnsureCapacity(message.Length + 20);
		if (printContext)
		{
			switch (level)
			{
			case Log.LogLevel.Debug:
				this.m_stringBuilder.Append("D ");
				break;
			case Log.LogLevel.Info:
				this.m_stringBuilder.Append("I ");
				break;
			case Log.LogLevel.Warning:
				this.m_stringBuilder.Append("W ");
				break;
			case Log.LogLevel.Error:
				this.m_stringBuilder.Append("E ");
				break;
			}
			this.m_stringBuilder.Append(this.GetTimeOfDay());
			this.m_stringBuilder.Append(" ");
		}
		this.m_stringBuilder.Append(message);
		this.m_fileWriter.WriteLine(this.m_stringBuilder.ToString());
		this.FlushContent();
	}

	// Token: 0x06008878 RID: 34936 RVA: 0x002BF54D File Offset: 0x002BD74D
	public void FilePrintRaw(string rawText)
	{
		this.InitFileWriter(false);
		if (this.m_fileWriter == null)
		{
			return;
		}
		this.m_fileWriter.Write(rawText);
		this.FlushContent();
	}

	// Token: 0x06008879 RID: 34937 RVA: 0x002BF574 File Offset: 0x002BD774
	public void PurgeFile()
	{
		string logsPath = global::Logger.LogsPath;
		string path = string.Format("{0}/{1}.{2}", logsPath, this.m_name, "log");
		if (File.Exists(path))
		{
			if (this.m_fileWriterInitialized)
			{
				this.m_fileWriter.Dispose();
				this.m_fileWriterInitialized = false;
			}
			File.Delete(path);
		}
	}

	// Token: 0x0600887A RID: 34938 RVA: 0x002BF5C8 File Offset: 0x002BD7C8
	public void FlushContent()
	{
		if (!this.m_fileWriterInitialized || this.m_fileWriter == null)
		{
			return;
		}
		try
		{
			this.m_fileWriter.Flush();
		}
		catch (Exception ex)
		{
			this.ConsolePrint(Log.LogLevel.Error, ex.Message, Array.Empty<object>());
			this.CloseFileWriter();
		}
	}

	// Token: 0x0600887B RID: 34939 RVA: 0x002BF620 File Offset: 0x002BD820
	public void CloseFileWriter()
	{
		try
		{
			this.m_fileWriter.Close();
		}
		catch
		{
		}
		finally
		{
			this.m_fileWriter = null;
		}
	}

	// Token: 0x0600887C RID: 34940 RVA: 0x002BF664 File Offset: 0x002BD864
	public string GetContent()
	{
		if (this.m_fileWriterInitialized)
		{
			this.m_fileWriter.Close();
			this.m_fileWriterInitialized = false;
		}
		string result = File.ReadAllText(string.Format("{0}/{1}.{2}", global::Logger.LogsPath, this.m_name, "log"));
		this.InitFileWriter(true);
		return result;
	}

	// Token: 0x0600887D RID: 34941 RVA: 0x002BF6B4 File Offset: 0x002BD8B4
	public void ConsolePrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.ConsolePrint(defaultLevel, false, format, args);
	}

	// Token: 0x0600887E RID: 34942 RVA: 0x002BF6D2 File Offset: 0x002BD8D2
	public void ConsolePrint(Log.LogLevel level, string format, params object[] args)
	{
		this.ConsolePrint(level, false, format, args);
	}

	// Token: 0x0600887F RID: 34943 RVA: 0x002BF6E0 File Offset: 0x002BD8E0
	public void ConsolePrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.ConsolePrint(defaultLevel, verbose, format, args);
	}

	// Token: 0x06008880 RID: 34944 RVA: 0x002BF700 File Offset: 0x002BD900
	public void ConsolePrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (!this.CanPrint(LogTarget.CONSOLE, level, verbose))
		{
			return;
		}
		string message = GeneralUtils.SafeFormat(format, args);
		this.ConsolePrint(level, verbose, message);
	}

	// Token: 0x06008881 RID: 34945 RVA: 0x002BF72C File Offset: 0x002BD92C
	public void ConsolePrint(Log.LogLevel level, bool verbose, string message)
	{
		if (!this.CanPrint(LogTarget.CONSOLE, level, verbose))
		{
			return;
		}
		string message2 = string.Format("[{0}] {1}", this.m_name, message);
		switch (level)
		{
		case Log.LogLevel.Debug:
		case Log.LogLevel.Info:
			Debug.Log(message2);
			return;
		case Log.LogLevel.Warning:
			Debug.LogWarning(message2);
			return;
		case Log.LogLevel.Error:
			Debug.LogError(message2);
			return;
		default:
			return;
		}
	}

	// Token: 0x06008882 RID: 34946 RVA: 0x002BF784 File Offset: 0x002BD984
	public void ScreenPrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.ScreenPrint(defaultLevel, false, format, args);
	}

	// Token: 0x06008883 RID: 34947 RVA: 0x002BF7A2 File Offset: 0x002BD9A2
	public void ScreenPrint(Log.LogLevel level, string format, params object[] args)
	{
		this.ScreenPrint(level, false, format, args);
	}

	// Token: 0x06008884 RID: 34948 RVA: 0x002BF7B0 File Offset: 0x002BD9B0
	public void ScreenPrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = this.GetDefaultLevel();
		this.ScreenPrint(defaultLevel, verbose, format, args);
	}

	// Token: 0x06008885 RID: 34949 RVA: 0x002BF7D0 File Offset: 0x002BD9D0
	public void ScreenPrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (!this.CanPrint(LogTarget.SCREEN, level, verbose))
		{
			return;
		}
		string message = GeneralUtils.SafeFormat(format, args);
		this.ScreenPrint(level, verbose, message);
	}

	// Token: 0x06008886 RID: 34950 RVA: 0x002BF7FC File Offset: 0x002BD9FC
	public void ForceScreenPrint(Log.LogLevel level, bool verbose, string message)
	{
		SceneDebugger sceneDebugger;
		if (!HearthstoneServices.TryGet<SceneDebugger>(out sceneDebugger))
		{
			return;
		}
		string message2 = string.Format("[{0}] {1}", this.m_name, message);
		sceneDebugger.AddMessage(level, message2, false);
	}

	// Token: 0x06008887 RID: 34951 RVA: 0x002BF830 File Offset: 0x002BDA30
	public void ScreenPrint(Log.LogLevel level, bool verbose, string message)
	{
		if (!this.CanPrint(LogTarget.SCREEN, level, verbose) && !this.CanPrint(LogTarget.CONSOLE, level, verbose))
		{
			return;
		}
		SceneDebugger sceneDebugger;
		if (!HearthstoneServices.TryGet<SceneDebugger>(out sceneDebugger))
		{
			return;
		}
		string message2 = string.Format("[{0}] {1}", this.m_name, message);
		sceneDebugger.AddMessage(level, message2, false);
	}

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x06008888 RID: 34952 RVA: 0x002BF87C File Offset: 0x002BDA7C
	public static string LogsPath
	{
		get
		{
			string result;
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				result = string.Format("{0}/{1}", FileUtils.ExternalDataPath, "Logs");
			}
			else if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				result = string.Format("{0}/{1}", FileUtils.PersistentDataPath, "Logs");
			}
			else
			{
				result = "Logs";
			}
			return result;
		}
	}

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x06008889 RID: 34953 RVA: 0x002BF8D0 File Offset: 0x002BDAD0
	public string FilePath
	{
		get
		{
			StreamWriter fileWriter = this.m_fileWriter;
			FileStream fileStream = (FileStream)((fileWriter != null) ? fileWriter.BaseStream : null);
			if (fileStream == null)
			{
				return null;
			}
			return fileStream.Name;
		}
	}

	// Token: 0x0600888A RID: 34954 RVA: 0x002BF8F4 File Offset: 0x002BDAF4
	private void InitFileWriter(bool append)
	{
		if (this.m_fileWriterInitialized)
		{
			return;
		}
		this.m_fileWriter = null;
		string logsPath = global::Logger.LogsPath;
		if (!Directory.Exists(logsPath))
		{
			try
			{
				Directory.CreateDirectory(logsPath);
			}
			catch (Exception)
			{
			}
		}
		string path = string.Format("{0}/{1}.{2}", logsPath, this.m_name, "log");
		try
		{
			this.m_fileWriter = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite));
			this.m_fileWriterInitialized = true;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600888B RID: 34955 RVA: 0x002BF980 File Offset: 0x002BDB80
	private string GetTimeOfDay()
	{
		if (HearthstoneApplication.IsMainThread)
		{
			int frameCount = Time.frameCount;
			if (frameCount != this.m_cachedTimeOfDay.FrameNumber)
			{
				this.m_cachedTimeOfDay.TimeOfDay = DateTime.Now.TimeOfDay.ToString();
				this.m_cachedTimeOfDay.FrameNumber = frameCount;
			}
		}
		else
		{
			this.m_cachedTimeOfDay.TimeOfDay = DateTime.Now.TimeOfDay.ToString();
		}
		return this.m_cachedTimeOfDay.TimeOfDay;
	}

	// Token: 0x0600888C RID: 34956 RVA: 0x002BFA10 File Offset: 0x002BDC10
	void Blizzard.T5.Core.ILogger.Log(LogLevel level, string format, params object[] args)
	{
		switch (level)
		{
		case LogLevel.Debug:
			this.Print(Log.LogLevel.Debug, format, args);
			return;
		case LogLevel.Information:
			this.Print(Log.LogLevel.Info, format, args);
			return;
		case LogLevel.Warning:
			this.Print(Log.LogLevel.Warning, format, args);
			return;
		case LogLevel.Error:
		case LogLevel.Critical:
			this.Print(Log.LogLevel.Error, format, args);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600888D RID: 34957 RVA: 0x002BFA60 File Offset: 0x002BDC60
	public void QueueMainThreadLog(LogLevel level, string format, params object[] args)
	{
		if (Processor.IsReady)
		{
			string userData = GeneralUtils.SafeFormat(format, args);
			switch (level)
			{
			case LogLevel.Debug:
				Processor.ScheduleCallback(0f, true, new Processor.ScheduledCallback(this.MainThreadDebug_CB), userData);
				return;
			case LogLevel.Information:
				Processor.ScheduleCallback(0f, true, new Processor.ScheduledCallback(this.MainThreadInfo_CB), userData);
				return;
			case LogLevel.Warning:
				Processor.ScheduleCallback(0f, true, new Processor.ScheduledCallback(this.MainThreadWarning_CB), userData);
				return;
			case LogLevel.Error:
			case LogLevel.Critical:
				Processor.ScheduleCallback(0f, true, new Processor.ScheduledCallback(this.MainThreadError_CB), userData);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x0600888E RID: 34958 RVA: 0x002BFB01 File Offset: 0x002BDD01
	private void MainThreadDebug_CB(object data)
	{
		this.Print(Log.LogLevel.Debug, data as string, Array.Empty<object>());
	}

	// Token: 0x0600888F RID: 34959 RVA: 0x002BFB15 File Offset: 0x002BDD15
	private void MainThreadInfo_CB(object data)
	{
		this.Print(Log.LogLevel.Info, data as string, Array.Empty<object>());
	}

	// Token: 0x06008890 RID: 34960 RVA: 0x002BFB29 File Offset: 0x002BDD29
	private void MainThreadWarning_CB(object data)
	{
		this.Print(Log.LogLevel.Warning, data as string, Array.Empty<object>());
	}

	// Token: 0x06008891 RID: 34961 RVA: 0x002BFB3D File Offset: 0x002BDD3D
	private void MainThreadError_CB(object data)
	{
		this.Print(Log.LogLevel.Error, data as string, Array.Empty<object>());
	}

	// Token: 0x040072C4 RID: 29380
	private const string OUTPUT_DIRECTORY_NAME = "Logs";

	// Token: 0x040072C5 RID: 29381
	private const string OUTPUT_FILE_EXTENSION = "log";

	// Token: 0x040072C6 RID: 29382
	private const int LOG_PREFIX_LENGTH = 20;

	// Token: 0x040072C7 RID: 29383
	private const int LOG_STRING_DEFAULT_LENGTH = 128;

	// Token: 0x040072C8 RID: 29384
	private string m_name;

	// Token: 0x040072C9 RID: 29385
	private StreamWriter m_fileWriter;

	// Token: 0x040072CA RID: 29386
	private bool m_fileWriterInitialized;

	// Token: 0x040072CB RID: 29387
	private StringBuilder m_stringBuilder;

	// Token: 0x040072CC RID: 29388
	private global::Logger.CachedTimeOfDay m_cachedTimeOfDay;

	// Token: 0x0200267A RID: 9850
	private struct CachedTimeOfDay
	{
		// Token: 0x0400F0B9 RID: 61625
		public string TimeOfDay;

		// Token: 0x0400F0BA RID: 61626
		public int FrameNumber;
	}
}
