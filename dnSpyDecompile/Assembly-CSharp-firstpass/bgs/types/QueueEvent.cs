using System;

namespace bgs.types
{
	// Token: 0x0200026E RID: 622
	public class QueueEvent
	{
		// Token: 0x060025A9 RID: 9641 RVA: 0x00085F03 File Offset: 0x00084103
		public QueueEvent(QueueEvent.Type t, int minSeconds, int maxSeconds, int bnetError, GameServerInfo gsInfo)
		{
			this.EventType = t;
			this.MinSeconds = minSeconds;
			this.MaxSeconds = maxSeconds;
			this.BnetError = bnetError;
			this.GameServer = gsInfo;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00085F30 File Offset: 0x00084130
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x00085F38 File Offset: 0x00084138
		public QueueEvent.Type EventType { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x00085F41 File Offset: 0x00084141
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x00085F49 File Offset: 0x00084149
		public int MinSeconds { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x00085F52 File Offset: 0x00084152
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x00085F5A File Offset: 0x0008415A
		public int MaxSeconds { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x00085F63 File Offset: 0x00084163
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x00085F6B File Offset: 0x0008416B
		public int BnetError { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x00085F74 File Offset: 0x00084174
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x00085F7C File Offset: 0x0008417C
		public GameServerInfo GameServer { get; set; }

		// Token: 0x020006F2 RID: 1778
		public enum Type
		{
			// Token: 0x040022AD RID: 8877
			UNKNOWN,
			// Token: 0x040022AE RID: 8878
			QUEUE_ENTER,
			// Token: 0x040022AF RID: 8879
			QUEUE_LEAVE,
			// Token: 0x040022B0 RID: 8880
			QUEUE_DELAY,
			// Token: 0x040022B1 RID: 8881
			QUEUE_UPDATE,
			// Token: 0x040022B2 RID: 8882
			QUEUE_DELAY_ERROR,
			// Token: 0x040022B3 RID: 8883
			QUEUE_AMM_ERROR,
			// Token: 0x040022B4 RID: 8884
			QUEUE_WAIT_END,
			// Token: 0x040022B5 RID: 8885
			QUEUE_CANCEL,
			// Token: 0x040022B6 RID: 8886
			QUEUE_GAME_STARTED,
			// Token: 0x040022B7 RID: 8887
			ABORT_CLIENT_DROPPED
		}
	}
}
