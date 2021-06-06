using System;

namespace bgs
{
	// Token: 0x0200020C RID: 524
	internal class ResourcesAPIPendingState
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00075B4B File Offset: 0x00073D4B
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x00075B53 File Offset: 0x00073D53
		public ResourcesAPI.ResourceLookupCallback Callback { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00075B5C File Offset: 0x00073D5C
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x00075B64 File Offset: 0x00073D64
		public object UserContext { get; set; }
	}
}
