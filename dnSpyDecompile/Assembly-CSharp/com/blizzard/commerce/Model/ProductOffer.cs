using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B76 RID: 2934
	[Serializable]
	public class ProductOffer
	{
		// Token: 0x06009AC0 RID: 39616 RVA: 0x0031C820 File Offset: 0x0031AA20
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class ProductOffer {\n");
			stringBuilder.Append("  offerDisplayEndTimeMs: ").Append(this.offerDisplayEndTimeMs).Append("\n");
			stringBuilder.Append("  offerPurchaseEndTimeMs: ").Append(this.offerPurchaseEndTimeMs).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400808E RID: 32910
		public int offerDisplayEndTimeMs;

		// Token: 0x0400808F RID: 32911
		public int offerPurchaseEndTimeMs;
	}
}
