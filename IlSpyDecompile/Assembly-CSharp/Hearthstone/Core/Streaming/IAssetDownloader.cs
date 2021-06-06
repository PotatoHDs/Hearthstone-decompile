namespace Hearthstone.Core.Streaming
{
	public interface IAssetDownloader
	{
		AssetDownloaderState State { get; }

		string AgentLogPath { get; }

		bool IsReady { get; }

		bool IsNewMobileVersionReleased { get; }

		bool IsRunningNewerBinaryThanLive { get; }

		bool IsVersionChanged { get; }

		bool IsAssetManifestReady { get; }

		bool DownloadAllFinished { get; set; }

		bool DisabledErrorMessageDialog { get; set; }

		double BytesPerSecond { get; }

		int MaxDownloadSpeed { get; set; }

		int InGameStreamingDefaultSpeed { get; set; }

		int DownloadSpeedInGame { get; set; }

		string PatchOverrideUrl { get; }

		string VersionOverrideUrl { get; }

		string[] DisabledAdventuresForStreaming { get; }

		bool Initialize();

		void Update(bool firstCall);

		void Shutdown();

		TagDownloadStatus GetDownloadStatus(string[] tags);

		TagDownloadStatus GetCurrentDownloadStatus();

		void StartDownload(string[] tags, bool initialDownload, bool localeChanged);

		void PauseAllDownloads();

		void DeleteDownloadedData();

		bool IsBundleDownloaded(string bundleName);

		void OnSceneLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData);

		void PrepareRestart();

		void DoPostTasksAfterInitialDownload();

		void UnknownSourcesListener(string onOff);

		void InstallAPKListener(string status);

		void StopDownloads();
	}
}
