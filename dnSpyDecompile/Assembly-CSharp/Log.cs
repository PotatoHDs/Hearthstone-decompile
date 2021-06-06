using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020009C2 RID: 2498
public class Log
{
	// Token: 0x06008852 RID: 34898 RVA: 0x002BE6E9 File Offset: 0x002BC8E9
	public static Log Get()
	{
		if (Log.s_instance == null)
		{
			Log.s_instance = new Log();
			Log.s_instance.Initialize();
		}
		return Log.s_instance;
	}

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x06008853 RID: 34899 RVA: 0x002BE70C File Offset: 0x002BC90C
	public static string ConfigPath
	{
		get
		{
			string text;
			if (1 == 3)
			{
				text = string.Format("{0}/{1}", Application.persistentDataPath, "log.config");
				if (!File.Exists(text))
				{
					text = FileUtils.GetAssetPath("log.config", false);
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
						text = FileUtils.GetAssetPath("log.config", false);
					}
				}
			}
			return text;
		}
	}

	// Token: 0x06008854 RID: 34900 RVA: 0x002BE794 File Offset: 0x002BC994
	public void Load()
	{
		string configPath = Log.ConfigPath;
		if (File.Exists(configPath))
		{
			this.m_logInfos.Clear();
			this.LoadConfig(configPath);
		}
		foreach (LogInfo logInfo in this.DEFAULT_LOG_INFOS)
		{
			if (!this.m_logInfos.ContainsKey(logInfo.m_name))
			{
				this.m_logInfos.Add(logInfo.m_name, logInfo);
			}
		}
		Log.ConfigFile.Print("log.config location: " + configPath, Array.Empty<object>());
	}

	// Token: 0x06008855 RID: 34901 RVA: 0x002BE81C File Offset: 0x002BCA1C
	public LogInfo GetLogInfo(string name)
	{
		LogInfo result = null;
		this.m_logInfos.TryGetValue(name, out result);
		return result;
	}

	// Token: 0x06008856 RID: 34902 RVA: 0x002BE83C File Offset: 0x002BCA3C
	public void AddLogInfo(string name)
	{
		LogInfo logInfo = null;
		if (!this.m_logInfos.TryGetValue(name, out logInfo))
		{
			logInfo = new LogInfo
			{
				m_name = name
			};
			this.m_logInfos.Add(logInfo.m_name, logInfo);
		}
		logInfo.m_filePrinting = true;
		logInfo.m_screenPrinting = true;
	}

	// Token: 0x06008857 RID: 34903 RVA: 0x002BE888 File Offset: 0x002BCA88
	public IEnumerable<string> GetEnabledLogNames()
	{
		return this.m_logInfos.Keys;
	}

	// Token: 0x06008858 RID: 34904 RVA: 0x002BE898 File Offset: 0x002BCA98
	public IEnumerable<string> GetDefaultLogNames()
	{
		List<string> list = new List<string>();
		foreach (LogInfo logInfo in this.DEFAULT_LOG_INFOS)
		{
			list.Add(logInfo.m_name);
		}
		return list;
	}

	// Token: 0x06008859 RID: 34905 RVA: 0x002BE8D1 File Offset: 0x002BCAD1
	private void Initialize()
	{
		this.Load();
	}

	// Token: 0x0600885A RID: 34906 RVA: 0x002BE8DC File Offset: 0x002BCADC
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
			LogInfo logInfo;
			if (!this.m_logInfos.TryGetValue(sectionName, out logInfo))
			{
				logInfo = new LogInfo
				{
					m_name = sectionName
				};
				this.m_logInfos.Add(logInfo.m_name, logInfo);
			}
			if (lineKey.Equals("ConsolePrinting", StringComparison.OrdinalIgnoreCase))
			{
				logInfo.m_consolePrinting = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("ScreenPrinting", StringComparison.OrdinalIgnoreCase))
			{
				logInfo.m_screenPrinting = GeneralUtils.ForceBool(value);
			}
			else if (lineKey.Equals("FilePrinting", StringComparison.OrdinalIgnoreCase))
			{
				logInfo.m_filePrinting = GeneralUtils.ForceBool(value);
			}
			else
			{
				if (lineKey.Equals("MinLevel", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						Log.LogLevel @enum = EnumUtils.GetEnum<Log.LogLevel>(value, StringComparison.OrdinalIgnoreCase);
						logInfo.m_minLevel = @enum;
						continue;
					}
					catch (ArgumentException)
					{
						continue;
					}
				}
				if (lineKey.Equals("DefaultLevel", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						Log.LogLevel enum2 = EnumUtils.GetEnum<Log.LogLevel>(value, StringComparison.OrdinalIgnoreCase);
						logInfo.m_defaultLevel = enum2;
						continue;
					}
					catch (ArgumentException)
					{
						continue;
					}
				}
				if (lineKey.Equals("AlwaysPrintErrors", StringComparison.OrdinalIgnoreCase))
				{
					logInfo.m_alwaysPrintErrors = GeneralUtils.ForceBool(value);
				}
				else if (lineKey.Equals("Verbose", StringComparison.OrdinalIgnoreCase))
				{
					logInfo.m_verbose = GeneralUtils.ForceBool(value);
				}
			}
		}
	}

	// Token: 0x04007266 RID: 29286
	public static Dictionary<string, global::Logger> AllLoggers = new Dictionary<string, global::Logger>();

	// Token: 0x04007267 RID: 29287
	public static global::Logger All = new global::Logger("All");

	// Token: 0x04007268 RID: 29288
	public static global::Logger AchievementManager = new global::Logger("AchievementManager");

	// Token: 0x04007269 RID: 29289
	public static global::Logger Achievements = new global::Logger("Achievements");

	// Token: 0x0400726A RID: 29290
	public static global::Logger AdTracking = new global::Logger("AdTracking");

	// Token: 0x0400726B RID: 29291
	public static global::Logger Adventures = new global::Logger("Adventures");

	// Token: 0x0400726C RID: 29292
	public static global::Logger Arena = new global::Logger("Arena");

	// Token: 0x0400726D RID: 29293
	public static global::Logger Asset = new global::Logger("Asset");

	// Token: 0x0400726E RID: 29294
	public static global::Logger AsyncLoading = new global::Logger("AsyncLoading");

	// Token: 0x0400726F RID: 29295
	public static global::Logger BattleNet = new global::Logger("BattleNet");

	// Token: 0x04007270 RID: 29296
	public static global::Logger BIReport = new global::Logger("BIReport");

	// Token: 0x04007271 RID: 29297
	public static global::Logger Box = new global::Logger("Box");

	// Token: 0x04007272 RID: 29298
	public static global::Logger BreakingNews = new global::Logger("BreakingNews");

	// Token: 0x04007273 RID: 29299
	public static global::Logger BugReporter = new global::Logger("BugReporter");

	// Token: 0x04007274 RID: 29300
	public static global::Logger CardbackMgr = new global::Logger("CardbackMgr");

	// Token: 0x04007275 RID: 29301
	public static global::Logger ChangedCards = new global::Logger("ChangedCards");

	// Token: 0x04007276 RID: 29302
	public static global::Logger ClientRequestManager = new global::Logger("ClientRequestManager");

	// Token: 0x04007277 RID: 29303
	public static global::Logger CloudStorage = new global::Logger("CloudStorage");

	// Token: 0x04007278 RID: 29304
	public static global::Logger CollectionDeckBox = new global::Logger("CollectionDeckBox");

	// Token: 0x04007279 RID: 29305
	public static global::Logger CollectionManager = new global::Logger("CollectionManager");

	// Token: 0x0400727A RID: 29306
	public static global::Logger CoinManager = new global::Logger("CoinManager");

	// Token: 0x0400727B RID: 29307
	public static global::Logger ConfigFile = new global::Logger("ConfigFile");

	// Token: 0x0400727C RID: 29308
	public static global::Logger ContentConnect = new global::Logger("ContentConnect");

	// Token: 0x0400727D RID: 29309
	public static global::Logger Crafting = new global::Logger("Crafting");

	// Token: 0x0400727E RID: 29310
	public static global::Logger CRM = new global::Logger("CRM");

	// Token: 0x0400727F RID: 29311
	public static global::Logger Dbf = new global::Logger("Dbf");

	// Token: 0x04007280 RID: 29312
	public static global::Logger DeckHelper = new global::Logger("DeckHelper");

	// Token: 0x04007281 RID: 29313
	public static global::Logger DeckRuleset = new global::Logger("DeckRuleset");

	// Token: 0x04007282 RID: 29314
	public static global::Logger Decks = new global::Logger("Decks");

	// Token: 0x04007283 RID: 29315
	public static global::Logger DeckTray = new global::Logger("DeckTray");

	// Token: 0x04007284 RID: 29316
	public static global::Logger DeepLink = new global::Logger("DeepLink");

	// Token: 0x04007285 RID: 29317
	public static global::Logger DelayedReporter = new global::Logger("DelayedReporter");

	// Token: 0x04007286 RID: 29318
	public static global::Logger DeviceEmulation = new global::Logger("DeviceEmulation");

	// Token: 0x04007287 RID: 29319
	public static global::Logger Downloader = new global::Logger("Downloader");

	// Token: 0x04007288 RID: 29320
	public static global::Logger EndOfGame = new global::Logger("EndOfGame");

	// Token: 0x04007289 RID: 29321
	public static global::Logger ErrorReporter = new global::Logger("ErrorReporter");

	// Token: 0x0400728A RID: 29322
	public static global::Logger EventTable = new global::Logger("EventTable");

	// Token: 0x0400728B RID: 29323
	public static global::Logger EventTiming = new global::Logger("EventTiming");

	// Token: 0x0400728C RID: 29324
	public static global::Logger ExceptionReporter = new global::Logger("ExceptionReporter");

	// Token: 0x0400728D RID: 29325
	public static global::Logger FaceDownCard = new global::Logger("FaceDownCard");

	// Token: 0x0400728E RID: 29326
	public static global::Logger FiresideGatherings = new global::Logger("FiresideGatherings");

	// Token: 0x0400728F RID: 29327
	public static global::Logger FlowPerformance = new global::Logger("FlowPerformance");

	// Token: 0x04007290 RID: 29328
	public static global::Logger Font = new global::Logger("Font");

	// Token: 0x04007291 RID: 29329
	public static global::Logger FullScreenFX = new global::Logger("FullScreenFX");

	// Token: 0x04007292 RID: 29330
	public static global::Logger GameMgr = new global::Logger("GameMgr");

	// Token: 0x04007293 RID: 29331
	public static global::Logger Gameplay = new global::Logger("Gameplay");

	// Token: 0x04007294 RID: 29332
	public static global::Logger Graphics = new global::Logger("Graphics");

	// Token: 0x04007295 RID: 29333
	public static global::Logger Hand = new global::Logger("Hand");

	// Token: 0x04007296 RID: 29334
	public static global::Logger InGameBrowser = new global::Logger("InGameBrowser");

	// Token: 0x04007297 RID: 29335
	public static global::Logger InGameMessage = new global::Logger("InGameMessage");

	// Token: 0x04007298 RID: 29336
	public static global::Logger InnKeepersSpecial = new global::Logger("InnKeepersSpecial");

	// Token: 0x04007299 RID: 29337
	public static global::Logger Jobs = new global::Logger("Jobs");

	// Token: 0x0400729A RID: 29338
	public static global::Logger LoadingScreen = new global::Logger("LoadingScreen");

	// Token: 0x0400729B RID: 29339
	public static global::Logger Login = new global::Logger("Login");

	// Token: 0x0400729C RID: 29340
	public static global::Logger MissingAssets = new global::Logger("MissingAssets");

	// Token: 0x0400729D RID: 29341
	public static global::Logger MobileCallback = new global::Logger("MobileCallback");

	// Token: 0x0400729E RID: 29342
	public static global::Logger MulliganManager = new global::Logger("MulliganManager");

	// Token: 0x0400729F RID: 29343
	public static global::Logger NarrativeManager = new global::Logger("NarrativeManager");

	// Token: 0x040072A0 RID: 29344
	public static global::Logger Net = new global::Logger("Net");

	// Token: 0x040072A1 RID: 29345
	public static global::Logger Notifications = new global::Logger("Notifications");

	// Token: 0x040072A2 RID: 29346
	public static global::Logger Offline = new global::Logger("Offline");

	// Token: 0x040072A3 RID: 29347
	public static global::Logger Options = new global::Logger("Options");

	// Token: 0x040072A4 RID: 29348
	public static global::Logger Packets = new global::Logger("Packet");

	// Token: 0x040072A5 RID: 29349
	public static global::Logger Party = new global::Logger("Party");

	// Token: 0x040072A6 RID: 29350
	public static global::Logger Performance = new global::Logger("Performance");

	// Token: 0x040072A7 RID: 29351
	public static global::Logger PlayErrors = new global::Logger("PlayErrors");

	// Token: 0x040072A8 RID: 29352
	public static global::Logger PlayModeInvestigation = new global::Logger("PlayModeInvestigation");

	// Token: 0x040072A9 RID: 29353
	public static global::Logger Power = new global::Logger("Power");

	// Token: 0x040072AA RID: 29354
	public static global::Logger Presence = new global::Logger("Presence");

	// Token: 0x040072AB RID: 29355
	public static global::Logger PVPDR = new global::Logger("PVPDR");

	// Token: 0x040072AC RID: 29356
	public static global::Logger RAF = new global::Logger("RAF");

	// Token: 0x040072AD RID: 29357
	public static global::Logger ReturningPlayer = new global::Logger("ReturningPlayer");

	// Token: 0x040072AE RID: 29358
	public static global::Logger Replay = new global::Logger("Replay");

	// Token: 0x040072AF RID: 29359
	public static global::Logger Reset = new global::Logger("Reset");

	// Token: 0x040072B0 RID: 29360
	public static global::Logger RewardBox = new global::Logger("RewardBox");

	// Token: 0x040072B1 RID: 29361
	public static global::Logger Services = new global::Logger("Services");

	// Token: 0x040072B2 RID: 29362
	public static global::Logger SmartDiscover = new global::Logger("SmartDiscover");

	// Token: 0x040072B3 RID: 29363
	public static global::Logger Spells = new global::Logger("Spells");

	// Token: 0x040072B4 RID: 29364
	public static global::Logger Sound = new global::Logger("Sound");

	// Token: 0x040072B5 RID: 29365
	public static global::Logger Spectator = new global::Logger("Spectator");

	// Token: 0x040072B6 RID: 29366
	public static global::Logger Store = new global::Logger("Store");

	// Token: 0x040072B7 RID: 29367
	public static global::Logger Tag = new global::Logger("Tag");

	// Token: 0x040072B8 RID: 29368
	public static global::Logger TavernBrawl = new global::Logger("TavernBrawl");

	// Token: 0x040072B9 RID: 29369
	public static global::Logger Telemetry = new global::Logger("Telemetry");

	// Token: 0x040072BA RID: 29370
	public static global::Logger TemporaryAccount = new global::Logger("TemporaryAccount");

	// Token: 0x040072BB RID: 29371
	public static global::Logger UberText = new global::Logger("UberText");

	// Token: 0x040072BC RID: 29372
	public static global::Logger UIStatus = new global::Logger("UIStatus");

	// Token: 0x040072BD RID: 29373
	public static global::Logger UserAttention = new global::Logger("UserAttention");

	// Token: 0x040072BE RID: 29374
	public static global::Logger W8Touch = new global::Logger("W8Touch");

	// Token: 0x040072BF RID: 29375
	public static global::Logger Zone = new global::Logger("Zone");

	// Token: 0x040072C0 RID: 29376
	private const string CONFIG_FILE_NAME = "log.config";

	// Token: 0x040072C1 RID: 29377
	private readonly LogInfo[] DEFAULT_LOG_INFOS = new LogInfo[]
	{
		new LogInfo
		{
			m_name = "Jobs",
			m_consolePrinting = true,
			m_minLevel = Log.LogLevel.Error
		},
		new LogInfo
		{
			m_name = "Downloader",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = Log.LogLevel.Info
		},
		new LogInfo
		{
			m_name = "Login",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = Log.LogLevel.Info
		},
		new LogInfo
		{
			m_name = "ExceptionReporter",
			m_filePrinting = true,
			m_consolePrinting = true,
			m_minLevel = Log.LogLevel.Info
		},
		new LogInfo
		{
			m_name = "Offline",
			m_filePrinting = true,
			m_minLevel = Log.LogLevel.Info
		}
	};

	// Token: 0x040072C2 RID: 29378
	private static Log s_instance;

	// Token: 0x040072C3 RID: 29379
	private Map<string, LogInfo> m_logInfos = new Map<string, LogInfo>();

	// Token: 0x02002679 RID: 9849
	public enum LogLevel
	{
		// Token: 0x0400F0B4 RID: 61620
		[Description("None")]
		None,
		// Token: 0x0400F0B5 RID: 61621
		[Description("Debug")]
		Debug,
		// Token: 0x0400F0B6 RID: 61622
		[Description("Info")]
		Info,
		// Token: 0x0400F0B7 RID: 61623
		[Description("Warning")]
		Warning,
		// Token: 0x0400F0B8 RID: 61624
		[Description("Error")]
		Error
	}
}
