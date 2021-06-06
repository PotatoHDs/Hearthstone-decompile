using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B77 RID: 2935
	[Serializable]
	public class ProductPrice
	{
		// Token: 0x06009AC2 RID: 39618 RVA: 0x0031C894 File Offset: 0x0031AA94
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductPrice {\n");
			stringBuilder.Append("  currencyTypeId: ").Append(this.currencyTypeId).Append("\n");
			stringBuilder.Append("  localizedOriginalPrice: ").Append(this.localizedOriginalPrice).Append("\n");
			stringBuilder.Append("  originalPrice: ").Append(this.originalPrice).Append("\n");
			stringBuilder.Append("  currentPrice: ").Append(this.currentPrice).Append("\n");
			stringBuilder.Append("  localizedCurrentPrice: ").Append(this.localizedCurrentPrice).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(this.currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04008090 RID: 32912
		public int currencyTypeId;

		// Token: 0x04008091 RID: 32913
		public string localizedOriginalPrice;

		// Token: 0x04008092 RID: 32914
		public string originalPrice;

		// Token: 0x04008093 RID: 32915
		public string currentPrice;

		// Token: 0x04008094 RID: 32916
		public string localizedCurrentPrice;

		// Token: 0x04008095 RID: 32917
		public string currencyCode;
	}
}
