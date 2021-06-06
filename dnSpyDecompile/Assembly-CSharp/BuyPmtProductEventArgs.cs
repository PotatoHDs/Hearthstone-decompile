using System;

// Token: 0x020006EC RID: 1772
public class BuyPmtProductEventArgs : BuyProductEventArgs
{
	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00202F18 File Offset: 0x00201118
	public override CurrencyType PaymentCurrency
	{
		get
		{
			return this.paymentCurrency;
		}
	}

	// Token: 0x060062B2 RID: 25266 RVA: 0x00202F20 File Offset: 0x00201120
	public BuyPmtProductEventArgs(long pmtProductId, CurrencyType paymentCurrency, int quantity)
	{
		this.pmtProductId = pmtProductId;
		this.quantity = quantity;
		this.paymentCurrency = paymentCurrency;
	}

	// Token: 0x060062B3 RID: 25267 RVA: 0x00202F40 File Offset: 0x00201140
	public BuyPmtProductEventArgs(Network.Bundle bundle, CurrencyType paymentCurrency, int quantity) : this(bundle.PMTProductID.GetValueOrDefault(), paymentCurrency, quantity)
	{
	}

	// Token: 0x040051FC RID: 20988
	public readonly long pmtProductId;

	// Token: 0x040051FD RID: 20989
	public readonly CurrencyType paymentCurrency;
}
