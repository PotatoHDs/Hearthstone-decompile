using System;

namespace Hearthstone.InGameMessage.UI
{
	// Token: 0x02001162 RID: 4450
	public class UIMessageCallbacks
	{
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600C2DA RID: 49882 RVA: 0x003B041F File Offset: 0x003AE61F
		// (set) Token: 0x0600C2DB RID: 49883 RVA: 0x003B0427 File Offset: 0x003AE627
		public Action OnShown { get; set; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x0600C2DC RID: 49884 RVA: 0x003B0430 File Offset: 0x003AE630
		// (set) Token: 0x0600C2DD RID: 49885 RVA: 0x003B0438 File Offset: 0x003AE638
		public Action OnClosed { get; set; }

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600C2DE RID: 49886 RVA: 0x003B0441 File Offset: 0x003AE641
		// (set) Token: 0x0600C2DF RID: 49887 RVA: 0x003B0449 File Offset: 0x003AE649
		public Action OnStoreOpened { get; set; }
	}
}
