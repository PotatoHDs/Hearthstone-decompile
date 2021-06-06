using Hearthstone.Core.Streaming;

namespace Hearthstone.Streaming
{
	public class ContentDownloadStatus
	{
		public string ContentTag;

		public long BytesTotal;

		public long BytesDownloaded;

		public float Progress;

		public DownloadTags.Quality InProgressQualityTag = DownloadTags.Quality.Initial;
	}
}
