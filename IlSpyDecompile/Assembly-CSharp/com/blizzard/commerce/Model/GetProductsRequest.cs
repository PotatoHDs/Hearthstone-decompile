using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class GetProductsRequest
	{
		public string paginationToken;

		public int externalPlatformId;

		public List<int> productIds;

		public List<string> currencyCodes;

		public int gameServiceRegionId;

		public int storeId;

		public int paginationSize;

		public string locale;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsRequest {\n");
			stringBuilder.Append("  paginationToken: ").Append(paginationToken).Append("\n");
			stringBuilder.Append("  externalPlatformId: ").Append(externalPlatformId).Append("\n");
			stringBuilder.Append("  productIds: ").Append(productIds).Append("\n");
			stringBuilder.Append("  currencyCodes: ").Append(currencyCodes).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(storeId).Append("\n");
			stringBuilder.Append("  paginationSize: ").Append(paginationSize).Append("\n");
			stringBuilder.Append("  locale: ").Append(locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
