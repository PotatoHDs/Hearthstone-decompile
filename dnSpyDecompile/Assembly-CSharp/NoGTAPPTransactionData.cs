using System;
using PegasusUtil;

// Token: 0x02000715 RID: 1813
public class NoGTAPPTransactionData
{
	// Token: 0x0600652D RID: 25901 RVA: 0x0020FE3A File Offset: 0x0020E03A
	public NoGTAPPTransactionData()
	{
		this.Product = ProductType.PRODUCT_TYPE_UNKNOWN;
		this.ProductData = 0;
		this.Quantity = 0;
	}

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x0600652E RID: 25902 RVA: 0x0020FE57 File Offset: 0x0020E057
	// (set) Token: 0x0600652F RID: 25903 RVA: 0x0020FE5F File Offset: 0x0020E05F
	public ProductType Product { get; set; }

	// Token: 0x170005F8 RID: 1528
	// (get) Token: 0x06006530 RID: 25904 RVA: 0x0020FE68 File Offset: 0x0020E068
	// (set) Token: 0x06006531 RID: 25905 RVA: 0x0020FE70 File Offset: 0x0020E070
	public int ProductData { get; set; }

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x06006532 RID: 25906 RVA: 0x0020FE79 File Offset: 0x0020E079
	// (set) Token: 0x06006533 RID: 25907 RVA: 0x0020FE81 File Offset: 0x0020E081
	public int Quantity { get; set; }
}
