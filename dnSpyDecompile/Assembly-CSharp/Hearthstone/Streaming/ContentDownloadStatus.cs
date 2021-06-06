using System;
using Hearthstone.Core.Streaming;

namespace Hearthstone.Streaming
{
	// Token: 0x0200106E RID: 4206
	public class ContentDownloadStatus
	{
		// Token: 0x0400975E RID: 38750
		public string ContentTag;

		// Token: 0x0400975F RID: 38751
		public long BytesTotal;

		// Token: 0x04009760 RID: 38752
		public long BytesDownloaded;

		// Token: 0x04009761 RID: 38753
		public float Progress;

		// Token: 0x04009762 RID: 38754
		public DownloadTags.Quality InProgressQualityTag = DownloadTags.Quality.Initial;
	}
}
