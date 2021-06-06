public class HearthstoneCheckoutTransactionData
{
	public string TransactionID { get; set; }

	public long ProductID { get; set; }

	public string CurrencyCode { get; set; }

	public string ErrorCodes { get; set; }

	public uint Quantity { get; set; }

	public bool IsVCPurchase { get; set; }

	public HearthstoneCheckoutTransactionData(long productID, string currencyCode, uint quantity, bool isVCPurchase)
	{
		ProductID = productID;
		CurrencyCode = currencyCode;
		Quantity = quantity;
		IsVCPurchase = isVCPurchase;
	}
}
