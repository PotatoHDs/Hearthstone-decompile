using System;

namespace Hearthstone.Login
{
	// Token: 0x0200112D RID: 4397
	public struct LoginStrategyParameters
	{
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x0600C0CE RID: 49358 RVA: 0x003AAF35 File Offset: 0x003A9135
		// (set) Token: 0x0600C0CF RID: 49359 RVA: 0x003AAF3D File Offset: 0x003A913D
		public IMobileAuth MobileAuth { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600C0D0 RID: 49360 RVA: 0x003AAF46 File Offset: 0x003A9146
		// (set) Token: 0x0600C0D1 RID: 49361 RVA: 0x003AAF4E File Offset: 0x003A914E
		public string ChallengeUrl { get; set; }
	}
}
