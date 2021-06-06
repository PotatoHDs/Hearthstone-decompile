namespace Hearthstone.Core.Streaming
{
	public enum AssetDownloaderState
	{
		UNINITIALIZED = -1,
		ERROR = 0,
		IDLE = 1,
		DOWNLOADING = 2,
		AGENT_IMPEDED = 3,
		DISK_FULL = 5,
		FETCHING_SIZE = 6,
		PAUSED_DURING_GAME = 7,
		AWAITING_WIFI = 8
	}
}
