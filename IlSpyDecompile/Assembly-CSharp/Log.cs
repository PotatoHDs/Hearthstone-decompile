using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Blizzard.T5.Core;
using UnityEngine;

public class Log
{
	public enum LogLevel
	{
		[Description("None")]
		None,
		[Description("Debug")]
		Debug,
		[Description("Info")]
		Info,
		[Description("Warning")]
		Warning,
		[Description("Error")]
		Error
	}

	public static Dictionary<string, Logger> AllLoggers = new Dictionary<string, Logger>();

	public static Logger All = new Logger("All");

	public static Logger AchievementManager = new Logger("AchievementManager");

	public static Logger Achievements = new Logger("Achievements");

	public static Logger AdTracking = new Logger("AdTracking");

	public static Logger Adventures = new Logger("Adventures");

	public static Logger Arena = new Logger("Arena");

	public static Logger Asset = new Logger("Asset");

	public static Logger AsyncLoading = new Logger("AsyncLoading");

	public static Logger BattleNet = new Logger("BattleNet");

	public static Logger BIReport = new Logger("BIReport");

	public static Logger Box = new Logger("Box");

	public static Logger BreakingNews = new Logger("BreakingNews");

	public static Logger BugReporter = new Logger("BugReporter");

	public static Logger CardbackMgr = new Logger("CardbackMgr");

	public static Logger ChangedCards = new Logger("ChangedCards");

	public static Logger ClientRequestManager = new Logger("ClientRequestManager");

	public static Logger CloudStorage = new Logger("CloudStorage");

	public static Logger CollectionDeckBox = new Logger("CollectionDeckBox");

	public static Logger CollectionManager = new Logger("CollectionManager");

	public static Logger CoinManager = new Logger("CoinManager");

	public static Logger ConfigFile = new Logger("ConfigFile");

	public static Logger ContentConnect = new Logger("ContentConnect");

	public static Logger Crafting = new Logger("Crafting");

	public static Logger CRM = new Logger("CRM");

	public static Logger Dbf = new Logger("Dbf");

	public static Logger DeckHelper = new Logger("DeckHelper");

	public static Logger DeckRuleset = new Logger("DeckRuleset");

	public static Logger Decks = new Logger("Decks");

	public static Logger DeckTray = new Logger("DeckTray");

	public static Logger DeepLink = new Logger("DeepLink");

	public static Logger DelayedReporter = new Logger("DelayedReporter");

	public static Logger DeviceEmulation = new Logger("DeviceEmulation");

	public static Logger Downloader = new Logger("Downloader");

	public static Logger EndOfGame = new Logger("EndOfGame");

	public static Logger ErrorReporter = new Logger("ErrorReporter");

	public static Logger EventTable = new Logger("EventTable");

	public static Logger EventTiming = new Logger("EventTiming");

	public static Logger ExceptionReporter = new Logger("ExceptionReporter");

	public static Logger FaceDownCard = new Logger("FaceDownCard");

	public static Logger FiresideGatherings = new Logger("FiresideGatherings");

	public static Logger FlowPerformance = new Logger("FlowPerformance");

	public static Logger Font = new Logger("Font");

	public static Logger FullScreenFX = new Logger("FullScreenFX");

	public static Logger GameMgr = new Logger("GameMgr");

	public static Logger Gameplay = new Logger("Gameplay");

	public static Logger Graphics = new Logger("Graphics");

	public static Logger Hand = new Logger("Hand");

	public static Logger InGameBrowser = new Logger("InGameBrowser");

	public static Logger InGameMessage = new Logger("InGameMessage");

	public static Logger InnKeepersSpecial = new Logger("InnKeepersSpecial");

	public static Logger Jobs = new Logger("Jobs");

	public static Logger LoadingScreen = new Logger("LoadingScreen");

	public static Logger Login = new Logger("Login");

	public static Logger MissingAssets = new Logger("MissingAssets");

	public static Logger MobileCallback = new Logger("MobileCallback");

	public static Logger MulliganManager = new Logger("MulliganManager");

	public static Logger NarrativeManager = new Logger("NarrativeManager");

	public static Logger Net = new Logger("Net");

	public static Logger Notifications = new Logger("Notifications");

	public static Logger Offline = new Logger("Offline");

	public static Logger Options = new Logger("Options");

	public static Logger Packets = new Logger("Packet");

	public static Logger Party = new Logger("Party");

	public static Logger Performance = new Logger("Performance");

	public static Logger PlayErrors = new Logger("PlayErrors");

	public static Logger PlayModeInvestigation = new Logger("PlayModeInvestigation");

	public static Logger Power = new Logger("Power");

	public static Logger Presence = new Logger("Presence");

	public static Logger PVPDR = new Logger("PVPDR");

	public static Logger RAF = new Logger("RAF");

	public static Logger ReturningPlayer = new Logger("ReturningPlayer");

	public static Logger Replay = new Logger("Replay");

	public static Logger Reset = new Logger("Reset");

	public static Logger RewardBox = new Logger("RewardBox");

	public static Logger Services = new Logger("Services");

	public static Logger SmartDiscover = new Logger("SmartDiscover");

	public static Logger Spells = new Logger("Spells");

	public static Logger Sound = new Logger("Sound");

	public static Logger Spectator = new Logger("Spectator");

	public static Logger Store = new Logger("Store");

	public static Logger Tag = new Logger("Tag");

	public static Logger TavernBrawl = new Logger("TavernBrawl");

	public static Logger Telemetry = new Logger("Telemetry");

