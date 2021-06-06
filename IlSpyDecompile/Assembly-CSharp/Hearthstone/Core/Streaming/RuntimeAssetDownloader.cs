using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using agent;
using bgs;
using Blizzard.T5.Core;
using Blizzard.Telemetry;
using Blizzard.Telemetry.WTCG.Client;
using UnityEngine;

namespace Hearthstone.Core.Streaming
{
	public class RuntimeAssetDownloader : ICallbackHandler, IAssetDownloader
	{
		public enum AgentInternalState
		{
			STATE_NONE = 0,
			STATE_STARTING = 1000,
			STATE_DOWNLOADING = 1001,
			STATE_INSTALLING = 1002,
			STATE_UPDATING = 1003,
			STATE_READY = 1004,
			STATE_RUNNING = 1005,
			STATE_CLOSING = 1006,
			STATE_VERSIONING = 1007,
			STATE_WAITING = 1008,
			STATE_FINISHED = 1009,
			STATE_CANCELED = 1010,
			STATE_IMPEDED = 1100,
			STATE_ERROR_START = 1200,
			STATE_FAILED = 1200,
			STATE_CANCELING = 1202,
			STATE_OUT_OF_DATE = 1203,
			STATE_ERROR_END = 1299
		}

		public enum AgentState
		{
			ERROR = -1,
			NONE,
			INSTALLED,
			VERSION,
			WAIT_SERVICE,
			UNKNOWN_APPS,
			OPEN_APP_STORE,
			UPDATE_APK,
			UPDATE_MANIFEST,
			UPDATE_GLOBAL,
			UPDATE_OPTIONAL,
			UPDATE_LOCALIZED,
			AWAITING_WIFI,
			DISK_FULL
		}

		public enum KindOfUpdate
		{
			VERSION,
			APK_UPDATE,
			MANIFEST_UPDATE,
			GLOBAL_UPDATE,
			OPTIONAL_UPDATE,
			LOCALE_UPDATE
		}

		[Serializable]
		public class DownloadProgress
		{
			public List<TagDownloadStatus> m_items = new List<TagDownloadStatus>();
		}

		public class BundleDataList
		{
			private struct DataInfo
			{
				public string m_md5;

				public long m_size;

				public DataInfo(string md5, long size)
				{
					m_md5 = md5;
					m_size = size;
				}
			}

			private Dictionary<string, DataInfo> m_dataFiles = new Dictionary<string, DataInfo>();

			private string m_dataFolder;

			public bool isEnabled => Vars.Key("Mobile.GenDataList").GetBool(def: false);

			public BundleDataList(string dataFolder)
			{
				m_dataFolder = dataFolder;
			}

			public void Clear()
			{
				if (isEnabled)
				{
					m_dataFiles.Clear();
				}
			}

			public IEnumerator Print()
			{
				if (!isEnabled)
				{
					yield break;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(m_dataFolder);
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					if (!m_dataFiles.ContainsKey(fileInfo.Name))
					{
						m_dataFiles.Add(fileInfo.Name, new DataInfo(CalculateMD5(fileInfo.FullName), fileInfo.Length));
					}
					yield return null;
				}
				Log.Downloader.PrintInfo("== Data Directory Info ==");
				foreach (KeyValuePair<string, DataInfo> dataFile in m_dataFiles)
				{
					Log.Downloader.PrintInfo("{0}\t{1}\t{2}", dataFile.Key, dataFile.Value.m_size, dataFile.Value.m_md5);
				}
				Log.Downloader.PrintInfo("====================");
			}

			private static string CalculateMD5(string filename)
			{
				using MD5 mD = MD5.Create();
				using FileStream inputStream = File.OpenRead(filename);
				return BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", "").ToLowerInvariant();
			}
		}

		public const int IN_GAME_STREAMING_DEFAULT_SPEED = 512000;

		private const string AGENT_FOLDER_NAME = "__agent__";

		private const string NGDP_PRODUCT_NAME = "hsb";

		private const string TAG_APK_NAME = "tag_apk";

		private const string VERSION_SERVICE_PROXY_HOST = "hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net";

		private const string LIVE_SERVICE_STRING = "Live";

		private const string RETURN_TO_LIVE_SERVICE_STRING = "Back to Live";

		private const string UPDATE_GETSTATUS_JOB_NAME = "RuntimeAssetDownloader.GetStatus";

		private static readonly string[] ASSET_MANIFEST_TAGS = new string[2]
		{
			DownloadTags.GetTagString(DownloadTags.Quality.Manifest),
			DownloadTags.GetTagString(DownloadTags.Content.Base)
		};

		private const float UPDATE_PROGRESS_REPORT_INTERVAL = 15f;

		public Dictionary<AgentState, KindOfUpdate> UpdateOp = new Dictionary<AgentState, KindOfUpdate>
		{
			{
				AgentState.VERSION,
				KindOfUpdate.VERSION
			},
			{
				AgentState.UPDATE_APK,
				KindOfUpdate.APK_UPDATE
			},
			{
				AgentState.UPDATE_MANIFEST,
				KindOfUpdate.MANIFEST_UPDATE
			},
			{
				AgentState.UPDATE_GLOBAL,
				KindOfUpdate.GLOBAL_UPDATE
			},
			{
				AgentState.UPDATE_OPTIONAL,
				KindOfUpdate.OPTIONAL_UPDATE
			},
			{
				AgentState.UPDATE_LOCALIZED,
				KindOfUpdate.LOCALE_UPDATE
			}
		};

		private bool m_allowUnknownApps;

		private bool m_versionCalled;

		private bool m_agentInit;

		private bool m_cancelledByUser;

		private bool m_askCellPopup = true;

		private bool m_pausedByNetwork;

		private bool m_redrawDialog;

		private bool m_showResumeDialog;

		private bool m_cellularEnabledSession;

		private bool m_retriedUpdateWithNoSpaceError;

		private bool m_optionalDownload;

		private int[] m_disabledAPKUpdates;

		private string m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_GENERIC";

		private string m_store;

		private string[] m_disabledAdventuresForStreaming = new string[0];

		private float m_progress;

		private float m_prevProgress;

		private long m_deviceSpace;

		private int m_internalAgentError;

		private AgentState m_agentState;

		private AgentState m_savedAgentState;

		private ProductStatus m_agentStatus;

		private IDisposable m_callbackDisposer;

		private float m_updateStartTime;

		private float m_apkUpdateStartTime;

		private float m_lastUpdateProgressReportTime;

		private DownloadProgress m_tagDownloads = new DownloadProgress();

		private Dictionary<int, int> m_tagLocations = new Dictionary<int, int>();

		private HashSet<string> m_tempUniqueTags = new HashSet<string>();

		private int m_instantSpeed;

		private int m_instantMaxSpeed;

		private int m_inGameStreamingDefaultSpeed = 512000;

		private bool m_inGameStreamingOff;

		private TagIndicatorManager m_tagIndicatorManager = new TagIndicatorManager();

		private BundleDataList m_bundleDataList;

		private ReactiveBoolOption m_isDownloadAllFinished = ReactiveBoolOption.CreateInstance(Option.DOWNLOAD_ALL_FINISHED);

		private bool blockedByDiskFull;

		private TagDownloadStatus m_curDownloadStatus;

		private string m_installDataPath;

		private string[] m_dataFolders = new string[3]
		{
			INSTALL_PATH + "/__agent__",
			INSTALL_PATH + "/Data",
			INSTALL_PATH + "/Strings"
		};

		private static string INSTALL_PATH
		{
			get
			{
				if (PlatformSettings.RuntimeOS == OSCategory.Android)
				{
					return AndroidDeviceSettings.Get().assetBundleFolder;
				}
				return FileUtils.BasePersistentDataPath;
			}
		}

		private string UpdateState
		{
			get
			{
				return Options.Get().GetString(Option.NATIVE_UPDATE_STATE);
			}
			set
			{
				Options.Get().SetString(Option.NATIVE_UPDATE_STATE, value);
			}
		}

		private static bool IsEnabledUpdate => UpdateUtils.AreUpdatesEnabledForCurrentPlatform;

		private bool AskUnkownApps
		{
			get
			{
				return Options.Get().GetBool(Option.ASK_UNKNOWN_APPS);
			}
			set
			{
				Options.Get().SetBool(Option.ASK_UNKNOWN_APPS, value);
			}
		}

		private bool IsInGame
		{
			get
			{
				if (HearthstoneServices.TryGet<SceneMgr>(out var service))
				{
					return service.IsInGame();
				}
				return false;
			}
		}

		protected bool IsEnabledDownload
		{
			get
			{
				if (m_optionalDownload)
				{
					if (DownloadPermissionManager.DownloadEnabled)
					{
						return !IsInGame;
					}
					return false;
				}
				return true;
			}
		}

		protected bool canDownload
		{
			get
			{
				if (IsEnabledDownload && NetworkReachabilityManager.InternetAvailable)
				{
					if (blockedByCellPermission)
					{
						return TotalBytes < Options.Get().GetInt(Option.CELL_PROMPT_THRESHOLD);
					}
					return true;
				}
				return false;
			}
		}

		private bool blockedByCellPermission
		{
			get
			{
				if (NetworkReachabilityManager.OnCellular)
				{
					return !m_cellularEnabledSession;
				}
				return false;
			}
		}

