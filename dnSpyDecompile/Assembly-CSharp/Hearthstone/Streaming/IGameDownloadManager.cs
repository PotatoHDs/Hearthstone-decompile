using System;
using Hearthstone.Core.Streaming;

namespace Hearthstone.Streaming
{
	// Token: 0x02001072 RID: 4210
	public interface IGameDownloadManager
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600B5D7 RID: 46551
		InterruptionReason InterruptionReason { get; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600B5D8 RID: 46552
		bool IsAnyDownloadRequestedAndIncomplete { get; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600B5D9 RID: 46553
		bool IsInterrupted { get; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600B5DA RID: 46554
		bool IsNewMobileVersionReleased { get; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600B5DB RID: 46555
		bool IsReadyToPlay { get; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600B5DC RID: 46556
		bool IsRunningNewerBinaryThanLive { get; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600B5DD RID: 46557
		bool IsReadyToReadAssetManifest { get; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600B5DE RID: 46558
		bool ShouldDownloadLocalizedAssets { get; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600B5DF RID: 46559
		string AgentLogPath { get; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600B5E0 RID: 46560
		double BytesPerSecond { get; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600B5E1 RID: 46561
		string PatchOverrideUrl { get; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600B5E2 RID: 46562
		string VersionOverrideUrl { get; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600B5E3 RID: 46563
		string[] DisabledAdventuresForStreaming { get; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600B5E4 RID: 46564
		// (set) Token: 0x0600B5E5 RID: 46565
		int MaxDownloadSpeed { get; set; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600B5E6 RID: 46566
		// (set) Token: 0x0600B5E7 RID: 46567
		int InGameStreamingDefaultSpeed { get; set; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600B5E8 RID: 46568
		// (set) Token: 0x0600B5E9 RID: 46569
		int DownloadSpeedInGame { get; set; }

		// Token: 0x0600B5EA RID: 46570
		bool Initialize();

		// Token: 0x0600B5EB RID: 46571
		void StartContentDownload(DownloadTags.Content content);

		// Token: 0x0600B5EC RID: 46572
		bool IsAssetDownloaded(string assetGuid);

		// Token: 0x0600B5ED RID: 46573
		bool IsBundleDownloaded(string assetBundleName);

		// Token: 0x0600B5EE RID: 46574
		bool IsReadyAssetsInTags(string[] tags);

		// Token: 0x0600B5EF RID: 46575
		bool IsCompletedInitialBaseDownload();

		// Token: 0x0600B5F0 RID: 46576
		TagDownloadStatus GetTagDownloadStatus(string[] tags);

		// Token: 0x0600B5F1 RID: 46577
		TagDownloadStatus GetCurrentDownloadStatus();

		// Token: 0x0600B5F2 RID: 46578
		ContentDownloadStatus GetContentDownloadStatus(DownloadTags.Content contentTag);

		// Token: 0x0600B5F3 RID: 46579
		void StartUpdateProcess(bool localeChange);

		// Token: 0x0600B5F4 RID: 46580
		void StartUpdateProcessForOptional();

		// Token: 0x0600B5F5 RID: 46581
		void PauseAllDownloads();

		// Token: 0x0600B5F6 RID: 46582
		void ResumeAllDownloads();

		// Token: 0x0600B5F7 RID: 46583
		void DeleteDownloadedData();

		// Token: 0x0600B5F8 RID: 46584
		void StopOptionalDownloads();
	}
}
