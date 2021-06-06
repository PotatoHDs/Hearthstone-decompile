using System;

namespace Hearthstone.Streaming
{
	// Token: 0x02001071 RID: 4209
	public enum InterruptionReason
	{
		// Token: 0x04009775 RID: 38773
		None,
		// Token: 0x04009776 RID: 38774
		Unknown,
		// Token: 0x04009777 RID: 38775
		Error,
		// Token: 0x04009778 RID: 38776
		Disabled,
		// Token: 0x04009779 RID: 38777
		Paused,
		// Token: 0x0400977A RID: 38778
		AwaitingWifi,
		// Token: 0x0400977B RID: 38779
		DiskFull,
		// Token: 0x0400977C RID: 38780
		AgentImpeded,
		// Token: 0x0400977D RID: 38781
		Fetching
	}
}
