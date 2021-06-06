using System;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x02001168 RID: 4456
	public class DungeonCrawlServices
	{
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600C310 RID: 49936 RVA: 0x003B0CD8 File Offset: 0x003AEED8
		// (set) Token: 0x0600C311 RID: 49937 RVA: 0x003B0CE0 File Offset: 0x003AEEE0
		public IDungeonCrawlData DungeonCrawlData { get; set; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600C312 RID: 49938 RVA: 0x003B0CE9 File Offset: 0x003AEEE9
		// (set) Token: 0x0600C313 RID: 49939 RVA: 0x003B0CF1 File Offset: 0x003AEEF1
		public ISubsceneController SubsceneController { get; set; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600C314 RID: 49940 RVA: 0x003B0CFA File Offset: 0x003AEEFA
		// (set) Token: 0x0600C315 RID: 49941 RVA: 0x003B0D02 File Offset: 0x003AEF02
		public AssetLoadingHelper AssetLoadingHelper { get; set; }
	}
}
