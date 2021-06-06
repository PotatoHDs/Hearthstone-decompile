public class BuyNoGTAPPEventArgs : BuyProductEventArgs
{
	public readonly NoGTAPPTransactionData transactionData;

	public override CurrencyType PaymentCurrency => CurrencyType.GOLD;

	public BuyNoGTAPPEventArgs(NoGTAPPTransactionData data)
	{
		transactionData = data;
		quantity = data.Quantity;
	}
}
