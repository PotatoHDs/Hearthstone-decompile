using System;

// Token: 0x020008DA RID: 2266
public class HearthstoneCheckoutTransactionData
{
	// Token: 0x17000733 RID: 1843
	// (get) Token: 0x06007D81 RID: 32129 RVA: 0x0028BCCA File Offset: 0x00289ECA
	// (set) Token: 0x06007D82 RID: 32130 RVA: 0x0028BCD2 File Offset: 0x00289ED2
	public string TransactionID { get; set; }

	// Token: 0x17000734 RID: 1844
	// (get) Token: 0x06007D83 RID: 32131 RVA: 0x0028BCDB File Offset: 0x00289EDB
	// (set) Token: 0x06007D84 RID: 32132 RVA: 0x0028BCE3 File Offset: 0x00289EE3
	public long ProductID { get; set; }

	// Token: 0x17000735 RID: 1845
	// (get) Token: 0x06007D85 RID: 32133 RVA: 0x0028BCEC File Offset: 0x00289EEC
	// (set) Token: 0x06007D86 RID: 32134 RVA: 0x0028BCF4 File Offset: 0x00289EF4
	public string CurrencyCode { get; set; }

	// Token: 0x17000736 RID: 1846
	// (get) Token: 0x06007D87 RID: 32135 RVA: 0x0028BCFD File Offset: 0x00289EFD
	// (set) Token: 0x06007D88 RID: 32136 RVA: 0x0028BD05 File Offset: 0x00289F05
	public string ErrorCodes { get; set; }

	// Token: 0x17000737 RID: 1847
	// (get) Token: 0x06007D89 RID: 32137 RVA: 0x0028BD0E File Offset: 0x00289F0E
	// (set) Token: 0x06007D8A RID: 32138 RVA: 0x0028BD16 File Offset: 0x00289F16
	public uint Quantity { get; set; }

	// Token: 0x17000738 RID: 1848
	// (get) Token: 0x06007D8B RID: 32139 RVA: 0x0028BD1F File Offset: 0x00289F1F
	// (set) Token: 0x06007D8C RID: 32140 RVA: 0x0028BD27 File Offset: 0x00289F27
	public bool IsVCPurchase { get; set; }

	// Token: 0x06007D8D RID: 32141 RVA: 0x0028BD30 File Offset: 0x00289F30
	public HearthstoneCheckoutTransactionData(long productID, string currencyCode, uint quantity, bool isVCPurchase)
	{
		this.ProductID = productID;
		this.CurrencyCode = currencyCode;
		this.Quantity = quantity;
		this.IsVCPurchase = isVCPurchase;
	}
}
