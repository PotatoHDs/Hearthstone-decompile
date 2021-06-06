using System;
using System.ComponentModel;

namespace bgs
{
	// Token: 0x02000267 RID: 615
	public class constants
	{
		// Token: 0x04000FAC RID: 4012
		public const ushort RouteToAnyUtil = 0;

		// Token: 0x04000FAD RID: 4013
		public const float ResubsribeAttemptDelaySeconds = 120f;

		// Token: 0x020006EE RID: 1774
		public enum BNetState
		{
			// Token: 0x04002294 RID: 8852
			BATTLE_NET_UNKNOWN,
			// Token: 0x04002295 RID: 8853
			BATTLE_NET_LOGGING_IN,
			// Token: 0x04002296 RID: 8854
			BATTLE_NET_TIMEOUT,
			// Token: 0x04002297 RID: 8855
			BATTLE_NET_LOGIN_FAILED,
			// Token: 0x04002298 RID: 8856
			BATTLE_NET_LOGGED_IN,
			// Token: 0x04002299 RID: 8857
			BATTLE_NET_DISCONNECTED
		}

		// Token: 0x020006EF RID: 1775
		public enum BnetRegion
		{
			// Token: 0x0400229B RID: 8859
			REGION_UNINITIALIZED = -1,
			// Token: 0x0400229C RID: 8860
			REGION_UNKNOWN,
			// Token: 0x0400229D RID: 8861
			REGION_US,
			// Token: 0x0400229E RID: 8862
			REGION_EU,
			// Token: 0x0400229F RID: 8863
			REGION_KR,
			// Token: 0x040022A0 RID: 8864
			REGION_TW,
			// Token: 0x040022A1 RID: 8865
			REGION_CN,
			// Token: 0x040022A2 RID: 8866
			REGION_LIVE_VERIFICATION = 40,
			// Token: 0x040022A3 RID: 8867
			REGION_PTR_LOC,
			// Token: 0x040022A4 RID: 8868
			REGION_DEV = 60,
			// Token: 0x040022A5 RID: 8869
			REGION_PTR = 98
		}

		// Token: 0x020006F0 RID: 1776
		public enum MobileEnv
		{
			// Token: 0x040022A7 RID: 8871
			[Description("Development")]
			DEVELOPMENT,
			// Token: 0x040022A8 RID: 8872
			[Description("Production")]
			PRODUCTION
		}

		// Token: 0x020006F1 RID: 1777
		public enum RuntimeEnvironment
		{
			// Token: 0x040022AA RID: 8874
			Mono,
			// Token: 0x040022AB RID: 8875
			MSDotNet
		}
	}
}
