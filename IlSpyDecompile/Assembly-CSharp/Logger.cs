using System;
using System.IO;
using System.Text;
using Blizzard.T5.Core;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class Logger : Blizzard.T5.Core.ILogger
{
	private struct CachedTimeOfDay
	{
		public string TimeOfDay;

		public int FrameNumber;
	}

	private const string OUTPUT_DIRECTORY_NAME = "Logs";

	private const string OUTPUT_FILE_EXTENSION = "log";

	private const int LOG_PREFIX_LENGTH = 20;

	private const int LOG_STRING_DEFAULT_LENGTH = 128;

	private string m_name;

	private StreamWriter m_fileWriter;

	private bool m_fileWriterInitialized;

	private StringBuilder m_stringBuilder;

	private CachedTimeOfDay m_cachedTimeOfDay;

	public static string LogsPath
	{
		get
		{
			string text = null;
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				return string.Format("{0}/{1}", FileUtils.ExternalDataPath, "Logs");
			}
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				return string.Format("{0}/{1}", FileUtils.PersistentDataPath, "Logs");
			}
			return "Logs";
		}
	}

	public string FilePath => ((FileStream)(m_fileWriter?.BaseStream))?.Name;

	public Logger(string name)
	{
		m_name = name;
		m_stringBuilder = new StringBuilder(128);
		Log.AllLoggers[name] = this;
	}

	public bool CanPrint(LogTarget target, Log.LogLevel level, bool verbose)
	{
		LogInfo logInfo = Log.Get().GetLogInfo(m_name);
		if (level == Log.LogLevel.Error && target != LogTarget.SCREEN && ShouldAlwaysPrintErrors())
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
		return target switch
		{
			LogTarget.CONSOLE => logInfo.m_consolePrinting, 
			LogTarget.SCREEN => logInfo.m_screenPrinting, 
			LogTarget.FILE => logInfo.m_filePrinting, 
			_ => false, 
		};
	}

	public bool CanPrint(Log.LogLevel level, bool? verbose = null)
	{
		LogInfo logInfo = Log.Get().GetLogInfo(m_name);
		if (level == Log.LogLevel.Error && ShouldAlwaysPrintErrors())
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
		if (verbose.HasValue && verbose.Value && !logInfo.m_verbose)
		{
			return false;
		}
		if (logInfo.m_consolePrinting)
		{
			return true;
		}
		if (logInfo.m_screenPrinting)
		{
			return true;
		}
		if (logInfo.m_filePrinting)
		{
			return true;
		}
		return false;
	}

	public bool CanPrint()
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		return CanPrint(defaultLevel, false);
	}

	public Log.LogLevel GetDefaultLevel()
	{
		return Log.Get().GetLogInfo(m_name)?.m_defaultLevel ?? Log.LogLevel.Debug;
	}

	public bool ShouldAlwaysPrintErrors()
	{
		return Log.Get().GetLogInfo(m_name)?.m_alwaysPrintErrors ?? true;
	}

	public bool IsVerbose()
	{
		return Log.Get().GetLogInfo(m_name)?.m_verbose ?? false;
	}

	public void Print(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		Print(defaultLevel, verbose: false, format, args);
	}

	public void Print(Log.LogLevel level, string format, params object[] args)
	{
		Print(level, verbose: false, format, args);
	}

	public void Print(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		Print(defaultLevel, verbose, format, args);
	}

	public void Print(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (CanPrint(level, verbose))
		{
			string message = GeneralUtils.SafeFormat(format, args);
			Print(level, verbose, message);
		}
	}

	public void Print(Log.LogLevel level, bool verbose, string message)
	{
		FilePrint(level, verbose, message);
		ConsolePrint(level, verbose, message);
		ScreenPrint(level, verbose, message);
	}

	public void PrintAndForcePrintToScreen(Log.LogLevel level, bool verbose, string message)
	{
		FilePrint(level, verbose, message);
		ConsolePrint(level, verbose, message);
		ForceScreenPrint(level, verbose, message);
	}

	public void PrintDebug(string format, params object[] args)
	{
		PrintDebug(verbose: false, format, args);
	}

	public void PrintDebug(bool verbose, string format, params object[] args)
	{
		Print(Log.LogLevel.Debug, verbose, format, args);
	}

	public void PrintInfo(string format, params object[] args)
	{
		PrintInfo(verbose: false, format, args);
	}

	public void PrintInfo(bool verbose, string format, params object[] args)
	{
		Print(Log.LogLevel.Info, verbose, format, args);
	}

	public void PrintWarning(string format, params object[] args)
	{
		PrintWarning(verbose: false, format, args);
	}

	public void PrintWarning(bool verbose, string format, params object[] args)
	{
		Print(Log.LogLevel.Warning, verbose, format, args);
	}

	public void PrintError(string format, params object[] args)
	{
		PrintError(verbose: false, format, args);
	}

	public void PrintError(bool verbose, string format, params object[] args)
	{
		Print(Log.LogLevel.Error, verbose, format, args);
	}

	public void ForceFilePrint(Log.LogLevel level, string message)
	{
		FilePrint(level, verbose: false, message, forced: true, printContext: false);
	}

	public void FilePrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		FilePrint(defaultLevel, verbose: false, format, args);
	}

	public void FilePrint(Log.LogLevel level, string format, params object[] args)
	{
		FilePrint(level, verbose: false, format, args);
	}

	public void FilePrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		FilePrint(defaultLevel, verbose, format, args);
	}

	public void FilePrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (CanPrint(LogTarget.FILE, level, verbose))
		{
			string message = GeneralUtils.SafeFormat(format, args);
			FilePrint(level, verbose, message);
		}
	}

	public void FilePrint(Log.LogLevel level, bool verbose, string message, bool forced = false, bool printContext = true)
	{
		if (!forced && !CanPrint(LogTarget.FILE, level, verbose))
		{
			return;
		}
		InitFileWriter(append: false);
		if (m_fileWriter == null)
		{
			return;
		}
		m_stringBuilder.Clear();
		m_stringBuilder.EnsureCapacity(message.Length + 20);
		if (printContext)
		{
			switch (level)
			{
			case Log.LogLevel.Debug:
				m_stringBuilder.Append("D ");
				break;
			case Log.LogLevel.Info:
				m_stringBuilder.Append("I ");
				break;
			case Log.LogLevel.Warning:
				m_stringBuilder.Append("W ");
				break;
			case Log.LogLevel.Error:
				m_stringBuilder.Append("E ");
				break;
			}
			m_stringBuilder.Append(GetTimeOfDay());
			m_stringBuilder.Append(" ");
		}
		m_stringBuilder.Append(message);
		m_fileWriter.WriteLine(m_stringBuilder.ToString());
		FlushContent();
	}

	public void FilePrintRaw(string rawText)
	{
		InitFileWriter(append: false);
		if (m_fileWriter != null)
		{
			m_fileWriter.Write(rawText);
			FlushContent();
		}
	}

	public void PurgeFile()
	{
		string logsPath = LogsPath;
		string path = string.Format("{0}/{1}.{2}", logsPath, m_name, "log");
		if (File.Exists(path))
		{
			if (m_fileWriterInitialized)
			{
				m_fileWriter.Dispose();
				m_fileWriterInitialized = false;
			}
			File.Delete(path);
		}
	}

	public void FlushContent()
	{
		if (m_fileWriterInitialized && m_fileWriter != null)
		{
			try
			{
				m_fileWriter.Flush();
			}
			catch (Exception ex)
			{
				ConsolePrint(Log.LogLevel.Error, ex.Message);
				CloseFileWriter();
			}
		}
	}

	public void CloseFileWriter()
	{
		try
		{
			m_fileWriter.Close();
		}
		catch
		{
		}
		finally
		{
			m_fileWriter = null;
		}
	}

	public string GetContent()
	{
		if (m_fileWriterInitialized)
		{
			m_fileWriter.Close();
			m_fileWriterInitialized = false;
		}
		string result = File.ReadAllText(string.Format("{0}/{1}.{2}", LogsPath, m_name, "log"));
		InitFileWriter(append: true);
		return result;
	}

	public void ConsolePrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		ConsolePrint(defaultLevel, verbose: false, format, args);
	}

	public void ConsolePrint(Log.LogLevel level, string format, params object[] args)
	{
		ConsolePrint(level, verbose: false, format, args);
	}

	public void ConsolePrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		ConsolePrint(defaultLevel, verbose, format, args);
	}

	public void ConsolePrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (CanPrint(LogTarget.CONSOLE, level, verbose))
		{
			string message = GeneralUtils.SafeFormat(format, args);
			ConsolePrint(level, verbose, message);
		}
	}

	public void ConsolePrint(Log.LogLevel level, bool verbose, string message)
	{
		if (CanPrint(LogTarget.CONSOLE, level, verbose))
		{
			string message2 = $"[{m_name}] {message}";
			switch (level)
			{
			case Log.LogLevel.Debug:
			case Log.LogLevel.Info:
				Debug.Log(message2);
				break;
			case Log.LogLevel.Warning:
				Debug.LogWarning(message2);
				break;
			case Log.LogLevel.Error:
				Debug.LogError(message2);
				break;
			}
		}
	}

	public void ScreenPrint(string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		ScreenPrint(defaultLevel, verbose: false, format, args);
	}

	public void ScreenPrint(Log.LogLevel level, string format, params object[] args)
	{
		ScreenPrint(level, verbose: false, format, args);
	}

	public void ScreenPrint(bool verbose, string format, params object[] args)
	{
		Log.LogLevel defaultLevel = GetDefaultLevel();
		ScreenPrint(defaultLevel, verbose, format, args);
	}

	public void ScreenPrint(Log.LogLevel level, bool verbose, string format, params object[] args)
	{
		if (CanPrint(LogTarget.SCREEN, level, verbose))
		{
			string message = GeneralUtils.SafeFormat(format, args);
			ScreenPrint(level, verbose, message);
		}
	}

	public void ForceScreenPrint(Log.LogLevel level, bool verbose, string message)
	{
		if (HearthstoneServices.TryGet<SceneDebugger>(out var service))
		{
			string message2 = $"[{m_name}] {message}";
			service.AddMessage(level, message2);
		}
	}

	public void ScreenPrint(Log.LogLevel level, bool verbose, string message)
	{
		if ((CanPrint(LogTarget.SCREEN, level, verbose) || CanPrint(LogTarget.CONSOLE, level, verbose)) && HearthstoneServices.TryGet<SceneDebugger>(out var service))
		{
			string message2 = $"[{m_name}] {message}";
			service.AddMessage(level, message2);
		}
	}

	private void InitFileWriter(bool append)
	{
		if (m_fileWriterInitialized)
		{
			return;
		}
		m_fileWriter = null;
		string logsPath = LogsPath;
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
		string path = string.Format("{0}/{1}.{2}", logsPath, m_name, "log");
		try
		{
			m_fileWriter = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite));
			m_fileWriterInitialized = true;
		}
		catch (Exception)
		{
		}
	}

	private string GetTimeOfDay()
	{
		if (HearthstoneApplication.IsMainThread)
		{
			int frameCount = Time.frameCount;
			if (frameCount != m_cachedTimeOfDay.FrameNumber)
			{
				m_cachedTimeOfDay.TimeOfDay = DateTime.Now.TimeOfDay.ToString();
				m_cachedTimeOfDay.FrameNumber = frameCount;
			}
		}
		else
		{
			m_cachedTimeOfDay.TimeOfDay = DateTime.Now.TimeOfDay.ToString();
		}
		return m_cachedTimeOfDay.TimeOfDay;
	}

	void Blizzard.T5.Core.ILogger.Log(LogLevel level, string format, params object[] args)
	{
		switch (level)
		{
		case LogLevel.Debug:
			Print(Log.LogLevel.Debug, format, args);
			break;
		case LogLevel.Information:
			Print(Log.LogLevel.Info, format, args);
			break;
		case LogLevel.Warning:
			Print(Log.LogLevel.Warning, format, args);
			break;
		case LogLevel.Error:
		case LogLevel.Critical:
			Print(Log.LogLevel.Error, format, args);
			break;
		}
	}

	public void QueueMainThreadLog(LogLevel level, string format, params object[] args)
	{
		if (Processor.IsReady)
		{
			string userData = GeneralUtils.SafeFormat(format, args);
			switch (level)
			{
			case LogLevel.Debug:
				Processor.ScheduleCallback(0f, realTime: true, MainThreadDebug_CB, userData);
				break;
			case LogLevel.Information:
				Processor.ScheduleCallback(0f, realTime: true, MainThreadInfo_CB, userData);
				break;
			case LogLevel.Warning:
				Processor.ScheduleCallback(0f, realTime: true, MainThreadWarning_CB, userData);
				break;
			case LogLevel.Error:
			case LogLevel.Critical:
				Processor.ScheduleCallback(0f, realTime: true, MainThreadError_CB, userData);
				break;
			}
		}
	}

	private void MainThreadDebug_CB(object data)
	{
		Print(Log.LogLevel.Debug, data as string);
	}

	private void MainThreadInfo_CB(object data)
	{
		Print(Log.LogLevel.Info, data as string);
	}

	private void MainThreadWarning_CB(object data)
	{
		Print(Log.LogLevel.Warning, data as string);
	}

	private void MainThreadError_CB(object data)
	{
		Print(Log.LogLevel.Error, data as string);
	}
}
