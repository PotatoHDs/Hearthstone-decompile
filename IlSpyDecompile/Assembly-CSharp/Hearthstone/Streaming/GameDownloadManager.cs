using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using Hearthstone.Core.Streaming;
using UnityEngine;

namespace Hearthstone.Streaming
{
	public class GameDownloadManager : IService, IGameDownloadManager, IHasFixedUpdate
	{
		[Serializable]
		private class SerializedState
		{
			public DownloadTags.Quality MaximumQualityTag;

			public string[] LastRequestedContentTags;

			public DownloadTags.Quality LastInprogressTag;
		}

		private const string UPDATE_PROCESS_JOB_NAME = "GameDownloadManager.UpdateProcess";

		private const float PROCESS_DELTA_TIME = 0.5f;

		protected const float MINIMUM_TIME_BETWEEN_RETRY_ATTEMPTS = 5f;

		private static readonly Dictionary<Locale, string> s_localeTags;

		protected IAssetDownloader m_assetDownloader;

		private SerializedState m_serializedState = new SerializedState();

		protected bool m_pausedExternally;

		private bool m_requestedInitialAssets;

		private bool m_requestedStreamingAssets;

		private bool m_bRegisterSceneLoadCallback;

		private bool m_localeChange;

		protected float m_timeOfLastAutoResumeAttempt;

		private ContentDownloadStatus m_curContentDownloadStatus = new ContentDownloadStatus();

		private string[] m_initialBaseTags;

		private LoginManager m_loginManager;

		private SceneMgr m_sceneMgr;

		private bool CanAskForCellularPermission => GetLoginManager()?.IsFullLoginFlowComplete ?? false;

		private string SerializedStatePath => Path.Combine(FileUtils.BasePersistentDataPath, "downloadmanager");

		private DownloadTags.Quality MaximumQualityTag
		{
			get
			{
				return GetSerializedValue(m_serializedState.MaximumQualityTag);
			}
			set
			{
				SetSerializedValue(ref m_serializedState.MaximumQualityTag, value);
			}
		}

		private string[] LastRequestedContentTags
		{
			get
			{
				return GetSerializedValue(m_serializedState.LastRequestedContentTags);
			}
			set
			{
				SetSerializedValue(ref m_serializedState.LastRequestedContentTags, value);
			}
		}

		private DownloadTags.Quality LastInprogressTag
		{
			get
			{
				return GetSerializedValue(m_serializedState.LastInprogressTag);
			}
			set
			{
				SetSerializedValue(ref m_serializedState.LastInprogressTag, value);
			}
		}

		protected virtual bool IsInGame => GetSceneManager()?.IsInGame() ?? false;

		public InterruptionReason InterruptionReason
		{
			get
			{
				if (!IsAnyDownloadRequestedAndIncomplete)
				{
					return InterruptionReason.None;
				}
				if (!GetDownloadEnabled())
				{
					return InterruptionReason.Disabled;
				}
				if (m_pausedExternally || IsInGame)
				{
					return InterruptionReason.Paused;
				}
				switch (m_assetDownloader.State)
				{
				case AssetDownloaderState.AGENT_IMPEDED:
					return InterruptionReason.AgentImpeded;
				case AssetDownloaderState.DISK_FULL:
					return InterruptionReason.DiskFull;
				case AssetDownloaderState.FETCHING_SIZE:
					return InterruptionReason.Fetching;
				case AssetDownloaderState.AWAITING_WIFI:
					return InterruptionReason.AwaitingWifi;
				case AssetDownloaderState.ERROR:
					return InterruptionReason.Error;
				case AssetDownloaderState.IDLE:
				case AssetDownloaderState.DOWNLOADING:
					return InterruptionReason.None;
				default:
					return InterruptionReason.Unknown;
				}
			}
		}

		public bool IsAnyDownloadRequestedAndIncomplete
		{
			get
			{
				if (LastRequestedContentTags == null || LastRequestedContentTags.Length == 0 || m_assetDownloader == null)
				{
					return false;
				}
				return !m_assetDownloader.GetDownloadStatus(LastRequestedContentTags).Complete;
			}
		}

		public bool IsInterrupted => InterruptionReason != InterruptionReason.None;

		public bool IsNewMobileVersionReleased
		{
			get
			{
				if (m_assetDownloader != null)
				{
					return m_assetDownloader.IsNewMobileVersionReleased;
				}
				return false;
			}
		}

