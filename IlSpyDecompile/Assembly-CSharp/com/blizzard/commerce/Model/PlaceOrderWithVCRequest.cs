using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class PlaceOrderWithVCRequest
	{
		public int pointOfSaleId;

		public int quantity;

		public int productId;

		public int gameServiceRegionId;

		public string externalTransactionId;

		public string currencyCode;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class PlaceOrderWithVCRequest {\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(pointOfSaleId).Append("\n");
			stringBuilder.Append("  quantity: ").Append(quantity).Append("\n");
			stringBuilder.Append("  productId: ").Append(productId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  externalTransactionId: ").Append(externalTransactionId).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
