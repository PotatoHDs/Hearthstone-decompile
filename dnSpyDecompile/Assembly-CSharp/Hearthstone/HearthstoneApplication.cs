using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using bgs;
using Blizzard.BlizzardErrorMobile;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.Core.Streaming;
using Hearthstone.DataModels;
using Hearthstone.Login;
using Hearthstone.Streaming;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone
{
	// Token: 0x02000FD7 RID: 4055
	public class HearthstoneApplication : MonoBehaviour
	{
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600B034 RID: 45108 RVA: 0x00366F9C File Offset: 0x0036519C
		// (remove) Token: 0x0600B035 RID: 45109 RVA: 0x00366FD4 File Offset: 0x003651D4
		public event Action WillReset;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600B036 RID: 45110 RVA: 0x0036700C File Offset: 0x0036520C
		// (remove) Token: 0x0600B037 RID: 45111 RVA: 0x00367044 File Offset: 0x00365244
		public event Action Resetting;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600B038 RID: 45112 RVA: 0x0036707C File Offset: 0x0036527C
		// (remove) Token: 0x0600B039 RID: 45113 RVA: 0x003670B4 File Offset: 0x003652B4
		public event Action Paused;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600B03A RID: 45114 RVA: 0x003670EC File Offset: 0x003652EC
		// (remove) Token: 0x0600B03B RID: 45115 RVA: 0x00367124 File Offset: 0x00365324
		public event Action Unpaused;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600B03C RID: 45116 RVA: 0x0036715C File Offset: 0x0036535C
		// (remove) Token: 0x0600B03D RID: 45117 RVA: 0x00367194 File Offset: 0x00365394
		public event Action OnShutdown;

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600B03E RID: 45118 RVA: 0x003671C9 File Offset: 0x003653C9
		public static string[] CommandLineArgs
		{
			get
			{
				if (HearthstoneApplication.s_cachedCmdLineArgs == null)
				{
					HearthstoneApplication.ReadCommandLineArgs();
				}
				return HearthstoneApplication.s_cachedCmdLineArgs;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600B03F RID: 45119 RVA: 0x003671DC File Offset: 0x003653DC
		// (set) Token: 0x0600B040 RID: 45120 RVA: 0x003671E3 File Offset: 0x003653E3
		public static float AwakeTime { get; private set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600B041 RID: 45121 RVA: 0x003671EB File Offset: 0x003653EB
		public static bool IsHearthstoneRunning
		{
			get
			{
				return HearthstoneApplication.s_instance != null;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600B042 RID: 45122 RVA: 0x003671F8 File Offset: 0x003653F8
		// (set) Token: 0x0600B043 RID: 45123 RVA: 0x003671FF File Offset: 0x003653FF
		public static bool IsHearthstoneClosing { get; private set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600B044 RID: 45124 RVA: 0x00367207 File Offset: 0x00365407
		// (set) Token: 0x0600B045 RID: 45125 RVA: 0x0036720F File Offset: 0x0036540F
		public bool IsLocaleChanged { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600B046 RID: 45126 RVA: 0x00367218 File Offset: 0x00365418
		// (set) Token: 0x0600B047 RID: 45127 RVA: 0x00367220 File Offset: 0x00365420
		public string TestType { get; private set; }

		// Token: 0x0600B048 RID: 45128 RVA: 0x00367229 File Offset: 0x00365429
		private void Awake()
		{
			HearthstoneApplication.AwakeTime = Time.realtimeSinceStartup;
			HearthstoneApplication.s_instance = this;
			base.gameObject.AddComponent<HSDontDestroyOnLoad>();
			this.IsLocaleChanged = false;
			HearthstoneApplication.s_mainThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x0600B049 RID: 45129 RVA: 0x0036725D File Offset: 0x0036545D
		private void Start()
		{
			this.PreInitialization();
			this.RunStartup();
		}

		// Token: 0x0600B04A RID: 45130 RVA: 0x0036726C File Offset: 0x0036546C
		private void LateUpdate()
		{
			if (this.m_unloadUnusedAssetsDelay > 0f)
			{
				this.m_unloadUnusedAssetsDelay -= Time.unscaledDeltaTime;
			}
			else if (this.m_unloadUnusedAssets)
			{
				this.m_unloadUnusedAssets = false;
				this.m_unloadUnusedAssetsDelay = 1f;
				Resources.UnloadUnusedAssets();
			}
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance == null)
			{
				return;
			}
			hearthstonePerformance.DoLateUpdate();
		}

		// Token: 0x0600B04B RID: 45131 RVA: 0x003672CC File Offset: 0x003654CC
		private void ExceptionReportInitialize()
		{
			ExceptionReporter.Get().Initialize(FileUtils.PersistentDataPath, this.m_logger, this);
			ExceptionReporter.Get().IsInDebugMode = Options.Get().GetBool(Option.DELAYED_REPORTER_STOP);
			ExceptionReporter.Get().SendExceptions = Vars.Key("Application.SendExceptions").GetBool(true);
			ExceptionReporter.Get().SendAsserts = Vars.Key("Application.SendAsserts").GetBool(false);
			ExceptionReporter.Get().SendErrors = Vars.Key("Application.SendErrors").GetBool(false);
			ExceptionSettings exceptionSettings = new ExceptionSettings();
			exceptionSettings.m_projectID = 70;
			exceptionSettings.m_moduleName = "Hearthstone Client";
			exceptionSettings.m_version = "20.4";
			exceptionSettings.m_branchName = Network.BranchName;
			exceptionSettings.m_buildNumber = 84593;
			exceptionSettings.m_locale = Localization.GetLocaleName();
			exceptionSettings.m_jiraProjectName = "HSTN";
			exceptionSettings.m_jiraComponent = "T5QA Confirmation";
			exceptionSettings.m_jiraVersion = "20.4 Patch";
			exceptionSettings.m_logLineLimits[ExceptionSettings.ReportType.BUG] = -1;
			if (HearthstoneApplication.IsInternal())
			{
				exceptionSettings.m_logLineLimits[ExceptionSettings.ReportType.EXCEPTION] = 0;
			}
			exceptionSettings.m_logPathsCallback = new ExceptionSettings.LogPathsHandler(this.GetLogPaths);
			exceptionSettings.m_attachableFilesCallback = new ExceptionSettings.AttachableFilesHandler(this.GetAttachableFiles);
			exceptionSettings.m_additionalInfoCallback = new ExceptionSettings.AdditionalInfoHandler(this.GetAdditionalInfo);
			exceptionSettings.m_readFileMethodCallback = new ExceptionSettings.ReadFileMethodHandler(this.ReadLogFileSharing);
			ExceptionReporter.Get().BeforeZipping += this.FlushAllLogs;
			ExceptionReporter.Get().SetSettings(exceptionSettings);
			this.Resetting += ExceptionReporter.Get().ClearExceptionHashes;
		}

		// Token: 0x0600B04C RID: 45132 RVA: 0x0036745C File Offset: 0x0036565C
		private string[] GetLogPaths(ExceptionSettings.ReportType type)
		{
			if (type != ExceptionSettings.ReportType.EXCEPTION || string.IsNullOrEmpty(LogArchive.Get().LogPath))
			{
				return null;
			}
			List<string> list = new List<string>
			{
				LogArchive.Get().LogPath
			};
			foreach (string path in global::Log.Get().GetDefaultLogNames())
			{
				list.Add(Path.Combine(global::Logger.LogsPath, path) + ".log");
			}
			return list.ToArray();
		}

		// Token: 0x0600B04D RID: 45133 RVA: 0x003674F4 File Offset: 0x003656F4
		private string[] GetAttachableFiles(ExceptionSettings.ReportType type)
		{
			List<string> list = new List<string>
			{
				Vars.GetClientConfigPath(),
				LocalOptions.OptionsPath
			};
			if (type != ExceptionSettings.ReportType.EXCEPTION)
			{
				list.Add(global::Logger.LogsPath);
			}
			return list.ToArray();
		}

		// Token: 0x0600B04E RID: 45134 RVA: 0x00367538 File Offset: 0x00365738
		private Dictionary<string, string> GetAdditionalInfo(ExceptionSettings.ReportType type)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (type == ExceptionSettings.ReportType.BUG)
			{
				dictionary.Add("Aurora.Env", Vars.Key("Aurora.Env").GetStr(""));
				dictionary.Add("Aurora.Version.Source", Vars.Key("Aurora.Version.Source").GetStr(""));
				dictionary.Add("Aurora.Version.String", Vars.Key("Aurora.Version.String").GetStr(""));
				dictionary.Add("Aurora.Version.Int", Vars.Key("Aurora.Version.Int").GetStr(""));
				dictionary.Add("Aurora.Version", Vars.Key("Aurora.Version").GetStr(""));
				dictionary.Add("Mode", HearthstoneApplication.GetMode().ToString());
				dictionary.Add("MEnv", HearthstoneApplication.GetMobileEnvironment().ToString());
			}
			else if (type == ExceptionSettings.ReportType.EXCEPTION || type == ExceptionSettings.ReportType.ANR)
			{
				dictionary.Add("GameAccountID", BnetUtils.TryGetGameAccountId().GetValueOrDefault().ToString());
			}
			DateTime dateTime;
			if (BnetBar.Get().TryGetServerTime(out dateTime))
			{
				string value = GameStrings.Format("GLOBAL_CURRENT_TIME_AND_DATE_DEV", new object[]
				{
					GameStrings.Format("GLOBAL_CURRENT_TIME", new object[]
					{
						DateTime.Now
					}),
					GameStrings.Format("GLOBAL_CURRENT_DATE", new object[]
					{
						dateTime
					}),
					GameStrings.Format("GLOBAL_CURRENT_TIME", new object[]
					{
						dateTime
					})
				});
				dictionary.Add("ServerTime", value);
			}
			return dictionary;
		}

		// Token: 0x0600B04F RID: 45135 RVA: 0x003676DC File Offset: 0x003658DC
		private byte[] ReadLogFileSharing(string filepath)
		{
			byte[] result = null;
			try
			{
				if (File.Exists(filepath))
				{
					FileInfo fileInfo = new FileInfo(filepath);
					FieldInfo field = global::Log.Get().GetType().GetField(Path.GetFileNameWithoutExtension(fileInfo.Name));
					if (field != null)
					{
						global::Logger logger = (global::Logger)field.GetValue(global::Log.Get());
						result = Encoding.ASCII.GetBytes(logger.GetContent());
					}
					else
					{
						result = File.ReadAllBytes(filepath);
					}
				}
			}
			catch (Exception ex)
			{
				global::Log.ExceptionReporter.PrintError("Failed to read log file '{0}': {2}", new object[]
				{
					filepath,
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x0600B050 RID: 45136 RVA: 0x00367784 File Offset: 0x00365984
		private void FlushAllLogs()
		{
			foreach (string text in Directory.GetFiles(global::Logger.LogsPath).ToArray<string>())
			{
				try
				{
					FileInfo fileInfo = new FileInfo(text);
					FieldInfo field = global::Log.Get().GetType().GetField(Path.GetFileNameWithoutExtension(fileInfo.Name));
					if (field != null)
					{
						global::Logger logger = (global::Logger)field.GetValue(global::Log.Get());
						if (logger != null)
						{
							logger.FlushContent();
						}
					}
				}
				catch (Exception ex)
				{
					global::Log.ErrorReporter.PrintError("Failed to flush '{0}' from the folder '{1}'\n: {2}", new object[]
					{
						text,
						global::Logger.LogsPath,
						ex.Message
					});
				}
			}
		}

		// Token: 0x0600B051 RID: 45137 RVA: 0x00367844 File Offset: 0x00365A44
		private void PreInitialization()
		{
			LogArchive.Get();
			PegasusUtils.SetStackTraceLoggingOptions(false);
			HearthstoneApplication.ReadCommandLineArgs();
			PlatformSettings.RecomputeDeviceSettings();
			this.UpdateWorkingDirectory();
			LocalOptions.Get().Initialize();
			Localization.Initialize();
			LaunchArguments.ReadLaunchArgumentsFromDeeplink();
			this.ApplyInitializationSettingsFromConfig();
			Processor.UseJobQueueAlerts = !HearthstoneApplication.IsPublic();
			PreviousInstanceStatus.ReportAppStatus();
			new JobQueueTelemetry(Processor.JobQueue, Processor.JobQueueAlerts, this.TestType);
			Application.runInBackground = true;
			Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.Normal;
			GameStrings.LoadNative();
			if (HearthstoneApplication.IsPublic())
			{
				Options.Get().SetOption(Option.SOUND, OptionDataTables.s_defaultsMap[Option.SOUND]);
				Options.Get().SetOption(Option.MUSIC, OptionDataTables.s_defaultsMap[Option.MUSIC]);
			}
			UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Splash/PreloadScreen"));
			UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PegUI"));
			Processor.QueueJob("HearthstoneApplication.InitializeDataTransferJobs", this.Job_InitializeDataTransferJobs(), new IJobDependency[]
			{
				this.DataTransferDependency
			});
			BugReporter.Init();
			this.InitializeGlobalDataContext();
			LoginManager.RegisterDeterminePostLoginSceneCallback(StartupSceneSource.DEEP_LINK, delegate(ref SceneMgr.Mode sceneToLoad)
			{
				DeepLinkManager.TryExecuteDeepLinkOnStartup(false);
				return true;
			});
		}

		// Token: 0x0600B052 RID: 45138 RVA: 0x00367963 File Offset: 0x00365B63
		private IEnumerator<IAsyncJobResult> Job_InitializeDataTransferJobs()
		{
			HsAppsFlyer.Initialize(60);
			TelemetryManager.Initialize();
			this.ExceptionReportInitialize();
			HearthstonePerformance.Initialize(this.TestType, 2134675.ToString());
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.CaptureAppStartTime();
			}
			AppLaunchTracker.TrackAppLaunch();
			yield return null;
			yield break;
		}

		// Token: 0x0600B053 RID: 45139 RVA: 0x00367972 File Offset: 0x00365B72
		private void OnDestroy()
		{
			HearthstoneApplication.s_instance = null;
		}

		// Token: 0x0600B054 RID: 45140 RVA: 0x0036797A File Offset: 0x00365B7A
		private void OnApplicationQuit()
		{
			HearthstoneApplication.IsHearthstoneClosing = true;
			UberText.StoreCachedData();
			if (this.OnShutdown != null)
			{
				this.OnShutdown();
			}
			PreviousInstanceStatus.ClosedWithoutCrash = true;
			HearthstoneServices.Shutdown();
			HearthstonePerformance.Shutdown();
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x0600B055 RID: 45141 RVA: 0x003679B0 File Offset: 0x00365BB0
		private void OnApplicationFocus(bool focus)
		{
			if (this.m_focused == focus)
			{
				return;
			}
			if (FatalErrorMgr.Get().IsUnrecoverable)
			{
				return;
			}
			this.m_focused = focus;
			this.FireFocusChangedEvent();
		}

		// Token: 0x0600B056 RID: 45142 RVA: 0x003679D8 File Offset: 0x00365BD8
		public void OnApplicationPause(bool pauseStatus)
		{
			ExceptionReporter.Get().OnApplicationPause(pauseStatus);
			if (FatalErrorMgr.Get().IsUnrecoverable)
			{
				return;
			}
			if (Time.frameCount == 0)
			{
				return;
			}
			if (pauseStatus)
			{
				HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
				if (hearthstonePerformance != null)
				{
					hearthstonePerformance.OnApplicationPause();
				}
				this.m_lastPauseTime = Time.realtimeSinceStartup;
				HearthstoneApplication.IsHearthstoneClosing = true;
				if (this.Paused != null)
				{
					this.Paused();
				}
				PreviousInstanceStatus.ClosedWithoutCrash = true;
				UberText.StoreCachedData();
				Network.ApplicationPaused();
				TelemetryManager.Client().SendAppPaused(true, 0f);
				TelemetryManager.NetworkComponent.FlushSamplers();
				TelemetryManager.Flush();
				return;
			}
			HearthstonePerformance hearthstonePerformance2 = HearthstonePerformance.Get();
			if (hearthstonePerformance2 != null)
			{
				hearthstonePerformance2.OnApplicationResume();
			}
			this.m_hasResetSinceLastResume = false;
			HearthstoneApplication.IsHearthstoneClosing = false;
			float num = Time.realtimeSinceStartup - this.m_lastPauseTime;
			TelemetryManager.Client().SendAppPaused(false, num);
			Debug.Log("Time spent paused: " + num);
			DemoMgr demoMgr;
			if (HearthstoneServices.TryGet<DemoMgr>(out demoMgr) && demoMgr.GetMode() == DemoMode.BLIZZ_MUSEUM && num > 180f)
			{
				this.ResetImmediately(false, false);
			}
			this.m_lastResumeTime = Time.realtimeSinceStartup;
			if (this.Unpaused != null)
			{
				this.Unpaused();
			}
			PreviousInstanceStatus.ClosedWithoutCrash = false;
			Network.ApplicationUnpaused();
			if (BattleNet.IsInitialized())
			{
				MobileCallbackManager.SendPushAcknowledgement();
			}
			SceneMgr sceneMgr;
			if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr) && sceneMgr.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
			{
				this.ResetImmediately(false, false);
			}
			DeepLinkManager.TryExecuteDeepLinkOnStartup(true);
		}

		// Token: 0x0600B057 RID: 45143 RVA: 0x00367B2F File Offset: 0x00365D2F
		public static HearthstoneApplication Get()
		{
			return HearthstoneApplication.s_instance;
		}

		// Token: 0x0600B058 RID: 45144 RVA: 0x00367B36 File Offset: 0x00365D36
		public static ApplicationMode GetMode()
		{
			if (!HearthstoneApplication.s_initializedMode)
			{
				return ApplicationMode.PUBLIC;
			}
			return HearthstoneApplication.s_mode;
		}

		// Token: 0x0600B059 RID: 45145 RVA: 0x00367B46 File Offset: 0x00365D46
		public static bool IsInternal()
		{
			return HearthstoneApplication.GetMode() == ApplicationMode.INTERNAL;
		}

		// Token: 0x0600B05A RID: 45146 RVA: 0x00367B50 File Offset: 0x00365D50
		public static bool IsPublic()
		{
			return HearthstoneApplication.GetMode() == ApplicationMode.PUBLIC;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600B05B RID: 45147 RVA: 0x00367B5A File Offset: 0x00365D5A
		public static bool IsMainThread
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId == HearthstoneApplication.s_mainThreadId;
			}
		}

		// Token: 0x0600B05C RID: 45148 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public static bool UseDevWorkarounds()
		{
			return false;
		}

		// Token: 0x0600B05D RID: 45149 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public static void SendStartupTimeTelemetry(string eventName)
		{
		}

		// Token: 0x0600B05E RID: 45150 RVA: 0x00367B70 File Offset: 0x00365D70
		public static MobileEnv GetMobileEnvironment()
		{
			string a = Vars.Key("Mobile.Mode").GetStr("undefined");
			if (a == "undefined")
			{
				a = "Production";
			}
			if (a == "Production")
			{
				return MobileEnv.PRODUCTION;
			}
			return MobileEnv.DEVELOPMENT;
		}

		// Token: 0x0600B05F RID: 45151 RVA: 0x00367BB5 File Offset: 0x00365DB5
		public bool IsResetting()
		{
			return this.m_resetting;
		}

		// Token: 0x0600B060 RID: 45152 RVA: 0x00367BBD File Offset: 0x00365DBD
		public void Reset()
		{
			base.StartCoroutine(this.WaitThenReset(false, false));
		}

		// Token: 0x0600B061 RID: 45153 RVA: 0x00367BCE File Offset: 0x00365DCE
		public void ResetAndForceLogin()
		{
			base.StartCoroutine(this.WaitThenReset(true, false));
		}

		// Token: 0x0600B062 RID: 45154 RVA: 0x00367BDF File Offset: 0x00365DDF
		public void ResetAndGoBackToNoAccountTutorial()
		{
			base.StartCoroutine(this.WaitThenReset(false, true));
		}

		// Token: 0x0600B063 RID: 45155 RVA: 0x00367BF0 File Offset: 0x00365DF0
		public bool ResetOnErrorIfNecessary()
		{
			if (!this.m_hasResetSinceLastResume && Time.realtimeSinceStartup < this.m_lastResumeTime + 1f)
			{
				base.StartCoroutine(this.WaitThenReset(false, false));
				return true;
			}
			return false;
		}

		// Token: 0x0600B064 RID: 45156 RVA: 0x00367C1F File Offset: 0x00365E1F
		public void Exit()
		{
			this.m_exiting = true;
			if (ExceptionReporter.Get().Busy)
			{
				base.StartCoroutine(this.WaitThenExit());
				return;
			}
			GeneralUtils.ExitApplication();
		}

		// Token: 0x0600B065 RID: 45157 RVA: 0x00367C47 File Offset: 0x00365E47
		public bool IsExiting()
		{
			return this.m_exiting;
		}

		// Token: 0x0600B066 RID: 45158 RVA: 0x00367C4F File Offset: 0x00365E4F
		public bool HasFocus()
		{
			return this.m_focused;
		}

		// Token: 0x0600B067 RID: 45159 RVA: 0x00367C57 File Offset: 0x00365E57
		public bool AddFocusChangedListener(HearthstoneApplication.FocusChangedCallback callback)
		{
			return this.AddFocusChangedListener(callback, null);
		}

		// Token: 0x0600B068 RID: 45160 RVA: 0x00367C64 File Offset: 0x00365E64
		public bool AddFocusChangedListener(HearthstoneApplication.FocusChangedCallback callback, object userData)
		{
			HearthstoneApplication.FocusChangedListener focusChangedListener = new HearthstoneApplication.FocusChangedListener();
			focusChangedListener.SetCallback(callback);
			focusChangedListener.SetUserData(userData);
			if (this.m_focusChangedListeners.Contains(focusChangedListener))
			{
				return false;
			}
			this.m_focusChangedListeners.Add(focusChangedListener);
			return true;
		}

		// Token: 0x0600B069 RID: 45161 RVA: 0x00367CA2 File Offset: 0x00365EA2
		public bool RemoveFocusChangedListener(HearthstoneApplication.FocusChangedCallback callback)
		{
			return this.RemoveFocusChangedListener(callback, null);
		}

		// Token: 0x0600B06A RID: 45162 RVA: 0x00367CAC File Offset: 0x00365EAC
		public bool RemoveFocusChangedListener(HearthstoneApplication.FocusChangedCallback callback, object userData)
		{
			HearthstoneApplication.FocusChangedListener focusChangedListener = new HearthstoneApplication.FocusChangedListener();
			focusChangedListener.SetCallback(callback);
			focusChangedListener.SetUserData(userData);
			return this.m_focusChangedListeners.Remove(focusChangedListener);
		}

		// Token: 0x0600B06B RID: 45163 RVA: 0x00367CD9 File Offset: 0x00365ED9
		public float LastResetTime()
		{
			return this.m_lastResetTime;
		}

		// Token: 0x0600B06C RID: 45164 RVA: 0x00367CE1 File Offset: 0x00365EE1
		public static bool UsingStandaloneLocalData()
		{
			return !string.IsNullOrEmpty(HearthstoneApplication.GetStandaloneLocalDataPath());
		}

		// Token: 0x0600B06D RID: 45165 RVA: 0x00367CF0 File Offset: 0x00365EF0
		public static bool TryGetStandaloneLocalDataPath(string subPath, out string outPath)
		{
			string standaloneLocalDataPath = HearthstoneApplication.GetStandaloneLocalDataPath();
			if (!string.IsNullOrEmpty(standaloneLocalDataPath))
			{
				outPath = Path.Combine(standaloneLocalDataPath, subPath);
				return true;
			}
			outPath = null;
			return false;
		}

		// Token: 0x0600B06E RID: 45166 RVA: 0x00367D1A File Offset: 0x00365F1A
		public void UnloadUnusedAssets()
		{
			this.m_unloadUnusedAssets = true;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600B06F RID: 45167 RVA: 0x00367D23 File Offset: 0x00365F23
		public static string HSLaunchURI
		{
			get
			{
				return LaunchArguments.CreateLaunchArgument();
			}
		}

		// Token: 0x0600B070 RID: 45168 RVA: 0x00367D2C File Offset: 0x00365F2C
		public void ControlANRMonitor(bool on)
		{
			if (!PlatformSettings.IsMobileRuntimeOS || !(on ^ HearthstoneApplication.m_ANRMonitorOn))
			{
				return;
			}
			if (on)
			{
				float @float = Options.Get().GetFloat(Option.ANR_WAIT_SECONDS);
				float float2 = Options.Get().GetFloat(Option.ANR_THROTTLE);
				global::Log.ExceptionReporter.PrintInfo("ANR Monitor starts with seconds({0}) and throttle({1})", new object[]
				{
					@float,
					float2
				});
				ExceptionReporter.Get().EnableANRMonitor(@float, float2);
				HearthstoneApplication.m_ANRMonitorOn = true;
				return;
			}
			global::Log.ExceptionReporter.PrintInfo("ANR Monitor stopped", Array.Empty<object>());
			ExceptionReporter.Get().EnableANRMonitor(0f, 0f);
			HearthstoneApplication.m_ANRMonitorOn = false;
		}

		// Token: 0x0600B071 RID: 45169 RVA: 0x00367DD1 File Offset: 0x00365FD1
		private void RunStartup()
		{
			HearthstoneServices.InitializeRuntimeServices(null);
			this.QueueStartupJobs();
		}

		// Token: 0x0600B072 RID: 45170 RVA: 0x00367DDF File Offset: 0x00365FDF
		private IEnumerator<IAsyncJobResult> Job_CacheCommandLineArgs()
		{
			string[] commandLineArgs = HearthstoneApplication.CommandLineArgs;
			Debug.LogFormat("Command Line: {0}", new object[]
			{
				string.Join(" ", commandLineArgs)
			});
			yield break;
		}

		// Token: 0x0600B073 RID: 45171 RVA: 0x00367DE8 File Offset: 0x00365FE8
		private void QueueStartupJobs()
		{
			WaitForGameDownloadManagerState waitForGameDownloadManagerState = new WaitForGameDownloadManagerState();
			JobDefinition jobDefinition = new JobDefinition("GameStrings.LoadAll", GameStrings.Job_LoadAll(), new IJobDependency[]
			{
				waitForGameDownloadManagerState
			});
			Processor.QueueJob("HearthstoneApplication.CacheCommandLineArgs", this.Job_CacheCommandLineArgs(), Array.Empty<IJobDependency>());
			Processor.QueueJob("HearthstoneApplication.InitializeMode", HearthstoneApplication.Job_InitializeMode(), Array.Empty<IJobDependency>());
			Processor.QueueJob(jobDefinition);
			Processor.QueueJob("UberText.LoadCachedData", UberText.Job_LoadCachedData(), Array.Empty<IJobDependency>());
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("HearthstoneApplication.SetWindowText", new Action(this.SetWindowText), new IJobDependency[]
			{
				jobDefinition.CreateDependency()
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_LogoAnimation", new LoadUIScreen("LogoAnimation.prefab:d2af09653759c2449b0426037b7fe9eb"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(IAssetLoader)
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_StartupCamera", new LoadResource("Prefabs/StartupCamera", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_OverlayUI", new LoadUIScreen("OverlayUI.prefab:af7221edeeba8412cb55e9d6b58bb8dc"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(IAssetLoader)
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_SplashScreen", new InstantiatePrefab("SplashScreen.prefab:c9347f27a19520a49af412dad268db15"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(IAssetLoader),
				typeof(LoginManager),
				typeof(ILoginService),
				typeof(Network),
				typeof(DemoMgr),
				new WaitForLogoAnimation(),
				new WaitForOverlayUI(),
				new WaitForTooltipPanelManager()
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_BaseUI", new LoadUIScreen("BaseUI.prefab:4d9d926d0cb3bc24380df232133b009b"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(SoundManager),
				typeof(Network),
				typeof(ITouchScreenService),
				typeof(IAssetLoader),
				typeof(LoginManager)
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_AdventureConfig", new InstantiatePrefab("AdventureConfig.prefab:6c56645a84199884fbb351611099d9a8"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(AchieveManager),
				typeof(AdventureProgressMgr),
				typeof(IAssetLoader),
				typeof(SpecialEventManager)
			}));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_CardColorSwitcher", new InstantiatePrefab("CardColorSwitcher.prefab:b30c8322821f1524d9e08a59e78e2c85"), new object[]
			{
				waitForGameDownloadManagerState,
				typeof(GameDownloadManager),
				typeof(SceneMgr),
				typeof(IAssetLoader)
			}));
		}

		// Token: 0x0600B074 RID: 45172 RVA: 0x0036812C File Offset: 0x0036632C
		private void InitializeGlobalDataContext()
		{
			DataContext dataContext = GlobalDataContext.Get();
			dataContext.BindDataModel(new DeviceDataModel
			{
				Category = PlatformSettings.OS,
				Mobile = PlatformSettings.IsMobile(),
				Notch = false,
				Screen = PlatformSettings.Screen
			});
			dataContext.BindDataModel(new AccountDataModel
			{
				Language = Localization.GetLocale()
			});
		}

		// Token: 0x0600B075 RID: 45173 RVA: 0x00368186 File Offset: 0x00366386
		private static IEnumerator<IAsyncJobResult> Job_InitializeMode()
		{
			if (HearthstoneApplication.s_initializedMode)
			{
				yield break;
			}
			HearthstoneApplication.s_mode = ApplicationMode.PUBLIC;
			HearthstoneApplication.s_initializedMode = true;
			HearthstoneApplication.ReadCommandLineArgs();
			yield break;
		}

		// Token: 0x0600B076 RID: 45174 RVA: 0x0036818E File Offset: 0x0036638E
		private IEnumerator WaitThenReset(bool forceLogin, bool forceNoAccountTutorial = false)
		{
			this.m_resetting = true;
			Navigation.Clear();
			yield return new WaitForEndOfFrame();
			this.ResetImmediately(forceLogin, forceNoAccountTutorial);
			yield break;
		}

		// Token: 0x0600B077 RID: 45175 RVA: 0x003681AC File Offset: 0x003663AC
		private void ResetImmediately(bool forceLogin, bool forceNoAccountTutorial = false)
		{
			global::Log.Reset.Print("HearthstoneApplication.ResetImmediately - forceLogin? " + forceLogin.ToString() + "  Stack trace: " + Environment.StackTrace, Array.Empty<object>());
			TelemetryManager.Client().SendClientReset(forceLogin, forceNoAccountTutorial);
			Processor.JobQueue.ClearJobs();
			UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Splash/PreloadScreen"));
			if (this.WillReset != null)
			{
				this.WillReset();
			}
			this.m_resetting = true;
			this.m_lastResetTime = Time.realtimeSinceStartup;
			if (DialogManager.Get() != null)
			{
				DialogManager.Get().ClearAllImmediately();
			}
			if (UniversalInputManager.Get() != null)
			{
				UniversalInputManager.Get().SetSystemDialogActive(false);
				UniversalInputManager.Get().SetGameDialogActive(false);
			}
			if (Network.TUTORIALS_WITHOUT_ACCOUNT)
			{
				Network.SetShouldBeConnectedToAurora(forceLogin || Options.Get().GetBool(Option.CONNECT_TO_AURORA));
			}
			FatalErrorMgr.Get().ClearAllErrors();
			this.m_hasResetSinceLastResume = true;
			if (forceNoAccountTutorial)
			{
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, false);
				Network.SetShouldBeConnectedToAurora(false);
			}
			if (this.Resetting != null)
			{
				this.Resetting();
			}
			TelemetryManager.Reset();
			MobileCallbackManager.SetTelemetryInfo(TelemetryManager.ProgramId, TelemetryManager.ProgramName, TelemetryManager.ProgramVersion, TelemetryManager.SessionId);
			Navigation.Clear();
			Processor.QueueJob("HearthstoneApplication.OnResetDownloadComplete", this.Job_OnResetDownloadComplete(), new IJobDependency[]
			{
				new WaitForGameDownloadManagerState()
			});
			this.m_resetting = false;
			global::Log.Reset.Print("\tHearthstoneApplication.ResetImmediately completed", Array.Empty<object>());
		}

		// Token: 0x0600B078 RID: 45176 RVA: 0x0036831C File Offset: 0x0036651C
		public IEnumerator<IAsyncJobResult> Job_OnResetDownloadComplete()
		{
			if (this.IsLocaleChanged)
			{
				global::Log.Downloader.PrintInfo("Reload new locale data", Array.Empty<object>());
				yield return GameDbf.CreateLoadDbfJob();
				AchieveManager.Get().LoadAchievesFromDBF();
				UberText.RebuildAllUberText();
				GameStrings.ReloadAll();
				InnKeepersSpecial.Get().ResetAdUrl();
				this.IsLocaleChanged = false;
			}
			Processor.QueueJob("UberText.LoadCachedData", UberText.Job_LoadCachedData(), Array.Empty<IJobDependency>());
			FontTable fontTable;
			if (HearthstoneServices.TryGet<FontTable>(out fontTable))
			{
				yield return new JobDefinition("FontTable.LoadFontDefs", fontTable.Job_LoadFontDefs(), Array.Empty<IJobDependency>());
			}
			yield return HearthstoneJobs.CreateJobFromDependency("Load_LogoAnimation", new InstantiatePrefab("LogoAnimation.prefab:d2af09653759c2449b0426037b7fe9eb"));
			yield return HearthstoneJobs.CreateJobFromDependency("Load_SplashScreen", new InstantiatePrefab("SplashScreen.prefab:c9347f27a19520a49af412dad268db15"));
			yield break;
		}

		// Token: 0x0600B079 RID: 45177 RVA: 0x0036832B File Offset: 0x0036652B
		private IEnumerator<IAsyncJobResult> Job_PostDBFLoadOnLocaleChange()
		{
			yield break;
		}

		// Token: 0x0600B07A RID: 45178 RVA: 0x00368333 File Offset: 0x00366533
		private IEnumerator WaitThenExit()
		{
			while (ExceptionReporter.Get().Busy)
			{
				yield return null;
			}
			GeneralUtils.ExitApplication();
			yield break;
		}

		// Token: 0x0600B07B RID: 45179 RVA: 0x0036833C File Offset: 0x0036653C
		private static void ReadCommandLineArgs()
		{
			string[] array = null;
			if (HearthstoneApplication.s_initializedMode)
			{
				string str = Vars.Key("Application.CommandLineOverride").GetStr(null);
				if (str != null)
				{
					array = str.Split(new char[]
					{
						' '
					}, StringSplitOptions.RemoveEmptyEntries);
					HearthstoneApplication.s_cachedCmdLineArgsAreModified = true;
				}
			}
			if (array == null)
			{
				if (Environment.GetCommandLineArgs() != null)
				{
					array = Environment.GetCommandLineArgs().Skip(1).ToArray<string>();
				}
				else
				{
					array = new string[0];
					Debug.LogFormat("Command Line is null", Array.Empty<object>());
				}
			}
			if (HearthstoneApplication.s_initializedMode)
			{
				string str2 = Vars.Key("Application.CommandLineAppend").GetStr(null);
				if (!string.IsNullOrEmpty(str2))
				{
					List<string> list = new List<string>(array);
					list.AddRange(str2.Split(new char[]
					{
						' '
					}, StringSplitOptions.RemoveEmptyEntries));
					array = list.ToArray();
					HearthstoneApplication.s_cachedCmdLineArgsAreModified = true;
				}
				if (HearthstoneApplication.s_cachedCmdLineArgsAreModified)
				{
					Debug.LogFormat("Modified Command Line: {0}", new object[]
					{
						string.Join(" ", array)
					});
				}
			}
			HearthstoneApplication.s_cachedCmdLineArgs = array;
			HearthstoneApplication.ProcessCommandLineArgs();
		}

		// Token: 0x0600B07C RID: 45180 RVA: 0x00368430 File Offset: 0x00366630
		private static void ProcessCommandLineArgs()
		{
			for (int i = 0; i < HearthstoneApplication.s_cachedCmdLineArgs.Length; i++)
			{
				string[] array = HearthstoneApplication.s_cachedCmdLineArgs[i].Split(new char[]
				{
					'='
				});
				string a = array[0].ToLower();
				if (!(a == "confignameoverride"))
				{
					if (a == "optionsnameoverride")
					{
						if (array.Length > 1)
						{
							Vars.s_optionsNameOverride = array[1];
						}
					}
				}
				else if (array.Length > 1)
				{
					Vars.s_configNameOverride = array[1];
				}
			}
		}

		// Token: 0x0600B07D RID: 45181 RVA: 0x003684AC File Offset: 0x003666AC
		public void ApplyInitializationSettingsFromConfig()
		{
			ConfigFile configFile = new ConfigFile();
			configFile.FullLoad(Vars.GetClientConfigPath());
			bool flag = configFile.Get("Jobs.EnableMonitor", false);
			Processor.SetJobQueueMonitorEnabled(flag);
			if (flag)
			{
				string logPath = LogArchive.Get().LogPath;
				Processor.SetTrackedQueueDataFilePrefix(Path.GetFileNameWithoutExtension(logPath));
				Processor.SetTrackedQueueDataDirectory(Path.GetDirectoryName(logPath));
			}
			if (HearthstoneApplication.IsInternal())
			{
				this.TestType = configFile.Get("Test.TestType", string.Empty);
				if (!string.IsNullOrEmpty(this.TestType) && this.TestType == "JobDelay")
				{
					Processor.JobQueue.Debug.DelayTest = true;
					string text = configFile.Get("Test.MinDelay", string.Empty);
					string text2 = configFile.Get("Test.MaxDelay", string.Empty);
					float num;
					if (float.TryParse(text, out num))
					{
						Processor.JobQueue.Debug.JobDelayMin = num;
					}
					else if (!string.IsNullOrEmpty(text))
					{
						global::Log.ConfigFile.PrintWarning("Unable to evaluate Minimum  Job Delay value {0}, defaulting to {1}" + text, new object[]
						{
							Processor.JobQueue.Debug.JobDelayMin
						});
					}
					if (float.TryParse(text2, out num))
					{
						Processor.JobQueue.Debug.JobDelayMax = num;
						return;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						global::Log.ConfigFile.PrintWarning("Unable to evaluate Maximum Job Delay value {0}, defaulting to {1}" + text2, new object[]
						{
							Processor.JobQueue.Debug.JobDelayMax
						});
					}
				}
			}
		}

		// Token: 0x0600B07E RID: 45182 RVA: 0x00090064 File Offset: 0x0008E264
		private static string GetStandaloneLocalDataPath()
		{
			return null;
		}

		// Token: 0x0600B07F RID: 45183 RVA: 0x00368620 File Offset: 0x00366820
		private void FireFocusChangedEvent()
		{
			HearthstoneApplication.FocusChangedListener[] array = this.m_focusChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(this.m_focused);
			}
		}

		// Token: 0x0600B080 RID: 45184 RVA: 0x00368658 File Offset: 0x00366858
		private void UpdateWorkingDirectory()
		{
			bool flag = false;
			if (!Application.isEditor && !PlatformSettings.IsMobileRuntimeOS)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
				string currentDirectory = Directory.GetCurrentDirectory();
				string text = (!directoryInfo.Exists) ? string.Empty : ((directoryInfo.Parent == null) ? directoryInfo.FullName : directoryInfo.Parent.FullName);
				if (PlatformSettings.RuntimeOS == OSCategory.Mac && directoryInfo.Exists && directoryInfo.Parent != null && directoryInfo.Parent.Parent != null)
				{
					text = directoryInfo.Parent.Parent.FullName;
				}
				if (!string.IsNullOrEmpty(text) && !text.Equals(currentDirectory, StringComparison.CurrentCultureIgnoreCase))
				{
					flag = true;
					Directory.SetCurrentDirectory(text);
					Debug.LogFormat("Set current working dir from={0} to={1}", new object[]
					{
						currentDirectory,
						Directory.GetCurrentDirectory()
					});
				}
			}
			if (!flag)
			{
				Debug.LogFormat("Current working dir={0}", new object[]
				{
					Directory.GetCurrentDirectory()
				});
			}
		}

		// Token: 0x0600B081 RID: 45185 RVA: 0x00368740 File Offset: 0x00366940
		private void SetWindowText()
		{
			IntPtr intPtr = HearthstoneApplication.FindWindow(null, "Hearthstone");
			if (intPtr != IntPtr.Zero)
			{
				HearthstoneApplication.SetWindowTextW(intPtr, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
			}
		}

		// Token: 0x0600B082 RID: 45186
		[DllImport("user32.dll")]
		public static extern int SetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string text);

		// Token: 0x0600B083 RID: 45187
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);

		// Token: 0x0400951A RID: 38170
		private const ApplicationMode DEFAULT_MODE = ApplicationMode.INTERNAL;

		// Token: 0x0400951B RID: 38171
		private const float kUnloadUnusedAssetsDelay = 1f;

		// Token: 0x0400951C RID: 38172
		public const float ANR_THROTTLE = 0.01f;

		// Token: 0x0400951D RID: 38173
		public const float ANR_WAIT_SECONDS = 10f;

		// Token: 0x0400951E RID: 38174
		private const int HEARTHSTONE_PROJECT_ID = 70;

		// Token: 0x0400951F RID: 38175
		private const string HEARTHSTONE_MODULE_NAME = "Hearthstone Client";

		// Token: 0x04009520 RID: 38176
		private const string HEARTHSTONE_PROJECT_NAME_JIRA = "HSTN";

		// Token: 0x04009521 RID: 38177
		private const string HEARTHSTONE_COMPONENT_NAME_JIRA = "T5QA Confirmation";

		// Token: 0x04009522 RID: 38178
		public static readonly PlatformDependentValue<bool> CanQuitGame = new PlatformDependentValue<bool>(PlatformCategory.OS)
		{
			PC = true,
			Mac = true,
			Android = true,
			iOS = false
		};

		// Token: 0x04009523 RID: 38179
		public static readonly PlatformDependentValue<bool> AllowResetFromFatalError = new PlatformDependentValue<bool>(PlatformCategory.OS)
		{
			PC = false,
			Mac = false,
			Android = true,
			iOS = true
		};

		// Token: 0x04009524 RID: 38180
		private static bool s_initializedMode = false;

		// Token: 0x04009525 RID: 38181
		private static string[] s_cachedCmdLineArgs = null;

		// Token: 0x04009526 RID: 38182
		private static bool s_cachedCmdLineArgsAreModified = false;

		// Token: 0x04009527 RID: 38183
		private bool m_exiting;

		// Token: 0x04009528 RID: 38184
		private bool m_focused = true;

		// Token: 0x04009529 RID: 38185
		private static bool m_ANRMonitorOn = false;

		// Token: 0x0400952A RID: 38186
		private List<HearthstoneApplication.FocusChangedListener> m_focusChangedListeners = new List<HearthstoneApplication.FocusChangedListener>();

		// Token: 0x0400952B RID: 38187
		private float m_lastResumeTime = -999999f;

		// Token: 0x0400952C RID: 38188
		private float m_lastPauseTime;

		// Token: 0x0400952D RID: 38189
		private bool m_hasResetSinceLastResume;

		// Token: 0x0400952E RID: 38190
		private const float AUTO_RESET_ON_ERROR_TIMEOUT = 1f;

		// Token: 0x0400952F RID: 38191
		private bool m_resetting;

		// Token: 0x04009530 RID: 38192
		private float m_lastResetTime;

		// Token: 0x04009531 RID: 38193
		private bool m_unloadUnusedAssets;

		// Token: 0x04009532 RID: 38194
		private float m_unloadUnusedAssetsDelay;

		// Token: 0x04009533 RID: 38195
		private static ApplicationMode s_mode = ApplicationMode.INVALID;

		// Token: 0x04009534 RID: 38196
		private static HearthstoneApplication s_instance = null;

		// Token: 0x04009535 RID: 38197
		private static int s_mainThreadId = -1;

		// Token: 0x0400953A RID: 38202
		private HearthstoneApplication.CrashLogger m_logger = new HearthstoneApplication.CrashLogger();

		// Token: 0x0400953B RID: 38203
		public WaitForCallback DataTransferDependency = new WaitForCallback();

		// Token: 0x020027FF RID: 10239
		private class FocusChangedListener : global::EventListener<HearthstoneApplication.FocusChangedCallback>
		{
			// Token: 0x06013AAD RID: 80557 RVA: 0x00539CD1 File Offset: 0x00537ED1
			public void Fire(bool focused)
			{
				this.m_callback(focused, this.m_userData);
			}
		}

		// Token: 0x02002800 RID: 10240
		// (Invoke) Token: 0x06013AB0 RID: 80560
		public delegate void FocusChangedCallback(bool focused, object userData);

		// Token: 0x02002801 RID: 10241
		private class CrashLogger : IExceptionLogger
		{
			// Token: 0x06013AB3 RID: 80563 RVA: 0x00539CED File Offset: 0x00537EED
			public void LogDebug(string format, params object[] args)
			{
				global::Log.ExceptionReporter.PrintDebug(format, args);
			}

			// Token: 0x06013AB4 RID: 80564 RVA: 0x00539CFB File Offset: 0x00537EFB
			public void LogInfo(string format, params object[] args)
			{
				global::Log.ExceptionReporter.PrintInfo(format, args);
			}

			// Token: 0x06013AB5 RID: 80565 RVA: 0x00539D09 File Offset: 0x00537F09
			public void LogWarning(string format, params object[] args)
			{
				global::Log.ExceptionReporter.PrintWarning(format, args);
			}

			// Token: 0x06013AB6 RID: 80566 RVA: 0x00539D17 File Offset: 0x00537F17
			public void LogError(string format, params object[] args)
			{
				global::Log.ExceptionReporter.PrintError(format, args);
			}
		}
	}
}