		public bool IsReadyToPlay
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return true;
				}
				if (!m_assetDownloader.IsReady)
				{
					return false;
				}
				return IsCompletedInitialBaseDownload();
			}
		}

		public bool IsRunningNewerBinaryThanLive
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return false;
				}
				return m_assetDownloader.IsRunningNewerBinaryThanLive;
			}
		}

		public bool IsReadyToReadAssetManifest
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return true;
				}
				return m_assetDownloader.IsAssetManifestReady;
			}
		}

		public double BytesPerSecond
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return 0.0;
				}
				return m_assetDownloader.BytesPerSecond;
			}
		}

		public string PatchOverrideUrl
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return string.Empty;
				}
				return m_assetDownloader.PatchOverrideUrl;
			}
		}

		public string VersionOverrideUrl
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return string.Empty;
				}
				return m_assetDownloader.VersionOverrideUrl;
			}
		}

		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return new string[0];
				}
				return m_assetDownloader.DisabledAdventuresForStreaming;
			}
		}

		public int MaxDownloadSpeed
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return 0;
				}
				return m_assetDownloader.MaxDownloadSpeed;
			}
			set
			{
				if (m_assetDownloader != null)
				{
					m_assetDownloader.MaxDownloadSpeed = value;
				}
			}
		}

		public int InGameStreamingDefaultSpeed
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return 0;
				}
				return m_assetDownloader.InGameStreamingDefaultSpeed;
			}
			set
			{
				if (m_assetDownloader != null)
				{
					m_assetDownloader.InGameStreamingDefaultSpeed = value;
				}
			}
		}

		public int DownloadSpeedInGame
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return 0;
				}
				return m_assetDownloader.DownloadSpeedInGame;
			}
			set
			{
				if (m_assetDownloader != null)
				{
					m_assetDownloader.DownloadSpeedInGame = value;
				}
			}
		}

		public bool ShouldDownloadLocalizedAssets
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return false;
				}
				if (PlatformSettings.IsMobileRuntimeOS)
				{
					if (m_assetDownloader.IsRunningNewerBinaryThanLive)
					{
						return false;
					}
					m_assetDownloader.PauseAllDownloads();
				}
				Log.Downloader.PrintInfo("Begin to download new locale data");
				DeleteSerializedState();
				m_assetDownloader.PrepareRestart();
				return true;
			}
		}

		public string AgentLogPath
		{
			get
			{
				if (m_assetDownloader == null)
				{
					return null;
				}
				return m_assetDownloader.AgentLogPath;
			}
		}

		static GameDownloadManager()
		{
			s_localeTags = new Dictionary<Locale, string>();
			foreach (Locale value in Enum.GetValues(typeof(Locale)))
			{
				s_localeTags[value] = Enum.GetName(typeof(Locale), value);
			}
		}

		protected IEnumerator<IAsyncJobResult> Job_UpdateProcess()
		{
			bool done = false;
			bool firstCall = true;
			while (!done)
			{
				if (m_assetDownloader != null)
				{
					done = StartContentDownloadWhenReady();
					m_assetDownloader.Update(firstCall);
					firstCall = false;
				}
				float seconds = (DownloadPermissionManager.DownloadEnabled ? 0.5f : 1.5f);
				yield return new WaitForDuration(seconds);
			}
		}

		private bool StartContentDownloadWhenReady()
		{
			if (!m_assetDownloader.IsReady)
			{
				return false;
			}
			if ((IsReadyToPlay && (m_assetDownloader.DownloadAllFinished || m_assetDownloader.IsRunningNewerBinaryThanLive)) || m_requestedStreamingAssets)
			{
				SceneMgr sceneManager = GetSceneManager();
				if (!m_bRegisterSceneLoadCallback && sceneManager != null)
				{
					sceneManager.RegisterScenePreLoadEvent(OnSceneLoad);
					m_bRegisterSceneLoadCallback = true;
				}
				if (Vars.Key("Mobile.StopDownloadAfter").HasValue && m_assetDownloader.DownloadAllFinished)
				{
					DownloadPermissionManager.DownloadEnabled = false;
					m_assetDownloader.DownloadAllFinished = false;
				}
				if (m_assetDownloader.DownloadAllFinished || IsInterrupted)
				{
					HearthstoneApplication.Get()?.ControlANRMonitor(on: true);
				}
				return m_assetDownloader.DownloadAllFinished;
			}
			if (IsReadyToPlay && !IsAnyDownloadRequestedAndIncomplete)
			{
				if (m_requestedInitialAssets)
				{
					m_assetDownloader.DoPostTasksAfterInitialDownload();
				}
				LastInprogressTag = GetNextTag(DownloadTags.Quality.Initial);
				if (!Vars.Key("Mobile.StopDownloadAfter").HasValue)
				{
					MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
				}
				else
				{
					string str = Vars.Key("Mobile.StopDownloadAfter").GetStr(string.Empty);
					DownloadTags.Quality qualityTag = DownloadTags.GetQualityTag(str);
					if (qualityTag != 0)
					{
						Log.Downloader.PrintInfo("Optional data will be stopped after downloading '{0}'", str);
						MaximumQualityTag = qualityTag;
					}
					else
					{
						Log.Downloader.PrintError("Unknown quality tag name has been used from deeplink: {0}", str);
						MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
					}
				}
				m_localeChange = false;
				StartInitialBaseDownload();
				m_requestedStreamingAssets = true;
			}
			else if (!m_requestedInitialAssets)
			{
				if (m_assetDownloader.IsVersionChanged)
				{
					DeleteSerializedState();
					DownloadPermissionManager.DownloadEnabled = true;
				}
				MaximumQualityTag = DownloadTags.Quality.Initial;
				StartInitialBaseDownload();
				m_requestedInitialAssets = true;
			}
			return false;
		}

		private string[] CombineWithAllAppropriateTags(params string[] tags)
		{
			List<string> list = new List<string>(AppropriateQualityTags());
			list.AddRange(tags);
			return list.ToArray();
		}

		private string[] AppropriateQualityTags()
		{
			return QualityTagsBetween(LastInprogressTag, MaximumQualityTag, PlatformSettings.ShouldFallbackToLowRes).Select(DownloadTags.GetTagString).ToArray();
		}

		private string[] AppropriateLocaleTags()
		{
			List<string> list = new List<string> { DownloadTags.GetTagString(DownloadTags.Locale.EnUS) };
			Locale locale = Localization.GetLocale();
			if (locale != Locale.UNKNOWN && locale != 0)
			{
				list.Add(s_localeTags[locale]);
			}
			return list.ToArray();
		}

		private void OnApplicationPause(bool paused)
		{
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				if (paused)
				{
					PauseAllDownloadsInternal();
				}
				else
				{
					ResumeAllDownloadsInternal();
				}
			}
		}

		private void SetSerializedValue<T>(ref T dest, T value)
		{
			bool num = !object.Equals(dest, value);
			dest = value;
			if (num)
			{
				SerializeState();
			}
		}

		private T GetSerializedValue<T>(T value)
		{
			if (m_serializedState == null)
			{
				TryDeserializeState();
			}
			return value;
		}

		private void SerializeState()
		{
			try
			{
				File.WriteAllText(SerializedStatePath, JsonUtility.ToJson(m_serializedState, !HearthstoneApplication.IsPublic()));
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Failed to serialize state: " + ex);
			}
		}

		private void TryDeserializeState()
		{
			string serializedStatePath = SerializedStatePath;
			if (File.Exists(serializedStatePath))
			{
				try
				{
					m_serializedState = JsonUtility.FromJson<SerializedState>(File.ReadAllText(serializedStatePath));
				}
				catch (Exception ex)
				{
					Log.Downloader.PrintError("Failed to deserialize state: " + ex);
				}
			}
		}

		private void DeleteSerializedState()
		{
			string serializedStatePath = SerializedStatePath;
			if (File.Exists(serializedStatePath))
			{
				try
				{
					File.Delete(serializedStatePath);
				}
				catch (Exception ex)
				{
					Error.AddDevFatal("Failed to delete the downloader state file({0}): {1}", serializedStatePath, ex.Message);
				}
			}
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			yield return HearthstoneApplication.Get().DataTransferDependency;
			if (!PlatformSettings.IsMobileRuntimeOS && !Application.isEditor)
			{
				UpdateUtils.CleanupOldAssetBundles(AssetBundleInfo.GetAssetBundlesDir());
			}
			if (!serviceLocator.Get<DemoMgr>().IsDemo() && (PlatformSettings.IsMobileRuntimeOS || (Application.isEditor && PlatformSettings.IsEmulating)) && Initialize())
			{
				StartUpdateProcess(localeChange: false);
				HearthstoneApplication.Get().Resetting += CreateUpdateProcessJob;
			}
		}

		public Type[] GetDependencies()
		{
			return new Type[2]
			{
				typeof(DemoMgr),
				typeof(VersionConfigurationService)
			};
		}

		public void Shutdown()
		{
			if (m_requestedStreamingAssets && PlatformSettings.IsMobileRuntimeOS)
			{
				GetSceneManager()?.UnregisterScenePreLoadEvent(OnSceneLoad);
			}
			if (m_assetDownloader != null)
			{
				m_assetDownloader.Shutdown();
			}
		}

		public void FixedUpdate()
		{
			ResumeStalledDownloadsIfAble();
		}

		public float GetSecondsBetweenUpdates()
		{
			return 1f;
		}

		public bool Initialize()
		{
			if (!Application.isEditor || EditorAssetDownloader.Mode != 0)
			{
				TryDeserializeState();
				IAssetDownloader assetDownloader2;
				if (!Application.isEditor)
				{
					IAssetDownloader assetDownloader = new RuntimeAssetDownloader();
					assetDownloader2 = assetDownloader;
				}
				else
				{
					IAssetDownloader assetDownloader = new EditorAssetDownloader();
					assetDownloader2 = assetDownloader;
				}
				m_assetDownloader = assetDownloader2;
				return m_assetDownloader.Initialize();
			}
			return false;
		}

		protected virtual bool GetDownloadEnabled()
		{
			return DownloadPermissionManager.DownloadEnabled;
		}

		public void StartContentDownload(DownloadTags.Content contentTag)
		{
			m_pausedExternally = false;
			StartContentDownload(new string[1] { DownloadTags.GetTagString(contentTag) });
		}

		public bool IsCompletedInitialBaseDownload()
		{
			if (m_assetDownloader == null || m_assetDownloader.DownloadAllFinished || m_assetDownloader.IsRunningNewerBinaryThanLive)
			{
				return true;
			}
			if (m_initialBaseTags == null || m_initialBaseTags.Length == 0 || !IsReadyAssetsInTags(new string[2]
			{
				DownloadTags.GetTagString(DownloadTags.Quality.Manifest),
				DownloadTags.GetTagString(DownloadTags.Content.Base)
			}))
			{
				return false;
			}
			return GetTagDownloadStatus(m_initialBaseTags).Complete;
		}

		public void StopOptionalDownloads()
		{
			bool flag = m_assetDownloader?.IsReady ?? false;
			if (flag && IsCompletedInitialBaseDownload())
			{
				Log.Downloader.PrintDebug("Requesting optional downloads to stop");
				m_assetDownloader?.StopDownloads();
			}
			else
			{
				Log.Downloader.PrintDebug("Could not stop optional downloads: {0}", (!flag) ? "Asset downloader not ready" : "Initial downloads not complete");
			}
		}

		private void StartInitialBaseDownload()
		{
			HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.StartInitialBaseDownload");
			List<string> list = new List<string>();
			if (DisabledAdventuresForStreaming.Length != 0)
			{
				if (AssetManifest.Get() != null)
				{
					string[] tagsInTagGroup = AssetManifest.Get().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content));
					list.AddRange(tagsInTagGroup);
				}
				else
				{
					list.AddRange(DisabledAdventuresForStreaming);
				}
			}
			else
			{
				list.Add(DownloadTags.GetTagString(DownloadTags.Content.Base));
			}
			if (m_initialBaseTags == null || m_initialBaseTags.Length == 0)
			{
				List<string> list2 = new List<string>();
				list2.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
				list2.Add(DownloadTags.GetTagString(DownloadTags.Quality.Initial));
				list2.AddRange(list);
				m_initialBaseTags = list2.ToArray();
			}
			StartContentDownload(list.ToArray());
		}

		private void StartContentDownload(string[] contentTag)
		{
			if (m_assetDownloader == null)
			{
				Log.Downloader.PrintError("StartContentDownload: AssetDownloader is not ready yet!");
				return;
			}
			string[] tags = (LastRequestedContentTags = CombineWithAllAppropriateTags(contentTag));
			HearthstoneApplication.Get()?.ControlANRMonitor(on: false);
			m_assetDownloader.StartDownload(tags, MaximumQualityTag == DownloadTags.Quality.Initial, m_localeChange);
		}

		protected void ResumeStalledDownloadsIfAble()
		{
			if (ShouldRestartStalledDownloads())
			{
				ResumeStalledDownloads();
			}
		}

		private void ResumeStalledDownloads()
		{
			m_timeOfLastAutoResumeAttempt = GetRealtimeSinceStartup();
			ResumeAllDownloads();
		}

		protected virtual float GetRealtimeSinceStartup()
		{
			return Time.realtimeSinceStartup;
		}

		private bool ShouldRestartStalledDownloads()
		{
			if (m_assetDownloader != null && m_assetDownloader.IsReady && !IsAutoResumeThrottled())
			{
				return AreOptionalDownloadsStalled();
			}
			return false;
		}

		private bool AreOptionalDownloadsStalled()
		{
			if (IsCompletedInitialBaseDownload() && IsAnyDownloadRequestedAndIncomplete && m_assetDownloader.State != AssetDownloaderState.DOWNLOADING)
			{
				InterruptionReason interruptionReason = InterruptionReason;
				if (IsInterrupted && interruptionReason != InterruptionReason.Error)
				{
					return interruptionReason == InterruptionReason.Unknown;
				}
				return true;
			}
			return false;
		}

		private bool IsAutoResumeThrottled()
		{
			return GetRealtimeSinceStartup() - m_timeOfLastAutoResumeAttempt < 5f;
		}

		public bool IsAssetDownloaded(string assetGuid)
		{
			if (m_assetDownloader == null)
			{
				if (string.IsNullOrEmpty(assetGuid))
				{
					return false;
				}
				return true;
			}
			if (!string.IsNullOrEmpty(assetGuid) && AssetManifest.Get().TryGetDirectBundleFromGuid(assetGuid, out var assetBundleName))
			{
				return m_assetDownloader.IsBundleDownloaded(assetBundleName);
			}
			return false;
		}

		public bool IsBundleDownloaded(string bundleName)
		{
			if (m_assetDownloader == null)
			{
				return true;
			}
			return m_assetDownloader.IsBundleDownloaded(bundleName);
		}

		public bool IsReadyAssetsInTags(string[] tags)
		{
			if (m_assetDownloader == null)
			{
				return true;
			}
			return m_assetDownloader.GetDownloadStatus(tags).Complete;
		}

		public TagDownloadStatus GetTagDownloadStatus(string[] tags)
		{
			if (m_assetDownloader == null)
			{
				return new TagDownloadStatus
				{
					Complete = true
				};
			}
			return m_assetDownloader.GetDownloadStatus(tags);
		}

		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			if (m_assetDownloader == null)
			{
				return null;
			}
			return m_assetDownloader.GetCurrentDownloadStatus();
		}

		public ContentDownloadStatus GetContentDownloadStatus(DownloadTags.Content contentTag)
		{
			return GetContentDownloadStatus(DownloadTags.GetTagString(contentTag));
		}

		protected IEnumerable<DownloadTags.Quality> QualityTagsAfter(DownloadTags.Quality startTag, bool fallbackToLowRes)
		{
			return QualityTagsBetween(startTag, DownloadTags.GetLastEnum<DownloadTags.Quality>(), fallbackToLowRes);
		}

		protected IEnumerable<DownloadTags.Quality> QualityTagsBetween(DownloadTags.Quality startTag, DownloadTags.Quality endTag, bool fallbackToLowRes)
		{
			bool started = false;
			string prevTag = string.Empty;
			foreach (DownloadTags.Quality qualityTag in Enum.GetValues(typeof(DownloadTags.Quality)))
			{
				if (!started && qualityTag == startTag)
				{
					started = true;
				}
				if (!started || (qualityTag == DownloadTags.Quality.PortHigh && fallbackToLowRes))
				{
					continue;
				}
				if (AssetManifest.Get() != null)
				{
					string overrideTag = AssetManifest.Get().ConvertToOverrideTag(DownloadTags.GetTagString(qualityTag), DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
					if (overrideTag != prevTag)
					{
						yield return qualityTag;
					}
					prevTag = overrideTag;
				}
				else if (qualityTag != 0)
				{
					yield return qualityTag;
				}
				if (qualityTag == endTag)
				{
					yield break;
				}
			}
		}

		private DownloadTags.Quality GetNextTag(DownloadTags.Quality startTag)
		{
			foreach (DownloadTags.Quality item in QualityTagsAfter(startTag, PlatformSettings.ShouldFallbackToLowRes))
			{
				if (item != startTag)
				{
					return item;
				}
			}
			return startTag;
		}

		private ContentDownloadStatus GetContentDownloadStatus(string contentTag)
		{
			if (m_assetDownloader == null)
			{
				return m_curContentDownloadStatus;
			}
			string[] source = CombineWithAllAppropriateTags(contentTag);
			TagDownloadStatus downloadStatus = m_assetDownloader.GetDownloadStatus(source.ToArray());
			m_curContentDownloadStatus.BytesTotal = downloadStatus.BytesTotal;
			m_curContentDownloadStatus.BytesDownloaded = downloadStatus.BytesDownloaded;
			m_curContentDownloadStatus.Progress = downloadStatus.Progress;
			m_curContentDownloadStatus.ContentTag = contentTag;
			foreach (DownloadTags.Quality item in QualityTagsAfter(m_curContentDownloadStatus.InProgressQualityTag, PlatformSettings.ShouldFallbackToLowRes))
			{
				m_curContentDownloadStatus.InProgressQualityTag = item;
				if (!m_assetDownloader.GetDownloadStatus(new string[2]
				{
					DownloadTags.GetTagString(m_curContentDownloadStatus.InProgressQualityTag),
					contentTag
				}).Complete)
				{
					break;
				}
			}
			return m_curContentDownloadStatus;
		}

		public void StartUpdateProcess(bool localeChange)
		{
			Log.Downloader.PrintInfo("StartUpdate locale={0}", Localization.GetLocale().ToString());
			m_requestedInitialAssets = false;
			m_requestedStreamingAssets = false;
			m_bRegisterSceneLoadCallback = false;
			m_localeChange = localeChange;
			LastInprogressTag = DownloadTags.Quality.Unknown;
			if (m_initialBaseTags != null)
			{
				m_initialBaseTags = null;
			}
			StartUpdateJobIfNotRunning();
		}

		protected virtual void StartUpdateJobIfNotRunning()
		{
			Processor.QueueJobIfNotExist("GameDownloadManager.UpdateProcess", Job_UpdateProcess());
		}

		public void StartUpdateProcessForOptional()
		{
			MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
			StartInitialBaseDownload();
			StartUpdateJobIfNotRunning();
		}

		public void PauseAllDownloads()
		{
			m_pausedExternally = true;
			PauseAllDownloadsInternal();
		}

		private void PauseAllDownloadsInternal()
		{
			if (m_assetDownloader != null)
			{
				m_assetDownloader.PauseAllDownloads();
			}
		}

		private void CreateUpdateProcessJob()
		{
			if (IsAnyDownloadRequestedAndIncomplete)
			{
				Processor.QueueJobIfNotExist("GameDownloadManager.UpdateProcess", Job_UpdateProcess());
			}
		}

		public void ResumeAllDownloads()
		{
			m_pausedExternally = false;
			ResumeAllDownloadsInternal();
		}

		private void ResumeAllDownloadsInternal()
		{
			if (m_assetDownloader != null && m_assetDownloader.GetCurrentDownloadStatus() != null)
			{
				HearthstoneApplication.Get()?.ControlANRMonitor(on: false);
				m_assetDownloader.StartDownload(LastRequestedContentTags, MaximumQualityTag == DownloadTags.Quality.Initial, m_localeChange);
			}
		}

		public void DeleteDownloadedData()
		{
			if (m_assetDownloader != null)
			{
				m_assetDownloader.PauseAllDownloads();
				m_assetDownloader.DeleteDownloadedData();
			}
		}

		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
			if (m_assetDownloader != null)
			{
				m_assetDownloader.OnSceneLoad(prevMode, nextMode, userData);
			}
		}

		private LoginManager GetLoginManager()
		{
			if (m_loginManager == null)
			{
				m_loginManager = HearthstoneServices.Get<LoginManager>();
			}
			return m_loginManager;
		}

		private SceneMgr GetSceneManager()
		{
			if (m_sceneMgr == null)
			{
				m_sceneMgr = HearthstoneServices.Get<SceneMgr>();
			}
			return m_sceneMgr;
		}

		public void UnknownSourcesListener(string onOff)
		{
			m_assetDownloader.UnknownSourcesListener(onOff);
		}

		public void InstallAPKListener(string status)
		{
			m_assetDownloader.InstallAPKListener(status);
		}
	}
}
