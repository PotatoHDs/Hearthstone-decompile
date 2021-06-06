using System;

namespace Hearthstone.Login
{
	// Token: 0x0200112F RID: 4399
	public struct GuestAccountInfo
	{
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600C0D4 RID: 49364 RVA: 0x003AAF57 File Offset: 0x003A9157
		// (set) Token: 0x0600C0D5 RID: 49365 RVA: 0x003AAF5F File Offset: 0x003A915F
		public string GuestId { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600C0D6 RID: 49366 RVA: 0x003AAF68 File Offset: 0x003A9168
		// (set) Token: 0x0600C0D7 RID: 49367 RVA: 0x003AAF70 File Offset: 0x003A9170
		public string RegionId { get; set; }
	}
}
