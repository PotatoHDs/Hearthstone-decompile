public class BuyPmtProductEventArgs : BuyProductEventArgs
{
	public readonly long pmtProductId;

	public readonly CurrencyType paymentCurrency;

	public override CurrencyType PaymentCurrency => paymentCurrency;

	public BuyPmtProductEventArgs(long pmtProductId, CurrencyType paymentCurrency, int quantity)
	{
		this.pmtProductId = pmtProductId;
		base.quantity = quantity;
		this.paymentCurrency = paymentCurrency;
	}

	public BuyPmtProductEventArgs(Network.Bundle bundle, CurrencyType paymentCurrency, int quantity)
		: this(bundle.PMTProductID.GetValueOrDefault(), paymentCurrency, quantity)
	{
	}
}
