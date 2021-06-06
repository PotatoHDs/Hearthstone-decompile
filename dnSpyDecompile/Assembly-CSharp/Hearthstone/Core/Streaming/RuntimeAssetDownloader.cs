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
	// Token: 0x0200108F RID: 4239
	public class RuntimeAssetDownloader : ICallbackHandler, IAssetDownloader
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600B757 RID: 46935 RVA: 0x00381F97 File Offset: 0x00380197
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600B758 RID: 46936 RVA: 0x00381FB1 File Offset: 0x003801B1
		// (set) Token: 0x0600B759 RID: 46937 RVA: 0x00381FBF File Offset: 0x003801BF
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600B75A RID: 46938 RVA: 0x00381FCE File Offset: 0x003801CE
		private static bool IsEnabledUpdate
		{
			get
			{
				return UpdateUtils.AreUpdatesEnabledForCurrentPlatform;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600B75B RID: 46939 RVA: 0x00381FD5 File Offset: 0x003801D5
		// (set) Token: 0x0600B75C RID: 46940 RVA: 0x00381FE3 File Offset: 0x003801E3
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

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600B75D RID: 46941 RVA: 0x00381FF4 File Offset: 0x003801F4
		private bool IsInGame
		{
			get
			{
				SceneMgr sceneMgr;
				return HearthstoneServices.TryGet<SceneMgr>(out sceneMgr) && sceneMgr.IsInGame();
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600B75E RID: 46942 RVA: 0x00382012 File Offset: 0x00380212
		protected bool IsEnabledDownload
		{
			get
			{
				return !this.m_optionalDownload || (DownloadPermissionManager.DownloadEnabled && !this.IsInGame);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600B75F RID: 46943 RVA: 0x00382030 File Offset: 0x00380230
		protected bool canDownload
		{
			get
			{
				return this.IsEnabledDownload && NetworkReachabilityManager.InternetAvailable && (!this.blockedByCellPermission || this.TotalBytes < (long)Options.Get().GetInt(Option.CELL_PROMPT_THRESHOLD));
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600B760 RID: 46944 RVA: 0x00382062 File Offset: 0x00380262
		private bool blockedByCellPermission
		{
			get
			{
				return NetworkReachabilityManager.OnCellular && !this.m_cellularEnabledSession;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600B761 RID: 46945 RVA: 0x0001FA65 File Offset: 0x0001DC65
		private bool VersionMismatchErrorEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600B762 RID: 46946 RVA: 0x00382076 File Offset: 0x00380276
		private bool RestartOnFalseHDDFullEnabled
		{
			get
			{
				return Vars.Key("Mobile.RestartOnFalseHDDFull").GetBool(true);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600B763 RID: 46947 RVA: 0x00382088 File Offset: 0x00380288
		private string MobileMode
		{
			get
			{
				return Vars.Key("Mobile.Mode").GetStr("Production");
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600B764 RID: 46948 RVA: 0x0038209E File Offset: 0x0038029E
		// (set) Token: 0x0600B765 RID: 46949 RVA: 0x003820A6 File Offset: 0x003802A6
		public long TotalBytes { get; protected set; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600B766 RID: 46950 RVA: 0x003820AF File Offset: 0x003802AF
		// (set) Token: 0x0600B767 RID: 46951 RVA: 0x003820B7 File Offset: 0x003802B7
		public long DownloadedBytes { get; protected set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600B768 RID: 46952 RVA: 0x003820C0 File Offset: 0x003802C0
		// (set) Token: 0x0600B769 RID: 46953 RVA: 0x003820C8 File Offset: 0x003802C8
		public long RequiredBytes { get; protected set; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600B76A RID: 46954 RVA: 0x003820D1 File Offset: 0x003802D1
		private float ProgressPercent
		{
			get
			{
				if (this.m_cancelledByUser || this.TotalBytes == 0L)
				{
					return this.m_prevProgress;
				}
				return this.m_progress;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600B76B RID: 46955 RVA: 0x003820F0 File Offset: 0x003802F0
		private string InstallDataPath
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_installDataPath))
				{
					this.m_installDataPath = RuntimeAssetDownloader.INSTALL_PATH + "/Data";
					if (PlatformSettings.RuntimeOS == OSCategory.Android)
					{
						this.m_installDataPath = this.m_installDataPath + "/" + AndroidDeviceSettings.Get().InstalledTexture;
					}
				}
				return this.m_installDataPath;
			}
		}

		// Token: 0x0600B76C RID: 46956 RVA: 0x00382150 File Offset: 0x00380350
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

		// Token: 0x0600B76D RID: 46957 RVA: 0x003821BC File Offset: 0x003803BC
		public void OnPatchOverrideUrlChanged(OverrideUrlChangedMessage msg)
		{
			if (!msg.m_product.Equals("hsb"))
			{
				global::Log.Downloader.PrintError("Unknown product name for {0}", new object[]
				{
					msg.m_product
				});
				return;
			}
			if (!string.IsNullOrEmpty(msg.m_overrideUrl))
			{
				global::Log.Downloader.PrintInfo("Using PatchURL: " + msg.m_overrideUrl, Array.Empty<object>());
				try
				{
					string[] array = new Uri(msg.m_overrideUrl).Host.Split(new char[]
					{
						'.'
					});
					if (array.Length != 0)
					{
						if (array[0].Length > 20)
						{
							this.PatchOverrideUrl = array[0].Substring(0, 20);
						}
						else
						{
							this.PatchOverrideUrl = array[0];
						}
					}
					return;
				}
				catch (Exception ex)
				{
					global::Log.Downloader.PrintError("Failed to set PatchURL: {0}", new object[]
					{
						ex.Message
					});
					return;
				}
			}
			if (!this.PatchOverrideUrl.Equals("Live"))
			{
				global::Log.Downloader.PrintInfo("Back to Live: " + msg.m_overrideUrl, Array.Empty<object>());
				this.PatchOverrideUrl = "Back to Live";
			}
		}

		// Token: 0x0600B76E RID: 46958 RVA: 0x003822E8 File Offset: 0x003804E8
		public void OnVersionServiceOverrideUrlChanged(OverrideUrlChangedMessage msg)
		{
			global::Log.Downloader.PrintInfo("OnVersionServericeOverrideUrlChanged: {0} -- {1}", new object[]
			{
				msg.m_product,
				msg.m_overrideUrl
			});
			if (!msg.m_product.Equals("hsb"))
			{
				global::Log.Downloader.PrintError("Unknown product name for {0}", new object[]
				{
					msg.m_product
				});
				return;
			}
			if ((string.IsNullOrEmpty(msg.m_overrideUrl) || !msg.m_overrideUrl.Contains("hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net")) && !this.VersionOverrideUrl.Equals("Live"))
			{
				this.VersionOverrideUrl = "Back to Live";
			}
		}

		// Token: 0x0600B76F RID: 46959 RVA: 0x0038238C File Offset: 0x0038058C
		public void OnNetworkStatusChangedMessage(NetworkStatusChangedMessage msg)
		{
			global::Log.Downloader.PrintInfo("OnNetworkStatusChangedMessage - cell {0}, wifi {1}, isCellAllowed {2}", new object[]
			{
				msg.m_hasCell,
				msg.m_hasWifi,
				msg.m_isCellAllowed
			});
			if (this.m_pausedByNetwork)
			{
				this.m_redrawDialog = true;
			}
		}

		// Token: 0x0600B770 RID: 46960 RVA: 0x003823E8 File Offset: 0x003805E8
		public void OnDownloadPausedDueToNetworkStatusChange(NetworkStatusChangedMessage msg)
		{
			global::Log.Downloader.PrintInfo("OnDownloadPausedDueToNetworkStatusChange - cell {0}, wifi {1}, isCellAllowed {2}", new object[]
			{
				msg.m_hasCell,
				msg.m_hasWifi,
				msg.m_isCellAllowed
			});
			this.m_pausedByNetwork = true;
		}

		// Token: 0x0600B771 RID: 46961 RVA: 0x0038243C File Offset: 0x0038063C
		public void OnDownloadResumedDueToNetworkStatusChange(NetworkStatusChangedMessage msg)
		{
			global::Log.Downloader.PrintInfo("OnDownloadResumedDueToNetworkStatusChange - cell {0}, wifi {1}, isCellAllowed {2}", new object[]
			{
				msg.m_hasCell,
				msg.m_hasWifi,
				msg.m_isCellAllowed
			});
			if (msg.m_isCellAllowed && msg.m_hasCell && !msg.m_hasWifi)
			{
				global::Log.Downloader.PrintInfo("User allowed to resume by using Cellular", Array.Empty<object>());
			}
			this.m_cellularEnabledSession = msg.m_isCellAllowed;
			this.m_pausedByNetwork = false;
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x003824C7 File Offset: 0x003806C7
		public void OnDownloadPausedByUser()
		{
			global::Log.Downloader.PrintInfo("OnDownloadPausedByUser", Array.Empty<object>());
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x003824DD File Offset: 0x003806DD
		public void OnDownloadResumedByUser()
		{
			global::Log.Downloader.PrintInfo("OnDownloadResumedByUser", Array.Empty<object>());
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600B774 RID: 46964 RVA: 0x003824F3 File Offset: 0x003806F3
		// (set) Token: 0x0600B775 RID: 46965 RVA: 0x003824FB File Offset: 0x003806FB
		public AssetDownloaderState State { get; private set; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600B776 RID: 46966 RVA: 0x00382504 File Offset: 0x00380704
		public string AgentLogPath
		{
			get
			{
				return RuntimeAssetDownloader.INSTALL_PATH + "__agent__/Logs";
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600B777 RID: 46967 RVA: 0x00382515 File Offset: 0x00380715
		// (set) Token: 0x0600B778 RID: 46968 RVA: 0x0038251D File Offset: 0x0038071D
		public bool IsReady { get; private set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600B779 RID: 46969 RVA: 0x00382526 File Offset: 0x00380726
		// (set) Token: 0x0600B77A RID: 46970 RVA: 0x0038252E File Offset: 0x0038072E
		public bool IsRunningNewerBinaryThanLive { get; private set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600B77B RID: 46971 RVA: 0x00382537 File Offset: 0x00380737
		// (set) Token: 0x0600B77C RID: 46972 RVA: 0x0038253F File Offset: 0x0038073F
		public bool IsVersionChanged { get; private set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600B77D RID: 46973 RVA: 0x00382548 File Offset: 0x00380748
		// (set) Token: 0x0600B77E RID: 46974 RVA: 0x00382550 File Offset: 0x00380750
		public bool IsAssetManifestReady { get; private set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600B77F RID: 46975 RVA: 0x00382559 File Offset: 0x00380759
		// (set) Token: 0x0600B780 RID: 46976 RVA: 0x00382566 File Offset: 0x00380766
		public bool DownloadAllFinished
		{
			get
			{
				return this.m_isDownloadAllFinished.Value;
			}
			set
			{
				this.m_isDownloadAllFinished.Set(value);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600B781 RID: 46977 RVA: 0x00382574 File Offset: 0x00380774
		public bool IsNewMobileVersionReleased
		{
			get
			{
				if (this.m_agentStatus == null || this.m_agentState == RuntimeAssetDownloader.AgentState.ERROR)
				{
					return true;
				}
				int installedVersionCode = this.GetInstalledVersionCode();
				int versionCode = this.GetVersionCode(this.m_agentStatus.m_configuration.m_liveDisplayVersion);
				return installedVersionCode < versionCode;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600B782 RID: 46978 RVA: 0x003825B4 File Offset: 0x003807B4
		// (set) Token: 0x0600B783 RID: 46979 RVA: 0x003825BC File Offset: 0x003807BC
		public bool DisabledErrorMessageDialog { get; set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600B784 RID: 46980 RVA: 0x003825C5 File Offset: 0x003807C5
		// (set) Token: 0x0600B785 RID: 46981 RVA: 0x003825CD File Offset: 0x003807CD
		public string PatchOverrideUrl { get; private set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600B786 RID: 46982 RVA: 0x003825D6 File Offset: 0x003807D6
		// (set) Token: 0x0600B787 RID: 46983 RVA: 0x003825DE File Offset: 0x003807DE
		public string VersionOverrideUrl { get; private set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600B788 RID: 46984 RVA: 0x003825E7 File Offset: 0x003807E7
		// (set) Token: 0x0600B789 RID: 46985 RVA: 0x003825EF File Offset: 0x003807EF
		public string[] DisabledAdventuresForStreaming
		{
			get
			{
				return this.m_disabledAdventuresForStreaming;
			}
			private set
			{
				this.m_disabledAdventuresForStreaming = value;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600B78A RID: 46986 RVA: 0x003825F8 File Offset: 0x003807F8
		// (set) Token: 0x0600B78B RID: 46987 RVA: 0x00382600 File Offset: 0x00380800
		public double BytesPerSecond { get; private set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600B78C RID: 46988 RVA: 0x00382609 File Offset: 0x00380809
		// (set) Token: 0x0600B78D RID: 46989 RVA: 0x00382628 File Offset: 0x00380828
		public int MaxDownloadSpeed
		{
			get
			{
				if (this.m_instantMaxSpeed != 0)
				{
					return this.m_instantMaxSpeed;
				}
				return Options.Get().GetInt(Option.MAX_DOWNLOAD_SPEED);
			}
			set
			{
				this.m_instantMaxSpeed = value;
				if (this.IsUpdating())
				{
					this.ResetDownloadSpeed(value);
					return;
				}
				if (SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
				{
					AgentEmbeddedAPI.SetUpdateParams(string.Format("download_limit={0}", value));
				}
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600B78E RID: 46990 RVA: 0x00382676 File Offset: 0x00380876
		// (set) Token: 0x0600B78F RID: 46991 RVA: 0x0038267E File Offset: 0x0038087E
		public int InGameStreamingDefaultSpeed
		{
			get
			{
				return this.m_inGameStreamingDefaultSpeed;
			}
			set
			{
				if (value < 0)
				{
					this.m_inGameStreamingOff = true;
					return;
				}
				this.m_inGameStreamingOff = false;
				this.m_inGameStreamingDefaultSpeed = value;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600B790 RID: 46992 RVA: 0x0038269A File Offset: 0x0038089A
		// (set) Token: 0x0600B791 RID: 46993 RVA: 0x003826BD File Offset: 0x003808BD
		public int DownloadSpeedInGame
		{
			get
			{
				if (this.m_instantSpeed != 0)
				{
					return this.m_instantSpeed;
				}
				return Options.Get().GetInt(Option.STREAMING_SPEED_IN_GAME, this.InGameStreamingDefaultSpeed);
			}
			set
			{
				this.m_instantSpeed = value;
				if (this.State == AssetDownloaderState.DOWNLOADING && SceneMgr.Get().IsInGame())
				{
					this.ResetDownloadSpeed(this.m_instantSpeed);
				}
			}
		}

		// Token: 0x0600B792 RID: 46994 RVA: 0x003826E8 File Offset: 0x003808E8
		public bool Initialize()
		{
			this.State = AssetDownloaderState.UNINITIALIZED;
			return this.SetInitialState();
		}

		// Token: 0x0600B793 RID: 46995 RVA: 0x003826F7 File Offset: 0x003808F7
		public void Update(bool firstCall)
		{
			this.AgentInitializeWhenReady();
			this.ProcessMobile();
		}

		// Token: 0x0600B794 RID: 46996 RVA: 0x00382708 File Offset: 0x00380908
		public void Shutdown()
		{
			global::Log.Downloader.PrintInfo("Shutdown", Array.Empty<object>());
			if (PlatformSettings.IsMobileRuntimeOS && !Application.isEditor)
			{
				AgentEmbeddedAPI.Shutdown();
			}
			global::Log.Downloader.PrintInfo("Disposed listeners for Agent", Array.Empty<object>());
			if (this.m_callbackDisposer != null)
			{
				this.m_callbackDisposer.Dispose();
			}
		}

		// Token: 0x0600B795 RID: 46997 RVA: 0x00382763 File Offset: 0x00380963
		public TagDownloadStatus GetDownloadStatus(string[] tags)
		{
			return this.FindDownloadStatus(tags);
		}

		// Token: 0x0600B796 RID: 46998 RVA: 0x0038276C File Offset: 0x0038096C
		public void StopDownloads()
		{
			AgentEmbeddedAPI.CancelAllOperations();
		}

		// Token: 0x0600B797 RID: 46999 RVA: 0x00382774 File Offset: 0x00380974
		public TagDownloadStatus GetCurrentDownloadStatus()
		{
			return this.m_curDownloadStatus;
		}

		// Token: 0x0600B798 RID: 47000 RVA: 0x0038277C File Offset: 0x0038097C
		public void StartDownload(string[] tags, bool initialDownload, bool localeChanged)
		{
			RuntimeAssetDownloader.KindOfUpdate kind;
			if (localeChanged)
			{
				TelemetryManager.Client().SendLocaleDataUpdateStarted(Localization.GetLocale().ToString());
				kind = RuntimeAssetDownloader.KindOfUpdate.LOCALE_UPDATE;
				this.m_optionalDownload = false;
				this.m_askCellPopup = true;
			}
			else if (initialDownload)
			{
				if (this.UpdateState == "Updated")
				{
					this.GoToIdleState(true);
					return;
				}
				TelemetryManager.Client().SendDataUpdateStarted();
				kind = RuntimeAssetDownloader.KindOfUpdate.GLOBAL_UPDATE;
				this.m_optionalDownload = false;
			}
			else
			{
				kind = RuntimeAssetDownloader.KindOfUpdate.OPTIONAL_UPDATE;
				this.m_optionalDownload = true;
				this.m_askCellPopup = true;
			}
			this.StartDownloadInternal(tags, kind);
		}

		// Token: 0x0600B799 RID: 47001 RVA: 0x00382808 File Offset: 0x00380A08
		private void StartDownloadInternal(string[] tags, RuntimeAssetDownloader.KindOfUpdate kind)
		{
			global::Log.Downloader.PrintInfo("StartDownload with {0}", new object[]
			{
				string.Join(", ", tags)
			});
			this.m_curDownloadStatus = this.CreateDownloadStatusIfNotExist(tags);
			if (!this.m_curDownloadStatus.Complete)
			{
				this.DownloadAllFinished = false;
				this.DoUpdate(kind);
			}
		}

		// Token: 0x0600B79A RID: 47002 RVA: 0x0038276C File Offset: 0x0038096C
		public void PauseAllDownloads()
		{
			AgentEmbeddedAPI.CancelAllOperations();
		}

		// Token: 0x0600B79B RID: 47003 RVA: 0x00382864 File Offset: 0x00380A64
		public void DeleteDownloadedData()
		{
			string[] dataFolders = this.m_dataFolders;
			for (int i = 0; i < dataFolders.Length; i++)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dataFolders[i]);
				if (directoryInfo.Exists)
				{
					directoryInfo.Delete(true);
				}
			}
			global::Log.Downloader.PrintInfo("ClearDownloadedData", Array.Empty<object>());
		}

		// Token: 0x0600B79C RID: 47004 RVA: 0x003828B2 File Offset: 0x00380AB2
		public bool IsBundleDownloaded(string bundleName)
		{
			return !PlatformSettings.IsMobileRuntimeOS || this.m_tagIndicatorManager.IsReady(bundleName);
		}

		// Token: 0x0600B79D RID: 47005 RVA: 0x003828CC File Offset: 0x00380ACC
		public void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
		{
			if (this.State != AssetDownloaderState.DOWNLOADING)
			{
				return;
			}
			if (prevMode != SceneMgr.Mode.GAMEPLAY && nextMode == SceneMgr.Mode.GAMEPLAY)
			{
				if (this.m_inGameStreamingOff)
				{
					AgentEmbeddedAPI.CancelAllOperations();
					this.State = AssetDownloaderState.PAUSED_DURING_GAME;
					return;
				}
				this.ResetDownloadSpeed(this.DownloadSpeedInGame);
			}
			if (!this.m_inGameStreamingOff && prevMode == SceneMgr.Mode.GAMEPLAY && nextMode != SceneMgr.Mode.GAMEPLAY)
			{
				this.ResetDownloadSpeed(this.MaxDownloadSpeed);
			}
		}

		// Token: 0x0600B79E RID: 47006 RVA: 0x0038292B File Offset: 0x00380B2B
		public void PrepareRestart()
		{
			GameStrings.LoadNative();
			this.ResetToUpdate();
		}

		// Token: 0x0600B79F RID: 47007 RVA: 0x00382938 File Offset: 0x00380B38
		public void DoPostTasksAfterInitialDownload()
		{
			global::Log.Downloader.PrintInfo("Process the post tasks after finishing initial data", Array.Empty<object>());
			this.FilterForiCloud();
		}

		// Token: 0x0600B7A0 RID: 47008 RVA: 0x00382954 File Offset: 0x00380B54
		private float ElapsedTimeFromStart(float startTime)
		{
			return Time.realtimeSinceStartup - startTime;
		}

		// Token: 0x0600B7A1 RID: 47009 RVA: 0x00382960 File Offset: 0x00380B60
		private static int GetTagsHashCode(string[] tags)
		{
			int num = 17;
			foreach (string text in tags)
			{
				num ^= text.GetHashCode();
			}
			return num ^ Localization.GetLocaleHashCode();
		}

		// Token: 0x0600B7A2 RID: 47010 RVA: 0x00382998 File Offset: 0x00380B98
		private TagDownloadStatus CreateDownloadStatusIfNotExist(string[] tags)
		{
			int tagsHashCode = RuntimeAssetDownloader.GetTagsHashCode(tags);
			int index;
			TagDownloadStatus tagDownloadStatus;
			if (this.m_tagLocations.TryGetValue(tagsHashCode, out index))
			{
				tagDownloadStatus = this.m_tagDownloads.m_items[index];
			}
			else
			{
				tagDownloadStatus = new TagDownloadStatus();
				tagDownloadStatus.Tags = tags;
				this.m_tagLocations[tagsHashCode] = this.m_tagDownloads.m_items.Count;
				this.m_tagDownloads.m_items.Add(tagDownloadStatus);
			}
			return tagDownloadStatus;
		}

		// Token: 0x0600B7A3 RID: 47011 RVA: 0x00382A0C File Offset: 0x00380C0C
		private TagDownloadStatus FindDownloadStatus(string[] tags)
		{
			tags = this.RemoveRepeatedTags(tags);
			TagDownloadStatus tagDownloadStatus = this.CreateDownloadStatusIfNotExist(tags);
			if (!tagDownloadStatus.Complete)
			{
				tagDownloadStatus.Complete = this.m_tagIndicatorManager.IsReady(tagDownloadStatus.Tags);
			}
			return tagDownloadStatus;
		}

		// Token: 0x0600B7A4 RID: 47012 RVA: 0x00382A4C File Offset: 0x00380C4C
		private string[] RemoveRepeatedTags(string[] tags)
		{
			this.m_tempUniqueTags.Clear();
			bool flag = false;
			foreach (string item in tags)
			{
				if (!this.m_tempUniqueTags.Add(item))
				{
					flag = true;
				}
			}
			if (flag)
			{
				tags = this.m_tempUniqueTags.ToArray<string>();
			}
			return tags;
		}

		// Token: 0x0600B7A5 RID: 47013 RVA: 0x00382A9B File Offset: 0x00380C9B
		private void StartApkDownload()
		{
			this.StartDownloadInternal(new string[]
			{
				"apk"
			}, RuntimeAssetDownloader.KindOfUpdate.APK_UPDATE);
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600B7A6 RID: 47014 RVA: 0x00382AB2 File Offset: 0x00380CB2
		private static string DownloadStatsPath
		{
			get
			{
				return Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "DownloadProgress.json");
			}
		}

		// Token: 0x0600B7A7 RID: 47015 RVA: 0x00382AC4 File Offset: 0x00380CC4
		private void DeserializeDownloadStats()
		{
			if (File.Exists(RuntimeAssetDownloader.DownloadStatsPath))
			{
				try
				{
					string json = File.ReadAllText(RuntimeAssetDownloader.DownloadStatsPath);
					this.m_tagDownloads = JsonUtility.FromJson<RuntimeAssetDownloader.DownloadProgress>(json);
					int pos = 0;
					this.m_tagDownloads.m_items.ForEach(delegate(TagDownloadStatus i)
					{
						Dictionary<int, int> tagLocations = this.m_tagLocations;
						int tagsHashCode = RuntimeAssetDownloader.GetTagsHashCode(i.Tags);
						int pos = pos;
						pos++;
						tagLocations[tagsHashCode] = pos;
					});
				}
				catch (Exception ex)
				{
					global::Log.Downloader.PrintError("Unable to deserialize {0}: {1}", new object[]
					{
						RuntimeAssetDownloader.DownloadStatsPath,
						ex
					});
				}
			}
		}

		// Token: 0x0600B7A8 RID: 47016 RVA: 0x00382B5C File Offset: 0x00380D5C
		private void SerializeDownloadStats()
		{
			try
			{
				string contents = JsonUtility.ToJson(this.m_tagDownloads, !HearthstoneApplication.IsPublic());
				File.WriteAllText(RuntimeAssetDownloader.DownloadStatsPath, contents);
			}
			catch (Exception ex)
			{
				global::Log.Downloader.PrintError("Unable to serialize {0}: {1}", new object[]
				{
					RuntimeAssetDownloader.DownloadStatsPath,
					ex
				});
			}
		}

		// Token: 0x0600B7A9 RID: 47017 RVA: 0x00382BC0 File Offset: 0x00380DC0
		private void DeleteDownloadStats()
		{
			if (File.Exists(RuntimeAssetDownloader.DownloadStatsPath))
			{
				try
				{
					File.Delete(RuntimeAssetDownloader.DownloadStatsPath);
					this.m_tagDownloads.m_items.Clear();
					this.m_tagLocations.Clear();
				}
				catch (Exception ex)
				{
					global::Error.AddDevFatal("Failed to delete the stats file({0}): {1}", new object[]
					{
						RuntimeAssetDownloader.DownloadStatsPath,
						ex.Message
					});
				}
			}
		}

		// Token: 0x0600B7AA RID: 47018 RVA: 0x00382C34 File Offset: 0x00380E34
		private void AgentInitializeWhenReady()
		{
			if (!AndroidDeviceSettings.Get().m_determineSDCard || this.m_agentInit)
			{
				return;
			}
			this.m_tagIndicatorManager.Initialize(this.InstallDataPath);
			VersionConfigurationService versionConfigurationService = HearthstoneServices.Get<VersionConfigurationService>();
			if (versionConfigurationService.IsPipelinePortNeeded() && versionConfigurationService.IsRequestingData)
			{
				return;
			}
			AndroidDeviceSettings.Get().DeleteOldNotificationChannels();
			global::Log.Downloader.PrintInfo("Set listeners for Agent", Array.Empty<object>());
			this.m_callbackDisposer = AgentEmbeddedAPI.Subscribe(this);
			string clientToken = versionConfigurationService.GetClientToken();
			if (!string.IsNullOrEmpty(clientToken))
			{
				global::Log.Downloader.PrintInfo("Token is specified: {0}", new object[]
				{
					clientToken
				});
			}
			string ngdpregion = this.GetNGDPRegion();
			this.m_agentState = RuntimeAssetDownloader.AgentState.WAIT_SERVICE;
			global::Log.Downloader.PrintInfo("initialization: " + RuntimeAssetDownloader.INSTALL_PATH + ", region: " + ngdpregion, Array.Empty<object>());
			if (!AgentEmbeddedAPI.Initialize(RuntimeAssetDownloader.INSTALL_PATH, Path.Combine(global::Logger.LogsPath, "AgentLogs"), clientToken, ngdpregion))
			{
				global::Error.AddDevFatal("Failed to initialize Agent service", Array.Empty<object>());
				this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
				this.State = AssetDownloaderState.ERROR;
				return;
			}
			this.m_bundleDataList = new RuntimeAssetDownloader.BundleDataList(this.InstallDataPath);
			this.m_agentInit = true;
			AgentEmbeddedAPI.SetTelemetry(true);
			this.DeserializeDownloadStats();
			this.StartVersion();
		}

		// Token: 0x0600B7AB RID: 47019 RVA: 0x00382D68 File Offset: 0x00380F68
		private bool ProcessMobile()
		{
			if (!this.IsEnabledDownload)
			{
				return false;
			}
			this.m_agentStatus = (this.m_agentInit ? AgentEmbeddedAPI.GetStatus() : null);
			if (this.m_agentState == RuntimeAssetDownloader.AgentState.ERROR && (this.m_agentStatus == null || !this.m_agentStatus.m_cachedState.m_baseState.m_playable))
			{
				this.State = AssetDownloaderState.ERROR;
				if (!this.m_optionalDownload)
				{
					StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get(this.m_errorMsg), GameStrings.Get("GLOBAL_QUIT"), delegate()
					{
						this.ShutdownApplication();
					});
				}
				return true;
			}
			if (!this.m_agentInit || this.m_agentState == RuntimeAssetDownloader.AgentState.NONE)
			{
				return false;
			}
			if (this.m_agentStatus == null)
			{
				return false;
			}
			if (this.IsWaitingAction())
			{
				global::Log.Downloader.PrintInfo("Waiting message!!!" + this.m_agentState.ToString(), Array.Empty<object>());
				return false;
			}
			if (this.m_agentState == RuntimeAssetDownloader.AgentState.AWAITING_WIFI)
			{
				if (this.canDownload)
				{
					if (this.m_cancelledByUser)
					{
						if (!this.m_showResumeDialog)
						{
							this.ShowResumeMessage();
							this.m_showResumeDialog = true;
						}
					}
					else
					{
						global::Log.Downloader.PrintInfo("Download start again with " + this.m_savedAgentState.ToString(), Array.Empty<object>());
						StartupDialog.Destroy();
						this.DoUpdate(this.UpdateOp[this.m_savedAgentState]);
					}
				}
				else if (this.m_redrawDialog)
				{
					this.ShowNetworkDialog();
					this.m_redrawDialog = false;
				}
				return false;
			}
			if (!this.IsUpdating())
			{
				global::Log.Downloader.PrintWarning("Not updating message!!!" + this.m_agentState.ToString(), Array.Empty<object>());
				return true;
			}
			RuntimeAssetDownloader.AgentInternalState agentState;
			int num;
			if (this.m_agentState == RuntimeAssetDownloader.AgentState.VERSION)
			{
				agentState = (RuntimeAssetDownloader.AgentInternalState)this.m_agentStatus.m_cachedState.m_versionProgress.m_agentState;
				num = this.m_agentStatus.m_cachedState.m_versionProgress.m_error;
			}
			else
			{
				agentState = (RuntimeAssetDownloader.AgentInternalState)this.m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_agentState;
				num = this.m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error;
				if (agentState == RuntimeAssetDownloader.AgentInternalState.STATE_UPDATING || agentState == RuntimeAssetDownloader.AgentInternalState.STATE_READY || agentState == RuntimeAssetDownloader.AgentInternalState.STATE_FINISHED)
				{
					this.UpdateDownloadedBytes();
				}
			}
			if (agentState <= RuntimeAssetDownloader.AgentInternalState.STATE_FINISHED)
			{
				if (agentState != RuntimeAssetDownloader.AgentInternalState.STATE_UPDATING)
				{
					if (agentState == RuntimeAssetDownloader.AgentInternalState.STATE_READY || agentState == RuntimeAssetDownloader.AgentInternalState.STATE_FINISHED)
					{
						global::Log.Downloader.PrintInfo("Done!!! state=" + agentState, Array.Empty<object>());
						if (this.m_agentState == RuntimeAssetDownloader.AgentState.VERSION)
						{
							this.ProcessInBlobGameSettings();
							string currentVersionStr = this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr;
							string liveDisplayVersion = this.m_agentStatus.m_configuration.m_liveDisplayVersion;
							TelemetryManager.Client().SendVersionFinished(currentVersionStr, liveDisplayVersion);
							global::Log.Downloader.PrintInfo("Current version: " + currentVersionStr + ", Live version: " + liveDisplayVersion, Array.Empty<object>());
							bool flag;
							if (liveDisplayVersion.IndexOf('.') == -1)
							{
								this.VersionFetchFailed();
							}
							else if (this.ShouldCheckBinaryUpdate(currentVersionStr, liveDisplayVersion, out flag))
							{
								this.IsVersionChanged = true;
								this.ResetToUpdate();
								if (PlatformSettings.RuntimeOS == OSCategory.Android)
								{
									if (!AndroidDeviceSettings.Get().AllowUnknownApps())
									{
										if (this.AskUnkownApps)
										{
											this.UpdateState = "UnknownApps";
											this.m_agentState = RuntimeAssetDownloader.AgentState.UNKNOWN_APPS;
											AndroidDeviceSettings.Get().TriggerUnknownSources("MobileCallbackManager.UnknownSourcesListener");
										}
										else
										{
											this.StartApkDownload();
										}
									}
									else
									{
										this.m_allowUnknownApps = true;
										this.StartApkDownload();
									}
								}
								else if (flag)
								{
									this.GoToIdleState(true);
								}
								else
								{
									this.OpenAppStore();
								}
							}
							else if (this.m_agentState != RuntimeAssetDownloader.AgentState.ERROR)
							{
								this.GoToIdleState(true);
							}
						}
						else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_APK)
						{
							global::Log.Downloader.PrintInfo("UPDATE_APK Done!!!", Array.Empty<object>());
							this.m_deviceSpace = FreeSpace.Measure();
							TelemetryManager.Client().SendUpdateFinished(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)this.m_deviceSpace / 1048576f, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
							this.TryToInstallAPK();
						}
						else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_MANIFEST)
						{
							this.GoToIdleState(true);
						}
						else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_GLOBAL)
						{
							global::Log.Downloader.PrintInfo("UPDATE_GLOBAL Done!!!", Array.Empty<object>());
							TelemetryManager.Client().SendDataUpdateFinished(this.ElapsedTimeFromStart(this.m_updateStartTime), this.DownloadedBytes, this.TotalBytes);
							this.UpdateState = "Updated";
							this.GoToIdleState(true);
						}
						else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_OPTIONAL)
						{
							global::Log.Downloader.PrintInfo("UPDATE_OPTIONAL Done!!!", Array.Empty<object>());
							TelemetryManager.Client().SendRuntimeUpdate(this.ElapsedTimeFromStart(this.m_updateStartTime), RuntimeUpdate.Intention.DONE);
							this.GoToIdleState(true);
							this.DownloadAllFinished = true;
							this.PrintDataList();
						}
						else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_LOCALIZED)
						{
							global::Log.Downloader.PrintInfo("UPDATE_LOCALIZED Done!!!", Array.Empty<object>());
							TelemetryManager.Client().SendLocaleDataUpdateFinished(this.ElapsedTimeFromStart(this.m_updateStartTime), this.DownloadedBytes, this.TotalBytes);
							this.UpdateState = "Updated";
							this.GoToIdleState(true);
						}
						else
						{
							global::Log.Downloader.PrintError("State error: " + this.m_agentState, Array.Empty<object>());
							this.m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_UPDATE";
							this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
						}
					}
				}
				else
				{
					this.m_tagIndicatorManager.Check();
					if (this.m_agentState != RuntimeAssetDownloader.AgentState.VERSION)
					{
						if (!this.IsAssetManifestReady && this.FindDownloadStatus(RuntimeAssetDownloader.ASSET_MANIFEST_TAGS).Complete)
						{
							global::Log.Downloader.PrintInfo("Asset manifest is ready!", Array.Empty<object>());
							this.SetAssetManifestReady();
						}
						global::Log.Downloader.PrintInfo("Downloading: {0} / {1}", new object[]
						{
							this.DownloadedBytes,
							this.TotalBytes
						});
						this.State = AssetDownloaderState.DOWNLOADING;
						if (this.m_cancelledByUser)
						{
							this.m_cancelledByUser = false;
							StartupDialog.Destroy();
							if (!this.canDownload && this.m_optionalDownload)
							{
								global::Log.Downloader.PrintInfo("Agent Service has been resumed.", Array.Empty<object>());
								DownloadPermissionManager.DownloadEnabled = true;
							}
						}
						else if (!this.canDownload)
						{
							global::Log.Downloader.PrintInfo("Circumstances have changed.  Stopping download.", Array.Empty<object>());
							this.StopDownloading();
						}
						if (this.ElapsedTimeFromStart(this.m_lastUpdateProgressReportTime) > 15f)
						{
							if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_APK)
							{
								TelemetryManager.Client().SendUpdateProgress(this.ElapsedTimeFromStart(this.m_apkUpdateStartTime), this.DownloadedBytes, this.TotalBytes);
								this.m_lastUpdateProgressReportTime = Time.realtimeSinceStartup;
							}
							else if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_GLOBAL)
							{
								TelemetryManager.Client().SendDataUpdateProgress(this.ElapsedTimeFromStart(this.m_updateStartTime), this.DownloadedBytes, this.TotalBytes);
								this.m_lastUpdateProgressReportTime = Time.realtimeSinceStartup;
							}
						}
					}
				}
			}
			else if (agentState != RuntimeAssetDownloader.AgentInternalState.STATE_CANCELED)
			{
				if (agentState != RuntimeAssetDownloader.AgentInternalState.STATE_IMPEDED)
				{
					if (agentState == RuntimeAssetDownloader.AgentInternalState.STATE_ERROR_START)
					{
						if (this.m_agentState == RuntimeAssetDownloader.AgentState.VERSION)
						{
							if (this.UpdateState == "Updated")
							{
								global::Log.Downloader.PrintInfo("Version failure but allow to play", Array.Empty<object>());
								this.GoToIdleState(true);
							}
							else
							{
								this.VersionFetchFailed();
							}
						}
						else if (num != this.m_internalAgentError)
						{
							CachedState cachedState = this.m_agentStatus.m_cachedState;
							if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_GLOBAL)
							{
								TelemetryManager.Client().SendDataUpdateFailed(this.ElapsedTimeFromStart(this.m_updateStartTime), this.DownloadedBytes, this.TotalBytes, num);
							}
							if (this.m_agentState == RuntimeAssetDownloader.AgentState.UPDATE_LOCALIZED)
							{
								TelemetryManager.Client().SendLocaleDataUpdateFailed(this.ElapsedTimeFromStart(this.m_updateStartTime), this.DownloadedBytes, this.TotalBytes, num);
							}
							this.m_deviceSpace = FreeSpace.Measure();
							global::Log.Downloader.PrintInfo("Measured free space: {0}", new object[]
							{
								this.m_deviceSpace
							});
							if (this.m_deviceSpace < this.RequiredBytes - this.DownloadedBytes)
							{
								global::Log.Downloader.PrintWarning("Agent might be failed because of the space problem.", Array.Empty<object>());
								num = 2101;
							}
							if (num == 801 || num == 2101)
							{
								if (this.RestartOnFalseHDDFullEnabled && this.m_deviceSpace > this.RequiredBytes - this.DownloadedBytes)
								{
									if (!this.m_retriedUpdateWithNoSpaceError)
									{
										this.DoUpdate(this.UpdateOp[this.m_agentState]);
										this.m_retriedUpdateWithNoSpaceError = true;
										global::Log.Downloader.PrintWarning("Received a false 'out of space' error. Retrying.", Array.Empty<object>());
									}
									else
									{
										global::Log.Downloader.PrintError("Received a false 'out of space' error again. Stop.", Array.Empty<object>());
										this.StopDownloading();
									}
								}
								else
								{
									global::Log.Downloader.PrintWarning("Out of space!", Array.Empty<object>());
									this.blockedByDiskFull = true;
									this.StopDownloading();
								}
							}
							else
							{
								global::Error.AddDevFatal("Unidentified error Error={0}", new object[]
								{
									num
								});
								if (!this.canDownload)
								{
									global::Log.Downloader.PrintError("failed to download.  Stopping download.", Array.Empty<object>());
								}
								else
								{
									this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
									this.m_errorMsg = this.GetAgentErrorMessage(num);
								}
								this.StopDownloading();
							}
						}
					}
				}
				else
				{
					this.State = AssetDownloaderState.AGENT_IMPEDED;
					global::Log.Downloader.PrintInfo("Impeded!!!", Array.Empty<object>());
				}
			}
			else if (this.m_pausedByNetwork && !this.canDownload)
			{
				global::Log.Downloader.PrintInfo("Circumstances have changed from Agent.  Stopping download.", Array.Empty<object>());
				this.StopDownloading();
			}
			else if (!this.m_cancelledByUser)
			{
				global::Log.Downloader.PrintInfo("Canceled!!!", Array.Empty<object>());
				this.m_prevProgress = this.ProgressPercent;
				this.m_savedAgentState = this.m_agentState;
				this.m_cancelledByUser = true;
				this.ShowResumeMessage();
			}
			this.m_internalAgentError = num;
			return false;
		}

		// Token: 0x0600B7AC RID: 47020 RVA: 0x00383715 File Offset: 0x00381915
		private void SetAssetManifestReady()
		{
			this.IsAssetManifestReady = true;
			this.m_tagIndicatorManager.Check();
		}

		// Token: 0x0600B7AD RID: 47021 RVA: 0x0038372C File Offset: 0x0038192C
		private bool ShouldCheckBinaryUpdate(string curVer, string liveVer, out bool hasNewBinary)
		{
			hasNewBinary = false;
			if (this.UpdateState == "UpdateAPK")
			{
				return true;
			}
			int installedVersionCode = this.GetInstalledVersionCode();
			int[] array = new int[]
			{
				20,
				4,
				0,
				installedVersionCode
			};
			if (string.IsNullOrEmpty(curVer))
			{
				try
				{
					int[] version;
					if (installedVersionCode > 0 && this.GetSplitVersion(liveVer, out version))
					{
						int num = this.CompareVersions(version, array, false);
						if (num < 0)
						{
							global::Log.Downloader.PrintError("The binary is newer than the live version: liveVer:{0} binaryVer:{1}", new object[]
							{
								liveVer,
								string.Concat(new object[]
								{
									array[0],
									".",
									array[1],
									".?.",
									array[3]
								})
							});
							if (this.VersionMismatchErrorEnabled)
							{
								this.m_errorMsg = "GLUE_LOADINGSCREEN_MISMATCHED_VERSION";
								this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
							}
							return false;
						}
						if (num > 0)
						{
							global::Log.Downloader.PrintInfo("The binary version is older than the live version: liveVer:{0} binaryVer:{1}", new object[]
							{
								liveVer,
								string.Concat(new object[]
								{
									array[0],
									".",
									array[1],
									".?.",
									array[3]
								})
							});
							return true;
						}
					}
				}
				catch (Exception ex)
				{
					global::Error.AddDevFatal("Failed to check the binary version with curVer:{0} liveVer:{1}: {2}", new object[]
					{
						curVer,
						liveVer,
						ex.Message
					});
				}
				this.UpdateState = "Update";
				return false;
			}
			if (curVer != liveVer)
			{
				global::Log.Downloader.PrintInfo("New version is detected", Array.Empty<object>());
				if (!this.NeedBinaryUpdate(false))
				{
					global::Log.Downloader.PrintInfo("It's new version already. {0}", new object[]
					{
						installedVersionCode
					});
					hasNewBinary = true;
				}
				return true;
			}
			int[] version2;
			if (this.GetSplitVersion(curVer, out version2) && this.CompareVersions(version2, array, true) > 0)
			{
				global::Log.Downloader.PrintError("Agent already has the new version strangely, let's try to update the binary.", Array.Empty<object>());
				return true;
			}
			return false;
		}

		// Token: 0x0600B7AE RID: 47022 RVA: 0x00383944 File Offset: 0x00381B44
		private void ResetToUpdate()
		{
			this.m_tagIndicatorManager.ClearAllIndicators();
			this.DeleteDownloadStats();
			this.DownloadAllFinished = false;
			this.IsAssetManifestReady = false;
			this.UpdateState = "Update";
			this.ClearDataList();
		}

		// Token: 0x0600B7AF RID: 47023 RVA: 0x00383978 File Offset: 0x00381B78
		protected int CompareVersions(int[] version1, int[] version2, bool compareMajorMinorOnly = false)
		{
			try
			{
				int num = version1[0] - version2[0];
				int num2 = version1[1] - version2[1];
				int num3 = this.GetBinaryVersionCode(version1) - this.GetBinaryVersionCode(version2);
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
				global::Error.AddDevFatal("Failed to compare two version array: {0}", new object[]
				{
					ex.Message
				});
			}
			return 0;
		}

		// Token: 0x0600B7B0 RID: 47024 RVA: 0x00383A08 File Offset: 0x00381C08
		private void FilterForiCloud()
		{
			if (PlatformSettings.RuntimeOS != OSCategory.iOS)
			{
				return;
			}
			string[] array = new string[]
			{
				LocalOptions.OptionsPath,
				global::Log.ConfigPath,
				global::Logger.LogsPath,
				FileUtils.CachePath,
				RuntimeAssetDownloader.INSTALL_PATH + "/Unity"
			};
			string[] array2 = new string[array.Length + this.m_dataFolders.Length];
			array.CopyTo(array2, 0);
			this.m_dataFolders.CopyTo(array2, array.Length);
			foreach (string text in array2)
			{
				if (File.Exists(text) && !UpdateUtils.addSkipBackupAttributeToItemAtPath(text))
				{
					global::Log.Downloader.PrintError("Failed to exclude from iCloud - " + text, Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600B7B1 RID: 47025 RVA: 0x00383AC4 File Offset: 0x00381CC4
		private bool SetInitialState()
		{
			this.PatchOverrideUrl = "Live";
			this.VersionOverrideUrl = "Live";
			this.m_store = AndroidDeviceSettings.Get().m_HSStore;
			if (RuntimeAssetDownloader.IsEnabledUpdate)
			{
				GameStrings.LoadNative();
				this.SanitizeDataFolder();
				AndroidDeviceSettings.Get().AskForSDCard();
			}
			else
			{
				if (Application.isEditor && PlatformSettings.IsEmulating)
				{
					GameStrings.LoadNative();
					this.m_agentState = RuntimeAssetDownloader.AgentState.VERSION;
					this.m_versionCalled = true;
					return true;
				}
				this.m_agentState = RuntimeAssetDownloader.AgentState.NONE;
			}
			return true;
		}

		// Token: 0x0600B7B2 RID: 47026 RVA: 0x00383B40 File Offset: 0x00381D40
		private void SanitizeDataFolder()
		{
			global::Log.Downloader.PrintInfo("SanitizeDataFolder", Array.Empty<object>());
			try
			{
				if (RuntimeAssetDownloader.INSTALL_PATH != null)
				{
					this.ClearAPKIndicator();
					string text = Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "Data");
					if (Directory.Exists(text))
					{
						UpdateUtils.CleanupOldAssetBundles(this.InstallDataPath);
						int num = 0;
						string[] array = new string[]
						{
							"atc",
							"astc",
							"dxt",
							"pvrtc",
							"etc1"
						};
						string[] array2 = Directory.GetDirectories(text);
						for (int i = 0; i < array2.Length; i++)
						{
							string path = array2[i];
							string foundTexture = Path.GetFileName(path);
							if (Array.Exists<string>(array, (string s) => s == foundTexture))
							{
								num++;
								global::Log.Downloader.PrintInfo("textureFormatFolder: " + foundTexture, Array.Empty<object>());
							}
						}
						if (num > 1)
						{
							global::Log.Downloader.PrintInfo("Removing TACT install folder...", Array.Empty<object>());
							Directory.Delete(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "__agent__"), true);
							Directory.Delete(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "Strings"), true);
							Directory.Delete(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "Data"), true);
							Directory.Delete(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "apk"), true);
						}
						else
						{
							global::Log.Downloader.PrintInfo("Trying to clean Data folder", Array.Empty<object>());
							foreach (string text2 in array)
							{
								string path2 = Path.Combine(text, text2);
								if (text2 != AndroidDeviceSettings.Get().InstalledTexture && Directory.Exists(path2))
								{
									global::Log.Downloader.PrintInfo("Delete the unsued texture folder - {0}", new object[]
									{
										text2
									});
									Directory.Delete(path2, true);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				global::Error.AddDevFatal("Failed to sanitize Data folder: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600B7B3 RID: 47027 RVA: 0x00383D5C File Offset: 0x00381F5C
		private bool IsWaitingAction()
		{
			return this.m_agentState == RuntimeAssetDownloader.AgentState.UNKNOWN_APPS || this.m_agentState == RuntimeAssetDownloader.AgentState.WAIT_SERVICE || this.m_agentState == RuntimeAssetDownloader.AgentState.OPEN_APP_STORE;
		}

		// Token: 0x0600B7B4 RID: 47028 RVA: 0x00383D7B File Offset: 0x00381F7B
		private bool IsUpdating()
		{
			return this.m_agentState != RuntimeAssetDownloader.AgentState.NONE && this.m_agentState != RuntimeAssetDownloader.AgentState.ERROR;
		}

		// Token: 0x0600B7B5 RID: 47029 RVA: 0x00383D93 File Offset: 0x00381F93
		private void GoToIdleState(bool assetManifestReady)
		{
			this.m_agentState = RuntimeAssetDownloader.AgentState.NONE;
			this.State = AssetDownloaderState.IDLE;
			this.IsReady = true;
			if (assetManifestReady)
			{
				this.SetAssetManifestReady();
			}
		}

		// Token: 0x0600B7B6 RID: 47030 RVA: 0x00383DB4 File Offset: 0x00381FB4
		private void ClearAPKIndicator()
		{
			string text = Path.Combine(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "apk"), "tag_apk");
			try
			{
				if (File.Exists(text))
				{
					global::Log.Downloader.PrintWarning("Deleted the APK indicator '{0}'", new object[]
					{
						text
					});
					File.Delete(text);
				}
			}
			catch (Exception ex)
			{
				global::Error.AddDevFatal("Failed to delete the APK indicator '{0}': {1}", new object[]
				{
					text,
					ex.Message
				});
			}
		}

		// Token: 0x0600B7B7 RID: 47031 RVA: 0x00383E34 File Offset: 0x00382034
		private string GetAdditionalTags(RuntimeAssetDownloader.KindOfUpdate kind)
		{
			if (kind == RuntimeAssetDownloader.KindOfUpdate.VERSION)
			{
				return "";
			}
			if (kind != RuntimeAssetDownloader.KindOfUpdate.APK_UPDATE)
			{
				string text = "";
				foreach (string arg in this.m_curDownloadStatus.Tags)
				{
					text += string.Format(" {0}?", arg);
				}
				if (PlatformSettings.RuntimeOS == OSCategory.Android)
				{
					text += string.Format(" {0}?", AndroidDeviceSettings.Get().InstalledTexture);
				}
				else if (PlatformSettings.RuntimeOS == OSCategory.iOS && Localization.GetLocale().ToString() == "zhCN" && PlatformSettings.LocaleVariant != LocaleVariant.China)
				{
					text += " GlobalCN?";
				}
				return text;
			}
			if (!(this.m_store == "CN") || !this.m_allowUnknownApps)
			{
				return "";
			}
			return string.Format("{0}? {1}?", this.m_store, this.MobileMode);
		}

		// Token: 0x0600B7B8 RID: 47032 RVA: 0x00383F20 File Offset: 0x00382120
		private int DoUpdate(RuntimeAssetDownloader.KindOfUpdate kind)
		{
			this.m_showResumeDialog = false;
			int num = (kind == RuntimeAssetDownloader.KindOfUpdate.VERSION) ? this.AgentStartVersion() : this.AgentStartUpdateGlobal(kind);
			if (num != 0)
			{
				global::Error.AddDevFatal("DoUpdate({0}) Error={1}", new object[]
				{
					kind.ToString(),
					num
				});
				this.m_errorMsg = ((num == 2410) ? "GLUE_LOADINGSCREEN_ERROR_UPDATE_CONFLICT" : "GLUE_LOADINGSCREEN_ERROR_UPDATE");
				this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
				return num;
			}
			if (!this.m_optionalDownload)
			{
				this.UpdateState = "Update";
			}
			switch (kind)
			{
			case RuntimeAssetDownloader.KindOfUpdate.VERSION:
				this.m_agentState = RuntimeAssetDownloader.AgentState.VERSION;
				this.UpdateState = "Version";
				break;
			case RuntimeAssetDownloader.KindOfUpdate.APK_UPDATE:
				this.m_agentState = RuntimeAssetDownloader.AgentState.UPDATE_APK;
				this.UpdateState = "UpdateAPK";
				this.m_apkUpdateStartTime = Time.realtimeSinceStartup;
				this.m_lastUpdateProgressReportTime = this.m_apkUpdateStartTime;
				this.m_deviceSpace = FreeSpace.Measure();
				TelemetryManager.Client().SendUpdateStarted(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, AndroidDeviceSettings.Get().InstalledTexture, this.InstallDataPath, (float)this.m_deviceSpace / 1048576f);
				break;
			case RuntimeAssetDownloader.KindOfUpdate.MANIFEST_UPDATE:
				this.m_agentState = RuntimeAssetDownloader.AgentState.UPDATE_MANIFEST;
				break;
			case RuntimeAssetDownloader.KindOfUpdate.GLOBAL_UPDATE:
				this.m_agentState = RuntimeAssetDownloader.AgentState.UPDATE_GLOBAL;
				this.m_updateStartTime = Time.realtimeSinceStartup;
				this.m_lastUpdateProgressReportTime = this.m_updateStartTime;
				break;
			case RuntimeAssetDownloader.KindOfUpdate.OPTIONAL_UPDATE:
				this.m_agentState = RuntimeAssetDownloader.AgentState.UPDATE_OPTIONAL;
				this.m_updateStartTime = Time.realtimeSinceStartup;
				break;
			case RuntimeAssetDownloader.KindOfUpdate.LOCALE_UPDATE:
				this.m_agentState = RuntimeAssetDownloader.AgentState.UPDATE_LOCALIZED;
				this.m_updateStartTime = Time.realtimeSinceStartup;
				break;
			}
			return num;
		}

		// Token: 0x0600B7B9 RID: 47033 RVA: 0x003840A8 File Offset: 0x003822A8
		private int AgentStartUpdateGlobal(RuntimeAssetDownloader.KindOfUpdate kind)
		{
			UserSettings userSettings = new UserSettings
			{
				m_region = this.GetNGDPRegion(),
				m_languages = Localization.GetLocale().ToString(),
				m_branch = this.GetBranch(),
				m_additionalTags = this.GetAdditionalTags(kind)
			};
			global::Log.Downloader.PrintInfo("ModifyProductInstall with locale: {0} and tags: {1} ", new object[]
			{
				userSettings.m_languages,
				userSettings.m_additionalTags
			});
			int num = AgentEmbeddedAPI.ModifyProductInstall(ref userSettings);
			if (num != 0)
			{
				global::Log.Downloader.PrintWarning("1st ModifyProductInstall Error={0}", new object[]
				{
					num
				});
				if (num != 2410)
				{
					return num;
				}
				AgentEmbeddedAPI.CancelAllOperations();
				num = AgentEmbeddedAPI.ModifyProductInstall(ref userSettings);
				if (num != 0)
				{
					global::Error.AddDevFatal("2nd ModifyProductInstall Error={0}", new object[]
					{
						num
					});
					return num;
				}
			}
			if (this.m_curDownloadStatus != null)
			{
				this.m_curDownloadStatus.StartProgress = this.m_curDownloadStatus.Progress;
			}
			this.TotalBytes = 0L;
			this.DownloadedBytes = 0L;
			this.RequiredBytes = 0L;
			this.BytesPerSecond = 0.0;
			this.m_deviceSpace = -1L;
			this.m_cancelledByUser = false;
			NotificationUpdateSettings settings = new NotificationUpdateSettings
			{
				m_cellDataThreshold = (long)Options.Get().GetInt(Option.CELL_PROMPT_THRESHOLD),
				m_isCellDataAllowed = this.m_cellularEnabledSession
			};
			num = AgentEmbeddedAPI.StartUpdate("", settings);
			this.ResetDownloadSpeed(this.MaxDownloadSpeed);
			return num;
		}

		// Token: 0x0600B7BA RID: 47034 RVA: 0x0038421C File Offset: 0x0038241C
		private int ResetDownloadSpeed(int speed)
		{
			global::Log.Downloader.PrintInfo("Set the download speed to {0}", new object[]
			{
				speed
			});
			int num = AgentEmbeddedAPI.SetUpdateParams(string.Format("download_limit={0}", speed));
			if (num != 0)
			{
				global::Log.Downloader.PrintError("SetUpdateParams Error={0}", new object[]
				{
					num
				});
			}
			return num;
		}

		// Token: 0x0600B7BB RID: 47035 RVA: 0x00384280 File Offset: 0x00382480
		private void UpdateDownloadedBytes()
		{
			this.TotalBytes = this.m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_expectedDownloadBytes;
			this.DownloadedBytes = this.m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_realDownloadedBytes;
			this.RequiredBytes = this.m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_expectedOriginalBytes;
			this.BytesPerSecond = this.m_agentStatus.m_cachedState.m_updateProgress.m_downloadDetails.m_downloadRate;
			this.m_progress = (float)this.m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_progress;
			this.PrintStatus(this.m_agentStatus);
			this.m_curDownloadStatus.BytesDownloaded = this.DownloadedBytes;
			this.m_curDownloadStatus.BytesTotal = this.TotalBytes;
			this.m_curDownloadStatus.Progress = this.m_progress;
			if (this.TotalBytes > 0L && this.m_deviceSpace == -1L)
			{
				global::Log.Downloader.PrintInfo("Total: {0}, Downloaded: {1}, Required: {2}, Speed: {3}", new object[]
				{
					this.TotalBytes,
					this.DownloadedBytes,
					this.RequiredBytes,
					this.BytesPerSecond
				});
				this.m_deviceSpace = FreeSpace.Measure();
				global::Log.Downloader.PrintInfo("Measured free space: {0}", new object[]
				{
					this.m_deviceSpace
				});
				if (this.RequiredBytes > this.m_deviceSpace)
				{
					global::Log.Downloader.PrintError("Device will run out of space during download.  {0} / {1}", new object[]
					{
						this.RequiredBytes,
						this.m_deviceSpace
					});
					this.blockedByDiskFull = true;
					this.StopDownloading();
				}
			}
			this.SerializeDownloadStats();
		}

		// Token: 0x0600B7BC RID: 47036 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void PrintStatus(ProductStatus agentStatus)
		{
		}

		// Token: 0x0600B7BD RID: 47037 RVA: 0x00384454 File Offset: 0x00382654
		protected bool GetSplitVersion(string versionStr, out int[] versionInt)
		{
			global::Log.Downloader.PrintInfo("VersionStr=" + versionStr, Array.Empty<object>());
			try
			{
				List<string> list = new List<string>();
				string[] array = versionStr.Split(new char[]
				{
					'_'
				});
				int num = 4;
				if (array.Length == 1)
				{
					list.AddRange(versionStr.Split(new char[]
					{
						'.'
					}));
				}
				else
				{
					string str = Vars.Key("Mobile.BinaryVersion").GetStr("");
					string oldValue = string.Empty;
					if (!string.IsNullOrEmpty(str))
					{
						oldValue = "." + str;
						array[1] = array[1].Replace(oldValue, "");
					}
					list.AddRange(array[1].Split(new char[]
					{
						'-'
					})[0].Split(new char[]
					{
						'.'
					}));
					list.Add(array[0]);
					if (!string.IsNullOrEmpty(str))
					{
						list.Add(str);
						num++;
					}
				}
				versionInt = Array.ConvertAll<string, int>(list.ToArray(), new Converter<string, int>(int.Parse));
				if (versionInt.Length < num)
				{
					throw new Exception("Version is too short");
				}
			}
			catch (Exception ex)
			{
				global::Error.AddDevFatal("Failed to parse the version string-'{0}': {1}", new object[]
				{
					versionStr,
					ex.Message
				});
				versionInt = new int[0];
				return false;
			}
			return true;
		}

		// Token: 0x0600B7BE RID: 47038 RVA: 0x003845B0 File Offset: 0x003827B0
		private void VersionFetchFailed()
		{
			global::Error.AddDevFatal("Agent: Failed to get Version! - {0}", new object[]
			{
				this.m_agentStatus.m_cachedState.m_versionProgress.m_error
			});
			this.m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_VERSION";
			this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
			TelemetryManager.Client().SendVersionError((uint)this.m_agentStatus.m_cachedState.m_versionProgress.m_error, (uint)this.m_agentStatus.m_cachedState.m_versionProgress.m_agentState, this.m_agentStatus.m_settings.m_languages, this.m_agentStatus.m_settings.m_region, this.m_agentStatus.m_settings.m_branch, this.m_agentStatus.m_settings.m_additionalTags);
		}

		// Token: 0x0600B7BF RID: 47039 RVA: 0x00384670 File Offset: 0x00382870
		private void ProcessInBlobGameSettings()
		{
			global::Log.Downloader.PrintInfo("Processing blob data...", Array.Empty<object>());
			string opaqueString = AgentEmbeddedAPI.GetOpaqueString("disabled_apk_update");
			if (!string.IsNullOrEmpty(opaqueString))
			{
				global::Log.Downloader.PrintInfo("Blob - disabled_apk_update: " + opaqueString, Array.Empty<object>());
				try
				{
					this.m_disabledAPKUpdates = Array.ConvertAll<string, int>(opaqueString.Split(new char[]
					{
						','
					}), (string s) => int.Parse(s.Trim()));
				}
				catch (Exception ex)
				{
					global::Log.Downloader.PrintError("Failed to parse the 'disabled_apk_update' - {0}: {1}", new object[]
					{
						opaqueString,
						ex.Message
					});
				}
			}
			this.InGameStreamingDefaultSpeed = -1;
			string opaqueString2 = AgentEmbeddedAPI.GetOpaqueString("disabled_adventures_for_streaming");
			if (!string.IsNullOrEmpty(opaqueString2))
			{
				global::Log.Downloader.PrintInfo("Blob - disabled_adventure_streaming: " + opaqueString2, Array.Empty<object>());
				this.m_disabledAdventuresForStreaming = opaqueString2.Split(new char[]
				{
					','
				});
			}
			string opaqueString3 = AgentEmbeddedAPI.GetOpaqueString("anr_throttle");
			if (!string.IsNullOrEmpty(opaqueString3))
			{
				global::Log.Downloader.PrintInfo("Blob - anr_throttle: " + opaqueString3, Array.Empty<object>());
				string[] array = opaqueString3.Split(new char[]
				{
					','
				});
				float num = Options.Get().GetFloat(Option.ANR_THROTTLE);
				float num2;
				if (float.TryParse(array[0], out num2) && num2 != num)
				{
					num = num2;
					Options.Get().SetFloat(Option.ANR_THROTTLE, num);
				}
				float num3 = Options.Get().GetFloat(Option.ANR_WAIT_SECONDS);
				float num4;
				if (array.Length > 1 && float.TryParse(array[1], out num4) && num4 != num3)
				{
					num3 = num4;
					Options.Get().SetFloat(Option.ANR_WAIT_SECONDS, num3);
				}
			}
			bool flag = GeneralUtils.ForceBool(AgentEmbeddedAPI.GetOpaqueString("use_legacy_login"));
			if (Vars.Key("Debug.FakeBlobUseLegacyLogin").HasValue)
			{
				flag = Vars.Key("Debug.FakeBlobUseLegacyLogin").GetBool(false);
				global::Log.Login.PrintInfo("Has debug override for blog login: {0}", new object[]
				{
					flag
				});
			}
			if (flag)
			{
				Vars.Key("Mobile.UseMASDK").Set("false", false);
			}
		}

		// Token: 0x0600B7C0 RID: 47040 RVA: 0x00384898 File Offset: 0x00382A98
		private int InCreateProduct(Func<int> preFunc = null)
		{
			UserSettings userSettings = new UserSettings
			{
				m_region = this.GetNGDPRegion(),
				m_languages = Localization.GetLocale().ToString(),
				m_branch = this.GetBranch()
			};
			if (preFunc != null)
			{
				preFunc();
			}
			int num = AgentEmbeddedAPI.CreateProductInstall("hsb", ref userSettings);
			if (num == 0)
			{
				global::Log.Downloader.PrintInfo("Installation is done! Version...", Array.Empty<object>());
				this.m_agentState = RuntimeAssetDownloader.AgentState.INSTALLED;
			}
			else
			{
				global::Log.Downloader.PrintWarning("CreateProductInstall Error={0}", new object[]
				{
					num.ToString()
				});
			}
			return num;
		}

		// Token: 0x0600B7C1 RID: 47041 RVA: 0x0038493C File Offset: 0x00382B3C
		private void CreateProduct()
		{
			int num = this.InCreateProduct(null);
			if (num != 0)
			{
				if (num == 2410)
				{
					num = this.InCreateProduct(() => AgentEmbeddedAPI.StartUninstall(""));
				}
				if (num != 0)
				{
					this.m_errorMsg = this.GetAgentErrorMessage(num);
					this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
				}
			}
		}

		// Token: 0x0600B7C2 RID: 47042 RVA: 0x0038499C File Offset: 0x00382B9C
		private void StartVersion()
		{
			if (this.m_versionCalled)
			{
				return;
			}
			this.m_versionCalled = true;
			this.SetPatchUrl();
			this.SetVersionOverrideUrl();
			this.m_agentStatus = AgentEmbeddedAPI.GetStatus();
			if (this.m_agentStatus == null || string.IsNullOrEmpty(this.m_agentStatus.m_product))
			{
				global::Log.Downloader.PrintInfo("initialization succeeded! Create product...", Array.Empty<object>());
				this.CreateProduct();
			}
			else
			{
				string languages = this.m_agentStatus.m_settings.m_languages;
				global::Log.Downloader.PrintInfo("initialization succeeded! Ready to update locale=" + languages, Array.Empty<object>());
				this.m_agentState = RuntimeAssetDownloader.AgentState.INSTALLED;
			}
			if (this.m_agentState == RuntimeAssetDownloader.AgentState.INSTALLED)
			{
				global::Log.Downloader.PrintInfo("StartVersion", Array.Empty<object>());
				int num = this.AgentStartVersion();
				if (num != 0)
				{
					this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
					global::Error.AddDevFatal("[Downloader] StartVersion Error={0}", new object[]
					{
						num.ToString()
					});
					return;
				}
				this.m_agentState = RuntimeAssetDownloader.AgentState.VERSION;
			}
		}

		// Token: 0x0600B7C3 RID: 47043 RVA: 0x00384A88 File Offset: 0x00382C88
		private int AgentStartVersion()
		{
			TelemetryManager.Client().SendVersionStarted(0);
			return AgentEmbeddedAPI.StartVersion("");
		}

		// Token: 0x0600B7C4 RID: 47044 RVA: 0x00384AA0 File Offset: 0x00382CA0
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
					string url = string.Format("{0}.version.battle.net", this.GetNGDPRegion());
					string clientToken = versionConfigurationService.GetClientToken();
					global::Log.Downloader.PrintInfo("Setting version override to token for LIVE:{0}", new object[]
					{
						clientToken
					});
					AgentEmbeddedAPI.SetVersionServiceUrlOverride("hsb", url, clientToken);
					this.VersionOverrideUrl = pipeline.ToString();
				}
				return;
			}
			if (!versionConfigurationService.IsPortInformationAvailable())
			{
				global::Log.Downloader.PrintWarning("Port information not available for version override. Falling back to live", Array.Empty<object>());
				return;
			}
			if (!versionConfigurationService.IsPipelinePortNeeded())
			{
				global::Log.Downloader.PrintInfo("Not using version service override as pipline is {0}", new object[]
				{
					pipeline.ToString()
				});
				return;
			}
			int? pipelinePort = versionConfigurationService.GetPipelinePort();
			if (pipelinePort != null)
			{
				string text = string.Format("tcp://{0}:{1}", "hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net", pipelinePort);
				string clientToken2 = versionConfigurationService.GetClientToken();
				global::Log.Downloader.PrintInfo("Setting version override to token:{0}, url:{1}", new object[]
				{
					clientToken2,
					text
				});
				AgentEmbeddedAPI.SetVersionServiceUrlOverride("hsb", text, clientToken2);
				this.VersionOverrideUrl = pipeline.ToString();
				return;
			}
			global::Log.Downloader.PrintInfo("Couldn't get port for version service override port {0}, value was null", new object[]
			{
				pipeline.ToString()
			});
		}

		// Token: 0x0600B7C5 RID: 47045 RVA: 0x00384C04 File Offset: 0x00382E04
		private string GetNGDPRegion()
		{
			string result;
			switch (MobileDeviceLocale.GetCurrentRegionId())
			{
			case constants.BnetRegion.REGION_US:
				result = "us";
				break;
			case constants.BnetRegion.REGION_EU:
				result = "eu";
				break;
			case constants.BnetRegion.REGION_KR:
				result = "kr";
				break;
			case constants.BnetRegion.REGION_TW:
				result = "kr";
				break;
			case constants.BnetRegion.REGION_CN:
				result = "cn";
				break;
			default:
				result = "us";
				break;
			}
			return result;
		}

		// Token: 0x0600B7C6 RID: 47046 RVA: 0x00384C64 File Offset: 0x00382E64
		private string GetBranch()
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				return "android_" + this.m_store.ToLower();
			}
			if (PlatformSettings.RuntimeOS != OSCategory.iOS)
			{
				return string.Empty;
			}
			if (PlatformSettings.LocaleVariant == LocaleVariant.China)
			{
				return "ios_cn";
			}
			return "ios";
		}

		// Token: 0x0600B7C7 RID: 47047 RVA: 0x00384CB0 File Offset: 0x00382EB0
		private void ShutdownApplication()
		{
			global::Log.Downloader.PrintInfo("ShutdownApplication", Array.Empty<object>());
			this.Shutdown();
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.Exit();
				return;
			}
			GeneralUtils.ExitApplication();
		}

		// Token: 0x0600B7C8 RID: 47048 RVA: 0x00384CF2 File Offset: 0x00382EF2
		private void OpenAppStore()
		{
			this.m_agentState = RuntimeAssetDownloader.AgentState.OPEN_APP_STORE;
			this.OpenAppStoreAlert();
		}

		// Token: 0x0600B7C9 RID: 47049 RVA: 0x00384D04 File Offset: 0x00382F04
		private bool InstallAPK()
		{
			if (!this.NeedBinaryUpdate(true))
			{
				return false;
			}
			if (this.SendToAppStore())
			{
				this.OpenAppStore();
			}
			else
			{
				string text = Path.Combine(Path.Combine(RuntimeAssetDownloader.INSTALL_PATH, "apk"), string.Format("Hearthstone_{0}_{1}.apk", this.m_store, this.MobileMode));
				global::Log.Downloader.PrintInfo("ApkPath: " + text, Array.Empty<object>());
				if (AndroidDeviceSettings.Get().m_AndroidSDKVersion < 24)
				{
					string text2 = Path.Combine(FileUtils.BaseExternalDataPath, "Hearthstone.apk");
					global::Log.Downloader.PrintInfo("Copy apk: " + text + " -> " + text2, Array.Empty<object>());
					try
					{
						File.Delete(text2);
						File.Copy(text, text2);
						text = text2;
					}
					catch (Exception ex)
					{
						global::Error.AddDevFatal("Failed to copy APK, Open app store instead: {0}", new object[]
						{
							ex.Message
						});
						this.m_agentState = RuntimeAssetDownloader.AgentState.OPEN_APP_STORE;
						this.OpenAppStore();
						return true;
					}
				}
				AndroidDeviceSettings.Get().ProcessInstallAPK(text, "MobileCallbackManager.InstallAPKListener");
			}
			return true;
		}

		// Token: 0x0600B7CA RID: 47050 RVA: 0x00384E14 File Offset: 0x00383014
		private void TryToInstallAPK()
		{
			if (this.InstallAPK())
			{
				this.m_agentState = RuntimeAssetDownloader.AgentState.NONE;
				return;
			}
			this.GoToIdleState(false);
		}

		// Token: 0x0600B7CB RID: 47051 RVA: 0x00384E30 File Offset: 0x00383030
		private bool SendToAppStore()
		{
			if (this.m_store == "Amazon" || this.m_store == "Google" || this.m_store == "CN_Huawei" || this.m_store == "OneStore")
			{
				return true;
			}
			if (this.m_store == "CN")
			{
				if (this.m_allowUnknownApps && AndroidDeviceSettings.Get().AllowUnknownApps() && !this.IsDisabledAPKUpdateVersion())
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

		// Token: 0x0600B7CC RID: 47052 RVA: 0x00384EC8 File Offset: 0x003830C8
		private bool IsDisabledAPKUpdateVersion()
		{
			bool flag;
			if (this.m_disabledAPKUpdates != null)
			{
				flag = Array.Exists<int>(this.m_disabledAPKUpdates, (int s) => s == 84593);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				global::Log.Downloader.PrintInfo("The current version-{0} is disabled for APK update.", new object[]
				{
					84593
				});
			}
			return flag2;
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x00384F34 File Offset: 0x00383134
		private bool NeedBinaryUpdate(bool report)
		{
			int installedVersionCode = this.GetInstalledVersionCode();
			string liveDisplayVersion = this.m_agentStatus.m_configuration.m_liveDisplayVersion;
			int binaryVersionCode = this.GetBinaryVersionCode(liveDisplayVersion);
			if (report)
			{
				global::Log.Downloader.PrintInfo("InstalledVersion: {0}, BinaryVersionFromLive: {1}", new object[]
				{
					installedVersionCode,
					binaryVersionCode
				});
				int versionCode = this.GetVersionCode(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr);
				TelemetryManager.Client().SendApkUpdate(installedVersionCode, binaryVersionCode, versionCode);
			}
			return binaryVersionCode != installedVersionCode;
		}

		// Token: 0x0600B7CE RID: 47054 RVA: 0x00384FBC File Offset: 0x003831BC
		private int GetInstalledVersionCode()
		{
			int num = 0;
			try
			{
				num = int.Parse(Application.version.Split(new char[]
				{
					'.'
				})[2]);
				if (num != 84593)
				{
					global::Log.Downloader.PrintError("Application.version is different from our setting", Array.Empty<object>());
					num = 84593;
				}
			}
			catch (Exception ex)
			{
				global::Error.AddDevFatal("Failed to read the installed version: {0}", new object[]
				{
					ex.Message
				});
			}
			return num;
		}

		// Token: 0x0600B7CF RID: 47055 RVA: 0x0038503C File Offset: 0x0038323C
		protected int GetVersionCode(string versionString)
		{
			int[] array;
			if (this.GetSplitVersion(versionString, out array))
			{
				return array[3];
			}
			return 0;
		}

		// Token: 0x0600B7D0 RID: 47056 RVA: 0x0038505C File Offset: 0x0038325C
		protected int GetBinaryVersionCode(string versionString)
		{
			int[] versionInt;
			if (this.GetSplitVersion(versionString, out versionInt))
			{
				return this.GetBinaryVersionCode(versionInt);
			}
			return 0;
		}

		// Token: 0x0600B7D1 RID: 47057 RVA: 0x0038507D File Offset: 0x0038327D
		protected int GetBinaryVersionCode(int[] versionInt)
		{
			if (versionInt.Length <= 4)
			{
				return versionInt[3];
			}
			return versionInt[4];
		}

		// Token: 0x0600B7D2 RID: 47058 RVA: 0x0038508C File Offset: 0x0038328C
		private void ShowResumeMessage()
		{
			if (!this.m_optionalDownload)
			{
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_RESUME"), GameStrings.Get("GLOBAL_RESUME_GAME"), delegate()
				{
					global::Log.Downloader.PrintInfo("Resume the update which has stopped.", Array.Empty<object>());
					this.DoUpdate(this.UpdateOp[this.m_savedAgentState]);
				});
			}
		}

		// Token: 0x0600B7D3 RID: 47059 RVA: 0x003850C8 File Offset: 0x003832C8
		private void OpenAppStoreAlert()
		{
			TelemetryManager.Client().SendOpeningAppStore(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)this.m_deviceSpace / 1048576f, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
			string key = (this.m_store == "CN") ? "GLUE_LOADINGSCREEN_APK_UPDATE_FROM_WEBSITE" : "GLUE_LOADINGSCREEN_APK_UPDATE_FROM_APP_STORE";
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get(key), GameStrings.Get("GLUE_LOADINGSCREEN_OPEN_APP_STORE"), delegate()
			{
				global::Log.Downloader.PrintInfo("Open App store", Array.Empty<object>());
				if (!AndroidDeviceSettings.Get().OpenAppStore())
				{
					StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_ERROR_CLIENT_CONFIG_MESSAGE"), GameStrings.Get("GLOBAL_QUIT"), null);
					global::Error.AddDevFatal("Invalid store in client.config", Array.Empty<object>());
				}
				this.ShutdownApplication();
			});
		}

		// Token: 0x0600B7D4 RID: 47060 RVA: 0x00385158 File Offset: 0x00383358
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
			}
			return "GLUE_LOADINGSCREEN_ERROR_UPDATE";
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600B7D5 RID: 47061 RVA: 0x003851EA File Offset: 0x003833EA
		protected bool ShouldShowCellPopup
		{
			get
			{
				return this.m_askCellPopup && this.TotalBytes > 0L;
			}
		}

		// Token: 0x0600B7D6 RID: 47062 RVA: 0x00385200 File Offset: 0x00383400
		protected void StopDownloading()
		{
			if (this.IsUpdating())
			{
				this.m_savedAgentState = this.m_agentState;
				global::Log.Downloader.PrintInfo("Calling CancelAllOperations!", Array.Empty<object>());
				if (!this.m_pausedByNetwork)
				{
					this.State = AssetDownloaderState.IDLE;
					AgentEmbeddedAPI.CancelAllOperations();
					if (this.m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error != 0)
					{
						TelemetryManager.Client().SendUpdateError((uint)this.m_agentStatus.m_cachedState.m_updateProgress.m_progressDetails.m_error, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
					}
				}
			}
			if (this.blockedByDiskFull)
			{
				this.m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_DISK_SPACE";
				this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
				this.State = AssetDownloaderState.DISK_FULL;
				TelemetryManager.Client().SendNotEnoughSpaceError((ulong)this.m_deviceSpace, (ulong)this.TotalBytes, FileUtils.BaseExternalDataPath);
				return;
			}
			this.ShowNetworkDialog();
		}

		// Token: 0x0600B7D7 RID: 47063 RVA: 0x003852DC File Offset: 0x003834DC
		private void ShowNetworkDialog()
		{
			global::Log.Downloader.PrintInfo("ShowNetworkDialog", Array.Empty<object>());
			StartupDialog.Destroy();
			if (this.blockedByCellPermission)
			{
				global::Log.Downloader.PrintInfo("Block by cell permission", Array.Empty<object>());
				if (this.ShouldShowCellPopup)
				{
					this.ShowCellularAllowance();
					return;
				}
				this.ShowAwaitingWifi();
				return;
			}
			else
			{
				if (!NetworkReachabilityManager.InternetAvailable)
				{
					this.ShowAwaitingWifi();
					return;
				}
				if (!this.m_optionalDownload)
				{
					this.m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_UPDATE";
					this.m_agentState = RuntimeAssetDownloader.AgentState.ERROR;
				}
				return;
			}
		}

		// Token: 0x0600B7D8 RID: 47064 RVA: 0x0038535C File Offset: 0x0038355C
		private void ShowCellularAllowance()
		{
			global::Log.Downloader.PrintInfo("Asking for cell permission", Array.Empty<object>());
			string arg = DownloadStatusView.FormatBytesAsHumanReadable(this.TotalBytes - this.DownloadedBytes);
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_HEADER"), string.Format(GameStrings.Get(this.m_optionalDownload ? "GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_ADDITIONAL_BODY" : "GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_INITIAL_BODY"), arg), GameStrings.Get("GLOBAL_BUTTON_YES"), delegate()
			{
				this.m_deviceSpace = FreeSpace.Measure();
				TelemetryManager.Client().SendUsingCellularData(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)this.m_deviceSpace / 1048576f, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
				global::Log.Downloader.PrintInfo("User said yes to cell prompt.", Array.Empty<object>());
				this.m_cellularEnabledSession = true;
				this.DoUpdate(this.UpdateOp[this.m_savedAgentState]);
				this.m_askCellPopup = false;
			}, GameStrings.Get("GLOBAL_BUTTON_NO"), delegate()
			{
				global::Log.Downloader.PrintInfo("User said no to cell prompt.", Array.Empty<object>());
				this.ShowAwaitingWifi();
				this.m_askCellPopup = false;
			});
			this.m_agentState = RuntimeAssetDownloader.AgentState.AWAITING_WIFI;
		}

		// Token: 0x0600B7D9 RID: 47065 RVA: 0x003853F4 File Offset: 0x003835F4
		private void ShowAwaitingWifi()
		{
			global::Log.Downloader.PrintInfo("Awaiting Wifi", Array.Empty<object>());
			this.m_deviceSpace = FreeSpace.Measure();
			TelemetryManager.Client().SendNoWifi(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)this.m_deviceSpace / 1048576f, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
			if (!this.m_optionalDownload)
			{
				global::Log.Downloader.PrintInfo("Showing the Wifi awaiting", Array.Empty<object>());
				StartupDialog.ShowStartupDialog(GameStrings.Get("GLUE_LOADINGSCREEN_UPDATE_HEADER"), GameStrings.Get("GLUE_LOADINGSCREEN_ERROR_CHECK_SETTINGS"), GameStrings.Get("GLUE_LOADINGSCREEN_BUTTON_SETTINGS"), delegate()
				{
					global::Log.Downloader.PrintInfo("Check your wireless settings.", Array.Empty<object>());
					UpdateUtils.ShowWirelessSettings();
					this.ShowAwaitingWifi();
				});
			}
			this.m_agentState = RuntimeAssetDownloader.AgentState.AWAITING_WIFI;
			this.State = AssetDownloaderState.AWAITING_WIFI;
		}

		// Token: 0x0600B7DA RID: 47066 RVA: 0x003854B4 File Offset: 0x003836B4
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
				global::Log.Downloader.PrintInfo("Overriding PatchURL: " + text, Array.Empty<object>());
				AgentEmbeddedAPI.SetPatchUrlOverride("hsb", text);
				this.PatchOverrideUrl = new Uri(text).Host;
			}
		}

		// Token: 0x0600B7DB RID: 47067 RVA: 0x00385568 File Offset: 0x00383768
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

		// Token: 0x0600B7DC RID: 47068 RVA: 0x003855BC File Offset: 0x003837BC
		public void UnknownSourcesListener(string onOff)
		{
			global::Log.Downloader.PrintInfo("Unknown sources: " + onOff, Array.Empty<object>());
			this.m_allowUnknownApps = (onOff == "on");
			this.StartApkDownload();
			global::Log.Downloader.PrintInfo("Start to update APK", Array.Empty<object>());
		}

		// Token: 0x0600B7DD RID: 47069 RVA: 0x00385610 File Offset: 0x00383810
		public void InstallAPKListener(string status)
		{
			global::Log.Downloader.PrintInfo("install APK: " + status, Array.Empty<object>());
			if (!(status == "success"))
			{
				TelemetryManager.Client().SendApkInstallFailure(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, "exception: " + status);
				this.OpenAppStoreAlert();
				return;
			}
			this.m_deviceSpace = FreeSpace.Measure();
			TelemetryManager.Client().SendApkInstallSuccess(this.m_agentStatus.m_cachedState.m_baseState.m_currentVersionStr, (float)this.m_deviceSpace / 1048576f, this.ElapsedTimeFromStart(this.m_apkUpdateStartTime));
			StartupDialog.ShowStartupDialog(GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"), GameStrings.Get("GLOBAL_RELAUNCH_APPLICATION_AFTER_INSTALLAPK"), GameStrings.Get("GLOBAL_QUIT"), delegate()
			{
				this.ShutdownApplication();
			});
		}

		// Token: 0x0600B7DE RID: 47070 RVA: 0x003856E7 File Offset: 0x003838E7
		private void ClearDataList()
		{
			if (this.m_bundleDataList != null)
			{
				this.m_bundleDataList.Clear();
			}
		}

		// Token: 0x0600B7DF RID: 47071 RVA: 0x003856FC File Offset: 0x003838FC
		private void PrintDataList()
		{
			if (this.m_bundleDataList != null)
			{
				Processor.RunCoroutine(this.m_bundleDataList.Print(), null);
				this.m_bundleDataList.Clear();
			}
		}

		// Token: 0x040097F4 RID: 38900
		public const int IN_GAME_STREAMING_DEFAULT_SPEED = 512000;

		// Token: 0x040097F5 RID: 38901
		private const string AGENT_FOLDER_NAME = "__agent__";

		// Token: 0x040097F6 RID: 38902
		private const string NGDP_PRODUCT_NAME = "hsb";

		// Token: 0x040097F7 RID: 38903
		private const string TAG_APK_NAME = "tag_apk";

		// Token: 0x040097F8 RID: 38904
		private const string VERSION_SERVICE_PROXY_HOST = "hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net";

		// Token: 0x040097F9 RID: 38905
		private const string LIVE_SERVICE_STRING = "Live";

		// Token: 0x040097FA RID: 38906
		private const string RETURN_TO_LIVE_SERVICE_STRING = "Back to Live";

		// Token: 0x040097FB RID: 38907
		private const string UPDATE_GETSTATUS_JOB_NAME = "RuntimeAssetDownloader.GetStatus";

		// Token: 0x040097FC RID: 38908
		private static readonly string[] ASSET_MANIFEST_TAGS = new string[]
		{
			DownloadTags.GetTagString(DownloadTags.Quality.Manifest),
			DownloadTags.GetTagString(DownloadTags.Content.Base)
		};

		// Token: 0x040097FD RID: 38909
		private const float UPDATE_PROGRESS_REPORT_INTERVAL = 15f;

		// Token: 0x040097FE RID: 38910
		public Dictionary<RuntimeAssetDownloader.AgentState, RuntimeAssetDownloader.KindOfUpdate> UpdateOp = new Dictionary<RuntimeAssetDownloader.AgentState, RuntimeAssetDownloader.KindOfUpdate>
		{
			{
				RuntimeAssetDownloader.AgentState.VERSION,
				RuntimeAssetDownloader.KindOfUpdate.VERSION
			},
			{
				RuntimeAssetDownloader.AgentState.UPDATE_APK,
				RuntimeAssetDownloader.KindOfUpdate.APK_UPDATE
			},
			{
				RuntimeAssetDownloader.AgentState.UPDATE_MANIFEST,
				RuntimeAssetDownloader.KindOfUpdate.MANIFEST_UPDATE
			},
			{
				RuntimeAssetDownloader.AgentState.UPDATE_GLOBAL,
				RuntimeAssetDownloader.KindOfUpdate.GLOBAL_UPDATE
			},
			{
				RuntimeAssetDownloader.AgentState.UPDATE_OPTIONAL,
				RuntimeAssetDownloader.KindOfUpdate.OPTIONAL_UPDATE
			},
			{
				RuntimeAssetDownloader.AgentState.UPDATE_LOCALIZED,
				RuntimeAssetDownloader.KindOfUpdate.LOCALE_UPDATE
			}
		};

		// Token: 0x040097FF RID: 38911
		private bool m_allowUnknownApps;

		// Token: 0x04009800 RID: 38912
		private bool m_versionCalled;

		// Token: 0x04009801 RID: 38913
		private bool m_agentInit;

		// Token: 0x04009802 RID: 38914
		private bool m_cancelledByUser;

		// Token: 0x04009803 RID: 38915
		private bool m_askCellPopup = true;

		// Token: 0x04009804 RID: 38916
		private bool m_pausedByNetwork;

		// Token: 0x04009805 RID: 38917
		private bool m_redrawDialog;

		// Token: 0x04009806 RID: 38918
		private bool m_showResumeDialog;

		// Token: 0x04009807 RID: 38919
		private bool m_cellularEnabledSession;

		// Token: 0x04009808 RID: 38920
		private bool m_retriedUpdateWithNoSpaceError;

		// Token: 0x04009809 RID: 38921
		private bool m_optionalDownload;

		// Token: 0x0400980A RID: 38922
		private int[] m_disabledAPKUpdates;

		// Token: 0x0400980B RID: 38923
		private string m_errorMsg = "GLUE_LOADINGSCREEN_ERROR_GENERIC";

		// Token: 0x0400980C RID: 38924
		private string m_store;

		// Token: 0x0400980D RID: 38925
		private string[] m_disabledAdventuresForStreaming = new string[0];

		// Token: 0x0400980E RID: 38926
		private float m_progress;

		// Token: 0x0400980F RID: 38927
		private float m_prevProgress;

		// Token: 0x04009810 RID: 38928
		private long m_deviceSpace;

		// Token: 0x04009811 RID: 38929
		private int m_internalAgentError;

		// Token: 0x04009812 RID: 38930
		private RuntimeAssetDownloader.AgentState m_agentState;

		// Token: 0x04009813 RID: 38931
		private RuntimeAssetDownloader.AgentState m_savedAgentState;

		// Token: 0x04009814 RID: 38932
		private ProductStatus m_agentStatus;

		// Token: 0x04009815 RID: 38933
		private IDisposable m_callbackDisposer;

		// Token: 0x04009816 RID: 38934
		private float m_updateStartTime;

		// Token: 0x04009817 RID: 38935
		private float m_apkUpdateStartTime;

		// Token: 0x04009818 RID: 38936
		private float m_lastUpdateProgressReportTime;

		// Token: 0x04009819 RID: 38937
		private RuntimeAssetDownloader.DownloadProgress m_tagDownloads = new RuntimeAssetDownloader.DownloadProgress();

		// Token: 0x0400981A RID: 38938
		private Dictionary<int, int> m_tagLocations = new Dictionary<int, int>();

		// Token: 0x0400981B RID: 38939
		private HashSet<string> m_tempUniqueTags = new HashSet<string>();

		// Token: 0x0400981C RID: 38940
		private int m_instantSpeed;

		// Token: 0x0400981D RID: 38941
		private int m_instantMaxSpeed;

		// Token: 0x0400981E RID: 38942
		private int m_inGameStreamingDefaultSpeed = 512000;

		// Token: 0x0400981F RID: 38943
		private bool m_inGameStreamingOff;

		// Token: 0x04009820 RID: 38944
		private TagIndicatorManager m_tagIndicatorManager = new TagIndicatorManager();

		// Token: 0x04009821 RID: 38945
		private RuntimeAssetDownloader.BundleDataList m_bundleDataList;

		// Token: 0x04009822 RID: 38946
		private ReactiveBoolOption m_isDownloadAllFinished = ReactiveBoolOption.CreateInstance(Option.DOWNLOAD_ALL_FINISHED);

		// Token: 0x04009823 RID: 38947
		private bool blockedByDiskFull;

		// Token: 0x04009827 RID: 38951
		private TagDownloadStatus m_curDownloadStatus;

		// Token: 0x04009828 RID: 38952
		private string m_installDataPath;

		// Token: 0x04009829 RID: 38953
		private string[] m_dataFolders = new string[]
		{
			RuntimeAssetDownloader.INSTALL_PATH + "/__agent__",
			RuntimeAssetDownloader.INSTALL_PATH + "/Data",
			RuntimeAssetDownloader.INSTALL_PATH + "/Strings"
		};

		// Token: 0x02002899 RID: 10393
		public enum AgentInternalState
		{
			// Token: 0x0400FA45 RID: 64069
			STATE_NONE,
			// Token: 0x0400FA46 RID: 64070
			STATE_STARTING = 1000,
			// Token: 0x0400FA47 RID: 64071
			STATE_DOWNLOADING,
			// Token: 0x0400FA48 RID: 64072
			STATE_INSTALLING,
			// Token: 0x0400FA49 RID: 64073
			STATE_UPDATING,
			// Token: 0x0400FA4A RID: 64074
			STATE_READY,
			// Token: 0x0400FA4B RID: 64075
			STATE_RUNNING,
			// Token: 0x0400FA4C RID: 64076
			STATE_CLOSING,
			// Token: 0x0400FA4D RID: 64077
			STATE_VERSIONING,
			// Token: 0x0400FA4E RID: 64078
			STATE_WAITING,
			// Token: 0x0400FA4F RID: 64079
			STATE_FINISHED,
			// Token: 0x0400FA50 RID: 64080
			STATE_CANCELED,
			// Token: 0x0400FA51 RID: 64081
			STATE_IMPEDED = 1100,
			// Token: 0x0400FA52 RID: 64082
			STATE_ERROR_START = 1200,
			// Token: 0x0400FA53 RID: 64083
			STATE_FAILED = 1200,
			// Token: 0x0400FA54 RID: 64084
			STATE_CANCELING = 1202,
			// Token: 0x0400FA55 RID: 64085
			STATE_OUT_OF_DATE,
			// Token: 0x0400FA56 RID: 64086
			STATE_ERROR_END = 1299
		}

		// Token: 0x0200289A RID: 10394
		public enum AgentState
		{
			// Token: 0x0400FA58 RID: 64088
			ERROR = -1,
			// Token: 0x0400FA59 RID: 64089
			NONE,
			// Token: 0x0400FA5A RID: 64090
			INSTALLED,
			// Token: 0x0400FA5B RID: 64091
			VERSION,
			// Token: 0x0400FA5C RID: 64092
			WAIT_SERVICE,
			// Token: 0x0400FA5D RID: 64093
			UNKNOWN_APPS,
			// Token: 0x0400FA5E RID: 64094
			OPEN_APP_STORE,
			// Token: 0x0400FA5F RID: 64095
			UPDATE_APK,
			// Token: 0x0400FA60 RID: 64096
			UPDATE_MANIFEST,
			// Token: 0x0400FA61 RID: 64097
			UPDATE_GLOBAL,
			// Token: 0x0400FA62 RID: 64098
			UPDATE_OPTIONAL,
			// Token: 0x0400FA63 RID: 64099
			UPDATE_LOCALIZED,
			// Token: 0x0400FA64 RID: 64100
			AWAITING_WIFI,
			// Token: 0x0400FA65 RID: 64101
			DISK_FULL
		}

		// Token: 0x0200289B RID: 10395
		public enum KindOfUpdate
		{
			// Token: 0x0400FA67 RID: 64103
			VERSION,
			// Token: 0x0400FA68 RID: 64104
			APK_UPDATE,
			// Token: 0x0400FA69 RID: 64105
			MANIFEST_UPDATE,
			// Token: 0x0400FA6A RID: 64106
			GLOBAL_UPDATE,
			// Token: 0x0400FA6B RID: 64107
			OPTIONAL_UPDATE,
			// Token: 0x0400FA6C RID: 64108
			LOCALE_UPDATE
		}

		// Token: 0x0200289C RID: 10396
		[Serializable]
		public class DownloadProgress
		{
			// Token: 0x0400FA6D RID: 64109
			public List<TagDownloadStatus> m_items = new List<TagDownloadStatus>();
		}

		// Token: 0x0200289D RID: 10397
		public class BundleDataList
		{
			// Token: 0x17002D3A RID: 11578
			// (get) Token: 0x06013C4A RID: 80970 RVA: 0x0053C2F5 File Offset: 0x0053A4F5
			public bool isEnabled
			{
				get
				{
					return Vars.Key("Mobile.GenDataList").GetBool(false);
				}
			}

			// Token: 0x06013C4B RID: 80971 RVA: 0x0053C307 File Offset: 0x0053A507
			public BundleDataList(string dataFolder)
			{
				this.m_dataFolder = dataFolder;
			}

			// Token: 0x06013C4C RID: 80972 RVA: 0x0053C321 File Offset: 0x0053A521
			public void Clear()
			{
				if (!this.isEnabled)
				{
					return;
				}
				this.m_dataFiles.Clear();
			}

			// Token: 0x06013C4D RID: 80973 RVA: 0x0053C337 File Offset: 0x0053A537
			public IEnumerator Print()
			{
				if (!this.isEnabled)
				{
					yield break;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(this.m_dataFolder);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (!this.m_dataFiles.ContainsKey(fileInfo.Name))
					{
						this.m_dataFiles.Add(fileInfo.Name, new RuntimeAssetDownloader.BundleDataList.DataInfo(RuntimeAssetDownloader.BundleDataList.CalculateMD5(fileInfo.FullName), fileInfo.Length));
					}
					yield return null;
				}
				FileInfo[] array = null;
				global::Log.Downloader.PrintInfo("== Data Directory Info ==", Array.Empty<object>());
				foreach (KeyValuePair<string, RuntimeAssetDownloader.BundleDataList.DataInfo> keyValuePair in this.m_dataFiles)
				{
					global::Log.Downloader.PrintInfo("{0}\t{1}\t{2}", new object[]
					{
						keyValuePair.Key,
						keyValuePair.Value.m_size,
						keyValuePair.Value.m_md5
					});
				}
				global::Log.Downloader.PrintInfo("====================", Array.Empty<object>());
				yield break;
			}

			// Token: 0x06013C4E RID: 80974 RVA: 0x0053C348 File Offset: 0x0053A548
			private static string CalculateMD5(string filename)
			{
				string result;
				using (MD5 md = MD5.Create())
				{
					using (FileStream fileStream = File.OpenRead(filename))
					{
						result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
					}
				}
				return result;
			}

			// Token: 0x0400FA6E RID: 64110
			private Dictionary<string, RuntimeAssetDownloader.BundleDataList.DataInfo> m_dataFiles = new Dictionary<string, RuntimeAssetDownloader.BundleDataList.DataInfo>();

			// Token: 0x0400FA6F RID: 64111
			private string m_dataFolder;

			// Token: 0x020029B1 RID: 10673
			private struct DataInfo
			{
				// Token: 0x06013FBD RID: 81853 RVA: 0x005420E6 File Offset: 0x005402E6
				public DataInfo(string md5, long size)
				{
					this.m_md5 = md5;
					this.m_size = size;
				}

				// Token: 0x0400FE1C RID: 65052
				public string m_md5;

				// Token: 0x0400FE1D RID: 65053
				public long m_size;
			}
		}
	}
}
