using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B57 RID: 2903
	[Serializable]
	public class CreateJournalRequest
	{
		// Token: 0x06009A83 RID: 39555 RVA: 0x0031B488 File Offset: 0x00319688
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CreateJournalRequest {\n");
			stringBuilder.Append("  storeProductId: ").Append(this.storeProductId).Append("\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(this.pointOfSaleId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  storeId: ").Append(this.storeId).Append("\n");
			stringBuilder.Append("  productType: ").Append(this.productType).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008020 RID: 32800
		public string storeProductId;

		// Token: 0x04008021 RID: 32801
		public int pointOfSaleId;

		// Token: 0x04008022 RID: 32802
		public int gameServiceRegionId;

		// Token: 0x04008023 RID: 32803
		public string storeId;

		// Token: 0x04008024 RID: 32804
		public int productType;
	}
}