		private bool VersionMismatchErrorEnabled => false;

		private bool RestartOnFalseHDDFullEnabled => Vars.Key("Mobile.RestartOnFalseHDDFull").GetBool(def: true);

		private string MobileMode => Vars.Key("Mobile.Mode").GetStr("Production");

		public long TotalBytes { get; protected set; }

		public long DownloadedBytes { get; protected set; }

		public long RequiredBytes { get; protected set; }

		private float ProgressPercent
		{
			get
			{
				if (m_cancelledByUser || TotalBytes == 0L)
				{
					return m_prevProgress;
				}
				return m_progress;
			}
		}

		private string InstallDataPath
		{
			get
			{
				if (string.IsNullOrEmpty(m_installDataPath))
				{
					m_installDataPath = INSTALL_PATH + "/Data";
					if (PlatformSettings.RuntimeOS == OSCategory.Android)
					{
						m_installDataPath = m_installDataPath + "/" + AndroidDeviceSettings.Get().InstalledTexture;
					}
				}
				return m_installDataPath;
			}
		}

		public AssetDownloaderState State { get; private set; }

		public string AgentLogPath => INSTALL_PATH + "__agent__/Logs";

		public bool IsReady { get; private set; }

		public bool IsRunningNewerBinaryThanLive { get; private set; }

		public bool IsVersionChanged { get; private set; }

		public bool IsAssetManifestReady { get; private set; }

		public bool DownloadAllFinished
		{
			get
			{
				return m_isDownloadAllFinished.Value;
			}
			set
			{
				m_isDownloadAllFinished.Set(value);
			}
		}

		public bool IsNewMobileVersionReleased
		{
			get
			{
				if (m_agentStatus == null || m_agentState == AgentState.ERROR)
				{
					return true;
				}
				int installedVersionCode = GetInstalledVersionCode();
				int versionCode = GetVersionCode(m_agentStatus.m_configuration.m_liveDisplayVersion);
				return installedVersionCode < versionCode;
			}
		}

		public bool DisabledErrorMessageDialog { get; set; }

		public string PatchOverrideUrl { get; private set; }

