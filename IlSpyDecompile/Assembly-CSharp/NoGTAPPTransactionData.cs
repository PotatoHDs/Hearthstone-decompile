using PegasusUtil;

public class NoGTAPPTransactionData
{
	public ProductType Product { get; set; }

	public int ProductData { get; set; }

	public int Quantity { get; set; }

	public NoGTAPPTransactionData()
	{
		Product = ProductType.PRODUCT_TYPE_UNKNOWN;
		ProductData = 0;
		Quantity = 0;
	}
}
