using System;

// Token: 0x02000706 RID: 1798
public abstract class BuyProductEventArgs
{
	// Token: 0x170005EC RID: 1516
	// (get) Token: 0x060064B6 RID: 25782
	public abstract CurrencyType PaymentCurrency { get; }

	// Token: 0x040053BF RID: 21439
	public int quantity;
}
