using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B6F RID: 2927
	[Serializable]
	public class PlaceOrderWithVCRequest
	{
		// Token: 0x06009AB2 RID: 39602 RVA: 0x0031C268 File Offset: 0x0031A468
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class PlaceOrderWithVCRequest {\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(this.pointOfSaleId).Append("\n");
			stringBuilder.Append("  quantity: ").Append(this.quantity).Append("\n");
			stringBuilder.Append("  productId: ").Append(this.productId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  externalTransactionId: ").Append(this.externalTransactionId).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(this.currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400806C RID: 32876
		public int pointOfSaleId;

		// Token: 0x0400806D RID: 32877
		public int quantity;

		// Token: 0x0400806E RID: 32878
		public int productId;

		// Token: 0x0400806F RID: 32879
		public int gameServiceRegionId;

		// Token: 0x04008070 RID: 32880
		public string externalTransactionId;

		// Token: 0x04008071 RID: 32881
		public string currencyCode;
	}
}
