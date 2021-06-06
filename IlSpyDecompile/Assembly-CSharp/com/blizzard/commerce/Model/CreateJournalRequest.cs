using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class CreateJournalRequest
	{
		public string storeProductId;

		public int pointOfSaleId;

		public int gameServiceRegionId;

		public string storeId;

		public int productType;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CreateJournalRequest {\n");
			stringBuilder.Append("  storeProductId: ").Append(storeProductId).Append("\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(pointOfSaleId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(storeId).Append("\n");
			stringBuilder.Append("  productType: ").Append(productType).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
