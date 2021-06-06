using System;
using System.Collections;
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
	// Token: 0x0200106F RID: 4207
	public class GameDownloadManager : IService, IGameDownloadManager, IHasFixedUpdate
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600B583 RID: 46467 RVA: 0x0037C730 File Offset: 0x0037A930
		private bool CanAskForCellularPermission
		{
			get
			{
				LoginManager loginManager = this.GetLoginManager();
				return loginManager != null && loginManager.IsFullLoginFlowComplete;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600B584 RID: 46468 RVA: 0x0037C74F File Offset: 0x0037A94F
		private string SerializedStatePath
		{
			get
			{
				return Path.Combine(FileUtils.BasePersistentDataPath, "downloadmanager");
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600B586 RID: 46470 RVA: 0x0037C774 File Offset: 0x0037A974
		// (set) Token: 0x0600B585 RID: 46469 RVA: 0x0037C760 File Offset: 0x0037A960
		private DownloadTags.Quality MaximumQualityTag
		{
			get
			{
				return this.GetSerializedValue<DownloadTags.Quality>(this.m_serializedState.MaximumQualityTag);
			}
			set
			{
				this.SetSerializedValue<DownloadTags.Quality>(ref this.m_serializedState.MaximumQualityTag, value);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600B588 RID: 46472 RVA: 0x0037C79B File Offset: 0x0037A99B
		// (set) Token: 0x0600B587 RID: 46471 RVA: 0x0037C787 File Offset: 0x0037A987
		private string[] LastRequestedContentTags
		{
			get
			{
				return this.GetSerializedValue<string[]>(this.m_serializedState.LastRequestedContentTags);
			}
			set
			{
				this.SetSerializedValue<string[]>(ref this.m_serializedState.LastRequestedContentTags, value);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600B58A RID: 46474 RVA: 0x0037C7C2 File Offset: 0x0037A9C2
		// (set) Token: 0x0600B589 RID: 46473 RVA: 0x0037C7AE File Offset: 0x0037A9AE
		private DownloadTags.Quality LastInprogressTag
		{
			get
			{
				return this.GetSerializedValue<DownloadTags.Quality>(this.m_serializedState.LastInprogressTag);
			}
			set
			{
				this.SetSerializedValue<DownloadTags.Quality>(ref this.m_serializedState.LastInprogressTag, value);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600B58B RID: 46475 RVA: 0x0037C7D8 File Offset: 0x0037A9D8
		protected virtual bool IsInGame
		{
			get
			{
				SceneMgr sceneManager = this.GetSceneManager();
				return sceneManager != null && sceneManager.IsInGame();
			}
		}

		// Token: 0x0600B58C RID: 46476 RVA: 0x0037C7F8 File Offset: 0x0037A9F8
		static GameDownloadManager()
		{
			foreach (object obj in Enum.GetValues(typeof(Locale)))
			{
				Locale locale = (Locale)obj;
				GameDownloadManager.s_localeTags[locale] = Enum.GetName(typeof(Locale), locale);
			}
		}

		// Token: 0x0600B58D RID: 46477 RVA: 0x0037C880 File Offset: 0x0037AA80
		protected IEnumerator<IAsyncJobResult> Job_UpdateProcess()
		{
			bool done = false;
			bool firstCall = true;
			while (!done)
			{
				if (this.m_assetDownloader != null)
				{
					done = this.StartContentDownloadWhenReady();
					this.m_assetDownloader.Update(firstCall);
					firstCall = false;
				}
				float seconds = DownloadPermissionManager.DownloadEnabled ? 0.5f : 1.5f;
				yield return new WaitForDuration(seconds);
			}
			yield break;
		}

		// Token: 0x0600B58E RID: 46478 RVA: 0x0037C890 File Offset: 0x0037AA90
		private bool StartContentDownloadWhenReady()
		{
			if (!this.m_assetDownloader.IsReady)
			{
				return false;
			}
			if ((this.IsReadyToPlay && (this.m_assetDownloader.DownloadAllFinished || this.m_assetDownloader.IsRunningNewerBinaryThanLive)) || this.m_requestedStreamingAssets)
			{
				SceneMgr sceneManager = this.GetSceneManager();
				if (!this.m_bRegisterSceneLoadCallback && sceneManager != null)
				{
					sceneManager.RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnSceneLoad));
					this.m_bRegisterSceneLoadCallback = true;
				}
				if (Vars.Key("Mobile.StopDownloadAfter").HasValue && this.m_assetDownloader.DownloadAllFinished)
				{
					DownloadPermissionManager.DownloadEnabled = false;
					this.m_assetDownloader.DownloadAllFinished = false;
				}
				if (this.m_assetDownloader.DownloadAllFinished || this.IsInterrupted)
				{
					HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
					if (hearthstoneApplication != null)
					{
						hearthstoneApplication.ControlANRMonitor(true);
					}
				}
				return this.m_assetDownloader.DownloadAllFinished;
			}
			if (this.IsReadyToPlay && !this.IsAnyDownloadRequestedAndIncomplete)
			{
				if (this.m_requestedInitialAssets)
				{
					this.m_assetDownloader.DoPostTasksAfterInitialDownload();
				}
				this.LastInprogressTag = this.GetNextTag(DownloadTags.Quality.Initial);
				if (!Vars.Key("Mobile.StopDownloadAfter").HasValue)
				{
					this.MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
				}
				else
				{
					string str = Vars.Key("Mobile.StopDownloadAfter").GetStr(string.Empty);
					DownloadTags.Quality qualityTag = DownloadTags.GetQualityTag(str);
					if (qualityTag != DownloadTags.Quality.Unknown)
					{
						Log.Downloader.PrintInfo("Optional data will be stopped after downloading '{0}'", new object[]
						{
							str
						});
						this.MaximumQualityTag = qualityTag;
					}
					else
					{
						Log.Downloader.PrintError("Unknown quality tag name has been used from deeplink: {0}", new object[]
						{
							str
						});
						this.MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
					}
				}
				this.m_localeChange = false;
				this.StartInitialBaseDownload();
				this.m_requestedStreamingAssets = true;
			}
			else if (!this.m_requestedInitialAssets)
			{
				if (this.m_assetDownloader.IsVersionChanged)
				{
					this.DeleteSerializedState();
					DownloadPermissionManager.DownloadEnabled = true;
				}
				this.MaximumQualityTag = DownloadTags.Quality.Initial;
				this.StartInitialBaseDownload();
				this.m_requestedInitialAssets = true;
			}
			return false;
		}

		// Token: 0x0600B58F RID: 46479 RVA: 0x0037CA6B File Offset: 0x0037AC6B
		private string[] CombineWithAllAppropriateTags(params string[] tags)
		{
			List<string> list = new List<string>(this.AppropriateQualityTags());
			list.AddRange(tags);
			return list.ToArray();
		}

		// Token: 0x0600B590 RID: 46480 RVA: 0x0037CA84 File Offset: 0x0037AC84
		private string[] AppropriateQualityTags()
		{
			return this.QualityTagsBetween(this.LastInprogressTag, this.MaximumQualityTag, PlatformSettings.ShouldFallbackToLowRes).Select(new Func<DownloadTags.Quality, string>(DownloadTags.GetTagString)).ToArray<string>();
		}

		// Token: 0x0600B591 RID: 46481 RVA: 0x0037CAB4 File Offset: 0x0037ACB4
		private string[] AppropriateLocaleTags()
		{
			List<string> list = new List<string>
			{
				DownloadTags.GetTagString(DownloadTags.Locale.EnUS)
			};
			Locale locale = Localization.GetLocale();
			if (locale != Locale.UNKNOWN && locale != Locale.enUS)
			{
				list.Add(GameDownloadManager.s_localeTags[locale]);
			}
			return list.ToArray();
		}

		// Token: 0x0600B592 RID: 46482 RVA: 0x0037CAF7 File Offset: 0x0037ACF7
		private void OnApplicationPause(bool paused)
		{
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				if (paused)
				{
					this.PauseAllDownloadsInternal();
					return;
				}
				this.ResumeAllDownloadsInternal();
			}
		}

		// Token: 0x0600B593 RID: 46483 RVA: 0x0037CB11 File Offset: 0x0037AD11
		private void SetSerializedValue<T>(ref T dest, T value)
		{
			bool flag = !object.Equals(dest, value);
			dest = value;
			if (flag)
			{
				this.SerializeState();
			}
		}

		// Token: 0x0600B594 RID: 46484 RVA: 0x0037CB3B File Offset: 0x0037AD3B
		private T GetSerializedValue<T>(T value)
		{
			if (this.m_serializedState == null)
			{
				this.TryDeserializeState();
			}
			return value;
		}

		// Token: 0x0600B595 RID: 46485 RVA: 0x0037CB4C File Offset: 0x0037AD4C
		private void SerializeState()
		{
			try
			{
				File.WriteAllText(this.SerializedStatePath, JsonUtility.ToJson(this.m_serializedState, !HearthstoneApplication.IsPublic()));
			}
			catch (Exception arg)
			{
				Log.Downloader.PrintError("Failed to serialize state: " + arg, Array.Empty<object>());
			}
		}

		// Token: 0x0600B596 RID: 46486 RVA: 0x0037CBA8 File Offset: 0x0037ADA8
		private void TryDeserializeState()
		{
			string serializedStatePath = this.SerializedStatePath;
			if (!File.Exists(serializedStatePath))
			{
				return;
			}
			try
			{
				this.m_serializedState = JsonUtility.FromJson<GameDownloadManager.SerializedState>(File.ReadAllText(serializedStatePath));
			}
			catch (Exception arg)
			{
				Log.Downloader.PrintError("Failed to deserialize state: " + arg, Array.Empty<object>());
			}
		}

		// Token: 0x0600B597 RID: 46487 RVA: 0x0037CC08 File Offset: 0x0037AE08
		private void DeleteSerializedState()
		{
			string serializedStatePath = this.SerializedStatePath;
			if (File.Exists(serializedStatePath))
			{
				try
				{
					File.Delete(serializedStatePath);
				}
				catch (Exception ex)
				{
					Error.AddDevFatal("Failed to delete the downloader state file({0}): {1}", new object[]
					{
						serializedStatePath,
						ex.Message
					});
				}
			}
		}

		// Token: 0x0600B598 RID: 46488 RVA: 0x0037CC5C File Offset: 0x0037AE5C
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			yield return HearthstoneApplication.Get().DataTransferDependency;
			if (!PlatformSettings.IsMobileRuntimeOS && !Application.isEditor)
			{
				UpdateUtils.CleanupOldAssetBundles(AssetBundleInfo.GetAssetBundlesDir());
			}
			if (!serviceLocator.Get<DemoMgr>().IsDemo() && (PlatformSettings.IsMobileRuntimeOS || (Application.isEditor && PlatformSettings.IsEmulating)) && this.Initialize())
			{
				this.StartUpdateProcess(false);
				HearthstoneApplication.Get().Resetting += this.CreateUpdateProcessJob;
			}
			yield break;
		}

		// Token: 0x0600B599 RID: 46489 RVA: 0x0037CC72 File Offset: 0x0037AE72
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(DemoMgr),
				typeof(VersionConfigurationService)
			};
		}

		// Token: 0x0600B59A RID: 46490 RVA: 0x0037CC94 File Offset: 0x0037AE94
		public void Shutdown()
		{
			if (this.m_requestedStreamingAssets && PlatformSettings.IsMobileRuntimeOS)
			{
				SceneMgr sceneManager = this.GetSceneManager();
				if (sceneManager != null)
				{
					sceneManager.UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnSceneLoad));
				}
			}
			if (this.m_assetDownloader != null)
			{
				this.m_assetDownloader.Shutdown();
			}
		}

		// Token: 0x0600B59B RID: 46491 RVA: 0x0037CCE1 File Offset: 0x0037AEE1
		public void FixedUpdate()
		{
			this.ResumeStalledDownloadsIfAble();
		}

		// Token: 0x0600B59C RID: 46492 RVA: 0x000C3B6A File Offset: 0x000C1D6A
		public float GetSecondsBetweenUpdates()
		{
			return 1f;
		}

		// Token: 0x0600B59D RID: 46493 RVA: 0x0037CCEC File Offset: 0x0037AEEC
		public bool Initialize()
		{
			if (!Application.isEditor || EditorAssetDownloader.Mode != EditorAssetDownloader.DownloadMode.None)
			{
				this.TryDeserializeState();
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
				this.m_assetDownloader = assetDownloader2;
				return this.m_assetDownloader.Initialize();
			}
			return false;
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600B59E RID: 46494 RVA: 0x0037CD38 File Offset: 0x0037AF38
		public InterruptionReason InterruptionReason
		{
			get
			{
				if (!this.IsAnyDownloadRequestedAndIncomplete)
				{
					return InterruptionReason.None;
				}
				if (!this.GetDownloadEnabled())
				{
					return InterruptionReason.Disabled;
				}
				if (this.m_pausedExternally || this.IsInGame)
				{
					return InterruptionReason.Paused;
				}
				switch (this.m_assetDownloader.State)
				{
				case AssetDownloaderState.ERROR:
					return InterruptionReason.Error;
				case AssetDownloaderState.IDLE:
				case AssetDownloaderState.DOWNLOADING:
					return InterruptionReason.None;
				case AssetDownloaderState.AGENT_IMPEDED:
					return InterruptionReason.AgentImpeded;
				case AssetDownloaderState.DISK_FULL:
					return InterruptionReason.DiskFull;
				case AssetDownloaderState.FETCHING_SIZE:
					return InterruptionReason.Fetching;
				case AssetDownloaderState.AWAITING_WIFI:
					return InterruptionReason.AwaitingWifi;
				}
				return InterruptionReason.Unknown;
			}
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x0037CDB0 File Offset: 0x0037AFB0
		protected virtual bool GetDownloadEnabled()
		{
			return DownloadPermissionManager.DownloadEnabled;
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600B5A0 RID: 46496 RVA: 0x0037CDB7 File Offset: 0x0037AFB7
		public bool IsAnyDownloadRequestedAndIncomplete
		{
			get
			{
				return this.LastRequestedContentTags != null && this.LastRequestedContentTags.Length != 0 && this.m_assetDownloader != null && !this.m_assetDownloader.GetDownloadStatus(this.LastRequestedContentTags).Complete;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600B5A1 RID: 46497 RVA: 0x0037CDED File Offset: 0x0037AFED
		public bool IsInterrupted
		{
			get
			{
				return this.InterruptionReason > InterruptionReason.None;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600B5A2 RID: 46498 RVA: 0x0037CDF8 File Offset: 0x0037AFF8
		public bool IsNewMobileVersionReleased
		{
			get
			{
				return this.m_assetDownloader != null && this.m_assetDownloader.IsNewMobileVersionReleased;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600B5A3 RID: 46499 RVA: 0x0037CE0F File Offset: 0x0037B00F
		public bool IsReadyToPlay
		{
			get
			{
				return this.m_assetDownloader == null || (this.m_assetDownloader.IsReady && this.IsCompletedInitialBaseDownload());
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600B5A4 RID: 46500 RVA: 0x0037CE30 File Offset: 0x0037B030
		public bool IsRunningNewerBinaryThanLive
		{
			get
			{
				return this.m_assetDownloader != null && this.m_assetDownloader.IsRunningNewerBinaryThanLive;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600B5A5 RID: 46501 RVA: 0x0037CE47 File Offset: 0x0037B047
		public bool IsReadyToReadAssetManifest
		{
			get
			{
				return this.m_assetDownloader == null || this.m_assetDownloader.IsAssetManifestReady;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600B5A6 RID: 46502 RVA: 0x0037CE5E File Offset: 0x0037B05E
		public double BytesPerSecond
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return 0.0;
				}
				return this.m_assetDownloader.BytesPerSecond;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600B5A7 RID: 46503 RVA: 0x0037CE7D File Offset: 0x0037B07D
		public string PatchOverrideUrl
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return string.Empty;
				}
				return this.m_assetDownloader.PatchOverrideUrl;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600B5A8 RID: 46504 RVA: 0x0037CE98 File Offset: 0x0037B098
		public string VersionOverrideUrl
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return string.Empty;
				}
				return this.m_assetDownloader.VersionOverrideUrl;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600B5A9 RID: 46505 RVA: 0x0037CEB3 File Offset: 0x0037B0B3
		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return new string[0];
				}
				return this.m_assetDownloader.DisabledAdventuresForStreaming;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600B5AA RID: 46506 RVA: 0x0037CECF File Offset: 0x0037B0CF
		// (set) Token: 0x0600B5AB RID: 46507 RVA: 0x0037CEE6 File Offset: 0x0037B0E6
		public int MaxDownloadSpeed
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return 0;
				}
				return this.m_assetDownloader.MaxDownloadSpeed;
			}
			set
			{
				if (this.m_assetDownloader != null)
				{
					this.m_assetDownloader.MaxDownloadSpeed = value;
				}
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600B5AC RID: 46508 RVA: 0x0037CEFC File Offset: 0x0037B0FC
		// (set) Token: 0x0600B5AD RID: 46509 RVA: 0x0037CF13 File Offset: 0x0037B113
		public int InGameStreamingDefaultSpeed
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return 0;
				}
				return this.m_assetDownloader.InGameStreamingDefaultSpeed;
			}
			set
			{
				if (this.m_assetDownloader != null)
				{
					this.m_assetDownloader.InGameStreamingDefaultSpeed = value;
				}
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600B5AE RID: 46510 RVA: 0x0037CF29 File Offset: 0x0037B129
		// (set) Token: 0x0600B5AF RID: 46511 RVA: 0x0037CF40 File Offset: 0x0037B140
		public int DownloadSpeedInGame
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return 0;
				}
				return this.m_assetDownloader.DownloadSpeedInGame;
			}
			set
			{
				if (this.m_assetDownloader != null)
				{
					this.m_assetDownloader.DownloadSpeedInGame = value;
				}
			}
		}

		// Token: 0x0600B5B0 RID: 46512 RVA: 0x0037CF56 File Offset: 0x0037B156
		public void StartContentDownload(DownloadTags.Content contentTag)
		{
			this.m_pausedExternally = false;
			this.StartContentDownload(new string[]
			{
				DownloadTags.GetTagString(contentTag)
			});
		}

		// Token: 0x0600B5B1 RID: 46513 RVA: 0x0037CF74 File Offset: 0x0037B174
		public bool IsCompletedInitialBaseDownload()
		{
			return this.m_assetDownloader == null || this.m_assetDownloader.DownloadAllFinished || this.m_assetDownloader.IsRunningNewerBinaryThanLive || (this.m_initialBaseTags != null && this.m_initialBaseTags.Length != 0 && this.IsReadyAssetsInTags(new string[]
			{
				DownloadTags.GetTagString(DownloadTags.Quality.Manifest),
				DownloadTags.GetTagString(DownloadTags.Content.Base)
			}) && this.GetTagDownloadStatus(this.m_initialBaseTags).Complete);
		}

		// Token: 0x0600B5B2 RID: 46514 RVA: 0x0037CFEC File Offset: 0x0037B1EC
		public void StopOptionalDownloads()
		{
			IAssetDownloader assetDownloader = this.m_assetDownloader;
			bool flag = assetDownloader != null && assetDownloader.IsReady;
			if (!flag || !this.IsCompletedInitialBaseDownload())
			{
				Log.Downloader.PrintDebug("Could not stop optional downloads: {0}", new object[]
				{
					(!flag) ? "Asset downloader not ready" : "Initial downloads not complete"
				});
				return;
			}
			Log.Downloader.PrintDebug("Requesting optional downloads to stop", Array.Empty<object>());
			IAssetDownloader assetDownloader2 = this.m_assetDownloader;
			if (assetDownloader2 == null)
			{
				return;
			}
			assetDownloader2.StopDownloads();
		}

		// Token: 0x0600B5B3 RID: 46515 RVA: 0x0037D064 File Offset: 0x0037B264
		private void StartInitialBaseDownload()
		{
			HearthstoneApplication.SendStartupTimeTelemetry("GameDownloadManager.StartInitialBaseDownload");
			List<string> list = new List<string>();
			if (this.DisabledAdventuresForStreaming.Length != 0)
			{
				if (AssetManifest.Get() != null)
				{
					string[] tagsInTagGroup = AssetManifest.Get().GetTagsInTagGroup(DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Content));
					list.AddRange(tagsInTagGroup);
				}
				else
				{
					list.AddRange(this.DisabledAdventuresForStreaming);
				}
			}
			else
			{
				list.Add(DownloadTags.GetTagString(DownloadTags.Content.Base));
			}
			if (this.m_initialBaseTags == null || this.m_initialBaseTags.Length == 0)
			{
				List<string> list2 = new List<string>();
				list2.Add(DownloadTags.GetTagString(DownloadTags.Quality.Manifest));
				list2.Add(DownloadTags.GetTagString(DownloadTags.Quality.Initial));
				list2.AddRange(list);
				this.m_initialBaseTags = list2.ToArray();
			}
			this.StartContentDownload(list.ToArray());
		}

		// Token: 0x0600B5B4 RID: 46516 RVA: 0x0037D114 File Offset: 0x0037B314
		private void StartContentDownload(string[] contentTag)
		{
			if (this.m_assetDownloader == null)
			{
				Log.Downloader.PrintError("StartContentDownload: AssetDownloader is not ready yet!", Array.Empty<object>());
				return;
			}
			string[] array = this.CombineWithAllAppropriateTags(contentTag);
			this.LastRequestedContentTags = array;
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.ControlANRMonitor(false);
			}
			this.m_assetDownloader.StartDownload(array, this.MaximumQualityTag == DownloadTags.Quality.Initial, this.m_localeChange);
		}

		// Token: 0x0600B5B5 RID: 46517 RVA: 0x0037D179 File Offset: 0x0037B379
		protected void ResumeStalledDownloadsIfAble()
		{
			if (this.ShouldRestartStalledDownloads())
			{
				this.ResumeStalledDownloads();
			}
		}

		// Token: 0x0600B5B6 RID: 46518 RVA: 0x0037D189 File Offset: 0x0037B389
		private void ResumeStalledDownloads()
		{
			this.m_timeOfLastAutoResumeAttempt = this.GetRealtimeSinceStartup();
			this.ResumeAllDownloads();
		}

		// Token: 0x0600B5B7 RID: 46519 RVA: 0x002B6A8D File Offset: 0x002B4C8D
		protected virtual float GetRealtimeSinceStartup()
		{
			return Time.realtimeSinceStartup;
		}

		// Token: 0x0600B5B8 RID: 46520 RVA: 0x0037D19D File Offset: 0x0037B39D
		private bool ShouldRestartStalledDownloads()
		{
			return this.m_assetDownloader != null && this.m_assetDownloader.IsReady && !this.IsAutoResumeThrottled() && this.AreOptionalDownloadsStalled();
		}

		// Token: 0x0600B5B9 RID: 46521 RVA: 0x0037D1C4 File Offset: 0x0037B3C4
		private bool AreOptionalDownloadsStalled()
		{
			if (this.IsCompletedInitialBaseDownload() && this.IsAnyDownloadRequestedAndIncomplete && this.m_assetDownloader.State != AssetDownloaderState.DOWNLOADING)
			{
				InterruptionReason interruptionReason = this.InterruptionReason;
				return !this.IsInterrupted || interruptionReason == InterruptionReason.Error || interruptionReason == InterruptionReason.Unknown;
			}
			return false;
		}

		// Token: 0x0600B5BA RID: 46522 RVA: 0x0037D20A File Offset: 0x0037B40A
		private bool IsAutoResumeThrottled()
		{
			return this.GetRealtimeSinceStartup() - this.m_timeOfLastAutoResumeAttempt < 5f;
		}

		// Token: 0x0600B5BB RID: 46523 RVA: 0x0037D220 File Offset: 0x0037B420
		public bool IsAssetDownloaded(string assetGuid)
		{
			if (this.m_assetDownloader == null)
			{
				return !string.IsNullOrEmpty(assetGuid);
			}
			string bundleName;
			return !string.IsNullOrEmpty(assetGuid) && AssetManifest.Get().TryGetDirectBundleFromGuid(assetGuid, out bundleName) && this.m_assetDownloader.IsBundleDownloaded(bundleName);
		}

		// Token: 0x0600B5BC RID: 46524 RVA: 0x0037D266 File Offset: 0x0037B466
		public bool IsBundleDownloaded(string bundleName)
		{
			return this.m_assetDownloader == null || this.m_assetDownloader.IsBundleDownloaded(bundleName);
		}

		// Token: 0x0600B5BD RID: 46525 RVA: 0x0037D27E File Offset: 0x0037B47E
		public bool IsReadyAssetsInTags(string[] tags)
		{
			return this.m_assetDownloader == null || this.m_assetDownloader.GetDownloadStatus(tags).Complete;
		}

		// Token: 0x0600B5BE RID: 46526 RVA: 0x0037D29B File Offset: 0x0037B49B
		public TagDownloadStatus GetTagDownloadStatus(string[] tags)
		{
			if (this.m_assetDownloader == null)
			{
				return new TagDownloadStatus
				{
					Complete = true
				};
			}
			return this.m_assetDownloader.GetDownloadStatus(tags);
		}

		// Token: 0x0600B5BF RID: 46527 RVA: 0x0037D2BE File Offset: 0x0037B4BE
		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			if (this.m_assetDownloader == null)
			{
				return null;
			}
			return this.m_assetDownloader.GetCurrentDownloadStatus();
		}

		// Token: 0x0600B5C0 RID: 46528 RVA: 0x0037D2D5 File Offset: 0x0037B4D5
		public ContentDownloadStatus GetContentDownloadStatus(DownloadTags.Content contentTag)
		{
			return this.GetContentDownloadStatus(DownloadTags.GetTagString(contentTag));
		}

		// Token: 0x0600B5C1 RID: 46529 RVA: 0x0037D2E3 File Offset: 0x0037B4E3
		protected IEnumerable<DownloadTags.Quality> QualityTagsAfter(DownloadTags.Quality startTag, bool fallbackToLowRes)
		{
			return this.QualityTagsBetween(startTag, DownloadTags.GetLastEnum<DownloadTags.Quality>(), fallbackToLowRes);
		}

		// Token: 0x0600B5C2 RID: 46530 RVA: 0x0037D2F2 File Offset: 0x0037B4F2
		protected IEnumerable<DownloadTags.Quality> QualityTagsBetween(DownloadTags.Quality startTag, DownloadTags.Quality endTag, bool fallbackToLowRes)
		{
			bool started = false;
			string prevTag = string.Empty;
			foreach (object obj in Enum.GetValues(typeof(DownloadTags.Quality)))
			{
				DownloadTags.Quality qualityTag = (DownloadTags.Quality)obj;
				if (!started && qualityTag == startTag)
				{
					started = true;
				}
				if (started && (qualityTag != DownloadTags.Quality.PortHigh || !fallbackToLowRes))
				{
					if (AssetManifest.Get() != null)
					{
						string overrideTag = AssetManifest.Get().ConvertToOverrideTag(DownloadTags.GetTagString(qualityTag), DownloadTags.GetTagGroupString(DownloadTags.TagGroup.Quality));
						if (overrideTag != prevTag)
						{
							yield return qualityTag;
						}
						prevTag = overrideTag;
						overrideTag = null;
					}
					else if (qualityTag != DownloadTags.Quality.Unknown)
					{
						yield return qualityTag;
					}
					if (qualityTag == endTag)
					{
						yield break;
					}
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600B5C3 RID: 46531 RVA: 0x0037D310 File Offset: 0x0037B510
		private DownloadTags.Quality GetNextTag(DownloadTags.Quality startTag)
		{
			foreach (DownloadTags.Quality quality in this.QualityTagsAfter(startTag, PlatformSettings.ShouldFallbackToLowRes))
			{
				if (quality != startTag)
				{
					return quality;
				}
			}
			return startTag;
		}

		// Token: 0x0600B5C4 RID: 46532 RVA: 0x0037D368 File Offset: 0x0037B568
		private ContentDownloadStatus GetContentDownloadStatus(string contentTag)
		{
			if (this.m_assetDownloader == null)
			{
				return this.m_curContentDownloadStatus;
			}
			string[] source = this.CombineWithAllAppropriateTags(new string[]
			{
				contentTag
			});
			TagDownloadStatus downloadStatus = this.m_assetDownloader.GetDownloadStatus(source.ToArray<string>());
			this.m_curContentDownloadStatus.BytesTotal = downloadStatus.BytesTotal;
			this.m_curContentDownloadStatus.BytesDownloaded = downloadStatus.BytesDownloaded;
			this.m_curContentDownloadStatus.Progress = downloadStatus.Progress;
			this.m_curContentDownloadStatus.ContentTag = contentTag;
			foreach (DownloadTags.Quality inProgressQualityTag in this.QualityTagsAfter(this.m_curContentDownloadStatus.InProgressQualityTag, PlatformSettings.ShouldFallbackToLowRes))
			{
				this.m_curContentDownloadStatus.InProgressQualityTag = inProgressQualityTag;
				if (!this.m_assetDownloader.GetDownloadStatus(new string[]
				{
					DownloadTags.GetTagString(this.m_curContentDownloadStatus.InProgressQualityTag),
					contentTag
				}).Complete)
				{
					break;
				}
			}
			return this.m_curContentDownloadStatus;
		}

		// Token: 0x0600B5C5 RID: 46533 RVA: 0x0037D474 File Offset: 0x0037B674
		public void StartUpdateProcess(bool localeChange)
		{
			Log.Downloader.PrintInfo("StartUpdate locale={0}", new object[]
			{
				Localization.GetLocale().ToString()
			});
			this.m_requestedInitialAssets = false;
			this.m_requestedStreamingAssets = false;
			this.m_bRegisterSceneLoadCallback = false;
			this.m_localeChange = localeChange;
			this.LastInprogressTag = DownloadTags.Quality.Unknown;
			if (this.m_initialBaseTags != null)
			{
				this.m_initialBaseTags = null;
			}
			this.StartUpdateJobIfNotRunning();
		}

		// Token: 0x0600B5C6 RID: 46534 RVA: 0x0037D4E4 File Offset: 0x0037B6E4
		protected virtual void StartUpdateJobIfNotRunning()
		{
			Processor.QueueJobIfNotExist("GameDownloadManager.UpdateProcess", this.Job_UpdateProcess(), Array.Empty<IJobDependency>());
		}

		// Token: 0x0600B5C7 RID: 46535 RVA: 0x0037D4FC File Offset: 0x0037B6FC
		public void StartUpdateProcessForOptional()
		{
			this.MaximumQualityTag = DownloadTags.GetLastEnum<DownloadTags.Quality>();
			this.StartInitialBaseDownload();
			this.StartUpdateJobIfNotRunning();
		}

		// Token: 0x0600B5C8 RID: 46536 RVA: 0x0037D515 File Offset: 0x0037B715
		public void PauseAllDownloads()
		{
			this.m_pausedExternally = true;
			this.PauseAllDownloadsInternal();
		}

		// Token: 0x0600B5C9 RID: 46537 RVA: 0x0037D524 File Offset: 0x0037B724
		private void PauseAllDownloadsInternal()
		{
			if (this.m_assetDownloader == null)
			{
				return;
			}
			this.m_assetDownloader.PauseAllDownloads();
		}

		// Token: 0x0600B5CA RID: 46538 RVA: 0x0037D53A File Offset: 0x0037B73A
		private void CreateUpdateProcessJob()
		{
			if (this.IsAnyDownloadRequestedAndIncomplete)
			{
				Processor.QueueJobIfNotExist("GameDownloadManager.UpdateProcess", this.Job_UpdateProcess(), Array.Empty<IJobDependency>());
			}
		}

		// Token: 0x0600B5CB RID: 46539 RVA: 0x0037D55A File Offset: 0x0037B75A
		public void ResumeAllDownloads()
		{
			this.m_pausedExternally = false;
			this.ResumeAllDownloadsInternal();
		}

		// Token: 0x0600B5CC RID: 46540 RVA: 0x0037D56C File Offset: 0x0037B76C
		private void ResumeAllDownloadsInternal()
		{
			if (this.m_assetDownloader == null)
			{
				return;
			}
			if (this.m_assetDownloader.GetCurrentDownloadStatus() != null)
			{
				HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
				if (hearthstoneApplication != null)
				{
					hearthstoneApplication.ControlANRMonitor(false);
				}
				this.m_assetDownloader.StartDownload(this.LastRequestedContentTags, this.MaximumQualityTag == DownloadTags.Quality.Initial, this.m_localeChange);
			}
		}

		// Token: 0x0600B5CD RID: 46541 RVA: 0x0037D5C0 File Offset: 0x0037B7C0
		public void DeleteDownloadedData()
		{
			if (this.m_assetDownloader == null)
			{
				return;
			}
			this.m_assetDownloader.PauseAllDownloads();
			this.m_assetDownloader.DeleteDownloadedData();
		}

		// Token: 0x0600B5CE RID: 46542 RVA: 0x0037D5E1 File Offset: 0x0037B7E1
		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
			if (this.m_assetDownloader == null)
			{
				return;
			}
			this.m_assetDownloader.OnSceneLoad(prevMode, nextMode, userData);
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600B5CF RID: 46543 RVA: 0x0037D5FC File Offset: 0x0037B7FC
		public bool ShouldDownloadLocalizedAssets
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return false;
				}
				if (PlatformSettings.IsMobileRuntimeOS)
				{
					if (this.m_assetDownloader.IsRunningNewerBinaryThanLive)
					{
						return false;
					}
					this.m_assetDownloader.PauseAllDownloads();
				}
				Log.Downloader.PrintInfo("Begin to download new locale data", Array.Empty<object>());
				this.DeleteSerializedState();
				this.m_assetDownloader.PrepareRestart();
				return true;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600B5D0 RID: 46544 RVA: 0x0037D65A File Offset: 0x0037B85A
		public string AgentLogPath
		{
			get
			{
				if (this.m_assetDownloader == null)
				{
					return null;
				}
				return this.m_assetDownloader.AgentLogPath;
			}
		}

		// Token: 0x0600B5D1 RID: 46545 RVA: 0x0037D671 File Offset: 0x0037B871
		private LoginManager GetLoginManager()
		{
			if (this.m_loginManager == null)
			{
				this.m_loginManager = HearthstoneServices.Get<LoginManager>();
			}
			return this.m_loginManager;
		}

		// Token: 0x0600B5D2 RID: 46546 RVA: 0x0037D68C File Offset: 0x0037B88C
		private SceneMgr GetSceneManager()
		{
			if (this.m_sceneMgr == null)
			{
				this.m_sceneMgr = HearthstoneServices.Get<SceneMgr>();
			}
			return this.m_sceneMgr;
		}

		// Token: 0x0600B5D3 RID: 46547 RVA: 0x0037D6A7 File Offset: 0x0037B8A7
		public void UnknownSourcesListener(string onOff)
		{
			this.m_assetDownloader.UnknownSourcesListener(onOff);
		}

		// Token: 0x0600B5D4 RID: 46548 RVA: 0x0037D6B5 File Offset: 0x0037B8B5
		public void InstallAPKListener(string status)
		{
			this.m_assetDownloader.InstallAPKListener(status);
		}

		// Token: 0x04009763 RID: 38755
		private const string UPDATE_PROCESS_JOB_NAME = "GameDownloadManager.UpdateProcess";

		// Token: 0x04009764 RID: 38756
		private const float PROCESS_DELTA_TIME = 0.5f;

		// Token: 0x04009765 RID: 38757
		protected const float MINIMUM_TIME_BETWEEN_RETRY_ATTEMPTS = 5f;

		// Token: 0x04009766 RID: 38758
		private static readonly Dictionary<Locale, string> s_localeTags = new Dictionary<Locale, string>();

		// Token: 0x04009767 RID: 38759
		protected IAssetDownloader m_assetDownloader;

		// Token: 0x04009768 RID: 38760
		private GameDownloadManager.SerializedState m_serializedState = new GameDownloadManager.SerializedState();

		// Token: 0x04009769 RID: 38761
		protected bool m_pausedExternally;

		// Token: 0x0400976A RID: 38762
		private bool m_requestedInitialAssets;

		// Token: 0x0400976B RID: 38763
		private bool m_requestedStreamingAssets;

		// Token: 0x0400976C RID: 38764
		private bool m_bRegisterSceneLoadCallback;

		// Token: 0x0400976D RID: 38765
		private bool m_localeChange;

		// Token: 0x0400976E RID: 38766
		protected float m_timeOfLastAutoResumeAttempt;

		// Token: 0x0400976F RID: 38767
		private ContentDownloadStatus m_curContentDownloadStatus = new ContentDownloadStatus();

		// Token: 0x04009770 RID: 38768
		private string[] m_initialBaseTags;

		// Token: 0x04009771 RID: 38769
		private LoginManager m_loginManager;

		// Token: 0x04009772 RID: 38770
		private SceneMgr m_sceneMgr;

		// Token: 0x02002870 RID: 10352
		[Serializable]
		private class SerializedState
		{
			// Token: 0x0400F9B6 RID: 63926
			public DownloadTags.Quality MaximumQualityTag;

			// Token: 0x0400F9B7 RID: 63927
			public string[] LastRequestedContentTags;

			// Token: 0x0400F9B8 RID: 63928
			public DownloadTags.Quality LastInprogressTag;
		}
	}
}
