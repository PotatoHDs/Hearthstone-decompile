using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B59 RID: 2905
	[Serializable]
	public class CreationParameters
	{
		// Token: 0x06009A87 RID: 39559 RVA: 0x0031B5D0 File Offset: 0x003197D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CreationParameters {\n");
			stringBuilder.Append("  pointOfSaleId: ").Append(this.pointOfSaleId).Append("\n");
			stringBuilder.Append("  gameServiceRegionId: ").Append(this.gameServiceRegionId).Append("\n");
			stringBuilder.Append("  productType: ").Append(this.productType).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008027 RID: 32807
		public int pointOfSaleId;

		// Token: 0x04008028 RID: 32808
		public int gameServiceRegionId;

		// Token: 0x04008029 RID: 32809
		public int productType;
	}
}
