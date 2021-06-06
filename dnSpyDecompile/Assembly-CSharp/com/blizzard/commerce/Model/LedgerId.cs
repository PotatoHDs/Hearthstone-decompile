using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	// Token: 0x02000B69 RID: 2921
	[Serializable]
	public class LedgerId
	{
		// Token: 0x06009AA6 RID: 39590 RVA: 0x0031BF78 File Offset: 0x0031A178
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class LedgerId {\n");
			stringBuilder.Append("  accountId: ").Append(this.accountId).Append("\n");
			stringBuilder.Append("  ledgerKey: ").Append(this.ledgerKey).Append("\n");
			stringBuilder.Append("  currencyCode: ").Append(this.currencyCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0400805E RID: 32862
		public int accountId;

		// Token: 0x0400805F RID: 32863
		public LedgerKey ledgerKey;

		// Token: 0x04008060 RID: 32864
		public string currencyCode;
	}
}