	public static Logger TemporaryAccount = new Logger("TemporaryAccount");

	public static Logger UberText = new Logger("UberText");

	public static Logger UIStatus = new Logger("UIStatus");

	public static Logger UserAttention = new Logger("UserAttention");

	public static Logger W8Touch = new Logger("W8Touch");

	public static Logger Zone = new Logger("Zone");

	private const string CONFIG_FILE_NAME = "log.config";

	private readonly LogInfo[] DEFAULT_LOG_INFOS = new LogInfo[5]
	{
		new LogInfo
		{
			m_name = "Jobs",
			m_consolePrinting = true,
			m_minLevel = LogLevel.Error
		},
		new LogInfo
		{
			m_name = "Downloader",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = LogLevel.Info
		},
		new LogInfo
		{
			m_name = "Login",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = LogLevel.Info
		},
		new LogInfo
		{
			m_name = "ExceptionReporter",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = LogLevel.Info
		},
		new LogInfo
		{
			m_name = "Offline",
			m_filePrinting = true,
			m_minLevel = LogLevel.Info
		}
	};

	private static Log s_instance;

	private Map<string, LogInfo> m_logInfos = new Map<string, LogInfo>();

	public static string ConfigPath
	{
		get
		{
			string text = null;
			if (1 == 3)
			{
				text = string.Format("{0}/{1}", Application.persistentDataPath, "log.config");
				if (!File.Exists(text))
				{
					text = FileUtils.GetAssetPath("log.config", useAssetBundleFolder: false);
				}
			}
			else
			{
				text = string.Format("{0}/{1}", FileUtils.ExternalDataPath, "log.config");
				if (!File.Exists(text))
				{
					text = string.Format("{0}/{1}", FileUtils.PersistentDataPath, "log.config");
					if (!File.Exists(text))
					{
						text = FileUtils.GetAssetPath("log.config", useAssetBundleFolder: false);
					}
				}
			}
			return text;
		}
	}

	public static Log Get()
	{
		if (s_instance == null)
		{
			s_instance = new Log();
			s_instance.Initialize();
		}
		return s_instance;
	}

	public void Load()
	{
		string configPath = ConfigPath;
		if (File.Exists(configPath))
		{
			m_logInfos.Clear();
			LoadConfig(configPath);
		}
		LogInfo[] dEFAULT_LOG_INFOS = DEFAULT_LOG_INFOS;
		foreach (LogInfo logInfo in dEFAULT_LOG_INFOS)
		{
			if (!m_logInfos.ContainsKey(logInfo.m_name))
			{
				m_logInfos.Add(logInfo.m_name, logInfo);
			}
		}
		ConfigFile.Print("log.config location: " + configPath);
	}

	public LogInfo GetLogInfo(string name)
	{
		LogInfo value = null;
		m_logInfos.TryGetValue(name, out value);
		return value;
	}

	public void AddLogInfo(string name)
	{
		LogInfo value = null;
		if (!m_logInfos.TryGetValue(name, out value))
		{
			value = new LogInfo
			{
				m_name = name
			};
			m_logInfos.Add(value.m_name, value);
		}
		value.m_filePrinting = true;
		value.m_screenPrinting = true;
	}

	public IEnumerable<string> GetEnabledLogNames()
	{
		return m_logInfos.Keys;
	}

	public IEnumerable<string> GetDefaultLogNames()
	{
		List<string> list = new List<string>();
		LogInfo[] dEFAULT_LOG_INFOS = DEFAULT_LOG_INFOS;
		foreach (LogInfo logInfo in dEFAULT_LOG_INFOS)
		{
			list.Add(logInfo.m_name);
		}
		return list;
	}

	private void Initialize()
	{
		Load();
	}

	private void LoadConfig(string path)
	{
		ConfigFile configFile = new ConfigFile();
		if (!configFile.LightLoad(path))
		{
			return;
		}
		foreach (ConfigFile.Line line in configFile.GetLines())
		{
			string sectionName = line.m_sectionName;
			string lineKey = line.m_lineKey;
			string value = line.m_value;
			if (!m_logInfos.TryGetValue(sectionName, out var value2))
			{
				value2 = new LogInfo
				{
					m_name = sectionName
				};
				m_logInfos.Add(value2.m_name, value2);
			}
			if (lineKey.Equals("ConsolePrinting", StringComparison.OrdinalIgnoreCase))
			{
				value2.m_consolePrinting = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("ScreenPrinting", StringComparison.OrdinalIgnoreCase))
			{
				value2.m_screenPrinting = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("FilePrinting", StringComparison.OrdinalIgnoreCase))
			{
				value2.m_filePrinting = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("MinLevel", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					LogLevel logLevel = (value2.m_minLevel = EnumUtils.GetEnum<LogLevel>(value, StringComparison.OrdinalIgnoreCase));
				}
				catch (ArgumentException)
				{
				}
			}
			else if (lineKey.Equals("DefaultLevel", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					LogLevel logLevel2 = (value2.m_defaultLevel = EnumUtils.GetEnum<LogLevel>(value, StringComparison.OrdinalIgnoreCase));
				}
				catch (ArgumentException)
				{
				}
			}
			else if (lineKey.Equals("AlwaysPrintErrors", StringComparison.OrdinalIgnoreCase))
			{
				value2.m_alwaysPrintErrors = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("Verbose", StringComparison.OrdinalIgnoreCase))
			{
				value2.m_verbose = GeneralUtils.ForceBool(value);
			}
		}
	}
}
