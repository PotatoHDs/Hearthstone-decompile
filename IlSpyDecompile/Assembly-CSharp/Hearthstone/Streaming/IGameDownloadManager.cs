using Hearthstone.Core.Streaming;

namespace Hearthstone.Streaming
{
	public interface IGameDownloadManager
	{
		InterruptionReason InterruptionReason { get; }

		bool IsAnyDownloadRequestedAndIncomplete { get; }

		bool IsInterrupted { get; }

		bool IsNewMobileVersionReleased { get; }

		bool IsReadyToPlay { get; }

		bool IsRunningNewerBinaryThanLive { get; }

		bool IsReadyToReadAssetManifest { get; }

		bool ShouldDownloadLocalizedAssets { get; }

		string AgentLogPath { get; }

		double BytesPerSecond { get; }

		string PatchOverrideUrl { get; }

		string VersionOverrideUrl { get; }

		string[] DisabledAdventuresForStreaming { get; }

		int MaxDownloadSpeed { get; set; }

		int InGameStreamingDefaultSpeed { get; set; }

		int DownloadSpeedInGame { get; set; }

		bool Initialize();

		void StartContentDownload(DownloadTags.Content content);

		bool IsAssetDownloaded(string assetGuid);

		bool IsBundleDownloaded(string assetBundleName);

		bool IsReadyAssetsInTags(string[] tags);

		bool IsCompletedInitialBaseDownload();

		TagDownloadStatus GetTagDownloadStatus(string[] tags);

		TagDownloadStatus GetCurrentDownloadStatus();

		ContentDownloadStatus GetContentDownloadStatus(DownloadTags.Content contentTag);

		void StartUpdateProcess(bool localeChange);

		void StartUpdateProcessForOptional();

		void PauseAllDownloads();

		void ResumeAllDownloads();

		void DeleteDownloadedData();

		void StopOptionalDownloads();
	}
}
