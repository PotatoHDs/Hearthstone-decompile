using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class Journal
	{
		public string reason;

		public string storeProductId;

		public int pointOfSaleId;

		public string globalOrderId;

		public string journalId;

		public int gameServiceRegionId;

		public string storeId;

		public int productType;

		public int status;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Journal {\n");
			stringBuilder.Append("  reason: ").Append(reason).Append("\n");
			stringBuilder.Append("  storeProductId: ").Append(storeProductId).Append("\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(pointOfSaleId).Append("\n");
			stringBuilder.Append("  globalOrderId: ").Append(globalOrderId).Append("\n");
			stringBuilder.Append("  journalId: ").Append(journalId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(storeId).Append("\n");
			stringBuilder.Append("  productType: ").Append(productType).Append("\n");
			stringBuilder.Append("  status: ").Append(status).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
