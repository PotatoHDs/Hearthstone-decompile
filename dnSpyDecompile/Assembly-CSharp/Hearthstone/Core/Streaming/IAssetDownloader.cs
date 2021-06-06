using System;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x0200108D RID: 4237
	public interface IAssetDownloader
	{
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600B72B RID: 46891
		AssetDownloaderState State { get; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600B72C RID: 46892
		string AgentLogPath { get; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600B72D RID: 46893
		bool IsReady { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600B72E RID: 46894
		bool IsNewMobileVersionReleased { get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600B72F RID: 46895
		bool IsRunningNewerBinaryThanLive { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600B730 RID: 46896
		bool IsVersionChanged { get; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600B731 RID: 46897
		bool IsAssetManifestReady { get; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600B732 RID: 46898
		// (set) Token: 0x0600B733 RID: 46899
		bool DownloadAllFinished { get; set; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600B734 RID: 46900
		// (set) Token: 0x0600B735 RID: 46901
		bool DisabledErrorMessageDialog { get; set; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600B736 RID: 46902
		double BytesPerSecond { get; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600B737 RID: 46903
		// (set) Token: 0x0600B738 RID: 46904
		int MaxDownloadSpeed { get; set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600B739 RID: 46905
		// (set) Token: 0x0600B73A RID: 46906
		int InGameStreamingDefaultSpeed { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600B73B RID: 46907
		// (set) Token: 0x0600B73C RID: 46908
		int DownloadSpeedInGame { get; set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600B73D RID: 46909
		string PatchOverrideUrl { get; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600B73E RID: 46910
		string VersionOverrideUrl { get; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600B73F RID: 46911
		string[] DisabledAdventuresForStreaming { get; }

		// Token: 0x0600B740 RID: 46912
		bool Initialize();

		// Token: 0x0600B741 RID: 46913
		void Update(bool firstCall);

		// Token: 0x0600B742 RID: 46914
		void Shutdown();

		// Token: 0x0600B743 RID: 46915
		TagDownloadStatus GetDownloadStatus(string[] tags);

		// Token: 0x0600B744 RID: 46916
		TagDownloadStatus GetCurrentDownloadStatus();

		// Token: 0x0600B745 RID: 46917
		void StartDownload(string[] tags, bool initialDownload, bool localeChanged);

		// Token: 0x0600B746 RID: 46918
		void PauseAllDownloads();

		// Token: 0x0600B747 RID: 46919
		void DeleteDownloadedData();

		// Token: 0x0600B748 RID: 46920
		bool IsBundleDownloaded(string bundleName);

		// Token: 0x0600B749 RID: 46921
		void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData);

		// Token: 0x0600B74A RID: 46922
		void PrepareRestart();

		// Token: 0x0600B74B RID: 46923
		void DoPostTasksAfterInitialDownload();

		// Token: 0x0600B74C RID: 46924
		void UnknownSourcesListener(string onOff);

		// Token: 0x0600B74D RID: 46925
		void InstallAPKListener(string status);

		// Token: 0x0600B74E RID: 46926
		void StopDownloads();
	}
}
