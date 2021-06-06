using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B65 RID: 2917
	[Serializable]
	public class GetProductsRequest
	{
		// Token: 0x06009A9E RID: 39582 RVA: 0x0031BBC0 File Offset: 0x00319DC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsRequest {\n");
			stringBuilder.Append("  paginationToken: ").Append(this.paginationToken).Append("\n");
			stringBuilder.Append("  externalPlatformId: ").Append(this.externalPlatformId).Append("\n");
			stringBuilder.Append("  productIds: ").Append(this.productIds).Append("\n");
			stringBuilder.Append("  currencyCodes: ").Append(this.currencyCodes).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(this.storeId).Append("\n");
			stringBuilder.Append("  paginationSize: ").Append(this.paginationSize).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008047 RID: 32839
		public string paginationToken;

		// Token: 0x04008048 RID: 32840
		public int externalPlatformId;

		// Token: 0x04008049 RID: 32841
		public List<int> productIds;

		// Token: 0x0400804A RID: 32842
		public List<string> currencyCodes;

		// Token: 0x0400804B RID: 32843
		public int gameServiceRegionId;

		// Token: 0x0400804C RID: 32844
		public int storeId;

		// Token: 0x0400804D RID: 32845
		public int paginationSize;

		// Token: 0x0400804E RID: 32846
		public string locale;
	}
}
