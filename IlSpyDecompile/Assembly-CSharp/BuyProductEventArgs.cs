public abstract class BuyProductEventArgs
{
	public int quantity;

	public abstract CurrencyType PaymentCurrency { get; }
}
