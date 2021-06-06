using System;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x0200108C RID: 4236
	public enum AssetDownloaderState
	{
		// Token: 0x040097E9 RID: 38889
		UNINITIALIZED = -1,
		// Token: 0x040097EA RID: 38890
		ERROR,
		// Token: 0x040097EB RID: 38891
		IDLE,
		// Token: 0x040097EC RID: 38892
		DOWNLOADING,
		// Token: 0x040097ED RID: 38893
		AGENT_IMPEDED,
		// Token: 0x040097EE RID: 38894
		DISK_FULL = 5,
		// Token: 0x040097EF RID: 38895
		FETCHING_SIZE,
		// Token: 0x040097F0 RID: 38896
		PAUSED_DURING_GAME,
		// Token: 0x040097F1 RID: 38897
		AWAITING_WIFI
	}
}
