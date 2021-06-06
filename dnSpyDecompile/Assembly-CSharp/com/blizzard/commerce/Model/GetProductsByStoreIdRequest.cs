using System;
using System.Collections.Generic;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B63 RID: 2915
	[Serializable]
	public class GetProductsByStoreIdRequest
	{
		// Token: 0x06009A9A RID: 39578 RVA: 0x0031B9F4 File Offset: 0x00319BF4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class GetProductsByStoreIdRequest {\n");
			stringBuilder.Append("  paginationToken: ").Append(this.paginationToken).Append("\n");
			stringBuilder.Append("  externalPlatformId: ").Append(this.externalPlatformId).Append("\n");
			stringBuilder.Append("  currencyCodes: ").Append(this.currencyCodes).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(this.storeId).Append("\n");
			stringBuilder.Append("  paginationSize: ").Append(this.paginationSize).Append("\n");
			stringBuilder.Append("  locale: ").Append(this.locale).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400803C RID: 32828
		public string paginationToken;

		// Token: 0x0400803D RID: 32829
		public int externalPlatformId;

		// Token: 0x0400803E RID: 32830
		public List<string> currencyCodes;

		// Token: 0x0400803F RID: 32831
		public int gameServiceRegionId;

		// Token: 0x04008040 RID: 32832
		public int storeId;

		// Token: 0x04008041 RID: 32833
		public int paginationSize;

		// Token: 0x04008042 RID: 32834
		public string locale;
	}
}
