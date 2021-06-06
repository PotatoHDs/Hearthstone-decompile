using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B67 RID: 2919
	[Serializable]
	public class Journal
	{
		// Token: 0x06009AA2 RID: 39586 RVA: 0x0031BDAC File Offset: 0x00319FAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class Journal {\n");
			stringBuilder.Append("  reason: ").Append(this.reason).Append("\n");
			stringBuilder.Append("  storeProductId: ").Append(this.storeProductId).Append("\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(this.pointOfSaleId).Append("\n");
			stringBuilder.Append("  globalOrderId: ").Append(this.globalOrderId).Append("\n");
			stringBuilder.Append("  journalId: ").Append(this.journalId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(this.storeId).Append("\n");
			stringBuilder.Append("  productType: ").Append(this.productType).Append("\n");
			stringBuilder.Append("  status: ").Append(this.status).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008053 RID: 32851
		public string reason;

		// Token: 0x04008054 RID: 32852
		public string storeProductId;

		// Token: 0x04008055 RID: 32853
		public int pointOfSaleId;

		// Token: 0x04008056 RID: 32854
		public string globalOrderId;

		// Token: 0x04008057 RID: 32855
		public string journalId;

		// Token: 0x04008058 RID: 32856
		public int gameServiceRegionId;

		// Token: 0x04008059 RID: 32857
		public string storeId;

		// Token: 0x0400805A RID: 32858
		public int productType;

		// Token: 0x0400805B RID: 32859
		public int status;
	}
}