		public string VersionOverrideUrl { get; private set; }

		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				return m_disabledAdventuresForStreaming;
			}
			private set
			{
				m_disabledAdventuresForStreaming = value;
			}
		}

		public double BytesPerSecond { get; private set; }

		public int MaxDownloadSpeed
		{
			get
			{
				if (m_instantMaxSpeed != 0)
				{
					return m_instantMaxSpeed;
				}
				return Options.Get().GetInt(Option.MAX_DOWNLOAD_SPEED);
			}
			set
			{
				m_instantMaxSpeed = value;
				if (IsUpdating())
				{
					ResetDownloadSpeed(value);
				}
				else if (SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
				{
					AgentEmbeddedAPI.SetUpdateParams($"download_limit={value}");
				}
			}
		}

		public int InGameStreamingDefaultSpeed
		{
			get
			{
				return m_inGameStreamingDefaultSpeed;
			}
			set
			{
				if (value < 0)
				{
					m_inGameStreamingOff = true;
					return;
				}
				m_inGameStreamingOff = false;
				m_inGameStreamingDefaultSpeed = value;
			}
		}

		public int DownloadSpeedInGame
		{
			get
			{
				if (m_instantSpeed != 0)
				{
					return m_instantSpeed;
				}
				return Options.Get().GetInt(Option.STREAMING_SPEED_IN_GAME, InGameStreamingDefaultSpeed);
			}
			set
			{
				m_instantSpeed = value;
				if (State == AssetDownloaderState.DOWNLOADING && SceneMgr.Get().IsInGame())
				{
					ResetDownloadSpeed(m_instantSpeed);
				}
			}
		}

		private static string DownloadStatsPath => Path.Combine(INSTALL_PATH, "DownloadProgress.json");

		protected bool ShouldShowCellPopup
		{
			get
			{
				if (m_askCellPopup)
				{
					return TotalBytes > 0;
				}
				return false;
			}
		}

		public void OnTelemetry(TelemetryMessage msg)
		{
			MessageOptions options = null;
			if (!string.IsNullOrEmpty(msg.m_component))
			{
				options = new MessageOptions
				{
					Context = new Context
					{
						Program = new Context.ProgramInfo
						{
							Id = msg.m_component
						}
					}
				};
			}
			if (TelemetryManager.Client() != null)
			{
				TelemetryManager.Client().EnqueueMessage(msg.m_packageName, msg.m_messageName, msg.m_payload, options);
			}
		}

		public void OnPatchOverrideUrlChanged(OverrideUrlChangedMessage msg)
		{
			if (!msg.m_product.Equals("hsb"))
			{
				Log.Downloader.PrintError("Unknown product name for {0}", msg.m_product);
			}
			else if (!string.IsNullOrEmpty(msg.m_overrideUrl))
			{
				Log.Downloader.PrintInfo("Using PatchURL: " + msg.m_overrideUrl);
				try
				{
					string[] array = new Uri(msg.m_overrideUrl).Host.Split('.');
					if (array.Length != 0)
					{
						if (array[0].Length > 20)
						{
							PatchOverrideUrl = array[0].Substring(0, 20);
						}
						else
						{
							PatchOverrideUrl = array[0];
						}
					}
				}
				catch (Exception ex)
				{
					Log.Downloader.PrintError("Failed to set PatchURL: {0}", ex.Message);
				}
			}
			else if (!PatchOverrideUrl.Equals("Live"))
			{
				Log.Downloader.PrintInfo("Back to Live: " + msg.m_overrideUrl);
				PatchOverrideUrl = "Back to Live";
			}
		}

		public void OnVersionServiceOverrideUrlChanged(OverrideUrlChangedMessage msg)
		{
			Log.Downloader.PrintInfo("OnVersionServericeOverrideUrlChanged: {0} -- {1}", msg.m_product, msg.m_overrideUrl);
			if (!msg.m_product.Equals("hsb"))
			{
				Log.Downloader.PrintError("Unknown product name for {0}", msg.m_product);
			}
			else if ((string.IsNullOrEmpty(msg.m_overrideUrl) || !msg.m_overrideUrl.Contains("hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net")) && !VersionOverrideUrl.Equals("Live"))
			{
				VersionOverrideUrl = "Back to Live";
			}
		}

		public void OnNetworkStatusChangedMessage(NetworkStatusChangedMessage msg)
		{
			Log.Downloader.PrintInfo("OnNetworkStatusChangedMessage - cell {0}, wifi {1}, isCellAllowed {2}", msg.m_hasCell, msg.m_hasWifi, msg.m_isCellAllowed);
			if (m_pausedByNetwork)
			{
				m_redrawDialog = true;
			}
		}

		public void OnDownloadPausedDueToNetworkStatusChange(NetworkStatusChangedMessage msg)
		{
			Log.Downloader.PrintInfo("OnDownloadPausedDueToNetworkStatusChange - cell {0}, wifi {1}, isCellAllowed {2}", msg.m_hasCell, msg.m_hasWifi, msg.m_isCellAllowed);
			m_pausedByNetwork = true;
		}

		public void OnDownloadResumedDueToNetworkStatusChange(NetworkStatusChangedMessage msg)
		{
			Log.Downloader.PrintInfo("OnDownloadResumedDueToNetworkStatusChange - cell {0}, wifi {1}, isCellAllowed {2}", msg.m_hasCell, msg.m_hasWifi, msg.m_isCellAllowed);
			if (msg.m_isCellAllowed && msg.m_hasCell && !msg.m_hasWifi)
			{
				Log.Downloader.PrintInfo("User allowed to resume by using Cellular");
			}
			m_cellularEnabledSession = msg.m_isCellAllowed;
			m_pausedByNetwork = false;
		}

		public void OnDownloadPausedByUser()
		{
			Log.Downloader.PrintInfo("OnDownloadPausedByUser");
		}

		public void OnDownloadResumedByUser()
		{
			Log.Downloader.PrintInfo("OnDownloadResumedByUser");
		}

		public bool Initialize()
		{
			State = AssetDownloaderState.UNINITIALIZED;
			return SetInitialState();
		}

		public void Update(bool firstCall)
		{
			AgentInitializeWhenReady();
			ProcessMobile();
		}

		public void Shutdown()
		{
			Log.Downloader.PrintInfo("Shutdown");
			if (PlatformSettings.IsMobileRuntimeOS && !Application.isEditor)
			{
				AgentEmbeddedAPI.Shutdown();
			}
			Log.Downloader.PrintInfo("Disposed listeners for Agent");
			if (m_callbackDisposer != null)
			{
				m_callbackDisposer.Dispose();
			}
		}

		public TagDownloadStatus GetDownloadStatus(string[] tags)
		{
			return FindDownloadStatus(tags);
		}

		public void StopDownloads()
		{
			AgentEmbeddedAPI.CancelAllOperations();
		}

		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			return m_curDownloadStatus;
		}

		public void StartDownload(string[] tags, bool initialDownload, bool localeChanged)
		{
			KindOfUpdate kind;
			if (localeChanged)
			{
				TelemetryManager.Client().SendLocaleDataUpdateStarted(Localization.GetLocale().ToString());
				kind = KindOfUpdate.LOCALE_UPDATE;
				m_optionalDownload = false;
				m_askCellPopup = true;
			}
			else if (initialDownload)
			{
				if (UpdateState == "Updated")
				{
					GoToIdleState(assetManifestReady: true);
					return;
				}
				TelemetryManager.Client().SendDataUpdateStarted();
				kind = KindOfUpdate.GLOBAL_UPDATE;
				m_optionalDownload = false;
			}
			else
			{
				kind = KindOfUpdate.OPTIONAL_UPDATE;
				m_optionalDownload = true;
				m_askCellPopup = true;
			}
			StartDownloadInternal(tags, kind);
		}

		private void StartDownloadInternal(string[] tags, KindOfUpdate kind)
		{
			Log.Downloader.PrintInfo("StartDownload with {0}", string.Join(", ", tags));
			m_curDownloadStatus = CreateDownloadStatusIfNotExist(tags);
			if (!m_curDownloadStatus.Complete)
			{
				DownloadAllFinished = false;
				DoUpdate(kind);
			}
		}

		public void PauseAllDownloads()
		{
			AgentEmbeddedAPI.CancelAllOperations();
		}

		public void DeleteDownloadedData()
		{
			string[] dataFolders = m_dataFolders;
			for (int i = 0; i < dataFolders.Length; i++)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dataFolders[i]);
				if (directoryInfo.Exists)
				{
					directoryInfo.Delete(recursive: true);
				}
			}
			Log.Downloader.PrintInfo("ClearDownloadedData");
		}

		public bool IsBundleDownloaded(string bundleName)
		{
			if (!PlatformSettings.IsMobileRuntimeOS)
			{
				return true;
			}
			return m_tagIndicatorManager.IsReady(bundleName);
		}

		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
			if (State != AssetDownloaderState.DOWNLOADING)
			{
				return;
			}
			if (prevMode != SceneMgr.Mode.GAMEPLAY && nextMode == SceneMgr.Mode.GAMEPLAY)
			{
				if (m_inGameStreamingOff)
				{
					AgentEmbeddedAPI.CancelAllOperations();
					State = AssetDownloaderState.PAUSED_DURING_GAME;
					return;
				}
				ResetDownloadSpeed(DownloadSpeedInGame);
			}
			if (!m_inGameStreamingOff && prevMode == SceneMgr.Mode.GAMEPLAY && nextMode != SceneMgr.Mode.GAMEPLAY)
			{
				ResetDownloadSpeed(MaxDownloadSpeed);
			}
		}

		public void PrepareRestart()
		{
			GameStrings.LoadNative();
			ResetToUpdate();
		}

		public void DoPostTasksAfterInitialDownload()
		{
			Log.Downloader.PrintInfo("Process the post tasks after finishing initial data");
			FilterForiCloud();
		}

		private float ElapsedTimeFromStart(float startTime)
		{
			return Time.realtimeSinceStartup - startTime;
		}

		private static int GetTagsHashCode(string[] tags)
		{
			int num = 17;
			foreach (string text in tags)
			{
				num ^= text.GetHashCode();
			}
			return num ^ Localization.GetLocaleHashCode();
		}

		private TagDownloadStatus CreateDownloadStatusIfNotExist(string[] tags)
		{
			int tagsHashCode = GetTagsHashCode(tags);
			TagDownloadStatus tagDownloadStatus;
			if (m_tagLocations.TryGetValue(tagsHashCode, out var value))
			{
				tagDownloadStatus = m_tagDownloads.m_items[value];
			}
			else
			{
				tagDownloadStatus = new TagDownloadStatus();
				tagDownloadStatus.Tags = tags;
				m_tagLocations[tagsHashCode] = m_tagDownloads.m_items.Count;
				m_tagDownloads.m_items.Add(tagDownloadStatus);
			}
			return tagDownloadStatus;
		}

		private TagDownloadStatus FindDownloadStatus(string[] tags)
		{
			tags = RemoveRepeatedTags(tags);
			TagDownloadStatus tagDownloadStatus = CreateDownloadStatusIfNotExist(tags);
			if (!tagDownloadStatus.Complete)
			{
				tagDownloadStatus.Complete = m_tagIndicatorManager.IsReady(tagDownloadStatus.Tags);
			}
			return tagDownloadStatus;
		}

		private string[] RemoveRepeatedTags(string[] tags)
		{
			m_tempUniqueTags.Clear();
			bool flag = false;
			string[] array = tags;
			foreach (string item in array)
			{
				if (!m_tempUniqueTags.Add(item))
				{
					flag = true;
				}
			}
			if (flag)
			{
				tags = m_tempUniqueTags.ToArray();
			}
			return tags;
		}

		private void StartApkDownload()
		{
			StartDownloadInternal(new string[1] { "apk" }, KindOfUpdate.APK_UPDATE);
		}

		private void DeserializeDownloadStats()
		{
			if (!File.Exists(DownloadStatsPath))
			{
				return;
			}
			try
			{
				string json = File.ReadAllText(DownloadStatsPath);
				m_tagDownloads = JsonUtility.FromJson<DownloadProgress>(json);
				int pos = 0;
				m_tagDownloads.m_items.ForEach(delegate(TagDownloadStatus i)
				{
					m_tagLocations[GetTagsHashCode(i.Tags)] = pos++;
				});
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Unable to deserialize {0}: {1}", DownloadStatsPath, ex);
			}
		}

		private void SerializeDownloadStats()
		{
			try
			{
				string contents = JsonUtility.ToJson(m_tagDownloads, !HearthstoneApplication.IsPublic());
				File.WriteAllText(DownloadStatsPath, contents);
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Unable to serialize {0}: {1}", DownloadStatsPath, ex);
			}
		}

		private void DeleteDownloadStats()
		{
			if (File.Exists(DownloadStatsPath))
			{
				try
				{
					File.Delete(DownloadStatsPath);
					m_tagDownloads.m_items.Clear();
					m_tagLocations.Clear();
				}
				catch (Exception ex)
				{
					Error.AddDevFatal("Failed to delete the stats file({0}): {1}", DownloadStatsPath, ex.Message);
				}
			}
		}

		private void AgentInitializeWhenReady()
		{
			if (!AndroidDeviceSettings.Get().m_determineSDCard || m_agentInit)
			{
				return;
			}
			m_tagIndicatorManager.Initialize(InstallDataPath);
			VersionConfigurationService versionConfigurationService = HearthstoneServices.Get<VersionConfigurationService>();
			if (!versionConfigurationService.IsPipelinePortNeeded() || !versionConfigurationService.IsRequestingData)
			{
				AndroidDeviceSettings.Get().DeleteOldNotificationChannels();
				Log.Downloader.PrintInfo("Set listeners for Agent");
				m_callbackDisposer = AgentEmbeddedAPI.Subscribe(this);
				string clientToken = versionConfigurationService.GetClientToken();
				if (!string.IsNullOrEmpty(clientToken))
				{
					Log.Downloader.PrintInfo("Token is specified: {0}", clientToken);
				}
				string nGDPRegion = GetNGDPRegion();
				m_agentState = AgentState.WAIT_SERVICE;
				Log.Downloader.PrintInfo("initialization: " + INSTALL_PATH + ", region: " + nGDPRegion);
				if (!AgentEmbeddedAPI.Initialize(INSTALL_PATH, Path.Combine(Logger.LogsPath, "AgentLogs"), clientToken, nGDPRegion))
				{
					Error.AddDevFatal("Failed to initialize Agent service");
					m_agentState = AgentState.ERROR;
					State = AssetDownloaderState.ERROR;
				}
				else
				{
					m_bundleDataList = new BundleDataList(InstallDataPath);
					m_agentInit = true;
					AgentEmbeddedAPI.SetTelemetry(enable: true);
					DeserializeDownloadStats();
					StartVersion();
				}
			}
		}

		private bool ProcessMobile()
		{
			if (!IsEnabledDownload)
			{
				return false;
			}
			m_agentStatus = (m_agentInit ? AgentEmbeddedAPI.GetStatus() : null);
			if (m_agentState == AgentState.ERROR && (m_agentStatus == null || !m_agentStatus.m_cachedState.m_baseState.m_playable))
			{
				State = AssetDownloaderState.ERROR;
				if (!m_optionalDownload)
				{
					StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get(m_errorMsg), GameStrings.Get("GLOBAL_QUIT"), delegate
					{
						ShutdownApplication();
					});
				}
				return true;
			}
			if (!m_agentInit || m_agentState == AgentState.NONE)
			{
				return false;
			}
			if (m_agentStatus == null)
			{
				return false;
			}
			if (IsWaitingAction())
			{
				Log.Downloader.PrintInfo("Waiting message!!!" + m_agentState);
				return false;
			}
			if (m_agentState == AgentState.AWAITING_WIFI)
			{
				if (canDownload)
				{
					if (m_cancelledByUser)
					{
						if (!m_showResumeDialog)
						{
							ShowResumeMessage();
							m_showResumeDialog = true;
						}
					}
					else
					{
						Log.Downloader.PrintInfo("Download start again with " + m_savedAgentState);
						StartupDialog.Destroy();
						DoUpdate(UpdateOp[m_savedAgentState]);
					}
				}
				else if (m_redrawDialog)
				{
					ShowNetworkDialog();
					m_redrawDialog = false;
				}
				return false;
			}
			if (!IsUpdating())
			{
				Log.Downloader.PrintWarning("Not updating message!!!" + m_agentState);
				return true;
			}
			int num = 0;
			AgentInternalState agentState;
			if (m_agentState == AgentState.VERSION)
			{
				agentState = (AgentInternalState)m_agentStatus.m_cachedState.m_versionProgress.m_agentState;
				num = m_agentStatus.m_cachedState.m_versionProgress.m_error;
			}
			else
			{
				agentState = (AgentInternalState)m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_agentState;
				num = m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error;
				if (agentState == AgentInternalState.STATE_UPDATING || agentState == AgentInternalState.STATE_READY || agentState == AgentInternalState.STATE_FINISHED)
				{
					UpdateDownloadedBytes();
				}
			}
			switch (agentState)
			{
			case AgentInternalState.STATE_READY:
			case AgentInternalState.STATE_FINISHED:
				Log.Downloader.PrintInfo("Done!!! state=" + agentState);
				if (m_agentState == AgentState.VERSION)
				{
					ProcessInBlobGameSettings();
					string currentVersionStr = m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr;
					string liveDisplayVersion = m_agentStatus.m_configuration.m_liveDisplayVersion;
					TelemetryManager.Client().SendVersionFinished(currentVersionStr, liveDisplayVersion);
					Log.Downloader.PrintInfo("Current version: " + currentVersionStr + ", Live version: " + liveDisplayVersion);
					bool hasNewBinary;
					if (liveDisplayVersion.IndexOf('.') == -1)
					{
						VersionFetchFailed();
					}
					else if (ShouldCheckBinaryUpdate(currentVersionStr, liveDisplayVersion, out hasNewBinary))
					{
						IsVersionChanged = true;
						ResetToUpdate();
						if (PlatformSettings.RuntimeOS == OSCategory.Android)
						{
							if (!AndroidDeviceSettings.Get().AllowUnknownApps())
							{
								if (AskUnkownApps)
								{
									UpdateState = "UnknownApps";
									m_agentState = AgentState.UNKNOWN_APPS;
									AndroidDeviceSettings.Get().TriggerUnknownSources("MobileCallbackManager.UnknownSourcesListener");
								}
								else
								{
									StartApkDownload();
								}
							}
							else
							{
								m_allowUnknownApps = true;
								StartApkDownload();
							}
						}
						else if (hasNewBinary)
						{
							GoToIdleState(assetManifestReady: true);
						}
						else
						{
							OpenAppStore();
						}
					}
					else if (m_agentState != AgentState.ERROR)
					{
						GoToIdleState(assetManifestReady: true);
					}
				}
				else if (m_agentState == AgentState.UPDATE_APK)
				{
					Log.Downloader.PrintInfo("UPDATE_APK Done!!!");
					m_deviceSpace = FreeSpace.Measure();
					TelemetryManager.Client().SendUpdateFinished(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)m_deviceSpace / 1048576f, ElapsedTimeFromStart(m_apkUpdateStartTime));
					TryToInstallAPK();
				}
				else if (m_agentState == AgentState.UPDATE_MANIFEST)
				{
					GoToIdleState(assetManifestReady: true);
				}
				else if (m_agentState == AgentState.UPDATE_GLOBAL)
				{
					Log.Downloader.PrintInfo("UPDATE_GLOBAL Done!!!");
					TelemetryManager.Client().SendDataUpdateFinished(ElapsedTimeFromStart(m_updateStartTime), DownloadedBytes, TotalBytes);
					UpdateState = "Updated";
					GoToIdleState(assetManifestReady: true);
				}
				else if (m_agentState == AgentState.UPDATE_OPTIONAL)
				{
					Log.Downloader.PrintInfo("UPDATE_OPTIONAL Done!!!");
					TelemetryManager.Client().SendRuntimeUpdate(ElapsedTimeFromStart(m_updateStartTime), RuntimeUpdate.Intention.DONE);
					GoToIdleState(assetManifestReady: true);
					DownloadAllFinished = true;
					PrintDataList();
				}
				else if (m_agentState == AgentState.UPDATE_LOCALIZED)
				{
					Log.Downloader.PrintInfo("UPDATE_LOCALIZED Done!!!");
					TelemetryManager.Client().SendLocaleDataUpdateFinished(ElapsedTimeFromStart(m_updateStartTime), DownloadedBytes, TotalBytes);
					UpdateState = "Updated";
					GoToIdleState(assetManifestReady: true);
				}
				else
				{
					Log.Downloader.PrintError("State error: " + m_agentState);
					m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_UPDATE";
					m_agentState = AgentState.ERROR;
				}
				break;
			case AgentInternalState.STATE_IMPEDED:
				State = AssetDownloaderState.AGENT_IMPEDED;
				Log.Downloader.PrintInfo("Impeded!!!");
				break;
			case AgentInternalState.STATE_CANCELED:
				if (m_pausedByNetwork && !canDownload)
				{
					Log.Downloader.PrintInfo("Circumstances have changed from Agent.  Stopping download.");
					StopDownloading();
				}
				else if (!m_cancelledByUser)
				{
					Log.Downloader.PrintInfo("Canceled!!!");
					m_prevProgress = ProgressPercent;
					m_savedAgentState = m_agentState;
					m_cancelledByUser = true;
					ShowResumeMessage();
				}
				break;
			case AgentInternalState.STATE_ERROR_START:
				if (m_agentState == AgentState.VERSION)
				{
					if (UpdateState == "Updated")
					{
						Log.Downloader.PrintInfo("Version failure but allow to play");
						GoToIdleState(assetManifestReady: true);
					}
					else
					{
						VersionFetchFailed();
					}
				}
				else
				{
					if (num == m_internalAgentError)
					{
						break;
					}
					_ = m_agentStatus.m_cachedState;
					if (m_agentState == AgentState.UPDATE_GLOBAL)
					{
						TelemetryManager.Client().SendDataUpdateFailed(ElapsedTimeFromStart(m_updateStartTime), DownloadedBytes, TotalBytes, num);
					}
					if (m_agentState == AgentState.UPDATE_LOCALIZED)
					{
						TelemetryManager.Client().SendLocaleDataUpdateFailed(ElapsedTimeFromStart(m_updateStartTime), DownloadedBytes, TotalBytes, num);
					}
					m_deviceSpace = FreeSpace.Measure();
					Log.Downloader.PrintInfo("Measured free space: {0}", m_deviceSpace);
					if (m_deviceSpace < RequiredBytes - DownloadedBytes)
					{
						Log.Downloader.PrintWarning("Agent might be failed because of the space problem.");
						num = 2101;
					}
					if (num == 801 || num == 2101)
					{
						if (RestartOnFalseHDDFullEnabled && m_deviceSpace > RequiredBytes - DownloadedBytes)
						{
							if (!m_retriedUpdateWithNoSpaceError)
							{
								DoUpdate(UpdateOp[m_agentState]);
								m_retriedUpdateWithNoSpaceError = true;
								Log.Downloader.PrintWarning("Received a false 'out of space' error. Retrying.");
							}
							else
							{
								Log.Downloader.PrintError("Received a false 'out of space' error again. Stop.");
								StopDownloading();
							}
						}
						else
						{
							Log.Downloader.PrintWarning("Out of space!");
							blockedByDiskFull = true;
							StopDownloading();
						}
					}
					else
					{
						Error.AddDevFatal("Unidentified error Error={0}", num);
						if (!canDownload)
						{
							Log.Downloader.PrintError("failed to download.  Stopping download.");
						}
						else
						{
							m_agentState = AgentState.ERROR;
							m_errorMsg = GetAgentErrorMessage(num);
						}
						StopDownloading();
					}
				}
				break;
			case AgentInternalState.STATE_UPDATING:
				m_tagIndicatorManager.Check();
				if (m_agentState == AgentState.VERSION)
				{
					break;
				}
				if (!IsAssetManifestReady && FindDownloadStatus(ASSET_MANIFEST_TAGS).Complete)
				{
					Log.Downloader.PrintInfo("Asset manifest is ready!");
					SetAssetManifestReady();
				}
				Log.Downloader.PrintInfo("Downloading: {0} / {1}", DownloadedBytes, TotalBytes);
				State = AssetDownloaderState.DOWNLOADING;
				if (m_cancelledByUser)
				{
					m_cancelledByUser = false;
					StartupDialog.Destroy();
					if (!canDownload && m_optionalDownload)
					{
						Log.Downloader.PrintInfo("Agent Service has been resumed.");
						DownloadPermissionManager.DownloadEnabled = true;
					}
				}
				else if (!canDownload)
				{
					Log.Downloader.PrintInfo("Circumstances have changed.  Stopping download.");
					StopDownloading();
				}
				if (ElapsedTimeFromStart(m_lastUpdateProgressReportTime) > 15f)
				{
					if (m_agentState == AgentState.UPDATE_APK)
					{
						TelemetryManager.Client().SendUpdateProgress(ElapsedTimeFromStart(m_apkUpdateStartTime), DownloadedBytes, TotalBytes);
						m_lastUpdateProgressReportTime = Time.realtimeSinceStartup;
					}
					else if (m_agentState == AgentState.UPDATE_GLOBAL)
					{
						TelemetryManager.Client().SendDataUpdateProgress(ElapsedTimeFromStart(m_updateStartTime), DownloadedBytes, TotalBytes);
						m_lastUpdateProgressReportTime = Time.realtimeSinceStartup;
					}
				}
				break;
			}
			m_internalAgentError = num;
			return false;
		}

		private void SetAssetManifestReady()
		{
			IsAssetManifestReady = true;
			m_tagIndicatorManager.Check();
		}

		private bool ShouldCheckBinaryUpdate(string curVer, string liveVer, out bool hasNewBinary)
		{
			hasNewBinary = false;
			if (UpdateState == "UpdateAPK")
			{
				return true;
			}
			int installedVersionCode = GetInstalledVersionCode();
			int[] array = new int[4] { 20, 4, 0, installedVersionCode };
			if (string.IsNullOrEmpty(curVer))
			{
				try
				{
					if (installedVersionCode > 0 && GetSplitVersion(liveVer, out var versionInt))
					{
						int num = CompareVersions(versionInt, array);
						if (num < 0)
						{
							Log.Downloader.PrintError("The binary is newer than the live version: liveVer:{0} binaryVer:{1}", liveVer, array[0] + "." + array[1] + ".?." + array[3]);
							if (VersionMismatchErrorEnabled)
							{
								m_errorMsg = "GLUE_LOADINGSCREEN_MISMATCHED_VERSION";
								m_agentState = AgentState.ERROR;
							}
							return false;
						}
						if (num > 0)
						{
							Log.Downloader.PrintInfo("The binary version is older than the live version: liveVer:{0} binaryVer:{1}", liveVer, array[0] + "." + array[1] + ".?." + array[3]);
							return true;
						}
					}
				}
				catch (Exception ex)
				{
					Error.AddDevFatal("Failed to check the binary version with curVer:{0} liveVer:{1}: {2}", curVer, liveVer, ex.Message);
				}
				UpdateState = "Update";
			}
			else
			{
				if (curVer != liveVer)
				{
					Log.Downloader.PrintInfo("New version is detected");
					if (!NeedBinaryUpdate(report: false))
					{
						Log.Downloader.PrintInfo("It's new version already. {0}", installedVersionCode);
						hasNewBinary = true;
					}
					return true;
				}
				if (GetSplitVersion(curVer, out var versionInt2) && CompareVersions(versionInt2, array, compareMajorMinorOnly: true) > 0)
				{
					Log.Downloader.PrintError("Agent already has the new version strangely, let's try to update the binary.");
					return true;
				}
			}
			return false;
		}

		private void ResetToUpdate()
		{
			m_tagIndicatorManager.ClearAllIndicators();
			DeleteDownloadStats();
			DownloadAllFinished = false;
			IsAssetManifestReady = false;
			UpdateState = "Update";
			ClearDataList();
		}

		protected int CompareVersions(int[] version1, int[] version2, bool compareMajorMinorOnly = false)
		{
			try
			{
				int num = version1[0] - version2[0];
				int num2 = version1[1] - version2[1];
				int num3 = GetBinaryVersionCode(version1) - GetBinaryVersionCode(version2);
				if (compareMajorMinorOnly)
				{
					num3 = 0;
				}
				if (num < 0 || (num == 0 && num2 < 0) || (num == 0 && num2 == 0 && num3 < 0))
				{
					return -1;
				}
				if (num > 0 || num2 > 0 || num3 > 0)
				{
					return 1;
				}
			}
			catch (Exception ex)
			{
				Error.AddDevFatal("Failed to compare two version array: {0}", ex.Message);
			}
			return 0;
		}

		private void FilterForiCloud()
		{
			if (PlatformSettings.RuntimeOS != OSCategory.iOS)
			{
				return;
			}
			string[] array = new string[5]
			{
				LocalOptions.OptionsPath,
				Log.ConfigPath,
				Logger.LogsPath,
				FileUtils.CachePath,
				INSTALL_PATH + "/Unity"
			};
			string[] array2 = new string[array.Length + m_dataFolders.Length];
			array.CopyTo(array2, 0);
			m_dataFolders.CopyTo(array2, array.Length);
			string[] array3 = array2;
			foreach (string text in array3)
			{
				if (File.Exists(text) && !UpdateUtils.addSkipBackupAttributeToItemAtPath(text))
				{
					Log.Downloader.PrintError("Failed to exclude from iCloud - " + text);
				}
			}
		}

		private bool SetInitialState()
		{
			PatchOverrideUrl = "Live";
			VersionOverrideUrl = "Live";
			m_store = AndroidDeviceSettings.Get().m_HSStore;
			if (IsEnabledUpdate)
			{
				GameStrings.LoadNative();
				SanitizeDataFolder();
				AndroidDeviceSettings.Get().AskForSDCard();
			}
			else
			{
				if (Application.isEditor && PlatformSettings.IsEmulating)
				{
					GameStrings.LoadNative();
					m_agentState = AgentState.VERSION;
					m_versionCalled = true;
					return true;
				}
				m_agentState = AgentState.NONE;
			}
			return true;
		}

		private void SanitizeDataFolder()
		{
			Log.Downloader.PrintInfo("SanitizeDataFolder");
			try
			{
				if (INSTALL_PATH == null)
				{
					return;
				}
				ClearAPKIndicator();
				string text = Path.Combine(INSTALL_PATH, "Data");
				if (!Directory.Exists(text))
				{
					return;
				}
				UpdateUtils.CleanupOldAssetBundles(InstallDataPath);
				int num = 0;
				string[] array = new string[5] { "atc", "astc", "dxt", "pvrtc", "etc1" };
				string[] directories = Directory.GetDirectories(text);
				foreach (string path in directories)
				{
					string foundTexture = Path.GetFileName(path);
					if (Array.Exists(array, (string s) => s == foundTexture))
					{
						num++;
						Log.Downloader.PrintInfo("textureFormatFolder: " + foundTexture);
					}
				}
				if (num > 1)
				{
					Log.Downloader.PrintInfo("Removing TACT install folder...");
					Directory.Delete(Path.Combine(INSTALL_PATH, "__agent__"), recursive: true);
					Directory.Delete(Path.Combine(INSTALL_PATH, "Strings"), recursive: true);
					Directory.Delete(Path.Combine(INSTALL_PATH, "Data"), recursive: true);
					Directory.Delete(Path.Combine(INSTALL_PATH, "apk"), recursive: true);
					return;
				}
				Log.Downloader.PrintInfo("Trying to clean Data folder");
				directories = array;
				foreach (string text2 in directories)
				{
					string path2 = Path.Combine(text, text2);
					if (text2 != AndroidDeviceSettings.Get().InstalledTexture && Directory.Exists(path2))
					{
						Log.Downloader.PrintInfo("Delete the unsued texture folder - {0}", text2);
						Directory.Delete(path2, recursive: true);
					}
				}
			}
			catch (Exception ex)
			{
				Error.AddDevFatal("Failed to sanitize Data folder: {0}", ex.Message);
			}
		}

		private bool IsWaitingAction()
		{
			if (m_agentState != AgentState.UNKNOWN_APPS && m_agentState != AgentState.WAIT_SERVICE)
			{
				return m_agentState == AgentState.OPEN_APP_STORE;
			}
			return true;
		}

		private bool IsUpdating()
		{
			if (m_agentState != 0)
			{
				return m_agentState != AgentState.ERROR;
			}
			return false;
		}

		private void GoToIdleState(bool assetManifestReady)
		{
			m_agentState = AgentState.NONE;
			State = AssetDownloaderState.IDLE;
			IsReady = true;
			if (assetManifestReady)
			{
				SetAssetManifestReady();
			}
		}

		private void ClearAPKIndicator()
		{
			string text = Path.Combine(Path.Combine(INSTALL_PATH, "apk"), "tag_apk");
			try
			{
				if (File.Exists(text))
				{
					Log.Downloader.PrintWarning("Deleted the APK indicator '{0}'", text);
					File.Delete(text);
				}
			}
			catch (Exception ex)
			{
				Error.AddDevFatal("Failed to delete the APK indicator '{0}': {1}", text, ex.Message);
			}
		}

		private string GetAdditionalTags(KindOfUpdate kind)
		{
			switch (kind)
			{
			case KindOfUpdate.VERSION:
				return "";
			case KindOfUpdate.APK_UPDATE:
				if (!(m_store == "CN") || !m_allowUnknownApps)
				{
					return "";
				}
				return $"{m_store}? {MobileMode}?";
			default:
			{
				string text = "";
				string[] tags = m_curDownloadStatus.Tags;
				foreach (string arg in tags)
				{
					text += $" {arg}?";
				}
				if (PlatformSettings.RuntimeOS == OSCategory.Android)
				{
					text += $" {AndroidDeviceSettings.Get().InstalledTexture}?";
				}
				else if (PlatformSettings.RuntimeOS == OSCategory.iOS && Localization.GetLocale().ToString() == "zhCN" && PlatformSettings.LocaleVariant != LocaleVariant.China)
				{
					text += " GlobalCN?";
				}
				return text;
			}
			}
		}

		private int DoUpdate(KindOfUpdate kind)
		{
			m_showResumeDialog = false;
			int num = ((kind == KindOfUpdate.VERSION) ? AgentStartVersion() : AgentStartUpdateGlobal(kind));
			if (num != 0)
			{
				Error.AddDevFatal("DoUpdate({0}) Error={1}", kind.ToString(), num);
				m_errorMsg = ((num == 2410) ? "GLUE_LOADINGSCREEN_ERROR_UPDATE_CONFLICT" : "GLUE_LOADINGSCREEN_ERROR_UPDATE");
				m_agentState = AgentState.ERROR;
				return num;
			}
			if (!m_optionalDownload)
			{
				UpdateState = "Update";
			}
			switch (kind)
			{
			case KindOfUpdate.VERSION:
				m_agentState = AgentState.VERSION;
				UpdateState = "Version";
				break;
			case KindOfUpdate.APK_UPDATE:
				m_agentState = AgentState.UPDATE_APK;
				UpdateState = "UpdateAPK";
				m_apkUpdateStartTime = Time.realtimeSinceStartup;
				m_lastUpdateProgressReportTime = m_apkUpdateStartTime;
				m_deviceSpace = FreeSpace.Measure();
				TelemetryManager.Client().SendUpdateStarted(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, AndroidDeviceSettings.Get().InstalledTexture, InstallDataPath, (float)m_deviceSpace / 1048576f);
				break;
			case KindOfUpdate.MANIFEST_UPDATE:
				m_agentState = AgentState.UPDATE_MANIFEST;
				break;
			case KindOfUpdate.GLOBAL_UPDATE:
				m_agentState = AgentState.UPDATE_GLOBAL;
				m_updateStartTime = Time.realtimeSinceStartup;
				m_lastUpdateProgressReportTime = m_updateStartTime;
				break;
			case KindOfUpdate.OPTIONAL_UPDATE:
				m_agentState = AgentState.UPDATE_OPTIONAL;
				m_updateStartTime = Time.realtimeSinceStartup;
				break;
			case KindOfUpdate.LOCALE_UPDATE:
				m_agentState = AgentState.UPDATE_LOCALIZED;
				m_updateStartTime = Time.realtimeSinceStartup;
				break;
			}
			return num;
		}

		private int AgentStartUpdateGlobal(KindOfUpdate kind)
		{
			UserSettings userSettings = default(UserSettings);
			userSettings.m_region = GetNGDPRegion();
			userSettings.m_languages = Localization.GetLocale().ToString();
			userSettings.m_branch = GetBranch();
			userSettings.m_additionalTags = GetAdditionalTags(kind);
			UserSettings settings = userSettings;
			Log.Downloader.PrintInfo("ModifyProductInstall with locale: {0} and tags: {1} ", settings.m_languages, settings.m_additionalTags);
			int num = AgentEmbeddedAPI.ModifyProductInstall(ref settings);
			if (num != 0)
			{
				Log.Downloader.PrintWarning("1st ModifyProductInstall Error={0}", num);
				if (num != 2410)
				{
					return num;
				}
				AgentEmbeddedAPI.CancelAllOperations();
				num = AgentEmbeddedAPI.ModifyProductInstall(ref settings);
				if (num != 0)
				{
					Error.AddDevFatal("2nd ModifyProductInstall Error={0}", num);
					return num;
				}
			}
			if (m_curDownloadStatus != null)
			{
				m_curDownloadStatus.StartProgress = m_curDownloadStatus.Progress;
			}
			TotalBytes = 0L;
			DownloadedBytes = 0L;
			RequiredBytes = 0L;
			BytesPerSecond = 0.0;
			m_deviceSpace = -1L;
			m_cancelledByUser = false;
			NotificationUpdateSettings settings2 = new NotificationUpdateSettings
			{
				m_cellDataThreshold = Options.Get().GetInt(Option.CELL_PROMPT_THRESHOLD),
				m_isCellDataAllowed = m_cellularEnabledSession
			};
			num = AgentEmbeddedAPI.StartUpdate("", settings2);
			ResetDownloadSpeed(MaxDownloadSpeed);
			return num;
		}

		private int ResetDownloadSpeed(int speed)
		{
			Log.Downloader.PrintInfo("Set the download speed to {0}", speed);
			int num = AgentEmbeddedAPI.SetUpdateParams($"download_limit={speed}");
			if (num != 0)
			{
				Log.Downloader.PrintError("SetUpdateParams Error={0}", num);
			}
			return num;
		}

		private void UpdateDownloadedBytes()
		{
			TotalBytes = m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_expectedDownloadBytes;
			DownloadedBytes = m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_realDownloadedBytes;
			RequiredBytes = m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_expectedOriginalBytes;
			BytesPerSecond = m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_downloadRate;
			m_progress = (float)m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_progress;
			PrintStatus(m_agentStatus);
			m_curDownloadStatus.BytesDownloaded = DownloadedBytes;
			m_curDownloadStatus.BytesTotal = TotalBytes;
			m_curDownloadStatus.Progress = m_progress;
			if (TotalBytes > 0 && m_deviceSpace == -1)
			{
				Log.Downloader.PrintInfo("Total: {0}, Downloaded: {1}, Required: {2}, Speed: {3}", TotalBytes, DownloadedBytes, RequiredBytes, BytesPerSecond);
				m_deviceSpace = FreeSpace.Measure();
				Log.Downloader.PrintInfo("Measured free space: {0}", m_deviceSpace);
				if (RequiredBytes > m_deviceSpace)
				{
					Log.Downloader.PrintError("Device will run out of space during download.  {0} / {1}", RequiredBytes, m_deviceSpace);
					blockedByDiskFull = true;
					StopDownloading();
				}
			}
			SerializeDownloadStats();
		}

		private void PrintStatus(ProductStatus agentStatus)
		{
		}

		protected bool GetSplitVersion(string versionStr, out int[] versionInt)
		{
			Log.Downloader.PrintInfo("VersionStr=" + versionStr);
			try
			{
				List<string> list = new List<string>();
				string[] array = versionStr.Split('_');
				int num = 4;
				if (array.Length == 1)
				{
					list.AddRange(versionStr.Split('.'));
				}
				else
				{
					string str = Vars.Key("Mobile.BinaryVersion").GetStr("");
					string empty = string.Empty;
					if (!string.IsNullOrEmpty(str))
					{
						empty = "." + str;
						array[1] = array[1].Replace(empty, "");
					}
					list.AddRange(array[1].Split('-')[0].Split('.'));
					list.Add(array[0]);
					if (!string.IsNullOrEmpty(str))
					{
						list.Add(str);
						num++;
					}
				}
				versionInt = Array.ConvertAll(list.ToArray(), int.Parse);
				if (versionInt.Length < num)
				{
					throw new Exception("Version is too short");
				}
			}
			catch (Exception ex)
			{
				Error.AddDevFatal("Failed to parse the version string-'{0}': {1}", versionStr, ex.Message);
				versionInt = new int[0];
				return false;
			}
			return true;
		}

		private void VersionFetchFailed()
		{
			Error.AddDevFatal("Agent: Failed to get Version! - {0}", m_agentStatus.m_cachedState.m_versionProgress.m_error);
			m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_VERSION";
			m_agentState = AgentState.ERROR;
			TelemetryManager.Client().SendVersionError((uint)m_agentStatus.m_cachedState.m_versionProgress.m_error, (uint)m_agentStatus.m_cachedState.m_versionProgress.m_agentState, m_agentStatus.m_settings.m_languages, m_agentStatus.m_settings.m_region, m_agentStatus.m_settings.m_branch, m_agentStatus.m_settings.m_additionalTags);
		}

		private void ProcessInBlobGameSettings()
		{
			Log.Downloader.PrintInfo("Processing blob data...");
			string opaqueString = AgentEmbeddedAPI.GetOpaqueString("disabled_apk_update");
			if (!string.IsNullOrEmpty(opaqueString))
			{
				Log.Downloader.PrintInfo("Blob - disabled_apk_update: " + opaqueString);
				try
				{
					m_disabledAPKUpdates = Array.ConvertAll(opaqueString.Split(','), (string s) => int.Parse(s.Trim()));
				}
				catch (Exception ex)
				{
					Log.Downloader.PrintError("Failed to parse the 'disabled_apk_update' - {0}: {1}", opaqueString, ex.Message);
				}
			}
			InGameStreamingDefaultSpeed = -1;
			string opaqueString2 = AgentEmbeddedAPI.GetOpaqueString("disabled_adventures_for_streaming");
			if (!string.IsNullOrEmpty(opaqueString2))
			{
				Log.Downloader.PrintInfo("Blob - disabled_adventure_streaming: " + opaqueString2);
				m_disabledAdventuresForStreaming = opaqueString2.Split(',');
			}
			string opaqueString3 = AgentEmbeddedAPI.GetOpaqueString("anr_throttle");
			if (!string.IsNullOrEmpty(opaqueString3))
			{
				Log.Downloader.PrintInfo("Blob - anr_throttle: " + opaqueString3);
				string[] array = opaqueString3.Split(',');
				float @float = Options.Get().GetFloat(Option.ANR_THROTTLE);
				if (float.TryParse(array[0], out var result) && result != @float)
				{
					@float = result;
					Options.Get().SetFloat(Option.ANR_THROTTLE, @float);
				}
				float float2 = Options.Get().GetFloat(Option.ANR_WAIT_SECONDS);
				if (array.Length > 1 && float.TryParse(array[1], out var result2) && result2 != float2)
				{
					float2 = result2;
					Options.Get().SetFloat(Option.ANR_WAIT_SECONDS, float2);
				}
			}
			bool flag = GeneralUtils.ForceBool(AgentEmbeddedAPI.GetOpaqueString("use_legacy_login"));
			if (Vars.Key("Debug.FakeBlobUseLegacyLogin").HasValue)
			{
				flag = Vars.Key("Debug.FakeBlobUseLegacyLogin").GetBool(def: false);
				Log.Login.PrintInfo("Has debug override for blog login: {0}", flag);
			}
			if (flag)
			{
				Vars.Key("Mobile.UseMASDK").Set("false", permanent: false);
			}
		}

		private int InCreateProduct(Func<int> preFunc = null)
		{
			UserSettings userSettings = default(UserSettings);
			userSettings.m_region = GetNGDPRegion();
			userSettings.m_languages = Localization.GetLocale().ToString();
			userSettings.m_branch = GetBranch();
			UserSettings settings = userSettings;
			preFunc?.Invoke();
			int num = AgentEmbeddedAPI.CreateProductInstall("hsb", ref settings);
			if (num == 0)
			{
				Log.Downloader.PrintInfo("Installation is done! Version...");
				m_agentState = AgentState.INSTALLED;
			}
			else
			{
				Log.Downloader.PrintWarning("CreateProductInstall Error={0}", num.ToString());
			}
			return num;
		}

		private void CreateProduct()
		{
			int num = InCreateProduct();
			switch (num)
			{
			case 2410:
				num = InCreateProduct(() => AgentEmbeddedAPI.StartUninstall(""));
				break;
			case 0:
				return;
			}
			if (num != 0)
			{
				m_errorMsg = GetAgentErrorMessage(num);
				m_agentState = AgentState.ERROR;
			}
		}

		private void StartVersion()
		{
			if (m_versionCalled)
			{
				return;
			}
			m_versionCalled = true;
			SetPatchUrl();
			SetVersionOverrideUrl();
			m_agentStatus = AgentEmbeddedAPI.GetStatus();
			if (m_agentStatus == null || string.IsNullOrEmpty(m_agentStatus.m_product))
			{
				Log.Downloader.PrintInfo("initialization succeeded! Create product...");
				CreateProduct();
			}
			else
			{
				string languages = m_agentStatus.m_settings.m_languages;
				Log.Downloader.PrintInfo("initialization succeeded! Ready to update locale=" + languages);
				m_agentState = AgentState.INSTALLED;
			}
			if (m_agentState == AgentState.INSTALLED)
			{
				Log.Downloader.PrintInfo("StartVersion");
				int num = AgentStartVersion();
				if (num != 0)
				{
					m_agentState = AgentState.ERROR;
					Error.AddDevFatal("[Downloader] StartVersion Error={0}", num.ToString());
				}
				else
				{
					m_agentState = AgentState.VERSION;
				}
			}
		}

		private int AgentStartVersion()
		{
			TelemetryManager.Client().SendVersionStarted(0);
			return AgentEmbeddedAPI.StartVersion("");
		}

		private void SetVersionOverrideUrl()
		{
			if (!VersionConfigurationService.IsEnabledForCurrentPlatform())
			{
				return;
			}
			VersionConfigurationService versionConfigurationService = HearthstoneServices.Get<VersionConfigurationService>();
			VersionPipeline pipeline = versionConfigurationService.GetPipeline();
			if (!versionConfigurationService.IsPipelinePortNeeded())
			{
				if (pipeline == VersionPipeline.LIVE)
				{
					string url = $"{GetNGDPRegion()}.version.battle.net";
					string clientToken = versionConfigurationService.GetClientToken();
					Log.Downloader.PrintInfo("Setting version override to token for LIVE:{0}", clientToken);
					AgentEmbeddedAPI.SetVersionServiceUrlOverride("hsb", url, clientToken);
					VersionOverrideUrl = pipeline.ToString();
				}
			}
			else if (!versionConfigurationService.IsPortInformationAvailable())
			{
				Log.Downloader.PrintWarning("Port information not available for version override. Falling back to live");
			}
			else if (versionConfigurationService.IsPipelinePortNeeded())
			{
				int? pipelinePort = versionConfigurationService.GetPipelinePort();
				if (pipelinePort.HasValue)
				{
					string text = string.Format("tcp://{0}:{1}", "hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net", pipelinePort);
					string clientToken2 = versionConfigurationService.GetClientToken();
					Log.Downloader.PrintInfo("Setting version override to token:{0}, url:{1}", clientToken2, text);
					AgentEmbeddedAPI.SetVersionServiceUrlOverride("hsb", text, clientToken2);
					VersionOverrideUrl = pipeline.ToString();
				}
				else
				{
					Log.Downloader.PrintInfo("Couldn't get port for version service override port {0}, value was null", pipeline.ToString());
				}
			}
			else
			{
				Log.Downloader.PrintInfo("Not using version service override as pipline is {0}", pipeline.ToString());
			}
		}

		private string GetNGDPRegion()
		{
			return MobileDeviceLocale.GetCurrentRegionId() switch
			{
				constants.BnetRegion.REGION_US => "us", 
				constants.BnetRegion.REGION_EU => "eu", 
				constants.BnetRegion.REGION_KR => "kr", 
				constants.BnetRegion.REGION_TW => "kr", 
				constants.BnetRegion.REGION_CN => "cn", 
				_ => "us", 
			};
		}

		private string GetBranch()
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				return "android_" + m_store.ToLower();
			}
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				if (PlatformSettings.LocaleVariant == LocaleVariant.China)
				{
					return "ios_cn";
				}
				return "ios";
			}
			return string.Empty;
		}

		private void ShutdownApplication()
		{
			Log.Downloader.PrintInfo("ShutdownApplication");
			Shutdown();
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.Exit();
			}
			else
			{
				GeneralUtils.ExitApplication();
			}
		}

		private void OpenAppStore()
		{
			m_agentState = AgentState.OPEN_APP_STORE;
			OpenAppStoreAlert();
		}

		private bool InstallAPK()
		{
			if (!NeedBinaryUpdate(report: true))
			{
				return false;
			}
			if (SendToAppStore())
			{
				OpenAppStore();
			}
			else
			{
				string text = Path.Combine(Path.Combine(INSTALL_PATH, "apk"), $"Hearthstone_{m_store}_{MobileMode}.apk");
				Log.Downloader.PrintInfo("ApkPath: " + text);
				if (AndroidDeviceSettings.Get().m_AndroidSDKVersion < 24)
				{
					string text2 = Path.Combine(FileUtils.BaseExternalDataPath, "Hearthstone.apk");
					Log.Downloader.PrintInfo("Copy apk: " + text + " -> " + text2);
					try
					{
						File.Delete(text2);
						File.Copy(text, text2);
						text = text2;
					}
					catch (Exception ex)
					{
						Error.AddDevFatal("Failed to copy APK, Open app store instead: {0}", ex.Message);
						m_agentState = AgentState.OPEN_APP_STORE;
						OpenAppStore();
						return true;
					}
				}
				AndroidDeviceSettings.Get().ProcessInstallAPK(text, "MobileCallbackManager.InstallAPKListener");
			}
			return true;
		}

		private void TryToInstallAPK()
		{
			if (InstallAPK())
			{
				m_agentState = AgentState.NONE;
			}
			else
			{
				GoToIdleState(assetManifestReady: false);
			}
		}

		private bool SendToAppStore()
		{
			if (m_store == "Amazon" || m_store == "Google" || m_store == "CN_Huawei" || m_store == "OneStore")
			{
				return true;
			}
			if (m_store == "CN")
			{
				if (m_allowUnknownApps && AndroidDeviceSettings.Get().AllowUnknownApps() && !IsDisabledAPKUpdateVersion())
				{
					return false;
				}
			}
			else if (AndroidDeviceSettings.Get().IsNonStoreAppAllowed())
			{
				return false;
			}
			return true;
		}

		private bool IsDisabledAPKUpdateVersion()
		{
			bool flag = m_disabledAPKUpdates != null && Array.Exists(m_disabledAPKUpdates, (int s) => s == 84593);
			if (flag)
			{
				Log.Downloader.PrintInfo("The current version-{0} is disabled for APK update.", 84593);
			}
			return flag;
		}

		private bool NeedBinaryUpdate(bool report)
		{
			int installedVersionCode = GetInstalledVersionCode();
			string liveDisplayVersion = m_agentStatus.m_configuration.m_liveDisplayVersion;
			int binaryVersionCode = GetBinaryVersionCode(liveDisplayVersion);
			if (report)
			{
				Log.Downloader.PrintInfo("InstalledVersion: {0}, BinaryVersionFromLive: {1}", installedVersionCode, binaryVersionCode);
				int versionCode = GetVersionCode(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr);
				TelemetryManager.Client().SendApkUpdate(installedVersionCode, binaryVersionCode, versionCode);
			}
			return binaryVersionCode != installedVersionCode;
		}

		private int GetInstalledVersionCode()
		{
			int num = 0;
			try
			{
				num = int.Parse(Application.version.Split('.')[2]);
				if (num != 84593)
				{
					Log.Downloader.PrintError("Application.version is different from our setting");
					num = 84593;
					return num;
				}
				return num;
			}
			catch (Exception ex)
			{
				Error.AddDevFatal("Failed to read the installed version: {0}", ex.Message);
				return num;
			}
		}

		protected int GetVersionCode(string versionString)
		{
			if (GetSplitVersion(versionString, out var versionInt))
			{
				return versionInt[3];
			}
			return 0;
		}

		protected int GetBinaryVersionCode(string versionString)
		{
			if (GetSplitVersion(versionString, out var versionInt))
			{
				return GetBinaryVersionCode(versionInt);
			}
			return 0;
		}

		protected int GetBinaryVersionCode(int[] versionInt)
		{
			if (versionInt.Length <= 4)
			{
				return versionInt[3];
			}
			return versionInt[4];
		}

		private void ShowResumeMessage()
		{
			if (!m_optionalDownload)
			{
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_RESUME"), GameStrings.Get("GLOBAL_RESUME_GAME"), delegate
				{
					Log.Downloader.PrintInfo("Resume the update which has stopped.");
					DoUpdate(UpdateOp[m_savedAgentState]);
				});
			}
		}

		private void OpenAppStoreAlert()
		{
			TelemetryManager.Client().SendOpeningAppStore(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)m_deviceSpace / 1048576f, ElapsedTimeFromStart(m_apkUpdateStartTime));
			string key = ((m_store == "CN") ? "GLUE_LOADINGSCREEN_APK_UPDATE_FROM_WEBSITE" : "GLUE_LOADINGSCREEN_APK_UPDATE_FROM_APP_STORE");
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get(key), GameStrings.Get("GLUE_LOADINGSCREEN_OPEN_APP_STORE"), delegate
			{
				Log.Downloader.PrintInfo("Open App store");
				if (!AndroidDeviceSettings.Get().OpenAppStore())
				{
					StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_ERROR_CLIENT_CONFIG_MESSAGE"), GameStrings.Get("GLOBAL_QUIT"), null);
					Error.AddDevFatal("Invalid store in client.config");
				}
				ShutdownApplication();
			});
		}

		private string GetAgentErrorMessage(int errorCode)
		{
			switch (errorCode)
			{
			case 2100:
			case 2101:
			case 2110:
			case 2111:
			case 2112:
			case 2113:
			case 2115:
			case 2116:
			case 2118:
			case 2120:
			case 2121:
			case 2122:
			case 2123:
			case 2126:
				return "GLUE_LOADINGSCREEN_ERROR_FILESYSTEM_MESSAGE";
			default:
				return "GLUE_LOADINGSCREEN_ERROR_UPDATE";
			}
		}

		protected void StopDownloading()
		{
			if (IsUpdating())
			{
				m_savedAgentState = m_agentState;
				Log.Downloader.PrintInfo("Calling CancelAllOperations!");
				if (!m_pausedByNetwork)
				{
					State = AssetDownloaderState.IDLE;
					AgentEmbeddedAPI.CancelAllOperations();
					if (m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error != 0)
					{
						TelemetryManager.Client().SendUpdateError((uint)m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error, ElapsedTimeFromStart(m_apkUpdateStartTime));
					}
				}
			}
			if (blockedByDiskFull)
			{
				m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_DISK_SPACE";
				m_agentState = AgentState.ERROR;
				State = AssetDownloaderState.DISK_FULL;
				TelemetryManager.Client().SendNotEnoughSpaceError((ulong)m_deviceSpace, (ulong)TotalBytes, FileUtils.BaseExternalDataPath);
			}
			else
			{
				ShowNetworkDialog();
			}
		}

		private void ShowNetworkDialog()
		{
			Log.Downloader.PrintInfo("ShowNetworkDialog");
			StartupDialog.Destroy();
			if (blockedByCellPermission)
			{
				Log.Downloader.PrintInfo("Block by cell permission");
				if (ShouldShowCellPopup)
				{
					ShowCellularAllowance();
				}
				else
				{
					ShowAwaitingWifi();
				}
			}
			else if (!NetworkReachabilityManager.InternetAvailable)
			{
				ShowAwaitingWifi();
			}
			else if (!m_optionalDownload)
			{
				m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_UPDATE";
				m_agentState = AgentState.ERROR;
			}
		}

		private void ShowCellularAllowance()
		{
			Log.Downloader.PrintInfo("Asking for cell permission");
			string arg = DownloadStatusView.FormatBytesAsHumanReadable(TotalBytes - DownloadedBytes);
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_HEADER"), string.Format(GameStrings.Get(m_optionalDownload ? "GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_ADDITIONAL_BODY" : "GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_INITIAL_BODY"), arg), GameStrings.Get("GLOBAL_BUTTON_YES"), delegate
			{
				m_deviceSpace = FreeSpace.Measure();
				TelemetryManager.Client().SendUsingCellularData(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)m_deviceSpace / 1048576f, ElapsedTimeFromStart(m_apkUpdateStartTime));
				Log.Downloader.PrintInfo("User said yes to cell prompt.");
				m_cellularEnabledSession = true;
				DoUpdate(UpdateOp[m_savedAgentState]);
				m_askCellPopup = false;
			}, GameStrings.Get("GLOBAL_BUTTON_NO"), delegate
			{
				Log.Downloader.PrintInfo("User said no to cell prompt.");
				ShowAwaitingWifi();
				m_askCellPopup = false;
			});
			m_agentState = AgentState.AWAITING_WIFI;
		}

		private void ShowAwaitingWifi()
		{
			Log.Downloader.PrintInfo("Awaiting Wifi");
			m_deviceSpace = FreeSpace.Measure();
			TelemetryManager.Client().SendNoWifi(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)m_deviceSpace / 1048576f, ElapsedTimeFromStart(m_apkUpdateStartTime));
			if (!m_optionalDownload)
			{
				Log.Downloader.PrintInfo("Showing the Wifi awaiting");
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_ERROR_CHECK_SETTINGS"), GameStrings.Get("GLUE_LOADINGSCREEN_BUTTON_SETTINGS"), delegate
				{
					Log.Downloader.PrintInfo("Check your wireless settings.");
					UpdateUtils.ShowWirelessSettings();
					ShowAwaitingWifi();
				});
			}
			m_agentState = AgentState.AWAITING_WIFI;
			State = AssetDownloaderState.AWAITING_WIFI;
		}

		private void SetPatchUrl()
		{
			string text = null;
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				text = AndroidDeviceSettings.Get().GetPatchUrlFromArgument();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = Vars.Key("Mobile.PatchUrl").GetStr("");
				if (!string.IsNullOrEmpty(text))
				{
					if (text.IndexOf(".", StringComparison.Ordinal) == -1)
					{
						text += ".corp.blizzard.net";
					}
					text = "http://" + text + ":6999/hsb";
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				Log.Downloader.PrintInfo("Overriding PatchURL: " + text);
				AgentEmbeddedAPI.SetPatchUrlOverride("hsb", text);
				PatchOverrideUrl = new Uri(text).Host;
			}
		}

		private void OnReceiveTelemetry(string msgName, string pkgName, byte[] buffer, string component)
		{
			MessageOptions options = null;
			if (!string.IsNullOrEmpty(component))
			{
				options = new MessageOptions
				{
					Context = new Context
					{
						Program = new Context.ProgramInfo
						{
							Id = component
						}
					}
				};
			}
			if (TelemetryManager.Client() != null)
			{
				TelemetryManager.Client().EnqueueMessage(pkgName, msgName, buffer, options);
			}
		}

		public void UnknownSourcesListener(string onOff)
		{
			Log.Downloader.PrintInfo("Unknown sources: " + onOff);
			m_allowUnknownApps = onOff == "on";
			StartApkDownload();
			Log.Downloader.PrintInfo("Start to update APK");
		}

		public void InstallAPKListener(string status)
		{
			Log.Downloader.PrintInfo("install APK: " + status);
			if (!(status == "success"))
			{
				TelemetryManager.Client().SendApkInstallFailure(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, "exception: " + status);
				OpenAppStoreAlert();
				return;
			}
			m_deviceSpace = FreeSpace.Measure();
			TelemetryManager.Client().SendApkInstallSuccess(m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)m_deviceSpace / 1048576f, ElapsedTimeFromStart(m_apkUpdateStartTime));
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"), GameStrings.Get("GLOBAL_RELAUNCH_APPLICATION_AFTER_INSTALLAPK"), GameStrings.Get("GLOBAL_QUIT"), delegate
			{
				ShutdownApplication();
			});
		}

		private void ClearDataList()
		{
			if (m_bundleDataList != null)
			{
				m_bundleDataList.Clear();
			}
		}

		private void PrintDataList()
		{
			if (m_bundleDataList != null)
			{
				Processor.RunCoroutine(m_bundleDataList.Print());
				m_bundleDataList.Clear();
			}
		}
	}
}
