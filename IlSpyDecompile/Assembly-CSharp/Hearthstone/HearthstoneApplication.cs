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
	public class HearthstoneApplication : MonoBehaviour
	{
		private class FocusChangedListener : EventListener<FocusChangedCallback>
		{
			public void Fire(bool focused)
			{
				m_callback(focused, m_userData);
			}
		}

		public delegate void FocusChangedCallback(bool focused, object userData);

		private class CrashLogger : IExceptionLogger
		{
			public void LogDebug(string format, params object[] args)
			{
				Log.ExceptionReporter.PrintDebug(format, args);
			}

			public void LogInfo(string format, params object[] args)
			{
				Log.ExceptionReporter.PrintInfo(format, args);
			}

			public void LogWarning(string format, params object[] args)
			{
				Log.ExceptionReporter.PrintWarning(format, args);
			}

			public void LogError(string format, params object[] args)
			{
				Log.ExceptionReporter.PrintError(format, args);
			}
		}

		private const ApplicationMode DEFAULT_MODE = ApplicationMode.INTERNAL;

		private const float kUnloadUnusedAssetsDelay = 1f;

		public const float ANR_THROTTLE = 0.01f;

		public const float ANR_WAIT_SECONDS = 10f;

		private const int HEARTHSTONE_PROJECT_ID = 70;

		private const string HEARTHSTONE_MODULE_NAME = "Hearthstone Client";

		private const string HEARTHSTONE_PROJECT_NAME_JIRA = "HSTN";

		private const string HEARTHSTONE_COMPONENT_NAME_JIRA = "T5QA Confirmation";

		public static readonly PlatformDependentValue<bool> CanQuitGame = new PlatformDependentValue<bool>(PlatformCategory.OS)
		{
			PC = true,
			Mac = true,
			Android = true,
			iOS = false
		};

		public static readonly PlatformDependentValue<bool> AllowResetFromFatalError = new PlatformDependentValue<bool>(PlatformCategory.OS)
		{
			PC = false,
			Mac = false,
			Android = true,
			iOS = true
		};

		private static bool s_initializedMode = false;

		private static string[] s_cachedCmdLineArgs = null;

		private static bool s_cachedCmdLineArgsAreModified = false;

		private bool m_exiting;

		private bool m_focused = true;

		private static bool m_ANRMonitorOn = false;

		private List<FocusChangedListener> m_focusChangedListeners = new List<FocusChangedListener>();

		private float m_lastResumeTime = -999999f;

		private float m_lastPauseTime;

		private bool m_hasResetSinceLastResume;

		private const float AUTO_RESET_ON_ERROR_TIMEOUT = 1f;

		private bool m_resetting;

		private float m_lastResetTime;

		private bool m_unloadUnusedAssets;

		private float m_unloadUnusedAssetsDelay;

		private static ApplicationMode s_mode = ApplicationMode.INVALID;

		private static HearthstoneApplication s_instance = null;

		private static int s_mainThreadId = -1;

		private CrashLogger m_logger = new CrashLogger();

		public WaitForCallback DataTransferDependency = new WaitForCallback();

		public static string[] CommandLineArgs
		{
			get
			{
				if (s_cachedCmdLineArgs == null)
				{
					ReadCommandLineArgs();
				}
				return s_cachedCmdLineArgs;
			}
		}

		public static float AwakeTime { get; private set; }

		public static bool IsHearthstoneRunning => s_instance != null;

		public static bool IsHearthstoneClosing { get; private set; }

		public bool IsLocaleChanged { get; set; }

		public string TestType { get; private set; }

		public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == s_mainThreadId;

		public static string HSLaunchURI => LaunchArguments.CreateLaunchArgument();

		public event Action WillReset;

		public event Action Resetting;

		public event Action Paused;

		public event Action Unpaused;

		public event Action OnShutdown;

		private void Awake()
		{
			AwakeTime = Time.realtimeSinceStartup;
			s_instance = this;
			base.gameObject.AddComponent<HSDontDestroyOnLoad>();
			IsLocaleChanged = false;
			s_mainThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		private void Start()
		{
			PreInitialization();
			RunStartup();
		}

		private void LateUpdate()
		{
			if (m_unloadUnusedAssetsDelay > 0f)
			{
				m_unloadUnusedAssetsDelay -= Time.unscaledDeltaTime;
			}
			else if (m_unloadUnusedAssets)
			{
				m_unloadUnusedAssets = false;
				m_unloadUnusedAssetsDelay = 1f;
				Resources.UnloadUnusedAssets();
			}
			HearthstonePerformance.Get()?.DoLateUpdate();
		}

		private void ExceptionReportInitialize()
		{
			ExceptionReporter.Get().Initialize(FileUtils.PersistentDataPath, m_logger, this);
			ExceptionReporter.Get().IsInDebugMode = Options.Get().GetBool(Option.DELAYED_REPORTER_STOP);
			ExceptionReporter.Get().SendExceptions = Vars.Key("Application.SendExceptions").GetBool(def: true);
			ExceptionReporter.Get().SendAsserts = Vars.Key("Application.SendAsserts").GetBool(def: false);
			ExceptionReporter.Get().SendErrors = Vars.Key("Application.SendErrors").GetBool(def: false);
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
			if (IsInternal())
			{
				exceptionSettings.m_logLineLimits[ExceptionSettings.ReportType.EXCEPTION] = 0;
			}
			exceptionSettings.m_logPathsCallback = GetLogPaths;
			exceptionSettings.m_attachableFilesCallback = GetAttachableFiles;
			exceptionSettings.m_additionalInfoCallback = GetAdditionalInfo;
			exceptionSettings.m_readFileMethodCallback = ReadLogFileSharing;
			ExceptionReporter.Get().BeforeZipping += FlushAllLogs;
			ExceptionReporter.Get().SetSettings(exceptionSettings);
			Resetting += ExceptionReporter.Get().ClearExceptionHashes;
		}

		private string[] GetLogPaths(ExceptionSettings.ReportType type)
		{
			if (type != 0 || string.IsNullOrEmpty(LogArchive.Get().LogPath))
			{
				return null;
			}
			List<string> list = new List<string> { LogArchive.Get().LogPath };
			foreach (string defaultLogName in Log.Get().GetDefaultLogNames())
			{
				list.Add(Path.Combine(Logger.LogsPath, defaultLogName) + ".log");
			}
			return list.ToArray();
		}

		private string[] GetAttachableFiles(ExceptionSettings.ReportType type)
		{
			List<string> list = new List<string>
			{
				Vars.GetClientConfigPath(),
				LocalOptions.OptionsPath
			};
			if (type != 0)
			{
				list.Add(Logger.LogsPath);
				_ = 1;
			}
			return list.ToArray();
		}

		private Dictionary<string, string> GetAdditionalInfo(ExceptionSettings.ReportType type)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			switch (type)
			{
			case ExceptionSettings.ReportType.BUG:
				dictionary.Add("Aurora.Env", Vars.Key("Aurora.Env").GetStr(""));
				dictionary.Add("Aurora.Version.Source", Vars.Key("Aurora.Version.Source").GetStr(""));
				dictionary.Add("Aurora.Version.String", Vars.Key("Aurora.Version.String").GetStr(""));
				dictionary.Add("Aurora.Version.Int", Vars.Key("Aurora.Version.Int").GetStr(""));
				dictionary.Add("Aurora.Version", Vars.Key("Aurora.Version").GetStr(""));
				dictionary.Add("Mode", GetMode().ToString());
				dictionary.Add("MEnv", GetMobileEnvironment().ToString());
				break;
			case ExceptionSettings.ReportType.EXCEPTION:
			case ExceptionSettings.ReportType.ANR:
				dictionary.Add("GameAccountID", BnetUtils.TryGetGameAccountId().GetValueOrDefault().ToString());
				break;
			}
			if (BnetBar.Get().TryGetServerTime(out var serverTime))
			{
				string value = GameStrings.Format("GLOBAL_CURRENT_TIME_AND_DATE_DEV", GameStrings.Format("GLOBAL_CURRENT_TIME", DateTime.Now), GameStrings.Format("GLOBAL_CURRENT_DATE", serverTime), GameStrings.Format("GLOBAL_CURRENT_TIME", serverTime));
				dictionary.Add("ServerTime", value);
			}
			return dictionary;
		}

		private byte[] ReadLogFileSharing(string filepath)
		{
			byte[] result = null;
			try
			{
				if (File.Exists(filepath))
				{
					FileInfo fileInfo = new FileInfo(filepath);
					FieldInfo field = Log.Get().GetType().GetField(Path.GetFileNameWithoutExtension(fileInfo.Name));
					if (field != null)
					{
						Logger logger = (Logger)field.GetValue(Log.Get());
						result = Encoding.ASCII.GetBytes(logger.GetContent());
						return result;
					}
					result = File.ReadAllBytes(filepath);
					return result;
				}
				return result;
			}
			catch (Exception ex)
			{
				Log.ExceptionReporter.PrintError("Failed to read log file '{0}': {2}", filepath, ex.Message);
				return result;
			}
		}

		private void FlushAllLogs()
		{
			string[] array = Directory.GetFiles(Logger.LogsPath).ToArray();
			foreach (string text in array)
			{
				try
				{
					FileInfo fileInfo = new FileInfo(text);
					FieldInfo field = Log.Get().GetType().GetField(Path.GetFileNameWithoutExtension(fileInfo.Name));
					if (field != null)
					{
						((Logger)field.GetValue(Log.Get()))?.FlushContent();
					}
				}
				catch (Exception ex)
				{
					Log.ErrorReporter.PrintError("Failed to flush '{0}' from the folder '{1}'\n: {2}", text, Logger.LogsPath, ex.Message);
				}
			}
		}

		private void PreInitialization()
		{
			LogArchive.Get();
			PegasusUtils.SetStackTraceLoggingOptions(forceUseMinimumLogging: false);
			ReadCommandLineArgs();
			PlatformSettings.RecomputeDeviceSettings();
			UpdateWorkingDirectory();
			LocalOptions.Get().Initialize();
			Localization.Initialize();
			LaunchArguments.ReadLaunchArgumentsFromDeeplink();
			ApplyInitializationSettingsFromConfig();
			Processor.UseJobQueueAlerts = !IsPublic();
			PreviousInstanceStatus.ReportAppStatus();
			new JobQueueTelemetry(Processor.JobQueue, Processor.JobQueueAlerts, TestType);
			Application.runInBackground = true;
			Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.Normal;
			GameStrings.LoadNative();
			if (IsPublic())
			{
				Options.Get().SetOption(Option.SOUND, OptionDataTables.s_defaultsMap[Option.SOUND]);
				Options.Get().SetOption(Option.MUSIC, OptionDataTables.s_defaultsMap[Option.MUSIC]);
			}
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Splash/PreloadScreen"));
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/PegUI"));
			Processor.QueueJob("HearthstoneApplication.InitializeDataTransferJobs", Job_InitializeDataTransferJobs(), DataTransferDependency);
			BugReporter.Init();
			InitializeGlobalDataContext();
			LoginManager.RegisterDeterminePostLoginSceneCallback(StartupSceneSource.DEEP_LINK, delegate
			{
				DeepLinkManager.TryExecuteDeepLinkOnStartup(fromUnpause: false);
				return true;
			});
		}

		private IEnumerator<IAsyncJobResult> Job_InitializeDataTransferJobs()
		{
			HsAppsFlyer.Initialize(60);
			TelemetryManager.Initialize();
			ExceptionReportInitialize();
			HearthstonePerformance.Initialize(TestType, 2134675.ToString());
			HearthstonePerformance.Get()?.CaptureAppStartTime();
			AppLaunchTracker.TrackAppLaunch();
			yield return null;
		}

		private void OnDestroy()
		{
			s_instance = null;
		}

		private void OnApplicationQuit()
		{
			IsHearthstoneClosing = true;
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

		private void OnApplicationFocus(bool focus)
		{
			if (m_focused != focus && !FatalErrorMgr.Get().IsUnrecoverable)
			{
				m_focused = focus;
				FireFocusChangedEvent();
			}
		}

		public void OnApplicationPause(bool pauseStatus)
		{
			ExceptionReporter.Get().OnApplicationPause(pauseStatus);
			if (FatalErrorMgr.Get().IsUnrecoverable || Time.frameCount == 0)
			{
				return;
			}
			if (pauseStatus)
			{
				HearthstonePerformance.Get()?.OnApplicationPause();
				m_lastPauseTime = Time.realtimeSinceStartup;
				IsHearthstoneClosing = true;
				if (this.Paused != null)
				{
					this.Paused();
				}
				PreviousInstanceStatus.ClosedWithoutCrash = true;
				UberText.StoreCachedData();
				Network.ApplicationPaused();
				TelemetryManager.Client().SendAppPaused(pauseStatus: true, 0f);
				TelemetryManager.NetworkComponent.FlushSamplers();
				TelemetryManager.Flush();
				return;
			}
			HearthstonePerformance.Get()?.OnApplicationResume();
			m_hasResetSinceLastResume = false;
			IsHearthstoneClosing = false;
			float num = Time.realtimeSinceStartup - m_lastPauseTime;
			TelemetryManager.Client().SendAppPaused(pauseStatus: false, num);
			Debug.Log("Time spent paused: " + num);
			if (HearthstoneServices.TryGet<DemoMgr>(out var service) && service.GetMode() == DemoMode.BLIZZ_MUSEUM && num > 180f)
			{
				ResetImmediately(forceLogin: false);
			}
			m_lastResumeTime = Time.realtimeSinceStartup;
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
			if (HearthstoneServices.TryGet<SceneMgr>(out var service2) && service2.IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
			{
				ResetImmediately(forceLogin: false);
			}
			DeepLinkManager.TryExecuteDeepLinkOnStartup(fromUnpause: true);
		}

		public static HearthstoneApplication Get()
		{
			return s_instance;
		}

		public static ApplicationMode GetMode()
		{
			if (!s_initializedMode)
			{
				return ApplicationMode.PUBLIC;
			}
			return s_mode;
		}

		public static bool IsInternal()
		{
			return GetMode() == ApplicationMode.INTERNAL;
		}

		public static bool IsPublic()
		{
			return GetMode() == ApplicationMode.PUBLIC;
		}

		public static bool UseDevWorkarounds()
		{
			return false;
		}

		public static void SendStartupTimeTelemetry(string eventName)
		{
		}

		public static MobileEnv GetMobileEnvironment()
		{
			string text = Vars.Key("Mobile.Mode").GetStr("undefined");
			if (text == "undefined")
			{
				text = "Production";
			}
			if (text == "Production")
			{
				return MobileEnv.PRODUCTION;
			}
			return MobileEnv.DEVELOPMENT;
		}

		public bool IsResetting()
		{
			return m_resetting;
		}

		public void Reset()
		{
			StartCoroutine(WaitThenReset(forceLogin: false));
		}

		public void ResetAndForceLogin()
		{
			StartCoroutine(WaitThenReset(forceLogin: true));
		}

		public void ResetAndGoBackToNoAccountTutorial()
		{
			StartCoroutine(WaitThenReset(forceLogin: false, forceNoAccountTutorial: true));
		}

		public bool ResetOnErrorIfNecessary()
		{
			if (!m_hasResetSinceLastResume && Time.realtimeSinceStartup < m_lastResumeTime + 1f)
			{
				StartCoroutine(WaitThenReset(forceLogin: false));
				return true;
			}
			return false;
		}

		public void Exit()
		{
			m_exiting = true;
			if (ExceptionReporter.Get().Busy)
			{
				StartCoroutine(WaitThenExit());
			}
			else
			{
				GeneralUtils.ExitApplication();
			}
		}

		public bool IsExiting()
		{
			return m_exiting;
		}

		public bool HasFocus()
		{
			return m_focused;
		}

		public bool AddFocusChangedListener(FocusChangedCallback callback)
		{
			return AddFocusChangedListener(callback, null);
		}

		public bool AddFocusChangedListener(FocusChangedCallback callback, object userData)
		{
			FocusChangedListener focusChangedListener = new FocusChangedListener();
			focusChangedListener.SetCallback(callback);
			focusChangedListener.SetUserData(userData);
			if (m_focusChangedListeners.Contains(focusChangedListener))
			{
				return false;
			}
			m_focusChangedListeners.Add(focusChangedListener);
			return true;
		}

		public bool RemoveFocusChangedListener(FocusChangedCallback callback)
		{
			return RemoveFocusChangedListener(callback, null);
		}

		public bool RemoveFocusChangedListener(FocusChangedCallback callback, object userData)
		{
			FocusChangedListener focusChangedListener = new FocusChangedListener();
			focusChangedListener.SetCallback(callback);
			focusChangedListener.SetUserData(userData);
			return m_focusChangedListeners.Remove(focusChangedListener);
		}

		public float LastResetTime()
		{
			return m_lastResetTime;
		}

		public static bool UsingStandaloneLocalData()
		{
			return !string.IsNullOrEmpty(GetStandaloneLocalDataPath());
		}

		public static bool TryGetStandaloneLocalDataPath(string subPath, out string outPath)
		{
			string standaloneLocalDataPath = GetStandaloneLocalDataPath();
			if (!string.IsNullOrEmpty(standaloneLocalDataPath))
			{
				outPath = Path.Combine(standaloneLocalDataPath, subPath);
				return true;
			}
			outPath = null;
			return false;
		}

		public void UnloadUnusedAssets()
		{
			m_unloadUnusedAssets = true;
		}

		public void ControlANRMonitor(bool on)
		{
			if (PlatformSettings.IsMobileRuntimeOS && (on ^ m_ANRMonitorOn))
			{
				if (on)
				{
					float @float = Options.Get().GetFloat(Option.ANR_WAIT_SECONDS);
					float float2 = Options.Get().GetFloat(Option.ANR_THROTTLE);
					Log.ExceptionReporter.PrintInfo("ANR Monitor starts with seconds({0}) and throttle({1})", @float, float2);
					ExceptionReporter.Get().EnableANRMonitor(@float, float2);
					m_ANRMonitorOn = true;
				}
				else
				{
					Log.ExceptionReporter.PrintInfo("ANR Monitor stopped");
					ExceptionReporter.Get().EnableANRMonitor(0f, 0f);
					m_ANRMonitorOn = false;
				}
			}
		}

		private void RunStartup()
		{
			HearthstoneServices.InitializeRuntimeServices();
			QueueStartupJobs();
		}

		private IEnumerator<IAsyncJobResult> Job_CacheCommandLineArgs()
		{
			string[] commandLineArgs = CommandLineArgs;
			Debug.LogFormat("Command Line: {0}", string.Join(" ", commandLineArgs));
			yield break;
		}

		private void QueueStartupJobs()
		{
			WaitForGameDownloadManagerState waitForGameDownloadManagerState = new WaitForGameDownloadManagerState();
			JobDefinition jobDefinition = new JobDefinition("GameStrings.LoadAll", GameStrings.Job_LoadAll(), waitForGameDownloadManagerState);
			Processor.QueueJob("HearthstoneApplication.CacheCommandLineArgs", Job_CacheCommandLineArgs());
			Processor.QueueJob("HearthstoneApplication.InitializeMode", Job_InitializeMode());
			Processor.QueueJob(jobDefinition);
			Processor.QueueJob("UberText.LoadCachedData", UberText.Job_LoadCachedData());
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("HearthstoneApplication.SetWindowText", SetWindowText, jobDefinition.CreateDependency()));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_LogoAnimation", new LoadUIScreen("LogoAnimation.prefab:d2af09653759c2449b0426037b7fe9eb"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(IAssetLoader)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_StartupCamera", new LoadResource("Prefabs/StartupCamera", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_OverlayUI", new LoadUIScreen("OverlayUI.prefab:af7221edeeba8412cb55e9d6b58bb8dc"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(IAssetLoader)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_SplashScreen", new InstantiatePrefab("SplashScreen.prefab:c9347f27a19520a49af412dad268db15"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(IAssetLoader), typeof(LoginManager), typeof(ILoginService), typeof(Network), typeof(DemoMgr), new WaitForLogoAnimation(), new WaitForOverlayUI(), new WaitForTooltipPanelManager()));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_BaseUI", new LoadUIScreen("BaseUI.prefab:4d9d926d0cb3bc24380df232133b009b"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(SoundManager), typeof(Network), typeof(ITouchScreenService), typeof(IAssetLoader), typeof(LoginManager)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_AdventureConfig", new InstantiatePrefab("AdventureConfig.prefab:6c56645a84199884fbb351611099d9a8"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(AchieveManager), typeof(AdventureProgressMgr), typeof(IAssetLoader), typeof(SpecialEventManager)));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromDependency("Load_CardColorSwitcher", new InstantiatePrefab("CardColorSwitcher.prefab:b30c8322821f1524d9e08a59e78e2c85"), waitForGameDownloadManagerState, typeof(GameDownloadManager), typeof(SceneMgr), typeof(IAssetLoader)));
		}

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

		private static IEnumerator<IAsyncJobResult> Job_InitializeMode()
		{
			if (!s_initializedMode)
			{
				s_mode = ApplicationMode.PUBLIC;
				s_initializedMode = true;
				ReadCommandLineArgs();
			}
			yield break;
		}

		private IEnumerator WaitThenReset(bool forceLogin, bool forceNoAccountTutorial = false)
		{
			m_resetting = true;
			Navigation.Clear();
			yield return new WaitForEndOfFrame();
			ResetImmediately(forceLogin, forceNoAccountTutorial);
		}

		private void ResetImmediately(bool forceLogin, bool forceNoAccountTutorial = false)
		{
			Log.Reset.Print("HearthstoneApplication.ResetImmediately - forceLogin? " + forceLogin + "  Stack trace: " + Environment.StackTrace);
			TelemetryManager.Client().SendClientReset(forceLogin, forceNoAccountTutorial);
			Processor.JobQueue.ClearJobs();
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Splash/PreloadScreen"));
			if (this.WillReset != null)
			{
				this.WillReset();
			}
			m_resetting = true;
			m_lastResetTime = Time.realtimeSinceStartup;
			if (DialogManager.Get() != null)
			{
				DialogManager.Get().ClearAllImmediately();
			}
			if (UniversalInputManager.Get() != null)
			{
				UniversalInputManager.Get().SetSystemDialogActive(active: false);
				UniversalInputManager.Get().SetGameDialogActive(active: false);
			}
			if ((bool)Network.TUTORIALS_WITHOUT_ACCOUNT)
			{
				Network.SetShouldBeConnectedToAurora(forceLogin || Options.Get().GetBool(Option.CONNECT_TO_AURORA));
			}
			FatalErrorMgr.Get().ClearAllErrors();
			m_hasResetSinceLastResume = true;
			if (forceNoAccountTutorial)
			{
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: false);
				Network.SetShouldBeConnectedToAurora(shouldBeConnected: false);
			}
			if (this.Resetting != null)
			{
				this.Resetting();
			}
			TelemetryManager.Reset();
			MobileCallbackManager.SetTelemetryInfo(TelemetryManager.ProgramId, TelemetryManager.ProgramName, TelemetryManager.ProgramVersion, TelemetryManager.SessionId);
			Navigation.Clear();
			Processor.QueueJob("HearthstoneApplication.OnResetDownloadComplete", Job_OnResetDownloadComplete(), new WaitForGameDownloadManagerState());
			m_resetting = false;
			Log.Reset.Print("\tHearthstoneApplication.ResetImmediately completed");
		}

		public IEnumerator<IAsyncJobResult> Job_OnResetDownloadComplete()
		{
			if (IsLocaleChanged)
			{
				Log.Downloader.PrintInfo("Reload new locale data");
				yield return GameDbf.CreateLoadDbfJob();
				AchieveManager.Get().LoadAchievesFromDBF();
				UberText.RebuildAllUberText();
				GameStrings.ReloadAll();
				InnKeepersSpecial.Get().ResetAdUrl();
				IsLocaleChanged = false;
			}
			Processor.QueueJob("UberText.LoadCachedData", UberText.Job_LoadCachedData());
			if (HearthstoneServices.TryGet<FontTable>(out var service))
			{
				yield return new JobDefinition("FontTable.LoadFontDefs", service.Job_LoadFontDefs());
			}
			yield return HearthstoneJobs.CreateJobFromDependency("Load_LogoAnimation", new InstantiatePrefab("LogoAnimation.prefab:d2af09653759c2449b0426037b7fe9eb"));
			yield return HearthstoneJobs.CreateJobFromDependency("Load_SplashScreen", new InstantiatePrefab("SplashScreen.prefab:c9347f27a19520a49af412dad268db15"));
		}

		private IEnumerator<IAsyncJobResult> Job_PostDBFLoadOnLocaleChange()
		{
			yield break;
		}

		private IEnumerator WaitThenExit()
		{
			while (ExceptionReporter.Get().Busy)
			{
				yield return null;
			}
			GeneralUtils.ExitApplication();
		}

		private static void ReadCommandLineArgs()
		{
			string[] array = null;
			if (s_initializedMode)
			{
				string str = Vars.Key("Application.CommandLineOverride").GetStr(null);
				if (str != null)
				{
					array = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					s_cachedCmdLineArgsAreModified = true;
				}
			}
			if (array == null)
			{
				if (Environment.GetCommandLineArgs() != null)
				{
					array = Environment.GetCommandLineArgs().Skip(1).ToArray();
				}
				else
				{
					array = new string[0];
					Debug.LogFormat("Command Line is null");
				}
			}
			if (s_initializedMode)
			{
				string str2 = Vars.Key("Application.CommandLineAppend").GetStr(null);
				if (!string.IsNullOrEmpty(str2))
				{
					List<string> list = new List<string>(array);
					list.AddRange(str2.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
					array = list.ToArray();
					s_cachedCmdLineArgsAreModified = true;
				}
				if (s_cachedCmdLineArgsAreModified)
				{
					Debug.LogFormat("Modified Command Line: {0}", string.Join(" ", array));
				}
			}
			s_cachedCmdLineArgs = array;
			ProcessCommandLineArgs();
		}

		private static void ProcessCommandLineArgs()
		{
			for (int i = 0; i < s_cachedCmdLineArgs.Length; i++)
			{
				string[] array = s_cachedCmdLineArgs[i].Split('=');
				string text = array[0].ToLower();
				if (!(text == "confignameoverride"))
				{
					if (text == "optionsnameoverride" && array.Length > 1)
					{
						Vars.s_optionsNameOverride = array[1];
					}
				}
				else if (array.Length > 1)
				{
					Vars.s_configNameOverride = array[1];
				}
			}
		}

		public void ApplyInitializationSettingsFromConfig()
		{
			ConfigFile configFile = new ConfigFile();
			configFile.FullLoad(Vars.GetClientConfigPath());
			bool num = configFile.Get("Jobs.EnableMonitor", defaultVal: false);
			Processor.SetJobQueueMonitorEnabled(num);
			if (num)
			{
				string logPath = LogArchive.Get().LogPath;
				Processor.SetTrackedQueueDataFilePrefix(Path.GetFileNameWithoutExtension(logPath));
				Processor.SetTrackedQueueDataDirectory(Path.GetDirectoryName(logPath));
			}
			if (!IsInternal())
			{
				return;
			}
			TestType = configFile.Get("Test.TestType", string.Empty);
			if (!string.IsNullOrEmpty(TestType) && TestType == "JobDelay")
			{
				Processor.JobQueue.Debug.DelayTest = true;
				string text = configFile.Get("Test.MinDelay", string.Empty);
				string text2 = configFile.Get("Test.MaxDelay", string.Empty);
				if (float.TryParse(text, out var result))
				{
					Processor.JobQueue.Debug.JobDelayMin = result;
				}
				else if (!string.IsNullOrEmpty(text))
				{
					Log.ConfigFile.PrintWarning("Unable to evaluate Minimum  Job Delay value {0}, defaulting to {1}" + text, Processor.JobQueue.Debug.JobDelayMin);
				}
				if (float.TryParse(text2, out result))
				{
					Processor.JobQueue.Debug.JobDelayMax = result;
				}
				else if (!string.IsNullOrEmpty(text2))
				{
					Log.ConfigFile.PrintWarning("Unable to evaluate Maximum Job Delay value {0}, defaulting to {1}" + text2, Processor.JobQueue.Debug.JobDelayMax);
				}
			}
		}

		private static string GetStandaloneLocalDataPath()
		{
			return null;
		}

		private void FireFocusChangedEvent()
		{
			FocusChangedListener[] array = m_focusChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(m_focused);
			}
		}

		private void UpdateWorkingDirectory()
		{
			bool flag = false;
			if (!Application.isEditor && !PlatformSettings.IsMobileRuntimeOS)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
				string currentDirectory = Directory.GetCurrentDirectory();
				string text = ((!directoryInfo.Exists) ? string.Empty : ((directoryInfo.Parent == null) ? directoryInfo.FullName : directoryInfo.Parent.FullName));
				if (PlatformSettings.RuntimeOS == OSCategory.Mac && directoryInfo.Exists && directoryInfo.Parent != null && directoryInfo.Parent.Parent != null)
				{
					text = directoryInfo.Parent.Parent.FullName;
				}
				if (!string.IsNullOrEmpty(text) && !text.Equals(currentDirectory, StringComparison.CurrentCultureIgnoreCase))
				{
					flag = true;
					Directory.SetCurrentDirectory(text);
					Debug.LogFormat("Set current working dir from={0} to={1}", currentDirectory, Directory.GetCurrentDirectory());
				}
			}
			if (!flag)
			{
				Debug.LogFormat("Current working dir={0}", Directory.GetCurrentDirectory());
			}
		}

		private void SetWindowText()
		{
			IntPtr intPtr = FindWindow(null, "Hearthstone");
			if (intPtr != IntPtr.Zero)
			{
				SetWindowTextW(intPtr, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
			}
		}

		[DllImport("user32.dll")]
		public static extern int SetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string text);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);
	}
}
