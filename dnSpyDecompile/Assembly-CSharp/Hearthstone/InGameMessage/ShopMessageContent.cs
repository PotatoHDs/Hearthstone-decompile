using System;

namespace Hearthstone.InGameMessage
{
	// Token: 0x0200115B RID: 4443
	public class ShopMessageContent
	{
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600C2BA RID: 49850 RVA: 0x003AFE50 File Offset: 0x003AE050
		// (set) Token: 0x0600C2BB RID: 49851 RVA: 0x003AFE58 File Offset: 0x003AE058
		public string Title { get; set; }

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600C2BC RID: 49852 RVA: 0x003AFE61 File Offset: 0x003AE061
		// (set) Token: 0x0600C2BD RID: 49853 RVA: 0x003AFE69 File Offset: 0x003AE069
		public string TextBody { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600C2BE RID: 49854 RVA: 0x003AFE72 File Offset: 0x003AE072
		// (set) Token: 0x0600C2BF RID: 49855 RVA: 0x003AFE7A File Offset: 0x003AE07A
		public long ProductID { get; set; }

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600C2C0 RID: 49856 RVA: 0x003AFE83 File Offset: 0x003AE083
		// (set) Token: 0x0600C2C1 RID: 49857 RVA: 0x003AFE8B File Offset: 0x003AE08B
		public bool OpenFullShop { get; set; }
	}
}
