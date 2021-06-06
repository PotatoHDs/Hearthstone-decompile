using System;
using System.Runtime.InteropServices;

namespace bgs.types
{
	// Token: 0x0200027B RID: 635
	public struct ChallengeInfo
	{
		// Token: 0x04001015 RID: 4117
		public ulong challengeId;

		// Token: 0x04001016 RID: 4118
		[MarshalAs(UnmanagedType.I1)]
		public bool isRetry;

		// Token: 0x04001017 RID: 4119
		public int type;
	}
}
